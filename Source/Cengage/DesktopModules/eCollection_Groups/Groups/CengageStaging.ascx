<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CengageStaging.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.CengageStaging" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="uc1" %>
<style type="text/css">
    .CancelContentManagement
    {
        border: 0px solid white;
        float: left;
        background: transparent;
        font-size: 10pt;
        cursor: pointer;
        text-decoration: none;
        margin-top: 5px;
    }
    .bannersec
    {
        z-index: 1 !important;
    }
    .popupokbtn
    {
        font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
        font-weight: 700;
        font-size: 10pt;
        border-radius: 3px;
        color: White;
        width: 135px;
        height: 35px;
        margin-left: 250px;
        float: left;
        border: 1px solid #6EB8C6;
        background: -webkit-gradient(linear, left top, left bottom, from(#01B3D7), to(#008FAC));
        background: -moz-linear-gradient(center top , white 0%, #01B3D7 5%, #008FAC 130%) repeat scroll 0 0 transparent;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#01B3D7', endColorstr='#008FAC', gradientType='0');
        padding: 5px;
        cursor: pointer;
        background: -ms-linear-gradient(top, #01B3D7 5%, #008FAC 130%) !important;
    }
    
    .popupokbtn:hover
    {
        background: -moz-linear-gradient(center top , #0B82A5 5%, #0B82A5 1%, #15617B 130%) repeat scroll 0 0 transparent !important;
        background: -webkit-gradient(linear, left top, left bottom, from(#0B82A5), to(#15617B)) !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0B82A5', endColorstr='#15617B', gradientType='0') !important;
        -ms-filter: "progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr='#00000000', endColorstr='#ff000000')";
        background: -ms-linear-gradient(top, #0B82A5 5%, #15617B 130%) !important;
    }
    .deletestagebtn
    {
        background: -moz-linear-gradient(center top , white 0%, #D308AF 5%, #A8066A 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#D308AF), to(#A8066A));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#D308AF', endColorstr='#A8066A', gradientType='0');
        background: -ms-linear-gradient(top, #D308AF 5%, #A8066A 130%);
        text-shadow: 1px 2px 2px #722993;
        cursor: pointer;
        display: inline-block;
        width: 182px;
        height: 35px;
        color: White !important;
        float: left;
        border-radius: 2px;
        -khtml-border-radius: 3px;
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        border: 1px solid #A8066A;
        cursor: pointer;
        font-weight: 700;
    }
    .H4
    {
        font-family: Raleway,Arial !important;
        font-weight: 700 !important;
        font-size: 12pt !important;
    }
    #DeleteBtndiv input[type=submit][disabled]:hover
    {
        background: -moz-linear-gradient(center top , white 0%, #D308AF 5%, #A8066A 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#D308AF), to(#A8066A));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#D308AF', endColorstr='#A8066A', gradientType='0');
        background: -ms-linear-gradient(top, #D308AF 5%, #A8066A 130%);
        color: White;
    }
    .deletestagebtn:hover
    {
        background: -moz-linear-gradient(center top , white 0%, #A8066A 5%, #A8066A 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#A8066A), to(#A8066A));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#A8066A', endColorstr='#A8066A', gradientType='0');
        background: -ms-linear-gradient(top, #A8066A 5%, #A8066A 130%);
    }
    .takeLiveBtn
    {
        background: -moz-linear-gradient(top, #689A2E 1%, #74A738 32%, #74A738 43%, #5E872E 95%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#689A2E), color-stop(32%,#74A738), color-stop(43%,#74A738), color-stop(95%,#5E872E));
        background: -webkit-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -o-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -ms-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: linear-gradient(to bottom, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#689A2E', endColorstr='#5E872E',GradientType=0 );
        color: white !important;
        height: 35px;
        width: 100px;
        text-shadow: 1px 2px 2px rgb(103, 141, 26);
        border: 1px solid #5E872E;
        border-radius: 2px;
        -khtml-border-radius: 3px;
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        float: left;
        cursor: pointer;
        font-weight: 700;
    }
    .takeLiveBtn:hover
    {
        background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#537A37), color-stop(32%,#59833A), color-stop(43%,#557D38), color-stop(95%,#436A32));
        background: -webkit-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%);
        background: -o-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%);
        background: -ms-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%);
        background: linear-gradient(to bottom, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#537A37', endColorstr='#436A32',GradientType=0 );
    }
    #TakeLiveBtndiv input[type=submit][disabled]:hover
    {
        background: -moz-linear-gradient(top, #689A2E 1%, #74A738 32%, #74A738 43%, #5E872E 95%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#689A2E), color-stop(32%,#74A738), color-stop(43%,#74A738), color-stop(95%,#5E872E));
        background: -webkit-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -o-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -ms-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: linear-gradient(to bottom, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#689A2E', endColorstr='#5E872E',GradientType=0 );
    }
    
    .dnnFormSuccess
    {
        background-color: #73A638 !important;
        background-image: url(./portals/0/images/success_icon.png) !important;
        background-position: 35px center !important;
        border: 1px solid #639624 !important;
        font-size: 10pt !important;
        font-weight: bold !important;
        text-transform: none !important;
        text-shadow: none !important;
        color: white !important;
        margin-left: -3px !important;
        width: 60.6% !important; /* text-indent: 30px !important;*/
        margin-bottom: 8px !important;
    }
    .dnnFormWarning
    {
        border-color: #EE3873 !important;
        background-color: #ED165D !important;
        background-image: url(./portals/0/images/error_icon.png) !important;
        background-position: 25px center !important;
        color: white !important;
        background-repeat: no-repeat !important;
        font-size: 10pt !important;
        font-weight: bold !important;
        width: 60.4% !important; /* text-indent: 30px !important;*/
        margin-left: -4px !important;
        text-transform: none !important;
        text-shadow: none !important;
        margin-bottom: 8px !important;
    }
    
    
    .UploadBooksBtn:hover
    {
        background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#537A37), color-stop(32%,#59833A), color-stop(43%,#557D38), color-stop(95%,#436A32));
        background: -webkit-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%);
        background: -o-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%);
        background: -ms-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%);
        background: linear-gradient(to bottom, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#537A37', endColorstr='#436A32',GradientType=0 );
    }
    .UploadBooksBtn
    {
        background: -moz-linear-gradient(top, #689A2E 1%, #74A738 32%, #74A738 43%, #5E872E 95%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#689A2E), color-stop(32%,#74A738), color-stop(43%,#74A738), color-stop(95%,#5E872E));
        background: -webkit-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -o-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -ms-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: linear-gradient(to bottom, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#689A2E', endColorstr='#5E872E',GradientType=0 );
        color: white !important;
        height: 35px;
        width: 117px;
        text-shadow: 1px 2px 2px rgb(103, 141, 26);
        border: 1px solid #97af82;
        border-radius: 3px;
        -khtml-border-radius: 3px;
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        float: left;
        margin-left: 16px;
        cursor: pointer;
        font-weight: 700;
    }
    #uploaddiv input[type=submit][disabled]:hover
    {
        background: -moz-linear-gradient(top, #689A2E 1%, #74A738 32%, #74A738 43%, #5E872E 95%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#689A2E), color-stop(32%,#74A738), color-stop(43%,#74A738), color-stop(95%,#5E872E));
        background: -webkit-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -o-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -ms-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: linear-gradient(to bottom, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
    }
    
    .SelectedBook
    {
        border-color: lightgray;
        border-width: 0.1em;
        border-style: solid;
        float: left;
        margin-top: 10px;
        width: 96%;
        color: rgb(131, 130, 130);
        min-height: 34px;
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        border-radius: 3px;
        -khtml-border-radius: 3px;
        background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
        background: -ms-linear-gradient(top, white 5%, #EAE9E9 130%) !important;
    }
    .titleIsbnspan
    {
        margin-left: 10px;
        font-weight: bolder;
        font-size: 10pt;
        font-family: Raleway,Arial;
        padding: 7px;
        float: left;
        padding-right: 0px;
        width: 80%;
        margin-top: 4px;
    }
    .removeanchor
    {
        padding: 11px;
        font-weight: bolder;
        font-size: 1em;
        font-family: Arial;
        color: #707070;
        cursor: pointer;
        text-align: right;
        float: right;
    }
    #SelectedBook
    {
        list-style-type: none;
        margin: 0;
        padding: 0;
    }
    #SelectedBook li
    {
        display: inline;
    }
    .sort
    {
        transform: rotate(180deg);
        -moz-transform: rotate(180deg); /* Firefox */
        -webkit-transform: rotate(180deg); /* Safari and Chrome */
        -o-transform: rotate(180deg);
        background-position: 20% 69% !important;
        filter: progid:DXImageTransform.Microsoft.Matrix(sizingMethod='auto expand', M11=-1, M12=0, M21=0, M22=-1); /* IE6,IE7 */
        -ms-filter: progid:DXImageTransform.Microsoft.Matrix(M11 = -1, M12 = 0, M21 = 0, M22 = -1,SizingMethod = 'auto expand'); /*IE8+*/
    }
    .stagingRptr
    {
        border-color: lightgray;
        border-width: 0.1em;
        border-style: solid;
        float: left;
        margin-top: 10px;
        width: 100%;
        color: rgb(131, 130, 130);
        height: 45px;
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        border-radius: 3px;
        -khtml-border-radius: 3px;
        background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
        background: -ms-linear-gradient(top, white 5%, #EAE9E9 130%);
        line-height: 42px;
        font-weight: 700;
        font-size: 10.5pt;
        font-family: Raleway, Arial;
    }
    .rowclick
    {
        background: #E5F6FF;
    }
    #TakeLiveBtndiv input[type=submit][disabled], #DeleteBtndiv input[type=submit][disabled]
    {
        cursor: default;
        background: transparent !important;
        color: #A5A3A3;
        border: 1px solid lightgray;
        color: #707070 !important;
        text-shadow: none !important;
        -moz-opacity: 0.5;
        opacity: 0.5;
        filter: none;
    }
    /*#TakeLiveBtndiv input[type=submit][disabled]:hover
    {
        cursor: default;
        background: transparent !important;
        color: #A5A3A3;
        border: 1px solid lightgray;
        color: #707070 !important;
        text-shadow: none !important;
        -moz-opacity: 0.5;
        opacity: 0.5;
        filter: none !important;
    }*/
    .LeftLineDiv
    {
        width: 87%;
        float: left;
        margin-left: 39px;
        border-left: 1px solid lightgray;
        min-height: 18px;
        margin-top: -1px;
    }
    .SearchDiv
    {
        width: 618px;
        float: left;
        margin-top: -4px;
        background-color: #EEEEEE;
        border: 1px solid #CCCCCC;
        height: 34px;
        position: relative;
    }
    .SearchInnerDiv
    {
        width: 618px;
        float: left;
        border: 0px solid white;
        height: 34px;
    }
    .Searchbtndiv
    {
        position: relative;
        cursor: pointer;
        border-right: 0px solid white;
        border-left: 1px solid #CCCCCC !important;
        width: 34px !important;
        height: 34px !important;
        float: right !important;
        background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
        background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%);
    }
    .Searchbtndiv:hover
    {
        background: -moz-linear-gradient(center top , #a0a1a1 0%, #a0a1a1 55%, #858685 130%) repeat scroll 0 0 transparent !important;
        background: -webkit-gradient(linear, center top, center bottom, color-stop(55%,#a0a1a1), color-stop(100%,#858685)) !important;
        background: -webkit-linear-gradient(center top, #a0a1a1 55%, #858685 100%) !important;
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#858685', endColorstr='#a0a1a1', GradientType=1) !important;
        background: -ms-filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#858685', endColorstr='#a0a1a1', GradientType=1) !important;
        background: -ms-linear-gradient(left, #858685 55%, #DBD9D9 130%) !important;
    }
    .classSearchwater
    {
        width: 560px !important;
        height: 29px !important;
        background: transparent !important;
        padding-left: 7px !important;
        padding-top: 1px !important;
        box-shadow: none !important;
        border: none !important;
        font-size: 10pt !important;
        color: Gray !important;
    }
    .classSearchwater:hover
    {
        border: none !important;
        height: 29px !important;
        padding-left: 7px !important;
        padding-top: 1px !important;
        background: transparent !important;
        width: 560px !important;
        color: Gray !important;
    }
    #SearchTextBox-list
    {
        width: 579px !important;
        margin-left: -1px !important;
        margin-top: 6px !important;
        box-shadow: none !important;
        -webkit-box-shadow: none !important;
    }
    #SearchTextBox-list .k-state-hover, #SearchTextBox-list .k-state-focused
    {
        background: #707070 !important;
        border: 1px solid #707070 !important;
        color: white !important;
    }
    .SearchInnerDiv ::-webkit-input-placeholder
    {
        font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
        font-size: 10pt;
        color: #707070;
    }
    .SearchInnerDiv ::-moz-placeholder
    {
        font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
        opacity: 1;
        font-size: 10pt;
        color: #707070;
    }
    /* firefox 19+ */
    input:-moz-placeholder
    {
        font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
        font-size: 10pt;
        color: #707070;
        opacity: 1;
    }
</style>
<div id="PMeBookContentMgt" class="PMeBkCntMgt">
    <div id="Firstdiv" class="eBkcntmgtFirtstDiv">
        <asp:Button ID="DownloadLogfile" runat="server" CssClass="ActiveAddButtonsHolder eBkcntmgtdwnldlogbtn"
            Text="DOWNLOAD LOG FILES" OnClick="DownloadLogfile_Click" />
        <div id="MessagesDiv" class="Div_FullWidth">
            <asp:UpdatePanel ID="MessageUpdatePanel" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <uc1:Messages ID="Messages" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="Messages_warning" runat="server" clientidmode="Static" style="display: none;"
                class="dnnFormMessage dnnFormWarning">
                <div class="dnnMsgHolder">
                    <span id="messagespan"></span>
                </div>
            </div>
        </div>
        <div class="eBkcntmgtuploaddiv">
            <h3 style="" class="eBkcntmgtuploadh3">
                Upload
            </h3>
            <div class="BulkUpload_BrowseDiv eBkcntmgtbrowsediv">
                <input id="txtAttachment" runat="server" type="text" clientidmode="Static" class="eBkcntmgttxtattch"
                    maxlength="250" onclick="return false" readonly="readonly" ondblclick="BrowseFile();" />
                <input type="button" id="AttachFileButton" class="ActiveAddButtonsHolder" class="eBkcntmgtbwsbtn"
                    onclick="BrowseFile();" value="BROWSE" />
                <asp:FileUpload ID="AttachAFile" runat="server" UseSubmitBehavior="false" onchange="SetValue(this.value)"
                    ClientIDMode="Static" CssClass="eBkcntmgtFileUpload" />
            </div>
            <div class="eBkcntmgtuploadbtndiv">
                <div id="uploaddiv" class="eBkcntmgtuploaddiv">
                    <asp:Button Text="UPLOAD" CssClass="UploadBooksBtn eBkcntmgtuploadbtn" ID="UploadBooksButton"
                        runat="server" ClientIDMode="Static" OnClick="UploadBooksButton_Click" UseSubmitBehavior="false" />
                </div>
                <div class="eBkcntmgtcnclbtndiv">
                    <asp:HyperLink ID="CancelButton" runat="server" Style="color: #1FB5E7;" CssClass="CancelContentManagement"
                        Text="Cancel"></asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="eBkcntmgtuploadins">
            <h4 class="H4">
                How to upload books:</h4>
            <p class="eBkcntmgtuploadinspg">
                Click on the Browse button. Now navigate to where you have stored the file, select
                and open it. Click the Upload button to upload the file to the staging area.
            </p>
        </div>
    </div>
    <hr class="eCollection_Menu_Mid_hr eBkcntmgtMidhr" />
    <div id="SecondDiv" class="eBkcntmgtsnddiv">
        <div class="eBkcntmgtinnerdiv">
            <div class="eBkcntmgtinnersnddiv">
                <div class="eBkcntmgtinnerthirddiv">
                    <div class="eBkcntmgtchkbxdiv">
                        <img id="CategoriesCheckBoxImg" onclick="javascript:checkAll(this);" clientidmode="Static"
                            class="eBkcntmgtchkbximg" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                    </div>
                    <input type="checkbox" id="SelectAllCheckBox" class="eBkcntmgtchkbx" />
                    <span class="eBkcntmgtchkbxspan" onclick="javascript:checkAll(CategoriesCheckBoxImg);">
                        SELECT ALL</span>
                </div>
                <div class="eBkcntmgtviewallchkbxdiv">
                    <div class="eBkcntmgtviewallchkbxinnerdiv">
                        <img id="ViewAllCheckImg" alt="uncheck" clientidmode="Static" onclick="javascript:viewAll(this);"
                            src="<%=Page.ResolveUrl("Portals/0/images/uncheck.png")%>" />
                    </div>
                    <asp:CheckBox ID="ViewLiveAllCheckbox" ClientIDMode="Static" runat="server" CssClass="eBkcntmgtchkbx"
                        AutoPostBack="true" OnCheckedChanged="ViewLiveAllCheckbox_CheckedChanged" />
                    <span class="eBkcntmgtviewallchkbxspan" onclick="javascript:viewAll(ViewAllCheckImg);">
                        Display only live files</span>
                </div>
                <asp:Button ID="NewBooksSortButton" Text="NEWEST" CssClass="NewBooksSortBtn" ClientIDMode="Static"
                    runat="server" OnClick="NewBooksSortButton_Click" UseSubmitBehavior="false" OnClientClick="javascript:Sort(this)"
                    CommandName="Ascending" />
            </div>
            <div class="LeftLineDiv">
            </div>
            <div class="SearchDiv">
                <div class="SearchInnerDiv">
                    <input type="text" id="SearchTextBox" class="classSearchwater" clientidmode="Static"
                        autocomplete="off" spellcheck="false" runat="server" title="Enter your search here ..." />
                    <div class="Searchbtndiv" id="SrchDiv">
                        <asp:Button ID="SearchButton" runat="server" ClientIDMode="Static" CssClass="SearchButton"
                            OnClick="SearchButton_Click" />
                    </div>
                </div>
            </div>
            <div class="LeftSecondLineDiv">
            </div>
            <asp:UpdatePanel ID="StagingUpdatePanel" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="NewBooksSortButton" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ViewLiveAllCheckbox" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="SearchButton" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div>
                        <div class="eBkcntmgtebkslistdiv">
                            <div class="eBkcntmgtinnerdiv">
                                <asp:Repeater ID="StagingRepeater" runat="server">
                                    <ItemTemplate>
                                        <div id="StagingRepeater" class="stagingRptr">
                                            <img id="Book2CheckBoxImg" alt="uncheck" onclick="Clicked(this)" clientidmode="Static"
                                                src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" class="eBkcntmgtbkslistchkimg" />
                                            <input type="checkbox" runat="server" id="StagingCheckBox" checked='<%# Eval("checked") %>'
                                                class="eBkcntmgtbkslistchkbx" />
                                            <span class="eBkcntmgtbkslistspan">[<%# Eval("ISBN")%>]</span>
                                            [Ver
                                            <%# Eval("Version").ToString()%>] <span class="eBkcntmgtbkslistversionname">
                                                <%# Eval("Server")%></span>
                                            <asp:Label ID="TitleLabel" runat="server" Style="display: none" Text='<%# Eval("Title")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="eBkcntmgtselBkdiv">
                            <div style="Div_fullWidth">
                                <ul id="SelectedBook" runat="server" class="eBkcntmgtselBkUl" clientidmode="Static">
                                </ul>
                                <asp:HiddenField ID="IsbnIdHidden" ClientIDMode="Static" runat="server" />
                            </div>
                            <div style="Div_fullWidth">
                                <div id="DeleteBtndiv" class="eBkcntmgtdelbtndiv">
                                    <asp:Button ID="DeleteFromStagingButton" runat="server" ClientIDMode="Static" CssClass="deletestagebtn eBkcntmgtdelbtn" Enabled="false" Text="DELETE FROM STAGING"
                                        OnClick="DeleteFromStagingButton_Click" />
                                </div>
                                <div id="TakeLiveBtndiv" class="eBkcntmgtTklivbtndiv">
                                    <asp:Button ID="TakeLiveButton" runat="server" ClientIDMode="Static" CssClass="takeLiveBtn eBkcntmgtTklivbtn"
                                        Enabled="false" Text="TAKE LIVE" OnClick="TakeLiveButton_Click" />
                                </div>
                            </div>
                            <div id="dialogmessage" title="Alert message!" class="eBkcntmgtdigmsgdiv">
                                <div class="eBkcntmgthdrbg">
                                    <span class="AfterRenewelHeaderSpan eBkcntmgthdrspan" id="AlertHeaderMessage" runat="server" clientidmode="Static">Alert Message</span>
                                </div>
                                <div class="eBkcntmgtPPMsgdiv">
                                    <div class="eBkcntmgtPPMsginnerdiv">
                                        <asp:Label ID="AlertmessageLabel" ClientIDMode="Static" runat="server" CssClass="eBkcntmgtalertmsg"></asp:Label>
                                    </div>
                                    <div style="width: 92%;">
                                        <input type="button" id="YesButton" onclick="javascript:YesbtnClick();"
                                            value="Yes" class="popupokbtn eBkcntmgtPPokbtn" />
                                        <input type="button" id="NoButton" onclick="javascript:NobtnClick();"
                                            value="No" class="popupokbtn eBkcntmgtPPcnclbtn" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<script type="text/javascript">
    var checkallFlag = true;
    var deleteFlag = false;
    $(document).ready(function () {
        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
        // This will call the function after postback..
    });

    var kwindow;
    function SetPopUpFlag() {
        $('.k-window-actions.k-header').css('cursor', 'pointer');
        kwindow = $("#dialogmessage"); //Give ur div id here
        if (!kwindow.data("kendoWindow")) {
            kwindow.kendoWindow({
                width: "665px",
                height: "300px",
                modal: true,
                draggable: false
            });
            kwindow.data("kendoWindow").center();
        }
        kwindow.data("kendoWindow").open();
        $(".k-icon.k-i-close").hide();
        $('a.k-window-action.k-link').mouseover(function () {
            $('a.k-window-action.k-link').parent().css("background-image", "url('Portals/0/images/close.png') !important");
            return false;
        });
        $('a.k-window-action.k-link').attr("onclick", "javascript:NobtnClick()");
    }
    var checkallCount = 0;
    var viewAllCount = 0;
    function viewAll(e) {
        document.getElementById('ViewLiveAllCheckbox').click();
        if (document.getElementById('ViewLiveAllCheckbox').checked) {
            e.src = GetFile('/Portals/0/images/check.png');
            viewAllCount = checkallCount;
            checkallCount = 0;
        }
        else {
            e.src = GetFile('/Portals/0/images/uncheck.png');
            checkallCount = viewAllCount;
        }
    }
    function Sort(e) {
        if (e.className.trim() == 'NewBooksSortBtn') {
            e.className = 'NewBookUpSort'
        }
        else {
            e.className = 'NewBooksSortBtn'
        }
    }
    var selectableCheckbox = 0;
    function checkAll(e) {
        var readyToDelorPub = 0;
        if (checkallFlag) {
            checkallCount = selectableCheckbox;
            e.src = GetFile("/Portals/0/images/tick_student.png");
            checkallFlag = false;
            for (var i = 0; i < $("#StagingRepeater input[type=checkbox]").length; i++) {
                if (!$("#StagingRepeater input[type=checkbox]")[i].checked) {
                    if ($("#StagingRepeater img")[i].parentNode.children[3].innerHTML.trim() == "") {
                        $("#StagingRepeater input[type=checkbox]")[i].click();
                        $("#StagingRepeater img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        $("#StagingRepeater img")[i].parentNode.className = "rowclick stagingRptr";
                        readyToDelorPub++;
                        var exist = false;
                        for (var k = 0; k < $("#SelectedBook")[0].childNodes.length; k++) {
                            var tis = $("#SelectedBook")[0].childNodes[k];
                            if (tis.nodeName != "#text") {
                                if ($("#SelectedBook")[0].childNodes[k].childNodes[1].innerHTML.trim() == $("#StagingRepeater img")[i].parentNode.children[2].innerHTML.replace(/\[|\]/g, "").trim()) {
                                    exist = true;
                                }
                            }
                            else {
                                if ($("#SelectedBook")[0].childNodes.length == 1) {
                                    $("<li class=\'SelectedBook\'><span class=\'titleIsbnspan\'>[" + $("#StagingRepeater img")[i].parentNode.children[4].innerHTML.trim() + "] " + $("#StagingRepeater img")[i].parentNode.children[2].innerHTML.trim() + "</span><span style='display:none'>" + $("#StagingRepeater img")[i].parentNode.children[2].innerHTML.replace(/\[|\]/g, "").trim() + "</span><a class=\'removeanchor\' onclick='javascript:Remove(this)'>x</a></li>").appendTo("#SelectedBook");
                                    $("#IsbnIdHidden").val($("#IsbnIdHidden").val().trim() + $("#StagingRepeater img")[i].parentNode.children[2].innerHTML.replace(/\[|\]/g, "").trim() + ",");
                                }
                            }
                        }
                        if (!exist) {
                            $("<li class=\'SelectedBook\'><span class=\'titleIsbnspan\'>[" + $("#StagingRepeater img")[i].parentNode.children[4].innerHTML.trim() + "] " + $("#StagingRepeater img")[i].parentNode.children[2].innerHTML.trim() + "</span><span style='display:none'>" + $("#StagingRepeater img")[i].parentNode.children[2].innerHTML.replace(/\[|\]/g, "").trim() + "</span><a class=\'removeanchor\' onclick='javascript:Remove(this)'>x</a></li>").appendTo("#SelectedBook");
                            $("#IsbnIdHidden").val($("#IsbnIdHidden").val().trim() + $("#StagingRepeater img")[i].parentNode.children[2].innerHTML.replace(/\[|\]/g, "").trim() + ",");
                        }
                    }
                }
            }
        }
        else {
            checkallCount = 0;
            readyToDelorPub = 0;
            checkallFlag = true;
            e.src = GetFile("/Portals/0/images/circle_big.png");
            for (var i = 0; i < $("#StagingRepeater input[type=checkbox]").length; i++) {
                if ($("#StagingRepeater input[type=checkbox]")[i].checked) {
                    $("#StagingRepeater input[type=checkbox]")[i].click();
                    $("#StagingRepeater img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                    $("#StagingRepeater img")[i].parentNode.className = "stagingRptr";
                    var selectedId = $("#StagingRepeater img")[i].parentNode.children[2].innerHTML.replace(/\[|\]/g, "").trim();
                    $('#SelectedBook li').map(function (k, n) {
                        if ($(n)[0].children[1].innerHTML == selectedId) {
                            $(n)[0].parentNode.removeChild($(n)[0]);
                        }
                    });
                    var SelectedValues = $("#IsbnIdHidden").val().trim().split(',');
                    document.getElementById("IsbnIdHidden").value = " ";
                    for (var j = 0; j < (SelectedValues.length - 1); j++) {
                        if (SelectedValues[j].trim() != selectedId && SelectedValues[j].trim() != '') {
                            $("#IsbnIdHidden").val($("#IsbnIdHidden").val().trim() + SelectedValues[j].trim() + ",")
                        }
                    }
                }
            }


        }
        if (readyToDelorPub > 0) {
            $("#TakeLiveButton").removeAttr("disabled");
            $("#DeleteFromStagingButton").removeAttr("disabled");
        }
        else {
            $("#TakeLiveButton").attr("disabled", "disabled");
            $("#DeleteFromStagingButton").attr("disabled", "disabled");
        }
    }
    function Clicked(checkbox) {
        if ($($(checkbox)).next().is(":checked") == false && $($(checkbox)).parent().children()[3].innerHTML.trim() == "") {
            $($(checkbox)).attr('src', GetFile("/Portals/0/images/tick_student.png"));
            $($(checkbox)).parent().attr("class", "rowclick stagingRptr");
            $($(checkbox)).next().attr("checked", "checked");
            if (checkallCount < selectableCheckbox) {
                checkallCount++;
                if (checkallCount == selectableCheckbox) {
                    checkallFlag = false;
                    $('#CategoriesCheckBoxImg').attr('src', GetFile("/Portals/0/images/tick_student.png"));
                }
                else
                    checkallFlag = true;
            }
            var deleteenable = true;
            $("<li class=\'SelectedBook\'><span class=\'titleIsbnspan\'>[" + $($(checkbox)).parent().children()[4].innerHTML.trim() + "] " + $($(checkbox)).parent().children()[2].innerHTML.trim() + "</span><span style='display:none'>" + $($(checkbox)).parent().children()[2].innerHTML.replace(/\[|\]/g, "").trim() + "</span><a class=\'removeanchor\' onclick='javascript:Remove(this)'>x</a></li>").appendTo("#SelectedBook");
            $("#IsbnIdHidden").val($("#IsbnIdHidden").val().trim() + $($(checkbox)).parent().children()[2].innerHTML.replace(/\[|\]/g, "").trim() + ",");
            var count = 0;
            for (var i = 0; i < $("#StagingRepeater input[type=checkbox]").length; i++) {
                if ($("#StagingRepeater input[type=checkbox]")[i].checked) {
                    if ($("#StagingRepeater img")[i].parentNode.children[3].innerHTML.trim() != "") {
                        deleteenable = false;
                    }
                    count++;
                }
            }
        }
        else {
            $($(checkbox)).next().removeAttr("checked");
            $($(checkbox)).attr('src', GetFile("/Portals/0/images/circle_big.png"));
            $($(checkbox)).parent().attr("class", "stagingRptr");
            if (checkallCount > 0) {
                checkallCount--;
                $('#CategoriesCheckBoxImg').attr('src', GetFile("/Portals/0/images/circle_big.png"));
                checkallFlag = true;
            }
            else
                checkallFlag = false;

            var undeleteenable = true;
            checkallFlag = true;
            var selectedId = $($(checkbox)).parent().children()[2].innerHTML.replace(/\[|\]/g, "").trim();
            $('#SelectedBook li').map(function (k, n) {
                if ($(n)[0].children[1].innerHTML == selectedId) {
                    $(n)[0].parentNode.removeChild($(n)[0]);
                }
            });
            var SelectedValues = $("#IsbnIdHidden").val().trim().split(',');
            document.getElementById("IsbnIdHidden").value = " ";
            for (var j = 0; j < (SelectedValues.length - 1); j++) {
                if (SelectedValues[j].trim() != selectedId && SelectedValues[j].trim() != '') {
                    $("#IsbnIdHidden").val($("#IsbnIdHidden").val().trim() + SelectedValues[j].trim() + ",")
                }
            }
            for (var k = 0; k < $("#StagingRepeater input[type=checkbox]").length; k++) {
                if ($("#StagingRepeater input[type=checkbox]")[k].checked) {
                    if ($("#StagingRepeater img")[k].parentNode.children[3].innerHTML.trim() != "")
                        undeleteenable = false;
                }
            }
        }
        EnableLivedeleteButtons();
    }
    function EnableLivedeleteButtons() {
        if (checkallCount <= 0) {
            $("#TakeLiveButton").attr("disabled", "disabled");
            $("#DeleteFromStagingButton").attr("disabled", "disabled");
        }
        else {
            $("#TakeLiveButton").removeAttr("disabled");
            $("#DeleteFromStagingButton").removeAttr("disabled");
        }
    }
    function PostBack() {
        if ($('#<%=StagingUpdatePanel.ClientID%>')[0].children[0].children[0].children[0].children.length == 0) {
            $('#<%=StagingUpdatePanel.ClientID%>')[0].children[0].children[0].children[0].style.display = "none";
        }
        else {
            $('#<%=StagingUpdatePanel.ClientID%>')[0].children[0].children[0].children[0].style.display = "block";
        }
        var searchAutoComplete = $("#SearchTextBox").data("kendoAutoComplete");
        if (searchAutoComplete == undefined) {
            $("#SearchTextBox").kendoAutoComplete({
                dataSource: {
                    transport: {
                        read: {
                            url: GetFile('/DesktopModules/eCollection_Groups/GroupsHandler.ashx?Search=isbnSearch'),
                            type: "GET",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json"
                        }
                    }
                },
                filter: "contains",
                separator: ", ",
                minLength: 1,
                placeholder: "Enter your search here ..."
            });
        }
        $('#SearchTextBox').keypress(function (e) {
            $('#SearchTextBox').focus();
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            var code = (e.keyCode ? e.keyCode : e.which);
            if ($("#SearchTextBox").length > 0 && $(this).val() != this.title) {
                $('#<%=SearchButton.ClientID%>').removeAttr("disabled");

                if (code == 13) {
                    e.preventDefault();
                    $('#<%=SearchButton.ClientID%>').click();
                }
            }
            else {
                if (code == 13) {
                    e.preventDefault();
                }
                $('#<%=SearchButton.ClientID%>').attr("disabled", "disabled");
            }
        });

        $('#SearchTextBox').keyup(function (e) {
            //$('#SearchTextBox').focus();
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            var code = (e.keyCode ? e.keyCode : e.which);
            if ($("#SearchTextBox").length > 0 && $(this).val() != this.title) {
                $('#<%=SearchButton.ClientID%>').removeAttr("disabled");

                if (code == 13) {
                    e.preventDefault();
                    $('#<%=SearchButton.ClientID%>').click();
                }
            }
            else {
                if (code == 13) {
                    e.preventDefault();
                }
                $('#<%=SearchButton.ClientID%>').attr("disabled", "disabled");
            }
        });
        $("#SearchTextBox").focus(function () {
            if ($(this).val() == this.title) {
                // $(this).val("");
            }
            if ($(this).val().length > 0 && $(this).val() != this.title) {
                $('#<%=SearchButton.ClientID%>').removeAttr("disabled");
            }
            else {
                $('#<%=SearchButton.ClientID%>').attr("disabled", "disabled");
            }
        });
        $("#SearchTextBox").blur(function () {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                // this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            if ($(this).val().trim() == "") {
                // $(this).val(this.title);
            }
            if ($(this).val().length > 0 && $(this).val() != this.title) {
                $('#<%=SearchButton.ClientID%>').removeAttr("disabled");
            }
            else {
                $('#<%=SearchButton.ClientID%>').attr("disabled", "disabled");
            }
        });
        $('#<%=SearchButton.ClientID%>').click(function () {
            checkallCount = 0;
            EnableLivedeleteButtons();
        });

        EnableLivedeleteButtons();

        $('#eCollectionContent').height(($('#PMeBookContentMgt').height() + 50) + 'px');

        $(".ViewBtnHeader").css({ "display": "none" });
        //For Temp Purpose
        $('#eCollectionContent').css({ "width": "950px", "margin": "-14px 0px 0px 12px" });
        $('#eCollectionMenu').css({ "width": "0px" });

        checkallCount = 0;
        selectableCheckbox = 0;
        for (var i = 0; i < $("#StagingRepeater input[type=checkbox]").length; i++) {
            if ($("#StagingRepeater input[type=checkbox]")[i].checked && $("#StagingRepeater input[type=checkbox]")[i].parentNode.children[3].innerHTML.trim() == "") {
                $("#StagingRepeater img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                $("#StagingRepeater img")[i].parentNode.className = "rowclick stagingRptr";
                checkallCount++;
            }
        }

        for (var i = 0; i < $("#StagingRepeater input[type=checkbox]").length; i++) {
            if ($("#StagingRepeater input[type=checkbox]")[i].parentNode.children[3].innerHTML.trim() == "") {
                selectableCheckbox++;
            }
            else if ($("#StagingRepeater input[type=checkbox]")[i].parentNode.children[3].innerHTML.trim() != "") {
                $("#StagingRepeater img")[i].style.opacity = 0;
                $("#StagingRepeater img")[i].style.width = "0px";
                $("#StagingRepeater img")[i].style.padding = "10px";
                $("#StagingRepeater img")[i].style.cursor = "inherit";
            }
        }
        if (checkallCount) {
            $("#TakeLiveButton").removeAttr("disabled");
            $("#DeleteFromStagingButton").removeAttr("disabled");
        }
        if (checkallCount == selectableCheckbox && checkallCount != 0) {
            checkallFlag = false;
            $("#CategoriesCheckBoxImg")[0].src = GetFile("/Portals/0/images/tick_student.png");
        }
        else {
            checkallFlag = true;
            $("#CategoriesCheckBoxImg")[0].src = GetFile("/Portals/0/images/circle_big.png");
        }

        $("#DeleteFromStagingButton").click(function () {
            if (!deleteFlag) {
                $("#AlertHeaderMessage")[0].innerHTML = "Confirm Delete";
                $("#AlertmessageLabel")[0].innerHTML = "Do you want delete ISBN/S from staging?";
                deleteFlag = true;
                SetPopUpFlag();
                return false;
            }
            deleteFlag = false;
            return true;
        });
    }

    function YesbtnClick() {
        kwindow.data("kendoWindow").close();
        if (deleteFlag) {
            $("#DeleteFromStagingButton").click();
            return false;
        }

        if (document.getElementById("IsbnIdHidden").value == "Upload") {
            document.getElementById("IsbnIdHidden").value = true;
            $("#UploadBooksButton").click();
        }
        else {

            document.getElementById("IsbnIdHidden").value = true;
            $("#TakeLiveButton").removeAttr("disabled");
            $("#TakeLiveButton").click();
        }
        return false;
    }

    function NobtnClick() {
        kwindow.data("kendoWindow").close();
        if (deleteFlag) {
            deleteFlag = false;
            return false;
        }
        if (document.getElementById("IsbnIdHidden").value == "Upload") {
            document.getElementById("IsbnIdHidden").value = false;
            $("#UploadBooksButton").click();
        }
        else {

            document.getElementById("IsbnIdHidden").value = false;
            $("#TakeLiveButton").removeAttr("disabled");
            $("#TakeLiveButton").click();
        }
        return false;
    }
    function Remove(obj) {
        var SelectedValues = $("#IsbnIdHidden").val().trim().split(',');
        document.getElementById("IsbnIdHidden").value = " ";
        for (var i = 0; i < (SelectedValues.length - 1); i++) {
            if (SelectedValues[i].trim() != obj.parentNode.children[1].innerHTML.replace(/\[|\]/g, "").trim() && SelectedValues[i].trim() != '') {
                $("#IsbnIdHidden").val($("#IsbnIdHidden").val().trim() + SelectedValues[i].trim() + ",")
            }
        }
        obj.parentNode.parentNode.removeChild(obj.parentNode);
        //for(var i=0;i<)
        checkallFlag = true;
        var enableDeletenTakeLive = true
        $("#CategoriesCheckBoxImg")[0].src = GetFile("/Portals/0/images/circle_big.png");
        $("#StagingRepeater img").each(function (i, n) {
            if ($(n)[0].parentNode.children[2].innerHTML.replace(/\[|\]/g, "").trim() == obj.parentNode.children[1].innerHTML.replace(/\[|\]/g, "").trim()) {
                $(n)[0].parentNode.children[0].src = GetFile("/Portals/0/images/circle_big.png");
                $(n)[0].parentNode.children[1].checked = false;
                $(n)[0].parentNode.className = "stagingRptr";
            }
            if ($(n)[0].parentNode.children[1].checked) {
                if ($(n)[0].parentNode.children[3].innerHTML.trim() != "") {
                    enableDeletenTakeLive = false;
                }
            }
        });
        if (enableDeletenTakeLive) {
            if ($("#IsbnIdHidden").val().trim().split(',').length > 1) {
                $("#TakeLiveButton").removeAttr("disabled");
                $("#DeleteFromStagingButton").removeAttr("disabled");
            }
            else {
                $("#TakeLiveButton").attr("disabled", "disabled");
                $("#DeleteFromStagingButton").attr("disabled", "disabled");
            }
        }
        else {
            $("#TakeLiveButton").attr("disabled", "disabled");
            $("#DeleteFromStagingButton").attr("disabled", "disabled");
        }
        checkallCount--;
    }
    function ViewAllimageclick(e) {
        if (e.alt == "uncheck") {
            e.src = GetFile("/Portals/0/images/check.png");
            e.alt = "check";
        }
        else {
            e.src = GetFile("/Portals/0/images/circle_big.png");
            e.alt = "uncheck";
        }
    }
    function Reset() {
        jQuery('#Cancel').click();
    }

    function BrowseFile() {
        document.getElementById('AttachAFile').click();

    }
    function SetValue(Uploadingfile) {
        document.getElementById('txtAttachment').value = Uploadingfile;
        //        var ext = $('#txtAttachment').val().split('.').pop().toLowerCase();
        //        if ($.inArray(ext, ['zip']) != -1) {
        //            $("#UploadBooksButton").removeAttr("disabled");
        //        }
        //        else {
        //            $("#UploadBooksButton").attr("disabled", "disabled");
        //        }
    }
</script>
