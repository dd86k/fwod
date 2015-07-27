# fwod
## Four Walls of Death - A silly roguelike

(Early screenshots)

![fwod on Windows](http://www.wilomgfx.net/didier/pages/fwod/img1.png)

![fwod on PuTTY](http://www.wilomgfx.net/didier/pages/fwod/img3.png)

Hello! This is a little something I'd like to show you, it's simply a console application game using the .NET framework, while being Mono compatible.

fwod would play like [NetHack](https://en.wikipedia.org/wiki/NetHack), a roguelike, turn-based game (well it's instantaneous), dungeon crawling game.

You can always send me an email at devddstuff@gmail.com or open a ticket/issue. (I won't bite!)

### Notes
- Code reorganization/restructures/cleanups are made from time to time
- The game will change dramatically often during development process
- Fixed 80x24 resolution for **maximum** compatiblity (ISO/ANSI screen size)
- Colors are planned
- Mono doesn't like `{string}.ToString()`, using `string.Format("{0}", object)` is prefered.
- A _Release_ will be made when I reach 5000 tweets.
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
- [ ] Items (Items.cs)
- [ ] Attack system (Attack.cs)
- [ ] Floor system (Floor.cs)
- [ ] Random chamber generator (Floor.cs)
- [ ] Enemy AI (Game.cs)
- [ ] Inventory system (Person.cs)
- [ ] Level system (Person.cs)
- [ ] Bosses (Optional) (Bosses.cs)
- [ ] Final bug fixing and optimization round
- [ ] Wiki (Optional)
- [ ] Official Release (1.0.0.0)
  - [ ] Celebrate!

### Builds
Builds will be available on the [Releases](https://github.com/guitarxhero/fwod/releases) tab.

### Contributing
You are free to fork my project in order to provide bug correcting suggestions.

### Installing
You only need Visual Studio 2012+, MonoDevelop, or Xamarin Studio, and you're good to go!

### License
This project uses the MIT license, which you can read from the LICENSE file.
Copyright information is included in the LICENSE file.

pls no bully :ok_hand::eyes::ok_hand::eyes::ok_hand::poop: