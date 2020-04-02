namespace Easybook.Api.BusinessLogic.EasyWallet.Constant
{
    public static class ApiReturnCodeConstant
    {
        public static readonly ReturnCodeObj SUCCESS = new ReturnCodeObj { Code = ApiReturnCodeEnum.SUCCESS.ToInt(), Message = "Success" };
        public static readonly ReturnCodeObj SUCCESS_BUT_FAILED_TO_RECORD_TRX = new ReturnCodeObj { Code = ApiReturnCodeEnum.SUCCESS_BUT_FAILED_TO_RECORD_TRX.ToInt(), Message = "Transaction successful but failed to record. Please contact our customer service with your reserve reference number." };

        public static readonly ReturnCodeObj ERR_VALIDATE_SNAPSHOT_FAILED = new ReturnCodeObj { Code = ApiReturnCodeEnum.VALIDATE_SNAPSHOT_FAILED.ToInt(), Message = "Validate Snapshot Failed." };
        public static readonly ReturnCodeObj ERR_VALIDATE_TRX_FAILED = new ReturnCodeObj { Code = ApiReturnCodeEnum.VALIDATE_TRX_FAILED.ToInt(), Message = "Validate SubTrans Failed." };
        public static readonly ReturnCodeObj ERR_VALIDATE_WITHDRAW_FAILED = new ReturnCodeObj { Code = ApiReturnCodeEnum.VALIDATE_WITHDRAW_FAILED.ToInt(), Message = "Validate Withdraw Failed." };

        public static readonly ReturnCodeObj ERR_SYSTEM_ERROR = new ReturnCodeObj { Code = ApiReturnCodeEnum.SYSTEM_ERROR.ToInt(), Message = "System error." };
        public static readonly ReturnCodeObj ERR_INVALID_INPUT = new ReturnCodeObj { Code = ApiReturnCodeEnum.INPUT_ERROR.ToInt(), Message = "Invalid input." };
        public static readonly ReturnCodeObj ERR_INVALID_CURRENCY = new ReturnCodeObj { Code = ApiReturnCodeEnum.INPUT_ERROR.ToInt(), Message = "Currency is not supported." };
        public static readonly ReturnCodeObj ERR_INVALID_SIGNATURE = new ReturnCodeObj { Code = ApiReturnCodeEnum.INVALID_SIGNATURE.ToInt(), Message = "Invalid signature." };

        public static class COMMON
        {
            // Cart under processing in Confirm seat
            public static readonly ReturnCodeObj ERR_CONFIRMBOOKING_UNDERPROCESSING = new ReturnCodeObj { Code = ApiReturnCodeEnum.CONFIRMBOOKING_UNDERPROCESSING.ToInt(), Message = "This booking is being processed by previous API request." };

            public static readonly ReturnCodeObj ERR_INVALID_BOOKING_REF_NO = new ReturnCodeObj { Code = ApiReturnCodeEnum.INVALID_BOOKING_REF_NO.ToInt(), Message = "invalid booking reference no." };
            public static readonly ReturnCodeObj ERR_INVALID_CURRENCY_CONVERSION_PAIR = new ReturnCodeObj { Code = ApiReturnCodeEnum.INVALID_CURRENCY_CONVERSION_PAIR.ToInt(), Message = "invalid currency conversion pair." };
            public static readonly ReturnCodeObj ERR_USER_NOT_FOUND = new ReturnCodeObj { Code = ApiReturnCodeEnum.USER_NOT_FOUND.ToInt(), Message = "user id not found." };
            public static readonly ReturnCodeObj ERR_INVALID_TOTALAMOUNT = new ReturnCodeObj { Code = ApiReturnCodeEnum.INVALID_TOTALAMOUNT.ToInt(), Message = "Total pay must be greater than 1.00." };

            #region Internal api common return code
            public static readonly ReturnCodeObj ERR_INVALID_PAYMENTGATEWAY = new ReturnCodeObj { Code = ApiReturnCodeEnum.INVALID_PAYMENTGATEWAY.ToInt(), Message = "Cannot check out using this payment option. Please try again later or choose other payment option." };
            #endregion Internal api common return code
        }

        public static class Wallet
        {
            public static readonly ReturnCodeObj ERR_WALLETACC_NOTEXIST = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_WALLETACC_NOTEXIST.ToInt(), Message = "Wallet Account ID could not be found." };

            public static readonly ReturnCodeObj ERR_WALLETID_NOTEXIST_IN_WALLETACC = new ReturnCodeObj { Code = ApiReturnCodeEnum.WALLET_QUERYWALLETACC_FAIL.ToInt(), Message = "WalletID does not exist." };
                        
            public static readonly ReturnCodeObj ERR_WALLET_EXCEED_LIMIT_TOPUP = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_WALLET_EXCEED_LIMIT_TOPUP.ToInt(), Message = "Exceed limit value topup." };

            public static readonly ReturnCodeObj ERR_WALLET_MINIMUM_AMOUNT_TOPUP = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_WALLET_MINIMUM_AMOUNT_TOPUP.ToInt(), Message = "Please add more amount for topup." };

            public static readonly ReturnCodeObj ERR_WALLET_INSUFFICIENT_BALANCE = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_WALLET_INSUFFICIENT_BALANCE.ToInt(), Message = "Insufficient available balance." };

            public static readonly ReturnCodeObj ERR_WALLET_DIFF_BALANCE = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_WALLET_DIFF_BALANCE.ToInt(), Message = "Amount could not larger than available balance." };

            public static readonly ReturnCodeObj ERR_TRAN_NOTEXIST = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_TRAN_NOTEXIST.ToInt(), Message = "Transaction does not exist." };

            public static readonly ReturnCodeObj ERR_USERIDnCURRENCY_NOTEXIST_IN_WALLETACC = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_USERIDnCURRENCY_NOTEXIST_IN_WALLETACC.ToInt(), Message = "Cannot find WalletID by userID and Currency." };

            public static readonly ReturnCodeObj ERR_USERID_NOTEXIST = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_USER_NOTEXIST.ToInt(), Message = "User ID does not exist." };

            public static readonly ReturnCodeObj ERR_WALLET_CHECKSUMACC_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_WALLET_CHECKSUMACC_FAIL.ToInt(), Message = "Failed to check checksum for wallet account." };

            public static readonly ReturnCodeObj ERR_WALLET_OVERDATE_RANGE_ = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_WALLET_OVERDATE_RANGE_.ToInt(), Message = "Searching of date range allow within 1 month. Please contact to our customer service for more extension if any inquiry!" };

            public static readonly ReturnCodeObj ERR_WALLET_OVER_LIMIT_TOPUPBONUS = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_WALLET_OVER_LIMIT_TOPUPBONUS.ToInt(), Message = "There is limit topup getting extra reward." };
            public static readonly ReturnCodeObj ERR_WALLET_WITHDRAWAL_RECEIPTAMOUNT = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_WALLET_WITHDRAWAL_RECEIPTAMOUNT.ToInt(), Message = "Your receipt amount is 0." };
            public static readonly ReturnCodeObj ERR_WALLET_OVER_LIMIT_WITHDRAWAL = new ReturnCodeObj { Code = ApiReturnCodeEnum.ERR_WALLET_OVER_LIMIT_WITHDRAWAL.ToInt(), Message = "Withdrawal is limited 2 times a month." };
        }

        public static class BUS
        {
            // invalid request
            public static readonly ReturnCodeObj ERR_INVALID_REQUEST = new ReturnCodeObj { Code = ApiReturnCodeEnum.BUS_INVALID_REQUEST.ToInt(), Message = "Invalid request." };

            // Get booking fare
            public static readonly ReturnCodeObj ERR_QUERYFARE_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.BUS_QUERYFARE_FAIL.ToInt(), Message = "Failed to query fare." };

            // Get Seat Plan
            public static readonly ReturnCodeObj ERR_SEATPLAN_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.BUS_QUERYSEAT_FAIL.ToInt(), Message = "Failed to query seat plan." };

            // Reserve seat
            public static readonly ReturnCodeObj ERR_RESERVESEAT_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.BUS_RESERVESEAT_FAIL.ToInt(), Message = "Failed to reserve seat(s)." };
            public static readonly ReturnCodeObj ERR_RESERVESEAT_TRIPEXPIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.BUS_RESERVESEAT_TRIPEXPIRED.ToInt(), Message = "Trip expired." };

            // Block seat
            public static readonly ReturnCodeObj ERR_SEATNOTAVAILABLE_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.BUS_SEATNOTAVAILABLE_FAIL.ToInt(), Message = "Failed to block seat(s). Seat not available" };

            // Confirm seat
            public static readonly ReturnCodeObj ERR_CONFIRMSEAT_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.BUS_CONFIRMSEAT_FAIL.ToInt(), Message = "Failed to confirm seat(s)." };

        }

        public static class TRAIN
        {
            public static readonly ReturnCodeObj ERR_INVALID_TRIPKEY = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_INVALID_TRIPKEY.ToInt(), Message = "invalid tripkey" };
            public static readonly ReturnCodeObj ERR_COACH_NOT_FOUND = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_COACH_NOT_FOUND.ToInt(), Message = "coach not found" };
            public static readonly ReturnCodeObj ERR_INVALID_COMPANYID = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_INVALID_COMPANYID.ToInt(), Message = "coach not found" };
            public static readonly ReturnCodeObj ERR_MAX_PAX_EXCEED = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_MAX_PAX_EXCEED.ToInt(), Message = "max pax exceeds" };

            // Get booking fare
            public static readonly ReturnCodeObj ERR_QUERYFARE_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_QUERYFARE_FAIL.ToInt(), Message = "Failed to query fare." };

            // Reserve seat
            public static readonly ReturnCodeObj ERR_RESERVESEAT_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_RESERVESEAT_FAIL.ToInt(), Message = "Failed to reserve seat(s)." };
            public static readonly ReturnCodeObj ERR_RESERVESEAT_TRIPEXPIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_RESERVESEAT_TRIPEXPIRED.ToInt(), Message = "Trip expired." };
            public static readonly ReturnCodeObj ERR_RESERVESEAT_SEATLOCKED = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_RESERVESEAT_SEATLOCKED.ToInt(), Message = "selected seat(s) already been taken. please choose another seat(s)" };
            public static readonly ReturnCodeObj ERR_RESERVESEAT_IC_DOB_NOT_MATCH = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_RESERVESEAT_IC_DOB_NOT_MATCH.ToInt(), Message = "IC and DOB do not match." };
            public static readonly ReturnCodeObj ERR_RESERVESEAT_INVALID_PAX_INFO = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_RESERVESEAT_INVALID_PAX_INFO.ToInt(), Message = "Invalid passenger information." };
            public static readonly ReturnCodeObj ERR_RESERVESEAT_INVALID_PAX_AGE = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_RESERVESEAT_INVALID_PAX_AGE.ToInt(), Message = "Invalid passenger age. Senior age must more or equal to 60." };
            public static readonly ReturnCodeObj ERR_RESERVESEAT_ADVANCED_TRIP_NOT_MORE_THAN_30D = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_RESERVESEAT_ADVANCED_TRIP_NOT_MORE_THAN_30D.ToInt(), Message = "Cannot purchase ticket with journey date more than 30 days from today." };

            // Confirm seat
            public static readonly ReturnCodeObj ERR_CONFIRMSEAT_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.TRAIN_CONFIRMSEAT_FAIL.ToInt(), Message = "Failed to confirm seat(s)." };
        }

        public static class FERRY
        {
            // Get booking fare
            public static readonly ReturnCodeObj ERR_QUERYFARE_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_QUERYFARE_FAIL.ToInt(), Message = "Failed to query fare." };

            // Get Seat Plan
            public static readonly ReturnCodeObj ERR_SEATPLAN_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_QUERYSEAT_FAIL.ToInt(), Message = "Failed to query seat plan." };

            // Reserve seat
            public static readonly ReturnCodeObj ERR_RESERVESEAT_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_RESERVESEAT_FAIL.ToInt(), Message = "Failed to reserve seat(s)." };
            public static readonly ReturnCodeObj ERR_RESERVESEAT_TRIPEXPIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_RESERVESEAT_TRIPEXPIRED.ToInt(), Message = "Trip expired." };

            // Confirm seat
            public static readonly ReturnCodeObj ERR_CONFIRMSEAT_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_CONFIRMSEAT_FAIL.ToInt(), Message = "Failed to confirm seat(s)." };

            // Require Seat Info
            public static readonly ReturnCodeObj ERR_REQUIRE_SEAT_INFO = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_REQUIRE_SEAT_INFO.ToInt(), Message = "Seat information is required.(SeatNumber, SeatCode, SeatId, SeatTimeStamp)." };

            // Seats not available
            public static readonly ReturnCodeObj ERR_SEATS_NOT_AVAILABLE = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_SEATS_NOT_AVAILABLE.ToInt(), Message = "Not enough seats for Passengers or sold out." };

            // Incorrect date info
            public static readonly ReturnCodeObj ERR_INCORRECT_DATE_INFO = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_INCORRECT_DATE_INFO.ToInt(), Message = "Customer Date of Birth Should less than Today and Passport Expiry date must be at least 6 months from date of travel." };

            // Number of Pax not same
            public static readonly ReturnCodeObj ERR_DIFFERENT_NUM_OF_PAX = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_DIFFERENT_NUM_OF_PAX.ToInt(), Message = "Number of seats must be same for both Depart and Return." };

            // Pax info details not complete
            public static readonly ReturnCodeObj ERR_PAX_INFO_DETAILS_REQUIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_DETAILS_PAX_INFO.ToInt(), Message = "Passenger information details are required." };

            //require passport issue date
            public static readonly ReturnCodeObj ERR_PASSPORT_ISSUE_DATE_REQUIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_PASSPORT_ISSUE_DATE.ToInt(), Message = "Passport issue date is required." };

            //require passport issue country
            public static readonly ReturnCodeObj ERR_PASSPORT_ISSUE_COUNTRY_REQUIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_PASSPORT_ISSUE_COUNTRY.ToInt(), Message = "Passport issue country is required." };

            //require residence country
            public static readonly ReturnCodeObj ERR_PAX_RESIDENCE_COUNTRY_REQUIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_PAX_RESIDENCE_COUNTRY.ToInt(), Message = "Passenger residence country is required." };

            //require passport number
            public static readonly ReturnCodeObj ERR_PASSPORT_NUM_REQUIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_PASSPORT_NUM.ToInt(), Message = "Passport number is required." };

            //require passport expiry date
            public static readonly ReturnCodeObj ERR_PASSPORT_EXPIRY_DATE_REQUIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_PASSPORT_EXPIRY_DATE.ToInt(), Message = "Passport expiry date is required." };

            //require pax dob
            public static readonly ReturnCodeObj ERR_PAX_DOB_REQUIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_PAX_DOB.ToInt(), Message = "Date of Birth is required." };

            //require pax gender
            public static readonly ReturnCodeObj ERR_PAX_GENDER_REQUIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_PAX_GENDER.ToInt(), Message = "Gender is required." };

            //require pax nationality
            public static readonly ReturnCodeObj ERR_PAX_NATIONALITY_REQUIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_PAX_NATIONALITY.ToInt(), Message = "Passenger nationality is required." };

            //require pax type
            public static readonly ReturnCodeObj ERR_PAX_TYPE_REQUIRED = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_PAX_TYPE.ToInt(), Message = "Passenger type (Adult/Child) is required." };

            //passenger type mismatch
            public static readonly ReturnCodeObj ERR_PAX_TYPE_MISMATCH = new ReturnCodeObj { Code = ApiReturnCodeEnum.FERRY_PAX_TYPE_MISMATCH.ToInt(), Message = "Child age must be 11 years old and below." };

        }

        public static class Car
        {
            public static readonly ReturnCodeObj ERR_CarNotFound = new ReturnCodeObj { Code = ApiReturnCodeEnum.Car_CarNotFound.ToInt(), Message = "Car not found." };
            public static readonly ReturnCodeObj ERR_ReserveRent_Failed = new ReturnCodeObj { Code = ApiReturnCodeEnum.Car_ReserveRent_Failed.ToInt(), Message = "Failed to reserve car rent." };
            public static readonly ReturnCodeObj ERR_ConfirmRent_Failed = new ReturnCodeObj { Code = ApiReturnCodeEnum.Car_ConfirmRent_Failed.ToInt(), Message = "Failed to confirm car rent." };
            public static readonly ReturnCodeObj ERR_QueryFare_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.Car_QueryFare_FAIL.ToInt(), Message = "Failed to query car fare." };
            public static readonly ReturnCodeObj ERR_RentOutofAvailableDate = new ReturnCodeObj { Code = ApiReturnCodeEnum.Car_RentOutofAvailableDate.ToInt(), Message = "Car rent is not in valid date range." };
            public static readonly ReturnCodeObj ERR_PickupNotAvailable = new ReturnCodeObj { Code = ApiReturnCodeEnum.Car_PickupNotAvailable.ToInt(), Message = "Car rent pickup address is not available." };
            public static readonly ReturnCodeObj ERR_DropoffNotAvailable = new ReturnCodeObj { Code = ApiReturnCodeEnum.Car_DropoffNotAvailable.ToInt(), Message = "Car rent dropoff address is not available." };       
        }

        public static class BUSDAYPASS
        {
            // invalid request
            public static readonly ReturnCodeObj ERR_INVALID_REQUEST = new ReturnCodeObj { Code = ApiReturnCodeEnum.BUSDAYPASS_INVALID_REQUEST.ToInt(), Message = "Invalid request." };

            // Get booking fare
            public static readonly ReturnCodeObj ERR_QUERYFARE_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.BUSDAYPASS_QUERYFARE_FAIL.ToInt(), Message = "Failed to query fare." };

            // Reserve seat
            public static readonly ReturnCodeObj ERR_RESERVESEAT_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.BUSDAYPASS_RESERVESEAT_FAIL.ToInt(), Message = "Failed to reserve seat(s)." };

            // Confirm seat
            public static readonly ReturnCodeObj ERR_CONFIRMSEAT_FAIL = new ReturnCodeObj { Code = ApiReturnCodeEnum.BUSDAYPASS_CONFIRMSEAT_FAIL.ToInt(), Message = "Failed to confirm seat(s)." };

        }
    }
    public enum ApiReturnCodeEnum
    {
        DEFAULT = 0,
        SUCCESS = 1,
        SUCCESS_BUT_FAILED_TO_RECORD_TRX = 2,
        CONFIRMBOOKING_UNDERPROCESSING = 3,

        VALIDATE_SNAPSHOT_FAILED = -5,
        VALIDATE_TRX_FAILED = -6,
        VALIDATE_WITHDRAW_FAILED = -7,

        WALLET_QUERYWALLETACC_FAIL = -7001,
        ERR_WALLET_EXCEED_LIMIT_TOPUP = -7002,
        ERR_WALLET_INSUFFICIENT_BALANCE = -7003,
        ERR_WALLET_DIFF_BALANCE = -7004,
        ERR_WALLET_NOTEXIST = -7005,
        ERR_TRAN_NOTEXIST = -7006,
        ERR_USERIDnCURRENCY_NOTEXIST_IN_WALLETACC = -7007,
        ERR_USER_NOTEXIST = -7008,
        ERR_WALLET_CHECKSUMACC_FAIL = -7009,
        ERR_WALLETACC_NOTEXIST = -7010,
        ERR_WALLET_OVERDATE_RANGE_ = -7011,
        ERR_WALLET_MINIMUM_AMOUNT_TOPUP = -7013,
        ERR_WALLET_OVER_LIMIT_TOPUPBONUS = -7014,
        ERR_WALLET_WITHDRAWAL_RECEIPTAMOUNT = -7015,
        ERR_WALLET_OVER_LIMIT_WITHDRAWAL = -7016,

        BUS_QUERYFARE_FAIL = -1001,
        BUS_RESERVESEAT_FAIL = -1002,
        BUS_RESERVESEAT_TRIPEXPIRED = -1003,
        BUS_CONFIRMSEAT_FAIL = -1004,
        BUS_QUERYSEAT_FAIL = -1005,
        BUS_INVALID_REQUEST = -1006,
        BUS_SEATNOTAVAILABLE_FAIL = -1007,

        TRAIN_INVALID_TRIPKEY = -5001,
        TRAIN_COACH_NOT_FOUND = -5002,
        TRAIN_INVALID_COMPANYID = -5003,
        TRAIN_MAX_PAX_EXCEED = -5004,
        TRAIN_QUERYFARE_FAIL = -5005,
        TRAIN_RESERVESEAT_FAIL = -5006,
        TRAIN_RESERVESEAT_TRIPEXPIRED = -5007,
        TRAIN_CONFIRMSEAT_FAIL = -5008,
        TRAIN_RESERVESEAT_SEATLOCKED = -5009,
        TRAIN_RESERVESEAT_IC_DOB_NOT_MATCH = -5010,
        TRAIN_RESERVESEAT_INVALID_PAX_INFO = -5011,
        TRAIN_RESERVESEAT_INVALID_PAX_AGE = -5012,
        TRAIN_RESERVESEAT_ADVANCED_TRIP_NOT_MORE_THAN_30D = -5013,

        SYSTEM_ERROR = -9001,
        INPUT_ERROR = -9002,
        ERR_INVALID_CURRENCY = -9003,
        INVALID_BOOKING_REF_NO = -9004,
        INVALID_CURRENCY_CONVERSION_PAIR = -9005,
        USER_NOT_FOUND = -9006,
        INVALID_SIGNATURE = -9007,
        INVALID_PAYMENTGATEWAY = -9008,
        INVALID_TOTALAMOUNT = -9009,

        FERRY_QUERYFARE_FAIL = -2001,
        FERRY_RESERVESEAT_FAIL = -2002,
        FERRY_RESERVESEAT_TRIPEXPIRED = -2003,
        FERRY_CONFIRMSEAT_FAIL = -2004,
        FERRY_QUERYSEAT_FAIL = -2005,
        FERRY_REQUIRE_SEAT_INFO = -2006,
        FERRY_SEATS_NOT_AVAILABLE = -2007,
        FERRY_INCORRECT_DATE_INFO = -2008,
        FERRY_DIFFERENT_NUM_OF_PAX = -2009,
        FERRY_DETAILS_PAX_INFO = -2010,
        FERRY_PASSPORT_ISSUE_DATE = -2011,
        FERRY_PASSPORT_ISSUE_COUNTRY = -2012,
        FERRY_PAX_RESIDENCE_COUNTRY = -2013,
        FERRY_PASSPORT_NUM = -2014,
        FERRY_PASSPORT_EXPIRY_DATE = -2015,
        FERRY_PAX_DOB = -2016,
        FERRY_PAX_GENDER = -2017,
        FERRY_PAX_NATIONALITY = -2018,
        FERRY_PAX_TYPE = -2019,
        FERRY_PAX_TYPE_MISMATCH = -2020,

        Car_CarNotFound = -3001,
        Car_ReserveRent_Failed = -3002,
        Car_ConfirmRent_Failed = -3003,
        Car_RentOutofAvailableDate = -3004,
        Car_PickupNotAvailable = -3005,
        Car_DropoffNotAvailable = -3006,
        Car_QueryFare_FAIL = -3007,

        BUSDAYPASS_QUERYFARE_FAIL = -90001,
        BUSDAYPASS_RESERVESEAT_FAIL = -90002,
        BUSDAYPASS_CONFIRMSEAT_FAIL = -90003,
        BUSDAYPASS_INVALID_REQUEST = -90004
    }

    public static class ApiReturnCodeEnumExtension
    {
        public static int ToInt(this ApiReturnCodeEnum returnCode)
        {
            return (int)returnCode;
        }

        public static string ToUpperString(this ApiReturnCodeEnum returnCode)
        {
            return returnCode.ToString().ToUpper();
        }

        public static string ToLowerString(this ApiReturnCodeEnum returnCode)
        {
            return returnCode.ToString().ToLower();
        }
    }
    
    public class ReturnCodeObj
    {
        public int Code = 0;
        public string Message = string.Empty;
    }
}
