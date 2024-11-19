using Colorblind.Colorblind.References;
using Colorblind.Labels;
using Colorblind.Settings;
using Kitchen;
using System.Collections.Generic;

namespace Colorblind.Dishes {

    public class BurgerService : DishService {

        protected override Pref requiredPref => ColorblindPreferences.ShowBurgerLabels;

        protected override void doActualSetup(ColorblindService service) {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.BurgerUnplated, ItemReferences.BurgerPlated }, ColourBlindLabelCreator.createBurgerLabels());
        }
    }
}
