    $(document).ready(function () {
        DoWork();
        jQuery('#UpgradeTrialTabHolder').addClass('selectedTabHolder');
        jQuery('#UpradeTrialCollectionTab').addClass('selectedTab');
    });

    function DoWork() {
        var flagStr;
        if ($("#upgradeFlaghdn").val() != "") {
            flagStr = $("#upgradeFlaghdn").val().split("-");

            if (flagStr[0] == 'Y') {
                $("#TeacherCheckDiv")[0].style.display = "block";
                $("#TeachDelDiv")[0].style.display = "none";
            }
            else {
                $("#TeacherCheckDiv")[0].style.display = "none";
                $("#TeachDelDiv")[0].style.display = "block";
            }

            if (flagStr[1] == 'Y') {
                $("#StudCheckDiv")[0].style.display = "block";
                $("#StudArchDiv")[0].style.display = "none";
            }
            else {
                $("#StudCheckDiv")[0].style.display = "none";
                $("#StudArchDiv")[0].style.display = "block";
            }
            if (flagStr[2] == 'Y') {
                $("#GrpCheckDiv")[0].style.display = "block";
                $("#GrpsDelDiv")[0].style.display = "none";
            }
            else {
                $("#GrpCheckDiv")[0].style.display = "none";
                $("#GrpsDelDiv")[0].style.display = "block";
            }
            if (flagStr[3] == 'Y') {
                $("#BooksCheckDiv")[0].style.display = "block";
                $("#BooksDelDiv")[0].style.display = "none";
            }
            else {
                $("#BooksCheckDiv")[0].style.display = "none";
                $("#BooksDelDiv")[0].style.display = "block";
            }
            if ($('#renewupgradehdn1').val() == 'renew'){
            if (flagStr[4] == 'Y') {
                $("#MoveStudDiv")[0].style.display = "block";
                $("#MoveStudArchDiv")[0].style.display = "none";
            }
            else {
                $("#MoveStudDiv")[0].style.display = "none";
                $("#MoveStudArchDiv")[0].style.display = "block";
            }
            }
        if (flagStr[3] == 'Y' && flagStr[0] == 'Y' ) //(flagStr[3] == 'Y' && flagStr[2] == 'Y' && flagStr[0] == 'Y') 
            {
                $("#DeleteHeadr")[0].style.display = "none";
            }
            
            else $("#DeleteHeadr")[0].style.display = "block";
            if (flagStr[1] == 'Y' && flagStr[2] == 'Y' && flagStr[4] == 'Y')//(flagStr[1] == 'Y')
                $("#ArchiveHeadr")[0].style.display = "none";
            else if (flagStr[4] == 'N' && $('#renewupgradehdn1').val() == 'upgrade' && (flagStr[1] == 'Y' && flagStr[2] == 'Y')) {
                $("#ArchiveHeadr")[0].style.display = "none";
            }
            else $("#ArchiveHeadr")[0].style.display = "block";
            if ( flagStr[4] == 'N' && flagStr[3] == 'N' && flagStr[2] == 'N' && flagStr[0] == 'N' && flagStr[1] == 'N') {
                $("#KeepHeadr")[0].style.display = "none";
            }
            else $("#KeepHeadr")[0].style.display = "block";
        }

        var MoveYearLevel;
        var flagString;
        flagString = $("#upgradeFlaghdn").val().split("-");

        $("#FinishBtn").click(function () {
            MoveYearLevel = true; // comment this line in future if popup is required
            $("#upgradeFlaghdn").val($("#upgradeFlaghdn").val() + "-N"); // comment this line in future if popup is required
            if (!MoveYearLevel && flagString[1] == 'Y') {
                $("#popupcontentUpg").css({ 'display': 'block' });
                $('.k-window-actions.k-header').css('cursor', 'pointer');
                dkwindow = $("#popupcontentUpg"); //Give ur div id here
                if (!dkwindow.data("kendoWindow")) {
                    dkwindow.kendoWindow({
                        width: "500px",
                        height: "264px",
                        modal: true,
                        draggable: false
                    });
                    dkwindow.data("kendoWindow").center();
                }
                dkwindow.data("kendoWindow").open();
                $(".k-icon.k-i-close").hide();
                $('a.k-window-action.k-link').mouseover(function () {
					EndUpdateProgress();
                    return false;
                });
				EndUpdateProgress();
                return false;
            }
			$('.load-srf').show();
            return true;
        });
        $("#YesButton").click(function () {
            $("#upgradeFlaghdn").val($("#upgradeFlaghdn").val() + "-Y");
            dkwindow.data("kendoWindow").close();
            MoveYearLevel = true;
            $("#FinishBtn").click();
            return true;
        });
        $("#NoButton").click(function () {
            $("#upgradeFlaghdn").val($("#upgradeFlaghdn").val() + "-N");
            dkwindow.data("kendoWindow").close();
            MoveYearLevel = true;
            $("#FinishBtn").click();
            return true;
        });
    }
	
	function EndUpdateProgress() {   
		$('.load-srf').hide();
		if ($.browser.msie && parseInt($.browser.version, 10) < 10) {
			$('#progressBar').hide();
		}
	}
