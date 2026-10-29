using HarmonyLib;
using Il2Cpp;

namespace QualityOfLife
{
    [HarmonyPatch( typeof( Panel_PickUnits ), "Update" )]
    internal class Patch_Panel_PickUnits_Update
    {
        static void Postfix( Panel_PickUnits __instance )
        {
            if ( Settings.Instance.EnableMod && Settings.Instance.UIExtraControls )
            {
                if ( ModInput.GetKeyDown( __instance, Settings.Instance.InteractKey ) )
                {
                    __instance.OnExecute();
                }

                if ( ModInput.GetKeyDown( __instance, Settings.Instance.EquipKey ) )
                {
                    __instance.OnExecuteAll();
                }
            }
        }
    }
}
