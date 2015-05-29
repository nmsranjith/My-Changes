using DotNetNuke.Common.Utilities;
using System.Data;
using DotNetNuke.Modules.eCollection_Students.Data;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using System.Collections.Generic;
using System;
using System.Linq;
using DotNetNuke.Modules.eCollection_Students.Components.Common;
using System.Data.SqlClient;

namespace DotNetNuke.Modules.eCollection_Students.Components.Controllers
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentsController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A controller class for to perform business activities
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public sealed class StudentsController
    {
        public static readonly StudentsController studCtrl = new StudentsController();

        private StudentsController() { }

        public static StudentsController Instance
        {
            get { return studCtrl; }
            set { }
        }
        
        private static readonly List<string> CacheKeys = new List<string>();

        /// <summary>
        ///  Get the student's Profile details
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public DataSet GetProfileDetails(Student student)
        {
            string cacheKey = string.Format("Students{0}", student.StudentId);
            var studentsDetails = (DataSet)DataCache.GetCache(cacheKey);
            if (studentsDetails == null)
            {
                studentsDetails = DataProvider.Instance().GetProfileDetails(student);
                if (studentsDetails != null)
                {
                    DataCache.SetCache(cacheKey, studentsDetails, TimeSpan.FromSeconds(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return studentsDetails;
        }

        /// <summary>
        ///  Get the student reading history
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public DataSet GetStudentsReadingHistory(Student student)
        {
            string cacheKey = string.Format("StudentsReadingHistory{0}", student.StudentId);
            var studentsDetails = (DataSet)DataCache.GetCache(cacheKey);
            if (studentsDetails == null)
            {
                studentsDetails = DataProvider.Instance().GetStudentsReadingHistory(student);
                if (studentsDetails != null)
                {
                    DataCache.SetCache(cacheKey, studentsDetails, TimeSpan.FromSeconds(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return studentsDetails;
        }

        /************************** READING HISTORY START*********************************/
        /// <summary>
        /// Gets all the reading history months of the student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<StudentReadingHistoryMonths> GetReadingHistoryMonths(Student student)
        {
            string cacheKey = string.Format("StudentsRHMonths{0}{1}", student.StudentId,student.ActionType);
            List<StudentReadingHistoryMonths> studentsDetails = (List<StudentReadingHistoryMonths>)DataCache.GetCache(cacheKey);
            if (studentsDetails == null)
            {
                studentsDetails = CBO.FillCollection<StudentReadingHistoryMonths>(DataProvider.Instance().GetReadingHistoryMonths(student));
                if (studentsDetails != null)
                {
                    DataCache.SetCache(cacheKey, studentsDetails, TimeSpan.FromSeconds(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return studentsDetails;
        }

        /// <summary>
        /// Gets all the reading Histories of the Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<StudentReadingHistory> GetReadingHistories(Student student)
        {
            string cacheKey = string.Format("StudentsRH{0}{1}", student.StudentId,student.ActionType);
            List<StudentReadingHistory> readingHistories = (List<StudentReadingHistory>)DataCache.GetCache(cacheKey);
            if (readingHistories == null)
            {
                readingHistories = CBO.FillCollection<StudentReadingHistory>(DataProvider.Instance().GetReadingHistories(student));
                if (readingHistories != null)
                {
                    DataCache.SetCache(cacheKey, readingHistories, TimeSpan.FromSeconds(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return readingHistories;
        }

        /// <summary>
        /// Gets all the circled words of the Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<StudentWords> GetMyWords(Student student)
        {           
           List<StudentWords> readingHistories = CBO.FillCollection<StudentWords>(DataProvider.Instance().GetMyWords(student));                
           return readingHistories;
        }

        /// <summary>
        /// Gets all the recordings of the Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<StudentRecordings> GetMyRecordings(Student student)
        {
            string cacheKey = string.Format("StudentsRHRecordings{0}{1}", student.StudentId, student.ActionType);
            List<StudentRecordings> readingHistories = (List<StudentRecordings>)DataCache.GetCache(cacheKey);
            if (readingHistories == null)
            {
                readingHistories = CBO.FillCollection<StudentRecordings>(DataProvider.Instance().GetMyRecordings(student));
                if (readingHistories != null)
                {
                    DataCache.SetCache(cacheKey, readingHistories, TimeSpan.FromSeconds(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return readingHistories;
        }

        /// <summary>
        /// Gets all the recording files of the Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<StudentRecordingFiles> GetMyRecordedFiles(Student student)
        {
            string cacheKey = string.Format("StudentsRecordedFiles{0}", student.StudentId);
            List<StudentRecordingFiles> readingHistories = (List<StudentRecordingFiles>)DataCache.GetCache(cacheKey);
            if (readingHistories == null)
            {
                readingHistories = CBO.FillCollection<StudentRecordingFiles>(DataProvider.Instance().GetMyRecordedFiles(student));
                if (readingHistories != null)
                {
                    DataCache.SetCache(cacheKey, readingHistories, TimeSpan.FromSeconds(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return readingHistories;
        }

        /// <summary>
        /// Gets the number of words circled by the Student in each reading session 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<StudentRHWordCount> GetMyRHWordsCount(Student student)
        {
            string cacheKey = string.Format("RHWordsCount{0}", student.StudentId);
            List<StudentRHWordCount> readingHistories = (List<StudentRHWordCount>)DataCache.GetCache(cacheKey);
            if (readingHistories == null)
            {
                readingHistories = CBO.FillCollection<StudentRHWordCount>(DataProvider.Instance().GetMyRHWordsCount(student));
                if (readingHistories != null)
                {
                    DataCache.SetCache(cacheKey, readingHistories, TimeSpan.FromSeconds(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return readingHistories;
        }


        /// <summary>
        /// Gets the total number of words circled by the Student / total number of recordings done in overall.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int GetMyWordsCount_MyRecordingsCount(Student student)
        {
            string cacheKey = string.Format("StudentsMyWordsCount_MyRecordingsCount{0}{1}", student.StudentId, student.ActionType);           
            return DataProvider.Instance().GetMyWordsCount_MyRecordingsCount(student);
        }
        /************************** READING HISTORY END *********************************/

        /// <summary>
        ///  Get all the members of the class
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<Student> GetClassMembers(Student student)
        {
            try
            {
                return CBO.FillCollection<Student>(DataProvider.Instance().GetClassMembers(student));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check username exists or not
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public string CheckUserNameExists(Student student)
        {
            try
            {
                return DataProvider.Instance().CheckUserNameExists(student);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Get Reading level history of a student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<Student> GetReadingLevelHistory(Student student)
        {
            IDataReader dr = DataProvider.Instance().GetReadingLevelHistory(student);
            List<Student> readLvlHis = new List<Student>();
            try
            {
                while (dr.Read())
                {
                    readLvlHis.Add(new Student()
                    {
                        CurrentReadingLevel = int.Parse(dr["ReadingLevel"].ToString()),
                        CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                        PercentRead = int.Parse(dr["PercentRead"].ToString())
                    });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return readLvlHis;
        }

        /// <summary>
        ///  Get all class names
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<Groups> GetClassNames(Student student)
        {
            string cacheKey = string.Format("ClassNames{0}", student.TeacherLoginName);
            var classes = (List<Groups>)DataCache.GetCache(cacheKey);
            if (classes == null)
            {
                classes = CBO.FillCollection<Groups>(DataProvider.Instance().GetClassNames(student));
                if (classes != null)
                {
                    DataCache.SetCache(cacheKey, classes, TimeSpan.FromMinutes(2));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return classes;
        }

        /// <summary>
        /// Get all group Names
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<Groups> GetGroupNames(Student student)
        {
            return CBO.FillCollection<Groups>(DataProvider.Instance().GetGroupNames(student));              
        }

        /// <summary>
        ///  Get the teacher Names of the group
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public List<string> GetTeacherName(int groupid)
        {
            IDataReader dr = DataProvider.Instance().GetTeacherName(groupid);
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
        /// Get all the students of the subscription
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<Student> GetAll(Student student)
        {
            string cacheKey = string.Format("Students{0}{1}", student.TeacherLoginName,student.SubscriptionId);
            List<Student> studentsDetails = (List<Student>)DataCache.GetCache(cacheKey);
            if (studentsDetails == null)
            {
                studentsDetails = CBO.FillCollection<Student>(DataProvider.Instance().GetStudentsList(student));
                if (studentsDetails != null)
                {                    
                    DataCache.SetCache(cacheKey, studentsDetails, TimeSpan.FromMinutes(5));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return studentsDetails;
        }

        /// <summary>
        /// Create student profile
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int? Add(Student student)
        {
            eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
            ClearMyCache(string.Concat(student.TeacherLoginName,student.SubscriptionId));
            return DataProvider.Instance().CreateStudentProfile(student);
        }

        /// <summary>
        /// Update the student profile
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int Update(Student student)
        {
            eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
            ClearAllCache();
            return DataProvider.Instance().UpdateStudentDetails(student);
        }

        /// <summary>
        /// Check whether the student exist or not
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int CheckExists(Student student)
        {
            return DataProvider.Instance().CheckExists(student);
        }

        /// <summary>
        ///  Get the subscription list of the user
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public DataTable GetSubscriptionsList(Student student)
        {
            string cacheKey = string.Format("StudentsSubscriptions{0}", student.UserLoginName);
            DataTable subscriptions = (DataTable)DataCache.GetCache(cacheKey);
            if (subscriptions == null)
            {
                subscriptions = DataProvider.Instance().GetSubscriptionsList(student);
                subscriptions.Columns.Add(new DataColumn("AddEditBtnText"));
                foreach (DataRow row in subscriptions.Rows)
                {
                    if (row["SubscriptionName"].ToString() == string.Empty)
                    {
                        row["SubscriptionName"] = string.Empty;
                        row["AddEditBtnText"] = "ADD LABEL";
                    }
                    else
                        row["AddEditBtnText"] = "EDIT LABEL";

                }
                if (subscriptions != null)
                {
                    DataCache.SetCache(cacheKey, subscriptions, TimeSpan.FromMinutes(5));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return subscriptions;
        }

        #region Bulk Upload 
            /// <summary>
            /// Create student profiles using Bulk upload process
            /// </summary>
            /// <param name="students"></param>
            /// <returns></returns>
            public List<Student> UploadStudentProfiles(Student students)
            {
                eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
                ClearAllCache();
                string cacheKey = string.Format("StudentsBulk{0}{1}", students.TeacherLoginName, students.SubscriptionId);
                List<Student> studentsDetails = (List<Student>)DataCache.GetCache(cacheKey);
                if (studentsDetails == null)
                {
                    var dt = DataProvider.Instance().UploadStudentProfiles(students);
                    studentsDetails = new List<Student>(dt.AsEnumerable()
                                     .Select(row => new Student
                                     {                                        
                                         StudentId = int.Parse(row["StudentId"].ToString()),
                                         FirstName = row["FirstName"].ToString(),
                                         LastName = row["LastName"].ToString(),
                                         DisplayName = row["DisplayName"].ToString(),
                                         FullName = row["FullName"].ToString(),
                                         UserLoginName = row["UserLoginName"].ToString(),
                                         CurrentReadingLevel = int.Parse(row["CurrentReadingLevel"].ToString()),
                                         CustSubUserSk = int.Parse(row["CustomerSubsUserSk"].ToString()),
                                         SubscriptionId = int.Parse(row["SubsSK"].ToString()),
                                         CreatedDate = DateTime.Parse(row["CreatedDate"].ToString()),
                                         ClassId = Null.SetNullInteger(row["ClassId"])
                                     }));
                //    studentsDetails = CBO.FillCollection<Student>();
                    if (studentsDetails != null)
                    {
                        DataCache.SetCache(cacheKey, studentsDetails, TimeSpan.FromMinutes(5));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                return studentsDetails;
            }
            
            /// <summary>
            ///  Get all Students Username
            /// </summary>
            /// <param name="students"></param>
            /// <returns></returns>
            public DataSet GetAllUserNames(Student students)
            {
                string cacheKey = string.Format("Students{0}{1}","BulkUpload",students.TeacherLoginName);
                DataSet UserNames = (DataSet)DataCache.GetCache(cacheKey);
                if (UserNames == null)
                {
                    UserNames = DataProvider.Instance().GetAllUserNames(students);                    
                    if (UserNames != null)
                    {
                        DataCache.SetCache(cacheKey, UserNames, TimeSpan.FromMinutes(10));
                        if (!CacheKeys.Contains(cacheKey))
                            CacheKeys.Add(cacheKey);
                    }
                }
                return UserNames;
            }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int UpdateReadingRecovery(Student student)
        {            
            ClearMyCache(student.StudentId.ToString());
            return DataProvider.Instance().UpdateStudentProfile(student);
        }

        /// <summary>
        /// Update book reading level of a student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int UpdateBookReadingLevel(Student student)
        {
            ClearMyCache(student.StudentId.ToString());
            return DataProvider.Instance().UpdateStudentProfile(student);
        }

        /// <summary>
        /// Update PM reading Level of a student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int UpdateReadingLevel(Student student)
        {
            ClearMyCache(student.StudentId.ToString());
            ClearAllCache();
            return DataProvider.Instance().UpdateStudentProfile(student);
        }

        /// <summary>
        /// Delete Student Profile
        /// </summary>
        /// <param name="students"></param>
        public void DeleteStudents(Student students)
        {
            eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
            ClearAllCache();
            DataProvider.Instance().DeleteStudents(students);
        }

        /// <summary>
        /// Get student profile details
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        public DataTable GetStudentsDetails(Student students)
        {
            return DataProvider.Instance().GetStudentsDetails(students);
        }

        /// <summary>
        /// Clear a particular cache
        /// </summary>
        /// <param name="cachename"></param>
        public void ClearMyCache(string cachename)
        {
            string cacheKey = string.Format("Students{0}", cachename);
            DataCache.RemoveCache(cacheKey);
        }

        /// <summary>
        /// Clear all the Caches
        /// </summary>
        public void ClearAllCache()
        {
            foreach (string cacheKey in CacheKeys)
                DataCache.RemoveCache(cacheKey);
        }        

        /// <summary>
        ///  Clear words from iPad
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int ClearFromiPad(Student student)
        {
            ClearAllCache();
            return DataProvider.Instance().ClearFromiPad(student);            
        }

        /// <summary>
        /// Check clear from iPad flag
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public List<IDCollection> CheckClearedFromiPad(Student student)
        {
            string cacheKey = string.Format("StudentsClearFromIpad{0}", student.StudentId);
            List<IDCollection> IsClearedDetails = (List<IDCollection>)DataCache.GetCache(cacheKey);
            if (IsClearedDetails == null)
            {
                IsClearedDetails = CBO.FillCollection<IDCollection>(DataProvider.Instance().CheckClearedFromiPad(student));
                if (IsClearedDetails != null)
                {
                    DataCache.SetCache(cacheKey, IsClearedDetails, TimeSpan.FromSeconds(10));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return IsClearedDetails;
        }

        /// <summary>
        /// Add students to Group/Class
        /// </summary>
        /// <param name="studentlist"></param>
        /// <returns></returns>
        public int AddStudentToGroup(List<Student> studentlist)
        {
            eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
            eCollection_Groups.Components.GroupController.Instance.ClearAllCaches();
            return DataProvider.Instance().AddStudentToGroup(studentlist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module_name"></param>
        /// <returns></returns>
        public List<Messages> GetErrorMessagesByModuleName(string module_name)
        {
            string cacheKey = string.Format("GetErrorMessagesByModuleName{0}", module_name);
            List<Messages> moduleMessages = (List<Messages>)DataCache.GetCache(cacheKey);
            if (moduleMessages == null)
            {
                moduleMessages = CBO.FillCollection<Messages>(DataProvider.Instance().GetErrorMessagesByModuleName(module_name));
                List<Messages> commonmessages = CBO.FillCollection<Messages>(DataProvider.Instance().GetErrorMessagesByModuleName(eCollection_StudentsModuleBase.CommonModule));
                moduleMessages.AddRange(commonmessages);
                if (moduleMessages != null)
                {
                    DataCache.SetCache(cacheKey, moduleMessages);
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return moduleMessages;
        }
		
        #region EXCEPTION MAIL
        /// <summary>
        /// Send Exception Email
        /// </summary>
        /// <param name="body"></param>
        /// <param name="errorMessage"></param>
        /// <param name="subssk"></param>
        public void SendExceptionMail(string body, string errormessage, int subssk)
        {
            DataProvider.Instance().SendExceptionMail(body, errormessage, subssk);
        }
        #endregion

        /// <summary>
        /// Get Min and Max Reading Level between from and To dates
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="grfrefdatefrom"></param>
        /// <param name="grfrefdateto"></param>
        /// <returns></returns>
        public int[] GetMinMaxReadingLevelByDate(int groupid, DateTime grfrefdatefrom, DateTime grfrefdateto)
        {
            IDataReader dr = DataProvider.Instance().GetMinMaxReadingLevelByDate(groupid, grfrefdatefrom, grfrefdateto);
            int[] RL = new int[3];
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
                    if (dr["InitRL"].ToString() != null && dr["InitRL"].ToString() != "")
                        RL[2] = (int)dr["InitRL"];
                    else RL[2] = 0;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return RL;
        }

        /// <summary>
        /// Get all unallocated students of the particular account / user group
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public SqlDataReader GetAllUnallocatedStudents(Student student)
        {
            try
            {
                return DataProvider.Instance().GetAllUnallocatedStudents(student);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Update status of license allocation
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int UpdateStudentLicenseAllocation(Student student)
        {
            try
            {
                ClearAllCache();
                eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
                eCollection_Groups.Components.GroupController.Instance.ClearAllCaches();
                eCollection_Sessions.Components.Controllers.SessionController.Instance.ClearAllCaches();
                return DataProvider.Instance().UpdateStudentLicenseAllocation(student);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        ///  Get all Active subscriptions
        /// </summary>
        /// <param name="loginname"></param>
        /// <param name="subsSk"></param>
        /// <returns></returns>
        public SqlDataReader GetAllActiveSubcriptions(string loginname, int subsSk)
        {
            try
            {
                return DataProvider.Instance().GetAllActiveSubcriptions(loginname, subsSk);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Switch student subscriptions
        /// </summary>
        /// <param name="loginname"></param>
        /// <param name="subsSk"></param>
        /// <returns></returns>
        public int SwitchStudentSubcriptions(Student student)
        {
            try
            {
                ClearAllCache();
                eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
                eCollection_Groups.Components.GroupController.Instance.ClearAllCaches();
                eCollection_Sessions.Components.Controllers.SessionController.Instance.ClearAllCaches();
                return DataProvider.Instance().SwitchStudentSubcriptions(student);
            }
            catch (Exception ex) { throw ex; }
        }
        
    }
}