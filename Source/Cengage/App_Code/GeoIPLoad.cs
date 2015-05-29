using System;
using DotNetNuke.Services.Scheduling;
using System.Text;
using System.IO;
using System.Net;
using System.IO.Compression;
using DotNetNuke.Instrumentation;
using System.Web.Hosting;
using System.Configuration;

namespace Cengage.Ecommerce.ScheduleTask
{
    public class GeoIPLoad : SchedulerClient
    {
        # region variable and object creation and initialization
        string destinationFilePath = HostingEnvironment.MapPath(ConfigurationManager.AppSettings.Get("GeoIPDBPath"));
        string proxyurl = ConfigurationManager.AppSettings.Get("GeoIPDBURL");
        string filePath = "";
        string proxyStatus = ConfigurationManager.AppSettings.Get("UseProxy");
        string proxyUserID = ConfigurationManager.AppSettings.Get("ProxyUserID");
        string proxyPassword = ConfigurationManager.AppSettings.Get("ProxyPassword");
        string proxyServer = ConfigurationManager.AppSettings.Get("ProxyServer");
        string proxyPort = ConfigurationManager.AppSettings.Get("ProxyPort");
        WebClient client;
        GZipStream zipStream;
        #endregion

        public GeoIPLoad(ScheduleHistoryItem oItem)
            : base()
        {
            this.ScheduleHistoryItem = oItem;
        }

        public override void DoWork()
        {
            try
            {
                this.Progressing();
                // download the file chechk for proxyStatus if proxyStatus is true get download location and download the file
                using (client = new WebClient())
                {
                    if (proxyStatus.ToLower() == "true")
                    {
                        WebProxy Proxy = new WebProxy(proxyServer, int.Parse(proxyPort));
                        Proxy.Credentials=new NetworkCredential(proxyUserID, proxyPassword); ;
                        client.Proxy = Proxy;
                    }
                    client.DownloadFile(proxyurl, destinationFilePath);
                }
                //open the downloaded file
                using (FileStream fs = new FileStream(destinationFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (zipStream = new GZipStream(fs, CompressionMode.Decompress))
                    {
                        string path = Path.GetTempPath();
                        string fileName = Path.GetFileName(proxyurl);
                        filePath = Path.Combine(path, fileName);
                        using (FileStream fOutStream =
                          new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            byte[] tempBytes = new byte[4096];
                            int i;
                            while ((i = zipStream.Read(tempBytes, 0, tempBytes.Length)) != 0)
                            {
                                fOutStream.Write(tempBytes, 0, i);
                            }
                        }
                    }
                }
                File.Copy(filePath, destinationFilePath, true);  
                this.ScheduleHistoryItem.Succeeded = true;
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Message" + ex.Message + "\t");
                if (ex.InnerException != null)
                {
                    sb.Append("Inner Exception Message" + ex.InnerException.Message + "\t");
                    sb.Append("Inner Exception Stacktrace" + ex.InnerException.StackTrace + "\t");
                }
                DnnLog.Error(sb);
            }
        }
    }
}