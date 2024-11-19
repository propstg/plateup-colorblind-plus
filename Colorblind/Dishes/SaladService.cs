using Colorblind.Colorblind.References;
using Colorblind.Labels;
using Colorblind.Settings;
using Kitchen;
using System.Collections.Generic;

namespace Colorblind.Dishes {

    public class SaladService : DishService {

        protected override Pref requiredPref => ColorblindPreferences.ShowSaladLabels;

        protected override void doActualSetup(ColorblindService service) {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.SaladPlated }, ColourBlindLabelCreator.createSaladLabels());
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.SaladApplePlated }, ColourBlindLabelCreator.createAppleSaladLabels());
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.SaladPotatoPlated }, ColourBlindLabelCreator.createPotatoSaladLabels());
        }
    }
}
