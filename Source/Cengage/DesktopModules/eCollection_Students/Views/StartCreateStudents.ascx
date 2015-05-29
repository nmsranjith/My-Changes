<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StartCreateStudents.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Students.Views.StartCreateStudents" %>
<div class="create-students">
	<span  class="titlelabel" title="How would you like to create students?">How would you like to create students?</span>
	<div class="SubsContent ">
		<div class="Div_leftwidth">
			<div class="sub-title">
				<span  class="sub-title-label" title="Manual via form">Manual via form</span>
			</div>
			<div class="SubsManageDiv">
				<span class="startdatespan">Enter student information into a form</span>
			</div>
		</div>
		<div class="Div_rightwidth">
			   <a id="StudCreateLnk" runat="server" class="UseBtn std-crt-use-btn">USE</a>
		</div>
	</div>
	<div class="SubsContent ">
		<div class="Div_leftwidth">
			<div class="sub-title">
				<span  class="sub-title-label" title="Upload via spreadsheet">Upload via spreadsheet</span>
			</div>
			<div class="SubsManageDiv ">
				<span class="startdatespan">Upload multiple students at once using a spreadsheet</span>
			</div>
		</div>
		<div class="Div_rightwidth">
			   <a id="StudBulkUploadLnk" runat="server" class="UseBtn std-crt-use-btn">USE</a>
		</div>
	</div>
	<div class="SubsContent ">
		<div class="Div_leftwidth">
			<div class="sub-title">
				<span  class="sub-title-label" title="Search Past students">Search Past students</span>
			</div>
			<div class="SubsManageDiv ">
				<span class="startdatespan">Find students that are not attached to a subscription</span>
			</div>
		</div>
		<div class="Div_rightwidth">
			   <a id="StudUnallocatedSearchLnk" runat="server" class="UseBtn std-crt-use-btn">USE</a>
		</div>
	</div>
	
</div>
<div id="LicenceExhaust" style="display: none; background: white !important;">
<div style="background-image: url('../Portals/0/images/topband.png'); background-color: White;
	height: 110px; margin-top: -9px; width: 102%; margin-left: -7px;">
	<span class="PopupHeaderSpan" style="margin-top: 36px;">Alert Message!</span>
</div>
<div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
	box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
	height: 87%;">
	<div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
		box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
		-webkit-box-shadow: 2px 7px 5px -6px lightgray;">
		<span id="LicExhaustMsg" Style="font-family: Raleway-regular, Arial, sans-serif;
			font-size: 10pt; color: #707070; padding: 30px; float: left;"></span>
		<asp:Label ID="TeacherTxt" runat="server" Visible="false" Style="font-family: Raleway-regular, Arial, sans-serif;
			font-size: 10pt; color: #707070; padding: 30px; float: left;">Licences for the subscription are exhausted, Please contact your Administrator</asp:Label>
		<asp:Label ID="AdminText" runat="server" Visible="false" Style="font-family: Raleway-regular, Arial, sans-serif;
			font-size: 10pt; color: #707070; padding: 30px; float: left;">Licences for the subscription are exhausted, Please buy/update the subscription. Please visit <a href="/pmecollection">BUY</a></asp:Label>
	</div>
	<div style="width: 92%;">
		<input type="button" id="OkButton" value="Ok" class="popupokbtn" />
	</div>
</div>
</div>
<script type="text/javascript">
    $(function () {
        if ($('#RoleChkHdn').val() == 'true') {
            jQuery(".ECollLeftModule").css('margin-top', '-345px');
        }
        $('.k-window-action.k-link').click(function () { window.location.href = 'students.aspx'; });
        //$('<a class="popupClosebg Licpopup" href="students.aspx"></a>').prependTo($('#dialog-message').parent('.k-window'));
        $('#CreatePageBtn').removeClass('srtbtnshide');
        $('#CreatePageFinishBtn').removeClass('srtbtnshide');
    });
</script>