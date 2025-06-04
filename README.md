# Database Snoop for AutoCAD Civil 3D 2025 + later

This tool uses .NET Reflection to list all properties of AutoCAD Civil 3D major objects, such as Alignments, Corridors, Surfaces, Networks, etc. It also lists collection items and allows selection of objects on-screen.

# Setup

```bash
git clone https://github.com/ADN-DevTech/Civil3DSnoop.git
cd Civil3DSnoop\Civil3DSnoopDB.bundle\Contents\Source\SnoopCivil3D
devenv SnoopCivil3D.sln
```



Open the SnoopCivil3D.sln using **Visual Studio 2022**. All references should be ready for AutoCAD Civil 3D 2025/2026 default install path; otherwise, go to **Project Properties** → **References**, then click on **Reference Paths** and adjust as needed.  
Build the project in **Release** mode; the DLLs will be placed under the `/Contents/NET48` and `/Contents/NET80` folders according to target frameworks.  
Copy the entire `.bundle` folder to  
**`%APPDATA%\Autodesk\ApplicationPlugins`**  
and launch Civil 3D.

# Usage

In AutoCAD Civil 3D, right-click to see the **"Snoop"** option; the main form will appear. On the left side is a list of the main collections on the active document. On the right side, the properties of the selected item are displayed.

Click the **"Select another file"** button to explore another drawing file. This will open the file in memory but not within Civil 3D.

# Author

- Originally written by Augusto Goncalves [@augustomaia](http://www.twitter.com/augustomaia), member of the Autodesk Developer Technical Services team.

- Migrated to support .NET 8.0 and modern Civil 3D versions by Madhukar Moogala, member of the APS Team.

### Limitations

The tool uses Reflection to list properties, so some properties might not be accessible or fully displayed. Most common properties should work fine.

### Known Issues

Some properties that cannot be reflected using .NET may cause the tool to stop working or skip those entries.

### Release History

- 1.0 Original release

- 1.1 New features, 2014 support

- 1.2 Bundle format update

- 1.3 Support for additional drawing types

- 1.4 Support for Civil 3D 2017

- 1.5 Support for Civil 3D 2018

- 1.6 Support for Civil 3D 2020

- 1.7 Updated for Civil 3D 2025 / 2026 with Visual Studio 2022 and .NET 8.01.5 2018 support

- 1.6 2020 support
- 1.7 2025-2026 support
