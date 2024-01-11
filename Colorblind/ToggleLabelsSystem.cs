using Colorblind.Settings;
using Controllers;
using HarmonyLib;
using Kitchen;
using KitchenMods;
using System.Reflection;

namespace Colorblind {

    public class ToggleLabelsSystem : GameSystemBase, IModSystem {

        private FieldInfo playerView_Data;

        protected override void Initialise() {
            base.Initialise();
            playerView_Data = AccessTools.Field(typeof(PlayerView), "Data");
        }

        protected override void OnUpdate() {
            if (ColorblindPreferences.isOn(ColorblindPreferences.ToggleLabelsWithButtonPress)) {
                if (anyLocalPlayerPushingButton()) {
                    ColorblindPreferences.setBool(Pref.AccessibilityColourBlindMode, !ColorblindPreferences.isOn(Pref.AccessibilityColourBlindMode));
                }
            }
        }

        private bool anyLocalPlayerPushingButton() {
            foreach(var player in PlayerInfoManager.FindObjectsOfType<PlayerView>()) {
                if (isReadyDownForPlayer(player)) {
                    return true;
                }
            }

            return false;
        }

        private bool isReadyDownForPlayer(PlayerView player) {
            PlayerView.ViewData viewData = (PlayerView.ViewData)playerView_Data.GetValue(player);
            ButtonState buttonState = viewData.Inputs.State.SecondaryAction1;

            return buttonState == ButtonState.Pressed;
        }
    }
}
