<%@ Control Language="C#" Inherits="DotNetNuke.Modules.eCollection_FeatureSlider.Edit"
    AutoEventWireup="false" CodeBehind="Edit.ascx.cs" %>
    
<div id="FirstDiv" class="Div_FullWidth">
    <div class="EditFirstDiv EditFirstDiv1">
        <div class="EditFirstInner1 EditFirstInnerDiv1">
            <h4>
                Number of slides:</h4>
        </div>
        <div class="EditFirstInner2">
            <asp:TextBox ID="SlidesCountTxt" runat="server" CssClass="SlidesCountTxt"></asp:TextBox>
            <asp:Button ID="SubmitSlideCount" runat="server" Text="Set Slide Count" CssClass="SliderSetSave_Buttons" OnClick="SubmitSlideCount_Click" />
        </div>
    </div>
</div>
<div id="SecondDiv" class="Div_FullWidth" visible="false" runat="server">
    <div class="EditSecondDiv">
        <div class="EditFirstInner1">
            <h4>
                Select slide number:</h4>
        </div>
        <div class="EditFirstInner2">
            <asp:DropDownList ID="SlidesDrpDwn" CssClass="SlidesDrpDwn" runat="server" Enabled="false"
                OnSelectedIndexChanged="SlidesDrpDwn_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>
    <div class="EditSecondDiv">
        <div class="EditFirstInner1">
            <h4>
                Slide header text:</h4>
        </div>
        <div class="EditFirstInner2">
            <asp:TextBox ID="Slide_HeaderTxt" runat="server" CssClass="SlidesCountTxt SlideHeaderTxt"
                Enabled="false"></asp:TextBox>
        </div>
    </div>
    <div class="EditSecondDiv">
        <div class="EditFirstInner1">
            <h4>
                Enter slide text:</h4>
        </div>
        <div class="EditFirstInner2">
            <asp:TextBox ID="SlideText_MultiLine" MaxLength="300" TextMode="MultiLine" CssClass="SlideTextMultiLine"
                Enabled="false" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="EditSecondDiv">
        <div class="EditFirstInner1">
            <h4>
                Choose slide image:</h4>
        </div>
        <div class="EditFirstInner2">
            <asp:FileUpload ID="Slide_FileUpload" runat="server" Enabled="false" />
        </div>
    </div>
    <div class="EditSecondDiv">
        <div class="EditFirstInner1">
            <asp:Button ID="Slide_SaveButton" runat="server" Text="Save" CssClass="SliderSetSave_Buttons" OnClick="Slide_SaveButton_Click" />
        </div>
    </div>
</div>
