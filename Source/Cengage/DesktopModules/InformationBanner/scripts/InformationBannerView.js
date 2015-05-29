function DeleteBannerAlerts(e) {
    var t = $(e).next().val();
    $.ajax({
        url: GetFile("/DesktopModules/InformationBanner/Handlers/AlertDeleteHandler.ashx?alertid=" + t + "&username=" + $("#InfoBannerUserName").val() + "&pageUrl=" + $('#PageUrlHdn').val()),
        dataType: "json",
        success: function(t) {
            if (t != 0) {
                $(e).parent().remove()
            }
        }
    })
}

