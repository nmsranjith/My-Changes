function setcustsubsk(custsubsk,subsid,totallicenses,dpnvalue) {
        $("#parentsubsid").val(custsubsk);
        //popuprenewecoll(1, subsid, totallicenses);
		popuprenew(1,'');
		 if (subsid != "") {
               // $("#chooseapp1").data("kendoDropDownList").select(function (dataItem) {
               //     return dataItem.text === subsid;
               // });
                //$('#chooseapp1').data('kendoDropDownList').text(subsid);
				if (subsid == "E-Collection Subscription-200") {
                   // $('#chooseapp1').data('kendoDropDownList').text(subsid);
					$('#chooseapp1').val(dpnvalue);
                }
                else {
                    //$('#chooseapp1').data('kendoDropDownList').text("E-Collection Subscription-100");
					$('#chooseapp1').val(dpnvalue);
                }
				$("#chooseapp1").kendoDropDownList({ dataTextField: "SUBS_ID",
            dataValueField: "SUB_QTY_ALL", change: dropdownchange, select: dropdownselect,
		animation:false
        });
				dropdownchange();
                $('#txtnoofstudents').val(totallicenses);
            }
            else {
                $('#chooseapp1').data('kendoDropDownList').text("Choose subscription pack");
            }
    }
	function httpGet(a){var b=null;b=new XMLHttpRequest();b.open("GET",a,false);b.send(null);return b.responseText}jQuery(function(){jQuery("#DashboardTabHolder").removeClass("selectedTabHolder");jQuery(".ViewBtnHeader").addClass("HideItems");jQuery("#DashboardTab").removeClass("selectedTab");jQuery("#SubscriptionTabHolder").addClass("selectedTabHolder");jQuery("#SubscriptionTab").addClass("selectedTab");if($("#SubstopDiv").css("display")=="none"){$("#eCollectionMenu").hide();$("#eCollectionContent").css("width","955px")}jQuery("#SubscriptionTabHolder").removeClass("HideItems");jQuery("#SubsNameTxt").keyup(function(a){if(a.keyCode==13){jQuery("#OkButton").click()}});$("#OkButton").click(function(){kwindow.data("kendoWindow").close();$.ajax({url:GetFile("/DesktopModules/eCollection_Dashboards/Handlers/Dashboard_Handler.ashx?pageContent=addsubsname&subsid="+jQuery("#SelectedSubsHdn").val()+"&newname="+jQuery("#SubsNameTxt").val().trim()),dataType:"json",success:function(){if(jQuery("#SubsNameTxt").val().trim()!=""){if(jQuery("#SelectedSubscription").text().trim()==jQuery("#"+jQuery("#SelectedSubsHdn").val()).html().trim()){jQuery("#SelectedSubscription").text(jQuery("#SubsNameTxt").val())}jQuery("#"+jQuery("#SelectedSubsHdn").val()).text(jQuery("#SubsNameTxt").val().substr(0,Math.min(30,jQuery("#SubsNameTxt").val().length)));jQuery("#"+jQuery("#SelectedSubsHdn").val()).attr("title",jQuery("#SubsNameTxt").val());jQuery("#SubsText"+jQuery("#SelectedSubsHdn").val()).hide();jQuery("#"+jQuery("#SelectedSubsHdn").val()).show()}else{if(jQuery("#SelectedSubscription").text().trim()==jQuery("#"+jQuery("#SelectedSubsHdn").val()).html().trim()){jQuery("#SelectedSubscription").text(jQuery("#SubsText"+jQuery("#SelectedSubsHdn").val()).text())}jQuery("#SubsText"+jQuery("#SelectedSubsHdn").val()).show();jQuery("#"+jQuery("#SelectedSubsHdn").val()).text("").hide();jQuery("#"+jQuery("#SelectedSubsHdn").val()).attr("title",jQuery("#SubsText"+jQuery("#SelectedSubsHdn").val()).text())}jQuery("#SubsNameTxt").val("")}});return false});$("#CancelButton").click(function(){jQuery("#SubsNameTxt").val("");kwindow.data("kendoWindow").close();return false})});function SetSelected(a){jQuery("#SelectedSubsHdn").val(a)}function AddLabel(a,b){if(a=="ADD LABEL"){jQuery("#popupHeader").text("Add Subscription Name");jQuery("#SubsNameTxt").val("");$("#AddNameLabel").css({display:"block"});$(".k-window-actions.k-header").css("cursor","pointer");kwindow=$("#AddNameLabel");if(!kwindow.data("kendoWindow")){kwindow.kendoWindow({width:"665px",height:"300px",modal:true,draggable:false});kwindow.data("kendoWindow").center()}kwindow.data("kendoWindow").open();$(".k-icon.k-i-close").hide();$("a.k-window-action.k-link").mouseover(function(){$("a.k-window-action.k-link").parent().addClass("popupClosebg");return false});jQuery("#SelectedSubsHdn").val(b)}else{jQuery("#popupHeader").text("Edit Subscription Name");jQuery("#SubsNameTxt").val(jQuery("#"+b).text().substr(0,Math.min(30,jQuery("#"+b).text().length)).trim());$(".k-window-actions.k-header").css("cursor","pointer");kwindow=$("#AddNameLabel");if(!kwindow.data("kendoWindow")){kwindow.kendoWindow({width:"665px",height:"300px",modal:true,draggable:false});kwindow.data("kendoWindow").center()}kwindow.data("kendoWindow").open();$(".k-icon.k-i-close").hide();$("a.k-window-action.k-link").mouseover(function(){$("a.k-window-action.k-link").parent().addClass("popupClosebg");return false});jQuery("#SelectedSubsHdn").val(b)}return false};
