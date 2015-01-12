===============================================================================
                              N o e s i s G U I
===============================================================================

  Company: Noesis Technologies
  Web: http://www.noesisengine.com
  Contact: info@noesisengine.com

===============================================================================


Version Changelog
-----------------

It can be found in Assets/NoesisGUI/Docs/Gui.Core.Changelog.html.
  

Package Folder Structure
------------------------

NoesisGUI package files are organized in 4 root folder:
    
  NoesisGUI/
    Docs/                  Documentation and tutorials for plugin use
    Samples/               Sample scenes ready to try and useful as start point
    Themes/                Sample theme files to skin your user interfaces
    UserControls/          Reusable UserControls used in samples
    index.html             Index of the documentation
    README.txt             This file
    
  Editor/
    NoesisGUI/             Editor extension scripts for XAML asset processing
      BuildTool/           Libraries used by editor extension scripts
  
  Plugins/
    NoesisGUI/
      Scripts/
        Core/              Runtime plugin scripts
        Proxies/           Wrapper classes to communicate with NoesisGUI
        NoesisGUIPanel.cs  The GUI script component
    Android/               Runtime library for Android
    iOS/                   Runtime static library for iOS
    x86/                   Runtime library for Windows & Linux 32 bits
    x86_64/                Runtime library for Windows 64 bits
    NoesisUnityRenderHook  Library that hooks to Unity native rendering

  StreamingAssets/
    NoesisGUI/             Processed data for each supported platform


Set up instructions
-------------------

After importing NoesisGUI package to your project, we recommend opening the
documentation index, through the main menu Window -> NoesisGUI -> Documentation
or by double clicking in the Assets/NoesisGUI/index.html file.

You will find a list of tutorials about NoesisGUI, please read them all
carefully, with special attention Unity Integration tutorial. It contains step
by step instructions to use NoesisGUI to provide an awesome user interface for
your Unity project.

You will notice that after package was imported, NoesisGUI is ready to be used.
Just select any sample scene under NoesisGUI/Samples and hit the play button.


Online support
--------------

If you have any questions or suffer any inconvenient, please visit our forums
at http://forums.noesisengine.com.
