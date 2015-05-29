function dlink() {
    $("#cmdSeeAll").addClass("shownull")
}
function OpenConfirmSavedSearchPopUp() {


    jQuery.fn.center = function () {
        this.css("position", "absolute");
        this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px");
        this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px");
        return this;
    }


    $("#Delete-message").dialog();
   
    //$('body').css({'background-color':'#000','opacity':'0.7'})
}
function blink() {
    $(".title-nav").addClass("title-nav1"), $(".rows").addClass("rows1")
}

function alink() {
    $(".home_search").addClass("shownull")
}

function clink() {
    $(".home_search").addClass("showall")
}

function DeleteSavedSearch(e) {
    var d = $(e).next().val();
    return $("#hndSavedSearchID").val(d), thisclick = $(e), $("#Delete-message").css({
        display: "block"
    }), $(".k-window-actions.k-header").css("cursor", "pointer"), dkwindowdel = $("#Delete-message"), dkwindowdel.data("kendoWindow") || (dkwindowdel.kendoWindow({
        width: "480px",
        height: "300px",
        modal: !0,
        draggable: !1
    }), dkwindowdel.data("kendoWindow").center()), dkwindowdel.data("kendoWindow").open(), $(".k-icon.k-i-close").hide(), $("a.k-window-action.k-link").mouseover(function() {
        return !1
    }), !1
}
	function DeleteSavedSearchYes() {
        dkwindowdel.data("kendoWindow").close();
        var savedsearchid1 = $('#hndSavedSearchID').val();
        var count="";
        if ($('#cmdSeeAll').val()=="See all" && $('#cmdSeeAll').css('display')=="block")
        {
             count = $('#homesearchdiv').next().next()[0].innerHTML;
        }
        $.ajax({
            url: GetFile('/DesktopModules/SavedSearch/Handlers/AlertDeleteHandler.ashx?savedsearchid=' + $('#tempHdn').val() + '&count=' + count + '&page=' + document.location.href.split('/')[3]),
            dataType: "json",
            success: function (value) {
                if (value != 0) {
				var total=parseInt($('#hndTotalSeeAll').val());
				$('#hndTotalSeeAll').val((total-1));
				if(parseInt($('#hndTotalSeeAll').val())<=parseInt(count))
				{
				dlink();
				}
				if($('#hndTotalSeeAll').val()=="0")
				{
				alink();
				$('.new-notable-nav').addClass("removeBorder");
				}
				$(thisclick).parent().parent().parent().remove();
				if ($('#cmdSeeAll').val()=="See all" && $('#hndTotalSeeAll').val()!="0")
        {
		var res=JSON.stringify(value);
		var val;
$.each(value, function (i, res) {
      // here you have access to
      //var DBSK = JSON.parse(res)[i].PROD_SAV_FAV_SK
	  val="0";
	  var DBSK = res.PROD_SAV_FAV_SK
	  $('.cross-icon').each(function()
	  {
	  
      if($(this).children().next().val()==DBSK)
	  {
	  val="1";
	  return false;
	  }
	  
	  });
if(val=="0" && DBSK != undefined)
      {
var html = '<div id="b1" class="rows">' +'<asp:Label Class="keyword3" >'+res.SEARCH_NAME+'</asp:Label>' +
'<label class="quotesLabel">- "</label>' +
'<label class="keyword2">'+res.KEYWORDS_STORED+'</label>' +
'</asp:Label><label class="quotesLabel">"</label>' +
'<div class="search-nav">' +
'<a id="cmdSearch" class="txt" href="'+res.URL_SEARCH+'">Search</a>' +
'<span class="ico-savedsearch" onclick="javascript:Navigateurl(this);">&nbsp;</span>' +
'<asp:HiddenField ID="urlHidden" runat="server" Value="'+res.URL_SEARCH+'" />' +
'<span id="level0" class="cross-icon" >' +
'<div ID="cmdDelete" class="icon-new icon-close-black" onclick="javascript:DeleteSavedSearch(this);"></div>' +
'<input type="hidden" name="idHidden" id="idHidden" value="'+res.PROD_SAV_FAV_SK+'" /> </span>' +
'</div>' +
'</div>';
//$(html).appendTo($("#SearchRepeater"));
//$(this).parent().find('#RatingMovie').prepend(img); 
$(html ).insertBefore( $('#newHdn') );
      }
      else
      {
        //ignore 
      }
});
		}
		else
		{
                    $(thisclick).parent().parent().parent().remove();
                    
					}
                }

            }
        });
        return false;
    }

function DeleteSavedSearchNo() {
    return dkwindowdel.data("kendoWindow").close(), !1
}

function Navigateurl(e) {
    var d = $(e).next().val();
    window.location = d
}
var dkwindowdel, thisclick;
$('#cmdDelete').live('click',function(){
debugger;
$('#tempHdn').val($(this).next().val());
});