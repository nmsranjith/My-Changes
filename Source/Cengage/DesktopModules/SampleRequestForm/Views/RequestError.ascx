<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestError.ascx.cs"
    Inherits="DotNetNuke.Modules.SampleRequestForm.Views.RequestError" %>
<div class="content_wrapper request-login">
    <div class="container">
        <div class="row bread-crumb">
            <a id="SecLvlDisciplineLnk" runat="server" href="#" class="bread-crumb_link">[Second
                Level Disipline]</a> <span class="ico-arrow">></span> <a id="ThirdLvlDisciplineLnk"
                    runat="server" href="#" class="bread-crumb_link paddingL30">[Third Level Disipline]</a>
            <span class="ico-arrow">></span> <span id="SRFProductName" runat="server" class="bread-crumb_label paddingL30">
                [Product Name]</span>
        </div>
        <div class="row success-inner">
            <h3>
                Sorry</h3>
            <p>
                Your sample request has failed due to an unexpected technical error, we apologise sincerely for this.
                <a id="ContactRep" runat="server" href="#" class="cnt-us">
                        Please contact customer service.</a>
            </p>			
            <p>
                if the request is urgent. We have been alerted to this error and will work to resolve
                it ASAP - please try to lodge this request again later today.</p>
        </div>
    </div>
</div>
