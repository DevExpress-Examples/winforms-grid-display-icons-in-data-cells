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
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
			DirectCast(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridControl1
			' 
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
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
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(666, 372)
			Me.Controls.Add(Me.gridControl1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'INSTANT VB NOTE: The following InitializeComponent event wireup was converted to a 'Handles' clause:
'ORIGINAL LINE: this.Load += new System.EventHandler(this.Form1_Load);
			DirectCast(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub


		Private Function GetImageFromResource(ByVal fileName As String) As Image
			Dim value = My.Resources.ResourceManager.GetObject(fileName, My.Resources.Culture)
			Return TryCast(value, Image)
		End Function

		Private Function GetImageData(ByVal fileName As String) As Byte()
			Dim img As Image = GetImageFromResource(fileName)
			Dim mem As New MemoryStream()
			img.Save(mem, System.Drawing.Imaging.ImageFormat.Bmp)
			Return mem.GetBuffer()
		End Function

		Private Function CreateTable() As DataTable
			Dim table As New DataTable()
			Dim dataRow As DataRow

			table.Columns.Add("CheckEdit", GetType(Boolean))
			table.Columns.Add("ImageComboBox", GetType(Integer))
			table.Columns.Add("PictureEdit", GetType(Image))
			table.Columns.Add("ContextImage", GetType(String))
			table.Columns.Add("HTMLImage", GetType(String))

			dataRow = table.NewRow()
			dataRow("CheckEdit") = True
			dataRow("ImageComboBox") = 1
			dataRow("PictureEdit") = GetImageFromResource("Image5")
			dataRow("ContextImage") = "Text1"
			table.Rows.Add(dataRow)

			dataRow = table.NewRow()
			dataRow("CheckEdit") = False
			dataRow("ImageComboBox") = 2
			dataRow("PictureEdit") = Nothing
			dataRow("ContextImage") = ""
			'"<image=show_16x16.png> Image left"
			dataRow("HTMLImage") = "Text " & "<image=Image7.png>"
			table.Rows.Add(dataRow)

			dataRow = table.NewRow()
			dataRow("CheckEdit") = False
			dataRow("ImageComboBox") = 3
			dataRow("PictureEdit") = Nothing
			dataRow("ContextImage") = "Text3"
			table.Rows.Add(dataRow)

			Return table
		End Function

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
			gridControl1.DataSource = CreateTable()

			Dim checkEdit As RepositoryItemCheckEdit = TryCast(gridControl1.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
			checkEdit.PictureChecked = GetImageFromResource("Image0")
			checkEdit.PictureUnchecked = GetImageFromResource("Image1")
			checkEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
			gridView1.Columns("CheckEdit").ColumnEdit = checkEdit

			Dim imageCombo As RepositoryItemImageComboBox = TryCast(gridControl1.RepositoryItems.Add("ImageComboBoxEdit"), RepositoryItemImageComboBox)
			Dim images As New DevExpress.Utils.ImageCollection()
			images.AddImage(GetImageFromResource("Image2"))
			images.AddImage(GetImageFromResource("Image3"))
			images.AddImage(GetImageFromResource("Image4"))
			imageCombo.SmallImages = images
			imageCombo.Items.Add(New ImageComboBoxItem("Minor", 1, 0))
			imageCombo.Items.Add(New ImageComboBoxItem("Moderate", 2, 1))
			imageCombo.Items.Add(New ImageComboBoxItem("Severe", 3, 2))
			imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
			gridControl1.RepositoryItems.Add(imageCombo)
			gridView1.Columns("ImageComboBox").ColumnEdit = imageCombo

			Dim pictureEdit As RepositoryItemPictureEdit = TryCast(gridControl1.RepositoryItems.Add("PictureEdit"), RepositoryItemPictureEdit)
			pictureEdit.SizeMode = PictureSizeMode.Zoom
			pictureEdit.NullText = " "
			gridView1.Columns("PictureEdit").ColumnEdit = pictureEdit
			gridControl1.RepositoryItems.Add(pictureEdit)

			Dim textEdit As New RepositoryItemTextEdit()
			'textEdit.ContextImageOptions.Image = GetImageFromResource("Image6");
			gridView1.Columns("ContextImage").ColumnEdit = textEdit
			gridControl1.RepositoryItems.Add(textEdit)

			Dim buttonEdit As New RepositoryItemButtonEdit()
			buttonEdit.TextEditStyle = TextEditStyles.DisableTextEditor
			buttonEdit.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True
			Dim collection = New DevExpress.Utils.ImageCollection()
			collection.AddImage(GetImageFromResource("Image7"))
			collection.Images.SetKeyName(0, "Image7.png")
			buttonEdit.HtmlImages = collection
			gridView1.Columns("HTMLImage").ColumnEdit = buttonEdit
			gridControl1.RepositoryItems.Add(buttonEdit)
		End Sub


	End Class
End Namespace

