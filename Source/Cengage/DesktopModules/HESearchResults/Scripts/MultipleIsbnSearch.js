var misbnwindow = $("#MultipleISBNPopup");
$('#MulpleIsbnBtn').click(function () {
    $("#MultipleISBNPopup").css({ 'display': 'block' });
    $('.k-window-actions.k-header').css('cursor', 'pointer');
    misbnwindow = $("#MultipleISBNPopup"); //Give ur div id here
    if (!misbnwindow.data("kendoWindow")) {
        misbnwindow.kendoWindow({
            content: GetFile("deploy/DesktopModules/HESearchResults/Views/MultipleIsbnSearch.aspx"),
            modal: true,
            draggable: false
        });
        misbnwindow.data("kendoWindow").center();
    }
    misbnwindow.data("kendoWindow").open();
    $(".k-icon.k-i-close").hide();
    $('a.k-window-action.k-link').mouseover(function () {
        return false;
    });
});

$("#MultiISBNDismiss").click(function () {
    misbnwindow.data("kendoWindow").close();
    return false;
});
$('#MultiISBNSearchBtn').click(function () {
    var isbnText = $('#Isbntextarea').val();
    if (isbnText.trim() != '') {
        $('#MultiIsbnError').addClass('HideItems');
        $('#MultiISBNtextarea').val($('#Isbntextarea').val());
        var jsonData = {
            'SearchText': isbnText,
            'Division': GetQueryStringParams('division')
        }
        jsonData = JSON.stringify(jsonData);

        $.ajax({
            url: GetFile('deploy/desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=multipleisbn'),
            cache: false,
            type: 'POST',
            data: jsonData,
            async: false,
            success: function (data) {
                window.open(data.replaceAll('"', ''), '_parent');
            }
        });
    }
    else {
        $('#MultiIsbnError').removeClass('HideItems');
    }
    return false;
});
$('#MultipleISBNBrowseBtn').click(function () {
    $('#MultipleISBNBrowse').click();
});
$('#MultiIsbnCancelBtn').click(function () {
    $('#MultipleISBNBrowse').val('');
    SetFileName('');
});
$('#MultiIsbnConfirmBtn').click(function () {
    $.ajax({
        url: "desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=getisbns&path=" + $('#MultipleISBNBrwsVal').text(),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (a) {
            if (a.d) { } else { }
        }
    })
});