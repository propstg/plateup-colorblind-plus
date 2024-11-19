using Colorblind.Colorblind.References;
using Colorblind.Labels;
using Colorblind.Settings;
using Kitchen;
using System.Collections.Generic;

namespace Colorblind.Dishes {

    public class DumplingService : DishService {

        protected override Pref requiredPref => ColorblindPreferences.ShowDumplingLabels;

        protected override void doActualSetup(ColorblindService service) {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.DumplingsRaw }, ColourBlindLabelCreator.createUncookedDumplingLabels());
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.DumplingsPlated }, ColourBlindLabelCreator.createCookedDumplingsLabels());
            service.addSingleItemLabels(SingleItems.DUMPLING_SINGLE_ITEM_LABELS);
        }
    }
}
