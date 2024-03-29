﻿using Kitchen;

namespace Colorblind.Settings {

    public class ColorblindPreferences {

        public static readonly int STEAM_NAME = 0;
        public static readonly int PROFILE_NAME = 1;
        public static readonly int NUMBERS_ONLY = 2;

        public static readonly Pref ShowStirFryLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowStirFryLabels));
        public static readonly Pref ShowTurkeyLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowTurkeyLabels));
        public static readonly Pref ShowStandaloneLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowStandaloneLabels));
        public static readonly Pref ShowBurgerLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowBurgerLabels));
        public static readonly Pref ShowPizzaLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowPizzaLabels));
        public static readonly Pref ShowSaladLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowSaladLabels));
        public static readonly Pref ShowSteakLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowSteakLabels));
        public static readonly Pref ShowDumplingLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowDumplingLabels));
        public static readonly Pref ShowBreakfastLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowBreakfastLabels));
        public static readonly Pref ShowAdditionalFishLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowAdditionalFishLabels));
        public static readonly Pref ReorderIceCreamLabels = new Pref(ColorblindMod.MOD_ID, nameof(ReorderIceCreamLabels));
        public static readonly Pref IceCreamLabelOrdering = new Pref(ColorblindMod.MOD_ID, nameof(IceCreamLabelOrdering));

        public static readonly Pref DisplayStyle = new Pref(ColorblindMod.MOD_ID, nameof(DisplayStyle));
        public static readonly Pref FontSize = new Pref(ColorblindMod.MOD_ID, nameof(FontSize));
        public static readonly Pref FontInvertColors = new Pref(ColorblindMod.MOD_ID, nameof(FontInvertColors));
        public static readonly Pref FontWideShadow = new Pref(ColorblindMod.MOD_ID, nameof(FontWideShadow));
        public static readonly Pref FontVerticalOffset = new Pref(ColorblindMod.MOD_ID, nameof(FontVerticalOffset));
        public static readonly Pref NamesInsteadOfChecks = new Pref(ColorblindMod.MOD_ID, nameof(NamesInsteadOfChecks));
        public static readonly Pref NameStyle = new Pref(ColorblindMod.MOD_ID, nameof(NameStyle));
        public static readonly Pref ToggleLabelsWithButtonPress = new Pref(ColorblindMod.MOD_ID, nameof(ToggleLabelsWithButtonPress));
        public static readonly Pref CustomerNameVerticalOffset = new Pref(ColorblindMod.MOD_ID, nameof(CustomerNameVerticalOffset));
        public static readonly Pref ItemBlacklist = new Pref(ColorblindMod.MOD_ID, nameof(ItemBlacklist));

        public static void registerPreferences() {
            addBoolPreference(ShowStirFryLabels, false);
            addBoolPreference(ShowTurkeyLabels, false);
            addBoolPreference(ShowStandaloneLabels);
            addBoolPreference(ShowBurgerLabels);
            addBoolPreference(ShowPizzaLabels);
            addBoolPreference(ShowSaladLabels);
            addBoolPreference(ShowSteakLabels);
            addBoolPreference(ShowDumplingLabels);
            addBoolPreference(ShowBreakfastLabels);
            addBoolPreference(ShowAdditionalFishLabels);
            addBoolPreference(ReorderIceCreamLabels);
            Preferences.AddPreference<int>(new IntPreference(IceCreamLabelOrdering, (int)IceCreamLabels.NOT_SET));
            Preferences.AddPreference<int>(new IntPreference(DisplayStyle, (int)DisplayStyles.EXPANDED));
            Preferences.AddPreference<float>(new FloatPreference(FontSize, 2.0f));
            addBoolPreference(FontInvertColors);
            addBoolPreference(FontWideShadow);
            Preferences.AddPreference<float>(new FloatPreference(FontVerticalOffset, 0f));
            addBoolPreference(NamesInsteadOfChecks);
            Preferences.AddPreference<int>(new IntPreference(NameStyle, STEAM_NAME));
            addBoolPreference(ToggleLabelsWithButtonPress, false);
            Preferences.AddPreference<float>(new FloatPreference(CustomerNameVerticalOffset, 0.2f));
            Preferences.AddPreference<string>(new StringPreference(ItemBlacklist, ""));
            Preferences.Load();

            setBool(ShowTurkeyLabels, false);
            migrateIceCreamLabelPreferenceIfNotSet();
        }

        private static void migrateIceCreamLabelPreferenceIfNotSet() {
            if (getIceCreamLabelStyle() == IceCreamLabels.NOT_SET) {
                setIceCreamLabelStyle(isOn(ReorderIceCreamLabels) ? IceCreamLabels.SCV : IceCreamLabels.CSV);
            }
        }

        public static bool isOn(Pref pref) {
            return Preferences.Get<bool>(pref);
        }

        public static void setBool(Pref pref, bool value) {
            Preferences.Set<bool>(pref, value);
        }

        private static void addBoolPreference(Pref pref) {
            Preferences.AddPreference<bool>(new BoolPreference(pref, true));
        }

        private static void addBoolPreference(Pref pref, bool defaultValue) {
            Preferences.AddPreference<bool>(new BoolPreference(pref, defaultValue));
        }

        public static bool isDisplayStyle(DisplayStyles style) {
            return Preferences.Get<int>(DisplayStyle) == (int)style;
        }

        public static DisplayStyles getDisplayStyle() {
            return (DisplayStyles)Preferences.Get<int>(DisplayStyle);
        }

        public static void setDisplayStyle(DisplayStyles value) {
            Preferences.Set<int>(DisplayStyle, (int)value);
        }

        public static float getFloat(Pref pref) {
            return Preferences.Get<float>(pref);
        }

        public static void setFloat(Pref pref, float value) {
            Preferences.Set<float>(pref, value);
        }

        public static float getFontSize() {
            return getFloat(FontSize);
        }

        public static float getFontVerticalOffset() {
            return getFloat(FontVerticalOffset);
        }

        public static IceCreamLabels getIceCreamLabelStyle() {
            return (IceCreamLabels)Preferences.Get<int>(IceCreamLabelOrdering);
        }

        public static void setIceCreamLabelStyle(IceCreamLabels value) {
            Preferences.Set<int>(IceCreamLabelOrdering, (int) value);
        }

        public static int getNameStyle() {
            return Preferences.Get<int>(NameStyle);
        }

        public static void setNameStyle(int value) {
            Preferences.Set<int>(NameStyle, value);
        }
        
        public static bool isSteamNameSelected() {
            return getNameStyle() == STEAM_NAME;
        }

        public static string getString(Pref pref) {
            return Preferences.Get<string>(pref);
        }

        public static void setString(Pref pref, string value) {
            Preferences.Set<string>(pref, value);
        }
    }
}
