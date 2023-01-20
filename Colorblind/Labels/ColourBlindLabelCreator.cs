using KitchenLib.Colorblind;
using KitchenLib.References;

namespace Colorblind.Labels {

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
            };
        }
    }
}
