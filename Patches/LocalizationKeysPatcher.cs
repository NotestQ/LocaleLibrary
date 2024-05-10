using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Sirenix.Serialization.Utilities;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

namespace ItemLibrary.Patches
{
    [HarmonyPatch(typeof(LocalizationKeys))]
    internal class LocalizationKeysPatcher
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LocalizationKeys.OnLanguageSwitch))]
        private static void OnLanguageSwitchPatch()
        {
            bool success = LocalizationHandler.LanguageDictionaries.TryGetValue(LocalizationSettings.SelectedLocale, 
                out Dictionary<string, string> value);

            if (success)
                LocalizationHandler.CurrentLanguageDictionary = value;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(LocalizationKeys.GetLocalizedString))]
        private static bool GetLocalizedStringPatch(ref LocalizationKeys.Keys key, ref string __result)
        {
            if (LocalizationHandler.CurrentLanguageDictionary != null && LocalizationHandler.CurrentLanguageDictionary.TryGetValue(key.ToString(), out __result))
            {
                return false;
            }
            return true;
        }
    }
}
