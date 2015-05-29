
function ShowUpdate() {
    checkedcount = 0;
    EnableButtons(checkedcount);
    $("#UpdateProgressImg").css("display", "block");
}
function EndUpdateProgress() {
    $("#UpdateProgressImg").css("display", "none");
}
function ShowClassMembers(gid, cid,sid) {
	if($(sid).text().trim()=='Hide')
		$(sid).text('Show');
	else
		$(sid).text('Hide');

    $('#ContainerId').val(cid);
    $('#ClassId').val(gid);
    if ($('#Class' + gid).is(':visible')) {
        $('#Class' + gid).fadeToggle("slow", "linear");
    }
    else {
        $('#Class' + gid).fadeToggle("slow", "linear");
    }
    SetStudentsListHeight();
    if (jQuery("#ClassListDiv" + gid).attr('class') == 'ClassnameSpan') {
        jQuery("#ClassListDiv" + gid).addClass('ClassNameSpanDown');
    }
    else {
        jQuery("#ClassListDiv" + gid).removeClass('ClassNameSpanDown');
    }
		
		
}

function ShowClass() {
    $('.ClassMembersList').each(function () {
        if ($(this).children().first().html().trim() == '') {
            $(this).parent().hide();
            $(this).parent().children().first().hide();
        }
        else {
            $(this).show();
            SetStudentsListHeight();
            SetStudentsListHeight();
            $(this).parent().show();
            $(this).parent().children().first().show();
        }
    });
}

function SetStudentsListHeight() {
    $('#eCollectionContent').height($('#StudentListDiv').height() + 120);
    $('#eCollectionMenu').height($('#StudentListDiv').height() + 111);
}

function ClickPMRL() {
    if ($('#ReadingLvlAscButton').is(':visible')) {
        $('#ReadingLvlAscButton').click();
    }
    else {
        $('#ReadingLvlDescButton').click();
    }

}

function ClickAZ() {
    if ($('#NamesAscButton').is(':visible')) {
        $('#NamesAscButton').click();
    }
    else {
        $('#NamesDescButton').click();
    }

}

var currentPage = 0;
var lastPage = 0;
var sPgr = 1;

var dataSource;
var dataSource1 = null;
var currentPage = 1;
var count = 1;
var Pagerlength = 1;
var search = 0;
var dataSource2 = null;


jQuery(function () {
    var data = new kendo.data.DataSource({
        transport: {
            read: {
                url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=names'),
                dataType: "json",
                serverFiltering: true
            }
        }
    });
    $("#SearchTextBox").kendoAutoComplete({
        dataSource: data,
        filter: "contains",
        placeholder: "student name",
        separator: ","
    });
    //$('.ClassSearchDiv').first().click();
    $('#SearchTextBox').focus();
    PostBack();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
});

function PostBack() {


    $('#SearchTextBox').keypress(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            e.preventDefault();
            var ex = jQuery.Event("keyup"); // or keypress/keydown
            ex.keyCode = 27; // for Esc
            $(document).trigger(ex); // trigger it on document
            $('#SearchButton').click();
        }

    });

    $('#SearchTextBox').keyup(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            e.preventDefault();
            var e = jQuery.Event("keyup"); // or keypress/keydown
            e.keyCode = 27; // for Esc
            $(document).trigger(e); // trigger it on document
            $('#SearchButton').click();
        }

    });
    $('#SearchTextBox').val($('#SearchTextBox').val());
    if (navigator.platform.indexOf("iPad") == 0) {
        $('#SrtDiv').css('width', '38%');
    }
    if (parseInt(jQuery('#StudentsCount').val()) == 0)
        jQuery('#StudentListDiv').hide();
    else if (parseInt(jQuery('#StudentsCount').val()) <= 10)
        jQuery('#LoadAllStudentDiv').parent().hide();

    SetStudentsListHeight();

    //$('.ClassSearchDiv').first().click();

    if ($.browser.msie && parseInt($.browser.version, 10) < 10) {
        $('#SearchTextBox').val('student name');
        $("#SearchTextBox").focus(function () {
            if ($(this).val() == 'student name') {
                $(this).val("");
            }

        });
        $("#SearchTextBox").blur(function () {
            if ($(this).val().trim() == "") {
                $('#SearchTextBox').val('student name');
            }

        });
    }
	jQuery("#studentssortdpn").kendoDropDownList({
	    animation: false
    });

    /*$('#studentssortdpn').change(function () {
        switch($(this).val())
		{
			case "1":
				 $('#ReadingLvlDescButton').click();
			break;
			case "2":
				$('#NamesDescButton').click();
    		break;
			case "3":				
				$('#NamesAscButton').click();
			break;
		}
		return false;
    });*/
    $('#CreateReadingSessionButton').click(function () {
        var loginNames = '';
        $('#listView input:checked').each(function () {
            loginNames = loginNames + ',' + jQuery(this).val() + ',' + jQuery(this).next().html() + ',' + jQuery(this).parent().next().children().first().children().last().html();
            jQuery('#UserLoginNameHdn').val(loginNames);
        });
        /******************/
        var ids = 0;
        $('#classlist input:checked').each(function () {
            ids = ids + ',' + jQuery(this).val() + ',' + jQuery(this).next().html() + ',' + jQuery(this).parent().next().children().first().children().last().html();
        });
        ids = ids.split(',');
        var unique = ids.filter(function (itm, i, ids) {
            return i == ids.indexOf(itm);
        });
        ids = unique;
        ids.shift();
        jQuery('#UserLoginNameHdn').val(ids);
    });

    $('#AddStudentToGroupButton').click(function () {
        var loginNames = '';
        $('#listView input:checked').each(function () {
            loginNames = loginNames + ',' + jQuery(this).val() + ',' + jQuery(this).next().html() + ',' + jQuery(this).parent().next().children().first().children().last().html();
            jQuery('#UserLoginNameHdn').val(loginNames);
        });
        /******************/
        var ids = 0;
        $('#classlist input:checked').each(function () {
            ids = ids + ',' + jQuery(this).val() + ',' + jQuery(this).next().html() + ',' + jQuery(this).parent().next().children().first().children().last().html();
        });
        ids = ids.split(',');
        var unique = ids.filter(function (itm, i, ids) {
            return i == ids.indexOf(itm);
        });
        ids = unique;
        ids.shift();
        jQuery('#UserLoginNameHdn').val(ids);
    });

    $('#PrintStudentCardButton').click(function () {
        var stuids = 0;
        $('#listView input:checked').each(function () {
            stuids = stuids + ',' + jQuery(this).val();
            jQuery('#SelectedStuds').val(stuids);
        });
        /******************/
        var ids = 0;
        $('#classlist input:checked').each(function () {
            ids = ids + ',' + jQuery(this).val();
        });
        ids = ids.split(',');
        var unique = ids.filter(function (itm, i, ids) {
            return i == ids.indexOf(itm);
        });
        ids = unique;
        ids.shift();
        jQuery('#SelectedStuds').val(ids);
    });


    jQuery('#SelectedStuds').val('');

    if ($.browser.mozilla) {
        $('.StudentList_TopDiv').addClass('StudentList_TopDivFX');
    }
    else if ($.browser.msie)
        $('.StudentList_TopDiv').addClass('StudentList_TopDivIE');

}



function GotoTop() {
    $('html, body').animate({ scrollTop: 280 }, 'slow');
}
var checkedcount = 0;
function EnableButtons(checkedcount) {
    if (checkedcount >= 1) {
        jQuery('#AddStudentToGroupButton').removeAttr("disabled").removeClass('DbldBtn').addClass('BtnStyle');
        jQuery('#AddStudentToGroupButton').parent().removeClass('DisabledAddButtonHolder').addClass('ActiveAddButtonsHolder');
        jQuery('#PrintStudentCardButton').removeAttr("disabled").removeClass('DbldPrintBtn').addClass('PrintBtn');
        jQuery('#PrintStudentCardButton').parent().removeClass('DisabledAddButtonHolder').addClass('ActiveAddButtonsHolder');
        jQuery('#DeleteStudentButton').removeAttr("disabled").removeClass('DbldDelBtn').addClass('CancelBtn canBtn');
        jQuery('#DeleteStudentButton').parent().removeClass('DisabledDeleteButtonHolder').addClass('ActiveDeleteButtonHolder');
        jQuery('#CreateReadingSessionButton').removeAttr("disabled").removeClass('DbldBtn').addClass('BtnStyle');
        jQuery('#CreateReadingSessionButton').parent().removeClass('DisabledAddButtonHolder').addClass('ActiveAddButtonsHolder');
    }
    else {
        jQuery('#AddStudentToGroupButton').attr('disabled', 'disabled').removeClass('BtnStyle').addClass('DbldBtn');
        jQuery('#AddStudentToGroupButton').parent().removeClass('ActiveAddButtonsHolder').addClass('DisabledAddButtonHolder');
        jQuery('#PrintStudentCardButton').attr('disabled', 'disabled').removeClass('PrintBtn').addClass('DbldPrintBtn');
        jQuery('#PrintStudentCardButton').parent().removeClass('ActiveAddButtonsHolder').addClass('DisabledAddButtonHolder');
        jQuery('#DeleteStudentButton').attr('disabled', 'disabled').removeClass('CancelBtn canBtn').addClass('DbldDelBtn');
        jQuery('#DeleteStudentButton').parent().removeClass('ActiveDeleteButtonHolder').addClass('DisabledDeleteButtonHolder');
        jQuery('#CreateReadingSessionButton').attr('disabled', 'disabled').removeClass('BtnStyle').addClass('DbldBtn');
        jQuery('#CreateReadingSessionButton').parent().removeClass('ActiveAddButtonsHolder').addClass('DisabledAddButtonHolder');
    }
    if (checkedcount == $('#classlist input[type="checkbox"]').length && $('#classlist input[type="checkbox"]').length > 0) {
        jQuery("#CheckAllDiv").hide();
        jQuery("#UnCheckAllDiv").show();
    }
    else {
        jQuery("#CheckAllDiv").show();
        jQuery("#UnCheckAllDiv").hide();
    }
}
function Checked(studentid) {
    jQuery('#CheckDiv' + studentid).parent().children("input").attr('checked', 'checked');
    jQuery('#CheckDiv' + studentid).toggle();
    jQuery('#UnCheckDiv' + studentid).toggle();
    jQuery("#CheckDiv" + studentid).parent().parent()[0].className = "rowclick";
    checkedcount++;
    EnableButtons(checkedcount);
}

function UnChecked(studentid) {
    jQuery("#CheckDiv" + studentid).parent().children("input").removeAttr('checked');
    jQuery("#CheckDiv" + studentid).toggle();
    jQuery("#UnCheckDiv" + studentid).toggle();
    jQuery("#CheckDiv" + studentid).parent().parent()[0].className = "";
    if (checkedcount > 0)
        checkedcount--;
    EnableButtons(checkedcount);
}
function CheckAll() {
    jQuery('#SelectedStuds').val('');
    jQuery('#CheckAllDiv').hide();
    jQuery('#UnCheckAllDiv').show();
    $('#listView input[type="checkbox"]').attr('checked', 'true');
    jQuery('#listView input[type="checkbox"]').next().next().next().hide();
    jQuery('#listView input[type="checkbox"]').next().next().next().next().show();
    for (var i = 0; i < jQuery('#listView input[type="checkbox"]').parent().parent().length; i++) {
        jQuery('#listView input[type="checkbox"]').parent().parent()[i].className = "rowclick";
    }

    checkedcount = jQuery('#listView input[type="checkbox"]').length;
    SelectAll();
    EnableButtons(checkedcount);
}

/***********  CHECK BOX  **************/
function SelectAll() {
    //  $('div.ClassSearchDiv').map(function () {
    $('#classlist input[type="checkbox"]').attr('checked', 'true');
    jQuery('#classlist input[type="checkbox"]').next().next().next().hide();
    jQuery('#classlist input[type="checkbox"]').next().next().next().next().show();
    jQuery('#classlist input[type="checkbox"]').parent().parent().addClass('rowclick');
    checkedcount = $('#classlist input[type="checkbox"]').length;
    //  });
}

function DeSelectAll() {
    //$('div.ClassSearchDiv').map(function () {
    $('#classlist input[type="checkbox"]').removeAttr('checked');
    jQuery('#classlist input[type="checkbox"]').next().next().next().next().hide();
    jQuery('#classlist input[type="checkbox"]').next().next().next().show();
    jQuery('#classlist input[type="checkbox"]').parent().parent().removeClass('rowclick');
    //  });
}
/************  END - CHECK BOX  *************/


function UnCheckAll() {
    jQuery('#SelectedStuds').val('');
    jQuery('#CheckAllDiv').show();
    jQuery('#UnCheckAllDiv').hide();
    $('#listView input[type="checkbox"]').removeAttr('checked');
    jQuery('#listView input[type="checkbox"]').next().next().next().show();
    jQuery('#listView input[type="checkbox"]').next().next().next().next().hide();
    for (var i = 0; i < jQuery('#listView input[type="checkbox"]').parent().parent().length; i++) {
        jQuery('#listView input[type="checkbox"]').parent().parent()[i].className = "";
    }

    checkedcount = 0;
    EnableButtons(checkedcount);
    DeSelectAll();
}
function GetSelectedStudents() {
    jQuery('#SelectedStudents').val(jQuery('#SelectedStuds').val());
}



var dkwindow;
var deleteFlag;
$("#DeleteStudentButton").click(function () {
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
            return false;
        });
        return false;
    }
    var stuids = '';
    $('#listView input:checked').each(function () {
        stuids = stuids + ',' + jQuery(this).val();
        jQuery('#SelectedStuds').val(stuids);
    });
    /******************/
    var ids = 0;
    $('#classlist input:checked').each(function () {
        ids = ids + ',' + jQuery(this).val();
    });
    ids = ids.split(',');
    var unique = ids.filter(function (itm, i, ids) {
        return i == ids.indexOf(itm);
    });
    ids = unique;
    ids.shift();
    jQuery('#SelectedStuds').val(ids);
    return true;
});
$("#YesButton").click(function () {
    dkwindow.data("kendoWindow").close();
    deleteFlag = true;
    $("#DeleteStudentButton").click();
    return false;
});
$("#NoButton").click(function () {
    dkwindow.data("kendoWindow").close();
    return false;
});

function PleseWait() {
    $('html, body').css("cursor", "auto").unbind('click'); ;
}

var sorttype = 'Names';
var sortdir = 'asc';  

