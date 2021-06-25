
namespace NestingApp
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tspMainTool = new CCWin.SkinControl.SkinToolStrip();
            this.btnUpload = new System.Windows.Forms.ToolStripButton();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnDownload = new System.Windows.Forms.ToolStripButton();
            this.btnSetting = new System.Windows.Forms.ToolStripSplitButton();
            this.txtSpace = new System.Windows.Forms.ToolStripTextBox();
            this.txtCurve = new System.Windows.Forms.ToolStripTextBox();
            this.txtPart = new System.Windows.Forms.ToolStripTextBox();
            this.txtGApopulation = new System.Windows.Forms.ToolStripTextBox();
            this.txtGAmutationrate = new System.Windows.Forms.ToolStripTextBox();
            this.checkPartinPart = new System.Windows.Forms.ToolStripMenuItem();
            this.checkExploreconcaveareas = new System.Windows.Forms.ToolStripMenuItem();
            this.skinSplitContainer1 = new CCWin.SkinControl.SkinSplitContainer();
            this.skinSplitContainer2 = new CCWin.SkinControl.SkinSplitContainer();
            this.tspMainTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skinSplitContainer1)).BeginInit();
            this.skinSplitContainer1.Panel2.SuspendLayout();
            this.skinSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skinSplitContainer2)).BeginInit();
            this.skinSplitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tspMainTool
            // 
            this.tspMainTool.Arrow = System.Drawing.Color.Black;
            this.tspMainTool.Back = System.Drawing.Color.White;
            this.tspMainTool.BackRadius = 4;
            this.tspMainTool.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.tspMainTool.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.tspMainTool.BaseFore = System.Drawing.Color.Black;
            this.tspMainTool.BaseForeAnamorphosis = false;
            this.tspMainTool.BaseForeAnamorphosisBorder = 4;
            this.tspMainTool.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.tspMainTool.BaseForeOffset = new System.Drawing.Point(0, 0);
            this.tspMainTool.BaseHoverFore = System.Drawing.Color.White;
            this.tspMainTool.BaseItemAnamorphosis = true;
            this.tspMainTool.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.tspMainTool.BaseItemBorderShow = true;
            this.tspMainTool.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("tspMainTool.BaseItemDown")));
            this.tspMainTool.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.tspMainTool.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("tspMainTool.BaseItemMouse")));
            this.tspMainTool.BaseItemNorml = null;
            this.tspMainTool.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.tspMainTool.BaseItemRadius = 4;
            this.tspMainTool.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.tspMainTool.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.tspMainTool.BindTabControl = null;
            this.tspMainTool.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.tspMainTool.Fore = System.Drawing.Color.Black;
            this.tspMainTool.GripMargin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.tspMainTool.HoverFore = System.Drawing.Color.White;
            this.tspMainTool.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tspMainTool.ItemAnamorphosis = true;
            this.tspMainTool.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.tspMainTool.ItemBorderShow = true;
            this.tspMainTool.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.tspMainTool.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.tspMainTool.ItemRadius = 4;
            this.tspMainTool.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.tspMainTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUpload,
            this.btnStart,
            this.btnDownload,
            this.btnSetting});
            this.tspMainTool.Location = new System.Drawing.Point(4, 28);
            this.tspMainTool.MaximumSize = new System.Drawing.Size(0, 80);
            this.tspMainTool.MinimumSize = new System.Drawing.Size(0, 80);
            this.tspMainTool.Name = "tspMainTool";
            this.tspMainTool.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.tspMainTool.Size = new System.Drawing.Size(1135, 80);
            this.tspMainTool.SkinAllColor = true;
            this.tspMainTool.TabIndex = 0;
            this.tspMainTool.Text = "skinToolStrip1";
            this.tspMainTool.TitleAnamorphosis = true;
            this.tspMainTool.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.tspMainTool.TitleRadius = 4;
            this.tspMainTool.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // btnUpload
            // 
            this.btnUpload.Image = ((System.Drawing.Image)(resources.GetObject("btnUpload.Image")));
            this.btnUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(73, 77);
            this.btnUpload.Text = "导入SVG";
            this.btnUpload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnStart
            // 
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(77, 77);
            this.btnStart.Text = "开始Nest";
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.Image")));
            this.btnDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(73, 77);
            this.btnDownload.Text = "下载SVG";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtSpace,
            this.txtCurve,
            this.txtPart,
            this.txtGApopulation,
            this.txtGAmutationrate,
            this.checkPartinPart,
            this.checkExploreconcaveareas});
            this.btnSetting.Image = ((System.Drawing.Image)(resources.GetObject("btnSetting.Image")));
            this.btnSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(58, 77);
            this.btnSetting.Text = "设置";
            this.btnSetting.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // txtSpace
            // 
            this.txtSpace.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtSpace.Name = "txtSpace";
            this.txtSpace.Size = new System.Drawing.Size(100, 27);
            this.txtSpace.Text = "0";
            this.txtSpace.ToolTipText = "零件之间的空间";
            // 
            // txtCurve
            // 
            this.txtCurve.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtCurve.Name = "txtCurve";
            this.txtCurve.Size = new System.Drawing.Size(100, 27);
            this.txtCurve.Text = "0.3";
            this.txtCurve.ToolTipText = "曲线公差";
            // 
            // txtPart
            // 
            this.txtPart.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(100, 27);
            this.txtPart.Text = "4";
            this.txtPart.ToolTipText = "零件旋转";
            // 
            // txtGApopulation
            // 
            this.txtGApopulation.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtGApopulation.Name = "txtGApopulation";
            this.txtGApopulation.Size = new System.Drawing.Size(100, 27);
            this.txtGApopulation.Text = "10";
            this.txtGApopulation.ToolTipText = "GA入口";
            // 
            // txtGAmutationrate
            // 
            this.txtGAmutationrate.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtGAmutationrate.Name = "txtGAmutationrate";
            this.txtGAmutationrate.Size = new System.Drawing.Size(100, 27);
            this.txtGAmutationrate.Text = "10";
            this.txtGAmutationrate.ToolTipText = "GA突变率";
            // 
            // checkPartinPart
            // 
            this.checkPartinPart.Name = "checkPartinPart";
            this.checkPartinPart.Size = new System.Drawing.Size(224, 26);
            this.checkPartinPart.Text = "部件在部件中";
            // 
            // checkExploreconcaveareas
            // 
            this.checkExploreconcaveareas.Name = "checkExploreconcaveareas";
            this.checkExploreconcaveareas.Size = new System.Drawing.Size(224, 26);
            this.checkExploreconcaveareas.Text = "探索凹面";
            // 
            // skinSplitContainer1
            // 
            this.skinSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.skinSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinSplitContainer1.Location = new System.Drawing.Point(4, 108);
            this.skinSplitContainer1.Name = "skinSplitContainer1";
            // 
            // skinSplitContainer1.Panel2
            // 
            this.skinSplitContainer1.Panel2.Controls.Add(this.skinSplitContainer2);
            this.skinSplitContainer1.Size = new System.Drawing.Size(1135, 521);
            this.skinSplitContainer1.SplitterDistance = 209;
            this.skinSplitContainer1.TabIndex = 1;
            // 
            // skinSplitContainer2
            // 
            this.skinSplitContainer2.CollapsePanel = CCWin.SkinControl.CollapsePanel.Panel2;
            this.skinSplitContainer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.skinSplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinSplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.skinSplitContainer2.Name = "skinSplitContainer2";
            this.skinSplitContainer2.Size = new System.Drawing.Size(922, 521);
            this.skinSplitContainer2.SplitterDistance = 698;
            this.skinSplitContainer2.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 633);
            this.Controls.Add(this.skinSplitContainer1);
            this.Controls.Add(this.tspMainTool);
            this.Name = "MainForm";
            this.Opacity = 0.8D;
            this.Radius = 8;
            this.SkinOpacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "嵌套";
            this.tspMainTool.ResumeLayout(false);
            this.tspMainTool.PerformLayout();
            this.skinSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skinSplitContainer1)).EndInit();
            this.skinSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skinSplitContainer2)).EndInit();
            this.skinSplitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CCWin.SkinControl.SkinToolStrip tspMainTool;
        private CCWin.SkinControl.SkinSplitContainer skinSplitContainer1;
        private CCWin.SkinControl.SkinSplitContainer skinSplitContainer2;
        private System.Windows.Forms.ToolStripButton btnUpload;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.ToolStripButton btnDownload;
        private System.Windows.Forms.ToolStripSplitButton btnSetting;
        private System.Windows.Forms.ToolStripTextBox txtSpace;
        private System.Windows.Forms.ToolStripTextBox txtCurve;
        private System.Windows.Forms.ToolStripTextBox txtPart;
        private System.Windows.Forms.ToolStripTextBox txtGApopulation;
        private System.Windows.Forms.ToolStripTextBox txtGAmutationrate;
        private System.Windows.Forms.ToolStripMenuItem checkPartinPart;
        private System.Windows.Forms.ToolStripMenuItem checkExploreconcaveareas;
    }
}

