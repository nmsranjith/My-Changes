using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Modules.eCollection_Groups.Data;
using DotNetNuke.Modules.eCollection_Groups.Components.Interfaces;
using DotNetNuke.Common.Utilities;
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Modules.eCollection_Groups.Components.ExceptionHandling;
using DotNetNuke.Modules.eCollection_Groups.Components.Common;
using DotNetNuke.Modules.eCollection_Groups.Components.Modal;
using System.Xml;

namespace DotNetNuke.Modules.eCollection_Groups.Components
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="GroupController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A controller class for to perform business activities
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public sealed class GroupController : IDataRepository<DotNetNuke.Modules.eCollection_Groups.Components.Groups, int>
    {
        private static readonly GroupController instance = new GroupController();
        private static readonly List<string> CacheKeys = new List<string>();

        private GroupController() { }

        public static GroupController Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupType"></param>
        /// <param name="userLoginName"></param>
        /// <param name="allOther"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public List<Groups> GetGroupList(char groupType, string userLoginName, bool allOther, int custSubId)
        {
            string cacheKey = string.Format("GetGroupList{0}", userLoginName + custSubId);           
            List<Groups> groupList = (List<Groups>)DataCache.GetCache(cacheKey);
            if (groupList == null)
            {
                groupList = CBO.FillCollection<Groups>(DataProvider.Instance().GetGroupList(groupType, userLoginName, custSubId));

                if (groupList != null)
                {
                    DataCache.SetCache(cacheKey, groupList, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }               
            }
            else
            {              
            }
            return groupList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClassType"></param>
        /// <param name="userLoginName"></param>
        /// <param name="allOther"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public List<Groups> GetClassList(char ClassType, string userLoginName, bool allOther, int custSubId)
        {
            string cacheKey = string.Format("GetClassList{0}", userLoginName + custSubId);          
            List<Groups> classList = (List<Groups>)DataCache.GetCache(cacheKey);
            if (classList == null)
            {
                classList = CBO.FillCollection<Groups>(DataProvider.Instance().GetClassList(ClassType, userLoginName, custSubId));

                if (classList != null)
                {
                    DataCache.SetCache(cacheKey, classList, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }               
            }
            else
            {                
            }
            return classList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public List<Groups> GetWordsByGroup(int groupID)
        {
            IDataReader dr = DataProvider.Instance().GetWordsByGroup(groupID);
            List<Groups> list = new List<Groups>();
            try
            {
                while (dr.Read())
                {
                    Groups group = new Groups();
                    group.WordCount = int.Parse(dr["WordsCount"].ToString());
                    group.CircledWord = dr["CircledWord"].ToString();
                    group.WordCircledAt = DateTime.Parse(dr["WordCircledAt"].ToString());
                    group.IsClearFromIpad = char.Parse(dr["IsClearFromIpad"].ToString());
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
        /// <param name="groupId"></param>
        /// <param name="groupType"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<Groups> GetGroupsByGroupID(int groupId, char groupType, string userLoginName)
        {

            return CBO.FillCollection<Groups>(DataProvider.Instance().GetGroupsByGroupID(groupId, groupType, userLoginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="groupType"></param>
        /// <param name="customerSubId"></param>
        /// <returns></returns>
        public bool ValidateGroupName(string groupName, char groupType, int customerSubId)
        {
            try
            {
                return DataProvider.Instance().ValidateGroupName(groupName, groupType, customerSubId);
            }
            catch (Exception e1) { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<Students> GetReadingLevelByGroup(int groupId)
        {
            IDataReader dr = DataProvider.Instance().GetReadingLevelByGroup(groupId);
            List<Students> list = new List<Students>();
            try
            {
                while (dr.Read())
                {
                    Students studentCollection = new Students();
                    studentCollection.ReadingLevel = int.Parse(dr["ReadingLevel"].ToString());
                    studentCollection.ReadingPercentage = dr["Percentage"].ToString() + "%";
                    studentCollection.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString());
                    list.Add(studentCollection);
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
        /// <param name="currentReadLevel"></param>
        /// <returns></returns>
        public int GetBookCount(int groupId, int currentReadLevel)
        {
            IDataReader dr = DataProvider.Instance().GetBookCount(groupId, currentReadLevel);
            int bookCount = 0;
            try
            {
                while (dr.Read())
                {
                    bookCount = int.Parse(dr["BookOpenedCount"].ToString());
                }
            }
            finally
            {
                dr.Close();
            }
            return bookCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<string> GetTeachersByGroup(int groupId)
        {
            IDataReader dr = DataProvider.Instance().GetTeachersByGroup(groupId);
            List<string> list = new List<string>();
            try
            {
                while (dr.Read())
                {
                    list.Add(dr["FirstName"].ToString() + " " + dr["LastName"].ToString());
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
        public Components.Groups GetGroupProfileByGroup(int groupId)
        {
            IDataReader dr = DataProvider.Instance().GetGroupProfileByGroup(groupId);
            Groups group = new Groups();
            try
            {
                while (dr.Read())
                {
                    group.MemberCount = int.Parse(dr["MemberCount"].ToString());
                    group.Name = dr["GroupName"].ToString();
                    group.GroupId = int.Parse(dr["GroupId"].ToString());
                    group.BooksOpened = int.Parse(dr["BooksOpened"].ToString());
                    group.Independent = int.Parse(dr["Independent"].ToString());
                    group.Guided = int.Parse(dr["Guided"].ToString());
                    group.MyRecording = int.Parse(dr["MyRecording"].ToString());
                    group.MyWords = int.Parse(dr["MyWords"].ToString());
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return group;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public List<IDCollection> GetMembersByGroup(int groupId, string role)
        {
            return CBO.FillCollection<IDCollection>(DataProvider.Instance().GetMembersByGroup(groupId, role));
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
        public List<Groups> GetReadingGraphByGroup(int groupId)
        {
            IDataReader dr = DataProvider.Instance().GetReadingGraphByGroup(groupId);
            List<Groups> list = new List<Groups>();
            try
            {
                while (dr.Read())
                {
                    Groups group = new Groups();
                    group.MaxReadLevel = int.Parse(dr["MaxReadLevel"].ToString());
                    group.MinReadLevel = int.Parse(dr["MinReadLevel"].ToString());
                    group.MinStartReadFrom = int.Parse(dr["MinStartReadFrom"].ToString());
                    group.MaxStartReadUpto = int.Parse(dr["MaxStartReadUpto"].ToString());
                    group.MinCurrReadFrom = int.Parse(dr["MinCurrReadFrom"].ToString());
                    group.MaxCurrReadUpto = int.Parse(dr["MaxCurrReadUpto"].ToString());
                    group.DateCreated = DateTime.Parse(dr["DateCreated"].ToString());
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
        /// <param name="groupId"></param>
        /// <param name="groupMemberId"></param>
        /// <param name="productId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<Groups> GetRecordingHistoryByGroup(int groupId, int groupMemberId, int productId, string month)
        {
            IDataReader dr = DataProvider.Instance().GetRecordingHistoryByGroup(groupId,  groupMemberId, productId, month);
            List<Groups> list = new List<Groups>();
            try
            {
                while (dr.Read())
                {
                    Groups group = new Groups();
                    group.PageName = string.Concat("Content Page ", dr["PageName"].ToString().Split('-')[1]);
                    group.SessionType = dr["SessionType"].ToString();
                    group.RecordFilePath = dr["RecordFilePath"].ToString();
                    //group.WordCount = dr["WordCount"].ToString() == string.Empty ? 0 : int.Parse(dr["WordCount"].ToString());
                    DateTime result;
                    if (DateTime.TryParse(dr["BookOpenForRecord"].ToString(), out result))
                        group.BookOpenAt = result;
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
        /// <param name="groupId"></param>
        /// <param name="grfRefDate"></param>
        /// <returns></returns>
        public int[] GetMinMaxReadingLevelByDate(int groupId, DateTime grfRefDate)
        {
            IDataReader dr = DataProvider.Instance().GetMinMaxReadingLevelByDate(groupId, grfRefDate);
            int[] RL = new int[2];
            try
            {
                while (dr.Read())
                {
                    if (dr["MinRL"].ToString() != null && dr["MinRL"].ToString() != "")
                        RL[0] = (int)dr["MinRL"];
                    else RL[0] = 0;
                    if (dr["MaxRL"].ToString() != null && dr["MaxRL"].ToString() != "")
                        RL[1] = (int)dr["MaxRL"];
                    else RL[1] = 0;
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return RL;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupMemberId"></param>
        /// <param name="productId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public List<Groups> GetLastSevenDaysRecordings(int groupId, int groupMemberId, int productId, DateTime fromDate, DateTime toDate)
        {
            IDataReader dr = DataProvider.Instance().GetLastSevenDaysRecordings(groupId, groupMemberId, productId, fromDate, toDate);
            List<Groups> list = new List<Groups>();
            try
            {
                while (dr.Read())
                {
                    Groups group = new Groups();
                    group.PageName = string.Concat("Content Page ", dr["PageName"].ToString().Split('-')[1]);
                    group.RecordFilePath = dr["RecordFilePath"].ToString();
					group.SessionType = dr["SessionType"].ToString();
                    group.BookOpenAt = DateTime.Parse(dr["BookOpenForRecord"].ToString());
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
        /// <param name="groupId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<Groups> GetReadingHistoryByGroup(int groupId,string month)
        {
            IDataReader dr = DataProvider.Instance().GetReadingHistoryByGroup(groupId, month);
            List<Groups> list = new List<Groups>();
            try
            {
                while (dr.Read())
                {
                    Groups group = new Groups();
                    group.StudentNames = dr["UserName"].ToString();
                    group.UserID = int.Parse(dr["MemberCusUserSk"].ToString());
                    group.Title = dr["Title"].ToString();
                    group.SessionType = dr["SessionType"].ToString();
                    group.BookImgName = dr["BookImgName"].ToString();
                    group.BookOpenAt = DateTime.Parse(dr["BookOpenAt"].ToString());
                    group.WordCount = int.Parse(dr["WordCount"].ToString());
                    //TimeSpan totalhours = TimeSpan.Parse(DateTime.Parse(dr["BookCloseAt"].ToString()).ToString("HH:mm")) - TimeSpan.Parse(DateTime.Parse(dr["BookOpenAt"].ToString()).ToString("HH:mm"));
                    group.BooksOpenedMin = Int32.Parse(DateTime.Parse(dr["BookCloseAt"].ToString()).Subtract(DateTime.Parse(dr["BookOpenAt"].ToString())).TotalSeconds.ToString());
                    group.ProductID = int.Parse(dr["ProductId"].ToString());
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
        /// <param name="groupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public List<Groups> GetLastSevenDaysReadings(int groupId, DateTime fromDate, DateTime toDate)
        {
            IDataReader dr = DataProvider.Instance().GetLastSevenDaysReadings(groupId, fromDate, toDate);
            List<Groups> list = new List<Groups>();
            try
            {
                while (dr.Read())
                {
                    Groups group = new Groups();
                    group.StudentNames = dr["UserName"].ToString();
                    group.UserID = int.Parse(dr["MemberCusUserSk"].ToString());
                    group.Title = dr["Title"].ToString();
                    group.BookImgName = dr["BookImgName"].ToString();
                    group.BookOpenAt = DateTime.Parse(dr["BookOpenAt"].ToString());
                    group.WordCount = int.Parse(dr["WordCount"].ToString());
                    group.BooksOpenedMin = Int32.Parse(DateTime.Parse(dr["BookCloseAt"].ToString()).Subtract(DateTime.Parse(dr["BookOpenAt"].ToString())).TotalSeconds.ToString());
                    group.ProductID = int.Parse(dr["ProductId"].ToString());
					group.SessionType = dr["SessionType"].ToString();
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
        /// <param name="groupId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<Groups> GetReadingSessionByGroup(int groupId, string month)
        {
            IDataReader dr = DataProvider.Instance().GetReadingSessionByGroup(groupId, month);
            List<Groups> list = new List<Groups>();
            try
            {
                while (dr.Read())
                {
                    Groups group = new Groups();
                    group.SessionName = dr["SessionName"].ToString();
                    group.SessionID = int.Parse(dr["SessionSK"].ToString());
                    group.GroupId = int.Parse(dr["GroupId"].ToString());
                    group.SessionNote = dr["SessionNote"].ToString();
                    group.BookOpened = int.Parse(dr["BookOpened"].ToString());
                    group.BookUnOpened = int.Parse(dr["BookUnOpened"].ToString());
                    group.SessionCreatedDate = DateTime.Parse(dr["SessionCreatedDate"].ToString());
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
        /// <param name="groupId"></param>
        /// <param name="refDate"></param>
        /// <returns></returns>
        public List<List<Groups>> GetReadingSessionByGroupAndSectionCat(int groupId, DateTime refDate)
        {
            DataSet ds = DataProvider.Instance().GetReadingSessionByGroupAndSectionCat(groupId, refDate);
            List<List<Groups>> list = new List<List<Groups>>();
            List<Groups> subList = null;
            try
            {

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        subList = new List<Groups>();
                        Groups group = new Groups();
                        group.SessionName = ds.Tables[i].Rows[j]["SessionName"].ToString();
                        group.SessionID = int.Parse(ds.Tables[i].Rows[j]["SessionSK"].ToString());
                        group.GroupId = int.Parse(ds.Tables[i].Rows[j]["GroupId"].ToString());
                        group.SessionNote = ds.Tables[i].Rows[j]["SessionNote"].ToString();
                        group.BookOpened = int.Parse(ds.Tables[i].Rows[j]["BookOpened"].ToString());
                        group.BookUnOpened = int.Parse(ds.Tables[i].Rows[j]["BookUnOpened"].ToString());
                        group.SessionCreatedDate = DateTime.Parse(ds.Tables[i].Rows[j]["SessionCreatedDate"].ToString());
                        subList.Add(group);
                    }
                    list.Add(subList);
                    subList = null;
                }                
            }
            finally
            {
                
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="Teachercollection"></param>
        /// <param name="StudentCollection"></param>
        /// <param name="customerSubSK"></param>
        public void UpdateMembers(Components.Groups groups, List<IDCollection> Teachercollection, List<Students> StudentCollection, int customerSubSK)
        {
            try
            {
                ClearAllCaches();
                DataProvider.Instance().UpdateMembers(groups, Teachercollection, StudentCollection, customerSubSK);
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
        /// <param name="groupId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public DataTable GetGroupNameByGroupId(int groupId, string role)
        {
            return DataProvider.Instance().GetGroupNameByGroupId(groupId, role);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grouptype"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<IDCollection> GetGroupName(char grouptype, string userLoginName)
        {
            IDataReader dr = DataProvider.Instance().GetGroupName(grouptype, userLoginName);
            List<IDCollection> list = new List<IDCollection>();
            try
            {
                while (dr.Read())
                {
                    IDCollection idCollection = new IDCollection(int.Parse(dr["GROUP_SK"].ToString()), dr["NAME"].ToString());
                    list.Add(idCollection);
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
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<IDCollection> GetSubscription(string userLoginName)
        {
            IDataReader dr = DataProvider.Instance().GetSubscription(userLoginName);
            List<IDCollection> list = new List<IDCollection>();
            try
            {
                while (dr.Read())
                {
                    IDCollection idCollection = new IDCollection(int.Parse(dr["CustomerSubSK"].ToString()), dr["Subscription"].ToString());
                    //IDCollection idCollection = new IDCollection(int.Parse(dr["CustomerSubSK"].ToString()), "Subscription1");
                    list.Add(idCollection);
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
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<Students> GetStudentByGroup(int groupId, string userLoginName)
        {
            string cacheKey = string.Format("GetStudentByGroup{0}", userLoginName);
            
            List<Students> studentList = (List<Students>)DataCache.GetCache(cacheKey);
            if (studentList == null)
            {
                IDataReader dr = DataProvider.Instance().GetStudentByGroup(groupId);
                try
                {

                    studentList = new List<Students>();
                    while (dr.Read())
                    {
                        Students group = new Students();
                        group.UserID = int.Parse(dr["CustomerSubsUserSk"].ToString());
                        group.custSubUserSK = int.Parse(dr["StudentId"].ToString());
                        group.StudentNames = string.Concat(Null.SetNullString(dr["FirstName"].ToString()), " ", Null.SetNullString(dr["LastName"].ToString()));
                        group.CurrentReadLevel = Null.SetNullInteger(int.Parse(dr["CurrentReadingLevel"].ToString()));
                        group.StudentLoginName = Null.SetNullString(dr["UserLoginName"].ToString());
                        studentList.Add(group);

                    }

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
           

            return studentList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<Students> GetStudentByGroup(int groupId)
        {
            return CBO.FillCollection<Students>(DataProvider.Instance().GetStudentByGroup(groupId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsId"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<Students> GetStudentsBySubcription(int subsId, string userLoginName)
        {
            string cacheKey = string.Format("GetStudentsBySubcription{0}", subsId);
            List<Students> studentList = (List<Students>)DataCache.GetCache(cacheKey);
            if (studentList == null)
            {
                studentList = CBO.FillCollection<Students>(DataProvider.Instance().GetStudentsBySubcription(subsId, userLoginName));

                if (studentList != null)
                {
                    DataCache.SetCache(cacheKey, studentList, TimeSpan.FromMinutes(1));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }

            return studentList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsId"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<IDCollection> GetTeachersbySubscription(int subsId, string userLoginName)
        {
            string cacheKey = string.Format("GetTeachersbySubscription{0}", subsId);
            List<IDCollection> teacherList = (List<IDCollection>)DataCache.GetCache(cacheKey);
            if (teacherList == null)
            {
                teacherList = CBO.FillCollection<IDCollection>(DataProvider.Instance().GetTeachersbySubscription(subsId, userLoginName));
                if (teacherList != null)
                {
                    DataCache.SetCache(cacheKey, teacherList, TimeSpan.FromMinutes(1));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }

            return teacherList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public int? Add(Groups group)
        {
            try
            {
                if (group.GroupId < 1)
                {
                    ClearAllCaches();
                    group.GroupId = DataProvider.Instance().AddGroup(group);
                }
                return group.GroupId;
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
        /// <param name="minReadLevel"></param>
        /// <param name="maxReadLevel"></param>
        /// <returns></returns>
        public List<string> GetColorByReadLevel(int minReadLevel, int maxReadLevel)
        {
            IDataReader dr = DataProvider.Instance().GetColorByReadLevel(minReadLevel, maxReadLevel);
            List<string> list = new List<string>();


            try
            {
                while (dr.Read())
                {

                    list.Add(dr["MinReadLevelColor"].ToString());
                    list.Add(dr["MaxReadLevelColor"].ToString());
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
        /// <param name="type"></param>
        /// <returns></returns>
        public List<string> GetMonthByGroup(int groupId, string type)
        {
            IDataReader dr = DataProvider.Instance().GetMonthByGroup(groupId,type);
            List<string> list = new List<string>();
            try
            {
                while (dr.Read())
                {
                    list.Add(dr["BookOpenedMonth"].ToString());
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
        /// <param name="groups"></param>
        /// <param name="groupId"></param>
        public void DeleteGroup(Components.Groups groups, List<string> groupId)
        {
            try
            {

                ClearAllCaches();
                DataProvider.Instance().DeleteGroup(groups, groupId);
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
        public void ClearAllCaches()
        {
            foreach (string cacheKey in CacheKeys)
                DataCache.RemoveCache(cacheKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Groups Get(int key)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Delete(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stagingXmlDoc"></param>
        /// <param name="countCheckReq"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        public int AddIsbnDetails(XmlDocument stagingXmlDoc, int countCheckReq, string server)
        {
            try
            {
                ClearAllCaches();
                return DataProvider.Instance().AddIsbnDetails(stagingXmlDoc, countCheckReq, server);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<Staging> GetBooksDetails(string loginName)
        {
            try
            {
                string cacheKey = string.Format("GetBooksDetails{0}", loginName);
                List<Staging> staging = (List<Staging>)DataCache.GetCache(cacheKey);
                if (staging == null)
                {
                    staging = CBO.FillCollection<Staging>(DataProvider.Instance().GetBooksDetails());

                    if (staging != null)
                    {
                        DataCache.SetCache(cacheKey, staging, TimeSpan.FromMinutes(20));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }

                return staging;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetBooksISBN()
        {
            try
            {
                 List<Staging> staging  = CBO.FillCollection<Staging>(DataProvider.Instance().GetBooksDetails());
                 List<string> IsbnName = staging.Select(u => u.ISBN.ToString()).ToList<string>();
                 return IsbnName;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="searchText"></param>
        public void GetSearchBooksDetails(string loginName, string searchText)
        {
            try
            {
                string cacheKey = string.Format("GetBooksDetails{0}", loginName);
                DataCache.RemoveCache(cacheKey);
                List<Staging> staging = CBO.FillCollection<Staging>(DataProvider.Instance().GetSearchBooksDetails(searchText));

                if (staging != null)
                {
                    DataCache.SetCache(cacheKey, staging, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="publishingXmlDoc"></param>
        /// <returns></returns>
        public int AddBooksInPublish(XmlDocument publishingXmlDoc)
        {
            try
            {
                ClearAllCaches();
                return DataProvider.Instance().AddBooksInPublish(publishingXmlDoc);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deleteXmlDoc"></param>
        /// <returns></returns>
        public int DeleteBooksInStaging(XmlDocument deleteXmlDoc)
        {
            try
            {
                ClearAllCaches();
                return DataProvider.Instance().DeleteBooksInStaging(deleteXmlDoc);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbnXmlDoc"></param>
        /// <returns></returns>
        public int CheckISBN(XmlDocument isbnXmlDoc)
        {
            try
            {
                return DataProvider.Instance().CheckISBN(isbnXmlDoc);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbnXmlDoc"></param>
        /// <returns></returns>
        public List<Staging> GetIsbnDetails(XmlDocument isbnXmlDoc)
        {
            IDataReader dr = DataProvider.Instance().GetIsbnDetails(isbnXmlDoc);
            List<Staging> list = new List<Staging>();


            try
            {
                while (dr.Read())
                {
                    Staging stage = new Staging();
                    stage.ISBN = long.Parse(dr["ISBN"].ToString());
                    //stage.NoOfScreensAudioFiles = int.Parse(dr["NUM_OF_SCREEN_AUDIO_FILES"].ToString());
                    // stage.NoOfUniqueWordAudioFiles = int.Parse(dr["NUM_OF_UNIQUE_WORD_AUDIO_FILES"].ToString());
                    list.Add(stage);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
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
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        public void GetSearchGroup(string searchString, string userLoginName, int custSubId)
        {
            string groupCacheKey = string.Format("GetGroupList{0}", userLoginName + custSubId);
            string classCacheKey = string.Format("GetClassList{0}", userLoginName + custSubId);
            DataCache.RemoveCache(groupCacheKey);
            DataCache.RemoveCache(classCacheKey);
            List<Groups> searchList = CBO.FillCollection<Groups>(DataProvider.Instance().GetSearchGroup(searchString, userLoginName, custSubId));
            List<Groups> classList = searchList.Where(u => u.GroupType.Equals('C')).ToList();
            List<Groups> groupList = searchList.Where(u => u.GroupType.Equals('N')).ToList();
            if (searchList != null)
            {
                DataCache.SetCache(groupCacheKey, groupList, TimeSpan.FromMinutes(20));
                if (!CacheKeys.Contains(groupCacheKey))
                    CacheKeys.Add(groupCacheKey);

                DataCache.SetCache(classCacheKey, classList, TimeSpan.FromMinutes(20));
                if (!CacheKeys.Contains(classCacheKey))
                    CacheKeys.Add(classCacheKey);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        public void GetSearchStudentDetails(string searchString, string userLoginName, int custSubId)
        {
            string cacheKey = string.Format("GetStudentsBySubcription{0}", custSubId);
            DataCache.RemoveCache(cacheKey);
            List<Students> studentList = CBO.FillCollection<Students>(DataProvider.Instance().GetSearchStudentDetails(searchString, userLoginName, custSubId));

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
        /// <param name="subsId"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<string> GetStudentsName(int subsId, string userLoginName)
        {
            string cacheKey = string.Format("GetStudentsName{0}", userLoginName + subsId);
            List<string> studentName = (List<string>)DataCache.GetCache(cacheKey);
            if (studentName == null)
            {
                List<Students> studentList = CBO.FillCollection<Students>(DataProvider.Instance().GetStudentsBySubcription(subsId, userLoginName));
                studentName = studentList.Select(u => u.StudentNames).ToList();
                if (studentName != null)
                {
                    DataCache.SetCache(cacheKey, studentName, TimeSpan.FromMinutes(1));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }

            return studentName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsId"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<string> GetTeachersName(int subsId, string userLoginName)
        {
            string cacheKey = string.Format("GetTeachersName{0}", userLoginName + subsId);
            List<string> teacherName = (List<string>)DataCache.GetCache(cacheKey);
            if (teacherName == null)
            {
                List<IDCollection> teachertList = CBO.FillCollection<IDCollection>(DataProvider.Instance().GetTeachersbySubscription(subsId, userLoginName));
                teacherName = teachertList.Select(u => u.Text).ToList();
                if (teacherName != null)
                {
                    DataCache.SetCache(cacheKey, teacherName, TimeSpan.FromMinutes(1));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }

            return teacherName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        public void GetSearchTeacherDetails(string searchString, string userLoginName, int custSubId)
        {
            string cacheKey = string.Format("GetTeachersbySubscription{0}", custSubId);
            DataCache.RemoveCache(cacheKey);
            List<IDCollection> teacherList = CBO.FillCollection<IDCollection>(DataProvider.Instance().GetSearchTeacherDetails(searchString, userLoginName, custSubId));

            if (teacherList != null)
            {
                DataCache.SetCache(cacheKey, teacherList, TimeSpan.FromMinutes(20));
                if (!CacheKeys.Contains(cacheKey))
                    CacheKeys.Add(cacheKey);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="searchString"></param>
        /// <param name="userLoginName"></param>
        public void GetSearchStudentInGroup(int groupId, string searchString, string userLoginName)
        {
            string cacheKey = string.Format("GetStudentByGroup{0}", userLoginName);
            DataCache.RemoveCache(cacheKey);
            IDataReader dr = DataProvider.Instance().GetSearchStudentByGroup(searchString,groupId);
            List<Students> studentList = new List<Students>();
            
            try
            {
                while (dr.Read())
                {
                    Students group = new Students();
                    group.UserID = int.Parse(dr["CustomerSubsUserSk"].ToString());
                    group.custSubUserSK = int.Parse(dr["StudentId"].ToString());
                    group.StudentNames =  string.Concat(Null.SetNullString(dr["FirstName"].ToString()), " ", Null.SetNullString(dr["LastName"].ToString()));
                    group.CurrentReadLevel = Null.SetNullInteger(int.Parse(dr["CurrentReadingLevel"].ToString()));
                    group.StudentLoginName = Null.SetNullString(dr["UserLoginName"].ToString());
                    studentList.Add(group);

                }
                 
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public List<string> GetStudentNameInGroup(int groupId, string userLoginName)
        {
            string cacheKey = string.Format("GetStudentNameInGroup{0}", userLoginName);
            List<string> studentName = (List<string>)DataCache.GetCache(cacheKey);
            if (studentName == null)
            {
                List<Students> teachertList = CBO.FillCollection<Students>(DataProvider.Instance().GetStudentByGroup(groupId));
                studentName = teachertList.Select(u => u.StudentNames).ToList();
                if (studentName != null)
                {
                    DataCache.SetCache(cacheKey, studentName, TimeSpan.FromMinutes(1));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }

            return studentName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public int GetGroupOwnerID(int groupId)
        {
            string cacheKey = string.Format("GetGroupOwnerID{0}", groupId);
            int ownerId = Null.SetNullInteger(DataCache.GetCache(cacheKey));
            if (ownerId<=0)
            {
                ownerId = DataProvider.Instance().GetGroupOwnerID(groupId);
                if (ownerId != null)
                {
                    DataCache.SetCache(cacheKey, ownerId, TimeSpan.FromMinutes(3));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return ownerId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="month"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public List<string> GetReadingSessionWrapper(int groupId, string month, int sessionId)
        {

            IDataReader dr = DataProvider.Instance().GetReadingSessionWrapper(groupId, month, sessionId);
            List<string> list = new List<string>();
            try
            {
                while (dr.Read())
                {
                    string groupName = dr["BookWrapperName"].ToString();
                    list.Add(groupName);
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
        /// <param name="sectionCat"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public List<string> GetReadingSessionWrapperBySectionCat(int groupId, string sectionCat, int sessionId)
        {

            IDataReader dr = DataProvider.Instance().GetReadingSessionWrapperBySectionCat(groupId, sectionCat, sessionId);
            List<string> list = new List<string>();
            try
            {
                while (dr.Read())
                {
                    string groupName = dr["BookWrapperName"].ToString();
                    list.Add(groupName);
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
        /// <param name="currentDate"></param>
        /// <param name="fromDate"></param>
        /// <param name="todate"></param>
        /// <param name="month"></param>
        /// <param name="updateStatus"></param>
        /// <returns></returns>
        public int UpdateIsClearFromStatus(int groupId, DateTime? currentDate, DateTime? fromDate, DateTime? todate, string month, string updateStatus)
        {
            try
            {
                ClearAllCaches();
                return DataProvider.Instance().UpdateIsClearFromStatus(groupId, currentDate, fromDate, todate, month, updateStatus);
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
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public Groups GetLoginDetails(string userLoginName, int custSubId)
        {
            string cacheKey = string.Format("GetLoginDetails{0}", userLoginName + custSubId);
            IDataReader dr = DataProvider.Instance().GetLoginDetails(userLoginName, custSubId);
            Groups group = new Groups();
            try
            {
                while (dr.Read())
                {
                    group.UserLoginName = dr["UserName"].ToString();
                    group.LoginName = dr["USER_LOGIN_NAME"].ToString();
                    group.CustomerSubId = int.Parse(dr["CustSubID"].ToString());
                }
                if (group != null)
                {
                    DataCache.SetCache(cacheKey, group, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return group;
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


        public bool InsertNewePubBooks(DataTable newXmlIsbnsDt)
        {
            try
            {
                return DataProvider.Instance().InsertNewePubBooks(newXmlIsbnsDt);
            }
            catch (Exception ex)
            {
                eCollection_GroupsModuleBase.LogFileWrite(ex);
                return false;
            }
        }

        public string GetProductAndAuthorIds()
        {
            try
            {
                return DataProvider.Instance().GetProductAndAuthorIds();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}