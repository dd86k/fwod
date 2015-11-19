# fwod
## Four Walls of Death - A silly roguelike

This project has been put on a halt for now.

I'm currently working on something that will greatly help this project.

(Early screenshots)

![fwod on Windows](http://didi,wcantin.ca/pages/fwod/img1.png)

![fwod on PuTTY](http://didi,wcantin.ca/pages/fwod/img3.png)

Hello! This is a little something I'd like to show you, it's simply a console application game using the .NET framework, while being Mono compatible.

It's a "silly and modernized" dungeon crawler, where, yes, would be happening in the year 1986.

Magic? Orcs? Gold coins? Pfft! Except for a few elements like chests, because um, they're classics!

Imagine this -- Doing puzzles, such as hacking terminals, to get extra loot, cool huh?

Wonder what else is in this bunker...

Anyway, fwod would play like [NetHack](https://en.wikipedia.org/wiki/NetHack), a roguelike, turn-based game, dungeon crawling game.

You can always send me an email at devddstuff@gmail.com or open a ticket/issue. (I won't bite!)

### Notes

- Code reorganization/restructures/cleanups are made from time to time
- The game will change dramatically often during development process
- Fixed 80x24 resolution for **maximum** compatiblity (ISO/ANSI screen size)
- Colors are planned
- Mono doesn't like `{string}.ToString()`, using `string.Format("{0}", object)` is prefered.
  - Except for StringBuilder objects. (MAGIC)
- A _Release_ will be made a week after once I reach 5000 tweets.
- Official "GameServer" planned.
  - Already have one set up, I just wish I could host it elsewhere than on my own network and server.
  - Play the game via SSH or telnet.
- Using the `Release` compile config trims ~9.5KB compared to `Debug`
- Console is damaged-based, meaning I only update what I need on screen.
- Bubbles are directly printed on screen.

### Progress

- [x] Multi-layer buffers
- [x] Collision system
- [x] Menu
- [x] Hit system
- [x] Statistics
- [ ] Person.cs
  - [ ] Inventory system
  - [ ] Leveling system
  - [ ] Attack system *
- [ ] Item.cs
  - [ ] Weapons *
    - [ ] Sword
	- [ ] Gun
  - [ ] Food and Drinks *
  - [ ] Armors *
- [ ] Game.cs
  - [ ] Turns
  - [ ] Enemy AI
- [ ] Floor.cs
  - [ ] Multi-Floor system
  - [ ] Puzzles
    - [ ] Sokoban
    - [ ] Terminal
  - [ ] Random chamber generator
  - [ ] Bosses (Optional)
- [ ] One final round
  - [ ] Fixes
    - [ ] Mono
  - [ ] Optimization
    - [ ] Change types (e.g. int to byte for Damage)
	- [ ] Less calls
- [ ] Multiplayer
  - Lobby
- [ ] Gamemodes
  - [ ] Story *
  - [ ] Survive
  - [ ] Duel (Multiplayer)
- [ ] Wiki (Optional)
- [ ] Official Release (1.0.0.0)
  - [ ] Celebrate! (PARTY HARD)

### Builds
Builds will be available on the [Releases](https://github.com/guitarxhero/fwod/releases) tab.

### Contributing
You are free to fork my project in order to provide bug correcting suggestions.

### Installing
You only need Visual Studio 2012+, Visual Studio Code (Mono/Linux-only), MonoDevelop, or Xamarin Studio, and you're good to go!

On MonoDevelop and Xamarin Studio, you will need to change one project setting.

Right click on the fwod Solution; Options; Under Execute; General: Run on external console. Good to go!

On Visual Studio Code, you will need the extra manual setup (like manually compiling the project) in order to get things running.

I'm currently using **Visual Studio 2012**.

### License
This project uses the MIT license, which you can read from the LICENSE file.
Copyright information is included in the LICENSE file.

pls no bully :ok_hand::eyes::ok_hand::eyes::ok_hand::poop: