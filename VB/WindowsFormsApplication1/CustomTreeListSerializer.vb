Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization
Imports DevExpress.XtraTreeList
Imports System.Reflection
Imports System.IO

Namespace WindowsFormsApplication1

	<XmlRootAttribute("TreeList"), XmlInclude(GetType(CustomerAddress)), XmlInclude(GetType(CustomerCertificate))> _
	Public Class CustomTreeListSerializer
		Inherits DevExpress.XtraTreeList.TreeList.TreeListXmlSerializationHelper
	End Class

	Public Module TreeListExtensions
        <System.Runtime.CompilerServices.Extension()> _
        Public Sub ExportToXmlEx(ByVal tree As TreeList, ByVal pathName As String)
            Dim sh As New CustomTreeListSerializer()
            Dim pi As PropertyInfo = GetType(DevExpress.XtraTreeList.TreeList.TreeListXmlSerializationHelper).GetProperty("TreeList", BindingFlags.GetProperty Or BindingFlags.Instance Or BindingFlags.NonPublic)
            pi.SetValue(sh, tree, Nothing)
            sh.OnBeforeSerializing()
            Dim stream As Stream = Nothing
            Try
                stream = New FileStream(TryCast(pathName, String), FileMode.Create)
                Dim serializer As New XmlSerializer(GetType(CustomTreeListSerializer))
                If stream IsNot Nothing Then
                    serializer.Serialize(stream, sh)
                End If
            Finally
                If TypeOf stream Is FileStream Then
                    CType(stream, FileStream).Close()
                End If
            End Try
        End Sub

        <System.Runtime.CompilerServices.Extension()> _
        Public Sub ImportFromXmlEx(ByVal tree As TreeList, ByVal pathName As String)
            If (Not tree.IsUnboundMode) OrElse tree.TreeListDisposing Then
                Return
            End If
            Dim stream As Stream = Nothing
            tree.BeginUpdate()
            tree.BeginUnboundLoad()
            Try
                stream = New FileStream(TryCast(pathName, String), FileMode.Open)
                Dim serializer As New XmlSerializer(GetType(CustomTreeListSerializer))
                Dim reader As System.Xml.XmlReader = New System.Xml.XmlTextReader(stream)
                If reader Is Nothing OrElse (Not serializer.CanDeserialize(reader)) Then
                    Return
                End If
                Dim sh As CustomTreeListSerializer = Nothing
                sh = CType(serializer.Deserialize(reader), CustomTreeListSerializer)
                If sh Is Nothing Then
                    Return
                End If
                Dim pi As PropertyInfo = GetType(DevExpress.XtraTreeList.TreeList.TreeListXmlSerializationHelper).GetProperty("TreeList", BindingFlags.GetProperty Or BindingFlags.Instance Or BindingFlags.NonPublic)
                pi.SetValue(sh, tree, Nothing)
                sh.OnAfterDeserializing()
            Finally
                If TypeOf stream Is FileStream Then
                    CType(stream, FileStream).Close()
                End If
                tree.EndUnboundLoad()
                tree.EndUpdate()
            End Try
        End Sub

	End Module
End Namespace
