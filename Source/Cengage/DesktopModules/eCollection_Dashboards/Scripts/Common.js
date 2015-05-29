jQuery(
     function SetMenuBackground() {         
         jQuery('#eCollectionecollnk').addClass('DbecollectionActive'); 
         jQuery('#bannertitle').addClass('bannerMultiProfile');                   
         if (parseInt(jQuery('#SubsCnt').val()) > 1) {
             jQuery(jQuery('#SelectedSubs').html()).appendTo('#masterhead');
             jQuery('#MainDiv').addClass('MultiSubsMainDiv');
             jQuery('.bannersececollection').addClass('bannerMultiHeight'); 
         }        

         var contentheight = jQuery('#eCollectionContent').height();
         jQuery('#eCollectionMenu').height(contentheight + 25 + 'px');
         jQuery('#eCollectionContent').height((contentheight + 5) + 'px');
         $('#eCollectionlnk').addClass('menuitemactive');


         if ($.browser.msie) {
             $('.TopBandBtnHdr').addClass('TopBandBtnHdrIE');
         }
         $('#UpgradeBox').mouseover(function () {
             $('#UpgradeLink').addClass('UpgradeLinkhover');
         });
         $('#UpgradeBox').mouseout(function () {
             $('#UpgradeLink').removeClass('UpgradeLinkhover');
         });
     }
    );

    function SelectedMenuCss(holder, tab) {
        jQuery('#' + holder).addClass('selectedTabHolder');
        jQuery('#' + tab).addClass('selectedTab');
    }