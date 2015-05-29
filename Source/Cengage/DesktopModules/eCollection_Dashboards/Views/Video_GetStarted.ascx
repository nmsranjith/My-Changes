<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Video_GetStarted.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Dashboards.Views.Video_GetStarted" %>
<asp:PlaceHolder ID="VideoPlaceHdr" runat="server" Visible="false">
    <div id="WelcomeVideoDiv">
        <div class="StarImg" style="margin-left: -12px;">
        </div>
        <h5 class="WelcomeVideo_Label">
            WELCOME VIDEO</h5>
        <div class="Div_FullWidth">
            <hr class="Dashboard_hr1" />
        </div>
        <div class="VideoCloseButton_Div">
            <asp:ImageButton ID="VideoCloseButton" ImageUrl="~/Portals/0/images/close.png" ClientIDMode="Static"
                runat="server" />
        </div>
        <div class="VideoImage_Div">
            <iframe width="595" height="360" src="https://www.youtube.com/embed/MtBAptQVho8?feature=player_detailpage&rel=0&loop=1"
                frameborder="0" allowfullscreen></iframe>
        </div>
    </div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="GetStartedPlaceHdr" runat="server" Visible="false">
    <div id="StepsDiv">
        <div class="Div_FullWidth">
            <div class="StarImg getstartedstar">
            </div>
            <h5 class="StepsDiv_Label">
                GET STARTED : <span id="StepsDiv_Label" runat="server" clientidmode="Static">3</span>
                STEP SET UP
            </h5>
            <div class="StepsCloseButton_Div">
                <asp:ImageButton ID="StepsCloseButton" ImageUrl="~/Portals/0/images/close.png" ClientIDMode="Static"
                    runat="server" />
            </div>
        </div>
        <div class="Div_FullWidth">
            <hr class="Dashboard_hr2" />
        </div>
        <div>
            <div id="BooksStep" runat="server" clientidmode="Static" class="StepSetup1">
                <b class="StepSetup_Bold">1. </b><a class="StepsLink1" href="/ecollection/books.aspx">SELECT YOUR
                    BOOKS</a> <span runat="server" class="Steps_Books_Count" id="AddedBooks"  onclick="navigateto('books')"></span>
                <div id="BooksStepTick" runat="server" clientidmode="Static"  onclick="navigateto('books')">
                </div>
            </div>
            <div id="TeachersStep" runat="server" clientidmode="Static" class="StepSetup1">
                <b class="StepSetup_Bold">2. </b><a class="StepsLink1" href="/ecollection/teachers.aspx">ADD TEACHERS</a>
                <div id="TeachersStepTick" runat="server" clientidmode="Static"  onclick="navigateto('teachers')">
                </div>
            </div>
            <div id="StudentsStep" runat="server" clientidmode="Static" class="StepSetup1">
                <b id="StudentsStepSno" runat="server" clientidmode="Static" class="StepSetup_Bold">
                    3. </b><a class="StepsLink1" href="/ecollection/students.aspx">ADD STUDENTS</a>
                <div id="StudentsStepTick" runat="server" clientidmode="Static"  onclick="navigateto('students')">
                </div>
            </div>
        </div>
    </div>
</asp:PlaceHolder>
<input id="VideoHdn" type="hidden" runat="server" clientidmode="Static" />
<input id="GetStartedHdn" type="hidden" runat="server" clientidmode="Static" />
<script src="desktopmodules/ecollection_dashboards/Scripts/Video_GetStarted.js" type="text/javascript"></script>
