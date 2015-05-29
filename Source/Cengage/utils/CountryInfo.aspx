<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CountryInfo.aspx.cs" Inherits="CountryInfo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        *
        {
            margin: 0;
            padding: 0;
            font-family: arial,helvetica,sans-serif;
            color: #666;
            font-size: 12px;
        }
        .container
        {
            width: 974px;
            margin-left: auto;
            margin-right: auto;
        }
        
        #light_wrapper
        {
            position: relative;
            background: url(bg.jpg) repeat;
            margin: 0 auto;
            margin: 0 auto;
            min-height: 100%;
            height: 100%;
            text-align: left;
        }
        #hd
        {
            position: relative;
            height: 100px;
            top: 10px;
        }
        #logo
        {
            position: absolute;
            left: 5px;
            background: url('CengageBrain.png');
            display: block;
            width: 127px;
            height: 72px;
            text-indent: -9999px;
        }
        
        
        #bdHead
        {
            background-image: url('bdHeadBg.png');
            background-repeat: no-repeat;
            background-position: top left;
            width: 984px;
            height: 11px;
        }
        
        #bdBody
        {
            background-image: url('bdBodyBg.png');
            background-repeat: repeat-y;
            background-position: top left;
            width: 984px;
            _width: 980px;
            background-color: #ffffff;
        }
        
        #bdFoot
        {
            background-image: url('bdFootBg.png');
            background-repeat: no-repeat;
            background-position: top left;
            width: 984px;
            height: 11px;
        }
        #ft
        {
            clear: both;
            margin-top: 30px;
            border-top: 1px solid #ccc;
            position: relative; /*padding: 0 7px;*/
        }
        #ft.nonCommerce
        {
            margin-top: 50px;
        }
        #ft #copyright
        {
            position: absolute;
            top: -20px;
            color: #999999;
            left: 0;
        }
        
        .medium_green_button
        {
            border: 1px solid #34740e;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            font-size: 12px;
            font-family: arial, helvetica, sans-serif;
            padding: 6px 10px 6px 10px;
            text-decoration: none;
            display: inline-block;
            text-shadow: -1px -1px 0 rgba(0,0,0,0.3);
            font-weight: bold;
            color: #FFFFFF;
            background-color: #4ba614;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#4ba614), to(#008c00));
            background-image: -webkit-linear-gradient(top, #4ba614, #008c00);
            background-image: -moz-linear-gradient(top, #4ba614, #008c00);
            background-image: -ms-linear-gradient(top, #4ba614, #008c00);
            background-image: -o-linear-gradient(top, #4ba614, #008c00);
            background-image: linear-gradient(to bottom, #4ba614, #008c00);
            filter: progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#4ba614, endColorstr=#008c00);
        }
        
        .medium_green_button:hover
        {
            border: 1px solid #224b09;
            background-color: #36780f;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#36780f), to(#005900));
            background-image: -webkit-linear-gradient(top, #36780f, #005900);
            background-image: -moz-linear-gradient(top, #36780f, #005900);
            background-image: -ms-linear-gradient(top, #36780f, #005900);
            background-image: -o-linear-gradient(top, #36780f, #005900);
            background-image: linear-gradient(to bottom, #36780f, #005900);
            filter: progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#36780f, endColorstr=#005900);
        }
        
.CSSTableGenerator { 
	margin:0 auto;
	padding:20px;	width:80%;
	box-shadow: 5px 5px 5px #888888;
	border:1px solid #000000;	
	-moz-border-radius-bottomleft:0px;
	-webkit-border-bottom-left-radius:0px;
	border-bottom-left-radius:0px;	
	-moz-border-radius-bottomright:0px;
	-webkit-border-bottom-right-radius:0px;
	border-bottom-right-radius:0px;	
	-moz-border-radius-topright:0px;
	-webkit-border-top-right-radius:0px;
	border-top-right-radius:0px;
		-moz-border-radius-topleft:0px;
	-webkit-border-top-left-radius:0px;
	border-top-left-radius:0px;
}.CSSTableGenerator table{
    border-collapse: collapse;
        border-spacing: 0;
	width:80%;
	height:100%;
	margin:0px;padding:0px;
}.CSSTableGenerator tr:last-child td:last-child {
	-moz-border-radius-bottomright:0px;
	-webkit-border-bottom-right-radius:0px;
	border-bottom-right-radius:0px;
}
.CSSTableGenerator table tr:first-child td:first-child {
	-moz-border-radius-topleft:0px;
	-webkit-border-top-left-radius:0px;
	border-top-left-radius:0px;
}
.CSSTableGenerator table tr:first-child td:last-child {
	-moz-border-radius-topright:0px;
	-webkit-border-top-right-radius:0px;
	border-top-right-radius:0px;
}.CSSTableGenerator tr:last-child td:first-child{
	-moz-border-radius-bottomleft:0px;
	-webkit-border-bottom-left-radius:0px;
	border-bottom-left-radius:0px;
}.CSSTableGenerator tr:hover td{
	
}
.CSSTableGenerator tr:nth-child(odd){ background-color:#ffffff; }
.CSSTableGenerator tr:nth-child(even)    { background-color:#cecece; }.CSSTableGenerator td{
	vertical-align:middle;
	border:1px solid #000000;
	border-width:0px 1px 1px 0px;
	text-align:left;
	padding:11px;
	font-size:14px;
	font-family:Arial;
	font-weight:normal;
	color:#000000;
}.CSSTableGenerator tr:last-child td{
	border-width:0px 1px 0px 0px;
}.CSSTableGenerator tr td:last-child{
	border-width:0px 0px 1px 0px;
}.CSSTableGenerator tr:last-child td:last-child{
	border-width:0px 0px 0px 0px;
}

.CSSTableGenerator th
{
text-align:center;
font-size:14px;
padding:10px;
}
.CSSTableGenerator tr:first-child td{
		background:-o-linear-gradient(bottom, #75c475 5%, #75c475 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #75c475), color-stop(1, #75c475) );
	background:-moz-linear-gradient( center top, #75c475 5%, #75c475 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr="#75c475", endColorstr="#75c475");	background: -o-linear-gradient(top,#75c475,75c475);
	background-color:#75c475;
	border:0px solid #000000;
	text-align:center;
	border-width:0px 0px 1px 1px;
	font-size:14px;
	font-family:Arial;
	font-weight:bold;
	color:#ffffff;
}
.CSSTableGenerator tr:first-child:hover td{
	background:-o-linear-gradient(bottom, #75c475 5%, #75c475 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #75c475), color-stop(1, #75c475) );
	background:-moz-linear-gradient( center top, #75c475 5%, #75c475 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr="#75c475", endColorstr="#75c475");	background: -o-linear-gradient(top,#75c475,75c475);

	background-color:#75c475;
}
.CSSTableGenerator tr:first-child td:first-child{
	border-width:0px 0px 1px 0px;
}
.CSSTableGenerator tr:first-child td:last-child{
	border-width:0px 0px 1px 1px;
}
        .buttons
        {
            padding: 20px;
            margin: 0 auto;
            width: 80%;
        }
    </style>
</head>
<body>
    <div id="light_wrapper">
        <div class="container">
            <div id="bdHead">
            </div>
            <div id="bdBody">
                <form id="form1" runat="server">
                <div class="buttons">
                    <asp:Button ID="countrydetails" CssClass="medium_green_button" runat="server" Text="GetCountryInfo"
                        OnClick="countrydetails_Click" />
                    <asp:Button ID="SimuateNZ" runat="server" Text="Simulate NZ" CssClass="medium_green_button"
                        OnClick="SimuateNZ_Click" />
                    <asp:Button ID="SimuateAUS" runat="server" Text="SimulateAUS" CssClass="medium_green_button"
                        OnClick="SimuateAUS_Click" />
                    <asp:Button ID="SimuateFIJI" runat="server" Text="Simulate FIJI" CssClass="medium_green_button"
                        OnClick="SimuatePacificCountries_Click" />
                    <asp:Button ID="SimuateInternational" runat="server" Text="Simulate International"
                        CssClass="medium_green_button" OnClick="SimuateInternational_Click" />
                </div>
                <asp:GridView ID="CountryGridView" runat="server" CssClass="CSSTableGenerator">
                </asp:GridView>
                </form>
            </div>
            <div id="bdFoot">
            </div>
            <div id="ft" class="nonCommerce">
                <span id="copyright">© <strong>2014 Cengage Learning </strong></span>
                <img src="cengage_footer.png" alt="Cengage Learning Logo" class="padTop">
            </div>
        </div>
    </div>
</body>
</html>
