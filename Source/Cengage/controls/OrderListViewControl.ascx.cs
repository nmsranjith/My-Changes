/*
 
 *  Project Name        :   Cengage Ecommerce
 *  Module Name         :   Purchase History
 *  Developer Name      :   Sujin D
 *  Date Created        :   08-07-2013
 *  Date Modified       :   25-07-2013
  
 */
using System;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.Instrumentation;
using DotNetNuke.Services.Exceptions;
using Cengage.Ecommerce.Dashboard.Data;

public partial class controls_OrderListViewControl : System.Web.UI.UserControl
{
    /// <summary>
    /// Bind orderSummary and OrderDetails Repeater on PageLoad.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        String orderID = null;
        try
        {
            //Condition enters only on first load event.
            if (!Page.IsPostBack)
            {
                orderID = Request.QueryString["orderid"].ToString();
                FillOrderSummary(orderID);
            }
            else
            {
                orderID = Cache["querystring"].ToString();
                FillOrderSummary(orderID);
            }
        }
        catch (Exception ex)//Exception catched here and log 
        {
            DNNLog(ex);
        }
    }

    /// <summary>
    /// Method to fetch OrderDetail  and to bind it in repeater
    /// </summary>
    /// <param name="orderID"></param>
    private void FillOrderDetails(string orderID)
    {
        DataTable orderDetailsDataTable = null;
        try
        {
            orderDetailsDataTable = new DataTable();
            orderDetailsDataTable.Columns.Add("purchaseddate1", typeof(string));
            orderDetailsDataTable.Columns.Add("ordnum", typeof(string));
            orderDetailsDataTable.Columns.Add("productTitle", typeof(string));
            orderDetailsDataTable.Columns.Add("product", typeof(string));
            orderDetailsDataTable.Columns.Add("quantity", typeof(string));
            orderDetailsDataTable.Columns.Add("price", typeof(string));
            orderDetailsDataTable.Columns.Add("status1", typeof(string));

            DataRow orderDataRow;
            string loginName = ConfigurationManager.AppSettings["LogInName"].ToString();
            string tradingAccount_Sk = ConfigurationManager.AppSettings["TradingAccount_Sk"].ToString();
            //Modified by Ramesh
            IDataReader reader = null; // DataProvider.Instance().GetOrderDetails(loginName, tradingAccount_Sk);
            //Loop used to fetch the user order details &populate datatable
            while (reader.Read())
            {
                orderDataRow = orderDetailsDataTable.NewRow();
                orderDataRow["purchaseddate1"] = (Convert.ToDateTime(reader["DATE PURCHASED"])).ToString("d MMM yyyy");
                orderDataRow["ordnum"] = reader["ORDER NUMBER"].ToString();
                orderDataRow["productTitle"] = reader["PRODUCT"].ToString() + " " + reader["ISBN"].ToString();
                orderDataRow["product"] = reader["PRODUCT"].ToString().Substring(0, 8) + "..." + reader["ISBN"].ToString();
                orderDataRow["quantity"] = reader["QUANTITY"].ToString();
                orderDataRow["price"] = "$" + reader["PRICE"].ToString();
                orderDataRow["status1"] = reader["DELIVERY STATUS"].ToString();
                orderDetailsDataTable.Rows.Add(orderDataRow);
            }
            OrderDetails.DataSource = orderDetailsDataTable.Select("ordnum=" + orderID).CopyToDataTable();
            OrderDetails.DataBind();
        }
        catch (Exception exc) //Module failed to load
        {
            throw exc;
        }
        finally
        {
            if (orderDetailsDataTable != null)
            {
                orderDetailsDataTable = null;
            }
        }
    }

    /// <summary>
    /// Method to fetch OrderSummary  and to bind it in repeater
    /// </summary>
    /// <param name="orderID"></param>
    private void FillOrderSummary(String orderID)
    {
        DataTable summaryDataTable = null;
        try
        {
            summaryDataTable = new DataTable();
            summaryDataTable.Columns.Add("purchaseddate", typeof(string));
            summaryDataTable.Columns.Add("referencenumber", typeof(string));
            summaryDataTable.Columns.Add("ordtotal", typeof(string));
            summaryDataTable.Columns.Add("discount", typeof(string));
            summaryDataTable.Columns.Add("shippingcharge", typeof(string));
            summaryDataTable.Columns.Add("total", typeof(string));
            summaryDataTable.Columns.Add("status", typeof(string));
            summaryDataTable.Columns.Add("username", typeof(string));
            summaryDataTable.Columns.Add("shippingaddress", typeof(string));
            summaryDataTable.Columns.Add("email", typeof(string));
            summaryDataTable.Columns.Add("ordernumber", typeof(string));
            DataRow orderSummaryDataRow;
            string loginName = ConfigurationManager.AppSettings["LogInName"].ToString();
            string tradingAccount_Sk = ConfigurationManager.AppSettings["TradingAccount_Sk"].ToString();
            //Modified by Ramesh
            IDataReader reader = null; // DataProvider.Instance().GetOrderedItems(loginName, tradingAccount_Sk);
            //Loop to fetch the user order summary and to store under DataTable
            while (reader.Read())
            {
                orderSummaryDataRow = summaryDataTable.NewRow();
                orderSummaryDataRow["ordernumber"] = reader["ORDER ID"].ToString();
                orderSummaryDataRow["purchaseddate"] = (Convert.ToDateTime(reader["DATE PURCHASED"])).ToString("d MMM yyyy");
                orderSummaryDataRow["referencenumber"] = reader["MY REFERENCE NUMBER"].ToString();
                orderSummaryDataRow["ordtotal"] = "$" + reader["ORDER TOTAL"].ToString();
                orderSummaryDataRow["discount"] = reader["DISCOUNT TOTAL"].ToString();
                orderSummaryDataRow["shippingcharge"] = reader["SHIPPING CHARGE"].ToString();
                orderSummaryDataRow["total"] = "$" + (decimal.Parse(reader["ORDER TOTAL"].ToString())
                + decimal.Parse(reader["SHIPPING CHARGE"].ToString()) - decimal.Parse(reader["DISCOUNT TOTAL"].ToString())).ToString()
                + reader["CURRENCY"].ToString();
                orderSummaryDataRow["status"] = reader["ORDER STATUS"].ToString();
                orderSummaryDataRow["username"] = reader["PURCHASER NAME"].ToString();
                orderSummaryDataRow["shippingaddress"] = reader["SHIPPED TO"].ToString();
                orderSummaryDataRow["email"] = reader["PURCHASER EMAIL"].ToString();
                summaryDataTable.Rows.Add(orderSummaryDataRow);
            }
            DataTable summary = summaryDataTable.Copy();
            //Method call to fill otherOrder repeater
            FillOtherOrders(summary, orderID);
            //Binding  orderSummary repeater
            OrderSummary.DataSource = summaryDataTable.Select("ordernumber=" + orderID).CopyToDataTable();
            OrderSummary.DataBind();
            ///Method call to bind orderDetails repeater
            FillOrderDetails(orderID);
        }
        catch (Exception exc) //Module failed to load
        {
            throw exc;
        }
        finally
        {
            //Disposing object
            if (summaryDataTable != null)
                summaryDataTable = null;
        }
    }

    /// <summary>
    /// Method to fetch OtherOrders  and to bind it in repeater
    /// </summary>
    /// <param name="summaryDataTable"></param>
    /// <param name="orderID"></param>
    private void FillOtherOrders(DataTable summaryDataTable, string orderID)
    {
        try
        {
            //Loop to iterate rows &  to delete a item from the table 
            for (int i = summaryDataTable.Rows.Count - 1; i >= 0; i--)
            {
                DataRow datarow = summaryDataTable.Rows[i];
                //If datarow equals orderID ..Delete the datarow
                if (datarow["ordernumber"].ToString() == orderID)
                {
                    datarow.Delete();
                }
            }
            OtherOrders.DataSource = summaryDataTable;
            OtherOrders.DataBind();
        }
        catch (Exception exc) //Module failed to load
        {
            throw exc;
        }
    }

    /// <summary>
    /// DataBound Trigger to show the status in different color corresponding to status.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OrderDetails_DataBound(object sender, RepeaterItemEventArgs e)
    {

        try
        {
            int i = 6;//status1
            HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("status1");
            DisplayStatus(i, div, e);
        }
        catch (Exception ex)//Exception catched here and log 
        {
            DNNLog(ex);
        }
    }

    /// <summary>
    ///  DataBound Trigger to show the status in different color corresponding to status.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OtherOrders_DataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            int i = 6;//status
            HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("status");
            DisplayStatus(i, div, e);
        }
        catch (Exception ex)//Exception catched here and log 
        {
            DNNLog(ex);
        }
    }

    /// <summary>
    /// Generic Method to display the status dynamically.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="div"></param>
    /// <param name="e"></param>
    private static void DisplayStatus(int i, HtmlGenericControl div, RepeaterItemEventArgs e)
    {
        try
        {
            DataRowView row;
            row = (DataRowView)e.Item.DataItem;
            if (row.Row.ItemArray[i].ToString() == "CANCELLED")
            {
                div.InnerHtml = "<span id='Dispacted_Cancelled'>CANCELLED</span>";
            }
            else
            {
                if (row.Row.ItemArray[i].ToString() == "DISPATCHED")
                {
                    div.InnerHtml = "<span id='Dispacted_Dispatched'>DISPATCHED</span>";

                }
                else
                {
                    if (row.Row.ItemArray[i].ToString() == "IN PROGRESS")
                    {
                        div.InnerHtml = "<span id='Dispacted_Inprogress'>IN PROGRESS</span>";
                    }
                }
            }
        }
        catch (Exception exc) //Module failed to load
        {
            throw exc;
        }
    }

    /// <summary>
    /// Logging Exception done here
    /// </summary>
    /// <param name="ex"></param>
    private static void DNNLog(Exception ex)
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
   
}