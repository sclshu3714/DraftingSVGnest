using CCWin;
using NestingLibPort;
using NestingLibPort.Data;
using NestingLibPort.Util;
using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

        public List<NestPath> transferSvgIntoPolygonsSvg(SvgDocument document)
        {
            List<NestPath> nestPaths = new List<NestPath>();
            foreach (SvgElement item in document.Children)
            {
                AddNestPath(item, ref nestPaths);
            }
            return nestPaths;
        }

        public void AddNestPath(SvgElement element, ref List<NestPath> nestPaths)
        {
            switch (element)
            {
                case Svg.SvgGroup Group:
                    {
                        foreach (SvgElement node in Group.Nodes.Where(nd => nd is SvgElement))
                            AddNestPath(node, ref nestPaths);
                    }
                    break;
                case Svg.SvgPolygon Polygon:
                    NestPath polygon = new NestPath();
                    foreach (SvgUnit point in Polygon.Points)
                    {
                        string[] s = point.Value.ToString().Split('.');
                        double x = Convert.ToDouble(s[0]);
                        double y = Convert.ToDouble(s[1]);
                        polygon.add(x, y);
                    }
                    polygon.bid = nestPaths.Count;
                    polygon.setRotation(4);
                    nestPaths.Add(polygon);
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
                    }
                    break;
                default:
                    break;
            }
        }

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
                double binWidth = picNestPath.Width;
                double binHeight = picNestPath.Height;
                bin.add(0, 0);
                bin.add(binWidth, 0);
                bin.add(binWidth, binHeight);
                bin.add(0, binHeight);
                Console.WriteLine("Bin Size : Width = " + binWidth + " Height=" + binHeight);
                var nestPaths = SvgUtil.transferSvgIntoPolygons("test.xml");
                //var nestPaths = transferSvgIntoPolygonsSvg(document);
                //Console.WriteLine("Reading File = test.xml");
                Console.WriteLine("No of parts = " + nestPaths.Count);
                Config config = new Config();
                Console.WriteLine("Configuring Nest");
                config.SPACING = Convert.ToInt32(this.txtSpace.Text.Trim());
                Nest nest = new Nest(bin, nestPaths, config, 1);
                Console.WriteLine("Performing Nest");
                List<List<Placement>> appliedPlacement = nest.startNest();
                Console.WriteLine("Nesting Completed");
                var svgPolygons = SvgUtil.svgGenerator(nestPaths, appliedPlacement, binWidth, binHeight);
                Console.WriteLine("Converted to SVG format");
                SvgUtil.saveSvgFile(svgPolygons, "output.svg");
                Console.WriteLine("Saved svg file..Opening File");
                if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\output.svg"))
                {
                    documentOut = GetSvgDocument($"{AppDomain.CurrentDomain.BaseDirectory}\\output.svg");
                    if (documentOut.HasChildren())
                        this.picNestPath.Image = documentOut.Draw();
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

        }

        /// <summary>
        /// 导入svg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            IMaximumSize = new Size(this.pictConvertedImage.Width, this.pictConvertedImage.Height);
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = $"SVG文件(*.svg)|*.svg|XML文件(*.xml)|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = dialog.FileName;
                if (dialog.FilterIndex == 1)
                    document = GetSvgDocument(FileName);
                else if (dialog.FilterIndex == 2)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(FileName);
                    document = GetSvgDocument(xmlDocument);
                }
                //foreach (SvgElement item in document.Children)
                //{
                //    if (item is Svg.SvgGroup && item.Nodes.Count > 0)
                //    {
                //        foreach (SvgElement node in item.Nodes.Where(nd => nd is SvgElement))
                //        {
                //            //node.Color = new SvgColourServer(Color.Red);
                //            GetChild(node, node.ID, "A", Color.Red.ToArgb().ToString());
                //           //new SvgColourServer(Color.Lime);
                //        }
                //    }
                //    else
                //    {
                //        //item.Color = new SvgColourServer(Color.Red);
                //        GetChild(item, item.ID, "A", Color.Red.ToArgb().ToString());
                //    }
                //}
                if (document != null)
                    pictConvertedImage.Image = document.Draw();
            }
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
    }
}
