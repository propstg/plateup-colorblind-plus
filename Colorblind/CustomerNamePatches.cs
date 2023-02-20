using HarmonyLib;
using Kitchen;
using KitchenLib.Utils;
using UnityEngine;

namespace Colorblind {

    [HarmonyPatch(typeof(CustomerView), "Update")]
    public class CustomerView_Update_Patch {

        private static readonly float DEFAULT_Y = 1.989f;
        public static float verticalOffset = 0f;

        public static void Postfix(PlayerView __instance, CustomerView.ViewData ___Data) {
            GameObject nameContainer = GameObjectUtils.GetChildObject(__instance.gameObject, "Name Container");
            if (___Data.State == CCustomerState.State.AtTable) {
                nameContainer.transform.localPosition = new Vector3(0, DEFAULT_Y + verticalOffset, 0);
            } else {
                nameContainer.transform.localPosition = new Vector3(0, DEFAULT_Y, 0);
            }
        }
    }
}
