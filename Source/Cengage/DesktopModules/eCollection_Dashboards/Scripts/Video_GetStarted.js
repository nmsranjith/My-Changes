    jQuery(function () {
        if (jQuery('#VideoHdn').val() == 'N')
            $('#WelcomeVideoDiv').addClass('zeroheight');

        if (jQuery('#GetStartedHdn').val() == 'N')
            $('#StepsDiv').addClass('zeroheight');

        jQuery('#VideoCloseButton').click(function () {
            $('#WelcomeVideoDiv').hide();
            $.ajax({
                url: GetFile('/DesktopModules/eCollection_Dashboards/Handlers/Dashboard_Handler.ashx?pageContent=close&video=N&getstarted=' + jQuery('#GetStartedHdn').val()),
                dataType: "json",
                success: function (value) {
                    if (value == 1)
                        jQuery('#WelcomeVideoDiv').hide();
                }
            });
            $('#WelcomeVideoDiv').addClass('zeroheight');
            scrollValue = 155 + $('#WelcomeVideoDiv').height() + $('StepsDiv').height();
            $('#eCollectionContent').height(155 - $('#WelcomeVideoDiv').height() + $('StepsDiv').height() + $('#TotalPurchase').height() + $('.DashboardDiv').height() + 450);
            $('#eCollectionMenu').height($('#eCollectionContent').height() + 12);
            return false;
        });

        jQuery('#StepsCloseButton').click(function () {
            $('#StepsDiv').hide();
            $.ajax({
                url: GetFile('/DesktopModules/eCollection_Dashboards/Handlers/Dashboard_Handler.ashx?pageContent=close&getstarted=N&video=' + jQuery('#VideoHdn').val()),
                dataType: "json",
                success: function (value) {
                    if (value == 1)
                        jQuery('#StepsDiv').hide();
                }
            });
            $('#StepsDiv').addClass('zeroheight');
            scrollValue = 155 + $('#WelcomeVideoDiv').height() + $('StepsDiv').height();
            $('#eCollectionContent').height(155 + $('#WelcomeVideoDiv').height() - $('StepsDiv').height() + $('#TotalPurchase').height() + $('.DashboardDiv').height() + 450);
            $('#eCollectionMenu').height($('#eCollectionContent').height() + 12);
            return false;
        });

    });

    function navigateto(page) {
        window.location.href = '/ecollection/'+page + '.aspx';
    }
