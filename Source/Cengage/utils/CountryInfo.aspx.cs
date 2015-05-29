using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Cengage.Ecommerce.CountryDetection.Components.Controllers;
using DotNetNuke.Instrumentation;

public partial class CountryInfo : System.Web.UI.Page
{
    Visitor visitor = null;
    DataSet countryRestrictionDataset;
    CountryController Controller;
    DataRow StoreMessages = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //GetCountryInformation();
    }
    public void GetCountryInformation()
    {
        Visitor visitor = null;
        visitor = (Visitor)Session["UserInfo"];
        DataTable dt = new DataTable();
        dt.Columns.Add("CountryInformation", typeof(string));
        dt.Columns.Add("Value", typeof(string));
        dt.Rows.Add("IPAdddress", visitor.IPAddress);
        dt.Rows.Add("ActiveCountry", visitor.CountryName);
        dt.Rows.Add("ActiveStore", visitor.StoreID);
        dt.Rows.Add("TradingPartner", visitor.TradingAccountSK);
        dt.Rows.Add("Catologue", visitor.AppTragetSearchCode);
        if (visitor.AppTragetSearchCode == "AUPRI")
        {

            dt.Rows.Add("Division", "Primary");
        }
        else if (visitor.AppTragetSearchCode == "AUSEC")
        {
            dt.Rows.Add("Division", "Secondary");
        }
        else { dt.Rows.Add("Division", ""); }



        CountryGridView.DataSource = dt;
        CountryGridView.DataBind();


    }
    protected void countrydetails_Click(object sender, EventArgs e)
    {
        GetCountryInformation();
    }
    protected void SimuateNZ_Click(object sender, EventArgs e)
    {
        try
        {
            visitor = new Visitor();
            // Get the restrictions for the country
                visitor.StoreID ="2";
                visitor.StoreSK = "S02-NZ";
                visitor.ShippingCountry = "New Zealand";
                visitor.CurrencyCode = "NZD";
                visitor.GstApplicable = "Y";
            visitor.CountryName = "New Zealand";
            visitor.CountryCode = "NZ";
            if (Session["UserInfo"] == null) { Session["UserInfo"] = visitor; }
            else
            {
                Visitor loggedinVisitor = (Visitor)Session["UserInfo"];
                visitor.UserID = loggedinVisitor.UserID;
                visitor.UserName = loggedinVisitor.UserName;
                visitor.DomainOfUser = loggedinVisitor.DomainOfUser;
                visitor.UserCreated = loggedinVisitor.UserCreated;
            }
            Session["Visitor"] = "visited";
            Session["UserInfo"] = visitor;
            Response.Redirect("http://s2.cengagelearning.co.nz?testcountry=nz");
        }
        catch (Exception ex)
        {
            DnnLog.Error("error in setting nz url.."+ex);
        }
    }
    protected void SimuateAUS_Click(object sender, EventArgs e)
    {
        try
        {
            visitor = new Visitor();
            // Get the restrictions for the country
            visitor.StoreID = "1";
            visitor.StoreSK = "S02-AU";
            visitor.ShippingCountry = "Australia";
            visitor.CurrencyCode = "AUD";
            visitor.GstApplicable = "Y";
            visitor.CountryName = "Australia";
            visitor.CountryCode = "AU";
            if (Session["UserInfo"] == null) { Session["UserInfo"] = visitor; }
            else
            {
                Visitor loggedinVisitor = (Visitor)Session["UserInfo"];
                visitor.UserID = loggedinVisitor.UserID;
                visitor.UserName = loggedinVisitor.UserName;
                visitor.DomainOfUser = loggedinVisitor.DomainOfUser;
                visitor.UserCreated = loggedinVisitor.UserCreated;
            }
            Session["Visitor"] = "visited";
            Session["UserInfo"] = visitor;
            Response.Redirect("http://s2.cengagelearning.com.au?testcountry=au");
        }
        catch (Exception ex)
        {
            DnnLog.Error("error in setting au url.." + ex);
        }
    }
    protected void SimuatePacificCountries_Click(object sender, EventArgs e)
    {
        try
        {
            visitor = new Visitor();
            // Get the restrictions for the country
            visitor.StoreID = "3";
            visitor.StoreSK = "S03-PACF";
            visitor.ShippingCountry = "Fiji";
            visitor.CurrencyCode = "AUD";
            visitor.GstApplicable = "n";
            visitor.CountryName = "Fiji";
            visitor.CountryCode = "FJ";
            if (Session["UserInfo"] == null) { Session["UserInfo"] = visitor; }
            else
            {
                Visitor loggedinVisitor = (Visitor)Session["UserInfo"];
                visitor.UserID = loggedinVisitor.UserID;
                visitor.UserName = loggedinVisitor.UserName;
                visitor.DomainOfUser = loggedinVisitor.DomainOfUser;
                visitor.UserCreated = loggedinVisitor.UserCreated;
            }
            Session["Visitor"] = "visited";
            Session["UserInfo"] = visitor;
            Response.Redirect("http://s2.cengagelearning.com.au?testcountry=fj");
        }
        catch (Exception ex)
        {
            DnnLog.Error("error in setting pacific url.." + ex);
        }
    }
	
	 protected void SimuateInternational_Click(object sender, EventArgs e)
    {
        try
        {
            visitor = new Visitor();
            // Get the restrictions for the country
            visitor.StoreID = "3";
            visitor.StoreSK = "S05-INTL";
            visitor.ShippingCountry = "We can't link your IP address to a country or location";
            visitor.CurrencyCode = "AUD";
            visitor.GstApplicable = "n";
            visitor.CountryName = "We can't link your IP address to a country or location";
            visitor.CountryCode = "--";
            if (Session["UserInfo"] == null) { Session["UserInfo"] = visitor; }
            else
            {
                Visitor loggedinVisitor = (Visitor)Session["UserInfo"];
                visitor.UserID = loggedinVisitor.UserID;
                visitor.UserName = loggedinVisitor.UserName;
                visitor.DomainOfUser = loggedinVisitor.DomainOfUser;
                visitor.UserCreated = loggedinVisitor.UserCreated;
            }
            Session["Visitor"] = "visited";
            Session["UserInfo"] = visitor;
            Response.Redirect("http://s2.cengagelearning.com.au?testcountry=intl");
        }
        catch (Exception ex)
        {
            DnnLog.Error("error in setting pacific url.." + ex);
        }
    }
}