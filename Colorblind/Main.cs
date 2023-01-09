using Colorblind.Labels;
using Colorblind.Menus;
using Colorblind.Settings;
using Kitchen;
using KitchenLib;
using KitchenLib.Event;
using KitchenLib.References;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Colorblind {

    public class ColorblindMod : BaseMod {

        public const string MOD_ID = "blargle.ColorblindPlus";
        public const string MOD_NAME = "Colorblind+";
        public const string MOD_AUTHOR = "blargle";
        public const string MOD_VERSION = "0.0.6";

        private ColorblindService service;

        public ColorblindMod() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, "1.1.2", Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise() {
            ColorblindPreferences.registerPreferences();
            initMenus();
            service = new ColorblindService();

            addLabelsToStirFry();
            addLabelsToTurkey();
            addLabelsToBurgers();
            addLabelsToPizza();
            addLabelsToSalad();
            addLabelsToSteak();
            makeIceCreamOrderingConsistentWithAppliance();
            service.addSingleItemLabels(SingleItems.SINGLE_ITEM_LABELS, ColorblindPreferences.ShowStandaloneLabels);
        }

        private void addLabelsToStirFry() {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.StirFryRaw, ItemReferences.StirFryPlated, ItemReferences.StirFryCooked },
                ColourBlindLabelCreator.createStirFryLabels(),
                ColorblindPreferences.ShowStirFryLabels);
        }

        private void addLabelsToTurkey() {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.TurkeyPlated },
                ColourBlindLabelCreator.createTurkeyLabels(),
                ColorblindPreferences.ShowTurkeyLabels);
        }

        private void addLabelsToBurgers() {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.BurgerUnplated, ItemReferences.BurgerPlated },
                ColourBlindLabelCreator.createBurgerLabels(),
                ColorblindPreferences.ShowBurgerLabels);
        }

        private void addLabelsToPizza() {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.PizzaRaw, ItemReferences.PizzaCooked, ItemReferences.PizzaPlated, ItemReferences.PizzaSlice },
                ColourBlindLabelCreator.createPizzaLabels(),
                ColorblindPreferences.ShowPizzaLabels);
        }

        private void addLabelsToSalad() {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.SaladPlated },
                ColourBlindLabelCreator.createSaladLabels(),
                ColorblindPreferences.ShowSaladLabels);
        }

        private void addLabelsToSteak() {
            clearExistingSteakLabels();
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.SteakPlated },
                ColourBlindLabelCreator.createSteakLabels(),
                ColorblindPreferences.ShowSteakLabels);
            service.addSingleItemLabels(SingleItems.STEAK_SINGLE_ITEM_LABELS, ColorblindPreferences.ShowSteakLabels);
        }

        private void clearExistingSteakLabels() {
            if (!ColorblindPreferences.isOn(ColorblindPreferences.ShowSteakLabels)) {
                return;
            }

            new List<int> { ItemReferences.SteakPlated,
                ItemReferences.SteakRare,ItemReferences.SteakMedium,ItemReferences.SteakWelldone,
                ItemReferences.BonedSteakRare,ItemReferences.BonedSteakMedium,ItemReferences.BonedSteakWelldone,
                ItemReferences.ThickSteakRare,ItemReferences.ThickSteakMedium,ItemReferences.ThickSteakWelldone,
                ItemReferences.ThinSteakRare,ItemReferences.ThinSteakMedium,ItemReferences.ThinSteakWelldone
            }.ForEach(service.setTextToBlankForAllColourBlindChildrenForItem);
        }

        private void makeIceCreamOrderingConsistentWithAppliance() {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.IceCreamServing },
                ColourBlindLabelCreator.createConsistentIceCreamLabels(),
                ColorblindPreferences.ReorderIceCreamLabels);
        }

        private void initMenus() {
            Events.PreferenceMenu_MainMenu_SetupEvent += (s, args) => {
                Type type = args.instance.GetType().GetGenericArguments()[0];
                args.mInfo.Invoke(args.instance, new object[] { MOD_NAME, typeof(MainMenu<>).MakeGenericType(type), false });
            };
            Events.PreferenceMenu_MainMenu_CreateSubmenusEvent += (s, args) => {
                args.Menus.Add(typeof(MainMenu<MainMenuAction>), new MainMenu<MainMenuAction>(args.Container, args.Module_list));
                args.Menus.Add(typeof(DishesMenu<MainMenuAction>), new DishesMenu<MainMenuAction>(args.Container, args.Module_list));
            };
            Events.PreferenceMenu_PauseMenu_SetupEvent += (s, args) => {
                Type type = args.instance.GetType().GetGenericArguments()[0];
                args.mInfo.Invoke(args.instance, new object[] { MOD_NAME, typeof(MainMenu<>).MakeGenericType(type), false });
            };
            Events.PreferenceMenu_PauseMenu_CreateSubmenusEvent += (s, args) => {
                args.Menus.Add(typeof(MainMenu<PauseMenuAction>), new MainMenu<PauseMenuAction>(args.Container, args.Module_list));
                args.Menus.Add(typeof(DishesMenu<PauseMenuAction>), new DishesMenu<PauseMenuAction>(args.Container, args.Module_list));
            };
        }
    }
}
