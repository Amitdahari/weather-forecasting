using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string csvFile = openFileDialog1.FileName;
            List<string[]> rows = File.ReadAllLines(csvFile).Select(x => x.Split(',')).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Year");
            for (int i = 1; i <= 12; i++)
                dt.Columns.Add("Month " + i);
            rows.ForEach(x => { dt.Rows.Add(x); });
            inputGrid.DataSource = dt;
        }
    }
}
