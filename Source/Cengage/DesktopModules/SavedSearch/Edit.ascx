<%@ Control language="C#" Inherits="DotNetNuke.Modules.SavedSearch.Edit" AutoEventWireup="true"  Codebehind="Edit.ascx.cs" %>
<script language="javascript">
    function NotNullMethod() {
        var Title = document.getElementById("<%=txtTitle.ClientID%>").value;
        if (Title == "") {
            document.getElementById("<%=lblErorTitle.ClientID%>").innerHTML = "*";
            return false;
        }
        document.getElementById("<%=lblErorTitle.ClientID%>").innerHTML = "";
        return true;
    }
   
    function ValidateCheckbox() {
        var CHK = document.getElementById("<%=chkModuleWidth.ClientID%>");
        var checkbox = CHK.getElementsByTagName("input");
        var counter = 0; var atLeast = 1
        for (var i = 0; i < checkbox.length; i++) {
            if (checkbox[i].checked) {
                counter++;
            }
        }
        if (counter == 1) {
            document.getElementById("<%=lblerrorModuleWidth.ClientID%>").innerHTML = "";
            return true;
            
        }
        document.getElementById("<%=lblerrorModuleWidth.ClientID%>").innerHTML = "Select Valid Module Width";
        return false;
    }
    function ValidateCount() {
        var reg1 = new RegExp("^([0-9]|10)$");
        var Count = document.getElementById("<%=txtDefaultDisplayCount.ClientID%>").value;
        if (Count == "") {
            document.getElementById("<%=lblDefaultDisplayCount.ClientID%>").innerHTML = "*";
            return false;
        }
        else if (!reg1.test(Count)) {
            document.getElementById("<%=lblDefaultDisplayCount.ClientID%>").innerHTML = "Enter numbers 1 to 10";
            return false;
        }
        else if(Count<1 || Count>10){
            document.getElementById("<%=lblDefaultDisplayCount.ClientID%>").innerHTML = "Enter numbers 1 to 10";
            return false;
        }
        document.getElementById("<%=lblDefaultDisplayCount.ClientID%>").innerHTML = "";
        return true;
    }
    function ValidateSave() {
        var a = NotNullMethod();
        
        var c = ValidateCheckbox();
        var d = ValidateCount();
        if (a && c && d) {
            return true;
        }
        else {
            return false;
        }
    }
</script>
<table>
<tr>
<td>
Title:
</td>
<td>
<input id="txtTitle" runat="server" type ="text" maxlength="50" value="Saved Searches"/>
</td>
<td>
<asp:Label ID="lblErorTitle" runat="server" ForeColor="red" />
</td>
</tr>

<tr>
<td>
Module Width:
</td>
<td>
<asp:CheckBoxList ID="chkModuleWidth" runat="server">
<asp:ListItem Value="Full Width" Text="Full Width" Selected="True" />
<asp:ListItem Value="2/3 Width" Text="2/3 Width" />
</asp:CheckBoxList>
</td>
<td>
<asp:Label ID="lblerrorModuleWidth" runat="server" ForeColor="red" />
</td>
</tr>
<tr>
<td>
Default Display Count:
</td>
<td>
<input id="txtDefaultDisplayCount" type ="text" runat="server" value="5" />
</td>
<td>
<asp:Label ID="lblDefaultDisplayCount" runat="server" ForeColor="red" />
</td>
</tr>
<tr>
<td>
<asp:Button ID="cmdSave" runat="server" Text="Save" OnClientClick="return ValidateSave();" onclick="cmdSave_Click" />
</td>
</tr>
</table>




