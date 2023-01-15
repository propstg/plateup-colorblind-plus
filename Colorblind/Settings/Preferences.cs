using Kitchen;

namespace Colorblind.Settings {

    public class ColorblindPreferences {

        public static readonly Pref ShowStirFryLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowStirFryLabels));
        public static readonly Pref ShowTurkeyLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowTurkeyLabels));
        public static readonly Pref ShowStandaloneLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowStandaloneLabels));
        public static readonly Pref ShowBurgerLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowBurgerLabels));
        public static readonly Pref ShowPizzaLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowPizzaLabels));
        public static readonly Pref ShowSaladLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowSaladLabels));
        public static readonly Pref ShowSteakLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowSteakLabels));
        public static readonly Pref ReorderIceCreamLabels = new Pref(ColorblindMod.MOD_ID, nameof(ReorderIceCreamLabels));
        public static readonly Pref DisplayStyle = new Pref(ColorblindMod.MOD_ID, nameof(DisplayStyle));
        public static readonly Pref FontSize = new Pref(ColorblindMod.MOD_ID, nameof(FontSize));
        public static readonly Pref FontInvertColors = new Pref(ColorblindMod.MOD_ID, nameof(FontInvertColors));
        public static readonly Pref FontWideShadow = new Pref(ColorblindMod.MOD_ID, nameof(FontWideShadow));
        public static readonly Pref FontVerticalOffset = new Pref(ColorblindMod.MOD_ID, nameof(FontVerticalOffset));
        public static readonly Pref NamesInsteadOfChecks = new Pref(ColorblindMod.MOD_ID, nameof(NamesInsteadOfChecks));

        public static void registerPreferences() {
            addBoolPreference(ShowStirFryLabels);
            addBoolPreference(ShowTurkeyLabels);
            addBoolPreference(ShowStandaloneLabels);
            addBoolPreference(ShowBurgerLabels);
            addBoolPreference(ShowPizzaLabels);
            addBoolPreference(ShowSaladLabels);
            addBoolPreference(ShowSteakLabels);
            addBoolPreference(ReorderIceCreamLabels);
            Preferences.AddPreference<int>(new IntPreference(DisplayStyle, (int)(DisplayStyles.EXPANDED)));
            Preferences.AddPreference<float>(new FloatPreference(FontSize, 2.0f));
            addBoolPreference(FontInvertColors);
            addBoolPreference(FontWideShadow);
            Preferences.AddPreference<float>(new FloatPreference(FontVerticalOffset, 0f));
            addBoolPreference(NamesInsteadOfChecks);
            Preferences.Load();
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
    }
}
