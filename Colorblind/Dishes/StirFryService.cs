using Colorblind.Colorblind.References;
using Colorblind.Labels;
using Colorblind.Settings;
using Kitchen;
using System.Collections.Generic;

namespace Colorblind.Dishes {

    public class StirFryService : DishService {

        protected override Pref requiredPref => ColorblindPreferences.ShowStirFryLabels;

        protected override void doActualSetup(ColorblindService service) {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.StirFryRaw, ItemReferences.StirFryPlated, ItemReferences.StirFryCooked }, ColourBlindLabelCreator.createStirFryLabels());
        }
    }
}
