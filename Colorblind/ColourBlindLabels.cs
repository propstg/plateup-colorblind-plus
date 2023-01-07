using Kitchen;
using KitchenData;
using KitchenLib.References;
using System.Collections;
using System.Collections.Generic;

namespace Colorblind {

    public static class SingleItems {

        public static readonly Dictionary<int, string> SINGLE_ITEM_LABELS = new Dictionary<int, string>() {
            { ItemReferences.Breadcrumbs, "Bcrumb" },
            { ItemReferences.BroccoliChopped, "B" },
            { ItemReferences.CarrotChopped, "C" },
            { ItemReferences.CheeseGrated, "Ch" },
            { ItemReferences.CranberriesChopped, "Cr" },
            { ItemReferences.CranberrySauce, "Csauce" },
            { ItemReferences.LettuceChopped, "L" },
            { ItemReferences.MushroomChopped, "Mu" },
            { ItemReferences.Olive, "Ol" },
            { ItemReferences.OnionChopped, "O" },
            { ItemReferences.Stuffing, "Stuff" },
            { ItemReferences.StuffingRaw, "Stuff" },
            { ItemReferences.TomatoChopped, "T" },
            { ItemReferences.TurkeySlice, "Tu" },
        };
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

        public static IEnumerable createPizzaLabels() {
            return new List<ColourBlindLabel> {
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.CheeseGrated), Text = "Ch" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.CheeseWrappedCooked), Text = "Ch" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.MushroomChopped), Text = "Mu" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.MushroomCookedWrapped), Text = "Mu" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.OnionChopped), Text = "O" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.OnionCookedWrapped), Text = "O" },
            };
        }

        public static IEnumerable createSaladLabels() {
            return new List<ColourBlindLabel> {
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.LettuceChopped), Text = "L" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.Olive), Text = "Ol" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.OnionChopped), Text = "O" },
                new ColourBlindLabel { Item = GameData.Main.Get<Item>(ItemReferences.TomatoChopped), Text = "T" },
            };
        }
    }
}
