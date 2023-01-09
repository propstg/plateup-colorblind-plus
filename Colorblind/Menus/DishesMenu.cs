using Colorblind.Settings;
using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using System.Collections.Generic;
using UnityEngine;

namespace Colorblind.Menus {

    public class DishesMenu<T> : KLMenu<T> {

        private static readonly List<bool> boolValues = new List<bool> { false, true };
        private static readonly List<string> boolLabels = new List<string> { "Normal Game Labels (if present)", "On" };

        public DishesMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int _) {
            AddInfo("Note: Changes made here will only take place after a game restart.");

            addBool("Stir Fry", ColorblindPreferences.ShowStirFryLabels);
            addBool("Turkey", ColorblindPreferences.ShowTurkeyLabels);
            addBool("Burgers", ColorblindPreferences.ShowBurgerLabels);
            addBool("Pizza", ColorblindPreferences.ShowPizzaLabels);
            addBool("Salad", ColorblindPreferences.ShowSaladLabels);
            addBool("Steak", ColorblindPreferences.ShowSteakLabels);
            addBool("Standalone Ingredients", ColorblindPreferences.ShowStandaloneLabels);
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
    }
}
