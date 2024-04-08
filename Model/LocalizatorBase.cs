using SKitLs.Utils.Localizations.Languages;
using SKitLs.Utils.Localizations.Localizators;

namespace SKitLs.Utils.Localizations.Model
{
    public abstract class LocalizatorBase : ILocalizator
    {
        /// <inheritdoc/>
        public string NotDefinedKey { get; set; } = "local.KeyNotDefined";

        /// <inheritdoc/>
        public LanguageCode DefaultLanguage { get; set; }

        /// <inheritdoc/>
        public string LocalsPath { get; private set; }

        public LocalizatorBase(string localsPath, LanguageCode defaultLanguage)
        {
            LocalsPath = localsPath ?? throw new ArgumentNullException(nameof(localsPath));
            DefaultLanguage = defaultLanguage;
        }

        public virtual string? ResolveString(LanguageCode? lang, string key, bool resolveDefault, params string?[] format)
            => InternalResolveString(lang, key, resolveDefault, format);

        public virtual string ResolveStringOrFallback(LanguageCode? lang, string key, bool resolveDefault, params string?[] format)
            => InternalResolveString(lang, key, resolveDefault, format) ?? FallbackString(lang, key, resolveDefault, format);

        protected abstract string? InternalResolveString(LanguageCode? lang, string key, bool resolveDefault, params string?[] format);

        protected virtual string FallbackString(LanguageCode? lang, string key, bool resolveDefault, params string?[] format)
        {
            var reply = InternalResolveString(lang, NotDefinedKey, resolveDefault, Enum.GetName(lang ?? DefaultLanguage), key, LocalsPath)
                ?? $"String Data is not defined ({key}:{Enum.GetName(lang ?? DefaultLanguage)}). Format params: {string.Join(", ", format)}";
            return format.Length > 0 ? reply[..(reply.Length - 2)] + "." : reply + "None";
        }
    }
}
