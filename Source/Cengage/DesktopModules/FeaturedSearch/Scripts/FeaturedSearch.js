function closeAlertMsg(a) {
	$(a).parent().parent().addClass("HideItems");	
	}
function AdvanceSearch() {
    var k = $("#AdvanceSearchDiv input:text").length,
		p = 0;
	if ($("#SearchNameTxt").val() != "") {
	 $("#SaveSearchNameerror").addClass("HideItems")
    $("#AdvanceSearchDiv input:text").each(function () {
        if ($(this).val().trim() == "") {
            p++
        }
    });
    if (p == k) {
        $("#AdvTitleTxt").focus();
		$("#SaveSearchItemserror").removeClass("HideItems")
        return
    } else {
	$("#SaveSearchItemserror").addClass("HideItems")	}
    var v = "",
		z = "",
		x = "",
		w = "",
		q = "",
		m = "",
		y = "",
		C = "",
		//b = "",
		//d = "",
		//g = "",
		//j = "",
		epf = "",
		//ef = "",
		lt = "",
		//ty = "";
    z = $("#AdvFormatTxt").val().trim();
    lt= $("#AdvLibraryTypeTxt").val().trim();
    w = $("#AdvOriginTxt").val().trim();
    q = $("#AdvPublishedYearTxt").val().trim();
    x= $("#AdvPublisherTxt").val().trim();
    epf= $("#AdveBookPlatformTxt").val().trim();

    z = z.replace(/^,|,$/g, "");
    x = x.replace(/^,|,$/g, "");
    w = w.replace(/^,|,$/g, "");
    q = q.replace(/^,|,$/g, "");
    epf = epf.replace(/^,|,$/g, "");
    lt = lt.replace(/^,|,$/g, "");
$("#FSFacetDrpDwn option").each(function()
{    
switch($(this).text().toLowerCase())
{
	case "format":
	if(z!='')
		z = $(this).val() + ":" + z;
    z = z.replace(/^:|:$/g, "");
	break;
	case "publisher":
	if(x!='')
		x = $(this).val() + ":" + x;
    x = x.replace(/^:|:$/g, "");
	break;
	case "origin":
	if(w!='')
		w = $(this).val() + ":" + w;
    w = w.replace(/^:|:$/g, "");
	break;
	case "published year":
	if(q!='')
		q = $(this).val() + ":" + q;
    q = q.replace(/^:|:$/g, "");
	break;
	case "ebook platform":
	if(epf!='')
		epf = $(this).val() + ":" + epf;
    epf = epf.replace(/^:|:$/g, "");
	break;
	case "library type":
	if(lt!='')
		lt = $(this).val() + ":" + lt;
    lt = lt.replace(/^:|:$/g, "");
	break;
	default:
	break;
	}
});
    m = $("#AdvAllWordsTxt").val(); ;
    y = $("#AdvExactPhraseTxt").val(); ;
    C = $("#AdvNoWordsTxt").val(); ;

    m = m.replace(/^,|,$/g, "");
    y = y.replace(/^,|,$/g, "");
    C = C.replace(/^,|,$/g, "");

    var D = "division=gale&tq=" + encodeURIComponent($("#AdvTitleTxt").val()) + "&aq=" + encodeURIComponent($("#AdvAuthorTxt").val()) + "&subq=" + encodeURIComponent($("#AdvSubjectTxt").val()) + "&allq=" + encodeURIComponent(m) + "&nq=" + encodeURIComponent(C) + "&etq=" + encodeURIComponent(y);
    D = D + "&fv=" + encodeURIComponent(z) + "&pv=" + encodeURIComponent(x) + "&pyv=" + encodeURIComponent(q) + "&ov=" + encodeURIComponent(w) + "&epf=" + encodeURIComponent(epf) + "&flt=" + encodeURIComponent(lt);
    var url = '';
    if (fGetQueryStringParams("cms") == "t") {
        url = "/search?cms=t&st=ad&" + D
    } else {
        url = "/search?st=ad&" + D
    }

    
	var fsk=0;
	var fhref=window.location.href;
	var href=fhref.split('/fsearch/');
	if(href.length>1){
			fsk=href[1];
	}
		else{}
        var jsonData = {
			'FeaturedSearchSk':fsk,
            'CurrentUrl': url,
            'SearchName': $('#SearchNameTxt').val(),
            'Title': $('#AdvTitleTxt').val(),
            'Author': $('#AdvAuthorTxt').val(),
            'Subject': $('#AdvSubjectTxt').val(),
            'Format': $('#AdvFormatTxt').val(),
            'LibraryType': $('#AdvLibraryTypeTxt').val(),
            'Origin': $('#AdvOriginTxt').val(),
            'PublishedYear': $('#AdvPublishedYearTxt').val(),
            'Publisher': $('#AdvPublisherTxt').val(),
            'EbookPlatform': $('#AdveBookPlatformTxt').val(),
            'AllWords': $('#AdvAllWordsTxt').val(),
            'ExactPhrase': $('#AdvExactPhraseTxt').val(),
            'NoneOfThese': $('#AdvNoWordsTxt').val(),
			'ModuleId': $('#FModuleIdHdn').val()
        }
        jsonData = JSON.stringify(jsonData);
        $.ajax({
            url: GetFile('/desktopmodules/featuredsearch/Handler/FeaturedSearchHandler.ashx?section=setsearchvalue'),
            cache: false,
            type: 'POST',
            async: false,
            data: jsonData,
            success: function (data) {
                window.location.href=href[0];
            }
        });
        return false
    } else {
        $("#SaveSearchNameerror").removeClass("HideItems")
    }
    return false
}
function SelectedItem(section,id) {
    if (section == 1) {
		var fhref=window.location.href;
		var href=fhref.split('/fsearch/');
		if(href.length>1)
			window.location.href=href[0]
        $('#NewSearchDiv').removeClass('HideItems').addClass('ShowItems');
        $('#CurrentSearchListDiv').addClass('HideItems').removeClass('ShowItems');		
    }
    else {
        $('#CurrentSearchListDiv').removeClass('HideItems').addClass('ShowItems');
        $('#NewSearchDiv').addClass('HideItems').removeClass('ShowItems');		
    }
	$('#'+id).attr('checked','true');
}
function fGetQueryStringParams(d) {
    var b = window.location.search.substring(1);
    var c = b.split("&");
    for (var a = 0; a < c.length; a++) {
        var f = c[a].split("=");
        if (f[0] == d) {
            return f[1]
        }
    }
}
function feditsearch(searchsk){
var fhref=window.location.href;
var href=fhref.split('/fsearch/');
if(href.length>1)
	window.location.href=href[0]+"/fsearch/"+searchsk;
else
	window.location.href=fhref+"/fsearch/"+searchsk;
}

function fRemoveItem(fsearchsk, control) {
    var jsonData = {
        'FeaturedSearchSk': fsearchsk
    }
    jsonData = JSON.stringify(jsonData);
    $.ajax({
        url: GetFile('/desktopmodules/featuredsearch/Handler/FeaturedSearchHandler.ashx?section=deletesearch'),
        cache: false,
        type: 'POST',
        async: false,
		data:jsonData,
        success: function (data) {
            $(control).parent().parent().addClass('HideItems');
        }
    });
}

$(function(){	
	var fhref=window.location.href;
	var href=fhref.split('/fsearch/');
	if(href.length>1)
		$('#fnewrd').removeAttr('checked');
});

