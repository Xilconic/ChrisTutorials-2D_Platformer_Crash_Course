# ChrisTutorials-2D_Platformer_Crash_Course
Following the Chris' Tutorials 2D Platformer crashcourse (https://www.youtube.com/playlist?list=PLyH-qXFkNSxmDU8ddeslEAtnXIDRLPd_V) to learn how to create a 2D platforming game in Unity.

## Assets
### Hero character assets
[rvros Animated Pixel Adventurer](https://rvros.itch.io/animated-pixel-hero)

Used:
- Adventurer-1.5.zip
- Adventurer-Hand-Combat.zip
- Adventurer-Bow.zip

### Enemies
#### Enemy knight assets
[Namatnieks Fantasy Knight - Free Pixelart Animated Character](https://aamatniekss.itch.io/fantasy-knight-free-pixelart-animated-character)

#### Other Enemies
[LuizMelo Monsters Creatures Fantasy](https://luizmelo.itch.io/monsters-creatures-fantasy)

Used:
- Monsters_Creatures_Fantasy.zip
- Monster_Creatures_Fantasy(Version 1.2).zip
- Monster_creatures_Fantasy(version 1.3).zip

### Font
[daniel linssen m5x7 font](https://managore.itch.io/m5x7)

### Tileset
[Namatnieks Free Pixelart Tileset - Cute Forest](https://aamatniekss.itch.io/free-pixelart-tileset-cute-forest)

### Pickups
[Henry Software Free Pixel Food](https://henrysoftware.itch.io/pixel-food)

Used:
- FreePixelFood.zip

### Arrow projectile
[Chris' Tutorials ice arrow](https://freeimage.host/i/straight-arrow-modified.bTMg6J)
Original source: [Kyrise's 16x16 RPG icon pack](https://kyrise.itch.io/kyrises-free-16x16-rpg-icon-pack)

### Background music
[YouFulca Legendary JRPG Battle Music Pack FREE](https://youfulca.itch.io/legendary-jrpg-battle-music-pack)

### Sound Effects
[Leohpaz Free RPG esentials SFX Bundle](https://leohpaz.itch.io/rpg-essentials-sfx-free)

## Packages
### Cinemachine
Uses version 2.9.7 of the `Cinemachine package` by Unity Technologies Inc.
Documentation: https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/index.html

### Input System
Uses version 1.7.0 of the `Input System` package by Unity Technologies Inc.
Documentation: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/index.html

### TextMesh Pro
Uses version 3.0.6 of the `TextMesh Pro` package by Unity Technologies Inc.
Documentation: https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html

## Building and Running locally

### Requirements
- [Unity](https://unity.com/releases/editor/archive) v2022.3.21f1
- [Visual Studio](https://visualstudio.microsoft.com/vs/community/) v17.9.2
	- With "Game development with Unity" extension
- [Python](https://www.python.org/downloads/) v3.12.2
- [Nuke](https://nuke.build/docs/introduction/) v8.0.0 (Installed from commandline: `dotnet tool install Nuke.GlobalTool --global`)

#### Configuring Github release capabilities
In the `.nuke/parameters.secrets.json` the `GitHubPersonalAccessToken` parameter is defined.
https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens#creating-a-fine-grained-personal-access-token describes how to define one for the Build Script.
The token requires the following grants:
- Read+Write to contents (needed for creating Releases and uploading files to a release)

To generate the `.nuke/parameters.secrets.json` file:
1. Open commandline in repository root.
2. Run `nuke :secrets secrets`
3. Provide a password (Save this to something like a Password Safe, as you'll require to provide it for build targets that depend on secrets)
4. Navigate the menu to `GitHubPersonalAccessToken` and provide a value.
5. Navigate the menu to `<Save and Exit>`.

### Building a Github Release (build script)
1. Open commandline in repository root.
2. Run `nuke --target CreateReleaseOnGithub --profile secrets --Version x.y.z`
	- The `Version` parameter can optionally be post-fixed with -alpha or -beta
	- Not specifying the `Version` parameter causes the script to ask you interactively in the commandline
3. Provide the password for the secrets file

### Building a Windows Release (build script)

#### 32bit
1. Open commandline in repository root.
2. Run `nuke --target BuildWin32BitReleaseZipFile --Version x.y.z`
	- The `Version` parameter can optionally be post-fixed with -alpha or -beta
	- Not specifying the `Version` parameter causes the script to ask you interactively in the commandline
3. The zip-file can be found in `<repo root>\Builds\winx86`
#### 64bit 
1. Open commandline in repository root.
2. Run `nuke --target BuildWin64BitReleaseZipFile --Version x.y.z`
	- The `Version` parameter can optionally be post-fixed with -alpha or -beta
	- Not specifying the `Version` parameter causes the script to ask you interactively in the commandline
3. The zip-file can be found in `<repo root>\Builds\winx64`

### Creating Release Commit
1. Open commandline in repository root.
2. Run `nuke --target CreateReleaseCommitAndPush --Version x.y.z`
	- The `Version` parameter can optionally be post-fixed with -alpha or -beta
	- Not specifying the `Version` parameter causes the script to ask you interactively in the commandline

### Windows (build script)

#### 32bit
1. Open commandline in repository root.
2. Run `nuke --target BuildAppAsWin32bit`.
3. Build can be found in `<repo root>\Builds\winx86`.

#### 64bit
1. Open commandline in repository root.
2. Run `nuke --target BuildAppAsWin64bit`.
3. Build can be found in `<repo root>\Builds\winx64`.

### Windows (manual)
1. From repository root, open `ChrisTutorialsPlatformerCrashCourse` folder.
2. File > Build Settings
3. Ensure Platform `Windows, Mac, Linux` is active.
	- If not, click the `Switch Platform` button in the lower right corner and wait for the switch to finish.
4. Click the 'Build and Run' button.
5. When asked, determine the location to build
	- Recommendation 32 bit: `<repo root>\Builds\winx86` for an `Windows` target platform and `Intel 32-bit` architecture
	- Recommendation 64 bit: `<repo root>\Builds\winx64` for an `Windows` target platform and `Intel 64-bit` architecture
6. Wait for the build to complete. The game will start automatically after.

### HTML 5 (manual)
1. From repository root, open `ChrisTutorialsPlatformerCrashCourse` folder.
2. File > Build Settings
3. Ensure Platform `WebGL` is active.
	- If not, click the `Switch Platform` button in the lower right corner and wait for the switch to finish.
4. Click the 'Build' button.
5. When asked, determine the location to build
	- Recommendation: `<repo root>\Builds\html5`
6. Wait for the build to complete.
7. Navigate into the build folder.
8. Open a command line in that folder.
9. Run the command: `python -m http.server`
10. Open a browser. Naviate to `localhost:8000` (assuming 8000 portnumber; Specific port number is mentioned in the command line window). Game is now started.