using Colorblind.Colorblind.References;
using Colorblind.Labels;
using Colorblind.Settings;
using HarmonyLib;
using Kitchen;
using KitchenData;
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

        private FieldInfo itemGroupView_colourblindLabel;
        private FieldInfo itemGroupView_componentLabels;
        private FieldInfo colourblindLabel_text;
        private FieldInfo colourblindLabel_item;
        private GameObject existingColourblindChild;

        GameData gamedata;

        public ColorblindService(GameData gamedata) {
            this.gamedata = gamedata;
            buildReflectionCache();
            getExistingColourBlindChildToCloneFromPie(gamedata);
            printExistingInfo();
        }

        private void buildReflectionCache() {
            itemGroupView_colourblindLabel = AccessTools.Field(typeof(ItemGroupView), "ColourblindLabel");
            itemGroupView_componentLabels = AccessTools.Field(typeof(ItemGroupView), "ComponentLabels");
            Type itemGroupViewInfo = typeof(ItemGroupView);
            Type colourBlindLabelType = typeof(ItemGroupView.ColourBlindLabel);
            colourblindLabel_text = colourBlindLabelType.GetField("Text");
            colourblindLabel_item = colourBlindLabelType.GetField("Item");
        }

        private void getExistingColourBlindChildToCloneFromPie(GameData gamedata) {
            Item item = gamedata.Get<Item>(ItemReferences.PieMeatRaw);
            existingColourblindChild = item.Prefab.transform.Find("Colour Blind").gameObject;
        }

        public void setupColorblindFeatureForItems(List<int> itemIds, ItemLabel[] labels, Pref requiredPreference) {
            if (!ColorblindPreferences.isOn(requiredPreference)) {
                ColorblindMod.Log($"{nameof(requiredPreference)} is off. Not adding labels.");
                return;
            }

            itemIds.Select(itemId => new ItemLabelGroup {
                itemId = itemId,
                itemLabels = labels,
            }).ToList().ForEach(setupColorBlindFeatureForItem);
        }

        public void addSingleItemLabels(Dictionary<int, VariableLabel> labels, Pref requiredPreference) {
            if (!ColorblindPreferences.isOn(requiredPreference)) {
                ColorblindMod.Log($"{nameof(requiredPreference)} is off. Not adding labels.");
                return;
            }

            foreach (KeyValuePair<int, VariableLabel> entry in labels) {
                addSingleItemLabels(new ItemLabel[] {
                    new ItemLabel { itemId = entry.Key, label = entry.Value.ToString() }
                });
            }
        }

        public void addSingleItemLabels(ItemLabel[] itemLabels) {
            foreach (ItemLabel itemLabel in itemLabels) {
                if (!gamedata.TryGet<Item>(itemLabel.itemId, out Item item)) {
                    ColorblindMod.Log($"Error: Unable to find item for id {itemLabel.itemId}");
                    continue;
                }

                GameObject clonedColourBlind = cloneColourBlindObjectAndAddToItem(item);
                TextMeshPro textMeshProObject = getTextMeshProFromClonedObject(clonedColourBlind);
                textMeshProObject.text = itemLabel.label;
            }
        }

        public void setupColorBlindFeatureForItem(ItemLabelGroup itemLabelGroup) {
            Item item = gamedata.Get<Item>(itemLabelGroup.itemId);
            ItemGroupView itemGroupView = item.Prefab.GetComponent<ItemGroupView>();
            itemGroupView_componentLabels.SetValue(itemGroupView, ColourBlindLabelCreatorUtil.createLabelGroup(itemLabelGroup));

            if (doesColourBlindChildExist(item)) {
                ColorblindMod.Log($"{itemLabelGroup.itemId} already has a Colour Blind child.");
                return;
            }

            GameObject clonedColourBlind = cloneColourBlindObjectAndAddToItem(item);
            setColourBlindLabelObjectOnItemGroupView(itemGroupView, clonedColourBlind);
        }

        public void setTextToBlankForAllColourBlindChildrenForItem(int itemId) {
            Item item = gamedata.Get<Item>(itemId);
            List<GameObject> colourBlindChildren = findChildrenByName(item.Prefab, "Colour Blind");
            ColorblindMod.Log($"Found {colourBlindChildren.Count} Colour Blind children in {itemId}'s prefab");
            foreach (GameObject colourBlindChild in colourBlindChildren) {
                getTextMeshProFromClonedObject(colourBlindChild).text = "";
            }
        }

        private TextMeshPro getTextMeshProFromClonedObject(GameObject clonedColourBlind) {
            GameObject titleChild = clonedColourBlind.transform.Find("Title").gameObject;
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

            ColorblindMod.Log($"Font size = {fontSize}, invertColors = {invertColors}, wideShadow = {wideShadow}");

            IEnumerable<Item> enumerable = gamedata.Get<Item>();
            foreach (Item item in enumerable) {
                if (item.Prefab == null) {
                    continue;
                }

                bool isModdedDish = Enum.GetValues(typeof(_DishReferences)).Cast<_DishReferences>().Any(id => (int)id == item.ID);

                List<GameObject> colourBlindChildren = findChildrenByName(item.Prefab, "Colour Blind");
                foreach (GameObject colourBlindChild in colourBlindChildren) {
                    if (!isModdedDish) {
                        colourBlindChild.transform.localPosition = offset;

                        if (colourBlindChild.transform.localScale != new Vector3(1, 1, 1)) {
                            ColorblindMod.Log("Setting localScale back to 1 for " + item.name);
                            colourBlindChild.transform.localScale = new Vector3(1, 1, 1);
                        }
                    }

                    TextMeshPro textMeshPro = getTextMeshProFromClonedObject(colourBlindChild);
                    textMeshPro.fontSize = fontSize;
                    textMeshPro.fontSizeMax = fontSize;
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
            var plated = gamedata.Get<Item>(fishId);
            List<GameObject> gameObjects = findChildrenByName(plated.Prefab, fishName);
            GameObject gameObject = Object.Instantiate(existingColourblindChild);
            gameObject.name = "Colour Blind";
            gameObject.transform.SetParent(gameObjects[0].transform);
            gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            getTextMeshProFromClonedObject(gameObject).text = fishLabel;
        }

        public TMP_FontAsset getFontFromTextMeshPro() => getTextMeshProFromClonedObject(existingColourblindChild).font;

        [System.Diagnostics.Conditional("DEBUG")]
        private void printExistingInfo() {
            foreach (Item item in gamedata.Get<Item>()) {
                ColorblindMod.Log("------------------------------------------------------------");
                ColorblindMod.Log(item.ID);
                ColorblindMod.Log(item.ToString());

                if (item.Prefab == null) {
                    ColorblindMod.Log("Prefab is null.");
                    continue;
                }
                ColorblindMod.Log(item.Prefab);

                ItemGroupView itemGroupView = item.Prefab.GetComponent<ItemGroupView>();
                if (itemGroupView == null) {
                    ColorblindMod.Log("No ItemGroupView component.");
                    continue;
                }
                ColorblindMod.Log(itemGroupView);

                IEnumerable colorblindLabels = (IEnumerable)itemGroupView_componentLabels.GetValue(itemGroupView);
                if (colorblindLabels == null) {
                    ColorblindMod.Log("No colorblind labels.");
                    continue;
                }
                foreach (var label in colorblindLabels) {
                    ColorblindMod.Log($"Found color blind label '{colourblindLabel_text.GetValue(label)}' for item {((Item)colourblindLabel_item.GetValue(label)).ID}");
                }
            }
        }

        public bool doesColourBlindChildExist(Item item) {
            return item.Prefab.transform.Find("Colour Blind") != null;
        }

        public GameObject cloneColourBlindObjectAndAddToItem(Item item) {
            GameObject clonedColourBlind = Object.Instantiate(existingColourblindChild);
            clonedColourBlind.transform.Find("Title").GetComponent<TextMeshPro>().text = "";
            clonedColourBlind.name = "Colour Blind";
            clonedColourBlind.transform.SetParent(item.Prefab.transform);
            clonedColourBlind.transform.localPosition = new Vector3(0, 0, 0);
            return clonedColourBlind;
        }

        public void setColourBlindLabelObjectOnItemGroupView(ItemGroupView itemGroupView, GameObject clonedColourBlind) {
            if (itemGroupView_colourblindLabel.GetValue(itemGroupView) == null) {
                TextMeshPro textMeshProObject = getTextMeshProFromClonedObject(clonedColourBlind);
                textMeshProObject.text = "";
                itemGroupView_colourblindLabel.SetValue(itemGroupView, textMeshProObject);
            }
        }
    }
}
