<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddTeachersToEditGroup.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.AddTeachersToEditGroup" %>
<div class="FinishCreateProfile CancelButtonGradient" style="margin-top: -76px;margin-right: 20px;">
<asp:Button ID="Backtocreategroupbtn" runat="server" ClientIDMode="Static" 
    style="margin-top: -76px;width: 179px;" 
    Text="CANCEL MAKING GROUP" CssClass="CancelBtn CancelButtonBackground" onclick="Backtocreategroupbtn_Click" 
       />
       </div>
<div class="GroupAddto">
<div class="GroupAddtoinnerfstdiv">
    <span>TO:</span>
    <asp:Button ID="BacktoCreateBtn" runat="server"  Text="BACK" CssClass="BackBtnGroup"
           onclick="BacktoCreateBtn_Click"/>
    </div>   
    <div id="DetailsDiv" runat="server" style="float: left; width: 60%; margin: -10px 0px 10px 1px;height:auto;overflow:hidden"></div>   
     <div class="GroupAddtoinnersnddiv">
    <ul id="SelectedStudentList" runat="server" clientidmode="Static">
    </ul>
    <div id="dialog" title="Alert Message" style="display: none;">
  <p><span id="StudentNamespan"></span> is Already added</p>
</div>
<asp:TextBox ID="SelectedValueTextBox" runat="server" ClientIDMode="Static" style="display: none;"></asp:TextBox>
<asp:TextBox ID="DeletedValueTextBox" runat="server" ClientIDMode="Static" style="display: none;"></asp:TextBox></div>
<%--<asp:Button  ID="BacktoCollectionButton" runat="server" Text="<<Back to Create Group" style="margin-bottom: 12px;margin-right: 9px;margin-top: 12px;cursor:pointer; float: right;background-color: #F9F9F9;width: 180px;height: 30px;border: 1px solid lightgray;border-radius: 5px;box-shadow: 1px 2px lightgray;" />--%>
</div>
<div id="StudentDiv" style="display:block; float: left;width:100%;">
<%-- <div class="GpRptcontfrstinnerdiv">
        <div class="GpStudentfrstdiv">
               <div id="SelectAllDiv"><img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" class="SelectallCheckBoxclass" id="SelectallCheckBox" alt=""  /><literal id="literal" ></literal></div>
               <span class="SelectAllSpan"> SELECT ALL</span>
        </div>
        <div class="GroupsSortReaddiv">
            <asp:Button ID="SortingButton" 
            CssClass="SortRead Sort" runat="server"  Text="A-Z" /><b style="float:right">|</b>
            <asp:Button ID="ReadingLevelButton" runat="server" CssClass="SortRead Reading" Text="PM READING LEVEL" />
        </div>

    </div>--%>
    <asp:UpdatePanel ID="CheckALLUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="GroupTopDiv">
                <div class="GroupSelectAll">
                    <div id="SelectAllDiv" style="float:left">
                        <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" class="SelectallCheckBoxclass"
                            id="SelectallCheckBox" alt="Image cannot be displayed" /><literal id="literal"></literal></div>
                    <span class="SelectAllSpan">SELECT ALL</span>
                </div>
                <div class="GroupsSortReaddiv" style="width:auto;margin-top:0px;">
                <span style="float: left;font-size: 10pt;font-family: Raleway, Arial, sans-serif;color:#707070;">PM READING LEVEL</span>
                    <asp:Button ID="ReadingLevelButton" CommandName="Ascending" Style="
                        width: 1px  !important;background-position: 99% 55%;float: left;margin-top: -1px;" runat="server" CssClass="SortRead Reading" />
                        <b
                        style="float: left;margin-top: -2px;">|</b>
                <span style="float: left;font-size: 10pt;margin-left: 10px;font-family: Raleway, Arial, sans-serif;color:#707070;">A-Z</span>
                    <asp:Button ID="SortingButton" CssClass="SortRead Sort" runat="server" CommandName="Ascending"  style="width: 1px !important;margin-right: 23px !important;" />
                        
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="margin-top:-1px" class="GpStudentleftlnglne"></div>
    <div class="SearchDiv" style="width:655px">
        <div class="SearchInnerDiv" style="width:655px">
             <input type="text" id="SearchTextBox" class="classSearchwater" runat="server" value="Enter your search here..." title="Enter your search here..." />
            <div class="Searchbtndiv">
            <asp:Button ID="SearchButton" runat="server"  CssClass="SearchButton" />
            </div>
            
        </div>
    </div>
    <div style="min-height: 40px;" class="GpStudentleftlnglne"></div>
     <div class="Addthrtedigrp">
        <div id="TeacherRep" class="GpRptitmcontentdiv">
        <asp:Repeater ID="StudentRepeater" runat="server">
            <ItemTemplate>
                <div id="TeacherRepeaterDiv" class="GpStudentRptcontentdiv">
                    <div class="GpStudentRptcontentimgdiv">
                        <img src="<%=Page.ResolveUrl("Portals/0/images/circle_small.png")%>" alt="" />
                    </div>
                    <div id="TeacherRepeaterContentDiv" class="GpStudentRptcontentinnerdiv">
                        <div class="GpStudentRptcontentinrfstdiv">
                         <input id="StudentCheckBoxes" clientidmode="Static" runat="server" type="checkbox" style="display:none" />
                         <asp:Label ID="studentid" runat="server" Text='<%# Eval("Id") %>' style="display:none" ></asp:Label>
                            <img id="StudentCheckBoxImg"  alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>"/>
                        </div>
                        <div  class="GpStudentRptcontentinsnddiv">
                             <asp:Label ID="StudentNameLabel" CssClass="CreateStTchlbl" runat="server" Text='<%# Eval("Text") %>'></asp:Label>
                        </div>
                       <div  class="GpStudentRptcontentinthrdiv">
                              <asp:Button ID="ClassProfileButton" runat="server" Text="ADD" CssClass="GpStudentRptcontentinthrbtn GpAddStudentRptbtn" />
                       </div>
                   </div>
                </div> 
        </ItemTemplate>
        </asp:Repeater>
        </div>
        

     </div>
      <div id="AllOtherGroupsDiv" class="AllOtherGroupsDiv" runat="server"  style="margin-top: -48px;">
        <div class="AllOtherGroupsImgDiv">
            <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
        </div>
        <div id="AllotherGroupDivBtn" class="AllotherGroupDivBtn" runat="server" style="width: 85%;margin-right: 20px;">
            <asp:Button ID="AllOtherGroupButton" runat="server" Text="All Other Teachers" 
                CssClass="AllOtherGroupBtn" onclick="AllOtherGroupButton_Click"   />
        </div>
    </div>
</div>

<asp:HiddenField ID="LastNode" runat="server" Value="empty" />
<script type="text/javascript">
    function removeFormField(id) {
        $(id).remove();
        return false;
    }
    $(document).ready(function () {
        jQuery("#<%=SearchTextBox.ClientID%>").focus(function () {
            if (jQuery(this).val() == this.title) {
                jQuery(this).val("");
            }
        });
        if ($.browser.msie) {
            $('#<%=ReadingLevelButton.ClientID %>').removeClass('Reading').css({ "padding-right": "15px", "width": "155px" });
        }
        jQuery("#<%=SearchTextBox.ClientID%>").blur(function () {
            if (jQuery(this).val().trim() == "") {
                jQuery(this).val(this.title);
            }
        });
        function GetFile(path) {
            var pathname = window.location.pathname;
            var temppath = pathname.split('/');
            var root = "http://" + window.location.host + "/" + temppath[1];
            var url = root + path;
            return url;
        }
        $('#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv input[type=submit]').click(function () {
            if ($("#SelectedStudentList li").length >= 1) {
                for (var i = 0; i < $("#SelectedStudentList")[0].childNodes.length; i++) {
                    var tis = $("#SelectedStudentList")[0].childNodes[i];
                    if (tis.nodeName != "#text") {
                        if ($("#SelectedStudentList")[0].childNodes[i].childNodes[1].innerHTML.trim() == this.parentNode.parentNode.children[0].children[1].innerHTML.trim()) {
                            document.getElementById("StudentNamespan").innerHTML = this.parentNode.parentNode.children[1].children[0].innerHTML.trim();
                            return false;
                        }
                    }
                }
            }
            else {
            }
            $("<li class=\'SelectedItem SelectedTeacher\'><span>" + this.parentNode.parentNode.children[1].children[0].innerHTML.trim() + "</span><span style='display:none'>" + this.parentNode.parentNode.children[0].children[1].innerHTML.trim() + "</span><a onclick='javascript:Remove(this)'>x</a></li>").appendTo("#SelectedStudentList");
            $("#SelectedValueTextBox").val($("#SelectedValueTextBox").val().trim() + this.parentNode.parentNode.children[0].children[1].innerHTML.trim() + ",");
            return false;
        });
        var checkallFlag = true;
        $("#SelectallCheckBox").click(function () {
            if (checkallFlag) {
                checkallFlag = false;
                for (var i = 0; i < $("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if (!$("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        $("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv input[type=checkbox]")[i].click();
                    }


                }
                this.src = GetFile("/Portals/0/images/tick_student.png");
            }
            else {
                checkallFlag = true;
                for (var i = 0; i < $("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                        $("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv input[type=checkbox]")[i].click();
                    }

                }
                this.src = GetFile("/Portals/0/images/circle_big.png");
            }

        });

        $("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv img").click(function () {
            this.parentNode.children[0].click();
            if (this.parentNode.children[0].checked) {
                var count = 0;
                for (var i = 0; i < $("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        count++;
                    }
                }

                if ($("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv input[type=checkbox]").length == count) {
                    $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/tick_student.png");
                    checkallFlag = false;
                }
                //                else {
                //                    checkallFlag = true;
                //                }
            }
            else {
                var count = 0;
                $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/circle_big.png");
                //checkallFlag = true;
                for (var i = 0; i < $("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        count++;
                    }
                    else {
                        $("#TeacherRep #TeacherRepeaterDiv #TeacherRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                    }
                }
                if (count != 0)
                    checkallFlag = true;

            }
        });

    });
    function Remove(obj) {
        var SelectedValues = $("#SelectedValueTextBox").val().trim().split(',');
        document.getElementById("SelectedValueTextBox").value = " ";
        for (var i = 0; i < SelectedValues.length; i++) {
            if (SelectedValues[i].trim() != obj.parentNode.children[1].innerHTML && SelectedValues[i].trim() != '') {
                $("#SelectedValueTextBox").val($("#SelectedValueTextBox").val().trim() + SelectedValues[i].trim() + ",")
            }
            else {
                if (obj.parentNode.children[1].title == "UserID" && SelectedValues[i].trim() != '')
                    $("#DeletedValueTextBox ").val($("#DeletedValueTextBox ").val().trim() + SelectedValues[i].trim() + ",")
            }
        }
        obj.parentNode.parentNode.removeChild(obj.parentNode);
    }
</script>
