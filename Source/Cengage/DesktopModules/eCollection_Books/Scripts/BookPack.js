var bpWindow1, bpWindow2;
$(function () {
    bpWindow1 = $('#ContentsPopup');
	bpWindow2 = $('#CustomPackPopup');

    $("#ContentsCloseButton").click(function () {		
        bpWindow1.data("kendoWindow").close();
		$('#PackSk').val('');
		//$('#PackNameHdr').text('');
        return false
    });
	
	 $("#CustomCancelButton").click(function () {
        bpWindow2.data("kendoWindow").close();
        return false
    });
	
	// $("#CustomSaveButton").click(function () {
    //    bpWindow2.data("kendoWindow").close();
    //    return false
    //});
	
	$('#CustomButton').click(function(){
	OpenCustomPack();
		return false;
	});
	
	$("#ContentSearchTxt").keyup(function (b) {
		if (b.keyCode == 13) {
			var a = SearchContents();
			return a;
		} else {}
	});
	
	$("#CustomSearchTxt").keyup(function (b) {
		if (b.keyCode == 13) {
			var a = SearchCustomContents();
			return a;
		} else {}
	});
	
	$('#contentsOrder').change(function(){
		SearchContents();
	});
	$('#CustomOrder').change(function(){
		SearchCustomContents();
	});
	jQuery("#contentsOrder").kendoDropDownList({
		animation: false
	});
	jQuery("#CustomOrder").kendoDropDownList({
		animation: false
	});
	
});

function OpenContents(packSk,packName) {
    $('#PackNameHdr').text($($('#PackName' + packSk)).text());
	$('#PackSk').val(packSk);
	$('#BPContentsHolder').html('');
	$('#BPContentsHolder').animate({ //animate element that has scroll
        scrollTop: 0 //for scrolling
    }, 1000);
	$("#ContentSearchTxt").val('');
	$('#contentsOrder').val('level');
	jQuery("#contentsOrder").kendoDropDownList({
		animation: false
	});
	
	var cntPPhtmlcontent='';
	$.ajax({
		type: "POST",
		url: GetFile('desktopmodules/eCollection_books/BooksHandler.ashx?BooksStatus=contents&packsk=' + packSk),
		dataType: "json",
		async: false,
		contentType: "application/json; charset=utf-8",
		success: function (value) {
			if(value != null && value.length!=0)
			{
				$.each(value, function (i, jsondata) {					
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="popup-image-width">';
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="bp-image-filename"><img src="'+jsondata.IMAGE_FILE_NAME+'" alt="" /></div><div class="bp-bookstitlendesc">';//<a href="' + jsondata.TabUrl + '">' + jsondata.TabName + '</a>';
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="bp-bookstitle" title="'+jsondata.Title+'"><span class="bptitlefonts bp-font-ellipse">' + jsondata.Title+'</span></div>';
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="bp-booksdesc"><div class="bp-font-ellipse"><span class="bpdescfonts">' + jsondata.ReadingLevel+'</span>';
					//cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpfonts"> / ' + jsondata.ReadingAge+'</span>'
					//cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpfonts"> / ' + jsondata.TEXTTYPE+'</span></div>';
					if (jsondata.ReadingAge!=null && jsondata.ReadingAge!=''){cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpdescfonts"> / ' + jsondata.ReadingAge+'</span>';}
					if (jsondata.TEXTTYPE!=null && jsondata.TEXTTYPE!=''){cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpdescfonts" title="'+jsondata.TEXTTYPE+'"> / ' + jsondata.TEXTTYPE+'</span>';}
					cntPPhtmlcontent = cntPPhtmlcontent + '</div></div></div></div>';
				});
				$(cntPPhtmlcontent).appendTo('#BPContentsHolder')
			}
		}
	});
    if (!bpWindow1.data("kendoWindow")) {
        bpWindow1.kendoWindow({
            modal: true,
            draggable: false
        });
    }
    bpWindow1.data("kendoWindow").center();
    bpWindow1.data("kendoWindow").open();
    $(".k-icon.k-i-close").hide();
    $("a.k-window-action.k-link").mouseover(function () {
        return false
    }).addClass('HideItems');
	$('.k-window-titlebar.k-header').addClass('HideItems');
    $("a.k-window-action.k-link").click(function () {        
    });
}

function SearchContents() { 
	var packSk=$('#PackSk').val();
	$('#BPContentsHolder').html('');
	var cntPPhtmlcontent='';
	$.ajax({
		type: "POST",
		url: GetFile('desktopmodules/eCollection_books/BooksHandler.ashx?BooksStatus=contentsearch&q=' + $('#ContentSearchTxt').val()+'&packsk=' + packSk+'&opt='+$('#contentsOrder').val()),
		dataType: "json",
		async: false,
		contentType: "application/json; charset=utf-8",
		success: function (value) {
			if(value != null && value.length!=0)
			{
				$.each(value, function (i, jsondata) {					
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="popup-image-width">';
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="bp-image-filename"><img src="'+jsondata.IMAGE_FILE_NAME+'" alt="" /></div><div class="bp-bookstitlendesc">';//<a href="' + jsondata.TabUrl + '">' + jsondata.TabName + '</a>';
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="bp-bookstitle" title="'+jsondata.Title+'"><span class="bptitlefonts bp-font-ellipse">' + jsondata.Title+'</span></div>';
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="bp-booksdesc"><div class="bp-font-ellipse"><span class="bpdescfonts">' + jsondata.ReadingLevel+'</span>';
					//cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpfonts"> / ' + jsondata.ReadingAge+'</span>'
					//cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpfonts"> / ' + jsondata.TEXTTYPE+'</span></div>';
					if (jsondata.ReadingAge!=null && jsondata.ReadingAge!=''){cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpdescfonts"> / ' + jsondata.ReadingAge+'</span>';}
					if (jsondata.TEXTTYPE!=null && jsondata.TEXTTYPE!=''){cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpdescfonts" title="'+jsondata.TEXTTYPE+'"> / ' + jsondata.TEXTTYPE+'</span>';}
					cntPPhtmlcontent = cntPPhtmlcontent + '</div></div></div></div>';
				});
				$(cntPPhtmlcontent).appendTo('#BPContentsHolder');
			}
			else
			{
				$('<span class="bpnoresult">No result found</span>').appendTo('#BPContentsHolder');
			}
		}
	});

}

function OpenCustomPack() {  
	$('#BPCustomContentsHolder').html('');
	$('#BPCustomContentsHolder').animate({ //animate element that has scroll
        scrollTop: 0 //for scrolling
    }, 1000);
	$("#CustomSearchTxt").val('');
	$('#CustomOrder').val('level');
	jQuery("#contentsOrder").kendoDropDownList({
		animation: false
	});
	$('#BPSaveError').addClass('HideItems').removeClass('ShowItems');
	var cntPPhtmlcontent='';
	$.ajax({
		type: "POST",
		url: GetFile('desktopmodules/eCollection_books/BooksHandler.ashx?BooksStatus=custom'),
		dataType: "json",
		async: false,
		contentType: "application/json; charset=utf-8",
		success: function (value) {
			if(value != null && value.length!=0)
			{
				$.each(value, function (i, jsondata) {					
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="popup-image-width"><div>';
					if(jsondata.Selected=='Y'){cntPPhtmlcontent = cntPPhtmlcontent +'<div class="bp-custom-chkbx"><span id="chkbx'+jsondata.PRODUCT_SK+'" class="bplargecheck" onclick="GetSelectedCount('+jsondata.PRODUCT_SK+')"></span><input id="chk'+jsondata.PRODUCT_SK+'" type="checkbox" class="HideItems" checked="true" value="'+jsondata.PRODUCT_SK+'"></div>';}
					else{cntPPhtmlcontent = cntPPhtmlcontent +'<div class="bp-custom-chkbx"><span id="chkbx'+jsondata.PRODUCT_SK+'" class="bplargeuncheck" onclick="GetSelectedCount('+jsondata.PRODUCT_SK+')"></span><input id="chk'+jsondata.PRODUCT_SK+'" type="checkbox" class="HideItems" value="'+jsondata.PRODUCT_SK+'"></div>';}
					
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="bp-image-filename"><img src="'+jsondata.IMAGE_FILE_NAME+'" alt="" /></div><div class="bp-bookstitlendesc">';//<a href="' + jsondata.TabUrl + '">' + jsondata.TabName + '</a>';
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="bp-bookstitle" title="'+jsondata.Title+'"><span class="bptitlefonts bp-font-ellipse">' + jsondata.Title+'</span></div>';
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="bp-booksdesc"><div class="bp-font-ellipse"><span class="bpdescfonts">' + jsondata.ReadingLevel+'</span>';
					//cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpfonts"> / ' + jsondata.ReadingAge+'</span>'
					//cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpfonts"> / ' + jsondata.TEXTTYPE+'</span></div>';
					if (jsondata.ReadingAge!=null && jsondata.ReadingAge!=''){cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpdescfonts"> / ' + jsondata.ReadingAge+'</span>';}
					if (jsondata.TEXTTYPE!=null && jsondata.TEXTTYPE!=''){cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpdescfonts" title="'+jsondata.TEXTTYPE+'"> / ' + jsondata.TEXTTYPE+'</span></div>';} else{cntPPhtmlcontent = cntPPhtmlcontent +'</div>';}
					cntPPhtmlcontent = cntPPhtmlcontent + '</div></div></div></div>';
					
				});
				$(cntPPhtmlcontent).appendTo('#BPCustomContentsHolder');
				SetSelectedCount();
			}
		}
	});
	if (!bpWindow2.data("kendoWindow")) {
        bpWindow2.kendoWindow({
            modal: true,
            draggable: false
        });
    }
    bpWindow2.data("kendoWindow").center();
    bpWindow2.data("kendoWindow").open();
    $(".k-icon.k-i-close").hide();
    $("a.k-window-action.k-link").mouseover(function () {
        return false
    }).addClass('HideItems');
	$('.k-window-titlebar.k-header').addClass('HideItems');
    $("a.k-window-action.k-link").click(function () {        
    });
	return false;
}


function SearchCustomContents() { 	
	$('#BPSaveError').addClass('HideItems').removeClass('ShowItems');
	$('#BPCustomContentsHolder').html('');
	var cntPPhtmlcontent='';
	$.ajax({
		type: "POST",
		url: GetFile('desktopmodules/eCollection_books/BooksHandler.ashx?BooksStatus=customsearch&q=' + $('#CustomSearchTxt').val()+'&opt='+$('#CustomOrder').val()),
		dataType: "json",
		async: false,
		contentType: "application/json; charset=utf-8",
		success: function (value) {
			if(value != null && value.length!=0)
			{
				$.each(value, function (i, jsondata) {					
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="popup-image-width"><div>';
					if(jsondata.Selected=='Y'){cntPPhtmlcontent = cntPPhtmlcontent +'<div class="bp-custom-chkbx"><span id="chkbx'+jsondata.PRODUCT_SK+'" class="bplargecheck" onclick="GetSelectedCount('+jsondata.PRODUCT_SK+')"></span><input id="chk'+jsondata.PRODUCT_SK+'" type="checkbox" class="HideItems" checked="true" value="'+jsondata.PRODUCT_SK+'"></div>';}
					else{cntPPhtmlcontent = cntPPhtmlcontent +'<div class="bp-custom-chkbx"><span id="chkbx'+jsondata.PRODUCT_SK+'" class="bplargeuncheck" onclick="GetSelectedCount('+jsondata.PRODUCT_SK+')"></span><input id="chk'+jsondata.PRODUCT_SK+'" type="checkbox" class="HideItems" value="'+jsondata.PRODUCT_SK+'"></div>';}
					
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="bp-image-filename"><img src="'+jsondata.IMAGE_FILE_NAME+'" alt="" /></div><div class="bp-bookstitlendesc">';//<a href="' + jsondata.TabUrl + '">' + jsondata.TabName + '</a>';
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="bp-bookstitle" title="'+jsondata.Title+'"><span class="bptitlefonts bp-font-ellipse">' + jsondata.Title+'</span></div>';
					cntPPhtmlcontent = cntPPhtmlcontent + '<div class="bp-booksdesc"><div class="bp-font-ellipse"><span class="bpdescfonts">' + jsondata.ReadingLevel+'</span>';
					//cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpfonts"> / ' + jsondata.ReadingAge+'</span>'
					//cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpfonts"> / ' + jsondata.TEXTTYPE+'</span></div>';
					if (jsondata.ReadingAge!=null && jsondata.ReadingAge!=''){cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpdescfonts"> / ' + jsondata.ReadingAge+'</span>';}
					if (jsondata.TEXTTYPE!=null && jsondata.TEXTTYPE!=''){cntPPhtmlcontent = cntPPhtmlcontent + '<span class="bpdescfonts" title="'+jsondata.TEXTTYPE+'"> / ' + jsondata.TEXTTYPE+'</span></div>';} else{cntPPhtmlcontent = cntPPhtmlcontent +'</div>';}
					cntPPhtmlcontent = cntPPhtmlcontent + '</div></div></div></div>';
					 
				});
				$(cntPPhtmlcontent).appendTo('#BPCustomContentsHolder');
				SetSelectedCount();
			}
			else
			{
				$('<span class="bpnoresult">No result found</span>').appendTo('#BPCustomContentsHolder');
			}
		}
	});

}

function BPSaveBtnClick(){
	var selectedBooks='',unchecked='';
	$('#BPSaveError').addClass('HideItems').removeClass('ShowItems');
	$('#CustomPackPopup input:checked').each(
	function(){
	selectedBooks=selectedBooks+','+$(this).val();
	});
	$("#CustomPackPopup input:checkbox:not(:checked)").each(
	function(){
		unchecked=unchecked+','+$(this).val();
	});
	if(selectedBooks==''){
		$('#BPSaveError').addClass('ShowItems').removeClass('HideItems');
		$('#BPErrorSpan').text('Please choose eBooks before you save your book pack.');
	}
	else
		selectedBooks=selectedBooks.replace(/^,|,$/g, "");
	if(unchecked!='')
		unchecked=unchecked.replace(/^,|,$/g, "");	
	
	var jsonData = {
		'ISBN': selectedBooks,
		'UncheckedISBNs':unchecked
	}
	jsonData = JSON.stringify(jsonData);
	$.ajax({
		type: "POST",
		url: GetFile('desktopmodules/eCollection_books/BooksHandler.ashx?BooksStatus=savecustom'),
		dataType: "json",
		async: false,
		data: jsonData,
		contentType: "application/json; charset=utf-8",
		success: function (value) {
			if(value==1)
			{					
				bpWindow2.data("kendoWindow").close();
			}					
		}
	});
    return false
}

function GetSelectedCount(id){	
	var chLen=$('#CustomPackPopup input:checked').length;
	if(chLen>=100 && $($('#chkbx'+id)).attr('class')=='bplargeuncheck')
	{
		$($('#chkbx'+id)).addClass('bplargeuncheck').removeClass('bplargecheck');
		$('#BPSaveError').addClass('ShowItems').removeClass('HideItems');
		$('#BPErrorSpan').text('A book pack can accommodate only 100 eBooks.');
		return false;
	}
	else	
	{
		$($('#chk'+id)).click();
		$('#BPSaveError').addClass('HideItems').removeClass('ShowItems');
	}
	if($($('#chkbx'+id)).attr('class')=='bplargeuncheck')
		$($('#chkbx'+id)).addClass('bplargecheck').removeClass('bplargeuncheck');
	else
	{
		$($('#chkbx'+id)).addClass('bplargeuncheck').removeClass('bplargecheck');		
	}
	SetSelectedCount(chLen);
}
function closeAlertMsg(id)
{
	$('#'+id).addClass('HideItems').removeClass('ShowItems');
}
function SetSelectedCount(chLen)
{	
	if(chLen==1)
		$('#CustomSelectedCnt').text($('#CustomPackPopup input:checked').length+' book');	
	else
		$('#CustomSelectedCnt').text($('#CustomPackPopup input:checked').length+' books');
}

function ChangeBtnClick()
{
	$('#BPHeader2').addClass('ShowItems').removeClass('HideItems');
	$('#BPHeader1').addClass('HideItems').removeClass('ShowItems');
}

function CancelBtnClick()
{
	$('#BPHeader1').addClass('ShowItems').removeClass('HideItems');
	$('#BPHeader2').addClass('HideItems').removeClass('ShowItems');
}

function SetBookPack()
{
var packsk=0;
packsk=$('input[name=bookpack]:checked', '.books-ecoll').val();

$.ajax({
		type: "POST",
		url: GetFile('desktopmodules/eCollection_books/BooksHandler.ashx?BooksStatus=setbookpack&packsk='+packsk),
		dataType: "json",
		async: false,
		contentType: "application/json; charset=utf-8",
		success: function (value) {
			if(value==1)
			{					
				window.location.href=$('#pageurlhdn').val();
			}					
		}
	});
}

function ShowUpdate() {
        $("#UpdateProgressImg").css("display", "block");
    }
    function EndUpdateProgress() {
        $("#UpdateProgressImg").css("display", "none");
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
    function CheckBoxImgClick(e) {        
        e.parentNode.children[0].click();
        var selectedID = document.getElementById("custItmSKHidden").value.split(',');
        if (e.parentNode.children[0].checked) {
            document.getElementById("custItmSKHidden").value += e.parentNode.children[1].innerHTML.trim() + ",";
            e.parentNode.parentNode.className = "bookRowStyle rowclick";
            e.src = GetFile("/Portals/0/images/tick_student.png");
            var isGracePeriod = jQuery('#lblGracePeriod').text();
            if (isGracePeriod == 'False') {
                $('[id$=RemovetosubsButtontop]').attr('disabled', false);
                $('[id$=RemoveSubButtonbot]').attr('disabled', false);
            }
        }
        else {
            document.getElementById("custItmSKHidden").value = "";
            e.parentNode.parentNode.className = "bookRowStyle";
            e.src = GetFile("/Portals/0/images/circle_big.png");
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
        }
    }
    $(document).ready(function () {
        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
        // This will call the function after postback..
    });
    function PostBack() {

        var searchAutocomplete = $("#SearchTextBox").data("kendoAutoComplete");
        if (searchAutocomplete == undefined) {
            $("#SearchTextBox").kendoAutoComplete({
                dataSource: {
                    transport: {
                        read: {
                            url: GetFile('/DesktopModules/eCollection_Books/BooksHandler.ashx?BooksStatus=booksAutoComplete'),
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

        pageName = 'Profile';       

        $('#eCollectionContent')[0].style.height = "";
        $("#eCollectionContent").height($("#eCollectionContent").children()[0].offsetHeight);
        $('#eCollectionMenu').height((jQuery('#eCollectionContent').height()) + 'px');
        $("#CategoriesDrpList").kendoDropDownList();
        $("#CategoriesDrpList").parent().show();
        //        var displayKendo = $("#CategoriesDrpList").data("kendoDropDownList");
        //        displayKendo.show();
        $("#level").css({ "color": "#1FB5E7" });
        // $("#slider-range").css({ "display": "inline-block", "margin-left": "13px", "margin-top": "20px" });
        $("#bookslistsliderDiv").addClass("divgradient");
        //$("#dialog-message").css({ 'display': 'block' });

        $("#dialog-message").dialog({
            modal: true,
            autoOpen: true,
            show: "blind",
            hide: "explode",
            buttons: {
                "Continue": function () {
                    if ($("#duringradio").is(":checked")) {

                    }
                    else {
                        $(this).dialog("close");
                    }
                }
            }
        });

        $('#SearchTextBox').keypress(function (e) {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    e.preventDefault();
                    $('#SearchButton').click();
                }
            
        });
    }

    $('#SearchButton').removeAttr('disabled');