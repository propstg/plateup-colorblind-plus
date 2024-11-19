using Colorblind.Colorblind.References;
using Colorblind.Labels;
using Colorblind.Settings;
using Kitchen;
using System.Collections.Generic;

namespace Colorblind.Dishes {

    public class FishService : DishService {

        protected override Pref requiredPref => ColorblindPreferences.ShowSteakLabels;


        protected override void doActualSetup(ColorblindService service) {
            clearExistingFishLabels(service);

            new List<int> {ItemReferences.FishPinkFried, ItemReferences.FishPinkRaw}
                .ForEach(service.setTextToBlankForAllColourBlindChildrenForItem);
            service.addSingleItemLabels(SingleItems.FISH_SINGLE_ITEM_LABELS, ColorblindPreferences.ShowSteakLabels);

            // All the plated fish shared a prefab, so only one needs updated
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.FishBluePlated }, ColourBlindLabelCreator.createFishLabels(), ColorblindPreferences.ShowSteakLabels);
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.FishOysterPlated }, ColourBlindLabelCreator.createOysterLabels(), ColorblindPreferences.ShowSteakLabels);
        }

        private void clearExistingFishLabels(ColorblindService service) {
            new List<int> { ItemReferences.FishPinkPlated, ItemReferences.FishBluePlated }.ForEach(service.setTextToBlankForAllColourBlindChildrenForItem);
        }
    }
}
