# InfinityNikkiRichPresence


Tired of __Infinity Nikki__ having lack of Discord Rich Presence in your Discord Bio?[^1]

Infold has a __Infinity Nikki__ Official Discord Server, but no actual Rich Presence for their game! How lame!

Not to worry, today, I'm here to fix that for you!

I've written a simple Rich Presence detection for the Game shown below!

![image](https://github.com/user-attachments/assets/5f9f59cb-7524-4912-9811-097ab458ce14)

*Screenshot taken during Release v1.0; Image subject to change on other Releases*


**Feel free to fork and compile on your own, or, head to the [Releases Page](https://github.com/tgrafk12/InfinityNikkiRichPresence/releases/tag/1.0) to download the pre-compiled EXE for yourself!**

Enjoy!


#### Planned Updates:
- Friend Code/Stylist ID (On Rich Presence as Button for easy add)

- Game Download Button (On Rich Presence via Epic Games/Website)

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


[^1]: I have no relation with [Infold Games](https://infoldgames.com/en/home), nor do not own the Icon/Images displayed in the Rich Presence. Images are Directly from their Game/Website. All Images are Property of [Infold Games](https://infoldgames.com/en/home) and are strictly used for the Implementation of DiscordRPC.

[^2]: This most likely will need to be a Modification to the game directly (Which If I'm not wrong, is against Infold's TOS). Unless I can figure out how to Stream Player data to the executable without modifying the game or injecting any .DLL files this is a _BIG_ _**maybe implementation**_. If you know how to make this work without breaking TOS, feel free to DM me!

[^3]: May be removed if they actually add a DiscordRPC into the game Natively...
