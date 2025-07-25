 
/* following nembersonly(e) is added by nitin to allow user to enter only numeric values 		*/
   function getReplace(content,strSearch,strReplace) 
     {
		if(content!="" && content.length>0)//length()
		{
			var oldW = strSearch;
			var newW = strReplace;
			var temp = content;
			var pos = temp.indexOf(oldW);
			while (pos > -1) 
			{
				temp = temp.substring(0,pos) + newW +temp.substring(pos + oldW.length);
				pos = temp.indexOf(oldW,pos + newW.length);
			}
			content=temp;
		}
		return content;
	 }
	 
        function KeyPress() 
        {
        if (window.event.keyCode == 13)
        window.event.keyCode =0;
        } 

	 function ConfirmDeleteByRow()
	 {	    
	    if(confirm("Are you sure you want to delete selected record(s)?"))
	    {
	        return true;      
	    }
	    else
	    {
	        return false;
	    }
	 }
function roundNumber(num, dec) 
{
        var divident;
        if(dec == 2)
            divident = 100;
        else if(dec == 3)
            divident = 1000;
        else if(dec == 4)
            divident = 10000;
        else if(dec == 5)
            divident = 100000;
        else if(dec == 6)
            divident = 1000000;
        else if(dec == 7)
            divident = 10000000;
        else if(dec == 8)
            divident = 100000000;

        var num = num * divident;
        num = Math.round(num);
        var result = num/divident;
        return result;
}

	 
function confirmDelete(prefixname,fieldname)
{
    var isSelected=false;
    var i=2;
    
    while(1)
    {
        if( i < 10)
            formobj=eval(prefixname+"0"+i+"$"+fieldname);
        else
            formobj=eval(prefixname+i+"$"+fieldname);
            
        if( formobj == null)
            break;
        if( formobj.checked==true)
        {
            isSelected=true;
            break;
        }
        i++;
    }
    if( isSelected == true)
    {
        return confirm("Are you sure you want to delete selected record(s)?");
    }
    else
    {
        alert("No record selected");
        return false;
    }
}

  

function IsNumeric(sText)
{
   var ValidChars = "0123456789.-";
   var IsNumber=true;
   var Char;
   var count=0;//to check tat there should be only zero or one time occurance of '.'  two '.'s not allowed in float 

 
   for (i = 0; i < sText.length && IsNumber == true; i++) 
      { 
      Char = sText.charAt(i); 
      
      if( Char=='.')    //check occurances of '.'
      {
        count++;
        if( count >1)   //if morethan 1 then its not float number
        {
            IsNumber = false;
            break;
            }
            
      }
      if (ValidChars.indexOf(Char) == -1) 
         {
         IsNumber = false;
         break;
         }
      }
   return IsNumber;
   
   }

				function floatNumbersOnly(e)
				{
					var unicode=e.charCode? e.charCode : e.keyCode
					if (unicode!=8 )
					{ //if the key isn't the backspace key (which we should allow)
						if( (unicode<48||unicode>57 ) ) //if not a number
							return false //disable key press
					}
				}

				function numbersonly(e)
				{
					var unicode=e.charCode? e.charCode : e.keyCode
					if (unicode!=8)
					{ //if the key isn't the backspace key (which we should allow)
						if (unicode<48||unicode>57) //if not a number
							return false //disable key press
					}
				}
				function nospace(e)
				{
					var unicode=e.charCode? e.charCode : e.keyCode
					if (unicode!=8)
					{ //if the key isn't the backspace key (which we should allow)
						if (unicode==32) //if not a number
							return false //disable key press
					}
				}
		
		        function trimString (str) 
		        {
                    while (str.charAt(0) == ' ')
                    str = str.substring(1);
                    while (str.charAt(str.length - 1) == ' ')
                         str = str.substring(0, str.length - 1);
                   
                    return str;
                }

function checkEdit()
{
	var _countCK=0;
	var _chkName;
	var _obj=document.Form1.getElementsByTagName('input');
	for(var i=0;i<_obj.length;i++)
	{
		if( _obj[i].type=='checkbox')
		{
			if( _obj[i].checked)
			{
				_countCK++;
				_chkName=_obj[i].name;
			}
		}
	}  
	if (_countCK>1)
	{
		alert('Select Only One CheckBox');
		return false;
	}
	else if(_countCK==0)
	{
		alert('Select Atleast One CheckBox');

		return false;
	}
	else
	{
		var obj=document.Form1.elements["ChkId"];
		obj.value=_chkName;
	}
}

function checkDelete()
{
	var _countCK=0;
	var _chkName;

	var _obj=document.Form1.getElementsByTagName('input');
	for(var i=0;i<_obj.length;i++)
	{
		if( _obj[i].type=='checkbox')
		{
			if( _obj[i].checked)
			{
				_countCK++;
				_chkName=_obj[i].name;
			}
		}
	}
	if(_countCK==0)
	{
		alert('Select Atleast One CheckBox');
		return false;
	}
	else
	{
		var bool;
		bool=confirm("Are You Sure Want to Delete Record");
		if (bool==true)
		{
			GetDelete();
			return true;
		}
		else
		{
			return false;
		}
	}
}
function GetDelete()
{
	var _countCK=0;
	var _chkName;
	var _obj=document.Form1.getElementsByTagName('input');
	var obj=document.Form1.elements["DeleteList"];
	for(var i=0;i<_obj.length;i++)
	{
		if( _obj[i].type=='checkbox')
		{
			if( _obj[i].checked)
			{
				_countCK++;
				_chkName=_obj[i].name;
				if(obj.value!="")
				{
					obj.value=obj.value+","+_chkName;
				}
				else
				{
					obj.value=_chkName;
				}
			}
		}
	}  
	
	
}
function rep(sString)
{
	var rep = /'/g;
	sString = sString.replace(rep,"''");
	return sString;
}
function textCounter(field, countfield, maxlimit) 
{
	if (field.value.length > maxlimit) // if too long...trim it!
		//alert('called');
		field.value = field.value.substring(0, maxlimit);
		// otherwise, update 'characters left' counter
	else 
		countfield.value = maxlimit - field.value.length;
}

function NewFullWindow(mypage,myname)
{
	var win=null;
	var height=screen.height;
	var width=screen.width;
	var top=0;
	var left=0;
	win=window.open(mypage,myname,'height='+height+',width='+width+',top=0,left=0,scrollbars=1,status=0,resizable');	
	
}
function NewWindow(mypage,myname,height,width)
{
	var win=null;
	var top=(screen.height - height)*0.5;
	var left=( screen.width-width)*0.5;
	win=window.open(mypage,myname,'height='+height+',width='+width+',top='+top+',left='+left+',scrollbars=1,status=0,resizable=0');	
	
}


function openErrorWindow(mypage)
{
	var win=null;
	var height=screen.height*0.50;
	var width=screen.width * 0.50;
	var top=screen.height*.25;
	var left=screen.width*0.25;
	var myname="Error window";
	win=window.open(mypage,myname,'height='+height+',width='+width+',top=0,left=0,scrollbars=1,status=0,resizable');	
	
}


function checkConfirm()
{
	var _countCK=0;
	var _chkName;

	var _obj=document.Form1.getElementsByTagName('input');
	for(var i=0;i<_obj.length;i++)
	{
		if( _obj[i].type=='checkbox')
		{
			if( _obj[i].checked)
			{
				_countCK++;
				_chkName=_obj[i].name;
			}
		}
	}
	if(_countCK==0)
	{
		alert('Select Atleast One CheckBox');
		return false;
	}
	else
	{
		var bool;
		bool=confirm("Are You Sure Want to Confirm Order");
		if (bool==true)
		{
			GetDelete();
			return true;
		}
		else
		{
			return false;
		}
	}
}
function GetConfirm()
{
	var _countCK=0;
	var _chkName;
	var _obj=document.Form1.getElementsByTagName('input');
	var obj=document.Form1.elements["DeleteList"];
	for(var i=0;i<_obj.length;i++)
	{
		if( _obj[i].type=='checkbox')
		{
			if( _obj[i].checked)
			{
				_countCK++;
				_chkName=_obj[i].name;
				if(obj.value!="")
				{
					obj.value=obj.value+","+_chkName;
				}
				else
				{
					obj.value=_chkName;
				}
			}
		}
	}  
}
function checkReject()
{
	var _countCK=0;
	var _chkName;

	var _obj=document.Form1.getElementsByTagName('input');
	for(var i=0;i<_obj.length;i++)
	{
		if( _obj[i].type=='checkbox')
		{
			if( _obj[i].checked)
			{
				_countCK++;
				_chkName=_obj[i].name;
			}
		}
	}
	if(_countCK==0)
	{
		alert('Select Atleast One CheckBox');
		return false;
	}
	else
	{
		var bool;
		bool=confirm("Are You Sure Want to Reject Order");
		if (bool==true)
		{
			GetDelete();
			return true;
		}
		else
		{
			return false;
		}
	}
}
function GetReject()
{
	var _countCK=0;
	var _chkName;
	var _obj=document.Form1.getElementsByTagName('input');
	var obj=document.Form1.elements["DeleteList"];
	for(var i=0;i<_obj.length;i++)
	{
		if( _obj[i].type=='checkbox')
		{
			if( _obj[i].checked)
			{
				_countCK++;
				_chkName=_obj[i].name;
				if(obj.value!="")
				{
					obj.value=obj.value+","+_chkName;
				}
				else
				{
					obj.value=_chkName;
				}
			}
		}
	}  
	
}
        
        // Added by Rajeev Date:12-05-2008
        
        // function for the first character to be UpperCase.
        
        function Upper(e,r)
        {
          r.value = r.value.substr(0, 1).toUpperCase()+ r.value.substr(1,r.length);
        }
        
        // function for all the character to be UpperCase
        
         function UpperAll(e,r)
        {
          r.value = r.value.toUpperCase();
        }
        
        
        // function for the restriction of the textbox.
        
        function Check(e,val)
        {
            if(val=="Number")
            {
                if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode !=13) 
                event.returnValue = false;
            }
            if(val=="Digits")
            {
                 if ((event.keyCode < 48 || event.keyCode > 57 ) && event.keyCode !=13 && event.keyCode !=46) 
                event.returnValue = false;
            }
            if(val=="Name")
            {
                if (((event.keyCode < 65 || event.keyCode > 90) && (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 97 || event.keyCode > 122)) && event.keyCode !=39 && event.keyCode !=46 && event.keyCode !=32 && event.keyCode !=13) 
                event.returnValue = false;
            }
            if(val=="Code")
            {
                 if (((event.keyCode < 65 || event.keyCode > 90) && (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 97 || event.keyCode > 122)) && event.keyCode !=13) 
                event.returnValue = false;
            }
        }
        
        // function for max length of the text
        
      function ValidateLengthAddress(val,len)
     {
            val = document.getElementById(val);
            var GetLength = val.value;
            if(GetLength.length > len)
            {
                document.getElementById(val.id).value = val.substring(0,len);
                alert('Length Exceeds Max('+ len +') Length.');
                return false;
            }
        }
        function CheckMinMaxLength(ControlObj,MinLength,MaxLength)
        {
            Val = ControlObj.value;
            
            if(Val.length < MinLength)
            {
                alert('Password Should not Be Less Than '+ MinLength +' Characters' );
                ControlObj.focus();
                return false;
            }
            else if(Val.length > MaxLength)
            {
                alert('Password Should not Be More Than '+ MaxLength +' Characters');
                ControlObj.focus();
                return false;
            }
        }
        // function for validation of specific file extension of File Upload
     
       function checkFileExtension(elem,fileType)
        {
            var filePath = elem.value;
            if(filePath.indexOf('.') == -1)
                return false;
            var validExtensions = new Array();
           
            if(fileType=="Picture")
            {
               var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
               validExtensions[0] = 'jpg';
               validExtensions[1] = 'jpeg';
               validExtensions[2] = 'gif';
               validExtensions[3] = 'bmp';
            }
            else if(fileType=="Doc")
            {
               var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
               validExtensions[0] = 'doc';
               validExtensions[1] = 'xls';
               validExtensions[2] = 'pdf';
               validExtensions[3] = 'txt';
            }

            for(var i = 0; i < validExtensions.length; i++) {
                if(ext == validExtensions[i])
                    return true;
            }

            //alert('The file extension ' + ext.toUpperCase() + ' is not allowed!');
            alert('Upload file is not a graphical image. Please upload files with extension .gif, .jpg or .jpeg !');
            remove(elem.id);
            return false;
        }
        
        // new function
        // e.g.      txtCityCode.Attributes.Add("onBlur", "return check_length(this,5,'Code');");
        //           txtCityName.Attributes.Add("onBlur", "return check_length(this,5,'');");
function check_length(my_form,maxLen,code)
{
    if(maxLen ==0)
    {
        maxLen=my_form.value.length +1;
    }
      if("Code" != code)
     {
        if (my_form.value.length > maxLen)
        {
            var msg = "Length Exceeds Max(" + maxLen +") Length";
            my_form.value = my_form.value.substr(0, 1).toUpperCase()+ my_form.value.substring(1, maxLen);
            return msg;
         }
         else
         {
            my_form.value = my_form.value.substr(0, 1).toUpperCase()+ my_form.value.substring(1, maxLen);
         }
      }
      else
      {
        if (my_form.value.length > maxLen)
        {
            var msg = "Length Exceeds Max(" + maxLen +") Length";
            my_form.value = my_form.value.substr(0, maxLen).toUpperCase();
            return msg;
         }
         else
         {
          my_form.value = my_form.value.substr(0, maxLen).toUpperCase();
         }
      }
}

// function for the first character to be UpperCase.
        
        function Upper(e,r)
        {
          r.value = r.value.substr(0, 1).toUpperCase()+ r.value.substr(1,r.length);
        }
        
        // function for all the character to be UpperCase
        
         function UpperAll(e,r)
        {
          r.value = r.value.toUpperCase();
        }
        
        
        // function for the restriction of the textbox.
        
         function CheckSpecialCharacter(e,val)
        {
            if(val=="Number")
            {
                if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode !=13) 
                event.returnValue = false;
                else
                {
                event.returnValue=true;
                }
            }
            if(val=="Digits")
            {
                 if ((event.keyCode < 48 || event.keyCode > 57 ) && event.keyCode !=13 && event.keyCode !=45) 
                {
                    event.returnValue = false;
                }
                else
                {
                    if(CheckDot(e.id,event.keyCode)==false)
                    return false;
                    else
                {
                event.returnValue=true;
                }
                }
            }
            if(val=="Name")
            {
                if (((event.keyCode < 65 || event.keyCode > 90) && (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 97 || event.keyCode > 122))  && event.keyCode !=46 && event.keyCode !=32 && event.keyCode !=13 && event.keyCode !=40 && event.keyCode !=41 && event.keyCode !=44 && event.keyCode !=38 && event.keyCode !=39 && event.keyCode !=47 && event.keyCode !=45) 
                {
                    event.returnValue = false;
                }
                
                else
                {
                    if(SpecialChar(e.id,event.keyCode)==false)
                     {
                        return false;
                     }
                     else
                    {
                     return true;
                    }
                }    
            }
             if(val=="Alpha")
            {
                if (((event.keyCode < 65 || event.keyCode > 90) && (event.keyCode < 97 || event.keyCode > 122)) && event.keyCode !=39 && event.keyCode !=46 && event.keyCode !=32 && event.keyCode !=13 && event.keyCode !=40) 
                {
                    event.returnValue = false;
                }
                
                else
                {
                    if(SpecialChar(e.id,event.keyCode)==false)
                     {
                        return false;
                     }
                     else
                    {
                     return true;
                    }
                }    
            }
            if(val=="Code")
            {
                 if (((event.keyCode < 65 || event.keyCode > 90) && (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 97 || event.keyCode > 122))&& event.keyCode !=39 && event.keyCode !=13) 
                {
                    event.returnValue = false;
                }
            }
             if(val=="Desc100")
            {
               if(MaxLength1(e.id,99)==false)
               {
                return false;
               }
            }
             if(val=="Desc250")
            {
               if(MaxLength1(e.id,249)==false)
               {
                return false;
               }
            }
            if(val=="Desc500")
            {
               if(MaxLength1(e.id,499)==false)
               {
                return false;
               }
            }
        }
   
   
 
        function SpecialChar(obj,code)
       {
          var frmObject= document.form1;
          var objc=frmObject.elements[obj];
          var a=objc.value+String.fromCharCode(code);
          var ascii = a.charCodeAt(0);
          
          Upper(obj,objc)
          
            if(ascii ==32)
           {
              return false;
           }
              
           if(((ascii < 65 || ascii > 90) && (ascii < 48 || ascii > 57) && (ascii < 97 || ascii > 122))) 
           {
              return false;
           }
       }

    function  CheckDot(obj,code)
   {
      var frmObject= document.form1;
      var objc=frmObject.elements[obj];
      
      var a=objc.value+String.fromCharCode(code);
      var ser = a.split(".");                      

         if(ser.length >2)
         {
            return false;
         }
         else
         {
           return true;
         }
    }

  function MaxLength1(obj,code)
   {
         var frmObject= document.form1;
         var objc=frmObject.elements[obj].value;
         var objca=frmObject.elements[obj];
         Upper(obj,objca);
        
         var a=objc.length;
         if(a > code)
         {
           return true;
         }
   }
   
function GetCheckRange(Range,TxtId)
{    
    var abc=Range.value;  
    var AllRange=abc.split(",");  
    var Chk; 
    var Match=0;
    
        for(var i=0;i< AllRange.length;i++)
        {   
            var OrigRange=AllRange[i];        
            var Rang=OrigRange.split("-");
            var RangeFrom=Rang[0];
            var RangeTo=Rang[1]; 
           
            //alert(RangeFrom + '-'+ document.getElementById(TxtId).value +'-' +RangeTo);                     
           
            if(eval(RangeFrom)<eval(document.getElementById(TxtId).value) && eval(document.getElementById(TxtId).value)<eval(RangeTo))
            { 
                Match++;
            }
            else
            {   
                Chk=1;
               if(Match==i)
               {
                if(i==AllRange.length-1)
                 {
                   alert('This Sample Number is not assign to you !');             
                   return ;   
                 } 
               }  
            } 
           Match++;        
         }  
}


function RemoveTextFromTextBox( obj )
{
    alert(obj);
    var Text;
    alert(Text);
    if((document.getElementById(obj).value)!="")
    {
          Text =  document.getElementById(obj).value
          alert(Text);
          Text="";
          alert(Text);
          document.getElementById(obj).Value=Text;
          alert(document.getElementById(obj).Value);
          
    }

}