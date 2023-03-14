using Kitchen.Modules;
using KitchenLib;
using UnityEngine;

namespace Colorblind.Menus {

    public class MainMenu<T> : KLMenu<T> {

        public MainMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int _) {
            AddSubmenuButton("Dishes", typeof(DishesMenu<T>));
            AddSubmenuButton("Font Settings", typeof(FontSettingsMenu<T>));
            AddSubmenuButton("Additional Settings", typeof(AdditionalSettingsMenu<T>));
            AddSubmenuButton("Experimental", typeof(ExperimentalMenu<T>));
            New<SpacerElement>();
            New<SpacerElement>();
            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }
    }
}
