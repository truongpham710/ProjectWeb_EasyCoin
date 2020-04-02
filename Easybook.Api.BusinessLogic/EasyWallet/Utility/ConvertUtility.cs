using System;


namespace Easybook.Api.BusinessLogic.EasyWallet.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConvertUtility
    {        
        /// <summary>
        /// Rounds to two decimal place.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal RoundToTwoDecimalPlaces(object value)
        {
            return Convert.ToDecimal(string.Format("{0:0.00}", value));             
        }

        public static decimal RoundToZeroDecimalPlaces(decimal value)
        {
            return Math.Round(value, 0);
        }

        public static decimal GetTaxValue(this decimal value, decimal taxPercentage)
        {
            return value * taxPercentage / 100;
        }    
    }
}