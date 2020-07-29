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

        /*private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string csvFile = openFileDialog1.FileName;
            List<string[]> rows = File.ReadAllLines(csvFile).Select(x => x.Split(',')).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Year");
            for (int i = 1; i <= 12; i++)
                dt.Columns.Add("Month " + i);
            rows.ForEach(x => { dt.Rows.Add(x); });
            inputGrid.DataSource = dt;
        }*/

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string excelFile = openFileDialog1.FileName;
            DataTable dt = LoadWorksheetInDataTable(excelFile, "Sheet1");
            inputGrid.DataSource = dt;
        }

        private DataTable LoadWorksheetInDataTable(string fileName, string sheetName)
        {
            DataTable sheetData = new DataTable();
            using (OleDbConnection conn = this.returnConnection(fileName))
            {
                conn.Open();
                // retrieve the data using data adapter
                OleDbDataAdapter sheetAdapter = new OleDbDataAdapter("select * from [" + sheetName + "$]", conn);
                sheetAdapter.Fill(sheetData);
                conn.Close();
            }
            return sheetData;
        }

        private OleDbConnection returnConnection(string fileName)
        {
            return new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=No;\"");
        }
    }
}
