<%@ WebHandler Language="C#" Class="SearchHandler" %>

using System;
using System.Web;
using Cengage.eCommerce.ExceptionHandling;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using DotNetNuke.Instrumentation;
using System.Web.SessionState;
using DotNetNuke.Common.Utilities;

public class SearchHandler : IHttpHandler, IRequiresSessionState 
{

    public void ProcessRequest(HttpContext Context)
    {

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
        if (HttpContext.Current.Cache[HttpContext.Current.Session.SessionID + "Auto"] == null)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SiteSqlServer"].ToString());
            List<string> list = new List<string>();

            try
            {
                connection.Open();
                System.Data.SqlClient.SqlCommand SelectCommand = new System.Data.SqlClient.SqlCommand("ECOMM_SEARCH_FULLTEXT_AUTOCOMPLETE", connection);
                SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SelectCommand.Parameters.Add("@AppTargetCode", !string.IsNullOrEmpty(Context.Session["AppTragetSearchCode"].ToString()) ? Context.Session["AppTragetSearchCode"].ToString() : ConfigurationManager.AppSettings["AUPRIAppTarget"].ToString());
                SqlDataReader dataReader = SelectCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    list.Add(dataReader["Title"].ToString());
                    list.Add(dataReader["Isbn13"].ToString());
                    list.Add(dataReader["Isbn10"].ToString());
                    list.AddRange(dataReader["Author"].ToString().Split(',').ToList());
                    //list.Add(dataReader["Attribute"].ToString());
                    list.AddRange(dataReader["AttributeValue"].ToString().Split(',').ToList());
                    list.AddRange(dataReader["Keywords"].ToString().Split(',').ToList());

                }
                HttpContext.Current.Cache[HttpContext.Current.Session.SessionID + "Auto"] = list.Distinct().ToList();
                return HttpContext.Current.Cache[HttpContext.Current.Session.SessionID + "Auto"] as List<string>;
            }
            catch (Exception Exception)
            {
                DataAccessException.Instance.ExceptionMessage(Exception);
                DnnLog.Error(Exception);
                return null;
            }
        }
        else
        {
            return HttpContext.Current.Cache[HttpContext.Current.Session.SessionID + "Auto"] as List<string>;
        }
    }

}