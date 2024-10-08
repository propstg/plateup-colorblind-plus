﻿using Colorblind.Colorblind.References;
using Kitchen;
using KitchenData;
using System.Collections;
using System.Linq;

namespace Colorblind.Labels {

    public class ColourBlindLabelCreatorUtil : ItemGroupView {

        public static IEnumerable createLabelGroup(ItemLabelGroup itemLabels) {
            return itemLabels.itemLabels.ToList()
                .Select(createLabel).ToList();
        }

        private static ColourBlindLabel createLabel(ItemLabel label) {
            return new ColourBlindLabel { Item = GameData.Main.Get<Item>(label.itemId), Text = label.label };
        }
    }

    public class ColourBlindLabelCreator {

        private static ItemLabel createLabel(int itemId, VariableLabel label) {
            return new ItemLabel { itemId = itemId, label = label.ToString() };
        }

        public static ItemLabel[] createStirFryLabels() {
            return new ItemLabel[] {
                createLabel(ItemReferences.BroccoliChopped, BaseLabels.Broccoli),
                createLabel(ItemReferences.BroccoliChoppedContainerCooked,BaseLabels.Broccoli),
                createLabel(ItemReferences.CarrotChopped, BaseLabels.Carrot),
                createLabel(ItemReferences.CarrotChoppedContainerCooked, BaseLabels.Carrot),
                createLabel(ItemReferences.MeatChopped, BaseLabels.Meat),
                createLabel(ItemReferences.MeatChoppedContainerCooked, BaseLabels.Meat),
                createLabel(ItemReferences.BroccoliServing, BaseLabels.BroccoliSide),
                createLabel(ItemReferences.BambooCooked, BaseLabels.Bamboo),
            };
        }

        public static ItemLabel[] createTurkeyLabels() {
            return new ItemLabel[] {
                createLabel(ItemReferences.TurkeySlice, BaseLabels.Turkey),
                createLabel(ItemReferences.CranberrySauce, BaseLabels.CranberrySauce),
                createLabel(ItemReferences.TurkeyGravy, BaseLabels.Gravy),
                createLabel(ItemReferences.Stuffing, BaseLabels.StuffingPlated),
            };
        }

        public static ItemLabel[] createConsistentIceCreamLabels() {
            return new ItemLabel[] {
                createLabel(ItemReferences.IceCreamStrawberry, BaseLabels.IceCreamStrawberry),
                createLabel(ItemReferences.IceCreamChocolate, BaseLabels.IceCreamChocolate),
                createLabel(ItemReferences.IceCreamVanilla, BaseLabels.IceCreamVanilla),
            };
        }

        public static ItemLabel[] createConsistentIceCreamLabelsReversed() {
            return new ItemLabel[] {
                createLabel(ItemReferences.IceCreamVanilla, BaseLabels.IceCreamVanilla),
                createLabel(ItemReferences.IceCreamChocolate, BaseLabels.IceCreamChocolate),
                createLabel(ItemReferences.IceCreamStrawberry, BaseLabels.IceCreamStrawberry),
            };
        }

        public static ItemLabel[] createBurgerLabels() {
            return new ItemLabel[] {
                createLabel(ItemReferences.CheeseGrated, BaseLabels.Cheese),
                createLabel(ItemReferences.OnionChopped, BaseLabels.Onion),
                createLabel(ItemReferences.TomatoChopped, BaseLabels.Tomato),
            };
        }

        public static ItemLabel[] createPizzaLabels() {
            return new ItemLabel[] {
                createLabel(ItemReferences.CheeseGrated, BaseLabels.Cheese),
                createLabel(ItemReferences.CheeseWrappedCooked, BaseLabels.Cheese),
                createLabel(ItemReferences.MushroomChopped, BaseLabels.Mushroom),
                createLabel(ItemReferences.MushroomCookedWrapped, BaseLabels.Mushroom),
                createLabel(ItemReferences.OnionChopped, BaseLabels.Onion),
                createLabel(ItemReferences.OnionCookedWrapped, BaseLabels.Onion),
            };
        }

        public static ItemLabel[] createSaladLabels() {
            return new ItemLabel[] {
                createLabel(ItemReferences.LettuceChopped, BaseLabels.Lettuce),
                createLabel(ItemReferences.Olive, BaseLabels.Olive),
                createLabel(ItemReferences.OnionChopped, BaseLabels.Onion),
                createLabel(ItemReferences.TomatoChopped, BaseLabels.Tomato),
            };
        }

        public static ItemLabel[] createSteakLabels() {
            return new ItemLabel[] {
                createLabel(ItemReferences.SteakRare, BaseLabels.SteakRegularRare),
                createLabel(ItemReferences.SteakMedium, BaseLabels.SteakRegularMedium),
                createLabel(ItemReferences.SteakWelldone, BaseLabels.SteakRegularWelldone),
                createLabel(ItemReferences.BonedSteakRare, BaseLabels.SteakBoneRare),
                createLabel(ItemReferences.BonedSteakMedium, BaseLabels.SteakBoneMedium),
                createLabel(ItemReferences.BonedSteakWelldone, BaseLabels.SteakBoneWelldone),
                createLabel(ItemReferences.ThickSteakRare, BaseLabels.SteakThickRare),
                createLabel(ItemReferences.ThickSteakMedium, BaseLabels.SteakThickMedium),
                createLabel(ItemReferences.ThickSteakWelldone, BaseLabels.SteakThickWelldone),
                createLabel(ItemReferences.ThinSteakRare, BaseLabels.SteakThinRare),
                createLabel(ItemReferences.ThinSteakMedium, BaseLabels.SteakThinMedium),
                createLabel(ItemReferences.ThinSteakWelldone, BaseLabels.SteakThinWelldone),
                createLabel(ItemReferences.SauceRedPortion, BaseLabels.RedWineJus),
                createLabel(ItemReferences.SauceMushroomPortion, BaseLabels.MushroomSauce),
                createLabel(ItemReferences.TomatoChopped, BaseLabels.SteakTomato),
                createLabel(ItemReferences.MushroomChopped, BaseLabels.SteakMushroom),
                createLabel(ItemReferences.BambooCookedContainerCooked, BaseLabels.Bamboo),
                createLabel(ItemReferences.BambooCooked, BaseLabels.Bamboo),
            };
        }

        public static ItemLabel[] createUncookedDumplingLabels() {
            return new ItemLabel[] {
                createLabel(ItemReferences.MeatChopped, BaseLabels.Meat),
                createLabel(ItemReferences.CarrotChopped, BaseLabels.Carrot),
            };
        }

        public static ItemLabel[] createCookedDumplingsLabels() {
            return new ItemLabel[] {
                createLabel(ItemReferences.CookedDumplings, BaseLabels.Dumplings),
                createLabel(ItemReferences.SeaweedCooked, BaseLabels.Seaweed),
                createLabel(ItemReferences.BambooCooked, BaseLabels.Bamboo),
            };
        }

        public static ItemLabel[] createAppleSaladLabels() {
            return new ItemLabel[] {
                createLabel(ItemReferences.AppleSlices, BaseLabels.Apple),
                createLabel(ItemReferences.LettuceChopped, BaseLabels.Lettuce),
                createLabel(ItemReferences.Mayonnaise, BaseLabels.Mayo),
                createLabel(ItemReferences.NutsIngredient, BaseLabels.Nuts),
            };
        }

        public static ItemLabel[] createPotatoSaladLabels() {
            return new ItemLabel[] {
                createLabel(ItemReferences.PotatoChoppedCooked, BaseLabels.Potato),
                createLabel(ItemReferences.Mayonnaise, BaseLabels.Mayo),
                createLabel(ItemReferences.OnionChopped, BaseLabels.Onion),
            };
        }

        public static ItemLabel[] createBreakfastLabels() {
            return new ItemLabel[] {
                createLabel(ItemReferences.BeansCooked, BaseLabels.Beans),
                createLabel(ItemReferences.BeansServing, BaseLabels.Beans),
                createLabel(ItemReferences.EggCooked, BaseLabels.Egg),
                createLabel(ItemReferences.MushroomChopped, BaseLabels.Mushroom),
                createLabel(ItemReferences.TomatoChopped, BaseLabels.Tomato),
            };
        }
    }
}
