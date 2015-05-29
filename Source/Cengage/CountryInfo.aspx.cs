using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CountryInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
}