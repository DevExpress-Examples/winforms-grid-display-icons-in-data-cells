using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.Controls;

namespace ImagesInCells
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraEditors.CheckEdit checkEdit1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(666, 372);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(37, 315);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Allow Edit";
            this.checkEdit1.Size = new System.Drawing.Size(80, 19);
            this.checkEdit1.TabIndex = 1;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(666, 372);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}


        private Image GetImageFromResource(string fileName) {
            Stream resource = typeof(Form1).Assembly.GetManifestResourceStream("ImagesInCells.Resources." + fileName);
            return Image.FromStream(resource);
        }

        private byte[] GetImageData(string fileName) {
			Image img = GetImageFromResource(fileName);
			MemoryStream mem = new MemoryStream();
            img.Save(mem, System.Drawing.Imaging.ImageFormat.Bmp);
			return mem.GetBuffer();
		}

		private void Form1_Load(object sender, System.EventArgs e) {
			DataTable table = new DataTable();
			table.Columns.Add("IsRead", typeof(bool));
			table.Columns.Add("Type", typeof(int));
			table.Columns.Add("Picture", typeof(byte[]));

			table.Rows.Add(new object[] {true, 1, GetImageData("datasource_enabled.bmp")});
			table.Rows.Add(new object[] {false, 2, null});
			table.Rows.Add(new object[] {null, 3, null});

			GridColumn column;
			gridView1.PopulateColumns(table);

			// CheckEdit
			RepositoryItemCheckEdit checkEdit = gridControl1.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
			checkEdit.PictureChecked = GetImageFromResource("read.bmp");
			checkEdit.PictureUnchecked = GetImageFromResource("unread.bmp");
			checkEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
			column = gridView1.Columns["IsRead"];
			column.ColumnEdit = checkEdit;
			column.Caption += " (CheckEdit)";
			
			// ImageComboBox
			RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as RepositoryItemImageComboBox;
			
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
			images.AddImage(GetImageFromResource("Error.png"));
			images.AddImage(GetImageFromResource("Warning.png"));
			images.AddImage(GetImageFromResource("Info.png"));
            imageCombo.SmallImages = images;
			imageCombo.Items.Add(new ImageComboBoxItem("Error", 1, 0));
			imageCombo.Items.Add(new ImageComboBoxItem("Warning", 2, 1));
			imageCombo.Items.Add(new ImageComboBoxItem("Info", 3, 2));
			imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
			column = gridView1.Columns["Type"];
			column.ColumnEdit = imageCombo;
			column.Caption += " (ImageComboBox)";
			column.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor;

			// PictureEdit
            RepositoryItemPictureEdit pictureEdit = gridControl1.RepositoryItems.Add("PictureEdit") as RepositoryItemPictureEdit;
			pictureEdit.SizeMode = PictureSizeMode.Zoom;
			pictureEdit.NullText = " ";
			column = gridView1.Columns["Picture"];
			column.ColumnEdit = pictureEdit;
			column.Caption += " (PictureEdit)";

			gridControl1.DataSource = table;
			gridView1.OptionsBehavior.Editable = checkEdit1.Checked;
		}

		private void checkEdit1_CheckedChanged(object sender, System.EventArgs e) {
			gridView1.OptionsBehavior.Editable = checkEdit1.Checked;
		}
	}
}

