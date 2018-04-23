Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.Utils.Controls

Namespace ImagesInCells
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Form1
		Inherits System.Windows.Forms.Form
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		Private WithEvents checkEdit1 As DevExpress.XtraEditors.CheckEdit
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

			'
			' TODO: Add any constructor code after InitializeComponent call
			'
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			Me.checkEdit1 = New DevExpress.XtraEditors.CheckEdit()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.checkEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridControl1
			' 
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridControl1.EmbeddedNavigator.Name = ""
			Me.gridControl1.Location = New System.Drawing.Point(0, 0)
			Me.gridControl1.MainView = Me.gridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.Size = New System.Drawing.Size(666, 372)
			Me.gridControl1.TabIndex = 0
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
			' 
			' gridView1
			' 
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
			Me.gridView1.OptionsView.ShowGroupPanel = False
			' 
			' checkEdit1
			' 
			Me.checkEdit1.Location = New System.Drawing.Point(37, 315)
			Me.checkEdit1.Name = "checkEdit1"
			Me.checkEdit1.Properties.Caption = "Allow Edit"
			Me.checkEdit1.Size = New System.Drawing.Size(80, 19)
			Me.checkEdit1.TabIndex = 1
'			Me.checkEdit1.CheckedChanged += New System.EventHandler(Me.checkEdit1_CheckedChanged);
			' 
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(666, 372)
			Me.Controls.Add(Me.checkEdit1)
			Me.Controls.Add(Me.gridControl1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.checkEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub


		Private Function GetImageFromResource(ByVal fileName As String) As Image
			Dim resource As Stream = GetType(Form1).Assembly.GetManifestResourceStream("Resources." & fileName)
			Return Image.FromStream(resource)
		End Function

		Private Function GetImageData(ByVal fileName As String) As Byte()
			Dim img As Image = GetImageFromResource(fileName)
			Dim mem As New MemoryStream()
			img.Save(mem, System.Drawing.Imaging.ImageFormat.Bmp)
			Return mem.GetBuffer()
		End Function

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
			Dim table As New DataTable()
			table.Columns.Add("IsRead", GetType(Boolean))
			table.Columns.Add("Type", GetType(Integer))
			table.Columns.Add("Picture", GetType(Byte()))

			table.Rows.Add(New Object() {True, 1, GetImageData("datasource_enabled.bmp")})
			table.Rows.Add(New Object() {False, 2, Nothing})
			table.Rows.Add(New Object() {Nothing, 3, Nothing})

			Dim column As GridColumn
			gridView1.PopulateColumns(table)

			' CheckEdit
			Dim checkEdit As RepositoryItemCheckEdit = TryCast(gridControl1.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
			checkEdit.PictureChecked = GetImageFromResource("read.bmp")
			checkEdit.PictureUnchecked = GetImageFromResource("unread.bmp")
			checkEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
			column = gridView1.Columns("IsRead")
			column.ColumnEdit = checkEdit
			column.Caption &= " (CheckEdit)"

			' ImageComboBox
			Dim imageCombo As RepositoryItemImageComboBox = TryCast(gridControl1.RepositoryItems.Add("ImageComboBoxEdit"), RepositoryItemImageComboBox)

			Dim images As New DevExpress.Utils.ImageCollection()
			images.AddImage(GetImageFromResource("Error.png"))
			images.AddImage(GetImageFromResource("Warning.png"))
			images.AddImage(GetImageFromResource("Info.png"))
			imageCombo.SmallImages = images
			imageCombo.Items.Add(New ImageComboBoxItem("Error", 1, 0))
			imageCombo.Items.Add(New ImageComboBoxItem("Warning", 2, 1))
			imageCombo.Items.Add(New ImageComboBoxItem("Info", 3, 2))
			imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
			column = gridView1.Columns("Type")
			column.ColumnEdit = imageCombo
			column.Caption &= " (ImageComboBox)"
			column.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor

			' PictureEdit
			Dim pictureEdit As RepositoryItemPictureEdit = TryCast(gridControl1.RepositoryItems.Add("PictureEdit"), RepositoryItemPictureEdit)
			pictureEdit.SizeMode = PictureSizeMode.Zoom
			pictureEdit.NullText = " "
			column = gridView1.Columns("Picture")
			column.ColumnEdit = pictureEdit
			column.Caption &= " (PictureEdit)"

			gridControl1.DataSource = table
			gridView1.OptionsBehavior.Editable = checkEdit1.Checked
		End Sub

		Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkEdit1.CheckedChanged
			gridView1.OptionsBehavior.Editable = checkEdit1.Checked
		End Sub
	End Class
End Namespace

