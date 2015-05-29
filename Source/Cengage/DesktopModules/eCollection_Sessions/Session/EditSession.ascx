﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditSession.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Sessions.Session.EditSession" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="uc1" %>

<script type="text/javascript">

    function removeFormField(id) {
        $(id).remove();
        return false;
    }


    $('#guided').click(function(){
        if ($(this).hasClass('guided'))
        {
            $(this).addClass('independent').removeClass('guided');
        }
        else
        {
            $(this).addClass('guided').removeClass('independent');
        }
    });



  jQuery(function () {
      jQuery("#<%=SessionDropDownList.ClientID %>").kendoDropDownList({ animation: false }); ;
		if (jQuery('#' + '<%= txtSessionName.ClientID %>').val().length == 0 || jQuery('#' + '<%= txtSessionName.ClientID %>').val() == '  Enter session name')
		    jQuery('#' + '<%= txtSessionName.ClientID %>').val('  Enter session name').css('color', 'lightgray');
		jQuery('#' + '<%= txtSessionName.ClientID %>').click(function () {
		    jQuery('#' + '<%= txtSessionName.ClientID %>').val("");
	});

		jQuery('#' + '<%= txtSessionName.ClientID %>').change(function () {
		    if (jQuery('#' + '<%= txtSessionName.ClientID %>').val().length == 0 || jQuery('#' + '<%= txtSessionName.ClientID %>').val() == '  Enter session name')
		        jQuery('#' + '<%= txtSessionName.ClientID %>').val('  Enter session name').css('color', 'lightgray');

        });

    });

    jQuery(function () {

        jQuery("#HistoryButton").click(function () {
            jQuery('#Switch').css({ 'background-image': 'url("../Portals/0/images/guided_off.png")' });
            $('#<%= lblSessiontype.ClientID %>').val("1");
            return false;
        });
        jQuery("#StudentsButton").click(function () {
            jQuery('#Switch').css({ 'background-image': 'url("../Portals/0/images/session_switch.png")' });
            $('#<%= lblSessiontype.ClientID %>').val("2");
            return false;
        });
    });

</script>
 <style type="text/css">
  .k-dropdown-wrap
{
   
background-color: white !important;
background-image: url('../Portals/0/images/Levelbg.png') !important;
background-repeat: repeat !important;

}
.k-dropdown-wrap .k-input
{
    height: 1.3em !important;
    margin-top: 2px !important;
    text-indent:0px !important;
    
}
.k-dropdown-wrap .k-select
{
    margin-top: 2px !important;
}
.k-dropdown-wrap .k-state-hover
{

background-color: white !important;

}

.k-select
    {
        margin-top: 4px;
    }
    .k-icon
    {
        background-image: url('../Portals/0/images/ui-icons_cccccc_256x240.png') !important;
    }
    .k-i-arrow-s
    {
        background-position: -129px -17px !important;
    }
    .k-numerictextbox .k-i-arrow-n
    {
        background-position: 0px -17px !important;
    }
    .k-numerictextbox .k-i-arrow-s
    {
        background-position: -65px -18px !important;
    }

.switch {
    border: 0px solid lightGrey;
    float:right; margin-right: 249px;
    border-radius: 0px;
    margin-top:7px; 
    width: 244px; 
    height: 48px;
    background-image: url('../Portals/0/images/session_switch.png');
    /* Other styles */
}

.guided {
    border: 0px solid lightGrey;
    float:right; margin-right: 249px;
    border-radius: 0px;
    margin-top:7px; 
    width: 285px; 
    height: 48px;
    background-image: url('../../Portals/0/images/switch_on.png');
    /* Other styles */
}

.independent {
    border: 0px solid lightGrey;
    float:right; margin-right: 249px;
    border-radius: 0px;
    margin-top:7px; 
    width: 285px; 
    height: 48px;
    background-image: url('../../Portals/0/images/switch_off.png');
    /* Other styles */
}



 .SelectedGroupItem 
 {
     background-color: #91B76C;
 }
 
 .SelectedStudentItem
 {
     background-color: #F69E47;
 }
 
  .SelectedTeacherItem
 {
     background-color: #21B4E7;
 }
 

 .SelectedGroupItem
    {     
    float: left;
    width: 128px;
    background-image: none;
    border-radius: 0px;
    font-weight: bold;
    height: 28px;
    border:1px solid lightgray;
    margin:4px;
    }

.SelectedStudentItem
    {     
    float: left;
    width: 128px;
    background-image: none;
    border-radius: 0px;
    font-weight: bold;
    height: 28px;
    border:1px solid lightgray;
    margin:4px;
    }

    .SelectedTeacherItem
    {     
    float: left;
    width: 128px;
    background-image: none;
    border-radius: 0px;
    font-weight: bold;
    height: 28px;
    border:1px solid lightgray;
    margin:4px;
    }

    .SelectedGroupItem span
    {
         float: left;
    display: inline;
    margin: 0px;
    margin-top: 4px;
    margin-left: 40px;
    color: white;
    font-family: Raleway-regular,Raleway, Arial, sans-serif;
    }
    
    .SelectedTeacherItem span
    {
         float: left;
    display: inline;
    margin: 0px;
    margin-top: 4px;
    margin-left: 40px;
    color: white;
    font-family: Raleway-regular,Raleway, Arial, sans-serif;
    }
    
.SelectedStudentItem span
    {
         float: left;
    display: inline;
    margin: 0px;
    margin-top: 4px;
    margin-left: 40px;
    color: white;
    font-family: Raleway-regular,Raleway, Arial, sans-serif;
    }
  .SelectedGroupItem a
    {
         float: right;
    margin-top: 3px;
    margin-right: 11px;
    color: white;
    font-family: Raleway-regular,Raleway;
    text-decoration: none;
    cursor:pointer;
    }
     .SelectedTeacherItem a
    {
         float: right;
    margin-top: 3px;
    margin-right: 11px;
    color: white;
    font-family: Raleway-regular,Raleway;
    text-decoration: none;
    cursor:pointer;
    }

 .SelectedStudentItem a
    {
         float: right;
    margin-top: 3px;
    margin-right: 11px;
    color: white;
    font-family: Raleway-regular,Raleway;
    text-decoration: none;
    cursor:pointer;
    }


    .SelectedGroupItem a:hover
    {
         color:Red;
    }

    .SelectedStudentItem a:hover
    {
         color:Red;
    
    }
      .SelectedTeacherItem a:hover
    {
         color:Red;
    
    }
    
    .GroupAddtoinnersnddiv
    {
         float: left;width: 99%;height: auto; margin: 20px 0px 10px 
    10px;border: 0px solid lightGray;
    }

    #SelectedStudentList
    {
    list-style-type:none;
    margin:0;
    padding:0;
    }
    #SelectedStudentList  li
    {
        list-style-type:none;
        display:inline; 
    }
       #SelectedStudentList  ul li
    {
        list-style-type:none;
        display:inline; 
    }

 </style>

<uc1:Messages ID="Messages" runat="server" />
<div style="width:96%;margin-top:19px;margin-left:2%">

<%--<div style="width:100%;background-image:url('../../Portals/0/images/editCreateGroupDiv.png');background-repeat:repeat;border: 1px solid lightGrey;height: 70px;"><div style="margin-top: 6px;">
<span style="font-size:x-large;font-weight:bolder; color:#006699;margin-left:10px;">
     <span style="color:#9CBEDE; font-size: large;">CREATE:</span><br /><span style="margin-left:10px;color:#4279A5">
    SESSION</span></span>
</div>
<asp:LinkButton ID="CancelMakingGroupLinkButton" style="color:#52494A;float: right; margin-top: -36px;margin-right: 43px;" runat="server" 
        Text="Cancel Making Session" onclick="CancelMakingSessionLinkButton_Click" ></asp:LinkButton>
</div>--%>
<div style="width:100%; margin-top: 15px; float:left; height: auto;border:1px solid lightgrey;">
<div style="float: left;height: 25px; margin: 25px 0px 10px 10px;">
<span class="Session">SUBSCRIPTION:</span>
</div>
<div style="float: left; width: 20%; margin: 22px 5px 0px 5px; height: 36px;">
    <asp:DropDownList ID="DrpSubscription" runat="server" style="width:100px">
    </asp:DropDownList>
</div>
</div>

<div style="width:100%; margin-top: 15px; float:left; height: auto;border:1px solid lightgrey;">
<div style="float: left;width:30px;height: 25px; margin: 25px 0px 10px 10px;text-align: center;">
<span class="Session">TO:</span>
</div>
<div style="float: left; width: 20%; margin: 13px 5px 0px 1px; height: 36px;">
   <asp:Button ID="AddStudentButton"             
            Enabled="true"  runat="server" Text="ADD STUDENTS" 
            onclick="AddStudentButton_Click" CssClass="SessionAddStudentButton"/></div>
<div style="color:#392C29;float: left; width: 18%; margin: 13px 5px 0px 1px; height: 36px;">  <asp:Button ID="AddGroupsButton"             
            Enabled="true"  runat="server" Text="ADD GROUPS" 
            onclick="AddGroupsButton_Click" CssClass="SessionAddStudentButton"/></div>
<div style="color:#392C29;float: left; width: 19%; margin: 13px 5px 0px 6px; height: 36px;"><asp:Button ID="AddTeachersButton"             
            Enabled="true"  runat="server" Text="ADD TEACHERS" 
            onclick="AddTeachersButton_Click" CssClass="SessionAddStudentButton"/></div>

<div style="float: left;width: 96%;height: 0.1px; margin: 1px 0px 0px 15px;border:1px solid lightgrey;border-color:#D4D4D4">
</div>

<div class="GroupAddtoinnersnddiv">
    <ul id="SelectedStudentList" runat="server" clientidmode="Static">
    </ul>
    <div id="dialog" title="Alert Message" style="display: none;">
    <p><span id="StudentNamespan"></span> is Already added</p>
    </div>
    <asp:TextBox ID="SelectedValueTextBox" runat="server" 
    ClientIDMode="Static" style="display: none;"></asp:TextBox>
    <asp:TextBox ID="SelectedValueGroupTextBox" runat="server" 
    ClientIDMode="Static" style="display: none;"></asp:TextBox>
    </div>

<%--
<div style="float: left;width: 96%;height: auto; margin: 13px 0px 0px 15px;border: 0px solid lightGrey;">
    <asp:Button ID="Button13" OnClientClick="removeFormField(this)" style="background-color:#F69E47;float: left;width:128px;background-image:none;border-radius:0px;font-weight: bold; height:28px;color:white;padding-left:14px;font-size:10pt" CssClass="ButtonStyle" Enabled="true"   runat="server" Text="Student Name x" />
</div>

<div style="float: left;width: 96%;height: 44px; margin: 5px 0px 0px 15px;border: 0px solid lightGrey;">
    <asp:Button ID="Button12" OnClientClick="removeFormField(this)" style="background-color:#91B76C; float: left;width:128px;background-image:none;border-radius:0px;font-weight: bold;  height:28px;color:white;padding-left:24px;font-size:10pt" CssClass="ButtonStyle" Enabled="true"  runat="server" Text="Group Name x" />
</div>

<div style="float: left;width: 96%;height: 44px; margin: -97px 0px 0px 152px;border: 0px solid lightGrey;">
    <asp:Button ID="Button14" OnClientClick="removeFormField(this)" style="background-color:#21B4E7; float: left;width:128px;background-image:none;border-radius:0px;font-weight: bold;  height:28px;color:white;padding-left:14px;font-size:10pt" CssClass="ButtonStyle" Enabled="true"  runat="server" Text="Teacher Name x" />
</div>

<div style="float: left;width: 96%;height: 44px; margin: -97px 0px 0px 290px;border: 0px solid lightGrey;">
    <asp:Button ID="Button15" OnClientClick="removeFormField(this)" style="background-color:#21B4E7; float: left;width:128px;background-image:none;border-radius:0px;font-weight: bold;  height:28px;color:white;padding-left:14px;font-size:10pt" CssClass="ButtonStyle" Enabled="true"  runat="server" Text="Teacher Name x" />
</div>

<div style="float: left;width: 96%;height: 44px; margin: -97px 0px 0px 429px;border: 0px solid lightGrey;">
    <asp:Button ID="Button16" OnClientClick="removeFormField(this)" style="background-color:#21B4E7; float: left;width:128px;background-image:none;border-radius:0px;font-weight: bold;  height:28px;color:white;padding-left:14px;font-size:10pt" CssClass="ButtonStyle" Enabled="true"  runat="server" Text="Teacher Name x" />
</div>

<div style="float: left;width: 96%;height: 44px; margin: -45px 0px 0px 152px;border: 0px solid lightGrey;">
    <asp:Button ID="Button17" OnClientClick="removeFormField(this)" style="background-color:#91B76C; float: left;width:128px;background-image:none;border-radius:0px;font-weight: bold;  height:28px;color:white;padding-left:24px;font-size:10pt" CssClass="ButtonStyle" Enabled="true"  runat="server" Text="Group Name x" />
</div>--%>


</div>


<div style="width:100%; margin-top: 15px; float:left; height: auto;border:1px solid lightgrey;">
<div style="float: left;width:155px;height: 25px; margin: 25px 0px 10px 10px;text-align: center;">
<span class="Session">BOOKS SELECTED:</span>
</div>
<div style="float: left; width: 130px; margin: 13px 0px 0px 1px; height: 36px;">
   <asp:Button ID="Button1"             
            Enabled="true"  runat="server" Text="ATTACH BOOKS" 
            onclick="AttachBooks_Click1" CssClass="SessionAddStudentButton"/></div>


<div style="float: left;width: 96%;height: 0.1px; margin: 1px 0px 0px 15px;border:1px solid lightgrey;border-color:#D4D4D4">
</div>

<div style="float: left;width: 16%;height: 108px; margin: 13px 0px 0px 15px;border: 0px solid lightGrey;">
    <img ID="BookCoverImage" src="..//Portals/0/images/TheLittleBlueHorse.png"
                                            Height="100px" Width="100px" />
</div>

<div style="float: left;width: 16%;height: 108px; margin: 13px 0px 0px 15px;border: 0px solid lightGrey;">
    <img ID="Image1" src="../Portals/0/images/TheLittleBlueHorse.png"
                                            Height="100px" Width="100px" />
</div>

<div style="float: left;width: 16%;height: 108px; margin: 13px 0px 0px 15px;border: 0px solid lightGrey;">
    <img ID="Image1" src="../Portals/0/images/srbook.png"
                                            Height="100px" Width="100px" />
</div>

<div style="float: left;width: 16%;height: 108px; margin: 13px 0px 0px 15px;border: 0px solid lightGrey;">
    <img ID="Image1" src="../Portals/0/images/book3.png"
                                            Height="100px" Width="100px" />
</div>

<div style="float: left;width: 16%;height: 108px; margin: 13px 0px 0px 15px;border: 0px solid lightGrey;">
    <img ID="Image1" src="../Portals/0/images/1.jpg"
                                            Height="100px" Width="100px" />
</div>

</div>








<%--<%--<div style="margin-top: 15px; float:left; width:100%; height: 118px;border: 1px solid lightGrey;background-color:#F4F0F1">
<div style="float: left; width: 150px; margin: 11px 0px 10px 11px; height: 36px;">                                        <asp:Image ID="BookCoverImage" AlternateText="Cover" ImageUrl="../../Portals/0/images/Book1.jpg"
                                            runat="server" Height="100px" Width="100px" /></div> 
<div style="float: left; width: 150px; margin: 11px 0px 10px 11px; height: 36px;">                                        <asp:Image ID="Image1" AlternateText="Cover" ImageUrl="../../Portals/0/images/Book1.jpg"
                                            runat="server" Height="100px" Width="100px" /></div>
<div style="float: left; width: 150px; margin: 11px 0px 10px 11px; height: 36px;">                                        <asp:Image ID="Image2" AlternateText="Cover" ImageUrl="../../Portals/0/images/Book1.jpg"
                                            runat="server" Height="100px" Width="100px" /></div>
<div style="float: left; width: 150px; margin: 11px 0px 10px 11px; height: 36px;">                                        <asp:Image ID="Image3" AlternateText="Cover" ImageUrl="../../Portals/0/images/srbook.png"
                                            runat="server" Height="100px" Width="100px" /></div>
<div style="float: left; width: 150px; margin: 11px 0px 10px 11px; height: 36px;">                                        <asp:Image ID="Image4" AlternateText="Cover" ImageUrl="../../Portals/0/images/srbook.png"
                                            runat="server" Height="100px" Width="100px" /></div>
<%--<div style="float: left; width: 150px; margin: 11px 0px 10px 11px; height: 36px;">                                        <asp:Image ID="Image3" AlternateText="Cover" ImageUrl="../../Portals/0/images/book3.png"
                                            runat="server" Height="100px" Width="100px" /></div>
<div style="float: left; width: 150px; margin: 40px 0px 10px 11px;height:36px;">
<asp:Button ID="AttachBooks" 
        style="float: left;width:120px;background-image:none;font-weight: bold;  height:28px;" 
        Enabled="true"   runat="server" Text="+ Attach Books" 
        onclick="AttachBooks_Click1" BorderStyle="None" 
         />
</div>

</div>--%>


<div style="float:left; width:100%;height:60px;margin-top: 15px;border: 1px solid lightGrey;background-color:#F4F0F1; ">
<div style="color:#6B5D63;float: left; width: 145px; margin: 25px 0px 10px 10px;font-size: 15px;"><span class="Session">SESSION TYPE:</span></div>
 
    <div id="Switch" style="display: inline-block; float: left; margin-top: 14px; 
        background-position: center; background-repeat: no-repeat;background-image: url(../Portals/0/images/session_switch.png); height:39px;">
        <asp:HiddenField ID=lblSessiontype runat="server" Value="1" />
        <div id="HistoryButton" style="width: 144px; height: 51px; border: 0px solid white;
            cursor: pointer; background-color: transparent; float: left;">
        </div>
        <div id="StudentsButton" style="border: 0px solid white; width: 144px; height: 51px;
            cursor: pointer; background-color: transparent; float: left;">
        </div>        
    </div>
<%--<div class="switch" >
<asp:Button ID="GuidedButton" runat="server" Text="Guided" 
        style="border-style: none; border-color: inherit; border-width: medium; cursor:pointer; color:#444243; font-weight: bold; float:left; background-image: url('../../Portals/0/images/Levelbg.png'); background-repeat: repeat; border-top-left-radius: 10px; border-bottom-left-radius: 10px;" 
        Height="39px" Width="182px" Visible=false />
<asp:Button ID="IndependentButton" runat="server" Text="Independent" 
        style="border-style: none; border-color: inherit; border-width: medium; cursor:pointer; color:#444243; font-weight: bold; float:left; background-image: url('../../Portals/0/images/Levelbg.png'); background-repeat: repeat;border-top-right-radius: 10px; border-bottom-right-radius: 10px;" 
        Height="39px" Width="182px" visible =false/>
</div>--%>
</div>

<%--<div style="float:left; width:100%;height:40px;margin-top: 5px;border: 1px solid lightGrey;background-color:#F4F0F1">
<div style="float: left; width: 500px; margin: 6px 0px 10px 214px; height: 36px;border:0px solid white"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl= "../../Portals/0/images/GuidIndep.png" style="float:left;margin-top:0px;cursor:pointer;height: 31px;width: 200px;border:0px solid white;box-shadow:none;border-radius:7px " />
</div> 
<div style="float: left; width: 115px; margin: 6px 0px 10px 1px; height: 36px;"></div>                   

</div>--%>


<div style="width:100%; margin-top: 15px; float:left; height: auto;border:1px solid lightgrey;">
<div style="float: left;width:120px;height: 25px; margin: 25px 0px 10px 10px;text-align: center;">
<span class="Session">NOTES:</span>
</div>
<div style="float: left;width: 96%;height: 0.1px; margin: 5px 0px 0px 15px;border:1px solid lightgrey;border-color:#D4D4D4">
</div>

<div style="float: left;width: 96%;height: 108px; margin: 15px 0px 0px 15px;border: 0px solid lightGrey;">
   <textarea  class="Notes"
        runat="server" id="txtNotes" cols="20" name="S1" rows="2"></textarea>
</div>
</div>



<%--<div style="float:left; width:100%;height:150px;border: 1px solid lightGrey;margin-top: 15px">
                        <br />
<div style="float: left;width:5%; height: 25px;margin: 2px 0px 10px 1px;">
    <span style="float: left;position: absolute;width: 0px;padding-top: 4px;padding-left: 3px;">Notes:</span> 
    </div>  
<div style="float: left; width: 90%; margin: -30px 0px 10px 70px;height:90px">

<textarea  style="resize:none;font: bold small sans-serif; height: 90px;border:0px solid white !important;width:99%;float: left;" 
        runat="server" id="TextArea1" cols="20" name="S1" rows="2">Lorem ipsum dolar sit amet,Lorem ipsum dolar sit amet,Lorem ipsum dolar sit amet,Lorem ipsum dolar sit amet,Lorem ipsum dolar sit amet,Lorem ipsum dolar sit amet,Lorem ipsum dolar sit amet,Lorem ipsum dolar sit amet,</textarea>
</div>                
</div>--%>


<div style="height:50px;float:left; width:100%; border-bottom:1px solid black;border: 1px solid lightGrey;background-color:#F4F0F1;margin-top: 15px;background-image: url('../../Portals/0/images/Tbrowimg.png')">
<div style="color:#6B5D63;float: left; width: 120px; margin: 18px 0px 10px 10px;"><span class="Session" style="width:125px;">SESSION NAME:</span></div>
<div class="EditSessionProfile_Div" style="margin-left:14px">
                    <div style="float: left; width: 95%;">
                        <asp:TextBox ID="txtSessionName" runat="server" CssClass="EditSessionProfile_TextBox"  >
                        </asp:TextBox></div>
                    <div class="EditSessionProfile_Span">
                        <span>*</span></div>
                </div>
</div>

 <div style="margin-top:15px;float:left; width:100%; height:45px;border-bottom:1px solid black;border: 1px solid lightGrey;background-color:#F4F0F1;background-image: url('../../Portals/0/images/Tbrowimg.png')">                                   
                                 <div style="float: left; width: 172px; margin-top: 12px; margin-left: 1px; margin-right: 0px; margin-bottom: 10px;margin: 15px 0px 10px 10px;text-align: center;">
                                      <span id="ReadingLevelLabel" class="Session">THIS SESSION ENDS:</span>
                                </div>
                                 <div style="float: left; width: 250px; margin: 12px 0px 10px -8px;">
                               <%-- <asp:DropDownList ID="SessionDropDownList" CssClass="dropdowns" runat="server" Height="28px"
                                    Width="100px" ClientMode="Static" style="color: #2A62C9;width:85%;border: 1px solid lightgray;box-shadow: 1px 2px lightgray;border-radius: 5px;background-color: whiteSmoke;">
                                    <asp:ListItem>Next Week</asp:ListItem>
                                    <asp:ListItem>Today</asp:ListItem>
                                </asp:DropDownList>    --%>     
                                 <select id="SessionDropDownList" runat="server" style="height: 27px;float: left;margin-left: 5px;position: absolute;width: 484px;">
                                    <option value="Next Week">Next Week</option>
                                    <option value="Today">Today</option>
                                </select>
                                 </div>          
                        </div>           

<div style="float: left;width: 100%;height: 0.1px; margin: 1px 0px 0px 1px;border:1px solid lightgrey;border-color:#D4D4D4;margin-top:15px">
</div>                        
                        
                                              
<div style="width:100%; margin-top: 15px; float:left; border:0px solid lightgrey;">
  <label style="float:left;margin-top:21px;font-size: 15px;color: #7A7A7A; font-weight: bold;margin: 15px 0px 10px 10px;"> FINISHED CREATING?</label>
<asp:Button ID="CreateSessionButton" runat="server"         
        Enabled="true" Text="CREATE SESSION"  CssClass="CreateSession" OnClick="CreateSessionButton_Click"
        />
  <asp:Button ID="CancelSessionGroup" runat="server" Text="CANCEL SESSION"  CssClass="CancelSession"
     OnClick="CancelCreateSession_Click" ></asp:Button>
</div>      
</div>
</div>