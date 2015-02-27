''//
''// (C) Copyright 2006-2015 by Autodesk, Inc.
''//
''//
''//
''// By using this code, you are agreeing to the terms
''// and conditions of the License Agreement that appeared
''// and was accepted upon download or installation
''// (or in connection with the download or installation)
''// of the Autodesk software in which this code is included.
''// All permissions on use of this code are as set forth
''// in such License Agreement provided that the above copyright
''// notice appears in all authorized copies and that both that
''// copyright notice and the limited warranty and
''// restricted rights notice below appear in all supporting 
''// documentation.
''//
''// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
''// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
''// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
''// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
''// UNINTERRUPTED OR ERROR FREE.
''//
''// Use, duplication, or disclosure by the U.S. Government is subject to 
''// restrictions set forth in FAR 52.227-19 (Commercial Computer
''// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
''// (Rights in Technical Data and Computer Software), as applicable.
''//
''//
''// This Sample is written by "Autodesk Developer Network" (ADN) Team
''//

Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.Windows

<Assembly: ExtensionApplication(GetType(SnoopApplication))> 

Public Class SnoopCommands
  <CommandMethod("snoopCivil3DDB")> _
  Public Shared Sub CommandSnoopCivil3DDatabase()
    Dim frm As New frmSnoopObjects(Application.DocumentManager.MdiActiveDocument.Database)
    Application.ShowModalDialog(frm)
  End Sub
End Class

#Region "IExtensionApplication implementation"

Public Class SnoopApplication
  Implements IExtensionApplication

  Public Sub Initialize() Implements Autodesk.AutoCAD.Runtime.IExtensionApplication.Initialize
    AddMenuItem()
  End Sub

  Public Sub Terminate() Implements Autodesk.AutoCAD.Runtime.IExtensionApplication.Terminate

  End Sub

  Private Sub AddMenuItem()
    Dim mnuSnoopCivil3DMenu As New ContextMenuExtension
    mnuSnoopCivil3DMenu.Title = "Snoop Civil3D"

    Dim mnuSnoopDB As New MenuItem("Snoop Database")
    AddHandler mnuSnoopDB.Click, AddressOf mnuSnoopDB_Click
    mnuSnoopCivil3DMenu.MenuItems.Add(mnuSnoopDB)

    Application.AddDefaultContextMenuExtension(mnuSnoopCivil3DMenu)
  End Sub

  Private Sub mnuSnoopDB_Click(ByVal sender As Object, ByVal e As EventArgs)
    Application.DocumentManager.MdiActiveDocument.SendStringToExecute("snoopCivil3DDB ", True, False, True)
  End Sub

End Class

#End Region
