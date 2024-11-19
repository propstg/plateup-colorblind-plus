using Colorblind.Colorblind.References;
using Colorblind.Labels;
using Colorblind.Settings;
using Kitchen;
using System.Collections.Generic;

namespace Colorblind.Dishes {

    public class BreakfastService : DishService {

        protected override Pref requiredPref => ColorblindPreferences.ShowBreakfastLabels;

        protected override void doActualSetup(ColorblindService service) {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.BreakfastPlated }, ColourBlindLabelCreator.createBreakfastLabels());
            service.addSingleItemLabels(SingleItems.BREAKFAST_SINGLE_ITEM_LABELS);
        }
    }
}
