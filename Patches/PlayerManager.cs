using HarmonyLib;
using Il2Cpp;
using Il2CppTLD.Gear;
using Il2CppTLD.SaveState;
using UnityEngine;

namespace QualityOfLife
{
	[HarmonyPatch( typeof( PlayerManager ), "PlaceMeshInWorld" )]
    internal class Patch_PlayerManager_PlaceMeshInWorld
	{
		static bool Prefix( PlayerManager __instance )
		{
			if ( __instance.m_ObjectToPlace != null )
			{
				GearItem ItemToPlace = __instance.m_ObjectToPlaceGearItem;
				if ( ItemToPlace != null && ItemToPlace.m_FoodItem != null )
				{
					foreach ( GearItemObject GearObj in GameManager.GetInventoryComponent().m_Items )
					{
						if ( GearObj != null && GearObj.m_GearItem != null && GearObj.m_GearItem.m_InsulatedFlask != null )
						{
							if ( GearObj.m_GearItem.m_InsulatedFlask.IsItemCompatibleWithFlask( ItemToPlace ) )
							{
								GearObj.m_GearItem.m_InsulatedFlask.TryRemoveItem( ItemToPlace );
							}
						}
					}
				}
			}
			return true;
		}
	}

	[HarmonyPatch( typeof( PlayerManager ), "Update" )]
	internal class Patch_PlayerManager_Update
	{
		static KeyCodeAction QuickSelectLightSource = new KeyCodeAction()
		{
			KeyCodeFunc = () => Settings.Instance.LightSourceKey,
			OnHold = () => OnLightSourceHold(),
			OnTap = () => OnLightSourceTap()
		};

		static KeyCodeAction QuickSelectWeapon = new KeyCodeAction()
		{
			KeyCodeFunc = () => Settings.Instance.WeaponKey,
			OnHold = () => OnWeaponHold(),
			OnTap = () => OnWeaponTap()
		};


        static bool Prefix( PlayerManager __instance )
		{
			if ( !Settings.Instance.EnableMod )
			{
				return true;
			}

            bool bCanDropItemInHands = true;
			bool bDropKeyDown = ModInput.GetKeyDown( __instance, Settings.Instance.DropKey );

			if ( bDropKeyDown && __instance.GetControlMode() == PlayerControlMode.BigCarry )
			{
				bCanDropItemInHands = false;
				InputManager.ExecuteHolsterAction();
			}

			if ( bCanDropItemInHands && bDropKeyDown && __instance.m_ItemInHands != null )
			{
				bCanDropItemInHands = bCanDropItemInHands && __instance.CheckIfCanDropGearItem( __instance.m_ItemInHands );
				bCanDropItemInHands = bCanDropItemInHands && !__instance.IsInspectModeActive();
				bCanDropItemInHands = bCanDropItemInHands && !InterfaceManager.IsPanelEnabled<Panel_Inventory>();

				PlayerAnimation PlayerAnim = GameManager.GetPlayerAnimationComponent();
				if ( PlayerAnim != null )
				{
					PlayerAnimation.State State = PlayerAnim.GetState();
					if ( State != PlayerAnimation.State.Showing )
					{
						bCanDropItemInHands = false;
#if DEBUG
						Debug.Log( string.Format( "Can't drop item in hand during PlayerAnimation.State ({0})", State.ToString() ) );
#endif
					}
				}

				if ( bCanDropItemInHands )
				{
					switch ( Settings.Instance.DropItemInHands )
					{
						case DropItemType.Any:
							__instance.m_ItemInHands.Drop( 1 );
							break;

						case DropItemType.LightSource:
							if ( IsLightSource( __instance.m_ItemInHands ) )
							{
								__instance.m_ItemInHands.Drop( 1 );
							}
							break;

						case DropItemType.NonWeapons:
							if ( !__instance.m_ItemInHands.IsWeapon() )
							{
								__instance.m_ItemInHands.Drop( 1 );
							}
							break;

						case DropItemType.None:
							break;
					}
				}
			}

            QuickSelectLightSource.Update();
			QuickSelectWeapon.Update();

            if ( ModInput.GetKeyDown( __instance, Settings.Instance.NavigateKey ) )
            {
                Inventory Inv = GameManager.GetInventoryComponent();
				if ( Inv != null )
				{
					GearItem Charcoal = Inv.GetNonRuinedItem( "GEAR_Charcoal" );
					if ( Charcoal != null )
					{
						__instance.UseInventoryItem( Charcoal );
					}
				}
            }

			if ( ModInput.GetKeyDown( __instance, Settings.Instance.ObjectivesKey ) )
			{
				WidgetUtils.ToggleMiniNavPanel<Panel_MissionsStory>();
			}

			if ( ModInput.GetKeyDown( __instance, Settings.Instance.TradesKey ) )
			{
				WidgetUtils.TogglePanelLogState( PanelLogState.Trader );
			}

			return true;
		}

		static void OnLightSourceHold()
		{
            Panel_ActionsRadial ActionsRadial = InterfaceManager.GetPanel<Panel_ActionsRadial>();
            if ( ActionsRadial != null )
            {
                ActionsRadial.ShowLightSourceRadial();
            }
        }

        static void OnLightSourceTap()
		{
			InputManager.ExecuteLightSourceAction();
        }

		static void OnWeaponHold()
		{
            Panel_ActionsRadial ActionsRadial = InterfaceManager.GetPanel<Panel_ActionsRadial>();
            if ( ActionsRadial != null )
            {
                ActionsRadial.ShowWeaponRadial();
            }
        }

        static void OnWeaponTap()
		{
			InputManager.ExecuteFirearmAction();
		}

        private static bool IsLightSource( GearItem Item )
        {
			if ( Item != null )
			{
				if ( Item.m_FlareItem != null || Item.m_KeroseneLampItem != null || Item.m_TorchItem != null || Item.m_MatchesItem != null )
				{
					return true;
				}
			}
			return false;
        }
    }

	[HarmonyPatch( typeof( PlayerManager ), "EnterInspectGearMode", new Type[] { typeof( GearItem ), typeof( Container ), typeof( IceFishingHole ), typeof( Harvestable ), typeof( CookingPotItem ) } )]
    internal class Patch_PlayerManager_EnterInspectGearMode
	{
#if false // 2024-06-26: Disabled for now until I have time to thoroughly investigate it. 
        static bool Prefix( PlayerManager __instance, GearItem gear, Container c, IceFishingHole hole, Harvestable h, CookingPotItem pot )
		{
			if ( Settings.Instance.EnableMod && ModInput.GetKey( __instance, Settings.Instance.AutoPickupKey ) )
			{
				Inventory Inv = GameManager.GetInventoryComponent();
				if ( Inv != null )
				{
					Inv.AddGear( gear, true );
					return false;
				}
			}
			return true;
		}
#endif

        static void Postfix( PlayerManager __instance, GearItem gear, Container c, IceFishingHole hole, Harvestable h, CookingPotItem pot )
		{
			if ( Settings.Instance.EnableMod && Settings.Instance.TravoisPickupWithContents )
			{
				Panel_HUD HUD = InterfaceManager.GetPanel<Panel_HUD>();

				if ( HUD != null && gear != null && gear.m_Travois != null )
				{
					MeasurementUnits Units = BaseStateSingleton<SettingsState>.Instance.m_Units;

					WidgetUtils.SetActive( HUD.m_InspectMode_Equip, true );
					WidgetUtils.SetLabelText( HUD.m_InspectMode_Equip, "Transfer" );
					WidgetUtils.SetLabelText( HUD.m_InspectMode_Weight, gear.GetItemWeightKG().ToFormattedStringWithUnits() );

					if ( HUD.m_InspectMode_ButtonLayout != null )
					{
						HUD.m_InspectMode_ButtonLayout.Reposition();
					}
				}
			}
        }
    }

	[HarmonyPatch( typeof( PlayerManager ), "OnEquipFromStandardGearItemInspection" )]
	internal class Patch_PlayerManager_OnEquipFromStandardGearItemInspection
	{
		static bool Prefix( PlayerManager __instance )
		{
			if ( Settings.Instance.EnableMod && Settings.Instance.TravoisPickupWithContents )
			{
				GearItem Gear = __instance.GearItemBeingInspected();
				if ( Gear != null && Gear.m_Travois != null )
				{
					ContainerInteraction Interaction = Gear.m_Travois.BigCarryItem.m_Container.GetComponent<ContainerInteraction>();
					if ( Interaction != null )
					{
						__instance.ExitInspectGearMode( true );
						WidgetUtils.SetActive( Gear, false );
						Interaction.PerformInteraction();
					}

					return false;
				}
			}
			return true;
		}
	}

}
