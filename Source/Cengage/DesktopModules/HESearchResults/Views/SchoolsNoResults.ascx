<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchoolsNoResults.ascx.cs" Inherits="DotNetNuke.Modules.HESearchResults.Views.SchoolsNoResults" %>
<div>
    <div class="sepLine">
        <img id="Img1" alt="separrow" class="Noimg" src="<%=Page.ResolveUrl("Portals/0/images/separatorarrow.PNG")%>" />
    </div>
    <div class="Nrfrdiv">
        <span class="nrsdiv" id="noresultspan">
            <b>Sorry, your search for
                "<span id="Searchtext"></span>" returned no results</b></span>
        <div class="Nrtdiv">
        <div id="Dmeandiv" style="display:none;">
            <b class="Nrbcol">Did you mean:</b><br />
            <a id="DmeanLink" class="Nracol" href="#" >
            </a>
            </div>
            <br />
            <b class="Nrbcol">Suggestions:</b><br />
            <span class="Nrbrcol">1. Make sure all words are spelled correctly.<br />
                2. Try different keywords.<br />
                3. Try more general keywords.<br />
                4. Still can't find it? </span><a href="contactus.aspx" class="Nracol">Contact
                    Customer Service</a>
        </div>
    </div>
</div>
<script src="<%=Page.ResolveUrl("DesktopModules/HESearchResults/Scripts/Noresult.js")%>" type ="text/javascript"></script>