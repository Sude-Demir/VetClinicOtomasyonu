namespace VetClinic.UI1
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.accMenu = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElementRandevu = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElementSistem = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElementAnasayfa = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement3 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement8 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement6 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement9 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accCikis = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.panelContent = new System.Windows.Forms.Panel();
            this.picHome = new System.Windows.Forms.PictureBox();
            this.panelHastaEkle = new System.Windows.Forms.Panel();
            this.panelHastaListele = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtAd = new DevExpress.XtraEditors.TextEdit();
            this.txtSoyad = new DevExpress.XtraEditors.TextEdit();
            this.dateDogumTarihi = new DevExpress.XtraEditors.DateEdit();
            this.cmbCinsiyet = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbTur = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.accMenu)).BeginInit();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            this.panelHastaEkle.SuspendLayout();
            this.panelHastaListele.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoyad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDogumTarihi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDogumTarihi.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCinsiyet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTur.Properties)).BeginInit();
            this.SuspendLayout();
            this.accMenu.AllowItemSelection = true;
            this.accMenu.Appearance.AccordionControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.accMenu.Appearance.AccordionControl.Options.UseFont = true;
            this.accMenu.Appearance.Item.Hovered.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.accMenu.Appearance.Item.Hovered.Options.UseFont = true;
            this.accMenu.Appearance.Item.Normal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.accMenu.Appearance.Item.Normal.Options.UseFont = true;
            this.accMenu.Appearance.Item.Pressed.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.accMenu.Appearance.Item.Pressed.Options.UseFont = true;
            this.accMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.accMenu.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElementAnasayfa,
            this.accordionControlElement2,
            this.accordionControlElementRandevu,
            this.accordionControlElementSistem});
            this.accMenu.Location = new System.Drawing.Point(0, 0);
            this.accMenu.Name = "accMenu";
            this.accMenu.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Hidden;
            this.accMenu.Size = new System.Drawing.Size(260, 600);
            this.accMenu.TabIndex = 0;
            this.accMenu.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            this.accMenu.Click += new System.EventHandler(this.accMenu_Click);
            this.accordionControlElementAnasayfa.Name = "accordionControlElementAnasayfa";
            this.accordionControlElementAnasayfa.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElementAnasayfa.Text = "🏠 ANASAYFA";
            this.accordionControlElementAnasayfa.Click += new System.EventHandler(this.accordionControlElementAnasayfa_Click);
            // 
            // accordionControlElement2
            // 
            this.accordionControlElement2.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement3,
            this.accordionControlElement8});
            this.accordionControlElement2.Expanded = true;
            this.accordionControlElement2.Name = "accordionControlElement2";
            this.accordionControlElement2.Text = "📋 HASTA İŞLEMLERİ";
            this.accordionControlElement2.Click += new System.EventHandler(this.accordionControlElement2_Click);
            // 
            // accordionControlElement3
            // 
            this.accordionControlElement3.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.accordionControlElement3.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement3.Name = "accordionControlElement3";
            this.accordionControlElement3.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement3.Text = "Hasta Ekle";
            this.accordionControlElement3.Click += new System.EventHandler(this.accordionControlElement3_Click);
            // 
            // accordionControlElement8
            // 
            this.accordionControlElement8.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.accordionControlElement8.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement8.Name = "accordionControlElement8";
            this.accordionControlElement8.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement8.Text = "Hasta Listesi";
            this.accordionControlElement8.Click += new System.EventHandler(this.accordionControlElement8_Click);
            // 
            // accordionControlElementRandevu
            // 
            this.accordionControlElementRandevu.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement6,
            this.accordionControlElement9});
            this.accordionControlElementRandevu.Expanded = true;
            this.accordionControlElementRandevu.Name = "accordionControlElementRandevu";
            this.accordionControlElementRandevu.Text = "📅 RANDEVU İŞLEMLERİ";
            // 
            // accordionControlElement6
            // 
            this.accordionControlElement6.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.accordionControlElement6.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement6.Name = "accordionControlElement6";
            this.accordionControlElement6.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement6.Text = "Randevu Oluştur";
            this.accordionControlElement6.Click += new System.EventHandler(this.accordionControlElement6_Click);
            // 
            // accordionControlElement9
            // 
            this.accordionControlElement9.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.accordionControlElement9.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement9.Name = "accordionControlElement9";
            this.accordionControlElement9.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement9.Text = "Randevu Listesi";
            this.accordionControlElement9.Click += new System.EventHandler(this.accordionControlElement9_Click);
            // 
            // accordionControlElementSistem
            // 
            this.accordionControlElementSistem.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accCikis});
            this.accordionControlElementSistem.Expanded = true;
            this.accordionControlElementSistem.Name = "accordionControlElementSistem";
            this.accordionControlElementSistem.Text = "⚙️ SİSTEM";
            // 
            // accCikis
            // 
            this.accCikis.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.accCikis.Appearance.Normal.Options.UseFont = true;
            this.accCikis.Name = "accCikis";
            this.accCikis.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accCikis.Text = "Çıkış";
            this.accCikis.Click += new System.EventHandler(this.accCikis_Click);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.picHome);
            this.panelContent.Controls.Add(this.panelHastaEkle);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(260, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(800, 600);
            this.panelContent.TabIndex = 1;
            // 
            // picHome
            // 
            this.picHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picHome.Location = new System.Drawing.Point(0, 0);
            this.picHome.Name = "picHome";
            this.picHome.Size = new System.Drawing.Size(800, 600);
            this.picHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHome.TabIndex = 0;
            this.picHome.TabStop = false;
            this.picHome.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panelHastaEkle
            // 
            this.panelHastaEkle.Controls.Add(this.panelHastaListele);
            this.panelHastaEkle.Controls.Add(this.layoutControl1);
            this.panelHastaEkle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHastaEkle.Location = new System.Drawing.Point(0, 0);
            this.panelHastaEkle.Name = "panelHastaEkle";
            this.panelHastaEkle.Size = new System.Drawing.Size(800, 600);
            this.panelHastaEkle.TabIndex = 1;
            this.panelHastaEkle.Visible = false;
            // 
            // panelHastaListele
            // 
            this.panelHastaListele.Controls.Add(this.gridControl1);
            this.panelHastaListele.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHastaListele.Location = new System.Drawing.Point(0, 400);
            this.panelHastaListele.Name = "panelHastaListele";
            this.panelHastaListele.Size = new System.Drawing.Size(800, 200);
            this.panelHastaListele.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(800, 200);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.View_RowStyle);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtAd);
            this.layoutControl1.Controls.Add(this.txtSoyad);
            this.layoutControl1.Controls.Add(this.dateDogumTarihi);
            this.layoutControl1.Controls.Add(this.cmbCinsiyet);
            this.layoutControl1.Controls.Add(this.cmbTur);
            this.layoutControl1.Controls.Add(this.btnKaydet);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(716, 179, 650, 400);
            this.layoutControl1.Root = null;
            this.layoutControl1.Size = new System.Drawing.Size(800, 400);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtAd
            // 
            this.txtAd.Location = new System.Drawing.Point(100, 20);
            this.txtAd.Name = "txtAd";
            this.txtAd.Size = new System.Drawing.Size(200, 20);
            this.txtAd.TabIndex = 0;
            // 
            // txtSoyad
            // 
            this.txtSoyad.Location = new System.Drawing.Point(100, 50);
            this.txtSoyad.Name = "txtSoyad";
            this.txtSoyad.Size = new System.Drawing.Size(200, 20);
            this.txtSoyad.TabIndex = 1;
            // 
            // dateDogumTarihi
            // 
            this.dateDogumTarihi.EditValue = null;
            this.dateDogumTarihi.Location = new System.Drawing.Point(100, 80);
            this.dateDogumTarihi.Name = "dateDogumTarihi";
            this.dateDogumTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDogumTarihi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDogumTarihi.Size = new System.Drawing.Size(200, 20);
            this.dateDogumTarihi.TabIndex = 2;
            // 
            // cmbCinsiyet
            // 
            this.cmbCinsiyet.Location = new System.Drawing.Point(100, 110);
            this.cmbCinsiyet.Name = "cmbCinsiyet";
            this.cmbCinsiyet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCinsiyet.Properties.Items.AddRange(new object[] {
            "Dişi",
            "Erkek"});
            this.cmbCinsiyet.Size = new System.Drawing.Size(200, 20);
            this.cmbCinsiyet.TabIndex = 3;
            // 
            // cmbTur
            // 
            this.cmbTur.Location = new System.Drawing.Point(100, 140);
            this.cmbTur.Name = "cmbTur";
            this.cmbTur.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTur.Properties.Items.AddRange(new object[] {
            "Kedi",
            "Köpek",
            "Kuş"});
            this.cmbTur.Size = new System.Drawing.Size(200, 20);
            this.cmbTur.TabIndex = 4;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(100, 170);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(100, 23);
            this.btnKaydet.TabIndex = 5;
            this.btnKaydet.Text = "KAYDET";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 600);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.accMenu);
            this.Name = "MainForm";
            this.Text = "VetClinic";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.accMenu)).EndInit();
            this.panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            this.panelHastaEkle.ResumeLayout(false);
            this.panelHastaListele.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtAd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoyad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDogumTarihi.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDogumTarihi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCinsiyet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTur.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.AccordionControl accMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElementRandevu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElementSistem;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElementAnasayfa;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement2;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement3;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement8;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement6;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement9;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accCikis;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.PictureBox picHome;
        private System.Windows.Forms.Panel panelHastaEkle;
        private System.Windows.Forms.Panel panelHastaListele;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit txtAd;
        private DevExpress.XtraEditors.TextEdit txtSoyad;
        private DevExpress.XtraEditors.DateEdit dateDogumTarihi;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCinsiyet;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTur;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
    }
}
