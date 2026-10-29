using ModSettings;
using UnityEngine;

namespace QualityOfLife
{
    internal enum DropItemType
    {
        // Disable dropping item in hand.
        None = 0,
        // Allow dropping any item in hand.
        Any,
        // Only drop light source items in hand.
        LightSource,
        // Allow dropping any item in hand that isn't a weapon.
        NonWeapons
    }

	internal enum ItemDropRotationType
	{
		// Use base game functionality.
		StickNorth = 0,
		// Drop items facing the same direction as the player.
		PlayerFacing,
		// Drop items facing a random diration.
		Random
	}

    internal enum WeightLabelType
    {
        // Weight Label is disabled.
        None = 0,
        // Weight Label is always visible.
        AlwaysOn,
        // Weight Label auto fades with the other hud elements.
        AutoHide
    }

    internal enum WindStatusType
    {
        // Wind StatusBar is disabled.
        None = 0,
        // Wind StatusBar only shows direction.
        DirectionOnly,
        // Wind StatusBar shows both direction and speed.
        DirectionAndSpeed,
		// Wind StatusBar only shows the shelter status icon.
		ShelterOnly,
    }

    internal class Settings : JsonModSettings
    {
        [Section( "Mod" )]
        [Name( "Enable Mod" )]
        [Description( "Enable / Disable Mod.  Allows turning off all features without changing individual settings." )]
        public bool EnableMod = true;


		[Section( "Animation" )]
		[Name( "Play Harvest Anims" )]
		[Description( "When true, plays the new harvest anims, when false uses the old progress bar." )]
		public bool AnimPlayHarvest = true;


        [Section( "Crafting" )]
        [Name( "Remember Filter" )]
        [Description( "Remembers which crafting filter was selected, and restores it when the crafting UI is opened." )]
        public bool CraftRememberFilter = true;


        [Section( "Difficulty" )]
		[Name( "Allow Take Torch" )]
		[Description( "Allows taking torchs from lit fires." )]
		public bool FireAllowTakeTortch = true;

		[Name( "Item Drop Rotation" )]
		[Description( "The rotation of items when dropped.\n"+
					  "StickNorth - Use base game behaviour.\n"+
					  "PlayerFacing - Drop items facing the same direction as the player.\n"+
					  "Random - Drop items facing a random diration." )]
		public ItemDropRotationType ItemDropRotation = ItemDropRotationType.PlayerFacing;

        [Name( "Safehouse Day Time Only" )]
        [Description( "Restrict using safehouse customization to day time only." )]
        public bool SafehouseDayTimeOnly = true;


        [Section( "Fire" )]
        [Name( "Auto-Select Lit Torch" )]
        [Description( "Auto selects the torch as the fire starter if the player is holding a lit torch." )]
        public bool FireAutoSelectLitTorch = true;

        [Name( "Auto-Select Mag Lens" )]
        [Description( "If the the Mag Lens is able to be used, then auto select it.  A lit torch takes precedence if 'Auto-Select Lit Torch' is enabled." )]
        public bool FireAutoSelectMagLens = true;

        [Name( "Auto-Skip Birch Bark" )]
        [Description( "Auto selects next tinder type when Fire Start UI is opened.  This prevents accidental usage of Birch Bark as tinder.\nIf tinder is required and Birch Bark is the only tinder available, this does nothing as the next item is still Birch Bark." )]
        public bool FireAutoSkipBirchBark = true;

        [Name( "Remember Selection" )]
        [Description( "Remember previous selected items and auto select them.  Above options take precedence is enabled." )]
        public bool FireRememberSelection = true;


		[Section( "Fishing" )]
		[Name( "Remember Selection" )]
		[Description( "Remember previous selected items and auto select them." )]
		public bool FishingRememberSelection = true;

		[Name( "Show Remaining Bait" )]
		[Description( "Show remaining bait on fishing UI." )]
		public bool FishingShowRemainingBait = true;

		[Name( "Show Tipup Condition" )]
		[Description( "Show Tipup condition on fishing UI." )]
		public bool FishingShowTipupCondition = true;


        [Section( "Input" )]
        [Name( "Journal Opens Journal" )]
        [Description( "Makes the journal key open the journal, not DLC objectives." )]
        public bool JournalOpensJournal = true;

        [Name( "Highlight Selection" )]
        [Description( "Changes the background color of items on the Harvest, FireStart and IceFishing UI to show which row has keyboard focus." )]
        public bool HighlightSelection = true;

        [Name( "UI Extra Controls" )]
        [Description( "Adds extra shortcut keys to various UI. (See README.md)" )]
        public bool UIExtraControls = true;

        [Name( "Drop Item in Hands" )]
        [Description( "Which type of items held in player's hands can be dropped with the Drop key. (None, Any, LightSource, NonWeapons)" )]
        public DropItemType DropItemInHands = DropItemType.Any;

        [Name( "QuickSelect Hold Duration" )]
        [Description( "Hold long QuickSelect Key must be held down to show Radial UI. (Default: 0.25)" )]
        public float QuickSelectHoldDuration = 0.25f;

        [Name( "Accept / Interact" )]
        [Description( "The key for accepting menu choices." )]
        public KeyCode InteractKey = KeyCode.E;

        [Name( "Drop / Put Back" )]
        [Description( "The key for dropping items." )]
        public KeyCode DropKey = KeyCode.Q;

        [Name( "Equip / Consume" )]
        [Description( "The key equipping items or consuming them." )]
        public KeyCode EquipKey = KeyCode.Space;

        [Name( "Objectives" )]
        [Description( "The key to show the objectives menu." )]
        public KeyCode ObjectivesKey = KeyCode.O;

        [Name( "Trades" )]
        [Description( "The key to show the trades menu." )]
        public KeyCode TradesKey = KeyCode.T;

        [Name( "Precision Rotate" )]
        [Description( "When this key is held, rotating the safe dial with mouse wheel is more precise." )]
        public KeyCode PrecisionRotateKey = KeyCode.LeftShift;

        [Name( "Navigate Quick Select" )]
        [Description( "Quick select key to equip Charcoal." )]
        public KeyCode NavigateKey = KeyCode.BackQuote;

        [Name( "LightSource Radial" )]
        [Description( "Quick select key to show the LightSource radial UI. (Hold to show menu. Press for default behaviour.)" )]
        public KeyCode LightSourceKey = KeyCode.Alpha5;

        [Name( "Weapon Radial" )]
        [Description( "Quick select key to show the Weapon radial UI." )]
        public KeyCode WeaponKey = KeyCode.Alpha6;


        [Section( "Radial" )]
        [Name( "Combine Items" )]
        [Description( "The radial menu will only show one item of each type.\n" +
                      "Mouse wheel will select which item of that type is used. (IE: Birch Tea, Coffee)\n" +
                      "Also sorts the items by Heat status first, then by Calories remaining and finally by HP." )]
        public bool RadialCombineItems = true;

        [Name( "Show Ruined Food" )]
        [Description( "Always show ruined food items in the radial menu." )]
        public bool RadialShowRuinedFood = true;

		[Name( "Show Insulated Flask Contents" )]
		[Description( "Always show Insulated Flasks contents in the radial menu." )]
		public bool RadialShowInsulatedFlaskContents = true;


        [Section( "Repair" )]
        [Name( "Repair Colored Amount" )]
        [Description( "Change the color of the repair amount when it is less than the full amount." )]
        public bool RepairColoredAmount = true;


        [Section( "Torch" )]
        [Name( "Lowest Torch" )]
        [Description( "The radial menu will equip the lowest quality torch." )]
        public bool TorchUseLowest = true;

        [Name( "Always Select Starter" )]
        [Description( "Always show the torch light UI to select the starter item.  This helps prevent accidental usage of matches." )]
        public bool TorchLightAlwaysShow = true;


		[Section( "Travois" )]
		[Name( "Show In Radial" )]
		[Description( "When enabled, the travois will appear under the navigation radial menu.  This can be used to deploy the Travois." )]
		public bool TravoisShowInRadial = true;


        [Section( "UI" )]
        [Name( "Buff Notification Offset" )]
        [Description( "A vertical offset applied to the buff notification UI." )]
        public float BuffOffsetVertical = 0.5f;

        [Name( "Console Dark Mode" )]
        [Description( "Change the console to use a dark mode color scheme." )]
        public bool ConsoleDarkMode = true;

        [Name( "Hypothermia Status Bar" )]
        [Description( "Show hypothermia risk on cold status bar." )]
        public bool HypoStatusBar = true;

        [Name( "Weight Label" )]
        [Description( "Show a weight label on the hud." )]
        public WeightLabelType WeightLabel = WeightLabelType.AutoHide;

        [Name( "Wind Status Bar" )]
        [Description( "Show a wind status bar." )]
        public WindStatusType WindStatusBar = WindStatusType.DirectionAndSpeed;

        [Name( "Show Stamina Until Full" )]
        [Description( "Show the stamina bar until it is fully recovered." )]
        public bool ShowStaminaUntilFull = true;

		[Name( "Show Temperature Labels" )]
		[Description( "Show labels on the cold and wind status bars that display FeelsLike and WindChill temps." )]
		public bool ShowTemperatureLabels = true;

        [Name( "ScrollWheel Item Selection" )]
        [Description( "ScrollWheel changes selected item istead of changing the page." )]
        public bool ScrollWheelItemSelection = true;


        public static Settings Instance = new();

        public static void OnLoad()
        {
            Instance.AddToModSettings( "Quality of Life" );
        }

        protected override void OnConfirm()
        {
            base.OnConfirm();
            Patch_uConsole_Start.UpdateConsoleColor();
        }
    }
}