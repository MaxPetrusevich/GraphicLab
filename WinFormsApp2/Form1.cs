//using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void PlotButton_Click(object sender, EventArgs e)
        {
            List<PointF> points = new List<PointF>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    float x, y;
                    if (float.TryParse(row.Cells[0].Value?.ToString(), out x) && float.TryParse(row.Cells[1].Value?.ToString(), out y))
                    {
                        points.Add(new PointF(x, y));
                    }
                }
            }

            // Построение графика
            grafik.PlotPoints(points.ToArray());
        }
    }
}
