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

        /* TO DO : TRY/CATCH/EXCEPTION!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!OOOOOOOOOOOOOOOOOO  OOOOOOOOO*/
        /* IN CASE YOU MISSED THE LINE ABOVE */


        //Opens a dialog box to select Excel file 
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string excelFile = openFileDialog1.FileName;
            DataTable dt = LoadWorksheetInDataTable(excelFile);
            inputGrid.DataSource = dt;
        }

        //Returns the Excel file data as a DataTable
        private DataTable LoadWorksheetInDataTable(string fileName)
        {
            DataTable sheetData = new DataTable();
            string Sheet1;

            //This section is to get the Sheet name.
            using (OleDbConnection conn = this.returnConnection(fileName)) 
            {
                conn.Open();
                DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                Sheet1 = dtSchema.Rows[0].Field<string>("TABLE_NAME");
                conn.Close();
            }

            //This section is to get the sheet data.
            using (OleDbConnection conn = this.returnConnection(fileName)) 
            {
                conn.Open();
                // retrieve the data using data adapter
                OleDbDataAdapter sheetAdapter = new OleDbDataAdapter("select * from [" + Sheet1 + "]", conn);
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
    }
}
