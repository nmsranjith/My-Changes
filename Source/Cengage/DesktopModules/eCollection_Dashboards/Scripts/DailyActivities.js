    var ldCnt = 0;
    $(document).ready(function () {
        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                if ($('#BoolCount').val() != "stop") {
                    $('#LoadMoreButton').click();
                    if ($('#BoolCount').val() != "stop" && ldCnt == 0) {
                        $('#LoadMoreImg').show();
                        $('#eCollectionContent').height($('#eCollectionContent').height() + $('#LoadMoreImg').height() + 40);
                        $('#eCollectionMenu').height($('#eCollectionContent').height() + 14);
                        ldCnt = 1;
                    }
                }
            }
        });
    });
    function SeeAllBooks(val) {
        $('#eCollectionContent').height($('#eCollectionContent').height() + val);
        $('#eCollectionMenu').height($('#eCollectionContent').height() + 14);
    }
    function LoadMore() {
        ldCnt = 0;
        $('#LoadMoreImg').hide();
        $('#eCollectionContent').height($('#SubDetailsDiv').height() + $('#WelcomeVideoDiv').height() + $('#StepsDiv').height() + $('#LoadMorepanel').height() + $('.DashboardDiv').height() + $('#DaysLeftDiv').height() + $('.TotalPurchase').height() +440);
        $('#eCollectionMenu').height($('#eCollectionContent').height() + 20);        
    }
