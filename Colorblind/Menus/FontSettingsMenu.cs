using Colorblind.Settings;
using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using System.Collections.Generic;
using UnityEngine;

namespace Colorblind.Menus {

    public class FontSettingsMenu<T> : KLMenu<T> {

        private static readonly List<bool> boolValues = new List<bool> { false, true };
        private static readonly List<string> boolLabels = new List<string> { "Off", "On" };
        private static readonly List<float> fontSizeValues = new List<float> {1.5f, 2.0f, 2.5f};
        private static readonly List<string> fontSizeLabels = new List<string> {"Small", "Normal", "Large"};
        private static readonly List<float> fontVerticalOffsetValues = new List<float> {0.3f, 0.2f, 0.1f, 0f, -0.1f, -0.2f, -0.3f};
        private static readonly List<string> fontVerticalOffsetLabels = new List<string> {"-0.3", "-0.2", "-0.1", "0", "0.1", "0.2", "0.3"};

        private ColorblindService colorblindService;

        public FontSettingsMenu(Transform container, ModuleList module_list, ColorblindService colorblindService) : base(container, module_list) {
            this.colorblindService = colorblindService;
        }

        public override void Setup(int _) {
            addFloat("Label Size", fontSizeValues, fontSizeLabels, ColorblindPreferences.FontSize);
            addBool("Invert Colors", ColorblindPreferences.FontInvertColors);
            addBool("Wide Shadow", ColorblindPreferences.FontWideShadow);
            addFloat("Vertical Offset", fontVerticalOffsetValues, fontVerticalOffsetLabels, ColorblindPreferences.FontVerticalOffset);
            addFloat("Customer Name Offset (when seated)", fontVerticalOffsetValues, fontVerticalOffsetLabels, ColorblindPreferences.CustomerNameVerticalOffset);
            New<SpacerElement>();
            AddInfo("These settings do not require a restart, but it might be best to change between days. Not all settings will apply until a new label is created or an existing one is updated.");
            New<SpacerElement>();
            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }

        private void addBool(string label, Pref pref) {
            Option<bool> option = new Option<bool>(boolValues, ColorblindPreferences.isOn(pref), boolLabels);
            AddLabel(label);
            AddSelect(option);
            option.OnChanged += delegate (object _, bool value) {
                ColorblindPreferences.setBool(pref, value);
                colorblindService.updateLabelStyles();
            };
        }

        private void addFloat(string label, List<float> values, List<string> labels, Pref pref) {
            Option<float> option = new Option<float>(values, ColorblindPreferences.getFloat(pref), labels);
            AddLabel(label);
            AddSelect(option);
            option.OnChanged += delegate (object _, float value) {
                ColorblindPreferences.setFloat(pref, value);
                colorblindService.updateLabelStyles();
                CustomerView_Update_Patch.verticalOffset = ColorblindPreferences.getFloat(ColorblindPreferences.CustomerNameVerticalOffset);
            };
        }
    }
}
