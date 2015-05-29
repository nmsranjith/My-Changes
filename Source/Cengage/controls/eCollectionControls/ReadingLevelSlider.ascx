<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReadingLevelSlider.ascx.cs"
    Inherits="DotNetNuke.UI.eCollectionControls.ReadingLevelSlider" %>
<script src="<%=Page.ResolveUrl("Resources/Shared/scripts/jquery/jquery-ui-touch-punch.min.js")%>"
    type="text/javascript"></script>
<style type="text/css">
    .widgetheader
    {
        background: -moz-linear-gradient(center top , rgb(136, 134, 134) 0%, rgb(136, 134, 134) 55%, rgb(67, 68, 67) 130%) repeat scroll 0 0 transparent !important;
        background: -webkit-gradient(linear, center top, center bottom, color-stop(55%,rgb(136, 134, 134)), color-stop(100%,rgb(67, 68, 67))) !important;
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#636363', endColorstr='#707070', GradientType=0) !important;
        background: -ms-filter: "progid:DXImageTransform.Microsoft.gradient(startColorstr='rgb(136, 134, 134)', endColorstr='rgb(67, 68, 67)', GradientType=0)" !important;
        background: -ms-linear-gradient(top, rgb(136, 134, 134) 55%, rgb(67, 68, 67) 130%) !important;
        border-left: 1px solid gray !important;
    }
    .ui-widget-header
    {
        background: none !important;
        color: White !important;
    }
	
</style>
<link href="<%=Page.ResolveUrl("DesktopModules/eCollection_Students/CSS/jQuery.ui.smoothness.css")%>"
    rel="Stylesheet" type="text/css" />
<div class="Sliderholder">
    <p style="display: none">
        <label for="amount">
            Price range:</label>
        <input type="text" id="amount" style="border: 0; color: #f6931f; font-weight: bold;" />
    </p>
  <div id="slider-range" class="ui-slider ui-slider-horizontal ui-widget ui-widget-content ui-corner-all sliderDiv"
        aria-disabled="false">
        <div id="SliderContent" class="scroll-content" style="margin-top: 0px; position: absolute;">
            <div id="1" class="scroll-content-item  rdLvl12 rdLv1" style="/* position: absolute;
                */
    margin-left: 0px;">
                1</div>
            <div id="2" class="scroll-content-item  rdLvl12" style="margin-left: 26px;">
                2</div>
            <div id="3" class="scroll-content-item  rdLvl345" style="margin-left: 52px;">
                3</div>
            <div id="4" class="scroll-content-item  rdLvl345" style="margin-left: 78px;">
                4</div>
            <div id="5" class="scroll-content-item  rdLvl345" style="margin-left: 104px;">
                5</div>
            <div id="6" class="scroll-content-item  rdLvl678" style="margin-left: 130px;">
                6</div>
            <div id="7" class="scroll-content-item  rdLvl678" style="margin-left: 156px;">
                7</div>
            <div id="8" class="scroll-content-item  rdLvl678" style="margin-left: 182px;">
                8</div>
            <div id="9" class="scroll-content-item  rdLvl91011" style="margin-left: 208px;">
                9</div>
            <div id="10" class="scroll-content-item  rdLvl91011" style="margin-left: 234px;">
                10</div>
            <div id="11" class="scroll-content-item  rdLvl91011" style="margin-left: 260px;">
                11</div>
            <div id="12" class="scroll-content-item  rdLvl121314" style="margin-left: 286px;">
                12</div>
            <div id="13" class="scroll-content-item  rdLvl121314" style="margin-left: 312px;">
                13</div>
            <div id="14" class="scroll-content-item  rdLvl121314" style="margin-left: 338px;">
                14</div>
            <div id="15" class="scroll-content-item  rdLvl1516" style="margin-left: 364px;">
                15</div>
            <div id="16" class="scroll-content-item  rdLvl1516" style="margin-left: 390px;">
                16</div>
            <div id="17" class="scroll-content-item  rdLvl1718" style="margin-left: 416px;">
                17</div>
            <div id="18" class="scroll-content-item  rdLvl1718" style="margin-left: 442px;">
                18</div>
            <div id="19" class="scroll-content-item  rdLvl1920" style="margin-left: 468px;">
                19</div>
            <div id="20" class="scroll-content-item  rdLvl1920" style="margin-left: 494px;">
                20</div>
            <div id="21" class="scroll-content-item  rdLvl2122" style="margin-left: 520px;">
                21</div>
            <div id="22" class="scroll-content-item  rdLvl2122" style="margin-left: 546px;">
                22</div>
            <div id="23" class="scroll-content-item  rdLvl2324" style="margin-left: 572px;">
                23</div>
            <div id="24" class="scroll-content-item  rdLvl2324" style="margin-left: 598px;">
                24</div>
        </div>
    </div>
</div>
<script type="text/javascript">
    var leftStartValue = 0, rightStartValue = 24;
    var pageName = '';
    $(function () {
        $("#slider-range").slider({
            range: true,
            min: 0,
            max: 24,
            values: [leftStartValue, rightStartValue],
            create: function (event, ui) {
                jQuery('<img src="' + GetFile('/Portals/0/images/slider_tab_top.png') + '" alt="" class="SliderTopImg"><img src="' + GetFile('/Portals/0/images/slider_tab_bottom.png') + '" alt="" class="SliderBottomImg">').appendTo(jQuery("#slider-range a"));
                              
                for (var i = 0; i <= leftStartValue; i++) {
                    $('#' + i).addClass('widgetheader');
                }

                for (var i = rightStartValue; i < 24; i++) {
                    $('#' + (i + 1)).addClass('widgetheader');
                }
                $('#' + (parseInt(rightStartValue) + 1)).addClass('widgetheader');

            },
            slide: function (event, ui) {
                if (ui.values[0] == ui.values[1])
                    return false;

                $("#amount").val(ui.values[0] + "-" + ui.values[1]);
                for (var i = 0; i <= 24; i++) {
                    $('#' + i).addClass('widgetheader');
                }

                for (var i = parseInt(ui.values[0]); i <= parseInt(ui.values[1]); i++) {
                    $('#' + i).removeClass('widgetheader');
                }
                $('#' + ui.values[0]).addClass('widgetheader');
            },
            stop: function (event, ui) {
                if (pageName == 'Profile')
                    SetBookReadLevel()
            }
        });
        $("#amount").val($("#slider-range").slider("values", 0) +
            " - " + $("#slider-range").slider("values", 1));
    });
</script>
