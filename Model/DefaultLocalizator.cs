using Newtonsoft.Json;
using SKitLs.Utils.Localizations.Prototype;

namespace SKitLs.Utils.Localizations.Model
{
    /// <summary>
    /// Represents a specialized service that enables string localization based on the provided language key.
    /// <para>
    /// Default realization of an <see cref="ILocalizator"/> interface.
    /// </para>
    /// </summary>
    public class DefaultLocalizator : ILocalizator
    {
        /// <summary>
        /// Determines localization resource files extension.
        /// </summary>
        public const string LocalExtension = ".json";

        /// <summary>
        /// Represents the key indicating that the requested string is not defined in the specified language.
        /// </summary>
        public string NotDefinedKey => "local.KeyNotDefined";
        /// <summary>
        /// Represents the path to the localization resource files.
        /// </summary>
        public string LocalsPath { get; private set; }
        private Dictionary<LangKey, Dictionary<string, string>> Localizations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultLocalizator"/> class with the specified localization path.
        /// </summary>
        /// <param name="localsPath">The path to the localization resource files.</param>
        public DefaultLocalizator(string localsPath)
        {
            LocalsPath = localsPath;
            Localizations = new();
            if (!Directory.Exists(LocalsPath)) Directory.CreateDirectory(LocalsPath);
            var files = Directory.GetFiles(LocalsPath).Select(x => new FileInfo(x)).Where(x => x.Extension == LocalExtension);
            foreach (LangKey lang in Enum.GetValues(typeof(LangKey)))
            {
                string? langName = Enum.GetName(typeof(LangKey), lang);
                if (langName is null) throw new ArgumentNullException(nameof(langName));
                foreach (var lFile in files.Where(x => x.Name.StartsWith(langName, true, null)))
                {
                    using var reader = new StreamReader(lFile.FullName);
                    var json = reader.ReadToEnd();
                    var langCollection = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                        ?? throw new Exception($"Was not able to deserialize package with {langName} language ({lFile.FullName})");
                    if (Localizations.ContainsKey(lang))
                    {
                        foreach (var pair in langCollection)
                        {
                            Localizations[lang].Add(pair.Key, pair.Value);
                        }
                    }
                    else Localizations.Add(lang, langCollection);
                }
            }
        }

        /// <summary>
        /// Resolves the localized string for the specified language key and key identifier, with optional format parameters.
        /// </summary>
        /// <param name="lang">The language key for localization.</param>
        /// <param name="key">The unique identifier for the localized string.</param>
        /// <param name="format">Optional. An array of strings to be formatted into the resolved localized string.</param>
        /// <returns>
        /// The localized string based on the specified language key and key identifier, or a fallback string
        /// if the requested string is not defined in the specified language.
        /// </returns>
        public string ResolveString(LangKey lang, string key, params string?[] format)
            => InternalResolveString(lang, key, format) ?? FallbackString(lang, key, format);
        private string? InternalResolveString(LangKey lang, string key, params string?[] format)
        {
            if (!(Localizations.ContainsKey(lang) && Localizations[lang].ContainsKey(key)))
                return lang != LangKey.EN
                    ? InternalResolveString(LangKey.EN, key, format)
                    : null;

            return string.Format(Localizations[lang][key], format);
        }
        private string FallbackString(LangKey lang, string key, params string?[] format)
        {
            var reply = InternalResolveString(LangKey.EN, NotDefinedKey, Enum.GetName(lang), key, LocalsPath)
                ?? "String Data is not defined. Format params: ";
            foreach (var param in format)
                reply += $"{param}, ";
            return format.Length > 0 ? reply[..(reply.Length - 2)] + "." : reply + "None";
        }
    }
}