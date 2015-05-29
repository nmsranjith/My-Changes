<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookSelectionWizard.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Books.Books.BookSelectionWizard" %>
   <%@ Register Src="BooksCategories.ascx" TagName="Books_Categories" TagPrefix="BC" %>
   <%@ Register Src="BooksLevels.ascx" TagName="Levels" TagPrefix="BLEVELS" %>
   <%@ Register Src="BooksReadingAge.ascx" TagName="Books_ReadingAge" TagPrefix="BR" %>
   <%@ Register Src="SeeAllCollection.ascx" TagName="Books_SeeAll" TagPrefix="SeeAll" %>
   <style type="text/css">
    #CategoriesDrpList-list, #ReadingAgeSelect-list
    {
        width: 270px !important;
        color: #707070 !important;
        margin-top:4px;
    }
    #SelectionContentDiv .k-dropdown
    {
        width: 276px !important;
    }
    #SelectionContentDiv  .k-input
    {
        color: #707070 !important;
    }
</style>
    <script type="text/javascript">

        jQuery(document).ready(function () {
            

            $('[id$=AddtosubsButton]').attr('disabled', true);
            $('[id$=BottomAddToSubscription]').attr('disabled', true);
            $('[id$=RemovetosubsButtontop]').attr('disabled', true);
            $('[id$=RemoveSubButtonbot]').attr('disabled', true);
            var seeAllFlag;

            jQuery("#CategoriesButton").click(function () {

                if (jQuery("#LevelsButton").hasClass("LevelBtnclass")) {
                    jQuery("#LevelsButton").removeClass("LevelBtnclass").addClass("LevelBtnaltclass");
                    //jQuery("#LevelsButton").removeClass("k-state-hover");
                }
                if (jQuery("#ReadingAgeButton").hasClass("LevelBtnclass")) {
                    jQuery("#ReadingAgeButton").removeClass("LevelBtnclass").addClass("LevelBtnaltclass");
                }
                jQuery("#CategoriesButton").addClass("LevelBtnclass").removeClass("LevelBtnaltclass");
                ////jQuery("#headSpan").text("Categories");
                //            jQuery("#LevelsButton").css("background-image", "url('/dotnetnuke/images/Levelbg.png')");
                //            jQuery("#LevelsButton").css("background-repeat", "repeat')");
                //            jQuery("#CategoriesButton").css("background-image", "none");
                //            jQuery("#CategoriesButton").css("background-color", "#C9C9C9");
                //document.getElementById("LevelDiv").className = "fade-out";
                //document.getElementById("CategoriesDiv").className = "fade-in";
                jQuery("#LevelCollection").css("display", "none");
                jQuery("#CategoriesDiv").css("display", "block");
                jQuery("#LevelDiv").css("display", "none");
                jQuery("#ReadingAgeDiv").css("display", "none");
                jQuery("#BooksContentDiv").height(jQuery("#BookSelectionWizardPlace").height());
                jQuery("#eCollectionContent").height(jQuery("#BookSelectionMainDiv").height() + 60);
                jQuery("#eCollectionMenu").height(jQuery("#eCollectionContent").height() - 15);

                return false;
            });
            jQuery("#LevelsButton").click(function () {
                if (jQuery("#CategoriesButton").hasClass("LevelBtnclass")) {
                    jQuery("#CategoriesButton").removeClass("LevelBtnclass").addClass("LevelBtnaltclass");
                    //jQuery("#LevelsButton").removeClass("k-state-hover");
                }
                if (jQuery("#ReadingAgeButton").hasClass("LevelBtnclass")) {
                    jQuery("#ReadingAgeButton").removeClass("LevelBtnclass").addClass("LevelBtnaltclass");
                }
                jQuery("#LevelsButton").addClass("LevelBtnclass").removeClass("LevelBtnaltclass");
                ////jQuery("#headSpan").text("Level packs");


                //            jQuery("#CategoriesButton").css("background-image", "url('/dotnetnuke/images/Levelbg.png')");
                //            jQuery("#CategoriesButton").css("background-repeat", "repeat')");
                //            jQuery("#LevelsButton").css("background-image", "none");
                //            jQuery("#LevelsButton").css("background-color", "#C9C9C9");
                //document.getElementById("CategoriesDiv").className = "fade-out";
                //document.getElementById("LevelDiv").className = "fade-in";
                jQuery("#LevelDiv").css("display", "block");
                jQuery("#CategoriesDiv").css("display", "none");
                jQuery("#LevelCollection").css("display", "none");
                jQuery("#ReadingAgeDiv").css("display", "none");
                jQuery("#BooksContentDiv").height(jQuery("#BookSelectionWizardPlace").height());
                jQuery("#eCollectionContent").height(jQuery("#BookSelectionMainDiv").height() + 60);
                jQuery("#eCollectionMenu").height(jQuery("#eCollectionContent").height() - 15);

                //lblBooksSelected

                return false;
            });

            jQuery("#ReadingAgeButton").click(function () {

                ////jQuery("#headSpan").text("Reading Age");
                if (jQuery("#CategoriesButton").hasClass("LevelBtnclass")) {
                    jQuery("#CategoriesButton").removeClass("LevelBtnclass").addClass("LevelBtnaltclass");
                    //jQuery("#LevelsButton").removeClass("k-state-hover");
                }
                if (jQuery("#LevelsButton").hasClass("LevelBtnclass")) {
                    jQuery("#LevelsButton").removeClass("LevelBtnclass").addClass("LevelBtnaltclass");
                }
                jQuery("#ReadingAgeButton").addClass("LevelBtnclass").removeClass("LevelBtnaltclass");
                jQuery("#LevelCollection").css("display", "none");
                jQuery("#LevelDiv").css("display", "none");
                jQuery("#CategoriesDiv").css("display", "none");
                jQuery("#ReadingAgeDiv").css("display", "block");
                jQuery("#ReadingAgeButton a").css("color", "#818080");
                //alert("Functionality under construction.......");
                jQuery("#BooksContentDiv").height(jQuery("#BookSelectionWizardPlace").height());
                jQuery("#eCollectionContent").height(jQuery("#BookSelectionMainDiv").height() + 60);
                jQuery("#eCollectionMenu").height(jQuery("#eCollectionContent").height() - 15);
                return false;
            });




            var seeallcollections = $("#BookSelectionWizardPlace").has('#LevelCollection');
            if ($.browser.msie) {
                $("#ReadingAgeshadow").addClass("arrow-right");
                $("#Categoryshadow").addClass("arrow-right2");
            }

            $("#CollectioDiv #CollectionRepdiv #CollectionContentFrstdiv img").click(function () {
                this.parentNode.children[0].click();
                if (this.parentNode.children[0].checked) {
                    for (var i = 0; i < $("#CollectioDiv #CollectionRepdiv #CollectionContentFrstdiv input[type=checkbox]").length; i++) {
                        if ($("#CollectioDiv #CollectionRepdiv #CollectionContentFrstdiv input[type=checkbox]")[i].checked) {
                            $("#CollectioDiv #CollectionRepdiv #CollectionContentFrstdiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        }
                    }

                }
                else {
                    for (var j = 0; j < $("#CollectioDiv #CollectionRepdiv #CollectionContentFrstdiv input[type=checkbox]").length; j++) {
                        if (!$("#CollectioDiv #CollectionRepdiv #CollectionContentFrstdiv input[type=checkbox]")[j].checked) {
                            $("#CollectioDiv #CollectionRepdiv #CollectionContentFrstdiv img")[j].src = GetFile("/Portals/0/images/circle_big.png");
                        }
                    }

                }
            });

            $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv img").click(function () {
                this.parentNode.children[0].click();
                var count = 0;
                var LevelBookcount = 0;
                var lev = "0";
                var index = this.parentNode.parentNode.parentNode.parentNode.children[0].children[2].innerHTML;
                if (this.parentNode.children[0].checked) {

                    for (var i = 0; i < $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children.length; i++) {
                        if ($("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children[i].children[0].children[0].checked) {
                            $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children[i].children[0].children[1].src = GetFile("/Portals/0/images/check.png");
                            count++;
                        }
                    }
                    if (count == $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children.length) {
                        this.parentNode.parentNode.parentNode.parentNode.children[0].children[1].children[0].src = GetFile("/Portals/0/images/check.png");
                        if (!this.parentNode.parentNode.parentNode.parentNode.children[0].children[0].checked)
                            this.parentNode.parentNode.parentNode.parentNode.children[0].children[0].click();
                    }
                    else {
                        this.parentNode.parentNode.parentNode.parentNode.children[0].children[1].children[0].src = GetFile("/Portals/0/images/unchecked.png");
                        if (this.parentNode.parentNode.parentNode.parentNode.children[0].children[0].checked)
                            this.parentNode.parentNode.parentNode.parentNode.children[0].children[0].click();
                    }

                    for (var i = 0; i < $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv input[type=checkbox]").length; i++) {
                        if ($("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv input[type=checkbox]")[i].checked) {
                            var bookCount = $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv #BooksCollectionCount")[i].innerHTML;
                            LevelBookcount += parseInt(bookCount)
                            var a = $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv #lblAttributeType")[i].innerHTML.toUpperCase().replace("LEVEL", "");
                            lev = lev + ',' + a;   //$("#SelectedBookscount").val(parseInt($("#SelectedBookscount").val()) + parseInt(bookCount));                            
                        }
                    }

                    // $('#SelectedBookscountLevels').text(LevelBookcount);
                    //  $('#SelectedBookscountBotLevels').text(LevelBookcount);


                    var isGracePeriod = jQuery('#lblGracePeriod').text();
                    //if (isGracePeriod == 'False') {
                    jQuery("#AddtosubsButton").removeClass("disabledaddtosubtn").addClass("addtosubtn");
                    $('[id$=AddtosubsButton]').attr('disabled', false);
                    jQuery("#BottomAddToSubscription").removeClass("disabledaddtosubtn").addClass("addtosubtn");
                    $('[id$=BottomAddToSubscription]').attr('disabled', false);
                    //}


                }
                else {
                    for (var j = 0; j < $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children.length; j++) {
                        if (!$("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children[j].children[0].children[0].checked) {
                            $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children[j].children[0].children[1].src = GetFile("/Portals/0/images/unchecked.png");
                            count++;
                        }
                    }
                    var count = 0;
                    for (var i = 0; i < $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children.length; i++) {
                        if ($("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children[i].children[0].children[0].checked) {
                            $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children[i].children[0].children[1].src = GetFile("/Portals/0/images/check.png");
                            count++;
                        }
                    }
                    if (count == 0) {
                        var isGracePeriod = jQuery('#lblGracePeriod').text();
                        //if (isGracePeriod == 'False') {
                        jQuery("#AddtosubsButton").removeClass("addtosubtn").addClass("disabledaddtosubtn");
                        $('[id$=AddtosubsButton]').attr('disabled', true);
                        jQuery("#BottomAddToSubscription").removeClass("addtosubtn").addClass("disabledaddtosubtn");
                        $('[id$=BottomAddToSubscription]').attr('disabled', true);
                        //}
                    }
                    this.parentNode.parentNode.parentNode.parentNode.children[0].children[1].children[0].src = GetFile("/Portals/0/images/unchecked.png");
                    if (this.parentNode.parentNode.parentNode.parentNode.children[0].children[0].checked)
                        this.parentNode.parentNode.parentNode.parentNode.children[0].children[0].click();

                    for (var i = 0; i < $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv input[type=checkbox]").length; i++) {
                        if ($("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv input[type=checkbox]")[i].checked) {
                            var bookCount = $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv #BooksCollectionCount")[i].innerHTML;
                            LevelBookcount += parseInt(bookCount)
                            var a = $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv #lblAttributeType")[i].innerHTML.toUpperCase().replace("LEVEL", "");
                            lev = lev + ',' + a;
                            //$("#SelectedBookscount").val(parseInt($("#SelectedBookscount").val()) + parseInt(bookCount));                            
                        }
                    }

                    //    $('#SelectedBookscountLevels').text(LevelBookcount);
                    //    $('#SelectedBookscountBotLevels').text(LevelBookcount);
                }
                //if (count > 1) {
                //            this.parentNode.parentNode.parentNode.parentNode.childNodes[1].childNodes[3].src = GetFile("/Portals/0/images/unchecked.png");
                //            if (this.parentNode.parentNode.parentNode.parentNode.childNodes[1].childNodes[1].checked)
                //                this.parentNode.parentNode.parentNode.parentNode.childNodes[1].childNodes[1].click();
                count = 0;
                //}
                lev = lev.toUpperCase().replace("LEVEL ", "");
                if (lev != "0") {
                    $.ajax({
                        url: GetFile('/DesktopModules/eCollection_Books/BooksHandler.ashx?BooksStatus=bookscount&values=' + lev+'&type=READINGLEVEL'),
                        dataType: "json",
                        success: function (value) {
                            value = value.split(',');
                            $('#SelectedBookscountLevels').text(value[0]);
                            $('#SelectedBookscountBotLevels').text(value[0]);
                            $('#AlreadySelectedBookCnt').text(value[1]);
                        }
                    });
                }
                else {
                    $('#SelectedBookscountLevels').text('0');
                    $('#SelectedBookscountBotLevels').text('0');
                    $('#AlreadySelectedBookCnt').text('0');
                }
            });
            $("#LevelContentDiv #LevelRepeater #LevelContentDiv #ImgDiv img").click(function () {
                var LevelBookcount = 0;
                var lev = "0";
                this.parentNode.parentNode.children[0].click();
                var index = this.parentNode.parentNode.children[2].innerHTML;
                if (this.parentNode.parentNode.children[0].checked) {
                    for (var i = 0; i < $("#LevelContentDiv #LevelRepeater #LevelContentDiv input[type=checkbox]").length; i++) {
                        if ($("#LevelContentDiv #LevelRepeater #LevelContentDiv input[type=checkbox]")[i].checked) {
                            $("#LevelContentDiv #LevelRepeater #LevelContentDiv #ImgDiv img")[i].src = GetFile("/Portals/0/images/check.png");
                        }
                    }



                    jQuery("#AddtosubsButton").removeClass("disabledaddtosubtn").addClass("addtosubtn");
                    $('[id$=AddtosubsButton]').attr('disabled', false);
                    jQuery("#BottomAddToSubscription").removeClass("disabledaddtosubtn").addClass("addtosubtn");
                    $('[id$=BottomAddToSubscription]').attr('disabled', false);

                    for (var i = 0; i < $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children.length; i++) {
                        if (!$("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children[i].children[0].children[0].checked) {
                            $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children[i].children[0].children[1].src = GetFile("/Portals/0/images/check.png");
                            $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children[i].children[0].children[0].click();
                        }

                    }
                    //                    var cr = 0;
                    //                    for (var i = 0; i < 2; i++) {
                    //                        if (!this.parentNode.parentNode.childNodes[3].childNodes[cr + 1].childNodes[1].childNodes[1].checked) {
                    //                            this.parentNode.parentNode.childNodes[3].childNodes[cr + 1].childNodes[1].childNodes[3].src = GetFile("/Portals/0/images/check.png");
                    //                            this.parentNode.parentNode.childNodes[3].childNodes[cr + 1].childNodes[1].childNodes[1].click();
                    //                        }
                    //                        cr = cr + 2;
                    //                    }
                    for (var i = 0; i < $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv input[type=checkbox]").length; i++) {
                        if ($("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv input[type=checkbox]")[i].checked) {
                            //var bookCount = $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv #BooksCollectionCount")[i].innerHTML;
                            //LevelBookcount += parseInt(bookCount)
                            var a = $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv #lblAttributeType")[i].innerHTML.toUpperCase().replace("LEVEL", "");
                            lev = lev + ',' + a;
                            //$("#SelectedBookscount").val(parseInt($("#SelectedBookscount").val()) + parseInt(bookCount));                            
                        }
                    }

                  //  $('#SelectedBookscountLevels').text(LevelBookcount);
                  //  $('#SelectedBookscountBotLevels').text(LevelBookcount);

                }
                else {
                    for (var j = 0; j < $("#LevelContentDiv #LevelRepeater #LevelContentDiv input[type=checkbox]").length; j++) {
                        if (!$("#LevelContentDiv #LevelRepeater #LevelContentDiv input[type=checkbox]")[j].checked) {
                            $("#LevelContentDiv #LevelRepeater #LevelContentDiv #ImgDiv img")[j].src = GetFile("/Portals/0/images/unchecked.png");

                        }
                    }
                    var count = 0;
                    for (var i = 0; i < $("#LevelContentDiv #LevelRepeater #LevelContentDiv input[type=checkbox]").length; i++) {
                        if ($("#LevelContentDiv #LevelRepeater #LevelContentDiv input[type=checkbox]")[i].checked) {
                            $("#LevelContentDiv #LevelRepeater #LevelContentDiv #ImgDiv img")[i].src = GetFile("/Portals/0/images/check.png");
                            count++;
                        }

                    }

                    if (count == 0) {
                        jQuery("#AddtosubsButton").removeClass("addtosubtn").addClass("disabledaddtosubtn");
                        $('[id$=AddtosubsButton]').attr('disabled', true);
                        jQuery("#BottomAddToSubscription").removeClass("addtosubtn").addClass("disabledaddtosubtn");
                        $('[id$=BottomAddToSubscription]').attr('disabled', true);
                    }

                    for (var i = 0; i < $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children.length; i++) {
                        if ($("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children[i].children[0].children[0].checked) {
                            $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children[i].children[0].children[1].src = GetFile("/Portals/0/images/unchecked.png");
                            $("#LevelContentDiv #LevelRepeater #BooksLevelDiv")[index].children[i].children[0].children[0].click();
                        }

                    }
                    //                    var co = 0;
                    //                    for (var i = 0; i < 2; i++) {
                    //                        if (this.parentNode.parentNode.childNodes[3].childNodes[co + 1].childNodes[1].childNodes[1].checked) {
                    //                            this.parentNode.parentNode.childNodes[3].childNodes[co + 1].childNodes[1].childNodes[3].src = GetFile("/Portals/0/images/unchecked.png");
                    //                            this.parentNode.parentNode.childNodes[3].childNodes[co + 1].childNodes[1].childNodes[1].click();
                    //                        }
                    //                        co = co + 2;
                    //                    }
                    for (var i = 0; i < $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv input[type=checkbox]").length; i++) {
                        if ($("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv input[type=checkbox]")[i].checked) {
                            //var bookCount = $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv #BooksCollectionCount")[i].innerHTML;
                            // LevelBookcount += parseInt(bookCount)
                            var a = $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv #lblAttributeType")[i].innerHTML.toUpperCase().replace("LEVEL", "");
                            lev = lev + ',' + a;
                            //$("#SelectedBookscount").val(parseInt($("#SelectedBookscount").val()) + parseInt(bookCount));                            
                        }
                    }

                    // $('#SelectedBookscountLevels').text(LevelBookcount);
                    // $('#SelectedBookscountBotLevels').text(LevelBookcount);
                }
                lev = lev.toUpperCase().replace("LEVEL ", "");
                if (lev != "0") {
                    $.ajax({
                        url: GetFile('/DesktopModules/eCollection_Books/BooksHandler.ashx?BooksStatus=bookscount&values=' + lev + '&type=READINGLEVEL'),
                        dataType: "json",
                        success: function (value) {
                            value = value.split(',');
                            $('#SelectedBookscountLevels').text(value[0]);
                            $('#SelectedBookscountBotLevels').text(value[0]);
                            $('#AlreadySelectedBookCnt').text(value[1]);
                        }
                    });
                }
                else {
                    $('#SelectedBookscountLevels').text('0');
                    $('#SelectedBookscountBotLevels').text('0');
                    $('#AlreadySelectedBookCnt').text('0');
                }
                return false;
            });



        });
</script>
<style type="text/css">
    #CategoriesDiv
    {
        -webkit-transition: opacity 0.5s ease-in;
        -moz-transition: opacity 0.5s ease-in;
        -o-transition: opacity 0.5s ease-in;
    }
    #CategoriesDiv.fade-out
    {
        opacity: 0;
    }
    #CategoriesDiv.fade-in
    {
        opacity: 1;
    }
    #LevelDiv
    {
        -webkit-transition: opacity 0.5s ease-in;
        -moz-transition: opacity 0.5s ease-in;
        -o-transition: opacity 0.5s ease-in;
    }
    #LevelDiv.fade-out
    {
        opacity: 0;
    }
    #LevelDiv.fade-in
    {
        opacity: 1;
    }
    #ReadingAgeSelect-list
    {
        width:270px !important;
        color:#707070 !important;
    }
    #CategoriesDrpList-list
    {
        width:270px !important;
        color:#707070 !important;
    }
    
    .LevelBtnaltclass
    {
        text-decoration:none !important;
        text-shadow:none!important;
        text-align :center !important;     
    }
    .LevelBtnclass
    {        
        text-decoration:none !important;
        text-align :center !important;
        height:14px !important
    }
    #eCollBookstabstrip ul
    {
        float: left;
width: 100%;
height: 48px;
padding: 0px;
margin-top: 0px;
    }
    
    #eCollBookstabstrip .k-state-active
    {
        height: 36px !important;
        margin-top: -6px !important;
        background: White !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='White', endColorstr='White', gradientType='0') !important;
       background: -ms-linear-gradient(top, White 5%, White 130%); 
    }
    
    
    #CategoriesDiv .k-dropdown-wrap
    {
        background: #707070 !important; 
        box-shadow: 1px 1px 7px -1px #707070; 
        width: 250px !important; 
        height: 26px !important; 
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#707070', endColorstr='#707070', gradientType='0') !important;
        border: 1px solid #707070 !important;
    }
   #CategoriesDiv .k-dropdown-wrap .k-input
    {
        color: white !important; 
    }
    
#ReadingAgeDiv .k-dropdown-wrap 
{
    background: #707070 !important; 
        box-shadow: 1px 1px 7px -1px #707070; 
        width: 250px !important; 
        height: 26px !important; 
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#707070', endColorstr='#707070', gradientType='0') !important;
        border: 1px solid #707070 !important;
}

#ReadingAgeDiv .k-dropdown-wrap .k-input {
color: white !important; 
}


.Div_FullWidth  .k-icon
{
    background: url('Portals/0/images/arrow_prim.png') no-repeat 0px 0px;        
    margin-top: 0px !important;    
    cursor: pointer; 
}

#CategoriesDiv .k-dropdown-wrap .k-select 
{
margin-top: 5px !important;
cursor: pointer;
}
    .colorlevelone
    {
        background-color: #12B8CE
    }
    .colorleveltwo
    {
        background-color: #F9ED35
    }
    


.arrow-right {
	width: 0; 
	height: 0; 
	border-top: 0px solid transparent;
	border-bottom: 32px solid transparent;
	margin-top:-32px;
	margin-left:-4px;
	border-left: 8px solid gray;
	filter: alpha(opacity=45);
	filter: progid:DXImageTransform.Microsoft.Blur(PixelRadius=3,MakeShadow=true,ShadowOpacity=0.30);
	-ms-filter: "progid:DXImageTransform.Microsoft.Blur(PixelRadius=3,MakeShadow=true,ShadowOpacity=0.30)";
}
.arrow-right2 {
	width: 0; 
	height: 0; 
	border-top: 0px solid transparent;
	border-bottom: 32px solid transparent;
	margin-top:-32px;
	margin-left:-4px;
	border-left: 7px solid gray;
	filter: alpha(opacity=45);
	filter: progid:DXImageTransform.Microsoft.Blur(PixelRadius=3,MakeShadow=true,ShadowOpacity=0.30);
	-ms-filter: "progid:DXImageTransform.Microsoft.Blur(PixelRadius=3,MakeShadow=true,ShadowOpacity=0.30)";
}
</style>
<div id="BookSelectionMainDiv" style="width: 100%; float: left; margin-top: 25px; height: auto; margin-bottom: 30px;">
<asp:Label ID="lblGracePeriod" ClientIDMode="Static" runat="server" Style="display:none"></asp:Label>


    <div class="GpNamediv">
        <H4 style="float: left !important;margin-left: 17px !important;margin-top: 20px !important;color: rgb(90, 90, 90); !important">Book selection wizard</H4><br />
        <br />
        <br />
        <p>
            Make the most of your subscription by selecting the eBooks you want. You still have
            time to finalise your book selection. If you haven't filled your subscription when
            the grace period expires the eCollection system will automatically fill your subscription
            with eBooks.
        </p>
    </div>
    <div class="GpMemberCountdiv" style="margin-left: 25px;">
        <img src="<%=Page.ResolveUrl("Portals/0/images/BookSelected.png")%>" alt="BookSelected"
            style="float: left; margin-left: 16px; margin-top: 25px; margin-bottom: 25px;
            margin-right: 6px;" />
        <asp:Label ID="lblBooksSelected" runat="server" CssClass="GPMemCountlbl" Text="" ClientIDMode=Static></asp:Label>
        <span class="GPMemSpan" style="padding-right: 15px">EBOOK/S SELECTED</span>
    </div>
    <div class="GpMemberCountdiv">
        <img src="<%=Page.ResolveUrl("Portals/0/images/BookLeft.png")%>" alt="BookSelected"
            style="float: left; margin-left: 20px; margin-top: 15px; margin-bottom: 15px;
            margin-right: 20px;" />
        <asp:Label ID="lblBooksLeft" runat="server" CssClass="GPMemCountlbl" Text=""></asp:Label>
        <span class="GPMemSpan">EBOOK/S LEFT</span>
    </div> 
    <div class="GpMemberCountdiv" style="margin-right: 1px;">
        <img src="<%=Page.ResolveUrl("Portals/0/images/DaysLeft.png")%>" alt="BookSelected"
            style="float: left; margin-left: 20px; margin-top: 25px; margin-bottom: 25px;
            margin-right: 20px;" />
        <asp:Label ID="lblDaysLeft" runat="server" CssClass="GPMemCountlbl" Text=""></asp:Label>
        <span class="GPMemSpan">DAY/S LEFT</span>
    </div>
    <br />
    <div class="SeeSelectedBookdiv" style="-moz-border-radius: 3px; -webkit-border-radius: 3px;
        border-radius: 3px; -khtml-border-radius: 3px; background: -moz-linear-gradient(top, #689A2E 1%, #74A738 32%, #74A738 43%, #5E872E 95%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#689A2E), color-stop(32%,#74A738), color-stop(43%,#74A738), color-stop(95%,#5E872E));
        background: -webkit-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -o-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -ms-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: linear-gradient(to bottom, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#689A2E', endColorstr='#5E872E',GradientType=0 );
        width: 30.4%; float: left; margin-left: 25px; margin-top: 10px; height: 35px;">
        <asp:Button ID="SeeSelectedBokks" ClientIDMode="Static" runat="server" OnClick="SeeSelectedBokks_Click"
            Text="SEE SELECTED EBOOK/S"  CssClass="SeeSelectedBooksImage" />
    </div>
    <div style="float: left; width: 93.5%; border: 0px; margin-top: 20px; margin-left: 25px;">
        <div id="eCollBookstabstrip" style="float: left; border: 0px; background-color: #E8E8E8;
            border: 1px solid #D3D3D3; width: 100%; height: 45px">
            <ul style="list-style-type: none;">
                <li id="LevelsLi" style="display: inline; float: left; height: 35px; margin-top: 5px;
                    margin-left: 5px;">
                    <asp:HyperLink ID="LevelsLink" runat="server"  CssClass="H5 LevelBtnaltclass"  Text="LEVELS" ></asp:HyperLink>                    
                </li>
                <li id="ReadingAgeLi" style="display: inline; float: left; height: 35px; margin-top: 5px;">
                    <asp:HyperLink ID="ReadingAgeLink" runat="server"  CssClass="H5 LevelBtnaltclass" Text="READING AGE" ></asp:HyperLink>                    
                </li>
                <li id="CategoriesLi" style="display: inline; float: left; height: 35px; margin-top: 5px;">
                    <asp:HyperLink ID="CategoriesLink" runat="server"  CssClass="H5 LevelBtnaltclass" Text="CATEGORIES" ></asp:HyperLink>                   
                </li>
            </ul>
        </div>
    </div>
    <div id="BooksContentDiv" style="float: left; margin-top: 15px; float: left; width: 91%; margin-top: 15px;
        margin-left: 25px;">                 
        <div id="BookSelectionWizardPlace" clientidmode="Static" runat="server" style="float: left; width: 100%;">                    
            <div id="SelectionContentDiv" runat="server" style="display: block;" clientidmode="Static">
                <asp:PlaceHolder ID="BookSelectionContentPlaceHolder" runat="server">
                </asp:PlaceHolder>                                 
            </div>
            
            <div id="SeeAlldiv" clientidmode="Static" runat="server" style="display:none">
            <asp:PlaceHolder ID="SeeAllPlaceHolder" runat="server"></asp:PlaceHolder>
           <%-- <SeeAll:Books_SeeAll ID="BooksSeeAll" runat="server">
                 </SeeAll:Books_SeeAll>  --%>
            </div>

            </div>
            
        </div>
    </div>
  