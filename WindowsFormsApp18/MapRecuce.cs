using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.OleDb;


namespace WindowsFormsApp18
{
    public static class MapRecuce
    {
        public static void MainMapReduceThread(int threadsNumber)
        {
            
        }

        //Splits the DataTable into few DataTables inorder to process each table on each Thread
        public static DataTable[] TableSplit(DataTable dt, int divider) 
        {
            int chunknum = dt.Rows.Count/divider; // Number of divided chunkes from the table
            if(dt.Rows.Count % divider != 0) // In case we'll have 1 more table than threads, we divide it for less
            {
                chunknum -= 1; 
            }
            return dt.AsEnumerable()
            .Select((row, index) => new { row, index })
            .GroupBy(x => x.index / chunknum)  // integer division, the fractional part is truncated
            .Select(g => g.Select(x => x.row).CopyToDataTable())
            .ToArray();
        }

    }
}
