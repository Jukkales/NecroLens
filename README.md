[![Build](https://github.com/Jukkales/NecroLens/actions/workflows/build-plugin.yml/badge.svg)](https://github.com/Jukkales/NecroLens/actions/workflows/build-plugin.yml)
[![Github Downloads (total)](https://img.shields.io/github/downloads/Jukkales/NecroLens/total.svg)]()

# NecroLens

![image](icon.png)

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
 - **EXPERIMENTAL** Automatically opens chest for you once near
   - Only when safe, max one time and it very rarely stuck you 

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

