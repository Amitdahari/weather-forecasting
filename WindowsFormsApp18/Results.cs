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
        public int Weight { get; private set; }
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

        //Construct - all calculations are done here using methods from DataFunc.
        public Results(DataTable pdt)
        {
            this.Weight = pdt.Rows.Count; //How many lines table has, for usage in comparison for average (weight of result).
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
        }

        //This function compares takes the best values of both Results puts them in the comparison.
        public void CompareResults(Results comparison)
        {
            //Compare 1 - Yearly Avarage
            comparison.YearlyAvg = (this.YearlyAvg * this.Weight + comparison.YearlyAvg * comparison.Weight) / (this.Weight + comparison.Weight);
            comparison.Weight += this.Weight;

            //Compare 2 - Best year
            if (this.BestYearAmount >= comparison.BestYearAmount)
            {  
                comparison.BestYear = this.BestYear;
                comparison.BestYearAmount = this.BestYearAmount;
            }

            //Compare 3 - Best Month
            if (this.BestMonthAmount >= comparison.BestMonthAmount)
            {
                comparison.BestMonth = this.BestMonth;
                comparison.BestMonthIndex = this.BestMonthIndex;
                comparison.BestMonthYear = this.BestMonthYear;
                comparison.BestMonthAmount = this.BestMonthAmount;
            }

            //Compare 4 - Worst Year
            if (this.WorstYearAmount <= comparison.WorstYearAmount)
            {
                comparison.WorstYear = this.WorstYear;
                comparison.WorstYearAmount = this.WorstYearAmount;
            }

            //Compare 5 - Worst Month
            if (this.WorstMonthAmount <= comparison.WorstMonthAmount)
            {
                comparison.WorstMonth = this.WorstMonth;
                comparison.WorstMonthIndex = this.WorstMonthIndex;
                comparison.WorstMonthYear = this.WorstMonthYear;
                comparison.WorstMonthAmount = this.WorstMonthAmount;
            }

            //Compare 6 - Best Season
            if (this.BestSeasonAmount >= comparison.BestSeasonAmount)
            {
                comparison.BestSeason = this.BestSeason;
                comparison.BestSeasonIndex = this.BestSeasonIndex;
                comparison.BestSeasonYear = this.BestSeasonYear;
                comparison.BestSeasonAmount = this.BestSeasonAmount;
            }


            //Compare 7 - Worst Season
            if (this.WorstSeasonAmount <= comparison.WorstSeasonAmount)
            {
                comparison.WorstSeason = this.WorstSeason;
                comparison.WorstSeasonIndex = this.WorstSeasonIndex;
                comparison.WorstSeasonYear = this.WorstSeasonYear;
                comparison.WorstSeasonAmount = this.WorstSeasonAmount;

            }

        }
    }
}
