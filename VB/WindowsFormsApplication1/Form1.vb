Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.XtraTreeList.Nodes

Namespace WindowsFormsApplication1
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
            treeList1.ExportToXmlEx("treeListData.xml")
		End Sub

		Private Sub simpleButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton2.Click
			If System.IO.File.Exists("treeListData.xml") Then
				treeList1.ImportFromXmlEx("treeListData.xml")
			End If
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim fields() As String = { "FirstName", "LastName", "Address", "Certificate" }
			For i As Integer = 0 To fields.Length - 1
				Dim treeListColumn As TreeListColumn = treeList1.Columns.Add()
				treeListColumn.FieldName = fields(i)
				treeListColumn.VisibleIndex = i
			Next i

            Dim nodeBob As TreeListNode = treeList1.AppendNode(New Object() {"Bob", "Brown", New CustomerAddress() With {.City = "Chicago", .Country = "USA"}, New CustomerCertificate() With {.Series = "AAA", .Number = "11-B-0235"}}, -1)
			Dim nodeJohn As TreeListNode = treeList1.AppendNode(New Object() { "John", "Stone", New CustomerAddress() With {.City = "Toronto", .Country = "Canada"}, New CustomerCertificate() With {.Series = "BBB", .Number = "11-B-0235"} }, nodeBob)
			Dim nodeJames As TreeListNode = treeList1.AppendNode(New Object() { "James", "Carrick", New CustomerAddress() With {.City = "New York", .Country = "USA"}, New CustomerCertificate() With {.Series = "CCC", .Number = "11-B-0235"} }, nodeBob)
            Dim nodeSara As TreeListNode = treeList1.AppendNode(New Object() {"Sara", "Taylor", New CustomerAddress() With {.City = "Vancouver", .Country = "Canada"}, New CustomerCertificate() With {.Series = "DDD", .Number = "11-B-0235"}}, -1)
			Dim nodeMarry As TreeListNode = treeList1.AppendNode(New Object() { "Marry", "Smith", New CustomerAddress() With {.City = "Calgary", .Country = "Canada"}, New CustomerCertificate() With {.Series = "EE", .Number = "11-B-0235"} }, nodeSara)
			Dim nodeMonika As TreeListNode = treeList1.AppendNode(New Object() { "Monika", "York", New CustomerAddress() With {.City = "Philadelphia", .Country = "USA"}, New CustomerCertificate() With {.Series = "FFF", .Number = "11-B-0235"} }, nodeSara)


		End Sub
	End Class

	Public Class CustomerAddress
		Private privateCountry As String
		Public Property Country() As String
			Get
				Return privateCountry
			End Get
			Set(ByVal value As String)
				privateCountry = value
			End Set
		End Property
		Private privateCity As String
		Public Property City() As String
			Get
				Return privateCity
			End Get
			Set(ByVal value As String)
				privateCity = value
			End Set
		End Property

		Public Overrides Function ToString() As String
			Return String.Format("{0} - {1}", Country, City)
		End Function
	End Class

	Public Class CustomerCertificate
		Private privateSeries As String
		Public Property Series() As String
			Get
				Return privateSeries
			End Get
			Set(ByVal value As String)
				privateSeries = value
			End Set
		End Property
		Private privateNumber As String
		Public Property Number() As String
			Get
				Return privateNumber
			End Get
			Set(ByVal value As String)
				privateNumber = value
			End Set
		End Property

		Public Overrides Function ToString() As String
			Return String.Format("{0} - {1}", Series, Number)
		End Function
	End Class
End Namespace
