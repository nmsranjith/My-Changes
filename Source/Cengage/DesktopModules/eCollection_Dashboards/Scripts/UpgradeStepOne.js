    var steplist = 4;
    $(document).ready(function () {
        if ($('#jscheck').val() == 'one') {
            DoWork();
        }
        jQuery('#DashboardTabHolder').removeClass('selectedTabHolder');
        jQuery('#DashboardTab').removeClass('selectedTab');
        jQuery('#UpgradeTrialTabHolder').addClass('selectedTabHolder');
        jQuery('#UpradeTrialCollectionTab').addClass('selectedTab');
    });
    function DoWork() {
        if ($("#IsLicLesshdn").val() == 1 && $("#IsQtyLesshdn").val() == 1) {
            $("#StudCheck")[0].checked = false;
            $("#BooksCheck")[0].checked = false;
            $("#GroupsCheck")[0].checked = false;
            $("#MOveAllStudent")[0].checked = false;
            steplist = 1;
            ShowSteptwo();
        }
        else if ($("#IsLicLesshdn").val() == 1) {
            $("#StudCheck")[0].checked = false;
            $("#GroupsCheck")[0].checked = false;
            $("#MOveAllStudent")[0].checked = false;
            steplist = 2;
            ShowSteptwo();
        }
        else if ($("#IsQtyLesshdn").val() == 1) {
            $("#BooksCheck")[0].checked = false;
            steplist = 3;
            ShowSteptwo();
        }

        if ($("#BackClickhdn").val() != 0) {
            var str = $("#BackClickhdn").val().split("-");

            if (str[0] == 'N') {
                $("#TeachersCheckDiv")[0].style.color = "#BDBDBD";
                $("#TeachersCheckDiv p").attr('style', 'color :#BDBDBD');
                $("#TeachersCheckDiv .hypenspan").attr('style', 'color :#BDBDBD');
                $("#TchChkSpan").parent().prev()[0].checked = false;
            }
            setValidatedList();

            if (str[1] == 'N') {
                $("#StudentsCheckDiv")[0].style.color = "#BDBDBD";
                $("#StudentsCheckDiv p").attr('style', 'color :#BDBDBD');
                $("#StudentsCheckDiv .hypenspan").attr('style', 'color :#BDBDBD');
                $("#StdChkSpan").parent().prev()[0].checked = false;

                $('#GrpsCheckDiv').hide('slow');
                $('#MOveAllStudentDiv').hide('slow');
            }


            if (str[2] == 'N') {
                $("#GrpsCheckDiv")[0].style.color = "#BDBDBD";
                $("#GrpChkSpan").parent().prev()[0].checked = false;
            }
            if (str[3] == 'N') {
                $("#BoksCheckDiv")[0].style.color = "#BDBDBD";
                $("#BoksCheckDiv p").attr('style', 'color :#BDBDBD');
                $("#BoksCheckDiv .hypenspan").attr('style', 'color :#BDBDBD');
                $("#BkChkSpan").parent().prev()[0].checked = false;
            }
            if (str[4] == 'N') {
                $("#MOveAllStudentDiv")[0].style.color = "#BDBDBD";
//                $("#MOveAllStudentDiv p").attr('style', 'color :#BDBDBD');
//                $("#MOveAllStudentDiv .hypenspan").attr('style', 'color :#BDBDBD');
                $("#MoveStdChkSpan").parent().prev()[0].checked = false;
                
            }

        }
        setValidatedList();
        $("#TchChkSpan").click(function () {
            if ($("#TchChkSpan").parent().prev()[0].checked == true) {
                $("#TeachersCheckDiv")[0].style.color = "#BDBDBD";
                $("#TeachersCheckDiv p").attr('style', 'color :#BDBDBD');
                $("#TeachersCheckDiv .hypenspan").attr('style', 'color :#BDBDBD');
                $("#TchChkSpan").parent().prev()[0].checked = false;
            }
            else {
                $("#TeachersCheckDiv")[0].style.color = "#000";
                $("#TeachersCheckDiv p").attr('style', 'color : gray');
                $("#TeachersCheckDiv .hypenspan").attr('style', 'color :gray');
                $("#TchChkSpan").parent().prev()[0].checked = true;
            }
        });
        $("#StdChkSpan").click(function () {
            if ($("#StdChkSpan").parent().prev()[0].checked == true) {
                $("#StudentsCheckDiv")[0].style.color = "#BDBDBD";
                $("#StudentsCheckDiv p").attr('style', 'color :#BDBDBD');
                $("#StudentsCheckDiv .hypenspan").attr('style', 'color :#BDBDBD');
                $("#StdChkSpan").parent().prev()[0].checked = false;
                $("#GrpChkSpan").parent().prev()[0].checked = true;
                $("#MoveStdChkSpan").parent().prev()[0].checked = true;
                //$('#RenewUpgradeToogle').hide('slow'); //.removeClass('ShowItems').addClass('HideItems');      
                $('#GrpsCheckDiv').hide('slow');
                if($('#renewupgradehdn').val()=='renew')
                $('#MOveAllStudentDiv').hide('slow');
                $("#GrpChkSpan").click();
                $("#MoveStdChkSpan").click();
            }
            else {
                $("#StudentsCheckDiv")[0].style.color = "#000";
                $("#StudentsCheckDiv p").attr('style', 'color : gray');
                $("#StudentsCheckDiv .hypenspan").attr('style', 'color :gray');
                $("#StdChkSpan").parent().prev()[0].checked = true;
                //$('#RenewUpgradeToogle').show('slow'); //.removeClass('HideItems').addClass('ShowItems');
                $('#GrpsCheckDiv').show('slow');
                if ($('#renewupgradehdn').val() == 'renew')
                $('#MOveAllStudentDiv').show('slow'); 
            }
        });
        $("#MoveStdChkSpan").click(function () {
            if ($("#MoveStdChkSpan").parent().prev()[0].checked == true) {
                $("#MOveAllStudentDiv")[0].style.color = "#BDBDBD";
//                $("#MOveAllStudentDiv p").attr('style', 'color :#BDBDBD');
//                $("#MOveAllStudentDiv .hypenspan").attr('style', 'color :#BDBDBD');
                $("#MoveStdChkSpan").parent().prev()[0].checked = false;
              
            }
            else {
                $("#MOveAllStudentDiv")[0].style.color = "#000";
//                $("#MOveAllStudentDiv p").attr('style', 'color : gray');
//                $("#MOveAllStudentDiv .hypenspan").attr('style', 'color :gray');
                $("#MoveStdChkSpan").parent().prev()[0].checked = true;
    
            }
        });
        $("#GrpChkSpan").click(function () {
            if ($("#GrpChkSpan").parent().prev()[0].checked == true) {
                $("#GrpsCheckDiv")[0].style.color = "#BDBDBD";
                $("#GrpChkSpan").parent().prev()[0].checked = false;
            }
            else {
                $("#GrpsCheckDiv")[0].style.color = "#000";
                $("#GrpChkSpan").parent().prev()[0].checked = true;
            }
        });
        $("#BkChkSpan").click(function () {
            if ($("#BkChkSpan").parent().prev()[0].checked == true) {
                $("#BoksCheckDiv")[0].style.color = "#BDBDBD";
                $("#BoksCheckDiv p").attr('style', 'color :#BDBDBD');
                $("#BoksCheckDiv .hypenspan").attr('style', 'color :#BDBDBD');
                $("#BkChkSpan").parent().prev()[0].checked = false;
            }
            else {
                $("#BoksCheckDiv")[0].style.color = "#000";
                $("#BoksCheckDiv p").attr('style', 'color : gray');
                $("#BoksCheckDiv .hypenspan").attr('style', 'color :gray');
                $("#BkChkSpan").parent().prev()[0].checked = true;
            }
        });


        $("#ContinueBtn").click(function () {
            setValidatedList();
            var cnt = 0;
            var flag = "";
            if ($("#TeachersCheck")[0].checked == true) {
                flag = flag + "Y";
                cnt++;
            }
            else flag = flag + "N";

            if ($("#StudCheck")[0].checked == true) {
                if ($("#IsLicLesshdn").val() == 1) {
                    flag = flag + "-N";
                    $("#StudCheck")[0].checked = false;
                    $("#GroupsCheck")[0].checked = false;
                    $("#MOveAllStudent")[0].checked = false;
                }
                else {
                    flag = flag + "-Y"; cnt++;
                }

            }
            else {
                flag = flag + "-N";
                $("#GroupsCheck")[0].checked = false;
                $("#MOveAllStudent")[0].checked = false;
            }
            if ($("#GroupsCheck")[0].checked == true) {
                flag = flag + "-Y";
                cnt++;
            }
            else flag = flag + "-N";
            if ($("#BooksCheck")[0].checked == true) {
                if ($("#IsQtyLesshdn").val() == 1) {
                    flag = flag + "-N";
                    $("#BooksCheck")[0].checked = false;
                }
                else {
                    flag = flag + "-Y";
                    cnt++;
                }

            }
            else flag = flag + "-N";

            if ($("#MOveAllStudent")[0].checked == true) {
                flag = flag + "-Y";
                cnt++;
            }
            else flag = flag + "-N";
            $("#Upgradeflaghdn").val(flag);

            //            if (steplist == cnt) {                
            //                return;
            //            }
            //            var step = $("#UpgradeStepCnt").val();
            //            if ((cnt == 4) || (cnt == 2 && $("#IsLicLesshdn").val() == 1 && $("#IsQtyLesshdn").val() == 1) || (cnt == 3 && ($("#IsLicLesshdn").val() == 1 || $("#IsQtyLesshdn").val() == 1)))
            //                step = 2;
            //            if (step == 2) {
            //                return true;
            //            }
            //            else {

            //                if ($("#UpgradeStepCnt").val() == 1) {
            //                    $("#UpgradeStepCnt").val(2);
            //                    ShowSteptwo();
            //                    return false;
            //                }

            //            }
        });

        $("#BackBtn").click(function () {
            if ($("#UpgradeStepCnt").val() == 2) {
                $("#UpgradeStepCnt").val(1);
                BactToPreStep();
                return false;
            }
        });


    }


    function ShowSteptwo() {
        var cnt1 = 0;
        if ($("#TeachersCheck")[0].checked == false) {
            $("#TeachersCheckDiv")[0].style.display = "none";
            $("#WarningDiv").children()[2].innerHTML = "<p>-Teachers Information</p>"
        }
        else {
            $("#TeachersCheckDiv")[0].style.display = "block";
            cnt1++;
        }
        if (($("#StudCheck")[0].checked == false && $("#IsLicLesshdn").val() != 1) || $("#IsLicLesshdn").val() == 1) {
            $("#StudentsCheckDiv")[0].style.display = "none";
            $("#GroupsCheck")[0].checked = false;
            $("#MOveAllStudent")[0].checked = false;
            $("#WarningDiv").children()[2].innerHTML = $("#WarningDiv").children()[2].innerHTML + "<p>-Students Information</p>"
        }
        else if ($("#StudCheck")[0].checked == true) {
            $("#StudentsCheckDiv")[0].style.display = "block";
            cnt1++;
        }
        if ($("#MOveAllStudent")[0].checked == false || $("#IsLicLesshdn").val() == 1) {
            $("#MOveAllStudentDiv")[0].style.display = "none";
            $("#WarningDiv").children()[2].innerHTML = "<p>-Move All Students Information</p>"
        }
        else {
            $("#MOveAllStudentDiv")[0].style.display = "block";
            cnt1++;
        }
        if ($("#GroupsCheck")[0].checked == false || $("#IsLicLesshdn").val() == 1) {
            $("#GrpsCheckDiv")[0].style.display = "none";
            $("#WarningDiv").children()[2].innerHTML = $("#WarningDiv").children()[2].innerHTML + "<p>-Groups Information</p>"
        }
        else {
            $("#GrpsCheckDiv")[0].style.display = "block";
            cnt1++;
        }

        if (($("#BooksCheck")[0].checked == false && $("#IsQtyLesshdn").val() != 1) || $("#IsQtyLesshdn").val() == 1) {
            $("#BoksCheckDiv")[0].style.display = "none";
            $("#WarningDiv").children()[2].innerHTML = $("#WarningDiv").children()[2].innerHTML + "<p>-Books Information</p>"
        }
        else if ($("#BooksCheck")[0].checked == true) {
            $("#BoksCheckDiv")[0].style.display = "block";
            cnt1++;
        }
        if (cnt1 != 4) {
            $("#WarningDiv")[0].style.display = "block";
        }
        else $("#WarningDiv")[0].style.display = "none";
    }
    function setValidatedList() {
        if ($("#IsLicLesshdn").val() == 1) {
            $("#StudCheck")[0].checked = false;
            $("#StudentsCheckDiv")[0].style.display = "none";
            $("#GroupsCheck")[0].checked = false;
            $("#GrpsCheckDiv")[0].style.display = "none";
            $("#MOveAllStudent")[0].checked = false;
            $("#MOveAllStudentDiv")[0].style.display = "none";
        }
        if ($("#IsQtyLesshdn").val() == 1) {
            $("#BooksCheck")[0].checked = false;
            $("#BoksCheckDiv")[0].style.display = "none";
        }
    }
    function BactToPreStep() {
        var str = $("#Upgradeflaghdn").val();
        var string = str.split("-");
        if (string[0] == 'Y') {
            $("#TeachersCheckDiv")[0].style.display = "block";
            $("#TeachersCheck")[0].checked = true;
        }
        else {
            $("#TeachersCheckDiv")[0].style.display = "block";
            $("#TeachersCheck")[0].checked = false;
        }
        if (string[1] == 'Y') {
            $("#StudentsCheckDiv")[0].style.display = "block";
            $("#StudCheck")[0].checked = true;
        }
        else {
            $("#StudentsCheckDiv")[0].style.display = "block";
            $("#StudCheck")[0].checked = false;
            $('#GrpsCheckDiv').hide('slow');
            $("#MOveAllStudent")[0].checked = false;
            $('#MOveAllStudentDiv').hide('slow');
        }
        if (string[2] == 'Y') {
            $("#GrpsCheckDiv")[0].style.display = "block";
            $("#GroupsCheck")[0].checked = true;
        }
        else {
            $("#GrpsCheckDiv")[0].style.display = "block";
            $("#GroupsCheck")[0].checked = false;
        }
        if (string[3] == 'Y') {
            $("#BoksCheckDiv")[0].style.display = "block";
            $("#BooksCheck")[0].checked = true;
        }
        else {
            $("#BoksCheckDiv")[0].style.display = "block";
            $("#BooksCheck")[0].checked = false;
        }
        if (string[4] == 'Y') {
            $("#MOveAllStudentDiv")[0].style.display = "block";
            $("#MOveAllStudent")[0].checked = true;
        }
        else {
            $("#MOveAllStudentDiv")[0].style.display = "block";
            $("#MOveAllStudent")[0].checked = false;
        
        }
        $("#WarningDiv")[0].style.display = "none";
        $("#WarningDiv").children()[2].innerHTML = ""
        setValidatedList();
    }
