using Colorblind.Settings;
using Kitchen;
using Kitchen.Modules;
using System.Collections.Generic;
using UnityEngine;

namespace Colorblind.Menus {

    public class DishesMenu : Menu<MenuAction> {

        private static readonly List<bool> boolValues = new List<bool> { false, true };
        private static readonly List<string> boolLabels = new List<string> { "Normal Game Labels (if present)", "On" };

        public DishesMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int _) {
            addBool("Burgers", ColorblindPreferences.ShowBurgerLabels);
            addBool("Pizza", ColorblindPreferences.ShowPizzaLabels);
            addBool("Salad", ColorblindPreferences.ShowSaladLabels);
            addBool("Steak", ColorblindPreferences.ShowSteakLabels);
            addBool("Dumplings", ColorblindPreferences.ShowDumplingLabels);
            addBool("Breakfast", ColorblindPreferences.ShowBreakfastLabels);
            addBool("Fish", ColorblindPreferences.ShowAdditionalFishLabels);
            addBool("Cake Flavors", ColorblindPreferences.ShowCakeLabels);
            addBool("Ingredients", ColorblindPreferences.ShowStandaloneLabels);
            AddInfo("Note: Changes made here will only take place after a game restart.");
            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }

        private void addBool(string label, Pref pref) {
            string labelWithColor = $"<color=purple>{label}</color> ";
            Option<bool> option = new Option<bool>(boolValues, ColorblindPreferences.isOn(pref), new List<string> {$"{label}: <color=red> Default </color>", $"{label}: <color=green>On</color>"});
            AddSelect(option);
            option.OnChanged += delegate (object _, bool value) {
                ColorblindPreferences.setBool(pref, value);
            };
        }
    }
}
