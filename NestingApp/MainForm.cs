using CCWin;
using NestingLibPort;
using NestingLibPort.Data;
using NestingLibPort.Util;
using Svg;
using Svg.Transforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace NestingApp
{
    public partial class MainForm : CCSkinMain
    {
       private List<NestPath> nestPaths = new List<NestPath>();
        //
        ///支持的最大图像大小
        ///
        public Size IMaximumSize { get; set; }
        public SvgDocument document { get; set; }
        public SvgDocument documentOut { get; set; }
        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.txtSpace.TextChanged += TxtSpace_TextChanged;
            this.txtCurve.TextChanged += TxtCurve_TextChanged;
            this.txtPart.TextChanged += TxtPart_TextChanged;
            this.txtGApopulation.TextChanged += TxtGApopulation_TextChanged;
            this.txtGAmutationrate.TextChanged += TxtGAmutationrate_TextChanged;
            this.checkPartinPart.CheckedChanged += CheckPartinPart_CheckedChanged;
            this.checkExploreconcaveareas.CheckedChanged += CheckExploreconcaveareas_CheckedChanged;
        }

        #region 设置
        private void CheckExploreconcaveareas_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void CheckPartinPart_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void TxtGAmutationrate_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TxtGApopulation_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TxtPart_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TxtCurve_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TxtSpace_TextChanged(object sender, EventArgs e)
        {
            
        }

        #endregion

        

        /// <summary>
        /// 开始嵌套计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\output.svg"))
            {
                File.Delete($"{AppDomain.CurrentDomain.BaseDirectory}\\output.svg");
            }
            this.Invoke(new Action(() => { toolStripProgressBar.Value = 0; toolStripProgressBar.Visible = true; this.Refresh(); }));
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (ts, te) =>
            {
                if (toolStripProgressBar.Value <= 95)
                {
                    int sc = Convert.ToInt32(this.txtSpace.Text) <= 0 ? 10 : ((10 / Convert.ToInt32(this.txtSpace.Text) < 0 ? 1 : (10 / Convert.ToInt32(this.txtSpace.Text))));
                    this.Invoke(new Action(() => { toolStripProgressBar.Value += sc; this.Refresh(); }));
                }
            };
            timer.Start();
            timer.Enabled = true;
            Task.Run(new Action(()=>
            {
                NestPath bin = new NestPath();
                double binWidth = picNestPath.Width - 10;
                double binHeight = picNestPath.Height - 10;
                bin.add(5, 5);
                bin.add(binWidth, 5);
                bin.add(binWidth, binHeight);
                bin.add(5, binHeight);
                Console.WriteLine("Bin Size : Width = " + binWidth + " Height=" + binHeight);
                //var nestPathsA = SvgUtil.transferSvgIntoPolygons("test.xml");
                //var nestPaths = transferSvgIntoPolygonsSvg(document);
                //Console.WriteLine("Reading File = test.xml");
                List<NestPath> paths = GetSelectNestPaths();
                Console.WriteLine("No of parts = " + paths.Count);
                Config config = new Config();
                Console.WriteLine("Configuring Nest");
                //Config.CLIIPER_SCALE = Convert.ToInt32(this.txtPart.Text.Trim());
                Config.CURVE_TOLERANCE = Convert.ToDouble(this.txtCurve.Text.Trim());
                config.SPACING = Convert.ToInt32(this.txtSpace.Text.Trim());
                config.POPULATION_SIZE = Convert.ToInt32(this.txtGAmutationrate.Text.Trim());
                config.MUTATION_RATE = Convert.ToInt32(this.txtGApopulation.Text.Trim());
                //config.CONCAVE = this.checkPartinPart.Checked;
                config.USE_HOLE = this.checkExploreconcaveareas.Checked;
                Nest nest = new Nest(bin, paths, config, 1);
                Console.WriteLine("Performing Nest");
                List<List<Placement>> appliedPlacement = nest.startNest();
                Console.WriteLine("Nesting Completed");
                var svgPolygons = SvgUtil.svgGenerator(paths, appliedPlacement, binWidth, binHeight);
                Console.WriteLine("Converted to SVG format");
                SvgUtil.saveSvgFile(svgPolygons, "output.svg");
                Console.WriteLine("Saved svg file..Opening File");
                if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\output.svg"))
                {
                    documentOut = GetSvgDocument($"{AppDomain.CurrentDomain.BaseDirectory}\\output.svg");
                    if (documentOut.HasChildren())
                    {
                        this.Invoke(new Action(() => {
                            this.picNestPath.Image = documentOut.Draw(); ;
                        }));
                    }
                }
                //Process.Start("output.svg");
                Console.ReadLine();
                this.Invoke(new Action(() => { timer.Stop(); toolStripProgressBar.Value = 100; toolStripProgressBar.Visible = false; this.Refresh(); }));
            }));
        }
        /// <summary>
        /// 下载嵌套计算的结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_Click(object sender, EventArgs e)
        {
            string sss = $"{AppDomain.CurrentDomain.BaseDirectory}\\output.svg";
            Process.Start(sss);

        }

        /// <summary>
        /// 导入svg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            IMaximumSize = new Size(this.picNestPath.Width - 10, this.picNestPath.Height - 10);
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = $"SVG文件(*.svg)|*.svg|XML文件(*.xml)|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = dialog.FileName;
                if (dialog.FilterIndex == 1)
                    document = GetSvgDocument(FileName);
                else
                    return;
                //else if (dialog.FilterIndex == 2)
                //{
                //    XmlDocument xmlDocument = new XmlDocument();
                //    xmlDocument.Load(FileName);
                //    document = GetSvgDocument(xmlDocument);
                //}
                if (document != null)
                {
                    this.picNestPath.Margin = new Padding(3);
                    this.picNestPath.Image = document.Draw();
                    //ImageList imageList = new ImageList();
                    //imageList.ImageSize = new Size(128, 128);
                    //SetLargeImageList(ref imageList);
                    imgList.Images.Clear();
                    transferSvgIntoPolygonsSvg(document, ref imgList);
                    //this.lvwTU.View = View.LargeIcon;
                    //this.lvwTU.LargeImageList = imageList;

                    List<ListViewItem> listViews = new List<ListViewItem>();
                    this.lvwTU.BeginUpdate();
                    for (int n = 0; n < imgList.Images.Count; n++)
                    {
                        ListViewItem lvi = new ListViewItem(Convert.ToString(nestPaths[n].bid.ToString()), n);
                        listViews.Add(lvi);
                    }
                    this.lvwTU.Items.AddRange(listViews.ToArray());
                    this.lvwTU.EndUpdate();
                }
            }
        }
        public List<NestPath> transferSvgIntoPolygonsSvg(SvgDocument document, ref ImageList imageList)
        {
            nestPaths = new List<NestPath>();
            foreach (SvgElement item in document.Children)
            {
                AddNestPath(item, ref nestPaths,ref imageList);
            }
            return nestPaths;
        }
        public void AddNestPath(SvgElement element, ref List<NestPath> nestPaths, ref ImageList imageList)
        {
            switch (element)
            {
                case Svg.SvgGroup Group:
                    {
                        foreach (SvgElement node in Group.Nodes.Where(nd => nd is SvgElement))
                            AddNestPath(node, ref nestPaths,ref imageList);
                        foreach (SvgElement node in Group.Children.Where(nd => nd is SvgElement))
                            AddNestPath(node, ref nestPaths, ref imageList);
                    }
                    break;
                case Svg.SvgPath Path:
                    {
                        NestPath nPath = new NestPath();
                        for (int n = 0; n < Path.PathData.Count; n += 1)
                        {
                            double x = Path.PathData[n].Start.X;
                            double y = Path.PathData[n].Start.Y;
                            nPath.add(x, y);
                        }
                        nPath.bid = nestPaths.Count;
                        nPath.setRotation(4);
                        nestPaths.Add(nPath);
                    }
                    break;
                case Svg.SvgPolygon Polygon:
                    {
                        NestPath polygon = new NestPath();
                        List<PointF> polygonf = new List<PointF>();
                        for (int n = 0; (n + 1) < Polygon.Points.Count; n += 2)
                        {
                            double x = Polygon.Points[n];
                            double y = Polygon.Points[n + 1];
                            polygon.add(x, y);
                            polygonf.Add(new PointF(Polygon.Points[n], Polygon.Points[n + 1]));
                        }
                        polygon.bid = nestPaths.Count;
                        polygon.setRotation(4);
                        nestPaths.Add(polygon);

                        Image imgSrc = new Bitmap(128, 128);
                        imgSrc.Tag = string.IsNullOrEmpty(element.ID) ? $"{nestPaths.Count}" : element.ID;
                        Graphics gSrc = Graphics.FromImage(imgSrc);
                        float MinX = polygonf.Min(p => p.X);
                        if (MinX > 5)
                            MinX -= 5;
                        float MinY = polygonf.Min(p => p.Y);//用最小值求平移
                        if (MinY > 5)
                            MinY -= 5;

                        float MaxX = polygonf.Max(p => p.X);
                        float MaxY = polygonf.Max(p => p.Y);//用最大值求缩放比例
                        float MinL = Math.Max(Math.Abs(MaxX - MinX), Math.Abs(MaxY - MinY));
                        float Scale = 0;
                        if (MinL < 11.8)
                            Scale = 118 / MinL;
                        else
                            Scale = MinL / 118;
                        for (int n = 0; n < polygonf.Count; n++)
                        {
                            PointF item = polygonf[n];
                            item.X -= MinX;
                            item.Y -= MinY;
                            item.X /= Scale;
                            item.Y /= Scale;
                            polygonf[n] = item;
                        }
                        gSrc.DrawPolygon(new Pen(Brushes.Red, 2), polygonf.ToArray());
                        imageList.Images.Add(imgSrc);
                        gSrc.Dispose();
                    }
                    break;
                case Svg.SvgRectangle Rectangle:
                    {
                        double width = Rectangle.Bounds.Width;
                        double height = Rectangle.Bounds.Height;
                        double x = Rectangle.Bounds.X;
                        double y = Rectangle.Bounds.Y;
                        NestPath rect = new NestPath();
                        rect.add(x, y);
                        rect.add(x + width, y);
                        rect.add(x + width, y + height);
                        rect.add(x, y + height);
                        rect.bid = nestPaths.Count;
                        rect.setRotation(4);
                        nestPaths.Add(rect);

                        Image imgSrc = new Bitmap(128, 128);
                        imgSrc.Tag = string.IsNullOrEmpty(element.ID) ? $"{nestPaths.Count}" : element.ID;
                        Graphics gSrc = Graphics.FromImage(imgSrc);
                        x = 5;
                        y = 5;
                        double MinL = Math.Max(width, height);
                        double Scale = 0;
                        if (MinL < 11.8)
                            Scale = 118 / MinL;
                        else
                            Scale = MinL / 118;
                        width /= Scale;
                        height /= Scale;
                        gSrc.DrawRectangle(new Pen(Brushes.Red, 2), (float)x, (float)y, (float)width, (float)height);
                        imageList.Images.Add(imgSrc);
                        gSrc.Dispose();
                    }
                    break;
                default:
                    break;
            }
        }

        public void SetLargeImageList(ref ImageList imageList)
        {
            int i = 0;
            nestPaths = new List<NestPath>();
            foreach (SvgElement element in document.Children)
            {
                switch (element)
                {
                    case Svg.SvgGroup Group:
                        break;

                    case Svg.SvgPath sPath:
                        {
                            Image imgSrc = new Bitmap(128, 128);
                            imgSrc.Tag = string.IsNullOrEmpty(element.ID) ? $"{i}" : element.ID;
                            Graphics gSrc = Graphics.FromImage(imgSrc);
                            GraphicsPath gpath = new GraphicsPath();
                            NestPath npPolygon = new NestPath();
                            foreach (var item in sPath.PathData)
                            {
                                gpath.AddLine(item.Start, item.End);
                                npPolygon.add(item.Start.X, item.Start.Y);
                                npPolygon.add(item.End.X, item.End.Y);
                            }
                            npPolygon.bid = i;
                            npPolygon.setRotation(4);
                            nestPaths.Add(npPolygon);

                            gSrc.DrawPath(new Pen(Brushes.Red, 2), gpath);
                            imageList.Images.Add(imgSrc);
                            gSrc.Dispose();
                        }
                        i += 1;
                        break;
                    case Svg.SvgPolygon Polygon:
                        {
                            Image imgSrc = new Bitmap(128, 128);
                            imgSrc.Tag = string.IsNullOrEmpty(element.ID) ? $"{i}" : element.ID;
                            Graphics gSrc = Graphics.FromImage(imgSrc);
                            List<PointF> polygon = new List<PointF>();
                            NestPath npPolygon = new NestPath();
                            for (int n = 0; (n + 1) < Polygon.Points.Count; n += 2)
                            {
                                polygon.Add(new PointF(Polygon.Points[n], Polygon.Points[n + 1]));
                                npPolygon.add(Polygon.Points[n], Polygon.Points[n + 1]);
                            }
                            npPolygon.bid = i;
                            npPolygon.setRotation(4);
                            nestPaths.Add(npPolygon);

                            float MinX = polygon.Min(p => p.X);
                            if (MinX > 5)
                                MinX -= 5;
                            float MinY = polygon.Min(p => p.Y);//用最小值求平移
                            if (MinY > 5)
                                MinY -= 5;

                            float MaxX = polygon.Max(p => p.X);
                            float MaxY = polygon.Max(p => p.Y);//用最大值求缩放比例
                            float MinL = Math.Max(Math.Abs(MaxX - MinX), Math.Abs(MaxY - MinY));
                            float Scale = 0;
                            if (MinL < 11.8)
                                Scale = 118 / MinL;
                            else
                                Scale = MinL / 118;
                            for (int n = 0; n < polygon.Count; n++)
                            {
                                PointF item = polygon[n];
                                item.X -= MinX;
                                item.Y -= MinY;
                                item.X /= Scale;
                                item.Y /= Scale;
                                polygon[n] = item;
                            }
                            gSrc.DrawPolygon(new Pen(Brushes.Red, 2), polygon.ToArray());
                            imageList.Images.Add(imgSrc);
                            gSrc.Dispose();
                        }
                        i += 1;
                        break;

                    case Svg.SvgRectangle Rectangle:
                        {
                            Image imgSrc = new Bitmap(128, 128);
                            imgSrc.Tag = string.IsNullOrEmpty(element.ID) ? $"{i}" : element.ID;
                            Graphics gSrc = Graphics.FromImage(imgSrc);
                            float width = Rectangle.Bounds.Width;
                            float height = Rectangle.Bounds.Height;
                            NestPath rect = new NestPath();
                            rect.add(Rectangle.Bounds.X, Rectangle.Bounds.Y);
                            rect.add(Rectangle.Bounds.X + width, Rectangle.Bounds.Y);
                            rect.add(Rectangle.Bounds.X + width, Rectangle.Bounds.Y + height);
                            rect.add(Rectangle.Bounds.X, Rectangle.Bounds.Y + height);
                            rect.bid = i;
                            rect.setRotation(4);
                            nestPaths.Add(rect);

                            float x = 5;
                            float y = 5;
                            float MinL = Math.Max(width, height);
                            float Scale = 0;
                            if (MinL < 11.8)
                                Scale = 118 / MinL;
                            else
                                Scale = MinL / 118;
                            width /= Scale;
                            height /= Scale;
                            gSrc.DrawRectangle(new Pen(Brushes.Red, 2), x, y, width, height);
                            imageList.Images.Add(imgSrc);
                            gSrc.Dispose();
                        }
                        i += 1;
                        break;
                    default:
                        break;
                }
            }
            //this.lvwTU.View = View.LargeIcon;
            //this.lvwTU.LargeImageList = imageList;
            this.lvwTU.BeginUpdate();
            for (int n = 0; n < imageList.Images.Count; n++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = n;
                lvi.Text = Convert.ToString(nestPaths[n].bid.ToString());
                lvi.Checked = true;
                this.lvwTU.Items.Add(lvi);
            }
            this.lvwTU.EndUpdate();
            this.lvwTU.CheckBoxes = true;
        }

        private void Item_MouseDown(object sender, MouseArg e)
        {
            if (sender is SvgElement element) {
                //element.Color = new  Color.Red;
            }
        }

        /// <summary>
        /// 将SVG文件转换为位图图像。
        /// </summary>
        /// <param name="filePath">SVG图像的全路径。</param>
        /// <returns>返回转换位图图像。</returns>
        public Bitmap GetBitmapFromSVG(string filePath)
        {
            SvgDocument document = GetSvgDocument(filePath);
            Bitmap bmp = document.Draw();
            return bmp;
        }
        /// <summary>
        /// 获取一个svgdocument操作使用提供的路径。
        /// </summary>
        /// <param name="filePath">位图图像的路径.</param>
        /// <returns>返回SVG文档</returns>
        public SvgDocument GetSvgDocument(XmlDocument xmlDocument)
        {
            SvgDocument sdocument = SvgDocument.Open(xmlDocument);
            if(sdocument != null)
                sdocument = AdjustSize(sdocument);
            return sdocument;
        }

        /// <summary>
        /// 获取一个svgdocument操作使用提供的路径。
        /// </summary>
        /// <param name="filePath">位图图像的路径.</param>
        /// <returns>返回SVG文档</returns>
        public SvgDocument GetSvgDocument(string filePath)
        {
            SvgDocument sdocument = SvgDocument.Open(filePath);
            return AdjustSize(sdocument);
        }
        /// <summary>
        /// 确保图像不超过最大大小，同时保留纵横比
        /// </summary>
        /// <param name="document">要调整大小的SVG文档</param>
        /// <returns>返回一个大小或根据该文件的原始文件。</returns>
        private SvgDocument AdjustSize(SvgDocument document)
        {
            if (document.Height > IMaximumSize.Height)
            {
                document.Width = (int)((document.Width / (double)document.Height) * IMaximumSize.Height);
                document.Height = IMaximumSize.Height;
            }
            return document;
        }
        ///
        /// 递归在svg图上赋值
        ///
        /// 配置列表中数据
        /// Svg标识点Id
        /// 对应标识点的数据
        /// 颜色的RGB值
        private void GetChild(SvgElement element, string Id, string value, string msgColor)
        {
            if ((element is SvgText) && element.ID == Id)
            {
                (element as SvgText).Text = value;
                try
                {
                    var fColor = Color.FromArgb(Convert.ToInt32(msgColor));
                    (element as SvgText).Fill = new SvgColourServer(fColor);
                }
                catch (Exception ex)
                {
                    (element as SvgText).Fill = new SvgColourServer(Color.Lime);
                }
            }
            else if ((element is SvgPath) && element.ID == Id)
            {
                try
                {
                    var fColor = Color.FromArgb(Convert.ToInt32(msgColor));
                    (element as SvgPath).Fill = new SvgColourServer(fColor);
                }
                catch (Exception ex)
                {
                    (element as SvgPath).Fill = new SvgColourServer(Color.Lime);
                }
            }
            else {
                var fColor = Color.FromArgb(Convert.ToInt32(msgColor));
                element.Color = new SvgColourServer(fColor);
            }
            if (element.Children.Count > 0)
            {
                foreach (Svg.SvgElement item in element.Children)
                {
                    GetChild(item, Id, value, msgColor);
                }
            }
        }

        private void pictConvertedImage_MouseDown(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.lvwTU.Items)
            {
                item.Checked = true;
            }
        }
        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbReSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.lvwTU.Items)
            {
                item.Checked = !item.Checked;
            }
        }

        public List<NestPath> GetSelectNestPaths()
        {
            List<NestPath> SelectNestPaths = new List<NestPath>();
            this.Invoke(new Action(() =>
            {
                int index = 0;
                this.lvwTUS.Items.Clear();
                foreach (ListViewItem item in this.lvwTU.Items)
                {
                    if (item.Checked)
                    {
                        SelectNestPaths.Add(nestPaths.ElementAt(index));
                        ListViewItem lvi = new ListViewItem(Convert.ToString(index + 1), index);
                        this.lvwTUS.Items.Add(lvi);
                    }
                    index++;
                }
            }));
            return SelectNestPaths;
        }

        private void checkPartinPart_Click(object sender, EventArgs e)
        {
            this.checkPartinPart.Checked = !this.checkPartinPart.Checked;
        }

        private void checkExploreconcaveareas_Click(object sender, EventArgs e)
        {
            this.checkPartinPart.Checked = !this.checkPartinPart.Checked;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //this.Hide();
                this.skin.Hide();//.Skin.hide();
                this.notifyIcon.Visible = true;
                //弹气泡/通知框提示
                this.notifyIcon.ShowBalloonTip(6, "Nest", "已经隐藏到系统托盘", ToolTipIcon.Info);
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                this.skin.Show();
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }
        }

        private void 显示主界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.skin.Hide();
            }
            else
            {
                this.skin.Show();
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
