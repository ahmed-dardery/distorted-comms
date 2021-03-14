# Distorted Comms
A simple-to-use mod for Among Us that makes Comms Sabotage more powerful!

![Demo](https://github.com/ahmed-dardery/distorted-comms/raw/main/demo/main.gif)

## How it works
In game, when an impostor calls the Comms Sabotage, all players's unique distinctive features are removed and instead they all become similar to each other! This makes this (usually useless) sabotage much more powerful!

## Installation
This is a client-side mod, which means *all* players should have this mod installed for the best experience. As such, you can play on official servers, although I recommend to use your own server using **[Imposter](https://github.com/Impostor/Impostor)**.

Currently, I'm only offerring the `dll` file for a manual installation, I might create a all-in-one package for this later.

**NOTE: this mod only supports Among Us v2021.3.5s on steam!**

### Custom Installation
1. Install Reactor BepInEx by following **[these instructions](https://docs.reactor.gg/docs/basic/install_bepinex/)**.
2. Install Reactor by following **[these instructions](https://docs.reactor.gg/docs/basic/install_reactor)**.
3. Download the **dll file** for your game version in the **Releases**.
4. Copy the dll file into **`Among Us/BepInEx/plugins`**.
5. (Optional) If you want to play on official servers, you must do the following :
    - Open **`Among Us/BepInEx/config/gg.reactor.api.cfg`** with a text editor.
    - Find the line `Modded handshake = true` and change it to `Modded handshake = false`.
    - Save and close your editor.

## Configuration
After running the game with mod installed at least once, a configuration file should be present at `\BepInEx\config\mod.dardy.distortedcomms.cfg`. This allows changing the following settings under header `[Distortion]`:

| Option | Description | Default |
| :-----: | :---------: | :-----------: |
| Duration | The duration (in seconds) in which the fading out / fading in occurs. | 3 |
| Back | The back color (RGBA in hex) of the distorted player when comms are active | 7F7F7FFF |
| Body | The body color (RGBA in hex) of the distorted player when comms are active | 7F7F7FFF |

Of course, you can leave them as-is.

**NOTE: the config parser doesn't implement any error detection, so be careful**

## Bug Reports
Due to circumstances, I haven't had the chance to thoroughly test this mod. If you encounter any issues you are more than welcome to post it in the Issues section!

## Special Thanks

- **Reactor**: Modding API that makes modding so much easier.
- **BepInEx**: The game patcher used to enable modding for the game.
- **Woodi-dev**, **NotHunter101**: inspiration to start modding Among Us, code-snippets to help understand the ins and outs.
- **Brybry16**: install instructions lifted directly from his mod.
