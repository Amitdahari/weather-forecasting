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

namespace WeatherForecastingOAZ
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
            this.process_button.Enabled = false;
            this.map_reduce_button.Enabled = false;
            this.get_result_button.Enabled = false;
        }

        //Opens a dialog box to select Excel file 
        private void choose_csv_button_Click(object sender, EventArgs e)
        {

            openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath); //Default folder=App folder.

            if (openFileDialog1.InitialDirectory.Substring(openFileDialog1.InitialDirectory.Length - 5) == "Debug") //If in Debug folder, change to project folder.
            {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())); //Default 
            }
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
                processingData = dt.Copy();//Take the table as our new DT for calculations.
                inputGrid.DataSource = dt; //Displaying DT on the DataGrid.
                threadsTextBox.Maximum = 16; //Normal PCs don't have more than 16 threads usually...
                this.view_File_path.Text = openFileDialog1.FileName;
                this.process_button.Enabled = true;
                this.map_reduce_button.Enabled = true;
                this.get_result_button.Enabled = true;

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
        //The number of threads for MapReduce
        int threads;


        //What happens when you press Map Reduce, number represents how many threads.
        private void map_reduce_button_Click(object sender, EventArgs e)
        {
            try
            {
                resultsTextBox.Text = ""; //Clear current text in text box.
                threads = (int)threadsTextBox.Value; //Number of threads.

                var usageBytes0 = GC.GetTotalMemory(true); //Memory usage after GC and before processing.
                var watch = System.Diagnostics.Stopwatch.StartNew(); //Start a timer for calculations.

                Results res = MapReduce.MainMapReduceThread(processingData, threads); //Activate MapReduce

                watch.Stop(); //End timer for calculation
                var usageBytes = GC.GetTotalMemory(false) - usageBytes0; //Memory usage after processing/before GC. Minus previous memory. 
                var elapsedMs = watch.ElapsedMilliseconds;  //Get timer value

                resultsTextBox.Text = DataFunc.ResultsText(res); //Push results into Textbox
                resultsTextBox.Text += "Overall run time: " + elapsedMs + " miliseconds. \n";
                resultsTextBox.Text += "Current memory usage: " + usageBytes/1024 + " KB";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //What happens when you press Process.
        private void process_button_Click(object sender, EventArgs e)
        {
            try
            {
                resultsTextBox.Text = ""; //Clear current text in text box.

                var usageBytes0 = GC.GetTotalMemory(true); //Memory usage after GC and before processing.
                var watch = System.Diagnostics.Stopwatch.StartNew(); //Start a timer for calculations.

                Results res = new Results(processingData); //Activate normal processing.

                watch.Stop(); //End timer for calculation
                var usageBytes = GC.GetTotalMemory(false) - usageBytes0; //Memory usage after processing/before GC. Minus previous memory. 
                var elapsedMs = watch.ElapsedMilliseconds; //Get timer value

                resultsTextBox.Text = DataFunc.ResultsText(res); //Push results into Textbox
                resultsTextBox.Text += "Overall run time: " + elapsedMs.ToString()+" miliseconds. \n";
                resultsTextBox.Text += "Current memory usage: " + usageBytes/1024 + " KB";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Saves the results from resultsTextBox into a Text file.
        private void get_result_button_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "Text files|*.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, resultsTextBox.Text);
                    MessageBox.Show("Message Box contents were saved!", "Notice", MessageBoxButtons.OK);
                    saveFileDialog1.FileName = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
