using System;
using System.Net.Mail;
using System.Configuration;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using System.Data;

namespace DotNetNuke.Modules.eCollection_Dashboards.Views
{
    public partial class RequestAccess : eCollection_DashboardsModuleBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = DashboardController.Instance.GetCountries();
                PostalAddressCountryDropDownList.DataSource = dt;
                PostalAddressCountryDropDownList.DataBind(); 
            }
            catch (Exception ex)
            {
                //DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                //Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RequestEarlyAccessbutton_Click(object sender, EventArgs e)
        {
            try
            {
                RequestAccessConfirmationContent.Visible = true;
                RequestAccessContent.Visible = false;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmailAddress"], ConfigurationManager.AppSettings["AdminName"]);
                m.Subject = "Request Access";
                m.Body = string.Concat("<html><body>Contact Information<br/>Name:    " + NameTextBox.Value + "<br/>Email:    " + UserEmail.Value + "<br/>Phone:    " + SignupPartnerPhone.Value + "<br/>School Information<br/>School Name:    " + SchoolName.Value + "<br/>School PhoneNumber:    " + SchoolPhoneNumber.Value + "<br/>Address:    " + (SignUpPartnerAddress.Value != "line 1" ? SignUpPartnerAddress.Value : string.Empty) + "<br/>" + (Line1Address.Value != "line 2" ? Line1Address.Value : string.Empty) + "Suburb:    " + SuburbTextBox.Value + "<br/>PostCode:    " + PostCodeTextBox.Value + "<br/>State:    " + PostalAddressStateDropDownList.Items[PostalAddressStateDropDownList.SelectedIndex] + "<br/>Country:    " + PostalAddressCountryDropDownList.SelectedValue + "<br/>Other:    " + (OtherCountryTextBox.Value != "Please specify which country" ? OtherCountryTextBox.Value : string.Empty) + "<br/>Comments:    <p>" + CommentsTextArea.Text + "</p></body></html>");
                m.To.Add(ConfigurationManager.AppSettings["ToAddress"]);
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["EmailDomainName"]);
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["NetworkCredentialUserName"], ConfigurationManager.AppSettings["NetworkCredentialPassword"]);
                smtp.Send(m);
            }
            catch (Exception ex)
            {
                //DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                //Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        } 
    }
}