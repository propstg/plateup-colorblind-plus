using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using System.Collections.Generic;
using UnityEngine;

namespace Colorblind {

    public class ColorblindMenu<T> : KLMenu<T> {

        private static readonly List<bool> boolValues = new List<bool> { false, true };
        private static readonly List<string> boolLabels = new List<string> { "Off", "On" };

        public ColorblindMenu(Transform container, ModuleList module_list) : base(container, module_list) { }

        public override void Setup(int _) {
            AddInfo("Note: Changes made here will only take place after a game restart.");

            addBool("Stir Fry", ColorblindPreferences.ShowStirFryLabels);
            addBool("Turkey", ColorblindPreferences.ShowTurkeyLabels);
            addBool("Burgers", ColorblindPreferences.ShowBurgerLabels);
            addBool("Pizza", ColorblindPreferences.ShowPizzaLabels);
            addBool("Salad", ColorblindPreferences.ShowSaladLabels);
            addBool("Standalone Ingredients", ColorblindPreferences.ShowStandaloneLabels);
            addBool("Reorder Ice Cream Labels", ColorblindPreferences.ReorderIceCreamLabels);
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
