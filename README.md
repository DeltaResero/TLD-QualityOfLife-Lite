# Quality of Life Lite
A cut-down fork of [VonDoogles](https://github.com/VonDoogles)' [TLD-QualityOfLife](https://github.com/VonDoogles/TLD-QualityOfLife) mod for The Long Dark.  It keeps the UI and control changes that make the game more consistent and easier to control, and drops the rest.  This is a personal version trimmed down to the features I use, so see the original for the complete feature set.

> [!IMPORTANT]
> Tested with The Long Dark 2.58 and MelonLoader v0.7.3.

> [!IMPORTANT]
> Quality of Life Lite requires the ModSettings mod to be installed.  (https://tldmods.com/)
>
> Don't install it alongside the original.  Both change the same menus, so if the original is also loaded this mod switches itself off and logs an error.  It keeps its own settings in `QualityOfLifeLite.json` and leaves the original's untouched.

# Building
The project builds against the game assemblies that MelonLoader generates, so run the game once with MelonLoader installed (and again after each game update) before building.

- Requires the .NET SDK (8 or newer).  The mod itself targets .NET 6, which MelonLoader runs mods on.
- The game folder is found through Steam's library list.  For a non-Steam copy, or if it isn't found, set it with `-p:TLDDir=/path/to/TheLongDark` or a `TLDDir` environment variable.
- `dotnet build -c Debug` only compiles.
- `dotnet build -c Release` also copies `QualityOfLifeLite.dll` into the game's `Mods` folder and creates `TLD-QualityOfLife-Lite-<version>.zip`.

# Features
## Mod
### Enable Mod
Enable / Disable Mod.  Allows turning off all features without changing individual settings.

## Animation
### Play Harvest Anims
When true, plays the new harvest anims, when false uses the old progress bar.

## Crafting
### Remember Filter
Remembers which crafting filter was selected, and restores it when the crafting UI is opened.

## Gameplay
### Item Drop Rotation
The rotation of items when dropped.
- StickNorth - Use base game behaviour.
- PlayerFacing - Drop items facing the same direction as the player.
- Random - Drop items facing a random direction.

## Fire
### Auto-Select Lit Torch
Auto selects the torch as the fire starter if the player is holding a lit torch.

### Auto-Select Mag Lens
If the Mag Lens is able to be used, then auto select it.  A lit torch takes precedence if 'Auto-Select Lit Torch' is enabled.

### Auto-Skip Birch Bark
Auto selects next tinder type when Fire Start UI is opened.  This prevents accidental usage of Birch Bark as tinder.
If tinder is required and Birch Bark is the only tinder available, this does nothing as the next item is still Birch Bark.

### Remember Selection
Remember previous selected items and auto select them.  Above options take precedence if enabled.

## Fishing
### Remember Selection
Remember previous selected items and auto select them.

### Show Remaining Bait
Show remaining bait on fishing UI.

### Show Tipup Condition
Show Tipup condition on fishing UI.

## Input
### Journal Opens Journal
Makes the journal key open the journal, not DLC objectives.

### Highlight Selection
Changes the background color of items on the Harvest, FireStart and IceFishing UI to show which row has keyboard focus.

### UI Extra Controls
Adds extra shortcut keys to various UI screens.
- Affliction:
    - Interact: Treats the selected wound.
    - A / D / MouseWheel: Scrolls the wound list.
- Badges:
    - A / D / MouseWheel: Scrolls the active list.
    - Tab: Cycles between Challenges and Feats.
- BasicMenu:
    - Interact: Selects the highlighted menu option.
- Breakdown:
    - Interact: Starts breakdown.
    - A / D / MouseWheel: Scrolls tool list.
- Clothing:
    - Drop: Drops the selected item.
    - Equip: Toggles the wear status of the selected item.
    - Interact: Examines the selected item.
    - MouseWheel: Scrolls the available items list for the selected clothing slot.
- Confirmation:
    - Interact: Confirms the dialog prompt. (Does not work on input dialogs like Rename.)
- Container:
    - Equip / Interact: Transfers the selected item to the other side.
    - Tab: Changes focus to the other side.
- Cooking:
    - Interact: Start cooking.
    - Drop: Secondary cooking action.
- Cook Water:
    - Interact: Start boiling / melting.
    - MouseWheel: Increase / Decrease the water amount.
- Crafting:
    - Interact: Start crafting.
    - A / D: Increase / Decrease quantity.
- Feed Fire:
    - Interact: Feed the selected item to the fire.
- Fire Start:
    - Interact: Start the fire.
- Gear Select:
    - Interact: Select gear.
    - MouseWheel: Scroll the gear list.
- IceFishing:
    - Interact: Start fishing.
    - A / D / MouseWheel: Increase / Decrease time.
- IceFishing HoleClear:
    - Interact: Start breaking ice.
    - A / D / MouseWheel: Scroll the tool list.
- Inventory:
    - Drop: Drops the selected item.
    - Equip: Equips the selected item.
    - Interact: Examines the selected item.
- Inventory Examine:
    - Escape: Cancel in-progress item repair.
    - A / D / MouseWheel: Increase / Decrease read time.
    - Interact: Start reading / Select action tool / Select action.
- Log:
    - Auto scroll to bottom of the daily list when opened.
    - Interact: Opens the selected sub-screen / Selects the selected item in sub-screens.
- MainMenu:
    - Equip: Toggle selected feat.
    - Interact: Continue on the feats screen.
    - MouseWheel: Scroll feats list.
- Map:
    - A / D: Cycle shown region map.
    - Tab: Toggle world map.
- Milling:
    - Interact: Start repairing.
- Pick Units:
    - Interact: Pick selected number of units.
    - Equip: Pick all units.
- Pick Water:
    - Interact: Take selected amount of water.
    - Equip: Take all water.
- Rest:
    - Interact: Start PassTime / Rest.
    - Tab: Cycle between PassTime / Rest.
    - Drop: Pickup bedroll.
    - A / D / MouseWheel: Increase / Decrease time.
- Select Region Map:
    - Interact: Continue with selected region.
- Snow Shelter Build:
    - Interact: Start building.
- Snow Shelter Interact:
    - Interact: Start selected action.
- Torch Light:
    - Interact: Use selected item.
- Weapon Picker:
    - Equip / Interact: Use selected weapon.
- Safe Cracking:
    - Interact: Opens safe if unlocked.
    - MouseWheel: Rotate safe dial.
    - Precision Rotate: When held, slows down mouse wheel rotation.

### Drop Item in Hands
Which type of items held in player's hands can be dropped with the Drop key. (None, Any, LightSource, NonWeapons)

### QuickSelect Hold Duration
How long a QuickSelect key must be held down to show the Radial UI. (Default: 0.25)

### Accept / Interact
The key for accepting menu choices.

### Drop / Put Back
The key for dropping items.

### Equip / Consume
The key for equipping items or consuming them.

### Objectives
The key to show the objectives menu.

### Trades
The key to show the trades menu.

### Precision Rotate
When this key is held, rotating the safe dial with mouse wheel is more precise.

### Navigate Quick Select
Quick select key to equip Charcoal.

### LightSource Radial
Quick select key to show the LightSource radial UI. (Hold to show menu. Press for default behaviour.)

### Weapon Radial
Quick select key to show the Weapon radial UI. (Hold to show menu. Press for default behaviour.)

## Radial
### Combine Items
The radial menu will only show one item of each type.  Mouse wheel will select which item of that type is used. (IE: Birch Tea, Coffee)
Also sorts the items by Heat status first, then by Calories remaining and finally by HP.

### Show Ruined Food
Always show ruined food items in the radial menu.

### Show Insulated Flask Contents
Always show Insulated Flasks contents in the radial menu.

## Repair
### Repair Colored Amount
Change the color of the repair amount when it is less than the full amount.

## Torch
### Lowest Torch
The radial menu will equip the lowest quality torch.

### Always Select Starter
Always show the torch light UI to select the starter item.  This helps prevent accidental usage of matches.

## Travois
### Show In Radial
When enabled, the travois will appear under the navigation radial menu.  This can be used to deploy the Travois.

## UI
### Buff Notification Offset
A vertical offset applied to the buff notification UI.

### Console Dark Mode
Change the console to use a dark mode color scheme.

### Hypothermia Status Bar
Show hypothermia risk on cold status bar.

### Weight Label
Show a weight label on the hud.
- None - Weight Label is disabled.
- AlwaysOn - Weight Label is always visible.
- AutoHide - Weight Label fades with the other hud elements.

### Wind Status Bar
Show a wind status bar.
- None - Wind Status Bar is disabled.
- DirectionOnly - Only shows wind direction.
- DirectionAndSpeed - Shows both wind direction and speed.
- ShelterOnly - Only shows the shelter status icon.

### Show Stamina Until Full
Show the stamina bar until it is fully recovered.

### Show Temperature Labels
Show labels on the cold and wind status bars that display FeelsLike and WindChill temps.

### ScrollWheel Item Selection
ScrollWheel changes selected item instead of changing the page.
