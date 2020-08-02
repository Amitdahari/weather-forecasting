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
    public static class MapReduce
    {

        private static Thread[] thr; //The existing "computers" as threads.
        private static Results[] finalRes; //What the results will give us.

        //Calls Map, Reduce and returns the Merge result.
        public static Results MainMapReduceThread(DataTable dt, int threadsNumber)
        {

            Map(dt, threadsNumber);
            Reduce(thr);
            return Merge();
        }

        //Maps the split data into "computers" - our threads. Each thread has one table.
        public static void Map(DataTable dt, int threads)
        {

            finalRes = new Results[threads];
            thr = new Thread[threads];
            DataTable[] splitDT = MapReduce.TableSplit(dt, threads);

            if (splitDT.Length==threads+1) //**In a case where taking a bigger chunk isn't worth is (small table) merge the 2 last tables.
            {
                splitDT[threads - 1].Merge(splitDT[threads]);
            }

            for (int i = 0; i < threads; i++)
            {
                int temp = i; //Using Lambda function and return values in threads forces this temp variable.
                thr[i] = new Thread(() => finalRes[temp] = ProcessData(splitDT[temp])); //Whatever the thread returns, will return to finalRes.
            }
            
        }

        //Creating a new result by calculating a split table.
        public static Results ProcessData(DataTable dt)
        {
            Results res = new Results(dt);
            return res;
        }

        //Starts running the threads. "The computers" activate their processing functions.
        public static void Reduce(Thread[] thr)
        {

            for (int i = 0; i < thr.Length; i++)
            {
                thr[i].Start();               
            }
        }

        //Merge the result
        public static Results Merge() 
        {

            for (int i = 0; i < finalRes.Length - 1; i++)
            {
                thr[i].Join();      //If the current thread didn't finish running, wait for it.
                thr[i].Abort();     //Can't have these threads lingering in the background, they make inconsistent timing.
                thr[i + 1].Join();  //Also wait for the next thread.

                finalRes[i].CompareResults(finalRes[i + 1]); //Actual comparison.
            }

            return finalRes[finalRes.Length - 1]; //Return the final and most updated result.
        }

        //Splits the DataTable into few DataTables inorder to process each table on each Thread.
        public static DataTable[] TableSplit(DataTable dt, int divider)
        {
            int chunkSize = dt.Rows.Count / divider; // Number of divided chunkes from the table


            if (dt.Rows.Count % divider != 0) // In case we'll have 1 more table than threads, we'll take a bigger chunk size.
            {
                if(dt.Rows.Count % divider  >  (chunkSize = dt.Rows.Count / divider)/8) //**Condition where bigger chunk size will be better.
                chunkSize += 1;  //Where it's not worth it, solution is in map.
            }


            return dt.AsEnumerable()
            .Select((row, index) => new { row, index })
            .GroupBy(x => x.index / chunkSize)  // Integer division, the fractional part is truncated
            .Select(g => g.Select(x => x.row).CopyToDataTable())
            .ToArray();
        }




    }
}
