namespace GDAL_O
{
    partial class GDAL
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.shp文件创立ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.创建字段ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.创建点要素ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开shp文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更换颜色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.字段管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.字段查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shp文件创立ToolStripMenuItem,
            this.打开shp文件ToolStripMenuItem,
            this.刷新ToolStripMenuItem,
            this.更换颜色ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(653, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // shp文件创立ToolStripMenuItem
            // 
            this.shp文件创立ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.创建字段ToolStripMenuItem,
            this.创建点要素ToolStripMenuItem,
            this.字段管理ToolStripMenuItem,
            this.字段查询ToolStripMenuItem});
            this.shp文件创立ToolStripMenuItem.Name = "shp文件创立ToolStripMenuItem";
            this.shp文件创立ToolStripMenuItem.Size = new System.Drawing.Size(107, 24);
            this.shp文件创立ToolStripMenuItem.Text = "创建shp文件";
            //this.shp文件创立ToolStripMenuItem.Click += new System.EventHandler(this.shp文件创立ToolStripMenuItem_Click);
            // 
            // 创建字段ToolStripMenuItem
            // 
            this.创建字段ToolStripMenuItem.Name = "创建字段ToolStripMenuItem";
            this.创建字段ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.创建字段ToolStripMenuItem.Text = "创建字段";
            this.创建字段ToolStripMenuItem.Click += new System.EventHandler(this.创建字段ToolStripMenuItem_Click);
            // 
            // 创建点要素ToolStripMenuItem
            // 
            this.创建点要素ToolStripMenuItem.Name = "创建点要素ToolStripMenuItem";
            this.创建点要素ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.创建点要素ToolStripMenuItem.Text = "创建点要素";
            this.创建点要素ToolStripMenuItem.Click += new System.EventHandler(this.创建点要素ToolStripMenuItem_Click);
            // 
            // 打开shp文件ToolStripMenuItem
            // 
            this.打开shp文件ToolStripMenuItem.Name = "打开shp文件ToolStripMenuItem";
            this.打开shp文件ToolStripMenuItem.Size = new System.Drawing.Size(107, 24);
            this.打开shp文件ToolStripMenuItem.Text = "打开shp文件";
            this.打开shp文件ToolStripMenuItem.Click += new System.EventHandler(this.打开shp文件ToolStripMenuItem_Click);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // 更换颜色ToolStripMenuItem
            // 
            this.更换颜色ToolStripMenuItem.Name = "更换颜色ToolStripMenuItem";
            this.更换颜色ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.更换颜色ToolStripMenuItem.Text = "更换颜色";
            this.更换颜色ToolStripMenuItem.Click += new System.EventHandler(this.更换颜色ToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(653, 478);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // 字段管理ToolStripMenuItem
            // 
            this.字段管理ToolStripMenuItem.Name = "字段管理ToolStripMenuItem";
            this.字段管理ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.字段管理ToolStripMenuItem.Text = "字段管理";
            this.字段管理ToolStripMenuItem.Click += new System.EventHandler(this.字段管理ToolStripMenuItem_Click);
            // 
            // 字段查询ToolStripMenuItem
            // 
            this.字段查询ToolStripMenuItem.Name = "字段查询ToolStripMenuItem";
            this.字段查询ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.字段查询ToolStripMenuItem.Text = "字段查询";
            this.字段查询ToolStripMenuItem.Click += new System.EventHandler(this.字段查询ToolStripMenuItem_Click);
            // 
            // GDAL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 509);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GDAL";
            this.Text = "GDAL";
            this.Load += new System.EventHandler(this.GDAL_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem shp文件创立ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 创建字段ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 创建点要素ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开shp文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更换颜色ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem 字段管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 字段查询ToolStripMenuItem;
    }
}