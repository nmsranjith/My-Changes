<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SessionDashBoardMenu.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Sessions.Dashboard.SessionDashBoardMenu" %>
<style type="text/css">
     .k-window-action
    {
        background-image: url('../Portals/0/images/close.png') !important;
        opacity: 1 !important;
        height: 26px !important;
        width: 26px !important;
        z-index: 1003 !important;
        margin-right: -32px !important;
        margin-top: 12px !important;
        border: none !important;
        margin-left: -10px;
    }
input[type=submit][disabled]
{
    cursor:default; 
}
</style>
<div class="Div_FullWidth">
    <center>
    <br />
       <%-- <div class="ActiveAddButtonsHolder">
        <asp:Button ID="StartReadingSessionButton" runat="server"  clientIdMode="static" Text="CREATE A READING SESSION" CssClass="AddButton" onclick="CreateSessionButton_Click"/>
        </div>--%>
         <div class="eColNavigationLinkHdr">
                <asp:HyperLink ID="StartReadingSessionButton" runat="server" Text="CREATE A READING SESSION" ClientIDMode="Static"
                    CssClass="BtnStyle eColNavigationLink"/>
            </div>  
        <br />
        
        <div class="DisabledAddButtonHolder">
        <asp:Button ID="EndSessionButton" runat="server"  clientIdMode="static" 
            CssClass="DbldEndBtn" Enabled="false" OnClick="EndSessionButton_Click" Text="END READING SESSION"/>
        </div>
        <br />        
        <div class="DisabledDeleteButtonHolder">
        <asp:Button ID="DeleteSessionButtonNew" runat="server"  clientIdMode="static"  
            CssClass="DbldDelBtn" Enabled="false" OnClick="DeleteSessionButton_Click" Text="DELETE READING SESSION"/>
        </div>        
            <asp:HiddenField ID="selectedSessionID" ClientIDMode="Static" runat="server" />            
    </center>   
</div>

 <div id="Delete-message" style="display: none; background: white !important;">
        <div style="background-image: url('../Portals/0/images/topband.png'); background-color: White;
            height: 110px; margin-top: -9px; width: 102%; margin-left: -7px;">
            <span class="AfterRenewelHeaderSpan" style="margin-top: 36px;">Confirm End Session</span>
        </div>
        <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
            box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
            height: 87%;">
            <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
                box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
                -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
                <span  runat=server id="MessageLiteralEnd" style="font-family: Raleway-regular,Raleway, Arial, sans-serif; font-size: 10pt;
                    color: #707070; padding: 23px; float: left;"></span>
            </div>
            <div style="width: 92%;">
                <input type="button" id="YesButton" style="margin-left: 192px;" value="Yes" class="popupokbtn" />
                <input type="button" id="NoButton" style="margin-left: 18px;" value="No" class="popupokbtn" />
            </div>
        </div>
    </div>

    
 <div id="DeleteSession-message"  style="display: none; background: white !important;">
        <div style="background-image: url('../Portals/0/images/topband.png'); background-color: White;
            height: 110px; margin-top: -9px; width: 102%; margin-left: -7px;">
            <span class="AfterRenewelHeaderSpan" style="margin-top: 36px;">Confirm Delete</span>
        </div>
        <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
            box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
            height: 87%;">
            <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
                box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
                -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
                <span id="MessageLiteralDelete" runat=server style="font-family: Raleway-regular,Raleway, Arial, sans-serif; font-size: 10pt;
                    color: #707070; padding: 23px; float: left;"></span>
            </div>
            <div style="width: 92%;">
                <input type="button" id="YesDeleteButton" style="margin-left: 192px;" value="Yes" class="popupokbtn" />
                <input type="button" id="NoDeleteButton" style="margin-left: 18px;" value="No" class="popupokbtn" />
            </div>
        </div>
    </div>
<script language="javascript" type="text/javascript">
    var count = 0;
    function deletestudents() {
        alert('Student(s) Deleted Successfully');
    }

    var dkwindow;
    var deleteFlag = false;
    var ActiveChecked;
    $("#EndSessionButton").click(function () {
        ActiveChecked = false;
        for (var i = 0; i < $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
            if ($("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                ActiveChecked = true;                
            }
        }

        if (!ActiveChecked) {
            for (var i = 0; i < jQuery("#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                if (jQuery("#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    //deleteFlag = true;
                    return false;
                }
            }
        }


        if (!deleteFlag) {
            $("#Delete-message").css({ 'display': 'block' });
            $('.k-window-actions.k-header').css('cursor', 'pointer');
            dkwindow = $("#Delete-message"); //Give ur div id here
            if (!dkwindow.data("kendoWindow")) {
                dkwindow.kendoWindow({
                    width: "665px",
                    height: "300px",
                    modal: true,
                    draggable: false
                });
                dkwindow.data("kendoWindow").center();
            }
            dkwindow.data("kendoWindow").open();
            $(".k-icon.k-i-close").hide();
            $('a.k-window-action.k-link').mouseover(function () {
                $('a.k-window-action.k-link').parent().css("background-image", "url('../Portals/0/images/close.png') !important");
                return false;
            });
            return false;
        }
        return true;
    });
    $("#YesButton").click(function () {
        dkwindow.data("kendoWindow").close();
        document.getElementById("selectedSessionID").value = "";
        for (var i = 0; i < $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
            if ($("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                document.getElementById("selectedSessionID").value += $("#ClassRepeaterDiv #ClassRepeaterContentDiv #RepeaterFstCol span")[i].innerHTML.trim() + ",";                
            }
        }
        //        for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
        //            if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
        //                document.getElementById("selectedSessionID").value += $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
        //            }
        //        }
        deleteFlag = true;
        $("#EndSessionButton").click();
        return false;
    });
    $("#NoButton").click(function () {
        dkwindow.data("kendoWindow").close();
        return false;
    });

    var dkwindow1;
    var deleteFlag1;

    $("#DeleteSessionButtonNew").click(function () {
        if (!deleteFlag1) {
            $("#DeleteSession-message").css({ 'display': 'block' });
            $('.k-window-actions.k-header').css('cursor', 'pointer');
            dkwindow1 = $("#DeleteSession-message"); //Give ur div id here
            if (!dkwindow1.data("kendoWindow")) {
                dkwindow1.kendoWindow({
                    width: "665px",
                    height: "300px",
                    modal: true,
                    draggable: false
                });
                dkwindow1.data("kendoWindow").center();
            }
            dkwindow1.data("kendoWindow").open();
            $(".k-icon.k-i-close").hide();
            $('a.k-window-action.k-link').mouseover(function () {
                $('a.k-window-action.k-link').parent().css("background-image", "url('../Portals/0/images/close.png') !important");
                return false;
            });
            return false;
        }
        return true;
    });
    $("#YesDeleteButton").click(function () {
        dkwindow1.data("kendoWindow").close();
        document.getElementById("selectedSessionID").value = "";

        for (var i = 0; i < $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
            if ($("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                document.getElementById("selectedSessionID").value += $("#ClassRepeaterDiv #ClassRepeaterContentDiv #RepeaterFstCol span")[i].innerHTML.trim() + ",";
            }
        }
        for (var i = 0; i < $("#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
            if ($("#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                document.getElementById("selectedSessionID").value += $("#GroupsRepeaterDiv #GroupRepeaterContentDiv #RepeaterFstCol span")[i].innerHTML.trim() + ",";
            }
        }

        //        for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
        //            if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
        //                document.getElementById("selectedSessionID").value += $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
        //            }
        //        }

        deleteFlag1 = true;
        $("#DeleteSessionButtonNew").click();
        return false;
    });
    $("#NoDeleteButton").click(function () {
        dkwindow1.data("kendoWindow").close();
        return false;
    });



</script>
