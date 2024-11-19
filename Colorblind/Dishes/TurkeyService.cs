using Colorblind.Colorblind.References;
using Colorblind.Labels;
using Colorblind.Settings;
using Kitchen;
using System.Collections.Generic;

namespace Colorblind.Dishes {

    public class TurkeyService : DishService {

        protected override Pref requiredPref => ColorblindPreferences.ShowTurkeyLabels;

        protected override void doActualSetup(ColorblindService service) {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.TurkeyPlated }, ColourBlindLabelCreator.createTurkeyLabels());
        }
    }
}
