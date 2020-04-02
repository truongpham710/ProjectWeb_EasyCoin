using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace Easybook.Api.Core.CrossCutting.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The box width
        /// </summary>
        const int BoxWidth = 70;

        /// <summary>
        /// Truncates to maximum length of string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns></returns>
        public static string TruncateToMaxLength(this string input, int maxLength)
        {
            return input.Length > maxLength ? input.Substring(0, maxLength) : input;
        }

        /// <summary>
        /// Splits and join.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static string SplitAndSkip(this string str, char delimiter, int count)
        {
            return string.Join(delimiter.ToString(), str.Split(delimiter).Skip(count));
        }

        /// <summary>
        /// Formats with.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static string FormatWith(this string str, params object[] args)
        {
            return String.Format(str, args);
        }

        /// <summary>
        /// Decodes the HTML.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string DecodeHtml(this string str)
        {
            return str.Replace("</br>", Environment.NewLine).Replace("<br/>", Environment.NewLine);
        }

        /// <summary>
        /// Encodes the HTML.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string EncodeHtml(this string str)
        {
            return str.Replace(Environment.NewLine, "<br/>");
        }

        /// <summary>
        /// Ases the key value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string AsKeyValue(this object obj)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(BoxStartEnd(BoxWidth));
            stringBuilder.AppendLine(obj.GetType().Name.Length > 40 ? obj.GetType().Name.Substring(0, 40).ToBoxLineMiddle(BoxWidth) : obj.GetType().Name.ToBoxLineMiddle(BoxWidth));
            //BuildKeyValueString(obj, ref stringBuilder);
            stringBuilder.AppendLine(BoxStartEnd(BoxWidth));

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static byte[] GetBytes(this string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Replaces the custom HTML key with value.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        public static string ReplaceCustomHtmlKeyWithValue(this string str, Dictionary<string, string> keyValues)
        {
            return keyValues.Aggregate(str, (current, keyValue) => current.Replace("<%" + keyValue.Key + "%>", keyValue.Value));
        }

        /// <summary>
        /// Replaces the custom HTML key with value.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        public static string ReplaceCustomMetaKeyWithValue(this string str, Dictionary<string, string> keyValues)
        {
            return keyValues.Aggregate(str, (current, keyValue) => current.Replace("{" + keyValue.Key + "}", keyValue.Value));
        }

        /// <summary>
        /// Replaces the key with value.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        public static string ReplaceKeyWithValue(this string str, Dictionary<string, string> keyValues)
        {
            return keyValues.Aggregate(str, (current, keyValue) => current.Replace(keyValue.Key, keyValue.Value));
        }

        /*****************************************************************
         * ------------------------PRIVATE METHODS---------------------- *
         ****************************************************************/

        /// <summary>
        /// Builds the key value string.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="stringBuilder">The string builder.</param>
        //private static void BuildKeyValueString(object obj, ref StringBuilder stringBuilder)
        //{
        //    foreach (var propertyInfo in obj.GetType().GetProperties())
        //    {
        //        if (GenericClassifier.IsICollection(obj.GetType()))
        //        {
        //            var listObj = obj as ICollection;
        //            if (propertyInfo.Name != "Item" && propertyInfo.Name != "Local")
        //            {
        //                continue;
        //            }

        //            stringBuilder.AppendLine("{0} ICollection".FormatWith(propertyInfo.PropertyType.Name).ToBoxLineMiddle(BoxWidth));
        //            if (listObj != null)
        //            {
        //                foreach (var childObj in listObj)
        //                {
        //                    BuildKeyValueString(childObj, ref stringBuilder);
        //                    stringBuilder.AppendLine(BoxStartEnd(BoxWidth));
        //                }
        //            }
        //            stringBuilder.AppendLine("End ICollection".FormatWith(propertyInfo.PropertyType.Name).ToBoxLineMiddle(BoxWidth));
        //        }
        //        else if (GenericClassifier.IsIEnumerable(obj.GetType()))
        //        {
        //            var listObj = obj as IEnumerable;
        //            if (propertyInfo.Name != "Item" && propertyInfo.Name != "Local")
        //            {
        //                continue;
        //            }

        //            stringBuilder.AppendLine("{0} IEnumerable".FormatWith(propertyInfo.PropertyType.Name).ToBoxLineMiddle(BoxWidth));
        //            if (listObj != null)
        //            {
        //                foreach (var childObj in listObj)
        //                {
        //                    BuildKeyValueString(childObj, ref stringBuilder);
        //                    stringBuilder.AppendLine(BoxStartEnd(BoxWidth));
        //                }
        //            }
        //            stringBuilder.AppendLine("End IEnumerable".FormatWith(propertyInfo.PropertyType.Name).ToBoxLineMiddle(BoxWidth));
        //        }
        //        else if (GenericClassifier.IsIList(obj.GetType()))
        //        {
        //            var listObj = obj as IList;
        //            if (propertyInfo.Name != "Item" && propertyInfo.Name != "Local")
        //            {
        //                continue;
        //            }

        //            stringBuilder.AppendLine("{0} IList".FormatWith(propertyInfo.PropertyType.Name).ToBoxLineMiddle(BoxWidth));
        //            if (listObj != null)
        //            {
        //                foreach (var childObj in listObj)
        //                {
        //                    BuildKeyValueString(childObj, ref stringBuilder);
        //                    stringBuilder.AppendLine(BoxStartEnd(BoxWidth));
        //                }
        //            }
        //            stringBuilder.AppendLine("End IList".FormatWith(propertyInfo.PropertyType.Name).ToBoxLineMiddle(BoxWidth));
        //        }
        //        else
        //        {
        //            var value = propertyInfo.GetValue(obj);
        //            if (value == null)
        //            {
        //                stringBuilder.AppendLine("{0} : {1}".FormatWith(propertyInfo.Name.PadRight(BoxWidth / 2 - 4), "NULL".PadLeft(BoxWidth / 2 - 3)).ToBoxLine(BoxWidth));
        //            }
        //            else if (obj.GetType().IsValueType
        //                || obj.GetType().IsPrimitive
        //                || value is String
        //                || value.GetType().IsPrimitive
        //                || value is DateTime
        //                || value is Decimal)
        //            {
        //                if (GenericClassifier.IsICollection(obj.GetType())
        //                    || GenericClassifier.IsIEnumerable(obj.GetType()))
        //                {
        //                    continue;
        //                }

        //                var stringValue = propertyInfo.GetValue(obj).ToString().Length > 30 ? propertyInfo.GetValue(obj).ToString().Substring(0, 30) : propertyInfo.GetValue(obj).ToString();
        //                stringBuilder.AppendLine("{0} : {1}".FormatWith(propertyInfo.Name.PadRight(BoxWidth / 2 - 4), stringValue.PadLeft(BoxWidth / 2 - 3)).ToBoxLine(BoxWidth));
        //            }
        //            else
        //            {
        //                BuildKeyValueString(value, ref stringBuilder);
        //            }
        //        }
        //    }

        //}

        /// <summary>
        /// To the box line.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="totalWidth">The total width.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private static string ToBoxLine(this string str, int totalWidth, params object[] args)
        {
            var s = String.Format(str, args);
            return "+ " + s.PadRight(totalWidth - 4) + " +";
        }

        /// <summary>
        /// To the box line middle.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="totalWidth">The total width.</param>
        /// <returns></returns>
        private static string ToBoxLineMiddle(this string str, int totalWidth)
        {

            return String.Format("+{0}+{1}++++++++++ {2} ++++++++++{1}+{0}+", new string('-', totalWidth - 2), Environment.NewLine, str.PadBoth(totalWidth - 22));
        }

        /// <summary>
        /// Boxes the start end.
        /// </summary>
        /// <param name="totalWidth">The total width.</param>
        /// <returns></returns>
        private static string BoxStartEnd(int totalWidth)
        {
            return $"+{new string('=', totalWidth - 2)}+";
        }

        /// <summary>
        /// Pads the both.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        private static string PadBoth(this string str, int length)
        {
            var spaces = length - str.Length;
            var padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }

        /// <summary>
        /// Gets the string between two different substrings.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="startString">The start string.</param>
        /// <param name="endString">The end string.</param>
        /// <returns></returns>
        public static string MiddleString(this string str, string startString, string endString)
        {
            if (!(str.Contains(startString) && str.Contains(endString)))
                return str;

            int startIndex = str.IndexOf(startString, StringComparison.CurrentCulture) + startString.Length;
            int endIndex = str.IndexOf(endString, startIndex, StringComparison.CurrentCulture);
            return str.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// Removes the last N characters.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="numberOfCharacters">The number of characters.</param>
        /// <returns></returns>
        public static string RemoveLast(this string str, int numberOfCharacters)
        {
            if (string.IsNullOrEmpty(str) || str.Length < numberOfCharacters)
                return string.Empty;

            return str.Remove(str.Length - numberOfCharacters, numberOfCharacters);
        }

        public static bool IsNullOrEmpty(this string currentString)
        {
            return string.IsNullOrEmpty(currentString);
        }

        public static bool IsNotNullAndEmpty(this string currentString)
        {
            return !string.IsNullOrEmpty(currentString);
        }

        public static bool IsNullOrWhiteSpace(this string currentString)
        {
            return string.IsNullOrWhiteSpace(currentString);
        }

        public static bool IsNotNullAndWhiteSpace(this string currentString)
        {
            return !string.IsNullOrWhiteSpace(currentString);
        }

        public static int ToInt32(this string currentString)
        {
            return Convert.ToInt32(currentString);
        }

        public static bool ToBoolean(this string currentString)
        {
            return Convert.ToBoolean(currentString);
        }

        public static bool IsCharacterWiseEqual(this string currentString, string anotherString)
        {
            return currentString.Trim().ToLower().Equals(anotherString.Trim().ToLower());
        }

        public static string ToLowerAndRemoveSpace(this string currentString)
        {
            return currentString.Replace(" ", string.Empty).ToLower();
        }

        public static string ToUpperFirstCharacter(this string currentString)
        {
            return currentString.First().ToString().ToUpper() + currentString.Substring(1);
        }

        /**
         * This method is written to get {numberOfChars} from {value} from the left
         * EXAMPLE: "Hello World".Left(5) = "Hello"
         */
        public static string Left(this string value, int numberOfChars)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            numberOfChars = Math.Abs(numberOfChars);

            return (value.Length <= numberOfChars ? value : value.Substring(0, numberOfChars));
        }

        /**
         * This method is written to get {numberOfChars} from {value} from the right
         * EXAMPLE: "Hello World".Right(5) = "World"
         */
        public static string Right(this string value, int numberOfChars)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            numberOfChars = Math.Abs(numberOfChars);

            return (value.Length <= numberOfChars ? value : value.Substring(value.Length - numberOfChars, numberOfChars));
        }

        //public static string Base64ForUrlEncode(this string str)
        //{
        //    byte[] encbuff = Encoding.UTF8.GetBytes(str);
        //    return HttpServerUtility.UrlTokenEncode(encbuff);
        //}

        //public static string Base64ForUrlDecode(this string str)
        //{
        //    byte[] decbuff = HttpServerUtility.UrlTokenDecode(str);
        //    return Encoding.UTF8.GetString(decbuff);
        //}

        // the string.Split() method from .NET tend to run out of memory on 80 Mb strings. 
        // this has been reported several places online. 
        // This version is fast and memory efficient and return no empty lines. 
        public static List<string> LowMemSplit(this string s, string seperator)
        {
            List<string> list = new List<string>();
            int lastPos = 0;
            int pos = s.IndexOf(seperator);
            while (pos > -1)
            {
                while (pos == lastPos)
                {
                    lastPos += seperator.Length;
                    pos = s.IndexOf(seperator, lastPos);
                    if (pos == -1)
                        return list;
                }

                string tmp = s.Substring(lastPos, pos - lastPos);
                if (tmp.Trim().Length > 0)
                    list.Add(tmp);
                lastPos = pos + seperator.Length;
                pos = s.IndexOf(seperator, lastPos);
            }

            if (lastPos < s.Length)
            {
                string tmp = s.Substring(lastPos, s.Length - lastPos);
                if (tmp.Trim().Length > 0)
                    list.Add(tmp);
            }

            return list;
        }
    }
}