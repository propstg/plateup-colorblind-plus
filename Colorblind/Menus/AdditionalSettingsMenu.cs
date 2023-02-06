using Colorblind.Settings;
using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using System.Collections.Generic;
using UnityEngine;

namespace Colorblind.Menus {

    public class AdditionalSettingsMenu<T> : KLMenu<T> {

        private static readonly List<bool> boolValues = new List<bool> { false, true };
        private static readonly List<string> boolLabels = new List<string> { "Off", "On" };
        private static readonly List<int> displayStyleValues = new List<int> { (int)DisplayStyles.COMPRESSED, (int)DisplayStyles.EXPANDED };
        private static readonly List<string> displayStyleLabels = new List<string> { "Compressed", "Expanded" };
        private static readonly List<int> iceCreamLabelValues = new List<int> { (int)IceCreamLabels.CSV, (int)IceCreamLabels.SCV, (int)IceCreamLabels.VCS };
        private static readonly List<string> iceCreamLabelLabels = new List<string> { "CSV", "SCV", "VCS" };
        private static readonly List<int> nameStyleValues = new List<int> { ColorblindPreferences.STEAM_NAME, ColorblindPreferences.PROFILE_NAME, ColorblindPreferences.NUMBERS_ONLY };
        private static readonly List<string> nameStyleLabels = new List<string> { "Steam Name", "Profile Name", "Numbers Only" };

        public AdditionalSettingsMenu(Transform container, ModuleList module_list) : base(container, module_list) {}

        public override void Setup(int _) {
            addDisplayStyle();
            addIceCreamLabelOrder();
            addReadyCheckSection();
            addBool("Press Ready to Toggle Labels", ColorblindPreferences.ToggleLabelsWithButtonPress);
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

        private void addIceCreamLabelOrder() {
            Option<int> option = new Option<int>(iceCreamLabelValues, (int)ColorblindPreferences.getIceCreamLabelStyle(), iceCreamLabelLabels);
            AddLabel("Ice Cream Label Order");
            AddSelect(option);
            option.OnChanged += delegate (object _, int value) {
                ColorblindPreferences.setIceCreamLabelStyle((IceCreamLabels) value);
            };
        }

        private void addReadyCheckSection() {
            if (ReadyCheckNamesUtil.isReadyCheckNamesInstalled) {
                addDisabledReadyCheckSection();
                return;
            }

            Option<int> option = new Option<int>(nameStyleValues, ColorblindPreferences.getNameStyle(), nameStyleLabels);
            addBool("Ready Check - Names", ColorblindPreferences.NamesInsteadOfChecks);
            AddLabel("Name Style");
            AddSelect(option);
            option.OnChanged += delegate (object _, int value) {
                ColorblindPreferences.setNameStyle(value);
            };
        }

        private void addDisabledReadyCheckSection() {
            AddLabel("Ready Check - Names");
            AddInfo("You have Ready Check Names installed. Name checking feature will be handled by that mod.");
        }
    }
}
