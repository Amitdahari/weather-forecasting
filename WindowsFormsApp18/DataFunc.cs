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
    /*This class will contain all of the methods we'll use for processing*/

    public static class DataFunc
    {

        // Support function for total of each year returns a list, ordered by years for 1,2,4 .
        public static double[] YearlyTotal(DataTable dt)
        {
            int totalyears = dt.Rows.Count;
            double[] yearlyTot = new double[totalyears];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int yearlySum = 0;
                for (int j = 1; j < dt.Columns.Count; j++)
                    yearlySum += Convert.ToInt32(dt.Rows[i].ItemArray[j]);
                yearlyTot[i] = yearlySum;
            }
            return yearlyTot;
        }


        // Output Result  1, total yearly average.
        public static double TotalYearlyAvg(DataTable dt)
        {
            return Queryable.Average(YearlyTotal(dt).AsQueryable());
        }

        // Output Result  2, best year.
        public static int[] BestYear(DataTable dt)
        {
            int maxValue = (int)YearlyTotal(dt).Max();
            int maxIndex = YearlyTotal(dt).ToList().IndexOf(maxValue);
            int bestYear = Convert.ToInt32(dt.Rows[maxIndex].ItemArray[0]);
            int[] result = { bestYear, maxValue };
            return result;
        }


        //Support function for 3,5, returns an array of month amounts sorted by years.
        public static double[] AmountsByMonth(DataTable dt, int month)
        {
            int totalmonths = dt.Rows.Count;
            double[] currentMonthArray = new double[totalmonths];
            for (int i = 0; i < totalmonths; i++)
                currentMonthArray[i] = Convert.ToInt32(dt.Rows[i].ItemArray[month]);

            return currentMonthArray;
        }

        // Output Result 3, best month and on what year.
        public static int[] BestMonthAndItsYear(DataTable dt)
        {
            int bestMonthIndex = 0;
            int bestMonthsYearIndex = 0;
            int maxValue = -1;

            for (int i = 1; i <= 12; i++)
            {
                if ((int)AmountsByMonth(dt, i).Max() > maxValue)
                {
                    maxValue = (int)AmountsByMonth(dt, i).Max();
                    bestMonthIndex = i - 1;
                    bestMonthsYearIndex = AmountsByMonth(dt, i).ToList().IndexOf(maxValue);
                }
            }

            int bestMonth = bestMonthIndex;
            int bestMonthsYear = Convert.ToInt32(dt.Rows[bestMonthsYearIndex].ItemArray[0]);
            int[] result = { bestMonth, bestMonthsYear, maxValue };
            return result;
        }

        // Output Result  4, worst year.
        public static int[] WorstYear(DataTable dt)
        {
            int minValue = (int)YearlyTotal(dt).Min();
            int minIndex = YearlyTotal(dt).ToList().IndexOf(minValue);
            int worstYear = Convert.ToInt32(dt.Rows[minIndex].ItemArray[0]);
            int[] result = { worstYear, minValue };
            return result;
        }

        // Output Result  5, worst month and on what year.
        public static int[] WorstMonthAndItsYear(DataTable dt)
        {
            int worstMonthIndex = 0;
            int worstMonthsYearIndex = 0;
            int minValue = 9999;

            for (int i = 1; i <= 12; i++)
            {
                if ((int)AmountsByMonth(dt, i).Min() < minValue)
                {
                    minValue = (int)AmountsByMonth(dt, i).Min();
                    worstMonthIndex = i - 1;
                    worstMonthsYearIndex = AmountsByMonth(dt, i).ToList().IndexOf(minValue);
                }
            }

            int bestMonth = worstMonthIndex;
            int bestMonthsYear = Convert.ToInt32(dt.Rows[worstMonthsYearIndex].ItemArray[0]);
            int[] result = { bestMonth, bestMonthsYear, minValue };
            return result;
        }

        //Support function for output 6,7
        public static double[] AmountsBySeason(DataTable dt, int season)
        {
            int[,] seasonMonths = { { 12, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 9, 10, 11 } };
            int totalseasons = dt.Rows.Count;
            double[] currentSeasonArray = new double[totalseasons];
            for (int i = 0; i < totalseasons; i++)
                for (int j = 0; j < 3; j++)
                    currentSeasonArray[i] += Convert.ToInt32(dt.Rows[i].ItemArray[seasonMonths[season, j]]);
            return currentSeasonArray;
        }


        // Output Result  6, best season and its year.
        public static int[] BestSeasonAndItsYear(DataTable dt)
        {
            int bestSeasonIndex = 0;
            int bestSeasonsYearIndex = 0;
            int maxValue = -1;

            for (int i = 0; i <= 3; i++)
            {
                if ((int)AmountsBySeason(dt, i).Max() > maxValue)
                {
                    maxValue = (int)AmountsBySeason(dt, i).Max();
                    bestSeasonIndex = i;
                    bestSeasonsYearIndex = AmountsBySeason(dt, i).ToList().IndexOf(maxValue);
                }
            }

            int bestSeason = bestSeasonIndex;
            int bestSeasonsYear = Convert.ToInt32(dt.Rows[bestSeasonsYearIndex].ItemArray[0]);
            int[] result = { bestSeason, bestSeasonsYear, maxValue };
            return result;
        }

        // Output Result  7, worst season and its year.
        public static int[] WorstSeasonAndItsYear(DataTable dt)
        {
            {
                int worstSeasonIndex = 0;
                int worstSeasonsYearIndex = 0;
                int minValue = 9999;

                for (int i = 0; i <= 3; i++)
                {
                    if ((int)AmountsBySeason(dt, i).Max() < minValue)
                    {
                        minValue = (int)AmountsBySeason(dt, i).Min();
                        worstSeasonIndex = i;
                        worstSeasonsYearIndex = AmountsBySeason(dt, i).ToList().IndexOf(minValue);
                    }
                }

                int bestSeason = worstSeasonIndex;
                int bestSeasonsYear = Convert.ToInt32(dt.Rows[worstSeasonsYearIndex].ItemArray[0]);
                int[] result = { bestSeason, bestSeasonsYear, minValue };
                return result;
            }
        }

        // Output Result  8, drought bonus.
        public static double WasThereA3YearsOfDrought(DataTable dt)
        {
            return 1;
        }



        /*We'll arrange the text.*/
        public static string ResultsText(Results res)
        {
            String resultText = "";
            resultText += "Results for table that were obtained in " + res.ExecutionTime.ToString() + " miliseconds:";
            //Result 1
            resultText += "\n\n1. Yearly average:";
            resultText += " " + res.YearlyAvg.ToString();
            //Result 2
            resultText += "\n\n2. Best year and its amount:";
            resultText += " The year " + res.BestYear.ToString();
            resultText += " with the amount of" + res.BestYearAmount.ToString();
            //Result 3
            resultText += "\n\n3. Best month, its amount and its year:";
            resultText += " The month " + res.BestMonth.ToString();
            resultText += " of the year " + res.BestMonthYear.ToString();
            resultText += " with the amount of" + res.BestMonthAmount.ToString();
            //Result 4
            resultText += "\n\n4. Worst year and its amount:";
            resultText += " The year " + res.WorstYear.ToString();
            resultText += " with the amount of " + res.WorstYearAmount.ToString();
            //Result 5
            resultText += "\n\n5. Worst month, its amount and its year:";
            resultText += " The month " + res.WorstMonth.ToString();
            resultText += " of the year " + res.WorstMonthYear.ToString();
            resultText += " with the amount of " + res.WorstMonthAmount.ToString();
            //Result 6
            resultText += "\n\n6. Best season, its amount and its year:";
            resultText += " The " + res.BestSeason.ToString() + " season ";
            resultText += " of the year " + res.BestSeasonYear.ToString();
            resultText += " with the amount of " + res.BestSeasonAmount.ToString();
            //Result 7
            resultText += "\n\n7. Worst season, its amount and its year:";
            resultText += " The " + res.WorstSeason.ToString() + " season ";
            resultText += " of the year " + res.WorstSeasonYear.ToString();
            resultText += " with the amount of " + res.WorstSeasonAmount.ToString();
            //Result 8
            resultText += "\n\n8. Droughts information: This is a bonus part, we'll see if we do it later on.\n\n\n";


            return resultText;
        }
    }
}
