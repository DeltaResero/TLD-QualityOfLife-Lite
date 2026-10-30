using MelonLoader;

namespace QualityOfLife;
internal sealed class QualityOfLifeMod : MelonMod
{
	public override void OnLateInitializeMelon()
	{
		// Every melon is registered by now, whichever order the Mods folder loaded in.
		MelonBase? Original = RegisteredMelons.FirstOrDefault( Melon => Melon != this && IsOriginalMod( Melon ) );
		if ( Original != null )
		{
			LoggerInstance.Error( $"VonDoogles' {Original.Info.Name} ({Original.MelonAssembly.Location}) is also installed.  Both patch the same menus, so {BuildInfo.ModName} is standing down.  Remove one of them." );
			Unregister( $"{Original.Info.Name} is also installed" );
			return;
		}

		Settings.OnLoad();
	}

	static bool IsOriginalMod( MelonBase Melon )
	{
		return Melon.MelonAssembly.Assembly.GetName().Name == "QualityOfLife"
			|| ( Melon.Info.Name == "Quality of Life" && Melon.Info.Author == "VonDoogles" );
	}
}
