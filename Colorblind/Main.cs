using Kitchen;
using KitchenData;
using KitchenLib;
using KitchenLib.Event;
using KitchenLib.References;
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
        public const string MOD_VERSION = "0.0.4";

        private readonly Dictionary<int, string> SINGLE_ITEM_LABELS = new Dictionary<int, string>() {
            { ItemReferences.Breadcrumbs, "Bcrumb" },
            { ItemReferences.BroccoliChopped, "B" },
            { ItemReferences.CarrotChopped, "C" },
            { ItemReferences.CheeseGrated, "Ch" },
            { ItemReferences.CranberriesChopped, "Cr" },
            { ItemReferences.CranberrySauce, "Csauce" },
            { ItemReferences.OnionChopped, "O" },
            { ItemReferences.Stuffing, "Stuff" },
            { ItemReferences.StuffingRaw, "Stuff" },
            { ItemReferences.TomatoChopped, "T" },
            { ItemReferences.TurkeySlice, "Tu" },
        };

        private FieldInfo itemGroupView_colourblindLabel;
        private FieldInfo itemGroupView_componentLabels;
        private FieldInfo colourblindLabel_text;
        private FieldInfo colourblindLabel_item;
        private GameObject existingColourBlindChild;

        public ColorblindMod() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, "1.1.2", Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise() {
            ColorblindPreferences.registerPreferences();
            initMenus();
            buildReflectionCache();
            printExistingInfo();
            getExistingColourBlindChildToCloneFromPie();
            addLabelsToStirFry();
            addLabelsToTurkey();
            addLabelsToBurgers();
            makeIceCreamOrderingConsistentWithAppliance();
            addSingleItemLabels();
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

        private void addLabelsToStirFry() {
            if (!ColorblindPreferences.isOn(ColorblindPreferences.ShowStirFryLabels)) {
                Debug.Log($"{MOD_ID}] Stir fry is off. Not adding labels.");
                return;
            }

            setupColorBlindFeatureForItem(ItemReferences.StirFryRaw, ColourBlindLabelCreator.createStirFryLabels());
            setupColorBlindFeatureForItem(ItemReferences.StirFryPlated, ColourBlindLabelCreator.createStirFryLabels());
            setupColorBlindFeatureForItem(ItemReferences.StirFryCooked, ColourBlindLabelCreator.createStirFryLabels());
        }

        private void addLabelsToTurkey() {
            if (!ColorblindPreferences.isOn(ColorblindPreferences.ShowTurkeyLabels)) {
                Debug.Log($"{MOD_ID}] Turkey is off. Not adding labels.");
                return;
            }

            setupColorBlindFeatureForItem(ItemReferences.TurkeyPlated, ColourBlindLabelCreator.createTurkeyLabels());
        }

        private void addLabelsToBurgers() {
            if (!ColorblindPreferences.isOn(ColorblindPreferences.ShowBurgerLabels)) {
                Debug.Log($"{MOD_ID}] Burgers are off. Not adding labels.");
                return;
            }

            setupColorBlindFeatureForItem(ItemReferences.BurgerUnplated, ColourBlindLabelCreator.createBurgerLabels());
            setupColorBlindFeatureForItem(ItemReferences.BurgerPlated, ColourBlindLabelCreator.createBurgerLabels());
        }

        private void makeIceCreamOrderingConsistentWithAppliance() {
            setupColorBlindFeatureForItem(ItemReferences.IceCreamServing, ColourBlindLabelCreator.createConsistentIceCreamLabels());
        }

        private void addSingleItemLabels() {
            if (!ColorblindPreferences.isOn(ColorblindPreferences.ShowStandaloneLabels)) {
                Debug.Log($"{MOD_ID}] Standalone is off. Not adding labels.");
                return;
            }

            foreach (KeyValuePair<int, string> entry in SINGLE_ITEM_LABELS) {
                Item item = GameData.Main.Get<Item>(entry.Key);
                GameObject clonedColourBlind = cloneColourBlindObjectAndAddToItem(item);
                TextMeshPro textMeshProObject = getTextMeshProFromClonedObject(clonedColourBlind);
                textMeshProObject.text = entry.Value;
            }
        }

        private void setupColorBlindFeatureForItem(int itemId, IEnumerable labels) {
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

        private void initMenus() {
            Events.PreferenceMenu_MainMenu_SetupEvent += (s, args) => {
                Type type = args.instance.GetType().GetGenericArguments()[0];
                args.mInfo.Invoke(args.instance, new object[] { MOD_NAME, typeof(ColorblindMenu<>).MakeGenericType(type), false });
            };
            Events.PreferenceMenu_MainMenu_CreateSubmenusEvent += (s, args) => {
                args.Menus.Add(typeof(ColorblindMenu<MainMenuAction>), new ColorblindMenu<MainMenuAction>(args.Container, args.Module_list));
            };
            Events.PreferenceMenu_PauseMenu_SetupEvent += (s, args) => {
                Type type = args.instance.GetType().GetGenericArguments()[0];
                args.mInfo.Invoke(args.instance, new object[] { MOD_NAME, typeof(ColorblindMenu<>).MakeGenericType(type), false });
            };
            Events.PreferenceMenu_PauseMenu_CreateSubmenusEvent += (s, args) => {
                args.Menus.Add(typeof(ColorblindMenu<PauseMenuAction>), new ColorblindMenu<PauseMenuAction>(args.Container, args.Module_list));
            };
        }
    }

    public class ColourBlindLabelCreator : ItemGroupView {

        public static IEnumerable createStirFryLabels() {
            return new List<ColourBlindLabel> {
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.BroccoliChopped), Text = "B" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.BroccoliChoppedContainerCooked), Text = "B" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.CarrotChopped), Text = "C" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.CarrotChoppedContainerCooked), Text = "C" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.MeatChopped), Text = "Me" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.MeatChoppedContainerCooked), Text = "Me" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.BroccoliServing), Text = "-b" },
            };
        }

        public static IEnumerable createTurkeyLabels() {
            return new List<ColourBlindLabel> {
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.TurkeySlice), Text = "Tu" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.CranberrySauce), Text = "C" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.TurkeyGravy), Text = "G" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.Stuffing), Text = "S" },
            };
        }

        public static IEnumerable createConsistentIceCreamLabels() {
            return new List<ColourBlindLabel> {
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.IceCreamStrawberry), Text = "S" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.IceCreamChocolate), Text = "C" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.IceCreamVanilla), Text = "V" },
            };
        }

        public static IEnumerable createBurgerLabels() {
            return new List<ColourBlindLabel> {
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.CheeseGrated), Text = "Ch" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.OnionChopped), Text = "O" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.TomatoChopped), Text = "T" },
            };
        }
    }
}
