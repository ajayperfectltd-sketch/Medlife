// Jidesh Kumar M  Add ShowODGDWaivePopup On 26/03/2015 47107
// BIBIN TC  ADD AMOUNT IN WORDS ON 26/09/2017 OMID  83900 
//----- jQuery Intellisense------------
/// <reference path="/SCORE/Jquery/jquery-1.7.2.min-vsdoc.js" />
//-------------------------------------

//For Numeric Validation

function ValidateNumberKeyPress(field, evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    var keychar = String.fromCharCode(charCode);
    if (charCode > 31 && (charCode < 48 || charCode > 57) && keychar != "." && keychar != "-" && charCode != 190) {
        return false;
    }

    if (keychar == "." && field.value.indexOf(".") != -1) {
        return false;
    }

    if (keychar == "-") {
        if (field.value.indexOf("-") != -1 /* || field.value[0] == "-" */) {
            return false;
        }
        else {
            //save caret position
            var caretPos = getCaretPosition(field);
            if (caretPos != 0) {
                return false;
            }
        }
    }
    return true;
}


//for Event Throw Button Fill Event
function PopUpFill() {
    window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
    return false;
}

//For Focusing Next Control When Enter key Case
function nextfocus(nextfield, curf, msg, mandatory, e) {
    try {
        var txtInput = document.getElementById(nextfield);
        if (txtInput.className == 'TxtOnLoad') {

            return true;
        }

        if (!e) {
            e = window.event;
        }
        if (e.keyCode) {
            var charCode = e.keyCode;
        }
        else {
            var charCode = e.charCode;
        }

        if (mandatory == 'textbox-Regional') {

            var evtobj = window.event ? event : e
            var isshift = evtobj.shiftKey;

            if ((charCode != 13) && (charCode != 9) && (charCode != 46) && (charCode != 8) && (charCode != 32) && charCode != 37 && charCode != 192 && charCode != 16 && charCode != 39 && charCode != 35 && charCode != 36) {
                var charval;
                switch (charCode) {
                    case 48: case 49: case 50: case 51: case 52: case 53: case 54: case 55: case 56: case 57:
                        charval = 'D' + String.fromCharCode(charCode);
                        break;
                    case 222:
                        charval = 'OemQuotes';
                        break;
                    case 186:
                        charval = 'OemSemicolon';
                        break
                    case 219:
                        charval = 'OemOpenBrackets';
                        break
                    case 221:
                        charval = 'OemCloseBrackets';
                        break
                    case 188:
                        charval = (isshift == true) ? 'Oemcomma' : ',';
                        break
                    case 190:
                        charval = (isshift == true) ? 'OemPeriod' : '.';
                        break
                    case 191:
                        charval = 'OemQuestion';
                        break
                    default:
                        charval = String.fromCharCode(charCode);
                }
                if (isshift == true) {
                    charval = charval + 'Shift';
                }

                document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = 'Regional-' + charval + '~' + getCaretPosition(document.getElementById(curf)) + '~' + curf;
                document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                return true;
            }
        }

            if (mandatory == 'numbox-mandatory' || mandatory == 'numbox' || mandatory == 'numbox-warning' || mandatory == 'branchcode' || mandatory == 'branchcode-bc'|| mandatory == 'typecode' || mandatory == 'transaccno' || mandatory == 'subno'  || mandatory == 'typecode-without-mandatory' || mandatory == 'transaccno-without-mandatory') {

            var keychar = String.fromCharCode(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && keychar != "." && keychar != "-" && (charCode < 96 || charCode > 105) && charCode != 110 && charCode != 190) {
                return false;
            }
            if (keychar == "-") {
                if (curf.value.indexOf("-") != -1 /* || field.value[0] == "-" */) {
                    return false;
                }
                else {
                    //save caret position                
                    var caretPos = getCaretPosition(curf);
                    if (caretPos != 0) {
                        return false;
                    }
                }
            }
        }
        if (mandatory == 'numbox-numberonly' || mandatory == 'numbox-numberonly-mandatory') {
            AllowNumberOnly(e);
        }

        if (!e) {
            e = window.event;
        }
        if (e.keyCode) {
            var k = e.keyCode;
        }
        else {
            var k = e.charCode;
        }
        if (k == "13" || k == "9") {
            var Val = document.getElementById(curf).value;
            if (mandatory == 'numbox-mandatory' || mandatory == 'numbox-numberonly-mandatory' || mandatory == 'numbox-warning' || mandatory == 'customer-number') {
                if (Val == '' || Val == '0') {
                    alert(msg + " Cannot be Empty");
                    document.getElementById(curf).focus();
                    return false;
                }
                else {
                    if (nextfield != '' || nextfield != null) {
                        document.getElementById(nextfield).focus();
                    }
                }
                //            document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = curf;
                //            document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                //            return false;
            }
            else if ((Val == '' || Val == '0') && mandatory == 'numbox') {
                document.getElementById(curf).value = '0';
                if (nextfield != '' || nextfield != null) {
                    // document.getElementById(nextfield).focus();//Commented: focus goes to next+1th control-Ajith-12/08/2014
                }
            }
            else    if (Val == '' && mandatory == 'textbox-mandatory') {

                alert(msg + " Cannot be Empty");

                document.getElementById(curf).focus();
                return false;

            }
            else if (Val == '' && mandatory == ' textbox-nomessage') {
                document.getElementById(curf).focus();
                return false;

            }

            else if (Val == '' && mandatory == 'textbox-Regional') {
                document.getElementById(nextfield).focus()
                return false;

            }

            else if ((Val == '' || Val == '0') && mandatory == 'dropdownlist-mandatory') {
                alert(msg + " Cannot be Empty");

                document.getElementById(curf).focus();
                return false;
            }
            else if ((Val == '' || Val == '0') && mandatory == 'checkboxlist') {
                alert(msg + " Cannot be Empty");
                document.getElementById(curf).focus();
                return false;
            }

            else if (mandatory == 'datebox-mandatory') {

                if ((Val == '' || Val == '0') && mandatory == 'datebox-mandatory') {
                    alert(msg + " Cannot be Empty");
                    document.getElementById(curf).focus();
                    return false;
                }
                document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = curf;
                document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                document.getElementById(nextfield).focus();
                return false;
            }
            else if (mandatory == 'datebox') {

                if (Val != '') {
                    document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = curf; ;
                    document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                    return false;
                }
                else {
                    if (nextfield != '' || nextfield != null) {
                        document.getElementById(nextfield).focus();
                    }
                    return false;
                }

            }
            else if (mandatory == 'AccCode-mandatory') {
                document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = 'ACCCODE' + curf; ;
                document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                return false;

            }
            else if (mandatory == 'AccCode') {
                document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = 'ACCCODE' + curf; ;
                document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                return false;

            }
            //            else if (mandatory == 'AccCode') {
            //                if (Val == '') {
            //                    document.getElementById(curf).value = '0';
            //                    document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = 'ACCCODE' + curf; ;
            //                    document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
            //                    return false;
            //                }
            //                else {
            //                    if (nextfield != '' || nextfield != null) {
            //                        document.getElementById(nextfield).focus();
            //                    }
            //                    return false;
            //                }
            //            }
            else if (mandatory == 'transaccno') {

                if (Val == '' || Val == '0') {
                    document.getElementById(curf).focus();
                    return false;
                }
                else {
                    document.getElementById(curf).value = zeroPad(Val, 6);
                    if (nextfield != '' || nextfield != null) {
                        document.getElementById(nextfield).focus();
                    }
                    return false;
                }
            }
            else if (mandatory == 'transaccno-without-mandatory') {
                if (Val == '' || Val == '0') {
                    document.getElementById(curf).focus();
                    return false;
                }
                else {
                    document.getElementById(curf).value = zeroPad(Val, 6);
                    if (nextfield != '' || nextfield != null) {
                        document.getElementById(nextfield).focus();
                    }
                    return false;
                }
            }
            else if (mandatory == 'typecode') {

                if (Val == '' || Val == '0') {
                    alert(msg + " Cannot be Empty");
                    document.getElementById(curf).focus();
                    return false;
                }
                else {
                    document.getElementById(curf).value = zeroPad(Val, 3);
                    if (nextfield != '' || nextfield != null) {
                        document.getElementById(nextfield).focus();
                    }
                    return false;
                }
            }
             else if (mandatory == 'typecodepopup') {

                if (Val == '' || Val == '0') {
                    alert(msg + " Cannot be Empty");
                    document.getElementById(curf).focus();
                    return false;
                }
                else if(isNaN(Val))
                {alert(msg + " Cannot be a String");
                    document.getElementById(curf).focus();
                    return false;
                }
                else {
                    document.getElementById(curf).value = zeroPad(Val, 3);
                    if (nextfield != '' || nextfield != null) {
                        document.getElementById(nextfield).focus();
                    }
                    return false;
                }
            }
            else if (mandatory == 'branchcode') {

                if (Val == '' || Val == '0') {
                    alert(msg + " Cannot be Empty");
                    document.getElementById(curf).focus();
                    return false;
                }
                else {
                    document.getElementById(curf).value = zeroPad(Val, 3);
                    if (nextfield != '' || nextfield != null) {
                        document.getElementById(nextfield).focus();
                    }
                    return false;
                }
            }
            else if (mandatory == 'branchcode-bc') {
                if (Val == '' || Val == '0') {
                    alert(msg + " Cannot be Empty");
                    document.getElementById(curf).focus();
                    return false;
                }
                else {
                if (document.getElementById(curf).value.Length>=11)
                {
                  return false;
                }
                else
                {
                  document.getElementById(curf).value = zeroPad(Val, 3);
                }
                    if (nextfield != '' || nextfield != null) {
                        document.getElementById(nextfield).focus();
                    }
                    return false;
                }
            }
            else if (mandatory == 'subnowithoutmandatory') {
                if (Val == '') {

                }
                else {
                    document.getElementById(curf).value = zeroPad(Val, 2);

                }
                document.getElementById(nextfield).focus();

                return false;
            }

            else if (mandatory == 'typecode-without-mandatory') {
                if (Val == '' || Val == '0') {
                    alert(msg + " Cannot be Empty");
                    document.getElementById(curf).focus();
                    return false;
                }
                else {
                    document.getElementById(curf).value = zeroPad(Val, 3);
                    if (nextfield != '' || nextfield != null) {
                        document.getElementById(nextfield).focus();
                    }
                    return false;
                }
            }
            else if (mandatory == 'subno') {

                if (Val == '' || Val == '0') {
                    alert(msg + " Cannot be Empty");
                    document.getElementById(curf).focus();
                    return false;
                }
                else {
                    document.getElementById(curf).value = zeroPad(Val, 2);
                    if (nextfield != '' || nextfield != null) {
                        document.getElementById(nextfield).focus();
                    }
                    return false;
                }
            }

            else {
                if (nextfield != '' || nextfield != null) {
                    document.getElementById(nextfield).focus();
                }
                return false;

            }
        }
    }
    //  }

    catch (err) {
    }
}

function AllowNumberOnly(e) {//Ajith

    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13]) !== -1 || // backspace, delete, tab, escape, enter
               (e.keyCode == 65 && e.ctrlKey === true) || // Ctrl+A
               (e.keyCode >= 35 && e.keyCode <= 39)) {// home, end, left, right
        return;
    }
    // Dont Allow
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        (e.preventDefault) ? e.preventDefault() : e.returnValue = false;
    }
}

var isShift = false;
var seperator = "/";
function DateFormat(txt, keyCode) {
    if (keyCode == 16)
        isShift = true;
    //Validate that its Numeric
    if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
        if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
            txt.value += seperator;
        }
        return true;
    }
    else {
        return false;
    }
}

function ValidateDate(txt, keyCode) {
    if (keyCode == 16)
        isShift = false;
    var val = txt.value;
    //  var lblmesg = document.getElementById("<%=lblMesg.ClientID%>");
    if (val.length == 10) {
        var splits = val.split("/");
        var dt = new Date(splits[1] + "/" + splits[0] + "/" + splits[2]);

        //Validation for Dates
        if (dt.getDate() == splits[0] && dt.getMonth() + 1 == splits[1]
            && dt.getFullYear() == splits[2]) {
            // lblmesg.style.color = "green";
            //lblmesg.innerHTML = "Valid Date";
        }
        else {
            //   lblmesg.style.color = "red";
            alert('Invalid Date');
            // lblmesg.innerHTML = "Invalid Date";
            return;
        }

        //Range Validation
        if (txt.id.indexOf("txtRange") != -1)
            RangeValidation(dt);

        //BirthDate Validation
        if (txt.id.indexOf("txtBirthDate") != -1)
            BirthDateValidation(dt)
    }
    else if (val.length < 10) {
        // lblmesg.style.color = "blue";      
    }
}

function showAdhocReportViewer(reportid, dbname, usercode, branchcodeuser, branchcode, fromdate, todate) {
    try {

        window.open("../../Presentation/MIS/MisAdhocReportViewer.aspx?reportid=" + reportid + "&dbname=" + dbname + "&usercode=" + usercode + "&branchcodeuser=" + branchcodeuser + "&branchcode=" + branchcode + "&fromdate=" + fromdate + "&todate=" + todate + "", "reportviewer", "status=0,left=200,top=50,resizable=1,scrollbars=1,width=900,height=750");
    }
    catch (err) {
    }
}

function showLockerStatus(dbname, usercode, branchcodeuser, deposittype) {

    var lockerno = "";
    //keyno = window.showModalDialog('../../Presentation/SafedepositLocker/SafeDepositLockerPosition.aspx?dbname=' + dbname + '&usercode=' + usercode + '&branchcodeuser=' + branchcodeuser + '&deposittype=' + deposittype + '', "LockerStatus", "status:no;dialogLeft:200;dialogTop:50;resizable:no;dialogWidth:900px;dialogHeight:650px;center:yes;edge:raised;");
    lockerno = window.showModalDialog('../../Presentation/SafedepositLocker/SafeDepositLockerPosition.aspx?dbname=' + dbname + '&usercode=' + usercode + '&branchcodeuser=' + branchcodeuser + '&deposittype=' + deposittype + '', "", "status:no;dialogLeft:200;dialogTop:50;resizable:no;dialogWidth:900px;dialogHeight:700px;center:yes;edge:raised;");
    document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = lockerno;
    document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();


    //try {

    //window.open("../../Presentation/SafedepositLocker/SafeDepositLockerPosition.aspx?dbname=" + dbname + "&usercode=" + usercode + "&branchcodeuser=" + branchcodeuser + "&deposittype=" + deposittype + "", "LockerStatus", "status=0,left=200,top=50,resizable=0,scrollbars=1,width=900,height=650");
    //}
    //catch (err) {
    //}
}

function showSearchFormUpload(module, branchcode, dbname, searchValue, critrea1, critrea2, critrea3, critrea4, critrea5) {
    try {
        var arry = new Array(10);
        arry = window.showModalDialog('../../Presentation/Common/Search.aspx?Module=' + module + '&dbname=' + dbname + '&branchcode=' + branchcode + '&searchValue=' + searchValue + '&critrea1=' + critrea1 + '&critrea2=' + critrea2 + '&critrea3=' + critrea3 + '&critrea4=' + critrea4 + '&critrea5=', "", "status:no;dialogLeft:7000;dialogTop:0;resizable:no;dialogWidth:730px;dialogHeight:455px;center:no;edge:raised;");

        try {
            var str = arry[0] + "!" + arry[1] + "!" + arry[2] + "!" + arry[3] + "!" + arry[4] + "!" + arry[5] + "!" + arry[6] + "!" + arry[7]
        }
        catch (Error) {

        }

        if (arry[0] > 0) {
            document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = str;
            document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
        }
    }
    catch (err) {
    }

}

function showImages(CustomerID, CustomerNo) {
    var arry = new Array(1);

    arry = window.showModalDialog('../../Presentation/Common/ImageShow.aspx?CustomerID=' + CustomerID + '&CustomerNo=' + CustomerNo + '', "", "status:no;dialogLeft:800;dialogTop:400;resizable:no;dialogWidth:250px;dialogHeight:420px;center:yes;edge:raised;toolbar=no,menubar=no,location=no,titlebar=no");
}

function zeroPad(num, count) {
    var numZeropad = num + '';
    while (numZeropad.length < count) {
        numZeropad = "0" + numZeropad;
    }
    return numZeropad;
}

function showExpenseForm(textboxname) {

    var expAmount = 0;
    expAmount = window.showModalDialog('../../Presentation/Expense/ExpensePopup.aspx', "", "status:no;dialogLeft:400;dialogTop:200;resizable:yes;dialogWidth:300px;dialogHeight:380px;center:no;edge:raised;location=no;");
    document.getElementById('ctl00_ContentPlaceHolder1_' + textboxname + '').value = CurrencyFormatted(expAmount);
    document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = textboxname;
    document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();

}
function setfocus(NumBox, e) {
    if (!e) {
        e = window.event;
    }

    if (e.keyCode) {
        var charCode = e.keyCode;
    }
    else {
        var charCode = e.charCode;
    }

    if (charCode == "13" || charCode == "9") {
        document.getElementById(NumBox).focus();
    }

}


///////////////////////Disable Mouse RightClick
var IE;
var NN;
function right(click) {
    if (IE && (event.button == 2 || event.button == 3)) {
        alert("The right click has been disabled here.");
        return false;
    }

    if (NN && (click.which == 2 || click.which == 3)) {
        alert("The right click has been disabled here.");
        return false;
    }

    return false;
}

function DisableRightclick() {

    if (navigator.appName == "Microsoft Internet Explorer") {
        IE = true;
    }
    if (navigator.appName == "Netscape") {
        NN = true;
    }
    if (document.layers) window.captureEvents(Event.MOUSEDOWN);
    if (document.layers) window.captureEvents(Event.MOUSEUP);
    document.onmousedown = right;
    document.onmouseup = right;
    window.document.layers = right;
}
function ShowGridViewOrnament(TransMode, dbname, FK_Customer, Critrea1, Critrea2, Critrea3, message) {
    window.showModalDialog('../../Presentation/Common/DetailViewOrnament.aspx?TransMode=' + TransMode + '&dbname=' + dbname + '&FK_Customer=' + FK_Customer + '&Critrea1=' + Critrea1 + '&Critrea2=' + Critrea2 + '&Critrea3=' + Critrea3 + '&message=' + message + '', "", "status:no;dialogLeft:300;dialogTop:200;resizable:No;dialogWidth:350px;dialogHeight:240px;center:YES;edge:raised;");
}
function ShowPaymentTransfer(useraction, branchcode, dbname, accountcode, criteria1, criteria2, chkTransfer) {
    try {

        window.showModalDialog('../../Presentation/Common/PaymentTransferPopup.aspx?UserAction=' + useraction + '&BranchCode=' + branchcode + '&Dbname=' + dbname + '&AccountCode=' + accountcode + '&Criteria1=' + criteria1 + '&Criteria2=' + criteria2 + '', "", "status:no;dialogLeft:400;dialogTop:200;resizable:no;dialogWidth:355px;dialogHeight:230px;center:no;edge:raised;location=no;");

        //            $('#divPaymentT').html('<iframe id="searciframe" frameborder="0" height="250" width="380" src="../../presentation/common/paymenttransferpopup.aspx?useraction=' + useraction + '&branchcode=' + branchcode + '&dbname=' + dbname + '&accountcode=' + accountcode + '&criteria1=' + criteria1 + '&criteria2=' + criteria2 + '"></iframe> ');
        //            $('#divPaymentT').dialog({ 
        //                title:'search',
        //                width:410,
        //                height:310,
        //                modal:true
        //            });

        document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = chkTransfer;
        document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();

    }
    catch (err) {
    }

}

//Shows Customer Photo and Address Details thru Popup screen
function CusInformation(CustomerID, CustomerNo, DbName) {
    var arry = new Array(1);
    arry = window.showModalDialog('../../Presentation/Common/CusInformation.aspx?CustomerID=' + CustomerID + '&CustomerNo=' + CustomerNo + '&DbName=' + DbName + '', "", "status:no;dialogLeft:400;dialogTop:200;resizable:no;dialogWidth:290px;dialogHeight:290px;center:yes;edge:raised;toolbar=no,menubar=no,location=no,titlebar=no");
}
//Showing Loan Slab
function ShowSlabDetails() {
    try {
        var result = 0;
        window.showModalDialog('../../Presentation/TermLoan/TermLoanSlab.aspx', "", "status:no;dialogLeft:400;dialogTop:100;resizable:no;dialogWidth:390px;dialogHeight:590px;center:no;edge:raised;location=no;");
    }
    catch (err) {
    }
}
//Showing InvestmentShare Slab
function ShowShareSlabDetails() {
    try {
        var result = 0;
        window.showModalDialog('../../Presentation/Investment/InvestmentShareSlab.aspx', "", "status:no;dialogLeft:400;dialogTop:100;resizable:no;dialogWidth:390px;dialogHeight:590px;center:no;edge:raised;location=no;");
    }
    catch (err) {
    }
}

function showVoucher(left, top, height, width) {
    try {

        var MyArgs = new Array(0);
        var WinSettings = "center:yes;resizable:no;status:0;dialogTop:" + top.toString() + "px;dialogLeft:" + left.toString() + "px;dialogWidth:" + width.toString() + "px;dialogHeight:" + height.ToString() + "px";
        var MyArgs = window.showModalDialog('../../Presentation/Common/VoucherPreview.aspx', MyArgs, WinSettings);
        document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
    }
    catch (err) {
    }
}

//Showing Borrowing Slab
function ShowBorrowingSlabDetails() {
    try {
        var result = 0;
        window.showModalDialog('../../Presentation/Borrowing/BorrowingSlab.aspx', "", "status:no;dialogLeft:400;dialogTop:100;resizable:no;dialogWidth:390px;dialogHeight:590px;center:no;edge:raised;location=no;");

    }
    catch (err) {
    }
}

function showTaxPopUpForm(textboxname) {
    try {
        var taxAmount = 0;
        taxAmount = window.showModalDialog('../../Presentation/Common/TaxPopup.aspx', "", "status:no;dialogLeft:400;dialogTop:200;resizable:yes;dialogWidth:10px;dialogHeight:310px;center:no;edge:raised;location=no;");
        document.getElementById('ctl00_ContentPlaceHolder1_' + textboxname + '').value = CurrencyFormatted(taxAmount);
        document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = textboxname;
        document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();

    }

    catch (err) {

    }
}
function CurrencyFormatted(amount) {
    var i = parseFloat(amount);
    if (isNaN(i)) { i = 0.00; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    i = parseInt((i + .005) * 100);
    i = i / 100;
    s = new String(i);
    if (s.indexOf('.') < 0) { s += '.00'; }
    if (s.indexOf('.') == (s.length - 2)) { s += '0'; }
    s = minus + s;
    return s;
}



/// <reference path="General.js" />
//Marqueee Closing Function
function marclose() {

    document.getElementById('div1').style.display = "none";
}


//Amount in Words  -- bibin tc
function amtInWords(amount) {
  
    var decAmount = amount;
    var sUnits = new Array(20);
    var sTens = new Array(8);
    var sHundreds = new Array(6);
    var sAmount;
    var i, iLenAmount, iDecPart, iIntegerPart;

    sUnits[1] = '';
    sUnits[2] = 'One';
    sUnits[3] = 'Two';
    sUnits[4] = 'Three';
    sUnits[5] = 'Four';
    sUnits[6] = 'Five';
    sUnits[7] = 'Six';
    sUnits[8] = 'Seven';
    sUnits[9] = 'Eight';
    sUnits[10] = 'Nine';
    sUnits[11] = 'Ten';
    sUnits[12] = 'Eleven';
    sUnits[13] = 'Twelve';
    sUnits[14] = 'Thirteen';
    sUnits[15] = 'Fourteen';
    sUnits[16] = 'Fifteen';
    sUnits[17] = 'Sixteen';
    sUnits[18] = 'Seventeen';
    sUnits[19] = 'Eighteen';
    sUnits[20] = 'Ninteen';
    sTens[1] = 'Twenty';
    sTens[2] = 'Thirty';
    sTens[3] = 'Forty';
    sTens[4] = 'Fifty';
    sTens[5] = 'Sixty';
    sTens[6] = 'Seventy';
    sTens[7] = 'Eighty';
    sTens[8] = 'Ninety';
    sHundreds[1] = 'Hundred';
    sHundreds[2] = 'Thousand';
    sHundreds[3] = 'Lakh';
    sHundreds[4] = 'Crore';
    if (decAmount == 10000000000000) {
        decAmount = 9999999999999.99;
    }
    if (decAmount == 0) {
        return "";
    }

    iDecPart = (decAmount - Math.round(decAmount)) * 100;
    iDecPart = Math.round(iDecPart);

    //Because Math.round results .50,.52,.53.......98,.99 in negative values

    if (iDecPart < 0) {
        iDecPart = 100 + iDecPart;
    }

    if (iDecPart == 0) {
        decAmount = decAmount;
    }
    else {
        decAmount = Math.round(decAmount - (iDecPart / 100));
    }

    iLenAmount = ((String)(decAmount)).length;

    if (iLenAmount == 1) {
        var index = parseInt(decAmount) + 1;
        sAmount = sUnits[index];
    }
    else {
        for (i = iLenAmount; i > 0; i--) {
            if (i == 13 || i == 12) {
                iIntegerPart = parseInt(decAmount / 100000000000);
                decAmount = parseInt(decAmount % 100000000000);
                if (iIntegerPart == 0) {
                    sAmount = sAmount;
                }
                else {
                    if (iIntegerPart < 20) {
                        sAmount = sUnits[iIntegerPart + 1] + " " + sHundreds[6] + " ";
                    }
                    else {
                        sAmount = sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[6] + " "
                    }
                }
            }
            else if (i == 11 || i == 10) {
                iIntegerPart = parseInt(decAmount / 1000000000);
                decAmount = parseInt(decAmount % 1000000000);
                if (iIntegerPart == 0) {
                    sAmount = sAmount;
                }
                else {
                    if (iIntegerPart < 20) {
                        if (sAmount == null) {
                            sAmount = sUnits[iIntegerPart + 1] + " " + sHundreds[5] + " ";
                        }
                        else {
                            sAmount = sAmount + " " + sUnits[iIntegerPart + 1] + " " + sHundreds[5] + " ";
                        }
                    }
                    else {
                        if (sAmount == null) {
                            sAmount = sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[5] + " ";
                        }
                        else {
                            sAmount = sAmount + " " + sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[5] + " ";
                        }
                    }
                }
            }
            else if (i == 9 || i == 8) {
                iIntegerPart = parseInt(decAmount / 10000000);
                decAmount = parseInt(decAmount % 10000000);
                if (iIntegerPart == 0) {
                    sAmount = sAmount;
                }
                else {
                    if (iIntegerPart < 20) {
                        if (sAmount == null) {
                            sAmount = sUnits[iIntegerPart + 1] + " " + sHundreds[4] + " ";
                        }
                        else {
                            sAmount = sAmount + " " + sUnits[iIntegerPart + 1] + " " + sHundreds[4] + " ";
                        }
                    }
                    else {
                        if (sAmount == null) {
                            sAmount = sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[4] + " ";
                        }
                        else {
                            sAmount = sAmount + " " + sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[4] + " ";
                        }
                    }
                }
            }
            else if (i == 7 || i == 6) {
                iIntegerPart = parseInt(decAmount / 100000);
                decAmount = (decAmount % 100000);
                if (iIntegerPart == 0) {
                    sAmount = sAmount;
                }
                else {
                    if (iIntegerPart < 20) {
                        if (sAmount == null) {
                            sAmount = sUnits[iIntegerPart + 1] + " " + sHundreds[3] + " ";
                        }
                        else {
                            sAmount = sAmount + " " + sUnits[iIntegerPart + 1] + " " + sHundreds[3] + " ";
                        }
                    }
                    else {
                        if (sAmount == null) {
                            sAmount = sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[3] + " ";
                        }
                        else {
                            sAmount = sAmount + " " + sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[3] + " ";
                        }
                    }
                }
            }
            else if (i == 5 || i == 4) {
                iIntegerPart = parseInt(decAmount / 1000);
                decAmount = (decAmount % 1000);
                if (iIntegerPart == 0) {
                    sAmount = sAmount;
                }
                else {
                    if (iIntegerPart < 20) {
                        if (sAmount == null) {
                            sAmount = sUnits[iIntegerPart + 1] + " " + sHundreds[2] + " ";
                        }
                        else {
                            sAmount = sAmount + " " + sUnits[iIntegerPart + 1] + " " + sHundreds[2] + " ";
                        }
                    }
                    else {
                        if (sAmount == null) {
                            sAmount = sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[2] + " ";
                        }
                        else {
                            sAmount = sAmount + " " + sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[2] + " ";
                        }
                    }
                }
            }
            else if (i == 3) {
                iIntegerPart = parseInt(decAmount / 100);
                decAmount = (decAmount % 100);
                if (iIntegerPart == 0) {
                    sAmount = sAmount;
                }
                else {
                    var index;
                    index = parseInt(iIntegerPart) + 1;
                    if (sAmount == null) {
                        sAmount = sUnits[index] + " " + sHundreds[1] + " ";
                    }
                    else {
                        sAmount = sAmount + " " + sUnits[index] + " " + sHundreds[1] + " ";
                    }
                }
            }
            else if (i == 2) {
                decAmount = parseInt(eval(decAmount));
                if (decAmount < 20) {
                    var index = parseInt(decAmount) + 1;
                    if (sAmount == null) {
                        sAmount = sUnits[index];
                    }
                    else {
                        sAmount = sAmount + " " + sUnits[index];
                    }
                }
                else {
                    var a = parseInt(((decAmount / 10) - 1));
                    var b = (decAmount % 10) + 1;
                    if (sAmount == null) {
                        sAmount = sTens[a] + " " + sUnits[b];
                    }
                    else {
                        sAmount = sAmount + " " + sTens[a] + " " + sUnits[b];
                    }
                }
            }
        }
    }
    if (iDecPart == 0) {
        sAmount = "Rs. " + sAmount;
    }
    else if (sAmount == "") {
        sAmount = "Paise ";
    }
    else {
        sAmount = "Rs. " + sAmount + " And Paise";
    }

    if (iDecPart < 20) {
        sAmount = sAmount + " " + sUnits[iDecPart + 1] + " ";
    }
    else {
        var fi = parseInt(((iDecPart / 10) - 1));
        var fii = parseInt((iDecPart % 10)) + 1;
        sAmount = sAmount + " " + sTens[fi] + " " + sUnits[fii] + " ";
    }

    sAmount = sAmount + "Only";

    return sAmount;

}

function showPrintButtonPopup(TransMode, ID, FK_Machine, TransType, Paths, BranchCodeUser, Duplicate) {
    try {

        window.showModalDialog('../../Presentation/Common/VoucherPrintPreview.aspx?TransMode=' + TransMode + '&ID=' + ID + '&FK_Machine=' + FK_Machine + '&TransType=' + TransType + '&Paths=' + Paths + '&BranchCodeUser=' + BranchCodeUser + '&Duplicate=' + Duplicate + '', "", "status:no;dialogLeft:400;dialogTop:200;resizable:yes;dialogWidth:850px;dialogHeight:500px;center:no;edge:raised;location=no;");
    }

    catch (err) {

    }
}

function showLoanBalanceConfirmation(accountid, asondate, intuptodate, dbname) {
    try {

        //window.open('../../Presentation/TermLoan/TermLoanInformation.aspx?AccountId=' + accountid + '&AsonDate=' + asondate + '&IntUptoDate=' + intuptodate + '&Dbname=' + dbname + '', "self", "left=200,top=50,width=650,height=350,status=0,resizable=0,scrollbars=0;");
        //window.showModalDialog('../../Presentation/TermLoan/TermLoanInformation.aspx?AccountId=' + accountid + '&AsonDate=' + asondate + '&IntUptoDate=' + intuptodate + '&Dbname=' + dbname + '', "", "status:no;dialogLeft:400;dialogTop:100;resizable:no;dialogWidth:650;dialogHeight:350;center:no;edge:raised;location=no;");

        window.parent.$('#divMessageBox').empty();
        window.parent.$('#divMessageBox').html('<iframe id="searcIframe" frameborder="0" height="340" width="660" src="../../Presentation/TermLoan/TermLoanInformation.aspx?AccountId=' + accountid + '&AsonDate=' + asondate + '&IntUptoDate=' + intuptodate + '&Dbname=' + dbname + '"></iframe>');
        window.parent.$('#divMessageBox').dialog({
            title: 'Balance Information',
            width: 695,
            height: 375,
            modal: true,
            show: 'fade',
            hide: 'fade'
            //,
            //buttons: { 'Ok': function() { $(this).dialog('close') } }
        });





    }

    catch (err) {
    }

}

//Showing Cash Denomination Screen
function ShowDenominationDetails(window, transdate, branchcode, user, mode, dbname, transtype, transamount) {
    try {
        var arry = new Array(1);
        arry = window.showModalDialog('../../Presentation/CashierModule/CashierDenominationPopup.aspx?TransDate=' + transdate + '&BranchCode=' + branchcode + '&User=' + user + '&Mode=' + mode + '&Dbname=' + dbname + '&TransType=' + transtype + '&TransAmount=' + transamount + '', "", "status:no;dialogLeft:400;dialogTop:100;resizable:no;dialogWidth:490px;dialogHeight:490px;center:no;edge:raised;location=no;");
        var str = arry[0];
        document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = str;
        document.getElementById('ctl00_ContentPlaceHolder1_btnSave').click();
    }
    catch (err) {
    }
}

function showPrint(Path, PageHeight, PageWidth, PrnName, PrintOption, Duplicate) {
    try {

        window.open('../../Presentation/Common/PrintPreview.aspx?Path=' + Path + '&PageHeight=' + PageHeight + '&PageWidth=' + PageWidth + '&PrnName=' + PrnName + '&PrintOption=' + PrintOption + '&Duplicate=' + Duplicate + '', "", "status:no;dialogLeft:7000;dialogTop:0;resizable:yes;dialogWidth:300px;dialogHeight:585px;center:no;edge:raised;");

    }

    catch (err) {
    }

}

function showBondPrint(Path, PageHeight, PageWidth, PrnName, PrintOption, Duplicate) {
    try {

        window.open('../../Presentation/Common/BondPreview.aspx?Path=' + Path + '&PageHeight=' + PageHeight + '&PageWidth=' + PageWidth + '&PrnName=' + PrnName + '&PrintOption=' + PrintOption + '&Duplicate=' + Duplicate + '', "", "status:no;dialogLeft:0;dialogTop:0;resizable:yes;width=810,height=640;center:no;edge:raised;");

    }

    catch (err) {
    }

}

function showPassBookPrint(Path, PageHeight, PageWidth, PrnName, PrintOption, Duplicate) {
    try {
        window.open('../../Presentation/Common/PassBookPrintPreview.aspx?Path=' + Path + '&PageHeight=' + PageHeight + '&PageWidth=' + PageWidth + '&PrnName=' + PrnName + '&PrintOption=' + PrintOption + '&Duplicate=' + Duplicate + '', "", "status:no;dialogLeft:0;dialogTop:0;resizable:yes;width=810,height=640;center:no;edge:raised;");
    }
    catch (err) {
    }
}

function showSplictPasswordForm(TransMode, ID, FK_Machine, BranchCodeUser) {
    try {


        var retvalue = window.showModalDialog('../../Presentation/Security/SplitPassword.aspx?TransMode=' + TransMode + '&ID=' + ID + '&FK_Machine=' + FK_Machine + '&BranchCodeUser=' + BranchCodeUser + '', "", "status:no;dialogLeft:400;dialogTop:200;resizable:yes;dialogWidth:320px;dialogHeight:180px;center:yes;edge:raised;location=no;");
        document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = 'DulicatePrint' + retvalue;
        document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
    }

    catch (err) {

    }
}

function showSplictPassword(TransMode, ID, FK_Machine, BranchCodeUser) {
    try {
        var retvalue = window.showModalDialog('Presentation/Security/SplitPassword.aspx?TransMode=' + TransMode + '&ID=' + ID + '&FK_Machine=' + FK_Machine + '&BranchCodeUser=' + BranchCodeUser + '', "", "status:no;dialogLeft:400;dialogTop:200;resizable:yes;dialogWidth:320px;dialogHeight:180px;center:yes;edge:raised;location=no;");
        document.getElementById('HdIDuser').value = retvalue;
        document.getElementById('btnScriptFill').click();
    }

    catch (err) {

    }
}

//Get Curson Position of a Control
function getCaretPosition(ctrl) {

    var CaretPos = 0; // IE Support
    if (document.selection) {
        ctrl.focus();
        var Sel = document.selection.createRange();
        Sel.moveStart('character', -ctrl.value.length);
        CaretPos = Sel.text.length;
    }
    // Firefox support
    else if (ctrl.selectionStart || ctrl.selectionStart == '0')
        CaretPos = ctrl.selectionStart;
    return (CaretPos);
}


//Set Cursor Position in a Specified Control (Support Both IE & Mozilla)
function movemiddle(cntrl) {

    var control = document.getElementById(cntrl);
    var length = document.getElementById(cntrl).value.length;
    setSelectionRange(control, length, length);
}

function setSelectionRange(input, selectionStart, selectionEnd) {
    if (input.setSelectionRange) {//mOZILLA
        input.focus();
        input.setSelectionRange(selectionStart, selectionStart);
    }
    else if (input.createTextRange) {
        var range = input.createTextRange();
        range.collapse(true);
        range.moveEnd('character', selectionEnd);
        range.moveStart('character', selectionStart);
        range.select();
    }
}
function showWorkSheet(Path, PageHeight, PageWidth, PrnName, PrintOption, Duplicate) {
    try {

        window.open('../../Presentation/Common/WorkSheetPreview.aspx?Path=' + Path + '&PageHeight=' + PageHeight + '&PageWidth=' + PageWidth + '&PrnName=' + PrnName + '&PrintOption=' + PrintOption + '&Duplicate=' + Duplicate + '', "self", "left=200,top=50,width=810,height=640,status=0,resizable=1,scrollbars=1;");

    }

    catch (err) {
    }

}



function ConvertToCurrencyFormat(textboxname) {
    try {
        var n = document.getElementById('ctl00_ContentPlaceHolder1_' + textboxname + '').value.replace(/,/gi, "");
        var result = parseFloat(n).toFixed(2);
        if (result > 0) {
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_' + textboxname + '').value = CurrencyFormat(result);
        }
    }
    catch (err) {
    }
}





function CurrencyFormat(amount) {

    var a = amount.split('.', 2)
    var d = a[1];
    var curamt = amount.substring(0, amount.length - 3);
    var famt = '';
    var ramt = '';
    var start = curamt.length - 1;
    var end = curamt.length;
    var hrdamt = '';
    var thsndamt = '';
    var lastamt = '';

    if (curamt.length < 4) {
        amount = curamt + '.' + d;
    }
    else {
        for (var i = 0; i < curamt.length; i++) {
            famt = curamt.substring(start, end);
            start = start - 1;
            end = end - 1;
            ramt = famt + ramt;
            if (ramt.length < 4) {
                hrdamt = ramt;
            }
            else {
                thsndamt = famt + thsndamt;
                if ((thsndamt.length == 2) || (curamt.length == i + 1)) {
                    thsndamt = thsndamt + ',';
                    lastamt = thsndamt + lastamt;
                    thsndamt = '';
                }
            }
        } 
        amount = lastamt + hrdamt + '.' + d;
    }
    return amount;
}


function eFocus(field) {
    var cntrl = field;
    cntrl.style.color = "Red";
    cntrl.style.fontWeight = "bold";
    //cntrl.style.backgroundColor = "Silver";
}
function eBlur(field) {
    var cn = field;
    cn.style.color = "Black";
    cn.style.fontWeight = "200";
    //cn.style.backgroundColor = "Transparent";
}
function ShowWaivePopup(ID_Master, AsOnDate, IntUptoDate, Amount, SubModule, RecoveryTime, dbname, textboxname, WaiveSessionName,NotAcc,InterestAmount,PrincipalAmount) {
    try {
        var Rebate = 0;
        var WaiveAmount = 0;
        WaiveAmount = window.showModalDialog('../../Presentation/CommonProcess/WaivingPopup.aspx?ID_Master=' + ID_Master + '&AsOnDate=' + AsOnDate + '&IntUptoDate=' + IntUptoDate + '&Amount=' + Amount + '&Rebate=' + Rebate + '&SubModule=' + SubModule + '&RecoveryTime=' + RecoveryTime + '&dbname=' + dbname + '&WaiveSessionName=' + WaiveSessionName + '&NotAcc=' + NotAcc + '&InterestAmount='+InterestAmount+'&PrincipalAmount='+PrincipalAmount+'', "", "status:no;dialogLeft:400;dialogTop:200;resizable:no;dialogWidth:320px;dialogHeight:400px;center:no;edge:raised;location=no;");
        document.getElementById('ctl00_ContentPlaceHolder1_' + textboxname + '').value = CurrencyFormatted(WaiveAmount);
        if (SubModule == "ODGD") {
            document.getElementById('ctl00_ContentPlaceHolder1_txtNetAmount').value = CurrencyFormatted(parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_txtNetAmount').value) - parseFloat(WaiveAmount));
        }
        //document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = WaiveAmount;
        if (SubModule == "SLJL") {
            document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
        }
    }
    catch (err) {
    }
}
function ShowODGDWaivePopup(ID_Master, AsOnDate, IntUptoDate, Amount, Rebate, SubModule, RecoveryTime, dbname, textboxname, WaiveSessionName,NotAcc) {
    try {

        var WaiveAmount = 0;
        if (SubModule == "ODGD") 
        {
            WaiveAmount = window.showModalDialog('../../Presentation/CommonProcess/WaivingPopup.aspx?ID_Master=' + ID_Master + '&AsOnDate=' + AsOnDate + '&IntUptoDate=' + IntUptoDate + '&Amount=' + Amount + '&Rebate=' + Rebate +  '&SubModule=' + SubModule + '&RecoveryTime=' + RecoveryTime + '&dbname=' + dbname + '&WaiveSessionName=' + WaiveSessionName + '&NotAcc=' + NotAcc + '&InterestAmount=0&PrincipalAmount=0', "", "status:no;dialogLeft:400;dialogTop:200;resizable:no;dialogWidth:320px;dialogHeight:400px;center:no;edge:raised;location=no;");
            document.getElementById('ctl00_ContentPlaceHolder1_' + textboxname + '').value = CurrencyFormatted(WaiveAmount);
            document.getElementById('ctl00_ContentPlaceHolder1_txtNetAmount').value = CurrencyFormatted(parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_txtNetAmount').value) - parseFloat(WaiveAmount));
        }
      
       
    }
    catch (err) {
    }

}


function showStFeeSettings(id) {

    window.showModalDialog('../../Presentation/ValueAddedService/FeeSettingsPopUp.aspx?id=' + id + '', "", "status:no;dialogLeft:350;dialogTop:200;resizable:yes;dialogWidth:100px;dialogHeight:255px;center:no;edge:raised;location=no;");

}
//----------------------------------------------------------------------------------






//------------------------- Search over popup---------------------------------------
function showSearchOverPopup(module, branchcode, dbname, searchValue, critrea1, critrea2, critrea3, critrea4, criteria5,criteria6,criteria7) {
    try {
        var $div = window.parent.$('<div />').appendTo('body');
        $div.attr('id', 'divSearchOverPopup');
        $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
        window.parent.$('#divSearchOverPopup').html('<iframe id="searcIframe" frameborder="0" height="485" width="770" src="../../Presentation/Common/SearchOverPopup.aspx?Module=' + module + '&dbname=' + dbname + '&branchcode=' + branchcode + '&searchValue=' + searchValue + '&critrea1=' + critrea1 + '&critrea2=' + critrea2 + '&critrea3=' + critrea3 + '&critrea4=' + critrea4 + '&critrea5=' + criteria5 +'&critrea6='+criteria6+'&critrea7='+criteria7+'"></iframe> ')
                .dialog({
                    title: 'Search',
                    width: 795,
                    height: 525,
                    modal: true
                });
    }
    catch (err) {
    }
}
function closeSearchOverPopup(arry) {
    try {
        window.parent.$('#divSearchOverPopup').dialog('close'); //To close SearchViewDialog
        try {
            var str = arry[0] + "!" + arry[1] + "!" + arry[2] + "!" + arry[3] + "!" + arry[4] + "!" + arry[5] + "!" + arry[6] + "!" + arry[7] + "!" + arry[8]
            var btnname='';
             if (arry[9] > 0 || arry[9] != "") {
            str = str + "!" + arry[9]
        }
        if (arry[10] > 0 || arry[10] != "") {
            str = str + "!" + arry[10]
        }
        if (arry[11] != "") {
            btnname=arry[11];
        }
        if (arry[12] != "") {
            btnname=arry[12];
        }
        }
        
        
        catch (Error) {
        }
        if (arry[0] > 0) {
            window.parent.$('#divPopup iframe').contents().find('#ctl00_ContentPlaceHolder1_HdValue').val(str);
            window.parent.$('#divPopup iframe').contents().find('#ctl00_ContentPlaceHolder1_btnScriptFill'+btnname).click()
        }
    }
    catch (err) {
    }
}
//-------------------------------------------------------------------------
//--------------SearchForm----------------------------------------------------------
function showSearchForm(module, branchcode, dbname, searchValue, critrea1, critrea2, critrea3, critrea4, criteria5) {
    try {
        window.parent.$('#divSearchView').html('<iframe id="searcIframe" frameborder="0" height="460" width="800" src="../../Presentation/Common/Search.aspx?Module=' + module + '&dbname=' + dbname + '&branchcode=' + branchcode + '&searchValue=' + searchValue + '&critrea1=' + critrea1 + '&critrea2=' + critrea2 + '&critrea3=' + critrea3 + '&critrea4=' + critrea4 + '&critrea5=' + criteria5 + '"></iframe> ')
            .dialog({
                title: 'Search',
                width: 820,
                height: 500,
                modal: true
            });
        window.parent.$('#divSearchView').parent().find('.dialog-titlebar-close').click(function(e) {
            window.parent.$('#divSearchView iframe').contents().find('#btnClear').click();
        });
    }
    catch (err) {
    }
}
//----
function closeSearchForm(arry) {
    try {
        var str = arry[0] + "!" + arry[1] + "!" + arry[2] + "!" + arry[3] + "!" + arry[4] + "!" + arry[5] + "!" + arry[6] + "!" + arry[7] + "!" + arry[8]
        if (arry[9] > 0 || arry[9] != "") {
            str = str + "!" + arry[9]
        }
        if (arry[10] > 0 || arry[10] != "") {
            str = str + "!" + arry[10]
        }
        window.parent.$('#divSearchView').dialog('close'); //To close SearchViewDialog

        if (arry[0] > 0) {
            //window.opener.document.getElementById().value = ;window.opener." + EventName + ";window.opener.document.getElementById('" + PassingField + "').focus();self.close();</script>";
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = str;
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();

            //-----------IB notification------------------               
            if (window.parent.$('.branchcode')[0]) {
                if (window.parent.$('.branchcode').attr("branchcodeuser")) {
                    var branchcode = arry[1].substring(0, 3);
                    var branchcodeuser = window.parent.$('.branchcode').attr("branchcodeuser");
                    if (Math.abs(branchcodeuser) != Math.abs(branchcode)) {
                        if (window.parent.$(".IBmsg").is(":visible")) {
                            window.parent.$(".IBmsg").dequeue();
                        }

                        window.parent.$.ajax({
                            type: "POST",
                            data: "{'BranchCode':'" + branchcode + "'}",
                            contentType: "application/json; charset=utf-8",
                            url: "/SCORE/ScoreWebService.asmx/GetBranchName",
                            dataType: "json",
                            success: function(BranchName) {
                                var IBT = 'IB Transaction';
                                if (BranchName.d == 'Invalid BranchCode!') {
                                    IBT = '';
                                }
                                var _left = window.parent.$('.branchcode').position().left;
                                var _top = window.parent.$('.branchcode').position().top - 70;
                                var _mggBox = '<div class="IBmsg">' +
                                                                '<table style="vertical-align: middle;height:100%;margin:0 10px 0 10px;">' +
                                                                '<tr>' +
                                                                    '<td style="text-decoration: blink;color:#ff0000">' + IBT + '</td>' +
                                                                '</tr>' +
                                                                '<tr>' +
                                                                    '<td>' + BranchName.d + '</td>' +
                                                                '</tr>' +
                                                                '</table>' +
                                                            '</div>';

                                window.parent.$(_mggBox).css({ 'left': _left, 'top': _top }).prependTo('body').
                                                show({ effect: 'slide', direction: 'down', speed: 'slow' }).
                                                delay(5000).hide({ effect: 'slide', direction: 'up' });

                                window.parent.$(".IBmsg").click(function() {
                                    window.parent.$(".IBmsg").dequeue();
                                });
                            }
                        });
                    }
                    else {
                        if (window.parent.$(".IBmsg").is(":visible")) {
                            window.parent.$(".IBmsg").dequeue();
                        }
                    }
                }
            }
            //--------------------------------------------
        }
    }
    catch (err) {
    }

}
//------------------------------------------------------------------------------ 
//---------------------------Search Account heads-------------------------------

function showDynamicSearch(module, branchcode, showBranchFilter, showStatusFilter, textBoxFilterCount, textBoxFilterNames, textBoxSearchValue, searchValue, dbname, critrea1, critrea2, critrea3, critrea4, criteria5) {
    try {
        window.parent.$('#divSearchView').html('<iframe id="iframeDynamic" align="left" width="680" height="480" frameborder="0" src="../../Presentation/Common/SearchDynamic.aspx?Module=' + module + '&dbname=' + dbname + '&showBranchFilter=' + showBranchFilter + '&showStatusFilter=' + showStatusFilter + '&textBoxFilterCount=' + textBoxFilterCount + '&textBoxFilterNames=' + textBoxFilterNames + '&branchcode=' + branchcode + '&textBoxSearchValue=' + textBoxSearchValue + '&searchValue=' + searchValue + '&critrea1=' + critrea1 + '&critrea2=' + critrea2 + '&critrea3=' + critrea3 + '&critrea4=' + critrea4 + '&critrea5=' + criteria5 + '"></iframe> ')
            .dialog({
                title: 'Search',
                width: 705,
                height: 520,
                modal: true
            });
    }
    catch (err) {
    }
}
function closeDynamicSearch(arry) {
    try {
        var str = arry[0] + "!" + arry[1] + "!" + arry[2] + "!" + arry[3] + "!" + arry[4] + "!" + arry[5] + "!" + arry[6] + "!" + arry[7] + "!" + arry[8]
        if (arry[9] > 0 || arry[9] != "") {
            str = str + "!" + arry[9]
        }
        if (arry[10] > 0 || arry[10] != "") {
            str = str + "!" + arry[10]
        }
        window.parent.$('#divSearchView').dialog('close'); //To close SearchViewDialog

        if (arry[0] > 0) {
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = str;
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
        }
    }
    catch (err)
         { }
}
//------------------------------------------------------------------------------
//---------------------------Common View Modal----------------------------------
function showModel(window) {
    try {
        $('#divSearchView').html('<iframe frameborder="0" height="550" width="415" src="../../Presentation/Common/View.aspx"></iframe> ');
        $('#divSearchView').dialog({
            title: 'View',
            width: 440,
            height: 590,
            modal: true
        });
    }
    catch (err) {
    }
}
function closeModel(arry) {
    try {
        var str = arry[0] + "!" + arry[1] + "!" + arry[2] + "!" + arry[3] + "!" + arry[4] + "!" + arry[5] + "!" + arry[6] + "!" + arry[7] + "!" + arry[8] + "!"
        window.parent.jQuery('#divSearchView').dialog('close'); //To close SearchViewDialog

        window.parent.document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = str;
        window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
    }
    catch (err) { }
}
//--------------------------------------------------------------------------------

//----------------------Batch Modal-----------------------------------------------
function showBatch(url) {

    try {
        window.parent.$('#divBatch').html('<iframe id="searcIframe" frameborder="0" height="520" width="780" src="' + url + '"></iframe> ');
        window.parent.$('#divBatch').dialog({
            title: 'Select Batch',
            width: 805,
            height: 560,
            closeOnEscape: false,
            modal: true
        });
        window.parent.$('#divBatch').parent().find('.dialog-titlebar-close').hide(); //hide title close button
    }
    catch (err) {
    }
}
function closeBatch(arry) {
    try {
        var str = arry[1];
        var str1 = arry[0];

        window.parent.jQuery('#divBatch').dialog('close'); //To close SearchViewDialog

        if (str != "") {
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = str + "!" + str1;
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btnDelete').click();
        }
        else {
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = str1;
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
        }
    }
    catch (err) {
    }
}
//--------------------------------------------------------------------------------------------

//------------------------Delete Confirmation Modal------------------------------------------
//Delete Reason Modal
function showDeleteModel(window, dbname) {
    try {
        $('#divDelete').html('<iframe frameborder="0" height="160" width="330" src="../../Presentation/Common/DeleteReson.aspx?DBNAME=' + dbname + '"></iframe> ');
        $('#divDelete').dialog({
            title: 'Confirm',
            width: 360,
            height: 200,
            modal: true
        });
    }
    catch (err) {
    }
}
//Reverse Reason Modal
function showReverseModel(window, usercode, dbname, effctDate, machine, branchcodeuser) {
    try {
        $('#divDelete').html('<iframe frameborder="0" height="160" width="330" src="../../Presentation/Common/DeleteReson.aspx?USERCODE=' + usercode + '&DBNAME=' + dbname + '&EFFECTDATE=' + effctDate + '&MACHINE=' + machine + '&BRANCHCODEUSER=' + branchcodeuser + '&"></iframe> ');
        $('#divDelete').dialog({
            title: 'Confirm',
            width: 360,
            height: 200,
            modal: true
        });
    }
    catch (err) {
    }
}
function closeDeleteModel(arry)//Common Closing for Delete,Reverse
{
    window.parent.jQuery('#divDelete').dialog('close');
    var str = arry[0];
    window.parent.document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = str;
    window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btnDelete').click();
}
//-------------------------------------------------------------------------------------------

//--------------------------Common Message Box------------------------------------------------
function MessageBox(msgCaption, msgType, msgMessage, msgImage, focusbtn, scriptfillclk, functionName) {
    var result = 0;
    if (msgType == 1) {
        $(document).ready(function() {
            $('<div></div>').text(msgMessage).dialog({
                title: msgCaption,
                modal: true,
                show: 'fade',
                hide: 'fade',
                minWidth: 300,
                buttons: {
                    'Ok': function() {
                        if (scriptfillclk == 1) {
                            document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = '1';
                            document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                        }
                        $(this).dialog('close')
                    }
                }
            }).parent().find('.dialog-titlebar-close').hide();
        });
    }
    if (msgType == 2) {
        $(document).ready(function() {
            $('<div></div>').text(msgMessage).dialog({
                title: msgCaption,
                modal: true,
                show: 'fade',
                hide: 'fade',
                minWidth: 300,
                buttons: {
                    'Ok': function() {
                        if (scriptfillclk == 1) {
                            document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = '1';
                            document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                        }
                        $(this).dialog('close')
                    },
                    'Cancel': function() {
                        if (scriptfillclk == 1) {
                            document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = '2';
                            document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                        }
                        $(this).dialog('close')
                    }
                }
            }).parent().find('.dialog-titlebar-close').hide();
        });
    }
    if (msgType == 3) {
        $(document).ready(function() {
            $('<div></div>').text(msgMessage).dialog({
                title: msgCaption,
                modal: true,
                show: 'fade',
                hide: 'fade',
                minWidth: 300,
                buttons: {
                    'Yes': function() {
                        if (scriptfillclk == 1) {
                            document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = '1';
                            document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                        }
                        $(this).dialog('close')
                    },
                    'No': function() {
                        if (scriptfillclk == 1) {
                            document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = '2';
                            document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                        }
                        $(this).dialog('close')
                    }
                }
            }).parent().find('.dialog-titlebar-close').hide();
        });
    }
}
//---------------------------------------------------------------------------------------
//-------------------ID card Print MessageBox------------------------------------------

function IDCardMessageBox(msgCaption, msgType, msgMessage, msgImage, focusbtn, scriptfillclk, functionName) {
    $(document).ready(function() {
        $('#divMessageBox').text(msgMessage);
        $('#divMessageBox').dialog({
            title: msgCaption,
            modal: true,
            show: 'fade',
            hide: 'fade',
            width: 400,
            buttons: {
                'Yes': function() {
                    if (scriptfillclk == 1) {
                        document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = '1';
                        document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                    }
                    $(this).dialog('close')
                },
                'No': function() {
                    if (scriptfillclk == 1) {
                        document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = '2';
                        document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                    }
                    $(this).dialog('close')
                }
            }
        });
    });


}
//---------------------------------------------------------------------------------
//-------------------Multiple Images---------------------------------------------------
function showMultipleImages(AccountId, AccountNo, DbName, Chqdate, SubModule) {
    //    var arry = new Array(1);
    //    arry = window.showModalDialog('../../Presentation/Common/MultipleImageShow.aspx?AccountId=' + AccountId + '&AccountNo=' + AccountNo + '&DbName=' + DbName + '&Chqdate=' + Chqdate + '&SubModule=' + SubModule + '', "", "status:no;dialogLeft:800;dialogTop:400;resizable:no;dialogWidth:398px;dialogHeight:279px;center:yes;edge:raised;toolbar=no,menubar=no,location=no,titlebar=no");

    try {
        $('#divMessageBox').html('<iframe id="photoSign" frameborder="0" height="300" width="360" src="../../Presentation/Common/MultipleImageShow.aspx?AccountId=' + AccountId + '&AccountNo=' + AccountNo + '&DbName=' + DbName + '&Chqdate=' + Chqdate + '&SubModule=' + SubModule + '"></iframe> ');
        $('#divMessageBox').dialog({
            title: 'Photo-Sign',
            width: 390,
            height: 345,
            modal: true
        });
    }
    catch (err) {
    }


}


//-------------------------Show Expense Popup with Session-------------------------------
function showExpenseFormWithSession(textboxname, SessionName) {
    try {
        var $div = $('<div />').appendTo('body');
        $div.attr('id', 'divExpensePopUp');
        $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
        window.parent.$('#divExpensePopUp').html('<iframe id="searcIframe" frameborder="0" height="350" width="375" src="../../Presentation/Expense/ExpensePopup.aspx?SessionName=' + SessionName + '&txtBox=' + textboxname + '"></iframe> ')
            .dialog({
                title: 'Expense Posting',
                width: 400,
                height: 390,
                closeOnEscape: false,
                modal: true
            });
        window.parent.$('#divExpensePopUp').parent().find('.dialog-titlebar-close').hide(); //hide title close button
    }
    catch (err) {
    }
}
function closeExpenseFormWithSession(expAmount, textboxname) {
    try {
        window.parent.$('#divExpensePopUp').dialog('close');
        window.parent.document.getElementById('ctl00_ContentPlaceHolder1_' + textboxname + '').value = CurrencyFormatted(expAmount);
        window.parent.document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = textboxname;
        window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
    }
    catch (err) {
    }
}
//---------------------------------------------------------------------------------------

//--------------------------Show Tax PopUp With Session----------------------------------
function showTaxPopUpFormWithSession(textboxname, SessionName) {
    try {
        var $div = $('<div />').appendTo('body');
        $div.attr('id', 'divTaxPopUp');
        $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
        window.parent.$('#divTaxPopUp').html('<iframe id="searcIframe" frameborder="0" height="325" width="260" src="../../Presentation/Common/TaxPopup.aspx?SessionName=' + SessionName + '&txtBox=' + textboxname + '"></iframe> ')
            .dialog({
                title: 'Service Tax Posting',
                width: 285,
                height: 365,
                closeOnEscape: false,
                modal: true
            });
        window.parent.$('#divTaxPopUp').parent().find('.dialog-titlebar-close').hide(); //hide title close button
    }
    catch (err) {
    }
}
function closeTaxPopUpFormWithSession(taxAmount, textboxname) {
    try {
        window.parent.$('#divTaxPopUp').dialog('close');
        window.parent.document.getElementById('ctl00_ContentPlaceHolder1_' + textboxname + '').value = CurrencyFormatted(taxAmount);
        window.parent.document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = textboxname;
        window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();

    }
    catch (err) {
    }
}

//---------------------------------------------------------------------------------------

//-------------------------Freeze,Lien Message--------------------------------------------

function GetFreezAndLienDetails(msgFreeze, msgLien, msgSplInstr, validate) {
    var result = 0;
    $(document).ready(function() {
        $('#divMessageBox').html(msgFreeze + "<br/>" + msgLien + "<br/>" + msgSplInstr);
        $('#divMessageBox').dialog({
            title: 'Freeze & Lien Details',
            modal: true,
            show: 'fade',
            hide: 'fade',
            width: 400,
            buttons: { 'Ok': function() { $(this).dialog('close') } }
        });
    });
}
//------------
function FreezAndLienDetails() {
    try {
        window.parent.$('#divMessageBox').empty();
        // window.parent.$('#divMessageBox').html('<iframe id="searcIframe" frameborder="0" height="190" width="370"></iframe>');
        // window.parent.$('#divMessageBox #searcIframe').attr("src", "../../Presentation/Common/Freeze.aspx");

        $("#divMessageBox").load("../../Presentation/Common/Freeze.aspx", {}, function(content) { }).dialog({
            title: 'Freeze & Lien Details',
            width: 395,
            height: 230,
            modal: true,
            show: 'fade',
            hide: 'fade'
        });
    }
    catch (err) {
    }

}
//-------------
function FreezAndLienDetailsover() {
    window.showModalDialog('../../Presentation/Common/Freeze.aspx', "", "status:no;resizable:yes;dialogWidth:360px;dialogHeight:190px;center:yes;edge:raised;scroll:yes;");
}
//----------------------Disable Second click-------------------------------------------

function DisableClick(btn) {
    document.getElementById('ctl00_ContentPlaceHolder1_' + btn + '').disabled = true;
}
//-----------QuickMenu Gobutton click--------            
function quickMenuGo() {
//var txtCont = document.getElementById('<%= Page.Master.FindControl("ContentPlaceHolder1").FindControl("HidCustomer").ClientID %>');
    QuickMenuLoad();
    
}
//-----------QuickMenu Gobutton Enter--------    
function quickMenuEnter(e) {
    if (!e) {
        e = window.event;
    }
    if (e.keyCode == 13) {
        QuickMenuLoad();
    }
}
//---------------------------------------------------------------------------------------------------------
function SmsOrEmailClick(btn) {

   var qkMenuCode = "s";// $("#ctl00_txtQuick").val();
    var qkLength = qkMenuCode.length;// $("#ctl00_txtQuick").val().length;

    if (qkMenuCode != "" && qkMenuCode != "Quick Menu") {
        var branchcodeuser = $('#ctl00_HidBranchCodeUser').val();
        var usercode = $('#ctl00_HidUserCode').val();
        var machinecode = $('#ctl00_HidMachineCode').val();
        var menucode = $('#ctl00_HidMenucode').val();
        var dbname = $('#ctl00_HidDbName').val();
        var trmode = $('#ctl00_HidMode').val();
       // var Customer=$('#ctl00_HidCustomer').val();
        if (Tools(qkMenuCode))//Calc,Reck
        { }       
        //Added on 08/09/2015 for SMS and Email Services
         else if (QuickCustomerServices(qkMenuCode, branchcodeuser, usercode, machinecode, menucode, trmode, dbname))//Quick customer popup
        { }
        else {

            Loading();
            $('#ctl00_HidQuick').val(1);
            document.getElementById('ctl00_btnScriptF').click();
        }
    }
}

//-------------------------------------------------------------------------------------------------------------------
function QuickMenuLoad() {
    var qkMenuCode = $("#ctl00_txtQuick").val();
    var qkLength = $("#ctl00_txtQuick").val().length;

    if (qkMenuCode != "" && qkMenuCode != "Quick Menu") {
        var branchcodeuser = $('#ctl00_HidBranchCodeUser').val();
        var usercode = $('#ctl00_HidUserCode').val();
        var machinecode = $('#ctl00_HidMachineCode').val();
        var menucode = $('#ctl00_HidMenucode').val();
        var dbname = $('#ctl00_HidDbName').val();
        var trmode = $('#ctl00_HidMode').val();
       // var Customer=$('#ctl00_HidCustomer').val();
        if (Tools(qkMenuCode))//Calc,Reck
        { }
        else if (QuickNomination(qkMenuCode, branchcodeuser, usercode, machinecode, menucode, trmode, dbname))   //Nomination popup
        { }
        else if (QuickCustomer(qkMenuCode, branchcodeuser, usercode, machinecode, menucode, trmode, dbname))//Quick customer popup
        { }
        //Added on 08/09/2015 for SMS and Email Services
        else if (QuickCustomerServices(qkMenuCode, branchcodeuser, usercode, machinecode, menucode, trmode, dbname))//Quick customer popup
        { }
        else {

            Loading();
            $('#ctl00_HidQuick').val(1);
            document.getElementById('ctl00_btnScriptF').click();
        }
    }
}
//------------------------------------------------------CustomerServices---------------------------------------------------
function QuickCustomerServices(quickcode, branchcodeuser, usercode, machinecode, menucode, trmode, dbname)//showing Quickcustomer from pages
{
    try {
       var customerid="";       
       var branchCode="";
       var typeCode="";
       var accountNo="";
       var trmodesubstring =trmode.substr(0, 2);
          if (quickcode.toUpperCase() == "S") {          
            if(trmodesubstring  =="DD" || trmodesubstring  =="TL" ||trmodesubstring  =="SL" || trmodesubstring  =="PD" ) 
                   {
                    // if((window.parent.$('input[type="text"][class="customer-number"]').is(':visible') == false)&&(trmode =="SLDLOP"||trmode=="SLDLMS"))
                    // return;
                   // if (window.parent.$('input[type="text"][class="customer-number"]').is(':disabled') == false)//Useraction=insert or not 
                  //{
                  //if (window.parent.$('input[type="text"][class="transaccno"]').val()!="")
                  //{
                  if(window.parent.$('input[type="text"][class="branchcode"]').is(':visible') )
                    branchCode = window.parent.$('input[type="text"][class="branchcode"]').val();  
                      if(window.parent.$('input[type="text"][class="typecode"]').is(':visible') )                                  
                    typeCode = window.parent.$('input[type="text"][class="typecode"]').val(); 
                      if(window.parent.$('input[type="text"][class="typecodepopup"]').is(':visible') )                                  
                    typeCode = window.parent.$('input[type="text"][class="typecodepopup"]').val();                                          
                        if(window.parent.$('input[type="text"][class="transaccno"]').is(':visible') )
                    accountNo = window.parent.$('input[type="text"][class="transaccno"]').val();                   
                                   
                    //accountNo = window.parent.$('input[type="text"][class="branchcode"]').val()+window.parent.$('input[type="text"][class="typecode"]').val()+window.parent.$('input[type="text"][class="transaccno"]').val();
                    var $div = $('<div />').appendTo('body');
                    $div.attr('id', 'divPopup');
                    $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
                    
                    //window.parent.$('#divPopup').html('<iframe frameborder="0" height="100%" width="100%" src="../../Presentation/Common/CustomerServices.aspx?BRANCHCODEUSER=' + branchcodeuser + '&USERCODE=' + usercode + '&MACHINECODE=' + machinecode + '&MENUCODE=' + menucode +'&DBNAME='+ dbname + '&CUSTOMERID='+customerid+'&ACCOUNTNO='+accountNo+'"></iframe>')
                   
                    window.parent.$('#divPopup').html('<iframe frameborder="0" height="100%" width="100%" src="../../Presentation/Common/CustomerServices.aspx?BRANCHCODEUSER=' + branchcodeuser + '&USERCODE=' + usercode + '&MACHINECODE=' + machinecode + '&MENUCODE=' + menucode +'&DBNAME='+ dbname + '&CUSTOMERID='+customerid+'&BRANCH='+branchCode+'&TYPE='+ typeCode + '&ACCOUNTNO='+accountNo+'&TRANSACTIONMODE='+trmode+'"></iframe>')
                        .dialog({
                            title: 'Customer Services',
                            width: 700,
                            height: 350,
                            modal: true   
                        });
                        
                    return true;

                //}
                //else
                //{
                //    alert('Please Enter Account Number')
                //    return true;
               // }
               
                //}
                
               // else {
               //     alert('Press Add Button')
                //    return true;
                //}
            }
            else {
                alert('QuickCustomerService not allowed in this page ')
                return true;
            }
        }
    }
    catch (err) {
    }
}

function closeQuickCustomerServices()//closing  QuickCustomer
{
    try {
//        if (custNo != 'close') {
//        if(member==1)
//        {
//          window.parent.$('input[class="button-search-member"]').click();
//        }
//        else if(member==2)
//        {
//         window.parent.$('input[class="button-search-customer"]').click();  
//        }
//        else if(member==3)
//        {
//         window.parent.$('input[class="button-search-number"]').click();  
//        }
//        else
//        {
////            window.parent.$('input[type="text"][class="customer-number"]').val(custNo); //CustomerNo textbox 
////            window.parent.$('input[class="button-search-customer"]').click();           //Customer search button click
//            if (window.parent.$('input[type="text"][class="transaccno"]').is(':visible') == true) //Account number textbox(Transaction Form,Disna(17/04/2015))
//             {
//               window.parent.$('input[class="button-search-account"]').click();  //Account number search button click
//             }
//             else
//             {
//              window.parent.$('input[type="text"][class="customer-number"]').val(custNo); //CustomerNo textbox 
//              window.parent.$('input[class="button-search-customer"]').click();           //Customer search button click
//             }
//             }
//        }
        window.parent.$('#divPopup').dialog('close');
        }
   
    catch (er) {
    }
}
//-------------------------------------------------------------------------------------------------------------------------
//---------------------------------Nomination popup------------------------------
//function QuickNomination(quickcode, branchcodeuser, usercode, machinecode, menucode, trmode, dbname) {
//    try {

//        if (quickcode.toUpperCase() == "N") {
//            if (trmode.toUpperCase().substring(4, 6) == "MS") {
//                var module = trmode.toUpperCase().substring(0, 2);
//                if (module == "SH" || module == "PD" ||
//                    module == "TD" || module == "DD" ||
//                    module == "OD" || module == "SL"
//                  ) {
//                    if (window.parent.$('input[type="text"][class="nominee-number"]').is(':disabled') == false)//Useraction=insert or not
//                    {
//                        var $div = $('<div />').appendTo('body');
//                        $div.attr('id', 'divPopup');
//                        $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
//                        window.parent.$('#divPopup').html('<iframe id="searcIframe" frameborder="0" height="390" width="420" src="../../Presentation/General/NominationPopup.aspx?BRANCHCODEUSER=' + branchcodeuser + '&USERCODE=' + usercode + '&MACHINECODE=' + machinecode + '&MENUCODE=' + menucode + '&DBNAME=' + dbname + '"></iframe>')
//                            .dialog({
//                                title: 'Quick Nomination',
//                                width: 445,
//                                height: 440,
//                                modal: true
//                            });
//                        window.parent.$('#divPopup').parent().find('.dialog-titlebar-close').hide(); 
//                        return true;
//                    }
//                    else {
//                        alert('Press Add Button')
//                        return true;
//                    }
//                }
//                else {
//                    alert('Quick Nomination not allowed in this page ')
//                    return true;
//                }
//            }
//            else {
//                alert('Quick Nomination not allowed in this page ')
//                return true;
//            }
//        }
//    }
//    catch (err) {
//    }

//}

function QuickNomination(quickcode, branchcodeuser, usercode, machinecode, menucode, trmode, dbname) {
    try {

        if (quickcode.toUpperCase() == "N") {
            if (trmode.toUpperCase().substring(4, 6) == "MS") {
                var module = trmode.toUpperCase().substring(0, 2);
                if (module == "SH" || module == "PD" ||
                    module == "TD" || module == "DD" ||
                    module == "OD" || module == "SL"
                  ) {
                    if (window.parent.$('input[type="text"][class="nominee-number"]').is(':disabled') == false)//Useraction=insert or not
                    {
                       var $div = $('<div />').appendTo('body');
        $div.attr('id', 'divPopup');
        $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
        window.parent.$('#divPopup').html('<iframe id="searcIframe" frameborder="0" height="480" width="800" src="../../Presentation/General/NominationCommon.aspx?Module=' + module + '&dbname=' + dbname + '&branchcodeuser=' + branchcodeuser + '&machinecode=' + machinecode + '&menucode=' + menucode + '&usercode=' + usercode + '&critrea3=' + '' + '&critrea4=' + trmode.toUpperCase().substring(0, 4) + '"></iframe> ')
            .dialog({
                title: 'Search',
                width: 815,
                height: 520,
                modal: true
            });
        window.parent.$('#divPopup').parent().find('.dialog-titlebar-close').click(function(e) {
            window.parent.$('#divPopup iframe').contents().find('#btnClear').click();
        });
                        return true;
                    }
                    else {
                        alert('Press Add Button')
                        return true;
                    }
                }
                else {
                    alert('Quick Nomination not allowed in this page ')
                    return true;
                }
            }
            else {
                alert('Quick Nomination not allowed in this page ')
                return true;
            }
        }
    }
    catch (err) {
    }

}
function closeQuickNomination(NomNo)//closing  QuickCustomer
{
    try {
        if (NomNo != 'close') {
            //window.parent.$('input[type="text"][class="nominee-number"]').val(NomNo); //NominationNo textbox   
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_HidNomination').value=NomNo;             
            window.parent.$('input[class="button-search-nominee"]').click();           //Nominee search button click
        }
        window.parent.$('#divPopup').dialog('close');
    }
    catch (er) {
    }
}
//---------------------------------End Nomination popup--------------------------

//---------------------------------Common Nomination popup------------------------------
function showSearchNominationCommonForm(module, branchcodeuser, dbname, machinecode, menucode, usercode, critrea3, critrea4, criteria5) {
    try {
        var $div = $('<div />').appendTo('body');
        $div.attr('id', 'divPopup');
        $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
        window.parent.$('#divPopup').html('<iframe id="searcIframe" frameborder="0" height="480" width="800" src="../../Presentation/General/NominationCommon.aspx?Module=' + module + '&dbname=' + dbname + '&branchcodeuser=' + branchcodeuser + '&machinecode=' + machinecode + '&menucode=' + menucode + '&usercode=' + usercode + '&critrea3=' + critrea3 + '&critrea4=' + critrea4 + '&critrea5=' + criteria5 + '"></iframe> ')
            .dialog({
                title: 'Search',
                width: 815,
                height: 520,
                modal: true
            });
        window.parent.$('#divPopup').parent().find('.dialog-titlebar-close').click(function(e) {
            window.parent.$('#divPopup iframe').contents().find('#btnClear').click();
        });
    }
    catch (err) {
    }
}
//----
function closeSearchNominationCommonForm(custNo)//closing  Common Nomination popup
{
    try {
        if (custNo != 'close') {
            window.parent.$('input[type="text"][class="customer-number"]').val(custNo); //CustomerNo textbox         
            window.parent.$('input[class="button-search-customer"]').click();           //Customer search button click
        }
        window.parent.$('#divPopup').dialog('close');
    }
    catch (er) {
    }
}
//---------------------------------End Common Nomination popup--------------------------

//---------------------------------QuickCustomer popup---------------------------

function QuickCustomer(quickcode, branchcodeuser, usercode, machinecode, menucode, trmode, dbname)//showing Quickcustomer from pages
{
    try {
       
       var customerid="";
        customerid=document.getElementById('ctl00_ContentPlaceHolder1_HidCustomer').value;
        var member="";
        if (window.parent.$('input[type="text"][class="customer-number"]').is(':visible') == true) //Account number textbox(Transaction Form,Disna(17/04/2015))
             {
        member=document.getElementById('ctl00_ContentPlaceHolder1_HidMember').value; 
        }
          if (quickcode.toUpperCase() == "Q") {
            if (trmode == "DDSBMS" ||trmode == "TDFDMS"||trmode == "GNNOMS" ||trmode=="DDSBTR"||trmode=="SHNSMS"||trmode=="SHNSTR"||trmode=="TDFDIP"||trmode=="TDFDMP"||trmode=="TDFDPM"||trmode=="TDFDPX"||trmode=="DDSBAR"||trmode=="DDSBOP"||trmode=="DDCAOP"||trmode=="DDTDMS"||trmode=="DDTDTR"||trmode=="DDTDOP"||trmode=="TDFDOP"||trmode=="TDCCOP"||trmode=="ODLOBO"||trmode=="ODGDAP"||trmode=="ODGDMS"||trmode=="ODGDOP"||
                trmode == "TDFDRN"||trmode=="ODLOMS"||trmode=="ODLORT"||trmode=="ODLOIP"||trmode=="ODLOCL"||trmode=="ODLOTR"||trmode=="SLJLMS"||trmode=="SLJLTR"||trmode=="SLJLRN"||trmode=="SLJLAT"||trmode=="SLDLMS"||trmode=="TDCDMS"||trmode=="TDCDOP"||trmode=="TDCDIP"||trmode=="TDCDMP"||trmode=="TDCDPM"||trmode=="TDCDRN"||trmode=="TDSDMS"||trmode=="ODMDRN"||trmode=="ODGDAR"||trmode=="ODGDRT"||trmode=="ODGDRU"||
                trmode=="SLDLTR"||trmode=="PDRDDR"||trmode=="PDRDAR"||trmode=="PDRDMP"||trmode=="PDRDPM"||trmode=="TDCCMS"||trmode=="TDCCRN"||trmode=="TDCCMP"||trmode=="TDCCPM"||trmode=="TDCCPX"||trmode=="DDCAMS"||trmode=="TDSDOP"||trmode=="TDSDIP"||trmode=="TDSDRP"||trmode=="TDSDMP"||trmode=="TDSDPM"||trmode=="TDCHMS"||trmode=="TDCHOP"||trmode=="ODMDPT"||trmode=="ODGDBD"||trmode=="ODGDPT"||trmode=="ODGDCL"||
                trmode=="DDCATR"||trmode=="PDRDMS"||trmode=="TLMLOP"||trmode=="TLMLAP"||trmode=="TLMLSA"||trmode=="TLMLMS"||trmode=="TLMLPT"||trmode=="TLMLRT"||trmode=="TLMLSU"||trmode=="TLMLRC"||trmode=="TLMLAR"||trmode=="TDCHMP"||trmode=="TDCHRN"||trmode=="TDEMMS"||trmode=="TDEMOP"||trmode=="TDEMIP"||trmode=="TDEMMP"||trmode=="TDEDMS"||trmode=="SLDLOP"||trmode=="SLJLOP"||
                trmode=="TLSLOP"||trmode=="TLSLAP"||trmode=="TLSLSA"||trmode=="TLSLMS"||trmode=="TLSLLO"||trmode=="TLSLPT"||trmode=="TLSLRT"||trmode=="TLSLSU"||trmode=="TLSLRC"||trmode=="TLODOP"||trmode=="TLODAP"||trmode=="TDEDOP"||trmode=="TDEDMP"||trmode=="PDRDOP"||trmode=="PDDDMS"||trmode=="PDDDOP"||trmode=="PDDDDR"||trmode=="PDDDAR"||trmode=="GNGNVG"||trmode=="GNGNVR"||
                trmode=="TLODSA"||trmode=="TLODMS"||trmode=="TLODBP"||trmode=="TLODSU"||trmode=="TLODPT"||trmode=="TLODRT"||trmode=="TLODRN"||trmode=="TLODLE"||trmode=="TLSDOP"||trmode=="TLSDAP"||trmode=="TLSDSA"||trmode=="PDDDMP"||trmode=="PDDDPM"||trmode=="PDDDPP"||trmode=="PDHDMS"||trmode=="PDHDOP"||trmode=="PDHDDR"||trmode=="PDHDAR"||
                trmode=="TLSDMS"||trmode=="TLSDLO"||trmode=="TLSDBP"||trmode=="TLSDLE"||trmode=="TLSDPT"||trmode=="TLSDRT"||trmode=="TLSDRN"||trmode=="TLSDSU"||trmode=="PDHDPM"||trmode=="PDHDMP"||trmode=="PDHDPP"||trmode=="PDGDMS"||trmode=="PDGDOP"||trmode=="PDGDDR"||trmode=="PDGDAR"||trmode=="PDGDMP"||trmode=="PDGDPM"||trmode=="ODLOOP"
                  ) {
                   if((window.parent.$('input[type="text"][class="customer-number"]').is(':visible') == false)&&(trmode =="SLDLOP"||trmode=="SLDLMS"))
                   return;
                  if (window.parent.$('input[type="text"][class="customer-number"]').is(':disabled') == false)//Useraction=insert or not 
                {
                if (window.parent.$('input[type="text"][class="transaccno"]').val()!="")
                {
                    var $div = $('<div />').appendTo('body');
                    $div.attr('id', 'divPopup');
                    $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });

                    window.parent.$('#divPopup').html('<iframe frameborder="0" height="100%" width="100%" src="../../Presentation/Common/QuickCustomerPopup.aspx?BRANCHCODEUSER=' + branchcodeuser + '&USERCODE=' + usercode + '&MACHINECODE=' + machinecode + '&MENUCODE=' + menucode +'&DBNAME='+ dbname + '&CUSTOMERID='+customerid+'&MEMBER='+member+'"></iframe>')
                        .dialog({
                            title: 'Quick Customer',
                            width: 850,
                            height: 550,
                            modal: true
                        });
                        
                    return true;

                }
                else{
                alert('Please Enter Account Number')
                    return true;
                }
                }
                
                else {
                    alert('Press Add Button')
                    return true;
                }
            }
            else {
                alert('QuickCustomer not allowed in this page ')
                return true;
            }
        }
    }
    catch (err) {
    }
}
function closeQuickCustomer(custNo,member)//closing  QuickCustomer
{
    try {
        if (custNo != 'close') {
        if(member==1)
        {
          window.parent.$('input[class="button-search-member"]').click();
        }
        else if(member==2)
        {
         window.parent.$('input[class="button-search-customer"]').click();  
        }
        else if(member==3)
        {
         window.parent.$('input[class="button-search-number"]').click();  
        }
        else
        {
//            window.parent.$('input[type="text"][class="customer-number"]').val(custNo); //CustomerNo textbox 
//            window.parent.$('input[class="button-search-customer"]').click();           //Customer search button click
            if (window.parent.$('input[type="text"][class="transaccno"]').is(':visible') == true) //Account number textbox(Transaction Form,Disna(17/04/2015))
             {
               window.parent.$('input[class="button-search-account"]').click();  //Account number search button click
             }
             else
             {
              window.parent.$('input[type="text"][class="customer-number"]').val(custNo); //CustomerNo textbox 
              window.parent.$('input[class="button-search-customer"]').click();           //Customer search button click
             }
             }
        }
        window.parent.$('#divPopup').dialog('close');
    }
    catch (er) {
    }
}
//----------------------Top Loading--------------------------------
function Loading() {
    $(document).ready(function() {
        $('<div class="loading">Loading...</div>').prependTo('body').show().position({
            my: "center top",
            at: "center top",
            of: window
        }
         );
    })
}

//----------------------Show Notification If IB Transaction-|-from popup-->(closeSearchForm)-----------------



function IBmsg(id, branchcodeuser) {

    var branchcode = $('#ctl00_ContentPlaceHolder1_' + id).val();
    if (branchcode != '' && branchcodeuser != '') {
        if (Math.abs(branchcodeuser) != Math.abs(branchcode)) {
            if ($(".IBmsg").is(":visible")) {
                $(".IBmsg").dequeue();
            }
            //Webservice
            $.ajax({
                type: "POST",
                data: "{'BranchCode':'" + branchcode + "'}",
                contentType: "application/json; charset=utf-8",
                url: "/SCORE/ScoreWebService.asmx/GetBranchName",
                dataType: "json",
                success: function(BranchName) {
                    var IBT = 'IB Transaction';
                    if (BranchName.d == 'Invalid BranchCode!') {
                        IBT = '';
                    }
                    var _left = $('#ctl00_ContentPlaceHolder1_' + id).position().left;
                    var _top = $('#ctl00_ContentPlaceHolder1_' + id).position().top - 70;

                    var _mggBox = '<div class="IBmsg">' +
                                                    '<table style="vertical-align: middle;height:100%;margin:0 10px 0 10px;">' +
                                                    '<tr>' +
                                                        '<td style="text-decoration: blink;color:#ff0000">' + IBT + '</td>' +
                                                    '</tr>' +
                                                    '<tr>' +
                                                        '<td>' + BranchName.d + '</td>' +
                                                    '</tr>' +
                                                    '</table>' +
                                                '</div>';

                    $(_mggBox).css({ 'left': _left, 'top': _top }).prependTo('body').
                                    show({ effect: 'slide', direction: 'down', speed: 'slow' }).
                                    delay(5000).hide({ effect: 'slide', direction: 'up' });
                    $(".IBmsg").click(function() {
                        $(".IBmsg").dequeue();
                    });
                }
            });
        }
        else {
            if ($(".IBmsg").is(":visible")) {
                $(".IBmsg").dequeue();
            }
        }
    }
}
//----------------------Tools--CALC,RECK--------------------------        
function Tools(Code) {
    try {
        if (Code.toUpperCase() == "C" || Code.toUpperCase() == "TOCALC") {
            window.open('../../Presentation/Common/ScoreCalculator.html', "Calculator", "status=0,resizable=0,width=235,height=380,center:1,edge:raised;");
            return true;
        }
        else if (Code.toUpperCase() == "R" || Code.toUpperCase() == "TORECK") {
            window.open('../../Presentation/Common/ScoreReckoner.html', "Reckoner", "status=0,resizable=0,width=262,height=365,center:1,edge:raised;");
            return true;
        }
        else if (Code.toUpperCase() == "F" || Code.toUpperCase() == "VAFPRG") {

            var branchcodeuser = $('#ctl00_HidBranchCodeUser').val();
            var usercode = $('#ctl00_HidUserCode').val();
            var machinecode = $('#ctl00_HidMachineCode').val();
            var menucode = $('#ctl00_HidMenucode').val();
            var dbname = $('#ctl00_HidDbName').val();
            var trmode = $('#ctl00_HidMode').val();
            var $div = $('<div />').appendTo('body');
            $div.attr('id', 'divPopup');
            $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
            window.parent.$('#divPopup').html('<iframe id="searcIframe" frameborder="0" height="250" width="480" src="../../Presentation/Security/FingerRegistration.aspx?BRANCHCODEUSER=' + branchcodeuser + '&USERCODE=' + usercode + '&MACHINECODE=' + machinecode + '&MENUCODE=' + menucode + '&DBNAME=' + dbname + '"></iframe>')
                            .dialog({
                                title: 'FingerPrint Registartion',
                                width: 500,
                                height: 280,
                                modal: true
                            });
            return true;
        }
    }
    catch (err) {
    }
}
//---------------------------------------------------------------------------------    
      function BrcodeSelect(txt) {
$('#ctl00_ContentPlaceHolder1_' + txt).select();
}
//---------------------------------Notification-----------------------------------------
function Notification(Message) {
    $('#divMessageBox').text(Message);
    $('#divMessageBox').dialog({
        title: 'Notification',
        position: ['right', 'bottom'],
        show: { effect: 'slide', direction: 'down' },
        hide: { effect: 'slide', direction: 'down' },
        buttons: {
            'View': function() {
                $('<div id="divFullsummary"></div>').dialog({
                    title: "Notifications",
                    height: 400,
                    width: 600,
                    buttons: {
                        'Close': function() { $(this).dialog('close'); }
                    }
                });
                //                            document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = '1'; 
                //                            document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();

                $(this).dialog('close')
            },
            'Not Now': function() {
                //                            document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = '2'; 
                //                            document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click(); 
                $(this).dialog('close')
            }
        }
    });

}
//-------------------------------Payment Transfer Popup--------------------------------------------

function ShowPaymentTransferWithSessionName(useraction, branchcode, dbname, accountcode, criteria1, criteria2, chkTransfer, SessionName) {
    try {
        var $div = $('<div />').appendTo('body');
        $div.attr('id', 'divPopup');
        $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
        window.parent.$('#divPopup').html('<iframe id="searcIframe" frameborder="0" height="250" width="520" src="../../Presentation/Common/PaymentTransferPopup.aspx?UserAction=' + useraction + '&BranchCode=' + branchcode + '&Dbname=' + dbname + '&AccountCode=' + accountcode + '&Criteria1=' + criteria1 + '&Criteria2=' + criteria2 + '&SessionName=' + SessionName + '&chkTransfer=' + chkTransfer + '"></iframe> ')
            .dialog({
                title: 'Trnsfer to Account',
                width: 535,
                height: 310,
                closeOnEscape: false,
                modal: true
            });
        window.parent.$('#divPopup').parent().find('.dialog-titlebar-close').hide(); //hide title close button
        //window.showModalDialog('../../Presentation/Common/PaymentTransferPopup.aspx?UserAction=' + useraction + '&BranchCode=' + branchcode + '&Dbname=' + dbname + '&AccountCode=' + accountcode + '&Criteria1=' + criteria1 + '&Criteria2=' + criteria2 + '&SessionName=' + SessionName+'', "", "status:no;dialogLeft:400;dialogTop:200;resizable:no;dialogWidth:355px;dialogHeight:230px;center:no;edge:raised;location=no;");
        // document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = chkTransfer;
        // document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();

    }
    catch (err) {
    }

}

function ClosePaymentTransferWithSessionName(chkTransfer) {
    try {
        window.parent.$('#divPopup').dialog('close');
        window.parent.document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = chkTransfer;
        window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
    }
    catch (err) {
    }

}


function ShowGridView(TransMode, dbname, FK_Customer, Critrea1, Critrea2, Critrea3, message) 
{
        var $div = $('<div />').appendTo('body');
        $div.attr('id', 'divPopup');
        $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
    window.parent.$('#divPopup').html('<iframe id="searcIframe" frameborder="0" height="380" width="585" src="../../Presentation/Common/DetailView.aspx?TransMode=' + TransMode + '&dbname=' + dbname + '&FK_Customer=' + FK_Customer + '&Critrea1=' + Critrea1 + '&Critrea2=' + Critrea2 + '&Critrea3=' + Critrea3 + '&message=' + message + '"></iframe> ')
    .dialog({
                width: 596,
                height: 415,
                closeOnEscape: false,
                modal: true
            });
}

//-----------------------------Show Multiple Operators---------------------------------

function ShowMultiOperatorDetails(useraction, branchcode, dbname, accountcode, criteria) {

    try {
        var $div = $('<div />').appendTo('body');
        $div.attr('id', 'divPopup');
        $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
        window.parent.$('#divPopup').html('<iframe id="searcIframe" frameborder="0" height="100%" width="100%" src="../../Presentation/Common/OperatorsPopup.aspx?UserAction=' + useraction + '&BranchCode=' + branchcode + '&Dbname=' + dbname + '&AccountCode=' + accountcode + '&Criteria=' + criteria + '"></iframe> ')
            .dialog({
                title: 'Operators Details',
                width: 650,
                height: 450,
                closeOnEscape: false,
                modal: true
            });
        window.parent.$('#divPopup').parent().find('.dialog-titlebar-close').hide(); //hide title close button
    }
    catch (err) {
    }
}
function closeMultiOperatorDetails() {
    try {
        window.parent.$('#divPopup').dialog('close');
    }
    catch (err) {
    }
}
//------------------------------------------------------------------------------------------

//--------------------------------Multiple Lonee Details-----------------------------------------
function ShowMultiLoaneeDetails(useraction, branchcode, dbname, accountcode, criteria) {
    try {
        var $div = $('<div />').appendTo('body');
        $div.attr('id', 'divPopup');
        $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
        window.parent.$('#divPopup').html('<iframe id="searcIframe" frameborder="0" height="280" width="390" src="../../Presentation/TermLoan/TermLoanLoaneePopup.aspx?UserAction=' + useraction + '&BranchCode=' + branchcode + '&Dbname=' + dbname + '&AccountCode=' + accountcode + '&Criteria=' + criteria + '"></iframe> ')
            .dialog({
                title: 'Loanee Details',
                width: 415,
                height: 325,
                closeOnEscape: false,
                modal: true
            });
        window.parent.$('#divPopup').parent().find('.dialog-titlebar-close').hide(); //hide title close button     
    }
    catch (err) {
    }

}
function closeMultiLoaneeDetails() {
    try {
        window.parent.$('#divPopup').dialog('close');
    }
    catch (err) {
    }
}
function PositionLabelPopup() {
    window.parent.$('#divPositionLabel').css({ 'background-color': '#F8F8F8', 'display': 'none' })
                .dialog({
                    title: 'Position Label',
                    width: 250,
                    height: 110,
                    closeOnEscape: false,
                    modal: true,
                    close: function(event, ui) {
                        document.getElementById('ctl00_ContentPlaceHolder1_HdValue').value = 3;
                        document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
                    }

                });

    window.parent.$('#divPositionLabel').parent().appendTo($('form'));
}
//------------------------------------------------------------------------------------------
//-------------------Checking Session null or not(webService)-------------------------------
function CheckSessionNullOrNot() {
    var sessionNull;
    //Webservice
    $.ajax({
        type: "POST",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        url: "/SCORE/ScoreWebService.asmx/CheckSessionNullorNot",
        dataType: "json",
        async: false,
        success: function(data) {
            sessionNull = data.d;
        },
        error: function() {
            return
        }
    });
    return sessionNull;
}
//------------------------------------------------------------------------------------------
//----------------------show ErrorList center bottom with color Red,Green--------------------------------------------
function ShowErrorList() {
    $(document).ready(function() {
        var className = 'errorMsg';
        if ($('#ctl00_ContentPlaceHolder1_bltErrorList > li:contains("Success")').length != 0) {
            className = 'successMsg';
        }
        $('#ctl00_ContentPlaceHolder1_bltErrorList').css({ 'margin': '12px 40px 12px 40px' });
        $('#ctl00_ContentPlaceHolder1_bltErrorList').wrap('<div class="' + className + '" />');
        $('.' + className).position
                ({
                    my: "center bottom",
                    at: "center bottom",
                    of: window
                }).show();

        $('.' + className).click(function() {
            $('.' + className).hide({ effect: 'slide', direction: 'down' });
        });
    });
}
function SendOTP(stat, dtTime, validity) {
    // var UserCode = $('#lnkResend').attr('UserCode'); // $('#hidLoginArray').val().split("!").slice(7, 8); //getting userCode from HiddenArryString
    var UserCode = $('#txtUserName').val();
    $.ajax({
        type: "POST",
        data: "{'utc':'" + dtTime + "','usercode':'" + UserCode + "'}",
        contentType: "application/json; charset=utf-8",
        url: "/SCORE/Login.aspx/SendOTPCode",
        dataType: "json",
        success: function(mobile) {
            if (mobile.d == "0") {
                $('#bltErrorList').empty();
                $('#bltErrorList').append('<li>OTP Sending Failed! Please Update Your Mobile Number</li>');
                return;
            }
            else {
                showOTPPopup(stat, mobile.d, validity);
            }
        }
    });
}
function showOTPPopup(stat, mobile, validity) {

    if ($('#lblOTPError').text().length > 0) {
        $('#lblOTPError').delay(5000).fadeOut();
    }
    $("#txtOtpCode").removeAttr("disabled");
    $("#btnConfirmOtp").removeAttr("disabled");
    var seconds = validity;
    if (stat == 'Resend' || stat == 'Login') {
        clearInterval(interval);
        interval = null;
    }
    if (interval == null) {
        interval = setInterval(function() {
            $('#disableIn').text(seconds);
            if (seconds == 0) {
                clearInterval(interval);
                $("#txtOtpCode").val('');
                $("#txtOtpCode").attr("disabled", "disabled");
                $("#btnConfirmOtp").attr("disabled", "disabled");
                $('#lblOTPError').css({ 'color': 'Red' }).text('Time Out!').show().delay(10000).fadeOut();
                $('#disableIn').text('');
            }
            seconds = seconds - 1;
        }, 1000);
    }
    var lastNo = ' +91 *******' + mobile;
    $('#lblLastMobNo').text(lastNo);
    $('#hidLoginArray').val($('#hidLoginArray').val() + mobile);
    $('#txtOtpCode').val('');
    $('#divOTP').css({
        left: $('#tdLogin').position().left - 80,
        top: $('#tdLogin').position().top - 35,
        position: 'absolute'
    });
    $('.OTPoverlay').fadeOut();
    $('<div class="OTPoverlay" id="overlay" style="display: none;">').appendTo('body').show();
    $('#divOTP').show('slide', { direction: 'down' }, 500);

    $('#btnScoreLogin').attr('disabled', 'disabled'); //Disable  form submit on modal otp popup
    $('body').bind('keydown', function(event) {
        if (event.keyCode == $.ui.keyCode.TAB) {
            if (event.srcElement.id == 'txtOtpCode') {
                $('#btnConfirmOtp').focus;
                return;
            }
            else {
                return false;
            }
        }
        else if (event.keyCode == $.ui.keyCode.ENTER) {

            if (event.srcElement.id == 'txtOtpCode' || event.srcElement.id == 'btnConfirmOtp') {
                $('#btnConfirmOtp').click();
                return;
            }
            else {
                return false;
            }
        }
    });
    $('#divOTP .closeOTPdiv').click(function() {
        $('#divOTP').hide('slide', { direction: 'down' }, 500);
        $('.OTPoverlay').fadeOut();
        $('body').unbind('keydown');
        $('#btnScoreLogin').removeAttr('disabled');
    });
    $('#txtOtpCode').focus();
}
function ValidateOTP() {

    $otpcode = $('#txtOtpCode').val();
    if ($otpcode == '') {
        $('#lblOTPError').css({ 'color': 'Red' });
        $('#lblOTPError').text('Enter Verification Code!').show().delay(10000).fadeOut();
        return false;
    }
    //    else if ($otpcode.length < 4) {
    //        $('#txtOtpCode').val('');
    //        $('#lblOTPError').css({ 'color': 'Red' });
    //        $('#lblOTPError').text('Invalid Verification Code!').show().delay(10000).fadeOut();
    //        return false;
    //    }
    else {
        return true;
    }
}
//-----------------------------------------------------------
function RegisterFinger(branchcodeuser, usercode, machinecode, menucode, dbname) {
  var $div = window.parent.$('<div />').appendTo('body');
    $div.attr('id', 'divPopupRegisterFinger');
    $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });
    window.parent.$('#divPopupRegisterFinger').html('<iframe id="searcIframe" frameborder="0" height="260" width="300" src="../../Presentation/Security/FingerRegistration.aspx?BRANCHCODEUSER=' + branchcodeuser + '&USERCODE=' + usercode + '&MACHINECODE=' + machinecode + '&MENUCODE=' + menucode + '&DBNAME=' + dbname + '"></iframe>')
            .dialog({
                title: 'FingerPrint Registartion',
                width: 330,
                height: 300,
                modal: true
            });
    window.parent.$('#divPopupRegisterFinger').parent().find('.dialog-titlebar-close').hide(); //hide title close button
}


function closeRegisterFinger(f1,f2,close) {
    try {
        if (close != 'close') {
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_HdFinger1').value = f1;
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_HdFinger2').value = f2;
            window.parent.document.getElementById('ctl00_ContentPlaceHolder1_btnScriptFill').click();
            }
        window.parent.$('#divPopupRegisterFinger').dialog('close');
    }
    catch (er) {
    }
}

//-----------------------------Show Locker Visual Display---------------------------------

function ShowLockerVisualDisplay(sectionid, dbname) {

    try {    
        
//        var $div = $('<div />').appendTo('body');
//        $div.attr('id', 'divPopup');
//        $div.css({ 'background-color': '#F8F8F8', 'display': 'none', 'position': 'absolute' });
//        
//        $('.divPopup').css('height', $(window).height()+'px');
//        window.parent.$('#divPopup').html('<iframe id="searcIframe" frameborder="0" src="../../Presentation/SafeDepositLocker/SafeDepositLockerVisualPopup.aspx?section=' + sectionid + '&Dbname=' + dbname + '"> </iframe> ')
//            .dialog({
//                title: 'Locker Display',
//                width: 1000,
//                height: 0,
//                closeOnEscape: false,
//                modal: true
//            });

//window.showModalDialog('../../Presentation/SafeDepositLocker/SafeDepositLockerVisualPopup.aspx?section=' + sectionid + '&Dbname=' + dbname + '', "", "status:no;resizable:yes;type=fullscreen;center:yes;edge:raised;scroll:yes;");
window.open('../../Presentation/SafeDepositLocker/SafeDepositLockerVisualPopup.aspx?section=' + sectionid + '&Dbname=' + dbname + '','Locker' , ',type=fullWindow,channelmode,scrollbars=yes');
//window.showModalDialog('../../Presentation/SafeDepositLocker/SafeDepositLockerVisualPopup.aspx?section=' + sectionid + '&Dbname=' + dbname + '','Locker' , ',type=fullWindow,fullscreen,scrollbars=yes');

//        //window.parent.$('#divPopup').parent().find('.dialog-titlebar-close').hide(); //hide title close button
    }
    catch (err) {
    }
}
function closeLockerVisualDisplay() {
    try {
        window.parent.$('#divPopup').dialog('close');
    }
    catch (err) {
    }
}
    function barcodefill(txt,nextfield)
    {
        var val = txt.value ;    
        var textLength = val.length;
    
        if (textLength>=12)
        {
        //alert('Hai');            
        document.getElementById('ctl00_ContentPlaceHolder1_txtBranchCode').value = val.substring(0, 3); 
        document.getElementById('ctl00_ContentPlaceHolder1_txtAccountType').value = val.substring(3, 6); 
        document.getElementById('ctl00_ContentPlaceHolder1_txtAccountNumber').value = val.substring(6, 12);        
        document.getElementById('ctl00_ContentPlaceHolder1_btnSearch').click();
        
        }
        else {
            document.getElementById('ctl00_ContentPlaceHolder1_txtBranchCode').value = zeroPad(0,3);
            if (nextfield != '' || nextfield != null) {
                document.getElementById(nextfield).focus();
            }
            return false;
        }
    }
    function amtInWords(amount) {

        var decAmount = amount;
        var sUnits = new Array(20);
        var sTens = new Array(8);
        var sHundreds = new Array(6);
        var sAmount;
        var i, iLenAmount, iDecPart, iIntegerPart;

        sUnits[1] = '';
        sUnits[2] = 'One';
        sUnits[3] = 'Two';
        sUnits[4] = 'Three';
        sUnits[5] = 'Four';
        sUnits[6] = 'Five';
        sUnits[7] = 'Six';
        sUnits[8] = 'Seven';
        sUnits[9] = 'Eight';
        sUnits[10] = 'Nine';
        sUnits[11] = 'Ten';
        sUnits[12] = 'Eleven';
        sUnits[13] = 'Twelve';
        sUnits[14] = 'Thirteen';
        sUnits[15] = 'Fourteen';
        sUnits[16] = 'Fifteen';
        sUnits[17] = 'Sixteen';
        sUnits[18] = 'Seventeen';
        sUnits[19] = 'Eighteen';
        sUnits[20] = 'Ninteen';
        sTens[1] = 'Twenty';
        sTens[2] = 'Thirty';
        sTens[3] = 'Forty';
        sTens[4] = 'Fifty';
        sTens[5] = 'Sixty';
        sTens[6] = 'Seventy';
        sTens[7] = 'Eighty';
        sTens[8] = 'Ninety';
        sHundreds[1] = 'Hundred';
        sHundreds[2] = 'Thousand';
        sHundreds[3] = 'Lakh';
        sHundreds[4] = 'Crore';
        if (decAmount == 10000000000000) {
            decAmount = 9999999999999.99;
        }
        if (decAmount == 0) {
            return "";
        }

        iDecPart = (decAmount - Math.round(decAmount)) * 100;
        iDecPart = Math.round(iDecPart);

        //Because Math.round results .50,.52,.53.......98,.99 in negative values

        if (iDecPart < 0) {
            iDecPart = 100 + iDecPart;
        }

        if (iDecPart == 0) {
            decAmount = decAmount;
        }
        else {
            decAmount = Math.round(decAmount - (iDecPart / 100));
        }

        iLenAmount = ((String)(decAmount)).length;

        if (iLenAmount == 1) {
            var index = parseInt(decAmount) + 1;
            sAmount = sUnits[index];
        }
        else {
            for (i = iLenAmount; i > 0; i--) {
                if (i == 13 || i == 12) {
                    iIntegerPart = parseInt(decAmount / 100000000000);
                    decAmount = parseInt(decAmount % 100000000000);
                    if (iIntegerPart == 0) {
                        sAmount = sAmount;
                    }
                    else {
                        if (iIntegerPart < 20) {
                            sAmount = sUnits[iIntegerPart + 1] + " " + sHundreds[6] + " ";
                        }
                        else {
                            sAmount = sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[6] + " "
                        }
                    }
                }
                else if (i == 11 || i == 10) {
                    iIntegerPart = parseInt(decAmount / 1000000000);
                    decAmount = parseInt(decAmount % 1000000000);
                    if (iIntegerPart == 0) {
                        sAmount = sAmount;
                    }
                    else {
                        if (iIntegerPart < 20) {
                            if (sAmount == null) {
                                sAmount = sUnits[iIntegerPart + 1] + " " + sHundreds[5] + " ";
                            }
                            else {
                                sAmount = sAmount + " " + sUnits[iIntegerPart + 1] + " " + sHundreds[5] + " ";
                            }
                        }
                        else {
                            if (sAmount == null) {
                                sAmount = sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[5] + " ";
                            }
                            else {
                                sAmount = sAmount + " " + sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[5] + " ";
                            }
                        }
                    }
                }
                else if (i == 9 || i == 8) {
                    iIntegerPart = parseInt(decAmount / 10000000);
                    decAmount = parseInt(decAmount % 10000000);
                    if (iIntegerPart == 0) {
                        sAmount = sAmount;
                    }
                    else {
                        if (iIntegerPart < 20) {
                            if (sAmount == null) {
                                sAmount = sUnits[iIntegerPart + 1] + " " + sHundreds[4] + " ";
                            }
                            else {
                                sAmount = sAmount + " " + sUnits[iIntegerPart + 1] + " " + sHundreds[4] + " ";
                            }
                        }
                        else {
                            if (sAmount == null) {
                                sAmount = sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[4] + " ";
                            }
                            else {
                                sAmount = sAmount + " " + sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[4] + " ";
                            }
                        }
                    }
                }
                else if (i == 7 || i == 6) {
                    iIntegerPart = parseInt(decAmount / 100000);
                    decAmount = (decAmount % 100000);
                    if (iIntegerPart == 0) {
                        sAmount = sAmount;
                    }
                    else {
                        if (iIntegerPart < 20) {
                            if (sAmount == null) {
                                sAmount = sUnits[iIntegerPart + 1] + " " + sHundreds[3] + " ";
                            }
                            else {
                                sAmount = sAmount + " " + sUnits[iIntegerPart + 1] + " " + sHundreds[3] + " ";
                            }
                        }
                        else {
                            if (sAmount == null) {
                                sAmount = sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[3] + " ";
                            }
                            else {
                                sAmount = sAmount + " " + sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[3] + " ";
                            }
                        }
                    }
                }
                else if (i == 5 || i == 4) {
                    iIntegerPart = parseInt(decAmount / 1000);
                    decAmount = (decAmount % 1000);
                    if (iIntegerPart == 0) {
                        sAmount = sAmount;
                    }
                    else {
                        if (iIntegerPart < 20) {
                            if (sAmount == null) {
                                sAmount = sUnits[iIntegerPart + 1] + " " + sHundreds[2] + " ";
                            }
                            else {
                                sAmount = sAmount + " " + sUnits[iIntegerPart + 1] + " " + sHundreds[2] + " ";
                            }
                        }
                        else {
                            if (sAmount == null) {
                                sAmount = sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[2] + " ";
                            }
                            else {
                                sAmount = sAmount + " " + sTens[parseInt(iIntegerPart / 10) - 1] + " " + sUnits[(iIntegerPart - parseInt(iIntegerPart / 10) * 10) + 1] + " " + sHundreds[2] + " ";
                            }
                        }
                    }
                }
                else if (i == 3) {
                    iIntegerPart = parseInt(decAmount / 100);
                    decAmount = (decAmount % 100);
                    if (iIntegerPart == 0) {
                        sAmount = sAmount;
                    }
                    else {
                        var index;
                        index = parseInt(iIntegerPart) + 1;
                        if (sAmount == null) {
                            sAmount = sUnits[index] + " " + sHundreds[1] + " ";
                        }
                        else {
                            sAmount = sAmount + " " + sUnits[index] + " " + sHundreds[1] + " ";
                        }
                    }
                }
                else if (i == 2) {
                    decAmount = parseInt(eval(decAmount));
                    if (decAmount < 20) {
                        var index = parseInt(decAmount) + 1;
                        if (sAmount == null) {
                            sAmount = sUnits[index];
                        }
                        else {
                            sAmount = sAmount + " " + sUnits[index];
                        }
                    }
                    else {
                        var a = parseInt(((decAmount / 10) - 1));
                        var b = (decAmount % 10) + 1;
                        if (sAmount == null) {
                            sAmount = sTens[a] + " " + sUnits[b];
                        }
                        else {
                            sAmount = sAmount + " " + sTens[a] + " " + sUnits[b];
                        }
                    }
                }
            }
        }
        if (iDecPart == 0) {
            sAmount = "Rs. " + sAmount;
        }
        else if (sAmount == "") {
            sAmount = "Paise ";
        }
        else {
            sAmount = "Rs. " + sAmount + " And Paise";
        }

        if (iDecPart < 20) {
            sAmount = sAmount + " " + sUnits[iDecPart + 1] + " ";
        }
        else {
            var fi = parseInt(((iDecPart / 10) - 1));
            var fii = parseInt((iDecPart % 10)) + 1;
            sAmount = sAmount + " " + sTens[fi] + " " + sUnits[fii] + " ";
        }

        sAmount = sAmount + "Only";

        return sAmount;

    }

    function showPrintButtonPopup(TransMode, ID, FK_Machine, TransType, Paths, BranchCodeUser, Duplicate) {
        try {

            window.showModalDialog('../../Presentation/Common/VoucherPrintPreview.aspx?TransMode=' + TransMode + '&ID=' + ID + '&FK_Machine=' + FK_Machine + '&TransType=' + TransType + '&Paths=' + Paths + '&BranchCodeUser=' + BranchCodeUser + '&Duplicate=' + Duplicate + '', "", "status:no;dialogLeft:400;dialogTop:200;resizable:yes;dialogWidth:850px;dialogHeight:500px;center:no;edge:raised;location=no;");
        }

        catch (err) {

        }
    }



    //-------------------------------Transfer To Account For WebPharma  transaction Popup --------------------------------------------

    function ShowTransferToAccountWithSessionName(fkclient, user) {
        try {
          
            var $div = $('<div />').appendTo('body');
            $div.attr('id', 'divPopup');
           
            $div.css({ 'background-color': '#F8F8F8', 'display': 'none' });                                     
            alert('hi');
            window.parent.$('#divPopup').html('<iframe id="searcIframe" frameborder="0" height="250" width="520" src="../../TransferToAccountPopUp.aspx?fkclient=' + fkclient + '&user=' + user + '"></iframe> ')
    
                .dialog({
                   
                    title: 'Trnsfer to Account',
                 
                    width: 535,
                    height: 310,
                    closeOnEscape: false,
                    modal: true
                });
     
            window.parent.$('#divPopup').parent().find('.dialog-titlebar-close').hide(); //hide title close button  
  
            window.parent.$('#divPopup').parent().find('.btnClose').hide();

        }
        catch (err) {
        }

    }
