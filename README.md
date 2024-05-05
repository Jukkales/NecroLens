[![Build](https://github.com/Jukkales/NecroLens/actions/workflows/build-plugin.yml/badge.svg)](https://github.com/Jukkales/NecroLens/actions/workflows/build-plugin.yml)
[![Downloads](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fpuni.sh%2Fapi%2Frepository%2Fjukka&query=%24%5B0%5D.DownloadCount&label=Downloads&color=4DC71F
)]()
[![Last Version](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fpuni.sh%2Fapi%2Frepository%2Fjukka&query=%24%5B0%5D.AssemblyVersion&label=release
)]()
[![Last Commit](https://img.shields.io/github/last-commit/Jukkales/NecroLens)]()

[![image](https://discordapp.com/api/guilds/1001823907193552978/embed.png?style=banner2)](https://discord.gg/Zzrcc8kmvy)

### Want to help with localization?
https://crowdin.com/project/necrolens

# NecroLens
```
https://puni.sh/api/repository/jukka
```

<img src="https://raw.githubusercontent.com/Jukkales/NecroLens/main/icon.png" width="100" height="100" align="left">

This Plugin allows you to explore a DeepDungeon with a tool like the HoloLens or GoogleGlass on.
You will be able to see trough walls, see where monster are looking ore be aware of you and many more.

This is an ESP hack (extrasensory perception), so don't use it if you not feel well.

## Installation
For installation instructions, please see my [custom plugin repo](https://github.com/Jukkales/DalamudPlugins).

![image](screen.png)

## Features
 - ESP draws for ever monster
   - Proximity mobs will have a large circle around them. Tey aggro you once you step in
   - Sight mobs wil have a 90° view in front and agro you once they see you in this area
   - Sound mobs are like proximity mobs, but they only aggro you if you run ner to them or touch them
   - Patrols always have a movement direction arrow
 - Detection of dungeon objects like chests, exit, return and so on
   - The plugin can NOT detect whats not there. 
   - Invisible Traps, Chest bombs and Hoards are ServerSide only! (expect you use a Pomander)
 - Highlight of objects once near
 - Respawn and Floor timer
 - Pomander tracker of static floor effects
 - Automatically opens chest for you once near
   - Only when safe, max one time and it very rarely stuck you 
 - `/pomander` command

## Usage
The Plugin is only active in **Palace of the Dead**, **Heaven on High** or **Eureka Orthos**. Once you enter this duties a Window opens up showing floor information and the ESP is drawing.

If you accidentally closed the main window you can bring it back with the `/necrolens` chat command.

## Planned Features
 - Mob Movement tracker
   - Shows you an indicator if its safe to move or if the mob will soon move
 - Accurate aggro ranges
   - Currently the aggro range is fixed but some mobs have slightly bigger or smaller ranges
   - Tracking these and safe these values to be more precise
 - Better remaining kills detection
 - Pomander auto usage
 - Enemy AoE Radar
 - Trap/Hoard location logging

---
_Special thanks to Leonhart for making the icon!_
