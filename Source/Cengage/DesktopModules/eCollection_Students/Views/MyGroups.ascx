﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyGroups.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Students.Views.MyGroups" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="Msg" %>
<Msg:Messages ID="Messages" runat="server">
</Msg:Messages>
<style type="text/css">
    #SearchTextBox-list
    {
        width: 618px !important;
        margin-left: -1px !important;
    }
    .eClUserSearchDiv
    {
        width: 94% !important;
    }
    .Div_FullWidth .k-input
    {
        padding-left: 6px !important;
    }
    .Searchbtndiv input[type=submit][disabled]
    {
        opacity: 1;
        filter: alpha(opacity=100);
        -moz-opacity: 1;
    }
    
    .ProfileButton 
    {
        background: url('../../portals/0/images/eye.png') no-repeat 7px center !important;
    }
</style>
<div id="StudentListDiv" class="StudentsList">
<div class="ProgressDivClass" style="display:none" id="UpdateProgressImg">
    <div class="ProgressInnerDiv">
        <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg"  alt="Processing" /> 
    </div>
</div>
    <asp:UpdatePanel ID="CheckingDiv" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="StudentList_TopDiv">
                <div class="StudentDashboard_CheckAllDiv">
                    <div id="CheckAllDiv" style="float: left; height: 20px; width: 9%;" onclick="javascript:CheckAll();">
                        <asp:Image ID="Image4" ClientMode="Static" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" /></div>
                </div>
                <div class="StudentDashboard_SortingDiv" style="margin-right: 22px; width: 40%;">
                    <%--<span style="float: left;">PM READING LEVEL</span>--%>
                    <asp:UpdatePanel ID="PMRdSrt" runat="server" UpdateMode="Conditional" style="float: left;">
                        <ContentTemplate>
                            <asp:Button ID="ReadingLevelAscButton" runat="server" Text="PM READING LEVEL" OnClick="ReadingLevelAscButton_Click"
                                CssClass="SortRead SortPM" />
                            <asp:Button ID="ReadingLevelDescButton" runat="server" Text="PM READING LEVEL" OnClick="ReadingLevelDescButton_Click"
                                CssClass="SortReadDesc SortPM" Visible="False" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <b style="margin: 0px 0px 0px 10px; float: left;">| </b>
                    <%--<span style="float: left;">
                        A-Z</span>--%>
                    <asp:UpdatePanel ID="NmeSrt" runat="server" UpdateMode="Conditional" style="float: left;">
                        <ContentTemplate>
                            <asp:Button ID="NamesAscButton" runat="server" Text="A – Z" OnClick="NamesAscButton_Click"
                                CssClass="SortRead SortNm" />
                            <asp:Button ID="NamesDescButton" runat="server" Text="A – Z" OnClick="NamesDescButton_Click"
                                CssClass="SortReadDesc SortNm" Visible="False" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="width: 89.8%; float: left; margin-left: 54px; border-left: 1px solid lightgray;
        min-height: 15px;">
    </div>
    <div style="width: 93.8%; border-style: solid; height: 34px; margin-left: 24px; border: 1px solid #CCCCCC;
        float: left; background-color: #EEEEEE; position: relative;">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="eClUserSearchDiv ">
                    <asp:TextBox ID="SearchTextBox" CssClass="eClSearchbox" ClientIDMode="Static" runat="server"
                        placeholder="Enter your search here.."></asp:TextBox>
                    <label id="eClSearchLabel" for="SearchTextBox">
                        Enter your search here</label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="BtnPnl" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="Searchbtndiv">
                    <asp:Button ID="SearchButton" runat="server" ClientIDMode="Static" OnClick="GroupSearch_Click" OnClientClick="ShowUpdate()" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="MidleftLinePnl" runat="server" UpdateMode="always">
        <ContentTemplate>
            <div id="MidleftLine" style="width: 89.8%; float: left; margin-left: 54px; border-left: 1px solid lightgray;
                min-height: 15px;" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="width: 89.8%; float: left; margin-left: 54px; border-left: 1px solid lightgray;
        margin-top: -27px;">
        <div style="float: left; width: 95.8%; margin-left: -8px;">
            <asp:UpdatePanel ID="GrpListPanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="Students" style="margin-top: 12px; float: left; width: 100%;">
                        <asp:ListView ID="GroupsList" runat="server" >
                            <ItemTemplate>
                                <div id="GroupsRepeaterDiv" style="float: left; width: 100%; height: 55px; font-size: 11pt;
                                    margin-top: 18px;">
                                    <asp:Label ID="classid" runat="server" ClientIDMode="Static" Text='<%# Eval("GroupId") %>'
                                        Style="display: none"></asp:Label>
                                    <div id="GroupRepeaterContentDiv" class="GroupRepeaterContentDiv" title='<%# Eval("NameToolTip")%>'>
                                        <div style="float: left; margin-top: 4px;text-align:center;margin-left:60px;">
                                            <asp:Label ID="MembersLabel" Style="font-size: 16pt; font-weight: bolder"
                                                runat="server" Text='<%# Eval("MemberCount") %>'></asp:Label><br />
                                            <span style="font-weight: bold;">Members</span>
                                        </div>
                                        <div style="float: left; margin-top: 10px;margin-left:25px;">
                                            <span style="font-weight: bold;">
                                                <%# Eval("Name") %></span><br />
                                            <asp:Label ID="TeacherNameLabel" Style="color: #1479A4 !important;
                                                font-weight: bold;" runat="server" Text='<%# Eval("TeacherName")%>'></asp:Label>
                                        </div>
                                        <div style="width: 19%; float: right; margin-top: 9px; margin-right: 5px;">
                                            <div class="greenBtn">                                                                                  
                                                <asp:Button ID="Profile" runat="server" ClientIDMode="Static"  Text="PAGE" class="ProfileButton"  OnClientClick="window.document.forms[0].target='_blank';" 
                                                    OnClick="ProfileButton_Click" CommandArgument='<%# Eval("GroupId") %>' />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>                      
                    </div>                                                             
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<asp:HiddenField ID="GroupIdHdn" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="GrpCount" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="searchhdn" runat="server" ClientIDMode="Static" />
<input type="hidden" id="lastsearch" runat="server" clientidmode="Static" />
<input type="hidden" id="newsearch" runat="server" clientidmode="Static" />
<script type="text/javascript">
    function ShowUpdate() {
        $("#UpdateProgressImg").css("display", "block");
    }
    function EndUpdateProgress() {
        $("#UpdateProgressImg").css("display", "none");
    }
    function loadprofile() {
        $('#LoadProfile').click();
       
    }

    jQuery(function () {
        if (parseInt(jQuery('#GrpCount').val()) <= 0) {
            jQuery('#StudentListDiv').hide();
        }

        $('#SearchTextBox').keypress(function (e) {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                
            }

            var code = (e.keyCode ? e.keyCode : e.which);
            
                if (code == 13) {
                    e.preventDefault();
                    $('#SearchButton').click();
                }
            
        });

        $('#SearchTextBox').keyup(function (e) {
            $('#SearchTextBox').focus();
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                
            }
            var code = (e.keyCode ? e.keyCode : e.which);
            
                if (code == 13) {
                    e.preventDefault();
                    $('#SearchButton').click();
                }
            
        }); 
        if ($.browser.msie && parseInt($.browser.version, 10) < 10) {
            $('#SearchTextBox').val('Enter your search here..');
            $("#SearchTextBox").focus(function () {
                if ($(this).val() == 'Enter your search here..') {
                    $(this).val("");
                }
            
            });
            $("#SearchTextBox").blur(function () {
                if ($(this).val().trim() == "") {
                    $('#SearchTextBox').val('Enter your search here..');
                }
            
            });
        }

        

        var data = new kendo.data.DataSource({
            transport: {
                read: {
                    url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=usergroups&loginname=' + $('#usernamehdn').val()),
                    dataType: "json"
                }
            }
        });

        //create AutoComplete UI component
        $("#SearchTextBox").kendoAutoComplete({
            dataTextField: "Name",
            dataSource: data,
            filter: "contains",
            placeholder: "Enter your search here...",
            separator: ", "
        });


        LoadClick();

    });
    function LoadClick() {
        $("#SearchButton").click(function () {           
                $('#searchhdn').val('search');
            $('#lastsearch').val($('#newsearch').val());
        });
    }
    function SelectedGroup(groupid) {
        jQuery('#GroupIdHdn').val(groupid);
    }
</script>