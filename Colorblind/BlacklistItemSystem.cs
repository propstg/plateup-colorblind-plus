using Colorblind.Settings;
using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using KitchenMods;
using System;
using UnityEngine;

namespace Colorblind {

    public class BlacklistItemSystem : FranchiseFirstFrameSystem, IModSystem {

        private ColorblindService service;

        protected override void Initialise() {
            base.Initialise();
            service = new ColorblindService();
        }

        protected override void OnUpdate() {
            var itemsToIgnoreString = ColorblindPreferences.getString(ColorblindPreferences.ItemBlacklist);
            Debug.Log(itemsToIgnoreString);
            string[] itemsToIgnore = itemsToIgnoreString.Split(',');
            foreach (var itemToIgnore in itemsToIgnore) {
                Debug.Log($"[{ColorblindMod.MOD_ID}] Blacklisting user-requested item: {itemToIgnore}");
                try {
                    int id = Int32.Parse(itemToIgnore);
                    if (GameData.Main.TryGet<Item>(id, out Item item)) {
                        if (item.Prefab != null) {
                            service.setTextToBlankForAllColourBlindChildrenForItem(id);
                            ItemGroupView component = item.Prefab.GetComponent<ItemGroupView>();
                            if (component != null) {
                                ReflectionUtils.GetField<ItemGroupView>("ComponentLabels").SetValue(component, null);
                            }
                        }
                    } else {
                        Debug.Log($"[{ColorblindMod.MOD_ID}] {itemToIgnore}--item not found with matching ID in gamedata");
                    }
                } catch (FormatException) {
                    Debug.Log($"[{ColorblindMod.MOD_ID}] Error parsing item {itemToIgnore}--not a valid int");
                }
            }
        }
    }
}
