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
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
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
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(666, 372);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
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
            var value = Properties.Resources.ResourceManager.GetObject(fileName, Properties.Resources.Culture);
            return value as Image;
        }

        private byte[] GetImageData(string fileName) {
			Image img = GetImageFromResource(fileName);
            using(var mem = new MemoryStream()) {
                img.Save(mem, System.Drawing.Imaging.ImageFormat.Bmp);
                return mem.GetBuffer();
            }
		}

        DataTable CreateTable()
        {
            DataTable table = new DataTable();
            DataRow dataRow;

            table.Columns.Add("CheckEdit", typeof(bool));
            table.Columns.Add("ImageComboBox", typeof(int));
            table.Columns.Add("PictureEdit", typeof(Image));
            table.Columns.Add("ContextImage", typeof(string));
            table.Columns.Add("HTMLImage", typeof(string));

            dataRow = table.NewRow();
            dataRow["CheckEdit"] = true;
            dataRow["ImageComboBox"] = 1;
            dataRow["PictureEdit"] = GetImageFromResource("Image5"); 
            dataRow["ContextImage"] = "Text1";
            table.Rows.Add(dataRow);

            dataRow = table.NewRow();
            dataRow["CheckEdit"] = false;
            dataRow["ImageComboBox"] = 2;
            dataRow["PictureEdit"] = null;
            dataRow["ContextImage"] = "";
            //"<image=show_16x16.png> Image left"
            dataRow["HTMLImage"] = "Text " + "<image=Image7.png>";
            table.Rows.Add(dataRow);

            dataRow = table.NewRow();
            dataRow["CheckEdit"] = false;
            dataRow["ImageComboBox"] = 3;
            dataRow["PictureEdit"] = null;
            dataRow["ContextImage"] = "Text3";
            table.Rows.Add(dataRow);

            return table;
        }

        private void Form1_Load(object sender, System.EventArgs e) {
            gridControl1.DataSource = CreateTable();

            RepositoryItemCheckEdit checkEdit = gridControl1.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            checkEdit.PictureChecked = GetImageFromResource("Image0");
            checkEdit.PictureUnchecked = GetImageFromResource("Image1");
            checkEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            gridView1.Columns["CheckEdit"].ColumnEdit = checkEdit;

            RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            images.AddImage(GetImageFromResource("Image2"));
            images.AddImage(GetImageFromResource("Image3"));
            images.AddImage(GetImageFromResource("Image4"));
            imageCombo.SmallImages = images;
            imageCombo.Items.Add(new ImageComboBoxItem("Minor", 1, 0));
            imageCombo.Items.Add(new ImageComboBoxItem("Moderate", 2, 1));
            imageCombo.Items.Add(new ImageComboBoxItem("Severe", 3, 2));
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridControl1.RepositoryItems.Add(imageCombo);
            gridView1.Columns["ImageComboBox"].ColumnEdit = imageCombo;

            RepositoryItemPictureEdit pictureEdit = gridControl1.RepositoryItems.Add("PictureEdit") as RepositoryItemPictureEdit;
            pictureEdit.SizeMode = PictureSizeMode.Zoom;
            pictureEdit.NullText = " ";
            gridView1.Columns["PictureEdit"].ColumnEdit = pictureEdit;
            gridControl1.RepositoryItems.Add(pictureEdit);

            RepositoryItemTextEdit textEdit = new RepositoryItemTextEdit();
            //textEdit.ContextImageOptions.Image = GetImageFromResource("Image6");
            gridView1.Columns["ContextImage"].ColumnEdit = textEdit;
            gridControl1.RepositoryItems.Add(textEdit);

            RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
            buttonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
            buttonEdit.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            var collection = new DevExpress.Utils.ImageCollection();
            collection.AddImage(GetImageFromResource("Image7"));
            collection.Images.SetKeyName(0, "Image7.png");
            buttonEdit.HtmlImages = collection;
            gridView1.Columns["HTMLImage"].ColumnEdit = buttonEdit;
            gridControl1.RepositoryItems.Add(buttonEdit);
        }
	}
}

