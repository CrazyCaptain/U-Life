using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UHacMnl.Models
{
    public class ChikkaIncomingSms
    {
        public string message_type { get; set; }
        public string mobile_number { get; set; }
        public string shortcode { get; set; }
        public string request_id { get; set; }
        public string message { get; set; }
        public double timestamp { get; set; }
        public DateTime timestamp_datetime
        {
            get
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
            }
        }
    }

    public class ChikkaReplySms
    {
        /// <summary>
        /// Initializes a new instance of the ChikkaReplySms class
        /// </summary>
        ///// <param name="messageId">Unique Id to be used for Chikka Api.</param>
        ///// <param name="message">Message sent to the recipients.</param>
        ///// <param name="contactNumber">Contact number of the recipient.</param>
        public ChikkaReplySms(string messageId, string message, string requestId, string contactNumber, string requestCost)
        {
            this.message_type = "REPLY";
            this.message_id = messageId;
            this.mobile_number = contactNumber;
            this.request_id = requestId;
            this.message = message;
            this.request_cost = requestCost;
        }

        public string message_type { get; set; }
        public string mobile_number { get; set; }
        public string request_id { get; set; }
        public string message_id { get; set; }
        public string message { get; set; }
        public string request_cost { get; set; }

        /// <summary>
        /// Returns a the data string for Chikka Api.
        /// </summary>
        public override string ToString()
        {
            return String.Format(
                "message_type={0}&mobile_number={1}&request_id={2}&message_id={3}&message={4}&request_cost={5}&shortcode={6}&client_id={7}&secret_key={8}",
                message_type,
                mobile_number,
                request_id,
                message_id,
                message,
                request_cost,
                ConfigurationManager.AppSettings["ChikkaShortCode"],
                ConfigurationManager.AppSettings["ChikkaClientId"],
                ConfigurationManager.AppSettings["ChikkaSecretKey"]);
        }
    }

    public class ChikkaSendSms
    {
        /// <summary>
        /// Initializes a new instance of the ChikkaSendSms class.
        /// </summary>
        /// <param name="messageId">Unique Id to be used for Chikka Api.</param>
        /// <param name="message">Message sent to the recipients.</param>
        /// <param name="contactNumber">Contact number of the recipient.</param>
        public ChikkaSendSms(int messageId, string message, string contactNumber)
        {
            this.message_type = "SEND";
            this.message_id = messageId;
            this.message = message;
            this.mobile_number = contactNumber;
        }

        public string message_type { get; set; }
        public string mobile_number { get; set; }
        public int message_id { get; set; }
        public string message { get; set; }

        /// <summary>
        /// Returns a the data string for Chikka Api.
        /// </summary>
        public override string ToString()
        {
            return String.Format("message_type={0}&mobile_number=63{1}&message_id={2}&message={3}&shortcode={4}&client_id={5}&secret_key={6}",
                "SEND",
                mobile_number,
                message_id,
                message,
                ConfigurationManager.AppSettings["ChikkaShortCode"],
                ConfigurationManager.AppSettings["ChikkaClientId"],
                ConfigurationManager.AppSettings["ChikkaSecretKey"]);
        }
    }

    public class ChikkaOutgoingSms
    {
        public string message_type { get; set; }
        public string shortcode { get; set; }
        public string message_id { get; set; }
        public string status { get; set; }
        public double credits_cost { get; set; }
        public double timestamp { get; set; }
        public DateTime timestamp_datetime
        {
            get
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
            }
        }
    }

}