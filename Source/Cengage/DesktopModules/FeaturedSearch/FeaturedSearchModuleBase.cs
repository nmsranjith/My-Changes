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
using System.Configuration;
using System.IO;
using System.Web;
namespace DotNetNuke.Modules.FeaturedSearch
{

    /// <summary>
    /// This base class can be used to define custom properties for multiple controls. 
    /// An example module, DNNSimpleArticle (http://dnnsimplearticle.codeplex.com) uses this for an ArticleId
    /// 
    /// Because the class inherits from PortalModuleBase, properties like ModuleId, TabId, UserId, and others, 
    /// are accessible to your module's controls (that inherity from FeaturedSearchModuleBase
    /// 
    /// </summary>

    public class FeaturedSearchModuleBase : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        /// <summary>
        ///  Writes Exceptions to log file in user readable format
        /// </summary>
        /// <param name="e"></param>
        public static void LogFileWrite(Exception e)
        {
            if (e.Source != "mscorlib")
            {
                FileStream fileStream = null;
                StreamWriter streamWriter = null;
                try
                {
                    string logFilePath = string.Concat(ConfigurationManager.AppSettings["LogFilePath"], "HE Exceptions - ", DateTime.Today.ToString("dd-MM-yyyy"), "." + "txt");

                    if (logFilePath.Equals("")) return;
                    #region Create the Log file directory if it does not exists
                    DirectoryInfo logDirInfo = null;
                    FileInfo logFileInfo = new FileInfo(logFilePath);
                    logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                    if (!logDirInfo.Exists) logDirInfo.Create();
                    #endregion Create the Log file directory if it does not exists

                    if (!logFileInfo.Exists)
                    {
                        fileStream = logFileInfo.Create();
                    }
                    else
                    {
                        fileStream = new FileStream(logFilePath, FileMode.Append);
                    }
                    streamWriter = new StreamWriter(fileStream);
                    var uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                    streamWriter.WriteLine(string.Concat("\r\n-----------------------------------", DateTime.Now, "---------------------------------------------",
                       "\r\n Website: ", uri.Host,
                        "\r\n Type: ", e.GetType(),
                        "\r\n Source: ", e.Source, "\r\n Exception: ", e.Message, "\r\n Description: ", e.StackTrace, "\r\n-----------------------------------------------------------------------------------------------------------------"));

                    // TeacherController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
                }
                finally
                {
                    if (streamWriter != null) streamWriter.Close();
                    if (fileStream != null) fileStream.Close();
                }
            }
        }        
    }
}
