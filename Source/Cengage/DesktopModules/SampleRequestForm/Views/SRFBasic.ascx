<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SRFBasic.ascx.cs" Inherits="DotNetNuke.Modules.SampleRequestForm.Views.SRFBasic" %>
<div id="overlay"></div>

<div id="progressBar" class="loadergif loadergifsize">
    <img src="/Portals/0/images/ajax-loader1.gif" class="loadergifsize"/>
</div>
<div class="load-srf">
	<div class="overlay-srf"></div>
<div class="sample-request-load"> 

 <div></div>
<div></div>
<div></div>
<div></div>
</div>
</div>


<div id="WrongRepPopup" class="HideItems save-search-name">
    <div class="favpopup">
        <h1>
            Invalid Sales Rep Selection</h1>
        <div class="save-cont">
            <p>
                Please select a valid division and rep.
            </p>            
        </div>   
		<div class="button">
            <button class="btn btn-affermative" type="button" id="SRFOKBtn">
                Ok</button>           
        </div>		
    </div>
</div>
<div id="SalesRepPopup" class="HideItems save-search-name">
    <div class="favpopup">
        <h1>
            Sales Rep</h1>
        <div class="save-cont">
            <p>
                Please select a sales rep.
            </p>            
        </div>   
		<div class="button">
            <button class="btn btn-affermative" type="button" id="SRFOKBtn1">
                Ok</button>           
        </div>		
    </div>
</div>
<div id="SampleBeforeSuccessPopup" class="HideItems save-search-name">
    <div class="favpopup">
        <h1>
            Continue sample request</h1>
        <div class="save-cont">
            <p>
                <%--Your request will be processed faster if you have a valid Lecturer Account. If you don’t have a Lecturer Account it may take up to 3 weeks for us to respond. Please contact our hotline (1800-355-9983) if you need this urgently.--%>				
				Your request will be processed much faster if you have a valid lecturer account. Please contact customer service if this is needed urgently.
				</p>  
				<p>anz.customerservice@cengage.com</p>  
				<p>AU: 1300 790 853</p>  
				<p>NZ: 0800 449 725</p>                        
        </div>   
		<div class="button">
            <button class="btn btn-cancel" type="button" id="SRFCancelButton">
                Cancel</button>
            <button class="btn btn-affermative" type="button" id="SRFOKBtn2">
                Ok</button>     
        </div>		
    </div>
</div>
<div id="SRFInstructorPopUp" class="HideItems">
    <div class="favpopup">
        <h1>
            Your email address is already
registered - Login for instant access</h1>
        <div class="save-cont">
            <p>
               Cengage Learning verifies all requests before sharing instructor materials. We already have your email address registered as an instructor.
				</p>  
				<p>Logging in may enable you to access resources instantly.</p> 
        </div>   
		<div class="button">
            <button class="btn btn-cancel" type="button" id="SRFNoThxButton">
                No,thanks</button>
            <button class="btn btn-affermative" type="button" id="SRFLgnBtn">
                Login</button>     
        </div>		
    </div>
</div>
<div class="request-login">
    <div class="container">
        <div class="row bread-crumb">
            <a id="SecLvlDisciplineLnk" runat="server" href="#" class="bread-crumb_link">[Second Level Disipline]</a> <span class="ico-arrow">
                ></span> <a id="ThirdLvlDisciplineLnk" runat="server" href="#" class="bread-crumb_link paddingL30">[Third Level Disipline]</a>
            <span class="ico-arrow">></span> <span id="SRFProductName" runat="server" class="bread-crumb_label paddingL30">[Product
                Name]</span>
        </div>
        <div class="row img-section border-btm">
            <h3>
                Instructor sample materials</h3>
            <p id="SRFLoggedInPara" runat="server" clientidmode="Static">
                Your Cengage Learning Instructor account provides speedy access to resources.</p>
            <div class="controls">
              <div id="SRFLoggedOutDiv" runat="server">  <p class="pull-left">
                    Have a Cengage account?</p>
                <a href="#" onclick="SRFLoginClick()"  class="SRFLoginLnk">Login</a>
				</div>
            <p id="SRFLoggedOutPara2" runat="server" class="srfloggedoutp">
                If you have a Cengage Instructor account already we can instantly verify you, saving you time and hassle</p></div>
            <div class="col-md-6  width50 leftside-div pull-left">
                <div class="control-group">
                    <label for="SRFPersonalName" class="control-label">
                        Your name:</label>
                    <div class="controls">
                        <input type="text" maxlength="100" autocomplete="off" id="SRFPersonalName"
                            runat="server" clientidmode="Static" /></div>
                    <span class="ico-mandatory"></span>
					<div id="SRFPersonalNameerror" class="srferrors alert alert-primary HideItems">
						
							<span class="ico-error pull-left"></span><span class="error-text">Please enter your name.</span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
								</span>
						
					</div>
                </div>
                
                <div class="control-group ">
                    <label for="SRFEmailid" class="control-label">
                        Email:</label>
                    <div class="controls">
                        <input type="text" maxlength="254" placeholder="eg.name@domain.com" 
                            autocomplete="off" id="SRFEmailid" runat="server" clientidmode="Static"/></div>
                    <span class="ico-mandatory"></span>
					<div id="SRFEmailiderror" class="srferrors alert alert-primary HideItems">
						
							<span class="ico-error pull-left"></span><span class="error-text">Please enter your email address. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
								</span>
						
					</div>
					<div id="SRFEmailInvaliderror" class="srferrors alert alert-primary HideItems">
						
							<span class="ico-error pull-left"></span><span class="error-text">Please enter a vaild email address. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
								</span>
						
					</div>
                </div>
                <p class="pre-cnt">
                    Preferred contact:</p>
                <div class="control-group mobile-div">
                    <select class="mobile" id="SRFContactDpn" runat="server" clientidmode="Static">
                        <option value="0">Please select</option>
                        <option value="1">Mobile</option>
                        <option value="2">Work</option>
                        <option value="3">Email</option>
                    </select>
                    <div class="controls">
                        <input type="text" maxlength="254" autocomplete="off" id="SRFPhone" onkeydown="javascript:SRFContactKeyDown(SRFContactDpn);" runat="server"
                            clientidmode="Static" /></div>
                    <span class="ico-mandatory"></span>
					<div id="SRFPhoneerror" class="srferrors alert alert-primary HideItems">
						
							<span class="ico-error pull-left"></span><span class="error-text">Please enter your contact number. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
								</span>
						
					</div>
					<div id="SRFPhoneerror1" class="srferrors alert alert-primary HideItems">
						
							<span class="ico-error pull-left"></span><span class="error-text">Please select contact type. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
								</span>
						
					</div>
					<div id="SRFPhoneerror2" class="srferrors alert alert-primary HideItems">
						
							<span class="ico-error pull-left"></span><span class="error-text">Please enter a vaild email address. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
								</span>
						
					</div>
					<input type="hidden" runat="server" id="ContactMobNumber" clientidmode="Static" />
					<input type="hidden" runat="server" id="ContactWorkNumber" clientidmode="Static" />
                </div>
				<div id="SRFAddressDiv" runat="server" clientidmode="Static" class="address-info">
				<p class="pre-cnt">
                    Shipping address:</p>
					<div class="control-group tip cname-div" >
						<label for="SRFShippingAddressTxt1" class="control-label ">
							Street:</label>
						<div class="controls">
							<input type="text" maxlength="30" autocomplete="off" id="SRFShippingAddressTxt1" placeholder="line 1" 
								runat="server" clientidmode="Static" />
						</div>
						<span class="ico-mandatory"></span>
						<div class="controls pad-left94">
							<input type="text" maxlength="30" autocomplete="off" id="SRFShippingAddressTxt2" placeholder="line 2" class="ship-adr1 address-line"
								runat="server" clientidmode="Static" />
						</div>
						<div class="controls pad-left94">
							<input type="text" maxlength="30" autocomplete="off" id="SRFShippingAddressTxt3" placeholder="line 3" class="ship-adr2 address-line"
								runat="server" clientidmode="Static" />
						</div>                  
						<div id="SRFShippingAddressError1" class="srferrors alert alert-primary HideItems">
							
								<span class="ico-error pull-left"></span><span class="error-text">Please enter shipping
									address. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
									</span>
							
						</div>
					</div>
					
					
					<div class="control-group tip cname-div" >
						<div class="sub-rub">
							<label for="SRFSuburbTxt" class="control-label">
								Suburb:</label>
							<div class="controls">
								<input type="text" maxlength="20" autocomplete="off" id="SRFSuburbTxt"
									runat="server" clientidmode="Static" />
							</div>
						</div>
						<div class="post-code">
							<label for="SRFPostcode" class="control-label ">
								Postcode:</label>
							<div class="controls">
								<input type="text" maxlength="9" autocomplete="off" id="SRFPostcode"
									runat="server" clientidmode="Static" onkeydown="javascript:validationnumeric();"/>
							</div>
							<span class="ico-mandatory"></span>
							
						</div>
					</div>					
					<div id="SRFsubrubpostError" class="srferrors alert alert-primary HideItems" >
						<span class="ico-error pull-left"></span><span class="error-text" id="SRFSubUrbErrorSpan">Please enter value.
						</span><span class="ico-error-close pull-right"> </span>
					</div>
					<div id="SRFsubrubpostError1" class="HideItems" ></div>
					<div  class="control-group tip cname-div">
						<div   id="SRFStateDiv" runat="server" clientidmode="Static" class="state-name">
							<div class="controls" >
								<select id="SRFStateDpn" class="H4Light" runat="server" clientidmode="Static">
									<option value="0">Please select</option>
									<option value="1">NSW</option>
									<option value="2">VIC</option>
									<option value="3">QLD</option>
									<option value="4">SA</option>
									<option value="5">WA</option>
									<option value="6">TAS</option>
									<option value="7">NT</option>
									<option value="8">ACT</option>
								</select>
							</div>
						</div>
						<div class="country-name">
							<div class="controls">                       
								<asp:DropDownList ID="SRFCountry" CssClass="H4Light" ClientIDMode="Static" DataTextField="COUNTRY_NAME" DataValueField="COUNTRY_CODE" runat="server">
								</asp:DropDownList>
							</div>
						</div>
						<span class="ico-mandatory"></span>							
					</div>
					<div id="SRFStateError" class="srferrors alert alert-primary HideItems" >
						<span class="ico-error pull-left"></span><span class="error-text">Please select a state.
						</span><span class="ico-error-close pull-right" ></span>
					</div>
				</div>
                <div class="control-group tip cname-div">
                    <label for="SRFcourseName" class="control-label cname-label">
                        Course name:</label>
                    <div class="controls">
                        <input type="text" maxlength="100" autocomplete="off" id="SRFcourseName"
                            runat="server" clientidmode="Static" />
                    </div>
                    <span class="ico-mandatory"></span>
					<div id="SRFcourseNameerror" class="srferrors alert alert-primary HideItems">
						
							<span class="ico-error pull-left"></span><span class="error-text">Please enter course name. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
								</span>
						
					</div>
                </div>
                
                <div class="control-group tip">
                    <label for="SRFcoursesCode" class="control-label ccode-label">
                        Course code:</label>
                    <div class="controls">
                        <input type="text" maxlength="100" autocomplete="off" id="SRFcoursesCode"
                            runat="server" clientidmode="Static" />
                    </div>
                    <span class="ico-mandatory"></span>
					<div id="SRFcoursesCodeerror" class="srferrors alert alert-primary HideItems">
						
							<span class="ico-error pull-left"></span><span class="error-text">Please enter course code. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
								</span>
						
					</div>
                </div>
                
                <p class="marginb0">
                    For use in:</p>
                <div class="control-group">
                    <div id="SemesterDiv" class="control-label">
                        <select class="H4Light" id="SRFSemesterDpn" runat="server" clientidmode="Static">
                            <option value="0">Select semester</option>
                            <option value="1">Semester/Trimester 1</option>
                            <option value="2">Semester/Trimester 2</option>
                            <option value="3">Trimester 3</option>
                            <option value="4">Other</option>
                        </select>
                    </div>     				
                <span class="ico-mandatory"></span>	
				<div id="SRFSemesterError" class="srferrors alert alert-primary pull-left HideItems">						
				<span class="ico-error pull-left"></span><span class="error-text">Please select a semester. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
					</span>					
				</div>
				</div>
                <div class="control-group ">
                    <label for="SRFSemesterEnrolment" class="control-label semester">
                        Semester enrolment:</label>
                    <div class="controls semestertextbox">
                        <input type="text" maxlength="5" autocomplete="off" id="SRFSemesterEnrolment"
                            runat="server" clientidmode="Static" onkeydown="javascript:validationnumeric();" onDrop="ValidateDropPasteNumeric(this)" /> <!--onpaste="ValidateDropPasteNumeric(this)" -->
                    </div>
                    <span class="ico-mandatory"></span>
					<div id="SRFSemesterEnrolmenterror" class="srferrors alert alert-primary HideItems">
						
							<span class="ico-error pull-left"></span><span class="error-text">Please enter semester enrolment. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
								</span>
						
					</div>
                </div>
                
                <div class="control-group ">
                    <label for="SRFBookCurrently" class="control-label bookcurrently">
                        Book currently in use:</label>
                    <div class="controls bookcurrentlytextbox">
                        <input type="text" maxlength="100" autocomplete="off" id="SRFBookCurrently"
                            runat="server" clientidmode="Static" />
                    </div>
                    <span class="ico-mandatory"></span>
					<div id="SRFBookCurrentlyerror" class="srferrors alert alert-primary HideItems">
						
							<span class="ico-error pull-left"></span><span class="error-text">Please enter title of the book currently in use. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
								</span>
						
					</div>
                </div>
                
                <div class="control-group tip institute">
                    <label for="SRFInstitution" class="control-label">
                        Institution:</label>
                    <div class="controls">
                        <input type="text" maxlength="100" autocomplete="off" id="SRFInstitution" runat="server" clientidmode="Static" />
                    </div>
                    <span class="ico-mandatory"></span>
					<div id="SRFInstitutionerror" class="srferrors alert alert-primary HideItems">
						
							<span class="ico-error pull-left"></span><span class="error-text">Please enter institution name. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
								</span>
						
					</div>
                </div>
                
                <div  id="SRFSecondaryEmailDiv1" runat="server"  clientidmode="Static"  class="sample-ebooks">
                    <h4>
                        To sample eBooks, we need a secondary email address. This enables you to view the eBook in the student interface - so you can see how your students experience eBooks.
						</h4>
                    
                </div>
                <div  id="SRFSecondaryEmailDiv2" runat="server"  clientidmode="Static" class="sample-ebooks">
                    <h5  id="CurrentSecEmailId" runat="server" clientidmode="Static"></h5>
                    <p>
                        For information or to update this email address please <a id="SRFContactUsLnk" href="#" runat="server" class="cnt-us">contact
                            us</a></p>
                </div>
				<div class="ProgressDivClass" style="display:none;" id="UpdateProgressImg">
					<div class="ProgressInnerDiv">
						<img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg"
							alt="Processing" />
					</div>
				</div>
                <div  id="SRFSecondaryEmailDiv" runat="server"  clientidmode="Static" visible="false" class="control-group">
                    <label for="SRFSecondaryEmail" class="control-label secondaryemail">
                        Secondary email:</label>
                    <div class="controls secondaryemailtextbox">
                        <input type="text" maxlength="254" isemailvalidate="true" autocomplete="off" id="SRFSecondaryEmail" onblur="SRFSecondaryEmailTabOut()"
                            runat="server" clientidmode="Static" />
                    </div>
                    <span class="ico-mandatory"></span>
                
                <div id="SRFSecondaryEmailError" class="srferrors alert alert-primary HideItems">
					
						<span class="ico-error pull-left"></span><span class="error-text">Please enter secondary email address. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
							</span>
					
				</div>
				<div id="SRFSecondaryValidEmailError" class="srferrors alert alert-primary HideItems">
					
						<span class="ico-error pull-left"></span><span class="error-text">Please enter a valid email address. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
							</span>
					
				</div>
				 <div id="SRFSecondaryUsedEmailError" class="srferrors alert alert-primary HideItems">
					
						<span class="ico-error pull-left"></span><span class="error-text">This email is already in use. Please enter an alternative email or contact <a href="/contact-us">Customer Service</a> </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
							</span>
					
				</div>
				 <div id="SRFSecondaryProcessEmailError" class="srferrors alert alert-primary HideItems">
					
						<span class="ico-error pull-left"></span><span class="error-text">This email is already associated with another lecturer account. Please enter a different email address. Alternatively you can enter a fake email address. e.g.yourfirstname.yourlastname@mail.com </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
						</span>
					
				</div>
				<div id="SRFSecondaryCreateEmailError" class="srferrors alert alert-primary HideItems">
					
						<span class="ico-error pull-left"></span><span class="error-text">An error occurred when processing your request. Please <a href="/contact-us">Contact Customer Service</a></span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
						</span>
					
				</div>
					</div>
                <p id="SRFSecondaryEmailPara" runat="server" visible="false" class="clr-red">
                    We will not add this email to any of our marketing lists</p>
				<div  id="SRFSecondaryPasswordDiv" visible="false" runat="server"  class="con-password HideItems"  clientidmode="Static">
					<div class="control-group ">
						<label for="SRFPassword" class="control-label password-lbl">
							Password:</label>
						<div class="controls passwordtextbox">
							<input type="password" maxlength="100" id="SRFPassword"
								runat="server" clientidmode="Static" />
						</div>
						<span class="ico-mandatory"></span>
					</div>
					<div id="SRFSecondaryPasswordError" class="srferrors alert alert-primary HideItems">
                        
                            <span class="ico-error pull-left"></span><span class="error-text">Please enter password. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
                                </span>
                        
                    </div>
					<div id="SRFSecondarySorryError" class="srferrors alert alert-primary HideItems">
                        
                            <span class="ico-error pull-left"></span><span class="error-text">We are unable to process your request at this time. Please try again later or Contact <a href="/contact-us">Customer Service</a></span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
                                </span>
                        
                    </div>
					<div id="SRFSecondaryIncorrectError" class="srferrors alert alert-primary HideItems">
                        
                            <span class="ico-error pull-left"></span><span class="error-text">The password entered is incorrect, please retry or <a href="/contact-us">Contact Customer Service</a></span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)">
                                </span>
                        
                    </div>
					<div class="control-group cnfrm-div">
						<div class="pull-right cnfrm-btn">
							<button id="SRFConfirmPassword" type="button" class="btn btn-affermative upper-case" onclick="SRFConfirmPasswordClick()" >
								CONFIRM PASSWORD</button>
						</div>
					</div>
				</div>
                <p>
                    How did you hear about this resource:</p>
                <div class="control-group">
                    <div id="ResourceDiv" class="control-label">
                        <select style="width: 100%; display: none;" class="H4Light" id="SRFResourceDpn"
                            runat="server" clientidmode="Static">
                            <option value="0">Please select</option>
                            <option value="1">Colleague or friend</option>
                            <option value="2">Cengage sales representative</option>
                            <option value="3">Browsing this site</option>
                            <option value="4">Somewhere else on the web</option>
							<option value="5">Via Cengage marketing - email</option>
							<option value="6">Via Cengage marketing - other</option>
							<option value="7">Review publication</option>
							<option value="8">Other</option>
                        </select>
                    </div>
                    <div id="ResourceControls">
                        <div id="secQuediv" style="display: none;" class="labelledtxtbox custsecquest">
                            <div class="inputStyle seque">
                                <input type="text" placeholder="Please specify a question" onclick="javascript:changestyle(this)"
                                    maxlength="100" autocomplete="off" id="SRFResourceTxt" runat="server" clientidmode="Static" />
                                <!--  onfocus="javascript:otherquesfocusstyle(this);" onblur="javascript:otherquesblurstyle(this);" -->
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 iefloat">
                    <!-- <span style="display: none;">
                        <input type="checkbox"></span> <span style="display: none;" class="ico-check" alt="">
                        </span><span style="display: inline-block;" class="ico-uncheck" alt=""></span> -->
                    <input id="SRFForTeachingChkbx" runat="server" type="checkbox" class="res-chkbox pull-left" />
                    <h6>
                        I am considering this resource for my teaching
                    </h6>
                </div>
                <div id="TeachingError">
                </div>
                <div class="col-md-12 iefloat">
                    <input id="SRFAlreadyUsingChkbx" runat="server" type="checkbox" class="res-chkbox pull-left" />
                    <h6>
                        I am already using this resource and need desk copies/help with supplements
                    </h6>
                </div>
                <div id="AlreadyUsingError">
                </div>
                <div class="col-md-12 iefloat">
                    <input id="SRFNeedHelpChkbx" runat="server" type="checkbox" class="res-chkbox pull-left" />
                    <h6>
                        I need help in accessing instructor supplements
                    </h6>
                </div>
                <div id="HelpError">
                </div>
            </div>
            <div class="col-md-6 width50 paddingL22 rightside-div pull-left">
                <h4>
                    You are requesting access to:</h4>
                  <asp:Repeater ID="SelectedFormatsRptr" runat="server">
                    <ItemTemplate>
                        <div class="col-mid prod-indv">
                            <div class="small-book  mar-top10">
                                <div class="prod-image-small pull-left">
                                    <img src='<%# Eval("ProductImageUrl")%>' alt="book "  onError="this.onerror=null;this.src='<%# string.Concat(FormatImageURL(),"HERnoimage.png") %>';"  />
                                </div>
                                <div class="book-content">
                                    <h5>
                                        <%# Eval("FormatTypeText")%></h5>
                                    <p class="book-exp">
                                        <%# Eval("Description")%></p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>          
                <div id="TeachingSupplementsTopDiv"  runat="server" class="teach-sub">
                    <h5 class="teaching">
                        Teaching supplements</h5>
                    <p id="SRFSuppMsgPara" runat="server"  class="teaching-msg"></p>
                    <div class="resource-scrolldiv customscrollbar" id="TeachingSupplementsDiv"  runat="server">
                       <asp:Repeater ID="TeachingSupplementsRptr" runat="server">
                            <ItemTemplate>
                                <div class="small-book"> 
                                    <%# BindHtml(Eval("Title")!=null?Eval("Title").ToString():string.Empty,Eval("ISBN")!=null?Eval("ISBN").ToString():string.Empty,Eval("Description")!=null?Eval("Description").ToString():string.Empty,Eval("MediaType")!=null?Eval("MediaType").ToString():string.Empty,Eval("WatchDemo")!=null?Eval("WatchDemo").ToString():string.Empty,Eval("LearnMore")!=null?Eval("LearnMore").ToString():string.Empty, Eval("IsCore") != null ? Eval("IsCore").ToString() : string.Empty)%>  
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <div class="row msg-box">
            <h4 class="message-title">
                Message</h4>
            <div class="controls big-textbox">               
				<textarea name="SRFCommentTxt" rows="10" cols="300" maxlength="3000" id="SRFCommentTxt" runat="server" clientidmode="Static" placeholder="Enter your text here.."></textarea>
            </div>
            <h3 class="srfmrgbtm">
                Please find your Sales Representative</h3>
        </div>
        <div class="pull-right srf-submit-btn HideItems">
            <asp:Button ID="SubmitRequestBtn" runat="server" CssClass="HideItems" OnClick="SubmitRequest_OnClick" ClientIDMode="Static" />			
			<input type="button" onclick="SRFSubmit()" class="btn btn-affermative upper-case pull-right HideItems" value="Send request"  />
        </div>
    </div>
</div>
<input type="hidden" id="UserTypeHdn" runat="server" clientidmode="Static" />
<input type="hidden" id="UserStatusHdn" runat="server" clientidmode="Static" />
<input type="hidden" id="SRFLgnHdn" />