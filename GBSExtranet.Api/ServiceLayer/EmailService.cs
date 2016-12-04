using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Web.Hosting;
using System.ServiceModel;
using GBSExtranet.Api.ViewModel;
using Business;
using System.Data;

namespace GBSExtranet.Api.ServiceLayer 
{
    public class EmailService : BaseService
    {
        
        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                try
                {
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_Server"]);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(recepientEmail));

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = ConfigurationManager.AppSettings["SMTP_Mail"];
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PortNo"]);
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);

                    //bool isLocalHost = bool.Parse(ConfigurationManager.AppSettings["IsLocalHost"]);
                    //if (!isLocalHost)
                    //{
                        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                        NetworkCred.UserName = ConfigurationManager.AppSettings["SMTP_Server"];
                        NetworkCred.Password = ConfigurationManager.AppSettings["SMTP_Password"];
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                    //}

                    smtp.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    throw new FaultException("Unable to email to reset password." + ex.Message);
                }
            }
        }

        private string PopulateBody(string userName, string url)
        {
            string body = string.Empty;
            try
            {
                string fileName = string.Empty;
               
                fileName = HostingEnvironment.MapPath("~/Email/MailTemplateEnglish.html");
                
                //else
                //{
                //    fileName = HostingEnvironment.MapPath("~/Email/EmailTemplate.htm");
                //}
                using (StreamReader reader = new StreamReader(fileName))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{name}", userName);
                body = body.Replace("{action_url}", url);
            }
            catch (Exception ex)
            {
                throw new FaultException("Unable to email to reset password." + ex.Message);
            }

            return body;
        }

        //public void SendEmail(UserData oUserData)
        //{
        //    try
        //    {
        //        //  log.Debug("Url traking");

        //        //   log.Debug("Url==========" + System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/");
        //        // log.Debug("Url==========" + System.Web.HttpContext.Current.Request.FilePath);

        //        //var url1 = OperationContext.Current.Channel.LocalAddress.Uri.AbsoluteUri + "SetPassword.aspx?UserId = " + oUserData.Id;
        //        //  var baseUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
        //        // var url = baseUrl + "SetPassword.aspx?UserId=" + oUserData.Id + "&Reset=true";

        //        //var url = "http://actionhtml5.itsat.com/api/" + "Email/SetPassword.aspx?UserId=" + oUserData.UserId;

        //        var url = "https://users.itsat.com/Development/" + "Email/SetPassword.aspx?UserId=" + oUserData.Id;

        //        string body = this.PopulateBody(oUserData.UserFullName, url, false);
        //        this.SendHtmlFormattedEmail(oUserData.UserId, "New user created!", body);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new FaultException("Unable to email to set password." + ex.Message);
        //    }
        //}
        public void SendResetPasswordEmail(User oUserData)
        {
            try
            {
                //log.Debug("Url traking");

                // log.Debug("Url==========" +  System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/");
                //log.Debug("Url==========" + System.Web.HttpContext.Current.Request.FilePath);

                //var url1 = OperationContext.Current.Channel.LocalAddress.Uri.AbsoluteUri + "SetPassword.aspx?UserId = " + oUserData.Id;
                //var baseUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";

                //var url = baseUrl + "Email/SetPassword.aspx?UserId=" + oUserData.Id;
                //var url = "http://actionhtml5.itsat.com/api/" + "Email/SetPassword.aspx?UserId=" + oUserData.UserId;
              //  var url = "https://users.itsat.com/Development/" + "Email/SetPassword.aspx?UserId=" + oUserData.ID;

                var url = "http://www.gdshotels.com/" + "SetPassword-en?UserID=" + oUserData.ID;
                string body = this.PopulateBody(oUserData.Name+" "+ oUserData.Surname, url);
                this.SendHtmlFormattedEmail(oUserData.Email, "Password reset", body);
            }
            catch (Exception ex)
            {
                throw new FaultException("Unable to email to reset password." + ex.Message);
            }
        }
    }
}
