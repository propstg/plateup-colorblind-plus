using Kitchen;

namespace Colorblind {

    public class ColorblindPreferences {

        public static readonly Pref ShowStirFryLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowStirFryLabels));
        public static readonly Pref ShowTurkeyLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowTurkeyLabels));
        public static readonly Pref ShowStandaloneLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowStandaloneLabels));
        public static readonly Pref ShowBurgerLabels = new Pref(ColorblindMod.MOD_ID, nameof(ShowBurgerLabels));

        public static void registerPreferences() {
            addBoolPreference(ShowStirFryLabels);
            addBoolPreference(ShowTurkeyLabels);
            addBoolPreference(ShowStandaloneLabels);
            addBoolPreference(ShowBurgerLabels);
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
    }
}
