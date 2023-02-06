﻿using Colorblind.Labels;
using Colorblind.Menus;
using Colorblind.Settings;
using Kitchen;
using KitchenLib;
using KitchenLib.Colorblind;
using KitchenLib.Event;
using KitchenLib.References;
using System.Collections.Generic;
using System.Reflection;

namespace Colorblind {

    public class ColorblindMod : BaseMod {

        public const string MOD_ID = "blargle.ColorblindPlus";
        public const string MOD_NAME = "Colorblind+";
        public const string MOD_AUTHOR = "blargle";
        public const string MOD_VERSION = "0.0.19";

        private ColorblindService service;

        public ColorblindMod() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, ">=1.1.3", Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise() {
            ColorblindPreferences.registerPreferences();
            initMenus();

            Events.BuildGameDataEvent += delegate (object s, BuildGameDataEventArgs args) {
                service = new ColorblindService();
                setupConsentElementUpdateTicksOverridePatch();
                addLabelsToStirFry();
                addLabelsToTurkey();
                addLabelsToBurgers();
                addLabelsToPizza();
                addLabelsToSalad();
                addLabelsToSteak();
                addLabelsToDumplings();
                makeIceCreamOrderingConsistentWithAppliance();
                service.addSingleItemLabels(SingleItems.SINGLE_ITEM_LABELS, ColorblindPreferences.ShowStandaloneLabels);
                service.updateLabelStyles();
            };
        }

        private void setupConsentElementUpdateTicksOverridePatch() {
            ConsentElement_UpdateTicks_Patch.overriddenFontAsset = service.getFontFromTextMeshPro();
            EndPracticeView_OnUpdate_Patch.overriddenFontAsset = service.getFontFromTextMeshPro();
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
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.SaladApplePlated },
                ColourBlindLabelCreator.createAppleSaladLabels(),
                ColorblindPreferences.ShowSaladLabels);
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.SaladPotatoPlated },
                ColourBlindLabelCreator.createPotatoSaladLabels(),
                ColorblindPreferences.ShowSaladLabels);
        }

        private void addLabelsToSteak() {
            clearExistingSteakLabels();
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.SteakPlated }, ColourBlindLabelCreator.createSteakLabels(), ColorblindPreferences.ShowSteakLabels);
            service.addSingleItemLabels(SingleItems.STEAK_SINGLE_ITEM_LABELS, ColorblindPreferences.ShowSteakLabels);
        }

        private void addLabelsToDumplings() {
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.DumplingsRaw },
                ColourBlindLabelCreator.createUncookedDumplingLabels(),
                ColorblindPreferences.ShowDumplingLabels);
            service.setupColorblindFeatureForItems(new List<int> { ItemReferences.DumplingsPlated },
                ColourBlindLabelCreator.createCookedDumplingsLabels(),
                ColorblindPreferences.ShowDumplingLabels);
            service.addSingleItemLabels(SingleItems.DUMPLING_SINGLE_ITEM_LABELS, ColorblindPreferences.ShowDumplingLabels);
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
            switch (ColorblindPreferences.getIceCreamLabelStyle()) {
                case IceCreamLabels.SCV:
                    ColorblindUtils.SetupColorBlindFeatureForItem(new ItemLabelGroup {
                        itemId = ItemReferences.IceCreamServing,
                        itemLabels = ColourBlindLabelCreator.createConsistentIceCreamLabels(),
                    });
                    break;
                case IceCreamLabels.VCS:
                    ColorblindUtils.SetupColorBlindFeatureForItem(new ItemLabelGroup {
                        itemId = ItemReferences.IceCreamServing,
                        itemLabels = ColourBlindLabelCreator.createConsistentIceCreamLabelsReversed(),
                    });
                    break;
            }
        }

        private void initMenus() {
            ModsPreferencesMenu<PauseMenuAction>.RegisterMenu(MOD_NAME, typeof(MainMenu<PauseMenuAction>), typeof(PauseMenuAction));
            Events.PreferenceMenu_PauseMenu_CreateSubmenusEvent += (s, args) => {
                args.Menus.Add(typeof(AdditionalSettingsMenu<PauseMenuAction>), new AdditionalSettingsMenu<PauseMenuAction>(args.Container, args.Module_list));
                args.Menus.Add(typeof(DishesMenu<PauseMenuAction>), new DishesMenu<PauseMenuAction>(args.Container, args.Module_list));
                args.Menus.Add(typeof(FontSettingsMenu<PauseMenuAction>), new FontSettingsMenu<PauseMenuAction>(args.Container, args.Module_list, service));
                args.Menus.Add(typeof(MainMenu<PauseMenuAction>), new MainMenu<PauseMenuAction>(args.Container, args.Module_list));
            };
        }
    }
}
