/*  
*  FILE          : Form1.cs
*  PROJECT       : SENG3120 - Assignment #3 
*  PROGRAMMER    : Ayushkumar Rakholiya
*  FIRST VERSION : 2026-02-22 
*  DESCRIPTION   : 
*    this file contains the code to create a Windows Forms application that connects to an OLAP cube, 
*    retrieves data based on user selections, and displays it in a Pareto chart. 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.AnalysisServices.AdomdClient;

namespace a3Report
{
    public partial class Form1 : Form
    {
        private const string connStr = "Data Source=AYUSH; Catalog=Assisgment3;";

        public Form1()
        {
            InitializeComponent();
            InitializeChart();
            LoadProducts();
            LoadLines();

            // chart refreshes when user changes selections
            lstProduct.SelectedIndexChanged += LstProductSelectedIndexChanged;
            lstLine.SelectedIndexChanged += LstLineSelectedIndexChanged;

            // showing all data with no filters
            List<string> reasons;
            List<double> counts;
            GetCubeData(out reasons, out counts);
            UpdateParetoChart(reasons, counts);
        }

        //
        // FUNCTION      : LstProductSelectedIndexChanged
        // DESCRIPTION   :
        //   this will handles the SelectedIndexChanged event for the product listbox.
        // PARAMETERS    :
        //   object sender
        //   EventArgs e
        // RETURNS       :
        //   void : no return value
        //
        private void LstProductSelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> reasons;
            List<double> counts;
            GetCubeData(out reasons, out counts);
            UpdateParetoChart(reasons, counts);
        }

        //
        // FUNCTION      : LstLineSelectedIndexChanged
        // DESCRIPTION   :
        //   this will handles the SelectedIndexChanged event for the line listbox.
        // PARAMETERS    :
        //   object sender
        //   EventArgs e
        // RETURNS       :
        //   void : no return value
        //
        private void LstLineSelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> reasons;
            List<double> counts;
            GetCubeData(out reasons, out counts);
            UpdateParetoChart(reasons, counts);
        }

        //
        // FUNCTION      : LoadProducts
        // DESCRIPTION   :
        //   it will loads all product descriptions from the OLAP cube into lstProduct.
        // PARAMETERS    :
        //   None
        // RETURNS       :
        //   void : no return value
        //
        private void LoadProducts()
        {
            try
            {
                lstProduct.Items.Clear();
                lstProduct.Items.Add("All");

                using (AdomdConnection conn = new AdomdConnection(connStr))
                {
                    conn.Open();
                    AdomdCommand productCommand = new AdomdCommand(
                        @"SELECT [Measures].[Yo Yo Data Count] ON COLUMNS,
                          [Yo Yo Product].[Description].[Description].MEMBERS ON ROWS
                          FROM [YoYoDB]",
                        conn
                    );
                    AdomdDataAdapter adapter = new AdomdDataAdapter(productCommand);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string value = row[0]?.ToString();
                        if (!string.IsNullOrEmpty(value))
                            lstProduct.Items.Add(value);
                    }
                }
                lstProduct.SelectedIndex = 0;
                lstProduct.SelectionMode = SelectionMode.One;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in loading products: " + ex.Message);
            }
        }

        //
        // FUNCTION      : LoadLines
        // DESCRIPTION   :
        //   it will loads all line names from the OLAP cube into lstLine.
        // PARAMETERS    :
        //   None
        // RETURNS       :
        //   void : no return value
        //
        private void LoadLines()
        {
            try
            {
                lstLine.Items.Clear();
                lstLine.Items.Add("All");

                using (AdomdConnection conn = new AdomdConnection(connStr))
                {
                    conn.Open();
                    AdomdCommand lineCommand = new AdomdCommand(
                        @"SELECT [Measures].[Yo Yo Data Count] ON COLUMNS,
                          [Line Table].[Line Name].[Line Name].MEMBERS ON ROWS
                          FROM [YoYoDB]",
                        conn
                    );
                    AdomdDataAdapter adapter = new AdomdDataAdapter(lineCommand);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string value = row[0]?.ToString();
                        if (!string.IsNullOrEmpty(value))
                            lstLine.Items.Add(value);
                    }
                }
                lstLine.SelectedIndex = 0;
                lstLine.SelectionMode = SelectionMode.One;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading lines: " + ex.Message);
            }
        }

        //
        // FUNCTION      : InitializeChart
        // DESCRIPTION   :
        //   this method will initialize the Pareto chart with appropriate settings,
        //   including titles, axes, series styles, and legends.
        // PARAMETERS    :
        //   None
        // RETURNS       :
        //   void : no return value
        //
        private void InitializeChart()
        {
            chartPareto.Series.Clear();
            chartPareto.ChartAreas.Clear();
            chartPareto.Titles.Clear();
            chartPareto.Legends.Clear();

            Title chartTitle = new Title("Pareto Chart - Reasons for Rejection (Rework & Scrap)");
            chartTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            chartPareto.Titles.Add(chartTitle);

            ChartArea chartArea = new ChartArea("ParetoArea");

            chartArea.AxisY.Title = "Count of YoYo";
            chartArea.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;

            chartArea.AxisY2.Title = "Cumulative Percentage (%)";
            chartArea.AxisY2.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chartArea.AxisY2.Minimum = 0;
            chartArea.AxisY2.Maximum = 100;
            chartArea.AxisY2.Interval = 20;
            chartArea.AxisY2.LabelStyle.Format = "0'%'";
            chartArea.AxisY2.MajorGrid.Enabled = false;
            chartArea.AxisY2.Enabled = AxisEnabled.True;

            chartArea.AxisX.Title = "Rejection Reasons";
            chartArea.AxisX.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.Angle = -45;

            chartPareto.ChartAreas.Add(chartArea);

            Series frequencySeries = new Series("Frequency");
            frequencySeries.ChartType = SeriesChartType.Column;
            frequencySeries.Color = Color.SteelBlue;
            frequencySeries.BorderColor = Color.DarkBlue;
            frequencySeries.BorderWidth = 1;
            frequencySeries.YAxisType = AxisType.Primary;
            frequencySeries.IsValueShownAsLabel = true;
            frequencySeries.LabelForeColor = Color.Black;
            frequencySeries.Font = new Font("Arial", 8);
            chartPareto.Series.Add(frequencySeries);

            Series cumulativeSeries = new Series("Cumulative %");
            cumulativeSeries.ChartType = SeriesChartType.Line;
            cumulativeSeries.Color = Color.OrangeRed;
            cumulativeSeries.BorderWidth = 3;
            cumulativeSeries.YAxisType = AxisType.Secondary;
            cumulativeSeries.MarkerStyle = MarkerStyle.Circle;
            cumulativeSeries.MarkerSize = 8;
            cumulativeSeries.MarkerColor = Color.Red;
            cumulativeSeries.IsValueShownAsLabel = true;
            cumulativeSeries.LabelFormat = "0.0'%'";
            cumulativeSeries.LabelForeColor = Color.DarkRed;
            cumulativeSeries.Font = new Font("Arial", 7);
            chartPareto.Series.Add(cumulativeSeries);

            Legend legend = new Legend("MainLegend");
            legend.Docking = Docking.Top;
            legend.Alignment = StringAlignment.Center;
            chartPareto.Legends.Add(legend);
        }

        //
        // FUNCTION      : UpdateParetoChart
        // DESCRIPTION   :
        //   it will updates the Pareto chart based on the provided data list.
        // PARAMETERS    :
        //   List<string> reasons
        //   List<double> counts
        // RETURNS       :
        //   void : No return value
        //
        private void UpdateParetoChart(List<string> reasons, List<double> counts)
        {
            chartPareto.Series["Frequency"].Points.Clear();
            chartPareto.Series["Cumulative %"].Points.Clear();

            if (reasons == null || counts == null || reasons.Count == 0 || counts.Count == 0)
                return;

            // sort it descending by count
            var sorted = reasons.Zip(counts, (r, c) => new { reason = r, count = c })
                                .OrderByDescending(x => x.count)
                                .ToList();

            double grandTotal = sorted.Sum(x => x.count);
            double cumulativeSum = 0;

            foreach (var item in sorted)
            {
                string category = item.reason;
                double value = item.count;
                chartPareto.Series["Frequency"].Points.AddXY(category, value);
                cumulativeSum += value;
                double cumulativePercent = (cumulativeSum / grandTotal) * 100;
                chartPareto.Series["Cumulative %"].Points.AddXY(category, cumulativePercent);
            }

            double maxValue = sorted.Max(x => x.count);
            chartPareto.ChartAreas["ParetoArea"].AxisY.Maximum = maxValue * 1.15;
            chartPareto.Invalidate();
        }

        //
        // FUNCTION      : GetCubeData
        // DESCRIPTION   :
        //   this will retrieves rejection reason data from the OLAP cube, filtered by selected product and line.
        // PARAMETERS    :
        //   out List<string> reasons
        //   out List<double> counts
        // RETURNS       :
        //   void : no return value
        //
        private void GetCubeData(out List<string> reasons, out List<double> counts)
        {
            reasons = new List<string>();
            counts = new List<double>();

            try
            {
                // check product selection:
                bool filterByProduct = true;
                if (lstProduct.SelectedIndex == 0 || lstProduct.SelectedIndex == -1)
                    filterByProduct = false;

                // check line selection
                bool filterByLine = true;
                if (lstLine.SelectedIndex == 0 || lstLine.SelectedIndex == -1)
                    filterByLine = false;

                // build product slicer string
                string productSlicer = "";
                if (filterByProduct)
                {
                    string selectedProduct = lstProduct.SelectedItem.ToString();
                    productSlicer = "{ [Yo Yo Product].[Description].&[" + selectedProduct + "] }";
                }

                // build line slicer string
                string lineSlicer = "";
                if (filterByLine)
                {
                    string selectedLine = lstLine.SelectedItem.ToString();
                    lineSlicer = "{ [Line Table].[Line Name].&[" + selectedLine + "] }";
                }

                // build the WHERE clause
                string whereClause = "";
                if (filterByProduct && filterByLine)
                    whereClause = "WHERE ( " + productSlicer + ", " + lineSlicer + " )";
                else if (filterByProduct)
                    whereClause = "WHERE ( " + productSlicer + " )";
                else if (filterByLine)
                    whereClause = "WHERE ( " + lineSlicer + " )";

                // run the full query
                string mdxQuery = @"SELECT [Measures].[Yo Yo Data Count] ON COLUMNS,
                                    [Reason Table].[Reason Name].[Reason Name].MEMBERS ON ROWS
                                    FROM [YoYoDB] " + whereClause;

                // execute query and read the results
                using (AdomdConnection connection = new AdomdConnection(connStr))
                {
                    connection.Open();

                    using (AdomdCommand command = new AdomdCommand(mdxQuery, connection))
                    {
                        AdomdDataAdapter adapter = new AdomdDataAdapter(command);
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);

                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            string reasonName = row[0]?.ToString();
                            double countValue = 0;

                            // skip rows where the reason name is blank or "Unknown"
                            if (string.IsNullOrWhiteSpace(reasonName) || reasonName.Equals("Unknown", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            if (row[1] != null && double.TryParse(row[1].ToString(), out countValue))
                            {
                                if (countValue > 0)
                                {
                                    reasons.Add(reasonName);
                                    counts.Add(countValue);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in loading data: " + ex.Message);
                reasons = new List<string>();
                counts = new List<double>();
            }
        }
    }
}
