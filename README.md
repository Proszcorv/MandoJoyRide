# MandoJoyRide

A 2D pixel-art endless runner built in Unity, starring a Mandalorian-style bounty hunter who uses a jetpack to dodge obstacles across the galaxy.

## Gameplay

- **Jetpack flight**: hold the thrust button to fly, release to fall. Thrusting builds up heat on a thermometer-style meter — let it cool down, or push your luck and risk exploding if you hold it too long at max heat.
- **Obstacles**: rocks on the ground and TIE fighters in the air, each with their own relative speed, spawning at an increasing rate as the run goes on.
- **Score**: increases automatically over time. Reach a high score to unlock the option to travel to a new planet.
- **Planets**: start on Tatooine; unlock new planets (currently adding Mustafar) with their own background, terrain, and color palette.

## Controls

- **Jump / Left Mouse Button**: activate jetpack thrust

## Project structure

```
Assets/
├── Scenes/       Main Menu and gameplay scenes
├── Scripts/      Gameplay, UI, and audio scripts
├── Sprites/      Character, obstacle, effect, UI, and planet artwork
├── Sounds/       Music and sound effects
└── Prefabs/      Reusable objects (explosion effect, etc.)
```

## Key systems

- **`JetpackController`** — movement, thrust input, ground detection, and death handling (collision or overheat).
- **`JetpackHeatMeter`** — tracks heat buildup/cooldown, drives the thermometer UI (color transition + shake warning), and triggers an explosion if overheated too long.
- **`ScoreManager`** — score tracking, Game Over UI, high score persistence (`PlayerPrefs`), and the New Planet unlock condition.
- **`GameSpeedManager`** — shared base speed that the background and obstacles scale from, so everything accelerates together as the run progresses.
- **`ObstacleSpawner`** — spawns obstacles at a difficulty curve that ramps up over time.
- **`PlanetData` / `PlanetManager`** — ScriptableObject-based system for swapping a planet's background, mountains, ground, and floor color, with progress saved between runs.
- **`MenuManager`** — main menu navigation (Play / Planets panel).
- **`MusicManager`** — background music playback.

## Built with

- Unity 6
- TextMesh Pro (UI text, pixel font)
- 2D Sprite / Physics2D

## Status

Actively in development. Core loop (jetpack flight, obstacles, scoring, Game Over/Restart, heat meter, audio) is complete. Currently building out the multi-planet system and main menu.
