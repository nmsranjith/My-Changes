/*
' Copyright (c) 2012 DotNetNuke Corporation
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using DotNetNuke.Modules.eCollection_Students.Components.Model;

namespace DotNetNuke.Modules.eCollection_Students.Data
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// An abstract class for the data access layer
    /// 
    /// The abstract data provider provides the methods that a control data provider (sqldataprovider)
    /// must implement. You'll find two commented out examples in the Abstract methods region below.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class DataProvider
    {

        #region Shared/Static Methods

        private static DataProvider provider;

        // return the provider
        public static DataProvider Instance()
        {
            if (provider == null)
            {
                const string assembly = "DotNetNuke.Modules.eCollection_Students.Data.SqlDataprovider,eCollection_Students";
                Type objectType = Type.GetType(assembly, true, true);

                provider = (DataProvider)Activator.CreateInstance(objectType);
                DataCache.SetCache(objectType.FullName, provider);
            }

            return provider;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Not returning class state information")]
        public static IDbConnection GetConnection()
        {
            const string providerType = "data";
            ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);

            Provider objProvider = ((Provider)_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);
            string _connectionString;
            if (!String.IsNullOrEmpty(objProvider.Attributes["connectionStringName"]) && !String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]]))
            {
                _connectionString = System.Configuration.ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]];
            }
            else
            {
                _connectionString = objProvider.Attributes["connectionString"];
            }

            IDbConnection newConnection = new System.Data.SqlClient.SqlConnection();
            newConnection.ConnectionString = _connectionString.ToString();
            newConnection.Open();
            return newConnection;
        }

        #endregion

        #region Abstract methods

        //public abstract IDataReader GetItems(int userId, int portalId);

        //public abstract IDataReader GetItem(int itemId);        

        public abstract IDataReader GetStudentsList(Student student);

        public abstract DataTable GetSubscriptionsList(Student student);

        public abstract int CheckExists(Student student);

        public abstract int CreateStudentProfile(Student student);

        public abstract int UpdateStudentDetails(Student student);

        public abstract int UpdateStudentProfile(Student student);

        public abstract DataSet GetProfileDetails(Student student);

        public abstract DataTable GetStudentsDetails(Student students);

        public abstract DataSet GetStudentsReadingHistory(Student students);

        public abstract IDataReader GetMinMaxReadingLevelByDate(int groupId, DateTime grfRefDateFrom, DateTime grfRefDateTo);

        public abstract void DeleteStudents(Student students);
        
        public abstract int ClearFromiPad(Student student);

        public abstract IDataReader CheckClearedFromiPad(Student student);

        public abstract IDataReader GetReadingLevelHistory(Student student);

        public abstract IDataReader GetClassNames(Student student);

        public abstract IDataReader GetGroupNames(Student student);

        public abstract IDataReader GetTeacherName(int groupId);

        public abstract IDataReader GetClassMembers(Student student);

        public abstract DataTable UploadStudentProfiles(Student student);

        public abstract DataSet GetAllUserNames(Student student);

        public abstract int AddStudentToGroup(List<Student> studentList);

        public abstract IDataReader GetErrorMessagesByModuleName(string moduleName);

        public abstract string CheckUserNameExists(Student student);

        public abstract SqlDataReader GetAllUnallocatedStudents(Student student);

        public abstract int UpdateStudentLicenseAllocation(Student student);

        public abstract SqlDataReader GetAllActiveSubcriptions(string Loginname, int subsSk);

        public abstract int SwitchStudentSubcriptions(Student student);

        /************************** READING HISTORY START*********************************/
        public abstract IDataReader GetReadingHistoryMonths(Student student);
        public abstract IDataReader GetReadingHistories(Student student);
        public abstract IDataReader GetMyWords(Student student);
        public abstract IDataReader GetMyRecordings(Student student);
        public abstract IDataReader GetMyRecordedFiles(Student student);
        public abstract IDataReader GetMyRHWordsCount(Student student);
        public abstract int GetMyWordsCount_MyRecordingsCount(Student student);
        /*************************** READING HISTORY END**********************************/        

        #region Exception Mail
        public abstract void SendExceptionMail(string body, string errorMessage, int subsSk);
        #endregion

        #endregion


    }

}