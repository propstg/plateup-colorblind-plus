using Colorblind.Settings;
using Controllers;
using Kitchen;
using KitchenLib.Utils;
using KitchenMods;
using System.Reflection;

namespace Colorblind {

    public class ToggleLabelsSystem : GameSystemBase, IModSystem {

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
            FieldInfo fieldInfo = ReflectionUtils.GetField<PlayerView>("Data");
            PlayerView.ViewData viewData = (PlayerView.ViewData)fieldInfo.GetValue(player);
            ButtonState buttonState = viewData.Inputs.State.SecondaryAction1;

            return buttonState == ButtonState.Pressed;
        }
    }
}
