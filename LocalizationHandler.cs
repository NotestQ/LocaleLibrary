using System;
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

        /*
         * Some if not all of the following overloads don't call their string counterparts and instead run their own versions of the same code.
         * There is no good reason for this and should be fixed.
         */
        
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
        /// <param name="dictionary">LocalizationKeys.Keys key translation pair to add to the Locale</param>
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
        /// Adds a translation for all available locales.
        /// </summary>
        /// <param name="key">LocalizationKey to use as the key for the translation.</param>
        /// <param name="translation">The translation you can get from the key.</param>
        public static void AddKeyTranslationForAllLocales(LocalizationKeys.Keys key, string translation)
        {
            foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
            {
                AddKeyTranslationForLocale(locale, key, translation);
            }
        }

        /// <summary>
        /// Adds multiple translations for the specified keys to all available locales.
        /// </summary>
        /// <param name="dictionary">LocalizationKeys.Keys key translation pair to add to all available locale</param>
        public static void AddKeyTranslationsForAllLocales(Dictionary<LocalizationKeys.Keys, string> dictionary)
        {
            foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
            {
                AddKeyTranslationsForLocale(locale, dictionary);
            }
        }

        /// <summary>
        /// Replaces a translation for specified locale.
        /// </summary>
        /// <param name="locale">Locale to add the key to if it doesn't exist and replace translation from.</param>
        /// <param name="key">LocalizationKey to use as the key for the translation.</param>
        /// <param name="translation">The translation you can get from the key.</param>
        public static void SetKeyTranslationForLocale(Locale locale, LocalizationKeys.Keys key, string translation)
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
        /// Replaces translations in the given locale for the ones in the dictionary. Does not replace the entire locale dictionary for the one provided.
        /// </summary>
        /// <param name="locale">Locale to add or replace to.</param>
        /// <param name="dictionary">LocalizationKeys.Keys key translation pair that will replace their counterparts in the Locale</param>
        public static void SetKeyTranslationsForLocale(Locale locale,  
            Dictionary<LocalizationKeys.Keys, string> dictionary)
        {
            foreach (KeyValuePair<LocalizationKeys.Keys, string> kvp in dictionary)
            {
                SetKeyTranslationForLocale(locale, kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Replaces a translation for all available locale.
        /// </summary>
        /// <param name="key">LocalizationKey to use as the key for the translation.</param>
        /// <param name="translation">The translation you can get from the key.</param>
        public static void SetKeyTranslationForAllLocales(LocalizationKeys.Keys key, string translation)
        {
            foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
            {
                SetKeyTranslationForLocale(locale, key, translation);
            }
        }

        /// <summary>
        /// Replaces translations in all available locales for the ones in the dictionary. Does not replace the entire locale dictionary for the one provided.
        /// </summary>
        /// <param name="dictionary">LocalizationKeys.Keys key translation pair that will replace their counterparts in all available locales</param>
        public static void SetKeyTranslationsForAllLocales(Dictionary<LocalizationKeys.Keys, string> dictionary)
        {
            foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
            {
                SetKeyTranslationsForLocale(locale, dictionary);
            }
        }

        /// <summary>
        /// Replaces the locale's dictionary for the one provided.
        /// </summary>
        /// <param name="locale">Locale to add or replace the dictionary.</param>
        /// <param name="dictionary">LocalizationKeys.Keys key translation pair that will replace Locale's entire dictionary</param>
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

        /// <summary>
        /// Gets translation string from the current language dictionary given a key, may return null if translation doesn't exist.
        /// </summary>
        /// <param name="key">Where do you want to get the localized string from</param>
        /// <returns>Translation from given key</returns>
        public static string? GetLocalizedString(LocalizationKeys.Keys key)
        {
            return GetLocalizedString(key.ToString());
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
        /// <param name="dictionary">string key translation pair to add to the Locale</param>
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
        /// Adds a translation for all available locales.
        /// </summary>
        /// <param name="key">string to use as the key for the translation.</param>
        /// <param name="translation">The translation you can get from the key.</param>
        public static void AddKeyTranslationForAllLocales(string key, string translation)
        {
            foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
            {
                AddKeyTranslationForLocale(locale, key, translation);
            }
        }

        /// <summary>
        /// Adds multiple translations for the specified keys to all available locales.
        /// </summary>
        /// <param name="dictionary">string key translation pair to add to all available locale</param>
        public static void AddKeyTranslationsForAllLocales(Dictionary<string, string> dictionary)
        {
            foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
            {
                AddKeyTranslationsForLocale(locale, dictionary);
            }
        }

        /// <summary>
        /// Replaces a translation for specified locale.
        /// </summary>
        /// <param name="locale">Locale to add the key to if it doesn't exist and replace translation from.</param>
        /// <param name="key">string to use as the key for the translation.</param>
        /// <param name="translation">The translation you can get from the key.</param>
        public static void SetKeyTranslationForLocale(Locale locale, string key, string translation)
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
        /// Replaces translations in the given locale for the ones in the dictionary. Does not replace the entire locale dictionary for the one provided.
        /// </summary>
        /// <param name="locale">Locale to add or replace to.</param>
        /// <param name="dictionary">string key translation pair that will replace their counterparts in the Locale</param>
        public static void SetKeyTranslationsForLocale(Locale locale,
            Dictionary<string, string> dictionary)
        {
            foreach (KeyValuePair<string, string> kvp in dictionary)
            {
                SetKeyTranslationForLocale(locale, kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Replaces a translation for all available locale.
        /// </summary>
        /// <param name="key">string to use as the key for the translation.</param>
        /// <param name="translation">The translation you can get from the key.</param>
        public static void SetKeyTranslationForAllLocales(string key, string translation)
        {
            foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
            {
                SetKeyTranslationForLocale(locale, key, translation);
            }
        }

        /// <summary>
        /// Replaces translations in all available locales for the ones in the dictionary. Does not replace the entire locale dictionary for the one provided.
        /// </summary>
        /// <param name="dictionary">string key translation pair that will replace their counterparts in all available locales</param>
        public static void SetKeyTranslationsForAllLocales(Dictionary<string, string> dictionary)
        {
            foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
            {
                SetKeyTranslationsForLocale(locale, dictionary);
            }
        }

        /// <summary>
        /// Replaces translations in the given locale for the ones in the dictionary. Does not replace the entire locale dictionary for the one provided.
        /// </summary>
        /// <param name="locale">Locale to add or replace the dictionary.</param>
        /// <param name="dictionary">string key translation pair that will replace the current one in the Locale</param>
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

        /// <summary>
        /// Gets translation string from the current language dictionary given a key, may return null if translation doesn't exist.
        /// </summary>
        /// <param name="key">Where do you want to get the localized string from</param>
        /// <returns>Translation from given key</returns>
        public static string? GetLocalizedString(string key)
        {
            if (LocalizationHandler.CurrentLanguageDictionary != null && LocalizationHandler.CurrentLanguageDictionary.TryGetValue(key.ToString(), out string result))
            {
                return result;
            }

            return null;
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
            Locale? locale = localeProvider.GetLocale(localeIdentifier);

            if (!locale)
            {
                locale = Locale.CreateLocale(localeIdentifier);
                localeProvider.AddLocale(locale);
                LocaleAdded?.Invoke(locale);
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

        /// <summary>
        /// Called when a new locale is added to the game's available locales list by an end user.
        /// </summary>
        public static event Action<Locale>? LocaleAdded;
    }
}
