<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HEInitialNoResult.ascx.cs"
    Inherits="DotNetNuke.Modules.HESearchResults.Views.HEInitialNoResult" %>
<div class="search-container" style="min-height: 400px">
    <div class="row">
        <div class="col-md-12">
            <!-- Search Bar-->
        </div>
        <div class="no-result-main">
            <div class="tabbable">
                <!-- Only required for left/right tabs -->
                <ul class="nav nav-tab">
                    <li class="active"><a id="productstab" href="#tab1" data-toggle="tab">Products</a></li>
                    <li><a id="infotab" href="#tab2" data-toggle="tab">Information</a></li>
                </ul>
                <div class="tab-content hefullwidth">
                    <div class="tab-pane active" id="tab1">
                        <!-- Product Tab -->
                        <div class="search-no-result">
                            <div class="no-result-txt">
                                <p>
                                    Sorry! There are no matching product results <span id="noresultforspan">for</span>
                                </p>
                                <p id="noresulttextpara" class="keyword">
                                    "<asp:Label ID="SearchTextLbl" runat="server" ></asp:Label>"</p>
								<p id="didyoumeanPara" runat="server" class="keyword">Did you mean?
                                    "<asp:HyperLink ID="DidYouMeanLink" runat="server" NavigateUrl="" ClientIDMode="Static"></asp:HyperLink>"</p>
                            </div>
                            <div class="suggestions-txt">
                                <p class="keyword">
                                    Suggestions:</p>
                                <p>
                                    -Make sure that all words are spelled correctly.</p>
                                <p>
                                    -Try different keywords.</p>
                                <p>
                                    -Try more general keywords.</p>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="tab2">
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
