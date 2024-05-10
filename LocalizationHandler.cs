using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace ItemLibrary
{
    public static class LocalizationHandler
    {
        public static Dictionary<Locale, Dictionary<string, string>> LanguageDictionaries = [];
        public static Dictionary<string, string>? CurrentLanguageDictionary;

        #region Enum Overload

        /// <summary>
        /// Adds a translation for specified locale.
        /// </summary>
        /// <param name="locale">Locale to add the key and translation to.</param>
        /// <param name="key">LocalizationKey to use as the key for the translation.</param>
        /// <param name="translation">The translation you can get from the key.</param>
        public static void AddKeyTranslationForLocale(Locale locale, LocalizationKeys.Keys key, string translation)
        {
            if (!LanguageDictionaries.ContainsKey(locale))
            {
                LanguageDictionaries.Add(locale, []);
            }

            LanguageDictionaries[locale].TryAdd(key.ToString(), translation);
        }

        /// <summary>
        /// Adds multiple translations for the specified keys.
        /// </summary>
        /// <param name="locale">Locale to add the key and translation to.</param>
        /// <param name="dictionary">LocalizationKeys.Keys KeyValuePair to add to the Locale</param>
        public static void AddKeyTranslationsForLocale(Locale locale,
            Dictionary<LocalizationKeys.Keys, string> dictionary)
        {
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            foreach (KeyValuePair<LocalizationKeys.Keys, string> kvp in dictionary)
            {
                dictionary2.Add(kvp.Key.ToString(), kvp.Value);
            }

            if (!LanguageDictionaries.ContainsKey(locale))
            {
                
                LanguageDictionaries.Add(locale, dictionary2);
                return;
            }

            foreach (KeyValuePair<string, string> kvp in dictionary2)
            {
                LanguageDictionaries[locale].TryAdd(kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Replaces a translation for specified locale.
        /// </summary>
        /// <param name="locale">Locale to add the key to if it doesn't exist and replace translation from.</param>
        /// <param name="key">LocalizationKey to use as the key for the translation.</param>
        /// <param name="translation">The translation you can get from the key.</param>
        public static void SetTranslationForLocale(Locale locale, LocalizationKeys.Keys key, string translation)
        {
            if (!LanguageDictionaries.ContainsKey(locale))
            {
                LanguageDictionaries.Add(locale, []);
            }

            if (!LanguageDictionaries[locale].TryAdd(key.ToString(), translation))
            {
                LanguageDictionaries[locale][key.ToString()] = translation;
            }
        }

        /// <summary>
        /// Replaces every translation in the given locale for the dictionary.
        /// </summary>
        /// <param name="locale">Locale to add or replace the dictionary.</param>
        /// <param name="dictionary">LocalizationKeys.Keys KeyValuePair that will replace the current one in the Locale</param>
        public static void SetKeyTranslationsDictionaryForLocale(Locale locale,
            Dictionary<LocalizationKeys.Keys, string> dictionary)
        {
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            foreach (KeyValuePair<LocalizationKeys.Keys, string> kvp in dictionary)
            {
                dictionary2.Add(kvp.Key.ToString(), kvp.Value);
            }

            if (!LanguageDictionaries.ContainsKey(locale))
            {
                LanguageDictionaries.Add(locale, dictionary2);
                return;
            }

            LanguageDictionaries[locale] = dictionary2;
        }

        #endregion

        #region String Overload

        /// <summary>
        /// Adds a translation for specified locale.
        /// </summary>
        /// <param name="locale">Locale to add the key and translation to.</param>
        /// <param name="key">string to use as the key for the translation.</param>
        /// <param name="translation">The translation you can get from the key.</param>
        public static void AddKeyTranslationForLocale(Locale locale, string key, string translation)
        {
            if (!LanguageDictionaries.ContainsKey(locale))
            {
                LanguageDictionaries.Add(locale, []);
            }

            LanguageDictionaries[locale].TryAdd(key, translation);
        }

        /// <summary>
        /// Adds multiple translations for the specified keys.
        /// </summary>
        /// <param name="locale">Locale to add the key and translation to.</param>
        /// <param name="dictionary">string KeyValuePair to add to the Locale</param>
        public static void AddKeyTranslationsForLocale(Locale locale,
            Dictionary<string, string> dictionary)
        {
            if (!LanguageDictionaries.ContainsKey(locale))
            {

                LanguageDictionaries.Add(locale, dictionary);
                return;
            }

            foreach (KeyValuePair<string, string> kvp in dictionary)
            {
                LanguageDictionaries[locale].TryAdd(kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Replaces a translation for specified locale.
        /// </summary>
        /// <param name="locale">Locale to add the key to if it doesn't exist and replace translation from.</param>
        /// <param name="key">string to use as the key for the translation.</param>
        /// <param name="translation">The translation you can get from the key.</param>
        public static void SetTranslationForLocale(Locale locale, string key, string translation)
        {
            if (!LanguageDictionaries.ContainsKey(locale))
            {
                LanguageDictionaries.Add(locale, []);
            }

            if (!LanguageDictionaries[locale].TryAdd(key, translation))
            {
                LanguageDictionaries[locale][key] = translation;
            }
        }

        /// <summary>
        /// Replaces every translation in the given locale for the dictionary.
        /// </summary>
        /// <param name="locale">Locale to add or replace the dictionary.</param>
        /// <param name="dictionary">string KeyValuePair that will replace the current one in the Locale</param>
        public static void SetKeyTranslationsDictionaryForLocale(Locale locale,
            Dictionary<string, string> dictionary)
        {
            if (!LanguageDictionaries.ContainsKey(locale))
            {
                LanguageDictionaries.Add(locale, dictionary);
                return;
            }

            LanguageDictionaries[locale] = dictionary;
        }

        #endregion

        /// <summary>
        /// Gets a locale and creates it if it doesn't exist from the LocaleIdentifier
        /// </summary>
        /// <param name="localeIdentifier"></param>
        /// <returns>An existing or a new Locale</returns>
        public static Locale GetCreateLocale(LocaleIdentifier localeIdentifier)
        {
            ILocalesProvider localeProvider = LocalizationSettings.AvailableLocales;
            Locale locale = localeProvider.GetLocale(localeIdentifier);

            if (!locale)
            {
                locale = Locale.CreateLocale(localeIdentifier);
                localeProvider.AddLocale(locale);
            }

            return locale;
        }

        /// <summary>
        /// Gets a locale and creates it if it doesn't exist from the CultureInfo
        /// </summary>
        /// <param name="cultureInfo"></param>
        /// <returns>An existing or a new Locale</returns>
        public static Locale GetCreateLocale(CultureInfo cultureInfo)
        {
            LocaleIdentifier localeIdentifier = new LocaleIdentifier(cultureInfo);
            return GetCreateLocale(localeIdentifier);
        }
    }
}
