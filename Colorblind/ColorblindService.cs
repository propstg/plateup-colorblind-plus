using Colorblind.Labels;
using Colorblind.Settings;
using Kitchen;
using KitchenData;
using KitchenLib.Colorblind;
using KitchenLib.References;
using KitchenLib.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Colorblind {

    public class ColorblindService {

        private FieldInfo itemGroupView_componentLabels;
        private FieldInfo colourblindLabel_text;
        private FieldInfo colourblindLabel_item;
        private GameObject existingColourblindChild;

        public ColorblindService() {
            buildReflectionCache();
            getExistingColourBlindChildToCloneFromPie();
            printExistingInfo();
        }

        private void getExistingColourBlindChildToCloneFromPie() {
            Item item = GameData.Main.Get<Item>(ItemReferences.PieMeatCooked);
            existingColourblindChild = GameObjectUtils.GetChildObject(item.Prefab, "Colour Blind");
        }

        private void buildReflectionCache() {
            itemGroupView_componentLabels = ReflectionUtils.GetField<ItemGroupView>("ComponentLabels");
            Type itemGroupViewInfo = typeof(ItemGroupView);
            Type colourBlindLabelType = itemGroupViewInfo.GetNestedType("ColourBlindLabel", BindingFlags.NonPublic);
            colourblindLabel_text = colourBlindLabelType.GetField("Text");
            colourblindLabel_item = colourBlindLabelType.GetField("Item");
        }

        public void setupColorblindFeatureForItems(List<int> itemIds, ItemLabel[] labels, Pref requiredPreference) {
            if (!ColorblindPreferences.isOn(requiredPreference)) {
                log($"{nameof(requiredPreference)} is off. Not adding labels.");
                return;
            }

            itemIds.Select(itemId => new ItemLabelGroup {
                itemId = itemId,
                itemLabels = labels,
            }).ToList().ForEach(ColorblindUtils.SetupColorBlindFeatureForItem);
        }

        public void addSingleItemLabels(Dictionary<int, VariableLabel> labels, Pref requiredPreference) {
            if (!ColorblindPreferences.isOn(requiredPreference)) {
                log($"{nameof(requiredPreference)} is off. Not adding labels.");
                return;
            }

            foreach (KeyValuePair<int, VariableLabel> entry in labels) {
                ColorblindUtils.AddSingleItemLabels(new ItemLabel[] {
                    new ItemLabel { itemId = entry.Key, label = entry.Value.ToString() }
                });
            }
        }
        public void setTextToBlankForAllColourBlindChildrenForItem(int itemId) {
            Item item = GameData.Main.Get<Item>(itemId);
            List<GameObject> colourBlindChildren = findChildrenByName(item.Prefab, "Colour Blind");
            log($"Found {colourBlindChildren.Count} Colour Blind children in {itemId}'s prefab");
            foreach (GameObject colourBlindChild in colourBlindChildren) {
                ColorblindUtils.getTextMeshProFromClonedObject(colourBlindChild).text = "";
            }
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

            log($"Font size = {fontSize}, invertColors = {invertColors}, wideShadow = {wideShadow}");

            IEnumerable<Item> enumerable = GameData.Main.Get<Item>();
            foreach (Item item in enumerable) {
                if (item.Prefab == null) {
                    continue;
                }

                bool isModdedDish = GDOUtils.GetCustomGameDataObject(item.ID) != null;

                List<GameObject> colourBlindChildren = findChildrenByName(item.Prefab, "Colour Blind");
                foreach (GameObject colourBlindChild in colourBlindChildren) {
                    if (!isModdedDish) {
                        colourBlindChild.transform.localPosition = offset;

                        if (colourBlindChild.transform.localScale != new Vector3(1, 1, 1)) {
                            log("Setting localScale back to 1 for " + item.name);
                            colourBlindChild.transform.localScale = new Vector3(1, 1, 1);
                        }
                    }

                    TextMeshPro textMeshPro = ColorblindUtils.getTextMeshProFromClonedObject(colourBlindChild);
                    textMeshPro.fontSize = fontSize;
                    textMeshPro.outlineWidth = outlineWidth;
                    textMeshPro.fontStyle = fontStyle;
                    textMeshPro.outlineColor = outlineColor;
                    textMeshPro.color = color;
                }
            }
        }

        public List<GameObject> findChildrenByName(GameObject parent, string name) {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in parent.transform) {
                if (child.name == name) {
                    children.Add(child.gameObject);
                }
                children.AddRange(findChildrenByName(child.gameObject, name));
            }
            return children;
        }

        public void setWeirdFishLabel(int fishId, string fishName, string fishLabel) {
            var plated = GameData.Main.Get<Item>(fishId);
            List<GameObject> gameObjects = findChildrenByName(plated.Prefab, fishName);
            GameObject gameObject = Object.Instantiate(existingColourblindChild);
            gameObject.name = "Colour Blind";
            gameObject.transform.SetParent(gameObjects[0].transform);
            gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            ColorblindUtils.getTextMeshProFromClonedObject(gameObject).text = fishLabel;
        }

        public TMP_FontAsset getFontFromTextMeshPro() => ColorblindUtils.getTextMeshProFromClonedObject(existingColourblindChild).font;

        [System.Diagnostics.Conditional("DEBUG")]
        private void printExistingInfo() {
            foreach (Item item in GameData.Main.Get<Item>()) {
                log("------------------------------------------------------------");
                log(item.ID);
                log(item.ToString());

                if (item.Prefab == null) {
                    log("Prefab is null.");
                    continue;
                }
                log(item.Prefab);

                ItemGroupView itemGroupView = item.Prefab.GetComponent<ItemGroupView>();
                if (itemGroupView == null) {
                    log("No ItemGroupView component.");
                    continue;
                }
                log(itemGroupView);

                IEnumerable colorblindLabels = (IEnumerable)itemGroupView_componentLabels.GetValue(itemGroupView);
                if (colorblindLabels == null) {
                    log("No colorblind labels.");
                    continue;
                }
                foreach (var label in colorblindLabels) {
                    log($"Found color blind label '{colourblindLabel_text.GetValue(label)}' for item {((Item)colourblindLabel_item.GetValue(label)).ID}");
                }
            }
        }

        private void log(object message) {
            Debug.Log($"[{ColorblindMod.MOD_ID}] {message}");
        }
    }
}
