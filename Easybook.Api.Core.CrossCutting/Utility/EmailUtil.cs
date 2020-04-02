using Easybook.Api.Core.CrossCutting.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Easybook.Api.Core.CrossCutting.Utility
{
    public static class EmailAddress
    {
        public static string ID_BusProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_ID_BusProductPIC");
            }
        }

        public static string VN_BusProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_VN_BusProductPIC");
            }
        }

        public static string TH_BusProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_TH_BusProductPIC");
            }
        }

        public static string MY_BusProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_MY_BusProductPIC");
            }
        }

        public static string SG_BusProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_SG_BusProductPIC");
            }
        }

        public static string VN_TrainProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_VN_TrainProductPIC");
            }
        }

        public static string TH_TrainProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_TH_TrainProductPIC");
            }
        }

        public static string KH_TrainProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_KH_TrainProductPIC");
            }
        }

        public static string Insurance
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_Insurance");
            }
        }

        public static string Storage
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_Storage");
            }
        }

        public static string NewRouteSuggestion
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_NewRouteSuggestion");
            }
        }

        public static string Storage2
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_Storage2");
            }
        }

        public static string SenderName_Easybook
        {
            get
            {
                return "Easybook.com";
            }
        }

        public static string Enquiry
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_Enquiry");
            }
        }

        public static string Enquiry2
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_Enquiry2");
            }
        }

        public static string Error
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_Error");
            }
        }

        public static string EverythingPersonInCharge
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_OverallPIC");
            }
        }

        public static string BusPersonInCharge
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_BusProductPIC");
            }
        }

        public static string CarPersonInCharge
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_CarProductPIC");
            }
        }

        public static string FerryPersonInCharge
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_FerryProductPIC");
            }
        }

        public static string TrainPersonInCharge
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_TrainProductPIC");
            }
        }

        public static string TourPersonInCharge
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_TourProductPIC");
            }
        }

        public static string TourBusinessTeam
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_TourBusinessTeam");
            }
        }

        public static string ETSBExternalCreditMonitorTeam
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_ETSBBalanceAlert");
            }
        }

        //SYTan : Added 03-May-2017
        public static string ETSB_PersonInCharge
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_ETSB_PIC");
            }
        }

        public static string EWallet_PIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EWallet_PIC");
            }
        }

        //public static string ETSB_SMSBalanceAlert
        //{
        //    get
        //    {
        //        return CommonUtil.GetAppSettingValue("SMS_ETSBBalanceAlert");
        //    }
        //}

        //SYTan : Added EBW-1584
        #region "EBW-1584 - Loas Booking Alert"
        public static string LA_TrainProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_LA_TrainProductPIC");
            }
        }

        public static string LA_BusProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_LA_BusProductPIC");
            }
        }

        public static string LA_CarProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_LA_CarProductPIC");
            }
        }

        public static string LA_TourProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_LA_TourProductPIC");
            }
        }

        public static string LA_FerryProductPIC
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_LA_FerryProductPIC");
            }
        }
        #endregion

        public static string CommonException
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_CommonException");
            }
        }

        public static string BOT_PersonInCharge
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_BOT_PIC");
            }
        }

        public static string VioletteTrain_PersonInCharge
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_VioletteTrain_PIC");
            }
        }

        public static string KST_PersonInCharge
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_KonsortiumPIC");
            }
        }

        public static string BOT_APIMonitorAlertTeam
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_BOT_APIMonitorAlertTeam");
            }
        }

        public static string KTMB_APIMonitorAlertTeam
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_KTMB_APIMonitorAlertTeam");
            }
        }

        public static string KTMB_LowBalanceAlertRecipient
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_KTMB_LowBalanceAlertRecipient");
            }
        }

        public static string MemberLogRecipient
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_MemberLogRecipient");
            }
        }

        public static string KLIAExpressPersonInCharge
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_KLIAExpressPIC");
            }
        }

        public static string KLIAExpressNotificationRecipient
        {
            get
            {
                return CommonUtil.GetAppSettingValue("EmailAddress_KLIAExpressNotificationRecipient");
            }
        }

        public static readonly string ProductOrderSummaryPersonInCharge =
            string.Join(",",
                    string.Join(",", new string[] { BusPersonInCharge, CarPersonInCharge, FerryPersonInCharge, TrainPersonInCharge, TourPersonInCharge })
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct()
                );

        public static readonly string TourOverallMail =
            string.Join(",",
                string.Join(",", new string[] { TourBusinessTeam, TourPersonInCharge })
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct()
                );

        // An elegant way for you to concatenate email addresses. Use it please.
        public static string Add(this string previousEmailAddress, string emailAddressToBeAdded)
        {
            if (!previousEmailAddress.IsNullOrWhiteSpace() && !emailAddressToBeAdded.IsNullOrWhiteSpace())
                return $"{previousEmailAddress},{emailAddressToBeAdded}";
            else if (previousEmailAddress.IsNullOrWhiteSpace())
                return emailAddressToBeAdded;
            else
                return previousEmailAddress;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class EmailUtil
    {
        internal class SMTP_AWS
        {
            public static string SERVER
            {
                get
                {
                    return CommonUtil.GetAppSettingValue("SMTP_AWS_Server");
                }
            }

            public static int MAIL_PORT
            {
                get
                {
                    return ConversionUtil.ToInteger(CommonUtil.GetAppSettingValue("SMTP_AWS_ServerNetMailPort"), 0);
                }
            }

            public static bool ENABLE_SSL
            {
                get
                {
                    return ConversionUtil.ToBoolean(CommonUtil.GetAppSettingValue("SMTP_AWS_EnableSSL"), false);
                }
            }

            public static string USERNAME
            {
                get
                {
                    return CommonUtil.GetAppSettingValue("SMTP_AWS_Username");
                }
            }

            public static string PASSWORD
            {
                get
                {
                    return CommonUtil.GetAppSettingValue("SMTP_AWS_Password");
                }
            }
        }
        internal class SMTP_AWS_IMPORTANT
        {
            public static string SERVER
            {
                get
                {
                    return CommonUtil.GetAppSettingValue("SMTP_AWS_IMPORTANT_Server");
                }
            }

            public static int MAIL_PORT
            {
                get
                {
                    return ConversionUtil.ToInteger(CommonUtil.GetAppSettingValue("SMTP_AWS_IMPORTANT_ServerNetMailPort"), 0);
                }
            }

            public static bool ENABLE_SSL
            {
                get
                {
                    return ConversionUtil.ToBoolean(CommonUtil.GetAppSettingValue("SMTP_AWS_IMPORTANT_EnableSSL"), false);
                }
            }

            public static string USERNAME
            {
                get
                {
                    return CommonUtil.GetAppSettingValue("SMTP_AWS_IMPORTANT_Username");
                }
            }

            public static string PASSWORD
            {
                get
                {
                    return CommonUtil.GetAppSettingValue("SMTP_AWS_IMPORTANT_Password");
                }
            }
        }

        internal class SMTP_AZURE
        {
            public static string SERVER
            {
                get
                {
                    return CommonUtil.GetAppSettingValue("SMTP_AZURE_Server");
                }
            }

            public static int MAIL_PORT
            {
                get
                {
                    return ConversionUtil.ToInteger(CommonUtil.GetAppSettingValue("SMTP_AZURE_ServerNetMailPort"), 0);
                }
            }

            public static bool ENABLE_SSL
            {
                get
                {
                    return ConversionUtil.ToBoolean(CommonUtil.GetAppSettingValue("SMTP_AZURE_EnableSSL"), false);
                }
            }

            public static string USERNAME
            {
                get
                {
                    return CommonUtil.GetAppSettingValue("SMTP_AZURE_Username");
                }
            }

            public static string PASSWORD
            {
                get
                {
                    return CommonUtil.GetAppSettingValue("SMTP_AZURE_Password");
                }
            }
        }

        //Konsortium
        internal class SMTP_KST
        {
            public static string SERVER
            {
                get
                {
                    return CommonUtil.GetAppSettingValue("SMTP_KST_Server");
                }
            }

            public static int MAIL_PORT
            {
                get
                {
                    return ConversionUtil.ToInteger(CommonUtil.GetAppSettingValue("SMTP_KST_ServerNetMailPort"), 0);
                }
            }

            public static bool ENABLE_SSL
            {
                get
                {
                    return ConversionUtil.ToBoolean(CommonUtil.GetAppSettingValue("SMTP_KST_EnableSSL"), false);
                }
            }

            public static string USERNAME
            {
                get
                {
                    return CommonUtil.GetAppSettingValue("SMTP_KST_Username");
                }
            }

            public static string PASSWORD
            {
                get
                {
                    return CommonUtil.GetAppSettingValue("SMTP_KST_Password");
                }
            }
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="recipient"></param>
        public static void routeSuggestion_SendEmail(string subject, string body, string recipient = "")
        {
            try
            {
                var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                    EnableSsl = SMTP_AWS.ENABLE_SSL
                };

                body = $"<html><body><div style='font-family:monospace;'>{body.Replace("\r\n", "<br>")}</div></body></html>";

                var message = new MailMessage(
                    EmailAddress.Enquiry, recipient.IsNullOrWhiteSpace() ? EmailAddress.Error : recipient,
                    $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}]-{subject}", body)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                client.Send(message);
            }
            catch (Exception ex)
            {
                // Absorb
            }
        }

        public static void charterOrdering_SendEmail(string subject, string body, string recipient = "", string ccEmail = "", string bccEmail = "")
        {
            try
            {
                var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                    EnableSsl = SMTP_AWS.ENABLE_SSL
                };

                body = $"<html><body><div style='font-family:monospace;'>{body.Replace("\r\n", "<br>")}</div></body></html>";

                var message = new MailMessage(
                    EmailAddress.Enquiry, recipient.IsNullOrWhiteSpace() ? EmailAddress.Error : recipient,
                    $"{subject}", body)
                {
                    From = new MailAddress(EmailAddress.Enquiry, EmailAddress.SenderName_Easybook),
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                if (!string.IsNullOrEmpty(ccEmail))
                    message.CC.Add(ccEmail);

                if (!string.IsNullOrEmpty(bccEmail))
                    message.Bcc.Add(bccEmail);

                client.Send(message);
            }
            catch (Exception ex)
            {
                // Absorb
            }
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="recipient"></param>
        public static void SendEmail(string subject, string body, string recipient = "")
        {
            try
            {
                var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                    EnableSsl = SMTP_AWS.ENABLE_SSL
                };

                body = $"<html><body><div style='font-family:monospace;'>{body.Replace("\r\n", "<br>").Replace(" ", "&nbsp;")}</div></body></html>";

                var message = new MailMessage(
                    EmailAddress.Enquiry, recipient.IsNullOrWhiteSpace() ? EmailAddress.Error : recipient,
                    $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}]-{subject}", body)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                client.Send(message);
            }
            catch (Exception ex)
            {
                // Absorb
            }
        }

        public static void SendEmailWithCC(string subject, string body, string recipient, string ccEmail)
        {
            try
            {
                var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                    EnableSsl = SMTP_AWS.ENABLE_SSL
                };

                var message = new MailMessage(EmailAddress.Enquiry, recipient, subject, body)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                if (!string.IsNullOrEmpty(ccEmail))
                    message.CC.Add(ccEmail);

                client.Send(message);
            }
            catch (Exception ex)
            {
                //LogUtil.Error(ex);
                // ignored
            }

        }

        public static void SendInsuranceEmailWithCC(string subject, string body, string recipient, string ccEmail, string attachmentPath = "")
        {
            try
            {
                var client = new SmtpClient(SMTP_AZURE.SERVER, SMTP_AZURE.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AZURE.USERNAME, SMTP_AZURE.PASSWORD),
                    EnableSsl = SMTP_AZURE.ENABLE_SSL
                };

                var message = new MailMessage(EmailAddress.Enquiry, recipient, subject, body)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                if (!string.IsNullOrEmpty(ccEmail))
                    message.CC.Add(ccEmail);

                AddInsuranceAttachment(attachmentPath, message);

                client.Send(message);
            }
            catch (Exception ex)
            {
                //LogUtil.Error(ex);
                // ignored
            }
        }

        public static void SendImportantEmailWithCC(string subject, string body, string recipient, string ccEmail, string attachmentPath = "")
        {
            try
            {
                var client = new SmtpClient(SMTP_AWS_IMPORTANT.SERVER, SMTP_AWS_IMPORTANT.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AWS_IMPORTANT.USERNAME, SMTP_AWS_IMPORTANT.PASSWORD),
                    EnableSsl = SMTP_AWS_IMPORTANT.ENABLE_SSL
                };

                var message = new MailMessage(EmailAddress.Enquiry, recipient, subject, body)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                if (!string.IsNullOrEmpty(ccEmail))
                    message.CC.Add(ccEmail);

                AddAttachment(attachmentPath, message);

                client.Send(message);
            }
            catch (Exception ex)
            {
                //LogUtil.Error(ex);
                // ignored
            }
        }

        public static void SendEmailWithMultipleCC(string subject, string body, string recipient, string ccEmail)
        {
            try
            {
                var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                    EnableSsl = SMTP_AWS.ENABLE_SSL
                };

                var message = new MailMessage(EmailAddress.Enquiry, recipient, subject, body)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                if (!string.IsNullOrEmpty(ccEmail))
                {
                    if (ccEmail.Contains(";"))
                    {
                        var emails = ccEmail.Split(';').ToList();
                        foreach (var email in emails)
                            message.CC.Add(email);
                    }
                    else
                    {
                        message.CC.Add(ccEmail);
                    }
                }

                client.Send(message);
            }
            catch (Exception ex)
            {
                //LogUtil.Error(ex);
                // ignored
            }
        }

        /// <summary>
        /// Gets the stack trace message.
        /// </summary>
        /// <param name="message">Exception or custom message.</param>
        /// <param name="memberName">Name of the source file.</param>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="sourceLineNumber">The source line number.</param>
        /// <returns></returns>
        public static string GetTraceMessage(string message,
                    [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                    [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                    [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            string result = "Exception Message: " + message + Environment.NewLine +
                "Method: " + memberName + Environment.NewLine +
                "File: " + sourceFilePath.Replace("\\", "|").Replace("|", "/") + Environment.NewLine +
                "Line: " + sourceLineNumber.ToString();
            return result;
        }

        public static void SendEmailv2(string subject, string body, string recipient)
        {
            SendEmailv2(subject, body, recipient, "", "", true);
        }

        public static void SendEmailWithoutBCCStorage(string subject, string body, string recipient)
        {
            SendEmailv2(subject, body, recipient, "", "", false);
        }

        public static void SendEmailWithCCOnly(string subject, string body, string recipient, string cc)
        {
            SendEmailv2(subject, body, recipient, cc, "", true);
        }

        public static void SendEmailWithBCCOnly(string subject, string body, string recipient, string bcc)
        {
            SendEmailv2(subject, body, recipient, "", bcc, true);
        }

        public static void SendEmailWithBCCOnlyAttachment(string subject, string body, string recipient, string bcc, string fullFileName, string formatFileName)
        {
            SendEmailWithAttachment(subject, body, recipient, "", bcc, fullFileName, formatFileName);
        }

        public static void SendSystemExceptionNotificationEmail(string moduleName, string methodName, string fileName,
            string exceptionMessage, string stackTrace, string customMessage)
        {
            string strBody = "The exception was thrown at the method " + methodName + " in the file " + fileName + ".<br /><br />";
            strBody += "Exception Message:<br />" + exceptionMessage + "<br /><br />";
            strBody += "Exception Stack Trace:<br />" + stackTrace + "<br /><br />";
            strBody += "Easibook Developer Message:<br />" + customMessage + "<br /><br />";
            SendEmailv2("OMG! Easibook Throws Exception: " + moduleName, strBody,
                EmailAddress.CommonException, "", "", false);
        }

        public static void SendBOT_APIExceptionAlertEmail(string api, int companyId, string returnErrorMessage, string exception, string scheduleGuid = "", string cartGuid = "", string bookingReference = "")
        {
            try
            {
                var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                    EnableSsl = SMTP_AWS.ENABLE_SSL
                };

                string emailBody = "<html><body><div style='font-family:monospace;'>";
                emailBody += string.IsNullOrEmpty(scheduleGuid) ? "" : $"ScheduleGuid: {scheduleGuid} <br/>";
                emailBody += string.IsNullOrEmpty(cartGuid) ? "" : $"Cart GUID: {cartGuid} <br/>";
                emailBody += string.IsNullOrEmpty(bookingReference) ? "" : $"Booking Reference: {bookingReference} <br/>";
                emailBody += $"Company ID: {companyId} <br/>";
                emailBody += $"Returned Error message: {returnErrorMessage} <br/>";
                emailBody += $"Exception: {exception} <br/>";
                emailBody += "</div></body></html>";

                var message = new MailMessage(
                    EmailAddress.Enquiry, EmailAddress.BOT_APIMonitorAlertTeam,
                    $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}-BOT_API {api} FAILED", emailBody)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                client.Send(message);
            }
            catch (Exception ex)
            {
                // Absorb
            }
        }

        public static void SendKTMB_APIExceptionAlertEmail(string api, string request, string returnErrorMessage, string exception, string cartGuid = "", string bookingReference = "")
        {
            try
            {
                var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                    EnableSsl = SMTP_AWS.ENABLE_SSL
                };

                string emailBody = "<html><body><div style='font-family:monospace;'>";
                emailBody += string.IsNullOrEmpty(cartGuid) ? "" : $"Cart GUID: {cartGuid} <br/>";
                emailBody += string.IsNullOrEmpty(bookingReference) ? "" : $"Booking Reference: {bookingReference} <br/>";
                emailBody += $"Request: {request} <br/>";
                emailBody += $"Returned Error message: {returnErrorMessage} <br/>";
                emailBody += $"Exception: {exception} <br/>";
                emailBody += "</div></body></html>";

                var message = new MailMessage(
                    EmailAddress.Enquiry, EmailAddress.KTMB_APIMonitorAlertTeam,
                    $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}-KTMB_API {api} FAILED. {(string.IsNullOrEmpty(cartGuid) ? "" : "Cart GUID: " + cartGuid)}", emailBody)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                client.Send(message);
            }
            catch (Exception ex)
            {
                // Absorb
            }
        }

        public static void SendSRTAutomationBookingExceptionAlertEmail(string operationType, string request, string returnErrorMessage, string exception, string cartGuid = "", string bookingReference = "")
        {
            try
            {
                var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                    EnableSsl = SMTP_AWS.ENABLE_SSL
                };

                string emailBody = "<html><body><div style='font-family:monospace;'>";
                emailBody += string.IsNullOrEmpty(cartGuid) ? "" : $"Cart GUID: {cartGuid} <br/>";
                emailBody += string.IsNullOrEmpty(bookingReference) ? "" : $"Booking Reference: {bookingReference} <br/>";
                emailBody += $"Request: {request} <br/>";
                emailBody += $"Returned Error message: {returnErrorMessage} <br/>";
                emailBody += $"Exception: {exception} <br/>";
                emailBody += "</div></body></html>";

                var message = new MailMessage(
                    EmailAddress.Enquiry, EmailAddress.KTMB_APIMonitorAlertTeam,
                    $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}-SRT_AutomationBooking- {operationType} FAILED. {(string.IsNullOrEmpty(cartGuid) ? "" : "Cart GUID: " + cartGuid)}", emailBody)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                client.Send(message);
            }
            catch (Exception ex)
            {
                // Absorb
            }
        }

        public static void SendKTMB_LowBalanceAlertEmail(string availableAmountLeft)
        {
            try
            {
                var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                    EnableSsl = SMTP_AWS.ENABLE_SSL
                };

                string emailBody = "<html><body><div style='font-family:monospace;'>";
                emailBody += "Hi team, our KTMB agent site account credit getting low! Please topup it ASAP! <br/>";
                emailBody += $"Balance left: {availableAmountLeft} <br/>";
                emailBody += "</div></body></html>";

                var message = new MailMessage(
                    EmailAddress.Enquiry, EmailAddress.KTMB_APIMonitorAlertTeam,
                    $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")} - [ALERT] KTMB Agent Account Balance LOW", emailBody)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                client.Send(message);
            }
            catch (Exception ex)
            {
                // Absorb
            }
        }

        public static void SendGlobalExceptionEmail(string subject, string body, string recipient)
        {
            SendEmailv2(subject, body, recipient, "", "", false);
        }

        public static void SendEmailv2(string subject, string body, string recipient, string cc, string bcc, bool bccToEasibookStorageEmail)
        {
            StringBuilder strProcessTrace = new StringBuilder();
            try
            {
                //make sure all addresses in good condition
                strProcessTrace.AppendLine("============================================");
                strProcessTrace.AppendLine("Step 1: Refine recipients, cc, bcc addresses");
                recipient = recipient.Replace(';', ',').Trim(',');
                //recipient = recipient[recipient.Length - 1] == ',' ? recipient.Substring(0, recipient.Length - 1) : recipient;
                cc = cc.Replace(';', ',').Trim(',');
                //cc = cc[cc.Length - 1] == ',' ? cc.Substring(0, cc.Length - 1) : cc;
                bcc = bcc.Replace(';', ',').Trim(',');
                //bcc = bcc[bcc.Length - 1] == ',' ? bcc.Substring(0, bcc.Length - 1) : bcc;

                strProcessTrace.AppendLine("Step 2: Creating SmtpClient");
                var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                    EnableSsl = SMTP_AWS.ENABLE_SSL
                };

                strProcessTrace.AppendLine("Step 3: Creating MailMessage");
                var message = new MailMessage(EmailAddress.Enquiry, recipient, subject, body)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                strProcessTrace.AppendLine("Step 4: Adding CC");
                if (!string.IsNullOrEmpty(cc))
                {
                    message.CC.Add(cc);
                }

                //Add storage into BCC if bccToEasibookStorageEmail is TRUE
                if (bccToEasibookStorageEmail && !subject.Contains("AXS") && !subject.Contains("Query Seat BOT") && !subject.Contains("Failed to block KTB"))
                {
                    bcc = EmailAddress.Add(bcc, EmailAddress.Storage);
                }
                strProcessTrace.AppendLine("Step 5: Adding BCC");
                if (!string.IsNullOrEmpty(bcc))
                {
                    message.Bcc.Add(bcc);
                }
                strProcessTrace.AppendLine("Step 6: Sending email");
                client.Send(message);
            }
            catch (Exception ex)
            {
                //System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-SendEmailv2-Subject: [{subject}]"));
            }
        }

        public static void SendEmailWithAttachment(string subject, string body, string recipient, string cc, string bcc, string fullFileName, string formatFileName)
        {
            StringBuilder strProcessTrace = new StringBuilder();
            
            //make sure all addresses in good condition
            strProcessTrace.AppendLine("============================================");
            strProcessTrace.AppendLine("Step 1: Refine recipients, cc, bcc addresses");
            recipient = recipient.Replace(';', ',').Trim(',');
            //recipient = recipient[recipient.Length - 1] == ',' ? recipient.Substring(0, recipient.Length - 1) : recipient;
            cc = cc.Replace(';', ',').Trim(',');
            //cc = cc[cc.Length - 1] == ',' ? cc.Substring(0, cc.Length - 1) : cc;
            bcc = bcc.Replace(';', ',').Trim(',');
            //bcc = bcc[bcc.Length - 1] == ',' ? bcc.Substring(0, bcc.Length - 1) : bcc;
            
            try
            {
                strProcessTrace.AppendLine("Step 2: Creating AZURE SmtpClient");
                var client = new SmtpClient(SMTP_AZURE.SERVER, SMTP_AZURE.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AZURE.USERNAME, SMTP_AZURE.PASSWORD),
                    EnableSsl = SMTP_AZURE.ENABLE_SSL
                };

                strProcessTrace.AppendLine("Step 3: Creating AZURE MailMessage");
                var message = new MailMessage(EmailAddress.Enquiry, recipient, subject, body)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                strProcessTrace.AppendLine("Step 4: Adding CC FOR AZURE");
                if (!string.IsNullOrEmpty(cc))
                {
                    message.CC.Add(cc);
                }

                strProcessTrace.AppendLine("Step 5: Adding BCC FOR AZURE");
                if (!string.IsNullOrEmpty(bcc))
                {
                    message.Bcc.Add(bcc);
                }

                if (fullFileName != "" && !IsFileLocked(fullFileName))
                {
                    System.Threading.Thread.Sleep(5000);

                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(fullFileName);

                    if (formatFileName != null && formatFileName != "")
                    {
                        attachment.Name = formatFileName;
                    }

                    strProcessTrace.AppendLine($"Step 6: Adding attachment FOR AZURE, Filename: {formatFileName}");
                    message.Attachments.Add(attachment);
                }

                strProcessTrace.AppendLine("Step 7: Sending email VIA AZURE");
                client.Send(message);
            }
            catch (Exception ex)
            {
                //System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-SendEmailWithAttachment-Subject: [{subject}]"));

                try
                {
                    strProcessTrace.AppendLine("Step 2: Creating SmtpClient");
                    var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                    {
                        Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                        EnableSsl = SMTP_AWS.ENABLE_SSL
                    };

                    strProcessTrace.AppendLine("Step 3: Creating MailMessage");
                    var message = new MailMessage(EmailAddress.Enquiry, recipient, subject, body)
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8
                    };

                    strProcessTrace.AppendLine("Step 4: Adding CC");
                    if (!string.IsNullOrEmpty(cc))
                    {
                        message.CC.Add(cc);
                    }

                    strProcessTrace.AppendLine("Step 5: Adding BCC");
                    if (!string.IsNullOrEmpty(bcc))
                    {
                        message.Bcc.Add(bcc);
                    }

                    if (fullFileName != "")
                    {
                        System.Net.Mail.Attachment attachment;
                        attachment = new System.Net.Mail.Attachment(fullFileName);

                        if (formatFileName != null && formatFileName != "")
                        {
                            attachment.Name = formatFileName;
                        }

                        strProcessTrace.AppendLine($"Step 6: Adding attachment, Filename: {formatFileName}");
                        message.Attachments.Add(attachment);
                    }

                    strProcessTrace.AppendLine("Step 7: Sending email");
                    client.Send(message);
                }
                catch (Exception e)
                {
                    //System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(e, strProcessTrace.ToString(), $"Exception-EmailUtil-SendEmailWithAttachment-Subject: [{subject}]"));
                }
            }
        }

        public static void SendEmailWithCCForSupplier(string subject, string body, string recipient, string ccEmail)
        {
            try
            {
                var client = new SmtpClient(SMTP_AZURE.SERVER, SMTP_AZURE.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AZURE.USERNAME, SMTP_AZURE.PASSWORD),
                    EnableSsl = SMTP_AZURE.ENABLE_SSL
                };

                var message = new MailMessage(EmailAddress.Enquiry, recipient, subject, body)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                if (!string.IsNullOrEmpty(ccEmail))
                    message.CC.Add(ccEmail);

                client.Send(message);
            }
            catch (Exception ex)
            {
                //LogUtil.Error(ex);
                try
                {
                    var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                    {
                        Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                        EnableSsl = SMTP_AWS.ENABLE_SSL
                    };

                    var message = new MailMessage(EmailAddress.Enquiry, recipient, subject, body)
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8
                    };

                    if (!string.IsNullOrEmpty(ccEmail))
                        message.CC.Add(ccEmail);

                    client.Send(message);
                }
                catch (Exception e)
                {
                    //LogUtil.Error(e);
                }
            }
        }
        public static void SendEmailWithAttachments(string subject, string body, string recipient, string cc, string bcc, List<Attachment> attachments, string sender = "")
        {
            StringBuilder strProcessTrace = new StringBuilder();
            try
            {
                //make sure all addresses in good condition
                strProcessTrace.AppendLine("============================================");
                strProcessTrace.AppendLine("Step 1: Refine recipients, cc, bcc addresses");
                recipient = recipient.Replace(';', ',').Trim(',');
                //recipient = recipient[recipient.Length - 1] == ',' ? recipient.Substring(0, recipient.Length - 1) : recipient;
                cc = cc.Replace(';', ',').Trim(',');
                //cc = cc[cc.Length - 1] == ',' ? cc.Substring(0, cc.Length - 1) : cc;
                bcc = bcc.Replace(';', ',').Trim(',');
                //bcc = bcc[bcc.Length - 1] == ',' ? bcc.Substring(0, bcc.Length - 1) : bcc;

                strProcessTrace.AppendLine("Step 2: Creating SmtpClient");
                var client = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
                    EnableSsl = SMTP_AWS.ENABLE_SSL
                };

                strProcessTrace.AppendLine("Step 3: Creating MailMessage");
                var message = new MailMessage(EmailAddress.Enquiry, recipient, subject, body)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                strProcessTrace.AppendLine("Step 4: Adding CC");
                if (!string.IsNullOrEmpty(cc))
                {
                    message.CC.Add(cc);
                }

                strProcessTrace.AppendLine("Step 5: Adding BCC");
                if (!string.IsNullOrEmpty(bcc))
                {
                    message.Bcc.Add(bcc);
                }

                strProcessTrace.AppendLine($"Step 6: Adding attachments");
                foreach (var attachment in attachments)
                {
                    message.Attachments.Add(attachment);
                }
                strProcessTrace.AppendLine("Step 7: Sending email");
                client.Send(message);
            }
            catch (Exception ex)
            {
                //System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-SendEmailWithAttachments-Subject: [{subject}]"));
            }
        }

        protected static bool IsFileLocked(string filePath)
        {
            FileStream stream = null;

            try
            {
                stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException ex)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                stream?.Close();
            }

            //file is not locked
            return false;
        }

        public static void SendEmailByWebsiteCompanyId(int websiteCompanyId, string body, string subject, string recipient, bool easibookOnly)
        {
            if (websiteCompanyId == 6)
            {
                SendKSTMail(body, subject, recipient, easibookOnly);
            }
            else
            {
                SendEmailv2(subject, body, recipient);
            }
        }

       

        public static string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);
            sbText.Replace("\r\n", string.Empty); sbText.Replace(" ", string.Empty);
            return sbText.ToString();
        }

        public static void SendKSTMail(string body, string subject, string recipient, bool easibookOnly)
        {
            StringBuilder strProcessTrace = new StringBuilder();
            try
            {
                //make sure all addresses in good condition
                strProcessTrace.AppendLine("============================================");
                strProcessTrace.AppendLine("Step 1: Refine recipients addresses");
                recipient = recipient.Replace(';', ',').Trim(',');
                //recipient = recipient[recipient.Length - 1] == ',' ? recipient.Substring(0, recipient.Length - 1) : recipient;

                strProcessTrace.AppendLine("Step 2: Creating SmtpClient");
                var client = new SmtpClient(SMTP_KST.SERVER, SMTP_KST.MAIL_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_KST.USERNAME, SMTP_KST.PASSWORD),
                    EnableSsl = SMTP_KST.ENABLE_SSL
                };

                strProcessTrace.AppendLine("Step 3: Creating MailMessage");
                var message = new MailMessage(SMTP_KST.USERNAME, recipient, subject, body)
                {
                    From = new MailAddress(SMTP_KST.USERNAME),
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                //BCC to easybook storage
                message.Bcc.Add(EmailAddress.Storage);

                if (!easibookOnly)
                {
                    message.CC.Add(EmailAddress.KST_PersonInCharge);
                }

                //message.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
                //message.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = SMTP_KST.USERNAME;
                //message.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = SMTP_KST.PASSWORD;

                strProcessTrace.AppendLine("Step 4: Sending email");
                client.Send(message);
            }
            catch (Exception ex)
            {
                //System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.Error(ex, $"Exception-EmailUtil-SendKSTMail-Subject: [{subject}]"));
            }
        }

        /*************************************************************************************************************/
        /*********************************** Do not touch anything beyond this point too******************************/
        /*************************************************************************************************************/

        //public static void Send(EmailTransferObject emailDetails)
        //{
        //    StringBuilder strProcessTrace = new StringBuilder();
        //    try
        //    {
        //        strProcessTrace.AppendLine("============================================");
        //        strProcessTrace.AppendLine("Step 1: Creating SmtpClient");
        //        var smtpClient = new SmtpClient(SMTP_AWS.SERVER, SMTP_AWS.MAIL_PORT)
        //        {
        //            Credentials = new NetworkCredential(SMTP_AWS.USERNAME, SMTP_AWS.PASSWORD),
        //            EnableSsl = SMTP_AWS.ENABLE_SSL
        //        };

        //        strProcessTrace.AppendLine("Step 2: Creating MailMessage");
        //        var mailMessage = new MailMessage(
        //            EmailAddress.Enquiry, emailDetails.RecipientAddress, emailDetails.Subject, emailDetails.Content)
        //        {
        //            IsBodyHtml = true,
        //            BodyEncoding = Encoding.UTF8
        //        };

        //        strProcessTrace.AppendLine("Step 3: Adding CC, BCC, and attachment");
        //        AddCarbonCopyAddress(emailDetails.CarbonCopyAddress, mailMessage);
        //        AddBlindCarbonCopyAddress(emailDetails.BlindCarbonCopyAddress, mailMessage);
        //        AddAttachment(emailDetails.AttachmentPath, mailMessage);

        //        if (emailDetails.CustomAttachmentList != null && emailDetails.CustomAttachmentList.Count > 0)
        //        {
        //            foreach (Attachment _attachment in emailDetails.CustomAttachmentList)
        //                mailMessage.Attachments.Add(_attachment);
        //        }

        //        strProcessTrace.AppendLine("Step 4: Sending email");
        //        //smtpClient.Send(mailMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-Send-Subject: [{emailDetails.Subject}]"));
        //    }
        //}

        /*************************************************************************************************************/
        /*********************************** Do not touch anything beyond this point *********************************/
        /*************************************************************************************************************/

        //public static void SendOrderSummary(EmailTransferObject emailDetails, bool isTaxInvoiceOnly = false, int companyId = 0)
        //{
        //    StringBuilder strProcessTrace = new StringBuilder();
        //    ////////////////////////////////////////////////////////////////////////////////
        //    // Agent Site CID:6 - Konsortium singapore use their own email server to send order summary
        //    if (companyId == 6)
        //    {
        //        try
        //        {
        //            //make sure all addresses in good condition
        //            strProcessTrace.AppendLine("============================================");
        //            strProcessTrace.AppendLine("Step 1: Creating SmtpClient");
        //            var smtpClient = new SmtpClient(SMTP_KST.SERVER, SMTP_KST.MAIL_PORT)
        //            {
        //                Credentials = new NetworkCredential(SMTP_KST.USERNAME, SMTP_KST.PASSWORD),
        //                EnableSsl = SMTP_KST.ENABLE_SSL
        //            };

        //            strProcessTrace.AppendLine("Step 2: Creating MailMessage");
        //            var mailMessage = new MailMessage(SMTP_KST.USERNAME, emailDetails.RecipientAddress, emailDetails.Subject, emailDetails.Content)
        //            {
        //                From = new MailAddress(SMTP_KST.USERNAME),
        //                IsBodyHtml = true,
        //                BodyEncoding = Encoding.UTF8,
        //            };

        //            strProcessTrace.AppendLine("Step 3: Adding CC, BCC, and attachment");
        //            // Expects either a single email address OR multiple email address separated by comma
        //            AddCarbonCopyAddress(emailDetails.CarbonCopyAddress, mailMessage);
        //            AddBlindCarbonCopyAddress(emailDetails.BlindCarbonCopyAddress, mailMessage);
        //            //Adding BCC to storage 1
        //            AddBlindCarbonCopyAddress(EmailAddress.Storage, mailMessage);

        //            // Skip attaching attachment if attachmentPath is empty/null
        //            if (emailDetails.AttachmentPath.IsNotNullAndWhiteSpace())
        //            {
        //                if (isTaxInvoiceOnly)
        //                    AddTaxInvoiceAttachment(emailDetails.AttachmentPath, mailMessage);
        //                else
        //                    AddAttachment(emailDetails.AttachmentPath, mailMessage);
        //            }

        //            strProcessTrace.AppendLine("Step 4: Adding custom attachment");
        //            if (emailDetails.CustomAttachmentList != null && emailDetails.CustomAttachmentList.Count > 0)
        //            {
        //                foreach (var attachment in emailDetails.CustomAttachmentList)
        //                {
        //                    mailMessage.Attachments.Add(attachment);
        //                }
        //            }

        //            strProcessTrace.AppendLine("Step 4: Sending email");
        //            //smtpClient.SendCompleted += SendEmailCompletedCallback;
        //            //smtpClient.SendAsync(mailMessage, emailDetails);
        //            //smtpClient.Send(mailMessage);
        //            smtpClient.SendCompleted += (s, e) =>
        //            {
        //                var details = e.UserState as EmailTransferObject;
        //                LogUtil.LogEmail(e.Error?.ToString() ?? "Successful", emailDetails.CartGuid, JsonConvert.SerializeObject(emailDetails));
        //                try
        //                {
        //                    if (mailMessage != null)
        //                        mailMessage.Dispose();
        //                }
        //                catch (Exception ex)
        //                {
        //                    System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-SendOrderSummary-CID-6-smtpClient.SendCompleted"));
        //                }
        //            };
        //            //smtpClient.SendAsync(mailMessage, emailDetails);
        //        }
        //        catch (Exception ex)
        //        {
        //            System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-SendOrderSummary-CID-6"));
        //        }
        //    }
        //    ////////////////////////////////////////////////////////////////////////////////
        //    else
        //    {
        //        //-------------- Send OS using AWS SMTP -------------//

        //        try
        //        {
        //            strProcessTrace.AppendLine("============================================");
        //            strProcessTrace.AppendLine("Step 1: Creating SmtpClient");
        //            var smtpClient = new SmtpClient(SMTP_AWS_IMPORTANT.SERVER, SMTP_AWS_IMPORTANT.MAIL_PORT)
        //            {
        //                Credentials = new NetworkCredential(SMTP_AWS_IMPORTANT.USERNAME, SMTP_AWS_IMPORTANT.PASSWORD),
        //                EnableSsl = SMTP_AWS_IMPORTANT.ENABLE_SSL
        //            };

        //            strProcessTrace.AppendLine("Step 2: Creating MailMessage");
        //            var mailMessage = new MailMessage(EmailAddress.Enquiry, emailDetails.RecipientAddress, emailDetails.Subject, emailDetails.Content)
        //            {
        //                From = new MailAddress(EmailAddress.Enquiry, EmailAddress.SenderName_Easybook),
        //                IsBodyHtml = true,
        //                BodyEncoding = Encoding.UTF8
        //            };

        //            strProcessTrace.AppendLine("Step 3: Adding CC, BCC, and attachment");
        //            // Expects either a single email address OR multiple email address separated by comma
        //            AddCarbonCopyAddress(emailDetails.CarbonCopyAddress, mailMessage);
        //            AddBlindCarbonCopyAddress(emailDetails.BlindCarbonCopyAddress, mailMessage);
        //            //Adding BCC to storage 1
        //            AddBlindCarbonCopyAddress(EmailAddress.Storage, mailMessage);

        //            // Skip attaching attachment if attachmentPath is empty/null
        //            if (emailDetails.AttachmentPath.IsNotNullAndWhiteSpace())
        //            {
        //                if (isTaxInvoiceOnly)
        //                    AddTaxInvoiceAttachment(emailDetails.AttachmentPath, mailMessage);
        //                else
        //                    AddAttachment(emailDetails.AttachmentPath, mailMessage);
        //            }

        //            strProcessTrace.AppendLine("Step 4: Adding custom attachment");
        //            if (emailDetails.CustomAttachmentList != null && emailDetails.CustomAttachmentList.Count > 0)
        //            {
        //                foreach (var attachment in emailDetails.CustomAttachmentList)
        //                {
        //                    mailMessage.Attachments.Add(attachment);
        //                }
        //            }

        //            // Check inline attachments for images
        //            if (emailDetails.LinkedResources != null && emailDetails.LinkedResources.Count > 0)
        //                mailMessage.AlternateViews.Add(AddInlineAttachments(emailDetails));

        //            strProcessTrace.AppendLine("Step 4: Sending email");
        //            //smtpClient.SendCompleted += SendEmailCompletedCallback;
        //            //smtpClient.SendAsync(mailMessage, emailDetails);
        //            //smtpClient.Send(mailMessage);
        //            smtpClient.SendCompleted += (s, e) =>
        //            {
        //                var details = e.UserState as EmailTransferObject;
        //                LogUtil.LogEmail(e.Error?.ToString() ?? "Successful", emailDetails.CartGuid, JsonConvert.SerializeObject(emailDetails));
        //                try
        //                {
        //                    if (mailMessage != null)
        //                        mailMessage.Dispose();
        //                }
        //                catch (Exception ex)
        //                {
        //                    System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-smtpClient.SendCompleted"));
        //                }
        //            };
        //            //smtpClient.SendAsync(mailMessage, emailDetails);
        //        }
        //        catch (Exception ex)
        //        {
        //            System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-SendOrderSummary-Detail: [{JsonConvert.SerializeObject(emailDetails)}]"));
        //        }

        //        //delay the email sending if OS/Invoice file is locked
        //        int waitingAttemptCounter = 0;
        //        while (waitingAttemptCounter < 3 && emailDetails.AttachmentPath.IsNotNullAndWhiteSpace() && IsFileLocked(emailDetails.AttachmentPath))
        //        {
        //            System.Threading.Thread.Sleep(5000);
        //            waitingAttemptCounter++;
        //        }

        //        //-------------- Send OS using Azure SMTP -------------//
        //        strProcessTrace = new StringBuilder();
        //        try
        //        {
        //            strProcessTrace.AppendLine("============================================");
        //            strProcessTrace.AppendLine("Step 1: Creating SmtpClient");
        //            var smtpClient = new SmtpClient(SMTP_AZURE.SERVER, SMTP_AZURE.MAIL_PORT)
        //            {
        //                Credentials = new NetworkCredential(SMTP_AZURE.USERNAME, SMTP_AZURE.PASSWORD),
        //                EnableSsl = SMTP_AZURE.ENABLE_SSL
        //            };

        //            strProcessTrace.AppendLine("Step 2: Creating MailMessage");
        //            var mailMessage = new MailMessage(EmailAddress.Enquiry, emailDetails.RecipientAddress, emailDetails.Subject, emailDetails.Content)
        //            {
        //                From = new MailAddress(EmailAddress.Enquiry, EmailAddress.SenderName_Easybook),
        //                IsBodyHtml = true,
        //                BodyEncoding = Encoding.UTF8
        //            };

        //            strProcessTrace.AppendLine("Step 3: Adding CC, BCC, and attachment");
        //            // Expects either a single email address OR multiple email address separated by comma
        //            AddCarbonCopyAddress(emailDetails.CarbonCopyAddress, mailMessage);
        //            AddBlindCarbonCopyAddress(emailDetails.BlindCarbonCopyAddress, mailMessage);
        //            //Adding BCC to storage 2
        //            AddBlindCarbonCopyAddress(EmailAddress.Storage2, mailMessage);

        //            // Skip attaching attachment if attachmentPath is empty/null
        //            if (emailDetails.AttachmentPath.IsNotNullAndWhiteSpace())
        //            {
        //                if (isTaxInvoiceOnly)
        //                    AddTaxInvoiceAttachment(emailDetails.AttachmentPath, mailMessage);
        //                else
        //                    AddAttachment(emailDetails.AttachmentPath, mailMessage);
        //            }

        //            strProcessTrace.AppendLine("Step 4: Adding custom attachment");
        //            if (emailDetails.CustomAttachmentList != null && emailDetails.CustomAttachmentList.Count > 0)
        //            {
        //                foreach (var attachment in emailDetails.CustomAttachmentList)
        //                {
        //                    mailMessage.Attachments.Add(attachment);
        //                }
        //            }

        //            // Check inline attachments for images
        //            if (emailDetails.LinkedResources != null && emailDetails.LinkedResources.Count > 0)
        //                mailMessage.AlternateViews.Add(AddInlineAttachments(emailDetails));

        //            strProcessTrace.AppendLine("Step 4: Sending email");
        //            //smtpClient.SendCompleted += SendEmailCompletedCallback;
        //            //smtpClient.SendAsync(mailMessage, emailDetails);
        //            //smtpClient.Send(mailMessage);

        //            smtpClient.SendCompleted += (s, e) =>
        //            {
        //                var details = e.UserState as EmailTransferObject;
        //                LogUtil.LogEmail(e.Error?.ToString() ?? "Successful", emailDetails.CartGuid, JsonConvert.SerializeObject(emailDetails));

        //                try
        //                {
        //                    if (mailMessage != null)
        //                        mailMessage.Dispose();
        //                }
        //                catch (Exception ex)
        //                {
        //                    System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-smtpClient.SendCompleted"));
        //                }
        //            };
        //            //smtpClient.SendAsync(mailMessage, emailDetails);
        //        }
        //        catch (Exception ex)
        //        {
        //            System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-SendOrderSummary-Detail: [{JsonConvert.SerializeObject(emailDetails)}]"));
        //        }
        //    }
        //}

        ///*************************************************************************************************************/
        ///*********************************** Do not touch anything beyond this point *********************************/
        ///*************************************************************************************************************/

        //public static void SendEmailWithTwoServer(EmailTransferObject emailDetails, bool bccToEasibookStorageEmail = false)
        //{
        //    //-------------- Send OS using AWS SMTP -------------//
        //    StringBuilder strProcessTrace = new StringBuilder();
        //    try
        //    {
        //        strProcessTrace.AppendLine("============================================");
        //        strProcessTrace.AppendLine("Step 1: Creating SmtpClient");
        //        var smtpClient = new SmtpClient(SMTP_AWS_IMPORTANT.SERVER, SMTP_AWS_IMPORTANT.MAIL_PORT)
        //        {
        //            Credentials = new NetworkCredential(SMTP_AWS_IMPORTANT.USERNAME, SMTP_AWS_IMPORTANT.PASSWORD),
        //            EnableSsl = SMTP_AWS_IMPORTANT.ENABLE_SSL
        //        };

        //        strProcessTrace.AppendLine("Step 2: Creating MailMessage");
        //        var mailMessage = new MailMessage(EmailAddress.Enquiry, emailDetails.RecipientAddress, emailDetails.Subject, emailDetails.Content)
        //        {
        //            From = new MailAddress(EmailAddress.Enquiry, EmailAddress.SenderName_Easybook),
        //            IsBodyHtml = true,
        //            BodyEncoding = Encoding.UTF8
        //        };

        //        strProcessTrace.AppendLine("Step 3: Adding CC, BCC, and attachment");
        //        // Expects either a single email address OR multiple email address separated by comma
        //        AddCarbonCopyAddress(emailDetails.CarbonCopyAddress, mailMessage);
        //        AddBlindCarbonCopyAddress(emailDetails.BlindCarbonCopyAddress, mailMessage);
        //        //Adding BCC to storage 1
        //        if (bccToEasibookStorageEmail)
        //            AddBlindCarbonCopyAddress(EmailAddress.Storage, mailMessage);

        //        strProcessTrace.AppendLine("Step 4: Adding custom attachment");
        //        if (emailDetails.CustomAttachmentList != null && emailDetails.CustomAttachmentList.Count > 0)
        //        {
        //            foreach (var attachment in emailDetails.CustomAttachmentList)
        //            {
        //                mailMessage.Attachments.Add(attachment);
        //            }
        //        }

        //        strProcessTrace.AppendLine("Step 4: Sending email");
        //        //smtpClient.SendCompleted += SendEmailCompletedCallback;
        //        //smtpClient.SendAsync(mailMessage, emailDetails);
        //        //smtpClient.Send(mailMessage);
        //        smtpClient.SendCompleted += (s, e) =>
        //        {
        //            var details = e.UserState as EmailTransferObject;
        //            LogUtil.LogEmail(e.Error?.ToString() ?? "Successful", emailDetails.CartGuid, JsonConvert.SerializeObject(emailDetails));
        //            try
        //            {
        //                if (mailMessage != null)
        //                    mailMessage.Dispose();
        //            }
        //            catch (Exception ex)
        //            {
        //                System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-SendEmailWithTwoServer-smtpClient.SendCompleted"));
        //            }
        //        };
        //        //smtpClient.SendAsync(mailMessage, emailDetails);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-SendEmailWithTwoServer-Detail: [{JsonConvert.SerializeObject(emailDetails)}]"));
        //    }

        //    //delay the email sending if OS/Invoice file is locked
        //    int waitingAttemptCounter = 0;
        //    while (waitingAttemptCounter < 3 && emailDetails.AttachmentPath.IsNotNullAndWhiteSpace() && IsFileLocked(emailDetails.AttachmentPath))
        //    {
        //        System.Threading.Thread.Sleep(5000);
        //        waitingAttemptCounter++;
        //    }

        //    //-------------- Send OS using Azure SMTP -------------//
        //    strProcessTrace = new StringBuilder();
        //    try
        //    {
        //        strProcessTrace.AppendLine("============================================");
        //        strProcessTrace.AppendLine("Step 1: Creating SmtpClient");
        //        var smtpClient = new SmtpClient(SMTP_AZURE.SERVER, SMTP_AZURE.MAIL_PORT)
        //        {
        //            Credentials = new NetworkCredential(SMTP_AZURE.USERNAME, SMTP_AZURE.PASSWORD),
        //            EnableSsl = SMTP_AZURE.ENABLE_SSL
        //        };

        //        strProcessTrace.AppendLine("Step 2: Creating MailMessage");
        //        var mailMessage = new MailMessage(EmailAddress.Enquiry, emailDetails.RecipientAddress, emailDetails.Subject, emailDetails.Content)
        //        {
        //            From = new MailAddress(EmailAddress.Enquiry, EmailAddress.SenderName_Easybook),
        //            IsBodyHtml = true,
        //            BodyEncoding = Encoding.UTF8
        //        };

        //        strProcessTrace.AppendLine("Step 3: Adding CC, BCC, and attachment");
        //        // Expects either a single email address OR multiple email address separated by comma
        //        AddCarbonCopyAddress(emailDetails.CarbonCopyAddress, mailMessage);
        //        AddBlindCarbonCopyAddress(emailDetails.BlindCarbonCopyAddress, mailMessage);
        //        //Adding BCC to storage 2
        //        if (bccToEasibookStorageEmail)
        //            AddBlindCarbonCopyAddress(EmailAddress.Storage2, mailMessage);

        //        strProcessTrace.AppendLine("Step 4: Adding custom attachment");
        //        if (emailDetails.CustomAttachmentList != null && emailDetails.CustomAttachmentList.Count > 0)
        //        {
        //            foreach (var attachment in emailDetails.CustomAttachmentList)
        //            {
        //                mailMessage.Attachments.Add(attachment);
        //            }
        //        }

        //        strProcessTrace.AppendLine("Step 4: Sending email");
        //        //smtpClient.SendCompleted += SendEmailCompletedCallback;
        //        //smtpClient.SendAsync(mailMessage, emailDetails);
        //        //smtpClient.Send(mailMessage);

        //        smtpClient.SendCompleted += (s, e) =>
        //        {
        //            var details = e.UserState as EmailTransferObject;
        //            LogUtil.LogEmail(e.Error?.ToString() ?? "Successful", emailDetails.CartGuid, JsonConvert.SerializeObject(emailDetails));

        //            try
        //            {
        //                if (mailMessage != null)
        //                    mailMessage.Dispose();
        //            }
        //            catch (Exception ex)
        //            {
        //                System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-SendEmailWithTwoServer-smtpClient.SendCompleted"));
        //            }
        //        };
        //        //smtpClient.SendAsync(mailMessage, emailDetails);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Threading.Tasks.Task.Factory.StartNew(() => LogUtil.ErrorWithConditionalEmailv2(ex, strProcessTrace.ToString(), $"Exception-EmailUtil-SendEmailWithTwoServer-Detail: [{JsonConvert.SerializeObject(emailDetails)}]"));
        //    }
        //}

        //private static void SendEmailCompletedCallback(object sender, AsyncCompletedEventArgs e)
        //{
        //    var emailDetails = e.UserState as EmailTransferObject;

        //    LogUtil.LogEmail(e.Error?.ToString() ?? "Successful", emailDetails.CartGuid, JsonConvert.SerializeObject(emailDetails));
        //}

        private static void AddCarbonCopyAddress(string carbonCopyAddress, MailMessage mailMessage)
        {
            if (carbonCopyAddress.IsNotNullAndWhiteSpace() && VerifyEmailAddressForOrderSummary(carbonCopyAddress))
            {
                mailMessage.CC.Add(carbonCopyAddress);
            }
        }

        private static void AddBlindCarbonCopyAddress(string blindCarbonCopyAddress, MailMessage mailMessage)
        {
            if (blindCarbonCopyAddress.IsNotNullAndWhiteSpace() && VerifyEmailAddressForOrderSummary(blindCarbonCopyAddress))
            {
                mailMessage.Bcc.Add(blindCarbonCopyAddress);
            }
        }

        private static void AddInsuranceAttachment(string attachmentPath, MailMessage mailMessage)
        {
            if (attachmentPath.IsNotNullAndWhiteSpace() && IsFileLocked(attachmentPath))
            {
                System.Threading.Thread.Sleep(5000);
            }

            if (attachmentPath.IsNullOrWhiteSpace() || IsFileLocked(attachmentPath)) { return; }

            mailMessage.Attachments.Add(new Attachment(attachmentPath) { Name = "Insurance Voucher.pdf" });
        }

        private static void AddAttachment(string attachmentPath, MailMessage mailMessage)
        {
            if (attachmentPath.IsNotNullAndWhiteSpace() && IsFileLocked(attachmentPath))
            {
                System.Threading.Thread.Sleep(5000);
            }

            if (attachmentPath.IsNullOrWhiteSpace() || IsFileLocked(attachmentPath)) { return; }

            mailMessage.Attachments.Add(new Attachment(attachmentPath) { Name = "Order Summary.pdf" });
        }

        private static void AddTaxInvoiceAttachment(string attachmentPath, MailMessage mailMessage)
        {
            if (attachmentPath.IsNotNullAndWhiteSpace() && IsFileLocked(attachmentPath))
            {
                System.Threading.Thread.Sleep(5000);
            }

            if (attachmentPath.IsNullOrWhiteSpace() || IsFileLocked(attachmentPath)) { return; }

            mailMessage.Attachments.Add(new Attachment(attachmentPath) { Name = "Tax Invoice.pdf" });
        }

        private static bool VerifyEmailAddressForOrderSummary(string emailAddress)
        {
            var emailAddresses = emailAddress.Split(new[] { "," }, StringSplitOptions.None);

            return emailAddresses.All(IsValidEmailAddress);
        }

        /// <summary>
        /// Determines whether a single email address provided is valid.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>
        ///   <c>true</c> if [is valid email address] [the specified email address]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidEmailAddress(string emailAddress)
        {
            try
            {
                if (emailAddress.IsNullOrWhiteSpace() || emailAddress.Equals("-")) { return false; }

                // Can avoid using Regex which is awesome
                var mailAddress = new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}