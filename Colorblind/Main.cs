using Kitchen;
using KitchenData;
using KitchenLib;
using KitchenLib.Event;
using KitchenLib.Utils;
using KitchenMods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Colorblind {

    public class ColorblindMod : BaseMod {

        public const string MOD_ID = "blargle.ColorblindPlus";
        public const string MOD_NAME = "Colorblind Plus";
        public const string MOD_VERSION = "0.0.1";

        public static bool isRegistered = false;

        public ColorblindMod() : base(MOD_ID, MOD_NAME, "blargle", MOD_VERSION, "1.1.2", Assembly.GetExecutingAssembly()) { }

        protected override void OnPostActivate(Mod mod) {
            base.OnPostActivate(mod);
            GenerateReferences();
        }

        private void GenerateReferences() {
            FieldInfo textMeshProLabel = ReflectionUtils.GetField<ItemGroupView>("ColourblindLabel");
            FieldInfo componentLabelsInfo = ReflectionUtils.GetField<ItemGroupView>("ComponentLabels");
            var itemGroupViewInfo = typeof(ItemGroupView);
            Type colourBlindLabelType = itemGroupViewInfo.GetNestedType("ColourBlindLabel", BindingFlags.NonPublic);
            FieldInfo textInfo = colourBlindLabelType.GetField("Text");
            FieldInfo itemInfo = colourBlindLabelType.GetField("Item");

            Events.BuildGameDataEvent += (s, args) => {
                Item existingMeat = GameData.Main.Get<Item>(KitchenLib.References.ItemReferences.PieMeatCooked);
                GameObject existingColourBlind = GameObjectUtils.GetChildObject(existingMeat.Prefab, "Colour Blind");
                Debug.Log(existingColourBlind);
                //ItemGroupView existingMeatGroup = existingMeat.Prefab.GetComponent<ItemGroupView>();
                //Debug.Log(existingMeatGroup);
                //TextMeshPro existingTMPro = (TextMeshPro)textMeshProLabel.GetValue(existingMeatGroup);
                //Debug.Log(existingTMPro);

                foreach (Item x in GameData.Main.Get<Item>()) {
                    Debug.Log("----------");
                    Debug.Log(x.ID);
                    Debug.Log(x.ToString());
                    Debug.Log(x.Prefab);
                    if (x.Prefab != null) {
                        Component[] components = x.Prefab.GetComponents(typeof(Component));
                        Debug.Log($"{x.Prefab}: {components.Length} components");
                        foreach (Component component in components) {
                            Component[] subComponents = component.GetComponents(typeof(Component));
                            Debug.Log($"{x.Prefab} / {component}: has {subComponents.Length} components");

                            GameObject clonedColourBlind = null;
                            if (GameObjectUtils.GetChildObject(x.Prefab, "Colour Blind") == null) {
                                Debug.Log("Colour blind is null... Attempting to clone?");
                                clonedColourBlind = UnityEngine.Object.Instantiate(existingColourBlind);
                                clonedColourBlind.transform.SetParent(x.Prefab.transform);
                            }

                            if (component is ItemGroupView) {
                                Debug.Log($"{x.Prefab} / {component} is an ItemGroupView");

                                if (textMeshProLabel.GetValue(component) == null) {
                                    textMeshProLabel.SetValue(component, GameObjectUtils.GetChildObject(clonedColourBlind, "Title").GetComponent<TextMeshPro>());
                                }

                                IEnumerable v = (IEnumerable)componentLabelsInfo.GetValue(component);
                                foreach (var aaaa in v) {
                                    Debug.Log($"'{textInfo.GetValue(aaaa)}' for item {((Item)itemInfo.GetValue(aaaa)).ID}");
                                    textInfo.SetValue(aaaa, "T");
                                }
                                componentLabelsInfo.SetValue(component, v);

                                componentLabelsInfo.SetValue(component, ItemGroupViewHackyWorkaround.createColourBlindLabels());

                            }
                            foreach (Component subComponent in subComponents) {
                                Debug.Log($"{x.Prefab} / {component} / {subComponent}");
                            }
                        }
                    }
                }
            };
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
            };
        }
    }

}
