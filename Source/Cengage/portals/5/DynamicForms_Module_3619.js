/***********************************************
* Data Springs Custom Javascript File
* Insert any custom client side javascript to be rendered for Dynamic Forms
* for demonstrations of Dynamic Forms please visit: http://www.datasprings.com/Products/DNNModules/DynamicForms/DynamicFormsDemonstration1/tabid/754/Default.aspx
***********************************************/


/***********************************************
* This function call is used to add validate that a field is an integer. 
* Example use: validateNumber($(DynamicForms_ShortFieldName))
***********************************************/


function validateNumber(fieldvalue){
if (!isNaN(fieldvalue) && (fieldvalue>0))
alert('This is a number greater than 0');
else
alert('This is not a number greater than 0');
}

/***********************************************
* This function call is used to add two decimal places to a field. For example 50.4 would be returned as 50.40 
* Example use: toTwoDecimals($(DynamicForms_ShortFieldName))
***********************************************/

function toTwoDecimals(n) {
  var s = "" + Math.round(n * 100) / 100
  var i = s.indexOf('.')
  if (i < 0) return s + ".00"
  var t = s.substring(0, i + 1) + s.substring(i + 1, i + 3)
  if (i + 2 == s.length) t += "0"
  return t
}


/***********************************************
* This function call is used to add commas to a number. For example 100000 would be returned as 100,000 
* Example use: addCommas($(DynamicForms_ShortFieldName))
***********************************************/

function addCommas(nStr)
{
	nStr += '';
	x = nStr.split('.');
	x1 = x[0];
	x2 = x.length > 1 ? '.' + x[1] : '';
	var rgx = /(\d+)(\d{3})/;
	while (rgx.test(x1)) {
		x1 = x1.replace(rgx, '$1' + ',' + '$2');
	}
	return x1 + x2;
}


/***********************************************
* This function call is used to return the value of a radio button. This function can be called for using calculations with radio buttons
* Example use: funcRadioCalc($(ShortFieldName_FieldID))
***********************************************/


function  funcRadioCalc(RadioName)

 {
var chkList1= document.getElementById (RadioName);
var arrayOfCheckBoxes= chkList1.getElementsByTagName("input");
for(var i=0;i<arrayOfCheckBoxes.length;i++)
{

if (arrayOfCheckBoxes[i].checked) {
return arrayOfCheckBoxes[i].value;
}
  }
}



function  fucRadioCalc(RadioName)

 {
var chkList1= document.getElementById (RadioName);
var arrayOfCheckBoxes= chkList1.getElementsByTagName("input");
for(var i=0;i<arrayOfCheckBoxes.length;i++)
{

if (arrayOfCheckBoxes[i].checked) {
return arrayOfCheckBoxes[i].value;
}
  }
}



 /***********************************************
* This is a client side event function call is used to verify the email address via a client side event 
* Example use: verifyEmail($(ShortFieldName))
***********************************************/

function verifyEmail(emaddress){
var status = false;    
var emailRegEx = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
     if (emaddress.search(emailRegEx) == -1) {
          alert("Please enter a valid email address.");
     }
     return status;
}



 /***********************************************
* This is a client side event function call is used to verify the a required field via a client side event 
* Example use: Verifyrequired($(ShortFieldName), 'Your alert message here'))
***********************************************/


function Verifyrequired(reqfield,alerttxt){
var status = false;
 if (reqfield.value==null||reqfield.value==""){
 alert(alerttxt);
 }
 return status;
}



/***********************************************
* This function call is used to calculate the total number of characters and notify the user that they exceeded their limitations. For more details
*please  visit this URL: http://www.datasprings.com/Products/DNNModules/DynamicForms/DynamicFormsDemonstration6/tabid/842/Default.aspx
***********************************************/


 function countChars(dId,txtVal,limit)
  {
      var totalLen = txtVal.length; 
      if (totalLen < limit)
      {
          document.getElementById(dId).innerHTML = "<font color='red'>You have used " + totalLen + " of " + limit + " characters available.<br>You have " + (limit - totalLen) + " characters remaining.</font>"; 
      }
      else
      {
          document.getElementById(dId).innerHTML = "<font color='red'>You have exceeded the character limit for this field.</font>";
      }
  }




/***********************************************
* This function call will format the number into a currency format
***********************************************/



function formatCurrency(num) {
num = num.toString().replace(/\$|\,/g,'');
if(isNaN(num))
num = "0";
sign = (num == (num = Math.abs(num)));
num = Math.floor(num*100+0.50000000001);
cents = num%100;
num = Math.floor(num/100).toString();
if(cents<10)
cents = "0" + cents;
for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
num = num.substring(0,num.length-(4*i+3))+','+
num.substring(num.length-(4*i+3));
return (((sign)?'':'-') + '$' + num + '.' + cents);
}



function validate_required(field,alerttxt)
{
with (field)
{
if (value==null||value=="")
{
alert(alerttxt);return false;
}
else
{
return true;
}
}
} 
function validate_email(field,alerttxt)
{
with (field)
{
apos=value.indexOf("@");
dotpos=value.lastIndexOf(".");
if (apos<1||dotpos-apos<2) 
{alert(alerttxt);return false;}
else {return true;}
}
}

function CalculateCheckBoxList(CheckBoxList) {
var objChkBoxLst = document.getElementById(CheckBoxList);
if(objChkBoxLst) {
var objChkBoxes = objChkBoxLst.getElementsByTagName('input');
var objChkLabels = objChkBoxLst.getElementsByTagName('label');
var i; var numSum = 0;            
for(i=0;i<objChkBoxes.length;i++) {                    
if(objChkBoxes[i].checked) {                    
numSum += GetLabelValue(objChkLabels, objChkBoxes[i].id);
}
}
}
numSum = Math.round(numSum * 100)/100;                
return (numSum);
}
function GetLabelValue(objLabelList, strForValue) {
if(objLabelList) {
var i; var numActualVal; var strElemForValue;
for(i=0;i<objLabelList.length;i++) {
if(objLabelList[i].htmlFor)
strElemForValue = objLabelList[i].htmlFor;
else
strElemForValue = objLabelList[i].getAttribute('for');
if(strElemForValue==strForValue) {                        
numActualVal = objLabelList[i].innerHTML;
if(numActualVal.indexOf('$')!=-1); {
numActualVal = numActualVal.substring(numActualVal.indexOf('$')+1, numActualVal.length);
} 
return(numActualVal*1);
}
}
}
return(0);
}
function CalculateCheckBoxListValues(CheckBoxList, CheckBoxValField) {
var objChkBoxLst = document.getElementById(CheckBoxList);
var objChkBoxVals = document.getElementById(CheckBoxValField);
var arrChkBoxVals = '';
if(objChkBoxVals)
arrChkBoxVals = objChkBoxVals.value.split('|');
if(objChkBoxLst) {
var objChkBoxes = objChkBoxLst.getElementsByTagName('input');                
var i; var numSum = 0;            
for(i=0;i<objChkBoxes.length;i++) {                    
if(objChkBoxes[i].checked) {                    
numSum += GetCBValue(i, arrChkBoxVals);
}
}
}
numSum = Math.round(numSum * 100)/100;                
return (numSum);
}
function GetCBValue(intIndex, arrChkValues) {            
if(arrChkValues) {
var i;
var arrTempVals;
var numRetVal = 0;
for(i=0;i<arrChkValues.length;i++) {
arrTempVals = arrChkValues[i].split(':');
if(arrTempVals) {
if(arrTempVals[0]==intIndex) {
numRetVal = arrTempVals[1];
if(numRetVal.indexOf('$')!=-1); {
numRetVal = numRetVal.substring(numRetVal.indexOf('$')+1, numRetVal.length);
} 
return(numRetVal*1);
}                        
}
}
}
return(0);
}      
/***********************************************
* Show Hint script- � Dynamic Drive (www.dynamicdrive.com)
* This notice MUST stay intact for legal use
* Visit http://www.dynamicdrive.com/ for this script and 100s more.
***********************************************/
		
var horizontal_offset="9px" //horizontal offset of hint box from anchor link

/////No further editting needed

var vertical_offset="0" //horizontal offset of hint box from anchor link. No need to change.
var ie=document.all
var ns6=document.getElementById&&!document.all

function getposOffset(what, offsettype){
var totaloffset=(offsettype=="left")? what.offsetLeft : what.offsetTop;
var parentEl=what.offsetParent;
while (parentEl!=null){
totaloffset=(offsettype=="left")? totaloffset+parentEl.offsetLeft : totaloffset+parentEl.offsetTop;
parentEl=parentEl.offsetParent;
}
return totaloffset;
}

function iecompattest(){
return (document.compatMode && document.compatMode!="BackCompat")? document.documentElement : document.body
}

function clearbrowseredge(obj, whichedge){
var edgeoffset=(whichedge=="rightedge")? parseInt(horizontal_offset)*-1 : parseInt(vertical_offset)*-1
if (whichedge=="rightedge"){
var windowedge=ie && !window.opera? iecompattest().scrollLeft+iecompattest().clientWidth-30 : window.pageXOffset+window.innerWidth-40
dropmenuobj.contentmeasure=dropmenuobj.offsetWidth
if (windowedge-dropmenuobj.x < dropmenuobj.contentmeasure)
edgeoffset=dropmenuobj.contentmeasure+obj.offsetWidth+parseInt(horizontal_offset)
}
else{
var windowedge=ie && !window.opera? iecompattest().scrollTop+iecompattest().clientHeight-15 : window.pageYOffset+window.innerHeight-18
dropmenuobj.contentmeasure=dropmenuobj.offsetHeight
if (windowedge-dropmenuobj.y < dropmenuobj.contentmeasure)
edgeoffset=dropmenuobj.contentmeasure-obj.offsetHeight
}
return edgeoffset
}

function showhint(menucontents, obj, e, tipwidth){
if ((ie||ns6) && document.getElementById("hintbox")){
dropmenuobj=document.getElementById("hintbox")
dropmenuobj.innerHTML=menucontents
dropmenuobj.style.left=dropmenuobj.style.top=-500
if (tipwidth!=""){
dropmenuobj.widthobj=dropmenuobj.style
dropmenuobj.widthobj.width=tipwidth
}
dropmenuobj.x=getposOffset(obj, "left")
dropmenuobj.y=getposOffset(obj, "top")
dropmenuobj.style.left=dropmenuobj.x-clearbrowseredge(obj, "rightedge")+obj.offsetWidth+"px"
dropmenuobj.style.top=dropmenuobj.y-clearbrowseredge(obj, "bottomedge")+"px"
dropmenuobj.style.visibility="visible"
obj.onmouseout=hidetip
}
}

function hidetip(e){
dropmenuobj.style.visibility="hidden"
dropmenuobj.style.left="-500px"
}

function createhintbox(){
var divblock=document.createElement("div")
divblock.setAttribute("id", "hintbox")
document.body.appendChild(divblock)
}

if (window.addEventListener)
window.addEventListener("load", createhintbox, false)
else if (window.attachEvent)
window.attachEvent("onload", createhintbox)
else if (document.getElementById)
window.onload=createhintbox



