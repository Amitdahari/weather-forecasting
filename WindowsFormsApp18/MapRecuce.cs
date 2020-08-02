using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Threading;
using System.Diagnostics;

namespace WindowsFormsApp18
{
    public static class MapRecuce
    {
        
        private static Thread[] thr; //The existing "computers" as threads.
        private static Results[] finalRes; //What the results will give us.

        public static Results MainMapReduceThread(DataTable dt,int threadsNumber)
        {

            Map(dt, threadsNumber);
            Reduce(thr);
            return Merge();
        }

        public static void Map(DataTable dt, int threads)
        {

            finalRes = new Results[threads];
            thr = new Thread[threads];
            DataTable[] splitDT = MapRecuce.TableSplit(dt, threads);


            for (int i = 0; i < threads; i++)
            {
                int temp = i;
                thr[i] = new Thread(() => finalRes[temp] = ProcessData(splitDT[temp]));                
            }

            Debug.WriteLine("\n Results length" + finalRes.Length.ToString());
            Debug.WriteLine("\n Tables length" + splitDT.Length.ToString());
            Debug.WriteLine("\n Threads length" + thr.Length.ToString());

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


        public static Results Merge()
        {
            int lastIndex=0;
            for (int i = 0; i <= finalRes.Length-1; i++)
            {
                finalRes[i]=finalRes[i].CompareResults(finalRes[i + 1]);
                lastIndex = i;
            }

            return finalRes[lastIndex];
        }

        //Splits the DataTable into few DataTables inorder to process each table on each Thread
        public static DataTable[] TableSplit(DataTable dt, int divider) 
        {

            int chunkSize = dt.Rows.Count / divider; // Number of divided chunkes from the table

            if (dt.Rows.Count % divider != 0) // In case we'll have 1 more table than threads, we'll take a bigger chunk size.
            {

                chunkSize += dt.Rows.Count % divider; //This we we won't have an extra table.
            }

            return dt.AsEnumerable()
            .Select((row, index) => new { row, index })
            .GroupBy(x => x.index / chunkSize)  // Integer division, the fractional part is truncated
            .Select(g => g.Select(x => x.row).CopyToDataTable())
            .ToArray();
        }




    }
}
