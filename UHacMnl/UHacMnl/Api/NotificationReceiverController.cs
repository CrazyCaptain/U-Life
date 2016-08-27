using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UHacMnl.Models;

namespace UHacMnl.Api
{
    public class NotificationReceiverController : ApiController
    {
        public async Task<HttpResponseMessage> Receive(ChikkaOutgoingSms sms)
        {
            try
            {
                if (sms.shortcode == ConfigurationManager.AppSettings["ChikkaShortCode"])
                {
                    ApplicationDbContext db = new ApplicationDbContext();
                    SmsRecipient smsRecipient = db.SmsRecipients.Find(sms.message_id);

                    switch (sms.status)
                    {
                        case "SENT":
                            smsRecipient.Status = (int)UHacMnl.Sms.SmsStatusCode.Success;
                            break;
                        case "FAILED":
                            smsRecipient.Status = (int)UHacMnl.Sms.SmsStatusCode.Error;
                            break;
                        default:
                            break;
                    }

                    db.Entry(smsRecipient).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return new HttpResponseMessage(HttpStatusCode.Accepted);

                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
