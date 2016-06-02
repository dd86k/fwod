# fwod
## Four Walls of Death - A silly roguelike

This project has been put on a halt for now.

I'm currently working on something that will greatly help this project.

(Early screenshots)

![fwod on Windows](http://didi.wcantin.ca/pages/fwod/img1.png)

![fwod on PuTTY](http://didi.wcantin.ca/pages/fwod/img3.png)

Hello! This is a little something I'd like to show you, it's simply a console application game using the .NET framework, while being Mono compatible.

It's a "silly and modernized" dungeon crawler, where, yes, would be happening in the year 1986.

Magic? Orcs? Gold coins? Pfft! Except for a few elements like chests, because um, they're classics!

Imagine this -- Doing puzzles, such as hacking terminals, to get extra loot, cool huh?

Wonder what else is in this bunker...

Anyway, fwod would play like [NetHack](https://en.wikipedia.org/wiki/NetHack), a roguelike, turn-based game, dungeon crawling game.

### Notes

- Code reorganization/restructures/cleanups are made from time to time
- The game will change dramatically often during development process
- Fixed 80x24 resolution for **maximum** compatiblity (ISO/ANSI screen size)
- Colors are planned

### Roadmap

- [x] Multi-layer buffers
- [x] Collision system
- [x] Menu
- [x] Hit system
- [x] Statistics
- [ ] Person.cs
  - [ ] Inventory system
  - [ ] Leveling system
  - [x] Attack system
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
- [ ] Wiki (Optional)
- [ ] Official Release (1.0.0.0)
  - [ ] Celebrate! (PARTY HARD)

### License
This project uses the MIT license, which you can read from the LICENSE file.
Copyright information is included in the [LICENSE](LICENSE) file.