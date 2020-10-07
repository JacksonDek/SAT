using System.Web.Mvc;
using SAT.UI.MVC.Models;
using System.Net.Mail; // mail
using System.Net; // mail
using System; // built in esception handling

namespace SAT.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            // Check that the cvm data just passed into the method is a calid ContactViewModel
            if (ModelState.IsValid)
            {
                // Build the message

                string message = $"You have recieved an email from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br/>{cvm.Message}";

                MailMessage mm = new MailMessage("admin@yoursite.com", "admin@yoursite.com", cvm.Subject, message);
                // MailMessage Properties
                // Allow HTML formatting in the Email
                mm.IsBodyHtml = true;
                mm.Priority = MailPriority.High;
                // Respond to the sender and not the admin@yoursiteurl.com
                mm.ReplyToList.Add(cvm.Email);
                mm.CC.Add("yourCCperson@yoursite.com");

                // SmtpClient - This is the infor from the host that allows this message to be sent
                SmtpClient client = new SmtpClient("mail.yourmaildomain.com");

                // Client Credentials
                client.Credentials = new NetworkCredential("admin@yoursite.com", "Your Password"); // Clear test password is NOT best practice

                // Set the SMTP port if needed
                client.Port = 8889;

                // Try to send the email
                try
                {
                    // Attempt to send 
                    client.Send(mm);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"We're sorry your request could not be completed at the time. Please try again later. Error Message: <br/> {ex.StackTrace}";
                    return View(cvm);
                }

                // Everything has been sent, so send the user to a confirmation page that is also strongly typed 
                return View("EmailConfirmation", cvm);
            }
            else // The ModelState is NOT valid
            {
                // Send back the form with the completed inputs so the user can try again and not have to re-type everything
                return View(cvm);
            }

        }
    }
}
