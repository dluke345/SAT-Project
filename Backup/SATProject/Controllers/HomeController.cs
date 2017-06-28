using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SATProject.Models;
using System.Net.Mail;

namespace ResponsiveConversion3Lab.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactData data)
        {
            if (ModelState.IsValid)
            {
                //try catch for sending mail
                try
                {
                    MailMessage m = new MailMessage("dluke@lukeskydeveloper.com",
                        "dluke345@gmail.com", "Email from WG MVC",
                        string.Format("{0} has sent you the following message: <br /><br />{1}" +
                            "The email address to which you should respond is {2}",
                            data.Name, data.Comments, data.Email));

                    //allow you to respond to the actual sender of the email instead
                    //of your website
                    m.ReplyToList.Add(data.Email);
                    m.IsBodyHtml = true;
                    m.Priority = MailPriority.High;

                    //create the client
                    SmtpClient client = new SmtpClient(
                        "relay-hosting.secureserver.net");

                    //send
                    //uncomment this out when deployed to GoDaddy
                    //client.Send(m);

                    ViewBag.Message = "Your message has been sent";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "We were unable to send your message.";
                    ViewBag.ExceptionMessage = ex.StackTrace;
                }
                return View("ContactThanks", data);
            }
            else
            {
                return View();
            }
        }
    }
}
