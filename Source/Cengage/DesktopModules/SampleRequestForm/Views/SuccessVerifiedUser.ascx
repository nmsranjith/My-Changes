<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuccessVerifiedUser.ascx.cs"
    Inherits="DotNetNuke.Modules.SampleRequestForm.Views.SuccessVerifiedUser" %>
<div class="content_wrapper request-login">
    <div class="container">        
        <div class="row bread-crumb">
            <a id="SecLvlDisciplineLnk" runat="server" href="#" class="bread-crumb_link">[Second Level Disipline]</a> <span class="ico-arrow">
                ></span> <a id="ThirdLvlDisciplineLnk" runat="server" href="#" class="bread-crumb_link paddingL30">[Third Level Disipline]</a>
            <span class="ico-arrow">></span> <span id="SRFProductName" runat="server" class="bread-crumb_label paddingL30">[Product
                Name]</span>
        </div>
        <div class="row success-inner">
            <h3 id="SuccessHeader" runat="server" visible="false">
                Your request has been sent successfully</h3>
            <h3 id="ErrorHeader" runat="server" visible="false">
                Your request has been sent successfully</h3>
            <p>                
                <span id="Error_Msg1"  runat="server" visible="false">There was an issue processing your request for your electronic resource items. Your request for print items has been forwarded to your rep. We are looking into the issue with the failed electronic resource samples now.</span>
                <span id="Error_Msg2"  runat="server" visible="false">There was an issue processing your request for some printed & electronic resource items. Your other electronic items have been processed and can be accessed from the link below. We are looking into the issue with the failed print & electronic resource samples now.</span>
                <span id="Error_Msg3"  runat="server" visible="false">There was an issue processing your request for printed & eBook items. Your other electronic items have been processed and can be accessed from the link below. We are looking into the issue with the failed print & eBook samples now.</span>
                <span id="Error_Msg4"  runat="server" visible="false">There was an issue processing your request for your electronic resource items. Your other electronic items have been processed and can be accessed from the link below. Your request for print items has been forwarded to your rep. We are looking into the issue with the failed electronic resource samples now.</span>
                <span id="Error_Msg5"  runat="server" visible="false">There was an issue processing your request for your eBook & electronic resource items. Your other electronic items have been processed and can be accessed from the link below. Your request for print items has been forwarded to your rep. We are looking into the issue with the failed electronic resource samples now.</span>
                <span id="Error_Msg6"  runat="server" visible="false">There was an issue processing your request for one or more of the Electronic instructor resources. Your other electronic items processed correctly and can be accessed from the link below. Your request for print items has been forwarded to your rep. We are looking into the issue with the failed electronic resource sample now.</span>
                <span id="Error_Msg7"  runat="server" visible="false">There was an issue processing your request for the eBook. Your other electronic items processed correctly and can be accessed from the link below. Your request for print items has been forwarded to your rep. We are looking into the issue with the failed eBook sample now.</span>
                <span id="Error_Msg8"  runat="server" visible="false">There was an issue processing your request for printed items. Your other electronic items have been processed and can be accessed from the link below. We are looking into the issue with the failed print sample now.</span>
                <span id="Error_Msg9"  runat="server" visible="false">There was an issue processing your request for your eBook & electronic resource items. Your request for print items has been forwarded to your rep. We are looking into the issue with the failed electronic resource samples now.</span>
                <span id="Error_Msg10"  runat="server" visible="false">There was an issue processing your request for your electronic resource items. Your request for eBook items has been processed and can be accessed from the link below. We are looking into the issue with the failed electronic resource samples now.</span>
                <span id="Error_Msg11"  runat="server" visible="false">There was an issue processing your request for your eBook & electronic resource items. Your other electronic items have been processed and can be accessed from the link below. We are looking into the issue with the failed electronic resource samples now.</span>
                <span id="Error_Msg12"  runat="server" visible="false">There was an issue processing your request for one or more of the Electronic instructor resources. Your other electronic items processed correctly and can be accessed from the link below. We are looking into the issue with the failed electronic resource sample now.</span>
                <span id="Error_Msg13"  runat="server" visible="false">There was an issue processing your request for the eBook. Your other electronic items processed correctly and can be accessed from the link below. We are looking into the issue with the failed eBook sample now.</span>
                <span id="Error_Msg14"  runat="server" visible="false">There was an issue processing your request for one or more of the Electronic instructor resources. Your request for print items has been forwarded to your rep. We are looking into the issue with the failed electronic resource sample now.</span>
            </p>
            <p class="msg">
                Thank you for requesting a sample copy of a Cengage Learning textbook, eBook or learning
                resource. We'll get it to you as soon as we can.
            </p>
            <h4>
                Print books</h4>
            <p class="msg">
                Usually arrive within 3-7 days if they are available in our warehouse, or around
                3 weeks if we need to order it in from overseas for you.</p>
            <h4 class="dresource">
                eBooks and digital resources:</h4>
            <p class="msg">
                <a href="/dashboard/tab/1" class="cnt-us access-dashboard">Can be accessed instantly from your Dashboard
                    ></a></p>
            <p class="msg">
                If you need more information, to check the status of your request or if we can help
                with anything else, <a id="ContactRep" runat="server" href="#" class="cnt-us">please contact your Representative.</a></p>
            <p class="msg">
                When you receive the item, please let us know if it's suitable for your teaching!</p>
        </div>
    </div>
</div>
