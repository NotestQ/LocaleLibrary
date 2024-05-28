# LocaleLibrary
A library to add and use localizations in Content Warning in almost exactly the same way the base game does.   

## Mod Usage
Download the [latest dll](https://github.com/NotestQ/LocaleLibrary/releases/latest) from the releases tab or clone and build the repo, then add it as a reference to the project. After adding it as a reference you can add it as a dependency:  
```cs
[BepInDependency(LocaleLibrary.MyPluginInfo.PLUGIN_GUID)] // Make sure to specify if it's a soft or a hard dependency! BepInEx sets dependencies to hard by default.
public class YourMod : BaseUnityPlugin { // ...
```  

### Integration
For in-depth documentation, check out the [documentation](https://github.com/NotestQ/LocaleLibrary/wiki/Library-Documentation)! Examples are included in the documentation, demos are not available at this point in time.

## Additions
By itself, this does nothing! This is a library for mod developers to add translations to their mods or add translations for the base game.

## Issues
If the mod is throwing an error use [the github issues page](https://github.com/NotestQ/LocaleLibrary/issues) and copy-paste the error in there, with a description of what is happening and what you expected to happen if applicable. Or just ping me at the Content Warning Modding Discord server!
