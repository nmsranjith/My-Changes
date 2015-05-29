var dataSource;
var dataSource1 = null;
var currentPage = 0;
var lastPage = 0;
var count = 1;
var Pagerlength = 1;
var search = 0;
jQuery(function () {

    $('#teacherssortdpn').kendoDropDownList({
        animation: false
    });
    //populateListView('teacherslist&listType=initial');
    if (navigator.platform.indexOf("iPad") == 0) {
        $('#SrtDiv').css('width', '17%');
    }
    //        if (parseInt(jQuery('#SelTeachersCnt').val()) > 10) {
    //            UnCheckAll();
    //            $('#searcheditems').html($('#SearchTextBox').val());
    //            jQuery('#Pager').html('');
    //            $.ajax({
    //                url: GetFile('/DesktopModules/eCollection_Teachers/Handlers/eCollectionHandler.ashx?autocomplete=paging&pgitmscnt=0&pageno=-1'),
    //                dataType: "json",
    //                success: function (value) {
    //                    if (value > 0) {
    //                        var len = parseInt(value / 10);
    //                        if (len <= 0) {
    //                            jQuery('#Pager').html('');
    //                            jQuery('#Pager').hide();
    //                        }
    //                        else
    //                            jQuery('#Pager').show();
    //                        if (value % 10 > 0)
    //                            len++;
    //                        lastPage = len;
    //                        if (len > 10)
    //                            len = 10;
    //                        FirstPage();
    //                    }
    //                }
    //            });
    //            $('.PagerHdr').show();
    //        }


    //        $('#NamesAscButton').click(function () {
    //            $('#NamesAscButton').addClass('srtbtnshide');
    //            $('#NamesDescButton').removeClass('srtbtnshide');
    //            //SortTeachers('desc', 'Names');
    //            //return false;

    //        });

    //        $('#NamesDescButton').click(function () {
    //            $('#NamesAscButton').removeClass('srtbtnshide');
    //            $('#NamesDescButton').addClass('srtbtnshide');
    //            //SortTeachers('asc', 'Names');
    //            //return false;
    //        });

    //        jQuery('<a class="TeacherLstPagerBtns"><<</a>').appendTo(jQuery('.TeacherListPager').children().children('span')[0].parentNode);
    //        jQuery('<a class="TeacherLstPagerBtns"><</a>').appendTo(jQuery('.TeacherListPager').children().children('span')[1].parentNode);
    //        jQuery('<a class="TeacherLstPagerBtns">></a>').appendTo(jQuery('.TeacherListPager').children().children('span')[2].parentNode);
    //        jQuery('<a class="TeacherLstPagerBtns">>></a>').appendTo(jQuery('.TeacherListPager').children().children('span')[3].parentNode);

    $('#CreateReadingSessionButton').click(function () {
        var loginNames = '';
        $('#listView input:checked').each(function () {
            loginNames = loginNames + ',' + jQuery(this).val();
        });
        jQuery('#UserLoginNameHdn').val(loginNames);
    });

    $('#AddTeacherToGroupButton').click(function () {
        var loginNames = '';
        $('#listView input:checked').each(function () {
            loginNames = loginNames + ',' + jQuery(this).val(); // +',' + jQuery(this).next().html() + ',' + jQuery(this).parent().next().children().next().html();               
        });
        jQuery('#UserLoginNameHdn').val(loginNames);
    });

    var contentheight = jQuery('#eCollectionContent').height() + 100;
    jQuery('#eCollectionMenu').height((contentheight) + 'px');
    jQuery('#eCollectionContent').height((contentheight + 5) + 'px');


    jQuery('#SelectedTeacherId').val('');

    if ($.browser.msie && parseInt($.browser.version, 10) < 10) {
        $('#SearchTextBox').val('teacher name');
        $("#SearchTextBox").focus(function () {
            if ($(this).val() == 'teacher name') {
                $(this).val("");
            }

        });
        $("#SearchTextBox").blur(function () {
            if ($(this).val().trim() == "") {
                $('#SearchTextBox').val('teacher name');
            }

        });
    }

});
function ClickAZ() {
    if ($('#NamesAscButton').is(':visible')) {
        $('#NamesAscButton').click();
    }
    else {
        $('#NamesDescButton').click();
    }
}

function SubscriptionMismatch() {
    $("#MultipleSubsTeachers").css({ 'display': 'block' });
    $('.k-window-actions.k-header').css('cursor', 'pointer');
    kwindow = $("#MultipleSubsTeachers"); //Give ur div id here
    if (!kwindow.data("kendoWindow")) {
        kwindow.kendoWindow({
            width: "665px",
            height: "300px",
            modal: true,
            draggable: false
        });
        kwindow.data("kendoWindow").center();
    }
    kwindow.data("kendoWindow").open();
    $(".k-icon.k-i-close").hide();
    $('a.k-window-action.k-link').mouseover(function () {
        $('a.k-window-action.k-link').parent().addClass("popupClosebg");
        return false;
    });
    return false;
}

function SortTeachers(sortDirection, sortExpression) {
    UnCheckAll();
    var stuIds = "";
    var stuFullNames = "";
    var CustSubUserSks = "";
    var userLoginName = "";
    for (var i = 0; i < jQuery('#listView input[type="checkbox"]').length; i++) {
        stuIds = stuIds + ' ' + jQuery('#listView input[type="checkbox"]')[i].value
        stuFullNames = stuFullNames + ',' + jQuery('#listView input[type="checkbox"]')[i].parentNode.nextElementSibling.firstElementChild.innerHTML;
        CustSubUserSks = CustSubUserSks + ',' + jQuery('#listView input[type="checkbox"]')[i].nextElementSibling.innerHTML;
        userLoginName = userLoginName + ',' + jQuery('#listView input[type="checkbox"]')[i].parentNode.nextElementSibling.children[1].innerHTML;
    }
    populateListView('sorting&stuIds=' + stuIds + '&stuFullNames=' + stuFullNames + '&userLoginName=' + userLoginName + '&CustSubUserSks=' + CustSubUserSks + '&sortDir=' + sortDirection + '&sortExp=' + sortExpression);
}
function SetSelected(teacherid) {
    jQuery('#SelectedTeacherId').val(teacherid);
}
function populateListView(text) {
    dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: GetFile('/DesktopModules/eCollection_Teachers/Handlers/eCollectionHandler.ashx?autocomplete=' + text),
                dataType: "json"
            }
        },
        pageSize: 10
    });

    if (dataSource1 == null || count == 1) {
        dataSource1 = dataSource;
    }

    $("#listView").kendoListView({
        dataSource: dataSource,
        template: kendo.template($("#template").html())
    });
}

function GotoTop() {
    $('html, body').animate({ scrollTop: 80 }, 'slow');
}
var checkedcount = 0;
function EnableButtons(checkedcount) {
    if (checkedcount >= 1) {
        jQuery('#AddTeacherToGroupButton').removeAttr("disabled").removeClass('DbldBtn').addClass('BtnStyle');
        jQuery('#AddTeacherToGroupButton').parent().removeClass('DisabledAddButtonHolder').addClass('ActiveAddButtonsHolder');
        jQuery('#DeleteTeacherButton').removeAttr("disabled").removeClass('DbldDelBtn').addClass('CancelBtn canBtn');
        jQuery('#DeleteTeacherButton').parent().removeClass('DisabledDeleteButtonHolder').addClass('ActiveDeleteButtonHolder');
        jQuery('#CreateReadingSessionButton').removeAttr("disabled").removeClass('DbldBtn').addClass('BtnStyle');
        jQuery('#CreateReadingSessionButton').parent().removeClass('DisabledAddButtonHolder').addClass('ActiveAddButtonsHolder');

    }
    else {
        jQuery('#AddTeacherToGroupButton').attr('disabled', 'disabled').removeClass('BtnStyle').addClass('DbldBtn');
        jQuery('#AddTeacherToGroupButton').parent().removeClass('ActiveAddButtonsHolder').addClass('DisabledAddButtonHolder');
        jQuery('#DeleteTeacherButton').attr('disabled', 'disabled').removeClass('CancelBtn canBtn').addClass('DbldDelBtn');
        jQuery('#DeleteTeacherButton').parent().removeClass('ActiveDeleteButtonHolder').addClass('DisabledDeleteButtonHolder');
        jQuery('#CreateReadingSessionButton').attr('disabled', 'disabled').removeClass('BtnStyle').addClass('DbldBtn');
        jQuery('#CreateReadingSessionButton').parent().removeClass('ActiveAddButtonsHolder').addClass('DisabledAddButtonHolder');

    }
    if (checkedcount == jQuery('#listView input[type="checkbox"]').length && jQuery('#listView input[type="checkbox"]').length != 0) {
        jQuery("#CheckAllDiv").hide();
        jQuery("#UnCheckAllDiv").show();
    }
    else {
        jQuery("#CheckAllDiv").show();
        jQuery("#UnCheckAllDiv").hide();
    }
}
function Checked(teacherid) {
    jQuery('#CheckDiv' + teacherid).parent().children("input").attr('checked', 'checked');
    jQuery('#CheckDiv' + teacherid).toggle();
    jQuery('#UnCheckDiv' + teacherid).toggle();
    //jQuery("#CheckDiv" + teacherid).parent().parent()[0].className = "rowclick List_Contents";
    checkedcount++;
    EnableButtons(checkedcount);
}

function UnChecked(teacherid) {
    jQuery("#CheckDiv" + teacherid).parent().children("input").removeAttr('checked');
    jQuery("#CheckDiv" + teacherid).toggle();
    jQuery("#UnCheckDiv" + teacherid).toggle();
   // jQuery("#CheckDiv" + teacherid).parent().parent()[0].className = "List_Contents";
    if (checkedcount > 0)
        checkedcount--;
    EnableButtons(checkedcount);
}
function CheckAll() {
    jQuery('#SelectedTeacherId').val('');
    jQuery('#CheckAllDiv').hide();
    jQuery('#UnCheckAllDiv').show();
    $('#listView input[type="checkbox"]').attr('checked', 'true');
    jQuery('#listView input[type="checkbox"]').next().next().next().hide();
    jQuery('#listView input[type="checkbox"]').next().next().next().next().show();
    checkedcount = jQuery('#listView input[type="checkbox"]').length;
   /* for (var i = 0; i < jQuery('#listView input[type="checkbox"]').parent().parent().length; i++) {
        jQuery('#listView input[type="checkbox"]').parent().parent()[i].className = "rowclick List_Contents";
    }*/
    EnableButtons(checkedcount);
}

function UnCheckAll() {
    jQuery('#SelectedTeacherId').val('');
    jQuery('#CheckAllDiv').show();
    jQuery('#UnCheckAllDiv').hide();
    $('#listView input[type="checkbox"]').removeAttr('checked');
    jQuery('#listView input[type="checkbox"]').next().next().next().show();
    jQuery('#listView input[type="checkbox"]').next().next().next().next().hide();
   /* for (var i = 0; i < jQuery('#listView input[type="checkbox"]').parent().parent().length; i++) {
        jQuery('#listView input[type="checkbox"]').parent().parent()[i].className = "List_Contents";
    }*/

    checkedcount = 0;
    EnableButtons(checkedcount);
}
function GetSelectedteachers() {
    jQuery('#Selectedteachers').val(jQuery('#SelectedTeacherId').val());
}



var dkwindow;
var deleteFlag;
$("#DeleteTeacherButton").click(function () {
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
    });
    jQuery('#UserLoginNameHdn').val(stuids);
    return true;
});
$("#YesButton").click(function () {
    dkwindow.data("kendoWindow").close();
    deleteFlag = true;
    $("#DeleteTeacherButton").click();
    return false;
});
$("#NoButton").click(function () {
    dkwindow.data("kendoWindow").close();
    return false;
});
$("#OkButton").click(function () {
    // UnCheckAll();
    $('#listView input:checked').each(function () {
        Checked(jQuery(this).val());
    });
    kwindow.data("kendoWindow").close();
    return false;
});



$(document).ready(function () {

    var data = new kendo.data.DataSource({
        transport: {
            read: {
                url: GetFile('/DesktopModules/eCollection_Teachers/Handlers/eCollectionHandler.ashx?autocomplete=names'),
                type: "GET",
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }
        }
    });

    //create AutoComplete UI component
    $("#SearchTextBox").kendoAutoComplete({
        dataSource: data,
        filter: "contains",
        placeholder: "Enter your search here...",
        separator: ", "
    });


    $('#SearchTextBox').keypress(function (e) {
        if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
            //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');  && $(this).val() != this.title
            //return false;
        }

        var code = (e.keyCode ? e.keyCode : e.which);
        if ($("#SearchTextBox").length > 0) {

            if (code == 13) {
                e.preventDefault();
                $('#SearchButton').click();
            }
        }
        else {
            if (code == 13) {
                e.preventDefault();
            }
        }
    });
    $('#SearchTextBox').keyup(function (e) {
        // $('#SearchTextBox').focus();
        if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
            //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
            //return false;
        }
        var code = (e.keyCode ? e.keyCode : e.which);
        if ($("#SearchTextBox").length > 0 && $(this).val() != this.title) {

            if (code == 13) {
                e.preventDefault();
                $('#SearchButton').click();
            }
        }
        else {
            if (code == 13) {
                e.preventDefault();
            }
        }
    });
    $('#SearchButton').click(function () {
        UnCheckAll();
    });

});

function ShowUpdate() {
    $("#UpdateProgressImg").css("display", "block");
}
function EndUpdateProgress() {
    $("#UpdateProgressImg").css("display", "none");
}
function ConstructPager(start, len) {
    jQuery('#Pager').html('');
    jQuery('<span class="SearchFirst" id="FirstPage" onclick="FirstPage()"><<</span><span class="SearchFirst" id="PreviousPage" onclick="PreviousPage()"><</span>').prependTo(jQuery('#Pager'));
    for (var i = start; i <= len; i++) {
        jQuery('<span class="SearchFirst" id="' + i + '" onclick="GetPage(' + i + ')">' + i + '</span>').appendTo(jQuery('#Pager'));
    }
    jQuery('<span class="SearchFirst" id="NextPage" onclick="NextPage()">></span><span class="SearchFirst" id="LastPage" onclick="LastPage()">>></span>').appendTo(jQuery('#Pager'));
}
function SetCurrentPage(pgno) {
    UnCheckAll();
    jQuery('#SelectedTeacherId').val('');
    currentPage = pgno;
}

function GetPage(pgNo) {
    if (pgNo != currentPage) {
        UnCheckAll();
        currentPage = pgNo;
        SearchResults();
    }
}

function FirstPage() {
    if (currentPage != 1) {
        currentPage = 1;
        if (lastPage > 10 && currentPage % 10 > 0) {
            ConstructPager(1, 10);
        }
        else
            ConstructPager(1, lastPage);
        SearchResults();
    }
}
function LastPage() {
    if (currentPage != lastPage) {
        currentPage = lastPage;
        if (lastPage > 10 && currentPage % 10 > 0) {
            ConstructPager(currentPage - parseInt(currentPage % 10) + 1, currentPage);
        }

        SearchResults();
    }
}
function PreviousPage() {
    if (currentPage != 1) {
        if (lastPage > 10 && currentPage % 10 == 1) {
            ConstructPager(currentPage - 10, currentPage - 1);
        }
        currentPage = currentPage - 1;
        SearchResults();
    }
}
function NextPage() {
    if (currentPage != lastPage) {
        currentPage = currentPage + 1;
        if (lastPage > 10 && currentPage % 10 == 1) {
            ConstructPager(currentPage, currentPage + Math.min(10, lastPage - currentPage));
        }
        SearchResults();
    }
}


function SearchResults() {

    if (search == 1)
        populateListView('paging&pageno=' + currentPage + '&pgitmscnt=' + jQuery('#listView input[type="checkbox"]').length + '&names=' + $('#searcheditems').html());
    else
        populateListView('paging&pageno=' + currentPage + '&pgitmscnt=' + jQuery('#listView input[type="checkbox"]').length);
    jQuery('#LMAboveClassName').show();
    jQuery('#Pager span').each(function () {
        jQuery(this).removeClass('Highlight');
        if (jQuery(this).attr('id') == currentPage) {
            jQuery(this).addClass('Highlight');
        }
    });
    jQuery('#Pager span').addClass('SearchFirst').removeClass('pagerDisabled');
    if (currentPage == lastPage) {
        jQuery('#NextPage').removeClass('SearchFirst').addClass('pagerDisabled');
        jQuery('#LastPage').removeClass('SearchFirst').addClass('pagerDisabled');
    }
    if (currentPage == 1) {
        jQuery('#FirstPage').removeClass('SearchFirst').addClass('pagerDisabled');
        jQuery('#PreviousPage').removeClass('SearchFirst').addClass('pagerDisabled');
    }

}
