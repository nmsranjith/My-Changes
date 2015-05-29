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
using DotNetNuke.Common.Utilities;
using System.Collections.Generic;

namespace DotNetNuke.Modules.eCollection_FeatureSlider
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditeCollection_FeatureSlider class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from eCollection_FeatureSliderModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : eCollection_FeatureSliderModuleBase
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
                //Implement your edit logic for your module

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SlidesDrpDwn_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Slide_SaveButton_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubmitSlideCount_Click(object sender, EventArgs e)
        {
            SecondDiv.Visible = true;
            List<int> SlidesCountsList = new List<int>();
            for (int i = 1; i <= Null.SetNullInteger(SlidesCountTxt.Text); i++)
                SlidesCountsList.Add(i);

            SlidesDrpDwn.DataSource = SlidesCountsList;
            SlidesDrpDwn.DataBind();
            if (SlidesCountsList.Count > 1)
            {
                SlidesDrpDwn.Enabled = true;
                Slide_HeaderTxt.Enabled = true;
                SlideText_MultiLine.Enabled = true;
                Slide_FileUpload.Enabled = true;
            }
        }
    }

}