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
using DotNetNuke.Services.Exceptions;
using System.Configuration;
using DotNetNuke.Modules.ProductFormat.Components.Modal;
using System.Collections.Generic;
using DotNetNuke.Modules.ProductFormat.Components.Controller;
using System.Data.SqlClient;
using System.IO;

namespace DotNetNuke.Modules.ProductFormat
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditProductFormat class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from ProductFormatModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : ProductFormatModuleBase
    {

        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SqlDataReader reader=ProductFormatController.Instance.GetAllProductFormats();
                    while (reader.Read())
                    {
                        switch (reader["FORMAT"].ToString())
                        {
                            case "Collections":
                                CollectionImage.ImageUrl = "~/"+reader["FILE_NAME"].ToString();
                                CollectionTxt.Text = reader["URL"].ToString();
                                CollectionImage.Visible = true;
                                break;
                            case "Archives":
                                ArchiveImage.ImageUrl = "~/" + reader["FILE_NAME"].ToString();
                                ArchiveTxt.Text = reader["URL"].ToString();
                                ArchiveImage.Visible = true;
                                break;
                            case "eBooks":
                                eBookImage.ImageUrl = "~/" + reader["FILE_NAME"].ToString();
                                eBooksTxt.Text = reader["URL"].ToString();
                                eBookImage.Visible = true;
                                break;
                            case "Print":
                                PrintImage.ImageUrl = "~/" + reader["FILE_NAME"].ToString();
                                PrintTxt.Text = reader["URL"].ToString();
                                PrintImage.Visible = true;
                                break;
                            case "Micro":
                                MicroImage.ImageUrl = "~/" + reader["FILE_NAME"].ToString();
                                MicroTxt.Text = reader["URL"].ToString();
                                MicroImage.Visible = true;
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        protected void SaveButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                string imgPath = ConfigurationManager.AppSettings["CMS-Image-Virtual-Path"], defaultPath = ConfigurationManager.AppSettings["ProductFormatDefaultImages"];
                List<ProductFormats> lstProductFormat = new List<ProductFormats>();
               
                lstProductFormat.Add(new ProductFormats()
                {
                    Format = "Collections",
                    Url = CollectionTxt.Text,
                    FileName = CollectionFileUpload.HasFile ? string.Concat(imgPath, CollectionFileUpload.FileName) : CollectionImage.ImageUrl != string.Empty ? string.Concat(imgPath,Path.GetFileName(Server.MapPath(CollectionImage.ImageUrl))) : string.Concat(defaultPath, "collections.jpg")
                });

                lstProductFormat.Add(new ProductFormats()
                {
                    Format = "Archives",
                    Url = ArchiveTxt.Text,
                    FileName = ArchiveFileUpload.HasFile ? string.Concat(imgPath, ArchiveFileUpload.FileName) : ArchiveImage.ImageUrl != string.Empty ? string.Concat(imgPath,Path.GetFileName(Server.MapPath(ArchiveImage.ImageUrl))) : string.Concat(defaultPath, "archives.jpg")
                });

                lstProductFormat.Add(new ProductFormats()
                {
                    Format = "eBooks",
                    Url = eBooksTxt.Text,
                    FileName = eBookFileUpload.HasFile ? string.Concat(imgPath, eBookFileUpload.FileName) :eBookImage.ImageUrl != string.Empty ? string.Concat(imgPath,Path.GetFileName(Server.MapPath(eBookImage.ImageUrl))) : string.Concat(defaultPath, "ebooks.jpg")
                });

                lstProductFormat.Add(new ProductFormats()
                {
                    Format = "Print",
                    Url = PrintTxt.Text,
                    FileName = PrintFileUpload.HasFile ? string.Concat(imgPath, PrintFileUpload.FileName) : PrintImage.ImageUrl != string.Empty ? string.Concat(imgPath,Path.GetFileName(Server.MapPath(PrintImage.ImageUrl))) : string.Concat(defaultPath, "print.jpg")
                });

                lstProductFormat.Add(new ProductFormats()
                {
                    Format = "Micro",
                    Url = MicroTxt.Text,
                    FileName = MicroFileUpload.HasFile ? string.Concat(imgPath, MicroFileUpload.FileName) : MicroImage.ImageUrl != string.Empty ? string.Concat(imgPath,Path.GetFileName(Server.MapPath(MicroImage.ImageUrl))) : string.Concat(defaultPath, "micro.jpg")
                });

                ProductFormatController.Instance.SaveProductFormats(lstProductFormat);
                if (CollectionFileUpload.HasFile)
                    CollectionFileUpload.PostedFile.SaveAs(Server.MapPath(imgPath + "/" + CollectionFileUpload.PostedFile.FileName));
                else
                    CollectionFileUpload.PostedFile.SaveAs(Server.MapPath(CollectionImage.ImageUrl != string.Empty ? CollectionImage.ImageUrl : string.Concat(defaultPath, "collections.jpg")));
                if (ArchiveFileUpload.HasFile)
                    ArchiveFileUpload.PostedFile.SaveAs(Server.MapPath(imgPath + "/" + ArchiveFileUpload.PostedFile.FileName));
                else
                    ArchiveFileUpload.PostedFile.SaveAs(Server.MapPath(ArchiveImage.ImageUrl != string.Empty ? ArchiveImage.ImageUrl : string.Concat(defaultPath, "archives.jpg")));
                if (eBookFileUpload.HasFile)
                    eBookFileUpload.PostedFile.SaveAs(Server.MapPath(imgPath + "/" + eBookFileUpload.PostedFile.FileName));
                else
                    eBookFileUpload.PostedFile.SaveAs(Server.MapPath(eBookImage.ImageUrl != string.Empty ? eBookImage.ImageUrl : string.Concat(defaultPath, "ebooks.jpg")));
                if (PrintFileUpload.HasFile)
                    PrintFileUpload.PostedFile.SaveAs(Server.MapPath(imgPath + "/" + PrintFileUpload.PostedFile.FileName));
                else
                    PrintFileUpload.PostedFile.SaveAs(Server.MapPath(PrintImage.ImageUrl != string.Empty ? PrintImage.ImageUrl : string.Concat(defaultPath, "print.jpg")));
                if (MicroFileUpload.HasFile)
                    MicroFileUpload.PostedFile.SaveAs(Server.MapPath(imgPath + "/" + MicroFileUpload.PostedFile.FileName));
                else
                    MicroFileUpload.PostedFile.SaveAs(Server.MapPath(MicroImage.ImageUrl != string.Empty ? MicroImage.ImageUrl : string.Concat(defaultPath, "micro.jpg")));
                Response.Redirect("/gale");
            }
            catch(Exception ex){
                LogFileWrite(ex);
            }
        }

    }

}