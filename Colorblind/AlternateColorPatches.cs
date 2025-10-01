using HarmonyLib;
using Kitchen;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;

namespace Colorblind {

    [HarmonyPatch(typeof(ItemGroupView), "PerformUpdate")]
    public static class ItemGroupView_PerformUpdate_Patch {

        public static bool enabled = false;
        public static bool inverted = false;

        public static void Postfix(TextMeshPro ___ColourblindLabel) {
            if (___ColourblindLabel != null && enabled) {
                string alternateColor = inverted ? "444" : "777";
                ___ColourblindLabel.text = string.Concat(
                    Regex.Matches(___ColourblindLabel.text, "[A-Z][a-z]*")
                        .Cast<Match>()
                        .Select((chunk, index) => index % 2 != 0 ? $"<color=#{alternateColor}>{chunk.Value}</color>" : chunk.Value));
            }
        }
    }
}
