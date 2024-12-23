# InfinityNikkiRichPresence


Tired of __Infinity Nikki__ having lack of Discord Rich Presence in your Discord Bio?[^1]

Infold has a __Infinity Nikki__ Official Discord Server, but no actual Rich Presence for their game! How lame!

Not to worry, today, I'm here to fix that for you!

I've written a simple Rich Presence detection for the Game, as shown below!

![image](https://github.com/user-attachments/assets/5f9f59cb-7524-4912-9811-097ab458ce14)

*Screenshot taken during Release v1.0; Image subject to change on other Releases*

### Features: 
- Discord Rich Presence for the game [Infinity Nikki](https://infinitynikki.infoldgames.com/en/home)
- Includes a config JSON file upon launching the exe (In the same directory as where it was ran) holding:
   - Playtime Tracking, updated Via Rich Presence in Discord and updated in Real time! (Since its Unavailable as of release)
      - *Can also be manually edited to properly show new "Rich Presence" users their long playtime in-game*
   - Logging Config: Showing you what exactly is being updated
      - Showing 3 Levels of logging details
- Game Download Button for older Discord Clients (via Epic Games/Website)

#### Planned Updates:
- Friend Code/Stylist ID (On Rich Presence as Button for easy add)

- Service Manager Implementation (Automatically Start Service/Rich Presence when game is running via Windows Service Manager)

- Language Localisation (As its Currently in English I want to plan for: (JP, CN, FR, RU) and maybe a few more)

- Massive Revamp of Data Shown on Rich Presence[^2]
   - Point of Interest Data (Shows In-Game Location)
   - Mission Currently Tracked
   - Mira Level
   - Current Player Whim Star Count

- UI for easier User Experience[^3]:
    - Configuration Options for what the user displays on their Activity Feed
    - Toggle for Service to be used or EXE itself

**Feel free to fork and compile on your own, or, head to the [Releases Page](https://github.com/tgrafk12/InfinityNikkiRichPresence/releases/) to download the pre-compiled EXE for yourself!**

Enjoy!

[^1]: I have no relation with [Infold Games](https://infoldgames.com/en/home) or [Discord](https://discord.com/), nor do not own the Icon/Images displayed in the Rich Presence. Images displayed are Directly from their Game/Website. Images of "Infinity Nikki" are Property of [Infold Games](https://infoldgames.com/en/home). Images of "Discord" are Property of [Discord](https://discord.com/). All image assets used are strictly used for the Implimentation of a Discord Rich Presence for Infinity Nikki and not intened for redistribution.

[^2]: This most likely will need to be a Modification to the game directly (Which If I'm not wrong, is against Infold's TOS). Unless I can figure out how to Stream Player data to the executable without modifying the game or injecting any .DLL files this is a _BIG_ _**maybe implementation**_. If you know how to make this work without breaking TOS, feel free to DM me!

[^3]: May be removed if they actually add a DiscordRPC into the game Natively...
