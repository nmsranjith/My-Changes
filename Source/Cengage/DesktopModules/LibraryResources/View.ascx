<%@ Control Language="C#" Inherits="DotNetNuke.Modules.LibraryResources.View" AutoEventWireup="false"
    CodeBehind="View.ascx.cs" %>
<div class="libtype">
    <div class="Div_FullWidth libtypetitle">
        <h4>
            Library Resources</h4>
    </div>
    <div class="libtypeleftdiv">
        <asp:Repeater ID="LeftLibraryResourcesRptr" runat="server">
            <ItemTemplate>
                <div class="Div_FullWidth libtypecontent">
                    <h5>
                        <a href='<%# Eval("URL")%>'>
                            <%# Eval("LIBRARY_TYPE")%></a></h5>
                    <div class="libtypedescription">
                        <%# Eval("DESCRIPTION")%>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="libtyperightdiv">
        <asp:Repeater ID="RightLibraryResourcesRptr" runat="server">
            <ItemTemplate>
                <div class="Div_FullWidth libtypecontent">
                    <h5>
                        <a href='<%# Eval("URL")%>'>
                            <%# Eval("LIBRARY_TYPE")%></a>
                    </h5>
                    <div class="libtypedescription">
                        <%# Eval("DESCRIPTION")%>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
