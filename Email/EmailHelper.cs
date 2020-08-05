//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mail;
//using MailKit.Net.Smtp;
//using System.Threading.Tasks;
//using SmtpClient = MailKit.Net.Smtp.SmtpClient;
//using MimeKit;

//namespace Identity.Email
//{
//    public class EmailHelper
//    {
//        #region Send Email
//        //public bool SendEmail(string userEmail, string confirmationLink)
//        //{
//        //    MailMessage mailMessage = new MailMessage();
//        //    mailMessage.From = new MailAddress("care@yogihosting.com");
//        //    mailMessage.To.Add(new MailAddress(userEmail));

//        //    mailMessage.Subject = "Confirm your email";
//        //    mailMessage.IsBodyHtml = true;
//        //    mailMessage.Body = confirmationLink;

//        //    SmtpClient client = new SmtpClient();
//        //    client.Credentials = new System.Net.NetworkCredential("info@rainpuddleslabradoodles.com", "Mydoodles!");
//        //    client.Host = "smtpout.secureserver.net";
//        //    client.Port = 80;

//        //    try
//        //    {
//        //        client.Send(mailMessage);
//        //        return true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // log exception
//        //    }
//        //    return false;
//        //}
//        #endregion
        
 
//        public class Message
//        {
//            public List<MailboxAddress> To { get; set; }
//            public string Subject { get; set; }
//            public string Content { get; set; }

//            public Message(IEnumerable<string> to, string subject, string content)
//            {
//                To = new List<MailboxAddress>();

//                To.AddRange(to.Select(x => new MailboxAddress(x)));
//                Subject = subject;
//                Content = content;
//            }
//        }
//        public MimeMessage CreateEmailMessage(Message message)
//        {          
//            var emailMessage = new MimeMessage();
//            emailMessage.From.Add(new MailboxAddress("noreply@fs.com"));
//            emailMessage.To.AddRange(message.To);
//            emailMessage.Subject = message.Subject;
//            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

//            return emailMessage;
//        }
//        public bool SendEmailPasswordReset(MimeMessage mailMessage)
//        {
//            using (var client = new SmtpClient())
//            {
//                try
//                {
//                    client.Connect("smtp.gmail.com", 465, true);
//                    client.AuthenticationMechanisms.Remove("XOAUTH2");
//                    client.Authenticate("edawarekaro@gmail.com", "edaware1999");
                
//                    client.Send(mailMessage);
//                    return true;
//                }
//                catch(Exception ex)
//                {
//                    //log an error message or throw an exception or both.
//                    throw;
//                }
//                finally
//                {
//                    client.Disconnect(true);
//                    client.Dispose();
//                }
//            }
//        }

//        public bool SendEmail(Message message)
//        {
//            try
//            {
//                var emailMessage = CreateEmailMessage(message);

//                SendEmailPasswordReset(emailMessage);
//                return true;
//            }
//            catch(Exception ex)
//            {
//                return false;
//            }
           
//        }

//        #region SendEmailTwoFactorCode
//        //public bool SendEmailTwoFactorCode(string userEmail, string code)
//        //{
//        //    MailMessage mailMessage = new MailMessage();
//        //    mailMessage.From = new MailAddress("care@yogihosting.com");
//        //    mailMessage.To.Add(new MailAddress(userEmail));

//        //    mailMessage.Subject = "Two Factor Code";
//        //    mailMessage.IsBodyHtml = true;
//        //    mailMessage.Body = code;

//        //    SmtpClient client = new SmtpClient();
//        //    client.Credentials = new System.Net.NetworkCredential("info@rainpuddleslabradoodles.com", "Mydoodles!");
//        //    client.Host = "smtpout.secureserver.net";
//        //    client.Port = 80;

//        //    try
//        //    {
//        //        client.Send(mailMessage);
//        //        return true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // log exception
//        //    }
//        //    return false;
//        //}
//        #endregion
//    }
//}
