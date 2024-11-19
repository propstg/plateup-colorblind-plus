using Colorblind.Settings;
using Kitchen;
using UniverseLib;

namespace Colorblind.Dishes {

    public abstract class DishService {

        protected abstract Pref requiredPref { get; }
        protected abstract void doActualSetup(ColorblindService service);
        
        public void setupLabels(ColorblindService service) {
            if (!ColorblindPreferences.isOn(requiredPref)) {
                ColorblindMod.Log($"Skipping registering {this.GetActualType()} because pref {requiredPref} is off");
                return;
            }

            doActualSetup(service);
        }
    }
}
