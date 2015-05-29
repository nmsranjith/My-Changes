<%@ Control Language="C#" Inherits="DotNetNuke.Modules.LibraryResources.Edit" AutoEventWireup="false"
    CodeBehind="Edit.ascx.cs" %>
	<script type="text/javascript">
	function LibraryResourceCancel()
	{
		window.location.href="/gale";
	}
	</script>
<div class="libtypetopdiv">
    <div class="libtypeorderdiv">
        Seq.No
    </div>
    <div class="libtypeselectdiv">
        Library Type
    </div>
    <div class="libtypedescriptiondiv">
        Description
    </div>
    <div class="libtypeurldiv">
        URL
    </div>
</div>
<div class="libtypetopdiv">
    <div class="libtypeorderdiv">
        <select id="StateDpn" class="libtypevalue" runat="server">
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
        </select>
    </div>
    <div class="libtypeselectdiv libtypevalue">
        State, National & Public Libraries
    </div>
    <div class="libtypedescriptiondiv">
        <asp:TextBox ID="StateDescriptionTxt" runat="server" maxlength="300" Rows="5" Columns="200" CssClass="Div_FullWidth" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="libtypeurldiv">
        <asp:TextBox ID="StateUrlTxt" runat="server" CssClass="libtypeurltxt"></asp:TextBox>
    </div>
</div>
<div class="libtypetopdiv">
    <div class="libtypeorderdiv">
        <select id="SchoolDpn" class="libtypevalue" runat="server">
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
        </select>
    </div>
    <div class="libtypeselectdiv libtypevalue">
        School Library
    </div>
    <div class="libtypedescriptiondiv">
        <asp:TextBox ID="SchoolDescriptionTxt" runat="server" maxlength="300" Rows="5" Columns="200" CssClass="Div_FullWidth" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="libtypeurldiv">
        <asp:TextBox ID="SchoolUrlTxt" runat="server" CssClass="libtypeurltxt"></asp:TextBox>
    </div>
</div>
<div class="libtypetopdiv">
    <div class="libtypeorderdiv">
        <select id="UniversityDpn" class="libtypevalue" runat="server">
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
        </select>
    </div>
    <div class="libtypeselectdiv libtypevalue">
        University, TAFE. RTO Libraries
    </div>
    <div class="libtypedescriptiondiv">
        <asp:TextBox ID="UnivDescriptionTxt" runat="server" maxlength="300" Rows="5" Columns="200" CssClass="Div_FullWidth" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="libtypeurldiv">
        <asp:TextBox ID="UnivUrlTxt" runat="server" CssClass="libtypeurltxt"></asp:TextBox>
    </div>
</div>
<div class="libtypetopdiv">
    <div class="libtypeorderdiv">
        <select id="SpecialDpn" class="libtypevalue" runat="server">
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
        </select>
    </div>
    <div class="libtypeselectdiv libtypevalue">
        Special Library
    </div>
    <div class="libtypedescriptiondiv">
        <asp:TextBox ID="SpecialDescriptionTxt" runat="server" maxlength="300" Rows="5" Columns="200" CssClass="Div_FullWidth" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="libtypeurldiv">
        <asp:TextBox ID="SpecialUrlTxt" runat="server" CssClass="libtypeurltxt"></asp:TextBox>
    </div>
</div>
<div class="libtypetopdiv libtypebtndiv">
    <input type="button" class="libtypesubmit" value="CANCEL" onclick="LibraryResourceCancel()" />
    <asp:Button ID="SaveButton" runat="server" Text="SAVE" CssClass="libtypesubmit" OnClick="SaveButton_Click" />
</div>
