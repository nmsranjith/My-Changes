<%@ Control language="C#" Inherits="DotNetNuke.Modules.ProductFormat.Edit" AutoEventWireup="false"  Codebehind="Edit.ascx.cs" %>
<script type="text/javascript">
	function ProductformatCancel()
	{
		window.location.href="/gale";
	}
	</script>
<div class="prodformat">
    <div class="formatheader">
        <h5>Product Format</h5>
    </div>
    <div class="formatupload">
       <h5>Image</h5>
    </div>
    <div class="formaturl">
        <h5>URL</h5>
    </div>
</div>
<div class="prodformat">
    <div class="formatheader">
        <h5>Collections</h5>
    </div>
    <div class="formatupload">
        <asp:FileUpload ID="CollectionFileUpload" runat="server" />
        <asp:Image ID="CollectionImage" runat="server" CssClass="formatimg" AlternateText="Collections" Visible="false" />
    </div>
    <div class="formaturl">
        <asp:TextBox ID="CollectionTxt" runat="server"></asp:TextBox>
    </div>
</div>
<div class="prodformat">
    <div class="formatheader">
        <h5>Archives</h5>
    </div>
    <div class="formatupload">
        <asp:FileUpload ID="ArchiveFileUpload" runat="server" />
        <asp:Image ID="ArchiveImage" runat="server" CssClass="formatimg" AlternateText="Archives"  Visible="false" />
    </div>
    <div class="formaturl">
        <asp:TextBox ID="ArchiveTxt" runat="server"></asp:TextBox>
    </div>
</div>
<div class="prodformat">
    <div class="formatheader">
        <h5>eBooks</h5>
    </div>
    <div class="formatupload">
        <asp:FileUpload ID="eBookFileUpload" runat="server" />
        <asp:Image ID="eBookImage" runat="server" CssClass="formatimg" AlternateText="eBooks"  Visible="false"/>
    </div>
    <div class="formaturl">
        <asp:TextBox ID="eBooksTxt" runat="server"></asp:TextBox>
    </div>
</div>
<div class="prodformat">
    <div class="formatheader">
        <h5>Print</h5>
    </div>
    <div class="formatupload">
        <asp:FileUpload ID="PrintFileUpload" runat="server" />
        <asp:Image ID="PrintImage" runat="server" CssClass="formatimg"  AlternateText="Print" Visible="false" />
    </div>
    <div class="formaturl">
        <asp:TextBox ID="PrintTxt" runat="server"></asp:TextBox>
    </div>
</div>
<div class="prodformat">
    <div class="formatheader">
        <h5>Micro</h5>
    </div>
    <div class="formatupload">
        <asp:FileUpload ID="MicroFileUpload" runat="server" />
        <asp:Image ID="MicroImage" runat="server" CssClass="formatimg"  AlternateText="Micro" Visible="false"/>
    </div>
    <div class="formaturl">
        <asp:TextBox ID="MicroTxt" runat="server"></asp:TextBox>
    </div>
</div>
<div class="prodformat prodformatbtndiv">
    <input type="button" class="prodformatsubmit" value="CANCEL" onclick="ProductformatCancel()" />
    <asp:Button ID="SaveButton" runat="server" Text="SAVE" CssClass="prodformatsubmit" OnClick="SaveButton_OnClick" />
</div>