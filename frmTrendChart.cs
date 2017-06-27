using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EventsTracker
{
    public partial class frmTrendChart : Form
    {
        private OleDbConnection mcnnAccessConnection;
        private Boolean mblnDoneOnce = false;
        private int mintFormWidth;
        private int mintFormHeight;
        private int mintDistanceToFirstLine;
        private Bitmap mobjFormBitmap;
        private Graphics mobjBitmapGraphics;
        private DataTable mobjEventsData;
        private DateTime mdteFirstLineDate;
        private int mintDistanceBetweenLines;
        public frmTrendChart()
        {
            InitializeComponent();
        }

        private void frmTrendChart_Load(object sender, EventArgs e)
        {
            mcnnAccessConnection = Program.objMainForm.cnnAccessConnection;
        }

        private void frmTrendChart_Activated(object sender, EventArgs e)
        {
            if (!mblnDoneOnce)
            {
                mblnDoneOnce = true;
                mintFormWidth = this.Width;
                mintFormHeight = this.Height;
                mobjFormBitmap = new Bitmap(mintFormWidth, mintFormHeight, this.CreateGraphics());
                mobjBitmapGraphics = Graphics.FromImage(mobjFormBitmap);
                mintDistanceToFirstLine = 55;
                mintDistanceBetweenLines = 5;
                LoadEventsData();

                if (mobjEventsData.Rows.Count > 0)
                {
                    mdteFirstLineDate = (DateTime)mobjEventsData.Rows[mobjEventsData.Rows.Count - 1]["EventTime"]; // last record
                    mdteFirstLineDate = mdteFirstLineDate.AddDays(-((mintFormHeight - mintDistanceToFirstLine) / mintDistanceBetweenLines) / 2); // go back a page worth of days
                    if (mdteFirstLineDate < (DateTime)mobjEventsData.Rows[0]["EventTime"])  // went past first record date
                        mdteFirstLineDate = (DateTime)mobjEventsData.Rows[0]["EventTime"]; // stop at first record date
                }
                else
                    mdteFirstLineDate = DateTime.Now;

                AdjustScrollBar();

                RefreshDisplay();
            }
        }

        private void AdjustScrollBar()
        {
            if (mobjEventsData.Rows.Count > 0)
            {
                TimeSpan spnAllDatesSpan;
                scrScroll.Minimum = 0;
                scrScroll.SmallChange = 5;
                scrScroll.LargeChange = ((mintFormHeight - mintDistanceToFirstLine) / mintDistanceBetweenLines) / 2;
                spnAllDatesSpan = (DateTime)mobjEventsData.Rows[mobjEventsData.Rows.Count - 1]["EventTime"] -
                    (DateTime)mobjEventsData.Rows[0]["EventTime"];
                scrScroll.Maximum = Convert.ToInt16(spnAllDatesSpan.TotalDays);
                
                if (mobjEventsData.Rows.Count > 0)   // initialize the scrollbar value to the current position
                    scrScroll.Value = Convert.ToInt16((mdteFirstLineDate - (DateTime)mobjEventsData.Rows[0]["EventTime"]).TotalDays);
            }
            else
            {
                scrScroll.Minimum = 0;
                scrScroll.SmallChange = 1;
                scrScroll.LargeChange = 10;
                scrScroll.Maximum = 0;
            }

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //Do nothing
        }

        private void frmTrendChart_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(mobjFormBitmap, 0, 0);
        }

        private void LoadEventsData()
        {
            OleDbDataReader objEventsReader;
            OleDbCommand objAccessCommand;
            string strSql;

            strSql = "Select * From Events order by EventTime";
            objAccessCommand = new OleDbCommand(strSql, mcnnAccessConnection);
            objAccessCommand.CommandType = CommandType.Text;
            objEventsReader = objAccessCommand.ExecuteReader(CommandBehavior.SingleResult);
            mobjEventsData = new DataTable();
            mobjEventsData.Load(objEventsReader);
            objEventsReader.Close();
        }

        private void RefreshDisplay()
        {
            int intX;
            int intY;
            int intXFor8am;
            int intWidthBetweenHours;
            Font objFont;
            DateTime dteCurrentLineDate;
            DateTime dteCurrentRecordDateTime;
            DateTime dteCurrentLineDate8am;
            TimeSpan spnSpanFrom8am;
            int intDataRowIndex;

            mobjBitmapGraphics.FillRectangle(Brushes.White, 0, 0, mintFormWidth, mintFormHeight);

            objFont = new Font("MS Sans Serif", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);

            // draw the time markers in the top of the form
            intXFor8am = mintFormWidth / 24;
            intWidthBetweenHours = mintFormWidth / 12;
            
            intY = 10;
            for (int intHour = 8; intHour <= 19; intHour++)
            {
                string strHour;
                intX = intXFor8am + (intWidthBetweenHours * (intHour - 8));
                if (intHour < 12)
                    strHour = intHour.ToString() + "AM";
                else
                    if (intHour > 12)
                        strHour = (intHour - 12).ToString() + "PM";
                    else
                        strHour = intHour.ToString() + "PM";

                mobjBitmapGraphics.DrawString(strHour, objFont, Brushes.Black, intX - 15, intY);

                mobjBitmapGraphics.DrawLine(Pens.Red, intX, 25, intX, 29);
            }

            // write the date for the first line only
            mobjBitmapGraphics.DrawString(string.Format("{0:ddd MMM dd, yyyy}", mdteFirstLineDate), objFont, Brushes.Black, 5, 40);

            dteCurrentLineDate = mdteFirstLineDate;  // initially
            dteCurrentLineDate8am = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", dteCurrentLineDate) + " 8am");
            intY = mintDistanceToFirstLine;
            intDataRowIndex = 0;

            // draw first day line
            if (dteCurrentLineDate.DayOfWeek == DayOfWeek.Monday)
                mobjBitmapGraphics.DrawLine(Pens.DarkGray, 0, intY, mintFormWidth, intY);
            else
                mobjBitmapGraphics.DrawLine(Pens.LightGray, 0, intY, mintFormWidth, intY);

            while (intDataRowIndex < mobjEventsData.Rows.Count & intY < mintFormHeight)
            {
                dteCurrentRecordDateTime = (DateTime)mobjEventsData.Rows[intDataRowIndex]["EventTime"];
                
                while (intY < mintFormHeight & Convert.ToDateTime( string.Format("{0:yyyy-MM-dd}", dteCurrentLineDate)) < 
                       Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", dteCurrentRecordDateTime)))
                {
                    dteCurrentLineDate = dteCurrentLineDate.AddDays(1);
                    intY += mintDistanceBetweenLines;
                    if (dteCurrentLineDate.DayOfWeek == DayOfWeek.Monday)
                        mobjBitmapGraphics.DrawLine(Pens.DarkGray, 0, intY, mintFormWidth, intY);
                    else
                        mobjBitmapGraphics.DrawLine(Pens.LightGray, 0, intY, mintFormWidth, intY);
                }

                while (intDataRowIndex < mobjEventsData.Rows.Count & Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", dteCurrentLineDate)) >
                       Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", dteCurrentRecordDateTime)))
                {
                    intDataRowIndex++;
                    dteCurrentRecordDateTime = (DateTime)mobjEventsData.Rows[intDataRowIndex]["EventTime"];
                }

                if (string.Format("{0:yyyyMMdd}", dteCurrentLineDate) == string.Format("{0:yyyyMMdd}", dteCurrentRecordDateTime))
                {
                    spnSpanFrom8am = dteCurrentRecordDateTime - dteCurrentLineDate8am;
                    intX = intXFor8am + (spnSpanFrom8am.Hours * intWidthBetweenHours) + ((spnSpanFrom8am.Minutes * intWidthBetweenHours) / 60);
                    switch ((string)mobjEventsData.Rows[intDataRowIndex]["EventCode"])
                    {
                        case "A":
                            mobjBitmapGraphics.FillEllipse(Brushes.Gray, intX - 2, intY - 3, 5, 5);
                            break;

                        case "C":
                            mobjBitmapGraphics.FillEllipse(Brushes.Black, intX - 2, intY - 3, 5, 5);
                            break;
                    }
                }
                
                intDataRowIndex++;
            }


            this.Invalidate();
        }

        private void scrScroll_Scroll(object sender, ScrollEventArgs e)
        {
            if (mobjEventsData.Rows.Count > 0)
                mdteFirstLineDate = ((DateTime)mobjEventsData.Rows[0]["EventTime"]).AddDays((double)scrScroll.Value);
            RefreshDisplay();
        }
    }
}
