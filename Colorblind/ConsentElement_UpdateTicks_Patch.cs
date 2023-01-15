﻿using Colorblind.Settings;
using HarmonyLib;
using Kitchen;
using Kitchen.Modules;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Colorblind {

    [HarmonyPatch(typeof(ConsentElement), "UpdateTicks")]
    class ConsentElement_UpdateTicks_Patch {

        public static TMP_FontAsset overriddenFontAsset;

        public static void Postfix(TextMeshPro ___Ticks, Dictionary<int, bool> ___Consents) {
            if (!ColorblindPreferences.isOn(ColorblindPreferences.NamesInsteadOfChecks)) {
                return;
            }

            string newText = "";
            foreach (KeyValuePair<int, bool> consent in ___Consents) {
                if (consent.Value) {
                    if (newText.Length > 0) {
                        newText += ", ";
                    }

                    var player = Players.Main.Get(consent.Key);
                    string color = ColorUtility.ToHtmlStringRGB(player.Profile.Colour);
                    string name = player.Name.Substring(0, 2);
                    newText += $"<color=#{color}>{name}</color>";
                }
            }
            ___Ticks.text = newText;
            ___Ticks.font = overriddenFontAsset;
        }
    }
}