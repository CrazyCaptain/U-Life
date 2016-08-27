using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHacMnl.Models
{
    public class SmsMessage
    {
        public Int32 Id { get; set; }
        //public String ChikkaMessageId { get; set; }
        public String Message { get; set; }
        //public Int32 SubscriberId { get; set; }
        //public virtual Subscriber Subscriber { get; set; }
        public DateTime Timestamp { get; set; }

        //public int Status { get; set; }
        public SmsMessage ShallowCopy()
        {
            return (SmsMessage)this.MemberwiseClone();
        }
    }

    public class SmsRecipient
    {
        public int SmsRecipientId { get; set; }
        public int Status { get; set; }

        public int SubscriberId { get; set; }
        public virtual Subscriber Subscriber { get; set; }

        public int SmsMessageId { get; set; }
        public virtual SmsMessage SmsMessage { get; set; }
    }

    public class SmsReceive
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public int SubscriberId { get; set; }
        public virtual Subscriber Subscriber { get; set; }
    }

    public class SmsReply
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public DateTime Timestamp { get; set; }
        public int Status { get; set; }
        public Int32 SubscriberId { get; set; }
        public string IdHex
        {
            get
            {
                return Id.ToString("X");
            }
        }

        public int SmsReceiveId { get; set; }
        public int SmsMessageId { get; set; }

        public virtual SmsReceive SmsReceive { get; set; }
        public virtual SmsMessage SmsMessage { get; set; }
    }
}