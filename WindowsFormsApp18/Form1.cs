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

        /*We'll use this to process the Data.*/
        DataTable processingData;

        private void mainThread()
        {
            String[] somearray = { "A1", "A2", "A3", "A4", "A5", "A6" };
            DataTable placeholderDt = new DataTable();
            placeholderDt.Clear();
            placeholderDt.Columns.Add("Results:");
            //Result 1
            placeholderDt.Rows.Add("Yearly average:");
            placeholderDt.Rows.Add(TotalYearlyAvg(processingData).ToString());
            //Result 2
            placeholderDt.Rows.Add("Best year and its amount:");          
            placeholderDt.Rows.Add();
            //Result 3
            placeholderDt.Rows.Add("Best month,its amount and its year:");
            placeholderDt.Rows.Add();
            //Result 4
            placeholderDt.Rows.Add("Worst year and its amount:");
            placeholderDt.Rows.Add();
            //Result 5
            placeholderDt.Rows.Add("Worst month,its amount and its year:");
            placeholderDt.Rows.Add();
            //Result 6
            placeholderDt.Rows.Add("Best season, its amount and its year:");
            placeholderDt.Rows.Add();
            //Result 7
            placeholderDt.Rows.Add("Worst season, its amount and its year:");
            placeholderDt.Rows.Add();
            //Result 8
            placeholderDt.Rows.Add("Droughts:");
            placeholderDt.Rows.Add();


            outputGrid.DataSource = placeholderDt;
        }



        // Support function
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
        private double BestYear(DataTable dt)
        {
            double maxValue = YearlyTotal(dt).Max();
            double maxIndex = YearlyTotal(dt).ToList().IndexOf(maxValue);
            return maxIndex;
        }

        // Input 3
        private double BestMonthAndItsYear(DataTable dt)
        {
            return 1;
        }

        // Input 4
        private double WorstYear(DataTable dt)
        {
            double minValue = YearlyTotal(dt).Min();
            double minIndex = YearlyTotal(dt).ToList().IndexOf(minValue);
            return minIndex;
        }

        // Input 5
        private double WorstMonthAndItsYear(DataTable dt)
        {
            return 1;
        }

        // Input 6
        private double BestSeasonAndItsYear(DataTable dt)
        {
            return 1;
        }

        // Input 7
        private double WorstSeasonAndItsYear(DataTable dt)
        {
            return 1;
        }

        // Input 8
        private double WasThereA3YearsOfDrought(DataTable dt)
        {
            return 1;
        }
    }
}
