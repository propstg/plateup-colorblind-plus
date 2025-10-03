using Colorblind.Settings;
using HarmonyLib;
using Kitchen;
using Kitchen.Modules;
using KitchenMods;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Colorblind {

    /*
    [HarmonyPatch(typeof(ConsentElementTick), "Setup")]
    class ConsentElementTick_Setup_Patch {

        public static TextMeshPro textMeshPro;

        public static void Postfix(ConsentElementTick __instance, SpriteRenderer ___Tick, int sides, bool ready) {
            TextMeshPro tmp = Object.Instantiate<TextMeshPro>(textMeshPro, __instance.transform, true);
            tmp.transform.position = __instance.transform.position;
            tmp.gameObject.SetActive(ready);
            tmp.fontSize = 16;
            tmp.text = ReadyCheckNamesUtil.createPlayerNameString(sides);
            ___Tick.gameObject.SetActive(false);
            SetLayerRecursively(tmp.gameObject, LayerMask.NameToLayer("UI"));
        }

        static void SetLayerRecursively(GameObject obj, int layer) {
            obj.layer = layer;
            foreach (Transform child in obj.transform) {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
    }

    */
    class ReadyCheckNamesUtil {

        public static bool isReadyCheckNamesInstalled => ModPreload.Mods.Any(mod => mod.Name == "Ready Check Names" || mod.Name == "2922176841");
        public static bool shouldSkipPostfix => isReadyCheckNamesInstalled || !ColorblindPreferences.isOn(ColorblindPreferences.NamesInsteadOfChecks);

        public static string createPlayerNameString(int playerId) {
            var player = Players.Main.All().Where(p => p.Index == playerId).FirstOrDefault();
            string color = ColorUtility.ToHtmlStringRGB(player.Profile.Colour);
            return $"<color=#{color}>{getName(player)}</color>";
        }

        private static string getName(PlayerInfo player) {
            if (ColorblindPreferences.isSteamNameSelected()) {
                return player.Username.Substring(0, 2);
            } else if (player.Profile.Name == null || "New chef".Equals(player.Profile.Name) || ColorblindPreferences.getNameStyle() == ColorblindPreferences.NUMBERS_ONLY) {
                return (player.Index + 1).ToString();
            }
            return player.Profile.Name.Substring(0, 2);
        }
    }
}
