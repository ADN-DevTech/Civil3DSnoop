''//
''// (C) Copyright 2006-2013 by Autodesk, Inc.
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

Imports System.Windows.Forms
Imports System.Reflection

Imports Autodesk.AutoCAD.EditorInput
Imports AcApplication = Autodesk.AutoCAD.ApplicationServices.Application
Imports Autodesk.AutoCAD.DatabaseServices

Imports Autodesk.Civil.ApplicationServices
Imports Autodesk.Civil.DatabaseServices.Styles


Public Class frmSnoopObjects
  Private Const stringEmptyCollection As String = "[Empty Collection]"
  Private Const stringCollection As String = "[Collection]"
  Private Const stringEmpty As String = "[Empty]"

  Public Sub frmCorridors_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    lvwProperties.FullRowSelect = True
    ListRootEntities()
    CheckIsLaunching()

    ' some properties may not work as expected
    ' this is the list to ignore (April 2013)
    _bannedList.Add("Profile_Area")
  End Sub

  Private Function AddTreeNode(ByVal nodes As TreeNodeCollection, ByVal text As String, ByVal tag As Object) As TreeNode
    Dim newNode As New TreeNode
    newNode.Text = text
    newNode.Tag = tag
    nodes.Add(newNode)
    Return newNode
  End Function

  Private Sub AddGroup(ByVal groupName As String)
    For Each g As ListViewGroup In lvwProperties.Groups
      If g.Header = groupName Then
        Return
      End If
    Next
    lvwProperties.Groups.Add(New ListViewGroup(groupName))
  End Sub

  Private Function GetGroup(ByVal groupName As String) As ListViewGroup
    For Each g As ListViewGroup In lvwProperties.Groups
      If g.Header = groupName Then
        Return g
      End If
    Next
    Return Nothing
  End Function

  Private Function GetValueAsString(ByVal prop As PropertyInfo, ByVal obj As Object) As String
    Dim propValue As Object = Nothing
    Try
      If Not (prop.CanRead) Then Return "[Write-only]"

      propValue = prop.GetValue(obj, Nothing)
      If (IsEnumerable(propValue)) Then
        Dim asEnum As IEnumerable = TryCast(propValue, IEnumerable)
        For Each item As Object In asEnum
          Return stringCollection 'at least one, ok
        Next
        Return stringEmptyCollection 'empty collection then
      End If
    Catch
    End Try
    If (propValue IsNot Nothing) Then Return propValue.ToString
    Return stringEmpty
  End Function

  Private Function GetValue(ByVal prop As PropertyInfo, ByVal obj As Object) As Object
    Dim propValue As Object = Nothing
    Try
      propValue = prop.GetValue(obj, Nothing)
    Catch ex As Exception
    End Try
    If (propValue IsNot Nothing) Then Return propValue
    Return stringEmpty
  End Function

  Private Function IsEnumerable(ByVal obj As Object) As Boolean
    Dim asString As String = TryCast(obj, String)
    If (asString IsNot Nothing) Then Return False 'strings are enumerable, but not collections
    Dim asEnum As IEnumerable = TryCast(obj, IEnumerable)
    Return (asEnum IsNot Nothing)
  End Function

  Private Function IsObjectId(obj As Object) As Boolean
    ' is a number (i.e. ID)?
    Dim idNumber As Long = 0
    If (TypeOf obj Is ObjectId) Then
      Return True
      'ElseIf (Long.TryParse(obj.ToString(), idNumber)) Then
      '  Dim id As New ObjectId(New IntPtr(idNumber))
      '  Return id.IsValid
    End If
    Return False
  End Function

  Private Function IsStringOrNumber(obj As Object) As Boolean
    If (TypeOf obj Is String) Then Return True
    Dim l As Long
    Return Long.TryParse(obj.ToString(), l)
  End Function

  Private Sub treObjects_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treObjects.AfterSelect
    If (e.Node.Tag Is Nothing) Then Return

    ListObjectInformation(e.Node.Tag)
  End Sub

  Private Sub ListObjectInformation(ByVal obj As Object)
    lvwProperties.Items.Clear()
    lvwProperties.Groups.Clear()

    Dim objType As Type = obj.GetType

    ListProperties(obj, objType)
    ListMethods(obj, objType)

    For Each collumn As ColumnHeader In lvwProperties.Columns
      collumn.Width = -1 'resize to content size
    Next
  End Sub

  Private Sub ListProperties(ByVal obj As Object, ByVal objType As Type)
    Dim properties As PropertyInfo() = objType.GetProperties()
    For Each prop As PropertyInfo In properties
      If (IsBanned(objType, prop.Name)) Then Continue For

      AddGroup(prop.DeclaringType.FullName)

      Dim propName As String = prop.Name
      Dim propType As String = prop.PropertyType.Name
      Dim propValue As String = GetValueAsString(prop, obj)

      Dim listItem As New ListViewItem(New String() {propName, propType, propValue})
      If ((propValue = stringCollection)) Then listItem.Font = New System.Drawing.Font(listItem.Font, Drawing.FontStyle.Bold)
      If (propValue = stringEmpty Or propValue = stringEmptyCollection) Then listItem.ForeColor = Drawing.Color.Red

      listItem.Tag = GetValue(prop, obj)
      listItem.Group = GetGroup(prop.DeclaringType.FullName)
      lvwProperties.Items.Add(listItem)
    Next
  End Sub

  Dim _bannedList As New Specialized.StringCollection

  Private Function IsBanned(objectType As Type, propName As String) As Boolean
    Return (_bannedList.Contains(String.Format("{0}_{1}", objectType.Name, propName)))
  End Function

  Private Function GetValue(ByVal method As MethodInfo, ByVal obj As Object) As Object
    Dim methodValue As Object = Nothing
    Try
      methodValue = method.Invoke(obj, Nothing)
    Catch ex As Exception
    End Try
    If (methodValue IsNot Nothing) Then Return methodValue
    Return stringEmpty
  End Function

  Private Sub ListMethods(ByVal obj As Object, ByVal objType As Type)
    Dim methods As MethodInfo() = objType.GetMethods()
    For Each meth As MethodInfo In methods
      If meth.Name.Contains("Reactor") Then Continue For 'skip some unwanted methods...
      If (meth.GetParameters().Length = 0 And Not meth.IsSpecialName And meth.ReturnType IsNot GetType(System.Void)) Then
        Dim methodValue As Object = GetValue(meth, obj)
        If (IsEnumerable(methodValue)) Then
          AddGroup(meth.DeclaringType.FullName)

          Dim propName As String = meth.Name
          Dim propType As String = meth.ReturnType.Name
          Dim propValue As String = stringCollection

          Dim listItem As New ListViewItem(New String() {propName, propType, propValue})
          listItem.Font = New System.Drawing.Font(listItem.Font, Drawing.FontStyle.Bold)

          listItem.Tag = methodValue
          listItem.Group = GetGroup(meth.DeclaringType.FullName)
          lvwProperties.Items.Add(listItem)
        End If
      End If
    Next
  End Sub

  Private _trans As Transaction

  Private Sub ListRootEntities()
    Dim db As Database = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database

    _trans = db.TransactionManager.StartTransaction()
    Dim civilDoc As CivilDocument = CivilApplication.ActiveDocument

    ListStyles(_trans, civilDoc)
    ListAlignments(_trans, civilDoc)
    ListCorridors(_trans, civilDoc)
    ListAssemblies(_trans, civilDoc)
    ListSubassemblies(_trans, civilDoc)
    ListPipeNetworks(_trans, civilDoc)
    ListSurfaces(_trans, civilDoc)
    ListPointGroups(_trans, civilDoc)
  End Sub

  Private Sub ListStyles(ByVal trans As Transaction, ByVal civilDoc As CivilDocument)
    Dim node As TreeNode = AddTreeNode(treObjects.Nodes, "Styles", civilDoc.Styles)
  End Sub

  Private Sub ListAlignments(ByVal trans As Transaction, ByVal civilDoc As CivilDocument)
    Dim alignRootNode As TreeNode = AddTreeNode(treObjects.Nodes, "Alignments", Nothing)
    ListMainCollection(alignRootNode, trans, civilDoc.GetAlignmentIds())
  End Sub

  Private Sub ListCorridors(ByVal trans As Transaction, ByVal civilDoc As CivilDocument)
    Dim corridorRootNode As TreeNode = AddTreeNode(treObjects.Nodes, "Corridors", Nothing)
    ListMainCollection(corridorRootNode, trans, civilDoc.CorridorCollection)
  End Sub

  Private Sub ListAssemblies(ByVal trans As Transaction, ByVal civilDoc As CivilDocument)
    Dim assRootNode As TreeNode = AddTreeNode(treObjects.Nodes, "Assemblies", Nothing)
    ListMainCollection(assRootNode, trans, civilDoc.AssemblyCollection)
  End Sub

  Private Sub ListSubassemblies(ByVal trans As Transaction, ByVal civilDoc As CivilDocument)
    Dim subAssRootNode As TreeNode = AddTreeNode(treObjects.Nodes, "Subassemblies", Nothing)
    ListMainCollection(subAssRootNode, trans, civilDoc.SubassemblyCollection)
  End Sub

  Private Sub ListPipeNetworks(ByVal trans As Transaction, ByVal civilDoc As CivilDocument)
    Dim pipeNetworkRootNode As TreeNode = AddTreeNode(treObjects.Nodes, "Pipe Networks", Nothing)
    ListMainCollection(pipeNetworkRootNode, trans, civilDoc.GetPipeNetworkIds())
  End Sub

  Private Sub ListSurfaces(ByVal trans As Transaction, ByVal civilDoc As CivilDocument)
    Dim surfaceRootNode As TreeNode = AddTreeNode(treObjects.Nodes, "Surfaces", Nothing)
    ListMainCollection(surfaceRootNode, trans, civilDoc.GetSurfaceIds)
  End Sub

  Private Sub ListPointGroups(ByVal trans As Transaction, ByVal civilDoc As CivilDocument)
    Dim pointGrpRootNode As TreeNode = AddTreeNode(treObjects.Nodes, "Point Groups", Nothing)
    ListMainCollection(pointGrpRootNode, trans, civilDoc.PointGroups)
  End Sub

  Public Sub ListMainCollection(ByVal rootNode As TreeNode, ByVal trans As Transaction, ByVal coll As IEnumerable)
    Try
      For Each itemId As ObjectId In coll
        Dim item As Object = trans.GetObject(itemId, OpenMode.ForWrite)
        Dim node As TreeNode = AddTreeNode(rootNode.Nodes, item.Name, item)
      Next
    Catch
    End Try
  End Sub

  Private Sub lvwProperties_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwProperties.SelectedIndexChanged
    If (lvwProperties.SelectedItems.Count <> 1) Then Return
    Dim selectedItem As ListViewItem = lvwProperties.SelectedItems(0)
    If (IsEnumerable(selectedItem.Tag)) Then
      CollectionItemSelected(selectedItem)
    ElseIf (IsObjectId(selectedItem.Tag)) Then
      ObjectIdItemSelected(selectedItem)
    End If
    Return 'this will treat only enumerables
  End Sub

  Private Sub ObjectIdItemSelected(selectedItem As ListViewItem)
    Dim objId As ObjectId = selectedItem.Tag
    Dim obj As DBObject = _trans.GetObject(objId, OpenMode.ForWrite)

    Dim currentTreeNode As TreeNode = treObjects.SelectedNode
    Dim objTreeNode As TreeNode = AddTreeNode(currentTreeNode.Nodes, _
                                              String.Format("[{0}] {1}", selectedItem.Text, GetNameOrType(obj)), _
                                              obj)
    treObjects.SelectedNode = objTreeNode
  End Sub

  Private Sub CollectionItemSelected(selectedItem As Object)
    Dim currentTreeNode As TreeNode = treObjects.SelectedNode
    Dim collectionName As String = selectedItem.SubItems(0).Text

    ' check if already exists
    For Each n As TreeNode In currentTreeNode.Nodes
      If (n.Text = collectionName) Then Return
    Next

    Dim collectionNode As TreeNode = AddTreeNode(currentTreeNode.Nodes, collectionName, Nothing)

    Dim indexNumber As Integer = 0
    For Each itemInCollection As Object In selectedItem.Tag
      Dim item As Object = itemInCollection
      If (item.GetType() Is GetType(ObjectId)) Then
        item = _trans.GetObject(DirectCast(itemInCollection, ObjectId), OpenMode.ForWrite)
      End If
      AddTreeNode(collectionNode.Nodes, GetNameOrIndex(indexNumber, item), item)
      indexNumber += 1
    Next

    If (indexNumber = 0) Then
      'ops, empty, change listview text, style and remove from tree
      selectedItem.SubItems(2).Text = stringEmptyCollection
      selectedItem.ForeColor = Drawing.Color.Red
      collectionNode.Remove()
    End If

    currentTreeNode.Expand()
  End Sub

  Private Function GetNameOrIndex(ByVal index As Integer, ByVal obj As Object)
    Dim propName As PropertyInfo = obj.GetType().GetProperty("Name")
    Try
      If (propName IsNot Nothing AndAlso propName.CanRead) Then
        Dim propValue As String = propName.GetValue(obj, Nothing)
        Return String.Format("[Item {0:0}] {1}", index, propValue)
      End If
    Catch
      ' nothing...
    End Try

    If IsStringOrNumber(obj) Then
      Return String.Format("[Item {0:0}] {1}", index, obj.ToString())
    End If

    Return String.Format("[Item {0:0}]", index)
  End Function

  Private Function GetNameOrType(ByVal obj As Object)
    Dim propName As PropertyInfo = obj.GetType().GetProperty("Name")
    If (propName IsNot Nothing AndAlso propName.CanRead) Then
      Return propName.GetValue(obj, Nothing)
    Else
      Return obj.GetType().Name
    End If
  End Function

  Private Sub frmCorridors_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    _trans.Abort()
    _trans.Dispose()
  End Sub

#Region "Registry Functions"

  Public Sub CheckIsLaunching()
    Dim acadAppKey As Microsoft.Win32.RegistryKey = GetAcadAppKey(False)
    Dim subKeys As [String]() = acadAppKey.GetSubKeyNames()
    For Each subKey As [String] In subKeys
      If subKey.Equals("Civil3DSnoopDB") Then
        chkLaunchWithC3D.Checked = True
      End If
    Next
    'now control changes
    AddHandler Me.chkLaunchWithC3D.CheckedChanged, AddressOf chkLaunchWithC3D_CheckedChanged
  End Sub

  Private Sub chkLaunchWithC3D_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLaunchWithC3D.CheckedChanged
    Dim acadAppKey As Microsoft.Win32.RegistryKey = GetAcadAppKey(True)
    If chkLaunchWithC3D.Checked Then
      Dim curAssemblyPath As String = System.Reflection.Assembly.GetExecutingAssembly().Location
      Dim acadAppC3DInspectorKey As Microsoft.Win32.RegistryKey = acadAppKey.CreateSubKey("Civil3DSnoopDB")
      acadAppC3DInspectorKey.SetValue("DESCRIPTION", "Civil3D Snoop Database", Microsoft.Win32.RegistryValueKind.[String])
      acadAppC3DInspectorKey.SetValue("LOADCTRLS", 14, Microsoft.Win32.RegistryValueKind.DWord)
      acadAppC3DInspectorKey.SetValue("LOADER", curAssemblyPath, Microsoft.Win32.RegistryValueKind.[String])
      acadAppC3DInspectorKey.SetValue("MANAGED", 1, Microsoft.Win32.RegistryValueKind.DWord)
    Else
      acadAppKey.DeleteSubKeyTree("Civil3DSnoopDB")
    End If
    acadAppKey.Close()
  End Sub

  Private Function GetAcadAppKey(ByVal forWrite As Boolean) As Microsoft.Win32.RegistryKey
    Dim acadKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey _
                                                 (Autodesk.AutoCAD.DatabaseServices.HostApplicationServices.Current.UserRegistryProductRootKey)
    Return acadKey.OpenSubKey("Applications", forWrite)
  End Function

#End Region

  Private Sub btnSelectObject_Click(sender As System.Object, e As System.EventArgs) Handles btnSelectObject.Click
    Dim ed As Editor = AcApplication.DocumentManager.MdiActiveDocument.Editor
    Using userIt As EditorUserInteraction = ed.StartUserInteraction(Me.Handle)
      Dim resSelEnt As PromptEntityResult = ed.GetEntity("Select an entity: ")
      If (resSelEnt.Status <> PromptStatus.OK) Then Exit Sub

      Dim obj As DBObject = _trans.GetObject(resSelEnt.ObjectId, OpenMode.ForWrite)

      AddToSelecionNode(obj)
      ListObjectInformation(obj)
    End Using
  End Sub

  Public Sub AddToSelecionNode(obj As DBObject)
    Dim selectionNode As TreeNode
    If (treObjects.Nodes.ContainsKey("Selected")) Then
      selectionNode = treObjects.Nodes.Item("Selected")
    Else
      selectionNode = treObjects.Nodes.Add("Selected", "Selected objects")
    End If
    treObjects.SelectedNode = AddTreeNode(selectionNode.Nodes, GetNameOrType(obj), obj)
    selectionNode.ExpandAll()
  End Sub
End Class