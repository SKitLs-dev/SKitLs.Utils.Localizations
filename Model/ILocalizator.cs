using SKitLs.Utils.Localizations.Languages;

namespace SKitLs.Utils.Localizations.Localizators
{
    /// <summary>
    /// Provides a specialized mechanism for localizing strings based on the given language key.
    /// </summary>
    public interface ILocalizator
    {
        /// <summary>
        /// Gets the key indicating that the requested string is not defined in the specified language.
        /// </summary>
        public string NotDefinedKey { get; }

        /// <summary>
        /// Gets or sets the default language code.
        /// </summary>
        public LanguageCode DefaultLanguage { get; set; }

        /// <summary>
        /// Gets the path to the localization resource files.
        /// </summary>
        public string LocalsPath { get; }

        /// <summary>
        /// Resolves the localized string for the specified language key and key identifier, with optional format parameters.
        /// </summary>
        /// <param name="lang">The language key for localization.</param>
        /// <param name="key">The unique identifier for the localized string.</param>
        /// <param name="format">Optional. An array of strings to be formatted into the resolved localized string.</param>
        /// <returns>The localized string based on the specified language key and key identifier.</returns>
        public string? ResolveString(LanguageCode? lang, string key, params string?[] format);

        /// <summary>
        /// Resolves the localized string for the specified language key and key identifier, with optional format parameters,
        /// or returns a fallback string if the localized string is not found.
        /// </summary>
        /// <param name="lang">The language key for localization.</param>
        /// <param name="key">The unique identifier for the localized string.</param>
        /// <param name="format">Optional. An array of strings to be formatted into the resolved localized string.</param>
        /// <returns>The localized string based on the specified language key and key identifier, or a fallback string if the localized string is not found.</returns>
        public string ResolveStringOrFallback(LanguageCode? lang, string key, params string?[] format);
    }
}