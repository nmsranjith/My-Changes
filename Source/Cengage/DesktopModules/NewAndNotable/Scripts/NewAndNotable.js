
    dkwindow2 = $("#NewNotableFavoritePopUp");
    $("#FavLoginBtn").click(function () {
        dkwindow2.data("kendoWindow").close();
        $('#loginlnk').click();
        return false;
    });
    $("#FavSignUpBtn").click(function () {
        dkwindow2.data("kendoWindow").close();
        window.location.href = '/signup';
        return false;
    });
	function SaveFavorite(prodId, favbtn) {
    if ($(favbtn).attr("class") == "btn btn-onoff") {
        $.ajax({
            url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=favorite&prodId=' + prodId + '&division=NA'),
            dataType: "json",
            success: function (data) {
                if (data == -1) {
                    $("#NewNotableFavoritePopUp").css({ 'display': 'block' });
                    $('.k-window-actions.k-header').css('cursor', 'pointer');
                    if (!dkwindow2.data("kendoWindow")) {
                        dkwindow2.kendoWindow({
                            modal: true,
                            draggable: false
                        });
                        dkwindow2.data("kendoWindow").center();
                    }
                    dkwindow2.data("kendoWindow").open();
                    $(".k-icon.k-i-close").hide();
                    $('a.k-window-action.k-link').mouseover(function () {
                        return false;
                    });
                    return false;
                }
                else if (data == 50) {
                    $("#MoreNewNotableFavoritePopUp").css({ 'display': 'block' });
                    $('.k-window-actions.k-header').css('cursor', 'pointer');
                    if (!dkwindow4.data("kendoWindow")) {
                        dkwindow4.kendoWindow({
                            modal: true,
                            draggable: false
                        });
                        dkwindow4.data("kendoWindow").center();
                    }
                    dkwindow4.data("kendoWindow").open();
                    $(".k-icon.k-i-close").hide();
                    $('a.k-window-action.k-link').mouseover(function () {
                        return false;
                    });
                    return false;
                }
                else {
                    $('#FavSpan' + prodId).removeClass('HideItems').addClass("ico-favour-right");
                    $(favbtn).addClass('btn-on').val('UNFAVOURITE');
                }
            }
        });
    }
    else {
        $.ajax({
            url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=removefavorite&prodId=' + prodId + '&division=NA'),
            dataType: "json",
            success: function (data) {
                $(favbtn).removeClass('btn-on').val('FAVOURITE');
                $('#FavSpan' + prodId).addClass('HideItems').removeClass("ico-favour-right");
            }
        });
    }
}

$(function(){
	
});

function ShowNew(){}//$(".NewFlag").each(function(){"y"==$(this).text().toLowerCase().trim()||"new"==$(this).text().toLowerCase().trim()?$(this).text("New"):$(this).parent().addClass("HideItems")})}function NNGetQueryStringParams(e){for(var o=window.location.search.substring(1),n=o.split("&"),i=0;i<n.length;i++){var a=n[i].split("=");if(a[0]==e)return a[1]}}function SaveFavorite(e,o){$.ajax("btn btn-onoff"==$(o).attr("class")?{url:GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=favorite&prodId="+e+"&division="+NNGetQueryStringParams("division")),dataType:"json",success:function(n){return-1==n?($("#NewNotableFavoritePopUp").css({display:"block"}),$(".k-window-actions.k-header").css("cursor","pointer"),dkwindow2.data("kendoWindow")||(dkwindow2.kendoWindow({modal:!0,draggable:!1}),dkwindow2.data("kendoWindow").center()),dkwindow2.data("kendoWindow").open(),$(".k-icon.k-i-close").hide(),$("a.k-window-action.k-link").mouseover(function(){return!1}),!1):50==n?($("#MoreNewNotableFavoritePopUp").css({display:"block"}),$(".k-window-actions.k-header").css("cursor","pointer"),dkwindow4.data("kendoWindow")||(dkwindow4.kendoWindow({modal:!0,draggable:!1}),dkwindow4.data("kendoWindow").center()),dkwindow4.data("kendoWindow").open(),$(".k-icon.k-i-close").hide(),$("a.k-window-action.k-link").mouseover(function(){return!1}),!1):($("#FavSpan"+e).removeClass("HideItems").addClass("ico-favour-right"),void $(o).addClass("btn-on").val("UNFAVORITE"))}}:{url:GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=removefavorite&prodId="+e+"&division="+NNGetQueryStringParams("division")),dataType:"json",success:function(){$(o).removeClass("btn-on").val("FAVORITE"),$("#FavSpan"+e).addClass("HideItems").removeClass("ico-favour-right")}})}