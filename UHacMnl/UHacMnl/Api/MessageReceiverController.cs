using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UHacMnl.Models;

namespace UHacMnl.Api
{
    public class MessageReceiverController : ApiController
    {
        public HttpResponseMessage Receive(ChikkaIncomingSms sms)
        {
            if (sms.shortcode != ConfigurationManager.AppSettings["ChikkaShortCode"])
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            ApplicationDbContext db = new ApplicationDbContext();

            Int32 subscriberId = 1;
            String requestCost = "P1.00";
            String responseMessage = null;

            String[] customVariable = sms.message.Split(new char[] { ',' }, 3);
            String type = customVariable[0];
            String FirstName = customVariable[1];
            String LastName = customVariable[2];


            if (type.Equals("REGISTER", StringComparison.InvariantCultureIgnoreCase))
            {

                responseMessage = "You have been registered, you have been charged P1.00.";
                Subscriber subscriber = new Subscriber() { FirstName = FirstName, LastName = LastName, ContactNumber = sms.mobile_number.Substring(2) };
                db.Subscribers.Add(subscriber);
                db.SaveChanges();
                subscriberId = subscriber.SubscriberId;
            }
            else if (type.Equals("INSURE", StringComparison.InvariantCultureIgnoreCase))
            {
                Subscriber subscriber = db.Subscribers.SingleOrDefault(m => m.ContactNumber == sms.mobile_number.Substring(2));
                subscriberId = subscriber.SubscriberId;
                requestCost = "P15.00";
                responseMessage = "You have insured 1 certificate for P15, you have been charged P1.00 for this text.";

            }
            else if (type.Equals("INQUIRE", StringComparison.InvariantCultureIgnoreCase))
            {
                Subscriber subscriber = db.Subscribers.SingleOrDefault(m => m.ContactNumber == sms.mobile_number.Substring(2));
                subscriberId = subscriber.SubscriberId;
                Int32 certs = subscriber.CertificatesLeft;
                responseMessage = "You have " + certs + " certificates.You have been charged P1.00.";

            }
            else if (type.Equals("CLAIM", StringComparison.InvariantCultureIgnoreCase))
            {
                Subscriber subscriber = db.Subscribers.SingleOrDefault(m => m.ContactNumber == sms.mobile_number.Substring(2));
                subscriberId = subscriber.SubscriberId;

                customVariable = sms.message.Split(new char[] { ',' }, 4);
                type = customVariable[0];
                Int32 certs;
                if (Int32.TryParse(customVariable[1], out certs))
                {
                    FirstName = customVariable[2];
                    LastName = customVariable[3];

                    responseMessage = "You have claimed " + certs + "certs. You have been charged P1.00.";
                }
                else
                {
                    responseMessage = "\'" + customVariable[1] + "\' is invalid. You have been charged P1.00.";
                }
            }


            SmsReceive smsReceive = new SmsReceive()
            {
                Message = sms.message,
                RequestId = sms.request_id,
                Timestamp = sms.timestamp_datetime,
                SubscriberId = subscriberId
            };

            db.SmsReceives.Add(smsReceive);
            db.SaveChanges();
            //}
            SmsMessage smsMessage = new SmsMessage()
            {
                Timestamp = DateTime.UtcNow.AddHours(8),
                Message = sms.message
            };
          
            SmsReply smsReply = new SmsReply()
            {
                RequestId = sms.request_id,
                SmsReceiveId = smsReceive.Id,
                Timestamp = DateTime.Now,
                Status = (int)UHacMnl.Sms.SmsStatusCode.Pending,
                SmsMessage = smsMessage,
                SubscriberId = 1
            };
            db.SmsReplies.Add(smsReply);
            db.SaveChanges();

            // Reply
            using (WebClient wc = new WebClient())
            {
                string upload = string.Empty;
                try
                {
                    ChikkaReplySms chikkaSms = new ChikkaReplySms(smsReply.SmsMessageId.ToString(), responseMessage, sms.request_id, sms.mobile_number, requestCost);
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string chikkaUpload = chikkaSms.ToString();
                    upload = wc.UploadString(Chikka.Constants.RequestUrl, chikkaUpload);
                }
                catch (Exception e)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}
