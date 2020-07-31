using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        /// <summary>
        /// Concats file name with save location.
        /// </summary>
        /// <param name="fileName">Name of file to be used for storage.</param>
        /// <returns>Full file path.</returns>
        public static string FullFilePath(this string fileName)
        {
            return $"{ GlobalConfig.GetFilePath() }\\{ fileName }";
        }

        /// <summary>
        /// Loads target file. 
        /// </summary>
        /// <param name="file">Target file name.</param>
        /// <returns>Loaded target file in list form.</returns>
        public static List<string> LoadFile(this string file)
        {
            if (File.Exists(file) == false)
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        /// <summary>
        /// Converts loaded string list to a list of models. 
        /// </summary>
        /// <param name="lines">List of string</param>
        /// <returns>List of models.</returns>
        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel
                {
                    Id = int.Parse(cols[0]),
                    PlaceNumber = int.Parse(cols[1]),
                    PlaceName = cols[2],
                    PrizeAmount = decimal.Parse(cols[3]),
                    PrizePercentage = double.Parse(cols[4])
                };
                output.Add(p);
            }

            return output;
        }

        /// <summary>
        /// Converts list of modles to a list of strings and saves the result to a file.
        /// </summary>
        /// <param name="models">List of models to be converted to list of strings.</param>
        /// <param name="fileName">Target file name</param>
        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{ p.Id }, { p.PlaceNumber }, { p.PlaceName }, { p.PrizeAmount }, { p.PrizePercentage }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
    }
}
