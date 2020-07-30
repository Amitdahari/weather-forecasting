using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

/*Important: In case of an 12.0 error, download Microsoft Access Database Engine Redistributable 2010.*/

namespace WindowsFormsApp18
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void choose_csv_button_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "XLSX files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            openFileDialog1.ShowDialog();
        }


        //Opens a dialog box to select Excel file 
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                string excelFile = openFileDialog1.FileName;
                DataTable dt = LoadWorksheetInDataTable(excelFile);
                inputGrid.DataSource = dt;
                this.view_File_path.Text = openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void process_button_Click(object sender, EventArgs e)
        {
            mainThread();
        }


        //Returns the Excel file data as a DataTable
        private DataTable LoadWorksheetInDataTable(string fileName)
        {
            DataTable sheetData = new DataTable();
            string sheetName;

            //This section is to get the Sheet name.
            using (OleDbConnection conn = this.returnConnection(fileName))
            {
                conn.Open();
                DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                sheetName = dtSchema.Rows[0].Field<string>("TABLE_NAME");
                conn.Close();
            }

            //This section is to get the sheet data.
            using (OleDbConnection conn = this.returnConnection(fileName))
            {
                conn.Open();
                // retrieve the data using data adapter
                OleDbDataAdapter sheetAdapter = new OleDbDataAdapter("select * from [" + sheetName + "]", conn);
                sheetAdapter.Fill(sheetData);
                conn.Close();
            }
            processingData = sheetData;
            return sheetData;
        }

        //Returns an object that represents a connection to MSOle component (Excel file).
        private OleDbConnection returnConnection(string fileName)
        {
            return new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=No;\"");
        }


        DataTable processingData;
        private void mainThread()
        {
            DataTable placeholderDt = new DataTable();
            placeholderDt.Clear();
            placeholderDt.Columns.Add("Results:");
            //Result 1
            placeholderDt.Rows.Add("Yearly Average");
            placeholderDt.Rows.Add(TotalYearlyAvg(processingData).ToString());
            //Result 2
            placeholderDt.Rows.Add("Yearly Average");
            placeholderDt.Rows.Add(TotalYearlyAvg(processingData).ToString());
            //Result 3
            placeholderDt.Rows.Add("Yearly Average");
            placeholderDt.Rows.Add(TotalYearlyAvg(processingData).ToString());
            //Result 4
            placeholderDt.Rows.Add("Yearly Average");
            placeholderDt.Rows.Add(TotalYearlyAvg(processingData).ToString());
            //Result 5
            placeholderDt.Rows.Add("Yearly Average");
            placeholderDt.Rows.Add(TotalYearlyAvg(processingData).ToString());
            //Result 6
            placeholderDt.Rows.Add("Yearly Average");
            placeholderDt.Rows.Add(TotalYearlyAvg(processingData).ToString());
            //Result 7
            placeholderDt.Rows.Add("Yearly Average");
            placeholderDt.Rows.Add(TotalYearlyAvg(processingData).ToString());
            //Result 8
            outputGrid.DataSource = placeholderDt;
        }


        private double[] YearlyTotal(DataTable dt)
        {

            int totalyears = processingData.Rows.Count;
            double[] yearlyTot = new double[totalyears];
            for (int i = 0; i < processingData.Rows.Count; i++)
            {
                int yearlySum = 0;
                for (int j = 1; j < processingData.Columns.Count; j++)
                    yearlySum += Convert.ToInt32(processingData.Rows[i].ItemArray[j]);
                yearlyTot[i] = yearlySum;
            }
            return yearlyTot;
        }

        // Input 1.
        private double TotalYearlyAvg(DataTable dt)
        {
            return Queryable.Average(YearlyTotal(dt).AsQueryable());
        }

        // Input 2.
        private double BestYearIndex(DataTable dt)
        {
            double maxValue = YearlyTotal(dt).Max();
            double maxIndex = YearlyTotal(dt).ToList().IndexOf(maxValue);
            return maxIndex;
        }
        // Input 4
        private double WorstYearIndex(DataTable dt)
        {
            double minValue = YearlyTotal(dt).Min();
            double minIndex = YearlyTotal(dt).ToList().IndexOf(minValue);
            return minIndex;
        }
    }
}
