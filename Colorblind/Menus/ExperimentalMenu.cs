using Colorblind.Settings;
using Kitchen;
using Kitchen.Modules;
using KitchenData;
using System;
using System.Linq;
using UnityEngine;

namespace Colorblind.Menus {

    public class ExperimentalMenu : Menu<PauseMenuAction> {

        public ExperimentalMenu(Transform container, ModuleList module_list) : base(container, module_list) {}

        public override void Setup(int _) {
            InfoBoxElement status = null;
            AddLabel("Item Blacklist");
            AddInfo("Use this feature to remove labels from modded dishes or vanilla items.");
            AddButton("1. Copy item data to clipboard", delegate { copyItemDataToClipboard(status); });
            AddInfo("2. Go to https://propstg.github.io/plateup-colorblind-plus/ and set up blacklist");
            AddButton("2a. Open above URL in external browser", delegate { Application.OpenURL("https://propstg.github.io/plateup-colorblind-plus/"); });
            AddButton("3. Update blacklist from clipboard", delegate { updateBlacklistFromClipboard(status); });
            status = AddInfo("");
            AddInfo("4. Restart your game for changes to take effect.");

            New<SpacerElement>();
            New<SpacerElement>();
            AddButton("Clear blacklist", delegate { clearBlacklist(); });

            AddInfo("Note: Changes made here will only take place after a game restart.");
            New<SpacerElement>();
            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate { RequestPreviousMenu(); });
        }

        private void copyItemDataToClipboard(InfoBoxElement status) {
            var allItems = string.Join(";;;", GameData.Main.Get<Item>().ToList()
                .Select(item => item.ID + ":::" + item.name)
                .ToList());
            var currentBlacklist = ColorblindPreferences.getString(ColorblindPreferences.ItemBlacklist);
            GUIUtility.systemCopyBuffer = $"CB+ALLITEMS---{allItems}---CB+CURRENT---{currentBlacklist}";
            status.SetLabel("Copied!");
        }

        private void updateBlacklistFromClipboard(InfoBoxElement status) {
            var blacklistString = GUIUtility.systemCopyBuffer;
            if (blacklistString == null || !blacklistString.StartsWith("CB+OFF--")) {
                status.SetLabel("Clipboard contents not in expected format.");
            }

            blacklistString = blacklistString.Substring("CB+OFF---".Length);
            string[] itemsToIgnore = blacklistString.Split(',');
            bool allSuccessful = true;
            foreach (var itemToIgnore in itemsToIgnore) {
                ColorblindMod.Log($"User requested to blacklist item {itemToIgnore}");
                try {
                    Int32.Parse(itemToIgnore);
                } catch (FormatException) {
                    ColorblindMod.Log($"Error parsing item {itemToIgnore}--not a valid int");
                    status.SetLabel($"Error parsing item {itemToIgnore}--not a valid int");
                    allSuccessful = false;
                }
            }

            if (allSuccessful) {
                status.SetLabel($"Successfully imported {itemsToIgnore.Length} items.");
                ColorblindPreferences.setString(ColorblindPreferences.ItemBlacklist, blacklistString);
            }
        }

        private void clearBlacklist() {
            ColorblindPreferences.setString(ColorblindPreferences.ItemBlacklist, "");
        }
    }
}
