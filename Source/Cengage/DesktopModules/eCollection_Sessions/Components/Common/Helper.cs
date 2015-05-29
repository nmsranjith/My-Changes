using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace DotNetNuke.Modules.eCollection_Sessions.Components.Common
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Helper" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Helper class
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public static class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> list)    
        {        
            DataTable dt = new DataTable();        
            if (list.Count > 0)        
            {            
                Type listType = list.ElementAt(0).GetType();            //Get element properties and add datatable columns              
                PropertyInfo[] properties = listType.GetProperties();            
                foreach (PropertyInfo property in properties)                
                    dt.Columns.Add(new DataColumn() { ColumnName = property.Name });            
                foreach (object item in list)            
                {                
                    DataRow dr = dt.NewRow();                
                    foreach (DataColumn col in dt.Columns)                    
                        dr[col] = listType.GetProperty(col.ColumnName).GetValue(item, null);                
                    dt.Rows.Add(dr);            
                }        
            }        
            return dt;    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldDate"></param>
        /// <param name="newDate"></param>
        /// <returns></returns>
        public static bool DiffInMonths(DateTime oldDate, DateTime newDate)
        {
            if (oldDate != DateTime.MinValue && newDate != DateTime.MinValue)
            {

                TimeSpan ts = newDate - oldDate;

                int months = ts.Days / 31;

                if (months >= 1)
                {
                    return true;
                }

            }
            return false;
        }

    }
}