using Newtonsoft.Json;
using SKitLs.Utils.Localizations.Languages;

namespace SKitLs.Utils.Localizations.Model
{
    /// <summary>
    /// Represents a specialized service that implements the <see cref="ILocalizator"/> interface, providing string localization based on the provided language key.
    /// <para/>
    /// This implementation, <see cref="GateLocalizator"/>, optimizes memory usage by accessing local files on need, trading speed for better memory usage.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="StoredLocalizator"/> class with the specified localization path.
    /// </remarks>
    /// <param name="localsPath">The path to the localization resource files.</param>
    /// <param name="defaultLanguage">The path to the localization resource files.</param>
    public class GateLocalizator(string localsPath, LanguageCode defaultLanguage = LanguageCode.EN) : LocalizatorBase(localsPath, defaultLanguage)
    {
        /// <summary>
        /// Determines the extension of localization resource files.
        /// </summary>
        public const string LocalExtension = ".json";

        /// <inheritdoc/>
        protected override string? InternalResolveString(LanguageCode? lang, string key, bool resolveDefault, params string?[] format)
        {
            if (!Directory.Exists(LocalsPath))
                Directory.CreateDirectory(LocalsPath);

            var langName = string.Empty;
            if (lang.HasValue)
            {
                langName = lang.Value.ToString();
            }
            else if (resolveDefault)
            {
                langName = DefaultLanguage.ToString();
            }
            else
                return null;

            var files = Directory.GetFiles(LocalsPath)
                .Select(x => new FileInfo(x))
                .Where(x => x.Extension == LocalExtension)
                .Where(x => x.Name.StartsWith(langName, true, null));

            foreach (var lFile in files)
            {
                using var reader = new StreamReader(lFile.FullName);
                var json = reader.ReadToEnd();
                var langCollection = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                    ?? throw new Exception($"Was not able to deserialize package with {langName} language ({lFile.FullName})");

                if (langCollection.TryGetValue(key, out string? resolved))
                    return resolved;
            }

            if (resolveDefault && lang != DefaultLanguage)
                return InternalResolveString(DefaultLanguage, key, resolveDefault, format);
            return null;
        }
    }
}