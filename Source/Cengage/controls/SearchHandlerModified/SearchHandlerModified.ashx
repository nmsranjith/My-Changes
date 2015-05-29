<%@ WebHandler Language="C#" Class="SearchHandlerModified" %>

using System;
using System.Web;
using Cengage.eCommerce.ExceptionHandling;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using DotNetNuke.Instrumentation;
using System.Text.RegularExpressions;
using System.Web.SessionState;
using DotNetNuke.Common.Utilities;

public class SearchHandlerModified : IHttpHandler, IRequiresSessionState 
{

    public void ProcessRequest(HttpContext Context)
    {
		DnnLog.Error("------------------");
        string ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject(GetProductSearchData(Context));
        Context.Response.Write(ClassJson);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private List<string> GetProductSearchData(HttpContext Context)
    {
		string appTarget = string.Empty;
        Visitor visitor = (Visitor)Context.Session["UserInfo"];
        switch (Context.Request.QueryString["ddlDivisionValue"].ToString().ToUpper())
        {
            case "0":
                appTarget = ConfigurationManager.AppSettings["AUPRIAppTarget"].ToString();
                Context.Session["Division"] = "0";
                break;
            case "1":
                if (visitor.CountryCode.ToUpper() == "NZ")
                    appTarget = ConfigurationManager.AppSettings["NZSECAppTarget"].ToString();
                else
                    appTarget = ConfigurationManager.AppSettings["AUSECAppTarget"].ToString();
                Context.Session["Division"] = "1";
                break;
            default:
                if (visitor.CountryCode.ToUpper() == "NZ")
                    appTarget = ConfigurationManager.AppSettings["NZALLAppTarget"].ToString();
                else
                    appTarget = ConfigurationManager.AppSettings["AUALLAppTarget"].ToString();
                Context.Session["Division"] = "2";
                break;

        }
        //if (HttpContext.Current.Cache[HttpContext.Current.Session.SessionID + "Auto"] == null)
        //{
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString());
            List<string> list = new List<string>();

            try
            {
                connection.Open();
                System.Data.SqlClient.SqlCommand SelectCommand = new System.Data.SqlClient.SqlCommand("ECOMM_SEARCH_FULLTEXT_AUTOCOMPLETE", connection);
                SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //SelectCommand.Parameters.Add("@AppTargetCode", !string.IsNullOrEmpty(Context.Session["AppTragetSearchCode"].ToString()) ? Context.Session["AppTragetSearchCode"].ToString() : ConfigurationManager.AppSettings["AUPRIAppTarget"].ToString());
				SelectCommand.Parameters.Add("@AppTargetCode", !string.IsNullOrEmpty(appTarget) ? appTarget : ConfigurationManager.AppSettings["AUPRIAppTarget"].ToString());
                DnnLog.Error(Context.Session["AppTragetSearchCode"].ToString());
                SqlDataReader dataReader = SelectCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    list.Add(dataReader["Title"].ToString().ToLower().Trim());
                    list.Add(dataReader["Isbn13"].ToString().ToLower().Trim());
                    list.Add(dataReader["Isbn10"].ToString().ToLower().Trim());
                    //list.AddRange(dataReader["Author"].ToString().ToLower().Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList());
                    //list.Add(dataReader["AttributeName"].ToString().ToLower().Trim());
                    //list.AddRange(dataReader["AttributeValue"].ToString().ToLower().Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList());
                    //list.AddRange(dataReader["Keywords"].ToString().ToLower().Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList());
					if(!string.IsNullOrEmpty(dataReader["Author"].ToString()))
                        list.AddRange(dataReader["Author"].ToString().ToLower().Trim().Split(',').Select(x => Regex.Replace(x.Trim(), "[^0-9a-zA-Z]+", " ")).ToList());
                    list.Add(dataReader["AttributeName"].ToString().ToLower().Trim());
                    if (!string.IsNullOrEmpty(dataReader["AttributeValue"].ToString()))
                        list.AddRange(dataReader["AttributeValue"].ToString().ToLower().Trim().Split(',').Select(x => Regex.Replace(x.Trim(), "[^0-9a-zA-Z]+", " ")).ToList());
                    if (!string.IsNullOrEmpty(dataReader["Keywords"].ToString()))
                        list.AddRange(dataReader["Keywords"].ToString().ToLower().Trim().Split(',').Select(x => Regex.Replace(x.Trim(), "[^0-9a-zA-Z]+", " ")).ToList());


                }
                HttpContext.Current.Cache[HttpContext.Current.Session.SessionID + "Auto"] = list.Distinct().ToList();
                //return HttpContext.Current.Cache[HttpContext.Current.Session.SessionID + "Auto"] as List<string>;
				return list.Distinct().ToList();
            }
            catch (Exception Exception)
            {
                DataAccessException.Instance.ExceptionMessage(Exception);
                DnnLog.Error(Exception);
                return null;
            }
        //}
        //else
        //{
        //    return HttpContext.Current.Cache[HttpContext.Current.Session.SessionID + "Auto"] as List<string>;
        //}
    }

}