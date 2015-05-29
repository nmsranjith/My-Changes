    var checkallFlag = true;
    var popupFlag = false;

    jQuery(function () {
        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);

    });
	function AddSelectedTeacher()
	{
		$('#SelectedStudentList').html('');
		$("#SelectedValueTextBox").val('');
		$('.gchkbx span').each(function(){
			if($(this).attr('class').trim()=='ico-check')
			{
				var teacherName = $(this).parent().parent().next().children('span.gausername').text().trim(),
				studID=$(this).parent().parent().children('input[type="checkbox"]').val().trim();				
				$("<li id='S" + studID + "' class=\'SelectedItem SelectedTeacherItem\'><span title=" + teacherName + ">" + (teacherName.length > 10 ? teacherName.substring(0, 9) + ' ...' : teacherName) + "</span><span style='display:none'>" +studID+ "</span><a onclick='Remove(this)'>x</a></li>").appendTo("#SelectedStudentList");
				$("#SelectedValueTextBox").val($("#SelectedValueTextBox").val().trim() + studID + ",");
			}
		});
	}
	function AutoSelectTeachers()
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
		AddSelectedTeacher();
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
		AddSelectedTeacher();
	});
	AutoSelectTeachers();
		jQuery("#groupssortdpn").kendoDropDownList({
		 animation: false
		});
        if ($('#RoleChkHdn').val() == 'true') {
            jQuery("#Backtocreategroupbtn").parent().css('margin-top', '-245px');
        }


        var searchAutocomplete = $("#SearchTextBox").data("kendoAutoComplete");
        if (searchAutocomplete == undefined) {
            $("#SearchTextBox").kendoAutoComplete({
                dataSource: {
                    transport: {
                        read: {
                            url: GetFile('/DesktopModules/eCollection_Sessions/SessionHandler.ashx?SessionStatus=teacherAutoComplete'),
                            type: "GET",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json"
                        }
                    }
                },
                filter: "contains",
                separator: ", ",
                minLength: 1,
                placeholder: "Enter your search here..."
            });
        }
        var postSelectAll = 0;
        for (var i = 0; i < $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
            if ($("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                postSelectAll++;
                $("#ClassRepeaterDiv #ClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                $('#ClassRepeaterDiv #ClassRepeaterContentDiv #ClassRepeaterSubmit input[type=button]')[i].className = "SessionAddButton GpAddStudentRptbtnDisable";
                $('#ClassRepeaterDiv #ClassRepeaterContentDiv #ClassRepeaterSubmit input[type=button]')[i].parentNode.className = "dbldgreenBtn";
                $('#ClassRepeaterDiv #ClassRepeaterContentDiv')[i].className = "rowclick TeachersList_Contents";
                $('#ClassRepeaterDiv #ClassRepeaterContentDiv #ClassRepeaterSubmit input[type=button]')[i].value = "REMOVE";
            }
        }
        if (postSelectAll == $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length && postSelectAll != 0) {
            $("#SelectAll")[0].src = GetFile("/Portals/0/images/tick_student.png");
            checkallFlag = false;
        }
        else {
            $("#SelectAll")[0].src = GetFile("/Portals/0/images/circle_big.png");
        }                 

        $('#<%=SearchButton.ClientID%>').click(function () {
            document.getElementById("pageNumber").value = "0,1";
        });

        $('#SearchTextBox').keypress(function (e) {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            var code = (e.keyCode ? e.keyCode : e.which);
            if ($('#SearchTextBox').length > 0 && $(this).val() != this.title) {
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

        $('#SearchTextBox').keyup(function (e) {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            var code = (e.keyCode ? e.keyCode : e.which);
            if ($("#SearchTextBox").length > 0 && $(this).val() != this.title) {

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


    }

    function Remove(obj) {

        var SelectedValues = $("#SelectedValueTextBox").val().trim().split(',');
        document.getElementById("SelectedValueTextBox").value = " ";
        for (var i = 0; i < SelectedValues.length; i++) {
            if (SelectedValues[i].trim() != obj.parentNode.children[1].innerHTML && SelectedValues[i].trim() != '') {
                $("#SelectedValueTextBox").val($("#SelectedValueTextBox").val().trim() + SelectedValues[i].trim() + ",")
            }
        }
        obj.parentNode.parentNode.removeChild(obj.parentNode);



    }


    function RemoveGroup(obj) {

        var SelectedValues = $("#SelectedValueGroupTextBox").val().trim().split(',');
        document.getElementById("SelectedValueGroupTextBox").value = " ";
        for (var i = 0; i < SelectedValues.length; i++) {
            if (SelectedValues[i].trim() != obj.parentNode.children[1].innerHTML && SelectedValues[i].trim() != '') {
                $("#SelectedValueGroupTextBox").val($("#SelectedValueGroupTextBox").val().trim() + SelectedValues[i].trim() + ",")
            }
        }
        obj.parentNode.parentNode.removeChild(obj.parentNode);

    }

    function showleftline(res) {
        if (res == 1) {
            $('#RepeaterLftConntrLine').show();
            $('#LeftConnectorLine').show();
        }
        else {
            $('#RepeaterLftConntrLine').hide();
            $('#LeftConnectorLine').hide();
        }
    }

    function RemoveTeacher(obj) {
        var SelectedValues = $("#SelectedValueTeacherTextBox").val().trim().split(',');
        document.getElementById("SelectedValueTeacherTextBox").value = " ";
        for (var i = 0; i < SelectedValues.length; i++) {
            if (SelectedValues[i].trim() != obj.parentNode.children[1].innerHTML && SelectedValues[i].trim() != '') {
                $("#SelectedValueTeacherTextBox").val($("#SelectedValueTeacherTextBox").val().trim() + SelectedValues[i].trim() + ",")
            }
        }
        checkallFlag = true;
        obj.parentNode.parentNode.removeChild(obj.parentNode);
        $("#ClassRepeaterDiv #ClassRepeaterContentDiv img").map(function (i, n) {
            if ($(n)[0].parentNode.parentNode.children[1].children[0].innerHTML == obj.parentNode.children[1].innerHTML) {
                $(n)[0].parentNode.children[1].src = GetFile("/Portals/0/images/circle_big.png");
                $(n)[0].parentNode.children[0].checked = false;
                $(n)[0].parentNode.parentNode.children[2].children[0].children[0].disabled = false;
                $(n)[0].parentNode.parentNode.children[2].children[0].children[0].className = "SessionAddButton";
                $(n)[0].parentNode.parentNode.children[2].children[0].className = "greenBtn";
                $(n)[0].parentNode.parentNode.className = "TeachersList_Contents";
                $("#SelectAll")[0].src = GetFile("/Portals/0/images/circle_big.png");
                $(n)[0].parentNode.parentNode.children[2].children[0].children[0].value = "ADD";
            }
        });
    }
	
	    jQuery(document).ready(function () {
        jQuery('#HeaderBtn').addClass('FinishCreateProfile');
        jQuery('#bg').addClass('CancelBtn');
        jQuery('#PageHeaderButton').css({ 'padding-top': '2px', 'float': 'left', 'color': 'white', 'text-decoration': 'none' });

    });
   
    function ShowUpdate() {
        $("#UpdateProgressImg").css("display", "block");
    }
    function EndUpdateProgress() {
        $("#UpdateProgressImg").css("display", "none");
    }

    function AppendTeachers(teacherName, TeacherId) {
        $("#SelectedStudentList").append('<li class="SelectedTeacherItem"><span titile="TeacherName">' +
                    (teacherName.length > 10 ? teacherName.substring(0, 10) + ' ..' : teacherName) +
                    '</span><span style="display:none;">' +
                     +TeacherId+
                    '</span><a onclick="RemoveTeacher(this);">x</a></li>');
        $('#SelectedValueTeacherTextBox').val($('#SelectedValueTeacherTextBox').val()+','+TeacherId);
    }

    function SelectTeacher(control, type) {
        var checkbox;
        if (type == 'image') {
            checkbox = control;
        }
        else {
            checkbox = $(control).parent().parent().parent().children().children().next();
        }
        var src = $(checkbox).attr('src').split('/');        
        if (src[src.length - 1] == 'circle_big.png') {
            $(checkbox).attr('src', GetFile("/Portals/0/images/tick_student.png"));
            $(checkbox).parent().parent().addClass('rowclick');
            $(checkbox).parent().parent().children().next().next().children().removeClass('greenBtn').addClass('dbldgreenBtn');
            $(checkbox).parent().parent().children().next().next().children().children().addClass('GpAddStudentRptbtnDisable').val('REMOVE');
            $(checkbox).parent().children().first().attr('checked', 'checked');
            AppendTeachers($(checkbox).parent().parent().children().next().children().next().text(), $(checkbox).parent().parent().children().next().children().first().text());

        }
        else {
            $(checkbox).attr('src', GetFile("/Portals/0/images/circle_big.png"));
            $(checkbox).parent().parent().removeClass('rowclick');
            $(checkbox).parent().parent().children().next().next().children().addClass('greenBtn').removeClass('dbldgreenBtn');
            $(checkbox).parent().parent().children().next().next().children().children().removeClass('GpAddStudentRptbtnDisable').val('ADD');
            $(checkbox).parent().children().first().removeAttr('checked');
            var SelectedValues = $("#SelectedValueTeacherTextBox").val().trim().split(',');
            document.getElementById("SelectedValueTeacherTextBox").value = " ";
            for (var i = 0; i < SelectedValues.length; i++) {
                if (SelectedValues[i].trim() != $(checkbox).parent().parent().children()[1].children[0].innerHTML && SelectedValues[i].trim() != '') {
                    $("#SelectedValueTeacherTextBox").val($("#SelectedValueTeacherTextBox").val().trim() + SelectedValues[i].trim() + ",")
                }
            }

            $("#SelectedStudentList span").each(function () {
                if ($(checkbox).parent().parent().children().next().children().first().text() == $(this).text()) {
                    $(this).parent().remove();
                }
            });
        }        
        if ($('#RepeaterLftConntrLine input[type=checkbox]').length == $('#RepeaterLftConntrLine input:checked').length) {
            $('#SelectAll').attr('src', GetFile("/Portals/0/images/tick_student.png"));
        }
        else {
            $('#SelectAll').attr('src', GetFile("/Portals/0/images/circle_big.png"));
        }
    }
    function CheckAll(checkboxctrl) {
        var src = $(checkboxctrl).parent().children().children().first().attr('src').split('/');
        $("#SelectedStudentList .SelectedTeacherItem").remove();
        if (src[src.length - 1] == 'circle_big.png') {
            $(checkboxctrl).parent().children().children().first().attr('src', GetFile("/Portals/0/images/tick_student.png"));
            $('#RepeaterLftConntrLine input').attr('checked', 'checked');
            $('#RepeaterLftConntrLine input').next().attr('src', GetFile("/Portals/0/images/tick_student.png"));
            $('#RepeaterLftConntrLine .TeachersList_Contents').addClass('rowclick');
            $('#RepeaterLftConntrLine .SessionAddButton').addClass('GpAddStudentRptbtnDisable').val('REMOVE'); 
            $('#RepeaterLftConntrLine .SessionAddButton').parent().removeClass('greenBtn').addClass('dbldgreenBtn');            
            $('#RepeaterLftConntrLine .TeachersList_Contents').each(function () {          
                AppendTeachers($(this).children().next().children().next().text(), $(this).children().next().children().first().text());
            });            
        }
        else {
            $(checkboxctrl).parent().children().children().first().attr('src', GetFile("/Portals/0/images/circle_big.png"));
            $('#RepeaterLftConntrLine input').removeAttr('checked');
            $('#RepeaterLftConntrLine input').next().attr('src', GetFile("/Portals/0/images/circle_big.png"));
            $('#RepeaterLftConntrLine .TeachersList_Contents').removeClass('rowclick');
            $('#RepeaterLftConntrLine .SessionAddButton').removeClass('GpAddStudentRptbtnDisable').val('ADD');
            $('#RepeaterLftConntrLine .SessionAddButton').parent().addClass('greenBtn').removeClass('dbldgreenBtn');
        }
    }
	function ClickSelectAll() {
        jQuery("#SelectAll").click();
    }