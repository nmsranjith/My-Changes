var dkwindow, dkwindow1, dkwindow2, dkwindow3, dkwindow4, dkwindow5, dkwindow6, AidNo = 0, FidNo = 0, cmsclick = false;
function GetTitle(title) {
    return title.replace(/'|'$/g, '');
}
function SetUpProductResults() {

    //if($('#FacetFilterDiv input:checked').length>0)
    //{
    var pubyrCheck = 0;
    var yr = new Date().getFullYear();
    $("#FacetFilterDiv span:contains('Published Year')").each(function () {
        var spanYear = $(this).parent().children().first().next().next();
        switch (spanYear.text().toLowerCase().trim()) {
            case "last 3 years":
                spanYear.text((yr) - 2 + ' - ' + yr);
                break;
            case "last 5 years":
                spanYear.text((yr) - 4 + ' - ' + yr);
                break;
            case "this year":
                spanYear.text(yr);
                break;
            default:
                break;
        }
        if ($(this).parent().children().next().attr('checked') == 'checked')
            pubyrCheck++;
    });
    if (pubyrCheck > 0) {
        $("#FacetFilterDiv span:contains('Published Year')").each(function () {
            if ($(this).parent().children().next().attr('checked') != 'checked') {
                $(this).parent().children().next().attr('disabled', 'true');
                $(this).parent().addClass('showonlydisabled');
                $(this).parent().parent().addClass('showonlydisabled').attr("style", "background:none !important;");
                $(this).next().addClass('showonlydisabled');
                $(this).next().next().addClass('showonlydisabled');
            }
            else {
            }
        });
    }
};
function GetQueryStringParams(d) {
    var b = window.location.search.substring(1);
    var c = b.split("&");
    for (var a = 0; a < c.length; a++) {
        var f = c[a].split("=");
        if (f[0] == d) {
            return f[1]
        }
    }
} function CheckAndOpenSaveSearch() {
    $.ajax({
        url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=checksavesearch'),
        cache: false,
        type: 'POST',
        async: false,
        success: function (data) {
            if (data == -1) {
                OpenSaveSearchAnonymousPopup(dkwindow3);
            }
            else if (data == 50) {
                $("#MoreSaveSearch").css({ 'display': 'block' });
                $('.k-window-actions.k-header').css('cursor', 'pointer');
                if (!dkwindow5.data("kendoWindow")) {
                    dkwindow5.kendoWindow({
                        modal: true,
                        draggable: false
                    });
                    dkwindow5.data("kendoWindow").center();
                }
                dkwindow5.data("kendoWindow").open();
                $(".k-icon.k-i-close").hide();
                $('a.k-window-action.k-link').mouseover(function () {
                    return false;
                });
                return false;
            }
            else {
                OpenSaveSearchPopup(dkwindow);
            }
        }
    });
}
function OpenSaveSearchSecondPopup(loginType) {
    if ($("#SearchNameTxt").val() != "") {
        SetSaveSearchValues();
        $("#SaveSearchNameerror").addClass("HideItems");
        if (loginType != "Anonymous")
            dkwindow.data("kendoWindow").close();
        else { }
        $("#SaveSearchPopUp2").css({
            display: "block"
        });
        $(".k-window-actions.k-header").css("cursor", "pointer");
        dkwindow1 = $("#SaveSearchPopUp2");
        if (!dkwindow1.data("kendoWindow")) {
            dkwindow1.kendoWindow({
                modal: true,
                draggable: false
            });
            dkwindow1.data("kendoWindow").center()
        }
        dkwindow1.data("kendoWindow").open();
        $(".k-icon.k-i-close").hide();
        $("a.k-window-action.k-link").mouseover(function () {
            return false
        })
    } else {
        $("#SaveSearchNameerror").removeClass("HideItems")
    }
}
function SetSaveSearchValues() {
    var setUrl = window.location.href;
    if ($('#infotab').parent().attr('class') == 'active')
        setUrl = setUrl + '&cms=t';
    var jsonData = {
        'CurrentUrl': setUrl,
        'KeyWords': $('#SearchTextHdn').val(),
        'SearchName': $('#SearchNameTxt').val(),
        'Division': GetQueryStringParams('division')
    }
    jsonData = JSON.stringify(jsonData);
    $.ajax({
        url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=setsearchvalue'),
        cache: false,
        type: 'POST',
        async: false,
        data: jsonData,
        success: function (data) {
        }
    });
    var U = decodeURIComponent(GetQueryStringParams("q")),
		ab = GetQueryStringParams("dt"),
		I = GetQueryStringParams("dc"),
		H = GetQueryStringParams("dv"),
		J = GetQueryStringParams("ds"),
		Y = decodeURIComponent(GetQueryStringParams("tq")),
		F = decodeURIComponent(GetQueryStringParams("aq")),
		V = decodeURIComponent(GetQueryStringParams("subq")),
		W = GetQueryStringParams("f"),
		aa = decodeURIComponent(GetQueryStringParams("etq")),
		M = decodeURIComponent(GetQueryStringParams("allq")),
		q = decodeURIComponent(GetQueryStringParams("nq")),
		P = decodeURIComponent(GetQueryStringParams("fv")),
		G = decodeURIComponent(GetQueryStringParams("pv")),
		Q = decodeURIComponent(GetQueryStringParams("pyv")),
		O = decodeURIComponent(GetQueryStringParams("ov"));
    ep = decodeURIComponent(GetQueryStringParams("epf"));
    lt = decodeURIComponent(GetQueryStringParams("flt"));
    if (U == undefined || U == "undefined" || U == "") {
        $("#SSKeywords").text("")
    } else {
        $("#SSKeywords").text('"' + U + '"')
    }
    if (Y != "undefined" && Y != "") {
        $("#SSTitle").text(Y);
        $("#PgTitle").removeClass("HideItems")
    } else { } if (F != "undefined" && F != "") {
        $("#SSAuthors").text(F);
        $("#PgAuthors").removeClass("HideItems")
    } else { } if (V != "undefined" && V != "") {
        var X = $("#SSSubject").text().trim();
        if (X != "") {
            $("#SSSubject").html(X + ' <span class="and">AND</span> ' + V)
        } else {
            $("#SSSubject").text(V)
        }
        $("#PgSubject").removeClass("HideItems")
    } else { } if (ab != undefined && ab != "") {
        switch (ab) {
            case "d":
                $("#SSDiscipline").text($("#cat" + I + "d" + H).text());
                $("#PgDiscipline").removeClass("HideItems");
                break;
            case "s":
                var X = $("#SSSubject").text().trim();
                if (X != "") {
                    $("#SSSubject").html('"' + X + '" <span class="and">AND</span> "' + $("#cat" + I + "d" + H + "s" + J).text() + '"')
                } else {
                    $("#SSSubject").text('"' + $("#cat" + I + "d" + H + "s" + J).text() + '"')
                }
                $("#PgSubject").removeClass("HideItems");
                break;
            default:
                $("#SSDisciplineCategory").text($("#cat" + I).text());
                $("#PgDisciplineCategory").removeClass("HideItems");
                break
        }
    } else { } if ($("#collapseTwo input:checked").length > 0) {
        var B = "";
        $("#collapseTwo input:checked").each(function () {
            B = B + ', "' + $(this).next().text() + '"'
        });
        B = B.replace(/^,|,$/g, "");
        B = B.replace(/,|,$/g, ' <span class="and">AND</span>  ');
        B = B.replace(/NEW|NEW$/g, "New Titles");
        B = B.replace(/AUDIENCE|AUDIENCE$/g, "AU/NZ Titles");
        B = B.replace(/PUBLISHER|PUBLISHER$/g, "Published Titles");
        $("#SSShowOnly").html(B);
        $("#PgShowOnly").removeClass("HideItems")
    } else { }
    var Z = "",
		R = "",
		L = "",
		j = "",
		K = "",
		epf = "",
		libtype = "";
    if ($("#FacetFilterDiv input:checked").length > 0) {
        $("#FacetFilterDiv input:checked").each(function () {
            Z = Z + ',"' + $(this).next().text() + '"';
            switch ($(this).next().next().next().text().toLowerCase()) {
                case "format":
                    R = R + ", " + $(this).next().text();
                    $("#PgFormat").removeClass("HideItems");
                    break;
                case "publisher":
                    L = L + ", " + $(this).next().text();
                    $("#PgPublisher").removeClass("HideItems");
                    break;
                case "published year":
                    j = j + ", " + $(this).next().text();
                    $("#PgPublishedYear").removeClass("HideItems");
                    break;
                case "origin":
                    K = K + ", " + $(this).next().text();
                    $("#PgOrigin").removeClass("HideItems");
                    break;
                case "ebook platform":
                    epf = epf + ", " + $(this).next().text();
                    $("#PgeBookPlatform").removeClass("HideItems");
                    break;
                case "library type":
                    libtype = libtype + ", " + $(this).next().text();
                    $("#PgLibraryType").removeClass("HideItems");
                    break;
                default:
                    break
            }
        })
    } else { }
    var k = "",
		A = "",
		D = "";
    if (P != "undefined" && P != "") {
        R = R + ", " + P.split(":")[1];
        $("#PgFormat").removeClass("HideItems")
    } else { } if (G != "undefined" && G != "") {
        L = L + ", " + G.split(":")[1];
        $("#PgPublisher").removeClass("HideItems")
    } else { } if (Q != "undefined" && Q != "") {
        j = j + ", " + Q.split(":")[1];
        $("#PgPublishedYear").removeClass("HideItems")
    } else { } if (O != "undefined" && O != "") {
        K = K + ", " + O.split(":")[1];
        $("#PgOrigin").removeClass("HideItems")
    } else { }
    if (ep != "undefined" && ep != "") {
        epf = epf + ", " + ep.split(":")[1];
        $("#PgeBookPlatform").removeClass("HideItems")
    } else { }
    if (lt != "undefined" && lt != "") {
        libtype = libtype + ", " + lt.split(":")[1];
        $("#PgLibraryType").removeClass("HideItems")
    } else { }
    R = R.replace(/^,|,$/g, "");
    L = L.replace(/^,|,$/g, "");
    j = j.replace(/^,|,$/g, "");
    K = K.replace(/^,|,$/g, "");
    epf = epf.replace(/^,|,$/g, "");
    libtype = libtype.replace(/^,|,$/g, "");
    if (M != "undefined" && M != "") {
        M = M.replace(/,|,$/g, ' <span class="and">AND</span> ');
        $("#SSIncluding").html(M);
        $("#PgIncluding").removeClass("HideItems")
    } else { } if (aa != "undefined" && aa != "") {
        aa = aa.replace(/,|,$/g, ' <span class="and">AND</span> ');
        $("#SSExactWords").html(aa);
        $("#PgExactWords").removeClass("HideItems")
    } else { } if (q != "undefined" && q != "") {
        q = q.replace(/,|,$/g, ' <span class="and">AND</span> ');
        $("#SSExcluding").html(q);
        $("#PgExcluding").removeClass("HideItems")
    } else { }
    $("#SSFormat").html(R);
    $("#SSPublisher").html(L);
    $("#SSPublishedYear").html(j);
    $("#SSOrigin").html(K);
    $("#SSeBookPlatform").html(epf);
    $("#SSLibraryType").html(libtype);
    //$("#SaveSearchPopUp1").css({
    //display: "block"
    //});
    $(".k-window-actions.k-header").css("cursor", "pointer");
}
function OpenSaveSearchPopup(z) {
    if (!z.data("kendoWindow")) {
        z.kendoWindow({
            modal: true,
            draggable: false
        });
        z.data("kendoWindow").center()
    }
    z.data("kendoWindow").open();
    $(".k-icon.k-i-close").hide();
    $("a.k-window-action.k-link").mouseover(function () {
        return false
    });
    return false
}
function OpenSaveSearchAnonymousPopup(a) {
    $("#SaveSearchAnonymous").css({
        display: "block"
    });
    $(".k-window-actions.k-header").css("cursor", "pointer");
    if (!a.data("kendoWindow")) {
        a.kendoWindow({
            modal: true,
            draggable: false
        });
        a.data("kendoWindow").center()
    }
    a.data("kendoWindow").open();
    $(".k-icon.k-i-close").hide();
    $("a.k-window-action.k-link").mouseover(function () {
        return false
    });
    return false
}

function closeAlertMsg(a) {
    $(a).parent().parent().addClass("HideItems");
    if ($('#MultiConfirmBtnDiv').attr('class') == 'HideItems') {
        $("#MultipleISBNPopup").removeClass("multiisbnpopuphgt");
        $(".isbn-popup").removeClass("multiisbnpopuphgt");
    }
    else {
        $("#MultipleISBNPopup").removeClass("multiisbnpopuphgt1");
        $(".isbn-popup").removeClass("multiisbnpopuphgt1");

        $("#MultipleISBNPopup").addClass("multiisbnpopuphgt");
        $(".isbn-popup").addClass("multiisbnpopuphgt");
    }
}

function IconSaveFavorite(b, a) {
    if ($(a).next().attr("class") == "btn btn-onoff" || $(a).next().attr("class") == "btn ipad-btn-onoff") {
        $.ajax({
            url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=favorite&prodId=" + b + "&division=" + GetQueryStringParams("division")),
            dataType: "json",
            async: false,
            success: function (c) {
                if (c == -1) {
                    $("#FavoritePopUp").css({
                        display: "block"
                    });
                    $(".k-window-actions.k-header").css("cursor", "pointer");
                    if (!dkwindow2.data("kendoWindow")) {
                        dkwindow2.kendoWindow({
                            modal: true,
                            draggable: false
                        });
                        dkwindow2.data("kendoWindow").center()
                    }
                    dkwindow2.data("kendoWindow").open();
                    $(".k-icon.k-i-close").hide();
                    $("a.k-window-action.k-link").mouseover(function () {
                        return false
                    });
                    return false
                } else {
                    if (c == 50) {
                        $("#MoreFavoritePopUp").css({
                            display: "block"
                        });
                        $(".k-window-actions.k-header").css("cursor", "pointer");
                        if (!dkwindow4.data("kendoWindow")) {
                            dkwindow4.kendoWindow({
                                modal: true,
                                draggable: false
                            });
                            dkwindow4.data("kendoWindow").center()
                        }
                        dkwindow4.data("kendoWindow").open();
                        $(".k-icon.k-i-close").hide();
                        $("a.k-window-action.k-link").mouseover(function () {
                            return false
                        });
                        return false
                    } else {
                        if (navigator.userAgent.match(/iPad/i) != null) {
                            $("#FavSpan" + b).removeClass("HideItems").addClass("ico-favour-right-ipad");
                            $(a).next().addClass("btn-onoff").addClass("ipad-btn-on").val("FAVOURITE");
                        }
                        else {
                            $("#FavSpan" + b).removeClass("HideItems").addClass("ico-favour-right");
                            $(a).next().addClass("btn-on").val("UNFAVOURITE")
                        }
                    }
                }
            }
        })
    } else {
        $.ajax({
            url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=removefavorite&prodId=" + b + "&division=" + GetQueryStringParams("division")),
            dataType: "json",
            async: false,
            success: function (c) {
                if (navigator.userAgent.match(/iPad/i) != null) {
                    $(a).next().addClass('ipad-btn-onoff').removeClass("btn-onoff").removeClass("btn-on").removeClass("ipad-btn-on").val("FAVOURITE");
                    $("#FavSpan" + b).addClass("HideItems").removeClass("ico-favour-right-ipad").removeClass("ico-favour-right");
                }
                else {
                    $(a).next().removeClass("btn-on").val("FAVOURITE");
                    $(a).next().blur();
                    $("#FavSpan" + b).addClass("HideItems").removeClass("ico-favour-right");

                }
            }
        })
    }
}

function SaveFavorite(b, a) {
    if ($(a).attr("class") == "btn btn-onoff" || $(a).attr("class") == "btn ipad-btn-onoff") {
        $.ajax({
            url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=favorite&prodId=" + b + "&division=" + GetQueryStringParams("division")),
            dataType: "json",
            async: false,
            success: function (c) {
                if (c == -1) {
                    $("#FavoritePopUp").css({
                        display: "block"
                    });
                    $(".k-window-actions.k-header").css("cursor", "pointer");
                    if (!dkwindow2.data("kendoWindow")) {
                        dkwindow2.kendoWindow({
                            modal: true,
                            draggable: false
                        });
                        dkwindow2.data("kendoWindow").center()
                    }
                    dkwindow2.data("kendoWindow").open();
                    $(".k-icon.k-i-close").hide();
                    $("a.k-window-action.k-link").mouseover(function () {
                        return false
                    });
                    return false
                } else {
                    if (c == 50) {
                        $("#MoreFavoritePopUp").css({
                            display: "block"
                        });
                        $(".k-window-actions.k-header").css("cursor", "pointer");
                        if (!dkwindow4.data("kendoWindow")) {
                            dkwindow4.kendoWindow({
                                modal: true,
                                draggable: false
                            });
                            dkwindow4.data("kendoWindow").center()
                        }
                        dkwindow4.data("kendoWindow").open();
                        $(".k-icon.k-i-close").hide();
                        $("a.k-window-action.k-link").mouseover(function () {
                            return false
                        });
                        return false
                    } else {
                        if (navigator.userAgent.match(/iPad/i) != null) {
                            $("#FavSpan" + b).removeClass("HideItems").addClass("ico-favour-right-ipad");
                            $(a).addClass("btn-onoff").addClass("ipad-btn-on").val("FAVOURITE");
                        }
                        else {
                            $("#FavSpan" + b).removeClass("HideItems").addClass("ico-favour-right");
                            $(a).addClass("btn-on").val("UNFAVOURITE")
                        }
                    }
                }
            }
        })
    } else {
        $.ajax({
            url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=removefavorite&prodId=" + b + "&division=" + GetQueryStringParams("division")),
            dataType: "json",
            async: false,
            success: function (c) {
                if (navigator.userAgent.match(/iPad/i) != null) {
                    $(a).addClass('ipad-btn-onoff').removeClass("btn-onoff").removeClass("btn-on").removeClass("ipad-btn-on").val("FAVOURITE");
                    $("#FavSpan" + b).addClass("HideItems").removeClass("ico-favour-right-ipad").removeClass("ico-favour-right");
                }
                else {
                    $(a).blur();
                    $("#FavSpan" + b).addClass("HideItems").removeClass("ico-favour-right");
                    $(a).removeClass("btn-on").val("FAVOURITE");

                }
            }
        })
    }
}

$('#AdvAttrTypesDrpDwn0').change(function () {
    if ($(this).val() != "0" && $("#AdvAttrTypesDrpDwn" + AidNo).val() != 0) {
        AidNo++;
        jQuery('<div class="rows"><div class="recommand"><select id="AdvAttrTypesDrpDwn' + AidNo + '" onchange="AddAttributeItem(AdvAttrTypesDrpDwn' + AidNo + ')" class="rec-dropdown"></select><input type="text" placeholder="" class="iconinput" onkeyup="CheckEnterKeyPress(event)"/><span class="ico-clearclose" onclick="RemoveItem(this)"> </span></div></div>').appendTo("#AttributeSet");
        $("#AdvAttrTypesDrpDwn0 option").clone().appendTo("#AdvAttrTypesDrpDwn" + AidNo);
        $("#AdvAttrTypesDrpDwn" + AidNo).val("0");
        jQuery("#AdvAttrTypesDrpDwn" + AidNo).kendoDropDownList({
            animation: false
        })
    } else { }
});

function AddAttributeItem(a) {
    if ($(a).val() != "0" && $("#AdvAttrTypesDrpDwn" + AidNo).val() != 0) {
        AidNo++;
        jQuery('<div class="rows"><div class="recommand"><select id="AdvAttrTypesDrpDwn' + AidNo + '" onchange="AddAttributeItem(AdvAttrTypesDrpDwn' + AidNo + ')" class="rec-dropdown"></select><input type="text" placeholder="" class="iconinput" onkeyup="CheckEnterKeyPress(event)"/><span class="ico-clearclose" onclick="RemoveItem(this)"> </span></div></div>').appendTo("#AttributeSet");
        $("#AdvAttrTypesDrpDwn0 option").clone().appendTo("#AdvAttrTypesDrpDwn" + AidNo);
        $("#AdvAttrTypesDrpDwn" + AidNo).val("0");
        jQuery("#AdvAttrTypesDrpDwn" + AidNo).kendoDropDownList({
            animation: false
        })
    } else { }
}
$('#FilterResultsDrpDwn0').change(function () {
    if ($(this).val() != "0" && $("#FilterResultsDrpDwn" + FidNo).val() != 0) {
        FidNo++;
        jQuery('<div class="rows"><div class="recommand"><select id="FilterResultsDrpDwn' + FidNo + '" class="rec-dropdown" onchange="AddFilterItems(FilterResultsDrpDwn' + FidNo + ')"></select><input type="text" placeholder="" class="iconinput" onkeyup="CheckEnterKeyPress(event)"/><span class="ico-clearclose" onclick="RemoveItem(this)"> </span></div></div>').appendTo("#FilterSet");
        $("#FilterResultsDrpDwn0 option").clone().appendTo("#FilterResultsDrpDwn" + FidNo);
        $("#FilterResultsDrpDwn" + FidNo).val("0");
        jQuery("#FilterResultsDrpDwn" + FidNo).kendoDropDownList({
            animation: false
        })
    } else { }
});
function AddFilterItems(a) {
    if ($(a).val() != "0" && $("#FilterResultsDrpDwn" + FidNo).val() != 0) {
        FidNo++;
        jQuery('<div class="rows"><div class="recommand"><select id="FilterResultsDrpDwn' + FidNo + '" class="rec-dropdown" onchange="AddFilterItems(FilterResultsDrpDwn' + FidNo + ')"></select><input type="text" placeholder="" class="iconinput" onkeyup="CheckEnterKeyPress(event)"/><span class="ico-clearclose" onclick="RemoveItem(this)"> </span></div></div>').appendTo("#FilterSet");
        $("#FilterResultsDrpDwn0 option").clone().appendTo("#FilterResultsDrpDwn" + FidNo);
        $("#FilterResultsDrpDwn" + FidNo).val("0");
        jQuery("#FilterResultsDrpDwn" + FidNo).kendoDropDownList({
            animation: false
        })
    } else { }
}
function RemoveItem(a) {
    $(a).parent().parent().remove()
}
function OpenCloseOptions(b, a) {
    $(b).click(function () {
        if ($(this).attr("class") != "btn-group open") {
            $(this).addClass("open");
            $(a).css("display", "block")
        } else {
            $(this).removeClass("open");
            $(a).css("display", "none")
        }
    })
}
function SetCommonQueryStrings(b, w, h, g, k, q, x) {
    var m = GetQueryStringParams("q");
    var v = GetQueryStringParams("division");
    var d = GetQueryStringParams("sf");
    var j = GetQueryStringParams("p");
    var p = GetQueryStringParams("f");
    if (m == undefined || m == "") {
        m = ""
    } else { } if (v == undefined || v == "") {
        v = "primary"
    } else { } if (j == undefined || j == "") {
        j = 1
    } else { } if (d == undefined || d == "") {
        d = ""
    } else { } if (p == undefined || p == "") {
        p = ""
    } else { }
    return CheckForAdvanveSearch(v, m, p, d, 1, b, w, h, g, k, q, x)
}
function OnSortSelect(g) {
    var d = GetQueryStringParams("s");
    if (d == undefined || d == "") {
        d = "R"
    } else { } if (g != d.toUpperCase()) {
        switch (g) {
            case "A":
                $("#SortResultsDrpDown").parent().children().next().children().first().text("Title (A to Z)");
                break;
            case "D":
                $("#SortResultsDrpDown").parent().children().next().children().first().text("Title (Z to A)");
                break;
            case "L":
                $("#SortResultsDrpDown").parent().children().next().children().first().text("Latest");
                break;
            default:
                $("#SortResultsDrpDown").parent().children().next().children().first().text("Recommended");
                break
        }
        var c = GetQueryStringParams("dt"),
			f = GetQueryStringParams("dc"),
			b = GetQueryStringParams("dv"),
			a = GetQueryStringParams("ds");
        if (c == undefined || c == "") {
            c = ""
        } else { } if (f == undefined || f == "") {
            f = ""
        } else { } if (b == undefined || b == "") {
            b = ""
        } else { } if (a == undefined || a == "") {
            a = ""
        } else { }
        SetCommonQueryStrings(g, c, f, b, a, 1, 1)
    } else { }
}



$('#ProductsSortDpdwn').change(function () {
    var d = GetQueryStringParams("s");
    if (d == undefined || d == "") {
        d = "R"
    } else { } if ($(this).val() != d.toUpperCase()) {
        g = $(this).val();
        switch (g.toUpperCase()) {
            case "A":
                $("#SortResultsDrpDown").parent().children().next().children().first().text("Title (A to Z)");
                break;
            case "D":
                $("#SortResultsDrpDown").parent().children().next().children().first().text("Title (Z to A)");
                break;
            case "L":
                $("#SortResultsDrpDown").parent().children().next().children().first().text("Latest");
                break;
            default:
                $("#SortResultsDrpDown").parent().children().next().children().first().text("Recommended");
                break
        }
        var c = GetQueryStringParams("dt"),
			f = GetQueryStringParams("dc"),
			b = GetQueryStringParams("dv"),
			a = GetQueryStringParams("ds");
        if (c == undefined || c == "") {
            c = ""
        } else { } if (f == undefined || f == "") {
            f = ""
        } else { } if (b == undefined || b == "") {
            b = ""
        } else { } if (a == undefined || a == "") {
            a = ""
        } else { }
        SetCommonQueryStrings(g, c, f, b, a, 1, 1)
    } else { }
});

function ShowOnlyFilter(g, parId) {
    if ($(parId).attr('class') != 'showonlydisabled') {
        // switch (g) {
        //     case 1:
        //	if($('#ShowOnlyNew').attr('checked')!='checked')
        //        $('#ShowOnlyNew').attr('checked', 'checked');
        //	else
        //		$('#ShowOnlyNew').removeAttr('checked');
        //        break;
        //    case 2:
        //	if($('#ShowOnlyAudienceCode').attr('checked')!='checked')
        //         $('#ShowOnlyAudienceCode').attr('checked', 'checked');
        //	else
        //		$('#ShowOnlyAudienceCode').removeAttr('checked');
        //         break;
        //     case 3:
        //	if($('#ShowOnlyPublished').attr('checked')!='checked')
        //          $('#ShowOnlyPublished').attr('checked', 'checked');
        //	else
        //		$('#ShowOnlyPublished').removeAttr('checked');
        //         break;
        //     default:
        //         break;
        // }
        var b = "";
        $("#collapseTwo input:checkbox").each(function () {
            if ($(this).attr("checked") == "checked") {
                $("#ShowOnlyChk" + $(this).val()).addClass("ico-check").removeClass("ico-uncheck");
                b = b + "," + $(this).val()
            } else {
                $("#ShowOnlyChk" + $(this).val()).removeClass("ico-check").addClass("ico-uncheck")
            }
        });
        b = b.replace(/^,|,$/g, "");
        var j = GetQueryStringParams("f"),
		d = GetQueryStringParams("q"),
		c = GetQueryStringParams("division");
        if (j == undefined || j == "") {
            j = ""
        } else { } if (d == undefined || d == "") {
            d = ""
        } else { } if (c != undefined) {
            switch (c.toUpperCase()) {
                case "PRIMARY":
                    break;
                case "SECONDARY":
                    break;
                case "HIGHER":
                    break;
                case "HIGHEREDUCATION":
                    break;
                case "VOCATIONAL":
                    break;
                case "GALE":
                    break;
                case "ALL":
                    break;
                default:
                    c = "all";
                    break
            }
        } else {
            c = "all"
        }
        var k = GetQueryStringParams("s");
        if (k != undefined) {
            switch (k.toUpperCase()) {
                case "R":
                    break;
                case "L":
                    break;
                case "A":
                    break;
                case "D":
                    break;
                default:
                    k = "R";
                    break
            }
        } else {
            k = "R"
        }
        var h = GetQueryStringParams("dt"),
		p = GetQueryStringParams("dc"),
		m = GetQueryStringParams("dv"),
		l = GetQueryStringParams("ds");
        if (h == undefined || h == "") {
            h = ""
        } else { } if (p == undefined || p == "") {
            p = ""
        } else { } if (m == undefined || m == "") {
            m = ""
        } else { } if (l == undefined || l == "") {
            l = ""
        } else { }
        $.ajax({
            url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=clearfacets'),
            type: "POST",
            cache: false,
            async: false,
            success: function () {
            }
        });
        CheckForAdvanveSearch(c, d, j, b, 1, k, h, p, m, l, 1, 1)
    }
    else { }
}
function ClearHierarchy() {
    $('#HierarchyFilterHdn').val('n');
    var a = GetQueryStringParams("s");
    if (a == undefined || a == "") {
        a = "R"
    } else { }
    $.ajax({
        url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=clearfacets'),
        type: "POST",
        cache: false,
        async: false,
        success: function () {
        }
    });
    SetCommonQueryStrings(a, "", "", "", "", 1, 1);
}
function SetCategoryFilter(b) {
    $('#HierarchyFilterHdn').val('y');
    var a = GetQueryStringParams("s");
    $.ajax({
        url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=clearfacets'),
        type: "POST",
        cache: false,
        async: false,
        success: function () {
        }
    });
    if (a == undefined || a == "") {
        a = "R"
    } else { }
    //if(GetQueryStringParams("dt")!=undefined && GetQueryStringParams("dt")=='dc' && GetQueryStringParams("dc")!=undefined && GetQueryStringParams("dc")==b)
    //SetCommonQueryStrings(a, "", "", "", "", 1, 1);
    //else
    SetCommonQueryStrings(a, "dc", b, "", "", 1, 1);
}
function SetDisciplineFilter(c, b) {
    $('#HierarchyFilterHdn').val('y');
    var a = GetQueryStringParams("s");
    $.ajax({
        url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=clearfacets'),
        type: "POST",
        cache: false,
        async: false,
        success: function () {
        }
    });
    if (a == undefined || a == "") {
        a = "R"
    } else { }
    //if(GetQueryStringParams("dt")!=undefined && GetQueryStringParams("dc")!=undefined && GetQueryStringParams("dv")!=undefined && GetQueryStringParams("dt")=='d' && GetQueryStringParams("dc")==c && GetQueryStringParams("dv")==b)
    //SetCommonQueryStrings(a, "", "", "", "", 1, 1);
    //else
    SetCommonQueryStrings(a, "d", c, b, "", 1, 1)
}
function SetSubjectFilter(b, d, c) {
    $('#HierarchyFilterHdn').val('y');
    var a = GetQueryStringParams("s");
    $.ajax({
        url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=clearfacets'),
        type: "POST",
        cache: false,
        async: false,
        success: function () {
        }
    });
    if (a == undefined || a == "") {
        a = "R"
    } else { }
    SetCommonQueryStrings(a, "s", b, d, c, 1, 1)
}
function CLearFacetFilter(a) {
    $("#Collapse" + a + " input:checked").each(function () {
        $(this).removeAttr("checked")
    });
    SetFacetFilter()
}
var fcntcheck = 0;
function SetFacetFilter(parentid, chkBxId, spId) {
    if (fcntcheck == 0) {
        fcntcheck = 1;
        if ($('#' + chkBxId).attr('disabled') != 'true' && $('#' + chkBxId).attr('disabled') != 'disabled') {
            //if($(spId).attr("class")=='ico-uncheck'){
            //	$(spId).addClass('ico-check').removeClass('ico-uncheck');
            //	$('#'+chkBxId).attr('checked','checked');
            //}
            //else{
            //	$(spId).addClass('ico-uncheck').removeClass('ico-check');
            //	$('#'+chkBxId).removeAttr('checked');
            //}
        }
        else {
            return;
        }
        var pfId = parentid;
        if ($('#Collapse' + parentid + ' input:checked').length == 0) {
            pfId = -1;
        }
        var pubyrCheck = 0;
        $("#FacetFilterDiv span:contains('Published Year')").each(function () {
            if ($(this).parent().children().next().attr('checked') == 'checked')
                pubyrCheck++;
        });
        if (pubyrCheck > 0) {
            $("#FacetFilterDiv span:contains('Published Year')").each(function () {
                if ($(this).parent().children().next().attr('checked') != 'checked') {
                    $(this).parent().children().next().attr('disabled', 'true');
                    $(this).parent().addClass('showonlydisabled');
                    $(this).parent().parent().addClass('showonlydisabled');
                    $(this).next().addClass('showonlydisabled');
                    $(this).next().next().addClass('showonlydisabled');
                }
                else {
                }
            });
        }
        var fq = "", fqCheck = "", selectedfacets = [];
        $('#FacetFilterDiv input:checkbox').each(function () {
            var facetValue = $(this).val().split('|');
            var at1, at2, at3, at4, at5;
            if ($(this).attr('checked') == 'checked') {
                $('#FacetChk' + $(this).attr('id')).addClass('ico-check').removeClass('ico-uncheck');
                if ($(this).next().next().next().text().toLowerCase() == "published year") {
                    // $('#Collapse' + parentid + ' input:checkbox').parent().parent().addClass('HideItems');                
                    $(this).parent().children().next().removeAttr('disabled');
                    $(this).parent().removeClass('showonlydisabled');
                    $(this).parent().parent().removeClass('showonlydisabled');
                    $(this).next().removeClass('showonlydisabled');
                    $(this).next().next().removeClass('showonlydisabled');
                }
                else {
                }
                if (facetValue[0] == pfId)
                    selectedfacets.push({ ATTRIBUTE_TYPE_SK: facetValue[0],
                        ATTRIBUTE_TYPE_value_SK: facetValue[1],
                        ATTRIBUTE_NAME: facetValue[2],
                        ATTRIBUTE_TYPE_VALUE: facetValue[3],
                        PROD_COUNT: facetValue[4],
                        IS_SELECTED: 'Y',
                        IS_CURRENT: 'Y',
                        IS_PARENT: 'N'
                    });
                else
                    selectedfacets.push({ ATTRIBUTE_TYPE_SK: facetValue[0],
                        ATTRIBUTE_TYPE_value_SK: facetValue[1],
                        ATTRIBUTE_NAME: facetValue[2],
                        ATTRIBUTE_TYPE_VALUE: facetValue[3],
                        PROD_COUNT: facetValue[4],
                        IS_SELECTED: 'Y',
                        IS_CURRENT: 'N',
                        IS_PARENT: 'N'
                    });
                if (fqCheck == facetValue[0])
                    fq = fq + ',' + facetValue[1];
                else {
                    fqCheck = facetValue[0];
                    if (at1 != facetValue[0] && at2 != '0' && at3 != facetValue[2] && at4 != '') {
                        if (facetValue[0] == pfId)
                            selectedfacets.push({ ATTRIBUTE_TYPE_SK: facetValue[0], ATTRIBUTE_TYPE_value_SK: "0", ATTRIBUTE_NAME: facetValue[2], ATTRIBUTE_TYPE_VALUE: '', PROD_COUNT: $('#FacetProductsCount' + parentid).text().trim(), IS_SELECTED: 'Y', IS_CURRENT: 'Y', IS_PARENT: 'Y' });
                        else
                            selectedfacets.push({ ATTRIBUTE_TYPE_SK: facetValue[0], ATTRIBUTE_TYPE_value_SK: "0", ATTRIBUTE_NAME: facetValue[2], ATTRIBUTE_TYPE_VALUE: '', PROD_COUNT: $('#FacetProductsCount' + parentid).text().trim(), IS_SELECTED: 'Y', IS_CURRENT: 'N', IS_PARENT: 'Y' });
                        fq = fq + ':' + facetValue[0] + '_' + facetValue[1];
                        at1 = facetValue[0]; at2 = '0'; at3 = facetValue[2]; at4 = '';
                    }
                }
            }
            else {
                $('#FacetChk' + $(this).attr('id')).removeClass('ico-check').addClass('ico-uncheck');
                if (facetValue[0] != pfId) {
                    if (at1 != facetValue[0] && at2 != '0' && at3 != facetValue[2] && at4 != '') {
                        selectedfacets.push({ ATTRIBUTE_TYPE_SK: facetValue[0], ATTRIBUTE_TYPE_value_SK: "0", ATTRIBUTE_NAME: facetValue[2], ATTRIBUTE_TYPE_VALUE: '', PROD_COUNT: $('#FacetProductsCount' + parentid).text().trim(), IS_SELECTED: 'N', IS_CURRENT: 'N', IS_PARENT: 'Y' });
                        at1 = facetValue[0]; at2 = '0'; at3 = facetValue[2]; at4 = '';
                    }
                }

                if (facetValue[0] == pfId)
                    selectedfacets.push({ ATTRIBUTE_TYPE_SK: facetValue[0], ATTRIBUTE_TYPE_value_SK: facetValue[1], ATTRIBUTE_NAME: facetValue[2], ATTRIBUTE_TYPE_VALUE: facetValue[3], PROD_COUNT: facetValue[4], IS_SELECTED: 'N', IS_CURRENT: 'Y', IS_PARENT: 'N' });
                else
                    selectedfacets.push({ ATTRIBUTE_TYPE_SK: facetValue[0], ATTRIBUTE_TYPE_value_SK: facetValue[1], ATTRIBUTE_NAME: facetValue[2], ATTRIBUTE_TYPE_VALUE: facetValue[3], PROD_COUNT: facetValue[4], IS_SELECTED: 'N', IS_CURRENT: 'N', IS_PARENT: 'N' });
                // if (facetValue[0] != parentid)               
                //     selectedfacets.push({ ATTRIBUTE_TYPE_SK: facetValue[0], ATTRIBUTE_TYPE_value_SK: 0, ATTRIBUTE_NAME: facetValue[2], ATTRIBUTE_TYPE_VALUE: '', PROD_COUNT: $('#FacetProductsCount' + parentid).text().trim(), IS_SELECTED: 'N', IS_CURRENT: 'N', IS_PARENT: 'N' });

            }
        });

        $.ajax({
            url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=currentfacets'),
            type: "POST",
            cache: false,
            data: JSON.stringify(selectedfacets),
            async: false,
            success: function (data) {
            }
        });

        fq = fq.replace(/^,|,$/g, ''); fq = fq.replace(/^:|:$/g, ''); fq = fq.replace(/^_|_$/g, '');

        var SOFilter = GetQueryStringParams("sf"), querykey = GetQueryStringParams("q"), division = GetQueryStringParams("division");
        if (SOFilter == undefined || SOFilter == '') SOFilter = ""; else { }
        if (querykey == undefined || querykey == '') querykey = ""; else { }
        if (division != undefined) {
            switch (division.toUpperCase()) {
                case "PRIMARY":
                    break;
                case "SECONDARY":
                    break;
                case "HIGHER":
                    break;
                case "HIGHEREDUCATION":
                    break;
                case "VOCATIONAL":
                    break;
                case "GALE":
                    break;
                case "ALL":
                    break;
                default: division = "all";
                    break;
            }
        }
        else
            division = "all";

        var sort = GetQueryStringParams('s');
        if (sort != undefined) {
            switch (sort.toUpperCase()) {
                case "R":
                    break;
                case "L":
                    break;
                case "A":
                    break;
                case "D":
                    break;
                default: sort = "R";
                    break;
            }
        }
        else {
            sort = "R";
        }
        var dt = GetQueryStringParams("dt"), dc = GetQueryStringParams("dc"), dv = GetQueryStringParams("dv"), ds = GetQueryStringParams("ds");
        if (dt == undefined || dt == '') dt = ""; else { }
        if (dc == undefined || dc == '') dc = ""; else { }
        if (dv == undefined || dv == '') dv = ""; else { }
        if (ds == undefined || ds == '') ds = ""; else { }
        CheckForAdvanveSearch(division, querykey, fq, SOFilter, 1, sort, dt, dc, dv, ds, 1, 1);
    }
    else
        fcntcheck = 0;
}
function CheckForAdvanveSearch(z, U, ab, I, H, J, Y, F, V, W, aa, M) {
    var q = GetQueryStringParams("st"),
		P = "", bwf = GetQueryStringParams("format");
    if (q != undefined && q != "") {
        switch (q.toLowerCase()) {
            case "ad":
                var G = GetQueryStringParams("tq");
                var Q = GetQueryStringParams("aq");
                var O = GetQueryStringParams("subq");
                var X = GetQueryStringParams("faq");
                var B = GetQueryStringParams("fatvq");
                var Z = GetQueryStringParams("allq");
                var R = GetQueryStringParams("etq");
                var L = GetQueryStringParams("nq");
                var j = GetQueryStringParams("fv");
                var K = GetQueryStringParams("pv");
                var k = GetQueryStringParams("pyv");
                var A = GetQueryStringParams("ov");
                var epf = GetQueryStringParams("epf");
                var lt = GetQueryStringParams("flt");
                if (G == undefined) {
                    G = ""
                } else { } if (Q == undefined) {
                    Q = ""
                } else { } if (O == undefined) {
                    O = ""
                } else { } if (X == undefined) {
                    X = ""
                } else { } if (B == undefined) {
                    B = ""
                } else { } if (Z == undefined) {
                    Z = ""
                } else { } if (R == undefined) {
                    R = ""
                } else { } if (L == undefined) {
                    L = ""
                } else { } if (j == undefined) {
                    j = ""
                } else { } if (K == undefined) {
                    K = ""
                } else { } if (k == undefined) {
                    k = ""
                } else { } if (A == undefined) {
                    A = ""
                } else { }
                if (epf == undefined) {
                    epf = "";
                } else { }
                if (lt == undefined) {
                    lt = "";
                } else { }
                if (ab != "" && ab != undefined) {
                    P = "search?st=ad&division=" + z + "&p=" + H + "&s=" + J + "&tq=" + G + "&aq=" + Q + "&subq=" + O + "&faq=" + X + "&fatvq=" + B + "&allq=" + Z + "&nq=" + L + "&etq=" + R + "&f=" + ab + "&sf=" + I + "&dt=" + Y + "&dc=" + F + "&dv=" + V + "&ds=" + W + "&fv=" + j + "&pv=" + K + "&pyv=" + k + "&ov=" + A + "&epf=" + epf + "&flt=" + lt
                } else {
                    P = "search?st=ad&division=" + z + "&p=" + H + "&s=" + J + "&tq=" + G + "&f=&aq=" + Q + "&subq=" + O + "&faq=" + X + "&fatvq=" + B + "&allq=" + Z + "&nq=" + L + "&etq=" + R + "&sf=" + I + "&dt=" + Y + "&dc=" + F + "&dv=" + V + "&ds=" + W + "&fv=" + j + "&pv=" + K + "&pyv=" + k + "&ov=" + A + "&epf=" + epf + "&flt=" + lt
                }
                break;
            case "advisbn":
                var D = GetQueryStringParams("keyisbn");
                if (D == undefined) {
                    D = ""
                }
                if (ab != "" && ab != undefined) {
                    P = "search?st=advisbn&division=" + z + "&p=" + H + "&s=" + J + "&f=" + ab + "&keyisbn=" + D + "&sf=" + I + "&dt=" + Y + "&dc=" + F + "&dv=" + V + "&ds=" + W
                } else {
                    P = "search?st=advisbn&division=" + z + "&p=" + H + "&s=" + J + "&keyisbn=" + D + "&f=&sf=" + I + "&dt=" + Y + "&dc=" + F + "&dv=" + V + "&ds=" + W
                }
                break;
            default:
                break
        }
    } else {
        if (ab != "" && ab != undefined) {
            P = "search?q=" + U + "&division=" + z + "&p=" + H + "&s=" + J + "&f=" + ab + "&sf=" + I + "&dt=" + Y + "&dc=" + F + "&dv=" + V + "&ds=" + W
        } else {
            P = "search?q=" + U + "&division=" + z + "&p=" + H + "&s=" + J + "&f=&sf=" + I + "&dt=" + Y + "&dc=" + F + "&dv=" + V + "&ds=" + W
        }
    }
    if ($("#AdvSearchOpenFlagHdn1").val() == "t") {
        P = P + "&ao=t"
    } else { }
    if ($("#HierarchyFilterHdn").val() == "y") {
        P = P + "&hf=y"
    } else { }
    if (bwf != undefined) {
        P = P + "&format=" + bwf;
    } else { }
    return ReturnOrRefreshUrl(M, P, aa)
}
function ReturnOrRefreshUrl(c, b, a) {
    if (c == 0) {
        return b + "&cms=t&cp=" + a
    } else {
        window.open(b, "_self")
    }
}
function AdvanceSearch() {
    var k = $("#AdvanceSearchDiv input:text").length,
		p = 0;
    $("#AdvanceSearchDiv input:text").each(function () {
        if ($(this).val().trim() == "") {
            p++
        }
    });
    if (p == k) {
        $("#AdvTitleTxt").focus();
        return
    } else { }
    var v = "",
		z = "",
		x = "",
		w = "",
		q = "",
		m = "",
		y = "",
		C = "",
		b = "",
		d = "",
		g = "",
		j = "",
		epf = "",
		ef = "",
		lt = "",
		ty = "";
    $("#AttributeSet select.rec-dropdown").each(function () {
        var a = $(this).parent().parent().children("input").val();
        if (a != "") {
            switch ($(this).find("option:selected").text().toLowerCase()) {
                case "format":
                    z = z + "," + a;
                    b = $(this).val();
                    break;
                case "publisher":
                    x = x + "," + a;
                    d = $(this).val();
                    break;
                case "published year":
                    w = w + "," + a;
                    g = $(this).val();
                    break;
                case "origin":
                    q = q + "," + a;
                    j = $(this).val();
                    break;
                case "ebook platform":
                    epf = epf + "," + a;
                    ef = $(this).val();
                    break;
                case "library type":
                    lt = lt + "," + a;
                    ty = $(this).val();
                    break;
                default:
                    break
            }
        } else { }
    });
    z = z.replace(/^,|,$/g, "");
    x = x.replace(/^,|,$/g, "");
    w = w.replace(/^,|,$/g, "");
    q = q.replace(/^,|,$/g, "");
    z = b + ":" + z;
    z = z.replace(/^:|:$/g, "");
    x = d + ":" + x;
    x = x.replace(/^:|:$/g, "");
    w = g + ":" + w;
    w = w.replace(/^:|:$/g, "");
    q = j + ":" + q;
    q = q.replace(/^:|:$/g, "");
    epf = ef + ":" + epf;
    epf = epf.replace(/^:|:$/g, "");
    lt = ty + ":" + lt;
    lt = lt.replace(/^:|:$/g, "");
    $("#FilterSet select.rec-dropdown").each(function () {
        if ($(this).next().val() != "") {
            switch ($(this).val()) {
                case "1":
                    m = m + "," + $(this).parent().parent().children("input").val();
                    break;
                case "2":
                    y = y + "," + $(this).parent().parent().children("input").val();
                    break;
                case "3":
                    C = C + "," + $(this).parent().parent().children("input").val();
                    break;
                default:
                    break
            }
        } else { }
    });
    m = m.replace(/^,|,$/g, "");
    y = y.replace(/^,|,$/g, "");
    C = C.replace(/^,|,$/g, "");

    var D = "division=" + GetQueryStringParams("division") + "&tq=" + encodeURIComponent($("#AdvTitleTxt").val()) + "&aq=" + encodeURIComponent($("#AdvAuthorTxt").val()) + "&subq=" + encodeURIComponent($("#AdvSubjectTxt").val()) + "&allq=" + encodeURIComponent(m) + "&nq=" + encodeURIComponent(C) + "&etq=" + encodeURIComponent(y);
    D = D + "&fv=" + encodeURIComponent(z) + "&pv=" + encodeURIComponent(x) + "&pyv=" + encodeURIComponent(w) + "&ov=" + encodeURIComponent(q) + "&epf=" + encodeURIComponent(epf) + "&flt=" + encodeURIComponent(lt);

    if (GetQueryStringParams("cms") == "t") {
        window.location.href = "search?cms=t&st=ad&" + D
    } else {
        window.location.href = "search?st=ad&" + D
    }
}
function OpenAdvanceSearch() {
    $("#AdvanceSearchDiv").slideToggle("slow");
    if ($("#tab3").attr("class") == "asearch tab3active") {
        $("#tab3").removeClass("tab3active");
        $("#AdvSearchOpenFlagHdn1").val("f");
        $("#AdvSearchOpenFlagHdn2").val("f");
        $("#AdvSearchOpenFlagHdn3").val("f")
    } else {
        $("#tab3").addClass("tab3active");
        $("#AdvSearchOpenFlagHdn1").val("t");
        $("#AdvSearchOpenFlagHdn2").val("t");
        $("#AdvSearchOpenFlagHdn3").val("t")
    }
    //GAPushTrackEvent("Site-Search", "Click", "Advance-Search-Button")
}
function CheckEnterKeyPress(c) {
    var b = "which" in c ? c.which : c.keyCode;
    if (b == 13) {
        var a = AdvanceSearch();
        return a
    } else { }
}
function SetFileName(a) {
    $("#MultipleISBNBrwsVal").text(a.split(/(\\|\/)/g).pop());
    if ($("#MultipleISBNBrwsVal").text() != "") {
        $("#MultiConfirmBtnDiv").removeClass("HideItems");
        if ($('#MultiIsbnError').attr('class') != 'alert alert-primary HideItems') {
            $("#MultipleISBNPopup").addClass("multiisbnpopuphgt1");
            $(".isbn-popup").addClass("multiisbnpopuphgt1");
        }
        else {
            $("#MultipleISBNPopup").addClass("multiisbnpopuphgt");
            $(".isbn-popup").addClass("multiisbnpopuphgt");
        }
    } else {
        $("#MultiConfirmBtnDiv").addClass("HideItems");
        if ($('#MultiIsbnError').attr('class') == 'alert alert-primary HideItems') {
            $("#MultipleISBNPopup").removeClass("multiisbnpopuphgt");
            $(".isbn-popup").removeClass("multiisbnpopuphgt");
        }
        else {
            $("#MultipleISBNPopup").removeClass("multiisbnpopuphgt1");
            $(".isbn-popup").removeClass("multiisbnpopuphgt1");

            $("#MultipleISBNPopup").addClass("multiisbnpopuphgt");
            $(".isbn-popup").addClass("multiisbnpopuphgt");
        }
    }
}
function OpenMultipleIsbnSearchPopUp() {
    $("#MultipleISBNPopup").dialog();
    $("#overlay").css('display', 'block');
    $('.ui-dialog-titlebar a').click(function () {
        $("#overlay").css('display', 'none');
        $('#Isbntextarea').val('');
        SetFileName('');
    });
    //$('body').css({'background-color':'#000','opacity':'0.7'})
}

function CloseMultipleIsbnSearch() {
    $('#MultipleISBNPopup').dialog('close');
    $('#Isbntextarea').val('');
    SetFileName('');
    $("#overlay").css('display', 'none');
}

var pageNumber = 0, totalCmsResults = 0, cmsitemscnt = 0, cmshtmlcontent = '', totalCount = 0;
function CMSResults() {
    pageNumber = $('#CMSPageNumberHdn').val();
    totalCount = $('#NoOfResultsHdn').val();
    var query = decodeURIComponent(GetQueryStringParams('q'));
    if (!cmsclick) {
        cmsclick = true;
        $("#Loaderimg").css('display', 'block');
        $.ajax({
            type: "POST",
            url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=cms&q=' + query + '&division=' + GetQueryStringParams('division')),
            dataType: "json",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (value) {
                if (value != null && value.length != 0) {
                    $.each(value, function (i, jsondata) {
                        cmsitemscnt++;
                        cmshtmlcontent = cmshtmlcontent + '<div class="cms"><div class="cnt"><h4>';
                        cmshtmlcontent = cmshtmlcontent + '<a href="' + jsondata.TabUrl + '">' + jsondata.TabName + '</a>';
                        cmshtmlcontent = cmshtmlcontent + '</h4>' + jsondata.TabDescription;
                        cmshtmlcontent = cmshtmlcontent + '<div class="infolink">';
                        cmshtmlcontent = cmshtmlcontent + '<a href="' + jsondata.TabUrl + '">' + jsondata.TabUrl + '</a>';
                        cmshtmlcontent = cmshtmlcontent + '</div></div></div>';
                    });
                }
                if (cmsitemscnt == 0 && GetQueryStringParams('f') == undefined) {
                    $('#productstab').click();
                    $('#HasNoResultDiv').removeClass('HideItems');
                    $('#HasResultDiv').addClass('HideItems');
                    var advcheck = GetQueryStringParams('st');
                    if (advcheck == undefined || advcheck == '') {
                        $('<div class="search-no-result"><div class="no-result-txt">' +
                        '<p>Sorry! There are no matching CMS page results for</p>' +
                        '<p class="keyword">"' + query + '"</p>' +
                        '</div><div class="suggestions-txt">' +
                        '<p class="keyword">Suggestions:</p>' +
                        '<p>-Make sure that all words are spelled correctly.</p>' +
                        '<p>-Try different keywords.</p>' +
                        '<p>-Try more general keywords.</p></div></div>').appendTo('#tab2');
                    }
                    else {
                        $('#noresultforspan').addClass('HideItems');
                        $('#noresulttextpara').addClass('HideItems');
                        $('<div class="search-no-result"><div class="no-result-txt">' +
                        '<p>Sorry! There are no matching CMS page results</p>' +
                        '</div><div class="suggestions-txt">' +
                        '<p class="keyword">Suggestions:</p>' +
                        '<p>-Make sure that all words are spelled correctly.</p>' +
                        '<p>-Try different keywords.</p>' +
                        '<p>-Try more general keywords.</p></div></div>').appendTo('#tab2');
                    }
                }
                else if (cmsitemscnt == 0) {
                    $('#productstab').click();
                    $('#HasNoResultDiv').removeClass('HideItems');
                    $('#HasResultDiv').addClass('HideItems');
                    $('<div class="search-empty"><h5>Your search did not return any results.</h5>' +
                        '<h5>Suggestions</h5>' +
                        '<ul>' +
                        '<li>- <a href="#">Reset your advanced search options.</a></li>' +
                        '<li>- <a href="#">Make sure that all words are spelled correctly.</a></li>' +
                        '<li>- <a href="#">Try different keywords.</a></li>' +
                        '<li>- <a href="#">Try more general keywords.</a></li>' +
                        '</ul></div>').appendTo('#tab2');
                }
                else {
                    //$('#infotab').click();
                    $.ajax({
                        type: "POST",
                        url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=cmscount&q=' + query + '&division=' + GetQueryStringParams('division')),
                        dataType: "json",
                        async: false,
                        contentType: "application/json; charset=utf-8",
                        success: function (value1) {
                            //alert(value1);
                            totalCmsResults = value1;
                        }
                    });
                    //totalCount=totalCmsResults;
                    if (parseInt(totalCmsResults) > 0) {
                        $('#CurrentPgStartSizeSpan').text(((pageNumber - 1) * cmsitemscnt + 1));
                        $('#CurrentPgEndSizeSpan').text(Math.min((pageNumber * cmsitemscnt), totalCount));
                        $('#TotalResultSpan').text(totalCmsResults);
                        $("#HasResultDiv").removeClass("HideItems");
                        $("#HasNoResultDiv").addClass("HideItems")
                    } else {
                        $("#HasResultDiv").addClass("HideItems");
                        $("#HasNoResultDiv").removeClass("HideItems")
                    }
                    $('#CurrentPgStartSizeSpan').text(((pageNumber - 1) * cmsitemscnt + 1));
                    $('#CurrentPgEndSizeSpan').text(Math.min((pageNumber * cmsitemscnt), totalCount));
                    $('#TotalResultSpan').text(totalCmsResults);
                    $(cmshtmlcontent).appendTo('#tab2');
                    var sort = GetQueryStringParams('s'), dt = GetQueryStringParams("dt"), dc = GetQueryStringParams("dc"), dv = GetQueryStringParams("dv"), ds = GetQueryStringParams("ds");
                    if (sort == undefined || sort == '') sort = 'R'; else { }
                    if (dt == undefined || dt == '') dt = ""; else { }
                    if (dc == undefined || dc == '') dc = ""; else { }
                    if (dv == undefined || dv == '') dv = ""; else { }
                    if (ds == undefined || ds == '') ds = ""; else { }

                    if (parseInt(totalCmsResults) > parseInt(totalCount)) {
                        var pagerContent = '<div id="PagerHoldercms" align="center" class="he-Pager">';
                        pagerContent = pagerContent + '<div id="StudentPagerDivcms" clientidmode="Static" class="he-srrefdiv" runat="server">';
                        pagerContent = pagerContent + '<span class="he-srresdiv">';
                        pagerContent = pagerContent + '<a href="' + SetCommonQueryStrings(sort, dt, dc, dv, ds, 1, 0) + '" title="First"><<</a>';
                        pagerContent = pagerContent + '<a><</a>';
                        pagerContent = pagerContent + '</span>';
                        pagerContent = pagerContent + '<span id="cmsPageControlcms" class="he-srretdiv">';
                        var tc = (totalCmsResults / totalCount) + (totalCmsResults % totalCount == 0 ? 0 : 1), i = 0;
                        for (i = 1; i <= tc; i++) {
                            pagerContent = pagerContent + '<a href="' + SetCommonQueryStrings(sort, dt, dc, dv, ds, i, 0) + '" title="Previous">' + i + '</a>';
                        }
                        var nextPage = tc > totalCount ? totalCount : i;
                        pagerContent = pagerContent + '</span>';
                        pagerContent = pagerContent + '<span class="he-srrefrdiv">';
                        pagerContent = pagerContent + '<a href="' + SetCommonQueryStrings(sort, dt, dc, dv, ds, tc - 1, 0) + '"  title="next">></a>';
                        pagerContent = pagerContent + '<a href="' + SetCommonQueryStrings(sort, dt, dc, dv, ds, tc, 0) + '" title="last">>></a>';
                        pagerContent = pagerContent + '</span>';
                        pagerContent = pagerContent + '</div>';
                        pagerContent = pagerContent + '</div>';
                        $(pagerContent).appendTo('#tab2');
                    }
                }
            }
        });
    }
    else {
        var cq = GetQueryStringParams("cms");

        if (cq == undefined || cq == '') {
            if (totalCmsResults > 0) {
                $('#CurrentPgStartSizeSpan').text(((pageNumber - 1) * cmsitemscnt + 1));
                $('#CurrentPgEndSizeSpan').text(Math.min((pageNumber * cmsitemscnt), totalCount));
                $('#TotalResultSpan').text(totalCmsResults);
                $("#HasResultDiv").removeClass("HideItems");
                $("#HasNoResultDiv").addClass("HideItems")
            } else {
                $("#HasResultDiv").addClass("HideItems");
                $("#HasNoResultDiv").removeClass("HideItems")
            }
        } else {
            if ($("#TotalResultLbl").text() > 0) {
                $("#CurrentPgStartSizeSpan").text($("#CurrentPgStartSizeLbl").text());
                $("#CurrentPgEndSizeSpan").text($("#CurrentPgEndSizeLbl").text());
                $("#TotalResultSpan").text($("#TotalResultLbl").text());
                $("#HasResultDiv").removeClass("HideItems");
                $("#HasNoResultDiv").addClass("HideItems")
            } else {
                if ($('#TotalResultSpan').text() != '') {
                    //$("#CurrentPgStartSizeSpan").text($("#CurrentPgStartSizeLbl").text());
                    //$("#CurrentPgEndSizeSpan").text($("#CurrentPgEndSizeLbl").text());
                    //$("#TotalResultSpan").text($("#TotalResultLbl").text());
                    //$("#HasResultDiv").removeClass("HideItems");
                    //$("#HasNoResultDiv").addClass("HideItems")
                }
                else {
                    $("#HasResultDiv").addClass("HideItems");
                    $("#HasNoResultDiv").removeClass("HideItems")
                }
            }
        }
    }
}

$(function () {
    var M = GetQueryStringParams("q");
    if (M != undefined) {
        $("#TextSearch").val(decodeURIComponent(M))
    }
    var F = GetQueryStringParams("s");
    switch (F) {
        case "A":
            $("#ProductsSortDpdwn").val("A");
            break;
        case "D":
            $("#ProductsSortDpdwn").val("D");
            break;
        case "L":
            $("#ProductsSortDpdwn").val("L");
            break;
        default:
            $("#ProductsSortDpdwn").val("R");
            break
    }
    $("#ProductsSortDpdwn").kendoDropDownList({
        animation: false
    });
    $("#ProductsSortDpdwn-list").addClass("sortDpnParent");
    var w = GetQueryStringParams("cms");
    if (w == undefined || w == "") {
        w = "products"
    }
    if ($("#LoadCMSHdn").val() == "Y") {
        w = "y";
    }
    $($('#StudentPagerDivcms').html() !== '')
    {
        $('#InfoPagerHolder').html($('#StudentPagerDivcms').html());
        $('#StudentPagerDivcms').html('');
    }
    switch (w.toLowerCase()) {
        case "t":
            cmsclick = true;
            $("#infotab").click();
            if ($("#TotalResultLbl").text() > 0) {
                $("#CurrentPgStartSizeSpan").text($("#CurrentPgStartSizeLbl").text());
                $("#CurrentPgEndSizeSpan").text($("#CurrentPgEndSizeLbl").text());
                $("#TotalResultSpan").text($("#TotalResultLbl").text());
                $("#HasResultDiv").removeClass("HideItems");
                $("#HasNoResultDiv").addClass("HideItems")
                $('#InformationPagerDiv').addClass('ShowItems').removeClass('HideItems');
                $('#ProductsPagerDiv').addClass('HideItems').removeClass('ShowItems');
                $('#infotab').parent().addClass('active');
                $('#tab2').addClass('active');
                $('#tab1').removeClass('active');
            } else {
                $("#HasResultDiv").addClass("HideItems");
                $("#HasNoResultDiv").removeClass("HideItems")
            }
            break;
        case "y":
            $('#infotab').click();
            $('#InformationPagerDiv').addClass('ShowItems').removeClass('HideItems');
            $('#ProductsPagerDiv').addClass('HideItems').removeClass('ShowItems');
            $('#infotab').parent().addClass('active');
            $('#tab2').addClass('active');
            $('#tab1').removeClass('active');
            CMSResults();
            /*    if ($("#TotalResultLbl").text() > 0) {
            $("#CurrentPgStartSizeSpan").text($("#CurrentPgStartSizeLbl").text());
            $("#CurrentPgEndSizeSpan").text($("#CurrentPgEndSizeLbl").text());
            $("#TotalResultSpan").text($("#TotalResultLbl").text());
            $("#HasResultDiv").removeClass("HideItems");
            $("#HasNoResultDiv").addClass("HideItems")
            } else {
            $("#HasResultDiv").addClass("HideItems");
            $("#HasNoResultDiv").removeClass("HideItems")
            }*/
            break;
        default:
            if ($("#ProdTotalResultLbl").text() > 0) {
                $("#CurrentPgStartSizeSpan").text($("#ProdCurrentPgStartSizeLbl").text());
                $("#CurrentPgEndSizeSpan").text($("#ProdCurrentPgEndSizeLbl").text());
                $("#TotalResultSpan").text($("#ProdTotalResultLbl").text());
                $("#HasResultDiv").removeClass("HideItems");
                $("#HasNoResultDiv").addClass("HideItems")
            } else {
                $("#HasResultDiv").addClass("HideItems");
                $("#HasNoResultDiv").removeClass("HideItems")
            }
            break
    }
    $('input:checkbox').prop('checked', false);
    if ($("#SNewHdn").val() == "0") {
        $("#ShowOnlyNew").attr("disabled", "true");
        $("#ShowOnlyNew").parent().addClass("showonlydisabled");
        $("#ShowOnlyNew").parent().parent().addClass("showonlydisabled").attr("style", "background:none !important;")
    } else { } if ($("#SAudienceHdn").val() == "0") {
        $("#ShowOnlyAudienceCode").attr("disabled", "true");
        $("#ShowOnlyAudienceCode").parent().addClass("showonlydisabled");
        $("#ShowOnlyAudienceCode").parent().parent().addClass("showonlydisabled").attr("style", "background:none !important;")
    } else { } if ($("#SPublishedHdn").val() == "0") {
        $("#ShowOnlyPublished").attr("disabled", "true");
        $("#ShowOnlyPublished").parent().addClass("showonlydisabled");
        $("#ShowOnlyPublished").parent().parent().addClass("showonlydisabled").attr("style", "background:none !important;")
    } else { }
    dkwindow6 = $("#MultipleISBNPopup");
    $("#MulpleIsbnBtn").click(function () {
        $("#MultipleISBNPopup").css({
            display: "block"
        });
        $(".k-window-actions.k-header").css("cursor", "pointer");
        dkwindow6 = $("#MultipleISBNPopup");
        if (!dkwindow6.data("kendoWindow")) {
            dkwindow6.kendoWindow({
                modal: true,
                draggable: false
            });
            dkwindow6.data("kendoWindow").center()
        }
        dkwindow6.data("kendoWindow").open();
        $(".k-icon.k-i-close").hide();
        $("a.k-window-action.k-link").mouseover(function () {
            return false
        })
    });
    $("#MultiISBNDismiss").click(function () {
        dkwindow6.data("kendoWindow").close();
        return false
    });
    $("#MultiISBNSearchBtn").click(function () {
        var b = $("#Isbntextarea").val();
        if (b.trim() != "") {
            $("#MultiIsbnError").addClass("HideItems");
            if ($('#MultiConfirmBtnDiv').attr('class') != 'HideItems') {
                $("#MultipleISBNPopup").removeClass("multiisbnpopuphgt1");
                $(".isbn-popup").removeClass("multiisbnpopuphgt1");

                $("#MultipleISBNPopup").addClass("multiisbnpopuphgt");
                $(".isbn-popup").addClass("multiisbnpopuphgt");
            }
            else {
                $("#MultipleISBNPopup").removeClass("multiisbnpopuphgt");
                $(".isbn-popup").removeClass("multiisbnpopuphgt");
            }

            $("#MultiISBNtextarea").val($("#Isbntextarea").val());
            var a = {
                SearchText: b,
                Division: GetQueryStringParams("division")
            };
            a = JSON.stringify(a);
            $.ajax({
                url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=multipleisbn"),
                cache: false,
                type: "POST",
                data: a,
                async: false,
                success: function (c) {
                    //window.open(c.replaceAll('"', ""), "_parent")
                    window.open(c.replace(/"/g, ""), "_parent")
                }
            })
        } else {
            $("#MultiIsbnError").removeClass("HideItems");
            if ($('#MultiConfirmBtnDiv').attr('class') != 'HideItems') {
                $("#MultipleISBNPopup").addClass("multiisbnpopuphgt1");
                $(".isbn-popup").addClass("multiisbnpopuphgt1");
            }
            else {
                $("#MultipleISBNPopup").addClass("multiisbnpopuphgt");
                $(".isbn-popup").addClass("multiisbnpopuphgt");
            }
        }


        return false
    });
    $("#MultipleISBNBrowseBtn").click(function () {
        $("#MultipleISBNBrowse").click()
    });
    $("#MultiIsbnCancelBtn").click(function () {
        $("#MultipleISBNBrowse").val("");
        SetFileName("")
    });
    $('#MultiIsbnConfirmBtn').click(function () {
        var fileUpload = $("#MultipleISBNBrowse").get(0);
        var files = fileUpload.files;

        var imag = new FormData();
        for (var i = 0; i < files.length; i++) {
            imag.append(files[i].name, files[i]);
        }

        //imag.append('Data', JSON.stringify({ objEnt: args }));

        $.ajax({
            type: "POST",
            url: "desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=getisbns&path=" + $('#MultipleISBNBrwsVal').text(),
            contentType: false,
            processData: false,
            data: imag,
            async: false,
            success: function (a) {
                var ele = a.replace(/"|"$/g, "").replace(/,|,$/g, ", ");
                $("#Isbntextarea").val(ele);
            }
        });
    });
    //$("#MultiIsbnConfirmBtn").click(function () {
    //	$.ajax({
    //		url: "desktopmodules/hesearchresults/views/heproductresults.ascx/export",
    //		type: "POST",
    //		contentType: "application/json; charset=utf-8",
    //		dataType: "json",
    //		success: function (a) {
    //			if (a.d) {} else {}
    //		}
    //	})
    //});
    $("#AdvanceSearchDiv input").keyup(function (b) {
        if (b.keyCode == 13) {
            var a = AdvanceSearch();
            return a
        } else { }
    });
    $(".accordion-toggle").click(function () {
        var a = $(this).children().first();
        switch (a.attr("class")) {
            case "ico-toggle-close":
                $(this).children().first().removeClass("ico-toggle-close").addClass("ico-toggle-open");
                break;
            case "ico-toggle-open":
                $(this).children().first().removeClass("ico-toggle-open").addClass("ico-toggle-close");
                break;
            default:
                break
        }
    });
    var x = GetQueryStringParams("ao");
    if (x != undefined && x.toLowerCase() == "t") {
        OpenAdvanceSearch()
    } else { }
    var A = [],
		z = [];
    var G = GetQueryStringParams("sf"),
		H = "",
		C = GetQueryStringParams("f"),
		K = "",
		N = GetQueryStringParams("dt"),
		B = GetQueryStringParams("dc"),
		E = GetQueryStringParams("dv"),
		I = GetQueryStringParams("ds");
    if (G != undefined && G != "") {
        A = G.split(",");
        $("#collapseTwo input:checkbox").each(function () {
            H = $(this).val().toString();
            if ($.inArray(H, A) != -1) {
                $(this).attr("checked", "checked");
                $("#ShowOnlyChk" + H).removeClass("ico-uncheck").addClass("ico-check")
            }
        })
    } else { } if (C != undefined && C != "") {
        z = C.split(":");
        $.each(z, function (d, c) {
            var b = c.split("_");
            $("#FacetAnchor" + b[0]).click();
            if (b[1] != undefined) {
                var a = b[1].split(",");
                $.each(a, function (e, f) {
                    $("#" + b[0] + f).attr("checked", "checked");
                    $("#FacetChk" + b[0] + f).removeClass("ico-uncheck").addClass("ico-check")
                })
            }
        })
    } else { }
    switch (N) {
        case "dc":
            $("#dc" + B).click();
            var J = $("#" + $("#collapseCategory" + B).attr("id") + " .dlink");
            if ($(J).length == 1) {
                $(J).click();
                if ($(".subli").length == 1) {
                    $(".subli").addClass("activehierarchyitem")
                } else {
                    $(".dlink").parent().addClass("activehierarchyitem")
                }
            } else {
                $("#Lidc" + B).addClass("activehierarchyitem")
            }
            SetDisciplineBottomBorder();
            break;
        case "d":
            $("#dc" + B).click();
            $("#dc" + B).next().addClass('activedcparent');
            if ($(".subli").length == 1) {
                $(".subli").addClass("activehierarchyitem")
            } else {
                $("#Lid" + B + "d" + E).addClass("activehierarchyitem")
            }
            $("#dc" + B + "d" + E).click();
            SetDisciplineBottomBorder();
            break;
        case "s":
            $("#dc" + B).click();
            $("#dc" + B).next().addClass('activedcparent');
            $("#dc" + B + "d" + E).click();
            $("#Lis" + B + "d" + E + "s" + I).addClass("activehierarchyitem");
            SetDisciplineBottomBorder();
            break;
        default:
            if ($(".dclink").length == 1) {
                $(".dclink").click();
                if ($(".dlink").length == 1) {
                    $(".dclink").next().addClass('activedcparent');
                    $(".dlink").click();
                    if ($(".subli").length == 1) {
                        $(".subli").addClass("activehierarchyitem")
                    } else {
                        $(".dlink").parent().addClass("activehierarchyitem")
                    }
                } else {
                    $(".dclink").parent().addClass("activehierarchyitem")
                }
            } else { }
            SetDisciplineBottomBorder();
            break
    }
    if ($("#DivisionHdn").val() != undefined) {
        switch ($("#DivisionHdn").val().toLowerCase()) {
            case "vocational":
                $('#dnn_CENGAGESUBMENU_VocationalLink').addClass('current-menu-parent');
                $("#DivisionHdn").val("Vocational");
                $(".DivisionLbl").text("Vocational");
                break;
            case "gale":
                $('#dnn_CENGAGESUBMENU_GaleLink').addClass('current-menu-parent');
                $("#DivisionHdn").val("Gale");
                $(".DivisionLbl").text("Gale");
                break;
            default:
                $('#dnn_CENGAGESUBMENU_HigherEducation').addClass('current-menu-parent');
                $("#DivisionHdn").val("Higher Education");
                $(".DivisionLbl").text("Higher Education");
                break
        }
    }
    SetUpProductResults();
    var stab = 0;
    $(this).click(function () {
        if ($('#tab1').attr('class') == 'tab-pane active') {
            $("#productstab").parent().addClass('active');
            $("#infotab").parent().removeClass('active');
        }
        if ($('#tab2').attr('class') == 'tab-pane active') {
            $("#infotab").parent().addClass('active');
            $("#productstab").parent().removeClass('active');
        }
    });
    $("#productstab").click(function () {
        if (stab == 0) {
            $('#ProductsPagerDiv').addClass('ShowItems').removeClass('HideItems');
            $('#InformationPagerDiv').addClass('HideItems').removeClass('ShowItems');
            $(this).parent().addClass('active');
            $('#tab1').addClass('active');
            $('#tab2').removeClass('active');
        }
        if ($("#ProdTotalResultLbl").text() > 0) {
            $("#CurrentPgStartSizeSpan").text($("#ProdCurrentPgStartSizeLbl").text());
            $("#CurrentPgEndSizeSpan").text($("#ProdCurrentPgEndSizeLbl").text());
            $("#TotalResultSpan").text($("#ProdTotalResultLbl").text());
            $("#HasResultDiv").removeClass("HideItems");
            $("#HasNoResultDiv").addClass("HideItems")
        } else {
            $("#HasResultDiv").addClass("HideItems");
            $("#HasNoResultDiv").removeClass("HideItems")
        }
    });
    $("#infotab").click(function () {
        $('#InformationPagerDiv').addClass('ShowItems').removeClass('HideItems');
        $('#ProductsPagerDiv').addClass('HideItems').removeClass('ShowItems');
        $(this).parent().addClass('active');
        $('#tab2').addClass('active');
        $('#tab1').removeClass('active');
        stab = 1;
        CMSResults();
        stab = 0;
    });
    $("#tab3").live("click", function () {
        OpenAdvanceSearch();
        GAPushTrackEvent("Site-Search", "Click", "Advance-Search-Button");
    });
    $("#clearAdvSearch").click(function () {
        $("#AdvResetBtn").click();
        $("#tab3").click()
    });
    $("div.favorite .btn").on("focus", function () {
        if ($(this).parent().children().next().attr("class") == "btn btn-onoff btn-on") {
            $(this).parent().children().first().addClass('ico-faviconfocus').removeClass('ico-favour-right');
            $(this).addClass('favbtnfocus').removeClass('btn-on').val("UNFAVOURITE")
        } else {
            $(this).children().next().val("FAVOURITE")
        }
    });
    $("div.favorite .btn").on("blur", function () {
        if ($(this).parent().children().next().attr("class") == "btn btn-onoff favbtnfocus") {
            $(this).parent().children().first().removeClass('ico-faviconfocus').addClass('ico-favour-right');
            $(this).removeClass('favbtnfocus').addClass('btn-on');
        }
        $(this).parent().children().next().val("FAVOURITE")
        if (navigator.userAgent.match(/iPad/i) != null) {
            //$("#FavSpan" + b).removeClass("HideItems").addClass("ico-favour-right-ipad");
            if ($(this).parent().children().next().attr("class") == "btn btn-onoff ipad-btn-on")
                $(this).parent().children().next().removeClass("ipad-btn-on").addClass("btn-on");
        }
    });
    $("div.favorite").on("mouseover", function () {
        if ($(this).children().next().attr("class") == "btn btn-onoff btn-on") {
            $(this).children().next().val("UNFAVOURITE")
        } else {
            $(this).children().next().val("FAVOURITE")
        }
    });
    $("div.favorite").on("mouseout", function () {
        $(this).children().next().val("FAVOURITE")
        if (navigator.userAgent.match(/iPad/i) != null) {
            //$("#FavSpan" + b).removeClass("HideItems").addClass("ico-favour-right-ipad");
            if ($(this).children().next().attr("class") == "btn btn-onoff ipad-btn-on")
                $(this).children().next().removeClass("ipad-btn-on").addClass("btn-on");
        }
    });

    $(".favspan.HideItems").each(function () {
        if ($(this).text().toLowerCase().trim() == "y") {
            $(this).parent().children().next().addClass("btn-on");
            $(this).parent().children().first().removeClass("HideItems").addClass("ico-favour-right")
        } else {
            $(this).parent().children().first().removeClass("btn-on");
            $(this).parent().children().first().addClass("HideItems").removeClass("ico-favour-right")
        }
    });

    $("#FacetFilterDiv input").on("focus", function (eve) {
        if (navigator.userAgent.match(/iPad/i) == null) {
            $(this).parent().parent().addClass("cbxparenthighlight");
        }
    });

    $("#FacetFilterDiv input").on("blur", function () {
        if (navigator.userAgent.match(/iPad/i) == null) {
            $(this).parent().parent().removeClass("cbxparenthighlight");
        }
    });

    $("#FacetFilterDiv input").keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            return false;
        }
    });

    $("#collapseTwo input").keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            return false;
        }
    });

    $("a.cleartxt").on("mouseover", function () {
        if (navigator.userAgent.match(/iPad/i) == null) {
            $(this).removeClass('cleartxt').addClass("clearselection cleartxt");
        }
    });
    $("a.cleartxt").on("mouseout", function () {
        if (navigator.userAgent.match(/iPad/i) == null) {
            $(this).removeClass("clearselection");
        }
    });
    $("a.cleartxt").on("focus", function () {
        if (navigator.userAgent.match(/iPad/i) == null) {
            $(this).removeClass('cleartxt').addClass("clearselection cleartxt");
        }
    });
    $("a.cleartxt").on("blur", function () {
        if (navigator.userAgent.match(/iPad/i) == null) {
            $(this).removeClass("clearselection");
        }
    });

    $("#collapseThree.accordion-body.in a").focus(function () {
        $(this).parent().addClass("sidemenuFormatTabBGcolor")
    }).focusout(function () {
        $(this).parent().removeClass("sidemenuFormatTabBGcolor")
    });

    var ss = GetQueryStringParams("ss");
    if (ss != undefined && ss != "") {
        $.ajax({
            url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=getsearchname"),
            cache: false,
            type: "POST",
            async: false,
            success: function (data) {
                $("#SearchNameTxt").val(data);
                //OpenSaveSearchPopup(z);
                $('#SaveSearchPopUp1').addClass('HideItems').css('display', 'none'); ;
                OpenSaveSearchSecondPopup("Anonymous");
            }
        });
        $("#SaveButtonA").click();
    }
    else { }
    dkwindow4 = $("#MoreFavoritePopUp");
    var ff = GetQueryStringParams("ff");
    if (ff != undefined && ff != "") {
        $("#MoreFavoritePopUp").css({
            display: "block"
        });
        $(".k-window-actions.k-header").css("cursor", "pointer");
        if (!dkwindow4.data("kendoWindow")) {
            dkwindow4.kendoWindow({
                modal: true,
                draggable: false
            });
            dkwindow4.data("kendoWindow").center()
        }
        dkwindow4.data("kendoWindow").open();
        $(".k-icon.k-i-close").hide();
        $("a.k-window-action.k-link").mouseover(function () {
            return false
        });
    }
    else { }

    dkwindow = $("#SaveSearchPopUp1");
    dkwindow3 = $("#SaveSearchAnonymous");
    $("span.SSBtn").click(function () {
        CheckAndOpenSaveSearch()
    });
    $("#SaveSearchBtn4").click(function () {
        CheckAndOpenSaveSearch()
    });
    $("#SaveSearchBtn5").click(function () {
        CheckAndOpenSaveSearch()
    });
    $("#SaveButtonA").click(function () {
        OpenSaveSearchSecondPopup("Normal");
        return false
    });
    $("#CancelButtonA").click(function () {
        dkwindow.data("kendoWindow").close();
        return false
    });
    $("#SaveButtonB").click(function () {
        $.ajax({
            url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=savesearch"),
            cache: false,
            type: "POST",
            async: false,
            success: function (b) {
                if (b == 50) {
                    dkwindow1.data("kendoWindow").close();
                    $("#MoreSaveSearch").css({ 'display': 'block' });
                    $('.k-window-actions.k-header').css('cursor', 'pointer');
                    if (!dkwindow5.data("kendoWindow")) {
                        dkwindow5.kendoWindow({
                            modal: true,
                            draggable: false
                        });
                        dkwindow5.data("kendoWindow").center();
                    }
                    dkwindow5.data("kendoWindow").open();
                    $(".k-icon.k-i-close").hide();
                    $('a.k-window-action.k-link').mouseover(function () {
                        return false;
                    });
                    return false;
                }
                else {
                    dkwindow1.data("kendoWindow").close();
                }
            }
        });
        return false
    });
    $("#CancelButtonB").click(function () {
        dkwindow1.data("kendoWindow").close();
        return false
    });
    $("#SaveSearchLoginBtn").click(function () {
        var setUrl = window.location.href;
        if ($('#infotab').parent().attr('class') == 'active')
            setUrl = setUrl + '&cms=t';
        if ($("#AnonSaveSearchTxt").val() != "") {
            var jsonData = {
                'CurrentUrl': setUrl,
                'KeyWords': $('#SearchTextHdn').val(),
                'SearchName': $('#AnonSaveSearchTxt').val(),
                'Division': GetQueryStringParams('division')
            }
            jsonData = JSON.stringify(jsonData);
            $.ajax({
                url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=setsearchvalue'),
                cache: false,
                type: 'POST',
                async: false,
                data: jsonData,
                success: function (data) {
                    $("#AnonSaveSearchNameerror").addClass("HideItems");
                    dkwindow3.data("kendoWindow").close();
                    $("#loginlnk").click();
                }
            });
            return false
        } else {
            $("#AnonSaveSearchNameerror").removeClass("HideItems")
        }
        return false
    });
    $("#SaveSearchSignUpBtn").click(function () {
        var setUrl = window.location.href;
        if ($('#infotab').parent().attr('class') == 'active')
            setUrl = setUrl + '&cms=t';
        if ($("#AnonSaveSearchTxt").val() != "") {
            var jsonData = {
                'CurrentUrl': setUrl,
                'KeyWords': $('#SearchTextHdn').val(),
                'SearchName': $('#AnonSaveSearchTxt').val(),
                'Division': GetQueryStringParams('division')
            }
            jsonData = JSON.stringify(jsonData);
            $.ajax({
                url: GetFile('desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=setsearchvalue'),
                cache: false,
                type: 'POST',
                async: false,
                data: jsonData,
                success: function (data) {
                    $("#AnonSaveSearchNameerror").addClass("HideItems");
                    dkwindow3.data("kendoWindow").close();
                    window.location.href = "/signup";
                }
            });
            return false
        } else {
            $("#AnonSaveSearchNameerror").removeClass("HideItems")
        }
    });
    dkwindow2 = $("#FavoritePopUp");
    $("#FavLoginBtn").click(function () {
        dkwindow2.data("kendoWindow").close();
        $("#loginlnk").click();
        return false
    });
    $("#FavSignUpBtn").click(function () {
        dkwindow2.data("kendoWindow").close();
        window.location.href = "/signup";
        return false
    });
    dkwindow5 = $("#MoreSaveSearch");
    $("#MoreSearchOkBtn").click(function () {
        dkwindow5.data("kendoWindow").close();
        return false
    });

    $("#MoreFavOkBtn").click(function () {
        dkwindow4.data("kendoWindow").close();
        return false
    });
    var sType = GetQueryStringParams('st');
    if (sType != "undefined" && sType == 'ad') {
        var adtitle = GetQueryStringParams('tq'), adauthor = GetQueryStringParams('aq'), adsubject = GetQueryStringParams('subq'),
			adnone = GetQueryStringParams('nq'), adall = GetQueryStringParams('allq'), adexact = GetQueryStringParams('etq'),
			adformat = GetQueryStringParams('fv'), adpublisher = GetQueryStringParams('pv'), adpubyear = GetQueryStringParams('pyv'),
			adorigin = GetQueryStringParams('ov'), adebookplatform = GetQueryStringParams('epf'), adlibtype = GetQueryStringParams('flt');
        if (adtitle != "undefined" && adtitle != "")
            $('#AdvTitleTxt').val(decodeURIComponent(adtitle));
        else
            $('#AdvTitleTxt').val();

        if (adauthor != "undefined" && adauthor != "")
            $('#AdvAuthorTxt').val(decodeURIComponent(adauthor));
        else
            $('#AdvAuthorTxt').val();

        if (adsubject != "undefined" && adsubject != "")
            $('#AdvSubjectTxt').val(decodeURIComponent(adsubject));
        else
            $('#AdvSubjectTxt').val();

        if (adformat != "undefined" && adformat != "") {
            $('#AdvAttrTypesDrpDwn' + AidNo + " option").filter(function () {
                return $(this).text().toLowerCase() == "format";
            }).prop('selected', true);
            $('#AdvAttrTypesDrpDwn' + AidNo).kendoDropDownList({
                animation: false
            });
            $('#AdvAttrTypesDrpDwn' + AidNo).parent().next().val(decodeURIComponent(adformat).split(':')[1]);
            AddAttributeItem("AdvAttrTypesDrpDwn" + AidNo);
        }

        if (adpublisher != "undefined" && adpublisher != "") {
            $('#AdvAttrTypesDrpDwn' + AidNo + " option").filter(function () {
                return $(this).text().toLowerCase() == "publisher";
            }).prop('selected', true);
            $('#AdvAttrTypesDrpDwn' + AidNo).kendoDropDownList({
                animation: false
            });
            $('#AdvAttrTypesDrpDwn' + AidNo).parent().next().val(decodeURIComponent(adpublisher).split(':')[1]);
            AddAttributeItem("AdvAttrTypesDrpDwn" + AidNo);
        }

        if (adpubyear != "undefined" && adpubyear != "") {
            $('#AdvAttrTypesDrpDwn' + AidNo + " option").filter(function () {
                return $(this).text().toLowerCase() == "published year";
            }).prop('selected', true);
            $('#AdvAttrTypesDrpDwn' + AidNo).kendoDropDownList({
                animation: false
            });
            $('#AdvAttrTypesDrpDwn' + AidNo).parent().next().val(decodeURIComponent(adpubyear).split(':')[1]);
            AddAttributeItem("AdvAttrTypesDrpDwn" + AidNo);
        }

        if (adorigin != "undefined" && adorigin != "") {
            $('#AdvAttrTypesDrpDwn' + AidNo + " option").filter(function () {
                return $(this).text().toLowerCase() == "origin";
            }).prop('selected', true);
            $('#AdvAttrTypesDrpDwn' + AidNo).kendoDropDownList({
                animation: false
            });
            $('#AdvAttrTypesDrpDwn' + AidNo).parent().next().val(decodeURIComponent(adorigin).split(':')[1]);
            AddAttributeItem("AdvAttrTypesDrpDwn" + AidNo);
        }

        if (adebookplatform != "undefined" && adebookplatform != "") {
            $('#AdvAttrTypesDrpDwn' + AidNo + " option").filter(function () {
                return $(this).text().toLowerCase() == "ebook platform";
            }).prop('selected', true);
            $('#AdvAttrTypesDrpDwn' + AidNo).kendoDropDownList({
                animation: false
            });
            $('#AdvAttrTypesDrpDwn' + AidNo).parent().next().val(decodeURIComponent(adebookplatform).split(':')[1]);
            AddAttributeItem("AdvAttrTypesDrpDwn" + AidNo);
        }

        if (adlibtype != "undefined" && adlibtype != "") {
            $('#AdvAttrTypesDrpDwn' + AidNo + " option").filter(function () {
                return $(this).text().toLowerCase() == "library type";
            }).prop('selected', true);
            $('#AdvAttrTypesDrpDwn' + AidNo).kendoDropDownList({
                animation: false
            });
            $('#AdvAttrTypesDrpDwn' + AidNo).parent().next().val(decodeURIComponent(adlibtype).split(':')[1]);
            AddAttributeItem("AdvAttrTypesDrpDwn" + AidNo);
        }

        if (adall != "undefined" && adall != "") {
            $('#FilterResultsDrpDwn' + FidNo).val(1);
            $('#FilterResultsDrpDwn' + FidNo).kendoDropDownList({
                animation: false
            });
            $('#FilterResultsDrpDwn' + FidNo).parent().next().val(decodeURIComponent(adall));
            AddFilterItems("FilterResultsDrpDwn" + FidNo);
        }
        if (adexact != "undefined" && adexact != "") {
            $('#FilterResultsDrpDwn' + FidNo).val(2);
            $('#FilterResultsDrpDwn' + FidNo).kendoDropDownList({
                animation: false
            });
            $('#FilterResultsDrpDwn' + FidNo).parent().next().val(decodeURIComponent(adexact));
            AddFilterItems("FilterResultsDrpDwn" + FidNo);
        }
        if (adnone != "undefined" && adnone != "") {
            $('#FilterResultsDrpDwn' + FidNo).val(3);
            $('#FilterResultsDrpDwn' + FidNo).kendoDropDownList({
                animation: false
            });
            $('#FilterResultsDrpDwn' + FidNo).parent().next().val(decodeURIComponent(adnone));
            AddFilterItems("FilterResultsDrpDwn" + FidNo);
        }
    }

    SetDisciplineBottomBorder();

    jQuery("#AdvAttrTypesDrpDwn0").kendoDropDownList({
        animation: false
    });
    jQuery("#FilterResultsDrpDwn0").kendoDropDownList({
        animation: false
    })

    $('.dlink.accordion-toggle').click(function () { SetDisciplineBottomBorder(); });
    $('.dclink.accordion-toggle').click(function () { SetDisciplineBottomBorder(); });
});
function SetDisciplineBottomBorder() {
    //alert($('#collapseThree .accordion-body.collapse').last().attr('id'));
    //alert($('#collapseThree .accordion-body.in').last().attr('id'));
    //alert($('#collapseThree .discipline-accordian').last().attr('id'));
    $('#collapseThree .accordion-body.collapse').last().addClass('distransborder accordion-body collapse');
    $('#collapseThree .accordion-body.in').last().addClass('distransborder accordion-body in');
    $('#collapseThree .discipline-accordian').last().addClass('distransborder discipline-accordian');
}

function GetHandler(e) {
    var t = null;
    t = new XMLHttpRequest;
    t.open("GET", e, false);
    t.send(null);
    return t.responseText
}

function GetFile(path) {
    var pathname = window.location.pathname;
    var temppath = pathname.split('/');
    var root = location.protocol + "//" + window.location.host + "/" + temppath[0];
    var url = root + path;
    return url;
}
function GetIndividualISBN(e, t) {
    var i = "";
    //var s = $("#selqty")[0].innerHTML;
    var o = "";
    var u = "";
    if ($(e).parents().find(".Qtycart").val() == 0) {
        outofstockselectedvalue = t + "|" + 1
    } else {
        outofstockselectedvalue = t + "|" + $(e).parents().find(".Qtycart").val()
    }
    idobject = e;

    var a = "CART";
    var f = outofstockselectedvalue;
    var l = GetHandler(GetFile("/DesktopModules/List/Handlers/ListHandler.ashx?Value=" + f + "&Action=" + a));
    if ($("#Cart")[0] != undefined) {
        if (l == 0) {
            $("#Cart")[0].innerText = "";
            $("#Cart")[0].innerHTML = ""
        } else {
            $("#Cart")[0].innerText = l;
            $("#Cart")[0].innerHTML = l
        }
        $(e).hide();
        $(e).parent().find("#Addedtocart").show();
        setTimeout(function () {
            $(e).show();
            $(e).parent().find("#Addedtocart").hide();
        }, 5000);
        outofstockselectedvalue = ""
    }
}
$(".Qtycart").keypress(function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
});