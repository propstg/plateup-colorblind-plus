using Colorblind.Colorblind.References;
using Colorblind.Labels;
using Colorblind.Settings;
using Kitchen;
using KitchenMods;
using System.Collections.Generic;
using System.Linq;

namespace Colorblind.Dishes {

    public class PizzaService : DishService {

        private static bool isZekNikzExpandedPizzaInstalled => ModPreload.Mods.Any(mod => mod.Name == "3568560603");

        protected override Pref requiredPref => ColorblindPreferences.ShowPizzaLabels;

        protected override void doActualSetup(ColorblindService service) {
            if (isZekNikzExpandedPizzaInstalled) {
                ColorblindMod.Log("Skipping pizza because Expanded Pizza is installed");
                return;
            }

            service.setupColorblindFeatureForItems(
                new List<int> { ItemReferences.PizzaRaw, ItemReferences.PizzaCooked, ItemReferences.PizzaPlated, ItemReferences.PizzaSlice }, ColourBlindLabelCreator.createPizzaLabels());
        }
    }
}
