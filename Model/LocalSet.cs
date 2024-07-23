namespace SKitLs.Utils.Localizations.Model
{
    /// <summary>
    /// Represents a set of localization data, including a key for the localized string and optional format parameters.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="LocalSet"/> struct with the specified localization key and format parameters.
    /// </remarks>
    /// <param name="localizationKey">The key for the localized string.</param>
    /// <param name="format">An array of objects to be formatted into the localized string.</param>
    public struct LocalSet(string localizationKey, object?[]? format = null)
    {
        /// <summary>
        /// Gets or sets the key for the localized string.
        /// </summary>
        public string LocalizationKey { get; set; } = localizationKey;

        /// <summary>
        /// Gets or sets an array of objects to be formatted into the localized string.
        /// </summary>
        public string?[] Format { get; set; } = format is not null ? format.Select(x => x?.ToString()).ToArray() : [];

        /// <summary>
        /// Implicitly converts a string key to a <see cref="LocalSet"/> instance.
        /// </summary>
        /// <param name="key">The localization key to be converted.</param>
        public static implicit operator LocalSet(string key) => new(key, null);
    }
}