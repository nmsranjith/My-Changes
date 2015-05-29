using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Services.Scheduling;
using System.Configuration;
using System.IO;
using SevenZip;

namespace Cengage.Ecommerce.ScheduleTask
{
    /// <summary>
    /// Summary description for PMeBookCM
    /// </summary>
    public class PMeBookCM : SchedulerClient
    {
        #region Private Members

        // Path where the files will get extracted
        private string driveName = ConfigurationManager.AppSettings.Get("StagingPath");
        private string publishingPath_tempPhysical = ConfigurationManager.AppSettings.Get("PublishingTempPath");
        private string publishingPath_physical = ConfigurationManager.AppSettings.Get("PublishingPath");
        private string ISBNThumNailsSourcePath = ConfigurationManager.AppSettings.Get("ISBNThumNailsSourcePath");
        private string ISBNThumNailsPath = ConfigurationManager.AppSettings.Get("ISBNThumNailsPath");
        #endregion
        public PMeBookCM(ScheduleHistoryItem oItem)
            : base()
        {
            this.ScheduleHistoryItem = oItem;
        }

        public override void DoWork()
        {
            try
             {
                //Perform required items for logging

                this.Progressing();

                //Your code goes here
                //To log note
                string sevenZipDllPath = ConfigurationManager.AppSettings.Get("SevenZipDllPath");
                SevenZip.SevenZipBase.SetLibraryPath(sevenZipDllPath);
                //SevenZip.SevenZipBase.SetLibraryPath(Server.MapPath("~/bin/7z.dll"));
                //Extract Files
                string publishPath = publishingPath_physical;
                string publishTempPath = publishingPath_tempPhysical;

                List<string> isbnInPublish = GetFileNames(publishTempPath, "*.epub");
                //List<string> isbnImageFileName = GetFileNames(ISBNThumNailsSourcePath, "");
                SevenZipExtractor extractor = null;
                foreach (string isbn in isbnInPublish)
                {
                    if (File.Exists(Path.Combine(publishTempPath, isbn)))
                    {
                        try
                        {
                            string output = Path.Combine(publishPath, isbn).Substring(0, Path.Combine(publishPath, isbn).LastIndexOf('.')).Trim();
                            if (Directory.Exists(output))
                            {
                                DirectoryInfo di = new DirectoryInfo(output);
                                di.Delete(true);
                            }
                            Directory.CreateDirectory(output);
                            extractor = new SevenZipExtractor(Path.Combine(publishTempPath, isbn));
                            extractor.ExtractArchive(output);
                            if (File.Exists(Path.Combine(publishingPath_physical, isbn)))
                                File.Delete(Path.Combine(publishingPath_physical, isbn));
                            File.Copy(Path.Combine(publishTempPath, isbn), Path.Combine(publishingPath_physical, isbn));							
                            File.Delete(Path.Combine(publishTempPath, isbn));
                            //ThumNails
                            string thumNailsSourcePath = ISBNThumNailsSourcePath;
                            thumNailsSourcePath = thumNailsSourcePath.Replace("ISBN", isbn.Substring(0, isbn.LastIndexOf('.')));
                            // if (File.Exists(Path.Combine(ISBNThumNailsSourcePath, isbn).Substring(0, Path.Combine(ISBNThumNailsSourcePath, isbn).LastIndexOf('.')).Trim()))
                            DirectoryInfo thumbNailsDirectory = new DirectoryInfo(thumNailsSourcePath);
                            foreach (FileInfo file in thumbNailsDirectory.GetFiles())
                            {
                                try
                                {
                                    if (File.Exists(Path.Combine(ISBNThumNailsPath, file.ToString())))
                                        File.Delete(Path.Combine(ISBNThumNailsPath, file.ToString()));
                                    File.Copy(Path.Combine(thumNailsSourcePath, file.ToString()), Path.Combine(ISBNThumNailsPath, file.ToString()));
                                }
                                catch (Exception ex) { DotNetNuke.Modules.eCollection_Dashboards.eCollection_DashboardsModuleBase.LogFileWrite(new Exception(ex.Message + " for ISBN : " + isbn, ex.InnerException)); }
                            }
                            List<string> fileNames = new List<string>();
                            string AudioSourcePath = ConfigurationManager.AppSettings.Get("ISBNAudioContextFilesPath");
                            //DotNetNuke.Modules.eCollection_Dashboards.eCollection_DashboardsModuleBase.LogFileWrite(new Exception(" for ISBN : " + isbn + " " + ConfigurationManager.AppSettings.Get("ISBNAudioContextFilesPath")));
                            AudioSourcePath = AudioSourcePath.Replace("ISBN", isbn.Substring(0, isbn.LastIndexOf('.')));
                            DirectoryInfo AudioDirectory = new DirectoryInfo(AudioSourcePath);
                            fileNames.Add((AudioDirectory.GetFiles().Where(p => (p.Extension == ".xlsx" || p.Extension == ".xls")).ToArray()[0] as FileInfo).FullName);

                            AudioSourcePath = ConfigurationManager.AppSettings.Get("ISBNAudioPageFilesPath");
                            //DotNetNuke.Modules.eCollection_Dashboards.eCollection_DashboardsModuleBase.LogFileWrite(new Exception(" for ISBN : " + isbn + " " + ConfigurationManager.AppSettings.Get("ISBNAudioPageFilesPath")));
                            AudioSourcePath = AudioSourcePath.Replace("ISBN", isbn.Substring(0, isbn.LastIndexOf('.')));
                            AudioDirectory = new DirectoryInfo(AudioSourcePath);
                            fileNames.Add((AudioDirectory.GetFiles().Where(p => (p.Extension == ".xlsx" || p.Extension == ".xls")).ToArray()[0] as FileInfo).FullName);

                            AudioSourcePath = ConfigurationManager.AppSettings.Get("ISBNAudioWordFilesPath");
                            //DotNetNuke.Modules.eCollection_Dashboards.eCollection_DashboardsModuleBase.LogFileWrite(new Exception(" for ISBN : " + isbn + " " + ConfigurationManager.AppSettings.Get("ISBNAudioWordFilesPath")));
                            AudioSourcePath = AudioSourcePath.Replace("ISBN", isbn.Substring(0, isbn.LastIndexOf('.')));
                            AudioDirectory = new DirectoryInfo(AudioSourcePath);
                            fileNames.Add((AudioDirectory.GetFiles().Where(p => (p.Extension == ".xlsx" || p.Extension == ".xls")).ToArray()[0] as FileInfo).FullName);

                            DotNetNuke.Modules.eCollection_Dashboards.eCollection_DashboardsModuleBase.WriteAudioPath(fileNames, isbn.Replace(".epub", string.Empty));

                        }
                        catch (Exception ex) { DotNetNuke.Modules.eCollection_Dashboards.eCollection_DashboardsModuleBase.LogFileWrite(new Exception(" for ISBN : " + isbn + " " + ex.ToString(), ex.InnerException)); }

                    }
                }
                if (Directory.Exists(publishTempPath) && GetFileNames(publishTempPath, "*.epub").Count == 0)
                    Directory.Delete(publishTempPath, true);
                if (extractor != null)
                    extractor.Dispose();
                //Show success
                this.ScheduleHistoryItem.Succeeded = true;
            }
            catch (Exception ex)
            {
                this.ScheduleHistoryItem.Succeeded = false;
                //InsertLogNote("Exception= " + ex.ToString());
                this.Errored(ref ex);
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            }
        }

        /// <summary>
        /// This method will get the collection of files existed in the path
        /// </summary>
        /// <param name="destinationPath"></param>
        /// <returns>return collection of filenames</returns>
        private List<string> GetFileNames(string destinationPath, string extension)
        {
            List<string> existedBook = new List<string>();
            if (Directory.Exists(destinationPath))
            {
                DirectoryInfo MyRoot = new DirectoryInfo(destinationPath);
                foreach (FileInfo f in MyRoot.GetFiles(extension))
                {
                    existedBook.Add(Path.GetFileName(f.FullName));
                }
            }
            return existedBook;
        }


    }
}