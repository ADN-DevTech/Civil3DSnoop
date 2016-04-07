================================================
          Snoop Civil 3D Database
Brought to you by the Autodesk Developer Network
================================================
------------------------
Snoop Civil 3D Database
------------------------

Description
-----------
This tool allows developers inspect the Civil3D Database without
writing code. It works by showing the root objects of
Civil3D 2012-2016 (i.e. Styles, Alignments, Corridors, Assemblies,
Subassemblies, Pipe networks and Surfaces) listing them on
the left with all objects on each collections, such as all
alignments on the active drawing, and once click on each item,
list all its properties on right.

System Requirements
-------------------
This plugin has been tested with AutoCAD Civil 3D from 2012 
2016 and requires the .NET Framework 4.0.

A pre-built version of the plugin has been provided which should
work on 32- and 64-bit Windows systems.

The source code has been provided as a Visual Studio 2012 project
containing VB.NET code (not required to run the plugin).

Installation
------------
Unzip and copy the Civil3DSnoopDB.bundle folder into the Roamming
folder. This is required to run the plug-in on all versions.

Windows Vista, 7, 8
C:\Users\<your login>\AppData\Roaming\Autodesk\ApplicationPlugins

Windows XP
C:\Documents and Settings\<your login>\Application Data\Autodesk\ApplicationPlugins

AutoCAD Civil 3D 2016
C:\Program Files\Autodesk\ApplicationPlugins
* Only the folder under Program Files will not show the security warning 

If you are using Vista or Windows 7, first check if the zip file
needs to be unblocked. Right-click on the zip file and select
"Properties". If you see an "Unblock" button, then click it. 

If you do not "Unblock" the zip or the .dll for your release,
AutoCAD will not load the plugin, and will not tell you about it.

Usage
-----
Inside Civil 3D, right click, select Snoop Civil3D. The form will
show the database. Select the object and inspect its properties.

Uninstallation
--------------
Just delete the Civil3DSnoopDB.bundle folder.

Limitations
-----------
The tool uses Reflection to list the properties, so it may not work
well for all properties. Most properties should work fine.

Known Issues
------------
The tool may stop working on some properties that cannot be 
reflected (using .NET).

Author
------
This plugin was written by Augusto Goncalves of the Autodesk
Developer Technical Services team. 

Further Reading
---------------
For more information on developing with AutoCAD Civil 3D, please
visit the AutoCAD Civil 3D Developer Center at
http://www.autodesk.com/developcivil

Feedback
--------
Email us at augusto.goncalves@autodesk.com with feedback or 
requests for enhancements.

Release History
---------------

1.0	Original release
1.1	New features, 2014 support
1.2	Bundle format
1.3 Support for additional drawing

(C) Copyright 2015 by Autodesk, Inc. 

Permission to use, copy, modify, and distribute this software in
object code form for any purpose and without fee is hereby granted, 
provided that the above copyright notice appears in all copies and 
that both that copyright notice and the limited warranty and
restricted rights notice below appear in all supporting 
documentation.

AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC. 
DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
UNINTERRUPTED OR ERROR FREE.
