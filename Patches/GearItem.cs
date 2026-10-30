using HarmonyLib;
using Il2Cpp;
using UnityEngine;

namespace QualityOfLife
{
    [HarmonyPatch( typeof( GearItem ), "StickToGroundAtPlayerFeet" )]
    internal class Patch_GearItem_StickToGroundAtPlayerFeet
    {
        static void Postfix( GearItem __instance, Vector3 pos )
        {
            if ( __instance != null && Settings.Instance.EnableMod && Settings.Instance.ItemDropRotation != ItemDropRotationType.StickNorth )
            {
                GameObject GameObj = __instance.GetInteractiveObject();
                if ( GameObj != null )
                {
                    float Angle = UnityEngine.Random.value * 360;

					if ( Settings.Instance.ItemDropRotation == ItemDropRotationType.PlayerFacing )
					{
						Transform PlayerTransform = GameManager.GetPlayerTransform();
						if ( PlayerTransform != null )
						{
							Angle = GameManager.GetPlayerTransform().eulerAngles.y;
						}
					}

                    // Turn around the item's own up so it stays flush on slopes, unless that axis lies
                    // sideways.  Items like the rifle are laid down by tilting the whole model, and turning
                    // around a sideways axis would roll them up onto their end.
                    Vector3 Up = GameObj.transform.up;
                    if ( Vector3.Angle( Up, Vector3.up ) > 45f )
                    {
                        Up = Vector3.up;
                    }
                    Quaternion Rot = Quaternion.AngleAxis( Angle, Up ) * GameObj.transform.rotation;
                    GameObj.transform.rotation = Rot;
                }
            }
        }
    }
}
