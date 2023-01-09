using Colorblind.Settings;
using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using System.Collections.Generic;
using UnityEngine;

namespace Colorblind.Menus {

    public class MainMenu<T> : KLMenu<T> {

        private static readonly List<bool> boolValues = new List<bool> { false, true };
        private static readonly List<string> boolLabels = new List<string> { "Off", "On" };
        private static readonly List<int> displayStyleValues = new List<int> { (int)DisplayStyles.COMPRESSED, (int)DisplayStyles.EXPANDED };
        private static readonly List<string> displayStyleLabels = new List<string> { "Compressed", "Expanded" };

        public MainMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int _) {
            AddSubmenuButton("Change Dish Settings", typeof(DishesMenu<T>));
            addDisplayStyle();
            addBool("Reorder Ice Cream Labels", ColorblindPreferences.ReorderIceCreamLabels);
            New<SpacerElement>();
            AddInfo("Note: Changes made here will only take place after a game restart.");
            New<SpacerElement>();
            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }

        private void addBool(string label, Pref pref) {
            Option<bool> option = new Option<bool>(boolValues, ColorblindPreferences.isOn(pref), boolLabels);
            AddLabel(label);
            AddSelect(option);
            option.OnChanged += delegate (object _, bool value) {
                ColorblindPreferences.setBool(pref, value);
            };
        }

        private void addDisplayStyle() {
            Option<int> option = new Option<int>(displayStyleValues, (int)ColorblindPreferences.getDisplayStyle(), displayStyleLabels);
            AddLabel("Display Style");
            AddSelect(option);
            AddInfo("Compressed example: NRMt Expanded example: Tn-R MsT");
            option.OnChanged += delegate (object _, int value) {
                ColorblindPreferences.setDisplayStyle((DisplayStyles)value);
            };
        }
    }
}
