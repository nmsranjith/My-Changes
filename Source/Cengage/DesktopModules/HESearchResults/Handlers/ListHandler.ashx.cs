using System;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Cengage.eCommerce.Lib;
using System.Configuration;
using System.Data;
using Cengage.Ecommerce.CengageServiceLibrary;
using Cengage.Ecommerce.CengageServiceClient;
using DotNetNuke.Instrumentation;
using System.Collections.Generic;
using DotNetNuke.Modules.HESearchResults.Components.Common;
using DotNetNuke.Modules.HESearchResults.Components;
using DotNetNuke.Modules.HESearchResults.Components.Controller;

namespace DotNetNuke.Modules.HESearchResults.Handlers
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="ProductresultListhandler" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Productresult listhandler for the cart, list and more button functionality
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public class ListHandler : HESearchResultsModuleBase, IHttpHandler, IRequiresSessionState
    {
        string Value = string.Empty;
        string Action = string.Empty;

        /// <summary>
        /// product result handler processrequest function
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            SearchController IECPR = new SearchController();
            string ClassJson = string.Empty;
            HttpSessionState Session = HttpContext.Current.Session;
            Visitor UserDetailInfo = (Visitor)Session["UserInfo"];
            Value = context.Request.QueryString["Value"].ToString();
            Action = context.Request.QueryString["Action"].ToString();

            switch (Action)
            {
                case "CART":
                    Cart cart = GetCart(UserDetailInfo.UserID);
                    cart = SplitValueToCart(cart, Value);
                    //if (UserDetailInfo.UserID == 0)
                    //{
                    if (context.Request.Cookies["dump"] == null)
                        context.Response.Cookies["dump"].Value = Cryptography.Encrypt(Value, ConfigurationManager.AppSettings["SecureKey"]);
                    else
                        context.Response.Cookies["dump"].Value = Cryptography.Encrypt(Cryptography.Decrypt(context.Request.Cookies["dump"].Value.ToString() == "" ? "" : context.Request.Cookies["dump"].Value.ToString(), ConfigurationManager.AppSettings["SecureKey"]) + '-' + Value, ConfigurationManager.AppSettings["SecureKey"]);
                    double expnumber = double.Parse(ConfigurationManager.AppSettings["CacheDuration"]) == 0 ? 2 : double.Parse(ConfigurationManager.AppSettings["CacheDuration"]);
                    context.Response.Cookies["dump"].Expires = DateTime.Now.AddDays(expnumber);
                    int sumcount = cart.GetCart().Sum(x => x.Quantity);
                    //Context.Session["dump"] = cart;
                    //}
                    //else
                    //{
                    //    Context.Response.Cookies["user_" + UserDetailInfo.UserID.ToString()].Value = Cryptography.Encrypt(Cryptography.Decrypt(Context.Request.Cookies["user_" + UserDetailInfo.UserID.ToString()].Value, ConfigurationManager.AppSettings["SecureKey"]) + '-' + Value, ConfigurationManager.AppSettings["SecureKey"]);
                    //    Context.Session["user_" + UserDetailInfo.UserID.ToString()] = cart;
                    //}
                    Context.Response.Write(sumcount);
                    return;
                case "LIST":
                    IECPR = new SearchController();
                    string ListName = context.Request.QueryString["ListName"];
                    string Listvalue = context.Request.QueryString["value"];
                    string UserSk = context.Request.QueryString["UserSk"];
                    string output = string.Empty;
                    string AddedListName = string.Empty;


                    string listDetail = string.Empty;
                    string listSK = string.Empty;
                    string[] listArray = Listvalue.Split(',');
                    if (listArray[0] != String.Empty && listArray[1] != string.Empty)
                    {
                        listSK = listArray[1];
                        listDetail = listArray[0];
                    }
                    if (listSK == "-1") ListName = "Shopping List";
                    if (ListName != "")
                    {
                        //CollListModules = (System.Collections.Generic.List<Cengage.Ecommerce.ProductResults.Components.Modal.ListInfo>)DataCache.GetCache("ResultListHeader");
                        CollListModules = IECPR.GetProduct(Constants.FLG_LISTQUOTEHEADER, "", int.Parse(UserSk), "List");
                        //DnnLog.Fatal("Cache ResultListHeader:" + DataCache.GetCache("ResultListHeader"));
                        DnnLog.Fatal("Cache ResultListHeader:" + CollListModules);
                        if (CollListModules == null)
                        {
                            CollListModules = IECPR.GetProduct(Constants.FLG_LISTQUOTEHEADER, "", int.Parse(UserSk), "List");
                        }
                        object found = CollListModules.FirstOrDefault(cc => cc.Name.ToUpper().ToString() == ListName.ToUpper());
                        if (found != null)
                        {
                            ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject("0");
                            context.Response.Write(0);
                            return;
                        }
                        else
                        {

                            //ListInfo _objListparameter = new ListInfo { UserSK = Convert.ToInt32(UserSk), Status = UserDetailInfo.TradingAccountSK.ToString(), UserListQuoteSK = ListName, GenericFlag = "List" };

                            IECPR.AddListQuote(Constants.FLG_CREATELIST_QUOTE, UserSk, UserDetailInfo.TradingAccountSK.ToString(), ListName.ToString(), "List", ref output);
                            ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject(AddedListName);
                            if (listDetail.Length > 0)
                            {
                                string addedlistsk = output;
                                listDetail = listDetail.Substring(0, (listDetail.Length - 1)) + ".";
                                //ListInfo _objListparameter = new ListInfo { Status = ProductQty, UserListQuoteSK = addedlistsk };

                                IECPR.AddListQuote(Constants.FLG_ADDTOLIST, UserSk, listDetail, addedlistsk, "List", ref output);
                                ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject(addedlistsk);
                                context.Response.Write(ClassJson);
                                return;
                            }
                        }
                    }
                    if (listSK != "0")
                    {
                        listDetail = listDetail.Substring(0, (listDetail.Length - 1)) + ".";
                        //ListInfo _objListparameter = new ListInfo { Status = ProductQty, UserListQuoteSK = addedlistsk };

                        IECPR.AddListQuote(Constants.FLG_ADDTOLIST, UserSk, listDetail, listSK, "List", ref output);


                        if ((context.Request.QueryString["fromdd"]) != null && (context.Request.QueryString["fromdd"] == "Y"))
                        {
                            ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject("DD");
                        }
                        else
                        {
                            ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject(listSK);
                        }
                        context.Response.Write(ClassJson);
                        return;
                    }
                    context.Response.Write(ClassJson);
                    break;
                case "CREATEQUOTE":
                    try
                    {
                        QuoteInfo Quote = new QuoteInfo();
                        Quote.CURRENCY = UserDetailInfo.CurrencyCode;
                        Quote.GST_APPLICABLE = UserDetailInfo.GstApplicable;
                        Quote.QUOTE_VALID_DAYS = int.Parse(ConfigurationManager.AppSettings["QuoteExpiryDays"]);
                        Quote.USER_CREATED = UserDetailInfo.UserName;
                        Quote.USER_SK = UserDetailInfo.UserID;
                        Quote.FreeShippingEligiblePrice = decimal.Parse(ConfigurationManager.AppSettings["FreeShippingEligibleCost"]);
                        Quote.SHIPPING_COST_STD = decimal.Parse(ConfigurationManager.AppSettings["ShippingCost"]);
                        Quote.ACTION = "CREATE";
                        Quote.TRADING_PARTNER_ACCOUNT_SK = string.IsNullOrEmpty(context.Request.QueryString["AccountSK"]) ? UserDetailInfo.TradingAccountSK : int.Parse(context.Request.QueryString["AccountSK"]);
                        Quote.QUOTE_NAME = context.Request.QueryString["QuoteName"];
                        Quote.TradingPartnerAccNo = context.Request.QueryString["AccountNo"];
                        string[] value = context.Request.QueryString["value"].Split(',');


                        System.Collections.Generic.Dictionary<int, string> productsks = new System.Collections.Generic.Dictionary<int, string>();
                        System.Collections.Generic.List<Product> products = new System.Collections.Generic.List<Product>();

                        for (int j = 0; j < value.Length; j++)
                        {
                            DataRow dr = Quote.ProductDetails.NewRow();

                            string[] subvalue = value[j].Split('-');
                            dr["PRODUCT_SK"] = int.Parse(subvalue[1]);
                            dr["QTY"] = int.Parse(subvalue[2]);

                            productsks.Add(int.Parse(subvalue[1]), subvalue[0]);
                            Quote.ProductDetails.Rows.Add(dr);
                            products.Add(new Product()
                            {
                                ISBN = subvalue[0],
                                AccountNumber = Quote.TRADING_PARTNER_ACCOUNT_SK.ToString(),
                                IsGSTIncluded = (UserDetailInfo.GstApplicable == "Y" ? true : false),
                                CurrencyCode = UserDetailInfo.CurrencyCode,
                                Quantity = int.Parse(dr["QTY"].ToString()),
                                ToCountry = UserDetailInfo.CountryCode
                            });
                        }

                        using (PriceSvcClient svcclient = new PriceSvcClient())
                        {
                            products = svcclient.GetProductPrice(products);
                        }

                        foreach (DataRow dr in Quote.ProductDetails.Rows)
                        {
                            Product pro = products.Where(p => p.ISBN == productsks[int.Parse(dr["PRODUCT_SK"].ToString())]).First();
                            dr["RRP"] = pro.RRP;
                            dr["DISCOUNT_PRICE"] = double.Parse((pro.LineItemValue / double.Parse(pro.Quantity.ToString())).ToString());
                        }

                        if (Quote.ProductDetails != null && Quote.ProductDetails.Rows.Count > 0)
                        {
                            IECPR.EditQuoteDetails(Quote);
                        }
                        ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject(Quote.LIST_QUOTE_SK.ToString() + "|" + Quote.TRADING_PARTNER_ACCOUNT_SK.ToString() + "|" + Quote.TradingPartnerAccNo.ToString());
                        /*IECPR.GetExistingQuotes(int.Parse(context.Request.QueryString["usersk"]), context.Request.QueryString["role"],
                        int.Parse(context.Request.QueryString["logintp"])).Tables[0].AsEnumerable()
                        .Select(row => new { SK = row["SK"].ToString(), NAME = row["NAME"].ToString() })
                            //.ToDictionary(row => row["SK"].ToString(),row => row["NAME"].ToString()).
                        );*/
                    }
                    catch (Exception e)
                    {
                        ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject(e.Message);
                        DnnLog.Error(e);
                    }
                    context.Response.Write(ClassJson);
                    break;
                case "ADDQUOTE":
                    try
                    {
                        QuoteInfo Quote = new QuoteInfo();

                        Quote.CURRENCY = UserDetailInfo.CurrencyCode;
                        Quote.GST_APPLICABLE = UserDetailInfo.GstApplicable;
                        Quote.QUOTE_VALID_DAYS = int.Parse(ConfigurationManager.AppSettings["QuoteExpiryDays"]);
                        Quote.USER_CREATED = UserDetailInfo.UserName;
                        Quote.USER_SK = UserDetailInfo.UserID;
                        Quote.FreeShippingEligiblePrice = decimal.Parse(ConfigurationManager.AppSettings["FreeShippingEligibleCost"]);
                        Quote.SHIPPING_COST_STD = decimal.Parse(ConfigurationManager.AppSettings["ShippingCost"]);
                        Quote.ACTION = "EDIT";
                        Quote.LIST_QUOTE_SK = int.Parse(context.Request.QueryString["listquotesk"]);
                        Quote.TRADING_PARTNER_ACCOUNT_SK = string.IsNullOrEmpty(context.Request.QueryString["AccountSK"]) ? UserDetailInfo.TradingAccountSK : int.Parse(context.Request.QueryString["AccountSK"]);



                        DataSet quoteProducts = IECPR.GetQuoteProducts(Quote.LIST_QUOTE_SK);
                        System.Collections.Generic.List<Product> products = new System.Collections.Generic.List<Product>();

                        string[] value = context.Request.QueryString["value"].Split(',');
                        System.Collections.Generic.Dictionary<int, string> productsks = new System.Collections.Generic.Dictionary<int, string>();
                        for (int j = 0; j < value.Length; j++)
                        {
                            DataRow dr = Quote.ProductDetails.NewRow();
                            string[] subvalue = value[j].Split('-');


                            dr["PRODUCT_SK"] = int.Parse(subvalue[1]);
                            dr["QTY"] = int.Parse(subvalue[2]);

                            if (quoteProducts != null && quoteProducts.Tables.Count > 0 && quoteProducts.Tables[0].Rows.Count > 0)
                            {
                                DataRow[] qp = quoteProducts.Tables[0].Select("PRODUCT_SK = " + dr["PRODUCT_SK"].ToString());
                                if (qp != null && qp.Length > 0)
                                {
                                    dr["QTY"] = int.Parse(dr["QTY"].ToString()) + int.Parse(qp[0]["QUANTITY"].ToString());
                                }
                            }
                            products.Add(new Product()
                            {
                                ISBN = subvalue[0],
                                AccountNumber = Quote.TRADING_PARTNER_ACCOUNT_SK.ToString(),
                                IsGSTIncluded = (UserDetailInfo.GstApplicable == "Y" ? true : false),
                                CurrencyCode = UserDetailInfo.CurrencyCode,
                                Quantity = int.Parse(dr["QTY"].ToString()),
                                ToCountry = UserDetailInfo.CountryCode
                            });
                            productsks.Add(int.Parse(subvalue[1]), subvalue[0]);
                            Quote.ProductDetails.Rows.Add(dr);
                        }

                        if (products.Count > 0)
                        {
                            //call wcf method
                            using (PriceSvcClient svcclient = new PriceSvcClient())
                            {
                                products = svcclient.GetProductPrice(products);
                            }
                            foreach (DataRow dr in Quote.ProductDetails.Rows)
                            {
                                Product pro = products.Where(p => p.ISBN == productsks[int.Parse(dr["PRODUCT_SK"].ToString())]).First();
                                dr["RRP"] = pro.RRP;
                                dr["DISCOUNT_PRICE"] = double.Parse((pro.LineItemValue / double.Parse(pro.Quantity.ToString())).ToString());
                            }

                            if (Quote.ProductDetails != null && Quote.ProductDetails.Rows.Count > 0)
                            {
                                IECPR.EditQuoteDetails(Quote);
                            }
                        }
                        ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject("Y");

                    }
                    catch (Exception e)
                    {
                        ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject("N");
                        DnnLog.Error(e);
                    }
                    context.Response.Write(ClassJson);
                    break;

                case "PRODUCTSTUDENT":
                    try
                    {
                        int productsk = int.Parse(context.Request.QueryString["productsk"]);
                        string division = context.Request.QueryString["division"];
                        DataSet PrdStdSet = IECPR.Selectsub(productsk, division);
                        string[] StudentBookList = PrdStdSet.Tables[0].Rows[0]["STUDENTBOOK"].ToString().Split(',');
                        int StudentIteration = 0;
                        int ProductIteration = 0;
                        List<string> BookListoutput = new List<string>();
                        foreach (string StudentBook in StudentBookList)
                        {
                            // HtmlAnchor StudentLink = new HtmlAnchor();
                            string StudentLink = string.Empty;
                            //StudentLink.Attributes.Add("class", "awidt");
                            //StudentLink.ID = "StudentSubProduct_" + StudentIteration.ToString();


                            if (StudentBook != string.Empty)
                            {
                                if (StudentBook.ToUpper().Split(':')[0].ToString().Trim() == "MASTER YEAR LEVEL" || StudentBook.ToUpper().Split(':')[0].ToString().Trim() == "YEAR LEVEL")
                                {

                                    if (UserDetailInfo.CountryCode == "AU")
                                    {
                                        if (StudentBook.Split(':')[1].ToString().Trim() == "0/1")
                                        {
                                            //DnnLog.Fatal("Step 3:" + StudentIteration + " attrbute Value " + StudentBook.Trim().ToUpper());
                                            StudentLink = StudentIteration == 0 ? "Student:!" + StudentBook.Trim().ToUpper().Split(':')[0].ToString() + "&nbsp" + ":FOUNDATION" : "Student:!" + " | " + StudentBook.Trim().ToUpper().Split(':')[0].ToString() + "&nbsp" + ":FOUNDATION";
                                        }
                                        else
                                        {
                                            StudentLink = StudentIteration == 0 ? "Student:!" + StudentBook.Trim() + "&nbsp" : "Student:!" + " | " + StudentBook.Trim() + "&nbsp";
                                        }

                                    }
                                    else if (UserDetailInfo.CountryCode == "NZ")
                                    {
                                        // DnnLog.Fatal("Step 4:" + StudentIteration + " attrbute Value " + StudentBook.ToUpper().Split(':')[1].ToString().Trim());
                                        if (StudentBook.ToUpper().Split(':')[1].ToString().Trim() == "FOUNDATION" && StudentBook.ToUpper().Split(':')[1].ToString().Trim() != "PRE-FOUNDATION")
                                        {
                                            //DnnLog.Fatal("Step 5:" + StudentIteration + " attrbute Value " + StudentBook.ToUpper().Split(':')[1].ToString().Trim());
                                            StudentLink = StudentIteration == 0 ? "Student:!" + StudentBook.Trim().ToUpper().Split(':')[0].ToString() + "&nbsp" + ":0/1" : "Student:!" + " | " + StudentBook.Trim().ToUpper().Split(':')[0].ToString() + "&nbsp" + ":0/1";
                                        }
                                        else if (StudentBook.Trim().ToUpper().Split(':')[1].ToString() != "PRE-FOUNDATION")
                                        {
                                            StudentLink = StudentIteration == 0 ? "Student:!" + StudentBook.Trim() + "&nbsp" : "Student:!" + " | " + StudentBook.Trim() + "&nbsp";
                                        }
                                    }
                                    else
                                    {
                                        StudentLink = StudentIteration == 0 ? "Student:!" + StudentBook.Trim() + "&nbsp" : "Student:!" + " | " + StudentBook.Trim() + "&nbsp";
                                    }
                                }
                                else
                                {
                                    StudentLink = StudentIteration == 0 ? "Student:!" + StudentBook.Trim() + "&nbsp" : "Student:!" + " | " + StudentBook.Trim() + "&nbsp";
                                }
                                //StudentLink.Attributes.Add("onclick", "Clientclick(this)");                                
                                //1StudentPlace.Controls.Add(StudentLink);
                                BookListoutput.Add(StudentLink);
                                StudentIteration++;

                            }
                        }


                        string[] ProductBookList = PrdStdSet.Tables[1].Rows[0]["PRODUCT"].ToString().Split('^');
                        string[] ProductBookListoutput = new string[] { };
                        foreach (string productBook in ProductBookList)
                        {
                            string[] productBook1 = productBook.Split(':');
                            if (productBook1[0].Trim() != string.Empty && productBook1[1].Trim() != string.Empty)
                            {
                                //HtmlAnchor ProductLink = new HtmlAnchor();
                                string ProductLink = string.Empty;
                                //ProductLink.Attributes.Add("Class", "awidt");
                                //ProductLink.ID = "StudentSubProduct_" + ProductIteration.ToString();
                                if (productBook != string.Empty)
                                {
                                    if (ProductIteration == 0)
                                    {
                                        ProductLink = "product:!" + productBook.Trim() + "&nbsp";
                                        ProductIteration++;
                                    }
                                    else
                                    {
                                        if (ProductLink == string.Empty)
                                        {
                                            ProductLink = "product:!" + "| " + productBook.Trim() + "&nbsp";
                                        }

                                    }
                                    //ProductLink.Attributes.Add("onclick", "Clientclick(this)");
                                    BookListoutput.Add(ProductLink);
                                    ProductIteration++;
                                    //ProductPlace.Controls.Add(ProductLink);

                                }
                            }
                        }
                        ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject(BookListoutput);
                    }
                    catch (Exception ex)
                    {
                        ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject("N");
                        DnnLog.Error(ex);
                    }
                    context.Response.Write(ClassJson);
                    break;
                case "REFINESEARCHPRODUCTATTR":
                    try
                    {
                        int prodsk = int.Parse(context.Request.QueryString["productsk"]);
                        string apptarget = context.Request.QueryString["division"];
                        int storesk = int.Parse(UserDetailInfo.StoreID);
                        Session["AttributeState"] = null;
                        string country = UserDetailInfo.CountryCode;//context.Request.QueryString["country"]; 
                        DataSet attributes = IECPR.SelectSearchStudProd(prodsk, apptarget, storesk, country);
                        if (attributes != null && attributes.Tables.Count > 0 && attributes.Tables[0].Rows.Count > 0)
                        {
                            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                            Dictionary<string, object> row;
                            foreach (DataRow dr in attributes.Tables[0].Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in attributes.Tables[0].Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }
                            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rows));
                        }
                        else
                        {
                            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject("N"));
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        DnnLog.Error(ex);
                        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject("N"));
                    }
                    break;
            }

            if (context.Request.QueryString["Action"] == "List")
            {

            }

        }

        /// <summary>
        /// product result handler Splitvaluetocart function
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="cartItems"></param>
        /// <returns></returns>
        private static Cart SplitValueToCart(Cart cart, string cartItems)
        {
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


        /// <summary>
        /// product result handler getcart function
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private Cart GetCart(int userid)
        {
            Cart cart = new Cart();
            //if (userid == 0)
            //{
            //    if (Context.Session["dump"] != null)
            //    {
            //        cart = (Cart)Context.Session["dump"];
            //    }
            //    else 
            if (Context.Request.Cookies["dump"] != null)
            {
                cart = SplitValueToCart(cart, Cryptography.Decrypt(Context.Request.Cookies["dump"].Value, ConfigurationManager.AppSettings["SecureKey"]));
                Context.Session["dump"] = cart;
            }
            //}
            //else
            //{
            //    if (Context.Session["user_" + userid.ToString()] != null)
            //    {
            //        cart = (Cart)Context.Session["user_" + userid.ToString()];
            //    }
            //    else if (Context.Request.Cookies["user_" + userid.ToString()] != null)
            //    {
            //        cart = SplitValueToCart(cart, Context.Request.Cookies["user_" + userid.ToString()].Value);
            //        Context.Session["user_" + userid.ToString()] = cart;
            //    }
            //}
            return cart;
        }

        //protected int AddListProduct(string _productQuantity, string _userListQuoteSK)
        //{
        //    //int returValue = 0;
        //    //if (_productQuantity.Length > 0)
        //    //{
        //    //    _productQuantity = _productQuantity.Substring(0, (_productQuantity.Length - 1)) + ".";
        //    //    ListInfo _objListparameter = new ListInfo { Status = _productQuantity, UserListQuoteSK = _userListQuoteSK };
        //    //    returValue = Common.iEcommerceList.AddListProduct(_objListparameter, ref OutPutValue);
        //    //}
        //    return returValue;
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}