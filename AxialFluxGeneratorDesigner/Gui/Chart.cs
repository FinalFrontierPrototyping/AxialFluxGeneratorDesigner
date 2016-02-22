using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AxialFluxGeneratorDesigner.Gui
{
    public class ChartData
    {
        private static bool HasNull(DataTable table)
        {
            return
                table.Columns.Cast<DataColumn>().Any(column => table.Rows.OfType<DataRow>().Any(r => r.IsNull(column)));
        }

        public void Plot(DataTable table, Chart chart, int indexAxisX, int indexAxisY)
        {
            if (HasNull(table))
            {
                MessageBox.Show(@"Iterate data first!");
            }
            else
            {
                try
                {
                    var xValue = new List<double>();
                    var yValue = new List<double>();

                    foreach (var series in chart.Series)
                    {
                        series.Points.Clear();
                    }

                    for (var i = 1; i < table.Columns.Count; i++)
                    {
                        xValue.Add(
                            Convert.ToDouble(table.Rows[indexAxisX][i].ToString()));
                        yValue.Add(
                            Convert.ToDouble(table.Rows[indexAxisY][i].ToString()));
                    }

                    if (Math.Abs(xValue.Min() - xValue.Max()) < 0.0000001 ||
                        Math.Abs(yValue.Min() - yValue.Max()) < 0.0000001)
                    {
                        MessageBox.Show(@"The selected data cannot be charted!");
                    }
                    else
                    {
                        chart.Series[0].Points.DataBindXY(xValue, yValue);
                        chart.Series[0].ChartType = SeriesChartType.FastLine;
                        chart.Series[0].Color = Color.Black;
                        chart.ChartAreas[0].AxisX.Maximum = xValue.Max();
                        chart.ChartAreas[0].AxisX.Minimum = xValue.Min();
                        chart.ChartAreas[0].AxisY.Maximum = yValue.Max();
                        chart.ChartAreas[0].AxisY.Minimum = yValue.Min();
                        chart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                        chart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                        chart.ChartAreas[0].AxisX.Title =
                            table.Rows[indexAxisX][0].ToString();
                        chart.ChartAreas[0].AxisY.Title =
                            table.Rows[indexAxisY][0].ToString();
                        chart.Series[0].IsVisibleInLegend = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Chart error: " + ex.Message);
                }
            }
        }

        public static void SaveImage(Chart chart)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    chart.SaveImage(ms, ChartImageFormat.Bmp);
                    var bm = new Bitmap(ms);
                    bm.Save(chart.Name + ".png");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Image save error: " + ex.Message);
            }
        }
    }
}