$(function () {
    jQuery('div.k-calendar-container').parent().attr('style', 'width:212px !important');
    jQuery('#AppDataTabHolder').addClass('selectedTabHolder');
    jQuery('#AppDataCollectionTab').addClass('selectedTab').css('padding', '11px 0px 0px 8px');
    if ($.browser.msie) {
        $('#ckblInfoDiv legend').css('margin-left', '22px');
    }
    if (navigator.platform.indexOf("iPad") == -1) {
        jQuery('#FromTextBox').kendoDatePicker({ format: "dd/MM/yyyy"
        });

        jQuery('#ToTextBox').kendoDatePicker({ format: "dd/MM/yyyy"
        });
    }
    $("#YesButton").click(function () {
        dkwindow.data("kendoWindow").close();
        return false;
    });
    $("#DateButton").click(function () {
        dkwindow.data("kendoWindow").close();
        return false;
    });
    $('#FromTextBox').change(function () {
        $('#hdnFrom').val($(this).val());
    });
    $('#ToTextBox').change(function () {
        $('#hdnTo').val($(this).val());
    });
    $('#ddlAccount').kendoDropDownList();

});
function HideUsers() {
    $('#u1').remove(); $('#u2').remove(); $('#u3').remove(); $('#u4').remove();
    $('#u5').remove(); $('#u6').remove(); $('#u7').remove(); $('#u8').remove();

    //        $('#u11').remove(); $('#u12').remove(); $('#u13').remove(); $('#u14').remove();
    //        $('#u15').remove(); $('#u16').remove(); $('#u17').remove(); $('#u18').remove();
}

function HideAccounts() {
}

function HideBooks() {
}

function HideSessions() {
}

function HideErrors() {
}

function ValidateSelection() {
    window.scroll(0, 0);
    $("#Select-message").css({ 'display': 'block' });
    $('.k-window-actions.k-header').css('cursor', 'pointer');
    dkwindow = $("#Select-message"); //Give ur div id here
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

function ValidateDateSelection() {
    window.scroll(0, 0);
    $("#SelectDate-message").css({ 'display': 'block' });
    $('.k-window-actions.k-header').css('cursor', 'pointer');
    dkwindow = $("#SelectDate-message"); //Give ur div id here
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