using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace MailAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        //Send Mail to Acquainteck
        [HttpPost]
        public JsonResult sendEmailViaWebApi(string body)
        {
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            try
            {
                m.From = new MailAddress("info.acquainteck@gmail.com");
                m.To.Add("info.acquainteck@gmail.com");
                m.Subject = "Mail From Website";
                m.IsBodyHtml = true;
                m.Body = body;
                sc.Host = "smtp.gmail.com";
                sc.Port = 587;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new System.Net.NetworkCredential("info.acquainteck@gmail.com", "Acqua#2018");
                sc.EnableSsl = false;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.Send(m);
                return Json("Thank you for contacting us. We will respond to you as soon as possible", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Ohh ! Something went wrong. Message could not sent."+"<br/>"+ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public string SendEmail(string from,string mail,string msg,string fullname,string bmail,string company,string contact,string job,string drpreson,string concern)
        {
            try
            {
                string body = this.PopulateBody("Acquainteck",
                "Mail from Webience",
                "https://demowebience.acquainteck.com",
                "You have one Enquiry from Webience" + "<br>" +
                "Mail Sent by using contact form" +"<br>"+ from +"<br>"+ mail +"<br>" + msg + "<br>" + fullname + "<br>" + bmail + "<br>" + company + "<br>" + contact +"<br>" + job + "<br>" + drpreson +"<br>" + concern);
                this.SendHtmlFormattedEmail("info.acquainteck@gmail.com", "Enquiry from Webience", body);
                return "Mail sent successfully !";
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
            
        }

        [HttpPost]
        public string PopulateBody(string userName, string title, string url, string description)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{UserName}", userName);
                body = body.Replace("{Title}", title);
                body = body.Replace("{Url}", url);
                body = body.Replace("{Description}", description);
                return body;
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
            
        }

        public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(recepientEmail));
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ConfigurationManager.AppSettings["Host"];
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                    NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                    smtp.Send(mailMessage);
                }

            }
            catch(Exception ex)
            {
                //return exception
            }
        }


        //for acquainteck
        [HttpPost]
        public string SendEmailAcqua(string name, string mail, string comment)
        {
            try
            {
                string body = this.PopulateBodyAcqua("Acquainteck",
                "Mail from Acquauinteck Website",
                "https://acquainteck.com/",
                "You have one Enquiry from Acquainteck Website" + "<br>" +
                "Mail Sent by using contact form" + "<br>"+ name + "<br>" +  mail + "<br>" + comment + "<br>");
                this.SendHtmlFormattedEmail("info.acquainteck@gmail.com", "Enquiry from Acquainteck", body);
                return "Mail sent successfully !";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        [HttpPost]
        public string PopulateBodyAcqua(string userName, string title, string url, string description)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplateAcqua.htm")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{UserName}", userName);
                body = body.Replace("{Title}", title);
                body = body.Replace("{Url}", url);
                body = body.Replace("{Description}", description);
                return body;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

    }
}
