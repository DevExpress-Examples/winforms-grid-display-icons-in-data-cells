Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.Utils.Controls
Imports DevExpress.XtraGrid
Imports System.Collections.Generic
Imports DevExpress.XtraGrid.Views.Grid

Namespace ImagesInCells

    ''' <summary>
    ''' Summary description for Form1.
    ''' </summary>
    Public Class Form1
        Inherits Form

        Private gridControl1 As GridControl

        Private gridView1 As GridView

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
            gridControl1 = New GridControl()
            gridView1 = New GridView()
            CType(gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' gridControl1
            ' 
            gridControl1.Dock = DockStyle.Fill
            gridControl1.Location = New System.Drawing.Point(0, 0)
            gridControl1.MainView = gridView1
            gridControl1.Name = "gridControl1"
            gridControl1.Size = New System.Drawing.Size(665, 372)
            gridControl1.TabIndex = 0
            gridControl1.ViewCollection.AddRange(New Views.Base.BaseView() {gridView1})
            ' 
            ' gridView1
            ' 
            gridView1.GridControl = gridControl1
            gridView1.Name = "gridView1"
            gridView1.OptionsView.RowAutoHeight = True
            gridView1.OptionsView.ShowGroupPanel = False
            ' 
            ' Form1
            ' 
            AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            ClientSize = New System.Drawing.Size(666, 372)
            Me.Controls.Add(gridControl1)
            Name = "Form1"
            Text = "Form1"
            AddHandler Load, New EventHandler(AddressOf Form1_Load)
            CType(gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(gridView1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
        End Sub

#End Region
        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Call Application.Run(New Form1())
        End Sub

        Private Function GetImageFromResource(ByVal fileName As String) As Image
            Dim value = Properties.Resources.ResourceManager.GetObject(fileName, Properties.Resources.Culture)
            Return TryCast(value, Image)
        End Function

        Private Function GetImageData(ByVal fileName As String) As Byte()
            Dim img As Image = GetImageFromResource(fileName)
            Using mem = New MemoryStream()
                img.Save(mem, System.Drawing.Imaging.ImageFormat.Bmp)
                Return mem.GetBuffer()
            End Using
        End Function

        Private Function CreateTable() As DataTable
            Dim table As DataTable = New DataTable()
            Dim dataRow As DataRow
            table.Columns.Add("CheckEdit", GetType(Boolean))
            table.Columns.Add("ImageComboBox", GetType(Integer))
            table.Columns.Add("PictureEdit", GetType(Image))
            table.Columns.Add("ContextImage", GetType(String))
            table.Columns.Add("HTMLImage", GetType(String))
            table.Columns.Add("CustomDraw", GetType(String))
            dataRow = table.NewRow()
            dataRow("CheckEdit") = True
            dataRow("ImageComboBox") = 1
            dataRow("PictureEdit") = GetImageFromResource("Image5")
            dataRow("ContextImage") = "Text1"
            dataRow("CustomDraw") = "Image0"
            table.Rows.Add(dataRow)
            dataRow = table.NewRow()
            dataRow("CheckEdit") = False
            dataRow("ImageComboBox") = 2
            dataRow("PictureEdit") = Nothing
            dataRow("ContextImage") = ""
            '"<image=show_16x16.png> Image left"
            dataRow("HTMLImage") = "Text " & "<image=Image7.png>"
            dataRow("CustomDraw") = "Image1"
            table.Rows.Add(dataRow)
            dataRow = table.NewRow()
            dataRow("CheckEdit") = False
            dataRow("ImageComboBox") = 3
            dataRow("PictureEdit") = Nothing
            dataRow("ContextImage") = "Text3"
            dataRow("CustomDraw") = "Image2"
            table.Rows.Add(dataRow)
            Return table
        End Function

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            gridControl1.DataSource = CreateTable()
            AddHandler gridView1.CustomDrawCell, AddressOf GridView1_CustomDrawCell
            AddHandler gridView1.CustomUnboundColumnData, AddressOf GridView1_CustomUnboundColumnData
            Dim checkEdit As RepositoryItemCheckEdit = TryCast(gridControl1.RepositoryItems.Add("CheckEdit"), RepositoryItemCheckEdit)
            checkEdit.PictureChecked = GetImageFromResource("Image0")
            checkEdit.PictureUnchecked = GetImageFromResource("Image1")
            checkEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
            gridView1.Columns("CheckEdit").ColumnEdit = checkEdit
            Dim imageCombo As RepositoryItemImageComboBox = TryCast(gridControl1.RepositoryItems.Add("ImageComboBoxEdit"), RepositoryItemImageComboBox)
            Dim images As DevExpress.Utils.ImageCollection = New DevExpress.Utils.ImageCollection()
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
            Dim textEdit As RepositoryItemTextEdit = New RepositoryItemTextEdit()
            textEdit.ContextImageOptions.Image = GetImageFromResource("Image6")
            gridView1.Columns("ContextImage").ColumnEdit = textEdit
            gridControl1.RepositoryItems.Add(textEdit)
            Dim buttonEdit As RepositoryItemHypertextLabel = New RepositoryItemHypertextLabel()
            buttonEdit.TextEditStyle = TextEditStyles.DisableTextEditor
            buttonEdit.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True
            Dim collection = New DevExpress.Utils.ImageCollection()
            collection.AddImage(GetImageFromResource("Image7"))
            collection.Images.SetKeyName(0, "Image7.png")
            buttonEdit.HtmlImages = collection
            gridView1.Columns("HTMLImage").ColumnEdit = buttonEdit
            gridControl1.RepositoryItems.Add(buttonEdit)
            gridView1.Columns.Add(New GridColumn() With {.Caption = "Unbound Column", .FieldName = "UnboundColumn", .UnboundDataType = GetType(Object), .Visible = True})
            Dim pictureEditUnbound As RepositoryItemPictureEdit = TryCast(gridControl1.RepositoryItems.Add("PictureEdit"), RepositoryItemPictureEdit)
            pictureEditUnbound.SizeMode = PictureSizeMode.Zoom
            pictureEditUnbound.NullText = " "
            gridView1.Columns("UnboundColumn").ColumnEdit = pictureEditUnbound
            gridControl1.RepositoryItems.Add(pictureEditUnbound)
        End Sub

        Private imageCache As Dictionary(Of String, Image) = New Dictionary(Of String, Image)()

        Private Sub GridView1_CustomUnboundColumnData(ByVal sender As Object, ByVal e As Views.Base.CustomColumnDataEventArgs)
            If e.IsSetData Then Return
            Dim view As GridView = TryCast(sender, GridView)
            Dim path = CStr(view.GetListSourceRowCellValue(e.ListSourceRowIndex, "CustomDraw"))
            If String.IsNullOrEmpty(path) Then Return
            Try
                e.Value = GetImageFromResource(path)
            Catch
            End Try
        End Sub

        Private Sub GridView1_CustomDrawCell(ByVal sender As Object, ByVal e As Views.Base.RowCellCustomDrawEventArgs)
            If e.RowHandle <> GridControl.NewItemRowHandle AndAlso Equals(e.Column.FieldName, "CustomDraw") Then
                e.DisplayText = String.Empty
                e.DefaultDraw()
                e.Cache.DrawImage(GetImageFromResource(e.CellValue.ToString()), e.Bounds.X, e.Bounds.Y)
            End If
        End Sub
    End Class
End Namespace
