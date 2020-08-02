using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace WindowsFormsApp18
{
    public class Results
    {

        //An array with the months names to use in results 3,5.
        private static String[] monthNames = (new System.Globalization.CultureInfo("en-US")).DateTimeFormat.MonthNames;
        //An array with the seasons names to use in results 6,7.
        private static String[] seasonNames = { "Winter", "Spring", "Summer", "Fall" };


        //ExecutionTimeResult
        public double ExecutionTime { get; private set; }
        //Result 1
        public double YearlyAvg { get; set; }
        public int ResultWeight { get; private set; }
        //Result 2
        public int BestYear { get; set; }
        public int BestYearAmount { get; set; }
        //Result 3
        public string BestMonth { get; set; }
        public int BestMonthIndex { get; set; }
        public int BestMonthYear { get; set; }
        public int BestMonthAmount { get; set; }
        //Result 4
        public int WorstYear { get; set; }
        public int WorstYearAmount { get; set; }
        //Result 5
        public string WorstMonth { get; set; }
        public int WorstMonthIndex { get; set; }
        public int WorstMonthYear { get; set; }
        public int WorstMonthAmount { get; set; }
        //Result 6
        public string BestSeason { get; set; }
        public int BestSeasonIndex { get; set; }
        public int BestSeasonYear { get; set; }
        public int BestSeasonAmount { get; set; }
        //Result 7
        public string WorstSeason { get; set; }
        public int WorstSeasonIndex { get; set; }
        public int WorstSeasonYear { get; set; }
        public int WorstSeasonAmount { get; set; }
        //Result 8

            
        public Results(DataTable pdt)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); //Start a timer for calculations.

            this.ResultWeight = pdt.Rows.Count; //How many lines table has, for usage in comparison for average.
            this.YearlyAvg = DataFunc.TotalYearlyAvg(pdt); //Calculate it here, this is not an array so one line is enough.

            int[] bestYearResultsArray = DataFunc.BestYear(pdt); //Calculate result array once for usage in the next lines.
            this.BestYear = bestYearResultsArray[0];
            this.BestYearAmount = bestYearResultsArray[1];

            int[] bestMonthResultsArray = DataFunc.BestMonthAndItsYear(pdt); //Calculate result array once for usage in the next lines.
            this.BestMonthIndex = bestMonthResultsArray[0];
            this.BestMonth = monthNames[BestMonthIndex];            
            this.BestMonthYear = bestMonthResultsArray[1];
            this.BestMonthAmount = bestMonthResultsArray[2];

            int[] worstYearResultsArray = DataFunc.WorstYear(pdt); //Calculate result array once for usage in the next lines. 
            this.WorstYear = DataFunc.WorstYear(pdt)[0];
            this.WorstYearAmount = DataFunc.WorstYear(pdt)[1];

            int[] worstMonthResultsArray = DataFunc.WorstMonthAndItsYear(pdt); //Calculate result array once for usage in the next lines.
            this.WorstMonthIndex = worstMonthResultsArray[0];
            this.WorstMonth = monthNames[WorstMonthIndex];
            this.WorstMonthYear = worstMonthResultsArray[1];
            this.WorstMonthAmount = worstMonthResultsArray[2];

            int[] bestSeasonResultArray = DataFunc.BestSeasonAndItsYear(pdt); //Calculate result array once for usage in the next lines.
            this.BestSeasonIndex = bestSeasonResultArray[0];
            this.BestSeason = seasonNames[BestSeasonIndex];
            this.BestSeasonYear = bestSeasonResultArray[1];
            this.BestSeasonAmount = bestSeasonResultArray[2];

            int[] worstSeasonResultArray = DataFunc.WorstSeasonAndItsYear(pdt); //Calculate result array once for usage in the next lines.
            this.WorstSeasonIndex = worstSeasonResultArray[0];
            this.WorstSeason = seasonNames[WorstSeasonIndex];            
            this.WorstSeasonYear = worstSeasonResultArray[1];
            this.WorstSeasonAmount = worstSeasonResultArray[2];

            watch.Stop(); //End timer for calculation
            var elapsedMs = watch.ElapsedMilliseconds;
            this.ExecutionTime = elapsedMs; //Save the elapsed time.
            

        }


        public Results CompareResults(Results comparison)
        {

        //Result 1
        this.YearlyAvg = (this.YearlyAvg*this.ResultWeight + comparison.YearlyAvg*comparison.ResultWeight / (this.ResultWeight+comparison.ResultWeight));

            ////Result 2
            //this.BestYear
            //this.BestYearAmount
            ////Result 3
            //this.BestMonth
            //this.BestMonthIndex
            //this.BestMonthYear
            //this.BestMonthAmount
            ////Result 4
            //this.WorstYear
            //this.WorstYearAmount
            ////Result 5
            //this.WorstMonth 
            //this.WorstMonthIndex
            //this.WorstMonthYear
            //this.WorstMonthAmount 
            ////Result 6
            //this.BestSeason
            //this.BestSeasonIndex
            //this.BestSeasonYear
            //this.BestSeasonAmount
            ////Result 7
            //this.WorstSeason 
            //this.WorstSeasonIndex
            //this.WorstSeasonYear
            //this.WorstSeasonAmount


            return this;
    }
    }
}
