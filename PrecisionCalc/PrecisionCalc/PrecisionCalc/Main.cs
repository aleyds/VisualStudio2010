using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrecisionCalc.CSV;
using ZedGraph;

namespace PrecisionCalc
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            PaneInit();
        }

        private void FormSizeChange(object sender, EventArgs e)
        {
            int hight = this.Height;
            int width = this.Width;


        }

        private void PaneInit()
        {
            GraphPane myPane = zedGraphControl.GraphPane;
            myPane.Title.Text = "锯齿误差曲线图";
            myPane.XAxis.Title.Text = "x轴";
            myPane.YAxis.Title.Text = "y轴";

        }

        private PointPairList PaneFill(DataTable dt)
        {
            double x, y1;
            PointPairList list1 = new PointPairList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    x = Convert.ToDouble(dt.Rows[i][0]);
                    y1 = Convert.ToDouble(dt.Rows[i][1]);
                    list1.Add(x, y1);
                }
            }
            return list1;
        }

        private void PaneDisplay(PointPairList display, Color LineColor)
        {
            GraphPane myPane = zedGraphControl.GraphPane;
            myPane.CurveList.Clear();
            LineItem myCurve = myPane.AddCurve("Porsche", display, LineColor, SymbolType.Diamond);
            zedGraphControl.IsShowPointValues = true;
            myPane.AxisChange();
            zedGraphControl.Invalidate();
        }

        private void OnOpenCsvFileClick(object sender, EventArgs e)
        {
            if (openCSVFile.ShowDialog() == DialogResult.OK)
            {
                textCVSFileTxt.Text = openCSVFile.FileName;
                DataTable dt = new DataTable();
                CSVFileHelper.ReadCSV(openCSVFile.FileName,0,dt);
                PointPairList displayList = PaneFill(dt);
                PaneDisplay(displayList, Color.Red);
            }
        }

        
    }
}
