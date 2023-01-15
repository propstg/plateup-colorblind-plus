using Colorblind.Labels;
using Colorblind.Settings;
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

        public void addSingleItemLabels(Dictionary<int, VariableLabel> labels, Pref requiredPreference) {
            if (!ColorblindPreferences.isOn(requiredPreference)) {
                Debug.Log($"{ColorblindMod.MOD_ID}] {nameof(requiredPreference)} is off. Not adding labels.");
                return;
            }

            foreach (KeyValuePair<int, VariableLabel> entry in labels) {
                if (!GameData.Main.TryGet<Item>(entry.Key, out Item item)) {
                    Debug.Log($"[{ColorblindMod.MOD_ID}] Unable to find item for {entry.Key}");
                    continue;
                }

                GameObject clonedColourBlind = cloneColourBlindObjectAndAddToItem(item);
                TextMeshPro textMeshProObject = getTextMeshProFromClonedObject(clonedColourBlind);
                textMeshProObject.text = entry.Value.ToString();
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

        public void setTextToBlankForAllColourBlindChildrenForItem(int itemId) {
            Item item = GameData.Main.Get<Item>(itemId);
            List<GameObject> colourBlindChildren = findChildrenByName(item.Prefab, "Colour Blind");
            Debug.Log($"Found {colourBlindChildren.Count} Colour Blind children in {itemId}'s prefab");
            foreach (GameObject colourBlindChild in colourBlindChildren) {
                getTextMeshProFromClonedObject(colourBlindChild).text = "";
            }
        }

        private List<GameObject> findChildrenByName(GameObject parent, string name) {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in parent.transform) {
                if (child.name == name) {
                    children.Add(child.gameObject);
                }
                children.AddRange(findChildrenByName(child.gameObject, name));
            }
            return children;
        }

        private TextMeshPro getTextMeshProFromClonedObject(GameObject clonedColourBlind) {
            GameObject titleChild = GameObjectUtils.GetChildObject(clonedColourBlind, "Title");
            return titleChild.GetComponent<TextMeshPro>();
        }

        public void updateLabelStyles() {
            float fontSize = ColorblindPreferences.getFontSize();
            bool invertColors = ColorblindPreferences.isOn(ColorblindPreferences.FontInvertColors);
            bool wideShadow = ColorblindPreferences.isOn(ColorblindPreferences.FontWideShadow);
            float verticalOffset = ColorblindPreferences.getFontVerticalOffset();

            float outlineWidth = wideShadow ? 0.2f : 0.107f;
            FontStyles fontStyle = wideShadow ? FontStyles.Bold : FontStyles.Normal;
            Color32 outlineColor = invertColors ? new Color32(255, 255, 255, 255) : new Color32(0, 0, 0, 255);
            Color color = invertColors ? new Color(0, 0, 0, 1) : new Color(0.881f, 0.923f, 1f, 1f);
            Vector3 offset = new Vector3(0f, verticalOffset, 0f);

            Debug.Log($"[{ColorblindMod.MOD_ID}] Font size = {fontSize}, invertColors = {invertColors}, wideShadow = {wideShadow}");

            IEnumerable<Item> enumerable = GameData.Main.Get<Item>();
            foreach (Item item in enumerable) {
                if (item.Prefab == null) {
                    continue;
                }

                List<GameObject> colourBlindChildren = findChildrenByName(item.Prefab, "Colour Blind");
                foreach (GameObject colourBlindChild in colourBlindChildren) {
                    colourBlindChild.transform.localPosition = offset;
                    TextMeshPro textMeshPro = getTextMeshProFromClonedObject(colourBlindChild);
                    textMeshPro.fontSize = fontSize;
                    textMeshPro.outlineWidth = outlineWidth;
                    textMeshPro.fontStyle = fontStyle;
                    textMeshPro.outlineColor = outlineColor;
                    textMeshPro.color = color;
                }
            }
        }

        public TMP_FontAsset getFontFromTextMeshPro() => getTextMeshProFromClonedObject(existingColourBlindChild).font;

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
