<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SessionProfileDashBoardMenu.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Sessions.Dashboard.SessionProfileDashBoardMenu" %>
<style type="text/css">
input[type=submit][disabled]
{
    cursor:default; 
}
</style>
<div class="Div_FullWidth">
    <center>
    <br /><%--<div class="ActiveAddButtonsHolder">
        <asp:Button ID="EditSessionButton" runat="server"  clientIdMode="static" 
            Text="EDIT SESSION" CssClass="AddButton" onclick="EditSessionButton_Click" />
            </div>--%>
            <div class="eColNavigationLinkHdr">
                <asp:HyperLink ID="EditSessionButton" runat="server" Text="EDIT SESSION" ClientIDMode="Static"
                    CssClass="BtnStyle eColNavigationLink"/>
            </div>  
        <br />
        <div class="ActiveAddButtonsHolder">
        <asp:Button ID="imgEndSession" runat="server" ClientIDMode=Static  Text="END SESSION" CssClass="EndBtn" onclick="EndSessionButton_Click"/> 
        </div>
    </center>
</div>


<div id="Delete-message" style="display: none; background: white !important;">
    <div style="background-image: url('Portals/0/images/topband.png'); background-color: White;
        height: 110px; margin-top: -9px; width: 102%; margin-left: -7px;">
        <span class="AfterRenewelHeaderSpan" style="margin-top: 36px;">Confirm End Session</span>
    </div>
    <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
        box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
        height: 87%;">
        <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
            box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
            -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
            <span id="MessageLiteralEnd"  runat=server style="font-family: Raleway-regular,Raleway, Arial, sans-serif;
                font-size: 10pt; color: #707070; padding: 23px; float: left;">Do you want to end?</span>
        </div>
        <div style="width: 92%;">
            <input type="button" id="YesButton" style="margin-left: 192px;" value="Yes" class="popupokbtn" />
            <input type="button" id="NoButton" style="margin-left: 18px;" value="No" class="popupokbtn" />
        </div>
    </div>
</div>

<script type="text/javascript" language="javascript">
  var dkwindow;
    var deleteFlag;
    $("#imgEndSession").click(function () {
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
                $('a.k-window-action.k-link').parent().css("background-image", "url('Portals/0/images/close.png') !important");
                return false;
            });
            return false;
        }
        return true;
    });
    $("#YesButton").click(function () {
        dkwindow.data("kendoWindow").close();
        //document.getElementById("selectedSessionID").value = "";

        //        for (var i = 0; i < $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
        //            if ($("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
        //                document.getElementById("selectedSessionID").value += $("#ClassRepeaterDiv #ClassRepeaterContentDiv #RepeaterFstCol span")[i].innerHTML.trim() + ",";
        //            }
        //        }
        //        for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
        //            if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
        //                document.getElementById("selectedSessionID").value += $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
        //            }
        //        }
        deleteFlag = true;
        $("#imgEndSession").click();
        return false;
    });
    $("#NoButton").click(function () {
        dkwindow.data("kendoWindow").close();
        return false;
    });


</Script>