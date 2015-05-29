<%@ Control Language="C#" Inherits="DotNetNuke.Modules.FeaturedSearch.View" AutoEventWireup="false"
    CodeBehind="View.ascx.cs" %>
<div class="ftopdiv flisttop">
    <span id="FeaturedSearchTitle" runat="server" clientidmode="Static" class="he_module_title">Featured Searches :</span>
    <asp:Repeater ID="FeaturedSearchRptr" runat="server">
        <ItemTemplate>
            <div class="Div_FullWidth ">
                <div class="searchname">
                    <a href='<%# Eval("CurrentUrl")%>'>
                        <%# Eval("SearchName")%></a>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <%--<div class="Div_FullWidth ">
        <div class="searchname">
            <a href="/search?st=ad&division=gale&tq=Education%20%26%20Professional%20Development&aq=&subq=&allq=&nq=&etq=&fv=&pv=&pyv=&ov=&epf=&lt=">
                Education & Professional Development</a>
        </div>
    </div>
    <div class="Div_FullWidth ">
        <div class="searchname">
            <a href="/search?st=ad&division=gale&tq=Professional%20Development&aq=&subq=&allq=&nq=&etq=&fv=&pv=&pyv=&ov=&epf=&lt=">
                Professional Development</a>
        </div>
    </div>
    <div class="Div_FullWidth ">
        <div class="searchname">
            <a href="/search?st=ad&division=gale&tq=Computer%20Science&aq=&subq=&allq=&nq=&etq=&fv=&pv=&pyv=&ov=&epf=&lt=">
                Computer Science</a>
        </div>
    </div>
    <div class="Div_FullWidth ">
        <div class="searchname">
            <a href="/search?st=ad&division=gale&tq=Computing%20%26%20Information%20Technology&aq=&subq=&allq=&nq=&etq=&fv=&pv=&pyv=&ov=&epf=&lt=">
                Computing & Information Technology</a>
        </div>
    </div>
    <div class="Div_FullWidth ">
        <div class="searchname">
            <a href="/search?st=ad&division=gale&tq=Media%20Arts%20%26%20Design&aq=&subq=&allq=&nq=&etq=&fv=&pv=&pyv=&ov=&epf=&lt=">
                Media Arts & Design</a>
        </div>
    </div>--%>
</div>
