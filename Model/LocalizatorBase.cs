using SKitLs.Utils.Localizations.Languages;
using SKitLs.Utils.Localizations.Localizators;

namespace SKitLs.Utils.Localizations.Model
{
    /// <summary>
    /// Represents the base class for a localizator, providing functionality to resolve localized strings.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="LocalizatorBase"/> class with the specified path to localization resources and default language code.
    /// </remarks>
    /// <param name="localsPath">The path to the localization resources.</param>
    /// <param name="defaultLanguage">The default language code for localization.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="localsPath"/> is null.</exception>
    public abstract class LocalizatorBase(string localsPath, LanguageCode defaultLanguage) : ILocalizator
    {
        /// <inheritdoc/>
        public string NotDefinedKey { get; set; } = "local.KeyNotDefined";

        /// <inheritdoc/>
        public LanguageCode DefaultLanguage { get; set; } = defaultLanguage;

        /// <inheritdoc/>
        public string LocalsPath { get; private set; } = localsPath ?? throw new ArgumentNullException(nameof(localsPath));

        /// <inheritdoc/>
        public virtual string? ResolveString(LanguageCode? lang, string key, bool resolveDefault, params string?[] format) => InternalResolveString(lang, key, resolveDefault, format);

        /// <inheritdoc/>
        public virtual string? ResolveString(LanguageCode? lang, LocalSet localSet, bool resolveDefault) => ResolveString(lang, localSet.LocalizationKey, resolveDefault, localSet.Format);

        /// <inheritdoc/>
        public virtual string ResolveStringOrFallback(LanguageCode? lang, string key, bool resolveDefault, params string?[] format) => InternalResolveString(lang, key, resolveDefault, format) ?? FallbackString(lang, key, resolveDefault, format);

        /// <inheritdoc/>
        public virtual string ResolveStringOrFallback(LanguageCode? lang, LocalSet localSet, bool resolveDefault) => ResolveStringOrFallback(lang, localSet.LocalizationKey, resolveDefault, localSet.Format);

        /// <summary>
        /// Resolves the localized string for the specified language key and key identifier, with optional format parameters.
        /// </summary>
        /// <param name="lang">The language key for localization.</param>
        /// <param name="key">The unique identifier for the localized string.</param>
        /// <param name="resolveDefault">Specifies whether to resolve the key in the default language if not found in the specified language.</param>
        /// <param name="format">Optional. An array of strings to be formatted into the resolved localized string.</param>
        /// <returns>The localized string based on the specified language key and key identifier, or null if not found.</returns>
        protected abstract string? InternalResolveString(LanguageCode? lang, string key, bool resolveDefault, params string?[] format);

        /// <summary>
        /// Generates a fallback string if the localized string is not found.
        /// </summary>
        /// <param name="lang">The language key for localization.</param>
        /// <param name="key">The unique identifier for the localized string.</param>
        /// <param name="resolveDefault">Specifies whether to resolve the key in the default language if not found in the specified language.</param>
        /// <param name="format">Optional. An array of strings to be formatted into the resolved localized string.</param>
        /// <returns>The fallback string based on the specified parameters.</returns>
        protected virtual string FallbackString(LanguageCode? lang, string key, bool resolveDefault, params string?[] format)
        {
            var reply = InternalResolveString(lang, NotDefinedKey, resolveDefault, Enum.GetName(lang ?? DefaultLanguage), key, LocalsPath)
                ?? $"String Data is not defined ({key}:{Enum.GetName(lang ?? DefaultLanguage)}). Format params: {string.Join(", ", format)}";
            return format.Length > 0 ? reply[..(reply.Length - 2)] + "." : reply + "None";
        }
    }
}