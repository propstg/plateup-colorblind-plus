using Colorblind.Colorblind.References;
using Colorblind.Dishes;
using Colorblind.Labels;
using Colorblind.Menus;
using Colorblind.Settings;
using HarmonyLib;
using Kitchen;
using Kitchen.Modules;
using KitchenData;
using KitchenMods;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Colorblind {

    public class ColorblindMod : GameSystemBase, IModInitializer {

        public const string MOD_ID = "blargle.ColorblindPlus";
        public const string MOD_NAME = "Colorblind+";
        public const string MOD_AUTHOR = "blargle";
        public static readonly string MOD_VERSION = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.ToString();

        public static ColorblindService service;

        public static bool registered = false;

        public void PreInject() {
            if (!registered) {
                registered = true;

                ColorblindPreferences.registerPreferences();
                service = new ColorblindService(GameData.Main);
                setupConsentElementUpdateTicksOverridePatch();

                Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(DishService))).ToList()
                    .Select(Activator.CreateInstance).Cast<DishService>().ToList()
                    .ForEach(t => t.setupLabels(service));

                makeIceCreamOrderingConsistentWithAppliance();
                service.addSingleItemLabels(SingleItems.SINGLE_ITEM_LABELS, ColorblindPreferences.ShowStandaloneLabels);
                service.updateLabelStyles();
            }
        }

        public void PostActivate(Mod mod) {
            if (registered) {
                return;
            }

            CustomerView_Update_Patch.verticalOffset = ColorblindPreferences.getFloat(ColorblindPreferences.CustomerNameVerticalOffset);

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MOD_ID);
        }


        protected override void OnUpdate() {
            addTwitchNamesToCustomersForTesting();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void addTwitchNamesToCustomersForTesting() {
            var twitchNameList = World.GetExistingSystem<TwitchNameList>();
            twitchNameList.AddData("Test");
        }

        private void setupConsentElementUpdateTicksOverridePatch() {
            //ConsentElement_UpdateTicks_Patch.overriddenFontAsset = service.getFontFromTextMeshPro();
            //EndPracticeView_OnUpdate_Patch.overriddenFontAsset = service.getFontFromTextMeshPro();
        }

        private void makeIceCreamOrderingConsistentWithAppliance() {
            switch (ColorblindPreferences.getIceCreamLabelStyle()) {
                case IceCreamLabels.SCV:
                    service.setupColorBlindFeatureForItem(new ItemLabelGroup {
                        itemId = ItemReferences.IceCreamServing,
                        itemLabels = ColourBlindLabelCreator.createConsistentIceCreamLabels(),
                    });
                    break;
                case IceCreamLabels.VCS:
                    service.setupColorBlindFeatureForItem(new ItemLabelGroup {
                        itemId = ItemReferences.IceCreamServing,
                        itemLabels = ColourBlindLabelCreator.createConsistentIceCreamLabelsReversed(),
                    });
                    break;
            }
        }

        [HarmonyPatch(typeof(AccessibilityMenu<MenuAction>), "Setup")]
        class AddMenuToPauseMenu {

            public static bool Prefix(AccessibilityMenu<MenuAction> __instance) {
                MethodInfo addSubmenu = __instance.GetType().GetMethod("AddSubmenuButton", BindingFlags.NonPublic | BindingFlags.Instance);
                addSubmenu.Invoke(__instance, new object[] { ColorblindMod.MOD_NAME, typeof(MainColorblindMenu), false });
                return true;
            }
        }

        [HarmonyPatch(typeof(PlayerPauseView), "SetupMenus")]
        class RegisterMenuPatch {

            public static bool Prefix(PlayerPauseView __instance) {
                ModuleList moduleList = (ModuleList) __instance.GetType().GetField("ModuleList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
                MethodInfo addMenu = __instance.GetType().GetMethod("AddMenu", BindingFlags.NonPublic| BindingFlags.Instance);

                addMenu.Invoke(__instance, new object[] { typeof(MainColorblindMenu), new MainColorblindMenu(__instance.ButtonContainer, moduleList) });
                addMenu.Invoke(__instance, new object[] { typeof(AdditionalSettingsMenu), new AdditionalSettingsMenu(__instance.ButtonContainer, moduleList) });
                addMenu.Invoke(__instance, new object[] { typeof(DishesMenu), new DishesMenu(__instance.ButtonContainer, moduleList) });
                addMenu.Invoke(__instance, new object[] { typeof(FontSettingsMenu), new FontSettingsMenu(__instance.ButtonContainer, moduleList, ColorblindMod.service) });
                addMenu.Invoke(__instance, new object[] { typeof(ExperimentalMenu), new ExperimentalMenu(__instance.ButtonContainer, moduleList) });
                return true;
            }
        }

        [Conditional("DEBUG")]
        public static void DebugLog(object message, [CallerFilePath] string callingFilePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null) {
            Log(message, callingFilePath, lineNumber, caller);
        }

        public static void Log(object message, [CallerFilePath] string callingFilePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null) {
            UnityEngine.Debug.Log($"[{MOD_ID}] [{caller}({callingFilePath}:{lineNumber})] {message}");
        }

        public void PostInject() {
        }
    }
}
