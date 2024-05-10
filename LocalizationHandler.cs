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

        public static void AddKeyTranslationForLocale(Locale locale, LocalizationKeys.Keys key, string translation)
        {
            if (!LanguageDictionaries.ContainsKey(locale))
            {
                LanguageDictionaries.Add(locale, []);
            }

            LanguageDictionaries[locale].TryAdd(key.ToString(), translation);
        }
       
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

        public static void AddKeyTranslationForLocale(Locale locale, string key, string translation)
        {
            if (!LanguageDictionaries.ContainsKey(locale))
            {
                LanguageDictionaries.Add(locale, []);
            }

            LanguageDictionaries[locale].TryAdd(key, translation);
        }

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

        public static Locale GetCreateLocale(CultureInfo cultureInfo)
        {
            LocaleIdentifier localeIdentifier = new LocaleIdentifier(cultureInfo);
            return GetCreateLocale(localeIdentifier);
        }
    }
}
