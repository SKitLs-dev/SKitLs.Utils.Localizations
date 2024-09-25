using Newtonsoft.Json;
using SKitLs.Utils.Localizations.Languages;
using SKitLs.Utils.Localizations.Localizators;

namespace SKitLs.Utils.Localizations.Model
{
    /// <summary>
    /// Represents a specialized service that implements the <see cref="ILocalizator"/> interface, providing string localization based on the provided language key.
    /// <para/>
    /// This implementation, <see cref="StoredLocalizator"/>, optimizes performance by preloading all localizations during initialization and storing them as strings,
    /// trading memory consumption for increased speed.
    /// </summary>
    public class StoredLocalizator : LocalizatorBase
    {
        /// <summary>
        /// Determines the extension of localization resource files.
        /// </summary>
        public const string LocalExtension = ".json";

        /// <summary>
        /// Dictionary storing localizations for each language code.
        /// </summary>
        private Dictionary<string, Dictionary<LanguageCode, string>> Localizations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredLocalizator"/> class with the specified localization path.
        /// </summary>
        /// <param name="localsPath">The path to the localization resource files.</param>
        /// <param name="defaultLanguage">The path to the localization resource files.</param>
        public StoredLocalizator(string localsPath = "Resources/Locals", LanguageCode defaultLanguage = LanguageCode.EN) : base(localsPath, defaultLanguage)
        {
            Localizations = [];
            LoadContent();
        }

        /// <inheritdoc/>
        public override IEnumerable<LanguageCode> GetSupportedLanguages() => Localizations.Values.SelectMany(x => x.Keys).Distinct();

        private void LoadContent()
        {
            if (!Directory.Exists(LocalsPath))
                Directory.CreateDirectory(LocalsPath);

            var files = Directory.GetFiles(LocalsPath).Select(x => new FileInfo(x)).Where(x => x.Extension == LocalExtension);

            foreach (LanguageCode lang in Enum.GetValues(typeof(LanguageCode)))
            {
                var langName = Enum.GetName(typeof(LanguageCode), lang);
                if (langName is null)
                    throw new ArgumentNullException(nameof(langName));
                
                foreach (var lFile in files.Where(x => x.Name.StartsWith(langName, true, null)))
                {
                    using var reader = new StreamReader(lFile.FullName);
                    var json = reader.ReadToEnd();
                    var langCollection = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                        ?? throw new Exception($"Was not able to deserialize package with {langName} language ({lFile.FullName})");

                    foreach (var lk in langCollection)
                    {
                        if (!Localizations.TryGetValue(lk.Key, out Dictionary<LanguageCode, string>? locals))
                        {
                            locals = [];
                            Localizations.Add(lk.Key, locals);
                        }
                        locals.Add(lang, lk.Value);
                    }
                }
            }
        }

        /// <inheritdoc/>
        protected override string? InternalResolveString(LanguageCode? lang, string key, bool resolveDefault, params string?[] format)
        {
            if (Localizations.TryGetValue(key, out Dictionary<LanguageCode, string>? locals))
            {
                if (lang is not null && locals.TryGetValue(lang.Value, out string? local))
                    return string.Format(local, format);
                else if (resolveDefault && locals.TryGetValue(DefaultLanguage, out string? defaultLocal))
                    return string.Format(defaultLocal, format);
            }
            return null;
        }
    }
}