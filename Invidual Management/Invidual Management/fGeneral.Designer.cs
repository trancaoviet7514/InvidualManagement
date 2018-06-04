namespace QuanLyQuanCafe
{
    partial class fGeneral
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fGeneral));
            this.menuOption = new System.Windows.Forms.MenuStrip();
            this.menuAccName = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuanLiChi = new System.Windows.Forms.ToolStripMenuItem();
            this.menuInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuOption.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuOption
            // 
            this.menuOption.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.menuOption.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuOption.Dock = System.Windows.Forms.DockStyle.None;
            this.menuOption.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAccName});
            this.menuOption.Location = new System.Drawing.Point(726, 4);
            this.menuOption.Name = "menuOption";
            this.menuOption.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuOption.Size = new System.Drawing.Size(101, 24);
            this.menuOption.TabIndex = 6;
            this.menuOption.Text = "text";
            // 
            // menuAccName
            // 
            this.menuAccName.AutoToolTip = true;
            this.menuAccName.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuAccName.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuQuanLiChi,
            this.menuInfo,
            this.menuLogout,
            this.menuSetting,
            this.menuThoat});
            this.menuAccName.ForeColor = System.Drawing.SystemColors.Highlight;
            this.menuAccName.Image = ((System.Drawing.Image)(resources.GetObject("menuAccName.Image")));
            this.menuAccName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuAccName.Name = "menuAccName";
            this.menuAccName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuAccName.Size = new System.Drawing.Size(93, 20);
            this.menuAccName.Text = "Đăng nhập";
            this.menuAccName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuAccName.Click += new System.EventHandler(this.menuAccName_Click);
            // 
            // menuQuanLiChi
            // 
            this.menuQuanLiChi.Image = ((System.Drawing.Image)(resources.GetObject("menuQuanLiChi.Image")));
            this.menuQuanLiChi.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.menuQuanLiChi.Name = "menuQuanLiChi";
            this.menuQuanLiChi.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuQuanLiChi.Size = new System.Drawing.Size(171, 22);
            this.menuQuanLiChi.Text = "Quản lí chi tiêu";
            this.menuQuanLiChi.Visible = false;
            this.menuQuanLiChi.Click += new System.EventHandler(this.menuTableManager_Click_1);
            // 
            // menuInfo
            // 
            this.menuInfo.Image = ((System.Drawing.Image)(resources.GetObject("menuInfo.Image")));
            this.menuInfo.Name = "menuInfo";
            this.menuInfo.Size = new System.Drawing.Size(171, 22);
            this.menuInfo.Text = "Thông tin cá nhân";
            this.menuInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuInfo.Visible = false;
            this.menuInfo.Click += new System.EventHandler(this.menuInfo_Click);
            // 
            // menuLogout
            // 
            this.menuLogout.Image = ((System.Drawing.Image)(resources.GetObject("menuLogout.Image")));
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(171, 22);
            this.menuLogout.Text = "Đăng xuất";
            this.menuLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuLogout.Visible = false;
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            // 
            // menuSetting
            // 
            this.menuSetting.Image = global::QuanLyQuanCafe.Properties.Resources.icon_Setting;
            this.menuSetting.Name = "menuSetting";
            this.menuSetting.Size = new System.Drawing.Size(171, 22);
            this.menuSetting.Text = "Cài đặt";
            // 
            // menuThoat
            // 
            this.menuThoat.Image = ((System.Drawing.Image)(resources.GetObject("menuThoat.Image")));
            this.menuThoat.Name = "menuThoat";
            this.menuThoat.Size = new System.Drawing.Size(171, 22);
            this.menuThoat.Text = "Thoát";
            this.menuThoat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuThoat.Visible = false;
            this.menuThoat.Click += new System.EventHandler(this.menuThoat_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.menuOption);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(836, 33);
            this.panel1.TabIndex = 7;
            // 
            // fGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 468);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "fGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Noodle";
            this.menuOption.ResumeLayout(false);
            this.menuOption.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuOption;
        private System.Windows.Forms.ToolStripMenuItem menuAccName;
        private System.Windows.Forms.ToolStripMenuItem menuInfo;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem menuQuanLiChi;
        private System.Windows.Forms.ToolStripMenuItem menuThoat;
        private System.Windows.Forms.ToolStripMenuItem menuSetting;
    }
}