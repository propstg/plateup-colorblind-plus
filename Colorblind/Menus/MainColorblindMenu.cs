using Kitchen;
using Kitchen.Modules;
using UnityEngine;

namespace Colorblind.Menus {

    public class MainColorblindMenu : Menu<MenuAction> {

        public MainColorblindMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int _) {
            AddSubmenuButton("Dishes", typeof(DishesMenu));
            AddSubmenuButton("Font Settings", typeof(FontSettingsMenu));
            AddSubmenuButton("Additional Settings", typeof(AdditionalSettingsMenu));
            AddSubmenuButton("Experimental", typeof(ExperimentalMenu));
            New<SpacerElement>();
            New<SpacerElement>();
            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }
    }
}
