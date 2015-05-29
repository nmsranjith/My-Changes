using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using System.Data.SqlClient;
using DotNetNuke.Modules.eCollection_Sessions.Components.Interfaces;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;
using DotNetNuke.Modules.eCollection_Sessions.Data;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using DotNetNuke.Modules.eCollection_Sessions.Components.ExceptionHandling;
using System.Collections;
using System.Data;

namespace DotNetNuke.Modules.eCollection_Sessions.Components.Controllers
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentsController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A controller class for to perform business activities
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public sealed class SessionController : IDataRepository<Sessions,int>
    {
        private static readonly SessionController instance = new SessionController();

        private static readonly List<string> CacheKeys = new List<string>();

        private SessionController() { }

        public static SessionController Instance
        {
            get
            {
                return instance;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public Sessions Get(int sessionId)
        {
            return CBO.FillObject<Sessions>(DataProvider.Instance().GetSessions(sessionId));                           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portalId"></param>
        /// <param name="sortAsc"></param>
        /// <returns></returns>
        public List<Sessions> GetAll(int portalId, bool sortAsc)
        {
            return CBO.FillCollection<Sessions>(DataProvider.Instance().GetAllSessions(portalId, sortAsc));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public int? Add(Sessions session)
        {
            try
            {
                if (session != null)
                {
                    if (session.SessionId <= 0)
                    {
                        session.SessionId = DataProvider.Instance().AddSession(session);
                    }
                    else
                    {
                        session.SessionId = DataProvider.Instance().UpdateSession(session);
                    }
                    ClearAllCaches();
                    HttpContext.Current.Session["SelectedGroups"] = null;
                    HttpContext.Current.Session["SelectedProducts"] = null;
                }
            }
            catch (SqlException exc)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);               
                throw exc;
            }
            return session.SessionId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public void Update(Sessions session)
        {
            try
            {
                if (session != null)
                {
                    if (session.SessionId > Null.NullInteger)
                    {
                        DataProvider.Instance().UpdateSession(session);
                    }
                    ClearAllCaches();
                    HttpContext.Current.Session["SelectedGroups"] = null;
                    HttpContext.Current.Session["SelectedProducts"] = null;
                }
            }
            catch (SqlException exc)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);
                if (exc.Message.Contains("unique_sessionName"))
                    throw new SessionValidationException("Session name already exists. Please specify a unique session name", MyEnums.CrudState.Update);
                throw exc;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        public void Delete(int sessionId)
        {
            if (sessionId > Null.NullInteger)
            {
                var a = Get(sessionId);

                //DataProvider.Instance().DeleteSession(sessionId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="portalID"></param>
        /// <param name="sortAsc"></param>
        /// <returns></returns>
        public List<Sessions> GetCachedSessions(int roleID, int portalID, bool sortAsc)
        {
            var strCacheKey = "SessionList-Cache-" + roleID + "-" + portalID;
            var sessionList = (List<Sessions>)DataCache.GetCache(strCacheKey);

            if (sessionList == null)
            {
                // caching settings
                var timeOut = 20 * Convert.ToInt32(Entities.Host.Host.PerformanceSetting);

                sessionList = GetAll(portalID, sortAsc);

                //Cache List if timeout > 0 and collection is not null
                if (timeOut > 0 & sessionList != null)
                {
                    DataCache.SetCache(strCacheKey, sessionList, new TimeSpan(0, 0, 0));
                }
            }
            return sessionList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="portalID"></param>
        public static void ClearCachedMembers(int roleID, int portalID)
        {
            var strCacheKey = "SessionList-Cache-" + roleID + "-" + portalID;
            DataCache.ClearCache(strCacheKey);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static IList<Sessions> GetSortedSessions(int sessionId)
        {
            return CBO.FillSortedList<string, Sessions>("SessionId", DataProvider.Instance().GetSessions(sessionId)).Values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static ArrayList GetSessionsArrayList(int sessionId)
        {
            return CBO.FillCollection(DataProvider.Instance().GetSessions(sessionId), typeof(Sessions));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loginName"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public List<Sessions> GetAllActiveSessions(int userId, string loginName,int subId) 
        {
            string cacheKey = string.Format("GetAllActiveSessions{0}", userId);
            List<Sessions> activeSessions = (List<Sessions>)DataCache.GetCache(cacheKey);
            if (activeSessions == null)
            {
                activeSessions = CBO.FillCollection<Sessions>(DataProvider.Instance().GetAllSessions(userId,loginName, subId));
                //activeSessions = Enumerable.Range(1, 20).Select(a1 => new Sessions() { SessionId = 1, Name = "ActiveSession" + a1, CreatedByUserId = 1, SessionCreatedDate = DateTime.Now.AddMinutes(a1) }).ToList();
                if (activeSessions != null)
                {
                    DataCache.SetCache(cacheKey, activeSessions, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return activeSessions;        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loginName"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public List<Sessions> GetAllFinishedSessions(int userId, string loginName, int subId)
        {

            string cacheKey = string.Format("GetAllFinishedSessions{0}", userId);
            List<Sessions> finishedSessions = (List<Sessions>)DataCache.GetCache(cacheKey);
            if (finishedSessions == null)
            {
                finishedSessions = CBO.FillCollection<Sessions>(DataProvider.Instance().GetAllSessions(userId, loginName, subId));
                //finishedSessions = Enumerable.Range(1, 20).Select(a1 => new Sessions() { SessionId = 1, Name = "FinishedSession" + a1, CreatedByUserId = 1, SessionCreatedDate = DateTime.Now.AddMinutes(a1) }).ToList();
                if (finishedSessions != null)
                {
                    DataCache.SetCache(cacheKey, finishedSessions, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return finishedSessions;        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loginName"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public List<Sessions> GetAllArchivedSessions(int userId, string loginName, int subId)
        {
            string cacheKey = string.Format("GetAllArchivedSessions{0}", userId);
            List<Sessions> archivedSessions = (List<Sessions>)DataCache.GetCache(cacheKey);
            if (archivedSessions == null)
            {
                archivedSessions = CBO.FillCollection<Sessions>(DataProvider.Instance().GetAllSessions(userId, loginName, subId));
                //archivedSessions = Enumerable.Range(1, 20).Select(a1 => new Sessions() { SessionId = 1, Name = "ArchivedSession" + a1, CreatedByUserId = 1, SessionCreatedDate = DateTime.Now.AddMinutes(a1), SessionExpiryDate = DateTime.Now.AddDays(-35) }).ToList();
                if (archivedSessions != null)
                {
                    DataCache.SetCache(cacheKey, archivedSessions, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return archivedSessions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<IDCollection> GetSubscription(int userId, string loginName)
        {
            string cacheKey = string.Format("GetSubscription{0}", userId);
            List<IDCollection> subscription = (List<IDCollection>)DataCache.GetCache(cacheKey);
            if (subscription == null)
            {
                subscription = CBO.FillCollection<IDCollection>(DataProvider.Instance().GetSubscription(userId, loginName));
            }
            return subscription;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<IDCollection> GetTeachers(int subId, string loginName)
        {
            string cacheKey = string.Format("GetTeachers{0}", subId);
            List<IDCollection> teachers = (List<IDCollection>)DataCache.GetCache(cacheKey);
            if (teachers == null)
            {
                teachers = CBO.FillCollection<IDCollection>(DataProvider.Instance().GetTeachers(subId, loginName));
                //archivedSessions = Enumerable.Range(1, 20).Select(a1 => new Sessions() { SessionId = 1, Name = "ArchivedSession" + a1, CreatedByUserId = 1, SessionCreatedDate = DateTime.Now.AddMinutes(a1) ,SessionExpiryDate = DateTime.Now.AddDays(-35) }).ToList();
                if (teachers != null)
                {
                    DataCache.SetCache(cacheKey, teachers, new TimeSpan(0, 1, 0));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return teachers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<Teacher> GetAllTeachersList(int subId, string loginName)
        {
            string cacheKey = string.Format("GetAllTeachersList{0}", subId);
            List<Teacher> teachers = (List<Teacher>)DataCache.GetCache(cacheKey);
            if (teachers == null)
            {
                teachers = CBO.FillCollection<Teacher>(DataProvider.Instance().GetTeachers(subId, loginName));
                //archivedSessions = Enumerable.Range(1, 20).Select(a1 => new Sessions() { SessionId = 1, Name = "ArchivedSession" + a1, CreatedByUserId = 1, SessionCreatedDate = DateTime.Now.AddMinutes(a1) ,SessionExpiryDate = DateTime.Now.AddDays(-35) }).ToList();
                if (teachers != null)
                {
                    DataCache.SetCache(cacheKey, teachers, new TimeSpan(0, 1, 0));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return teachers;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupType"></param>
        /// <param name="loginName"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public List<Group> GetGroups(MyEnums.GroupType groupType, string loginName,int subId)
        {
            string cacheKey = string.Format("GetGroups{0}", groupType);
            List<Group> groups = (List<Group>)DataCache.GetCache(cacheKey);
            List<Group> groupsList = new List<Group>();
            if (groups == null)
            {
                groupsList = CBO.FillCollection<Group>(DataProvider.Instance().GetGroups(groupType, loginName, subId)); 
                //archivedSessions = Enumerable.Range(1, 20).Select(a1 => new Sessions() { SessionId = 1, Name = "ArchivedSession" + a1, CreatedByUserId = 1, SessionCreatedDate = DateTime.Now.AddMinutes(a1) ,SessionExpiryDate = DateTime.Now.AddDays(-35) }).ToList();
                if (groupsList != null)
                {
                    DataCache.SetCache(cacheKey, groupsList, new TimeSpan(0, 1, 0));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
                groups = groupsList;
            }
            return groupsList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<string> GetTeacherName(int groupId)
        {
            IDataReader dr = DataProvider.Instance().GetTeacherName(groupId);
            List<string> list = new List<string>();
            try
            {
                while (dr.Read())
                {
                    list.Add(dr["FIRST_NAME"].ToString() + " " + dr["LAST_NAME"].ToString());
                }
                
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearAllCaches()
        {
            foreach (string cacheKey in CacheKeys)
                DataCache.RemoveCache(cacheKey);
            eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <returns></returns>
        public List<Books> GetBooks(int subId)
        {
            string cacheKey = string.Format("GetBooks{0}", subId);
            List<Books> books = (List<Books>)DataCache.GetCache(cacheKey);
            

            if (books == null)
            {
                books = CBO.FillCollection<Books>(DataProvider.Instance().GetBooks(subId));
            }
            return books;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<Student> GetStudentsList(int subId,string loginName)
        {
            string cacheKey = string.Format("GetStudentsList{0}", subId);
            List<Student> studentsDetails = (List<Student>)DataCache.GetCache(cacheKey);
            if (studentsDetails == null)
            {
                studentsDetails = CBO.FillCollection<Student>(DataProvider.Instance().GetStudentsList(subId,loginName));
                if (studentsDetails != null)
                {
                    DataCache.SetCache(cacheKey, studentsDetails, new TimeSpan(0, 1, 0));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return studentsDetails;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<Student> GetStudentsFullList(int subId, string loginName)
        {
            string cacheKey = string.Format("GetFullStudentsList{0}", subId);
            List<Student> studentsDetails = (List<Student>)DataCache.GetCache(cacheKey);
            if (studentsDetails == null)
            {
                studentsDetails = CBO.FillCollection<Student>(DataProvider.Instance().GetStudentsList(subId, loginName));
                if (studentsDetails != null)
                {
                    DataCache.SetCache(cacheKey, studentsDetails, new TimeSpan(0, 1, 0));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return studentsDetails;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDCollectionLists"></param>
        /// <param name="loginName"></param>
        public void UpdateSessionExpiryDate(List<IDCollection> IDCollectionLists, string loginName)
        {
            try
            {               
                    DataProvider.Instance().UpdateSessionExpiryDate(IDCollectionLists,loginName);
                    
                    ClearAllCaches();                    
               
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
        /// <param name="sessionId"></param>
        /// <param name="SessionExpiryDate"></param>
        /// <param name="loginName"></param>
        public void UpdateSessionExpiryDateAlone(int sessionId, DateTime SessionExpiryDate,string loginName)
        {
            try
            {
                if (sessionId != null)
                {
                    DataProvider.Instance().UpdateSessionExpiryDateAlone(sessionId, SessionExpiryDate, loginName);
                    ClearAllCaches();
                }
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
        /// <param name="IDCollectionLists"></param>
        /// <param name="loginName"></param>
        public void DeleteSession(List<IDCollection> IDCollectionLists,string loginName)
        {
            try
            {                
                DataProvider.Instance().DeleteSession(IDCollectionLists,loginName);

                ClearAllCaches();
                
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
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public List<KeyValuePair<DateTime, string>> GetSessionOpenedStudents(int sessionId)
        {
            string cacheKey = string.Format("GetSessionOpenedStudents{0}", sessionId);
            List<KeyValuePair<DateTime, string>> openedStudents = (List<KeyValuePair<DateTime, string>>)DataCache.GetCache(cacheKey);
            if (openedStudents == null)
            {
                IDataReader dr=  DataProvider.Instance().GetSessionOpenedStudents(sessionId);
                openedStudents = new List<KeyValuePair<DateTime, string>>();
                try
                {
                    while (dr.Read())
                    {
                        openedStudents.Add(new KeyValuePair<DateTime, string>((DateTime)dr["SessionOpenedTime"], dr["StudentName"].ToString().Length > 20 ? string.Concat(dr["StudentName"].ToString().Substring(0, 15), " ..") : dr["StudentName"].ToString()));
                    }
                    if (openedStudents.Count == 0) openedStudents = null;
                }
                finally
                {
                    CBO.CloseDataReader(dr, true);
                }

            }
            return openedStudents;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public List<string> GetSessionUnOpenedStudents(int sessionId)
        {
            string cacheKey = string.Format("GetSessionUnOpenedStudents{0}", sessionId);
            List<string> unOpenedStudents = (List<string>)DataCache.GetCache(cacheKey);
            if (unOpenedStudents == null)
            {
                IDataReader dr = DataProvider.Instance().GetSessionUnOpenedStudents(sessionId);
                unOpenedStudents = new List<string>();
                try
                {
                    while (dr.Read())
                    {
                        unOpenedStudents.Add(dr["StudentName"].ToString().Length > 25 ? string.Concat(dr["StudentName"].ToString().Substring(0, 23), " ..") : dr["StudentName"].ToString());
                    }
                    if (unOpenedStudents.Count == 0) unOpenedStudents = null;
                }
                finally
                {
                    CBO.CloseDataReader(dr, true);
                }
            }
            return unOpenedStudents;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public List<SessionMembers> GetSessionMembers(int sessionId)
        {
            List<SessionMembers> SelectedSessionMembers = new List<SessionMembers>();
            string cacheKey = string.Format("GetSessionMembers{0}", sessionId);
            List<SessionMembers> SessionMembersList = (List<SessionMembers>)DataCache.GetCache(cacheKey);
            if (SessionMembersList == null)
            {
                IDataReader dr = DataProvider.Instance().GetSessionMembers(sessionId);

                try
                {
                    while (dr.Read())
                    {
                        if (dr["MemberType"].ToString().Equals("GROUP"))
                        {
                            SessionMembers sessionMembers = new SessionMembers()
                            {
                                GRP_MEM_SK = int.Parse(dr["GROUP_SK"].ToString()),
                                GroupName = dr["NAME"].ToString(),
                                MemberType = "GROUP",
                                Added_date = DateTime.Parse(dr["AddedDate"].ToString()),
                                SESSION_MEMBER_SK = int.Parse(dr["SESSION_MEMBER_SK"].ToString())
                            };
                            if (!SelectedSessionMembers.Contains(sessionMembers))
                            {
                                SelectedSessionMembers.Add(sessionMembers);
                            }
                        }
                        if (dr["MemberType"].ToString().Equals("STUDENT"))
                        {
                            SessionMembers sessionMembers = new SessionMembers()
                            {
                                CUST_SUBS_USER_SK = int.Parse(dr["GROUP_SK"].ToString()),
                                StudentName = dr["NAME"].ToString(),
                                MemberType = "USER",
                                Added_date = DateTime.Parse(dr["AddedDate"].ToString()),
                                SESSION_MEMBER_SK = int.Parse(dr["SESSION_MEMBER_SK"].ToString())
                            };
                            bool studentExists = SelectedSessionMembers.Any(e => e.CUST_SUBS_USER_SK == sessionMembers.CUST_SUBS_USER_SK);
                            if (!studentExists)
                            {
                                SelectedSessionMembers.Add(sessionMembers);
                            }
                        }
                        if (dr["MemberType"].ToString().Equals("TEACHER") || dr["MemberType"].ToString().Equals("SUBS ADMIN"))
                        {
                            SessionMembers sessionMembers = new SessionMembers()
                            {
                                CUST_SUBS_USER_SK = int.Parse(dr["GROUP_SK"].ToString()),
                                TeacherName = dr["NAME"].ToString(),
                                MemberType = "USER",
                                Added_date = DateTime.Parse(dr["AddedDate"].ToString()),
                                SESSION_MEMBER_SK = int.Parse(dr["SESSION_MEMBER_SK"].ToString())
                            };
                            bool studentExists = SelectedSessionMembers.Any(e => e.CUST_SUBS_USER_SK == sessionMembers.CUST_SUBS_USER_SK);
                            if (!studentExists)
                            {
                                SelectedSessionMembers.Add(sessionMembers);
                            }
                        }
                    }
                }
                finally
                {
                    CBO.CloseDataReader(dr, true);
                }
                //if (SessionMembersList != null)
                //{
                //    DataCache.SetCache(cacheKey, SessionMembersList, new TimeSpan(0, 0, 0));
                //    if (!CacheKeys.Contains(cacheKey))
                //        CacheKeys.Add(cacheKey);
                //}
            }
            return SelectedSessionMembers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public List<SessionProducts> GetSessionProducts(int sessionId)
        {
            List<SessionProducts> SelectedSessionProducts = new List<SessionProducts>();
            string cacheKey = string.Format("GetSessionProducts{0}", sessionId);
            List<SessionProducts> SessionProductsList = (List<SessionProducts>)DataCache.GetCache(cacheKey);
            List<Books> books = (List<Books>)DataCache.GetCache(cacheKey);
            if (SessionProductsList == null)
            {
                IDataReader dr = DataProvider.Instance().GetSessionProducts(sessionId);
                List<Books> templist = new List<Books>();

                try
                {
                    while (dr.Read())
                    {

                        Books book = new Books();
                        book.PRODUCT_SK = Convert.ToInt32(dr["PRODUCT_SK"]);
                        book.CUST_SUBS_ITEM_SK = Convert.ToInt32(dr["CUST_SUBS_ITEM_SK"]);
                        book.IMAGE_FILE_NAME = dr["IMAGE_FILE_NAME"].ToString();
                        book.ADDED_DATE = DateTime.Parse(dr["ADDED_DATE"].ToString());
                        book.Title = dr["Title"].ToString().Replace("eBook:", "");
                        book.COPYRIGHT_YEAR = dr["COPYRIGHT_YEAR"].ToString();
                        //book.NO_OF_PAGES = Null.SetNullInteger(dr["NO_OF_PAGES"]) < 0 ? "0" : dr["NO_OF_PAGES"].ToString();
                        book.Author = dr["Author"].ToString();
                        book.ATTRIBUTE_TYPE_ID = dr["ATTRIBUTE_TYPE_ID"].ToString();
                        book.ATTRIBUTE_TYPE_VALUE = dr["ATTR_VALUE"].ToString();

                        templist.Add(book);
                    }
                }
                finally
                {
                    CBO.CloseDataReader(dr, true);
                }
                List<Books> list = new List<Books>();
                int count = templist.Count;
                while (templist.Count > 0)
                {

                    Books book1 = new Books();
                    Books book2 = templist.First();
                    book1.ReadingLevel = book2.ReadingLevel;
                    book1.ADDED_DATE = book2.ADDED_DATE;
                    book1.CUST_SUBS_ITEM_SK = book2.CUST_SUBS_ITEM_SK;
                    book1.IMAGE_FILE_NAME = book2.IMAGE_FILE_NAME;

                    book1.Title = book2.Title;
                    book1.COPYRIGHT_YEAR = book2.COPYRIGHT_YEAR;
                    // book1.NO_OF_PAGES = book2.NO_OF_PAGES;
                    book1.Author = book2.Author;
                    book1.PRODUCT_SK = book2.PRODUCT_SK;
                    //templist.Remove(book2);
                    int[] index = new int[templist.Count];
                    int count1 = 0;
                    foreach (Books booklist in templist)
                    {
                        if (booklist.PRODUCT_SK == book1.PRODUCT_SK)
                        {
                            if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "GUIDED READING LEVEL")
                            {
                                book1.ReadingLevel = booklist.ATTRIBUTE_TYPE_VALUE;
                            }
                            if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "COLOUR LEVEL")
                            {
                                book1.ColourLevel = booklist.ATTRIBUTE_TYPE_VALUE;
                            }
                            if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "READING AGE")
                            {
                                book1.ReadingAge = booklist.ATTRIBUTE_TYPE_VALUE;
                            }
                            if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "TEXT TYPE")
                            {
                                book1.TEXTTYPE = booklist.ATTRIBUTE_TYPE_VALUE;
                            }
                            index[count1] = templist.IndexOf(booklist);
                            count1++;
                        }

                    }

                    list.Add(book1);
                    while (count1 > 0)
                    {

                        for (int i = count1 - 1; i >= 0; i--)
                        {
                            templist.RemoveAt(index[i]);
                            count1--;
                        }
                    }

                }

                books = list;



                foreach (Books booklist in list)
                {
                    if (booklist.CUST_SUBS_ITEM_SK.ToString().Length > 0)
                    {
                        SessionProducts sessionProducts = new SessionProducts()
                        {
                            CUST_SUBS_ITEM_SK = booklist.CUST_SUBS_ITEM_SK,
                            Books_AddedDate = booklist.ADDED_DATE,
                            ImageFileName = booklist.IMAGE_FILE_NAME,
                            PreferredName = booklist.Author,
                            Title = booklist.Title,
                            CopyRightYear = booklist.COPYRIGHT_YEAR,
                            NoOfPages = booklist.NO_OF_PAGES,
                            ReadingLevel = booklist.ReadingLevel,
                            ReadingAge = booklist.ReadingAge,
                            AttributeTypeValue=booklist.ATTRIBUTE_TYPE_VALUE,
                            ColourLevel = booklist.ColourLevel,
                            TEXTTYPE=booklist.TEXTTYPE,                            
                        };
                        if (!SelectedSessionProducts.Contains(sessionProducts))
                        {
                            SelectedSessionProducts.Add(sessionProducts);
                        }
                    }
                }


            }
            return SelectedSessionProducts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SessionId"></param>
        /// <param name="MonthName"></param>
        /// <returns></returns>
        public List<Group> GetReadingHistoryBySession(int SessionId,string MonthName)
        {
            string strBookOpened = string.Empty;
            IDataReader dr = DataProvider.Instance().GetReadingHistory(SessionId, MonthName);
            List<Group> list = new List<Group>();
            Int32 BookOpenedMin = 0;
            try
            {
                while (dr.Read())
                {
                    Group group = new Group();
                    group.StudentNames = dr["UserName"].ToString();
                    group.UserID = int.Parse(dr["CUST_SUBS_USER_SK"].ToString());                   
                    group.WordCount = int.Parse(dr["WordCount"].ToString()); 
                    group.Title = dr["Title"].ToString();
                    //group.ISBN = dr["ISBN"].ToString();
                    group.BookImgName = dr["BookImgName"].ToString();
                    group.SessionType = dr["SessionType"].ToString();
                    group.SessionCreatedDate = dr["SessionCreatedDate"].ToString() == string.Empty ? DateTime.Now : DateTime.Parse(dr["SessionCreatedDate"].ToString());
                    group.BookOpenAt = DateTime.Parse(dr["BookOpenAt"].ToString());
                    BookOpenedMin = Int32.Parse(DateTime.Parse(dr["BookClosedAt"].ToString()).Subtract(DateTime.Parse(dr["BookOpenAt"].ToString())).TotalSeconds.ToString());
                    if (BookOpenedMin > 3600)
                    {
                        strBookOpened = (BookOpenedMin/3600).ToString() + "." + (BookOpenedMin % 3600).ToString("00") + " hrs";
                    }
                    else
                    {
                        strBookOpened = (BookOpenedMin / 60).ToString() + "." + (BookOpenedMin % 60).ToString("00") + " mins";
                    }
                    group.BooksOpenedMin = strBookOpened;
                    
                    group.SessionHistoryId = int.Parse(dr["SESS_HIST_SK"].ToString());
                    list.Add(group);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SessionId"></param>
        /// <param name="month"></param>
        /// <param name="studentId"></param>
        /// <param name="sessHistoryId"></param>
        /// <returns></returns>
        public List<Group> GetRecordingHistoryBySession(int SessionId, string month, int studentId,int sessHistoryId)
        {
            IDataReader dr = DataProvider.Instance().GetRecordingHistory(SessionId,month, studentId, sessHistoryId);
            List<Group> list = new List<Group>();

            try
            {
                while (dr.Read())
                {
                    Group group = new Group();
                    group.PageName = dr["Page_Name"].ToString();
                    group.RecordFilePath = dr["RCRD_FILE_PATH"].ToString();
                    group.SessionType = dr["SessionType"].ToString();
                    group.BookOpenAt = DateTime.Parse(dr["BOOK_OPENED_AT"].ToString());
                    //group.WordCount = int.Parse(dr["WordCount"].ToString());
                    list.Add(group);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SessionId"></param>
        /// <returns></returns>
        public List<Group> GetReadingHistoryBySessionLastSeven(int SessionId)
        {
            string strBookOpened = string.Empty;
            IDataReader dr = DataProvider.Instance().GetReadingHistoryLastSeven(SessionId);
            List<Group> list = new List<Group>();
            Int32 BookOpenedMins = 0;
            try
            {
                while (dr.Read())
                {
                    Group group = new Group();
                    group.StudentNames = dr["UserName"].ToString();
                    group.UserID = int.Parse(dr["CUST_SUBS_USER_SK"].ToString());
                    //group.MemberID = int.Parse(dr["MemberId"].ToString());
                    //group.RecordFilePath = dr["RecordFilePath"].ToString();
                    //if (dr["RecordStartTime"].ToString() != string.Empty  )
                    //{
                    //    group.RecordStartTime = DateTime.Parse(dr["RecordStartTime"].ToString());
                    //}
                    //group.CircledWord = dr["CircledWord"].ToString();
                    group.WordCount = int.Parse(dr["WordCount"].ToString()); 
                    group.Title = dr["Title"].ToString();
                    //group.ISBN = dr["ISBN"].ToString();
                    group.BookImgName = dr["BookImgName"].ToString();
                    group.SessionType = dr["SessionType"].ToString();
                    group.SessionCreatedDate = dr["SessionCreatedDate"].ToString() == string.Empty ? DateTime.Now : DateTime.Parse(dr["SessionCreatedDate"].ToString());
                    group.BookOpenAt = DateTime.Parse(dr["BookOpenAt"].ToString());
                    BookOpenedMins = Int32.Parse(DateTime.Parse(dr["BookClosedAt"].ToString()).Subtract(DateTime.Parse(dr["BookOpenAt"].ToString())).TotalSeconds.ToString());
                    if (BookOpenedMins > 3600)
                    {
                        strBookOpened = (BookOpenedMins / 3600).ToString() + "." + (BookOpenedMins % 3600).ToString("00") + " hrs";
                    }
                    else
                    {
                        strBookOpened = (BookOpenedMins / 60).ToString() + "." + (BookOpenedMins % 60).ToString("00") + " mins";
                    }
                    group.BooksOpenedMin = strBookOpened;
                    group.SessionHistoryId = int.Parse(dr["SESS_HIST_SK"].ToString());
                    list.Add(group);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SessionId"></param>
        /// <param name="studentId"></param>
        /// <param name="sessHistoryId"></param>
        /// <returns></returns>
        public List<Group> GetRecordingHistoryBySessionLastSeven(int SessionId, int studentId, int sessHistoryId)
        {
            IDataReader dr = DataProvider.Instance().GetRecordingHistoryLastSeven(SessionId, studentId, sessHistoryId);
            List<Group> list = new List<Group>();

            try
            {
                while (dr.Read())
                {
                    Group group = new Group();
                    group.PageName = dr["Page_Name"].ToString();
                    group.RecordFilePath = dr["RCRD_FILE_PATH"].ToString();
                    group.BookOpenAt = DateTime.Parse(dr["BookOpenAt"].ToString());
                    group.SessionType = dr["SessionType"].ToString();
                    //group.WordCount = int.Parse(dr["WordCount"].ToString());
                    list.Add(group);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public List<string> GetSessionHistoryBySessionMonthNames(int sessionId)
        {
            string cacheKey = string.Format("GetSessionHistoryBySessionMonthNames{0}", sessionId);
            List<string> MonthNames = (List<string>)DataCache.GetCache(cacheKey);
            if (MonthNames == null)
            {
                IDataReader dr = DataProvider.Instance().GetSessionHistoryBySessionMonthNames(sessionId);
                MonthNames = new List<string>();

                try
                {
                    while (dr.Read())
                    {
                        MonthNames.Add(dr["BookOpenedMonth"].ToString());
                    }
                    if (MonthNames.Count == 0) MonthNames = null;

                    //if (MonthNames != null)
                    //{
                    //    DataCache.SetCache(cacheKey, MonthNames, new TimeSpan(0, 0, 0));
                    //    if (!CacheKeys.Contains(cacheKey))
                    //        CacheKeys.Add(cacheKey);
                    //
                }
                finally
                {
                    CBO.CloseDataReader(dr, true);
                }
            }
            return MonthNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<int> GetReadingLevel(int groupId)
        {
            IDataReader dr = DataProvider.Instance().GetReadingLevel(groupId);
            List<int> list = new List<int>();


            try
            {
                while (dr.Read())
                {
                    list.Add(int.Parse(dr["MaxReadLevel"].ToString()));
                    list.Add(int.Parse(dr["MinReadLevel"].ToString()));
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<Student> GetStudentByGroup(int groupId)
        {

            IDataReader dr = DataProvider.Instance().GetStudentByGroup(groupId);
            List<Student> list = new List<Student>();

            try
            {
                while (dr.Read())
                {
                    Student student = new Student();
                    student.CUST_SUBS_SK = int.Parse(dr["CUST_SUBS_USER_SK"].ToString());
                    student.StudentName = dr["USER_LOGIN_NAME"].ToString();
                    student.CurrentReadingLevel = Convert.ToInt32(dr["CurrentReadingLevel"]);
                    student.Checked = false;                    
                    list.Add(student);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerSubSK"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<IDCollection> GetClassListBySubscription(string CustomerSubSK, string userLoginName)
        {
            return CBO.FillCollection<IDCollection>(DataProvider.Instance().GetClassListBySubscription(CustomerSubSK, userLoginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupType"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<Group> GetGroupsByGroupID(int groupId, char groupType, string userLoginName)
        {
            return CBO.FillCollection<Group>(DataProvider.Instance().GetGroupsByGroupID(groupId, groupType, userLoginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerSubSK"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<IDCollection> GetGroupListBySubscription(string CustomerSubSK, string userLoginName)
        {
            return CBO.FillCollection<IDCollection>(DataProvider.Instance().GetgroupListBySubscription(CustomerSubSK, userLoginName));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="fromReadLvl"></param>
        /// <param name="toReadLvl"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public List<Books> GetBooksByReadingLevel(int subId, int fromReadLvl, int toReadLvl,string LoginName)
        { 
            string cacheKey = string.Format("GetSessionBooksByReadingLevel{0}", subId);
            List<Books> books = (List<Books>)DataCache.GetCache(cacheKey);            
            if (books == null)
            {
                IDataReader dr = DataProvider.Instance().GetBooksByReadingLevel(subId, fromReadLvl, toReadLvl, LoginName);
                List<Books> templist = new List<Books>();
                try
                {
                    while (dr.Read())
                    {

                        Books book = new Books();
                        book.PRODUCT_SK = Convert.ToInt32(dr["PRODUCT_SK"]);
                        book.CUST_SUBS_ITEM_SK = Convert.ToInt32(dr["CUST_SUBS_ITEM_SK"]);
                        book.IMAGE_FILE_NAME = dr["IMAGE_FILE_NAME"].ToString();
                        book.IMAGE_TYPE = dr["IMAGE_TYPE"].ToString();
                        book.Title = dr["Title"].ToString().Replace("eBook:", "");
                        book.COPYRIGHT_YEAR = dr["COPYRIGHT_YEAR"].ToString();
                        book.NO_OF_PAGES = Null.SetNullInteger(dr["NO_OF_PAGES"]) < 0 ? "0" : dr["NO_OF_PAGES"].ToString();
                        book.Author = dr["PREFERRED_NAME"].ToString();
                        book.ATTRIBUTE_TYPE_ID = dr["ATTRIBUTE_TYPE_ID"].ToString();
                        book.ATTRIBUTE_TYPE_VALUE = dr["ATTR_VALUE"].ToString();
                        book.Checked = false;
                        book.ClassName = string.Empty;
                        book.CheckImgPathName = string.Empty;
                        templist.Add(book);
                    }

                    //


                    List<Books> list = new List<Books>();

                    int count = templist.Count;
                    int[] booksList = GetBookListByLevels(subId, fromReadLvl, toReadLvl, LoginName);


                    for (int i = 0; i < booksList.Length; i++)
                    {
                        if (templist.Count > 0)
                        {
                            Books book1 = new Books();
                            book1.PRODUCT_SK = booksList[i];
                            int[] index = new int[templist.Count];
                            int count1 = 0;
                            foreach (Books booklist in templist)
                            {
                                if (booklist.PRODUCT_SK == booksList[i])
                                {
                                    book1.CUST_SUBS_ITEM_SK = booklist.CUST_SUBS_ITEM_SK;
                                    book1.IMAGE_FILE_NAME = booklist.IMAGE_FILE_NAME;
                                    book1.IMAGE_TYPE = booklist.IMAGE_TYPE;
                                    book1.Title = booklist.Title;
                                    book1.COPYRIGHT_YEAR = booklist.COPYRIGHT_YEAR;
                                    book1.NO_OF_PAGES = booklist.NO_OF_PAGES;
                                    book1.Author = booklist.Author;
                                    book1.Checked = booklist.Checked;
                                    book1.ClassName = booklist.ClassName;
                                    book1.CheckImgPathName = booklist.CheckImgPathName;
                                    if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "GUIDED READING LEVEL")
                                    {
                                        book1.ReadingLevel = booklist.ATTRIBUTE_TYPE_VALUE;
                                    }
                                    if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "COLOUR LEVEL")
                                    {
                                        book1.ColourLevel = booklist.ATTRIBUTE_TYPE_VALUE;
                                    }
                                    if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "READING AGE")
                                    {
                                        book1.ReadingAge = booklist.ATTRIBUTE_TYPE_VALUE;
                                    }
                                    if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "TEXT TYPE")
                                    {
                                        book1.TEXTTYPE = booklist.ATTRIBUTE_TYPE_VALUE;
                                    }
                                    index[count1] = templist.IndexOf(booklist);
                                }

                        }

                        list.Add(book1);
                        while (count1 > 0)
                        {

                                for (int j = count1 - 1; j >= 0; j--)
                                {
                                    templist.RemoveAt(index[j]);
                                    count1--;
                                }
                            }


                        }
                    }
                    books = list;
                    DataCache.SetCache(cacheKey, list, new TimeSpan(0, 20, 0));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
                finally
                {
                    CBO.CloseDataReader(dr, true);
                }
            }
            return books;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="fromReadLvl"></param>
        /// <param name="toReadLvl"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public int[] GetBookListByLevels(int subId, int fromReadLvl, int toReadLvl, string LoginName)
        {
            int[] bookList = null;
            string bookslist = "";
            IDataReader dr = DataProvider.Instance().GetBookListByReadingLevel(subId, fromReadLvl, toReadLvl, LoginName);
            try
            {
                while (dr.Read())
                {
                    bookslist = bookslist.Contains(dr["PRODUCT_SK"].ToString()) ? bookslist : bookslist + "/" + dr["PRODUCT_SK"].ToString();
                }
                String[] str = bookslist.Split('/');
                bookList = new int[str.Length - 1];
                for (int i = 1; i < str.Length; i++)
                {
                    bookList[i - 1] = Convert.ToInt32(str[i]);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return bookList;
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
                moduleMessages =  CBO.FillCollection<Messages>(DataProvider.Instance().GetErrorMessagesByModuleName(Module_Name));
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
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loginName"></param>
        /// <param name="SubId"></param>
        /// <returns></returns>
        public List<string> GetAllLookUpNames(int userId, string loginName, int SubId)
        {
            string cacheKey = string.Format("GetAllLookUpNames{0}", userId);
            List<string> LookUpNames = (List<string>)DataCache.GetCache(cacheKey);
            if (LookUpNames == null)
            {
                List<string> sesionList = new List<string>();
                IDataReader dr = DataProvider.Instance().GetAllLookUpNames(userId, loginName,SubId);                
                try
                {
                    while (dr.Read())
                    {
                        string sessionName = dr["SessionName"].ToString();
                        sesionList.Add(sessionName);
                    }
                    LookUpNames = sesionList;
                    if (LookUpNames != null)
                    {
                        DataCache.SetCache(cacheKey, LookUpNames, TimeSpan.FromMinutes(20));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                finally
                {
                    CBO.CloseDataReader(dr, true);
                }
            }
            return LookUpNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userId"></param>
        /// <param name="loginName"></param>
        /// <param name="SubId"></param>
        public void GetNamesForSearch(string searchString, int userId, string loginName, int SubId)
        {

            List<Sessions> _activeSessions = new List<Sessions>();
            List<Sessions> _finishedSessions = new List<Sessions>();
            List<Sessions> _archivedSessions = new List<Sessions>();
            string GetAllActiveSessionsCacheKey = string.Format("GetAllActiveSessions{0}", userId);
            string GetAllFinishedSessionsCacheKey = string.Format("GetAllFinishedSessions{0}", userId);
            string GetAllArchivedSessionsCacheKey = string.Format("GetAllArchivedSessions{0}", userId);
            DataCache.RemoveCache(GetAllActiveSessionsCacheKey);
            DataCache.RemoveCache(GetAllFinishedSessionsCacheKey);
            DataCache.RemoveCache(GetAllArchivedSessionsCacheKey);
            try
            {
                List<Sessions> searchList = CBO.FillCollection<Sessions>(DataProvider.Instance().GetNamesForSearch(searchString, userId, loginName, SubId));
                 _activeSessions = searchList.FindAll(delegate(Sessions sessions) { return DateTime.Now.Date < sessions.SessionExpiryDate.Date; });
                 _finishedSessions = searchList.FindAll(delegate(Sessions sessions) { return DateTime.Now.Date >= sessions.SessionExpiryDate.Date && !Helper.DiffInMonths(sessions.SessionExpiryDate.Date, DateTime.Now.Date); });
                 _archivedSessions = searchList.FindAll(delegate(Sessions sessions) { return Helper.DiffInMonths(sessions.SessionExpiryDate.Date, DateTime.Now.Date); });
            }
            catch (Exception e1) { }
            
                DataCache.SetCache(GetAllActiveSessionsCacheKey, _activeSessions, TimeSpan.FromMinutes(20));
                if (!CacheKeys.Contains(GetAllActiveSessionsCacheKey))
                    CacheKeys.Add(GetAllActiveSessionsCacheKey);

                DataCache.SetCache(GetAllFinishedSessionsCacheKey, _finishedSessions, TimeSpan.FromMinutes(20));
                if (!CacheKeys.Contains(GetAllFinishedSessionsCacheKey))
                    CacheKeys.Add(GetAllFinishedSessionsCacheKey);

                DataCache.SetCache(GetAllArchivedSessionsCacheKey, _archivedSessions, TimeSpan.FromMinutes(20));
                if (!CacheKeys.Contains(GetAllArchivedSessionsCacheKey))
                    CacheKeys.Add(GetAllArchivedSessionsCacheKey);
                            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsId"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<string> GetLookupStudentsName(int subsId, string userLoginName)
        {
            string cacheKey = string.Format("GetStudentsName{0}", userLoginName + subsId);
            List<string> SelectedstudentList = (List<string>)DataCache.GetCache(cacheKey);
            
            if (SelectedstudentList == null)
            {
                IDataReader dr = null;
                List<string> studentList = new List<string>();
                try
                {
                    dr = DataProvider.Instance().GetStudentLookupNames(subsId, userLoginName);
                    while (dr.Read())
                    {
                        string studentName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                        studentList.Add(studentName);
                    }

                    SelectedstudentList = studentList;
                    if (studentList != null)
                    {
                        DataCache.SetCache(cacheKey, studentList, TimeSpan.FromMinutes(1));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                finally
                {
                    CBO.CloseDataReader(dr, true);
                }
            }

            return SelectedstudentList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        public void GetSearchStudentDetails(string searchString, string userLoginName, int custSubId)
        {
            string cacheKey = string.Format("GetStudentsList{0}", custSubId);
            DataCache.RemoveCache(cacheKey);
            List<Student> studentList = CBO.FillCollection<Student>(DataProvider.Instance().GetSearchStudentDetails(searchString, userLoginName, custSubId));

            if (studentList != null)
            {
                DataCache.SetCache(cacheKey, studentList, TimeSpan.FromMinutes(20));
                if (!CacheKeys.Contains(cacheKey))
                    CacheKeys.Add(cacheKey);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public List<Group> GetSearchGroup(string searchString, string userLoginName, int custSubId)
        {
            string groupCacheKey = string.Format("GetGroups{0}", (char)MyEnums.GroupType.N);
            string classCacheKey = string.Format("GetGroups{0}", (char)MyEnums.GroupType.C);
            DataCache.RemoveCache(groupCacheKey);
            DataCache.RemoveCache(classCacheKey);
            List<Group> searchList = CBO.FillCollection<Group>(DataProvider.Instance().GetSearchGroup(searchString, userLoginName, custSubId));
            List<Group> classList = searchList.Where(u => u.GroupType.Equals('C')).ToList();
            List<Group> groupList = searchList.Where(u => u.GroupType.Equals('N')).ToList();
            if (searchList != null)
            {
                DataCache.SetCache(groupCacheKey, groupList, TimeSpan.FromMinutes(20));
                if (!CacheKeys.Contains(groupCacheKey))
                    CacheKeys.Add(groupCacheKey);

                DataCache.SetCache(classCacheKey, classList, TimeSpan.FromMinutes(20));
                if (!CacheKeys.Contains(classCacheKey))
                    CacheKeys.Add(classCacheKey);
            }

            return groupList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public List<string> GetGroupName(string userLoginName, int custSubId)
        {
            string cacheKey = string.Format("GetGroupName{0}", userLoginName + custSubId);
            IDataReader dr = DataProvider.Instance().GetGroupName(userLoginName, custSubId);
            List<string> list = new List<string>();
            try
            {
                while (dr.Read())
                {
                    string groupName = dr["NAME"].ToString();
                    list.Add(groupName);
                }
                if (list != null)
                {
                    DataCache.SetCache(cacheKey, list, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<string> GetTeachersLookUp(int subId, string loginName)
        {
            string cacheKey = string.Format("GetTeachersLookUp{0}", subId);
            IDataReader dr = DataProvider.Instance().GetTeachers(subId, loginName);
            List<string> list = new List<string>();
            try
            {
                while (dr.Read())
                {
                    string groupName = dr["Value"].ToString();
                    list.Add(groupName);
                }
                if (list != null)
                {
                    DataCache.SetCache(cacheKey, list, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        public void GetSearchTeacherDetails(string searchString, string userLoginName, int custSubId)
        {
            string cacheKey = string.Format("GetTeachers{0}", custSubId);
            DataCache.RemoveCache(cacheKey);
            List<IDCollection> teachers = CBO.FillCollection<IDCollection>(DataProvider.Instance().GetSearchTeachers(searchString, userLoginName, custSubId));

            if (teachers != null)
            {
                DataCache.SetCache(cacheKey, teachers, TimeSpan.FromMinutes(20));
                if (!CacheKeys.Contains(cacheKey))
                    CacheKeys.Add(cacheKey);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <param name="Attribute_type"></param>
        /// <returns></returns>
        public List<string> GetBooksLookUp(int subId, string loginName, string Attribute_type)
        {
            string cacheKey = string.Format("GetBooksLookUp{0}", subId);
            IDataReader dr = DataProvider.Instance().GetBooksCategories(subId,Attribute_type, loginName);
            List<string> list = new List<string>();
            try
            {
                while (dr.Read())
                {
                    string groupName = dr["ATTRIBUTE_TYPE_VALUE"].ToString();
                    list.Add(groupName);
                }
                if (list != null)
                {
                    DataCache.SetCache(cacheKey, list, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="custSubId"></param>
        /// <param name="Attribute_Type"></param>
        /// <returns></returns>
        public int[] GetBookListBySearch(string searchString, int custSubId, string Attribute_Type)
        {
            int[] bookList = null;
            string bookslist = "";
            IDataReader dr = DataProvider.Instance().GetBookListBySearch(searchString, custSubId, Attribute_Type);
            try
            {
                while (dr.Read())
                {
                    bookslist = bookslist.Contains(dr["PRODUCT_SK"].ToString()) ? bookslist : bookslist + "/" + dr["PRODUCT_SK"].ToString();
                }
                String[] str = bookslist.Split('/');
                bookList = new int[str.Length - 1];
                for (int i = 1; i < str.Length; i++)
                {
                    bookList[i - 1] = Convert.ToInt32(str[i]);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return bookList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="custSubId"></param>
        /// <param name="Attribute_Type"></param>
        public void GetSearchBook(string searchString, int custSubId, string Attribute_Type)
        {
            string cacheKey = string.Format("GetSessionBooksByReadingLevel{0}", custSubId);
            DataCache.RemoveCache(cacheKey);
            List<Books> books = new List<Books>();
            IDataReader dr = DataProvider.Instance().GetSearchGroup(searchString, custSubId, Attribute_Type);
            List<Books> templist = new List<Books>();
            try
            {
                while (dr.Read())
                {

                    Books book = new Books();
                    book.PRODUCT_SK = Convert.ToInt32(dr["PRODUCT_SK"]);
                    book.CUST_SUBS_ITEM_SK = Convert.ToInt32(dr["CUST_SUBS_ITEM_SK"]);
                    book.IMAGE_FILE_NAME = dr["IMAGE_FILE_NAME"].ToString();
                    book.IMAGE_TYPE = dr["IMAGE_TYPE"].ToString();
                    book.Title = dr["Title"].ToString().Replace("eBook:", "");
                    book.COPYRIGHT_YEAR = dr["COPYRIGHT_YEAR"].ToString();
                    book.NO_OF_PAGES = Null.SetNullInteger(dr["NO_OF_PAGES"]) < 0 ? "0" : dr["NO_OF_PAGES"].ToString();
                    book.Author = dr["PREFERRED_NAME"].ToString();
                    book.ATTRIBUTE_TYPE_ID = dr["ATTRIBUTE_TYPE_ID"].ToString();
                    book.ATTRIBUTE_TYPE_VALUE = dr["ATTR_VALUE"].ToString();
                    book.Checked = false;
                    book.ClassName = string.Empty;
                    book.CheckImgPathName = string.Empty;
                    templist.Add(book);
                }

                //


                List<Books> list = new List<Books>();

                int count = templist.Count;
                int[] booksList = GetBookListBySearch(searchString, custSubId, Attribute_Type);


                for (int i = 0; i < booksList.Length; i++)
                {
                    if (templist.Count > 0)
                    {
                        Books book1 = new Books();
                        book1.PRODUCT_SK = booksList[i];
                        int[] index = new int[templist.Count];
                        int count1 = 0;
                        foreach (Books booklist in templist)
                        {
                            if (booklist.PRODUCT_SK == booksList[i])
                            {
                                book1.CUST_SUBS_ITEM_SK = booklist.CUST_SUBS_ITEM_SK;
                                book1.IMAGE_FILE_NAME = booklist.IMAGE_FILE_NAME;
                                book1.IMAGE_TYPE = booklist.IMAGE_TYPE;
                                book1.Title = booklist.Title;
                                book1.COPYRIGHT_YEAR = booklist.COPYRIGHT_YEAR;
                                book1.NO_OF_PAGES = booklist.NO_OF_PAGES;
                                book1.Author = booklist.Author;
                                book1.Checked = booklist.Checked;
                                book1.ClassName = booklist.ClassName;
                                book1.CheckImgPathName = booklist.CheckImgPathName;
                                if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "GUIDED READING LEVEL")
                                {
                                    book1.ReadingLevel = booklist.ATTRIBUTE_TYPE_VALUE;
                                }
                                if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "COLOUR LEVEL")
                                {
                                    book1.ColourLevel = booklist.ATTRIBUTE_TYPE_VALUE;
                                }
                                if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "READING AGE")
                                {
                                    book1.ReadingAge = booklist.ATTRIBUTE_TYPE_VALUE;
                                }
                                if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "TEXT TYPE")
                                {
                                    book1.TEXTTYPE = booklist.ATTRIBUTE_TYPE_VALUE;
                                }
                                index[count1] = templist.IndexOf(booklist);
                            }

                        }
                        list.Add(book1);
                        while (count1 > 0)
                        {

                            for (int j = count1 - 1; j >= 0; j--)
                            {
                                templist.RemoveAt(index[j]);
                                count1--;
                            }
                        }


                    }
                }
                books = list;
                if (books != null)
                {
                    DataCache.SetCache(cacheKey, books, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }

        }

        #region EXCEPTION MAIL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="errorMessage"></param>
        /// <param name="SubsSk"></param>
        public void SendExceptionMail(string body, string errorMessage, int SubsSk)
        {
            DataProvider.Instance().SendExceptionMail(body, errorMessage, SubsSk);
        }
        #endregion
    }
}