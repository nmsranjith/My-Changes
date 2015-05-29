<%@ Control Language="C#" Inherits="DotNetNuke.Modules.ProductFormat.View" AutoEventWireup="false"
    CodeBehind="View.ascx.cs" %>
<div class="lib-images">
   <asp:Repeater ID="ProductFormatsRptr" runat="server">
    <ItemTemplate>
        <div class="inline">
            <a href='<%# Eval("URL") %>'>
                <img src='<%# Eval("FILE_NAME")%>' alt="" class="" /></a>
        </div>
    </ItemTemplate>
</asp:Repeater>
</div>

