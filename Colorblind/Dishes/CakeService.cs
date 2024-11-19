using Colorblind.Colorblind.References;
using Colorblind.Settings;
using Kitchen;

namespace Colorblind.Dishes {

    public class CakeService : DishService {

        protected override Pref requiredPref => ColorblindPreferences.ShowSteakLabels;


        protected override void doActualSetup(ColorblindService service) {
            service.setTextForAllColourBlindChildrenForItem(ItemReferences.ChocolateFlavour, $"<sprite name=\"cake\"> Choc");
            service.setTextForAllColourBlindChildrenForItem(ItemReferences.CoffeeFlavour, $"<sprite icon=\"cake\"> Coff");
            service.setTextForAllColourBlindChildrenForItem(ItemReferences.LemonFlavour, $"<sprite icon=\"cake\"> Lem");
        }
    }
}
