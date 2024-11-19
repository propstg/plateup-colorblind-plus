using Colorblind.Colorblind.References;
using Colorblind.Labels;
using Colorblind.Settings;
using Kitchen;
using System.Collections.Generic;

namespace Colorblind.Dishes {

    public class SteakService : DishService {

        protected override Pref requiredPref => ColorblindPreferences.ShowSteakLabels;

        protected override void doActualSetup(ColorblindService service) {
            clearExistingSteakLabels(service);
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.SteakPlated }, ColourBlindLabelCreator.createSteakLabels());
            service.addSingleItemLabels(SingleItems.STEAK_SINGLE_ITEM_LABELS);
        }

        private void clearExistingSteakLabels(ColorblindService service) {
            new List<int> { ItemReferences.SteakPlated,
                ItemReferences.SteakRare,ItemReferences.SteakMedium,ItemReferences.SteakWelldone,
                ItemReferences.BonedSteakRare,ItemReferences.BonedSteakMedium,ItemReferences.BonedSteakWelldone,
                ItemReferences.ThickSteakRare,ItemReferences.ThickSteakMedium,ItemReferences.ThickSteakWelldone,
                ItemReferences.ThinSteakRare,ItemReferences.ThinSteakMedium,ItemReferences.ThinSteakWelldone
            }.ForEach(service.setTextToBlankForAllColourBlindChildrenForItem);
        }
    }
}
