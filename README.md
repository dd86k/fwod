# fwod
## Four Walls of Death - A silly roguelike

(Early screenshots)

![fwod on Windows](http://www.wilomgfx.net/didier/pages/fwod/img1.png)

![fwod on PuTTY](http://www.wilomgfx.net/didier/pages/fwod/img3.png)

Hello! This is a little something I'd like to show you, it's simply a console application game using the .NET framework, while being Mono compatible.

fwod would play like [NetHack](https://en.wikipedia.org/wiki/NetHack), a roguelike, turn-based game (well it's instantaneous), dungeon crawling game.

Reason I'm doing this is to expand my knowledge about C# even further and learn more about game development. In a way. Next might be with the UnityAPI, who knows?

It's an open personal project, so no rushing here.

You can always send me an email at devddstuff@gmail.com or open a ticket/issue. (I won't bite!)

### Notes
- Code reorganization/restructures/cleanups are made from time to time
- The game will change dramatically often during development process
- Fixed 80x24 resolution for **maximum** compatiblity (ISO/ANSI screen size)
- Colors are planned
- Mono doesn't like `{string}.ToString()`, using `string.Format("{0}", object)` is prefered.
- A _Release_ will be made when I reach 5000 tweets.
- Official "GameServer" planned.
  - It's already set up, I just wish I could host it elsewhere than on my own network and server.
  - Play the game via SSH.
    - Telnet is considered.
- Using the `Release` compile config trims ~9.5KB compared to `Debug`
- Console is damaged-based, meaning I only update what I need on screen.

### Progress

- [x] Multi-layer buffers
- [x] Collision system
- [x] Menu
- [ ] Attack system
- [ ] Items
- [ ] Inventory system
- [ ] Enemy AI
- [ ] Floor system
- [ ] Random chamber generator
- [ ] Leveling system
- [ ] Bosses (Optional)
- [ ] Final bug fixing and optimization round
- [ ] Wiki (Optional)
- [ ] Official Release (Release)
  - [ ] Celebrate!

### Builds
Builds will be available on my [website](http://www.wilomgfx.net/didier/pages/fwod.html).

### Contributing
You are free to fork my project in order to provide bug correcting suggestions.
I try to be open ears most of the time.

### Installing
You only need Visual Studio 2012+, MonoDevelop, or Xamarin Studio, and you're good to go!

### License
This project uses the MIT license, which you can read from the LICENSE file.
Copyright information is included in the LICENSE file.

pls no bully :ok_hand::eyes::ok_hand::eyes::ok_hand::poop: