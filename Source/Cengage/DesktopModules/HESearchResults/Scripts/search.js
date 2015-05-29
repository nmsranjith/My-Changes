function GetFile(e) { var t = window.location.pathname; var n = t.split("/"); var r = location.protocol + "//" + window.location.host + "/" + n[0]; var i = r + e; return i }

function ShowfacetRelation() {
    var e = GetQueryStringParams("f");
    var t = false;
    if (e != undefined && e != "") {
        var n = [];
        n = e.split(":");
        for (var r = 0; r < n.length; r++) {
            var i = n[r].split("_")[0];
            var s = n[r].split("_")[1];
            if (s == undefined) {
                s = "0"
            }
            var o = s.split(",");
            for (var u = 0; u < o.length; u++) {
                $(".LeftMenu span input").each(function () {
                    var e = $(this).parent().next()[0].value.split("_");
                    if (e[0] == i && e[1] == o[u]) {
                        t = true;
                        if (u != 0) {
                            $("#SelectedAttributeList").append("<li class='newLiStyle'><div><span class='H5 or filter'>OR</span></div></li>")
                        }
                        $("#SelectedAttributeList").append("<li class='newLiStyle'><div class='maths H5'><span class='or newfilter' style='color: #707070 !important;'>" + $("input[value^=" + i + "_" + o[u] + "]")[0].defaultValue.split("_")[3] + "</span><div class='crs' onclick=RemoveAttr('" + i + "_" + o[u] + "');></div></div></li>")
                    }
                })
            }
            if (t) {
                if (r != n.length - 1) {
                    $("#SelectedAttributeList").append("<li class='newLiStyle Plusc'><div class='plus'>+</div></li>")
                }
                t = false
            }
        }
    }
}

function RemoveAttr(e) {
    var t = $("input[value^=" + e + "]")[0].nextElementSibling;
    FireCheckedEvent(t)
}

function FireCheckedEvent(e) {
    var t = true;
    for (var n = 1; n < e.parentNode.parentNode.parentNode.children.length; n++) {
        if (e.parentNode.parentNode.parentNode.children[n].children[0].checked & e.parentNode.parentNode.parentNode.children[0].value == "N") {
            t = false
        }
    }
    if (t) {
        if (e.src.indexOf("check") > 0) {
            e.src = GetFile("/Portals/0/Images/pagebk.PNG")
        } else {
            e.src = GetFile("/Portals/0/Images/check.PNG");
            e.parentNode.parentNode.style.cssText = "font-weight: 700"
        }
        e.parentNode.children[0].children[0].click()
    }
    CurrentAttributeState(e)
}

function OnSortSelect(e) {
    var t = this.dataItem(e.item.index());
    var n = GetQueryStringParams("q");
    var r = GetQueryStringParams("division");
    var i = GetQueryStringParams("p");
    var s = GetQueryStringParams("f");
    if (n == undefined) {
        n = ""
    }
    if (r == undefined) {
        r = "primary"
    }
    if (i == undefined) {
        i = 1
    }
    var o = GetQueryStringParams("searchtype");
    if (o != undefined && o != "") {
        var u = "";
        switch (t.value.toLowerCase()) {
            case "l":
                u = "L";
                break;
            case "a":
                u = "A";
                break;
            case "d":
                u = "D";
                break;
            default:
                u = "R";
                break
        }
        switch (o.toLowerCase()) {
            case "advkey":
                var a = GetQueryStringParams("k_q");
                var f = GetQueryStringParams("t_q");
                var l = GetQueryStringParams("a_q");
                var c = GetQueryStringParams("Sub_q");
                var h = GetQueryStringParams("fa_q");
                var p = GetQueryStringParams("fatv_q");
                var d = GetQueryStringParams("all_q");
                var v = GetQueryStringParams("et_q");
                if (a == undefined) {
                    a = ""
                }
                if (f == undefined) {
                    f = ""
                }
                if (l == undefined) {
                    l = ""
                }
                if (c == undefined) {
                    c = ""
                }
                if (h == undefined) {
                    h = ""
                }
                if (p == undefined) {
                    p = ""
                }
                if (d == undefined) {
                    d = ""
                }
                if (v == undefined) {
                    v = ""
                }
                if (s != "" && s != undefined) {
                    window.open("/search?searchtype=advkey&division=" + r + "&p=" + i + "&s=" + u + "&k_q=" + a + "&t_q=" + f + "&a_q=" + l + "&Sub_q=" + c + "&fa_q=" + h + "&fatv_q=" + p + "&all_q=" + d + "&et_q=" + v + "&f=" + s, "_self")
                } else {
                    window.open("/search?searchtype=advkey&division=" + r + "&p=" + i + "&s=" + u + "&k_q=" + a + "&t_q=" + f + "&a_q=" + l + "&Sub_q=" + c + "&fa_q=" + h + "&fatv_q=" + p + "&all_q=" + d + "&et_q=" + v, "_self")
                }
                break;
            case "advpri":
                break;
            case "advsec":
                break;
            case "advisbn":
                var m = GetQueryStringParams("keyisbn");
                if (m == undefined) {
                    m = ""
                }
                if (s != "" && s != undefined) {
                    window.open("/search?q=&searchtype=advisbn&division=" + r + "&p=" + i + "&s=" + u + "&f=" + s + "&keyisbn=" + m, "_self")
                } else {
                    window.open("/search?q=&searchtype=advisbn&division=" + r + "&p=" + i + "&s=" + u + "&keyisbn=" + m, "_self")
                }
                break;
            default:
                break
        }
    } else {
        switch (t.value.toLowerCase()) {
            case "l":
                u = "L";
                break;
            case "a":
                u = "A";
                break;
            case "d":
                u = "D";
                break;
            default:
                u = "R";
                break
        }
        if (s != "" && s != undefined) {
            window.open("/search?q=" + n + "&division=" + r + "&p=" + i + "&s=" + u + "&f=" + s, "_self")
        } else {
            window.open("/search?q=" + n + "&division=" + r + "&p=" + i + "&s=" + u, "_self")
        }
    }
}

function GetQueryStringParams(e) {
    var t = window.location.search.substring(1);
    var n = t.split("&");
    for (var r = 0; r < n.length; r++) {
        var i = n[r].split("=");
        if (i[0] == e) {
            return i[1]
        }
    }
}

function CurrentAttributeState(e) {
    var t = [];
    $("input[name^=" + $(e).next()[0].value + "_AttributeInfo]").each(function () {
        var e = $(this).val().split("_");
        var n = false;
        if (t.length > 0) {
            for (var r = 0; r < t.length; r++) {
                if (t[r].ATTRIBUTE_TYPE_SK == e[0] && t[r].ATTRIBUTE_TYPE_VALUE_SK == e[1]) {
                    n = true
                }
            }
        }
        if (!n) {
            t.push({
                ATTRIBUTE_TYPE_SK: e[0],
                ATTRIBUTE_TYPE_VALUE_SK: e[1],
                ATTRIBUTE_NAME: e[2],
                ATTRIBUTE_TYPE_VALUE: e[3],
                PROD_COUNT: e[4],
                IS_CURRENT: "Y",
                IS_SELECTED: "N",
                SEQNUM: e[6]
            })
        }
    });
    var n = [];
    $(".LeftMenu span input:checked").each(function () {
        var e = $(this).parent().next()[0].value.split("_");
        var t = false;
        if (n.length > 0) {
            for (var r = 0; r < n.length; r++) {
                if (n[r].ATTRIBUTE_TYPE_SK == e[0] && n[r].ATTRIBUTE_TYPE_VALUE_SK == e[1]) {
                    t = true
                }
            }
        }
        if (!t) {
            n.push({
                ATTRIBUTE_TYPE_SK: e[0],
                ATTRIBUTE_TYPE_VALUE_SK: e[1],
                ATTRIBUTE_NAME: e[2],
                ATTRIBUTE_TYPE_VALUE: e[3],
                PROD_COUNT: e[4],
                IS_CURRENT: "N",
                IS_SELECTED: "Y",
                SEQNUM: e[6]
            })
        }
    });
    if (t.length > 0) {
        for (var r = 0; r < t.length; r++) {
            iscurrentchecked = false;
            if (n.length > 0) {
                for (var i = 0; i < n.length; i++) {
                    if (t[r].ATTRIBUTE_TYPE_SK == n[i].ATTRIBUTE_TYPE_SK && t[r].ATTRIBUTE_TYPE_VALUE_SK == n[i].ATTRIBUTE_TYPE_VALUE_SK) {
                        n[i].IS_SELECTED = "Y";
                        n[i].IS_CURRENT = "Y"
                    }
                }
            }
        }
    }
    if (t.length > 0) {
        for (var r = 0; r < t.length; r++) {
            isexist = false;
            if (n.length > 0) {
                for (var i = 0; i < n.length; i++) {
                    if (t[r].ATTRIBUTE_TYPE_SK == n[i].ATTRIBUTE_TYPE_SK && t[r].ATTRIBUTE_TYPE_VALUE_SK == n[i].ATTRIBUTE_TYPE_VALUE_SK) {
                        isexist = true
                    }
                }
            }
            if (!isexist) {
                n.push({
                    ATTRIBUTE_TYPE_SK: t[r].ATTRIBUTE_TYPE_SK,
                    ATTRIBUTE_TYPE_VALUE_SK: t[r].ATTRIBUTE_TYPE_VALUE_SK,
                    ATTRIBUTE_NAME: t[r].ATTRIBUTE_NAME,
                    ATTRIBUTE_TYPE_VALUE: t[r].ATTRIBUTE_TYPE_VALUE,
                    PROD_COUNT: t[r].PROD_COUNT,
                    IS_CURRENT: "Y",
                    IS_SELECTED: "N",
                    SEQNUM: t[r].SEQNUM
                })
            }
        }
    }
    $.ajax({
        url: "/DesktopModules/hesearchresults/Handlers/AttributeState.ashx",
        type: "POST",
        cache: false,
        data: JSON.stringify(n),
        async: false,
        success: function (e) { }
    });
    var s = getFacetQueryString();
    var o = GetQueryStringParams("q");
    if (o == undefined || o == "") {
        o = ""
    }
    var u = GetQueryStringParams("division");
    if (u != undefined) {
        switch (u.toUpperCase()) {
            case "PRIMARY":
                break;
            case "SECONDARY":
                break;
            case "ALL":
                break;
            default:
                u = "all";
                break
        }
    } else {
        u = "all"
    }
    var a = GetQueryStringParams("s");
    if (a != undefined) {
        switch (a.toUpperCase()) {
            case "R":
                break;
            case "L":
                break;
            case "A":
                break;
            case "D":
                break;
            default:
                a = "R";
                break
        }
    } else {
        a = "R"
    }
    var f = 1;
    var l = GetQueryStringParams("searchtype");
    if (l != undefined && l != "") {
        switch (l.toLowerCase()) {
            case "advkey":
                var c = GetQueryStringParams("k_q");
                var h = GetQueryStringParams("t_q");
                var p = GetQueryStringParams("a_q");
                var d = GetQueryStringParams("Sub_q");
                var v = GetQueryStringParams("fa_q");
                var m = GetQueryStringParams("fatv_q");
                var g = GetQueryStringParams("all_q");
                var y = GetQueryStringParams("et_q");
                if (c == undefined) {
                    c = ""
                }
                if (h == undefined) {
                    h = ""
                }
                if (p == undefined) {
                    p = ""
                }
                if (d == undefined) {
                    d = ""
                }
                if (v == undefined) {
                    v = ""
                }
                if (m == undefined) {
                    m = ""
                }
                if (g == undefined) {
                    g = ""
                }
                if (y == undefined) {
                    y = ""
                }
                window.open("/search?searchtype=advkey&division=" + u + "&p=" + f + "&s=" + a + "&k_q=" + c + "&t_q=" + h + "&a_q=" + p + "&Sub_q=" + d + "&fa_q=" + v + "&fatv_q=" + m + "&all_q=" + g + "&et_q=" + y + "&f=" + s, "_self");
                break;
            case "advpri":
                break;
            case "advsec":
                break;
            case "advisbn":
                var b = GetQueryStringParams("keyisbn");
                if (b == undefined) {
                    b = ""
                }
                if (s != "" && s != undefined) {
                    window.open("/search?q=&searchtype=advisbn&division=" + u + "&p=" + f + "&s=" + a + "&f=" + s + "&keyisbn=" + b, "_self")
                } else {
                    window.open("/search?q=&searchtype=advisbn&division=" + u + "&p=" + f + "&s=" + a + "&keyisbn=" + b, "_self")
                }
                break;
                break;
            default:
                break
        }
    } else {
        window.open("/search?q=" + o + "&division=" + u + "&p=" + f + "&s=" + a + "&f=" + s, "_self")
    }
}

function getFacetQueryString() {
    var e = [];
    var t = "";
    if ($(".LeftMenu span input:checked").length > 0) {
        for (var n = 0; $(".LeftMenu span input:checked").parent().parent().next(".lefttext").children("input").length > n; n += 2) {
            var r = 0;
            for (i = 0; i < e.length; i++) {
                if (e[i].Name == $(".LeftMenu span input:checked").parent().parent().next(".lefttext").children("input")[n + 1].value) {
                    r = 1;
                    e[i].Value = e[i].Value + "," + $(".LeftMenu span input:checked").parent().parent().next(".lefttext").children("input")[n].value
                }
            }
            if (r == 0) {
                e.push({
                    Name: $(".LeftMenu span input:checked").parent().parent().next(".lefttext").children("input")[n + 1].value,
                    Value: $(".LeftMenu span input:checked").parent().parent().next(".lefttext").children("input")[n].value
                })
            }
        }
        for (i = 0; i < e.length; i++) {
            t = t + ":" + e[i].Name + "_" + e[i].Value
        }
    }
    if (t != "") {
        return t.substring(1)
    } else {
        return t
    }
}

function TakeAttributes() {
    var e = [];
    if ($(".LeftMenu span input:checked").length > 0) {
        for (var t = 0; $(".LeftMenu span input:checked").parent().parent().next(".lefttext").children("input").length > t; t += 2) {
            var n = 0;
            for (i = 0; i < e.length; i++) {
                if (e[i].Name == $(".LeftMenu span input:checked").parent().parent().next(".lefttext").children("input")[t + 1].value) {
                    n = 1;
                    e[i].Value = e[i].Value + "," + $(".LeftMenu span input:checked").parent().parent().next(".lefttext").children("input")[t].value
                }
            }
            if (n == 0) {
                e.push({
                    Name: $(".LeftMenu span input:checked").parent().parent().next(".lefttext").children("input")[t + 1].value,
                    Value: $(".LeftMenu span input:checked").parent().parent().next(".lefttext").children("input")[t].value
                })
            }
        }
        var r = "";
        for (i = 0; i < e.length; i++) {
            r = r + ":" + e[i].Name + "_" + e[i].Value
        }
    }
    return r
}

function onselect(e) {
    var t = this.dataItem(e.item.index());
    selectoperation(t.NAME, t.SK);
    if (SuccessFlagForGA) {
        GAPushTrackEvent("Lists", "Click", "Add-to-list")
    }
}

function selectoperation(e, t) {
    isCart = false;
    isList = true;
    isQuote = false;
    callcount = 0;
    var n = "";
    var r = "";
    if ($("#selqty")[0].innerHTML == 0) {
        $("#msglbl").css("display", "block");
        $("#MsgDiv").addClass("dnnFormWarning");
        $("#MsgDiv").text("Please select one or more items to add to the List.").show();
        $("#addtolstdiv1 select").data("kendoDropDownList").text("Add to list");
        return false
    }
    var i = $(".ipadisbn").length;
    var s = "";
    var o = "";
    var u = "";
    if (i != 0) {
        $("#Spanisbn").text(" ");
        $("#OutOfStockIsbn").text(" ");
        $.each($(".ipadisbn"), function () {
            var e = $(this).val().split("-");
            var t = e[2];
            var i = e[3];
            if (t == "Y") {
                n = e[4] + "-" + e[1] + "/" + n;
                if (i != "OUT OF STOCK") {
                    r = e[4] + "-" + e[1] + "/" + r
                }
            }
            if (t == "N") {
                s = e[0];
                u = $("#ipadtit" + e[0]).val();
                $("#Spanisbn").append("<li>" + s + "-" + u + "</li>")
            }
            if (i == "OUT OF STOCK") {
                o = e[0];
                u = $("#ipadtit" + e[0]).val();
                $("#OutOfStockIsbn").append("<li>" + o + "-" + u + "</li>")
            }
        })
    }
    $("#hdnselectedvalue").val(n);
    $("#hdnNonOutofStockSelectedValues").val(r);
    listparams = e + "," + t;
    isAddToListEntered = false;
    if (s != "") {
        ShowSaleRestriction()
    } else {
        if (isList) {
            AddingItemToListrestriction(n, r)
        }
    }
}

function AddingItemToListrestriction(e, t) {
    if (e != t) {
        Showoutofstockheadcart();
        return false
    } else {
        if (isList) {
            AddingItemToList(e)
        }
    }
}

function AddingItemToList(e) {
    var t, n;
    if (listparams != "") {
        var r = listparams.split(",");
        t = r[0];
        n = r[1]
    }
    e = e.substring(0, e.length - 1);
    e = e + ".," + n;
    $("#hdnselectedvalue").val(e);
    if (callcount != 0) {
        return false
    }
    callcount++;
    if (e == ".," + n) {
        return false
    }
    $(".k-window-actions.k-header").css("cursor", "pointer");
    if (t == "Create New List") {
        var i = $("#Popuocreatelistbutton");
        if ($("#UserIDHiddenField").val() == "0") {
            var i = $("#PopuocreatelistAnonbutton");
            if (!i.data("kendoWindow")) {
                i.kendoWindow({
                    width: "600px",
                    height: "440px",
                    modal: true,
                    draggable: false
                });
                i.data("kendoWindow").center()
            }
            i.data("kendoWindow").open();
            i.data("kendoWindow").center()
        } else {
            $(".k-window-actions.k-header").css("cursor", "pointer");
            var i = $("#Popuocreatelistbutton");
            if (!i.data("kendoWindow")) {
                i.kendoWindow({
                    width: "600px",
                    height: "380px",
                    modal: true,
                    draggable: false
                });
                i.data("kendoWindow").center()
            }
            i.data("kendoWindow").open();
            i.data("kendoWindow").center()
        }
        $(".popurerrordiv").removeClass("dnnFormSuccess dnnFormWarning").text("").hide();
        $("#txtcreatelistbox").removeClass("removeItalic").val("i.e. wish-list, to booklist next year, save for later");
        i.data("kendoWindow").open();
        i.data("kendoWindow").center();
        $(".k-icon.k-i-close").hide();
        $("a.k-window-action.k-link").mouseover(function () {
            $("a.k-window-action.k-link").parent().css("background-image", "url('./Portals/0/images/close.png') !important");
            return false
        });
        $("#CreateListLoginButton").click(function () {
            if ($("#CreateListAnonTextBox").val() == "i.e. wish-list, to booklist next year, save for later" || $("#CreateListAnonTextBox").val().trim() == "") {
                $("#errorDiv4").removeClass("dnnFormSuccess").addClass("dnnFormWarning").html("Please enter list name.   ").show();
                return false
            }
            var t = httpGet(GetFile("desktopmodules/List/Handlers/ListHandler.ashx?Action=AnonList&ListName=" + $("#CreateListAnonTextBox").val() + "&value=" + e));
            if (e.length > 0) {
                GAPushTrackEvent("Lists", "Click", "Add-to-list")
            }
            GAPushTrackEvent($("#CreateListAnonTextBox").val(), "Click", "Create-Lists");
            $("#CreateListAnonTextBox").val("i.e. wish-list, to booklist next year, save for later");
            i.data("kendoWindow").close();
            OpenLoginWindowFromList();
            return false
        });
        $("#CreateListSignUpButton").click(function () {
            if ($("#CreateListAnonTextBox").val() == "i.e. wish-list, to booklist next year, save for later" || $("#CreateListAnonTextBox").val().trim() == "") {
                $("#errorDiv4").removeClass("dnnFormSuccess").addClass("dnnFormWarning").html("Please enter list name.   ").show();
                return false
            }
            var t = httpGet(GetFile("desktopmodules/List/Handlers/ListHandler.ashx?Action=AnonList&ListName=" + $("#CreateListAnonTextBox").val() + "&value=" + e));
            if (e.length > 0) {
                GAPushTrackEvent("Lists", "Click", "Add-to-list")
            }
            GAPushTrackEvent($("#CreateListAnonTextBox").val(), "Click", "Create-Lists");
            $("#CreateListAnonTextBox").val("i.e. wish-list, to booklist next year, save for later");
            i.data("kendoWindow").close();
            OpenWindowFromList();
            return false
        });
        $("#CancelCreateListButton").click(function () {
            i.data("kendoWindow").close();
            $("#txtcreatelistbox").val("i.e. wish-list, to booklist next year, save for later")
        })
    } else {
        if (t == "Shopping List" && $("#UserIDHiddenField")[0].value == "0") {
            var i = $("#Popuocreateuserbutton");
            if (!i.data("kendoWindow")) {
                i.kendoWindow({
                    width: "600px",
                    height: "270px",
                    modal: true,
                    draggable: false
                });
                i.data("kendoWindow").center()
            }
            i.data("kendoWindow").open();
            i.data("kendoWindow").center();
            $("#SignupButton").click(function () {
                i.data("kendoWindow").close();
                var t = GetHandler(GetFile("desktopmodules/List/Handlers/ListHandler.ashx?Action=AnonShoppingList&ListName=&value=" + e));
                OpenWindowFromList()
            });
            $("#LoginButton").click(function () {
                i.data("kendoWindow").close();
                var t = GetHandler(GetFile("desktopmodules/List/Handlers/ListHandler.ashx?Action=AnonShoppingList&ListName=&value=" + e));
                OpenLoginWindowFromList()
            })
        } else {
            var s = GetHandler(GetFile("desktopmodules/hesearchresults/Handlers/ListHandler.ashx?Action=LIST&ListName=&value=" + e + "&UserSk=" + $("#UserIDHiddenField").val()));
            if (s == "0") { } else {
                SuccessFlagForGA = true;
                $("#addedLabel").show();
                $("#addtolstdiv1").hide()
            }
            return false
        }
    }
    isAddToListEntered = true;
    isList = false;
    isokavail = false;
    listparams = ""
}

function kendoddlquoteclick() {
    if ($("#AddtoQuote-list ul.k-list li:first").text().toLowerCase() == "add to quote") {
        $("#AddtoQuote-list ul.k-list li:first").remove()
    }
}

function OnQuoteSelect(e) {
    if ($("#selqty")[0].innerHTML == 0) {
        $("#msglbl").css("display", "block");
        $("#MsgDiv").addClass("dnnFormWarning");
        $("#MsgDiv").text("Please select one or more items to add to the Quote.").show();
        $("#AddtoQuoteDiv select").data("kendoDropDownList").text("Add to quote");
        return false
    } else {
        var t = this.dataItem(e.item.index());
        selectquoteoperation(t.NAME, t.SK)
    }
}

function productpages() {
    $(".sepimg").css("height", "975px");
    $(".Noimg").css("display", "none");
    $(".ModCmsPageResultC").css("display", "block");
    $("#cmspagediv").css("display", "block");
    $("#searchfilterdiv").css("display", "none");
    $(".ModCmsPageResultC").css("float", "right");
    $(".showres").css("display", "block");
    $("#tab1").css("display", "none");
    $("#tab2").css("display", "block");
    $("#pagestab").css("width", "98%");
    $("#productstab").css("width", "20%");
    $("#productstab span").addClass("Padtopt");
    $("#pagestab span").css("padding-top", "4px");
    $("#pagestab").removeClass("tabPG");
    $("#pagestab").addClass("tabPR");
    $("#productstab").removeClass("tabPR");
    $("#productstab").addClass("tabPG");
    $("#cntDiv").addClass("cntDivss");
    $("#PagerHoldercms").css("margin-left", "0px");
    $(".Nrfrdiv").css("display", "none")
}

function QtyTxtchange(e, t) {
    eventflag = true;
    var n = parseInt($(e).val()) || 0;
    $("#addtolstdiv1 select").data("kendoDropDownList").text("Add to list");
    if ($("#addedLabel")[0].style.display == "block") {
        $("#addedLabel").css("display", "none");
        $("#addtolstdiv1").css("display", "block")
    }
    if ($("#QuoteAdded")[0].style.display == "block") {
        $("#QuoteAdded").css("display", "none");
        $("#AddtoQuoteDiv").css("display", "block")
    }
    if ($("#AddedToCart")[0].style.display == "block") {
        $("#AddedToCart").css("display", "none");
        $("#CartAdd").css("display", "block")
    }
    var r = $("#" + t).prev();
    var i = $("#" + t).next();
    if (n != 0) {
        if (r.checked != true) {
            r.value = true;
            r.checked = true;
            i.src = GetFile("/Portals/0/Images/check.PNG");
            count++;
            var s = parseInt($("#SelectedCount")[0].value);
            s = s + 1
        }
        s = count;
        $(".selSer").css("background-color", "#21B4E6");
        $(".selSer").css("color", "#FFFFFF");
        $("#SelectedCount")[0].value = s;
        $("#MsgDiv").hide();
        if (s == $("#searDet .sdDet .chbox_cs input[type=checkbox]").length) {
            $("#ImgSelect").attr("src", img);
            $("#selqty")[0].innerHTML = "All";
            $("#MainChk")[0].checked = true;
            $("#MsgDiv").hide()
        }
        i.attr("src", img);
        r.click();
        $("#" + t).addClass("ipadisbn");
        $("#selqty")[0].innerHTML = $(".ipadisbn").length;
        flag = false
    } else {
        $("#" + t).removeClass("ipadisbn");
        if (r.checked == true) {
            r.checked = false;
            r.value = false;
            i.src = GetFile("/Portals/0/Images/pagebk.PNG");
            count--
        }
        var s = parseInt($("#SelectedCount")[0].value);
        if (s == 0) {
            $(".selSer").css("background-color", "White");
            $(".selSer").css("color", "#707070")
        } else {
            s = s - 1
        }
        $("#SelectedCount")[0].value = s;
        $("#selqty")[0].innerHTML = $(".ipadisbn").length;
        i.attr("src", img1);
        $("#ImgSelect").attr("src", img1);
        r.checked = false;
        $("#MainChk")[0].checked = false;
        flag = true
    }
    var o = i.onclick;
    if (typeof o == "function") {
        o.call(i)
    }
}

function closesalerestrict() {
    if (isCart && isokavail) {
        AddToCartItemsrestriction($("#hdnselectedvalue").val(), $("#hdnNonOutofStockSelectedValues").val())
    } else {
        if (isCart) {
            isCart = false
        }
    } if (isList && isokavail) {
        AddingItemToListrestriction($("#hdnselectedvalue").val(), $("#hdnNonOutofStockSelectedValues").val())
    } else {
        if (isList) {
            isList = false
        }
    }
}

function ShowSaleRestriction() {
    Salerestrictionwindow = $("#OrderRestrictPopup");
    if (!Salerestrictionwindow.data("kendoWindow")) {
        Salerestrictionwindow.kendoWindow({
            width: "441px",
            height: "230px",
            modal: true,
            draggable: false,
            close: closesalerestrict
        });
        Salerestrictionwindow.data("kendoWindow").center()
    }
    Salerestrictionwindow.data("kendoWindow").open();
    Salerestrictionwindow.data("kendoWindow").center()
}

function ShowSaleRestrictionquote() {
    var e = $("#OrderRestrictPopupQuote");
    if (!e.data("kendoWindow")) {
        e.kendoWindow({
            width: "441px",
            height: "230px",
            modal: true,
            draggable: false
        });
        e.data("kendoWindow").center()
    }
    e.data("kendoWindow").open();
    e.data("kendoWindow").center();
    $("#OKQuote").click(function () {
        e.data("kendoWindow").close();
        $("#Spanisbn").text("")
    })
}

function Showoutofstockcart() {
    Outofstocksinglewindow = $("#SoutofStockPopup");
    if (!Outofstocksinglewindow.data("kendoWindow")) {
        Outofstocksinglewindow.kendoWindow({
            width: "441px",
            height: "230px",
            modal: true,
            draggable: false
        })
    }
    Outofstocksinglewindow.data("kendoWindow").open();
    Outofstocksinglewindow.data("kendoWindow").center()
}

function closeoutofstock() {
    if (isList && $("#OutOfStockIsbn").text() != "") {
        AddingItemToList($("#hdnNonOutofStockSelectedValues").val());
        $("#OutOfStockIsbn").text("")
    } else {
        if (isList) {
            AddingItemToList($("#hdnselectedvalue").val())
        }
    } if (isCart && $("#OutOfStockIsbn").text() != "") {
        AddToCartItems($("#hdnNonOutofStockSelectedValues").val(), true);
        $("#OutOfStockIsbn").text("")
    } else {
        if (isCart) {
            AddToCartItems($("#hdnselectedvalue").val(), false)
        }
    }
}

function Showoutofstockheadcart() {
    Outofstockwindow = $("#OutofstockPopup");
    if (!Outofstockwindow.data("kendoWindow")) {
        Outofstockwindow.kendoWindow({
            width: "441px",
            height: "230px",
            modal: true,
            draggable: false,
            close: closeoutofstock
        });
        Outofstockwindow.data("kendoWindow").center()
    }
    Outofstockwindow.data("kendoWindow").open();
    Outofstockwindow.data("kendoWindow").center()
}

function PostBack() {
    function n(e) {
        alert(e)
    }

    function r(e) {
        var t = e.which ? e.which : e.keyCode;
        if (!(t == 8 || t == 46) && (t < 48 || t > 57)) {
            return false
        }
    }

    function i(e) {
        var t = this.dataItem(e.item.index());
        $("#msglbl").css("display", "block");
        $("#MsgDiv").addClass("dnnFormSuccess");
        $("#MsgDiv").text("ITEMS ARE ADDED TO THE " + t.text.toUpperCase() + " SUCCESSFULLY.").show()
    }

    function o() {
        window.location.href = "productdetail.aspx"
    }

    function u() {
        window.scrollTo(0, 0)
    }
    $("#custommessage").live("click", function () {
        $(this).parent().css("display", "none")
    });
    var e = 0;
    for (var t = 0; t < $(".dsource_cs .chbox_cs input[type=checkbox]").length; t++) {
        if ($(".dsource_cs .chbox_cs input[type=checkbox]")[t].checked) {
            $(".dsource_cs .chbox_cs input[type=checkbox]")[t].parentNode.parentNode.childNodes[3].src = img;
            $(".dsource_cs .ThirdDiv_cs")[t].style.cssText = "font-weight: 700";
            $(".ktab1").css("float", "left")
        }
    }
    for (var t = 0; t < $("#searDet .sdDet .chkqtydiv .chbox_cs input[type=checkbox]").length; t++) {
        if ($("#searDet .sdDet .chkqtydiv .chbox_cs input[type=checkbox]")[t].checked) {
            $("#searDet .sdDet .chkqtydiv .chbox_cs input[type=checkbox]")[t].parentNode.children[1].src = img;
            if ($("#SelectedCount").val() != "") {
                e = parseInt($("#SelectedCount")[0].value);
                $("#SelectedCount")[0].value = e + 1
            } else {
                $("#SelectedCount")[0].value = e + 1
            }
        }
    }
    $("#selqty")[0].innerHTML = $(".ipadisbn").length;
    $("#Recommended").kendoDropDownList({
        animation: false
    });
    $(".detailspan").click(function () {
        var e = $(this).parent().parent().parent().parent().next().find("img")[0];
        var t = $(this).parent().parent().parent().parent().next().find("b")[0];
        var n = $(this).parent().parent().parent().parent().next().find("b")[1];
        var r = $(this).parent().parent().parent().parent().next().find("p")[1];
        var i = $(this).parent().parent().parent().parent().next().find("a")[0];
        if ($(this).parent().parent().parent().parent().next().hasClass("active")) {
            $(this).parent().parent().parent().parent().next().slideToggle();
            $(this).parent().parent().parent().parent().next().removeClass("active");
            $(this)[0].innerHTML = " MORE";
            $(this)[0].style.cssText = "padding-left: 38px; padding-right:38px;";
            e.style.display = "none"
        } else {
            $(this).parent().parent().parent().parent().next().slideToggle();
            $(this).parent().parent().parent().parent().next().addClass("active");
            $(this)[0].innerHTML = " LESS";
            $(this)[0].style.cssText = "padding-left: 41px; padding-right:41px;";
            var s = $(this).prev().text();
            if ($("#ismoreopened" + s).val() == "N") {
                e.style.display = "block";
                $.ajax({
                    type: "POST",
                    url: GetFile("desktopmodules/hesearchresults/Handlers/ListHandler.ashx?Value=&Action=REFINESEARCHPRODUCTATTR&productsk=" + s + "&division=" + GetQueryStringParams("division")),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (r) {
                        if (r != "N") {
                            var o = r.length;
                            $("#ismoreopened" + s).val("Y");
                            var u = "";
                            if ($("#StudentAttributes" + s)[0].innerHTML != undefined) {
                                u = $("#StudentAttributes" + s)[0].innerHTML
                            }
                            var a = "";
                            if ($("#ProductAttributes" + s)[0].innerHTML != undefined) {
                                a = $("#ProductAttributes" + s)[0].innerHTML
                            }
                            $.each(r, function (e, t) {
                                if (t.ATT_GROUP == "STUDENT") {
                                    u += "<div>" + t.ATTRIBUTE_NAME + ": ";
                                    var n = t.ATTRIBUTE_TYPE_VALUE.split(",");
                                    var r = t.ATTRIBUTE_TYPE_value_SK.split(",");
                                    for (var e = 0; e < r.length; e++) {
                                        if (r.length > 1) {
                                            if (e < r.length - 1) {
                                                u += '<a style=color:#707070!important href="/search?q=&division=' + GetQueryStringParams("division") + "&f=" + t.ATTRIBUTE_TYPE_SK + "_" + r[e] + '">' + n[e] + "</a>, "
                                            }
                                            if (e == r.length - 1) {
                                                u += 'and <a style=color:#707070!important href="/search?q=&division=' + GetQueryStringParams("division") + "&f=" + t.ATTRIBUTE_TYPE_SK + "_" + r[e] + '">' + n[e] + "</a>"
                                            }
                                        } else {
                                            u += '<a style=color:#707070!important href="/search?q=&division=' + GetQueryStringParams("division") + "&f=" + t.ATTRIBUTE_TYPE_SK + "_" + r[e] + '">' + n[e] + "</a>, "
                                        }
                                    }
                                    u = u.trim().replace(/^,|,$/g, "");
                                    u = u.replace(", and", " and");
                                    u += " | </div>"
                                }
                                $("#StudentAttributes" + s)[0].innerHTML = u.substring(0, u.length - 9) + "</div>";
                                if (t.ATT_GROUP == "PRODUCT") {
                                    a += "<div>" + t.ATTRIBUTE_NAME + ": ";
                                    var n = t.ATTRIBUTE_TYPE_VALUE.split(",");
                                    var r = t.ATTRIBUTE_TYPE_value_SK.split(",");
                                    for (var e = 0; e < r.length; e++) {
                                        if (r.length > 1) {
                                            if (e < r.length - 1) {
                                                a += '<a style=color:#707070!important href="/search?q=&division=' + GetQueryStringParams("division") + "&f=" + t.ATTRIBUTE_TYPE_SK + "_" + r[e] + '">' + n[e] + "</a>, "
                                            }
                                            if (e == r.length - 1) {
                                                a += 'and <a style=color:#707070!important href="/search?q=&division=' + GetQueryStringParams("division") + "&f=" + t.ATTRIBUTE_TYPE_SK + "_" + r[e] + '">' + n[e] + "</a>"
                                            }
                                        } else {
                                            a += '<a style=color:#707070!important href="/search?q=&division=' + GetQueryStringParams("division") + "&f=" + t.ATTRIBUTE_TYPE_SK + "_" + r[e] + '">' + n[e] + "</a>, "
                                        }
                                    }
                                    a = a.trim().replace(/^,|,$/g, "");
                                    a = a.replace(", and", " and");
                                    a += " | </div>"
                                }
                                $("#ProductAttributes" + s)[0].innerHTML = a.substring(0, a.length - 9) + "</div>"
                            })
                        }
                        if (u == undefined || u.trim() == "") {
                            t.style.display = "none"
                        } else {
                            t.style.display = "block"
                        } if (a == undefined || a.trim() == "") {
                            n.style.display = "none"
                        } else {
                            n.style.display = "block"
                        } if (t.style.display == "none" && n.style.display == "block") {
                            n.style.display = "block";
                            n.style.cssText = "margin-top: -45px;display: block;";
                            i.style.cssText = "margin-top: -40px;"
                        }
                        if (t.style.display == "none" && n.style.display == "none") {
                            i.style.cssText = "margin-top: -45px;"
                        }
                        e.style.display = "none"
                    }
                })
            }
        }
    });
    if ($.browser.msie) { }
    $("#productstab span").addClass("Padtop");
    var s;
    $("#pagestab").live("click", function () {
        var e = $("#PagesCount")[0].innerHTML;
        if (GetQueryStringParams("cms") != undefined) {
            $(".sepimg").css("height", "975px");
            $("#contdiv").css("display", "none");
            $("#searchfilterdiv").css("display", "none");
            $(".ModCmsPageResultC").css("display", "block");
            $("#cmspagediv").css("display", "block");
            $(".showres").css("display", "block");
            $("#tab1").css("display", "none");
            $("#tab2").css("display", "block");
            $("#pagestab").css("width", "98%");
            $("#productstab").css("width", "20%");
            $("#productstab span").addClass("Padtopt");
            $("#pagestab span").css("padding-top", "4px");
            $("#pagestab").removeClass("tabPG");
            $("#pagestab").addClass("tabPR");
            $("#productstab").removeClass("tabPR");
            $("#productstab").addClass("tabPG");
            $("#cntDiv").addClass("cntDivss");
            $(".Nrfrdiv").css("display", "none");
            $(".ktab").css("margin-top", "0px");
            $("#tabstrip1").css("height", "auto")
        } else {
            $(".sepimg").css("height", "975px");
            $("#contdiv").css("display", "none");
            $("#searchfilterdiv").css("display", "none");
            $(".ModCmsPageResultC").css("display", "block");
            $("#cmspagediv").css("display", "block");
            $(".showres").css("display", "block");
            $("#tab1").css("display", "none");
            $("#tab2").css("display", "block");
            $("#pagestab").css("width", "98%");
            $("#productstab").css("width", "20%");
            $("#productstab span").addClass("Padtopt");
            $("#pagestab span").css("padding-top", "4px");
            $("#pagestab").removeClass("tabPG");
            $("#pagestab").addClass("tabPR");
            $("#productstab").removeClass("tabPR");
            $("#productstab").addClass("tabPG");
            $("#cntDiv").addClass("cntDivss");
            $(".Nrfrdiv").css("display", "none");
            $(".ktab").css("margin-top", "0px");
            $("#tabstrip1").css("height", "auto");
            $("#cmspagediv").css("display", "none");
            $("#StudentPagerDivcms").css("display", "none");
            if (s == "true") {
                if (e == 0) {
                    $("#StudentPagerDivcms").css("display", "none")
                } else {
                    if (e > 20) {
                        $("#StudentPagerDivcms").css("display", "block")
                    }
                }
            }
            var t = GetQueryStringParams("q");
            var n = GetQueryStringParams("division");
            var r = $("#cmsview");
            if (s != "true") {
                s = "true";
                $("#Loaderimg").css("display", "block");
                $.ajax({
                    type: "POST",
                    url: GetFile("desktopmodules/hesearchresults/handlers/CmsResult.ashx?q=" + t + "&division=" + n),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (t) {
                        var n = "";
                        $.each(t, function (e, t) {
                            n += "<div class='rowstylev'>";
                            n += "<div class='cmsrevfdivv'>";
                            n += "<a class='PageTitlev' href=" + t.Exacturl + ">" + t.TABNAME + "";
                            n += "</a>";
                            n += "</div>";
                            n += "<div class='cmsrevsdivv'>";
                            n += "<span>" + t.DESCRIPTION + "";
                            n += "</span>";
                            n += "</div>";
                            n += "<div class='cmsrevtdivv'>";
                            n += "<a class='PageLinkv' href=" + t.Exacturl + ">" + t.Exacturl + "";
                            n += "</a>";
                            n += "</div>";
                            n += "</div>";
                            r.append(n);
                            n = ""
                        });
                        $(".cmsresdiv").css("width", "80px");
                        $(".cmsrefrdiv").css("width", "80px");
                        $("#PagerHoldercms").css("margin-left", "-145px");
                        $("#Loaderimg").css("display", "none");
                        if (e == 0) {
                            $("#StudentPagerDivcms").css("display", "none")
                        } else {
                            if (e > 20) {
                                $("#StudentPagerDivcms").css("display", "block")
                            }
                        }
                    }
                })
            }
        }
    });
    $("#productstab").live("click", function () {
        if ($("#PrdCount")[0].innerHTML == 0) {
            $("#searAct").css("display", "none");
            $(".Nrfrdiv").css("display", "block");
            $(".ktab").css("margin-top", "-320px");
            $(".Nrfrdiv").css("margin-top", "80px");
            $(".Nrfrdiv").css("margin-left", "225px");
            $("#tabstrip1").css("height", "63px");
            $("#Img1").css("display", "none");
            $("#Div3").css("display", "none")
        }
        $(".sepimg").css("height", "930px");
        $(".ModCmsPageResultC").css("display", "none");
        $("#cmspagediv").css("display", "none");
        $(".showres").css("display", "none");
        $("#productstab").removeClass("tabPG");
        $("#productstab").addClass("tabPR");
        $("#pagestab").removeClass("tabPR");
        $(".ktab").css("position", "relative");
        $(".ktab").css("margin-left", "0px");
        $("#pagestab").addClass("tabPG");
        $("#productstab span").addClass("Padtop");
        $("#pagestab span").css("padding-top", "2px");
        $("#searchfilterdiv").css("display", "block");
        $("#tab1").css("display", "block");
        $("#tab2").css("display", "none")
    });
    $(".stdrepeater").live("click", function () {
        if ($(this).find("div input:button")[1].value != "ADDED TO CART") {
            $(this).find("div input:button")[1].value = "ADDED TO CART";
            $(this).find("div input:button").css("cursor", "default");
            $(this).find("div input:button")[1].style.cssText = "color:white !important;cursor:default;margin-top: 5px;";
            $(this).find("div img").addClass("displaynone");
            $(this).removeClass("AddtoCart");
            $(this).removeClass("stdrepeater");
            $(this).addClass("Added");
            $(this).addClass("addedstd")
        }
        return false
    })
}

function calculateTotal(e, t, n, r, i, s) {
    var o = parseInt($(e).val()) || 0;
    if (o != 0) {
        $(e).parent().children().first().val(o);
        $("#" + t).val("");
        $("#" + t).addClass("ipadisbn");
        $("#" + t).val(t + "-" + o + "-" + n + "-" + r + "-" + i + "-" + s)
    } else {
        $("#" + t).removeClass("ipadisbn")
    }
}

function Onchanged() {
    if ($("#selqty")[0].innerHTML == 0) {
        $("#msglbl").css("display", "block");
        $("#MsgDiv").addClass("dnnFormWarning");
        $("#MsgDiv").text("Please select one or more items to add to the List.").show();
        $("#addtolstdiv1 select").data("kendoDropDownList").text("Add to list");
        return false
    } else {
        $("#addtolstdiv1 select").data("kendoDropDownList").text("Add to list");
        $("#msglbl").css("display", "none");
        return true
    }
}

function OpenWindowFromList() {
    //var e = $("#window"),
    //t = $("#signuplnk").bind("click", function () {
    //e.data("kendoWindow").open()
    //});
    //$("#signuplnk").click(function (t) {
    window.open("/signup.aspx", "_parent");
    // e.data("kendoWindow").open();
    //e.data("kendoWindow").center()
    //});
    //onClose = function () {
    //   t.show()
    //};
    /*if (!e.data("kendoWindow")) {
    e.kendoWindow({
    width: "500px",
    height: "993px",
    content: GetFile("/signup.aspx"),
    close: onClose,
    modal: true,
    draggable: false
    });
    e.data("kendoWindow").center();
    $(".k-icon.k-i-close").css("display", "none");
    $("a.k-window-action.k-link").mouseover(function () {
    $("a.k-window-action.k-link").parent().css("background-image", "url('./Portals/0/images/close.png') !important");
    return false
    })
    }*/
}

function OpenLoginWindowFromList() {
    var e = $("#loginWindow");
    e.kendoWindow({
        width: "491px",
        height: "317px",
        content: GetFile("/signin.aspx"),
        modal: true,
        draggable: true,
        resizable: true
    });
    e.data("kendoWindow").center();
    e.data("kendoWindow").open();
    e.data("kendoWindow").center();
    if ($.browser.msie) {
        $("#loginWindow").css({
            height: "300px",
            "margin-left": "-19px",
            width: "509px"
        });
        $("#loginWindow").parent().css("height", "317px")
    } else {
        if (navigator.userAgent.match(/AppleWebKit/) && !navigator.userAgent.match(/Chrome/)) {
            $("#loginWindow").css({
                height: "300px",
                "margin-left": "-19px",
                width: "507px"
            });
            $("#loginWindow").parent().css("height", "317px")
        } else {
            $("#loginWindow").css({
                height: "300px",
                "margin-left": "-19px",
                width: "509px"
            });
            $("#loginWindow").parent().css("height", "317px")
        }
    }
    $("a.k-window-action.k-link").mouseover(function () {
        $("a.k-window-action.k-link").parent().css("background-image", "url('./Portals/0/images/close.png') !important");
        return false
    })
}

function CreateShoppingList(e) {
    if ($("#UserIDHiddenField").val() == "0") {
        var t = $("#Popuocreateuserbutton");
        if (!t.data("kendoWindow")) {
            t.kendoWindow({
                width: "600px",
                height: "270px",
                modal: true,
                draggable: false
            });
            t.data("kendoWindow").center()
        }
        t.data("kendoWindow").open();
        t.data("kendoWindow").center()
    } else {
        $(".k-window-actions.k-header").css("cursor", "pointer");
        var t = $("#Popuocreatelistbutton");
        if (!t.data("kendoWindow")) {
            t.kendoWindow({
                width: "600px",
                height: "380px",
                modal: true,
                draggable: false
            });
            t.data("kendoWindow").center()
        }
        $(".popurerrordiv").removeClass("dnnFormSuccess dnnFormWarning").text("").hide();
        $("#txtcreatelistbox").removeClass("removeItalic").val("i.e. wish-list, to booklist next year, save for later");
        t.data("kendoWindow").open();
        t.data("kendoWindow").center();
        $(".k-icon.k-i-close").hide();
        $("a.k-window-action.k-link").mouseover(function () {
            $("a.k-window-action.k-link").parent().css("background-image", "url('./Portals/0/images/close.png') !important");
            return false
        });
        $("#CancelCreateListButton").click(function () {
            t.data("kendoWindow").close();
            $("#txtcreatelistbox").val("i.e. wish-list, to booklist next year, save for later")
        });
        $("#CreateListButton").click(function () {
            if ($("#txtcreatelistbox").val() == "i.e. wish-list, to booklist next year, save for later") {
                $(".popurerrordiv").removeClass("dnnFormSuccess").addClass("dnnFormWarning").html("Please enter list name.   ").show();
                return false
            } else {
                if ($("#txtcreatelistbox").val().trim().length == 0) {
                    $(".popurerrordiv").removeClass("dnnFormSuccess").addClass("dnnFormWarning").html("Please enter list name.   ").show();
                    return false
                } else {
                    var n = GetHandler(GetFile("desktopmodules/hesearchresults/Handlers/ListHandler.ashx?Value=&Action=&ProcessType=CreateList&ListName=" + $("#txtcreatelistbox").val() + "&ProductQty=" + e + "&UserSk=" + $("#UserIDHiddenField").val()));
                    var r = n.substring(1, n.length - 1);
                    if (r == "0") {
                        $(".popurerrordiv").removeClass("dnnFormSuccess").addClass("dnnFormWarning").text("List Name already exist").show()
                    } else {
                        t.data("kendoWindow").close();
                        $("#addedLabel").show();
                        $("#addtolstdiv1").hide()
                    }
                    return false
                }
            }
        })
    }
}

function httpGet(e) {
    var t = null;
    t = new XMLHttpRequest;
    t.open("GET", e, false);
    t.send(null);
    return t.responseText
}

function OnUserSelect(e) {
    var t = this.dataItem(e.item.index()).REGISTERED_BUSINESS_NAME;
    var n = this.dataItem(e.item.index()).TRADING_PARTNER_ACCOUNT_SK;
    $("#hdnAccountSK").val(n);
    accountname = t.split("/")[0]
}

function ShowQuotePopup() {
    $(".k-window-actions.k-header").css("cursor", "pointer");
    var e = $("#QuotePopup");
    if (!e.data("kendoWindow")) {
        e.kendoWindow({
            width: "600px",
            height: "380px",
            modal: true,
            draggable: false
        });
        e.data("kendoWindow").center();
        $(".popurerrordiv").removeClass("dnnFormSuccess dnnFormWarning").text("").hide();
        e.data("kendoWindow").open();
        e.data("kendoWindow").center();
        $("#QuotePopup").parent().appendTo("form");
        $(".k-icon.k-i-close").hide();
        $("a.k-window-action.k-link").mouseover(function () {
            $("a.k-window-action.k-link").parent().css("background-image", "url('./Portals/0/images/close.png') !important");
            return false
        })
    }
}

function GetSelectedISBN() {
    isCart = true;
    isList = false;
    isQuote = false;
    var e = "";
    var t = "";
    var n = $(".ipadisbn").length;
    isAddToCartEntered = false;
    var r = "";
    var i = "";
    var s = "";
    $("#Spanisbn").text(" ");
    $("#OutOfStockIsbn").text(" ");
    if (n != 0) {
        $.each($(".ipadisbn"), function () {
            var n = $(this).val().split("-");
            var o = n[2];
            var u = n[3];
            if (o == "Y") {
                e = n[0] + "|" + n[1] + "-" + e;
                if (u != "OUT OF STOCK") {
                    t = n[0] + "|" + n[1] + "-" + t
                }
            }
            if (o == "N") {
                r = n[0];
                s = $("#ipadtit" + n[0]).val();
                $("#Spanisbn").append("<li>" + r + "-" + s + "</li>")
            }
            if (u == "OUT OF STOCK") {
                i = n[0];
                s = $("#ipadtit" + n[0]).val();
                $("#OutOfStockIsbn").append("<li>" + i + "-" + s + "</li>")
            }
        });
        $("#hdnselectedvalue").val(e);
        $("#hdnNonOutofStockSelectedValues").val(t);
        if (r != "") {
            ShowSaleRestriction()
        } else {
            if (isCart) {
                AddToCartItemsrestriction(e, t)
            }
        }
    } else {
        $("#MsgDiv").removeClass("dnnFormSuccess");
        $("#MsgDiv").addClass("dnnFormWarning");
        $("#MsgDiv").html("Please select one or more items to add to the Cart.").show();
        return false
    }
}

function AddToCartItemsrestriction(e, t) {
    if (e != t) {
        Showoutofstockheadcart()
    } else {
        if (isCart) {
            AddToCartItems(e, false)
        }
    }
}

function AddToCartItems(e, t) {
    e = e.substring(0, e.length - 1);
    var n = "CART";
    var r = e;
    var i = GetHandler(GetFile("desktopmodules/List/Handlers/ListHandler.ashx?Value=" + r + "&Action=" + n));
    if ($("#Cart")[0] != undefined) {
        if (i == 0) {
            $("#Cart")[0].innerText = "";
            $("#Cart")[0].innerHTML = ""
        } else {
            $("#Cart")[0].innerText = i;
            $("#Cart")[0].innerHTML = i
        }
    }
    $.each($(".ipadisbn"), function () {
        var e = $(this).val().split("-");
        var n = e[2];
        var r = e[3];
        if (r == "OUT OF STOCK" & t) { } else {
            if (n == "Y") {
                $(".added" + e[0]).show();
                $(".add" + e[0]).hide();
                $("#CartAdd").hide();
                $("#AddedToCart").show()
            }
        }
    });
    isAddToCartEntered = true;
    isCart = false;
    isokavail = false
}

function selectquoteoperation(e, t) {
    isCart = false;
    isList = false;
    isQuote = true;
    callcount = 0;
    var n = "";
    var r = "";
    isAddToQuoteEntered = false;
    var i = $(".ipadisbn").length;
    var s = "";
    var o = "";
    var u = "";
    if (i != 0) {
        $("#Spanisbn").text(" ");
        $("#OutOfStockIsbn").text(" ");
        $.each($(".ipadisbn"), function () {
            var e = $(this).val().split("-");
            var t = e[2];
            var i = e[5];
            var a = e[3];
            if (t == "Y") {
                n = e[0] + "-" + e[4] + "-" + e[1] + "," + n;
                if (a != "OUT OF STOCK") {
                    r = e[0] + "-" + e[4] + "-" + e[1] + "," + r
                }
            }
            if (t == "N" || i == "Y") {
                s = e[0];
                u = $("#ipadtit" + e[0]).val();
                $("#Spanisbn").text($("#Spanisbn").text() + s + "-" + u + "\n\n")
            }
            if (a == "OUT OF STOCK") {
                o = e[0];
                u = $("#ipadtit" + e[0]).val();
                $("#OutOfStockIsbn").append("<li>" + o + "-" + u + "</li>")
            }
        })
    }
    if (s != "") {
        var a = $("#OrderRestrictPopup");
        a.kendoWindow({
            width: "441px",
            height: "230px",
            modal: true,
            draggable: false,
            close: f
        });
        a.data("kendoWindow").center();
        a.data("kendoWindow").open();
        a.data("kendoWindow").center();
        $("#Ok").click(function () {
            isokavail = true;
            a.data("kendoWindow").close();
            $("#Spanisbn").text("")
        });

        function f() {
            if (isQuote && isokavail) {
                AddToquoteItemsrestriction(e, t, n, r, o)
            }
        }
    } else {
        AddToquoteItemsrestriction(e, t, n, r, o)
    }
}

function AddToquoteItemsrestriction(e, t, n, r, i) {
    if (i != "") {
        var s = $("#OutofstockPopup");
        if (!s.data("kendoWindow")) {
            s.kendoWindow({
                width: "441px",
                height: "230px",
                modal: true,
                draggable: false,
                close: o
            });
            s.data("kendoWindow").center()
        }
        s.data("kendoWindow").open();
        s.data("kendoWindow").center();
        $("#OkStock").click(function () {
            $("#OutOfStockIsbn").text("");
            if (isQuote) {
                AddToQuoteItems(e, t, n)
            }
            s.data("kendoWindow").close()
        });

        function o() {
            if (isQuote && $("#OutOfStockIsbn").text() != "") {
                AddToQuoteItems(e, t, r)
            }
        }
    } else {
        if (isQuote) {
            AddToQuoteItems(e, t, n)
        }
    }
}

function AddToQuoteItems(e, t, n) {
    n = n.substring(0, n.length - 1);
    $("#hdnselectedvalue").val(n);
    $(".k-window-actions.k-header").css("cursor", "pointer");
    if (e == "Create Quote") {
        var r = $("#QuotePopup");
        $(".k-window-actions.k-header").css("cursor", "pointer");
        if (!r.data("kendoWindow")) {
            r.kendoWindow({
                width: "600px",
                height: "380px",
                modal: true,
                draggable: false
            });
            r.data("kendoWindow").center()
        }
        $("#txtQuoteName").val("");
        $("#hdnAccountSK").val("");
        $("#txtAccountName").val("");
        $("#OuterWarninCreate").hide();
        $(".popurerrordiv").removeClass("dnnFormSuccess dnnFormWarning").text("").hide();
        r.data("kendoWindow").open();
        r.data("kendoWindow").center();
        $(".k-icon.k-i-close").hide();
        $("a.k-window-action.k-link").mouseover(function () {
            $("a.k-window-action.k-link").parent().css("background-image", "url('./Portals/0/images/close.png') !important");
            return false
        })
    } else {
        var i = GetHandler(GetFile("desktopmodules/hesearchresults/Handlers/ListHandler.ashx?Action=ADDQUOTE&listquotesk=" + t.split("|")[0] + "&AccountSK=" + $("#hdnAccountSK").val() + "&value=" + n));
        $("#QuoteAdded").show();
        $("#AddtoQuoteDiv").hide();
        return false
    }
    isAddToQuoteEntered = true;
    isQuote = false;
    isokavail = false
}

function GetHandler(e) {
    var t = null;
    t = new XMLHttpRequest;
    t.open("GET", e, false);
    t.send(null);
    return t.responseText
}

function onclosestock() {
    outofstockselectedvalue = "";
    idobject = undefined
}

function GetIndividualISBN(e, t, n, r) {
    var i = "";
    var s = $("#selqty")[0].innerHTML;
    var o = "";
    var u = "";
    u = n;
    if (u == "N") {
        ShowAllowrestrictionpop()
    } else {
        o = r;
        if ($(e).parents().eq(3).find(".QtyTxt").val() == 0) {
            outofstockselectedvalue = t + "|" + 1
        } else {
            outofstockselectedvalue = t + "|" + $(e).parents().eq(3).find(".QtyTxt").val()
        }
        idobject = e;
        if (o == "OUT OF STOCK") {
            outofStockwindow.data("kendoWindow").open();
            outofStockwindow.data("kendoWindow").center()
        } else {
            var a = "CART";
            var f = outofstockselectedvalue;
            var l = GetHandler(GetFile("desktopmodules/List/Handlers/ListHandler.ashx?Value=" + f + "&Action=" + a));
            if ($("#Cart")[0] != undefined) {
                if (l == 0) {
                    $("#Cart")[0].innerText = "";
                    $("#Cart")[0].innerHTML = ""
                } else {
                    $("#Cart")[0].innerText = l;
                    $("#Cart")[0].innerHTML = l
                }
            }
            $(e).hide();
            $(e).prev().show();
            outofstockselectedvalue = ""
        }
    }
}

function Showoutofstock() {
    var e = $("#SoutofStockPopup");
    if (!e.data("kendoWindow")) {
        e.kendoWindow({
            width: "441px",
            height: "230px",
            modal: true,
            draggable: false
        })
    }
    e.data("kendoWindow").open();
    e.data("kendoWindow").center();
    $("#SitemcOk").click(function () {
        e.data("kendoWindow").close()
    })
}

function ShowAllowrestrictionpop() {
    var e = $("#AllowsalePopups");
    if (!e.data("kendoWindow")) {
        e.kendoWindow({
            width: "441px",
            height: "230px",
            modal: true,
            draggable: false
        });
        e.data("kendoWindow").center()
    }
    e.data("kendoWindow").open();
    e.data("kendoWindow").center();
    $("#SitemcOks").click(function () {
        e.data("kendoWindow").close()
    })
}

function Clientclick(e) {
    $("#hiddensearch").val($(e)[0].innerText);
    $("#searchClickbutton").click()
}
$(document).ready(function () {
    $(".intxt").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
    $("#addtolstdiv1").css("display", "block");
    $("#Div3").css("display", "block");
    $("#CartAdd").css("display", "block");
    if (GetQueryStringParams("cp") != undefined || GetQueryStringParams("cms") != undefined) {
        productpages()
    }
    if (GetQueryStringParams("searchtype") == "advisbn") {
        $("#pagestab").css("display", "none");
    }
    ShowfacetRelation();
    if ($("#addedLabel")[0].style.display == "block") {
        $("#addtolstdiv1").css("display", "none")
    }
    $("#Selectsort").kendoDropDownList({
        animation: false,
        select: OnSortSelect
    });
    var e = GetQueryStringParams("s");
    var t = $("#Selectsort").data("kendoDropDownList");
    if (e != "" && e != undefined) {
        switch (e.toLowerCase()) {
            case "l":
                $('#Selectsort option[value="L"]').prop("selected", true);
                t.value("L");
                break;
            case "a":
                $('#Selectsort option[value="A"]').prop("selected", true);
                t.value("A");
                break;
            case "d":
                $('#Selectsort option[value="D"]').prop("selected", true);
                t.value("D");
                break;
            default:
                $('#Selectsort option[value="R"]').prop("selected", true);
                t.value("R");
                break
        }
    }
    var n = GetFile("/Portals/0/Images/check.PNG");
    $(".LeftMenu span input:checked").each(function () {
        $(this).parent().parent().find(".unchkboxs_cs").attr("src", n)
    });
    var r = GetQueryStringParams("f");
    if (r != "" && r != undefined) {
        var i = [];
        i = r.split(":");
        var s = false;
        for (var o = 0; o < i.length; o++) {
            var u = i[o].split("_")[0];
            var a = i[o].split("_")[1];
            if (a == undefined) {
                a = "0"
            }
            var f = a.split(",");
            for (var l = 0; l < f.length; l++) {
                $(".LeftMenu span input").each(function () {
                    var e = $(this).parent().next()[0].value.split("_");

                    if (e[0] == u && e[1] == f[l]) {
                        $(this).parent().parent().find(".unchkboxs_cs").attr("src", n);
                        if ($(this).prop("checked") == false) {
                            $(this).click()
                        }
                        s = true
                    }
                    if (s) {
                        return false
                    }
                })
            }
        }
    }
});
var img = GetFile("/Portals/0/Images/check.PNG");
var img1 = GetFile("/Portals/0/Images/pagebk.PNG");
var SuccessFlagForGA = false;
var isAddToListEntered = false;
var listname = "";
var listname = "";
$(".ModCmsPageResultC").css("display", "none");
$(".showres").css("display", "none");
var eventflag = false;
$(document).ready(function () {
    function t() {
        if ($("#HeaderAddtoList-list ul.k-list li:first").text().toLowerCase() == "add to list") {
            $("#HeaderAddtoList-list ul.k-list li:first").remove()
        }
    }

    function s() {
        var e = [],
            t;
        var n = window.location.href.slice(window.location.href.indexOf("?") + 1).split("&");
        for (var r = 0; r < n.length; r++) {
            t = n[r].split("=");
            e.push(t[0]);
            e[t[0]] = t[1]
        }
        return e
    }

    function o() {
        $(".sepimg").css("height", "975px");
        $(".Noimg").css("display", "none");
        $("#contdiv").css("display", "none");
        $("#searchfilterdiv").css("display", "none");
        $(".showres").css("display", "block");
        $(".ModCmsPageResultC").css("display", "block");
        $("#cmspagediv").css("display", "block");
        $(".ModCmsPageResultC").css("float", "right");
        $("#tab1").css("display", "none");
        $("#tab2").css("display", "block");
        $("#pagestab").css("width", "98%");
        $("#productstab").css("width", "20%");
        $("#productstab span").addClass("Padtopt");
        $("#pagestab span").css("padding-top", "4px");
        $("#pagestab").removeClass("tabPG");
        $("#pagestab").addClass("tabPR");
        $("#productstab").removeClass("tabPR");
        $("#productstab").addClass("tabPG");
        $("#cntDiv").addClass("cntDivss");
        $("#PrdCount")[0].innerHTML = 0;
        $("#Div3").css("display", "none")
    }
    $("#CreateListButton").click(function () {
        if ($("#txtcreatelistbox").val() == "i.e. wish-list, to booklist next year, save for later") {
            $(".popurerrordiv").removeClass("dnnFormSuccess").addClass("dnnFormWarning").html("Please enter list name.   ").show();
            return false
        } else {
            if ($("#txtcreatelistbox").val().trim().length == 0) {
                $(".popurerrordiv").removeClass("dnnFormSuccess").addClass("dnnFormWarning").html("Please enter list name.   ").show();
                return false
            } else {
                listname = $("#txtcreatelistbox").val().trim();
                var e = GetHandler(GetFile("desktopmodules/hesearchresults/Handlers/ListHandler.ashx?Action=LIST&ListName=" + $("#txtcreatelistbox").val() + "&value=" + $("#hdnselectedvalue").val() + "&UserSk=" + $("#UserIDHiddenField").val()));
                if (parseInt(e) == 0) {
                    $(".popurerrordiv").removeClass("dnnFormSuccess").addClass("dnnFormWarning").text("List Name already exist").show();
                    return false
                } else {
                    if (e == "Invalid") {
                        $("txtcreatelistbox").val("i.e. wish-list, to booklist next year, save for later");
                        $("#Popuocreatelistbutton").data("kendoWindow").close()
                    } else {
                        GAPushTrackEvent("Lists", "Click", "Add-to-list");
                        GAPushTrackEvent($("#txtcreatelistbox").val(), "Click", "Create-Lists");
                        $("txtcreatelistbox").val("i.e. wish-list, to booklist next year, save for later");
                        $("#Popuocreatelistbutton").data("kendoWindow").close();
                        $("#addtolstdiv1 select").data("kendoDropDownList").dataSource.insert(0, {
                            NAME: $("#txtcreatelistbox").val(),
                            SK: e
                        });
                        $("#addtolstdiv1 select").data("kendoDropDownList").refresh();
                        if ($("#WishList")[0] != undefined) {
                            var t = $("#WishList")[0].innerText;
                            if (t == "") {
                                t = $("#WishList")[0].innerHTML
                            }
                            if (t == "") {
                                $("#WishList")[0].innerText = "(1)";
                                $("#WishList")[0].innerHTML = "(1)"
                            } else {
                                var n = t.indexOf(")");
                                var r = t.substring(1, n);
                                listcount = Number(r) + 1;
                                $("#WishList")[0].innetText = listcount;
                                $("#WishList")[0].innerHTML = listcount
                            }
                            $("#addedLabel").show();
                            $("#addtolstdiv1").hide()
                        }
                    }
                }
                return false
            }
        }
    });
    $("#btnCreate").click(function () {
        if ($("#txtQuoteName").val().trim().length == 0) {
            $("#OuterWarninCreate").show();
            $("#WarninCreate").html("Please enter Custom quote reference");
            return false
        } else {
            $.ajax({
                url: GetFile("desktopmodules/hesearchresults/Handlers/ListHandler.ashx?Action=CREATEQUOTE&QuoteName=" + $("#txtQuoteName").val() + "&AccountSK=" + $("#hdnAccountSK").val() + "&value=" + $("#hdnselectedvalue").val() + "&AccountNo=" + accountname),
                dataType: "json",
                success: function (e) {
                    if (e.indexOf("|") != -1) {
                        $("#AddtoQuoteDiv select").data("kendoDropDownList").dataSource.insert(0, {
                            NAME: $("#txtQuoteName").val(),
                            SK: e
                        });
                        $("#AddtoQuoteDiv select").data("kendoDropDownList").refresh();
                        $("#QuotePopup").data("kendoWindow").close();
                        $("#QuoteAdded").show();
                        $("#AddtoQuoteDiv").hide()
                    } else {
                        $("#OuterWarninCreate").show();
                        $("#WarninCreate").html(e)
                    }
                }
            });
            return false
        }
    });
    $("#AddtoQuoteDiv select").kendoDropDownList({
        animation: false,
        select: OnQuoteSelect,
        open: kendoddlquoteclick,
        cache: false,
        dataTextField: "NAME",
        dataValueField: "SK"
    });
    $("#AddtoQuoteDiv select").data("kendoDropDownList").text("Add to quote");
    var e = GetQueryStringParams("q");
    if (e != "" && e != undefined) {
        e = decodeURIComponent(e);
        $("#TextSearch").val(e);
        $("#TextSearchFooter").val(e)
    }
    $("#addtocartbtn").click(function () {
        if ($("#selqty")[0].innerHTML == 0) {
            $("#msglbl").css("display", "block");
            $("#MsgDiv").addClass("dnnFormWarning");
            $("#MsgDiv").text("Please select one or more items to add to the Cart.").show();
            return false
        } else {
            return true
        }
    });
    $("#addtolstdiv1 select").kendoDropDownList({
        animation: false,
        select: onselect,
        open: t,
        cache: false,
        dataTextField: "NAME",
        dataValueField: "SK"
    });
    $("#AddtoQuoteDiv select").live("change", function () {
        $("#AddtoQuoteDiv select").data("kendoDropDownList").text("Add to quote")
    });
    $("#addtolstdiv1 select").live("change", function () {
        $("#addtolstdiv1 select").data("kendoDropDownList").text("Add to list")
    });
    $("#addtolstdiv1 select").data("kendoDropDownList").text("Add to list");
    $("#AddtoQuoteDiv select").data("kendoDropDownList").text("Add to Quote");
    $(".repouter").live("each", function () {
        $(this).find(".left2 .sub44 .addtolstdiv select").kendoDropDownList();
        $(this).find(".left2 .sub44 .addtolstdiv select").data("kendoDropDownList").text("Add to list")
    });
    $(".center").live("click", function () {
        jQuery(".page1").removeClass("Highlight").addClass("removehighlight");
        jQuery(this).parent().removeClass("removehighlight").addClass("Highlight");
        jQuery(this).css("color", "whiite !important")
    });
    $("#txtAccountName").kendoAutoComplete({
        dataTextField: "REGISTERED_BUSINESS_NAME",
        filter: "contains",
        dataValueField: "TRADING_PARTNER_ACCOUNT_SK",
        select: OnUserSelect,
        dataSource: {
            transport: {
                read: {
                    type: "GET",
                    url: GetFile("/DesktopModules/Quote/Handlers/QuoteHandler.ashx?QuoteSK= &Value= &Action=ACCOUNT"),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }
            }
        }
    });
    $(".dnnFormValidationSummary").css("display", "none");
    var n = $("#cmsPagenum").val();
    if (n > 0) {
        $("#PagesCount")[0].innerHTML = n;
        $("#Hidpagecnt").val(n)
    } else {
        $("#PagesCount")[0].innerHTML = 0;
        $("#Hidpagecnt").val(0)
    }
    var r = $("#PagesCount")[0].innerHTML;
    var i = $("#PrdCount")[0].innerHTML;
    if (i == 0 & r > 0) {
        if (GetQueryStringParams("cms") != undefined) {
            $(".Nrfrdiv").css("display", "none");
            $("#searAct").css("display", "none");
            $("#searDet").css("display", "none");
            $("#PrdCount")[0].innerHTML = 0;
            $(".ktab").css("margin-top", "0px");
            $(".ktab").css("position", "none");
            $(".Nrfrdiv").css("margin-top", "80px");
            $(".Nrfrdiv").css("margin-left", "225px");
            $("#tabstrip1").css("height", "63px");
            $("#Img1").css("display", "none");
            $("#Div3").css("display", "none")
        } else {
            $(".Nrfrdiv").css("display", "block");
            $("#searAct").css("display", "none");
            $("#searDet").css("display", "none");
            $("#PrdCount")[0].innerHTML = 0;
            $(".ktab").css("position", "none");
            $(".ktab").css("margin-top", "-320px");
            $(".Nrfrdiv").css("margin-top", "80px");
            $(".Nrfrdiv").css("margin-left", "225px");
            $("#tabstrip1").css("height", "63px");
            $("#Img1").css("display", "none");
            $("#Div3").css("display", "none");
            $("#cmspagediv").css("display", "none")
        }
    }
    if (i > 0 & r == 0) {
        $("#pagestab").css("display", "none")
    }
    $(".lefttext").click(function () {
        $(this).prev().find(".chkbox").click()
    });
    PostBack();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
    $(".scrolldiv").mCustomScrollbar()

    if (GetQueryStringParams("division") != undefined) {
        switch (GetQueryStringParams("division").toLowerCase()) {
            case "primary":
                $('#dnn_CENGAGESUBMENU_PrimaryLink').addClass('current-menu-parent');
                break;
            case "secondary":
                $('#dnn_CENGAGESUBMENU_SecondaryLink').addClass('current-menu-parent');
                break;
            default:
                break
        }
    }
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
                    //	window.open(c.replaceAll('"', ""), "_parent")
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
});
var chkboxlength = $("#searDet")[0].childNodes.length;
var count = 0;
$("#selectall").live("click", function () {
    if ($("#addedLabel")[0].style.display == "block") {
        $("#addedLabel").css("display", "none");
        $("#addtolstdiv1").css("display", "block")
    }
    if ($("#QuoteAdded")[0].style.display == "block") {
        $("#QuoteAdded").css("display", "none");
        $("#AddtoQuoteDiv").css("display", "block")
    }
    if ($("#AddedToCart")[0].style.display == "block") {
        $("#AddedToCart").css("display", "none");
        $("#CartAdd").css("display", "block")
    }
    var e = $("#ImgSelect").attr("src");
    var t = e.split("/");
    $("#addtolstdiv1 select").data("kendoDropDownList").text("Add to list");
    if (t[t.length - 1].toLowerCase() == "pagebk.png") {
        count = $("#searDet .sdDet .chbox_cs input[type=checkbox]").length;
        $(".selSer").css("background-color", "#21B4E6");
        $(".selSer").css("color", "#FFFFFF");
        $("#ImgSelect").attr("src", img);
        $("#MainChk")[0].checked = true;
        var n = 0;
        for (var r = 0; r < $("#searDet .sdDet .chbox_cs input[type=checkbox]").length; r++) {
            $("#searDet .sdDet .chbox_cs input[type=checkbox]")[r].checked = true;
            $("#searDet .sdDet .chbox_cs img")[r].src = img;
            if ($("#searDet .sdDet input[type=text]")[r].value == 0) {
                $("#searDet .sdDet input[type=text]")[r].value = 1
            }
            n = n + 1
        }
        $("#SelectedCount")[0].value = n;
        $("#selqty")[0].innerHTML = "All";
        $("#MsgDiv").hide();
        $(".allisbn").addClass("ipadisbn")
    } else {
        $(".allisbn").removeClass("ipadisbn");
        count = 0;
        $(".selSer").css("background-color", "White");
        $(".selSer").css("color", "#707070");
        $("#selqty")[0].innerHTML = $(".ipadisbn").length;
        $("#ImgSelect").attr("src", img1);
        $("#MainChk")[0].checked = false;
        for (var r = 0; r < $("#searDet .sdDet .chbox_cs input[type=checkbox]").length; r++) {
            $("#searDet .sdDet .chbox_cs input[type=checkbox]")[r].checked = false;
            $("#searDet .sdDet .chbox_cs img")[r].src = img1;
            if ($("#searDet .sdDet input[type=text]")[r].value == 1) {
                $("#searDet .sdDet input[type=text]")[r].value = 0
            }
        }
        $("#SelectedCount")[0].value = 0;
        $("#selqty")[0].innerHTML = $(".ipadisbn").length
    }
});
var flag = true;
$("#searDet .sdDet .chkqtydiv .chbox_cs img").bind("click", function () {
    if (eventflag == true) {
        eventflag = false;
        return false
    } else {
        var e = 0;
        $("#addtolstdiv1 select").data("kendoDropDownList").text("Add to list");
        e = parseInt(this.parentNode.parentNode.children[1].children[0].children[0].children[0].value);
        var t = this.parentNode.parentNode.children[1].children[0].id;
        if ($("#addedLabel")[0].style.display == "block") {
            $("#addedLabel").css("display", "none");
            $("#addtolstdiv1").css("display", "block")
        }
        if ($("#QuoteAdded")[0].style.display == "block") {
            $("#QuoteAdded").css("display", "none");
            $("#AddtoQuoteDiv").css("display", "block")
        }
        if ($("#AddedToCart")[0].style.display == "block") {
            $("#AddedToCart").css("display", "none");
            $("#CartAdd").css("display", "block")
        }
        var n = this.src;
        var r = n.split("/");
        if (r[r.length - 1].toLowerCase() == "pagebk.png") {
            $(this).prev().addClass("ipadisbn");
            if ($("#" + t + " input[type=text]")[0].value == 0) {
                $("#" + t + " input[type=text]")[0].value = 0 + 1
            }
            count++;
            var i = parseInt($("#SelectedCount")[0].value);
            i = i + 1;
            $(".selSer").css("background-color", "#21B4E6");
            $(".selSer").css("color", "#FFFFFF");
            $("#SelectedCount")[0].value = i;
            $("#selqty")[0].innerHTML = $(".ipadisbn").length;
            $("#MsgDiv").hide();
            if (i == $("#searDet .sdDet .chbox_cs input[type=checkbox]").length) {
                $("#ImgSelect").attr("src", img);
                $("#selqty")[0].innerHTML = "All";
                $("#MainChk")[0].checked = true;
                $("#MsgDiv").hide()
            }
            this.src = img;
            this.parentNode.children[0].click();
            flag = false
        } else {
            $(this).prev().removeClass("ipadisbn");
            var s = $(this).prev().val().split("-");
            s[1] = "1";
            $(this).prev().val(s.join("-"));
            if ($("#" + t + " input[type=text]")[0].value == 1 || $("#" + t + " input[type=text]")[0].value > 1) {
                $("#" + t + " input[type=text]")[0].value = 0
            }
            count--;
            var i = parseInt($("#SelectedCount")[0].value);
            if (i == 0) {
                $(".selSer").css("background-color", "White");
                $(".selSer").css("color", "#707070")
            } else {
                i = i - 1
            }
            $("#SelectedCount")[0].value = i;
            $("#selqty")[0].innerHTML = $(".ipadisbn").length;
            this.src = img1;
            $("#ImgSelect").attr("src", img1);
            this.parentNode.children[0].checked = false;
            $("#MainChk")[0].checked = false;
            flag = true
        }
    }
});
var listparams = "";
var Salerestrictionwindow;
var Outofstocksinglewindow;
var Outofstockwindow;
$("#Ok").click(function () {
    isokavail = true;
    Salerestrictionwindow = $("#OrderRestrictPopup");
    Salerestrictionwindow.data("kendoWindow").close();
    $("#Spanisbn").text("")
});
$("#OkStock").click(function () {
    $("#OutOfStockIsbn").text("");
    $("#OutofstockPopup").data("kendoWindow").close()
});
$(".ellipsis").ellipsis();
var accountname = "";
var isCart = false;
var isList = false;
var isQuote = false;
var isAddToCartEntered = false;
var isokavail = false;
var isAddToQuoteEntered = false;
var callcount = 0;
var outofstockselectedvalue = "";
var idobject;
var outofStockwindow = $("#SoutofStockPopup");
if (!outofStockwindow.data("kendoWindow")) {
    outofStockwindow.kendoWindow({
        width: "441px",
        height: "230px",
        modal: true,
        draggable: false,
        close: onclosestock
    });
    outofStockwindow.data("kendoWindow").center()
}
$("#SitemcOk").click(function () {
    if (outofstockselectedvalue != "") {
        var e = "CART";
        var t = outofstockselectedvalue;
        var n = GetHandler(GetFile("desktopmodules/List/Handlers/ListHandler.ashx?Value=" + t + "&Action=" + e));
        if ($("#Cart")[0] != undefined) {
            if (n == 0) {
                $("#Cart")[0].innerText = "";
                $("#Cart")[0].innerHTML = ""
            } else {
                $("#Cart")[0].innerText = n;
                $("#Cart")[0].innerHTML = n
            }
        }
        $(window).scrollTop();
        if (idobject != undefined) {
            $(idobject).hide();
            $(idobject).parents().eq(0).find(".headeraddedline").show()
        }
    }
    $("#SoutofStockPopup").data("kendoWindow").close();
    outofstockselectedvalue = ""
});

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