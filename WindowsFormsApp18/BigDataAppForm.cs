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
    /*Main class using a Windows Form*/
    public partial class BigDataAppForm : Form
    {

        public BigDataAppForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Opens a dialog box to select Excel file 
        private void choose_csv_button_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "XLSX files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            openFileDialog1.ShowDialog();
        }
        
        //Extract the file when pressing OK.
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                string excelFile = openFileDialog1.FileName;
                DataTable dt = LoadWorksheetInDataTable(excelFile);
                processingData = dt;
                inputGrid.DataSource = dt;
                processingData = dt.Copy();
                this.view_File_path.Text = openFileDialog1.FileName;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                object[] obj = new object[] { null, null, null, "TABLE" };
                DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, obj);
                sheetName = dtSchema.Rows[0].Field<string>("TABLE_NAME");
                conn.Close();
            }

            //This section is to get the sheet data.
            using (OleDbConnection conn = this.returnConnection(fileName))
            {
                conn.Open();
                // Retrieve the data using data adapter
                string sqlCmd = "select * from [" + sheetName + "]";
                OleDbDataAdapter sheetAdapter = new OleDbDataAdapter(sqlCmd, conn);
                sheetAdapter.Fill(sheetData);
                conn.Close();
            }            
            return sheetData;
        }

        //Returns an object that represents a connection to MSOle component (Excel file).
        private OleDbConnection returnConnection(string fileName)
        {
            return new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=No;\"");
        }



        //The DataTable we'll work with 
        DataTable processingData; 

        //What happens when you press Map Reduce, number represents how many threads.
        private void map_reduce_button_Click(object sender, EventArgs e)
        {
            MapRecuce.MainMapReduceThread(1);
        }


        //What happens when you press Process.
        private void process_button_Click(object sender, EventArgs e)
        {
            resultsTextBox.Text = DataFunc.resultsText(processingData);
        }
    }
}
