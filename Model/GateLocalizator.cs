using Newtonsoft.Json;
using SKitLs.Utils.Localizations.Languages;

namespace SKitLs.Utils.Localizations.Model
{
    public class GateLocalizator : LocalizatorBase
    {
        /// <summary>
        /// Determines the extension of localization resource files.
        /// </summary>
        public const string LocalExtension = ".json";

        public GateLocalizator(string localsPath, LanguageCode defaultLanguage = LanguageCode.EN) : base(localsPath, defaultLanguage)
        { }

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