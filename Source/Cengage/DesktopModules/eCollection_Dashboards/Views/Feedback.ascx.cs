using System;
using System.Configuration;
using System.Net.Mail;
using System.Collections.Generic;

namespace DotNetNuke.Modules.eCollection_Dashboards.Views
{
    public partial class Feedback : eCollection_DashboardsModuleBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FeedBackSubmitbutton_Click(object sender, EventArgs e)
        {
            try
            {
                FeedBackSuccess.Visible = true;
                FeedBackFormContent.Visible = false;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmailAddress"], ConfigurationManager.AppSettings["AdminName"]);
                m.Subject = "FeedBack";
                m.Body = string.Concat("<html><body>Name:    " + NameTextBox.Value + "<br/>Email:    " + UserEmail.Value + "<br/>Phone:    " + SignupPartnerPhone.Value + "<br/>FeedBack:<p>" + CommentsTextArea.Text + "</p></body></html>");
                m.To.Add(ConfigurationManager.AppSettings["ToAddress"]);
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["EmailDomainName"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["NetworkCredentialUserName"], ConfigurationManager.AppSettings["NetworkCredentialPassword"]);
                smtp.Send(m);            
            }
            catch (Exception ex)
            {
                //Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }
    }
}