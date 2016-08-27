using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using UHacMnl.Models;

namespace UHacMnl
{

    public class TelcoPrefix
    {
        public static string[] Globe = { "817", "905", "906", "915", "916", "917", "926", "927", "935", "936", "937", "977", "994", "996", "997" };
        public static string[] Smart = { "813", "907", "908", "909", "910", "912", "918", "919", "920", "921", "928", "929", "930", "938", "939", "947", "948", "949", "989", "998" };
        public static string[] Sun = { "922", "923", "925", "932", "933", "934", "942", "943" };
    }

    public class Sms
    {
        public static string getMessage(string message)
        {
            return message.Trim();
        }

        public class SmsFailure
        {
            /// <summary>
            /// Initializes a new instance of the SmsFailure class.
            /// </summary>
            /// <param name="recipientId">The target recipient.</param>
            /// <param name="statusCode">The status code of the sms.</param>
            public SmsFailure(int recipientId, int statusCode)
            {
                this.RecpientId = recipientId;

                switch (statusCode)
                {
                    case -1:
                        this.Status = new Status()
                        {
                            Code = (int)SmsStatusCode.Pending,
                            Message = Enum.GetName(typeof(SmsStatusCode), SmsStatusCode.Pending)
                        };
                        break;
                    case 0:
                        this.Status = new Status()
                        {
                            Code = (int)SmsStatusCode.Success,
                            Message = Enum.GetName(typeof(SmsStatusCode), SmsStatusCode.Success)
                        };
                        break;
                    case 1:
                        this.Status = new Status()
                        {
                            Code = (int)SmsStatusCode.Error,
                            Message = Enum.GetName(typeof(SmsStatusCode), SmsStatusCode.Error)
                        };
                        break;
                    default:
                        //this.Status = new Status() { Code = (int)SmsStatusCode., Message = Enum.GetName(typeof(SmsStatusCode), SmsStatusCode.) }; };
                        break;
                }
            }

            public int RecpientId { get; set; }
            public Status Status { get; set; }
        }

        public class Status
        {
            public int Code { get; set; }
            public string Message { get; set; }
        }

        /// <summary>
        /// Contains the values of status codes for App7 Sms.
        /// </summary>
        public enum SmsStatusCode
        {
            /// <summary>
            /// Sms is awaiting processing.
            /// </summary>
            Pending = -1,
            /// <summary>
            /// Sms has been successfully sent.
            /// </summary>
            Success = 0,
            /// <summary>
            /// Sms has encountered an error.
            /// </summary>
            Error = 1
        }
    }

    public class Chikka
    {
        public class Constants
        {
            public const string RequestUrl = "https://post.chikka.com/smsapi/request";
            public const string RequestCost = "P1.00";
        }

        public static string chikkaPost(int messageId, string message, string contactNumber)
        {
            using (WebClient wc = new WebClient())
            {
                ChikkaSendSms chikkaSms = new ChikkaSendSms(messageId, message, contactNumber);
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                return wc.UploadString(Chikka.Constants.RequestUrl, chikkaSms.ToString());
            }
        }

    }


}