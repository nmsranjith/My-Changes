
    $(function () {

        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
        // This will call the function after postback..
        if ($.browser.mozilla) {
            $('#SortingButton').css('margin-top', '1px');
            $('.LeftLineDiv').css('margin-top', '-2px');
        }
    });
    
    function PostBack() {
	
	$('.gchkbx span').click(function(){
		var selGrps='';
		var attr = $(this).attr('class');
		if (typeof attr !== typeof undefined && attr !== false && attr !='HideItems') { 
			if(attr.trim()=='ico-uncheck')		
				$(this).removeClass('ico-uncheck').addClass('ico-check');			
			else
				$(this).removeClass('ico-check').addClass('ico-uncheck');
			$(this).parent().children('input[type="checkbox"]').click();
		}
		else
		return;
		var chkBxs=0,cChkBxs=0,type='',fnBx=0;
		if($('#ActiveSessionOuterDiv').is(':visible'))
		{
			chkBxs=chkBxs+$('#ActiveSessionOuterDiv input[type="checkbox"]').length;
			cChkBxs=cChkBxs+$('#ActiveSessionOuterDiv input:checked').length;
			$('#ActiveSessionOuterDiv input:checked').each(function(){
			if($(this).next().text()!='undefined')
				selGrps=selGrps+','+$(this).next().text();	
			});
			
		}
		if($('#FinishedSessionOuterDiv').is(':visible'))
		{
			chkBxs=chkBxs+$('#FinishedSessionOuterDiv input[type="checkbox"]').length;
			cChkBxs=cChkBxs+$('#FinishedSessionOuterDiv input:checked').length;
			fnBx=$('#FinishedSessionOuterDiv input:checked').length;
			$('#FinishedSessionOuterDiv input:checked').each(function(){
			if($(this).next().text()!='undefined')
				selGrps=selGrps+','+$(this).next().text();				
			});			
		}
		if($('#ArchivedSessionOuterDiv').is(':visible'))
		{
			chkBxs=chkBxs+$('#ArchivedSessionOuterDiv input[type="checkbox"]').length;
			cChkBxs=cChkBxs+$('#ArchivedSessionOuterDiv input:checked').length;
			$('#ArchivedSessionOuterDiv input:checked').each(function(){
			if($(this).next().text()!='undefined')
				selGrps=selGrps+','+$(this).next().text();
			});			
		}		
			
		if(chkBxs==cChkBxs)
			$('#SelectAllChkbx').removeClass('ico-uncheck').addClass('ico-check');	
		else
			$('#SelectAllChkbx').removeClass('ico-check').addClass('ico-uncheck');	
			
		if(cChkBxs==1)
		{		
			selGrps=selGrps;
			if(fnBx!=cChkBxs)
			{
				$('#EndSessionButton').parent().addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
				$('#EndSessionButton').removeAttr("disabled", "disabled").addClass("EndBtn").removeClass('DbldEndBtn ');
			}
			else
			{
				$('#EndSessionButton').parent().removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
				$('#EndSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldEndBtn ');
			}
			$('#DeleteSessionButtonNew').parent().addClass("ActiveDeleteButtonHolder").removeClass("DisabledDeleteButtonHolder");
			$('#DeleteSessionButtonNew').removeAttr("disabled", "disabled").addClass("CancelBtn").removeClass('DbldDelBtn');			
		}
		else if (cChkBxs==0)
		{
			$('#EndSessionButton').parent().removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
			$('#EndSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldEndBtn ');
			$('#DeleteSessionButtonNew').parent().removeClass("ActiveDeleteButtonHolder").addClass("DisabledDeleteButtonHolder");
			$('#DeleteSessionButtonNew').attr("disabled", "disabled").removeClass("CancelBtn").addClass('DbldDelBtn');			
		}
		else
		{
			if(fnBx!=cChkBxs)
			{
				$('#EndSessionButton').parent().addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
				$('#EndSessionButton').removeAttr("disabled", "disabled").addClass("EndBtn").removeClass('DbldEndBtn ');
			}
			else
			{
				$('#EndSessionButton').parent().removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
				$('#EndSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldEndBtn ');
			}
			$('#DeleteSessionButtonNew').parent().addClass("ActiveDeleteButtonHolder").removeClass("DisabledDeleteButtonHolder");
			$('#DeleteSessionButtonNew').removeAttr("disabled", "disabled").addClass("CancelBtn").removeClass('DbldDelBtn');			
		}
		selGrps=selGrps.replace(/^,|,$/g, "");
		$('#selectedGroupID').val(selGrps);
	});
	$('#SelectAllChkbx').click(function(){
		var selGrps='';
		if($(this).attr('class').trim()=='ico-uncheck')	
		{
			var cChkBxs=0,fnBx=0;
			$(this).removeClass('ico-uncheck').addClass('ico-check');	
			if($('#ActiveSessionOuterDiv').is(':visible'))
			{
				$('#ActiveSessionOuterDiv .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false && attr != 'ico-check' && attr !='HideItems') { 
						$(this).removeClass('ico-uncheck').addClass('ico-check');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
				cChkBxs=cChkBxs+$('#ActiveSessionOuterDiv input:checked').length;
				$('#ActiveSessionOuterDiv input:checked').each(function(){
				if($(this).next().text()!='undefined')
					selGrps=selGrps+','+$(this).next().text();
				});
			}
			
			if($('#FinishedSessionOuterDiv').is(':visible'))
			{
				$('#FinishedSessionOuterDiv .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false && attr != 'ico-check' && attr !='HideItems') { 
						$(this).removeClass('ico-uncheck').addClass('ico-check');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
				fnBx=$('#FinishedSessionOuterDiv input:checked').length;
				cChkBxs=cChkBxs+$('#FinishedSessionOuterDiv input:checked').length;
				$('#FinishedSessionOuterDiv input:checked').each(function(){
				if($(this).next().text()!='undefined')
					selGrps=selGrps+','+$(this).next().text();
				});
			}
			
			if($('#ArchivedSessionOuterDiv').is(':visible'))
			{
				$('#ArchivedSessionOuterDiv .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false&& attr != 'ico-check' && attr !='HideItems') { 
						$(this).removeClass('ico-uncheck').addClass('ico-check');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
				cChkBxs=cChkBxs+$('#ArchivedSessionOuterDiv input:checked').length;
				$('#ArchivedSessionOuterDiv input:checked').each(function(){
				if($(this).next().text()!='undefined')
					selGrps=selGrps+','+$(this).next().text();
				});
			}
			
			/*if(cChkBxs==1)
			{
				$('#EditGroupDiv').addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
				$('#EditGroupButton').removeAttr("disabled", "disabled").addClass("BtnStyle").removeClass('DbldBtn');
				$('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
				$('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
			}
			else
			{
				$('#MergeGroupDiv').addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
				$('#MergeGroupButton').removeAttr("disabled", "disabled").addClass("BtnStyle").removeClass('DbldBtn');
				$('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
				$('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
			}*/
			if(fnBx!=cChkBxs)
			{
				$('#EndSessionButton').parent().addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
				$('#EndSessionButton').removeAttr("disabled", "disabled").addClass("EndBtn").removeClass('DbldEndBtn');
			}
			else
			{
				$('#EndSessionButton').parent().removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
				$('#EndSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldEndBtn ');
			}
			$('#DeleteSessionButtonNew').parent().addClass("ActiveDeleteButtonHolder").removeClass("DisabledDeleteButtonHolder");
			$('#DeleteSessionButtonNew').removeAttr("disabled", "disabled").addClass("CancelBtn").removeClass('DbldDelBtn');			
		}
		else
		{
			$(this).removeClass('ico-check').addClass('ico-uncheck');
			if($('#ActiveSessionOuterDiv').is(':visible'))
			{
				$('#ActiveSessionOuterDiv .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false && attr != 'ico-uncheck' && attr !='HideItems') { 
						$(this).removeClass('ico-check').addClass('ico-uncheck');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
			}
			
			if($('#FinishedSessionOuterDiv').is(':visible'))
			{
				$('#FinishedSessionOuterDiv .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false && attr != 'ico-uncheck' && attr !='HideItems') { 
						$(this).removeClass('ico-check').addClass('ico-uncheck');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
			}
			
			if($('#ArchivedSessionOuterDiv').is(':visible'))
			{
				$('#ArchivedSessionOuterDiv .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false && attr != 'ico-uncheck' && attr !='HideItems') { 
						$(this).removeClass('ico-check').addClass('ico-uncheck');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
			}
			$('#EndSessionButton').parent().removeClass("ActiveAddButtonHolder ").addClass("DisabledAddButtonHolder");
			$('#EndSessionButton').attr("disabled", "disabled").removeClass("EndBtn").addClass('DbldEndBtn');
			$('#DeleteSessionButtonNew').parent().removeClass("ActiveDeleteButtonHolder").addClass("DisabledDeleteButtonHolder");
			$('#DeleteSessionButtonNew').attr("disabled", "disabled").removeClass("CancelBtn").addClass('DbldDelBtn');			
			selGrps='';
		}	
		selGrps=selGrps.replace(/^,|,$/g, "");
		$('#selectedGroupID').val(selGrps);
	});
		
jQuery("#sessionssortdpn").kendoDropDownList({
		animation: false
	})
    
	}
	
function CloseItems(hideBtn,parentId)
{
	if($(hideBtn).text()=='Hide')
	{
		$($('#'+parentId)).addClass('HideItems').removeClass('ShowItems');
		$(hideBtn).text('Show');
	}
	else
	{
		$($('#'+parentId)).removeClass('HideItems').addClass('ShowItems');
		$(hideBtn).text('Hide');
	}
	var chkBxs=0,cChkBxs=0,type='',fnBx=0,selGrps='';
		if($('#ActiveSessionOuterDiv').is(':visible'))
		{
			chkBxs=chkBxs+$('#ActiveSessionOuterDiv input[type="checkbox"]').length;
			cChkBxs=cChkBxs+$('#ActiveSessionOuterDiv input:checked').length;
			$('#ActiveSessionOuterDiv input:checked').each(function(){
			if($(this).next().text()!='undefined')
				selGrps=selGrps+','+$(this).next().text();	
			});
			
		}
		if($('#FinishedSessionOuterDiv').is(':visible'))
		{
			chkBxs=chkBxs+$('#FinishedSessionOuterDiv input[type="checkbox"]').length;
			cChkBxs=cChkBxs+$('#FinishedSessionOuterDiv input:checked').length;
			fnBx=$('#FinishedSessionOuterDiv input:checked').length;
			$('#FinishedSessionOuterDiv input:checked').each(function(){
			if($(this).next().text()!='undefined')
				selGrps=selGrps+','+$(this).next().text();				
			});			
		}
		if($('#ArchivedSessionOuterDiv').is(':visible'))
		{
			chkBxs=chkBxs+$('#ArchivedSessionOuterDiv input[type="checkbox"]').length;
			cChkBxs=cChkBxs+$('#ArchivedSessionOuterDiv input:checked').length;
			$('#ArchivedSessionOuterDiv input:checked').each(function(){
			if($(this).next().text()!='undefined')
				selGrps=selGrps+','+$(this).next().text();
			});			
		}		
			
		if(chkBxs==cChkBxs)
			$('#SelectAllChkbx').removeClass('ico-uncheck').addClass('ico-check');	
		else
			$('#SelectAllChkbx').removeClass('ico-check').addClass('ico-uncheck');	
			
		if(cChkBxs==1)
		{		
			selGrps=selGrps;
			if(fnBx!=cChkBxs)
			{
				$('#EndSessionButton').parent().addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
				$('#EndSessionButton').removeAttr("disabled", "disabled").addClass("EndBtn").removeClass('DbldEndBtn ');
			}
			else
			{
				$('#EndSessionButton').parent().removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
				$('#EndSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldEndBtn ');
			}
			$('#DeleteSessionButtonNew').parent().addClass("ActiveDeleteButtonHolder").removeClass("DisabledDeleteButtonHolder");
			$('#DeleteSessionButtonNew').removeAttr("disabled", "disabled").addClass("CancelBtn").removeClass('DbldDelBtn');			
		}
		else if (cChkBxs==0)
		{
			$('#EndSessionButton').parent().removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
			$('#EndSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldEndBtn ');
			$('#DeleteSessionButtonNew').parent().removeClass("ActiveDeleteButtonHolder").addClass("DisabledDeleteButtonHolder");
			$('#DeleteSessionButtonNew').attr("disabled", "disabled").removeClass("CancelBtn").addClass('DbldDelBtn');			
		}
		else
		{
			if(fnBx!=cChkBxs)
			{
				$('#EndSessionButton').parent().addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
				$('#EndSessionButton').removeAttr("disabled", "disabled").addClass("EndBtn").removeClass('DbldEndBtn ');
			}
			else
			{
				$('#EndSessionButton').parent().removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
				$('#EndSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldEndBtn ');
			}
			$('#DeleteSessionButtonNew').parent().addClass("ActiveDeleteButtonHolder").removeClass("DisabledDeleteButtonHolder");
			$('#DeleteSessionButtonNew').removeAttr("disabled", "disabled").addClass("CancelBtn").removeClass('DbldDelBtn');			
		}
}
	
