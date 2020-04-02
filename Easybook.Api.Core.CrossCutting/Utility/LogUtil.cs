using System;
using System.Threading.Tasks;

namespace Easybook.Api.Core.CrossCutting.Utility
{
    public class LogUtil
    {
        private static readonly Logger _logger = LogManager.GetLogger("ErrorLogger");
        private static readonly Logger UnexpectedError_logger = LogManager.GetLogger("UnexpectedErrorLogger");
        private static readonly Logger BotLogger = LogManager.GetLogger("BotLogger");
        private static readonly Logger RodaLogger = LogManager.GetLogger("RodaLogger");
        private static readonly Logger LuxuryCoachLogger = LogManager.GetLogger("LuxuryCoachLogger");
        private static readonly Logger KTMBLogger = LogManager.GetLogger("KTMBLogger");
        private static readonly Logger KAILogger = LogManager.GetLogger("KAILogger");
        private static readonly Logger OketiketLogger = LogManager.GetLogger("OketiketLogger");
        private static readonly Logger APILogger = LogManager.GetLogger("APILogger");
        private static readonly Logger InfoLogger = LogManager.GetLogger("InfoLogger");
        private static readonly Logger MembershipLogger = LogManager.GetLogger("MembershipLogger");
        private static readonly Logger PageTrackingLogger = LogManager.GetLogger("PageTrackingLogger");
        private static readonly Logger KuraKuraLogger = LogManager.GetLogger("KuraKuraLogger");
        private static readonly Logger SRTLogger = LogManager.GetLogger("SRTLogger");
        private static readonly Logger WalletLogger = LogManager.GetLogger("WalletLogger");

        private const string _subject = "[Exception from NLog]";

        //Info
        public static void Log(string message, string subject = "")
        {
            try
            {
                LogEventInfo errorEvent = new LogEventInfo(LogLevel.Info, "", message);
                
                errorEvent.Properties["subject"] = subject ?? "NULL";
                errorEvent.Properties["error-source"] = "";
                errorEvent.Properties["error-class"] = "";
                errorEvent.Properties["error-method"] = "";
                errorEvent.Properties["error-message"] = message;
                errorEvent.Properties["inner-error-message"] = "";

                InfoLogger.Log(errorEvent);
            }
            catch (Exception ex)
            {}
        }

        public static void Log(string subject, string source, string className, string method, string message, string detailContent)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                errorEvent.Properties["subject"] = subject;
                errorEvent.Properties["error-source"] = source;
                errorEvent.Properties["error-class"] = className;
                errorEvent.Properties["error-method"] = method;
                errorEvent.Properties["error-message"] = message;
                errorEvent.Properties["inner-error-message"] = detailContent;

                InfoLogger.Log(errorEvent);
            }
            catch (Exception)
            {
                //Ignored
            }
        }

        public static void LogEmail(string errorMessage, string cartGuid, string emailContent)
        {
            try 
            {
                var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                errorEvent.Properties["subject"] = "Order Summary Email Log";
                errorEvent.Properties["error-source"] = cartGuid;
                errorEvent.Properties["error-class"] = "EmailUtil";
                errorEvent.Properties["error-method"] = "SendCompletedCallBack";
                errorEvent.Properties["error-message"] = errorMessage;
                errorEvent.Properties["inner-error-message"] = emailContent;

                InfoLogger.Log(errorEvent);
            }
            catch (Exception) 
            {
                //Ignored
            }
        }
        
        public static async void LogBot(BOT_Logs log)
        {
            await Task.Run(() =>
            {
                try
                {
                    var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                    errorEvent.Properties["operation-type"] = log.OperationType;
                    errorEvent.Properties["status"] = log.Status;
                    errorEvent.Properties["create-date"] = log.CreateDate;
                    errorEvent.Properties["company-id"] = log.CompanyId;
                    errorEvent.Properties["booking-reference"] = log.BookingReference;
                    errorEvent.Properties["trip-guid"] = log.TripGuid;
                    errorEvent.Properties["schedule-guid"] = log.ScheduleGuid;
                    errorEvent.Properties["cart-guid"] = log.CartGuid;
                    errorEvent.Properties["seat"] = log.Seat;
                    errorEvent.Properties["seat-quantity"] = log.SeatQuantity;
                    errorEvent.Properties["url"] = log.Url;
                    errorEvent.Properties["response-xml"] = log.ResponseXml;
                    errorEvent.Properties["returned-error-message"] = log.ReturnedErrorMessage;
                    errorEvent.Properties["ex-message"] = log.ExceptionMessage;

                    BotLogger.Log(errorEvent);
                }
                catch(Exception) 
                { 
                    //Ignore
                }
            });
        }
                
        public static async void LogRoda(Roda_Log log)
        {
            await Task.Run(() =>
            {
                try
                {
                    var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                    errorEvent.Properties["operation-type"] = log.Operation;
                    errorEvent.Properties["status"] = log.Status;
                    errorEvent.Properties["trip-guid"] = log.TripGuid;
                    errorEvent.Properties["return-code"] = log.ReturnCode;
                    errorEvent.Properties["return-message"] = log.ReturnMessage;
                    errorEvent.Properties["booking-code"] = log.BookingCode;
                    errorEvent.Properties["seat-quantity"] = log.SeatQuantity;
                    errorEvent.Properties["cart-guid"] = log.CartGuid;
                    errorEvent.Properties["company-id"] = log.CompanyId;
                    errorEvent.Properties["ex-message"] = log.ExceptionMessage;
                    errorEvent.Properties["create-date"] = log.CreateDate;

                    RodaLogger.Log(errorEvent);
                }
                catch (Exception)
                {
                    //Ignore
                }
            });
        }

        public static async void LogLuxuryCoach(LuxuryCoach_Logs log)
        {
            await Task.Run(() =>
            {
                try
                {
                    var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                    errorEvent.Properties["operation-type"] = log.OperationType;
                    errorEvent.Properties["status"] = log.Status;
                    errorEvent.Properties["create-date"] = log.CreateDate;
                    errorEvent.Properties["booking-reference"] = log.BookingReference;
                    errorEvent.Properties["trip-guid"] = log.TripGuid;
                    errorEvent.Properties["schedule-guid"] = log.ScheduleGuid;
                    errorEvent.Properties["cart-guid"] = log.CartGuid;
                    errorEvent.Properties["seat"] = log.Seat;
                    errorEvent.Properties["seat-quantity"] = log.SeatQuantity;
                    errorEvent.Properties["response-xml"] = log.ResponseXml;
                    errorEvent.Properties["returned-error-message"] = log.ReturnedErrorMessage;
                    errorEvent.Properties["ex-message"] = log.ExceptionMessage;
                    errorEvent.Properties["request"] = log.Request;

                    LuxuryCoachLogger.Log(errorEvent);
                }
                catch (Exception)
                {
                    //Ignore 
                }
            });
        }

        public static void LogOketiket(Model.Easybook.Model.OkeTiketAPILogs log)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                errorEvent.Properties["api"] = log.API;
                errorEvent.Properties["status"] = log.Status;
                errorEvent.Properties["create-date"] = log.CreateDate;
                errorEvent.Properties["booking-reference"] = log.BookingReference;
                errorEvent.Properties["cart-guid"] = log.CartGuid;
                errorEvent.Properties["seat"] = log.Seat;
                errorEvent.Properties["seat-quantity"] = log.SeatQuantity;
                errorEvent.Properties["request"] = log.Request;
                errorEvent.Properties["response"] = log.Response;
                errorEvent.Properties["returned-error-message"] = log.ErrorMessage;
                errorEvent.Properties["ex-message"] = log.ExceptionMessage;

                OketiketLogger.Log(errorEvent);
            }
            catch (Exception)
            {
                //Ignore
            }
        }

        public static void LogKAI(Model.Easybook.Model.KAILog log)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                errorEvent.Properties["api"] = log.API;
                errorEvent.Properties["status"] = log.Status;
                errorEvent.Properties["create-date"] = log.CreateDate;
                errorEvent.Properties["booking-reference"] = log.BookingReference;
                errorEvent.Properties["cart-guid"] = log.CartGuid;
                errorEvent.Properties["seat"] = log.Seat;
                errorEvent.Properties["seat-quantity"] = log.SeatQuantity;
                errorEvent.Properties["request"] = log.Request;
                errorEvent.Properties["response"] = log.Response;
                errorEvent.Properties["returned-error-message"] = log.ErrorMessage;
                errorEvent.Properties["ex-message"] = log.ExceptionMessage;

                KAILogger.Log(errorEvent);
            }
            catch (Exception)
            {
                //Ignore
            }
        }

        public static void LogKTMB(KTMB_Logs log)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                errorEvent.Properties["api"] = log.API;
                errorEvent.Properties["status"] = log.Status;
                errorEvent.Properties["create-date"] = log.CreateDate;
                errorEvent.Properties["booking-reference"] = log.BookingReference;
                errorEvent.Properties["cart-guid"] = log.CartGuid;
                errorEvent.Properties["seat"] = log.Seat;
                errorEvent.Properties["seat-quantity"] = log.SeatQuantity;
                errorEvent.Properties["request"] = log.Request;
                errorEvent.Properties["response"] = log.Response;
                errorEvent.Properties["returned-error-message"] = log.ReturnedErrorMessage;
                errorEvent.Properties["ex-message"] = log.ExceptionMessage;

                KTMBLogger.Log(errorEvent);
            }
            catch (Exception)
            {
                //Ignore
            }
        }

        public static void LogSRT(SRTTrainLogs log)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                errorEvent.Properties["operation-type"] = log.OperationType;
                errorEvent.Properties["status"] = log.Status;
                errorEvent.Properties["create-date"] = log.CreateDate;
                errorEvent.Properties["company-id"] = log.CompanyId;
                errorEvent.Properties["booking-reference"] = log.BookingReference;
                errorEvent.Properties["trip-guid"] = log.TripGuid;
                errorEvent.Properties["schedule-guid"] = log.ScheduleGuid;
                errorEvent.Properties["cart-guid"] = log.CartGuid;
                errorEvent.Properties["seat"] = log.Seat;
                errorEvent.Properties["seat-quantity"] = log.SeatQuantity;
                errorEvent.Properties["request"] = log.Request;
                errorEvent.Properties["response"] = log.Response;
                errorEvent.Properties["returned-error-message"] = log.ReturnedErrorMessage;
                errorEvent.Properties["ex-message"] = log.ExceptionMessage;
                errorEvent.Properties["session-id"] = log.SessionId;

                SRTLogger.Log(errorEvent);
            }
            catch (Exception)
            {
                //Ignore
            }
        }

        public static void LogAPI(MemberAgentLogs log)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                errorEvent.Properties["aspnetuser-id"] = log.AspNetUserId;
                errorEvent.Properties["product-id"] = log.ProductId;
                errorEvent.Properties["api-caller-type"] = log.ApiCallerType;
                errorEvent.Properties["api-method"] = log.ApiMethod;
                errorEvent.Properties["request"] = log.Request;
                errorEvent.Properties["exception"] = log.Exception;
                errorEvent.Properties["status"] = log.Status;
                errorEvent.Properties["return-code"] = log.ReturnCode;
                errorEvent.Properties["create-date"] = DateTime.Now;

                APILogger.Log(errorEvent);
            }
            catch (Exception)
            {
                //Ignore
            }
        }

        public static void LogWalletAPI(Wallet_Logs log)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                errorEvent.Properties["aspnetuser-id"] = log.AspNetUserId;
                errorEvent.Properties["product-id"] = log.ProductId;
                errorEvent.Properties["api-caller-type"] = log.ApiCallerType;
                errorEvent.Properties["api-method"] = log.ApiMethod;
                errorEvent.Properties["request"] = log.Request;
                errorEvent.Properties["exception"] = log.Exception;
                errorEvent.Properties["status"] = log.Status;
                errorEvent.Properties["return-code"] = log.ReturnCode;
                errorEvent.Properties["create-date"] = DateTime.Now;

                WalletLogger.Log(errorEvent);
            }
            catch (Exception)
            {
                //Ignore
            }
        }

        public static async void LogMember(MembershipLogs membershipLogs)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                errorEvent.Properties["method-name"] = membershipLogs.MethodName;
                errorEvent.Properties["status"] = membershipLogs.Status;
                errorEvent.Properties["email"] = membershipLogs.Email;
                errorEvent.Properties["password"] = membershipLogs.Password;
                errorEvent.Properties["message"] = membershipLogs.Message;
                errorEvent.Properties["ipaddress"] = membershipLogs.IpAddress;
                errorEvent.Properties["phonenumber"] = membershipLogs.PhoneNumber;
                errorEvent.Properties["dialcode"] = membershipLogs.DialCode;
                errorEvent.Properties["module"] = membershipLogs.Module;
                errorEvent.Properties["action"] = membershipLogs.Action;
                errorEvent.Properties["aspnetuserid"] = membershipLogs.AspNetUserId;
                
                MembershipLogger.Log(errorEvent);
            }
            catch (Exception ex)
            {
                //Ignore
            }
        }

        public static async void LogPageTracking(string ipAddress)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);
                errorEvent.Properties["ip-address"] = ipAddress;
                PageTrackingLogger.Log(errorEvent);
            }
            catch (Exception)
            {
                //Ignore
            }
        }

        public static  void LogKuraKura(KuraKuraLog log) //async
        {
            //await Task.Run(() =>
            //{
                try
                {
                    var errorEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);

                    errorEvent.Properties["operation-type"] = log.Operation;
                    errorEvent.Properties["status"] = log.Status;
                    errorEvent.Properties["trip-guid"] = log.TripGuid;
                    errorEvent.Properties["return-code"] = log.ReturnCode;
                    errorEvent.Properties["return-message"] = log.ReturnMessage;
                    errorEvent.Properties["seat-quantity"] = log.SeatQuantity;
                    errorEvent.Properties["cart-guid"] = log.CartGuid;
                    errorEvent.Properties["company-id"] = log.CompanyId;
                    errorEvent.Properties["ex-message"] = log.ExceptionMessage;
                    errorEvent.Properties["create-date"] = log.CreateDate;
                    errorEvent.Properties["issue-name"] = log.IssueName;


                    KuraKuraLogger.Log(errorEvent);
                }
                catch (Exception)
                {
                    //Ignore
                }
            //});
        }

        //Error
        public static void Error(Exception exception, string subject = _subject,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, "", exception.Message);                                
                errorEvent.Properties["subject"] = subject;
                errorEvent.Properties["error-source"] = sourceFilePath.Replace("\\", "|").Replace("|", "/");
                errorEvent.Properties["error-class"] = exception.TargetSite?.DeclaringType?.Name ?? "Unknown Class Name";
                //sourceLineNumber.ToString();
                errorEvent.Properties["error-method"] = exception.TargetSite?.Name ?? "Unknown Method Name";
                errorEvent.Properties["error-message"] = exception.Message;
                errorEvent.Properties["inner-error-message"] = exception.ToString();
                errorEvent.Properties["recipient-email-address"] = "truong.pham@easybook.com";

                _logger.Error(errorEvent);
            }
            catch (Exception) {}
        }

        //Unexpected Error
        public static void UnexpectedError(Exception exception, string subject = _subject, Guid errorID = new Guid(),
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, "", exception.Message);
                errorEvent.Properties["errorid"] = errorID;
                errorEvent.Properties["subject"] = subject;
                errorEvent.Properties["error-source"] = sourceFilePath.Replace("\\", "|").Replace("|", "/");
                errorEvent.Properties["error-class"] = exception.TargetSite?.DeclaringType?.Name ?? "Unknown Class Name";
                //sourceLineNumber.ToString();
                errorEvent.Properties["error-method"] = exception.TargetSite?.Name ?? "Unknown Method Name";
                errorEvent.Properties["error-message"] = exception.Message;
                errorEvent.Properties["inner-error-message"] = exception.ToString();
                errorEvent.Properties["recipient-email-address"] = "truong.pham@easybook.com";

                UnexpectedError_logger.Error(errorEvent);               
            }
            catch (Exception) { }
        }        

        public static void ErrorWithConditionalEmail(Exception exception,
            string subject = _subject, string recipientEmailAddress = "",
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Error, string.Empty, exception.Message);
                errorEvent.Properties["ex-message"] = exception.Message;
                errorEvent.Properties["ex-stacktrace"] = exception.StackTrace;
                errorEvent.Properties["subject"] = subject;
                errorEvent.Properties["error-source"] = sourceFilePath.Replace("\\", "|").Replace("|", "/");
                errorEvent.Properties["error-class"] = exception.TargetSite?.DeclaringType?.Name ?? "Unknown Class Name";
                errorEvent.Properties["error-method"] = exception.TargetSite?.Name ?? "Unknown Method Name";
                errorEvent.Properties["error-message"] = exception.Message;
                errorEvent.Properties["inner-error-message"] = exception.ToString();
                errorEvent.Properties["recipient-email-address"] = recipientEmailAddress;

                _logger.Error(errorEvent);
            }
            catch (Exception)
            {
                // Logger got error, what more do you want?
            }
        }

        public static void ErrorWithConditionalEmailv2(Exception exception, string extraErrorMessage = "",
            string subject = _subject, string recipientEmailAddress = "", 
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                var errorEvent = new LogEventInfo(LogLevel.Error, string.Empty, exception.Message);
                errorEvent.Properties["ex-message"] = exception.Message;
                errorEvent.Properties["ex-stacktrace"] = exception.StackTrace;
                errorEvent.Properties["subject"] = subject;
                errorEvent.Properties["error-source"] = sourceFilePath.Replace("\\", "|").Replace("|", "/");
                errorEvent.Properties["error-class"] = exception.TargetSite?.DeclaringType?.Name ?? "Unknown Class Name";
                errorEvent.Properties["error-method"] = exception.TargetSite?.Name ?? "Unknown Method Name";
                errorEvent.Properties["error-message"] = $"{exception.Message} {extraErrorMessage}";
                errorEvent.Properties["inner-error-message"] = exception.ToString();
                errorEvent.Properties["recipient-email-address"] = recipientEmailAddress;

                _logger.Error(errorEvent);
            }
            catch (Exception)
            {
                // Logger got error, what more do you want?
            }
        }

        //Error
        public static void AngularError(string message, string subject = _subject,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, "", message);
                errorEvent.Properties["subject"] = subject;
                errorEvent.Properties["error-source"] = sourceFilePath.Replace("\\", "|").Replace("|", "/");
                errorEvent.Properties["error-class"] = ""; //sourceLineNumber.ToString();
                errorEvent.Properties["error-method"] = "";
                errorEvent.Properties["error-message"] = "";
                errorEvent.Properties["inner-error-message"] = message;

                _logger.Error(errorEvent);
            }
            catch (Exception ex)
            {

            }
        }

        //Fatal
        public static void Fatal(Exception exception, string subject = _subject,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                LogEventInfo errorEvent = new LogEventInfo(LogLevel.Fatal, "", exception.Message);
                errorEvent.Properties["subject"] = subject;
                errorEvent.Properties["error-source"] = sourceFilePath.Replace("\\", "|").Replace("|", "/");
                errorEvent.Properties["error-class"] = exception.TargetSite.DeclaringType.Name; //sourceLineNumber.ToString();
                errorEvent.Properties["error-method"] = exception.TargetSite.Name;
                errorEvent.Properties["error-message"] = exception.Message;
                errorEvent.Properties["inner-error-message"] = exception.ToString();

                _logger.Fatal(errorEvent);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
