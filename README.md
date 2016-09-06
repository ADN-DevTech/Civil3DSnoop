# Database Snoop for AutoCAD Civil 3D 2017

This tool uses .NET Reflection to list all properties of AutoCAD Civil 3D major objects, such as Alignments, Corridors, Surfaces, Networks, etc. It also lists collections items and allows selection of objects on-screen.

# Setup

Open the [SnoopCivil3D.sln](Civil3DSnoopDB.bundle\Contents\Source\SnoopCivil3D\SnoopCivil3D.sln) on Visual Studio 2015. All references should be ready for AutoCAD Civil 3D 2017 default install path, otherwise go to project properties >> References, then click on Reference Paths and adjust. Build the project in Release, the DLL should be placed at /Contents/R21 folder. Copy the entire .bundle folder to <b>c:\Program Files\Autodesk\Autodesk\ApplicationPlugins</b> folder and launch Civil 3D.

# Usage

On AutoCAD Civil 3D, mouse right-click should show the "Snoop" option, the main form should appear. At the left side is a list of the main collections on the active document. On the right side, the properties of the item selected on the left.

Click on "Select another file" button to explore another file. This will not open on Civil3D, but will be "in memory".

# Author

This plugin was written by Augusto Goncalves [@augustomaia](http://www.twitter.com/augustomaia), member of the Autodesk Developer Technical Services team. 

### Limitations

The tool uses Reflection to list the properties, so it may not work well for all properties. Most properties should work fine.

### Known Issues

The tool may stop working on some properties that cannot be reflected (using .NET).

### Release History

1.0	Original release
1.1	New features, 2014 support
1.2	Bundle format
1.3 Support for additional drawing
1.4 2017 support