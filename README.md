# fwod
## Four Walls of Death - A silly roguelike

(Early screenshots)

![fwod on Windows](http://www.wilomgfx.net/didier/pages/fwod/img1.png)

![fwod in PuTTY](http://www.wilomgfx.net/didier/pages/fwod/img3.png)

Hello! This is a little something I'd like to show you, it's simply a console application game using the .NET framework, while being Mono compatible.

fwod would play like NetHack, a roguelike, turn-based game (well it's instant), dungeon crawling game.

Reason I'm doing this is to expand my knowledge about C# even further and learn more profoundly about game development. (In a way. Next might be with Unity)

It's a personal project, so no rushing here.

You can always send me an [email](mailto:devddstuff@gmail.com) or open a ticket/issue. (I won't bite!)

### Notes
- Code reorganization/restructures/cleanups are made from time to time
- The game will change dramatically often during development process
- Fixed 80x24 resolution (buffer-limited) for increased compatiblity
  - Windows 10 buffer screen size is bigger by default, and I can't shrink it
    - Also NetHack works the same way
  - Some or most terminals under GNU/Linux "BufferHeight"'s stops at 24
  - PuTTY's window stops at height 24 (Window-wise, not buffer)
- Colors are planned

### Progress
Very likely in order
- Proposal: Basic idea, in code
- Implementing: Implementation of the idea
- Functional: Works, to say the least (most of it)
- Testing: Testing the implementation

This list will change often and probably miss something.

- [x] Multi-layer buffers
- [ ] Collision system (NOW: Fixing bugs)
  - [x] Proposal
  - [x] Implementing
  - [x] Functional
  - [x] Testing
- [ ] Menu
  - [ ] Proposal
  - [ ] Implementing
  - [ ] Functional
  - [ ] Testing
- [ ] Random chamber generator (Milestone)
  - [ ] Proposal
  - [ ] Implementing
  - [ ] Functional
  - [ ] Testing
- [ ] Floor system
  - [ ] Proposal
  - [ ] Implementing
  - [ ] Functional
  - [ ] Testing
- [ ] Enemy system
  - [ ] Proposal
  - [ ] Implementing
  - [ ] Functional
  - [ ] Testing
- [ ] Attacking system
  - [ ] Proposal
  - [ ] Implementing
  - [ ] Functional
  - [ ] Testing
- [ ] Items
  - [ ] Proposal
  - [ ] Implementing
  - [ ] Functional
  - [ ] Testing
- [ ] Inventory system (Milestone)
  - [ ] Proposal
  - [ ] Implementing
  - [ ] Functional
  - [ ] Testing
- [ ] Leveling system
  - [ ] Proposal
  - [ ] Implementing
  - [ ] Functional
  - [ ] Testing
- [ ] Bosses (Optional)
- [ ] Final bug fixing round
- [ ] Optimization round
- [ ] Wiki (Optional)
- [ ] Official Release (Milestone)
- [ ] Celebrate!

### Builds
Some builds will be available on my [website](http://www.wilomgfx.net/didier/pages/fwod.html) (When I reach 5000 tweets).
A "GameServer" (cool name huh?) will be available to play the game via SSH (no Mono/.NET required!).

It will be accessible via this command:
``ssh -p 1337 -l anon {IP/address to be announced}``

### General Issues
- Code is _very_ unoptimized
  - May redo the entire structure of it

### License
This project uses the MIT license, which you can read from the LICENSE file.

pls no bully :ok_hand::eyes::ok_hand::eyes::ok_hand::poop:
