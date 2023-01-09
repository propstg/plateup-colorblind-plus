using Kitchen;
using KitchenData;
using KitchenLib.References;
using System.Collections;
using System.Collections.Generic;

namespace Colorblind.Labels {

    public class ColourBlindLabelCreator : ItemGroupView {

        private static ColourBlindLabel createLabel(int itemId, VariableLabel label) {
            return new ColourBlindLabel { Item = GameData.Main.Get<Item>(itemId), Text = label.ToString() };
        }

        public static IEnumerable createStirFryLabels() {
            return new List<ColourBlindLabel> {
                createLabel(ItemReferences.BroccoliChopped, BaseLabels.Broccoli),
                createLabel(ItemReferences.BroccoliChoppedContainerCooked,BaseLabels.Broccoli),
                createLabel(ItemReferences.CarrotChopped, BaseLabels.Carrot),
                createLabel(ItemReferences.CarrotChoppedContainerCooked, BaseLabels.Carrot),
                createLabel(ItemReferences.MeatChopped, BaseLabels.Meat),
                createLabel(ItemReferences.MeatChoppedContainerCooked, BaseLabels.Meat),
                createLabel(ItemReferences.BroccoliServing, BaseLabels.BroccoliSide),
            };
        }

        public static IEnumerable createTurkeyLabels() {
            return new List<ColourBlindLabel> {
                createLabel(ItemReferences.TurkeySlice, BaseLabels.Turkey),
                createLabel(ItemReferences.CranberrySauce, BaseLabels.CranberrySauce),
                createLabel(ItemReferences.TurkeyGravy, BaseLabels.Gravy),
                createLabel(ItemReferences.Stuffing, BaseLabels.StuffingPlated),
            };
        }

        public static IEnumerable createConsistentIceCreamLabels() {
            return new List<ColourBlindLabel> {
                createLabel(ItemReferences.IceCreamStrawberry, BaseLabels.IceCreamStrawberry),
                createLabel(ItemReferences.IceCreamChocolate, BaseLabels.IceCreamChocolate),
                createLabel(ItemReferences.IceCreamVanilla, BaseLabels.IceCreamVanilla),
            };
        }

        public static IEnumerable createBurgerLabels() {
            return new List<ColourBlindLabel> {
                createLabel(ItemReferences.CheeseGrated, BaseLabels.Cheese),
                createLabel(ItemReferences.OnionChopped, BaseLabels.Onion),
                createLabel(ItemReferences.TomatoChopped, BaseLabels.Tomato),
            };
        }

        public static IEnumerable createPizzaLabels() {
            return new List<ColourBlindLabel> {
                createLabel(ItemReferences.CheeseGrated, BaseLabels.Cheese),
                createLabel(ItemReferences.CheeseWrappedCooked, BaseLabels.Cheese),
                createLabel(ItemReferences.MushroomChopped, BaseLabels.Mushroom),
                createLabel(ItemReferences.MushroomCookedWrapped, BaseLabels.Mushroom),
                createLabel(ItemReferences.OnionChopped, BaseLabels.Onion),
                createLabel(ItemReferences.OnionCookedWrapped, BaseLabels.Onion),
            };
        }

        public static IEnumerable createSaladLabels() {
            return new List<ColourBlindLabel> {
                createLabel(ItemReferences.LettuceChopped, BaseLabels.Lettuce),
                createLabel(ItemReferences.Olive, BaseLabels.Olive),
                createLabel(ItemReferences.OnionChopped, BaseLabels.Onion),
                createLabel(ItemReferences.TomatoChopped, BaseLabels.Tomato),
            };
        }

        public static IEnumerable createSteakLabels() {
            return new List<ColourBlindLabel> {
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
            };
        }
    }
}
