/* 
Created by Remin
On 10-10-2013 
*/

function Confirm() {
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    if (confirm("Do you want to delete data?")) {
        confirm_value.value = "Yes";
    } else {
        confirm_value.value = "No";
    }
    document.forms[0].appendChild(confirm_value);
}
function ConfirmSalesReturn() {
    if (Page_ClientValidate("submit")) {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Do you want to do salesReturn...?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }
}
function ConfirmApprove() {

    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    if (confirm("Are You Sure, you want to Approve ?")) {
        confirm_value.value = "Yes";
        true;
    } else {
        confirm_value.value = "No";
        false;
    }
    document.forms[0].appendChild(confirm_value);
}
function ConfirmSchedule() {
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    if (confirm("Schedule already processed.Do you want to reprocess data?")) {
        confirm_value.value = "Yes";
    } else {
        confirm_value.value = "No";
    }
    document.forms[0].appendChild(confirm_value);
}
function ConfirmUpdatePurchaseOrder() {
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    if (confirm("Do you want to Update status?")) {
        confirm_value.value = "Yes";
    } else {
        confirm_value.value = "No";
    }
    document.forms[0].appendChild(confirm_value);
}
function ConfirmPurchaseApprove() {
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    if (confirm("Do you want to Update status?")) {
        confirm_value.value = "Yes";
    } else {
        confirm_value.value = "No";
    }
    document.forms[0].appendChild(confirm_value);
}
function ConfirmPurchasePrint() {
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    if (confirm("Do you want to Print.?")) {
        confirm_value.value = "Yes";
    } else {
        confirm_value.value = "No";
    }
    document.forms[0].appendChild(confirm_value);
}

function ConfirmUpdation() {
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    if (confirm("Item Already Exists..Do you Want to Update???")) {
        confirm_value.value = "Yes";
    } else {
        confirm_value.value = "No";
    }
    document.forms[0].appendChild(confirm_value);
}
function ConfirmPrint() {
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_Print";
    if (confirm("Do you want to Print?")) {
        confirm_value.value = "Yes";
    } else {
        confirm_value.value = "No";
    }
    document.forms[0].appendChild(confirm_value);
}

function CheckNumWithDecimal(eventRef) {
    //take charecter code
    var carCode = eventRef.keyCode ? eventRef.keyCode : ((eventRef.charCode) ? eventRef.charCode : eventRef.which);
   
        //if carcode not for back space,tab,#,$,%,',.,and not between  0-9 then cancel event
     // ((carCode != 35) && (carCode != 36) && (carCode != 37) && (carCode != 39) && (carCode != 9))
   
       if((carCode != 8) && (carCode != 46) && (carCode != 9) && (carCode < 48)   || (carCode > 57))  {
            eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
        }

        //take target text box to oTextbox
        var oTextbox = document.getElementById((eventRef.target || eventRef.srcElement).id);
       
        //taking index of . in text
        var indexval = oTextbox.value.indexOf(".");
        //take charecter kyepressed position to leni

        //var leni = GetCursorLocation(oTextbox);

        //handle the case of .
        if (carCode == 46) {
            //if . already exists then cancel the second keypress of .
            if (indexval != -1) {
                eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
            }
            else {
                if (oTextbox.value == "") {
                    oTextbox.value = "0";
                    ventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
                }
            }
        }

        //if key press cherecter in between 0 and 9 then 
        if ((carCode > 47) && (carCode < 58)) {
            //if text in text box does not contain . then limit cheking using maximum length
            if (indexval == -1) {
                if (oTextbox.value.length >= 10) {
                    eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
                }
            }
            else {

                //if text box contain . and current keypress position lessthan or equal to position of . then compare for if lenth of text before . ia alredy >=15 then cancel current action

                //if (leni <= (indexval + 1)) {
                //    if ((indexval) >= 15) {
                //        eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
                //    }
                //}
                //    //else if digit numbers after . alredy 2 or more cancel current action
                //else 
               
                if ((oTextbox.value.length - indexval - 1) >= 3) {
                    eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
                }
            }
        }
    
}
function CheckNumWithDecimal1(eventRef) {
    //take charecter code
    var carCode = eventRef.keyCode ? eventRef.keyCode : ((eventRef.charCode) ? eventRef.charCode : eventRef.which);

    //if carcode not for back space,tab,#,$,%,',.,and not between  0-9 then cancel event
    // ((carCode != 35) && (carCode != 36) && (carCode != 37) && (carCode != 39) && (carCode != 9))

    if ((carCode != 8) && (carCode != 46) && (carCode != 9) && (carCode < 48) || (carCode > 57)) {
        eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
    }

    //take target text box to oTextbox
    var oTextbox = document.getElementById((eventRef.target || eventRef.srcElement).id);

    //taking index of . in text
    var indexval = oTextbox.value.indexOf(".");

    //take charecter kyepressed position to leni

    //var leni = GetCursorLocation(oTextbox);

    //handle the case of .
    if (carCode == 46) {
        //if . already exists then cancel the second keypress of .
        if (indexval != -1) {
            eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
        }
        else {
            if (oTextbox.value == "") {
                oTextbox.value = "0";
                ventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
            }
        }
    }

    //if key press cherecter in between 0 and 9 then 
    if ((carCode > 47) && (carCode < 58)) {
        //if text in text box does not contain . then limit cheking using maximum length
        if (indexval == -1) {
            if (oTextbox.value.length >= 10) {
                eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
            }
        }
        else {

            //if text box contain . and current keypress position lessthan or equal to position of . then compare for if lenth of text before . ia alredy >=15 then cancel current action

            //if (leni <= (indexval + 1)) {
            //    if ((indexval) >= 15) {
            //        eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
            //    }
            //}
            //    //else if digit numbers after . alredy 2 or more cancel current action
            //else 
            if ((oTextbox.value.length - indexval - 1) >= 3) {
                eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
            }
        }
    }

}

function CheckInteger(eventRef) {
    var carCode = eventRef.keyCode ? eventRef.keyCode : ((eventRef.charCode) ? eventRef.charCode : eventRef.which);
    //Mohammed Dilshad For Restrict string on feild ON 11/2/14
    //if carcode not for back space,tab,#,$,%,',.,and not between  0-9 then cancel event
    // ((carCode != 35) && (carCode != 36) && (carCode != 37) && (carCode != 39) && (carCode != 9))
   
    if ((carCode != 8) && (carCode != 9) && ((carCode < 48) || (carCode > 57))) {
        eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
    }

}
function CheckIntegerAndNeg(eventRef) {
    var carCode = eventRef.keyCode ? eventRef.keyCode : ((eventRef.charCode) ? eventRef.charCode : eventRef.which);
    //Mohammed Dilshad For Restrict string on feild ON 11/2/14
    //if carcode not for back space,tab,#,$,%,',.,and not between  0-9 then cancel event
    // ((carCode != 35) && (carCode != 36) && (carCode != 37) && (carCode != 39) && (carCode != 9))

    if ((carCode != 8) && (carCode != 9) && (carCode != 45) && ((carCode < 48) || (carCode > 57))) {
        eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
    }

}
//Negative and decimal

function CheckNumWithDecimalNegative(eventRef) {
    //take charecter code
    var carCode = eventRef.keyCode ? eventRef.keyCode : ((eventRef.charCode) ? eventRef.charCode : eventRef.which);
    
    //if carcode not for back space,tab,#,$,%,',.,and not between  0-9 then cancel event
    // ((carCode != 35) && (carCode != 36) && (carCode != 37) && (carCode != 39) && (carCode != 9))

    if ((carCode != 8) && (carCode != 46) &&(carCode!=45) && (carCode != 9) && (carCode < 48) || (carCode > 57)) {
        eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
    }

    //take target text box to oTextbox
    var oTextbox = document.getElementById((eventRef.target || eventRef.srcElement).id);

    //taking index of . in text
    var indexval = oTextbox.value.indexOf(".");

    //take charecter kyepressed position to leni

    //var leni = GetCursorLocation(oTextbox);

    //handle the case of .
    if (carCode == 46) {
        //if . already exists then cancel the second keypress of .
        if (indexval != -1) {
            eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
        }
        else {
            if (oTextbox.value == "") {
                oTextbox.value = "0";
                ventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
            }
        }
    }

    //if key press cherecter in between 0 and 9 then 
    if ((carCode > 47) && (carCode < 58)) {
        //if text in text box does not contain . then limit cheking using maximum length
        if (indexval == -1) {
            if (oTextbox.value.length >= 10) {
                eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
            }
        }
        else {

            //if text box contain . and current keypress position lessthan or equal to position of . then compare for if lenth of text before . ia alredy >=15 then cancel current action

            //if (leni <= (indexval + 1)) {
            //    if ((indexval) >= 15) {
            //        eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
            //    }
            //}
            //    //else if digit numbers after . alredy 2 or more cancel current action
            //else 
            if ((oTextbox.value.length - indexval - 1) >= 3) {
                eventRef.preventDefault ? eventRef.preventDefault() : eventRef.returnValue = false;
            }
        }
    }

}

//disable enter key
function EnterKeyFilter() {
    if (window.event.keyCode == 13) {       
        event.returnValue = false;
        event.cancel = true;
    }
}

//***************************** For AsyncFileUpload**********************************************************

function uploadStarted(sender, args) {
   
    var filename = args.get_fileName();
    var filext = filename.substring(filename.lastIndexOf(".") + 1).toLowerCase();
    if (filext == "jpg" || filext == "jpeg" || filext == "gif" || filext == "bmp" || filext == "png" || filext == "jpe" || filext == "dib" || filext == "jfif" || filext == "tif" || filext == "tiff") {
        return true;
    }
    else {
        var err = new Error();
        err.name = 'File upload error';
        err.message = 'Invalid file format selected';
        throw (err);
    }
}

function UploadComplete(sender, args) {
   
     // $("#<%=btnHidden.ClientID%>").Click();
   document.getElementById('btnHidden').click();
}
//***************************** For AsyncFileUpload**********************************************************


/******************************** For Webservice  **********************************************************/
function CallMenu(userid,clientid) {
    // Webservice
    $.ajax({
        type: "POST",
        data: "{'userid':'" + userid + "','clientid':'" + clientid + "'}",
        contentType: "application/json; charset=utf-8",
        url: "Textile.asmx/FillMenu",
        dataType: "json",
        success: function (MenuHtml) {
            $('#Ul1').html(MenuHtml.d);
        },
        error: function (MenuHtml) {
            //alert(MenuHtml.d);
            //alert('webservice_error');
        }
    });
}
function QuickMenu(userid, clientid) {
    // Webservice
    $.ajax({
        type: "POST",
        data: "{'userid':'" + userid + "','clientid':'" + clientid + "'}",
        contentType: "application/json; charset=utf-8",
        url: "Textile.asmx/QuickMenu",
        dataType: "json",
        success: function (MenuHtml) {
            $('#Ul1').html(MenuHtml.d);
        },
        error: function (MenuHtml) {
            //alert(MenuHtml.d);
            //alert('webservice_error');
        }
    });
}
function CallMenuGreen(userid, clientid) {
    // Webservice
    $.ajax({
        type: "POST",
        data: "{'userid':'" + userid + "','clientid':'" + clientid + "'}",
        contentType: "application/json; charset=utf-8",
        url: "Textile.asmx/FillMenuGreen",
        dataType: "json",
        success: function (MenuHtml) {
            alert(MenuHtml);
            $('#navigation').html(MenuHtml.d);
        },
        error: function (MenuHtml) {
            alert(MenuHtml.d);
            //alert('webservice_error');
        }
    });
}
/******************************** For Webservice  **********************************************************/

/******************************** Disable Enter Key ********************************************************/
function EnterKeyFilter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}
/******************************** Disable Enter Key ********************************************************/

$(window).on('resize', function () {
    if ($(window).height() > 750) {
        $('#myIframe').addClass('iframe900');
        $('#myIframe').removeClass('iframe768');
        $('#myIframe').removeClass('iframe800');
        $('#left_navi').addClass('left_navi900');
        $('#left_navi').removeClass('left_navi768');
        $('#left_navi').removeClass('left_navi800');
        
    }else if ($(window).height() > 700) {
        $('#myIframe').addClass('iframe800');
        $('#myIframe').removeClass('iframe768');
        $('#myIframe').removeClass('iframe900');
        $('#left_navi').addClass('left_navi800');
        $('#left_navi').removeClass('left_navi768');
        $('#left_navi').removeClass('left_navi900');
    } else {
        $('#myIframe').addClass('iframe768');
        $('#myIframe').removeClass('iframe900');
        $('#myIframe').removeClass('iframe800');
        $('#left_navi').addClass('left_navi768');
        $('#left_navi').removeClass('left_navi900');
        $('#left_navi').removeClass('left_navi800');
    }
})

