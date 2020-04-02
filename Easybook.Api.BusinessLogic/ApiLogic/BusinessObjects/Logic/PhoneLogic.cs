using PhoneNumbers;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Easybook.Api.BusinessLogic.EasyCommon
{
    public class PhoneLogic
    {
        private string regionCode = string.Empty;
        private PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
        private PhoneNumber phoneNumber;
        private PhoneNumberUtil.ValidationResult validationResult;
        private PhoneNumberType phoneNumberType;
        private Boolean checkValidationNumberForRegion;
        private string outputRegionCode;
        private PhoneNumber examplePhoneNumber;
       // private string _defaultRegionCode = "SG";

        private StringBuilder _error;

        public PhoneLogic()
        {
            _error = new StringBuilder();
        }


        public bool IsValidPhone(string contact, string countrycode, out string error, out int? phonePrefixId, out string numberWithoutDialCode, out string  dialCode)
        {
            //default setting
            phonePrefixId = null;
            numberWithoutDialCode = null;
            dialCode = null;
            try
            {
                //If not null or white space
                if (!string.IsNullOrWhiteSpace(contact) && !string.IsNullOrWhiteSpace(countrycode))
                {
                    var phonePrefix = countrycode.ToUpper();

                    //If no select country Phone Prefix.
                    if (phonePrefix == null || !phonePrefix.Any())
                    {
                        _error.AppendLine("Invalid Dial Code.");
                        error = _error.ToString();
                        return false;
                    }

                    var prefixRecord = phonePrefix.FirstOrDefault();
                    //phonePrefixId = prefixRecord.Id;
                    //dialCode = prefixRecord.Prefix;
                    numberWithoutDialCode = FormatPhoneNumber(contact.Trim());
                    error = string.Empty;
                    return true;

                    #region PhoneNumberUtil: Disable this advance checking: due to limitation of updated phone format
                    //////regionCode = phonePrefix.FirstOrDefault().CountryCode;
                    //phoneNumber = phoneNumberUtil.Parse(contact, countrycode);
                    //validationResult = phoneNumberUtil.IsPossibleNumberWithReason(phoneNumber);
                    //checkValidationNumberForRegion = phoneNumberUtil.IsValidNumberForRegion(phoneNumber, countrycode);
                    //phoneNumberType = phoneNumberUtil.GetNumberType(phoneNumber);
                    //outputRegionCode = phoneNumberUtil.GetRegionCodeForNumber(phoneNumber);
                    //examplePhoneNumber = phoneNumberUtil.GetExampleNumberForType(countrycode, PhoneNumberType.MOBILE);

                    //if (checkValidationNumberForRegion && validationResult.Equals(PhoneNumberUtil.ValidationResult.IS_POSSIBLE) && phoneNumberType.Equals(PhoneNumberType.MOBILE) && outputRegionCode == countrycode)
                    //{
                    //    ulong nationalNumber = phoneNumber.NationalNumber;
                    //    error = null;
                    //    phonePrefixId = phonePrefix.FirstOrDefault().Id;
                    //    numberWithoutDialCode = Convert.ToString(nationalNumber);
                    //    return true;
                    //}
                    //else if (!checkValidationNumberForRegion)
                    //{
                    //    _error.AppendLine("Invalid contactr for the country.");
                    //    error = _error.ToString();
                    //    return false;
                    //}
                    //else if (!phoneNumberType.Equals(PhoneNumberType.MOBILE))
                    //{
                    //    _error.AppendLine("Invalid mobile number.");
                    //    error = _error.ToString();
                    //    return false;
                    //}
                    //else
                    //{
                    //    _error.AppendLine($"Invalid mobile number. Example of Phone number: {examplePhoneNumber.NationalNumber.ToString()}");
                    //    error = _error.ToString();
                    //    return false;
                    //}
                    #endregion

                }
                _error.AppendLine($"Invalid Mobile Number.");
                error = _error.ToString();
                numberWithoutDialCode = null;
                return false;

            }
            catch (Exception ex)
            {
                //EmailUtil.SendEmail("[Exception-InsertSubTran]", $"{ex.StackTrace}", "karchoon@easybook.com");
                _error.AppendLine(ex.Message.ToString());
                error = _error.ToString();
                return false;
            }
        }

        static string getDialCode(string contact)
        {
            if (!string.IsNullOrEmpty(contact))
            {
                return contact.Split(new char[0])[0].Replace("+", "");
            }
            return "";
        }

        static string getNationalNumber(string contact)
        {
            if (!string.IsNullOrEmpty(contact))
            {
                return contact.Split(new char[0])[1].Replace("+", "");
            }
            return "";
        }

        public static string FormatPhoneNumber(string phoneNumber)
        {
            ////make sure phoneNumber is not encrtyped string for this function
            if (string.IsNullOrEmpty(phoneNumber)) { return string.Empty; }
            ////due to we allow + sign front-end but backend we have to remove it
            return Regex.Replace(phoneNumber, @"[\+]", string.Empty);
        }

        public static string FormatMobileNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return string.Empty;

            return Regex.Replace(phoneNumber, @"[^\d]", string.Empty);
        }

        //public bool IsValidPhoneRegistration(string regionCode)
        //{
        //    //var phonePrefix = new CommonPhonePrefixLogic().GetRegistrationEnabledAll();
        //    //If no select country Phone Prefix.
        //    if (phonePrefix != null && phonePrefix.Any(x => x.CountryCode == regionCode))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
