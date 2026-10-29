using Il2Cpp;
using UnityEngine;

namespace QualityOfLife
{
    public static class TravoisUtil
    {
		public static void DropTravois( GearItem Gear )
		{
			if ( Gear != null && Gear.m_Travois != null && Gear.m_Travois.CanDropTravois() )
			{
				Transform PlayerTrans = GameManager.GetPlayerTransform();
				if ( Gear.m_Travois.TryFindValidTravoisDropPosition( PlayerTrans ) )
				{
					Gear.Drop( 1 );
				}
			}
		}
    }
}