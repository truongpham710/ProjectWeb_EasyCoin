using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace Easybook.Api.Core.CrossCutting.Utility
{
    public class CommonUtil
    {
        /// <summary>
        /// Convert number digits to words
        /// </summary>
        /// <param name="number">The number</param>
        /// <returns></returns>
        public static string ConvertDigitsToWords(int number)
        {
            if (number == 0) return "zero";

            if (number < 0) return "minus " + ConvertDigitsToWords(Math.Abs(number));

            var words = "";

            if ((number / 1000000) > 0)
            {
                words += ConvertDigitsToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertDigitsToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertDigitsToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "") words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        public static Dictionary<string, string> ConvertXmlStringToDictionary(string data)
        {
            var doc = XDocument.Parse("<doc>" + data + "</doc>");

            return doc.Descendants()
                .Where(p => p.HasElements == false)
                .ToDictionary(element => element.Name.LocalName, element => element.Value);
        }

        /// <summary>
        /// UPPERCASE the 1st letter of a string
        /// </summary>
        /// <param name="input">original string</param>
        /// <param name="capitalizeEachWord">capitalize each word (seperated by spaces). eg: [K]uala [L]umpur</param>
        /// <returns></returns>
        public static string Capitalize(string input, bool capitalizeEachWord = true)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(input)) return string.Empty;

            if (capitalizeEachWord)
            {
                StringBuilder sbResult = new StringBuilder();
                string[] words = input.Split(' ');
                foreach (string word in words)
                {
                    if (!string.IsNullOrEmpty(word.Trim()))
                    {
                        if (sbResult.Length > 0)
                            sbResult.Append(" ");
                        if (word.Trim().Length > 1)
                            sbResult.Append(char.ToUpper(word[0]) + word.Substring(1).ToLower());
                        else
                            sbResult.Append(char.ToUpper(word[0]));
                    }
                }
                return sbResult.ToString();
            }
            else
            {
                if (input.Trim().Length > 1)
                    return char.ToUpper(input[0]) + input.Substring(1).ToLower();
                return input.ToUpper();
            }
        }

        public static string GetAbsoluteUrl()
        {
          return string.Format("{0}://{1}{2}{3}",
                                HttpContext.Current.Request.Url.Scheme,
                                (string.IsNullOrEmpty(HttpContext.Current.Request.Url.Host) ? string.Empty : HttpContext.Current.Request.Url.Host),
                                (HttpContext.Current.Request.Url.Port == 80) ? String.Empty : ":" + HttpContext.Current.Request.Url.Port,
                                (string.IsNullOrEmpty(HttpContext.Current.Request.ApplicationPath.Replace("/", ""))) ? String.Empty : HttpContext.Current.Request.ApplicationPath);
        }

        /// <summary>
        /// Get appsetting value by key
        /// </summary>
        /// <param name="key">appsetting key</param>
        /// <param name="defaultValue">default value to be return if key invalid/not found</param>
        /// <returns></returns>
        public static string GetAppSettingValue(string key, string defaultValue = "")
        {
            if (string.IsNullOrEmpty(key)) return defaultValue;
            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
                {
                    return ConfigurationManager.AppSettings[key].ToString();
                }
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string GetCountryCodeTripKeyPrefix(string countryCode)
        {
            switch (countryCode)
            {
                case "SG":
                case "MY":
                    return "SG-";
                case "ID":
                    return "ID-";
                case "TH":
                    return "TH-";
                case "VN":
                    return "VN-";
                case "MM":
                    return "MM-";
                default:
                    return string.Empty;
            }
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #region Regular Expression Converter
        /// <summary>
        /// Flter out all other characters except alphanumeric and white space. (NOT CASE SENSITIVE)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToAlphaNumericAndWhiteSpaceOnly(string input)
        {
            return Regex.Replace(input, "[^a-zA-Z0-9 ]", "");
        }

        public static string ToAlphaNumericOnly(string input)
        {
            return Regex.Replace(input, "[^a-zA-Z0-9]", "");
        }

        public static string ToAlphaOnly(string input)
        {
            return Regex.Replace(input, "[^a-zA-Z]", "");
        }

        public static string ToNumericOnly(string input)
        {
            return Regex.Replace(input, "[^0-9]", "");
        }
        #endregion

        #region Save data to config
        public static void SaveDataToConfig(string tokenlist,string key)
        {
            // Open App.Config of executable
            System.Configuration.Configuration config =
             ConfigurationManager.OpenExeConfiguration
                        (ConfigurationUserLevel.None);

            // Add an Application Setting.
            config.AppSettings.Settings.Add(key,
                           tokenlist);

            // Save the changes in App.config file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion
    }
}
