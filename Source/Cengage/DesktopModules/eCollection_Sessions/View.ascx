<%@ Control language="C#" Inherits="DotNetNuke.Modules.eCollection_Sessions.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
<div class="MainDiv" id="MainDiv">
<div class="TopBand">
        <div style="width: 70%; float: left;">
            <h1 id="PageHeader" runat="server" clientidmode="Static"></h1>
        </div>
        <div style="width: 28%; float: left;">
            <div id="HeaderBtn"  style="width: 73%;float: right;padding: 7px 4px 7px 0px;display:none" >
                <div id="bg" style="border: 0px solid transparent; text-decoration: initial; width: 25px;float: left;"></div>
            <%--<asp:Button ID="PageHeaderButton1" Text="FINISH CREATING STUDENT PROFILE" runat="server" EnableViewState="false" ClientIDMode="Static" />--%>
                <asp:HyperLink ID="PageHeaderButton" runat="server" EnableViewState="false" ClientIDMode="Static"></asp:HyperLink>
            </div>
        </div>
    </div>
    <div id="eCollectionMenu" class="eCollectionMenuStyle">
        <asp:PlaceHolder ID="eCollectionMenuPlaceHolder" runat="server">
           
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="FunctionalityPlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
    <div id="eCollectionContent" class="eCollectionContentStyle">
        <div style="width: 100%; float: left;z-index: -1000; background-color: white;">
            <asp:PlaceHolder ID="ContentPlaceHolder" runat="server"></asp:PlaceHolder>
        </div>
    </div>
</div>
<div class="HideItems">
    <asp:HiddenField ID="SubsCnt" runat="server" ClientIDMode="Static" />
    <div id="SelectedSubs">
        <div class="Div_FullWidth SubsBannerDiv">
            You are using <span id="SelectedSubscription" runat="server" clientidmode="Static">
            </span>
        </div>
    </div>
</div>
<asp:HiddenField ID="PageName" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="schoolname" runat="server" ClientIDMode="Static" />

<style type="text/css">
    .ui-datepicker-trigger
    {
        position:absolute !important;
        padding-left:3px !important;
        width:18px;
        height:20px;
		margin-top:3px;
		}
    
</style>
<script type="text/javascript">
    jQuery(
     function SetMenuBackground() {
 $('#ecollimg_ecoll').addClass('ecollactive_ecoll');        
         SelectedMenuCss('SessionTabHolder', 'SessionsTab');         
         var contentheight = jQuery('#eCollectionContent').height();
         jQuery('#eCollectionMenu').height(contentheight + 'px');
         jQuery('#eCollectionContent').height((contentheight + 5) + 'px');
     }
    );    
 function SelectedMenuCss(holder, tab) {
        jQuery('#SessionTabHolder').addClass('selectedTabHolder');
        jQuery('#SessionsTab').addClass('selectedTab');
    }

    $(document).ready(function () {
        jQuery('#eCollectionecollnk').addClass('DbecollectionActive'); 
        jQuery('#bannertitle').addClass('bannerMultiProfile');
        //jQuery('#bannertitle h1').html(jQuery('#lblbannertxt').html()).show();
        if (parseInt(jQuery('#SubsCnt').val()) > 1) {
            jQuery(jQuery('#SelectedSubs').html()).appendTo('#masterhead');
            jQuery('#MainDiv').addClass('MultiSubsMainDiv');
            jQuery('.bannersececollection').addClass('bannerMultiHeight');
            $('#SubscriptionTabHolder').show();
        }
        else {
            $('#SubscriptionTabHolder').hide();
        }


        $('#eCollectionlnk').addClass('menuitemactive');
        var contentheight = $('#eCollectionContent').height();
        $('#eCollectionMenu').height(contentheight + 'px');
        $('#eCollectionContent').height((contentheight + 5) + 'px');
        var pagename = (window.location.href).split("=");
        var arr = ['createsession', 'editsession', 'addstudenttosession', 'addgroupstosession', 'addteacherstosession', 'addbookstosession', 'addbookstosession','groups','students','teachers','books'];
        if ($.inArray(pagename[pagename.length - 1].toLowerCase(), arr) > -1) {
            $("#SubscriptionTabHolder").css("visibility", "hidden");
            $("#DashboardTabHolder").css("visibility", "hidden");
            $("#StudentsTabHolder").css("visibility", "hidden");
            $("#GroupsTabHolder").css("visibility", "hidden");
            $("#BooksTabHolder").css("visibility", "hidden");
            $("#TeachersTabHolder").css("visibility", "hidden");
            $(".eCollection_Menu_Mid_hr").css("visibility", "hidden");
            $("#eBookManagementTabHolder").css("visibility", "hidden");
            $("#AppDataTabHolder").css("visibility", "hidden");  
        }
        jQuery('#SessionTabHolder').addClass('selectedTabHolder');
        jQuery('#SessionsTab').addClass('selectedTab');
    });

    jQuery(function () {
        if ($.browser.msie && parseInt($.browser.version, 10) < 10) {
            $('#SearchTextBox').val('Enter your search here ..');
            $("#SearchTextBox").focus(function () {
                if ($(this).val() == 'Enter your search here ..') {
                    $(this).val("");
                }
                
            });
            $("#SearchTextBox").blur(function () {
                if ($(this).val().trim() == "") {
                    $('#SearchTextBox').val('Enter your search here ..');
                }
                

            });
        }

        //if (navigator.platform.indexOf("iPad") == -1) {
            //var today = new Date();
           // var yr1 = today.getFullYear();
            //var maxDate = new Date(yr1, 11, 31);
           // jQuery('#DateofBirthTextBox').kendoDatePicker({ min: today, max: maxDate, format: "dd/MM/yyyy"
           // });         
		var currDate;
		var from = jQuery('#DOBHdFld').val().split("/");
		jQuery('#DateofBirthTextBox').val(new Date(from[2], from[1] - 1, from[0]).format("dd-MM-yyyy"));			
		// jQuery('#DateofBirthTextBox').hide();
		// jQuery('#DateofBirthTextBox').show();	
		   $( "#DateofBirthTextBox" ).datepicker({
			  showOn: "button",
			  dateFormat: "dd-mm-yy",
			  minDate:new Date(),
			  buttonImage: "/portals/0/images/callender.png",
			  buttonImageOnly: true			  
			});
       // }
		//else if(getOsVersion() >= 7)
		//{
		//  jQuery('#DateofBirthTextBox').hide();
		//  jQuery('#DateofBirthTextBox1').show();	
		 //  $( "#DateofBirthTextBox1" ).datepicker({
		//	  showOn: "button",
		//	  dateFormat: "dd-mm-yy",
		//	  setDate:'11-30-2013',
		//	  buttonImage: "/portals/0/images/callender.png",
		//	  buttonImageOnly: true
		//	});		
		//}
    });

   


    function getOsVersion() {
        var agent = window.navigator.userAgent,
         start = agent.indexOf('OS ');

        if (navigator.platform.indexOf("iPad") != -1) {
            return window.Number(agent.substr(start + 3, 3).replace('_', '.'));
        } else {
            return 0;
        };

    }

//    function GetFile(path) {
//        var pathname = window.location.pathname;
//        var temppath = pathname.split('/');
//        var root;
//        //if (temppath[1] == "ecollection.aspx")
//        root = "http://" + window.location.host;
//        //        else
//        //            root = "http://" + window.location.host + "/" + temppath[1];
//        //var root = "http://" + window.location.host + "/";
//        //alert(temppath[1]);
//        var url = root + path;
//        return url;
//    }

</script>
