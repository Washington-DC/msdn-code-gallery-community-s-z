using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;

namespace System.Web.UI.DataVisualization.Charting.Samples
{
	/// <summary>
	/// Summary description for MultipleYAxis.
	/// </summary>
	public partial class MultipleYAxis : System.Web.UI.Page
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Populate series with random data of different scale
			Random		random = new Random();
			DateTime	date = DateTime.Now.Date;
			for(int pointIndex = 0; pointIndex < 13; pointIndex++)
			{
				Chart1.Series["Series1"].Points.AddXY(date, random.Next(5,100));
				Chart1.Series["Series2"].Points.AddXY(date, random.Next(8000, 8200));
				Chart1.Series["Series3"].Points.AddXY(date, random.Next(1000,3000));
				date = date.AddDays(1);
			}

			if(UseMultipleYAxis.Checked)
			{
				// Set custom chart area position
				Chart1.ChartAreas["ChartArea1"].Position = new ElementPosition(25,10,68,85);
				Chart1.ChartAreas["ChartArea1"].InnerPlotPosition = new ElementPosition(10,0,90,90);

				// Create extra Y axis for second and third series
				CreateYAxis(Chart1, Chart1.ChartAreas["ChartArea1"], Chart1.Series["Series2"], 13, 8);
				CreateYAxis(Chart1, Chart1.ChartAreas["ChartArea1"], Chart1.Series["Series3"], 22, 8);
			}

		}

		/// <summary>
		/// Creates Y axis for the specified series.
		/// </summary>
		/// <param name="chart">Chart control.</param>
		/// <param name="area">Original chart area.</param>
		/// <param name="series">Series.</param>
		/// <param name="axisOffset">New Y axis offset in relative coordinates.</param>
		/// <param name="labelsSize">Extar space for new Y axis labels in relative coordinates.</param>
		public void CreateYAxis(Chart chart, ChartArea area, Series series, float axisOffset, float labelsSize)
		{
			// Create new chart area for original series
			ChartArea areaSeries = chart.ChartAreas.Add("ChartArea_" + series.Name);
			areaSeries.BackColor = Color.Transparent;
			areaSeries.BorderColor = Color.Transparent;
			areaSeries.Position.FromRectangleF(area.Position.ToRectangleF());
			areaSeries.InnerPlotPosition.FromRectangleF(area.InnerPlotPosition.ToRectangleF());
			areaSeries.AxisX.MajorGrid.Enabled = false;
			areaSeries.AxisX.MajorTickMark.Enabled = false;
			areaSeries.AxisX.LabelStyle.Enabled = false;
			areaSeries.AxisY.MajorGrid.Enabled = false;
			areaSeries.AxisY.MajorTickMark.Enabled = false;
			areaSeries.AxisY.LabelStyle.Enabled = false;
			areaSeries.AxisY.IsStartedFromZero = area.AxisY.IsStartedFromZero;


			series.ChartArea = areaSeries.Name;

			// Create new chart area for axis
			ChartArea areaAxis = chart.ChartAreas.Add("AxisY_" + series.ChartArea);
			areaAxis.BackColor = Color.Transparent;
			areaAxis.BorderColor = Color.Transparent;
			areaAxis.Position.FromRectangleF(chart.ChartAreas[series.ChartArea].Position.ToRectangleF());
			areaAxis.InnerPlotPosition.FromRectangleF(chart.ChartAreas[series.ChartArea].InnerPlotPosition.ToRectangleF());

			// Create a copy of specified series
			Series seriesCopy = chart.Series.Add(series.Name + "_Copy");
			seriesCopy.ChartType = series.ChartType;
			foreach(DataPoint point in series.Points)
			{
				seriesCopy.Points.AddXY(point.XValue, point.YValues[0]);
			}

			// Hide copied series
			seriesCopy.IsVisibleInLegend = false;
			seriesCopy.Color = Color.Transparent;
			seriesCopy.BorderColor = Color.Transparent;
			seriesCopy.ChartArea = areaAxis.Name;

			// Disable drid lines & tickmarks
			areaAxis.AxisX.LineWidth = 0;
			areaAxis.AxisX.MajorGrid.Enabled = false;
			areaAxis.AxisX.MajorTickMark.Enabled = false;
			areaAxis.AxisX.LabelStyle.Enabled = false;
			areaAxis.AxisY.MajorGrid.Enabled = false;
			areaAxis.AxisY.IsStartedFromZero = area.AxisY.IsStartedFromZero;
			areaAxis.AxisY.LabelStyle.Font = area.AxisY.LabelStyle.Font;

			// Adjust area position
			areaAxis.Position.X -= axisOffset;
			areaAxis.InnerPlotPosition.X += labelsSize;

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

	}	
}
