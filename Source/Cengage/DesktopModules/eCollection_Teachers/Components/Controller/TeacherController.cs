using System;
using System.Collections.Generic;
using System.Data;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.eCollection_Teachers.Data;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using System.Xml;
using System.IO;
using System.Web;

namespace DotNetNuke.Modules.eCollection_Teachers.Components.Controller
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="TeacherController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A controller class for to perform business activities
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class TeacherController
    {
        #region Class Members
            public static readonly TeacherController teacherCtlr = new TeacherController();
            private static readonly List<string> CacheKeys = new List<string>();
        #endregion

        #region Constructor
        
            private TeacherController() { }

            public static TeacherController Instance
            {
                get { return teacherCtlr; }
                set { }
            }

        #endregion

        #region Teachers List
            /// <summary>
            /// 
            /// </summary>
            /// <param name="TeacherLoginName"></param>
            /// <returns></returns>
            public List<Teacher> GetAll(Teacher teacher)
            {
                string cacheKey = string.Format("Teachers{0}{1}", teacher.TeacherLoginName,teacher.SubscriptionId);
                List<Teacher> teachersDetails = (List<Teacher>)DataCache.GetCache(cacheKey);
                if (teachersDetails == null)
                {
                    teachersDetails = CBO.FillCollection<Teacher>(DataProvider.Instance().GetTeachersList(teacher));
                    if (teachersDetails != null)
                    {
                        DataCache.SetCache(cacheKey, teachersDetails, TimeSpan.FromMinutes(3));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                return teachersDetails;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public void DeleteTeachers(Teacher teacher)
            {
                eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
                ClearAllCache();
                DataProvider.Instance().DeleteTeachers(teacher);
            }
        #endregion

        #region Teacher Profile
            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public DataSet GetProfileDetails(Teacher teacher)
            {
                string cacheKey = string.Format("Teachers{0}", teacher.TeacherId);
                var teachersDetails = (DataSet)DataCache.GetCache(cacheKey);
                if (teachersDetails == null)
                {
                    teachersDetails = DataProvider.Instance().GetProfileDetails(teacher);
                    if (teachersDetails != null)
                    {
                        DataCache.SetCache(cacheKey, teachersDetails, TimeSpan.FromSeconds(20));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                return teachersDetails;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public DataSet GetReadingHistory(Teacher teacher)
            {
                string cacheKey = string.Format("TeachersReadingHistory{0}", teacher.TeacherId);
                var teachersRHDetails = (DataSet)DataCache.GetCache(cacheKey);
                if (teachersRHDetails == null)
                {
                    teachersRHDetails = DataProvider.Instance().GetReadingHistory(teacher);
                    if (teachersRHDetails != null)
                    {
                        DataCache.SetCache(cacheKey, teachersRHDetails, TimeSpan.FromSeconds(20));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                return teachersRHDetails;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public int UpdateBookReadLevel(Teacher teacher)
            {
                ClearMyCache(teacher.TeacherId.ToString());
                return DataProvider.Instance().UpdateBookReadLevel(teacher);
            }
        #endregion

        #region Invite Teachers
            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public int? InviteTeacher(Teacher teacher)
            {
                eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
                ClearMyCache(teacher.UserLoginName);
                teacher.MailBody = MailbodyBuilder(teacher.FirstName ,teacher.EmailUrl );
                return DataProvider.Instance().CreateTeacherProfile(teacher);
            }
        #endregion

        #region Bulk upload
            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public DataTable UploadTeacherProfiles(Teacher teacher)
            {
                eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();                
                return DataProvider.Instance().UploadTeacherProfiles(teacher);
            }
        #endregion

            #region MailBody Builder
            public  string MailbodyBuilder(string fName ,string urlLink)
            {
                string Body = string.Empty;
                //Read the email template into a string
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Portals/0/MailTemplates/TeacherInvitation.htm")))
                {
                    Body = reader.ReadToEnd();
                }
                Body = Body.Replace("{User}", fName);
                Body = Body.Replace("{LINK}", urlLink);                                                
                return Body;
            }
            #endregion

            #region Words Log  
            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public int ClearFromiPad(Teacher teacher)
            {
                ClearAllCache();
                return DataProvider.Instance().ClearFromiPad(teacher);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public List<IDCollection> CheckClearedFromiPad(Teacher teacher)
            {
                string cacheKey = string.Format("StudentsClearFromIpad{0}", teacher.TeacherId);
                List<IDCollection> IsClearedDetails = (List<IDCollection>)DataCache.GetCache(cacheKey);
                if (IsClearedDetails == null)
                {
                    IsClearedDetails = CBO.FillCollection<IDCollection>(DataProvider.Instance().CheckClearedFromiPad(teacher));
                    if (IsClearedDetails != null)
                    {
                        DataCache.SetCache(cacheKey, IsClearedDetails, TimeSpan.FromSeconds(10));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                return IsClearedDetails;
            }
        #endregion       

        #region Reading History
        /************************** READING HISTORY START*********************************/
        /// <summary>
            /// Gets all the reading history months of the teacher
        /// </summary>
        /// <param name="Teacher"></param>
        /// <returns></returns>
        public List<TeacherReadingHistoryMonths> GetReadingHistoryMonths(Teacher teacher)
        {
            string cacheKey = string.Format("TeachersRHMonths{0}{1}{2}", teacher.TeacherId, teacher.ActionType, teacher.SubscriptionId);
            List<TeacherReadingHistoryMonths> TeachersDetails = (List<TeacherReadingHistoryMonths>)DataCache.GetCache(cacheKey);
            if (TeachersDetails == null)
            {
                TeachersDetails = CBO.FillCollection<TeacherReadingHistoryMonths>(DataProvider.Instance().GetReadingHistoryMonths(teacher));
                if (TeachersDetails != null)
                {
                    DataCache.SetCache(cacheKey, TeachersDetails, TimeSpan.FromSeconds(10));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return TeachersDetails;
        }

        /// <summary>
        /// Gets all the reading Histories of the teacher
        /// </summary>
        /// <param name="Teacher"></param>
        /// <returns></returns>
        public List<TeacherReadingHistory> GetReadingHistories(Teacher teacher)
        {
            string cacheKey = string.Format("TeachersRH{0}{1}{2}", teacher.TeacherId, teacher.ActionType, teacher.SubscriptionId);
            List<TeacherReadingHistory> readingHistories = (List<TeacherReadingHistory>)DataCache.GetCache(cacheKey);
            if (readingHistories == null)
            {
                readingHistories = CBO.FillCollection<TeacherReadingHistory>(DataProvider.Instance().GetReadingHistories(teacher));
                if (readingHistories != null)
                {
                    DataCache.SetCache(cacheKey, readingHistories, TimeSpan.FromSeconds(10));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return readingHistories;
        }

        /// <summary>
        /// Gets all the circled words of the teacher
        /// </summary>
        /// <param name="Teacher"></param>
        /// <returns></returns>
        public List<TeacherWords> GetMyWords(Teacher teacher)
        {            
            List<TeacherWords> readingHistories = CBO.FillCollection<TeacherWords>(DataProvider.Instance().GetMyWords(teacher));           
            return readingHistories;
        }

        /// <summary>
        /// Gets all the recordings of the teacher
        /// </summary>
        /// <param name="Teacher"></param>
        /// <returns></returns>
        public List<TeacherRecordings> GetMyRecordings(Teacher teacher)
        {
            string cacheKey = string.Format("TeachersRHRecordings{0}{1}{2}", teacher.TeacherId, teacher.ActionType, teacher.SubscriptionId);
            List<TeacherRecordings> readingHistories = (List<TeacherRecordings>)DataCache.GetCache(cacheKey);
            if (readingHistories == null)
            {
                readingHistories = CBO.FillCollection<TeacherRecordings>(DataProvider.Instance().GetMyRecordings(teacher));
                if (readingHistories != null)
                {
                    DataCache.SetCache(cacheKey, readingHistories, TimeSpan.FromSeconds(10));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return readingHistories;
        }

        /// <summary>
        /// Gets all the recording files of the teacher
        /// </summary>
        /// <param name="Teacher"></param>
        /// <returns></returns>
        public List<TeacherRecordingFiles> GetMyRecordedFiles(Teacher teacher)
        {
            string cacheKey = string.Format("TeachersRecordedFiles{0}{1}", teacher.TeacherId,teacher.SubscriptionId);
            List<TeacherRecordingFiles> readingHistories = (List<TeacherRecordingFiles>)DataCache.GetCache(cacheKey);
            if (readingHistories == null)
            {
                readingHistories = CBO.FillCollection<TeacherRecordingFiles>(DataProvider.Instance().GetMyRecordedFiles(teacher));
                if (readingHistories != null)
                {
                    DataCache.SetCache(cacheKey, readingHistories, TimeSpan.FromSeconds(10));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return readingHistories;
        }

        /// <summary>
        /// Gets the number of words circled by the teacher in each reading session 
        /// </summary>
        /// <param name="Teacher"></param>
        /// <returns></returns>
        public List<TeacherRHWordCount> GetMyRHWordsCount(Teacher teacher)
        {
            string cacheKey = string.Format("RHWordsCount{0}{1}", teacher.TeacherId,teacher.SubscriptionId);
            List<TeacherRHWordCount> readingHistories = (List<TeacherRHWordCount>)DataCache.GetCache(cacheKey);
            if (readingHistories == null)
            {
                readingHistories = CBO.FillCollection<TeacherRHWordCount>(DataProvider.Instance().GetMyRHWordsCount(teacher));
                if (readingHistories != null)
                {
                    DataCache.SetCache(cacheKey, readingHistories, TimeSpan.FromSeconds(10));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return readingHistories;
        }


        /// <summary>
        /// Gets the total number of words circled by the Teacher / total number of recordings done in overall.
        /// </summary>
        /// <param name="Teacher"></param>
        /// <returns></returns>
        public int GetMyWordsCount_MyRecordingsCount(Teacher teacher)
        {
            return DataProvider.Instance().GetMyWordsCount_MyRecordingsCount(teacher);
        }
        /************************** READING HISTORY END *********************************/
        #endregion

        #region Add to Group
        /// <summary>
            /// 
            /// </summary>
            /// <param name="teacherList"></param>
            /// <returns></returns>
            public int AddTeacherToGroup(List<Teacher> teacherList)
            {
                eCollection_Groups.Components.GroupController.Instance.ClearAllCaches();
                eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
                return DataProvider.Instance().AddTeacherToGroup(teacherList);
            }
        #endregion

        #region Common Methods
            /// <summary>
            /// 
            /// </summary>
            /// <param name="LoginName"></param>
            public void ClearMyCache(string LoginName)
            {
                string cacheKey = string.Format("Teachers{0}", LoginName);
                DataCache.RemoveCache(cacheKey);
            }

            /// <summary>
            /// 
            /// </summary>
            public void ClearAllCache()
            {
                foreach (string cacheKey in CacheKeys)
                    DataCache.RemoveCache(cacheKey);
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
            /// <param name="teacher"></param>
            /// <returns></returns>
            public List<IDCollection> GetSubscriptionsList(Teacher teacher)
            {
                return CBO.FillCollection<IDCollection>(DataProvider.Instance().GetSubscriptionsList(teacher));
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public List<IDCollection> GetCustSubuserSk(Teacher teacher)
            {
                return CBO.FillCollection<IDCollection>(DataProvider.Instance().GetCustSubuserSk(teacher));
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public List<Groups> GetGroupNames(Teacher teacher)
            {
                return CBO.FillCollection<Groups>(DataProvider.Instance().GetGroupNames(teacher));
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

            #region EXCEPTION MAIL
            /// <summary>
            /// 
            /// </summary>
            /// <param name="body"></param>
            public void SendExceptionMail(string body, string errorMessage, int SubsSk)
            {
                DataProvider.Instance().SendExceptionMail(body, errorMessage, SubsSk);
            }
            #endregion
        #endregion
    }
}