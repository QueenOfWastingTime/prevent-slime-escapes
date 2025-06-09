# Prevent Slime Escapes
Stardew Valley mod to prevent slimes outside the hutch from escaping over night.
## Description
In the base game, if you are keeping slimes outside on your farm (not in the hutch or farm house), they have a chance to "escape" every night, meaning that they are deleted and you get a messege in the morning that they escaped.  
This mod prevents that, enabling you to keep slimes on your farm without worrying about any of them disappearing.  
Note that with slimes breeding, but (with this mod) never disappearing, the number of slimes on your farm might grow expotentially. (I'm not sure about the breeding mechanics of slimes, maybe there is a limit, maybe not.)  
We just wrote this mod so that I could keep singular display slimes in their own little enclosures.

Please also note that this mod has nothing to do with the fact that slimes get teleported to the place they hatched when you load a file. So if your problem is that your slimes keep escaping their enclosures, as in they are still on your farm but teleported out of their pen, this mod will NOT fix that.
### Code Explaination
This is a SMAPI mod that uses Harmony to change the original game code. It is also just about THE simplest Harmony mod imaginable.  
The (approximate, decompiled) base game code is:  
```
if (characters.Count > 5)
{
    int slimesEscaped = 0;
    for (int k = characters.Count - 1; k >= 0; k--)
    {
        if (characters[k] is GreenSlime && Game1.random.NextDouble() < 0.035)
        {
            characters.RemoveAt(k);
            slimesEscaped++;
        }
    }
    if (slimesEscaped > 0)
    {
        Game1.multiplayer.broadcastGlobalMessage((slimesEscaped == 1) ? "Strings\\Locations:Farm_1SlimeEscaped" : "Strings\\Locations:Farm_NSlimesEscaped", false, null, slimesEscaped.ToString() ?? "");
    }
}
```
("GreenSlime" refers to all slimes in this code.)  
And all that our mod does is change the "5" in `if (characters.Count > 5)` to the maximum integer, causing the if condition to never be satisfied and the code to always be skipped.  

You can look at our mod code in this repo and a compiled version in the releases.

## Installation

This is a SMAPI mod so:

 - Install [SMAPI](https://stardewvalleywiki.com/Modding:Player_Guide/Getting_Started#Install_SMAPIhttps://stardewvalleywiki.com/Modding:Player_Guide/Getting_Started#Install_SMAPI)
 - Unpack the zip from the newest release to the mods folder created when installing SMAPI