using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Cengage.eCommerce.Lib;
using System.IO;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using DotNetNuke.Instrumentation;

namespace DotNetNuke.Modules.HESearchResults.Handlers
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Productresultattributestate" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Attributestate class for the productresult module.
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public class AttributeState : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// product result handler processrequest function
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            DnnLog.Error("Handler attr state");
            try
            {
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                String jsonString = String.Empty;

                context.Request.InputStream.Position = 0;
                using (StreamReader inputStream = new StreamReader(context.Request.InputStream))
                {
                    jsonString = inputStream.ReadToEnd();

                }

                List<Facets> facetList = jsonSerializer.Deserialize<List<Facets>>(jsonString);




                HttpContext.Current.Session["AttributeState"] = ToDataTable(facetList);

                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write("OK");
            }
            catch (Exception ex)
            {
                DnnLog.Error("Error" + ex.Message);
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        /// <summary>
        /// product result handler ToDataTable function
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable ToDataTable(List<Facets> data)// T is any generic type
        {
            try
            {
                DataTable AttributeStateTable = new DataTable();
                AttributeStateTable.Columns.Add("ATTRIBUTE_NAME", typeof(string));
                AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_SK", typeof(int));
                AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_VALUE", typeof(string));
                AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_VALUE_SK", typeof(int));
                AttributeStateTable.Columns.Add("PROD_COUNT", typeof(int));
                AttributeStateTable.Columns.Add("IS_MULTI_SELECT", typeof(char));
                AttributeStateTable.Columns.Add("IS_CURRENT", typeof(char));
                AttributeStateTable.Columns.Add("IS_SELECTED", typeof(char));
                AttributeStateTable.Columns.Add("SEQNUM", typeof(int));

                data.ForEach(a =>
                {
                    AttributeStateTable.Rows.Add(a.ATTRIBUTE_NAME, a.ATTRIBUTE_TYPE_SK, a.ATTRIBUTE_TYPE_VALUE, a.ATTRIBUTE_TYPE_VALUE_SK, a.PROD_COUNT, a.IS_MULTI_SELECT, a.IS_CURRENT, a.IS_SELECTED, a.SEQNUM);
                }
                );
                return AttributeStateTable;
                /* PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

                 DataTable table = new DataTable();
                 for (int i = 0; i < props.Count; i++)
                 {
                     PropertyDescriptor prop = props[i];
                     table.Columns.Add(prop.Name, prop.PropertyType);
                 }
                 object[] values = new object[props.Count];
                 foreach (T item in data)
                 {
                     for (int i = 0; i < values.Length; i++)
                     {
                         values[i] = props[i].GetValue(item);
                     }
                     table.Rows.Add(values);
                 }
                 return table;*/
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}