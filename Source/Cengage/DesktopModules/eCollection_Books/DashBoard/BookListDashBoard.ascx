<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookListDashBoard.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Books.DashBoard.BookListDashBoard" %>
<div>
    <center>
    <br />
       <div id="CreateGroupDiv" class="ActiveAddButtonsHolder" style="float: left;margin-left: 23px;">
       <asp:Button ID="StartReadingSessionButton" 
            runat="server" clientIdMode="static" Text="CREATE A READING SESSION" 
            CssClass="BtnStyle creategroupbtn" onclick="StartReadingSessionButton_Click" />
       
            </div>
        <asp:HiddenField ID="selectedSessionID" ClientIDMode="Static" runat="server" />
    </center>
</div>

<script type="text/javascript">

    $(document).ready(function () {
        $("#StartReadingSessionButton").click(function () {
            $('#selectedSessionID').val($('#custItmSKHidden').val());            

        });
    });

   
</script>
