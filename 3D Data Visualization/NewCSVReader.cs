using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


/// <summary>
/// Most codes come from here: https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/
/// This script parses a CSV file, converting its values into ints or floats if able, and returning a List<Dictionary<string, object>>.
/// This is used with many trajectory, location, data recording, path, spline, related scripts
/// 
/// This is a updated version.
/// 
/// User can directily use the path of the file. No need to put the csv file in the Resources folder.
/// 
/// To remove possbile empty lines, suggest use
/// using System.Linq;
/// and
///         bookMarks = bookMarks.Where(item => item != null).ToList();  
///         
/// in the script that use this NewCSVReader.cs 
/// </summary>


public class NewCSVReader : MonoBehaviour
{

    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; // Define delimiters, regular expression craziness
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r"; // Define line delimiters, regular experession craziness
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string path)
    {

        var list = new List<Dictionary<string, object>>(); //declare dictionary list

        string[] lines = File.ReadAllLines(path);

        //if (lines.Length <= 1) return list; //Check that there is more than one line

        var header = Regex.Split(lines[0], SPLIT_RE); //Split header (element 0)

        // Loops through lines
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE); //Split lines according to SPLIT_RE, store in var (usually string array)
            if (values.Length == 0 || values[0] == "") continue; // Skip to end of loop (continue) if value is 0 length OR first value is empty

            var entry = new Dictionary<string, object>(); // Creates dictionary object

            // Loops through every value
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j]; // Set local variable value
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", ""); // Trim characters
                object finalvalue = value; //set final value

                int n; // Create int, to hold value if int

                float f; // Create float, to hold value if float

                // If-else to attempt to parse value into int or float
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry); // Add Dictionary ("entry" variable) to list
        }
        return list; //Return list
    }




}
