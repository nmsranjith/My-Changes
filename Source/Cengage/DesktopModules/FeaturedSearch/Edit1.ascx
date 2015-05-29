<%@ Control Language="C#" Inherits="DotNetNuke.Modules.FeaturedSearch.Edit" AutoEventWireup="false"
    CodeBehind="Edit.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/FeaturedSearch/Scripts/FeaturedSearch.js"
    ForceProvider="DnnFormBottomProvider" />
<div class="fsave-cont ftopdiv">
    <span class="fspan">Options : </span>
    <input type="radio" value="New Search" name="SelectItem" onclick="SelectedItem(1)"
        class="pull-left" />
    <span class="fspan">New Search</span>
    <input type="radio" value="Current search list" name="SelectItem" onclick="SelectedItem(2)"
        class="pull-left" />
    <span class="fspan">Current search list</span>
</div>
<div id="NewSearchDiv" class="ftopdiv">
    <div class="fsave-cont">
        <p>
            Please enter the name of your search:
        </p>
        <p>
            <label for="SearchNameTxt">
                Name:
            </label>
            <input id="SearchNameTxt" type="text" class="ftxt" name="name" placeholder="i.e. wish-list, to booklist next year, save for later" />
        </p>
        <div id="SaveSearchNameerror" class="alert alert-primary HideItems">
            <div>
                <span class="ico-error pull-left"></span><span class="error-text">Please enter name
                    of your search. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
                    </span>
            </div>
        </div>
    </div>
    <div id="AdvanceSearchDiv">
        <div class="fsearch-top">
            <div class="ffindTxt">
                Find results with:</div>
            <div class="fsearch_nav">
                <div class="frows">
                    <label for="AdvTitleTxt">
                        Title:</label>
                    <span>
                        <input id="AdvTitleTxt" type="text" name="Title" class="ftxt" maxlength="50" /></span>
                </div>
                <div class="frows">
                    <label for="AdvAuthorTxt">
                        Author:</label>
                    <span>
                        <input id="AdvAuthorTxt" type="text" name="Author" class="ftxt" maxlength="50" /></span>
                </div>
                <div class="frows">
                    <label for="AdvSubjectTxt">
                        Subject:</label>
                    <span>
                        <input id="AdvSubjectTxt" type="text" name="Subject" class="ftxt" maxlength="50" /></span>
                </div>
                <div id="AttributeSet">
                    <div class="frows">
                        <div class="frecommand">
                            <asp:DropDownList ID="AdvAttrTypesDrpDwn0" runat="server" ClientIDMode="Static" DataTextField="ATTRIBUTE_NAME"
                                DataValueField="ATTRIBUTE_TYPE_SK" CssClass="frec-dropdown" AppendDataBoundItems="true"
                                onchange="AddAttributeItem(AdvAttrTypesDrpDwn0)">
                                <asp:ListItem Text="Choose" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <input type="text" placeholder="" class="ficoninput" maxlength="50" />
                        </div>
                    </div>
                </div>
                <div id="FilterSet">
                    <div class="ffillterTxt">
                        Filter results:</div>
                    <div class="frows">
                        <div class="frecommand">
                            <select id="FilterResultsDrpDwn0" class="frec-dropdown" onchange="AddFilterItems(FilterResultsDrpDwn0)">
                                <option value="0">Choose</option>
                                <option value="1">All these words</option>
                                <option value="2">This exact phrase</option>
                                <option value="3">None of these words</option>
                            </select>
                            <input type="text" placeholder="" class="ficoninput" maxlength="50" />
                        </div>
                    </div>
                </div>
                <div class="pull-right fadvanced-search-btn">
                    <button type="button" class="btn btn-affermative upper-case" onclick="AdvanceSearch()">
                        SAVE</button>
                </div>
            </div>
        </div>
        <input type="reset" id="AdvResetBtn" class="HideItems" />
    </div>
</div>
<div id="CurrentSearchListDiv" class="ftopdiv flisttop">
    <div>
        <div class="searchname">
            Cognitive Development
        </div>
        <div class="deletesearch">
            <span class="ico-clearclose" onclick="RemoveItem(this)"></span>
        </div>
    </div>
    <div>
        <div class="searchname">
            Education & Professional Development
        </div>
        <div class="deletesearch">
            <span class="ico-clearclose" onclick="RemoveItem(this)"></span>
        </div>
    </div>
</div>
