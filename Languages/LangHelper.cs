namespace SKitLs.Utils.Localizations.Languages
{
    /// <summary>
    /// Provides helper methods for working with language codes.
    /// </summary>
    public static class LangHelper
    {

        /// <summary>
        /// Converts a language tag to a corresponding <see cref="LanguageCode"/> enumeration value.
        /// </summary>
        /// <param name="tag">The language tag to convert.</param>
        /// <returns>The <see cref="LanguageCode"/> enumeration value corresponding to the provided language tag.</returns>
        /// <inheritdoc cref="Enum.Parse{TEnum}(string, bool)"/>
        public static LanguageCode FromTag(string tag) => Enum.Parse<LanguageCode>(tag, true);
    }
}