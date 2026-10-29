using HarmonyLib;
using Il2Cpp;

namespace QualityOfLife
{
    [HarmonyPatch( typeof( Panel_ActionPicker ), "TakeTorchCallback" )]
    internal class Patch_Panel_ActionPicker_TakeTorchCallback
    {
        static bool Prefix( Panel_ActionPicker __instance )
        {
            if ( Settings.Instance.EnableMod && !Settings.Instance.FireAllowTakeTortch )
            {
                GameAudioManager.PlayGUIError();
                return false;
            }
            return true;
        }
    }

}
