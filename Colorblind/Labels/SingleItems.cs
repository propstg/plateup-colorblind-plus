using Colorblind.Colorblind.References;
using System.Collections.Generic;

namespace Colorblind.Labels {

    public static class SingleItems {

        public static readonly Dictionary<int, VariableLabel> SINGLE_ITEM_LABELS = new Dictionary<int, VariableLabel>() {
            { ItemReferences.Apple, BaseLabels.Apple },
            { ItemReferences.AppleSlices, BaseLabels.Apple },
            { ItemReferences.Breadcrumbs, BaseLabels.Breadcrumbs },
            { ItemReferences.BroccoliChopped, BaseLabels.Broccoli },
            { ItemReferences.CarrotChopped, BaseLabels.Carrot },
            { ItemReferences.CheeseGrated, BaseLabels.Cheese },
            { ItemReferences.CranberriesChopped, BaseLabels.Cranberry },
            { ItemReferences.CranberrySauce, BaseLabels.CranberrySauce },
            { ItemReferences.LettuceChopped, BaseLabels.Lettuce },
            { ItemReferences.Mayonnaise, BaseLabels.Mayo },
            { ItemReferences.MeatChopped, BaseLabels.Meat },
            { ItemReferences.MushroomChopped, BaseLabels.Mushroom },
            { ItemReferences.NutsChopped, BaseLabels.Nuts },
            { ItemReferences.NutsIngredient, BaseLabels.Nuts },
            { ItemReferences.Olive, BaseLabels.Olive },
            { ItemReferences.OnionChopped, BaseLabels.Onion },
            { ItemReferences.Seaweed, BaseLabels.Seaweed },
            { ItemReferences.SeaweedCooked, BaseLabels.Seaweed },
            { ItemReferences.Stuffing, BaseLabels.Stuffing },
            { ItemReferences.StuffingRaw, BaseLabels.Stuffing },
            { ItemReferences.TomatoChopped, BaseLabels.Tomato },
            { ItemReferences.TurkeySlice, BaseLabels.Turkey },
        };

        public static readonly Dictionary<int, VariableLabel> STEAK_SINGLE_ITEM_LABELS = new Dictionary<int, VariableLabel>() {
            { ItemReferences.SteakRare, BaseLabels.SteakRegularRare },
            { ItemReferences.SteakMedium, BaseLabels.SteakRegularMedium },
            { ItemReferences.SteakWelldone, BaseLabels.SteakRegularWelldone },
            { ItemReferences.BonedSteakRare, BaseLabels.SteakBoneRare },
            { ItemReferences.BonedSteakMedium, BaseLabels.SteakBoneMedium },
            { ItemReferences.BonedSteakWelldone, BaseLabels.SteakBoneWelldone },
            { ItemReferences.ThickSteakRare, BaseLabels.SteakThickRare },
            { ItemReferences.ThickSteakMedium, BaseLabels.SteakThickMedium },
            { ItemReferences.ThickSteakWelldone, BaseLabels.SteakThickWelldone },
            { ItemReferences.ThinSteakRare, BaseLabels.SteakThinRare },
            { ItemReferences.ThinSteakMedium, BaseLabels.SteakThinMedium },
            { ItemReferences.ThinSteakWelldone, BaseLabels.SteakThinWelldone },
            { ItemReferences.Meat, BaseLabels.SteakRegular },
            { ItemReferences.MeatThick, BaseLabels.SteakThick },
            { ItemReferences.MeatThin, BaseLabels.SteakThin },
            { ItemReferences.MeatBoned, BaseLabels.SteakBone },
        };

        public static readonly Dictionary<int, VariableLabel> DUMPLING_SINGLE_ITEM_LABELS = new Dictionary<int, VariableLabel>() {
            { ItemReferences.CookedDumplings, BaseLabels.Dumplings },
            { ItemReferences.PreparedDumplings, BaseLabels.Dumplings },
        };

        public static readonly Dictionary<int, VariableLabel> BREAKFAST_SINGLE_ITEM_LABELS = new Dictionary<int, VariableLabel>() {
            { ItemReferences.BeansRawPot, BaseLabels.Beans },
            { ItemReferences.BeansServing, BaseLabels.Beans },
            { ItemReferences.Egg, BaseLabels.Egg },
            { ItemReferences.EggCooked, BaseLabels.Egg },
            { ItemReferences.EggCracked, BaseLabels.Egg },
        };
    }
}
