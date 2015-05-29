using System;
using System.Collections.Generic;
using Cengage.eCommerce.Lib;

namespace DotNetNuke.Modules.SampleRequestForm.Views
{
    public partial class SuccessNonVerifiedUser : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CoreFormatProducts"] != null)
                {
                    List<SRFItems> formatItems = Session["CoreFormatProducts"] as List<SRFItems>;
                    ContactRep.HRef = string.Concat("/contact-us/div/", Request.QueryString["div"]);
                    if (formatItems != null && formatItems.Count > 0)
                    {
                        SecLvlDisciplineLnk.HRef = formatItems[0].SecondLevelUrl;
                        SecLvlDisciplineLnk.InnerText = formatItems[0].SecondLevelDiscipline;
                        ThirdLvlDisciplineLnk.HRef = formatItems[0].ThirdLevelUrl;
                        ThirdLvlDisciplineLnk.InnerText = formatItems[0].ThirdLevelDiscipline;
                        SRFProductName.InnerText = formatItems[0].ProductName;
                    }
                }
                else
                {
                    Response.Redirect("/search?q=&division=" + Request.QueryString["div"]);
                }
            }
            Session["CoreFormatProducts"] = null;
        }
    }
}