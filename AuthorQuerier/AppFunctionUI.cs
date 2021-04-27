using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
namespace AuthorQuerier
{
    public class AppFunctionUi
    {
        private readonly ILogger<AppFunctionUi> _logg;
        /// <summary>
        /// Injects the Ilogger service for use in this class
        /// </summary>
        /// <param name="logg"></param>
        public AppFunctionUi(ILogger<AppFunctionUi> logg)
        {
            _logg = logg;
        }
        public async System.Threading.Tasks.Task HomePage()
        {
            bool running = true;
            do
            {
                Console.WriteLine("Hey there!\n\n" +
                "Welcome!!!\n\n" +
                "Enter 1 to find the most active authors by their comment count\n\n" +
                "Enter 2 to find the author with the highest comment count\n\n" +
                "Enter 3 to find  authors by date created\n\n" +
                "Enter 4 to find authors by the number of articles submitted\n\n" +
                "Enter 5 to quit the applicaion\n\n");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        try
                        {
                            int threshold = GetThreshold();
                            var listOfNames = await AppFunction.GetUserNames(threshold);
                            PrintFromList(listOfNames);
                        }
                        catch (Exception e) when (e is FormatException || e is InvalidOperationException)
                        {
                            Console.WriteLine(e.Message);
                            _logg.LogError(e.Message);
                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine("Request Error, Please try again later");
                            _logg.LogError(e.Message);
                        }
                        break;
                    case "2":
                        try
                        {
                            string authorsName = await AppFunction.GetUsernameWithHighestCommentCount();
                            Console.WriteLine(authorsName);
                        }
                        catch (Exception e) when (e is FormatException || e is InvalidOperationException)
                        {
                            Console.WriteLine(e.Message);
                            _logg.LogError(e.Message);
                        }
                        catch   (HttpRequestException e)
                        {
                            Console.WriteLine("Request Error, Please try again later");
                            _logg.LogError(e.Message);
                        }
                        break;
                    case "3":
                        try
                        {
                            int thresholdValue = GetThreshold();
                            var listOfAuthorNames = await AppFunction.GetUsernamesSortedByRecordDate(thresholdValue);
                            PrintFromList(listOfAuthorNames);
                        }
                        catch (Exception e) when (e is FormatException || e is InvalidOperationException)
                        {
                            Console.WriteLine(e.Message);
                            _logg.LogError(e.Message);
                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine("Request Error, Please try again later");
                            _logg.LogError(e.Message);
                        }
                        break;
                    case "4":
                        try
                        {
                            var ranges = GetRange();
                            var authorsInspecifiedRange = await AppFunction.GetUsernamesAccordingToRange(ranges);
                            PrintFromDict(authorsInspecifiedRange);
                        }
                        catch (Exception e) when (e is FormatException || e is InvalidOperationException)
                        {
                            Console.WriteLine(e.Message);
                            _logg.LogError(e.Message);
                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine("Request Error, Please try again later");
                            _logg.LogError(e.Message);
                        }
                        break;
                    case "5":
                        running = false;
                        break;
                }
            } while (running);
        }
        /// <summary>
        /// Prints a list of names to the console.
        /// </summary>
        /// <param name="listOfNames"></param>
        private static void PrintFromList(List<string> listOfNames)
        {
            int i = 1;
            foreach (var name in listOfNames)
            {
                Console.WriteLine($"{i++}.\t\t{name}\n\n");
            }
        }
        /// <summary>
        /// Prints the list of names from a dictionary.
        /// </summary>
        /// <param name="result"></param>
        private static void PrintFromDict(Dictionary<string, List<string>> result)
        {
            int i = 1;
            foreach (var entry in result)
            {
                foreach (var item in entry.Value)
                {
                    string values =  $" {i++}.\t\t{item}\n";
                    Console.WriteLine(values);
                }

            }
        }
        /// <summary>
        /// Gets an input value from the console. 
        /// </summary>
        /// <returns>Integer</returns>
        private static int GetThreshold()
        {
            bool isValid = false;
            do
            {
                Console.WriteLine("Enter a numeric threshold value ranging from 1:\n");
                string choice = Console.ReadLine();
                int threshold = Utilities.ParseInput(choice);
                isValid = true;
                return threshold;
            } while (isValid);
        }
        /// <summary>
        /// Gets a range of values from the console.
        /// </summary>
        /// <returns>A dictionary of the input values parsed as integers  </returns>
        private static Dictionary<int, int> GetRange()
        {
            bool isValid = false;
            do
            {
                Console.WriteLine("Enter a start range:\n");
                string startRange = Console.ReadLine();
                int range1 = Utilities.ParseInput(startRange);
                Console.WriteLine("Enter an end range:\n");
                string endRange = Console.ReadLine();
                int range2 = Utilities.ParseInput(endRange);
                var dict = new Dictionary<int, int>();
                dict.Add(range1, range2);
                isValid = true;
                return dict;
            } while (isValid);
        }
    }
}
