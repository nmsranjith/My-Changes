<%@ Control Language="C#" Inherits="DotNetNuke.Modules.InformationBanner.Edit" AutoEventWireup="true"
    CodeBehind="Edit.ascx.cs" %>
<link href="<%=Page.ResolveUrl("DesktopModules/eCollection_Students/CSS/jQuery.ui.smoothness.css")%>"
    rel="Stylesheet" type="text/css" />
<script type="text/javascript">

    function alink() {
        $(function () {
            $(".datepickerCompleted").kendoDateTimePicker({

                format: "MM/dd/yyyy hh:mm:ss tt"
            });
        });
    }

    function validateerrormessage(a) {
        if (a == "") {
            //alert("Error Message Empty");
            $('#<%= lblError.ClientID %>').text("Error Message Empty");
            return false;
        }
        else {
            return true;
        }
    }
    function validateerrortype(a) {

        if (a == "Select Error Type") {
            //alert("Select valid Error Type");
            $('#<%= lblError.ClientID %>').text("Error Type Empty");
            return false;
        }
        else {
            return true;
        }
    }
    function validatestartdate(a, b) {
        var reg1 = new RegExp("^(([0]?[1-9]|1[0-2])/([0-2]?[0-9]|3[0-1])/[1-2][0-9][0-9][0-9] ([0]?[0-9]|1[0-2]):([0-5]?[0-9]):([0-5]?[0-9]) [APap][mM])$");
        var reg2 = new RegExp("^(([0]?[1-9]|1[0-2])/([0-2]?[0-9]|3[0-1])/[1-2][0-9][0-9][0-9] ([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9])$");
        if (b == "" || a == "") {
            //alert("Start Date Empty");
            $('#<%= lblError.ClientID %>').text("Start/End date is Empty");
            return false;
        }
        else if (!reg1.test(b) && !reg2.test(b)) {
            $('#<%= lblError.ClientID %>').text("End date is not in Format");
            return false;
        }
        else if (!reg1.test(a) && !reg2.test(a)) {
            $('#<%= lblError.ClientID %>').text("Start date is not in Format");
            return false;
        }
        else if (Date.parse(a) > Date.parse(b)) {
            $('#<%= lblError.ClientID %>').text("End date should be greater than start date");
            return false;
        }
        else {
            return true;
        }
    }

    function validateenddate(a) {
        if (a == "") {
            //alert("End Date Empty");
            $('#<%= lblError.ClientID %>').text("End date Empty");
            return false;
        }
        else {
            return true;
        }
    }
    function ValidateAdd(errormessage, errortype, startdate, enddate) {
        $('#<%= lblError.ClientID %>').text("");

        var errormessagevalue = document.getElementById(errormessage).value;
        var errortypevalue = document.getElementById(errortype).value;
        var errorstartdate = document.getElementById(startdate).value;
        var errorenddate = document.getElementById(enddate).value;

        var astd = errorstartdate.split('/'), aend = errorenddate.split('/');

        var d = new Date(astd[1] + '/' + astd[0] + '/' + astd[2]);
        //alert(new Date(astd[1] + '/' + astd[0] + '/' + astd[2]));
       if(astd[2].length<4){
		 alert('Please enter a valid start date');
            return false;
		}
		else if (new Date(astd[1] + '/' + astd[0] + '/' + astd[2]).toLocaleString().toLowerCase() == 'invalid date') {
            alert('Please enter a valid start date');
            return false;
        }
		if(aend[2].length<4){
		 alert('Please enter a valid end date');
            return false;
		}
        else if (new Date(aend[1] + '/' + aend[0] + '/' + aend[2]).toLocaleString().toLowerCase() == 'invalid date') {
            alert('Please enter a valid end date');
            return false;
        }
        errorstartdate = new Date(astd[1] + '/' + astd[0] + '/' + astd[2]);
        errorenddate = new Date(aend[1] + '/' + aend[0] + '/' + aend[2]);
        //errorstartdate = document.getElementById(startdate).value;
        //errorenddate = document.getElementById(enddate).value;
        //var d = validateenddate(errorenddate);
        //var c = validatestartdate(errorstartdate, errorenddate);
        var b = validateerrortype(errortypevalue);
        var a = validateerrormessage(errormessagevalue);
        if (a && b && c) {
            return true;
        }
        else {
            return false;
        }
    }
    function ValidateUpdate(errormessage, errortype, startdate, enddate) {

        //          alert(document.getElementById(errortype));
        //          alert(document.getElementById(errortype).value);
        $('#<%= lblError.ClientID %>').text("");
        var errormessagevalue = document.getElementById(errormessage).value;
        var errortypevalue = document.getElementById(errortype).value;
        var errorstartdate = document.getElementById(startdate).value;
        var errorenddate = document.getElementById(enddate).value;



        //var d = validateenddate(errorenddate);
        var c = validatestartdate(errorstartdate, errorenddate);
        var b = validateerrortype(errortypevalue);
        var a = validateerrormessage(errormessagevalue);
        if (a && b && c) {
            return true;
        }
        else {
            return false;
        }
    }
    $(function () {
        $(".datepickertxt").datepicker({
            showOn: "button",
            dateFormat: "dd/mm/yy",
            buttonImage: "/portals/0/images/callender.png",
            buttonImageOnly: true
        });
        $(".datepickeredittxt").datepicker({
            showOn: "button",
            dateFormat: "dd/mm/yy",
            buttonImage: "/portals/0/images/callender.png",
            buttonImageOnly: true
        });
        /*	$( "#txtinputEndDate2" ).datepicker({
        showOn: "button",
        dateFormat: "dd/mm/yy",
        //  minDate:new Date(),
        buttonImage: "/portals/0/images/callender.png",
        buttonImageOnly: true			  
        });
        $( "#txtinputStartDate1" ).datepicker({
        showOn: "button",
        dateFormat: "dd/mm/yy",
        buttonImage: "/portals/0/images/callender.png",
        buttonImageOnly: true			  
        });
        $( "#txtinputStartDate2" ).datepicker({
        showOn: "button",
        dateFormat: "dd/mm/yy",
        //  minDate:new Date(),
        buttonImage: "/portals/0/images/callender.png",
        buttonImageOnly: true			  
        });
        */

    });
</script>
<asp:Label ID="lblError" runat="server" ForeColor="red" CssClass="left-module" />
<asp:GridView ID="grdAlert" CssClass="left-module" runat="server" AutoGenerateColumns="False"
    EnableModelValidation="True" OnRowCancelingEdit="grdAlert_RowCancelingEdit" OnRowEditing="grdAlert_RowEditing"
    OnRowUpdating="grdAlert_RowUpdating" ShowFooter="True" OnRowDeleting="grdAlert_RowDeleting"
    OnRowCommand="grdAlert_RowCommand" OnRowDataBound="grdAlert_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="Alert ID">
            <ItemTemplate>
                <asp:Label ID="lblAlertID" runat="server" Text='<%# Eval("Alert_ID")%>' />
            </ItemTemplate>
            <ControlStyle Width="90%" />
            <FooterStyle Width="50px" />
            <ItemStyle Width="50px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Error Message">
            <ItemTemplate>
                <asp:Label ID="lblErrorMessage" runat="server" Text='<%# Eval("ERROR_MESSAGE")%>' />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtErrorMessage" runat="server" MaxLength="100" Text='<%# Eval("ERROR_MESSAGE")%>' />
            </EditItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtinputErrorMessage" MaxLength="100" runat="server" />
            </FooterTemplate>
            <ControlStyle Width="90%" />
            <FooterStyle Width="50px" />
            <ItemStyle Width="50px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Error Type">
            <ItemTemplate>
                <asp:Label ID="lblErrorType" runat="server" Text='<%# Eval("ErrorType")%>' />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlErrorType" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                    SelectedValue='<%# Eval("ErrorType") %>'>
                    <asp:ListItem Value="Select Error Type" Text="Select Error Type"></asp:ListItem>
                    <asp:ListItem Value="General Message" Text="General Message"></asp:ListItem>
                    <asp:ListItem Value="Error Message" Text="Error Message"></asp:ListItem>
                    <asp:ListItem Value="Success Message" Text="Success Message"></asp:ListItem>
                    <asp:ListItem Value="Warning Message" Text="Warning Message"></asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:DropDownList ID="ddlinputErrorType" runat="server">
                    <asp:ListItem Value="Select Error Type" Text="Select Error Type"></asp:ListItem>
                    <asp:ListItem Value="General Message" Text="General Message"></asp:ListItem>
                    <asp:ListItem Value="Error Message" Text="Error Message"></asp:ListItem>
                    <asp:ListItem Value="Success Message" Text="Success Message"></asp:ListItem>
                    <asp:ListItem Value="Warning Message" Text="Warning Message"></asp:ListItem>
                </asp:DropDownList>
            </FooterTemplate>
            <ControlStyle Width="90%" />
            <FooterStyle Width="50px" />
            <ItemStyle Width="50px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Start Date">
            <ItemTemplate>
                <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("Start_Date")%>' CssClass="editspandatetxt" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtStartDate1" CssClass="datepickeredittxt" runat="server"
                    Text='<%# Eval("Start_Date")%>' ClientIDMode="Static" />
                <%-- <input type="text" id="txtinputStartDate1" class="datepickeredittxt" value='<%# Eval("Start_Date")%>'  runat="server" clientidmode="Static"/>--%>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtinputStartDate" CssClass="datepickertxt" runat="server" ClientIDMode="Static" /><%--
                <input type="text" id="txtinputStartDate2" class="datepickertxt" />--%>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="End Date">
            <ItemTemplate>
                <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("End_Date")%>' CssClass="editspandatetxt" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEndDate1" runat="server" CssClass="datepickeredittxt" ClientIDMode="Static"
                    Text='<%# Eval("End_Date")%>' />
                <%--  <input type="text" id="txtEndDate1" class="datepickeredittxt" value='<%# Eval("End_Date")%>' runat="server" clientidmode="Static" />--%>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtinputEndDate" CssClass="datepickertxt" runat="server" ClientIDMode="Static" /><%--
                <input type="text" id="txtinputEndDate" class="datepickertxt"  runat="server" clientidmode="Static" />--%>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Edit">
            <ItemTemplate>
                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" CommandName="Update" />
                <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CommandName="Cancel" />
            </EditItemTemplate>
            <FooterTemplate>
                <asp:LinkButton ID="lnkAdd" runat="server" Text="Add" CommandName="Add" />
            </FooterTemplate>
            <ControlStyle Width="85%" />
            <FooterStyle Width="70px" />
            <ItemStyle Width="70px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Delete">
            <ItemTemplate>
                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="Delete" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<link src="/Resources/Shared/scripts/kendoui/styles/kendo.common.min.css" />
<link src="/Resources/Shared/scripts/kendoui/styles/kendo.default.min.css" />
<link src="/Resources/Shared/scripts/kendoui/styles/kendo.dataviz.min.css" />
<script src="/Resources/Shared/scripts/kendoui/kendo.all.min.js" type="text/javascript"></script>
