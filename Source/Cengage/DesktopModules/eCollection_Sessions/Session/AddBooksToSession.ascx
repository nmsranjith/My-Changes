<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBooksToSession.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Sessions.Session.AddBooksToSession" %>
<%@ Register Src="~/Controls/eCollectionControls/ReadingLevelSlider.ascx" TagName="RLSlider"
    TagPrefix="RL" %>
    <%@ Register Src="CustomPaging.ascx" TagName="CustomPaging" TagPrefix="CP" %>
<script type="text/javascript">

    function changeGroupsStyle() {
        jQuery("#BooksTab").css("background-color", "#111E48");
    }

    function SetBookReadLevel() {

        var readLevel = $("#amount").val().split('-');
        var fromReadLvl = parseInt(readLevel[0]) + 1;
        var toReadLvl = readLevel[1];
        document.getElementById("SliderRange").value = $("#amount").val();
        $("#hiddenbtn").click();
    }
    function GetFwdPageNumber() {
        var pageno = 0;
        document.getElementById("SliderRange").value = $("#amount").val();
        pageno = parseInt(document.getElementById("pageNumber").value.trim());
        pageno++;
        document.getElementById("pageNumber").value = pageno;
    }
    function GetBckPageNumber() {
        var pageno = 0;
        document.getElementById("SliderRange").value = $("#amount").val();
        pageno = parseInt(document.getElementById("pageNumber").value.trim());
        pageno--;
        document.getElementById("pageNumber").value = pageno;
    }


    function GetItemsSelectedId() {

        if (Added) {

            var selectedID = document.getElementById("custItmSKHidden").value.split(',');
            document.getElementById("custItmSKHidden").value += e.parentNode.children[1].innerHTML.trim() + ",";
        }
        else if (Removed) {
            var index = selectedID.indexOf(e.parentNode.children[1].innerHTML.trim());
            if (index > -1)
                selectedID.splice(index, 1);

            var blkstr = $.map(selectedID, function (val, index) {
                var str = val;
                return str;
            }).join(",");

            document.getElementById("custItmSKHidden").value = blkstr;
        }
    }
    function ShowUpdate() {
        $("#UpdateProgressImg").css("display", "block");
    }
    function EndUpdateProgress() {
        $("#UpdateProgressImg").css("display", "none");
    }


    function CheckBoxImgClick(e) {
        //        if (e.parentNode.children[0].value == "true") {
        //            if (!e.parentNode.children[0].checked)
        //                e.parentNode.children[0].click();
        //        }
        //        else {
        //            if (e.parentNode.children[0].checked)
        //                e.parentNode.children[0].click();
        //        }        
        e.parentNode.children[0].click();
        var selectedID = document.getElementById("custItmSKHidden").value.split(',');
        if (e.parentNode.children[0].checked) {
            document.getElementById("custItmSKHidden").value += e.parentNode.children[1].innerHTML.trim() + ",";
            e.parentNode.parentNode.className = "bookRowStyle rowclick";

            e.parentNode.parentNode.children[3].children[0].className = "GpAddStudentRptbtnDisable";            
            e.parentNode.parentNode.children[3].children[0].value = "REMOVE";
            e.parentNode.parentNode.children[3].children[0].disabled = false;
            e.src = GetFile("/Portals/0/images/tick_student.png");
            var isGracePeriod = jQuery('#lblGracePeriod').text();
            if (isGracePeriod == 'False') {
                $('[id$=RemovetosubsButtontop]').attr('disabled', false);
                $('[id$=RemoveSubButtonbot]').attr('disabled', false);
            }
        }
        else {
            document.getElementById("custItmSKHidden").value = "";
            e.parentNode.parentNode.className="";
            e.parentNode.parentNode.className = "bookRowStyle";
            e.src = GetFile("/Portals/0/images/circle_big.png");
            e.parentNode.parentNode.children[3].children[0].disabled = false;
            var index = selectedID.indexOf(e.parentNode.children[1].innerHTML.trim());
            if (index > -1)
                selectedID.splice(index, 1);

            var blkstr = $.map(selectedID, function (val, index) {
                var str = val;
                return str;
            }).join(",");

            document.getElementById("custItmSKHidden").value = blkstr;
            $('[id$=RemovetosubsButtontop]').attr('disabled', true);
            $('[id$=RemoveSubButtonbot]').attr('disabled', true);
            e.parentNode.parentNode.children[3].children[0].className = "GpStudentRptcontentinthrbtn GpAddStudentRptbtn";
            e.parentNode.parentNode.children[3].children[0].value = "ADD";
        }
    }
    $(document).ready(function () {
        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
        // This will call the function after postback..
    });
    function PostBack() {
        if ($('#RoleChkHdn').val() == 'true') {
            jQuery("#Backtocreategroupbtn").parent().css('margin-top', '-245px');
        }
        var searchAutocomplete = $("#SearchTextBox").data("kendoAutoComplete");
        if (searchAutocomplete == undefined) {
            $("#SearchTextBox").kendoAutoComplete({
                dataSource: {
                    transport: {
                        read: {
                            url: GetFile('/DesktopModules/eCollection_Sessions/SessionHandler.ashx?SessionStatus=booksAutoComplete'),
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

        for (var i = 0; i < $('#BooksContentDiv #SelectedBookRepdiv  input[type=checkbox]').length; i++) {
            if ($('#BooksContentDiv #SelectedBookRepdiv  input[type=checkbox]')[i].checked) {                
                $('#BooksContentDiv #SelectedBookRepdiv  input[type=submit]')[i].className = "GpAddStudentRptbtnDisable";
                $('#BooksContentDiv #SelectedBookRepdiv  input[type=submit]')[i].value = "REMOVE";
                $('#BooksContentDiv #SelectedBookRepdiv  input[type=submit]')[i].disabled = false;
            }
        }
         
        pageName = 'Profile';
        if (navigator.userAgent.match(/AppleWebKit/) && !navigator.userAgent.match(/Chrome/)) {
            $('.Sliderholder').addClass('sliderDivProfSaf');
        }
        if ($.browser.msie) {
            $('.Sliderholder').addClass('sliderDivProfIE');
        }

        if ($.browser.mozilla) {
            $('.Sliderholder').addClass('sliderDivProfMoz');
        }

        $("#eCollectionContent").height($("#eCollectionContent").children()[0].offsetHeight);
        $('#eCollectionMenu').height((jQuery('#eCollectionContent').height()) + 'px');
        $("#<%=CategoriesDrpList.ClientID %>").kendoDropDownList({ animation: false });
        $("#<%=CategoriesDrpList.ClientID %>").parent().show();
        // jQuery("#slider-range").css({ "display": "inline-block", "margin-left": "30px", "margin-top": "10px" });
        jQuery('#HeaderBtn').addClass('FinishCreateProfile');
        jQuery('#bg').addClass('CancelBtn');
        jQuery('#PageHeaderButton').css({ 'padding-top': '2px', 'float': 'left', 'color': 'white', 'text-decoration': 'none' });


        $('#SearchTextBox').keypress(function (e) {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            var code = (e.keyCode ? e.keyCode : e.which);
            if ($('#SearchTextBox').length > 0 && $(this).val() != this.title) {
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
        
        $('#BooksContentDiv #SelectedBookRepdiv  input[type=submit]').click(function () {
            //if (!this.parentNode.parentNode.children[0].children[0].checked) {
                this.disabled = true;
                this.className = "GpAddStudentRptbtnDisable";
                this.parentNode.style.border = "1px solid lightgray";
                CheckBoxImgClick(this.parentNode.parentNode.children[0].children[2]);
                //alert(this.parentNode.parentNode.children[0].children[2].src);
                //if (!this.parentNode.parentNode.children[0].children[0].checked)
                //  this.parentNode.parentNode.children[0].children[0].click();
                // this.parentNode.parentNode.children[0].children[2].src = GetFile("/Portals/0/images/tick_student.png");
			if (this.value == "REMOVE") {
                this.parentNode.parentNode.className = "bookRowStyle rowclick";
            }
            //}
            return false;
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
        jQuery('#eCollectionContent').height((jQuery('#StudentListDiv').height() + 80) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
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
        jQuery('#eCollectionContent').height((jQuery('#StudentListDiv').height() + 80) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }


    function RemoveTeacher(obj) {

        var SelectedValues = $("#SelectedValueTeacherTextBox").val().trim().split(',');
        document.getElementById("SelectedValueTeacherTextBox").value = " ";
        for (var i = 0; i < SelectedValues.length; i++) {
            if (SelectedValues[i].trim() != obj.parentNode.children[1].innerHTML && SelectedValues[i].trim() != '') {
                $("#SelectedValueTeacherTextBox").val($("#SelectedValueTeacherTextBox").val().trim() + SelectedValues[i].trim() + ",")
            }
        }
        obj.parentNode.parentNode.removeChild(obj.parentNode);
        jQuery('#eCollectionContent').height((jQuery('#StudentListDiv').height() + 80) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
    
    
    
                 
</script>
<style type="text/css">
    #SelectedBookContentFrstdiv
	{
		width:8%;
	}
    .SearchInnerDiv
    {
        width: 647px;
        float: left;
    }
    .SearchInnerDiv .k-dropdown-wrap
    {
background: #707070 !important; 
box-shadow: 1px 1px 7px -1px #707070; 
height: 26px !important; 
border: 1px solid #707070 !important;
    }
    
.SearchInnerDiv .k-dropdown-wrap .k-input 
    {
        color: white !important; 
        margin-top: 4px !important;
    } 
    
 
    #SearchTextBox-list
    {
        width: 358px !important;
    }
    .Groupsearchtxt:hover
    {
        height: 30px;
        border: 0px solid transparent !important;
        padding-left: 7.5px !important;
        padding-top: 3px !important;
        background: transparent !important;
        width: 320px !important;
        box-shadow: none !important;
        font-size:10pt !important;
    }
    
         
   
    
    .GpStudentRptcontentinthrdiv
    {
        float: right;
        margin-top: -37px;
        background: -moz-linear-gradient(top, #689A2E 1%, #74A738 32%, #74A738 43%, #5E872E 95%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#689A2E), color-stop(32%,#74A738), color-stop(43%,#74A738), color-stop(95%,#5E872E));
        background: -webkit-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -o-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -ms-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: linear-gradient(to bottom, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#689A2E', endColorstr='#5E872E',GradientType=0 );
        border: 1px solid #97af82;
        text-indent: 0px;
        border-radius: 3px;
        -khtml-border-radius: 3px;
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        color: white;
        height: 35px;
        margin-right: 8px;
    }
    .GpStudentRptcontentinthrdiv:hover
    {
        background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#537A37), color-stop(32%,#59833A), color-stop(43%,#557D38), color-stop(95%,#436A32)) !important;
        background: -webkit-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%) !important;
        background: -o-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%) !important;
        background: -ms-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%) !important;
        background: linear-gradient(to bottom, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%) !important;
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#537A37', endColorstr='#436A32',GradientType=0 ) !important;
    }
    .GpStudentRptcontentinthrbtn
    {
        border: 0px solid #6C9E2D;
        width: 120px;
        height: 35px;
        text-indent: 19px;
        cursor: pointer;
        background: url('../../Portals/0/images/eye.png') 10% 48% no-repeat;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
        font-size: 10pt;
        text-shadow: 1px 2px 2px rgb(103, 141, 26);
    }
    .GpAddStudentRptbtn
    {
        background: -moz-linear-gradient(top, #689A2E 1%, #74A738 32%, #74A738 43%, #5E872E 95%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#689A2E), color-stop(32%,#74A738), color-stop(43%,#74A738), color-stop(95%,#5E872E));
        background: -webkit-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -o-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -ms-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: linear-gradient(to bottom, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#689A2E', endColorstr='#5E872E',GradientType=0 );
        font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
        font-size: 10pt;
        text-indent: 0px;
        text-shadow: 1px 2px 2px rgb(103, 141, 26);
        border-radius: 3px;
        -khtml-border-radius: 3px;
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        color: white;
    }
    .GpAddStudentRptbtn:hover
    {
        background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#537A37), color-stop(32%,#59833A), color-stop(43%,#557D38), color-stop(95%,#436A32)) !important;
        background: -webkit-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%) !important;
        background: -o-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%) !important;
        background: -ms-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%) !important;
        background: linear-gradient(to bottom, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%) !important;
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#537A37', endColorstr='#436A32',GradientType=0 ) !important;
    }
    .GpAddStudentRptbtnDisable
    {
        background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #EAE9E9 130%) repeat scroll 0 0 transparent !important;
        background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9)) !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0') !important;
        font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
        background: -ms-linear-gradient(top, #fdfdfd 5%, #E9E9E9 130%) !important;
        font-size: 10pt;
        text-indent: 0px;
        border-radius: 3px;
        -khtml-border-radius: 3px;
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        color: #707070;
        border: 1px solid lightgray !important;
        width: 120px;
        height: 35px;
    }
    .GpStudentRptcontentinnerdiv
    {
        border: 1px solid lightgray;
        width: 93.4%;
        height: 50px;
        margin-left: -30px;
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        border-radius: 3px;
        -khtml-border-radius: 3px;
        background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
        background: -ms-linear-gradient(top, white 5%, #EAE9E9 130%) !important;
    }

    .Groupsearchtxt
    {
        border: 0px solid transparent !important;
        width: 320px !important;
        height: 30px !important;
        float: left;
        padding-left: 7.5px !important;
        padding-top: 3px !important;
        background: transparent !important;
        box-shadow: none !important;
        font-size:10pt !important;
    }
    #CategoriesDrpList-list ul
    {
        background: -moz-linear-gradient(center top , white 0%, white 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#FDFDFD), to(#EAE9E9));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
        background: -ms-linear-gradient(top, white 5%, #EAE9E9 130%) !important;
    }
    #CategoriesDrpList-list .k-list > .k-state-selected
    {
        background: #707070 !important;
        border: 1px solid #707070 !important;
        color: White !important;
    }
    #CategoriesDrpList-list .k-list > .k-state-hover
    {
        background: #CCC !important;
        border: 1px solid #CCC !important;
        color: Black !important;
    }
    .SearchInnerDiv ::-webkit-input-placeholder
    {
        font-family: Raleway-regular,Raleway,Arial, Sans-Serif;
        font-size: 10pt;
        color: #707070;
    }
    .SearchInnerDiv .k-icon 
{
    background: url('/Portals/0/images/arrow_prim.png') no-repeat 0px 0px !important;       
    margin-top :5px !important; 
}
    
.Div_FullWidth  .k-icon
{
    background: url('/Portals/0/images/arrow_prim.png') no-repeat 0px 0px !important;        
    margin-top: 0px !important;    
    cursor: pointer; 
}
    .SearchInnerDiv ::-moz-placeholder
    {
        font-family: Raleway-regular,Raleway,Arial,Sans-Serif;
        opacity: 1;
        font-size: 10pt;
        color: #707070;
    }
    .SearchInnerDiv ::-ms-input-placeholder
    {
        font-family: Raleway-regular,Raleway,Arial,Sans-Serif;
        font-size: 10pt;
        color: #707070;
    }
    .SearchDiv
    {
        width: 647px;
        float: left;
        margin-left: 24px;
        margin-top: 2px;
        border: 1px solid #CCCCCC;
        height: 42px;
    }
    .bookRowStyle
    {
        height: 150px;
        width: 100%;
        float: left;
        border: 1px solid lightgray;
        margin-bottom: 20px;
        background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
        background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%);
        box-shadow: 1px 1px 5px lightgray;
    }
    .SelectedGroupItem
    {
        background-color: #67A024;
    }
    
    .SelectedStudentItem
    {
        background-color: #BB078A;
    }
    
    .SelectedTeacherItem
    {
        background-color: #21B4E6;
    }
    
    
    .SelectedGroupItem
    {
        float: left;
        min-width: 128px;
        background-image: none;
        border-radius: 0px;
        font-weight: bold;
        height: 28px;
        border: 1px solid lightgray;
        margin: 4px;
    }
    
    .SelectedStudentItem
    {
        float: left;
        min-width: 128px;
        background-image: none;
        border-radius: 0px;
        font-weight: bold;
        height: 28px;
        border: 1px solid lightgray;
        margin: 4px;
    }
    
    .SelectedTeacherItem
    {
        float: left;
        min-width: 128px;
        background-image: none;
        border-radius: 0px;
        font-weight: bold;
        height: 28px;
        border: 1px solid lightgray;
        margin: 4px;
    }
    
    .SelectedGroupItem span
    {
        float: left;
        display: inline;
        margin: 0px;
        margin-top: 6px;
        margin-left: 20px;
        margin-right: 20px;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, sans-serif;
        font-size: 9.4pt;
        font-weight: bold;
    }
    
    .SelectedTeacherItem span
    {
        float: left;
        display: inline;
        margin: 0px;
        margin-top: 6px;
        margin-left: 20px;
        margin-right: 20px;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, sans-serif;
        font-size: 9.4pt;
        font-weight: bold;
    }
    
    .SelectedStudentItem span
    {
        float: left;
        display: inline;
        margin: 0px;
        margin-top: 6px;
        margin-left: 20px;
        margin-right: 20px;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, sans-serif;
        font-size: 9.4pt;
        font-weight: bold;
    }
    .SelectedGroupItem a
    {
        float: right;
        margin-top: 5px;
        margin-right: 11px;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, sans-serif;
        text-decoration: none;
        cursor: pointer;
    }
    .SelectedTeacherItem a
    {
        float: right;
        margin-top: 5px;
        margin-right: 11px;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, sans-serif;
        text-decoration: none;
        cursor: pointer;
    }
    
    .SelectedStudentItem a
    {
        float: right;
        margin-top: 5px;
        margin-right: 11px;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, sans-serif;
        text-decoration: none;
        cursor: pointer;
    }
    
    
    .SelectedGroupItem a:hover
    {
        color: Red;
    }
    
    .SelectedStudentItem a:hover
    {
        color: Red;
    }
    .SelectedTeacherItem a:hover
    {
        color: Red;
    }
    
    .GroupAddtoinnersnddiv
    {
        float: left;
        width: 99%;
        height: auto;
        margin: 20px 0px 10px 10px;
        border: 0px solid lightGray;
    }
    
    #SelectedStudentList
    {
        list-style-type: none;
        margin: 0;
        padding: 0;
    }
    #SelectedStudentList li
    {
        list-style-type: none;
        display: inline;
    }
    #SelectedStudentList ul li
    {
        list-style-type: none;
        display: inline;
    }
    
    
    
    table tr:not(:last-child)
    {
        background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
        background: -ms-linear-gradient(top, white 5%, #EAE9E9 130%) !important;
        border: 1px solid lightgray;
    }
    select
    {
        height: 33px;
    }
   
    .ui-widget-header
    {
        border: 0px solid transparent !important;
    }
    .Sliderholder
    {
        width: 97% !important;
        margin-left: 0px !important;
    }
    .sliderDivProfSaf
    {
        width: 94.4% !important;
    }
    .sliderDivProfIE
    {
        width: 96% !important;
    }
    .sliderDivProfMoz
    {
        width: 97.1% !important;
    }
    .Searchbtndiv
    {
        position: relative;
        border-left: 1px solid #CCCCCC !important;
        width: 34px !important;
        height: 34px !important;
        float: right !important;
        background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent !important;
        background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9)) !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0') !important;
        background: -ms-linear-gradient(top, white 5%, #EAE9E9 130%) !important;
    }
    .Searchbtndiv input[type=submit]
    {
        cursor: pointer;
        border: 0px;
        float: left;
        width: 14px;
        margin-top: 7px;
        margin-left: 10px;
        background-color: transparent !important;
        height: 24px;
        background-image: url(../Portals/0/images/search_icon.png');
        background-repeat: no-repeat;
        background-position: center;
    }
  

.page:hover
    {
        background: -webkit-gradient(linear, left top, left bottom, from(#707070), to(#707070));
        background: -moz-linear-gradient(to bottom,#707070, #707070);
        background: progid:DXImageTransform.Microsoft.gradient(startColorstr='#707070', endColorstr='#707070', gradientType='0'); /*filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#707070', endColorstr='#707070', gradientType='0');*/
        background: -ms-linear-gradient(top, #707070 5%, #707070 130%) !important;
        background-color: #707070;
        color: White !important;
    }
    .Pager a[disabled]:hover
    {
        background: -webkit-gradient(linear, left top, left bottom, from(#F9F9F9), to(#E9E9E9));
        background: -moz-linear-gradient(to bottom,#F9F9F9, #E9E9E9);
        background: progid:DXImageTransform.Microsoft.gradient(startColorstr='#F9F9F9', endColorstr='#E9E9E9', gradientType='0'); /*filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#F9F9F9', endColorstr='#F9F9F9', gradientType='0');*/
        background: -ms-linear-gradient(top, #F9F9F9 5%, #E9E9E9 130%) !important;
        background-color: #E9E9E9;
    }
    input:-moz-placeholder
    {
        font-family: Raleway,Arial,Sans-Serif;
        font-size: 10pt;
        color: #707070;
        opacity: 1;
    }
    #PageControl
    {
        margin-left:0px !important;
    }
    #RightNavigationContainer
    {
        width:12.8% !important;
    }
    .BooksImgClass
    {
        margin-top: 4px; 
        margin-left: 5px;                
    }    
    .eCollectionContentStyle ,.eCollectionMenuStyle
    {
        padding-bottom:40px;
    }
</style>
<%--<div style="width:98%;float:left;margin-top: 16px;">
<div   style="width:99%; margin-top: 4px;height:auto;border: 1px solid lightGrey;float:left;margin-left:15px;margin-bottom:9px;">
    <div style="float: left;width:10%; margin: 25px 0px 10px 6px;">
    <span style="float: left;position: absolute;width: 114px;padding-top: 4px;padding-left: 3px;" class="Session">Attached books:</span>
    </div>   
    <div id="DetailsDiv" runat="server" style="float: left; width: 60%; margin: 25px 0px 10px 1px;height:auto;overflow:hidden"></div>       
<div style="float: right; margin: 1px 0px 10px 1px;">
    <asp:Button ID="BackToSession" 
        style="cursor: pointer; float: right;
            margin-right: 16px; padding: 10px; background-image: url('../Portals/0/images/GoBack.png');
            font-size: 8.45pt; color: white; padding-left: 35px; height: 37px; border: 0px solid transparent;
            width: 124px; margin-top:12px;"
        Enabled="true"  runat="server" OnClick="BackToSession_Click" /></div>
</div>
<div style="float:left;width:99%;margin-left: 16px; border:1px solid lightGrey;margin-top: 0px;height:40px;">
<asp:TextBox ID="BookSearchTextBox" runat="server" style="border:0px solid white;width: 320px;height: 37px;position: absolute;"></asp:TextBox>
<asp:Button ID="BookSearchButton" runat="server" Text="Search" style="color:#1479A4; float:right;width: 173px;height: 33px;margin-top: 3px;margin-right: 3px;cursor:pointer; box-shadow: 1px 2px lightgray;border-radius: 5px;background-color: whiteSmoke;font-weight: bold;background-image: url('../../Portals/0/images/Levelbg.png');background-repeat: repeat;" />
 <select id="CategoriesDropDown" runat="server" style="float: right;margin-top: 4px;margin-right: 41px;color:#1479A4;">
                <option>Text type</option>
                <option>Levels</option>
                <option>Reading</option>
            </select>
</div>
<div style="float: left;width: 99%;margin-left: 16px;border: 1px solid lightgray;margin-top: 14px;height: 100px;">
 <RL:RLSlider ID="ReadingLevelSlider" runat="server">
            </RL:RLSlider>
</div>
<div  style="width:99%;float:left;margin-top: 15px;margin-left: 16px;">

<asp:GridView ID="BooksGridView"  runat="server" AutoGenerateColumns="false" 
        ShowHeader="false" Width="100%" GridLines="None" AllowPaging="true" 
        PagerStyle-HorizontalAlign="Center"
        PageSize="5" onrowdatabound="BooksGridView_RowDataBound" 
        onpageindexchanging="BooksGridView_PageIndexChanging">
        <PagerSettings Mode="NextPrevious"   NextPageText="next >>"  Position="Bottom" PreviousPageText="<< previous"/> 
                            <Columns>
                             <asp:TemplateField ItemStyle-Width="10%" ItemStyle-CssClass="tablerow1" ItemStyle-Height="120px">
                                    <ItemTemplate>
                                    <asp:CheckBox ID="BooksCheckBox" runat="server" style="float: left;margin-left: 24px;display:none;" />
<img id="ClassCheckBoxImg"  alt="uncheck" onclick="javascript:imageclick(this);"  style="height: 28px;width: 27px;margin-left: 15px;"  clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>"/>
                                     </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="30%" ItemStyle-CssClass="tablerow2">
                                    <ItemTemplate>
                                         <img id="BookCoverImage" src="<%=Page.ResolveUrl ("Portals/0/images/TheLittleBlueHorse.png")%>" style="margin-top: 4px;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="62%" ItemStyle-CssClass="tablerow3">
                                    <ItemTemplate>
                                       <div style="width:100%;float:left">
                                       <div style="width:100%;float:left">
                                       <asp:Label ID="BookNameLabel" runat="server" style="color: #1479A4;font-size: 19px;" Text="Big Book of Word Problems:"></asp:Label>
                                       <span style="color: #1479A4;font-size: 19px;">Years</span>
                                       <asp:Label ID="FromYearLabel" style="color: #1479A4;font-size: 19px;" runat="server" Text="5" ></asp:Label>
                                       <span style="color: #1479A4;font-size: 19px;">and</span>
                                       <asp:Label ID="ToYearLabel" runat="server" style="color: #1479A4;font-size: 20px;" Text="6" ></asp:Label>
                                       <br />
                                       <asp:Label ID="AuthorNameLabel" runat="server" style="color: #1479A4;margin-top: 5px;float: left;font-weight: bold;" Text="Author"></asp:Label>
                                       </div>
                                       <div style="width:100%;float:left;margin-top: 5px;font-weight: bold;font-size: 14px;color:#666666;">
                                       <asp:Label ID="StudentLabel" runat="server" Text="Student" ></asp:Label>
                                       <asp:Label ID="ReadingLevelLabel" runat="server" Text="[Reading Level]" ></asp:Label>,
                                       <asp:Label ID="ColourLabel" runat="server" Text="[Colour]" ></asp:Label>,
                                       <asp:Label ID="ReadingAgeLabel" runat="server" Text="[Reading Age:5]" ></asp:Label><br />
                                       <asp:Label ID="BookLabel" runat="server" style="margin-top: 5px;float: left;" Text="Book:"></asp:Label>
                                       <asp:Label ID="PagesCountLabel" runat="server" style="margin-top: 5px;float: left;" Text="[16 Pages], " ></asp:Label>
                                       <asp:Label ID="YearLabel" runat="server" style="margin-top: 5px;float: left;" Text="[&copy Year]" ></asp:Label>
                                       </div>
                                       </div>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NextPrevious" />
                        </asp:GridView>
                       
</div>
</div>--%>
<div class="BooksDivset">
    <div style="width: 93%; margin-top: 4px; height: auto; border: 1px solid lightGrey;
        float: left; margin-left: 24px; margin-bottom: 9px;" >
        <div style="float: left; width: 10%; margin: 25px 0px 10px 6px;">
            <span style="float: left; position: absolute; width: 125px; padding-top: 4px; padding-left: 3px;display:none;"
                class="Session">TO:</span>
        </div>
        <div id="DetailsDiv" runat="server" style="float: left; width: 60%; margin: 25px 0px 10px 1px;
            height: auto; overflow: hidden;display:none;">
        </div>
        <div class="ActiveAddButtonsHolder BackBtn">
            <asp:Button ID="BackToSession" CssClass="BackToSession" Enabled="true" runat="server"
                Text="BACK" OnClick="BackToSession_Click" />
        </div>
        <div class="GroupAddtoinnersnddiv" style="display:none">
            <ul id="SelectedStudentList" runat="server" clientidmode="Static">
            </ul>
            <div id="dialog" title="Alert Message" style="display: none;">
                <p>
                    <span id="StudentNamespan"></span>is Already added</p>
            </div>
            <asp:TextBox ID="SelectedValueTextBox" runat="server" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
            <asp:TextBox ID="SelectedValueGroupTextBox" runat="server" ClientIDMode="Static"
                Style="display: none;"></asp:TextBox>
            <asp:TextBox ID="SelectedValueTeacherTextBox" runat="server" ClientIDMode="Static"
                Style="display: none;"></asp:TextBox>
        </div>
    </div>
    <div class="SearchDiv">
       <%-- <div class="Div_FullWidth">--%>
                <div class="ProgressDivClass" style="display:none" id="UpdateProgressImg">
                    <div class="ProgressInnerDiv">
                        <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg" alt="Processing" /> 
                    </div>
                </div>
            <div class="SearchInnerDiv">
                <asp:UpdatePanel ID="BookUpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="SearchTextBox" ClientIDMode="Static" runat="server" CssClass="Groupsearchtxt"  title="Enter your search here ..."></asp:TextBox>
                        <asp:DropDownList ID="CategoriesDrpList" AutoPostBack="true"  ClientIDMode="Static" runat="server" Style="float: left;
                             font-family: Raleway, Arial, Sans-Serif; font-size: 9pt;width:200px; margin-left:25px;
                            color: #707070; margin-top: 4px;"  OnSelectedIndexChanged="itemSelected" 
                            TabIndex="0">
                            <asp:ListItem Text="TEXT TYPE"></asp:ListItem>
                            <asp:ListItem Text="GUIDED READING LEVEL"></asp:ListItem>
                            <asp:ListItem Text="READING AGE"></asp:ListItem>
                            <asp:ListItem Text="CHARACTER FAMILY"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="SearchButton" runat="server" OnClick="SearchButton_Click" Text="SEARCH" OnClientClick="ShowUpdate()" 
                            Style="color: White; float: right; width: 88px; font-family: Raleway ExtraBold, Arial, Sans-Serif;
                            text-shadow: 1px 2px 2px #4D7B18; font-size: 10pt; height: 33px; margin-top: 4px;
                            margin-right: 3px; border: 1px solid lightgray; cursor:pointer ;
                            border-radius: 3px; background: -moz-linear-gradient(center top , white 0%, #8EBC5A 5%, #609624 130%) repeat scroll 0 0 transparent;
                            background: -webkit-gradient(linear, left top, left bottom, from(#8EBC5A), to(#609624));
                            background: -ms-linear-gradient(top, #8EBC5A 5%, #609624 130%) !important; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#8EBC5A', endColorstr='#609624', gradientType='0');" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="CategoriesDrpList" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        <%-- </div>--%>
    </div>
    <%-- <div class="BooksSliderdiv" id="bookslistsliderDiv" style="width: 671px">
        <span id="TitleSpan" style="font-size: 12pt; font-weight: bold; color: #707070; margin-top: 13px;
            float: left; margin-left: 15px;">TITLE AVAILABLE ON
            <label id="level" for="level">
                MARY'S
            </label>
            BOOK SHELF: </span>
        <hr style="float: left; width: 94%; margin-left: 15px; border-left: 0px;" />
        <RL:RLSlider ID="ReadingLevelSlider" runat="server">
        </RL:RLSlider>
    </div>--%>
    <div style="width: 91%; margin-top: 10px; margin-left: 25px; padding: 15px 0px 60px 15px;
        float: left; border: 1px solid #CCC;">
        <h5>
            TITLES AVALABLE ON THE BOOKSHELF:
        </h5>
        <div class="eCollectionEditLbl">
            <%-- style="margin-top: 30px">--%>
            <RL:RLSlider ID="ReadingLevelSlider" runat="server">
            </RL:RLSlider>
            <asp:HiddenField ID="SliderValue" ClientIDMode="Static" runat="server" />
        </div>
    </div>
    <asp:UpdatePanel ID="MessageDivUpdatePanel" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <div id="MessageOuterDiv" runat="server" style="width:100%;position:static ;">
                <div class="bubble" >
                    <asp:Label ID="Message1" runat="server" Text="" />            
                </div>
            </div> 
        </ContentTemplate> 
    </asp:UpdatePanel> 
    <asp:HiddenField ID="custItmSKHidden" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="SliderRange" ClientIDMode="Static" runat="server" />
    <div id="BooksContentDiv" class="BooksContentDiv">
        <asp:UpdatePanel ID="BooksUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="hiddenbtn" ClientIDMode="Static" Style="display: none" runat="server"
                    OnClick="hiddenbtn_Click" />
                <asp:Repeater ID="CollectionRepeater" runat="server"  OnItemDataBound="CollectionRepeater_ItemDataBound" >
                    <ItemTemplate>
                        <div id="SelectedBookRepdiv" class='<%# Eval("ClassName") %>'>
                            <div id="SelectedBookContentFrstdiv" style="float: left; margin-top: 68px;">
                                <input type="checkbox" id="BooksCheckBox" runat="server" checked='<%# Eval("Checked") %>'
                                    style="float: left; margin-left: 24px; display: none;" />
                                <asp:Label ID="CUST_SUBS_ITEM_SK" runat="server" Style="display: none" Text='<%# Eval("CUST_SUBS_ITEM_SK") %>'></asp:Label>
                                <asp:Label ID="lblIMAGE_FILE_NAME" runat="server" Visible="false" Text='<%# Eval("IMAGE_FILE_NAME") %>'></asp:Label>
                                <img id="ClassCheckBoxImg" alt=""  clientidmode="Static" style="float: left; margin-left: 24px;
                                    width: 20px; cursor: pointer;" onclick="javascript:CheckBoxImgClick(this);"  src="<%#Page.ResolveUrl(string.Format("Portals/0/images/{0}",Eval("CheckImgPathName"))) %>" />
                            </div>
                            <div class="DashBoard_Items_books">
                                <asp:Image runat="server" ID="BookCoverImage" ClientIDMode="Static" CssClass ="DashBoard_Items_books_images BooksImgClass" ImageUrl='<%# Eval("IMAGE_FILE_NAME")%>'/>

                            </div>
                            <div style="width: 68%; float: right; margin-top: 20px;">
                                <div style="width: 100%; float: left">
                                    <asp:Label ID="BookNameLabel" runat="server" Style="color: #707070; font-size: 12pt;
                                        font-family: Raleway-regular,Raleway, Arial, Sans-Serif; font-weight: 700;" Text='<%# Eval("Title") %>'></asp:Label>
                                    <%-- <span style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                        font-weight: 700;">Years</span>
                                    <asp:Label ID="FromYearLabel" Style="color: #707070; font-size: 12pt; padding: 5px;
                                        font-family: Raleway-regular,Raleway, arial, sans-serif; font-weight: 700;" runat="server"
                                        Text="5"></asp:Label>
                                    <span style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                        font-weight: 700;">and</span>
                                    <asp:Label ID="ToYearLabel" runat="server" Style="padding: 5px; color: #707070; font-size: 12pt;
                                        font-family: Raleway-regular,Raleway, Arial, Sans-Serif; font-weight: 700;" Text="6"></asp:Label>--%>
                                    <br />
                                    <asp:Label ID="AuthorNameLabel" runat="server" Style="font-family: Arial-Regular, Sans-Serif;
                                        font-size: 10pt; color: #707070; margin-top: 5px; float: left;" Text='<%# Eval("Author") %>'></asp:Label>
                                </div>
                                <div style="font-family: Arial-Regular, Sans-Serif; width: 100%; float: left; margin-top: 5px;
                                    font-size: 10pt; color: #707070;">
                                    <asp:Label ID="ColourLabel" runat="server" Text='<%# Eval("ColourLevel") %>'></asp:Label>,
                                    <asp:Label ID="ReadingLevelLabel" runat="server" Text='<%#  string.Format("PM level {0}", Eval("ReadingLevel")) %>'></asp:Label>,
                                    <asp:Label ID="ReadingAgeLabel" runat="server" Text="Reading age: "><%# Eval("ReadingAge") %></asp:Label><br />
                                    <asp:Label ID="AttributeTypeLabel" runat="server" Text='<%# Eval("TEXTTYPE") %>' Style="float:left;margin-top: 5px;"></asp:Label>
                                    <asp:Label ID="YearLabel" runat="server" Style="float:left;margin-top:5px ;width:100%" Text='<%#  string.Format("Copyright: {0}", Eval("COPYRIGHT_YEAR")) %>'></asp:Label>
                                </div>
                            </div>
                            <div class="GpStudentRptcontentinthrdiv">
                                            <asp:Button ID="ClassProfileButton" ClientIDMode="Static" runat="server"  Text="ADD"
                                                CssClass="GpStudentRptcontentinthrbtn GpAddStudentRptbtn" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                 <CP:CustomPaging ID="CustomPaging" runat="server">
                        </CP:CustomPaging>
                <%--<center id="PagerHolder" class="Pager" style="min-width: 90%">
                    <div id="StudentPagerDiv" clientidmode="Static" style="width: 96%; float: right;
                        display: block; margin-top: 18px;" runat="server">
                        <div style="float: left; margin-left: 227px; width: 23.8%;">
                            <asp:Button ID="PreviousButton" Style="cursor: pointer; font-family: Raleway; color: rgb(31, 181, 231);
                                border: 0px solid white; float: left; background: transparent;" OnClientClick="javascript:GetBckPageNumber();"
                                ClientIDMode="Static" runat="server" Text="Previous" OnClick="PreviousButton_Click">
                            </asp:Button>
                            <asp:Button ID="ShowNextButton" Style="cursor: pointer; font-family: Raleway; color: rgb(31, 181, 231);
                                margin-left: 7px; border: 0px solid white; float: left; background: transparent;"
                                OnClientClick="javascript:GetFwdPageNumber();" ClientIDMode="Static" runat="server"
                                Text="Next>>" OnClick="ShowNextButton_Click"></asp:Button>
                        </div>
                        <asp:HiddenField ID="pageNumber" ClientIDMode="Static" Value="1" runat="server" />
                    </div>
                </center>--%>
                <%--<asp:GridView ID="BooksGridView" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                    Width="100%" GridLines="None" AllowPaging="true" PagerStyle-HorizontalAlign="Center"
                    PageSize="5" OnRowDataBound="BooksGridView_RowDataBound" OnPageIndexChanging="BooksGridView_PageIndexChanging" >
                    <PagerSettings Mode="NextPrevious" NextPageText="next >>" Position="Bottom" PreviousPageText="<< previous" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-Height="120px">
                            <ItemTemplate>
                                <asp:CheckBox ID="BooksCheckBox" runat="server" Style="float: left; margin-left: 24px;
                                    display: none;" />
                                    <asp:Label ID="CUST_SUBS_ITEM_SK" runat="server" Visible = false Text='<%# Eval("CUST_SUBS_ITEM_SK") %>'></asp:Label>
                                <img id="ClassCheckBoxImg" alt="uncheck" onclick="javascript:imageclick(this);" class="ClassBookcheckimg"
                                    clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                                    <asp:Label ID="CheckBoxClicked" runat="server" Visible = false></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="30%">
                            <ItemTemplate>
                            <!-- =Page.ResolveUrl ("Portals/0/images/TheLittleBlueHorse.png") -->
                                <img id="BookCoverImage"  src="<%#Page.ResolveUrl(string.Format("Portals/0/images/{0}",Eval("IMAGE_FILE_NAME"))) %>"
                                    style="margin-top: 4px;margin-left:5px;" />
                                    <%--<asp:ImageField DataImageUrlField="<%# Eval("IMAGE_FILE_NAME") %>"
                                            DataImageUrlFormatString="<%=Page.ResolveUrl ("Portals/0/images/{0}") %>"  style="margin-top: 4px;margin-left:5px;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="62%">
                            <ItemTemplate>
                                <div class="ClassBookitemdiv">
                                    <div class="ClassBookitemdiv">
                                        <asp:Label ID="BookNameLabel" runat="server" CssClass="ClassBooknmlbl" Style="color: #707070; font-size: 12pt;font-family:Raleway ExtraBold, Arial, Sans-Serif;    font-weight:bold;" Text='<%# Eval("Title") %>'></asp:Label>
                                     <%--   <span class="ClassBooknmspn">Years</span>
                                        <asp:Label ID="FromYearLabel" CssClass="ClassBooknmlbl" runat="server" Text="5"></asp:Label>
                                        <span class="ClassBooknmspn">and</span>
                                        <asp:Label ID="ToYearLabel" runat="server" CssClass="ClassBooknmlbl" Text="6"></asp:Label>
                                        <br />
                                        <asp:Label ID="AuthorNameLabel" runat="server" CssClass="ClassBookauthorlbl" Text='<%# Eval("Author") %>'></asp:Label>
                                    </div>
                                    <div class="ClassBookinnerdiv">
                                        <asp:Label ID="StudentLabel" runat="server" Style="font-style:italic" Text="Student"></asp:Label>
                                        <asp:Label ID="ReadingLevelLabel" runat="server" Text="[Reading Level]"></asp:Label>,
                                        <asp:Label ID="ColourLabel" runat="server" Text="[Colour]"></asp:Label>,
                                        <asp:Label ID="ReadingAgeLabel" runat="server" Text="[Reading Age:5]"></asp:Label><br />
                                        <asp:Label ID="BookLabel" runat="server" Style="font-style:italic"  CssClass="ClassBookindivlbl" Text="Book:"></asp:Label>
                                        <asp:Label ID="PagesCountLabel" runat="server" CssClass="ClassBookindivlbl" Text='<%#  string.Format("{0} pages , ", Eval("NO_OF_PAGES")) %>'></asp:Label>
                                        <asp:Label ID="YearLabel" runat="server" CssClass="ClassBookindivlbl" Text='<%#Eval("COPYRIGHT_YEAR") %>'></asp:Label>
                                    </div>                                   
                                </div>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="30%">
                            <ItemTemplate>
                            <div id="ClassRepeaterSubmit" style="width: 130%; float: right; padding-top: 7px;">
                                 <div id="BooksAdd" class="greenBtn" >
                                            <asp:Button ID="BooksAddButton" runat="server" Text="ADD" class="ProfileButton SessionAddButton" ForeColor="White"
                                                CommandArgument='<%# Eval("CUST_SUBS_ITEM_SK") %>' />
                                 </div>
                                 </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="NextPrevious" />
                </asp:GridView>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
