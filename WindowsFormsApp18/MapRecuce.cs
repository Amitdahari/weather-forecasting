using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Threading;

namespace WindowsFormsApp18
{
    public static class MapRecuce
    {
        
        private static Thread[] thr;
        public static void MainMapReduceThread(DataTable dt,int threadsNumber)
        {
            Reduce(Map(dt, threadsNumber));
         //   Merge();
        }
        public static Thread[] Map(DataTable dt, int threads)
        {
            Results[] res = new Results[threads];
            thr = new Thread[threads];
            DataTable[] splitDT = MapRecuce.TableSplit(dt, threads);

                for (int i = 0; i < threads; i++)
                {
                   thr[i] = new Thread(() => res[i] = ProcessData(splitDT[i]));
                }

            return thr;

            
        }
        
        public static Results ProcessData(DataTable dt)
        {
            Results res = new Results(dt);

            return res;
        }
        public static void Reduce(Thread[] thr)
        {
            
            
            for (int i = 0; i < thr.Length; i++)
            {
               thr[i].Start();
               thr[i].Join();
            }
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
