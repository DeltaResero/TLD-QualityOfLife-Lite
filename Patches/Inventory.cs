using HarmonyLib;
using Il2Cpp;
using Il2CppTLD.Gear;
using Il2CppVLB;
using MelonLoader;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace QualityOfLife
{
	[HarmonyPatch( typeof( Inventory ), "AddGear" )]
	internal class Patch_Inventory_AddGear
	{
		static void Postfix( Inventory __instance, GearItem gi, bool enableNotificationFlag )
		{
			TravoisContentItem.SetTravois( gi, null );
		}
	}

	[HarmonyPatch( typeof( Inventory ), "Serialize" )]
	internal class Patch_Inventory_Serialize
	{
		static bool Prefix( Inventory __instance )
		{
			return true;
		}

		static void Postfix( Inventory __instance, ref string __result )
		{
			if ( Settings.Instance.EnableMod && Settings.Instance.TravoisPickupWithContents )
			{
				JsonObject? Parsed = InventoryJson.TryParse( __result );
				if ( Parsed == null )
				{
					return;
				}

				JsonArray InnerItems = new();

				Il2CppSystem.Collections.Generic.List<GearItem> ItemList = new();
				TravoisUtil.GetTravoisContentItems( __instance, ItemList );

				foreach ( GearItem Item in ItemList )
				{
					TravoisContentItem ContentItem = Item.GetComponent<TravoisContentItem>();
					if ( ContentItem?.Travois != null )
					{
						JsonObject Obj = new();
						Obj[ "ContainerID" ] = ContentItem.Travois.m_InstanceID;
						Obj[ "InnerItemID" ] = Item.m_InstanceID;
						InnerItems.Add( Obj );
					}
				}

				Parsed[ "QOL_InnerItems" ] = InnerItems;
				__result = Parsed.ToJsonString( InventoryJson.WriteOptions );
				return;
			}
		}
	}

	[HarmonyPatch( typeof( Inventory ), "Deserialize" )]
	internal class Patch_Inventory_Deserialize
	{
		static void Postfix( Inventory __instance, string text )
		{
			if ( Settings.Instance.EnableMod )
			{
				if ( Settings.Instance.TravoisPickupWithContents )
				{
                    JsonObject? Parsed = InventoryJson.TryParse( text );

                    // Backwards compatibility for v1.12.0
                    TryDeserialzeQOL_v1_12_0( __instance, Parsed );

					if ( Parsed != null && Parsed.TryGetPropertyValue( "QOL_InnerItems", out JsonNode? InnerItems ) )
					{
						JsonArray? Array = InnerItems as JsonArray;
						if ( Array != null )
						{
							for ( int Idx = 0; Idx < Array.Count; ++Idx )
							{
								JsonNode? Obj = Array[ Idx ];
								int ContainerID = InventoryJson.GetInt( Obj, "ContainerID" );
								int InnerItemID = InventoryJson.GetInt( Obj, "InnerItemID" );

								GearItem? InnerItem = GearHelper.GetItemByID( __instance.m_Items, InnerItemID );
								GearItem? OuterItem = GearHelper.GetItemByID( __instance.m_Items, ContainerID );

								if ( InnerItem != null && OuterItem != null )
								{
									TravoisContentItem.SetTravois( InnerItem, OuterItem );
								}
							}
						}
					}
				}

                Il2CppSystem.Collections.Generic.List<GearItem> CatTailItems = new();
				__instance.GearInInventory( "GEAR_CattailStalk", CatTailItems );

				foreach ( GearItem Item in CatTailItems )
				{
					CatTailHelper.TryUpdateCalories( Item?.m_FoodItem );
				}
			}
		}

        private static void TryDeserialzeQOL_v1_12_0( Inventory __instance, JsonObject? Parsed )
        {
            if ( Parsed != null && Parsed.TryGetPropertyValue( "QOL_GearContainers", out JsonNode? ArrayNode ) )
            {
                JsonArray? Array = ArrayNode as JsonArray;
                if ( Array != null )
                {
                    for ( int Idx = 0; Idx < Array.Count; ++Idx )
                    {
                        JsonNode? Obj = Array[ Idx ];
                        int InstanceID = InventoryJson.GetInt( Obj, "InstanceID" );
                        string? ContainerSerialized = Obj?[ "ContainerSerialized" ]?.GetValue<string>();

                        GearItem? Item = GearHelper.GetItemByID( __instance.m_Items, InstanceID );
						Container? GearContainer = Item?.GetOrAddComponent<Container>();

						if ( GearContainer != null && ContainerSerialized != null )
						{
							Il2CppSystem.Collections.Generic.List<GearItem> LoadedItems = new();
							GearContainer.Deserialize( ContainerSerialized, LoadedItems );
						}
                    }
                }
            }
        }
    }

	internal static class InventoryJson
	{
		// Escape as little as possible so the game's save text is left unchanged apart from our additions.
		internal static readonly JsonSerializerOptions WriteOptions = new() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

		internal static JsonObject? TryParse( string Text )
		{
			try
			{
				return JsonNode.Parse( Text, documentOptions: new JsonDocumentOptions { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip } ) as JsonObject;
			}
			catch ( Exception Ex )
			{
				MelonLogger.Warning( $"Failed to parse inventory save data: {Ex.Message}" );
				return null;
			}
		}

		internal static int GetInt( JsonNode? Obj, string Key )
		{
			// TinyJSON wrote numbers that may have been saved as floats, so accept any numeric form.
			return Obj?[ Key ] is JsonValue Value && Value.TryGetValue( out double Number ) ? (int)Number : 0;
		}
	}

	[HarmonyPatch( typeof( Inventory ), "GetExtraScentIntensity" )]
	internal class Patch_Inventory_GetCurrentTotalScentIntensity
    {
		static void Postfix( Inventory __instance, ref float __result )
		{
			if ( Settings.Instance.EnableMod && Settings.Instance.TravoisPickupWithContents )
			{
				Il2CppSystem.Collections.Generic.List<GearItem> ItemList = new();
				__instance.GetItems( "GEAR_Travois", ItemList );

				foreach ( GearItem Item in ItemList )
				{
					if ( Item != null && Item.m_Travois != null )
					{
						Container GearContainer = Item.m_Travois.GetComponent<Container>();
						if ( GearContainer != null )
						{
							foreach ( GearItem InnerItem in GearContainer.m_Items )
							{
								if ( InnerItem != null && InnerItem.m_GearItemData != null )
								{
									__result += InnerItem.m_GearItemData.m_ScentIntensity;
								}
							}
						}
					}
				}
			}
		}
	}
}
