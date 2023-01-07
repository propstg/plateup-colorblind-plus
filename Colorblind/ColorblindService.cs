using Kitchen;
using KitchenData;
using KitchenLib.References;
using KitchenLib.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace Colorblind {

    public class ColorblindService {

        private FieldInfo itemGroupView_colourblindLabel;
        private FieldInfo itemGroupView_componentLabels;
        private FieldInfo colourblindLabel_text;
        private FieldInfo colourblindLabel_item;
        private GameObject existingColourBlindChild;

        public ColorblindService() {
            buildReflectionCache();
            getExistingColourBlindChildToCloneFromPie();
            printExistingInfo();
        }

        private void buildReflectionCache() {
            itemGroupView_colourblindLabel = ReflectionUtils.GetField<ItemGroupView>("ColourblindLabel");
            itemGroupView_componentLabels = ReflectionUtils.GetField<ItemGroupView>("ComponentLabels");
            Type itemGroupViewInfo = typeof(ItemGroupView);
            Type colourBlindLabelType = itemGroupViewInfo.GetNestedType("ColourBlindLabel", BindingFlags.NonPublic);
            colourblindLabel_text = colourBlindLabelType.GetField("Text");
            colourblindLabel_item = colourBlindLabelType.GetField("Item");
        }

        private void getExistingColourBlindChildToCloneFromPie() {
            Item pie = GameData.Main.Get<Item>(ItemReferences.PieMeatCooked);
            existingColourBlindChild = GameObjectUtils.GetChildObject(pie.Prefab, "Colour Blind");
        }

        public void setupColorblindFeatureForItems(List<int> items, IEnumerable labels, Pref requiredPreference) {
            items.ForEach(item => setupColorBlindFeatureForItem(item, labels, requiredPreference));
        }

        private void setupColorBlindFeatureForItem(int itemId, IEnumerable labels, Pref requiredPreference) {
            if (!ColorblindPreferences.isOn(requiredPreference)) {
                Debug.Log($"{ColorblindMod.MOD_ID}] {nameof(requiredPreference)} is off. Not adding labels.");
                return;
            }

            Item item = GameData.Main.Get<Item>(itemId);
            ItemGroupView itemGroupView = item.Prefab.GetComponent<ItemGroupView>();
            itemGroupView_componentLabels.SetValue(itemGroupView, labels);

            if (doesColourBlindChildExist(item)) {
                Debug.Log($"{itemId} already has a Colour Blind child.");
                return;
            }

            GameObject clonedColourBlind = cloneColourBlindObjectAndAddToItem(item);
            setColourBlindLabelObjectOnItemGroupView(itemGroupView, clonedColourBlind);
        }

        public void addSingleItemLabels() {
            if (!ColorblindPreferences.isOn(ColorblindPreferences.ShowStandaloneLabels)) {
                Debug.Log($"{ColorblindMod.MOD_ID}] Standalone is off. Not adding labels.");
                return;
            }

            foreach (KeyValuePair<int, string> entry in SingleItems.SINGLE_ITEM_LABELS) {
                if (!GameData.Main.TryGet<Item>(entry.Key, out Item item)) {
                    Debug.Log($"[{ColorblindMod.MOD_ID}] Unable to find item for {entry.Key}");
                    continue;
                }

                GameObject clonedColourBlind = cloneColourBlindObjectAndAddToItem(item);
                TextMeshPro textMeshProObject = getTextMeshProFromClonedObject(clonedColourBlind);
                textMeshProObject.text = entry.Value;
            }
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
                TextMeshPro textMeshProObject = getTextMeshProFromClonedObject(clonedColourBlind);
                textMeshProObject.text = "";
                itemGroupView_colourblindLabel.SetValue(itemGroupView, textMeshProObject);
            }
        }

        private TextMeshPro getTextMeshProFromClonedObject(GameObject clonedColourBlind) {
            GameObject titleChild = GameObjectUtils.GetChildObject(clonedColourBlind, "Title");
            return titleChild.GetComponent<TextMeshPro>();
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
    }
}
