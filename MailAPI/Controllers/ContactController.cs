using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Mvc;

namespace MailAPI.Controllers
{
    public class ContactController : ApiController
    {
        [System.Web.Mvc.HttpPost]
        public ActionResult SendEmailViaWebApi(string body)
        {
            
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("info.acquainteck@gmail.com");
                mail.To.Add("info.acquainteck@gmail.com");
                mail.Subject = "Message from website";
                mail.Body = "Test Mail";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("info.acquainteck@gmail.com", "Acqua#2018");
                SmtpServer.EnableSsl = false;
                SmtpServer.Send(mail);
                return Json(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>("Mail Sent"));
            }
            catch (Exception ex)
            {
                return Json(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>("Mail Not Sent"+ex.Message));
            }

        }
    }
}
