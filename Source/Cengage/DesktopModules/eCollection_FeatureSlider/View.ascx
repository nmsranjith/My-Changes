<%@ Control Language="C#" Inherits="DotNetNuke.Modules.eCollection_FeatureSlider.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<div class="Fslidertopdiv">
    <div class="Fsliderscnddiv">
        <h3 class="Fsliderh3">
            OUR FAVOURITE FEATURES:
        </h3>
        <div>
            <div id="FS1" class="FS ShowItems">
                <h4 class="Fsliderh4">
                    Custom Reading profile
                </h4>
                <p class="Fsliderp">
                    Turn functions on or off including the ability to hide the text to turn a book into
                    a re-telling opportunity</p>
            </div>
            <div id="FS2" class="FS HideItems">
                <h4 class="Fsliderh4">
                    Edit
                </h4>
                <p class="Fsliderp">
                    Students can edit the text
                </p>
            </div>
            <div id="FS3" class="FS HideItems">
                <h4 class="Fsliderh4">
                    My Words
                </h4>
                <p class="Fsliderp">
                    Students can create a customised word list for<br />
                    spelling or word study
                </p>
            </div>
            <div id="FS4" class="FS HideItems">
                <h4 class="Fsliderh4">
                    Record
                </h4>
                <p class="Fsliderp">
                    Students can record themselves reading a text
                </p>
            </div>
            <div id="FS5" class="FS HideItems">
                <h4 class="Fsliderh4">
                    Reading sessions
                </h4>
                <p class="Fsliderp">
                    Teachers can allocate books, set reading profiles and include a personalised note
                    to a student or group of students</p>
            </div>
        </div>
        <div class="Fsliderbtndiv">
            <input type="button" id="eCollPrevButton" value="PREV" class="eCollectionsliderButton ActiveAddButtonsHolder" />
            <input type="button" id="eCollNextButton" value="NEXT" class="eCollectionsliderButton ActiveAddButtonsHolder" />
        </div>
    </div>
    <div id="OtherFeatureSlider" class="FeatureSlider Fsliderimgdiv">
        <asp:Image ID="CustomReadingProfile" AlternateText="" ImageUrl="Images/Custom Reading Profile.png" CssClass="HideItems FSSliderImages" runat="server"/>
        <asp:Image ID="Edit" AlternateText="" ImageUrl="Images/Edit.png" runat="server" CssClass="HideItems FSSliderImages"/>
        <asp:Image ID="MyWords" AlternateText="" ImageUrl="Images/My Words.png" runat="server" CssClass="HideItems FSSliderImages"/>
        <asp:Image ID="Record" AlternateText="" ImageUrl="Images/Record.png" runat="server" CssClass="HideItems FSSliderImages"/>
        <asp:Image ID="Readingsessions" AlternateText="" ImageUrl="Images/FSlider5.png" runat="server" CssClass="HideItems FSSliderImages"/>
    </div>
</div>

   <script type="text/javascript">
       var ClickCount = 1;
       $(function () {
           ShowImage();
           $("#eCollNextButton").click(function () {
               if (ClickCount < 5)
                   ClickCount++;
               else
                   ClickCount = 1;
               ShowImage();
           });
           $("#eCollPrevButton").click(function () {
               if (ClickCount > 1)
                   ClickCount--;
               else
                   ClickCount = 5;
               ShowImage();
           });
       });
       function ShowImage() {
           $('#OtherFeatureSlider img').removeClass('ShowItems').addClass('HideItems');
           $('.FS').removeClass('ShowItems').addClass('HideItems');
           if (ClickCount == 1) {
               $('#' + '<%= CustomReadingProfile.ClientID %>').removeClass('HideItems').addClass('ShowItems');
               $('#FS1').removeClass('HideItems').addClass('ShowItems');
           }
           else if (ClickCount == 2) {
               $('#' + '<%= Edit.ClientID %>').removeClass('HideItems').addClass('ShowItems');
               $('#FS2').removeClass('HideItems').addClass('ShowItems');
           }
           else if (ClickCount == 3) {
               $('#' + '<%= MyWords.ClientID %>').removeClass('HideItems').addClass('ShowItems');
               $('#FS3').removeClass('HideItems').addClass('ShowItems');
           }
           else if (ClickCount == 4) {
               $('#' + '<%= Record.ClientID %>').removeClass('HideItems').addClass('ShowItems');
               $('#FS4').removeClass('HideItems').addClass('ShowItems');
           }
           else{
               $('#' + '<%= Readingsessions.ClientID %>').removeClass('HideItems').addClass('ShowItems');
               $('#FS5').removeClass('HideItems').addClass('ShowItems');
           }

       }
   </script>