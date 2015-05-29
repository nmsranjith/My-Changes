    function ShowUpdate() {
        $("#UpdateProgressImg").css("display", "block");
    }
    function EndUpdateProgress() {
        $("#UpdateProgressImg").css("display", "none");
    }
    $(document).ready(function () {
        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
        // This will call the function after postback..
    });

    var checkedCount = 0;
    var checkallFlag = true;
    var totalCheckbx = 0;
	function AddSelectedStudent()
	{
		$('#SelectedStudentList').html('');
		$("#SelectedValueTextBox").val('');
		$('.gchkbx span').each(function(){
			if($(this).attr('class').trim()=='ico-check')
			{
				var teacherName = $(this).parent().parent().next().children('span.gausername').text().trim(),
				studID=$(this).parent().parent().children('input[type="checkbox"]').val().trim();				
				$("<li id='S" + studID + "' class=\'SelectedItem\'><span title=" + teacherName + ">" + (teacherName.length > 10 ? teacherName.substring(0, 9) + ' ...' : teacherName) + "</span><span style='display:none'>" +studID+ "</span><a onclick='Remove(this)'>x</a></li>").appendTo("#SelectedStudentList");
				$("#SelectedValueTextBox").val($("#SelectedValueTextBox").val().trim() + studID + ",");
			}
		});
	}
	function AutoSelectStudents()
	{
		var studsList=$("#SelectedValueTextBox").val().split(',');
		for(var i=0;i<studsList.length;i++)
		{
			$('#CheckDiv'+studsList[i]).children().click();		
		}
	}
	function PostBack() {	
	$('.gasaddbtn input[type="button"]').click(function(){
		$(this).parent().parent().parent().children().children('.gchkbx').children().click();
	});
	$('.gchkbx span').click(function(){
		var selGrps='';	
		var attr = $(this).attr('class');
		if (typeof attr !== typeof undefined && attr !== false && attr!='eg-hide') { 
			if(attr.trim()=='ico-uncheck')		
			{
				$(this).removeClass('ico-uncheck').addClass('ico-check');			
				$(this).parent().parent().next().children('div.gasaddbtn').children().val('REMOVE').attr('class','btn btn-general');				
			}
			else
			{
				$(this).removeClass('ico-check').addClass('ico-uncheck');
				$(this).parent().parent().next().children('div.gasaddbtn').children().val('ADD').attr('class','btn btn-affermative');
			}
			$(this).parent().parent().children('input[type="checkbox"]').click();
		}
		else
		return;
		var chkBxs=0,cChkBxs=0,type='',chkbxlen=0,chkedLen=0;
		chkbxlen=$('.gschkbx input[type="checkbox"]').length;
		chkedLen=$('.gschkbx input:checked').length;
		if(chkbxlen==chkedLen)
		{
			$('#SelectAllChkbx').attr('class','ico-check');
		}
		else
		{
			$('#SelectAllChkbx').attr('class','ico-uncheck');
		}
		AddSelectedStudent();
	});
	
	$('#SelectAllChkbx').click(function(){
		var allchkbx=$(this).attr('class');		
		if(allchkbx.trim()=='ico-uncheck')
		{
			$('.gchkbx span').each(function(){
				$(this).attr('class','ico-check');
				$(this).parent().parent().children('input[type="checkbox"]').click();		
				$(this).parent().parent().next().children('div.gasaddbtn').children().val('REMOVE').attr('class','btn btn-general');
			});
			$(this).attr('class','ico-check');	
		}
		else
		{
			$('.gchkbx span').each(function(){
				$(this).attr('class','ico-uncheck');	
				$(this).parent().parent().children('input[type="checkbox"]').click();
				$(this).parent().parent().next().children('div.gasaddbtn').children().val('ADD').attr('class','btn btn-affermative');
			});
			$(this).attr('class','ico-uncheck');
		}
		AddSelectedStudent();
	});
	
	AutoSelectStudents();
	jQuery("#groupssortdpn").kendoDropDownList({
		animation: false
	});
        if ($('#RoleChkHdn').val() == 'true') {
            jQuery("#Backtocreategroupbtn").parent().css('margin-top', '-295px');
        }

        if ($("#<%=AllotherGroupDivBtn.ClientID%>")[0].style.display == "none") {
            //$(".Addthrtedigrp")[0].style.paddingBottom = "0px";
        }
        var version = getInternetExplorerVersion();
        var searchAutoComplete = $("#SearchTextBox").data("kendoAutoComplete");
        if (searchAutoComplete == undefined) {
            $("#SearchTextBox").kendoAutoComplete({
                dataSource: {
                    transport: {
                        read: {
                            url: GetFile('/DesktopModules/eCollection_Groups/GroupsHandler.ashx?Search=studentAutoComplete'),
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
        //        var control;
        //        var currentPage = document.getElementById("pageNumber").value.split(',');
        //        var pageLength = currentPage[0] + 10;
        //        for (var p = currentPage[0]; p < pageLength; p++) {
        //            if (p == 0) {
        //                control = $("#PagerHolder #StudentPagerDiv #PageControl a")[p].attributes[3].value;
        //            }
        //            $("#PagerHolder #StudentPagerDiv #PageControl a")[p].attributes[3].value = control;
        //        }
        if ($.browser.msie) {
            $("#borderLine").css("margin-top", "0px");
            $("#ReadingLevelButton").css("margin-top", "0px");
            $("#SortingButton").css("margin-top", "0px");
        }
        if ($.browser.mozilla) {
            $("#borderLine").css("margin-top", "-1px");
            $("#ReadingLevelButton").css("margin-top", "0px");
            $("#SortingButton").css("margin-top", "0px");
        }

        if ($.browser.msie) {
            var textvalue = $("#SearchTextBox").val();
            $("#SearchTextBox").val('Enter your search here ...');
            if (version > -1) {
                if (version != 9.0 && version != 10.0) {
                    if (textvalue.trim().length != 0) {
                        $("#SearchTextBox").val(textvalue);
                    }
                    $("#SearchTextBox").css("line-height", "27.5px");
                }
                else {
                    $("#SearchTextBox").val(textvalue);
                }

            }
        }
        totalCheckbx = $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]").length;
        checkedCount = $('#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input:checked').length;
        CheckAllCheck();      
        jQuery('#eCollectionContent').height((jQuery('#StudentDashboard').height() + 80) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 9) + 'px');
        if ($.browser.msie || $.browser.mozilla) {
            $('#triggerEndlnk').css({ "padding-top": "6px", "margin-top": "-2px" });
        }
        $('#<%=SearchButton.ClientID%>').click(function () { document.getElementById("pageNumber").value = "0,1"; });
        $('#SearchTextBox').keypress(function (e) {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            var code = (e.keyCode ? e.keyCode : e.which);
            if ($("#SearchTextBox").length > 0 && $(this).val() != this.title) {
                $('#<%=SearchButton.ClientID%>').removeAttr("disabled").css("color", "Black");

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
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            var code = (e.keyCode ? e.keyCode : e.which);
            if ($("#SearchTextBox").length > 0 && $(this).val() != this.title) {
                $('#<%=SearchButton.ClientID%>').removeAttr("disabled").css("color", "Black");

                if (code == 13) {
                    e.preventDefault();
                    $('#<%=SearchButton.ClientID%>').click();
                }
            }
            else {
                if (code == 13) {
                    e.preventDefault();
                }
            }
        });
        if (document.getElementById("pageNumber").value == "1") {
            $("#PreviousButton").attr('disabled', 'disabled');
            $("#ShowPreviousButton").attr('disabled', 'disabled');
        }
        $("#triggerEndlnkbtn").addClass("dottedlink");
        $("#triggerFirstlnkbtn").addClass("dottedlink");


       
        $("#classSearchTextBox").kendoComboBox({
            placeholder: "Class Name",
            dataTextField: "Text",
            dataValueField: "Id",
            filter: "contains",
            dataSource: {
                transport: {
                    read: {
                        url: GetFile('/DesktopModules/eCollection_Groups/GroupsHandler.ashx?Search=GetClassBySubs'),
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        serverFiltering: true,
                        serverPaging: true,
                        pageSize: 20
                    }
                }
            }
        });


        if ($("#SelectedStudentList li").length >= 1) {
            for (var j = 0; j < $("#SelectedStudentList")[0].childNodes.length; j++) {
                var tis = $("#SelectedStudentList")[0].childNodes[j];
                if (tis.nodeName != "#text") {
                    var RefNode = $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]");
                    for (var i = 0; i < RefNode.length; i++) {

                        if ($("#SelectedStudentList")[0].childNodes[j].childNodes[1].innerHTML.trim() == RefNode.parent()[i].children[1].innerHTML.trim()) {
                            var check = $(RefNode.parent()[i].children[2]).attr("id");
                            var uncheck = $(RefNode.parent()[i].children[3]).attr("id");
                            $('#' + check).parent().children("input").attr('checked', 'checked');
                            jQuery('#' + check).parent().parent()[0].className = "rowclick GpStudentRptcontentinnerdiv";
                            jQuery('#' + check).hide();
                            jQuery('#' + uncheck).show();
                            checkedCount++;
                            $($('#' + check).parent().parent().children()[2].children[0]).val("REMOVE");
                            $($('#' + check).parent().parent().children()[2].children[0]).removeClass('GpAddStudentRptbtn');
                            $($('#' + check).parent().parent().children()[2].children[0]).addClass('GpAddStudentRptbtnDisable');
                            $($('#' + check).parent().parent().children()[2]).removeAttr("style");
                            $($('#' + check).parent().parent().children()[2]).attr("style", "border:1px solid rgb(206, 200, 200)");
                    
                        }
                    }


                }
            }
        }
        

    }
    
    function CheckAll(obj) {
        if (!checkallFlag) {
            checkedCount = $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]").length;
            var RefNode = $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]");
            for (var i = 0; i < RefNode.length; i++) {

                if (!RefNode.parent()[i].children[0].checked) {
                    var check = $(RefNode.parent()[i].children[2]).attr("id");
                    var uncheck = $(RefNode.parent()[i].children[3]).attr("id");
                    $('#' + check).parent().children("input").attr('checked', 'checked');
                    jQuery('#' + check).parent().parent()[0].className = "rowclick GpStudentRptcontentinnerdiv";
                    jQuery('#' + check).hide();
                    jQuery('#' + uncheck).show();
                    if ($("#SelectedStudentList li").length >= 1) {
                        for (var j = 0; j < $("#SelectedStudentList")[0].childNodes.length; j++) {
                            var tis = $("#SelectedStudentList")[0].childNodes[j];
                            if (tis.nodeName != "#text") {
                                if ($("#SelectedStudentList")[0].childNodes[j].childNodes[1].innerHTML.trim() == $('#' + check).parent().children()[1].innerHTML.trim()) {
                                    document.getElementById("StudentNamespan").innerHTML = $('#' + check).parent().parent().children()[1].children[0].innerHTML.trim();
                                    return false;
                                }
                            }
                        }
                    }
                    $($('#' + check).parent().parent().children()[2].children[0]).val("REMOVE");
                    $($('#' + check).parent().parent().children()[2].children[0]).removeClass('GpAddStudentRptbtn');
                    $($('#' + check).parent().parent().children()[2].children[0]).addClass('GpAddStudentRptbtnDisable');
                    $($('#' + check).parent().parent().children()[2]).removeAttr("style");
                    $($('#' + check).parent().parent().children()[2]).attr("style", "border:1px solid rgb(206, 200, 200)");
                    var teacherName = $('#' + check).parent().parent().children()[1].children[0].innerHTML;
                    $("<li id='S" + check + "' class=\'SelectedItem\'><span title=" + teacherName + ">" + (teacherName.length > 10 ? teacherName.substring(0, 9) + ' ...' : teacherName) + "</span><span style='display:none'>" + $('#' + check).parent().children()[1].innerHTML.trim() + "</span><a onclick='Remove(this)'>x</a></li>").appendTo("#SelectedStudentList");
                    $("#SelectedValueTextBox").val($("#SelectedValueTextBox").val().trim() + $('#' + check).parent().children()[1].innerHTML.trim() + ",");
                    
                }

            }
            checkallFlag = true;
            var newsrc = GetFile("/Portals/0/images/tick_student.png");
            $("#SelectAllDiv").children("img").attr("src", newsrc);
            SetContentHeight();
        }
        else {
            var RefNode = $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]");
            for (var i = 0; i < RefNode.length; i++) {
                if (RefNode.parent()[i].children[0].checked) {

                    var check = $(RefNode.parent()[i].children[2]).attr("id");
                    var uncheck = $(RefNode.parent()[i].children[3]).attr("id");
                    UnCheck(check, uncheck);

                }
            }
            checkedCount--;
            checkallFlag = false;
            var newsrc = GetFile("/Portals/0/images/circle_big.png");
            $("#SelectAllDiv").children("img").attr("src", newsrc);
        }

    }

    function CheckAllCheck() {
        var totalCheckbx = $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]").length;
        if (checkedCount == totalCheckbx && totalCheckbx > 0) {
            var newsrc = GetFile("/Portals/0/images/tick_student.png");
            $("#SelectAllDiv").children("img").attr("src", newsrc);
            checkallFlag = true;
        }
        else {
            var newsrc = GetFile("/Portals/0/images/circle_big.png");
            $("#SelectAllDiv").children("img").attr("src", newsrc);
            checkallFlag = false;
        }
        SetContentHeight();
    }
    function SetContentHeight() {
        jQuery('#eCollectionContent').height((jQuery('#StudentDashboard').height() + 100) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 9) + 'px');
    }
    function Check(chk, unchk) {
        if ($($('#' + chk).parent().parent().children()[2].children[0]).val() == "ADD") {
            $('#' + chk).parent().children("input").attr('checked', 'checked');
            jQuery('#' + chk).parent().parent()[0].className = "rowclick GpStudentRptcontentinnerdiv";
            jQuery('#' + chk).hide();
            jQuery('#' + unchk).show();
            checkedCount++;
            $($('#' + chk).parent().parent().children()[2].children[0]).val("REMOVE");
            CheckAllCheck();
            var teacherName = $('#' + chk).parent().parent().children()[1].children[0].innerHTML;
            $("<li id='S" + chk + "' class=\'SelectedItem\'><span title=" + teacherName + ">" + (teacherName.length > 10 ? teacherName.substring(0, 9) + ' ...' : teacherName) + "</span><span style='display:none'>" + $('#' + chk).parent().children()[1].innerHTML.trim() + "</span><a onclick='Remove(this)'>x</a></li>").appendTo("#SelectedStudentList");
            $("#SelectedValueTextBox").val($("#SelectedValueTextBox").val().trim() + $('#' + chk).parent().children()[1].innerHTML.trim() + ",");
            $($('#' + chk).parent().parent().children()[2].children[0]).removeClass('GpAddStudentRptbtn');
            $($('#' + chk).parent().parent().children()[2].children[0]).addClass('GpAddStudentRptbtnDisable');
            $($('#' + chk).parent().parent().children()[2]).removeAttr("style");
            $($('#' + chk).parent().parent().children()[2]).attr("style", "border:1px solid rgb(206, 200, 200)");
            SetContentHeight();
        }
        else {
            $($('#' + chk).parent().parent().children()[2].children[0]).val("ADD");
            jQuery('#' + unchk).click();
        }
    }
    function UnCheck(chk, unchk) {
        if (checkedCount > 0)
            checkedCount--;
        CheckAllCheck();
        jQuery('#' + unchk).parent().children("input").removeAttr('checked');
        jQuery('#' + chk).parent().parent()[0].className = "GpStudentRptcontentinnerdiv";
        jQuery('#' + chk).show();
        jQuery('#' + unchk).hide();
        $('#S' + chk).remove();
        var selectedId = $('#' + chk).parent().children()[1].innerHTML.trim();
        $('#SelectedStudentList li').map(function (i, n) {
            if ($(n)[0].children[1].innerHTML == selectedId) {
                $(n)[0].parentNode.removeChild($(n)[0]);
            }
        });
        var SelectedValues = $("#SelectedValueTextBox").val().trim().split(',');
        document.getElementById("SelectedValueTextBox").value = " ";
        for (var i = 0; i < (SelectedValues.length - 1); i++) {
            if (SelectedValues[i].trim() != selectedId && SelectedValues[i].trim() != '') {
                $("#SelectedValueTextBox").val($("#SelectedValueTextBox").val().trim() + SelectedValues[i].trim() + ",")
            }
        }

        $($('#' + chk).parent().parent().children()[2].children[0]).addClass('GpAddStudentRptbtn');
        $($('#' + chk).parent().parent().children()[2].children[0]).removeClass('GpAddStudentRptbtnDisable');
        $($('#' + chk).parent().parent().children()[2].children[0]).val("ADD");
        $($('#' + chk).parent().parent().children()[2]).removeAttr("style");
        $($('#' + chk).parent().parent().children()[2]).attr("style", "border:1px solid #74A738");
        SetContentHeight();
    }


   
    function Remove(obj) {
        var SelectedValues = $("#SelectedValueTextBox").val().trim().split(',');
        var selectedVal = obj.parentNode.children[1].innerHTML.trim();
        document.getElementById("SelectedValueTextBox").value = " ";
        for (var i = 0; i < SelectedValues.length; i++) {
            if (SelectedValues[i].trim() != obj.parentNode.children[1].innerHTML && SelectedValues[i].trim() != '') {
                $("#SelectedValueTextBox").val($("#SelectedValueTextBox").val().trim() + SelectedValues[i].trim() + ",")
            }
        }
        
        var RefNode = $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]");
        for (var i = 0; i < RefNode.length; i++) {
            if (selectedVal == RefNode.parent()[i].children[1].innerHTML.trim()) {

                var check = $(RefNode.parent()[i].children[2]).attr("id");
                var uncheck = $(RefNode.parent()[i].children[3]).attr("id");
                $('#' + check).parent().children("input").removeAttr('checked');
                jQuery('#' + check).parent().parent()[0].className = "GpStudentRptcontentinnerdiv";
                jQuery('#' + check).show();
                jQuery('#' + uncheck).hide();
                if (checkedCount > 0)
                    checkedCount--;
                CheckAllCheck();
                $($('#' + check).parent().parent().children()[2].children[0]).val("ADD");
                $($('#' + check).parent().parent().children()[2].children[0]).removeClass('GpAddStudentRptbtnDisable');
                $($('#' + check).parent().parent().children()[2].children[0]).addClass('GpAddStudentRptbtn');
                $($('#' + check).parent().parent().children()[2]).removeAttr("style");
                $($('#' + check).parent().parent().children()[2]).attr("style", "border:1px solid #74A738");
            }
        }
        obj.parentNode.parentNode.removeChild(obj.parentNode);
        SetContentHeight();

    }
