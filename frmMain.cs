using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace EventsTracker
{
    public partial class frmMain : Form
    {
        private OleDbConnection mcnnAccessConnection;

        public frmMain()
        {
            InitializeComponent();
        }

        public OleDbConnection cnnAccessConnection
        {
            get
            {
                return mcnnAccessConnection;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            mcnnAccessConnection = new OleDbConnection();
            mcnnAccessConnection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Path.GetDirectoryName(Application.ExecutablePath) + "\\EventsTracker.mdb;";
            mcnnAccessConnection.Open();
        }

        private void txtEventCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == "\r"[0])
            {
                string strEventCode;
                string strSql;
                OleDbCommand adoAccessCommand;

                strEventCode = txtEventCode.Text.Trim().ToUpper();
                switch (strEventCode)
                {
                    case "A":
                    case "C":
                        strSql = "Insert into Events(EventTime, EventCode) values (Now(), '" + strEventCode + "')";
                        adoAccessCommand = new OleDbCommand(strSql, mcnnAccessConnection);
                        adoAccessCommand.CommandType = CommandType.Text;
                        adoAccessCommand.ExecuteNonQuery();

                        this.Close();
                        break;

                    default:
                        MessageBox.Show("Unknown code", "Enter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void btnViewTrend_Click(object sender, EventArgs e)
        {
            frmTrendChart objTrendChartForm;

            objTrendChartForm = new frmTrendChart();
            objTrendChartForm.ShowDialog();
        }

    }
}
