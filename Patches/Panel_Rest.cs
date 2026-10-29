using HarmonyLib;
using Il2Cpp;
using UnityEngine;

namespace QualityOfLife
{

    [HarmonyPatch( typeof( Panel_Rest ), "Update" )]
    internal class Patch_Panel_Rest_Update
    {
        static void Postfix( Panel_Rest __instance )
        {
            if ( Settings.Instance.EnableMod && Settings.Instance.UIExtraControls )
            {
                if ( ModInput.GetKeyDown( __instance, Settings.Instance.InteractKey ) )
                {
                    if ( __instance.m_ShowPassTime && __instance.m_PassTimeButtonObject.activeInHierarchy )
                    {
                        __instance.OnPassTime();
                    }
                    else if ( __instance.m_SleepButton.activeInHierarchy )
                    {
                        __instance.OnRest();
                    }
                }

                if ( ModInput.GetKeyDown( __instance, KeyCode.Tab ) )
                {
                    if ( __instance.m_ShowPassTime && !__instance.m_ShowPassTimeOnly )
                    {
                        __instance.OnSelectRest();
                    }
                    else if ( !__instance.m_ShowPassTime )
                    {
                        __instance.OnSelectPassTime();
                    }
                }

                if ( ModInput.GetKeyDown( __instance, Settings.Instance.DropKey ) )
                {
                    __instance.OnPickUp();
                }

                float Scroll = InputManager.GetScroll( __instance );
                if ( Scroll < 0 || ModInput.GetKeyDown( __instance, KeyCode.A ) )
                {
                    __instance.OnDecreaseHours();
                }
                else if ( Scroll > 0 || ModInput.GetKeyDown( __instance, KeyCode.D ) )
                {
                    __instance.OnIncreaseHours();
                }
            }
        }
    }

}
