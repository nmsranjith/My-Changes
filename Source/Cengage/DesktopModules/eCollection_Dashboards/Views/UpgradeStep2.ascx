<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpgradeStep2.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Dashboards.Views.UpgradeStep2" %>
<div class="load-srf">
	<div class="overlay-srf"></div>
<div class="sample-request-load"> 

 <div></div>
<div></div>
<div></div>
<div></div>
</div>
</div>
<div class="row">
    <div id="laststep">
    </div>
    <div id="formcontent">
        <p id="KeepHeadr">
            This information will be kept:</p>
        <div class="lists" id="TeacherCheckDiv">
            <div class="Div_FullWidth upgradedivmargintop">
                <div class="formcontenttick">
                </div>
                <label class="listfont">
                    All teachers attached to this subscription.</label></div>
            <div class="Div_FullWidth">
                <span class="hypenspan">- </span>
                <p>
                    Teacher access is free and does not count against your purchased</p>
                <p>
                    licenses.</p>
                <span class="hypenspan upgradeMargintop">- </span>
                <p class="upgradeMargintop">
                    You can remove individual teacher access from your accounts</p>
                <p>
                    dashboard at any time.</p>
            </div>
        </div>
        <div class="lists" id="StudCheckDiv">
            <div class="Div_FullWidth upgradedivmargintop">
                <div class="formcontenttick">
                </div>
                <label class="listfont">
                    All student information.</label></div>
            <div class="Div_FullWidth">
                <span class="hypenspan">- </span>
                <p>
                    This includes profile information, reading history, PM reading level, reading age,
                </p>
                <p>
                    student recordings &nbsp;and My Words.</p>
            </div>
        </div>
        
        <div class="lists" id="GrpCheckDiv">
            <div class="Div_FullWidth upgradedivmargintop">
                <div class="formcontenttick">
                </div>
                <label class="listfont">
                    All groups and classes.
                </label>
            </div>
            <p>
            </p>
        </div>
        <div class="lists" clientidmode="Static" id="MoveStudDiv" runat="server" style="display:none;">
            <div class="Div_FullWidth upgradedivmargintop">
                <div class="formcontenttick">
                </div>
                <label class="listfont">
                    All students up one year level.</label></div>
            <div class="Div_FullWidth">
                <span class="hypenspan"> </span>
                <p>
                   
                </p>
                <p>
                 </p>
            </div>
        </div>
        <div class="lists" id="BooksCheckDiv">
            <div class="Div_FullWidth upgradedivmargintop">
                <div class="formcontenttick">
                </div>
                <label class="listfont">
                    Use the same books as last year.</label></div>
            <div class="Div_FullWidth">
                <span class="hypenspan">- </span>
                <p>
                    You are able to modify your book selection for the first 30 days of your
                </p>
                <p>
                    subscription.</p>
            </div>
        </div>
        <div class="Div_FullWidth upgradedivmargintop">
            <p id="ArchiveHeadr">
                This information will be archived:</p>
        </div>
        <div class="lists" id="StudArchDiv">
            <div class="Div_FullWidth upgradedivmargintop">
                <div class="dontgrey">
                </div>
                <label class="listfont">
                    All student information.</label></div>
            <div class="Div_FullWidth">
                <span class="hypenspan">- </span>
                <p>
                    This includes profile information, reading history, PM reading level, reading age,
                </p>
                <p>
                    student recordings &nbsp;and My Words.</p>
            </div>
        </div>
        <div class="lists" id="MoveStudArchDiv" clientidmode="Static" runat="server">
            <div class="Div_FullWidth upgradedivmargintop">
                <div class="dontgrey">
                </div>
                <label class="listfont">
                    All students up one year level.</label></div>
            <div class="Div_FullWidth">
                <span class="hypenspan"> </span>
                <p>
                   
                </p>
                <p>
                  </p>
            </div>
        </div>
        
        <div class="lists" id="GrpsDelDiv">
            <div class="Div_FullWidth upgradedivmargintop">
                <div class="dontgrey">
                </div>
                <label class="listfont">
                    All groups and classes.
                </label>
            </div>
            <p>
            </p>
        </div>
        <div class="Div_FullWidth upgradedivmargintop">
            <p id="DeleteHeadr">
                This information will be deleted:</p>
        </div>
        <div class="lists " id="TeachDelDiv">
            <div class="Div_FullWidth upgradedivmargintop">
                <div class="dontred">
                </div>
                <label class="listfont">
                    All teachers attached to this subscription.</label></div>
            <div class="Div_FullWidth">
                <span class="hypenspan">- </span>
                <p>
                    Teacher access is free and does not count against your purchased</p>
                <p>
                    licenses.</p>
                <span class="hypenspan upgradeMargintop">- </span>
                <p class="upgradeMargintop">
                    You can remove individual teacher access from your accounts</p>
                <p>
                    dashboard at any time.</p>
            </div>
        </div>
        
        <div class="lists " id="BooksDelDiv">
            <div class="Div_FullWidth upgradedivmargintop">
                <div class="dontred">
                </div>
                <label class="listfont">
                    Use the same books as last year.</label></div>
            <div class="Div_FullWidth">
                <span class="hypenspan">- </span>
                <p>
                    You are able to modify your book selection for the first 30 days of your
                </p>
                <p>
                    subscription.</p>
            </div>
        </div>
        <%--<div class="lists" id="GrpsDelDiv">
            <div class="dontred">
            </div>
            <label class="listfont">
                All groups and classes.
            </label>
            <p>
            </p>
        </div>--%>
        <div class="buttons">
            <asp:Button Text="<< Back" CssClass="greybtnUpgrade" runat="server" ClientIDMode="Static"
                OnClick="BackBtn_Click" />
            <asp:Button ID="FinishBtn" Text="Finish" CssClass="greenBtnUpgrade" runat="server"
                ClientIDMode="Static" OnClick="FinishStepsBtn_Click" />
        </div>
    </div>
</div>
<div id="popupcontentUpg" style="display: none;">
    <h3>
        Students</h3>
    <p>
        <strong>Would you like us to move ALL students on this subscription up one year level?</strong></p>
    <p style="font-size: 12px !important; color: #555  !important; font-weight: normal !important;">
        This can save you time by removing the need to manually update each student individually.</p>
    <div class="buttons">
        <input type="button" id="NoButton" value="NO, LEAVE AS IS" class="darkgreyBtn " />
        <input type="button" id="YesButton" value="YES, UPDATE" class="greenBtnUpgrade " />
    </div>
</div>
<asp:HiddenField ID="upgradeFlaghdn" runat="server" Value="" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="renewupgradehdn1" ClientIDMode="Static" Value="0" />
<script src="desktopmodules/ecollection_dashboards/Scripts/UpgradeStep2.js" type="text/javascript"></script>
