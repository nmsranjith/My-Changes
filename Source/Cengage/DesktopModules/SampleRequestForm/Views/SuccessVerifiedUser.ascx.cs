using System;
using System.Collections.Generic;
using Cengage.eCommerce.Lib;

namespace DotNetNuke.Modules.SampleRequestForm.Views
{
    public partial class SuccessVerifiedUser : SampleRequestFormModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LogValues(Request.QueryString["fc"]);
                switch (Request.QueryString["fc"])
                {
                    case "em1":
                        Error_Msg1.Visible = true;
                        break;
                    case "em2":
                        Error_Msg2.Visible = true;
                        break;
                    case "em3":
                        Error_Msg3.Visible = true;
                        break;
                    case "em4":
                        Error_Msg4.Visible = true;
                        break;
                    case "em5":
                        Error_Msg5.Visible = true;
                        break;
                    case "em6":
                        Error_Msg6.Visible = true;
                        break;
                    case "em7":
                        Error_Msg7.Visible = true;
                        break;
                    case "em8":
                        Error_Msg8.Visible = true;
                        break;
                    case "em9":
                        Error_Msg9.Visible = true;
                        break;
                    case "em10":
                        Error_Msg10.Visible = true;
                        break;
                    case "em11":
                        Error_Msg11.Visible = true;
                        break;
                    case "em12":
                        Error_Msg12.Visible = true;
                        break;
                    case "em13":
                        Error_Msg13.Visible = true;
                        break;
                    case "em14":
                        Error_Msg14.Visible = true;
                        break;
                    default:
                        SuccessHeader.Visible = true;
                        break;
                }     
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