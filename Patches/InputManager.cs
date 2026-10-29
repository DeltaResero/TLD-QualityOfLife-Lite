using HarmonyLib;
using Il2Cpp;
using UnityEngine;

namespace QualityOfLife
{
    [HarmonyPatch( typeof( InputManager ), "ExecuteJournalAction" )]
    internal class Patch_InputManager_ExecuteJournalAction
	{
		static bool Prefix()
		{
			if ( Settings.Instance.EnableMod && Settings.Instance.JournalOpensJournal && ModInput.ShouldProcessInput( KeyCode.J ) )
			{
				WidgetUtils.TogglePanelLogState( PanelLogState.DayListStats );
				return false;
			}
			return true;
		}
	}


    [HarmonyPatch( typeof( InputManager ), "GetRadialButtonHeldDown" )]
    internal class Patch_InputManager_GetRadialButtonHeldDown
    {
        static void Postfix( InputManager __instance, ref bool __result )
        {
            if ( Settings.Instance.EnableMod && Settings.Instance.LightSourceKey != KeyCode.None )
            {
                if ( ModInput.GetKey( __instance, Settings.Instance.LightSourceKey ) )
                {
                    __result = true;
                }
            }

            if ( Settings.Instance.EnableMod && Settings.Instance.WeaponKey != KeyCode.None )
            {
                if ( ModInput.GetKey( __instance, Settings.Instance.WeaponKey ) )
                {
                    __result = true;
                }
            }
        }
    }

}
