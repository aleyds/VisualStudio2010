using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrecisionCalc
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void FormSizeChange(object sender, EventArgs e)
        {
            int hight = this.Height;
            int width = this.Width;


        }

        private void OnOpenCsvFileClick(object sender, EventArgs e)
        {
            if (openCSVFile.ShowDialog() == DialogResult.OK)
            {
                textCVSFileTxt.Text = openCSVFile.FileName;
            }
        }

        
    }
}
