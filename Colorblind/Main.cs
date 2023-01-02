using Kitchen;
using KitchenData;
using KitchenLib;
using KitchenLib.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace Colorblind {

    public class ColorblindMod : BaseMod {

        public const string MOD_ID = "blargle.ColorblindPlus";
        public const string MOD_NAME = "Colorblind+";
        public const string MOD_AUTHOR = "blargle";
        public const string MOD_VERSION = "0.0.1";

        private FieldInfo itemGroupView_colourblindLabel;
        private FieldInfo itemGroupView_componentLabels;
        private FieldInfo colourblindLabel_text;
        private FieldInfo colourblindLabel_item;
        private GameObject existingColourBlindChild;
        private bool isRegistered;

        public ColorblindMod() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, "1.1.2", Assembly.GetExecutingAssembly()) { }

        protected override void Initialise() {
            if (!isRegistered) {
                buildReflectionCache();
                printExistingInfo();
                getExistingColourBlindChildToCloneFromPie();
                setupColorBlindFeatureForItem(KitchenLib.References.ItemReferences.StirFryRaw);
                setupColorBlindFeatureForItem(KitchenLib.References.ItemReferences.StirFryPlated);
                setupColorBlindFeatureForItem(KitchenLib.References.ItemReferences.StirFryCooked);
                isRegistered = true;
            }
        }

        private void buildReflectionCache() {
            itemGroupView_colourblindLabel = ReflectionUtils.GetField<ItemGroupView>("ColourblindLabel");
            itemGroupView_componentLabels = ReflectionUtils.GetField<ItemGroupView>("ComponentLabels");
            Type itemGroupViewInfo = typeof(ItemGroupView);
            Type colourBlindLabelType = itemGroupViewInfo.GetNestedType("ColourBlindLabel", BindingFlags.NonPublic);
            colourblindLabel_text = colourBlindLabelType.GetField("Text");
            colourblindLabel_item = colourBlindLabelType.GetField("Item");
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void printExistingInfo() {
            foreach (Item item in GameData.Main.Get<Item>()) {
                Debug.Log("------------------------------------------------------------");
                Debug.Log(item.ID);
                Debug.Log(item.ToString());

                if (item.Prefab == null) {
                    Debug.Log("Prefab is null.");
                    continue;
                }
                Debug.Log(item.Prefab);

                ItemGroupView itemGroupView = item.Prefab.GetComponent<ItemGroupView>();
                if (itemGroupView == null) {
                    Debug.Log("No ItemGroupView component.");
                    continue;
                }
                Debug.Log(itemGroupView);

                IEnumerable colorblindLabels = (IEnumerable)itemGroupView_componentLabels.GetValue(itemGroupView);
                if (colorblindLabels == null) {
                    Debug.Log("No colorblind labels.");
                    continue;
                }
                foreach (var label in colorblindLabels) {
                    Debug.Log($"Found color blind label '{colourblindLabel_text.GetValue(label)}' for item {((Item)colourblindLabel_item.GetValue(label)).ID}");
                }
            }
        }

        private void getExistingColourBlindChildToCloneFromPie() {
            Item pie = GameData.Main.Get<Item>(KitchenLib.References.ItemReferences.PieMeatCooked);
            existingColourBlindChild = GameObjectUtils.GetChildObject(pie.Prefab, "Colour Blind");
        }

        private void setupColorBlindFeatureForItem(int itemId) {
            Item item = GameData.Main.Get<Item>(itemId);
            if (doesColourBlindChildExist(item)) {
                Debug.Log($"{itemId} already has a Colour Blind child.");
                return;
            }

            GameObject clonedColourBlind = cloneColourBlindObjectAndAddToItem(item);
            ItemGroupView itemGroupView = item.Prefab.GetComponent<ItemGroupView>();
            setColourBlindLabelObjectOnItemGroupView(itemGroupView, clonedColourBlind);
            itemGroupView_componentLabels.SetValue(itemGroupView, ItemGroupViewHackyWorkaround.createColourBlindLabels());
        }

        private bool doesColourBlindChildExist(Item item) {
            return GameObjectUtils.GetChildObject(item.Prefab, "Colour Blind") != null;
        }

        private GameObject cloneColourBlindObjectAndAddToItem(Item item) {
            GameObject clonedColourBlind = UnityEngine.Object.Instantiate(existingColourBlindChild);
            clonedColourBlind.name = "Colour Blind";
            clonedColourBlind.transform.SetParent(item.Prefab.transform);
            clonedColourBlind.transform.localPosition = new Vector3(0, 0, 0);
            return clonedColourBlind;
        }

        private void setColourBlindLabelObjectOnItemGroupView(ItemGroupView itemGroupView, GameObject clonedColourBlind) {
            if (itemGroupView_colourblindLabel.GetValue(itemGroupView) == null) {
                GameObject titleChild = GameObjectUtils.GetChildObject(clonedColourBlind, "Title");
                TextMeshPro textMeshProObject = titleChild.GetComponent<TextMeshPro>();
                itemGroupView_colourblindLabel.SetValue(itemGroupView, textMeshProObject);
            }
        }
    }

    public class ItemGroupViewHackyWorkaround : ItemGroupView {

        public static object createColourBlindLabels() {
            return new List<ColourBlindLabel> {
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(KitchenLib.References.ItemReferences.BroccoliChopped), Text = "B" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(KitchenLib.References.ItemReferences.BroccoliChoppedContainerCooked), Text = "B" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(KitchenLib.References.ItemReferences.CarrotChopped), Text = "C" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(KitchenLib.References.ItemReferences.CarrotChoppedContainerCooked), Text = "C" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(KitchenLib.References.ItemReferences.MeatChopped), Text = "Me" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(KitchenLib.References.ItemReferences.MeatChoppedContainerCooked), Text = "Me" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(KitchenLib.References.ItemReferences.BroccoliServing), Text = "-b" },
            };
        }
    }
}
