<%@ Control Language="C#" Inherits="DotNetNuke.Modules.NewAndNotable.Edit" AutoEventWireup="false"
    CodeBehind="Edit.ascx.cs" %>
<link href="DesktopModules/NewAndNotable/module.css" type="text/css" />
<div class="wrapper">
    <div class="form-nav">
        <div class="row">
            <label>
                Title</label>
            <input type="text" id="titleOfModule" runat="server" class='txt titleOfModule' clientidmode="Static"  maxlength="50" />
            <div class='mandarydiv'>
                *</div>
        </div>
		<div class="row">
			<div id="DisplayCntMsg" class='mandarydiv'>
				Please enter ISBN's equal to the default display count
            </div>
		</div>
        <div class="row">
            <label>
                ISBN</label>
            <input type="text" id="txtIsbn1" runat="server" clientidmode="Static" class='txt mnisbn validisbn' />
            <div class='mandarydiv'>
                *</div>
            <input type="text" id="txtIsbn2" runat="server" clientidmode="Static" class='rightTxt mnisbn validisbn' />
            <div class='mandarydiv'>
                *</div>
            <input type="text" id="txtIsbn3" runat="server" clientidmode="Static" class='rightTxt isbn validisbn' />
            <div class='mandarydiv'>
                *</div>
            <input type="text" id="txtIsbn4" runat="server" clientidmode="Static" class='rightTxt isbn validisbn' />
            <div class='mandarydiv'>
                *</div>
            <input type="text" id="txtIsbn5" runat="server" clientidmode="Static" class='rightTxt isbn validisbn' />
            <div class='mandarydiv'>
                *</div>
            <input type="text" id="txtIsbn6" runat="server" clientidmode="Static" class='rightTxt isbn validisbn' />
            <div class='mandarydiv'>
                *</div>
        </div>
        <div class="row">
            <label>
                Default Display Count</label>
            <input type="text" id="displayCount" runat="server" clientidmode="Static" class='txt displayCount' />
            <div class='mandarydiv'>
                *</div>
        </div>
        <div class="row">
            <input type="button" id="btnNewSave" runat="server" clientidmode="Static"   style="display:none;" onserverclick="btnNewSave_Click" />
			<input type="button" id="btnNewSaveHidden" value="Save" onclick="javascript:return NewSave(this);"/>
            <div  class='mandarydiv'>
                *</div>
        </div>
    </div>
	<input type="hidden" id="hdnNewDivision" runat="server" clientidmode="Static" />
<input type="hidden" id="hdnFlagIsbn" clientidmode="Static" value="true" />
</div>

<script type="text/javascript">
    function GetFile(path) {
        var pathname = window.location.pathname;
        var temppath = pathname.split('/');
        var root = location.protocol + "//" + window.location.host + "/" + temppath[0];
        var url = root + path;
        return url;
    }
    function httpGet(theUrl) {
        var xmlHttp = null;
        xmlHttp = new XMLHttpRequest();
        xmlHttp.open("GET", theUrl, false);
        xmlHttp.send(null);
        return xmlHttp.responseText;
    }
    $(document).ready(function () {
        var isAllISBNValid;
        $('.mandarydiv').hide();
		
        $('.validisbn').blur(function () {
			return CheckISBNs("blur");           
        });
        $('.displayCount').blur(function () {
		var reg1 = new RegExp("^[2-6]$");
            if ($('.displayCount').val().trim() == "") {
                $(this).next()[0].innerHTML = "Please enter the count";
                $(this).next().show();
                return false;
            }
			else if (!reg1.test($('.displayCount').val().trim())) {
            $(this).next()[0].innerHTML = "Count should be between 2 to 6";
                $(this).next().show();
                return false;
        }
            else {
			$(this).next().hide();
                return true;
            }
        });
        $('.titleOfModule').blur(function () {
            if ($('.titleOfModule').val().trim() == "") {
                $(this).next()[0].innerHTML = "Please enter the title";
                $(this).next().show();
                return false;
            }
            else {
			$(this).next().hide();
                return true;
            }
        });
    });
function CheckISBNs(type)
{
	var NNCnt=0,NNECnt=0;
	var flag22="true";
	$('.validisbn').each(function(){
		if($(this).val().trim()=='')
		{
			NNCnt++;
			$(this).next()[0].innerHTML = "";
		}
		else
		{
			NNECnt++;
			var record5 = httpGet(GetFile('/DesktopModules/NewAndNotable/Components/Handlers/NewAndNotableHandler.ashx?isbn=' + $(this).val().trim() + "&division=" + document.location.href.split('/')[3]));
			if (record5 == "false") 
			{
					$(this).next()[0].innerHTML = "Please enter valid ISBN";
					$(this).next().show();
					flag22="false";
			}
			else
			{
				   $(this).next()[0].innerHTML = "";
				   
			}
		}
	});
	
    if(NNCnt==6)
	{
		$('#txtIsbn1').next()[0].innerHTML = "*";
		$('#txtIsbn1').next().show();
		$('#txtIsbn2').next()[0].innerHTML = "*";
		$('#txtIsbn2').next().show();
        flag22="false";
	}
	else if(NNCnt==5)
	{
		if($('#txtIsbn1').val().trim()=='')
		{
			$('#txtIsbn1').next()[0].innerHTML = "*";
			$('#txtIsbn1').next().show();
		}
		else
		{			
			$('#txtIsbn2').next()[0].innerHTML = "*";
			$('#txtIsbn2').next().show();
		}
		flag22="false";
	}
			
	$('.validisbn').each(function(){
		var t1=$(this).val().trim(),eqs=0,t3=$(this);
		$('.validisbn').each(function(){
			var t2=$(this).val().trim();
			if(t2!='' && t2==t1)
			{
				eqs++;
				if(eqs>1)
				{	
					t3.next()[0].innerHTML = "Duplicate ISBNs";
					t3.next().show();				
					$(this).next()[0].innerHTML = "Duplicate ISBNs";
					$(this).next().show();
					flag22="false";
				}
			}
		});		
	});
	if(type=="save" && NNECnt<$('#displayCount').val().trim())
	{
		$('#DisplayCntMsg').show();
		flag22="false";
	}
	else
	{
		$('#DisplayCntMsg').hide();
	}
	return flag22;
}
function NewSave(click)
{	
	var flag22=CheckISBNs('save');
	var title,isbn=true,displaycount;
    if ($('.titleOfModule').val().trim() == "") 
    {
	$('#titleOfModule').next()[0].innerHTML = "Please enter the title";
			$('#titleOfModule').next().show();
        $(click).next()[0].innerHTML = "Please enter the title";
        //$(click).next().show();
        title= false;
    }
    else 
    {
        title= true;
    }
    var count = 0;
	var reg1 = new RegExp("^[2-6]$");
    
    if ($('.displayCount').val().trim() == ""||$('.displayCount').val().trim() == "0") 
    {
	$('#displayCount').next()[0].innerHTML = "Please enter the count";
			$('#displayCount').next().show();
        $(click).next()[0].innerHTML = "Please enter the count";
        //$(click).next().show();
        displaycount= false;
    }
    else 
    {
		if (reg1.test($('.displayCount').val().trim()))		
            displaycount=  true;
			else
			{
				$('#displayCount').next()[0].innerHTML = "Count should be between 2 to 6";
			$('#displayCount').next().show();
				displaycount= false;
			}
    }
	var flag11;
	if($('#hdnFlagIsbn').val()=="false")
	{
		flag11=false;
	}
	else
	{
		flag11=true;
	}
	var flag33;
	if(flag22=="false")
	{
		flag33=false;
	}
	else
	{
	    flag33=true;
	}
	if (title && isbn && displaycount && flag11 && flag33)
    {
	$(".mandarydiv").hide();
        $("#btnNewSave").click();
        return true;
    }
    else
    {

        return false;
    }
}
</script>
