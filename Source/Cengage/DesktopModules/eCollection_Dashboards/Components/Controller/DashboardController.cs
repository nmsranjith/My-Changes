
using System.Collections.Generic;
using System;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using DotNetNuke.Modules.eCollection_Dashboards.Data;
using DotNetNuke.Common.Utilities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
namespace DotNetNuke.Modules.eCollection_Dashboards.Components.Controller
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="DashboardController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A controller class for to perform business activities
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class DashboardController
    {
        #region Class Members
            private static readonly DashboardController dasshboardCtlr = new DashboardController();
            private static readonly List<string> CacheKeys = new List<string>();
        #endregion

        #region Constructor

        private DashboardController() { }

        public static DashboardController Instance
        {
            get
            {                
                return dasshboardCtlr;
            }
        }    

        #endregion

        #region User Details
        /// <summary>
        /// Get logged in user details
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public Users UserDetails(Users user)
        {
            try
            {
                string cacheKey = string.Format("DashBoardUsers{0}{1}", user.UserLoginName, user.SubscriptionId);
                Users userDetails = (Users)DataCache.GetCache(cacheKey);
                if (userDetails == null)
                {
                    userDetails = CBO.FillObject<Users>(DataProvider.Instance().UserDetails(user));
                    if (userDetails != null)
                    {
                        DataCache.SetCache(cacheKey, userDetails, TimeSpan.FromMinutes(5));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                // Users userDetails = new Users() { FirstName = "Jagan", UserRole = "SUBS ADMIN" };
                return userDetails;
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); throw ex;
            }
        }

        /// <summary>
        /// Close the video / get started steps
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int VideoGetStartedClose(Users user)
        {
            try
            {
                ClearAllCache();
                return DataProvider.Instance().VideoGetStartedClose(user);
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); throw ex;
            }
        }
        #endregion

        #region Subscription Details

        /// <summary>
        /// Get all the subscriptions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataTable GetSubscriptionsList(Users user)
        {
            try
            {
                string cacheKey = string.Format("DashBoardSubscriptions{0}", user.UserLoginName);
                DataTable subscriptions = (DataTable)DataCache.GetCache(cacheKey);
                if (subscriptions == null)
                {
                    subscriptions = DataProvider.Instance().GetSubscriptionsList(user);
                    if (subscriptions != null)
                    {
                        DataCache.SetCache(cacheKey, subscriptions, TimeSpan.FromMinutes(3));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                return subscriptions;
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); throw ex;
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public int[] ValidateSubs(int subscription)
        {
            DataSet ds = DataProvider.Instance().ValidateSubs(subscription);
            int[] validation = new int[4];
            try
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    validation[i] = Convert.ToInt32(ds.Tables[i].Rows[0].ItemArray[0].ToString());
                }
                return validation;
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool MigrateRenewalSubscription(UpgradeFlags flags)
        {
            try
            {                
                ClearAllCache();
                eCollection_Students.Components.Controllers.StudentsController.Instance.ClearAllCache();
                eCollection_Teachers.Components.Controller.TeacherController.Instance.ClearAllCache();
                eCollection_Groups.Components.GroupController.Instance.ClearAllCaches();
                eCollection_Sessions.Components.Controllers.SessionController.Instance.ClearAllCaches();
                eCollection_Books.Components.Controllers.BooksController.Instance.ClearAllCache();
                return DataProvider.Instance().MigrateRenewalSubscription(flags);
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool UpgradeSubscription(UpgradeFlags flags)
        {
            try
            {
                ClearAllCache();
                eCollection_Students.Components.Controllers.StudentsController.Instance.ClearAllCache();
                eCollection_Teachers.Components.Controller.TeacherController.Instance.ClearAllCache();
                eCollection_Groups.Components.GroupController.Instance.ClearAllCaches();
                eCollection_Sessions.Components.Controllers.SessionController.Instance.ClearAllCaches();
                eCollection_Books.Components.Controllers.BooksController.Instance.ClearAllCache();
                return DataProvider.Instance().UpgradeSubscription(flags);
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); throw ex;
            }
        }


        /// <summary>
        /// Add a name to a subscription
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public int AddSubscriptionName(Subscription subscription)
        {
            try
            {
                ClearAllCache();
                return DataProvider.Instance().AddSubscriptionName(subscription);
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Module_Name"></param>
        /// <returns></returns>
        public List<Messages> GetErrorMessagesByModuleName(string Module_Name)
        {
            string cacheKey = string.Format("GetErrorMessagesByModuleName{0}", Module_Name);
            List<Messages> moduleMessages = (List<Messages>)DataCache.GetCache(cacheKey);
            if (moduleMessages == null)
            {
                moduleMessages = CBO.FillCollection<Messages>(DataProvider.Instance().GetErrorMessagesByModuleName(Module_Name));              
                if (moduleMessages != null)
                {
                    DataCache.SetCache(cacheKey, moduleMessages);
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return moduleMessages;
        }

        /// <summary>
        /// Get subscription details
        /// </summary>
        /// <param name="subsId"></param>
        /// <returns></returns>
        public Subscription SubscriptionDetails(Users user)
        {
            try
            {
                string cacheKey = string.Format("DashBoardSubscription{0}",user.UserLoginName,user.SubscriptionId);
                Subscription subscriptionDetails = (Subscription)DataCache.GetCache(cacheKey);
                if (subscriptionDetails == null)
                {
                    subscriptionDetails = CBO.FillObject<Subscription>(DataProvider.Instance().SubscriptionDetails(user.SubscriptionId));
                    if (subscriptionDetails != null)
                    {
                        DataCache.SetCache(cacheKey, subscriptionDetails, TimeSpan.FromMinutes(5));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                    else subscriptionDetails = new Subscription();
                }
                //Subscription subscriptionDetails = new Subscription()
                //{
                //    BooksAdded = 300,
                //    TotalLicenses = 1000,
                //    UsedLicenses = 300,
                //    DaysLeft = 300
                //};
                return subscriptionDetails;
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsId"></param>
        /// <returns></returns>
        public Subscription GetStartedDetails(int subsId)
        {
            try
            {
                string cacheKey = string.Format("DashBoardGetStarted{0}", subsId);
                Subscription getStartedDetails = (Subscription)DataCache.GetCache(cacheKey);
                if (getStartedDetails == null)
                {
                    getStartedDetails = new Subscription();
                    IDataReader dr = DataProvider.Instance().GetStartedDetails(subsId);
                    try
                    {
                        dr.Read();
                        getStartedDetails.TeachersCount = int.Parse(dr["TeachersAdded"].ToString());
                        getStartedDetails.BooksAdded = int.Parse(dr["BooksAdded"].ToString());
                        getStartedDetails.TotalBooks = int.Parse(dr["TotalBooks"].ToString());
                        getStartedDetails.UsedLicences = int.Parse(dr["UsedLicences"].ToString());
                    }
                    finally
                    {
                        CBO.CloseDataReader(dr, true);
                    }
                    if (getStartedDetails != null)
                    {
                        DataCache.SetCache(cacheKey, getStartedDetails, TimeSpan.FromMinutes(5));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                return getStartedDetails;
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); throw ex;
            }
        }
        #endregion

        #region Purchase Details
       /// <summary>
        /// Get Purchased Details of the subscription
       /// </summary>
       /// <param name="subsId"></param>
       /// <returns></returns>
        public List<Subscription> GetPurchaseDetails(int subsId)
        {
            try
            {
                string cacheKey = string.Format("DashBoardPurchase{0}", subsId);
                List<Subscription> purchasedList = (List<Subscription>)DataCache.GetCache(cacheKey);
                if (purchasedList == null)
                {
                    purchasedList = (CBO.FillCollection<Subscription>(DataProvider.Instance().GetPurchaseDetails(subsId)));
                    if (purchasedList != null)
                    {
                        DataCache.SetCache(cacheKey, purchasedList, TimeSpan.FromMinutes(5));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                //List<Subscription> purchasedList = new List<Subscription>();
                //string[] adminNames = { "Francis J", "Adams Joseph", "Peter J", "Jagan Babu", "Jagan babu", "Peter J", "Jagan Babu", };
                //int license = 300;
                //for (int i = 0; i < 5; i++)
                //{
                //    purchasedList.Add(new Subscription()
                //      {
                //          BooksAdded = 60,
                //          TotalLicenses = 1000,
                //          UsedLicenses = license,
                //          AdminName = adminNames[i],
                //          StartedDate = DateTime.Now.AddDays(-i)
                //      });
                //    license -= 60;
                //}
                return purchasedList;
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); throw ex;
            }
        }

        #endregion

        #region Daily Activities
        /// <summary>
        /// Get all dates of the activities done by the user
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public IEnumerable<DateCollection> GetAllDates(Subscription subscriptions)
        {
            List<DateCollection> allDates = new List<DateCollection>();
            string cacheKey = string.Format("UsersActivityDates{0}{1}",subscriptions.SubsId, subscriptions.AdminName);
            List<DateCollection> dateList = (List<DateCollection>)DataCache.GetCache(cacheKey);
              if (dateList == null)
              {
                 dateList = (CBO.FillCollection<DateCollection>(DataProvider.Instance().GetAllDates(subscriptions)));                  
                var all= dateList.Select(x => new { x.ActivityType, x.AddedBy, x.Prefix, x.DateCreated }).ToList().Distinct();
                dateList = null;
                dateList= all.AsEnumerable().Select(x => new DateCollection() { ActivityType = x.ActivityType, AddedBy = x.AddedBy, Prefix = x.Prefix, DateCreated = x.DateCreated }).ToList();
                if (dateList != null)
                {
                    DataCache.SetCache(cacheKey, dateList, TimeSpan.FromMinutes(5));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
              }              
            return dateList;
        }

        /// <summary>
        /// Get all particular activity data of the user
        /// </summary>
        /// <param name="subscriptions"></param>
        /// <returns></returns>
        public List<Activity> GetDailyActivities(Subscription subscriptions)
        {
             string cacheKey = string.Format("UsersActivities{0}{1}", subscriptions.SubsId,subscriptions.AdminName);
             subscriptions.ActivityList = (List<Activity>)DataCache.GetCache(cacheKey);
             if (subscriptions.ActivityList  == null)
              {
                  subscriptions.ActivityList = (CBO.FillCollection<Activity>(DataProvider.Instance().GetDailyActivities(subscriptions)));
                  if (subscriptions.ActivityList != null)
                  {
                      DataCache.SetCache(cacheKey, subscriptions.ActivityList, TimeSpan.FromMinutes(5));
                      if (!CacheKeys.Contains(cacheKey))
                          CacheKeys.Add(cacheKey);
                  }
              }          
            return subscriptions.ActivityList;
        }

        /// <summary>
        /// Get all upgraded details of the subscription
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public List<Activity> GetUpgradedActivities(Subscription subscription)
        {
            try
            {
                string cacheKey = string.Format("UsersUpgraded{0}", subscription.SubsId);
                subscription.ActivityList = (List<Activity>)DataCache.GetCache(cacheKey);

                if (subscription.ActivityList == null)
                {
                    subscription.ActivityList = new List<Activity>();
                    IDataReader dr = DataProvider.Instance().GetUpgradedActivities(subscription);
                    try
                    {
                        while (dr.Read())
                        {
                            Activity getUpgradedDetails = new Activity();
                            getUpgradedDetails.Name = "";
                            getUpgradedDetails.UsedLicenses = int.Parse(dr["UsedLicenses"].ToString());
                            getUpgradedDetails.DateCreated = string.Format("{0:dd}{1} {0:MMMM yyyy}", DateTime.Parse(dr["DateCreated"].ToString()), (DateTime.Parse(dr["DateCreated"].ToString()).Day == (1 | 21 | 31)) ? "st" : (DateTime.Parse(dr["DateCreated"].ToString()).Day == (2 | 22)) ? "nd" : (DateTime.Parse(dr["DateCreated"].ToString()).Day == (3 | 23)) ? "rd" : "th");
                            getUpgradedDetails.TotalBooks = int.Parse(dr["TotalBooks"].ToString());
                            subscription.ActivityList.Add(getUpgradedDetails);
                        }
                    }
                    finally
                    {
                        CBO.CloseDataReader(dr, true);
                    }
                    if (subscription.ActivityList != null)
                    {
                        DataCache.SetCache(cacheKey, subscription.ActivityList, TimeSpan.FromMinutes(5));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }

                return subscription.ActivityList;
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); throw ex;
            }
        }
         

        /// <summary>
        /// Get all added books of the subscription with added date
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public List<Activity> GetAllBooksAdded(Subscription subscription)
        {
            try
            {
                string cacheKey = string.Format("UsersBooks{0}", subscription.SubsId);
                subscription.ActivityList = (List<Activity>)DataCache.GetCache(cacheKey);
                if (subscription.ActivityList == null)
                {
                    subscription.ActivityList = (CBO.FillCollection<Activity>(DataProvider.Instance().GetAllBooksAdded(subscription)));
                    if (subscription.ActivityList != null)
                    {
                        DataCache.SetCache(cacheKey, subscription.ActivityList, TimeSpan.FromMinutes(5));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                return subscription.ActivityList;
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); throw ex;
            }
        }

        #endregion

        #region memberfunction
        /// <summary>
        /// 
        /// </summary>
        public void ClearAllCache()
        {
            foreach (string cacheKey in CacheKeys)
                DataCache.RemoveCache(cacheKey);
        }
        #endregion

        #region "After Renewal"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="RenewType"></param>
        /// <param name="NewCustSubs"></param>
        /// <param name="OldCustSubs"></param>
        /// <returns></returns>
        public int? StartRenewal(string loginName, string RenewType, int NewCustSubs, int OldCustSubs)
        {
            try
            {
                if (RenewType != string.Empty)
                {
                    DataProvider.Instance().StartAfterRenewal(loginName, RenewType, NewCustSubs, OldCustSubs);
                }
            }
            catch (SqlException exc)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);
                throw exc;
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="NewCustSubs"></param>
        /// <param name="OldCustSubs"></param>
        /// <returns></returns>
        public int GetRenewalLicenseCount(string loginName, int NewCustSubs, int OldCustSubs)
        {
            try
            {
                int LicenseCount = DataProvider.Instance().GetRenewalLicenseCount(loginName, NewCustSubs, OldCustSubs);
                return LicenseCount;
            }
            catch (SqlException exc)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);
                throw exc;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="NewCustSubs"></param>
        /// <param name="OldCustSubs"></param>
        /// <returns></returns>
        public int UpdateInActiveAndArchive(string loginName, int NewCustSubs, int OldCustSubs)
        {
            try
            {
                int LicenseCount = DataProvider.Instance().UpdateInActiveAndArchive(loginName, NewCustSubs, OldCustSubs);
                ClearAllCache();
                return LicenseCount;
            }
            catch (SqlException exc)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);
                throw exc;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public string GetRenewalSubscriptionName(string loginName)
        {
            try
            {
                string SubscriptionName = DataProvider.Instance().GetRenewalSubscriptionName(loginName);
                return SubscriptionName;
            }
            catch (SqlException exc)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);
                throw exc;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="NewCustSubs"></param>
        /// <param name="OldCustSubs"></param>
        /// <returns></returns>
        public int GetRenewalBookCount(string loginName, int NewCustSubs, int OldCustSubs)
        {
            try
            {
                int BookCount = DataProvider.Instance().GetRenewalBookCount(loginName, NewCustSubs, OldCustSubs);
                return BookCount;
            }
            catch (SqlException exc)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);
                throw exc;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsid"></param>
        /// <returns></returns>
        public List<Renewel> GetAllrenewels(int subsid)//string loginname)
        {
            IDataReader dr = null;
            List<Renewel> list = new List<Renewel>();
            try
            {
                dr = DataProvider.Instance().GetAllRenewels(subsid);//loginname);               
                while (dr.Read())
                {
                    Renewel renewel = new Renewel();
                    renewel.NEW_CUST_SUBS_SK = int.Parse(dr["NEW_CUST_SUBS_SK"].ToString());
                    renewel.OLD_CUST_SUBS_SK = int.Parse(dr["OLD_CUST_SUBS_SK"].ToString());
                    renewel.RENEWED_DATE = DateTime.Parse(dr["RENEWED_DATE"].ToString());
                    renewel.OLD_SUBS_NAME = dr["OLD_SUBS_NAME"].ToString();
                    //group.WordCount = int.Parse(dr["WordCount"].ToString());
                    list.Add(renewel);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
        }

        #endregion      

        #region Request Access
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetCountries()
        {
            return DataProvider.Instance().GetCountries();
        }
        #endregion 

        #region App Data Usage
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public DataTable GetAccountDetails()
            {
                return DataProvider.Instance().GetAccountDetails();
            }
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="fromdate"></param>
            /// <param name="todate"></param>
            /// <param name="accountsk"></param>
            /// <param name="reportType"></param>
            /// <returns></returns>
            public DataSet GetReportInfo(DateTime fromdate, DateTime todate, int accountsk, string reportType)
            {
                return DataProvider.Instance().GetReportInfo(fromdate, todate, accountsk, reportType);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="Account_Sk"></param>
            /// <param name="fromdate"></param>
            /// <param name="todate"></param>
            /// <returns></returns>
            public DataSet GetAccountInformation(int Account_Sk, DateTime fromdate, DateTime todate)
            {
                return DataProvider.Instance().GetAccountInformation(Account_Sk,fromdate ,todate);
            }
        #endregion  

        #region Customer Service
        /// <summary>
        /// 
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public int UploadTeacherProfiles(XmlDocument doc)
        {
            eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
            return DataProvider.Instance().UploadTeacherProfiles(doc);
        }
        #endregion

        #region PM Content Management -- Audio Files Insertion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="audio"></param>
        /// <returns></returns>
        public int InsertAudioFiles(Audio audio)
        {
            try
            {
                return DataProvider.Instance().InsertAudioFiles(audio);
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region EXCEPTION MAIL
            /// <summary>
            /// 
            /// </summary>
            /// <param name="body"></param>
            /// <param name="errorMessage"></param>
            /// <param name="SubsSk"></param>
            public void SendExceptionMail(string body,string errorMessage,int SubsSk)
            {
                DataProvider.Instance().SendExceptionMail(body, errorMessage, SubsSk);
            }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public string GetUserinfo(string guid)
        {
            try
            {
                return DataProvider.Instance().GetUserinfo(guid);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}