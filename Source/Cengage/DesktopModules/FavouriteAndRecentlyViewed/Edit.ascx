<%@ Control language="C#" Inherits="DotNetNuke.Modules.FavouriteAndRecentlyViewed.Edit" AutoEventWireup="false"  Codebehind="Edit.ascx.cs" %>
<script type="text/javascript">
   $(document).ready(function () {
        var isAllISBNValid;
        $('.mandarydiv1').hide();
        $('.titleclass').blur(function () {
		
            if ($('.titleclass').val().trim() == "") {
                $(this).next()[0].innerHTML = "Please enter the title";
                $(this).next().show();
				$('#hdnFlagtitle').val("false");
                return false;
            }
			
            else {
			$(this).next().hide();
			$('#hdnFlagtitle').val("true");
                return true;
            }
        });
		 $('.displayCountclass').blur(function () {
		var reg1 = new RegExp("^([1-9]|10)$");
		// var reg1 = /^[1-10]$/;
            if ($('.displayCountclass').val().trim() == "") {
                $(this).next()[0].innerHTML = "Please enter the count";
                $(this).next().show();
				$('#hdnFlagCount').val("false");
                return false;
            }
			else if (!reg1.test($('.displayCountclass').val().trim())) {
			//else if (parseInt(($('.displayCountclass').val().trim())<1)||(parseInt($('.displayCountclass').val().trim())>10)) {
            $(this).next()[0].innerHTML = "Count should be between 1 to 10";
                $(this).next().show();
				$('#hdnFlagCount').val("false");
                return false;
        }
		
            else {
			$(this).next().hide();
			$('#hdnFlagCount').val("true");
                return true;
            }
        });
    });
	function NewSave(click)
	{
	var reg1 = new RegExp("^([1-9]|10)$");
		// var reg1 = /^[1-10]$/;
            if ($('#displayCount').val().trim() == "") {
                $('#displayCount').next()[0].innerHTML = "Please enter the count";
                $('#displayCount').next().show();
				$('#hdnFlagCount').val("false");
                //return false;
            }
			else if (!reg1.test($('#displayCount').val().trim())) {
			//else if (parseInt(($('#displayCount').val().trim())<1)||(parseInt($('#displayCount').val().trim())>10)) {
            $('#displayCount').next()[0].innerHTML = "Count should be between 1 to 10";
                $('#displayCount').next().show();
				$('#hdnFlagCount').val("false");
                //return false;
        }
		
            else {
			$('#displayCount').next().hide();
			$('#hdnFlagCount').val("true");
                //return true;
            }
	 if ($('#titleOfModule').val().trim() == "") {
                $('#titleOfModule').next()[0].innerHTML = "Please enter the title";
                $('#titleOfModule').next().show();
				$('#hdnFlagtitle').val("false");
                //return false;
            }
			
            else {
			$('#titleOfModule').next().hide();
			$('#hdnFlagtitle').val("true");
                //return true;
            }
	
	var flag3
	if(($("#favChkbx").prop('checked') == true)||($("#recentChkbx").prop('checked') == true)){
    flag3=true;
	$('#errChk').html("");
}
else
{
$('#errChk').html("Please check atleast one Checkbox");
flag3=false;
}
	var flag1;
		if($('#hdnFlagtitle').val()=="false")
		{
		flag1=false;
		}
		else
		{
		flag1=true;
		}
		var flag2;
		if($('#hdnFlagCount').val()=="false")
		{
		flag2=false;
		}
		else
		{
		flag2=true;
		}
if(flag1&&flag2&&flag3){
$("#btnSave").click();
return true;
}
else{

return false;
}
	}
	</script>
<div>
    <div>
    <span class="fav-edit">Title</span>
    <input type = "text" id="titleOfModule" runat = "server" class="titleclass" clientidmode="Static" maxlength="50" />
	<div class='mandarydiv1'>Please enter the title</div>
    </div>
    <div>
    <span class="fav-edit">Show Favourites</span>
    <input type = "checkbox" id = "favChkbx" runat ="server" clientidmode="Static"/><asp:Label ID="errChk" ForeColor="red" style="font-weight:bold; margin-left: 10px;" runat="server" clientidmode="Static"/>
    </div>
    <div>
    <span class="fav-edit">Show Recently Viewed</span>
    <input type = "checkbox" id ="recentChkbx" clientidmode="Static" runat = "server"/>
    </div>
    <div>
    <span class="fav-edit">Default Display Count</span>
    <input type = "text" id ="displayCount" clientidmode="Static" runat ="server" class="displayCountclass" /><div class='mandarydiv1'>
                Please enter the count</div>
    </div>

    <input type = "button" id ="btnSave" clientidmode="Static" value="Save" runat ="server" style="display:none;" onserverclick = "btnSave_Click"/> 
	<input type="button" id="btnNewSaveHidden" value="Save" onclick="javascript:return NewSave(this);"/>
</div>
<input type="hidden" id="hdnFlagCount" clientidmode="Static" value="true" />
<input type="hidden" id="hdnFlagtitle" clientidmode="Static" value="true" />
<input type ="hidden" id = "divisionHidden" runat = "server"  clientidmode="Static"/>