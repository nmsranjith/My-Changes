function GetQueryStringParams(i) {
    var e = window.location.search.substring(1);
    var h = e.split("&");
    for (var j = 0; j < h.length; j++) {
        var g = h[j].split("=");
        if (g[0] == i) {
            return g[1]
        }
    }
}
function CheckAndOpenSaveSearch() {
    $.ajax({
        url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=checksavesearch"),
        dataType: "json",
        async: false,
        success: function (b) {
            if (b == -1) {
                OpenSaveSearchAnonymousPopup(dkwindow3)
            } else {
                if (b == 50) {
                    $("#MoreSaveSearch").css({
                        display: "block"
                    });
                    $(".k-window-actions.k-header").css("cursor", "pointer");
                    if (!dkwindow5.data("kendoWindow")) {
                        dkwindow5.kendoWindow({
                            modal: true,
                            draggable: false
                        });
                        dkwindow5.data("kendoWindow").center()
                    }
                    dkwindow5.data("kendoWindow").open();
                    $(".k-icon.k-i-close").hide();
                    $("a.k-window-action.k-link").mouseover(function () {
                        return false
                    });
                    return false
                } else {
                    OpenSaveSearchPopup(dkwindow)
                }
            }
        }
    })
}
function OpenSaveSearchPopup(C) {
    var m = decodeURIComponent(GetQueryStringParams("q")),
		e = GetQueryStringParams("dt"),
		v = GetQueryStringParams("dc"),
		p = GetQueryStringParams("dv"),
		w = GetQueryStringParams("ds"),
		f = decodeURIComponent(GetQueryStringParams("tq")),
		r = decodeURIComponent(GetQueryStringParams("aq")),
		b = decodeURIComponent(GetQueryStringParams("subq")),
		c = GetQueryStringParams("f"),
		d = decodeURIComponent(GetQueryStringParams("etq")),
		h = decodeURIComponent(GetQueryStringParams("allq")),
		N = decodeURIComponent(GetQueryStringParams("nq")),
		n = decodeURIComponent(GetQueryStringParams("fv")),
		s = decodeURIComponent(GetQueryStringParams("pv")),
		o = decodeURIComponent(GetQueryStringParams("pyv")),
		i = decodeURIComponent(GetQueryStringParams("ov"));
    if (m == undefined || m == "undefined" || m == "") {
        $("#SSKeywords").text("")
    } else {
        $("#SSKeywords").text('"' + m + '"')
    }
    if (f != "undefined" && f != "") {
        $("#SSTitle").text(f);
        $("#PgTitle").removeClass("HideItems")
    } else { } if (r != "undefined" && r != "") {
        $("#SSAuthors").text(r);
        $("#PgAuthors").removeClass("HideItems")
    } else { } if (b != "undefined" && b != "") {
        var a = $("#SSSubject").text().trim();
        if (a != "") {
            $("#SSSubject").html(a + ' <span class="and">AND</span> ' + b)
        } else {
            $("#SSSubject").text(b)
        }
        $("#PgSubject").removeClass("HideItems")
    } else { } if (e != undefined && e != "") {
        switch (e) {
            case "d":
                $("#SSDiscipline").text($("#cat" + v + "d" + p).text());
                $("#PgDiscipline").removeClass("HideItems");
                break;
            case "s":
                var a = $("#SSSubject").text().trim();
                if (a != "") {
                    $("#SSSubject").html('"' + a + '" <span class="and">AND</span> "' + $("#cat" + v + "d" + p + "s" + w).text() + '"')
                } else {
                    $("#SSSubject").text('"' + $("#cat" + v + "d" + p + "s" + w).text() + '"')
                }
                $("#PgSubject").removeClass("HideItems");
                break;
            default:
                $("#SSDisciplineCategory").text($("#cat" + v).text());
                $("#PgDisciplineCategory").removeClass("HideItems");
                break
        }
    } else { } if ($("#collapseTwo input:checked").length > 0) {
        var x = "";
        $("#collapseTwo input:checked").each(function () {
            x = x + ',"' + $(this).next().text() + '"'
        });
        x = x.replace(/^,|,$/g, "");
        x = x.replace(/,|,$/g, ' <span class="and">AND</span>  ');
        x = x.replace(/NEW|NEW$/g, "New Titles");
        x = x.replace(/AUDIENCE|AUDIENCE$/g, "AU/NZ Titles");
        x = x.replace(/PUBLISHER|PUBLISHER$/g, "Published Titles");
        $("#SSShowOnly").html(x);
        $("#PgShowOnly").removeClass("HideItems")
    } else { }
    var g = "",
		l = "",
		u = "",
		S = "",
		t = "";
    if ($("#FacetFilterDiv input:checked").length > 0) {
        $("#FacetFilterDiv input:checked").each(function () {
            g = g + ',"' + $(this).next().text() + '"';
            switch ($(this).next().next().next().text().toLowerCase()) {
                case "format":
                    l = l + "," + $(this).next().text();
                    $("#PgFormat").removeClass("HideItems");
                    break;
                case "publisher":
                    u = u + "," + $(this).next().text();
                    $("#PgPublisher").removeClass("HideItems");
                    break;
                case "published year":
                    S = S + "," + $(this).next().text();
                    $("#PgPublishedYear").removeClass("HideItems");
                    break;
                case "origin":
                    t = t + "," + $(this).next().text();
                    $("#PgOrigin").removeClass("HideItems");
                    break;
                default:
                    break
            }
        })
    } else { }
    var T = "",
		E = "",
		y = "";
    if (n != "undefined" && n != "") {
        l = l + "," + n.split(":")[1];
        $("#PgFormat").removeClass("HideItems")
    } else { } if (s != "undefined" && s != "") {
        u = u + "," + s.split(":")[1];
        $("#PgPublisher").removeClass("HideItems")
    } else { } if (o != "undefined" && o != "") {
        S = S + "," + o.split(":")[1];
        $("#PgPublishedYear").removeClass("HideItems")
    } else { } if (i != "undefined" && i != "") {
        t = t + "," + i.split(":")[1];
        $("#PgOrigin").removeClass("HideItems")
    } else { }
    l = l.replace(/^,|,$/g, "");
    u = u.replace(/^,|,$/g, "");
    S = S.replace(/^,|,$/g, "");
    t = t.replace(/^,|,$/g, "");
    if (h != "undefined" && h != "") {
        h = h.replace(/,|,$/g, ' <span class="and">AND</span> ');
        $("#SSIncluding").html(h);
        $("#PgIncluding").removeClass("HideItems")
    } else { } if (d != "undefined" && d != "") {
        d = d.replace(/,|,$/g, ' <span class="and">AND</span> ');
        $("#SSExactWords").html(d);
        $("#PgExactWords").removeClass("HideItems")
    } else { } if (N != "undefined" && N != "") {
        N = N.replace(/,|,$/g, ' <span class="and">AND</span> ');
        $("#SSExcluding").html(N);
        $("#PgExcluding").removeClass("HideItems")
    } else { }
    $("#SSFormat").html(l);
    $("#SSPublisher").html(u);
    $("#SSPublishedYear").html(S);
    $("#SSOrigin").html(t);
    $("#SaveSearchPopUp1").css({
        display: "block"
    });
    $(".k-window-actions.k-header").css("cursor", "pointer");
    if (!C.data("kendoWindow")) {
        C.kendoWindow({
            modal: true,
            draggable: false
        });
        C.data("kendoWindow").center()
    }
    C.data("kendoWindow").open();
    $(".k-icon.k-i-close").hide();
    $("a.k-window-action.k-link").mouseover(function () {
        return false
    });
    return false
}
function OpenSaveSearchAnonymousPopup(b) {
    $("#SaveSearchAnonymous").css({
        display: "block"
    });
    $(".k-window-actions.k-header").css("cursor", "pointer");
    if (!b.data("kendoWindow")) {
        b.kendoWindow({
            modal: true,
            draggable: false
        });
        b.data("kendoWindow").center()
    }
    b.data("kendoWindow").open();
    $(".k-icon.k-i-close").hide();
    $("a.k-window-action.k-link").mouseover(function () {
        return false
    });
    return false
}
function closeAlertMsg(b) {
    $(b).parent().parent().addClass("HideItems")
}
function SaveFavorite(d, c) {
    if ($(c).attr("class") == "btn btn-onoff") {
        $.ajax({
            url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=favorite&prodId=" + d + "&division=" + GetQueryStringParams("division")),
            dataType: "json",
            async: false,
            success: function (a) {
                if (a == -1) {
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
                    if (a == 50) {
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
                        $("#FavSpan" + d).removeClass("HideItems").addClass("ico-favour-right");
                        $(c).addClass("btn-on").val("UNFAVORITE")
                    }
                }
            }
        })
    } else {
        $.ajax({
            url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=removefavorite&prodId=" + d + "&division=" + GetQueryStringParams("division")),
            dataType: "json",
            async: false,
            success: function (a) {
                $(c).removeClass("btn-on").val("FAVORITE");
                $("#FavSpan" + d).addClass("HideItems").removeClass("ico-favour-right")
            }
        })
    }
}
function AddAttributeItem(b) {
    if ($(b).val() != "0" && $("#AdvAttrTypesDrpDwn" + AidNo).val() != 0) {
        AidNo++;
        jQuery('<div class="rows"><div class="recommand"><select id="AdvAttrTypesDrpDwn' + AidNo + '" onchange="AddAttributeItem(AdvAttrTypesDrpDwn' + AidNo + ')" class="rec-dropdown"></select><input type="text" placeholder="" class="iconinput" onkeyup="CheckEnterKeyPress(event)"/><span class="ico-clearclose" onclick="RemoveItem(AdvAttrTypesDrpDwn' + AidNo + ')"> </span></div></div>').appendTo("#AttributeSet");
        $("#AdvAttrTypesDrpDwn0 option").clone().appendTo("#AdvAttrTypesDrpDwn" + AidNo);
        $("#AdvAttrTypesDrpDwn" + AidNo).val("0");
        jQuery("#AdvAttrTypesDrpDwn" + AidNo).kendoDropDownList({
            animation: false
        })
    } else { }
}
function AddFilterItems(b) {
    if ($(b).val() != "0" && $("#FilterResultsDrpDwn" + FidNo).val() != 0) {
        FidNo++;
        jQuery('<div class="rows"><div class="recommand"><select id="FilterResultsDrpDwn' + FidNo + '" class="rec-dropdown" onchange="AddFilterItems(FilterResultsDrpDwn' + FidNo + ')"></select><input type="text" placeholder="" class="iconinput" onkeyup="CheckEnterKeyPress(event)"/><span class="ico-clearclose" onclick="RemoveItem(FilterResultsDrpDwn' + FidNo + ')"> </span></div></div>').appendTo("#FilterSet");
        $("#FilterResultsDrpDwn0 option").clone().appendTo("#FilterResultsDrpDwn" + FidNo);
        $("#FilterResultsDrpDwn" + FidNo).val("0");
        jQuery("#FilterResultsDrpDwn" + FidNo).kendoDropDownList({
            animation: false
        })
    } else { }
}
function RemoveItem(b) {
    $(b).parent().parent().parent().remove()
}
function OpenCloseOptions(d, c) {
    $(d).click(function () {
        if ($(this).attr("class") != "btn-group open") {
            $(this).addClass("open");
            $(c).css("display", "block")
        } else {
            $(this).removeClass("open");
            $(c).css("display", "none")
        }
    })
}
function SetCommonQueryStrings(c, o, f, a, e, i, l) {
    var u = GetQueryStringParams("q");
    var n = GetQueryStringParams("division");
    var r = GetQueryStringParams("sf");
    var s = GetQueryStringParams("p");
    var t = GetQueryStringParams("f");
    if (u == undefined || u == "") {
        u = ""
    } else { } if (n == undefined || n == "") {
        n = "primary"
    } else { } if (s == undefined || s == "") {
        s = 1
    } else { } if (r == undefined || r == "") {
        r = ""
    } else { } if (t == undefined || t == "") {
        t = ""
    } else { }
    return CheckForAdvanveSearch(n, u, t, r, 1, c, o, f, a, e, i, l)
}
function OnSortSelect(l) {
    var h = GetQueryStringParams("s");
    if (h == undefined || h == "") {
        h = "R"
    } else { } if (l != h.toUpperCase()) {
        switch (l) {
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
        var k = GetQueryStringParams("dt"),
			i = GetQueryStringParams("dc"),
			j = GetQueryStringParams("dv"),
			e = GetQueryStringParams("ds");
        if (k == undefined || k == "") {
            k = ""
        } else { } if (i == undefined || i == "") {
            i = ""
        } else { } if (j == undefined || j == "") {
            j = ""
        } else { } if (e == undefined || e == "") {
            e = ""
        } else { }
        SetCommonQueryStrings(l, k, i, j, e, 1, 1)
    } else { }
}
function ShowOnlyFilter(q) {
    $(q).next().click();
    var r = "";
    $("#collapseTwo input:checkbox").each(function () {
        if ($(this).attr("checked") == "checked") {
            $("#ShowOnlyChk" + $(this).val()).addClass("ico-check").removeClass("ico-uncheck");
            r = r + "," + $(this).val()
        } else {
            $("#ShowOnlyChk" + $(this).val()).removeClass("ico-check").addClass("ico-uncheck")
        }
    });
    r = r.replace(/^,|,$/g, "");
    var t = GetQueryStringParams("f"),
		a = GetQueryStringParams("q"),
		s = GetQueryStringParams("division");
    if (t == undefined || t == "") {
        t = ""
    } else { } if (a == undefined || a == "") {
        a = ""
    } else { } if (s != undefined) {
        switch (s.toUpperCase()) {
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
                s = "all";
                break
        }
    } else {
        s = "all"
    }
    var e = GetQueryStringParams("s");
    if (e != undefined) {
        switch (e.toUpperCase()) {
            case "R":
                break;
            case "L":
                break;
            case "A":
                break;
            case "D":
                break;
            default:
                e = "R";
                break
        }
    } else {
        e = "R"
    }
    var f = GetQueryStringParams("dt"),
		o = GetQueryStringParams("dc"),
		n = GetQueryStringParams("dv"),
		i = GetQueryStringParams("ds");
    if (f == undefined || f == "") {
        f = ""
    } else { } if (o == undefined || o == "") {
        o = ""
    } else { } if (n == undefined || n == "") {
        n = ""
    } else { } if (i == undefined || i == "") {
        i = ""
    } else { }
    CheckForAdvanveSearch(s, a, t, r, 1, e, f, o, n, i, 1, 1)
}
function SetCategoryFilter(d) {
    var c = GetQueryStringParams("s");
    if (c == undefined || c == "") {
        c = "R"
    } else { }
    SetCommonQueryStrings(c, "dc", d, "", "", 1, 1)
}
function SetDisciplineFilter(f, e) {
    var d = GetQueryStringParams("s");
    if (d == undefined || d == "") {
        d = "R"
    } else { }
    SetCommonQueryStrings(d, "d", f, e, "", 1, 1)
}
function SetSubjectFilter(h, g, f) {
    var e = GetQueryStringParams("s");
    if (e == undefined || e == "") {
        e = "R"
    } else { }
    SetCommonQueryStrings(e, "s", h, g, f, 1, 1)
}
function CLearFacetFilter(b) {
    $("#Collapse" + b + " input:checked").each(function () {
        $(this).removeAttr("checked")
    });
    SetFacetFilter()
}
function SetFacetFilter(s) {
    var t = "",
		f = "";
    $("#FacetFilterDiv input:checkbox").each(function () {
        if ($(this).attr("checked") == "checked") {
            if ($(this).next().next().next().text().toLowerCase() == "published year") {
                $("#Collapse" + s + " input:checkbox").parent().parent().addClass("HideItems");
                $(this).parent().parent().removeClass("HideItems")
            } else { }
            $("#FacetChk" + $(this).attr("id")).addClass("ico-check").removeClass("ico-uncheck");
            var b = $(this).val().split("|");
            if (f == b[0]) {
                t = t + "," + b[1]
            } else {
                f = b[0];
                t = t + ":" + b[0] + "_" + b[1]
            }
        } else {
            $("#FacetChk" + $(this).attr("id")).removeClass("ico-check").addClass("ico-uncheck")
        }
    });
    t = t.replace(/^,|,$/g, "");
    t = t.replace(/^:|:$/g, "");
    t = t.replace(/^_|_$/g, "");
    var a = GetQueryStringParams("sf"),
		u = GetQueryStringParams("q"),
		e = GetQueryStringParams("division");
    if (a == undefined || a == "") {
        a = ""
    } else { } if (u == undefined || u == "") {
        u = ""
    } else { } if (e != undefined) {
        switch (e.toUpperCase()) {
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
                e = "all";
                break
        }
    } else {
        e = "all"
    }
    var i = GetQueryStringParams("s");
    if (i != undefined) {
        switch (i.toUpperCase()) {
            case "R":
                break;
            case "L":
                break;
            case "A":
                break;
            case "D":
                break;
            default:
                i = "R";
                break
        }
    } else {
        i = "R"
    }
    var r = GetQueryStringParams("dt"),
		n = GetQueryStringParams("dc"),
		l = GetQueryStringParams("dv"),
		o = GetQueryStringParams("ds");
    if (r == undefined || r == "") {
        r = ""
    } else { } if (n == undefined || n == "") {
        n = ""
    } else { } if (l == undefined || l == "") {
        l = ""
    } else { } if (o == undefined || o == "") {
        o = ""
    } else { }
    CheckForAdvanveSearch(e, u, t, a, 1, i, r, n, l, o, 1, 1)
}
function CheckForAdvanveSearch(C, m, e, v, p, w, f, r, b, c, d, h) {
    var N = GetQueryStringParams("st"),
		n = "";
    if (N != undefined && N != "") {
        switch (N.toLowerCase()) {
            case "ad":
                var s = GetQueryStringParams("tq");
                var o = GetQueryStringParams("aq");
                var i = GetQueryStringParams("subq");
                var a = GetQueryStringParams("faq");
                var x = GetQueryStringParams("fatvq");
                var g = GetQueryStringParams("allq");
                var l = GetQueryStringParams("etq");
                var u = GetQueryStringParams("nq");
                var S = GetQueryStringParams("fv");
                var t = GetQueryStringParams("pv");
                var T = GetQueryStringParams("pyv");
                var E = GetQueryStringParams("ov");
                if (s == undefined) {
                    s = ""
                } else { } if (o == undefined) {
                    o = ""
                } else { } if (i == undefined) {
                    i = ""
                } else { } if (a == undefined) {
                    a = ""
                } else { } if (x == undefined) {
                    x = ""
                } else { } if (g == undefined) {
                    g = ""
                } else { } if (l == undefined) {
                    l = ""
                } else { } if (u == undefined) {
                    u = ""
                } else { } if (S == undefined) {
                    S = ""
                } else { } if (t == undefined) {
                    t = ""
                } else { } if (T == undefined) {
                    T = ""
                } else { } if (E == undefined) {
                    E = ""
                } else { } if (e != "" && e != undefined) {
                    n = "search?st=ad&division=" + C + "&p=" + p + "&s=" + w + "&tq=" + s + "&aq=" + o + "&subq=" + i + "&faq=" + a + "&fatvq=" + x + "&allq=" + g + "&nq=" + u + "&etq=" + l + "&f=" + e + "&sf=" + v + "&dt=" + f + "&dc=" + r + "&dv=" + b + "&ds=" + c + "&fv=" + S + "&pv=" + t + "&pyv=" + T + "&ov=" + E
                } else {
                    n = "search?st=ad&division=" + C + "&p=" + p + "&s=" + w + "&tq=" + s + "&f=&aq=" + o + "&subq=" + i + "&faq=" + a + "&fatvq=" + x + "&allq=" + g + "&nq=" + u + "&etq=" + l + "&sf=" + v + "&dt=" + f + "&dc=" + r + "&dv=" + b + "&ds=" + c + "&fv=" + S + "&pv=" + t + "&pyv=" + T + "&ov=" + E
                }
                break;
            case "advisbn":
                var y = GetQueryStringParams("keyisbn");
                if (y == undefined) {
                    y = ""
                }
                if (e != "" && e != undefined) {
                    n = "search?searchtype=advisbn&division=" + C + "&p=" + p + "&s=" + w + "&f=" + e + "&keyisbn=" + y + "&sf=" + v + "&dt=" + f + "&dc=" + r + "&dv=" + b + "&ds=" + c
                } else {
                    n = "search?searchtype=advisbn&division=" + C + "&p=" + p + "&s=" + w + "&keyisbn=" + y + "&f=&sf=" + v + "&dt=" + f + "&dc=" + r + "&dv=" + b + "&ds=" + c
                }
                break;
            default:
                break
        }
    } else {
        if (e != "" && e != undefined) {
            n = "search?q=" + m + "&division=" + C + "&p=" + p + "&s=" + w + "&f=" + e + "&sf=" + v + "&dt=" + f + "&dc=" + r + "&dv=" + b + "&ds=" + c
        } else {
            n = "search?q=" + m + "&division=" + C + "&p=" + p + "&s=" + w + "&f=&sf=" + v + "&dt=" + f + "&dc=" + r + "&dv=" + b + "&ds=" + c
        }
    }
    if ($("#AdvSearchOpenFlagHdn1").val() == "t") {
        n = n + "&ao=t"
    } else { }
    return ReturnOrRefreshUrl(h, n, d)
}
function ReturnOrRefreshUrl(f, e, d) {
    if (f == 0) {
        return e + "&cms=t&cp=" + d
    } else {
        window.open(e, "_self")
    }
}
function AdvanceSearch() {
    var e = $("#AdvanceSearchDiv input:text").length,
		r = 0;
    $("#AdvanceSearchDiv input:text").each(function () {
        if ($(this).val().trim() == "") {
            r++
        }
    });
    if (r == e) {
        $("#AdvTitleTxt").focus();
        return
    } else { }
    var t = "",
		n = "",
		h = "",
		u = "",
		s = "",
		A = "",
		i = "",
		o = "",
		c = "",
		a = "",
		f = "",
		B = "";
    $("#AttributeSet select.rec-dropdown").each(function () {
        var b = $(this).parent().parent().children("input").val();
        if (b != "") {
            switch ($(this).find("option:selected").text().toLowerCase()) {
                case "format":
                    n = n + "," + b;
                    c = $(this).val();
                    break;
                case "publisher":
                    h = h + "," + b;
                    a = $(this).val();
                    break;
                case "published year":
                    u = u + "," + b;
                    f = $(this).val();
                    break;
                case "origin":
                    s = s + "," + b;
                    B = $(this).val();
                    break;
                default:
                    break
            }
        } else { }
    });
    n = n.replace(/^,|,$/g, "");
    h = h.replace(/^,|,$/g, "");
    u = u.replace(/^,|,$/g, "");
    s = s.replace(/^,|,$/g, "");
    n = c + ":" + n;
    n = n.replace(/^:|:$/g, "");
    h = a + ":" + h;
    h = h.replace(/^:|:$/g, "");
    u = f + ":" + u;
    u = u.replace(/^:|:$/g, "");
    s = B + ":" + s;
    s = s.replace(/^:|:$/g, "");
    $("#FilterSet select.rec-dropdown").each(function () {
        if ($(this).next().val() != "") {
            switch ($(this).val()) {
                case "1":
                    A = A + "," + $(this).parent().parent().children("input").val();
                    break;
                case "2":
                    i = i + "," + $(this).parent().parent().children("input").val();
                    break;
                case "3":
                    o = o + "," + $(this).parent().parent().children("input").val();
                    break;
                default:
                    break
            }
        } else { }
    });
    A = A.replace(/^,|,$/g, "");
    i = i.replace(/^,|,$/g, "");
    o = o.replace(/^,|,$/g, "");
    var l = "division=" + GetQueryStringParams("division") + "&tq=" + $("#AdvTitleTxt").val() + "&aq=" + $("#AdvAuthorTxt").val() + "&subq=" + $("#AdvSubjectTxt").val() + "&allq=" + A + "&nq=" + o + "&etq=" + i;
    l = l + "&fv=" + n + "&pv=" + h + "&pyv=" + u + "&ov=" + s;
    if (GetQueryStringParams("cms") == "t") {
        window.location.href = "search?cms=t&st=ad&" + l
    } else {
        window.location.href = "search?st=ad&" + l
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
    GAPushTrackEvent("Site-Search", "Click", "Advance-Search-Button")
}
function CheckEnterKeyPress(f) {
    var e = "which" in f ? f.which : f.keyCode;
    if (e == 13) {
        var d = AdvanceSearch();
        return d
    } else { }
}
function SetFileName(b) {
    $("#MultipleISBNBrwsVal").text(b.split(/(\\|\/)/g).pop());
    if ($("#MultipleISBNBrwsVal").text() != "") {
        $("#MultiConfirmBtnDiv").removeClass("HideItems");
        $("#MultipleISBNPopup").addClass("multiisbnpopuphgt")
    } else {
        $("#MultiConfirmBtnDiv").addClass("HideItems");
        $("#MultipleISBNPopup").removeClass("multiisbnpopuphgt")
    }
}
var dkwindow, dkwindow1, dkwindow2, dkwindow3, dkwindow4, dkwindow5, dkwindow6, AidNo = 0,
	FidNo = 0,
	cmsclick = false;
$(function () {
    var m = GetQueryStringParams("q");
    if (m != undefined) {
        $("#TextSearch").val(decodeURIComponent(m))
    }
    $("#ProductsSortDpdwn").kendoDropDownList({
        animation: false
    });
    $("#ProductsSortDpdwn-list").addClass("sortDpnParent");
    var g = GetQueryStringParams("cms");
    if (g == undefined || g == "") {
        g = "products"
    }
    if ($("#LoadCMSHdn").val() == "Y") {
        $("#infotab").click()
    }
    switch (g.toLowerCase()) {
        case "t":
            cmsclick = true;
            $("#infotab").click();
            if ($("#TotalResultLbl").text() > 0) {
                $("#CurrentPgStartSizeSpan").text($("#CurrentPgStartSizeLbl").text());
                $("#CurrentPgEndSizeSpan").text($("#CurrentPgEndSizeLbl").text());
                $("#TotalResultSpan").text($("#TotalResultLbl").text())
                $('#HasResultDiv').removeClass('HideItems');
                $('#HasNoResultDiv').addClass('HideItems');
            }
            else {
                $('#HasResultDiv').addClass('HideItems');
                $('#HasNoResultDiv').removeClass('HideItems');
            }
            break
        default:
            if ($("#ProdTotalResultLbl").text() > 0) {
                $("#CurrentPgStartSizeSpan").text($("#ProdCurrentPgStartSizeLbl").text());
                $("#CurrentPgEndSizeSpan").text($("#ProdCurrentPgEndSizeLbl").text());
                $("#TotalResultSpan").text($("#ProdTotalResultLbl").text())
                $('#HasResultDiv').removeClass('HideItems');
                $('#HasNoResultDiv').addClass('HideItems');
            }
            else {
                $('#HasResultDiv').addClass('HideItems');
                $('#HasNoResultDiv').removeClass('HideItems');
            }
            break;
    }
    var t = GetQueryStringParams("s");
    switch (t) {
        case "A":
            $("#ProductsSortDpdwn").parent().children().children().first().text("Title (A to Z)");
            $("#ProductsSortDpdwn").val(t);
            break;
        case "D":
            $("#ProductsSortDpdwn").parent().children().children().first().text("Title (Z to A)");
            $("#ProductsSortDpdwn").val(t);
            break;
        case "L":
            $("#ProductsSortDpdwn").parent().children().children().first().text("Latest");
            $("#ProductsSortDpdwn").val(t);
            break;
        default:
            $("#ProductsSortDpdwn").parent().children().children().first().text("Recommended");
            $("#ProductsSortDpdwn").val("R");
            break
    }
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
        var k = $("#Isbntextarea").val();
        if (k.trim() != "") {
            $("#MultiIsbnError").addClass("HideItems");
            $("#MultiISBNtextarea").val($("#Isbntextarea").val());
            var j = {
                SearchText: k,
                Division: GetQueryStringParams("division")
            };
            j = JSON.stringify(j);
            $.ajax({
                url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=multipleisbn"),
                cache: false,
                type: "POST",
                data: j,
                async: false,
                success: function (q) {
                    window.open(q.replaceAll('"', ""), "_parent")
                }
            })
        } else {
            $("#MultiIsbnError").removeClass("HideItems")
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
    $("#MultiIsbnConfirmBtn").click(function () {
        $.ajax({
            url: "desktopmodules/hesearchresults/views/heproductresults.ascx/export",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (j) {
                if (j.d) { } else { }
            }
        })
    });
    $("#AdvanceSearchDiv input").keyup(function (k) {
        if (k.keyCode == 13) {
            var j = AdvanceSearch();
            return j
        } else { }
    });
    $(".accordion-toggle").click(function () {
        var j = $(this).children().first();
        switch (j.attr("class")) {
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
    var d = GetQueryStringParams("ao");
    if (d != undefined && d.toLowerCase() == "t") {
        OpenAdvanceSearch()
    } else { }
    var y = [],
		e = [];
    var u = GetQueryStringParams("sf"),
		h = "",
		s = GetQueryStringParams("f"),
		o = "",
		b = GetQueryStringParams("dt"),
		r = GetQueryStringParams("dc"),
		v = GetQueryStringParams("dv"),
		i = GetQueryStringParams("ds");
    if (u != undefined && u != "") {
        y = u.split(",");
        $("#collapseTwo input:checkbox").each(function () {
            h = $(this).val().toString();
            if ($.inArray(h, y) != -1) {
                $(this).attr("checked", "checked");
                $("#ShowOnlyChk" + h).removeClass("ico-uncheck").addClass("ico-check")
            }
        })
    } else { } if (s != undefined && s != "") {
        e = s.split(":");
        $.each(e, function (w, q) {
            var k = q.split("_");
            $("#FacetAnchor" + k[0]).click();
            if (k[1] != undefined) {
                var j = k[1].split(",");
                $.each(j, function (x, z) {
                    $("#" + k[0] + z).attr("checked", "checked");
                    $("#FacetChk" + k[0] + z).removeClass("ico-uncheck").addClass("ico-check")
                })
            }
        })
    } else { }
    switch (b) {
        case "dc":
            $("#dc" + r).click();
            var n = $("#" + $("#collapseCategory" + r).attr("id") + " .dlink");
            if ($(n).length == 1) {
                $(n).click();
                if ($(".subli").length == 1) {
                    $(".subli").addClass("activehierarchyitem")
                } else {
                    $(".dlink").parent().addClass("activehierarchyitem")
                }
            } else {
                $("#Lidc" + r).addClass("activehierarchyitem")
            }
            break;
        case "d":
            $("#dc" + r).click();
            if ($(".subli").length == 1) {
                $(".subli").addClass("activehierarchyitem")
            } else {
                $("#Lid" + r + "d" + v).addClass("activehierarchyitem")
            }
            $("#dc" + r + "d" + v).click();
            break;
        case "s":
            $("#dc" + r).click();
            $("#dc" + r + "d" + v).click();
            $("#Lis" + r + "d" + v + "s" + i).addClass("activehierarchyitem");
            break;
        default:
            if ($(".dclink").length == 1) {
                $(".dclink").click();
                if ($(".dlink").length == 1) {
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
            break
    }
    if ($("#DivisionHdn").val() != undefined) {
        switch ($("#DivisionHdn").val().toLowerCase()) {
            case "vocational":
                $("#DivisionHdn").val("Vocational");
                $(".DivisionLbl").text("Vocational");
                break;
            case "gale":
                $("#DivisionHdn").val("Gale");
                $(".DivisionLbl").text("Gale");
                break;
            default:
                $("#DivisionHdn").val("Higher Education");
                $(".DivisionLbl").text("Higher Education");
                break
        }
    }
    SetUpProductResults();
    $("#productstab").click(function () {
        if ($("#ProdTotalResultLbl").text() > 0) {
            $("#CurrentPgStartSizeSpan").text($("#ProdCurrentPgStartSizeLbl").text());
            $("#CurrentPgEndSizeSpan").text($("#ProdCurrentPgEndSizeLbl").text());
            $("#TotalResultSpan").text($("#ProdTotalResultLbl").text())
            $('#HasResultDiv').removeClass('HideItems');
            $('#HasNoResultDiv').addClass('HideItems');
        }
        else {
            $('#HasResultDiv').addClass('HideItems');
            $('#HasNoResultDiv').removeClass('HideItems');
        }
    });
    var l = $("#CMSPageNumberHdn").val(),
		c = 0,
		p = 0,
		f = "",
		a = 15;
    $("#infotab").click(function () {
        var k = decodeURIComponent(GetQueryStringParams("q"));
        if (!cmsclick) {
            cmsclick = true;
            $("#Loaderimg").css("display", "block");
            $.ajax({
                type: "POST",
                url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=cms&q=" + k + "&division=" + GetQueryStringParams("division")),
                dataType: "json",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (C) {
                    $.each(C, function (F, G) {
                        p++;
                        f = f + '<div class="span6"><div class="cnt"><h4>';
                        f = f + '<a href="' + G.TabUrl + '">' + G.TabName + "</a>";
                        f = f + "</h4>" + G.TabDescription;
                        f = f + '<div class="infolink">';
                        f = f + '<a href="' + G.TabUrl + '">' + G.TabUrl + "</a>";
                        f = f + "</div></div></div>"
                    });
                    if (p == 0 && GetQueryStringParams("f") == undefined) {
                        $("#HasNoResultDiv").removeClass("HideItems");
                        $("#HasResultDiv").addClass("HideItems");
                        $('<div class="search-no-result"><div class="no-result-txt"><p>Sorry! There are no matching CMS page results for</p><p class="keyword">"' + k + '"</p></div><div class="suggestions-txt"><p class="keyword">Suggestions:</p><p>-Make sure that all words are spelled correctly.</p><p>-Try different keywords.</p><p>-Try more general keywords.</p></div></div>').appendTo("#tab2")
                    } else {
                        if (p == 0) {
                            $("#HasNoResultDiv").removeClass("HideItems");
                            $("#HasResultDiv").addClass("HideItems");
                            $('<h5>Your search did not return any results.</h5><h5>Suggestions</h5><ul><li>- <a href="#">Reset your advanced search options.</a></li><li>- <a href="#">Make sure that all words are spelled correctly.</a></li><li>- <a href="#">Try different keywords.</a></li><li>- <a href="#">Try more general keywords.</a></li></ul>').appendTo("#tab2")
                        } else {
                            $.ajax({
                                type: "POST",
                                url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=cms&q=" + k + "&division=" + GetQueryStringParams("division") + "&type=cmscount"),
                                dataType: "json",
                                async: false,
                                contentType: "application/json; charset=utf-8",
                                success: function (F) {
                                    c = F
                                }
                            });
                            if (c > 0) {
                                $("#CurrentPgStartSizeSpan").text((l - 1) * p + 1);
                                $("#CurrentPgEndSizeSpan").text(Math.min(l * p, a));
                                $("#TotalResultSpan").text(c)
                                $('#HasResultDiv').removeClass('HideItems');
                                $('#HasNoResultDiv').addClass('HideItems');
                            }
                            else {
                                $('#HasResultDiv').addClass('HideItems');
                                $('#HasNoResultDiv').removeClass('HideItems');
                            }
                            $(f).appendTo("#tab2");
                            var w = GetQueryStringParams("s"),
								q = GetQueryStringParams("dt"),
								E = GetQueryStringParams("dc"),
								z = GetQueryStringParams("dv"),
								x = GetQueryStringParams("ds");
                            if (w == undefined || w == "") {
                                w = "R"
                            } else { } if (q == undefined || q == "") {
                                q = ""
                            } else { } if (E == undefined || E == "") {
                                E = ""
                            } else { } if (z == undefined || z == "") {
                                z = ""
                            } else { } if (x == undefined || x == "") {
                                x = ""
                            } else { } if (a > 15) {
                                var D = '<div id="PagerHoldercms" align="center" class="he-Pager">';
                                D = D + '<div id="StudentPagerDivcms" clientidmode="Static" class="he-srrefdiv" runat="server">';
                                D = D + '<span class="he-srresdiv">';
                                D = D + '<a href="' + SetCommonQueryStrings(w, q, E, z, x, 1, 0) + '"><<</a>';
                                D = D + "<a><</a>";
                                D = D + "</span>";
                                D = D + '<span id="cmsPageControlcms" class="he-srretdiv">';
                                var A = a / 10 + (a % 10 == 0 ? 0 : 1);
                                for (var B = 1; B <= A; B++) {
                                    D = D + '<a href="' + SetCommonQueryStrings(w, q, E, z, x, B, 0) + '">' + B + "</a>"
                                }
                                D = D + "</span>";
                                D = D + '<span class="he-srrefrdiv">';
                                D = D + "<a>></a>";
                                D = D + '<a href="' + SetCommonQueryStrings(w, q, E, z, x, A, 0) + '">>></a>';
                                D = D + "</span>";
                                D = D + "</div>";
                                D = D + "</div>";
                                $(D).appendTo("#tab2")
                            }
                        }
                    }
                }
            })
        } else {
            var j = GetQueryStringParams("cms");
            if (j == undefined || j == "") {
                if (c > 0) {
                    $("#CurrentPgStartSizeSpan").text((l - 1) * p + 1);
                    $("#CurrentPgEndSizeSpan").text(Math.min(l * p, a));
                    $("#TotalResultSpan").text(c)
                    $('#HasResultDiv').removeClass('HideItems');
                    $('#HasNoResultDiv').addClass('HideItems');
                }
                else {
                    $('#HasResultDiv').addClass('HideItems');
                    $('#HasNoResultDiv').removeClass('HideItems');
                }
            } else {
                if ($("#TotalResultLbl").text() > 0) {
                    $("#CurrentPgStartSizeSpan").text($("#CurrentPgStartSizeLbl").text());
                    $("#CurrentPgEndSizeSpan").text($("#CurrentPgEndSizeLbl").text());
                    $("#TotalResultSpan").text($("#TotalResultLbl").text())
                    $('#HasResultDiv').removeClass('HideItems');
                    $('#HasNoResultDiv').addClass('HideItems');
                }
                else {
                    $('#HasResultDiv').addClass('HideItems');
                    $('#HasNoResultDiv').removeClass('HideItems');
                }
            }
        }
    });
    $("#tab3").live("click", function () {
        OpenAdvanceSearch()
    });
    $("#clearAdvSearch").click(function () {
        $("#AdvResetBtn").click();
        $("#tab3").click()
    });

    $("div.favorite").on("mouseover", function () {
        if ($(this).children().next().attr("class") == "btn btn-onoff btn-on") {
            $(this).children().next().val("UNFAVORITE")
        } else {
            $(this).children().next().val("FAVORITE")
        }
    });
    $("div.favorite").on("mouseout", function () {
        $(this).children().next().val("FAVORITE")
    });
  
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
        if ($("#SearchNameTxt").val() != "") {
            $("#SaveSearchNameerror").addClass("HideItems");
            dkwindow.data("kendoWindow").close();
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
        return false
    });
    $("#CancelButtonA").click(function () {
        dkwindow.data("kendoWindow").close();
        return false
    });
    $("#SaveButtonB").click(function () {
        var j = {
            CurrentUrl: window.location.href,
            KeyWords: $("#SearchTextHdn").val(),
            SearchName: $("#SearchNameTxt").val(),
            Division: GetQueryStringParams("division")
        };
        j = JSON.stringify(j);
        $.ajax({
            url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=savesearch"),
            cache: false,
            type: "POST",
            data: j,
            success: function (k) {
                dkwindow1.data("kendoWindow").close()
            }
        });
        return false
    });
    $("#CancelButtonB").click(function () {
        dkwindow1.data("kendoWindow").close();
        return false
    });
    $("#SaveSearchLoginBtn").click(function () {
        if ($("#AnonSaveSearchTxt").val() != "") {
            $("#AnonSaveSearchNameerror").addClass("HideItems");
            dkwindow3.data("kendoWindow").close();
            $("#loginlnk").click();
            return false
        } else {
            $("#AnonSaveSearchNameerror").removeClass("HideItems")
        }
        return false
    });
    $("#SaveSearchSignUpBtn").click(function () {
        if ($("#AnonSaveSearchTxt").val() != "") {
            $("#AnonSaveSearchNameerror").addClass("HideItems");
            dkwindow3.data("kendoWindow").close();
            window.location.href = "/signup";
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
    dkwindow4 = $("#MoreFavoritePopUp");
    $("#MoreFavOkBtn").click(function () {
        dkwindow4.data("kendoWindow").close();
        return false
    });
    jQuery("#AdvAttrTypesDrpDwn0").kendoDropDownList({
        animation: false
    });
    jQuery("#FilterResultsDrpDwn0").kendoDropDownList({
        animation: false
    })
});

function SetUpProductResults() {
    $(".AvailabilityCheck").each(function () {
        if ($(this).text().toLowerCase().trim() != "") {
            $(this).parent().addClass("ShowItems")
        } else {
            $(this).parent().addClass("HideItems")
        }
    });
    $(".AvailabilityCheck1").each(function () {
        if ($(this).text().toLowerCase().trim() != "") {
            $(this).parent().parent().addClass("ShowItems")
        } else {
            $(this).parent().parent().addClass("HideItems")
        }
    });
    $(".NewFlag").each(function () {
        if ($(this).text().toLowerCase().trim() == "y") {
            $(this).text("New");
            $(this).parent().removeClass("HideItems")
        } else {
            $(this).parent().addClass("HideItems")
        }
    });
    $(".eChapterFlag").each(function () {
        if ($(this).text().toLowerCase().trim() == "y") {
            $(this).text("Available")
        } else {
            $(this).parent().parent().addClass("HideItems")
        }
    });
    $(".eBookCheck").each(function () {
        if ($(this).text().toLowerCase().trim() == "ebk") {
            $(this).text("eBook From")
        } else {
            $(this).text("eBook")
        }
    });
    $(".SuppAvl").each(function () {
        if ($(this).text().toLowerCase().trim() == "y") {
            $(this).text("available")
        } else {
            $(this).parent().addClass("HideItems")
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
}