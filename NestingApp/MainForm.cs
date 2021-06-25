using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NestingApp
{
    public partial class MainForm : CCSkinMain
    {
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
        /// 导入svg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 开始嵌套计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 下载嵌套计算的结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_Click(object sender, EventArgs e)
        {

        }
    }
}
