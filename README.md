# MandoJoyRide

**[🕹️ Play MandoJoyRide on itch.io!](https://proszcorv.itch.io/mando-joyride)**

A 2D pixel-art endless runner built in Unity, starring a Mandalorian-style bounty hunter who uses a jetpack to dodge obstacles across the galaxy. Playable directly in desktop and mobile browsers!

## Gameplay

* **Jetpack flight**: Hold the thrust button to fly, release to fall. Thrusting builds up heat on a thermometer-style meter — let it cool down, or push your luck and risk exploding if you hold it too long at max heat.
* **Obstacles**: Rocks/lava pits on the ground and projectiles in the air, each with their own relative speed, spawning at an increasing rate as the run goes on.
* **Score**: Increases automatically over time. Reach a high score (1000 points) to unlock the option to travel to a new planet.
* **Planets**: Start on Tatooine; unlock new planets (like Mustafar) featuring their own unique backgrounds, terrain, color palettes, custom obstacles, and audio cues.

## Controls

* **Spacebar / Left Mouse Button / Touch Screen**: Activate jetpack thrust.

## Project structure

```text
Assets/
├── Scenes/       Main Menu and gameplay scenes
├── Scripts/      Gameplay, UI, and audio scripts
├── Sprites/      Character, obstacle, effect, UI, and planet artwork
├── Sounds/       Music and sound effects
└── Prefabs/      Reusable objects (explosion effect, etc.)

```

## Key systems

* **`JetpackController`** — Movement, thrust input (keyboard/mouse/touch), ground detection, and death handling (collision or overheat).
* **`JetpackHeatMeter`** — Tracks heat buildup/cooldown, drives the thermometer UI (color transition + shake warning), and triggers an explosion if overheated too long.
* **`ScoreManager`** — Score tracking, Game Over UI, high score persistence (`PlayerPrefs`), and the New Planet unlock conditions.
* **`GameSpeedManager`** — Shared base speed that the background and obstacles scale from, so everything accelerates together as the run progresses.
* **`ObstacleSpawner`** — Spawns obstacles at a difficulty curve that ramps up over time, handling custom prefabs per planet.
* **`PlanetData` / `PlanetManager**` — ScriptableObject-based system for swapping a planet's visual assets, obstacles, and sound effects, with unlock progress saved between runs.
* **`MenuManager`** — Main menu navigation, dynamic planet selection panels, and progression UI.
* **`MusicManager`** — Background music playback and environment-specific sounds.

## Built with

* Unity 6
* TextMesh Pro (UI text, pixel font)
* 2D Sprite / Physics2D

## Status

Fully playable and published for WebGL (Desktop & Mobile). The core gameplay loop, multi-planet progression system, UI, and mobile touch controls are complete.
