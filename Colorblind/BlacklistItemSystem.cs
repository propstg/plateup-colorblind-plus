using Colorblind.Settings;
using Kitchen;
using KitchenData;
using KitchenMods;
using System;
using System.Reflection;

namespace Colorblind {

    public class BlacklistItemSystem : FranchiseFirstFrameSystem, IModSystem {

        private ColorblindService service;
        private FieldInfo itemGroupView_componentLabels;

        protected override void Initialise() {
            base.Initialise();
            service = new ColorblindService(GameData.Main);
            itemGroupView_componentLabels = typeof(ItemGroupView).GetField("ComponentLabels", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        protected override void OnUpdate() {
            var itemsToIgnoreString = ColorblindPreferences.getString(ColorblindPreferences.ItemBlacklist);
            ColorblindMod.Log(itemsToIgnoreString);
            string[] itemsToIgnore = itemsToIgnoreString.Split(',');
            foreach (var itemToIgnore in itemsToIgnore) {
                ColorblindMod.Log($"Blacklisting user-requested item: {itemToIgnore}");
                try {
                    int id = Int32.Parse(itemToIgnore);
                    if (GameData.Main.TryGet<Item>(id, out Item item)) {
                        if (item.Prefab != null) {
                            service.setTextToBlankForAllColourBlindChildrenForItem(id);
                            ItemGroupView component = item.Prefab.GetComponent<ItemGroupView>();
                            if (component != null) {
                                itemGroupView_componentLabels.SetValue(component, null);
                            }
                        }
                    } else {
                        ColorblindMod.Log($"{itemToIgnore}--item not found with matching ID in gamedata");
                    }
                } catch (FormatException) {
                    ColorblindMod.Log($"Error parsing item {itemToIgnore}--not a valid int");
                }
            }
        }
    }
}
