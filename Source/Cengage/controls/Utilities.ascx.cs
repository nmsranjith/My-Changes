using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using System.Configuration;
using System.Text;
using System.IO;
using DotNetNuke.Instrumentation;
using System.Web.UI.HtmlControls;
using Cengage.eCommerce.Lib;
using Cengage.eCommerce.CountryDetection;

using Cengage.Ecommerce.List.Components.Model;
using Cengage.Ecommerce.List.Components.Common;
using Cengage.Ecommerce.List.Components.Controller;
using System.Data;
using Cengage.Ecommerce.CountryDetection.Components.Controllers;

public partial class controls_Utilities : System.Web.UI.UserControl
{
    string fileName, sandBox, strIpAddress = string.Empty;
    Visitor visitor;
    protected void Page_Load(object sender, EventArgs e)
    {
        Login();
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Logout();
    }


    private void Login()
    {
        try
        {
            if (Session["cengageusername"] != null)
            {
                HtmlGenericControl LoginMessage = new HtmlGenericControl();
                LoginMessage = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("LoginMessage");
                LoginMessage.Visible = true;
                LoginMessage.Style.Add("display", "block");

                HtmlGenericControl LoginUserName = new HtmlGenericControl();
                LoginUserName = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("LoginUserName");
                LoginUserName.InnerHtml = Session["cengageusername"].ToString();
            }
        }
        catch (Exception ex)
        {
            DnnLog.Error("exception in setting obj .. " + ex.Message);
        }
        Visitor user = new Visitor();

        if (Session["UserInfo"] != null)
        {
            user = (Visitor)(Session["UserInfo"]);
            if (user.CengageUser != 0) Session["Visitor"] = "visited";
            if (user.CountryName != null)
            {
                if (user.CountryCode == "--")
                {
                    CountryName.InnerHtml = "<span class='b-dropdown-img'></span>" + "AUSTRALIA";
                    user.CountryCode = "AU";
                    user.CountryName = "Australia";
                    DataSet countryRestrictionDataset;
                    DataRow StoreMessages = null;
                    CountryController CountryController = new CountryController();
                    countryRestrictionDataset = CountryController.GetCountryRestrictions(user.CountryCode);
                    if (countryRestrictionDataset.Tables[0].Rows.Count > 0)
                    {
                        StoreMessages = countryRestrictionDataset.Tables[0].Rows[0];
                        user.StoreID = StoreMessages["STORESK"].ToString();
                        user.StoreSK = StoreMessages["STOREID"].ToString();
                        if (user.UserID == 0) user.TradingAccountSK = Convert.ToInt32(StoreMessages["TRADINGACCNUMBER"]);
                        user.ShippingCountry = StoreMessages["SHIPPINGCOUNTRY"].ToString();
                        user.CurrencyCode = StoreMessages["CURRENCYCODE"].ToString();
                        user.GstApplicable = StoreMessages["GST"].ToString();
                        user.TradingAccountSK = Convert.ToInt32(StoreMessages["TRADINGACCNUMBER"]);
                    }
                    if (HttpContext.Current.Request.Url.ToString().ToLower().Contains(".au"))
                        Session["Visitor"] = "visited";
                    Session["UserInfo"] = user;
                    CountrySeprator.Visible = true;
                }
                else if (user.CountryCode.ToLower() == "other")
                {
                    CountryName.InnerHtml = "<span class='b-dropdown-img'></span>" + "International";
                    user.CountryCode = "other";
                    user.CountryName = user.CountryName;
                    Session["UserInfo"] = user;
                    CountrySeprator.Visible = true;
                }
                else if (!ConfigurationManager.AppSettings.Get("PacificCountries").Contains(user.CountryName) && !(ConfigurationManager.AppSettings.Get("AustralianCountries").Contains(user.CountryName)))
                {
                    CountryName.InnerHtml = "<span class='b-dropdown-img'></span>" + "International";
                    user.CountryCode = "OTHER";
                    DataSet countryRestrictionDataset;
                    DataRow StoreMessages = null;
                    CountryController CountryController = new CountryController();
                    countryRestrictionDataset = CountryController.GetCountryRestrictions(user.CountryCode);
                    if (countryRestrictionDataset.Tables[0].Rows.Count > 0)
                    {
                        StoreMessages = countryRestrictionDataset.Tables[0].Rows[0];
                        user.StoreID = StoreMessages["STORESK"].ToString();
                        user.StoreSK = StoreMessages["STOREID"].ToString();
                        if (user.UserID == 0) user.TradingAccountSK = Convert.ToInt32(StoreMessages["TRADINGACCNUMBER"]);
                        user.ShippingCountry = StoreMessages["SHIPPINGCOUNTRY"].ToString();
                        user.CurrencyCode = StoreMessages["CURRENCYCODE"].ToString();
                        user.GstApplicable = StoreMessages["GST"].ToString();
                        user.TradingAccountSK = Convert.ToInt32(StoreMessages["TRADINGACCNUMBER"]);
                    }
                    Session["UserInfo"] = user;
                    CountrySeprator.Visible = true;
                }
                else
                {
                    DnnLog.Fatal("country name is...." + user.CountryName.Trim());
                    if (user.CountryName.Trim() != string.Empty)
                    {
                        CountryName.InnerHtml = "<span class='b-dropdown-img'></span>" + user.CountryName.Trim();
                        CountrySeprator.Visible = true;
                    }
                    else
                    {
                        GetCountry(user);
                    }
                }
            }
            else
            {
                GetCountry(user);
            }



            if (user.UserName != null)
            {
                if (user.UserName != string.Empty)
                {
                    LogOut.Visible = true;
                    logoutsepreator.Visible = true;
                    SignUp.Visible = false;
                    loginlnk.Visible = false;
                    wishlistlnk.Attributes.Add("style", "border-left:2px solid #dddddd;");


                }
                else
                {
                    DnnLog.Fatal("username is empty");
                    LogOut.Visible = false;
                    logoutsepreator.Visible = false;
                    wishlistlnk.Attributes.Add("style", "border-left:none;");
                    SignUp.Visible = true;
                    if (Session["Asian"] != null)
                    { SignUp.Attributes.Add("onclick", "OpenAsianCountryWindow();"); }
                    loginlnk.Visible = true;
                }
            }
            else
            {
                DnnLog.Fatal("username is null");
                LogOut.Visible = false;
                logoutsepreator.Visible = false;
                wishlistlnk.Attributes.Add("style", "border-left:none;");
                SignUp.Visible = true;
                if (Session["Asian"] != null)
                { SignUp.Attributes.Add("onclick", "OpenAsianCountryWindow();"); }
                loginlnk.Visible = true;
            }


            //Cart Count




        }
        else
        {
            LogOut.Visible = false;
            logoutsepreator.Visible = false;
            wishlistlnk.Attributes.Add("style", "border-left:none;");
            SignUp.Visible = true;
            if (Session["Asian"] != null)
            { SignUp.Attributes.Add("onclick", "OpenAsianCountryWindow();"); }
            loginlnk.Visible = true;
            GetCountry(user);
        }


        if (user != null)
        {

            Cart cart = GetCart(user.UserID);
            Session["Cart"] = cart;
            if (cart.NoOfProductAvailable != 0)
            {
                Cart.InnerText = "(" + cart.GetCart().Sum(x => x.Quantity) + ")";
            }

        }

        //GetList(user.UserID);
        if (user.UserID != 0)
        {
            if (Session["UserListCount"] == null)
                GetList(user.UserID);
            else
            { WishList.InnerText = "(" + Session["UserListCount"].ToString() + ")"; DnnLog.Fatal("Util WishList UserListCount ...."); }
        }
        else
        {
            if (Session["UserListCount"] != null)
            { Session["UserListCount"] = null; DnnLog.Fatal("Util UserListCount ...."); }
        }

    }

    private void GetList(int userid)
    {
        DnnLog.Fatal("Util GetList ....");
        List<ListInfo> CollListModules = new List<ListInfo>();
        IeCommerceList IEC = (IeCommerceList)new ConcertClass();
        CollListModules = IEC.GetListHeader(userid, "List");
        if (CollListModules.Count >= 1)
        {
            WishList.InnerText = "(" + CollListModules.Count.ToString() + ")";
            Session["UserListCount"] = CollListModules.Count;
        }

    }

    private Cart GetCart(int userid)
    {

        Cart cart = new Cart();
        //if (userid == 0)
        //{
        //    if (Session["dump"] != null)
        //    {
        //        cart = (Cart)Session["dump"];
        //    }
        //    else 
        if (userid == 0)
        {
            if (Request.Cookies["dump"] != null)
            {
                cart = SplitValueToCart(Cryptography.Decrypt(Request.Cookies["dump"].Value, ConfigurationManager.AppSettings["SecureKey"]));
                //Session["dump"] = cart;
            }
        }
        else
        {
            if (Request.Cookies["dump_" + userid.ToString()] != null)
            {
                cart = SplitValueToCart(Cryptography.Decrypt(Request.Cookies["dump_" + userid.ToString()].Value, ConfigurationManager.AppSettings["SecureKey"]));
                //Session["dump"] = cart;
            }
        }
        //}
        //else
        //{
        //    if (Session["user_" + userid.ToString()] != null)
        //    {
        //        cart = (Cart)Session["user_" + userid.ToString()];
        //    }
        //    else if (Request.Cookies["user_" + userid.ToString()] != null)
        //    {
        //        cart = SplitValueToCart(Cryptography.Decrypt(Request.Cookies["user_" + userid.ToString()].Value, ConfigurationManager.AppSettings["SecureKey"]));
        //        Session["user_" + userid.ToString()] = cart;
        //    }
        //}
        return cart;
    }

    private static Cart SplitValueToCart(string cartItems)
    {
        Cart cart = new Cart();
        if (cartItems != null && cartItems != "")
        {
            string[] cartItemsArray = cartItems.Split('-');

            foreach (string item in cartItemsArray)
            {

                string[] itemvalues = item.Split('|');
                if (itemvalues[0] != String.Empty && itemvalues[1] != string.Empty)
                {
                    CartDetail cartitem = new CartDetail();
                    cartitem.ISBN13 = itemvalues[0];
                    if (itemvalues[1] == "" || itemvalues[1] == null)
                        cartitem.Quantity = 1;
                    else
                        cartitem.Quantity = Convert.ToInt16(itemvalues[1]);
                    cart.AddProduct(cartitem);
                }
            }

        } return cart;
    }

    private void Logout()
    {
        Visitor user = new Visitor();
        Session["BusinessTradingPartnerAccountSk"] = null;
        Session["PersonalTradingPartnerAccountSk"] = null;
        Session["cengageusername"] = null;
        if (Session["UserInfo"] != null)
        {
            Session["Subscription"] = null;
            Session["UserName"] = null;
            user = (Visitor)(Session["UserInfo"]);
            user.UserID = 0;
            user.UserName = null;
            user.DomainOfUser = null;
            user.UserEmail = null;
            user.UserCreated = null;
            Session["UserInfo"] = user;
            Session["IsAuthenticated"] = null;
            LogOut.Visible = false;
            logoutsepreator.Visible = false;
            SignUp.Visible = true;
            if (Session["Asian"] != null)
            { SignUp.Attributes.Add("onclick", "OpenAsianCountryWindow();"); }
            loginlnk.Visible = true;
            wishlistlnk.Attributes.Add("style", "border-left:none;");
            DataSet countryRestrictionDataset;
            DataRow StoreMessages = null;
            CountryController CountryController = new CountryController();
            countryRestrictionDataset = CountryController.GetCountryRestrictions(user.CountryCode);
            if (countryRestrictionDataset.Tables[0].Rows.Count > 0)
            {
                StoreMessages = countryRestrictionDataset.Tables[0].Rows[0];
                user.StoreID = StoreMessages["STORESK"].ToString();
                user.StoreSK = StoreMessages["STOREID"].ToString();
                if (user.UserID == 0) user.TradingAccountSK = Convert.ToInt32(StoreMessages["TRADINGACCNUMBER"]);
                user.ShippingCountry = StoreMessages["SHIPPINGCOUNTRY"].ToString();
                user.CurrencyCode = StoreMessages["CURRENCYCODE"].ToString();
                user.GstApplicable = StoreMessages["GST"].ToString();
                user.TradingAccountSK = Convert.ToInt32(StoreMessages["TRADINGACCNUMBER"]);
            }
            Session["Visitor"] = "visited";
            Session["UserInfo"] = user;
            if (user.UserID == 0)
            {
                if (Request.Cookies["dump"] != null)
                {
                    HttpCookie myCookie = new HttpCookie("dump");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myCookie);
                    Request.Cookies.Remove("dump");
                }
            }
            else
            {
                if (Request.Cookies["dump_" + user.UserID.ToString()] != null)
                {
                    HttpCookie myCookie = new HttpCookie("dump_" + user.UserID.ToString());
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myCookie);
                    Request.Cookies.Remove("dump_" + user.UserID.ToString());
                }
            }
            Response.Redirect(Page.ResolveUrl("Primary.aspx"));
        }

    }

    private Visitor LookupCountry()
    {
        CountryLookup getCountry = null;
        try
        {
            fileName = Server.MapPath(ConfigurationManager.AppSettings.Get("GeoIPDBpath"));
            sandBox = ConfigurationManager.AppSettings.Get("SandBox");
            strIpAddress = string.Empty;
            // Take IPAddress from configuration file
            if (sandBox.ToLower() == "true")
            {
                strIpAddress = ConfigurationManager.AppSettings.Get("SimulateIP");
            }
            else
            {
                strIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                // Take remote address if our IP is not forwarding to any other IP
                if (strIpAddress == null)
                {
                    strIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            visitor = new Visitor();
            visitor.IPAddress = strIpAddress;
            getCountry = new CountryLookup(fileName);
            visitor.CountryCode = getCountry.lookupCountryCode(strIpAddress);
            visitor.CountryName = getCountry.lookupCountryName(strIpAddress);

        }
        catch (FileNotFoundException ex)// file not found exception
        {
            StringBuilder ExceptionMessage = new StringBuilder();
            ExceptionMessage.Append("Message" + ex.Message + "\t");
            // append exception message to string builder
            if (ex.InnerException != null)
            {
                ExceptionMessage.Append("Inner Exception Message" + ex.InnerException.Message + "\t");
                ExceptionMessage.Append("Inner Exception Stacktrace" + ex.InnerException.StackTrace + "\t");
            }
            DnnLog.Error(ExceptionMessage);
        }
        return visitor;
    }

    private void GetCountry(Visitor user)
    {
        user = LookupCountry();
        CountryName.InnerHtml = "<span class='b-dropdown-img'></span>" + user.CountryName.Trim();
        CountrySeprator.Visible = CountryName.InnerHtml != string.Empty ? true : false;

    }



    protected void CountryLogout_Click(object sender, EventArgs e)
    {
        Logout();
    }

    protected void Confirm_Click(object sender, EventArgs e)
    {
        Visitor usersi = new Visitor();
        usersi = (Visitor)(Session["UserInfo"]);

        if (usersi.UserID == 0)
        {
            if (Request.Cookies["dump"] != null)
            {
                HttpCookie myCookie = new HttpCookie("dump");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie); ;
            }
        }
        else
        {
            if (Request.Cookies["dump_" + usersi.UserID.ToString()] != null)
            {
                HttpCookie myCookie = new HttpCookie("dump_" + usersi.UserID.ToString());
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie); ;
            }
        }
    }
}
