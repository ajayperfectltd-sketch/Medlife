<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InterStatePurchase.aspx.cs" Inherits="InterStatePurchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopUpGridSelection.ascx" TagName="CommonPopupGrid" TagPrefix="uc3" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="Popup/PopupPurchaseExpense.ascx" TagName="CommonPopupExpense" TagPrefix="uc4" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/alertify.min.js"></script>

    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />

    <script src="js/toastr.js"></script>
    <script src="js/Navigation.js"></script>
    <script src="AssetLogin/js/jquery.min.js"></script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
         .Plabel
        {
            margin-top: 6px !important;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            padding-top: 180px;
            min-height: 100%;
            width: 100%;
            display: none;
            position: fixed;
            opacity: 0.5;
            filter: alpha(opacity=50);
            -moz-opacity: 0.5;
            background-color: White;
            z-index: 999;
        }
    </style>
    <script type="text/javascript">

        function pageLoad() {
            $("#<%#ChkIncStax.ClientID%>").change(function () {
                TaxAmount();
            });
        }
        function EnterTab(e, Id1) {

            var intKey = window.Event ? e.which : e.KeyCode;


            if (intKey == 13) {

                $("#" + Id1).focus();

            }
            return false;
        }

        function focuschange(id) {

            $("#" + id).focus();
        }


        var submit = 0;
        function disablebutn() {
            try {

                window.setTimeout('delaysubmit()', 50);
            }
            catch (e) {
                alert(e);
            }
        }

        function releasebtn() {


            $("#btnSubmit").prop('disabled', false);

        }
        function ShowDivOnButtonClick() {
            var x = document.getElementById("divEcelFormat");
            x.style.display = 'block';
        }
        function HideDivOnButtonClick() {
            var x = document.getElementById("divEcelFormat");
            x.style.display = 'none';
        }
        function PopupOnReason() {
            try {


                $("#divPopupReason").css('display', 'block');
            }
            catch (ex) {

            }
        }
        function delaysubmit() {
            try {
                if (submit == 1) {
                    return false;

                }

                $("#btnSubmit").prop('disabled', true);

                window.setTimeout('releasebtn()', 3000);
                submit = 1;
            }
            catch (e) {
                alert(e);
            }
        }
        function uploadStartedabdc(sender, args) {
            // alert('dd');
            var filename = args.get_fileName();
            var filext = filename.substring(filename.lastIndexOf(".") + 1).toLowerCase();

            if (filext == "xlsx" || filext == "xls") {
                Hideinsert();
                return true;

            }
            else {
                alert('Please upload only Excel files ')
                $('#FileUploadExcel').val('');
            }

        }

        function Hideinsert() {

            //var x = document.getElementById("btnAdd");
            //x.style.display = 'none';
        }

        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);
            Check();

        }

        function ClearAdd() {
            try {
                var dt = new Date();
                $("#hfDate").val(dt.format("dd/MM/yyyy"));
                $("#txtInvoiceDate").val(dt.format("dd/MM/yyyy"));
                $("#txtpopupBillDate").val(dt.format("dd/MM/yyyy"));
                $("#txtpopupChequeDate").val(dt.format("dd/MM/yyyy"));
                $('#<%#chkDiscounttax.ClientID%>').prop('checked', false);
                $("#txtInvoiceNo").val("");
                $("#ddlPurchaseOrder").val("0");
                $("#ddlSupplier").val("0");
                $("#txtSupplierName").val("");
                $("#txtPurchaseOrder").val("");
                $("#txtPurchaseOrderNo").val("0");
                $("#txtTotVat").val("0");
                $("#txtTotCess").val("0");
                $("#txtTotNetPur").val("0");
                $("#lblReferenceNo").text("0");
                $("#txtTotalAmount").val("0");
                $("#txtRoundOff").val("0");
                $("#txtFright").val("0");
                $("#txtBillNetAmount").val("0");
                $("#hfID_Purchase").val("");
                $("#txtpopupTax").val("0");
                $("#txtpopupDiscountPer").val("0");
                $("#txtpopupNetPayAmount").val("0");
                $("#txtpopupDiscountAmount").val("0");
                $("#txtpopupBalanceAmount").val("0");
                $("#hfApproveOk").val("0");

                $("#txtpopupLoading").val("0");
                $("#txtpopupUnLoading").val("0");
                $("#txtpopupDisplay").val("0");
                $("#txtpopupOthers").val("0");
                $("#chkCredit").prop("checked", false);
                $("#ChkIncStax").prop("checked", true);
                // $("#chkInterState").prop("checked", false);
                $("#txtTotNetPur").val("0");
                $("#txtBillNetAmount").val("0");


                // DiscountOnTax();

                ItemClear();
            }
            catch (e) {
                alert(e);
            }
        }
        //function getDiscountPerAndAmount() {
        //    var salesPrice = parseFloat($("#txtSalesPrice").val().trim() == "" ? "0" : $("#txtSalesPrice").val());

        //    var Mrp = parseFloat($("#txtMRP").val().trim() == "" ? "0" : $("#txtMRP").val());
        //    if (salesPrice > Mrp) {
        //        $("#txtSalesPrice").val(Mrp);
        //        salesPrice = Mrp;
        //    }
        //    var discAmnt = (Mrp - salesPrice).toFixed(4);

        //    $("#txtDiscountAmt").val(discAmnt);
        //    var discPer = (discAmnt * 100) / Mrp;
        //    $("#txtDiscountPer").val(discPer.toFixed(2));

        //    getSalesPrice(2);


        //}
        function checkNumAndDecimal(evnt, txtbox) {
            //var input =[0];
            //alert(window.getSelection().toString().trim()
            //if (window.getSelection().toString().trim() != "") {

            //if (evnt.target.selectionEnd > 1) {
            //    evnt.target.selectionEnd = evnt.target.value.length + 1
            //CheckNumWithDecimalwithReplaceReplace(evnt);
            //}
            //else {
            //    CheckNumWithDecimalwithReplaceReplace(evnt, false);
            //}
            //CheckNumWithDecimal(evnt);
            var selectAll = false;
            if (window.getSelection) {

                var ta = $(txtbox).get(0);
                if (ta.value.substring(ta.selectionStart, ta.selectionEnd) != "") {
                    selectAll = true;
                }

            }
            // For IE
            if (document.selection && document.selection.type != "Control") {
                if (document.selection.createRange().text != "") {
                    selectAll = true;
                }
            }
            CheckNumWithDecimalwithReplace(evnt, selectAll);




        }
        function DiscountOnTax() {

            if ($('#chkDiscounttax').is(":checked")) {
                var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();

                var TaxAmount = $("#hfTaxValue").val() == "" ? "0" : $("#hfTaxValue").val();
                var qty = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
                var Total = (PurchaseValue - TaxAmount) * (qty)
                $("#txtPurDiscAmnt").val(Total.toFixed(4));

            }
            else {
                var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
                var qty = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
                var Q = (PurchaseValue) * (qty)
                $("#txtPurDiscAmnt").val(Q.toFixed(4));

            }

        }

        function ItemClear() {

            $("#hfItemId").val("");
            $("#hfSlNo ").val("");
            //$("#DivPreviousPurchase").hide();
            $("#ddlItem").val("0");
            $("#ddlCategory").val("0");
            $("#txtItemShortName").val("");
            $("#ddlUnit").val("0");
            $("#ddlSubunit").val("0");
            $("#txtBatch").val("");
            var toDate = new Date();
            $("#txtExpiryDate").val(toDate.format("dd/MM/yyyy"));
            $("#txtHSNSAC").val("");
            $("#txtQuantity").val("0");
            $("#txtFree").val("0");
            $("#txtActqty").val("0");
            $("#txtPurchaseRate").val("0");
            $("#chkInctax").prop("checked", false);
            //  $("#chkInterState").prop("checked", false);
            $("#txttaxAmount").val("0");
            $("#chkDiscounttax").prop("checked", false);
            $("#txtPurDiscPer").val("0");
            $("#txtPurDiscAmnt").val("0");
            $("#txtNetPur").val("0");
            $("#txtMRP").val("0");
            $("#txtProfitPer").val("0");
            $("#txtProfitAmount").val("0");
            $("#txtDiscountPer").val("0");
            $("#txtDiscountAmt").val("0");
            $("#ChkIncStax").prop("checked", true);
            $("#txtSTaxAmount").val("0");
            $("#txtSalesPrice").val("0");
            $("#txtAmount").val("0");
            $("#hfUnitQuantity").val("0");
            $("#hfTotTaxPercentage").val("0");
            $("#hfPurchaseValue").val("0");
            $("#hfTotTaxsalesPercentage").val("0");
            $('#txtKey').val("0");

        }

        function popUpStockSelectClick() {

            try {
                var ItemName = $("#txtItemShortName").val();
                var hfUser = $("#hfUser").val();
                var hfClient = $("#hfClient").val();
                var ddlCategory = $("#ddlCategory").val();
                var ddlSupplier = $("#ddlSupplier").val();
                var txtInvoiceDate = $("#txtInvoiceDate").val();
                var hfItemvalue = $("#hfItemvalue").val();
                var ItemType = "2";



                $.ajax({
                    type: "POST",
                    url: 'Textile.asmx/GetUrlEncrypt',
                    data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','ddlSupplier':'" + ddlSupplier + "','txtInvoiceDate':'" + txtInvoiceDate + "','hfItemvalue':'" + hfItemvalue + "','hfClient':'" + hfClient + "','ItemType':'" + ItemType + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (MenuHtml) {
                        url = "ItemSearch.aspx?" + MenuHtml.d;
                        popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                        ",statusbar=no,menubar=no,resizable=0,width=1015," +
                        "height=650,left = 200,top = 100");
                        popUpObj.focus();

                    },
                    error: function (MenuHtml) {

                    }
                });


                $("#ddlUnit").focus();
            }
            catch (e) {
                alert(e);
            }

        }

        function popUpPreviousPurchaseSelect() {

            try {
                var ItemName = $("#txtItemShortName").val();
                var hfUser = $("#hfUser").val();
                var hfClient = $("#hfClient").val();
                var ddlCategory = $("#ddlCategory").val();
                var ddlSupplier = $("#ddlSupplier").val();
                var txtInvoiceDate = $("#txtInvoiceDate").val();
                var hfItemvalue = $("#hfItemvalue").val();
                var ItemType = "2";
                $.ajax({
                    type: "POST",
                    url: 'Textile.asmx/GetUrlEncrypt',
                    data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','ddlSupplier':'" + ddlSupplier + "','txtInvoiceDate':'" + txtInvoiceDate + "','hfItemvalue':'" + hfItemvalue + "','hfClient':'" + hfClient + "','ItemType':'" + ItemType + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (MenuHtml) {


                        // url = "PreviousPurchase.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val() + '&Supplier=' + $("#ddlSupplier").val() + '&Date=' + $("#txtInvoiceDate").val() + '&ID=' + $("#hfItemvalue").val()
                        url = "PreviousPurchase.aspx?" + MenuHtml.d;

                        popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                        ",statusbar=no,menubar=no,resizable=0,width=1015," +
                        "height=650,left = 200,top = 100");
                        popUpObj.focus();
                    },
                    error: function (MenuHtml) {
                        // alert('GetMinItemStockName');
                        //alert('webservice_error');
                        // $("#hfEncryptvalue").val("");
                    }
                });


            }
            catch (e) {
                alert(e);
            }

        }
        function popUpStockSelect() {

            try {
                var Cat = $("#ddlCategory").val();

                var ItemName = $("#txtItemShortName").val();
                //  alert(ItemName.toString());
                if (ItemName.length > 0) {

                    var hfUser = $("#hfUser").val();
                    var hfClient = $("#hfClient").val();
                    var ddlCategory = $("#ddlCategory").val();
                    var ddlSupplier = $("#ddlSupplier").val();
                    var txtInvoiceDate = $("#txtInvoiceDate").val();
                    var hfItemvalue = $("#hfItemvalue").val();
                    var ItemType = "2";


                    $.ajax({
                        type: "POST",
                        url: 'Textile.asmx/GetUrlEncrypt',
                        data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','ddlSupplier':'" + ddlSupplier + "','txtInvoiceDate':'" + txtInvoiceDate + "','hfItemvalue':'" + hfItemvalue + "','hfClient':'" + hfClient + "','ItemType':'" + ItemType + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (MenuHtml) {

                            //url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val() + '&Supplier=' + $("#ddlSupplier").val() + '&Date=' + $("#txtInvoiceDate").val()

                            url = "ItemSearch.aspx?" + MenuHtml.d;

                            popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                            ",statusbar=no,menubar=no,resizable=0,width=1015," +
                            "height=650,left = 200,top = 100");
                            popUpObj.focus();

                        },
                        error: function (MenuHtml) {

                        }
                    });

                }
                else {



                }

            }
            catch (e) {
                alert(e);
            }

        }


        function popUpSupplierNameSelectSelect() {

            try {


                var ItemName = $("#txtSupplierName").val();

                if (ItemName.length > 0) {
                    //$("#ImageButton3").click();
                }
                else {



                }

            }
            catch (e) {
                alert(e);
            }

            $("#txtPurchaseOrderNo").focus();
        }

        function popUpSupplierSearchNameSelectSelect() {

            try {


                var ItemName = $("#txtSupplierName").val();

                if (ItemName.length > 0) {

                }
                else {



                }

            }
            catch (e) {
                alert(e);
            }

        }


        function calculateActualQuantity() {
            var qty = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
            var free = $("#txtFree").val() == "" ? "0" : $("#txtFree").val();

            var nounit = $("#hfUnitQuantity").val() == "" ? "0" : $("#hfUnitQuantity").val();
            var ActQty = parseFloat(nounit) * parseFloat(qty);
            var SubunitNo = $("#hfSubUnitQuantity").val() == "" ? "0" : $("#hfSubUnitQuantity").val();
            $("#txtCheckActQuantity").val(ActQty.toFixed(4));

            if (SubunitNo != 0)
                var Actquantity = (parseFloat(qty) + parseFloat(free)) * (parseFloat(nounit)) * (parseFloat(SubunitNo));
            else
                var Actquantity = (parseFloat(qty) + parseFloat(free)) * (parseFloat(nounit));


            $("#txtActqty").val(Actquantity.toFixed(4));



        }

        function GetValues() {

            var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
        }

        //function getpurchaseamount() {

        //    var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
        //    var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();

        //    if ($('#chkInctax').is(":checked")) {

        //        var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();

        //        var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

        //        $("#txttaxAmount").val(parseFloat(taxAmount).toFixed(4));
        //        var PurchaseValue = (parseFloat(PurchaseRate) * parseFloat(Quantity)) - (parseFloat(taxAmount));
        //        $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));
        //    }
        //    else {

        //        var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
        //        var taxAmount = ((parseFloat(PurchaseRate) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
        //        $("#txttaxAmount").val(taxAmount.toFixed(4));
        //        var PurchaseValue = parseFloat(PurchaseRate) * parseFloat(Quantity);
        //        $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));


        //    }
        //    if ($('#chkDiscounttax').is(":checked")) {

        //        var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
        //        var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
        //        var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
        //        var DiscountAmount = isNaN(((parseFloat(PurchaseValue) + parseFloat(taxAmount)) * (DiscPer)) / parseFloat(100)) ? 0 : ((parseFloat(PurchaseValue) + parseFloat(taxAmount)) * (DiscPer)) / parseFloat(100);
        //        $("#txtPurDiscAmnt").val(parseFloat(DiscountAmount).toFixed(4));

        //    }

        //    else {


        //        var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
        //        var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
        //        var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
        //        var DiscountAmount = isNaN((parseFloat(PurchaseValue) * parseFloat(DiscPer)) / parseFloat(100)) ? 0 : (parseFloat(PurchaseValue) * parseFloat(DiscPer)) / parseFloat(100);

        //        $("#txtPurDiscAmnt").val(DiscountAmount.toFixed(4));

        //    }
        //    var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
        //    var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
        //    var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
        //    var NetPurchase = (parseFloat(PurchaseValue) + parseFloat(taxAmount)) - parseFloat(DiscountAmount);

        //    $("#txtNetPur").val(NetPurchase.toFixed(4));

        //}


        function getpurchaseAmountOrPer(CalcField) {


            var PurchaseRate = parseFloat($("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val());
            var Quantity = parseFloat($("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val());
            var DiscPer = 0;
            var TotalTaxPercetage = parseFloat($("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val());
            var ActPur = 0, TaxAmount = 0, DiscAmnt = 0;
            var NetPurchase = 0;

            if (CalcField == "Amount") {
                DiscPer = parseFloat($("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val()).toFixed(4);
                if ($('#chkInctax').is(":checked")) {
                    ActPur = (PurchaseRate * 100) / (100 + TotalTaxPercetage);
                    TaxAmount = ((ActPur) * (TotalTaxPercetage / 100) * Quantity).toFixed(4);
                    if ($('#chkDiscounttax').is(":checked")) {
                        TaxAmount = TaxAmount - (TaxAmount * DiscPer / 100);
                    }
                    DiscAmnt = (ActPur * Quantity) * (DiscPer / 100);

                    
                } else {
                    TaxAmount = ((PurchaseRate) * (TotalTaxPercetage / 100) * Quantity).toFixed(4);
                    if ($('#chkDiscounttax').is(":checked")) {
                        TaxAmount = TaxAmount - (TaxAmount * DiscPer / 100);
                    }
                    DiscAmnt = (PurchaseRate * Quantity) * (DiscPer / 100);
                }
                if (isNaN(TaxAmount))
                    TaxAmount = 0;
                if (isNaN(DiscAmnt))
                    DiscAmnt = 0;
                $("#txttaxAmount").val(parseFloat(TaxAmount).toFixed(4));
                $("#txtPurDiscAmnt").val(DiscAmnt.toFixed(4));
            }
            else if (CalcField == "Perc") {
                DiscAmnt = parseFloat($("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val()).toFixed(4);
               
                if ($('#chkInctax').is(":checked")) {
                    ActPur = (PurchaseRate * 100) / (100 + TotalTaxPercetage);
                    TaxAmount = ((ActPur) * (TotalTaxPercetage / 100) * Quantity).toFixed(4);
                    DiscPer = ((DiscAmnt / (ActPur * Quantity)) * 100).toFixed(4);
                    if ($('#chkDiscounttax').is(":checked")) {
                        TaxAmount = TaxAmount - (TaxAmount * DiscPer / 100);
                    }
                    
                } else {
                    TaxAmount = ((PurchaseRate) * (TotalTaxPercetage / 100) * Quantity).toFixed(4);
                    DiscPer = ((DiscAmnt / (PurchaseRate * Quantity)) * 100).toFixed(4);
                    if ($('#chkDiscounttax').is(":checked")) {
                        TaxAmount = TaxAmount - (TaxAmount * DiscPer / 100);
                    }

                }
                if (isNaN(TaxAmount))
                    TaxAmount = 0;
                if (isNaN(DiscPer))
                    DiscPer = 0;
                $("#txttaxAmount").val(parseFloat(TaxAmount).toFixed(4));
                $("#txtPurDiscPer").val(parseFloat(DiscPer).toFixed(4));
            }
            if ($('#chkInctax').is(":checked")) {
                NetPurchase = parseFloat(PurchaseRate * Quantity) - parseFloat(DiscAmnt);
            }
            else {
                NetPurchase = (parseFloat(PurchaseRate * Quantity) + parseFloat(TaxAmount)) - parseFloat(DiscAmnt);
            }
            if (isNaN(NetPurchase))
                NetPurchase = 0;
            $("#txtNetPur").val(NetPurchase.toFixed(4));
        }


        //function getpurchaseamount2() {

        //    var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
        //    var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
        //    var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
        //    if ($('#chkInctax').is(":checked")) {

        //        var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
        //        var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));
        //        var PurchaseValue = (parseFloat(PurchaseRate) * parseFloat(Quantity)) - (parseFloat(taxAmount));
        //        $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));
        //        if ($('#chkDiscounttax').is(":checked")) {

        //            var ExcludeDis = parseFloat(PurchaseRate) * parseFloat(DiscPer) / parseFloat(100);
        //            var ActPur = parseFloat(PurchaseRate) - parseFloat(ExcludeDis);
        //            var taxAmount = ((parseFloat(ActPur)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

        //        }
        //        //else {
        //        //    var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));
        //        //}

        //        var singleAmnt = ((parseFloat(PurchaseRate)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

        //        $("#hfSingleQty").val(parseFloat(singleAmnt).toFixed(4));
        //        $("#txttaxAmount").val(parseFloat(taxAmount).toFixed(4));

        //    }
        //    else {

        //        var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
        //        if ($('#chkDiscounttax').is(":checked")) {

        //            var ExcludeDis = parseFloat(PurchaseRate) * parseFloat(DiscPer) / parseFloat(100);
        //            var ActPur = parseFloat(PurchaseRate) - parseFloat(ExcludeDis);
        //            var taxAmount = ((parseFloat(ActPur) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
        //            var singleAmnt = ((parseFloat(ActPur) * parseFloat(TotalTaxPercetage)) / parseFloat(100));
        //        }
        //        else {
        //            var taxAmount = ((parseFloat(PurchaseRate) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
        //            var singleAmnt = ((parseFloat(PurchaseRate) * parseFloat(TotalTaxPercetage)) / parseFloat(100));
        //        }


        //        $("#hfSingleQty").val(parseFloat(singleAmnt).toFixed(4));
        //        $("#txttaxAmount").val(taxAmount.toFixed(4));
        //        var PurchaseValue = parseFloat(PurchaseRate) * parseFloat(Quantity);
        //        $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));

        //    }
        //    if ($('#chkDiscounttax').is(":checked")) {

        //        var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
        //        var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
        //        var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
        //        //  var DiscountAmount = isNaN(((parseFloat(PurchaseValue) + parseFloat(taxAmount)) * (DiscPer)) / parseFloat(100)) ? 0 : ((parseFloat(PurchaseValue) + parseFloat(taxAmount)) * (DiscPer)) / parseFloat(100);
        //        var DiscountAmount = isNaN(((parseFloat(PurchaseValue)) * (DiscPer)) / parseFloat(100)) ? 0 : ((parseFloat(PurchaseValue)) * (DiscPer)) / parseFloat(100);
        //        $("#txtPurDiscAmnt").val(parseFloat(DiscountAmount).toFixed(4));


        //    }

        //    else {


        //        var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
        //        var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
        //        var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
        //        var DiscountAmount = isNaN((parseFloat(PurchaseValue) * parseFloat(DiscPer)) / parseFloat(100)) ? 0 : (parseFloat(PurchaseValue) * parseFloat(DiscPer)) / parseFloat(100);

        //        $("#txtPurDiscAmnt").val(DiscountAmount.toFixed(4));

        //    }
        //    var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
        //    var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
        //    var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
        //    var NetPurchase = (parseFloat(PurchaseValue) + parseFloat(taxAmount)) - parseFloat(DiscountAmount);
        //    $("#txtNetPur").val(NetPurchase.toFixed(4));

        //}
















        //function getpurchaseper1() {
        //    var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
        //    var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
        //    if ($('#chkInctax').is(":checked")) {
        //        var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();

        //        var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

        //        $("#txttaxAmount").val(parseFloat(taxAmount).toFixed(4));
        //        var PurchaseValue = (parseFloat(PurchaseRate) * parseFloat(Quantity)) - (parseFloat(taxAmount));
        //        $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));
        //    }
        //    else {

        //        var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
        //        var taxAmount = ((parseFloat(PurchaseRate) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
        //        $("#txttaxAmount").val(taxAmount.toFixed(4));
        //        var PurchaseValue = parseFloat(PurchaseRate) * parseFloat(Quantity);
        //        $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));

        //    }
        //    if ($('#chkDiscounttax').is(":checked")) {

        //        var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
        //        var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
        //        var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();

        //        var DiscPer = isNaN(parseFloat((DiscountAmount)) * (100) / parseFloat(PurchaseValue)) ? 0 : parseFloat((DiscountAmount)) * (100) / parseFloat(PurchaseValue);
        //        $("#txtPurDiscPer").val(parseFloat(DiscPer).toFixed(4));


        //    }

        //    else {
        //        var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
        //        var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
        //        var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
        //        var DiscPer = isNaN(parseFloat((DiscountAmount) * (100)) / parseFloat(PurchaseValue)) ? 0 : parseFloat((DiscountAmount) * (100)) / parseFloat(PurchaseValue);
        //        $("#txtPurDiscPer").val(DiscPer).toFixed(4);
        //    }
        //    var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
        //    var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
        //    var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
        //    var NetPurchase = (parseFloat(PurchaseValue) + parseFloat(taxAmount)) - parseFloat(DiscountAmount);
        //    $("#txtNetPur").val(NetPurchase.toFixed(4));


        //}
        //function getpurchaseper() {

        //    var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
        //    var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
        //    var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
        //    if ($('#chkInctax').is(":checked")) {


        //        var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
        //        if ($('#chkDiscounttax').is(":checked")) {

        //            var ExcludeDis = parseFloat(PurchaseRate) * parseFloat(DiscPer) / parseFloat(100);
        //            var ActPur = parseFloat(PurchaseRate) - parseFloat(ExcludeDis);
        //            var taxAmount = ((parseFloat(ActPur)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

        //        }
        //        else {
        //            var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));
        //        }
        //        //  var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

        //        $("#txttaxAmount").val(parseFloat(taxAmount).toFixed(4));
        //        var PurchaseValue = (parseFloat(PurchaseRate) * parseFloat(Quantity)) - (parseFloat(taxAmount));
        //        $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));
        //    }
        //    else {
        //        var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
        //        if ($('#chkDiscounttax').is(":checked")) {

        //            var ExcludeDis = parseFloat(PurchaseRate) * parseFloat(DiscPer) / parseFloat(100);
        //            var ActPur = parseFloat(PurchaseRate) - parseFloat(ExcludeDis);
        //            var taxAmount = ((parseFloat(ActPur) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
        //            // var singleAmnt = ((parseFloat(ActPur) * parseFloat(TotalTaxPercetage)) / parseFloat(100));
        //        }
        //        else {
        //            var taxAmount = ((parseFloat(PurchaseRate) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
        //            //  var singleAmnt = ((parseFloat(PurchaseRate) * parseFloat(TotalTaxPercetage)) / parseFloat(100));
        //        }

        //        // var taxAmount = ((parseFloat(PurchaseRate) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
        //        $("#txttaxAmount").val(taxAmount.toFixed(4));
        //        var PurchaseValue = parseFloat(PurchaseRate) * parseFloat(Quantity);
        //        $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));

        //    }
        //    if ($('#chkDiscounttax').is(":checked")) {

        //        var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
        //        var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
        //        var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();

        //        var DiscPer = isNaN(parseFloat((DiscountAmount)) * (100) / parseFloat(PurchaseValue)) ? 0 : parseFloat((DiscountAmount)) * (100) / parseFloat(PurchaseValue);

        //        $("#txtPurDiscPer").val(parseFloat(DiscPer).toFixed(4));
        //        // $("#txtPurDiscPer").val(isNaN(DiscPer) ? 0 : DiscPer).toFixed(4);
        //        //alert(DiscPer);
        //    }

        //    else {


        //        var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
        //        var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
        //        var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();

        //        var DiscPer = isNaN(parseFloat((DiscountAmount) * (100)) / parseFloat(PurchaseValue)) ? 0 : parseFloat((DiscountAmount) * (100)) / parseFloat(PurchaseValue);
        //        // alert(DiscPer);
        //        $("#txtPurDiscPer").val(DiscPer.toFixed(4));
        //        // $("#txtPurDiscPer").val(isNaN(DiscPer) ? 0 : DiscPer).toFixed(4);

        //        // else
        //        //  $("#txtPurDiscPer").val(0);

        //        //var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
        //        //var DiscountAmount = (parseFloat(PurchaseValue) * parseFloat(DiscPer)) / parseFloat(100);
        //        //$("#txtPurDiscAmnt").val(DiscountAmount.toFixed(4));

        //    }

        //    var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
        //    var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
        //    var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
        //    var NetPurchase = (parseFloat(PurchaseValue) + parseFloat(taxAmount)) - parseFloat(DiscountAmount);
        //    $("#txtNetPur").val(NetPurchase.toFixed(4));


        //}
        function getSalesPrice(parameter1) {

            var a = $('#txtKey').val();

            if (a != 0 && parameter1 == "0")
                parameter1 = a;

            if (a == 0)
                parameter1 = parameter1


            if (parameter1 == "0") {
               
                var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                var DiscountAmt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
                
                var SalesPrice = isNaN((parseFloat(MRP)) - (parseFloat(DiscountAmt))) ? "0" : (parseFloat(MRP)) - (parseFloat(DiscountAmt));
                console.log(SalesPrice);
                $("#txtSalesPrice").val(parseFloat(SalesPrice).toFixed(4));
                //  $("#txtSalesPrice").val(isNaN(SalesPrice) ? 0 : SalesPrice).toFixed(4);
                ProfitDiscnCalculation1();
                $('#txtKey').val(parameter1);

            }
            if (parameter1 == "1") {

                var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                var ProfitPer = $("#txtProfitPer").val() == "" ? "0" : $("#txtProfitPer").val();
                // alert(ProfitPer);
                var TaxPercentage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
                // alert(TaxPercentage);
                var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
                SalesPriceCal();

                var salesPrice = parseFloat($("#txtSalesPrice").val() == "" ? "0" : $("#txtSalesPrice").val());
                if (salesPrice > MRP) {
                    var maxProfitPer = 0;
                    if ($('#chkInctax').is(":checked")) {

                        maxProfitPer = (parseFloat(MRP) - parseFloat(PurchaseRate)) * 100 / parseFloat(PurchaseRate);
                    }
                    else {
                        var TaxAmount = parseFloat((PurchaseRate) * parseFloat(TaxPercentage)) / parseFloat(100);
                        maxProfitPer = (parseFloat(MRP) - (parseFloat(PurchaseRate) + (parseFloat(TaxAmount)))) * 100 / (parseFloat(PurchaseRate) + (parseFloat(TaxAmount)));

                    } 
                    toastr.error("Max. Profit Percentage must be " + maxProfitPer.toFixed(4));
                    $("#txtProfitPer").val(maxProfitPer.toFixed(4));
                    $("#txtSalesPrice").val("0");
                    SalesPriceCal();
                }
                Discount1();
                $('#txtKey').val(parameter1);
                //var ui = $('#txtKey').val();
                //alert(ui);
            }
            if (parameter1 == "3") {


                var TaxPercentage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
                var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
                var txtProfitAmount = $("#txtProfitAmount").val() == "" ? "0" : $("#txtProfitAmount").val();
                var TaxAmount = parseFloat((PurchaseRate) * parseFloat(TaxPercentage)) / parseFloat(100);
                if ($('#chkInctax').is(":checked")) {

                    var PurchaseRateNew = (parseFloat(PurchaseRate));
                }
                else {
                    var PurchaseRateNew = (parseFloat(PurchaseRate)) + (parseFloat(TaxAmount));
                }
                // var PurchaseRateNew = parseFloat(PurchaseRate) + parseFloat(TaxAmount);
                var ProfitPercentage = isNaN((parseFloat(txtProfitAmount) * parseFloat(100)) / parseFloat(PurchaseRateNew)) ? 0 : (parseFloat(txtProfitAmount) * parseFloat(100)) / parseFloat(PurchaseRateNew);
                $("#txtProfitPer").val(parseFloat(ProfitPercentage).toFixed(4));
                // $("#txtProfitPer").val(isNaN(ProfitPercentage) ? 0 : ProfitPercentage).toFixed(4);


                SalesPriceCal();
                Discount1();
                $('#txtKey').val(parameter1);
            }
            if (parameter1 == "2") {

                SalesPriceDisc();
                ProfitDiscnCalculation1();
                $('#txtKey').val(parameter1);

            }
            if (parameter1 == "4") {
                var MRP = parseFloat($("#txtMRP").val() == "" ? "0" : $("#txtMRP").val());
                var DiscountAmt = parseFloat($("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val());

                // Guard against division by zero
                var DiscountPer = (MRP > 0)
                    ? (isNaN((DiscountAmt * 100) / MRP) ? 0 : (DiscountAmt * 100) / MRP)
                    : 0;

                $("#txtDiscountPer").val(parseFloat(DiscountPer).toFixed(4));
                SalesPriceDisc();
                ProfitDiscnCalculation1();
                $('#txtKey').val(parameter1);
            }
            if (parameter1 == "5") {
                var MRP = parseFloat($("#txtMRP").val() == "" ? "0" : $("#txtMRP").val());
                var salesPrice = parseFloat($("#txtSalesPrice").val() == "" ? "0" : $("#txtSalesPrice").val());

                // Guard: only calculate if MRP is greater than 0
                var DiscountAmt = (MRP > 0) ? (MRP - salesPrice) : 0;
                $("#txtDiscountAmt").val(DiscountAmt.toFixed(4));

                var DiscountPer = (MRP > 0)
                    ? (isNaN((parseFloat(DiscountAmt) * 100) / MRP) ? 0 : (parseFloat(DiscountAmt) * 100) / MRP)
                    : 0;

                $("#txtDiscountPer").val(parseFloat(DiscountPer).toFixed(4));
                ProfitDiscnCalculation1();
                var TaxPercentage = $("#hfTotTaxsalesPercentage").val() == "" ? "0" : $("#hfTotTaxsalesPercentage").val();
                var taxAmount = 0;
                if ($('#ChkIncStax').is(":checked")) {
                  
                    taxAmount = ((parseFloat(salesPrice)) * (parseFloat(TaxPercentage))) / ((parseFloat(TaxPercentage) + parseFloat(100)));
                    if (taxAmount != null)
                       
                      
                        $("#txtSTaxAmount").val(parseFloat(taxAmount).toFixed(4));


                }
                else {
                    taxAmount = (parseFloat(salesPrice) * parseFloat(TaxPercentage)) / parseFloat(100);
                  
                    if (taxAmount != null)
                        $("#txtSTaxAmount").val(parseFloat(taxAmount).toFixed(4));

                }


                $('#txtKey').val(parameter1);
            }

        }

        //function getSalesPrice(parameter1) {
        //    var a = $('#txtKey').val();

        //    if (a != 0 && parameter1 == "0")
        //        parameter1 = a;

        //    if (a == 0)
        //        parameter1 = parameter1


        //    if (parameter1 == "0") {

        //        var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
        //        var DiscountAmt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
        //        var SalesPrice = isNaN((parseFloat(MRP)) - (parseFloat(DiscountAmt))) ? "0" : (parseFloat(MRP)) - (parseFloat(DiscountAmt));
        //        $("#txtSalesPrice").val(parseFloat(SalesPrice).toFixed(4));

        //        ProfitDiscnCalculation1();
        //        $('#txtKey').val(parameter1);

        //    }
        //    if (parameter1 == "1") {

        //        var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
        //        var ProfitPer = $("#txtProfitPer").val() == "" ? "0" : $("#txtProfitPer").val();

        //        var TaxPercentage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();

        //        var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
        //        SalesPriceCal();
        //        Discount1();
        //        $('#txtKey').val(parameter1);

        //    }
        //    if (parameter1 == "3") {


        //        var TaxPercentage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
        //        var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
        //        var txtProfitAmount = $("#txtProfitAmount").val() == "" ? "0" : $("#txtProfitAmount").val();
        //        var TaxAmount = parseFloat((PurchaseRate) * parseFloat(TaxPercentage)) / parseFloat(100);
        //        if ($('#chkInctax').is(":checked")) {

        //            var PurchaseRateNew = (parseFloat(PurchaseRate));
        //        }
        //        else {
        //            var PurchaseRateNew = (parseFloat(PurchaseRate)) + (parseFloat(TaxAmount));
        //        }

        //        var ProfitPercentage = isNaN((parseFloat(txtProfitAmount) * parseFloat(100)) / parseFloat(PurchaseRateNew)) ? 0 : (parseFloat(txtProfitAmount) * parseFloat(100)) / parseFloat(PurchaseRateNew);
        //        $("#txtProfitPer").val(parseFloat(ProfitPercentage).toFixed(4));
        //        SalesPriceCal();
        //        Discount1();
        //        $('#txtKey').val(parameter1);
        //    }
        //    if (parameter1 == "2") {

        //        SalesPriceDisc();
        //        ProfitDiscnCalculation1();
        //        $('#txtKey').val(parameter1);

        //    }
        //    if (parameter1 == "4") {

        //        var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
        //        var DiscountAmt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
        //        var DiscountPer = isNaN((parseFloat(DiscountAmt) * parseFloat(100)) / parseFloat(MRP)) ? 0 : (parseFloat(DiscountAmt) * parseFloat(100)) / parseFloat(MRP);
        //        $("#txtDiscountPer").val(parseFloat(DiscountPer).toFixed(4));
        //        SalesPriceDisc();
        //        ProfitDiscnCalculation1();
        //        $('#txtKey').val(parameter1);
        //    }

        //}

        function Discount1() {
            var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
            var SalesPrice = $("#txtSalesPrice").val() == "" ? "0" : $("#txtSalesPrice").val();
            var DiscountAmount = isNaN(parseFloat(MRP) - parseFloat(SalesPrice)) ? 0 : parseFloat(MRP) - parseFloat(SalesPrice);
            $("#txtDiscountAmt").val(parseFloat(DiscountAmount).toFixed(4));


            var DiscountPer = isNaN((parseFloat(DiscountAmount) * parseFloat(100)) / parseFloat(MRP)) ? 0 : (parseFloat(DiscountAmount) * parseFloat(100)) / parseFloat(MRP);
            $("#txtDiscountPer").val(parseFloat(DiscountPer).toFixed(4));


        }
        function SalesPriceCal() {

            var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
            var ProfitPer = $("#txtProfitPer").val() == "" ? "0" : $("#txtProfitPer").val();
            var TaxPercentage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
            var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
            var TaxAmount = parseFloat((PurchaseRate) * parseFloat(TaxPercentage)) / parseFloat(100);

            if ($('#chkInctax').is(":checked")) {

                var PurchaseRateAmount = (parseFloat(PurchaseRate));
            }
            else {
                var PurchaseRateAmount = (parseFloat(PurchaseRate)) + (parseFloat(TaxAmount));
            }
            if (ProfitPer == 0) {
                $("#txtProfitAmount").val(0);
                var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();

                $("#txtSalesPrice").val(parseFloat(MRP).toFixed(4));


            }
            else {

                var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();

                if ($('#chkInctax').is(":checked")) {

                    var PurchaseRateAmount = (parseFloat(PurchaseRate));
                }
                else {
                    var PurchaseRateAmount = (parseFloat(PurchaseRate)) + (parseFloat(TaxAmount));
                }

                var ProfitAmount = isNaN((parseFloat(PurchaseRateAmount) * parseFloat(ProfitPer)) / parseFloat(100)) ? 0 : (parseFloat(PurchaseRateAmount) * parseFloat(ProfitPer)) / parseFloat(100);
                $("#txtProfitAmount").val(parseFloat(ProfitAmount).toFixed(4));



                var SalesPe = isNaN((parseFloat(PurchaseRateAmount)) + (parseFloat(ProfitAmount))) ? 0 : (parseFloat(PurchaseRateAmount)) + (parseFloat(ProfitAmount));
                $("#txtSalesPrice").val(parseFloat(SalesPe).toFixed(4));



            }
            var salePrice = $("#txtSalesPrice").val() == "" ? "0" : $("#txtSalesPrice").val();

            //if (parseFloat(salePrice) > parseFloat(MRP))
            //alert('Sales Price not greater than MRP');




        }
        function SalesPriceDisc() {


            var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
            var DiscountPer = $("#txtDiscountPer").val() == "" ? "0" : $("#txtDiscountPer").val();

            if (DiscountPer == 0) {
                $("#txtDiscountAmt").val(0);
                var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                $("#txtSalesPrice").val(parseFloat(MRP).toFixed(4));


            }
            else {

                var Discount = isNaN(((parseFloat(MRP)) * parseFloat(DiscountPer)) / parseFloat(100)) ? 0 : ((parseFloat(MRP)) * parseFloat(DiscountPer)) / parseFloat(100);
                $("#txtDiscountAmt").val(parseFloat(Discount).toFixed(4));


                var DiscountAmt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
                var SalesPrice = isNaN((parseFloat(MRP)) - (parseFloat(DiscountAmt))) ? 0 : (parseFloat(MRP)) - (parseFloat(DiscountAmt));
                $("#txtSalesPrice").val(parseFloat(SalesPrice).toFixed(4));

            }

        }

        function ProfitDiscnCalculation1() {


            var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
            var TaxPercentage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
            var TaxAmount = parseFloat((PurchaseRate) * parseFloat(TaxPercentage)) / parseFloat(100);

            if ($('#chkInctax').is(":checked")) {

                var PurchaseRateNew = parseFloat(PurchaseRate);
            }
            else {

                var PurchaseRateNew = parseFloat(PurchaseRate) + parseFloat(TaxAmount);
            }


            var SalesPrice = $("#txtSalesPrice").val() == "" ? "0" : $("#txtSalesPrice").val();

            var ProfitAmount = isNaN(parseFloat(SalesPrice) - parseFloat(PurchaseRateNew)) ? 0 : parseFloat(SalesPrice) - parseFloat(PurchaseRateNew);
            console.log(ProfitAmount);
            if (PurchaseRateNew > 0)
                {
                var ProfitPer = isNaN((parseFloat(ProfitAmount) * parseFloat(100)) / parseFloat(PurchaseRateNew)) ? 0 : (parseFloat(ProfitAmount) * parseFloat(100)) / parseFloat(PurchaseRateNew);
                console.log(ProfitPer);
        }
            if (ProfitPer < 0)
                $("#txtProfitPer").val(0);
            if (ProfitPer > 0)
                $("#txtProfitPer").val(ProfitPer.toFixed(4));

            if (ProfitAmount < 0)
                $("#txtProfitAmount").val(0);
            if (ProfitAmount > 0)
                $("#txtProfitAmount").val(ProfitAmount.toFixed(4));


            var MRP = parseFloat($("#txtMRP").val() == "" ? "0" : $("#txtMRP").val());
            var DiscountAmt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();

            // Add MRP check before calculating DiscountPer
            var DiscountPer = (MRP > 0)
                ? (isNaN((parseFloat(DiscountAmt) * parseFloat(100)) / MRP)
                    ? 0
                    : (parseFloat(DiscountAmt) * parseFloat(100)) / MRP)
                : 0;

            $("#txtDiscountPer").val(parseFloat(DiscountPer).toFixed(4));


        }

        function TaxAmount() {

            var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();


            var DiscountAmt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
            var SalesPrice = MRP - DiscountAmt
            var TaxPercentage = $("#hfTotTaxsalesPercentage").val() == "" ? "0" : $("#hfTotTaxsalesPercentage").val();

            if ($('#ChkIncStax').is(":checked")) {

                var taxAmount = ((parseFloat(SalesPrice)) * (parseFloat(TaxPercentage))) / ((parseFloat(TaxPercentage) + parseFloat(100)));
                if (taxAmount != null)
                    $("#txtSTaxAmount").val(parseFloat(taxAmount).toFixed(4));
                $("#txtSalesPrice").val(parseFloat(SalesPrice).toFixed(4));

            }
            else {
                var taxAmount = (parseFloat(SalesPrice) * parseFloat(TaxPercentage)) / parseFloat(100);
                if (taxAmount != null)
                    $("#txtSTaxAmount").val(parseFloat(taxAmount).toFixed(4));
                var Sale = parseFloat(SalesPrice) + parseFloat(taxAmount)

                $("#txtSalesPrice").val(parseFloat(Sale).toFixed(4));
                if (parseFloat(MRP) == parseFloat(SalesPrice)) {
                    //alert('Sales Price not greater than MRP');
                    return false;
                }
            }
        }
        function LoadQuotationDetails() {
            $("#btnQuation").click();

        }





        function RoundOffCalc() {
            // oldfrom  //var RoundOff = $("#txtRoundOff").val() == "" ? "0" : $("#txtRoundOff").val();

            //var totalAmount = $("#txtTotalAmount").val() == "" ? "0" : $("#txtTotalAmount").val();

            //var total = parseFloat(totalAmount);

            //var BilltotalAmount = $("#txtBillNetAmount").val() == "" ? "0" : $("#txtBillNetAmount").val();
            //var fracpart = (((total < 1.0) ? total : (total % Math.floor(total))) * 100).toFixed(2);
            //if (parseFloat(fracpart) >= 50) {
            //    var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);

            //    var totalnetRoundamount = parseFloat(total) + parseFloat(Balancefract);
            //    var balfract = parseFloat(fracpart / 100).toFixed(2);

            //    $("#txtRoundOff").val(Balancefract);


            //}

            //else if (fracpart < 50) {

            //    var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);


            //    var totalnetRoundamount = parseFloat(total) - parseFloat(fracpart / 100);
            //    var balfract = parseFloat(fracpart / 100).toFixed(2);
            //    $("#txtRoundOff").val(-balfract);


            //}//oldto

            var RoundOff = $("#txtRoundOff").val() == "" ? "0" : $("#txtRoundOff").val();
            var other = $("#txtOther").val() == "" ? "0" : $("#txtOther").val();
            //alert(RoundOff);
            var totalAmount = $("#txtBillNetAmount").val() == "" ? "0" : $("#txtBillNetAmount").val();

            // var fright = $("#txtFright").val() == "" ? "0" : $("#txtFright").val();
            // var total = parseFloat(fright) + parseFloat(totalAmount)
            // var ExpenseAmount = $("#hfOtherCharges").val() == "" ? "0" : $("#hfOtherCharges").val();
            // alert(ExpenseAmount);

            var total = parseFloat(totalAmount);
            var BilltotalAmount = $("#txtBillNetAmount").val() == "" ? "0" : $("#txtBillNetAmount").val();
            var fracpart = (((total < 1.0) ? total : (total % Math.floor(total))) * 100).toFixed(2);
            if (parseFloat(fracpart) >= 50) {
                var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);

                var totalnetRoundamount = parseFloat(total) + parseFloat(Balancefract);
                var balfract = parseFloat(fracpart / 100).toFixed(2);

                $("#txtBillNetAmount").val(totalnetRoundamount.toFixed(2));
                $("#txtRoundOff").val(Balancefract);


            }

            else if (fracpart < 50) {

                var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);


                var totalnetRoundamount = parseFloat(total) - parseFloat(fracpart / 100);
                var balfract = parseFloat(fracpart / 100).toFixed(2);

                $("#txtBillNetAmount").val(totalnetRoundamount.toFixed(2));
                $("#txtRoundOff").val(-balfract);


            }

        }


        function PopRoundOffCalc() {
            var RoundOff = $("#txtpopupRoundOff").val() == "" ? "0" : $("#txtpopupRoundOff").val();

            var totalAmount = $("#txtpopupPurchaseAmoount").val() == "" ? "0" : $("#txtpopupPurchaseAmoount").val();
            var Disc = $("#txtpopupDiscountAmount").val() == "" ? "0" : $("#txtpopupDiscountAmount").val();
            var total = parseFloat(totalAmount) - parseFloat(Disc)



            var BilltotalAmount = $("#txtpopupNetPayAmount").val() == "" ? "0" : $("#txtpopupNetPayAmount").val();
            var fracpart = (((total < 1.0) ? total : (total % Math.floor(total))) * 100).toFixed(2);
            if (parseFloat(fracpart) >= 50) {
                var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);

                var totalnetRoundamount = parseFloat(total) + parseFloat(Balancefract);
                var balfract = parseFloat(fracpart / 100).toFixed(2);

                $("#txtpopupNetPayAmount").val(totalnetRoundamount.toFixed(2));
                $("#txtpopupRoundOff").val(Balancefract);


            }

            else if (fracpart < 50) {

                var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);


                var totalnetRoundamount = parseFloat(total) - parseFloat(fracpart / 100);
                var balfract = parseFloat(fracpart / 100).toFixed(2);

                $("#txtpopupNetPayAmount").val(totalnetRoundamount.toFixed(2));
                $("#txtpopupRoundOff").val(-balfract);


            }


        }


        function ClearSearch() {

        }


        function CalculateNetTotal() {


            //if ($("#txtTotalAmount").val() == "") {
            //    $("#txtTotalAmount").val("0");
            //}
            //if ($("#txtRoundOff").val() == "") {
            //    $("#txtRoundOff").val("0");
            //}
            //if ($("#txtTotalAmount").val() != "" && $("#txtRoundOff").val() != "") {



            //    var totalAmount = $("#txtTotalAmount").val() == "" ? "0" : $("#txtTotalAmount").val();
            //    sum = parseFloat(totalAmount);

            //}


            //RoundOffCalc();
            //ItemClear();
        }
        function CalculateNetT() {


            //if ($("#txtTotalAmount").val() == "") {
            //    $("#txtTotalAmount").val("0");
            //}
            //if ($("#txtRoundOff").val() == "") {
            //    $("#txtRoundOff").val("0");
            //}
            //if ($("#txtTotalAmount").val() != "" && $("#txtRoundOff").val() != "") {



            //    var totalAmount = $("#txtTotalAmount").val() == "" ? "0" : $("#txtTotalAmount").val();
            //    sum = parseFloat(totalAmount);

            //}
            RoundOffCalc();
            ItemClear();

        }




        function CalculateWholeSalePricefromPercentage() {

            var retailPrice = $("#txtRetailPrice").val() == "" ? "0" : $("#txtRetailPrice").val();
            var PerWholeSale = $("#txtPerforWholeSale").val() == "" ? "0" : $("#txtPerforWholeSale").val();
            var WholesalePrice = 0;
            WholesalePrice = ((parseFloat(retailPrice) * parseFloat(PerWholeSale)) / parseFloat(100));
            WholesalePrice = parseFloat(retailPrice) - parseFloat(WholesalePrice);

            $("#txtWholeSalePrice").val(WholesalePrice.toFixed(2));


        }
        function CalculatePercentagefromWholeSalePrice() {

            var retailPrice = $("#txtRetailPrice").val() == "" ? "0" : $("#txtRetailPrice").val();
            var Whole = $("#txtWholeSalePrice").val() == "" ? "0" : $("#txtWholeSalePrice").val();
            var Wholesaleper = 0;
            Wholesaleper = (((parseFloat(retailPrice) - parseFloat(Whole)) * parseFloat(100)) / parseFloat(retailPrice));
            if (parseFloat(Wholesaleper) > parseFloat(0)) {
                $("#txtPerforWholeSale").val(Wholesaleper.toFixed(2));
            }
            else {
                $("#txtPerforWholeSale").val(0);
            }



        }
        function CalculatePercentagefromCardPrice() {

            var retailPrice = $("#txtRetailPrice").val() == "" ? "0" : $("#txtRetailPrice").val();
            var PrivilagePrice = $("#txtPrivilagePrice").val() == "" ? "0" : $("#txtPrivilagePrice").val();
            var Privilageper = 0;
            Privilageper = (((parseFloat(retailPrice) - parseFloat(PrivilagePrice)) * parseFloat(100)) / parseFloat(retailPrice));
            $("#txtPerforCardPrice").val(Privilageper.toFixed(2));

            if (parseFloat(Privilageper) > parseFloat(0)) {
                $("#txtPerforCardPrice").val(Privilageper.toFixed(2));
            }
            else {
                $("#txtPerforCardPrice").val(0);
            }
        }


        function CalculateCardPricefromPercentage() {

            var retailPrice = $("#txtRetailPrice").val() == "" ? "0" : $("#txtRetailPrice").val();
            var PerCardSale = $("#txtPerforCardPrice").val() == "" ? "0" : $("#txtPerforCardPrice").val();
            var CardsalePrice = 0;
            CardsalePrice = ((parseFloat(retailPrice) * parseFloat(PerCardSale)) / parseFloat(100));
            CardsalePrice = parseFloat(retailPrice) - parseFloat(CardsalePrice);

            $("#txtPrivilagePrice").val(CardsalePrice.toFixed(2));


        }

        function calculatePurchaseTotal() {
            var DiscAmnt = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
            var Qty = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
            var Free = $("#txtFree").val() == "" ? "0" : $("#txtFree").val();
            var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
            if (parseFloat(Qty) > 0) {
                if (parseFloat(PurchaseRate) > 0) {


                    Purtot = ((parseFloat(Qty) * parseFloat(PurchaseRate)) + (parseFloat(Free)) + parseFloat(DiscAmnt));

                    $("#txtPurTotal").val(Purtot.toFixed(2));

                    CalculateNetPur();
                }
            }
        }

        function calculateVatAmnt() {

            try {

                var vatper = $("#txtVat").val() == "" ? "0" : $("#txtVat").val();
                var Cessper = $("#txtCess").val() == "" ? "0" : $("#txtCess").val();

                var Purtot = $("#txtPurTotal").val() == "" ? "0" : $("#txtPurTotal").val();
                var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();



                var VatAmnt = (((parseFloat(Purtot) - (parseFloat(Purtot) * parseFloat(DiscPer) / parseFloat(100))) * parseFloat(vatper)) / parseFloat(100));

                var CessAmnt = (((parseFloat(Purtot) - (parseFloat(Purtot) * parseFloat(DiscPer) / parseFloat(100))) * parseFloat(Cessper)) / parseFloat(100));

                $("#txtVatAmount").val(VatAmnt.toFixed(2));
                $("#txtCessAmt").val(CessAmnt.toFixed(2));

            }
            catch (e) {
                alert(e);
            }


        }




        function CalculateNetPur() {


            var PurTot = $("#txtPurTotal").val() == "" ? "0" : $("#txtPurTotal").val();
            var PurDisc = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();

            var netpur = parseFloat(PurTot) - parseFloat(PurDisc);
            $("#txtNetPur").val(netpur.toFixed(2));

        }


        function calculatePurTotal() {

            var NetPur = $("#txtNetPur").val() == "" ? "0" : $("#txtNetPur").val();

            var PurDisc = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
            var Discper = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
            if (parseFloat(NetPur) > 0) {
                varpurtotal = parseFloat(NetPur) + parseFloat(PurDisc);
                Discper = parseFloat(PurDisc) * parseFloat(100) / parseFloat(varpurtotal);



                $("#txtPurTotal").val(varpurtotal.toFixed(2));

                CalculatePurchaseRates();
            }


        }


        function calculatePurTotalEdit() {

            var NetPur = $("#txtNetPur").val() == "" ? "0" : $("#txtNetPur").val();
            var PurDisc = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
            var Discper = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
            if (parseFloat(NetPur) > 0) {
                varpurtotal = parseFloat(NetPur) + parseFloat();
                Discper = parseFloat(PurDisc) * parseFloat(100) / parseFloat(varpurtotal);



                $("#txtPurTotal").val(varpurtotal.toFixed(2));

                CalculateProfitPercentage();
            }
        }

        function CalculatePurchaseRate() {

            var DiscAmnt = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
            var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
            var Purtot = $("#txtPurTotal").val() == "" ? "0" : $("#txtPurTotal").val();
            var Qty = $("#txtActqty").val() == "" ? "0" : $("#txtActqty").val();


            if (parseFloat(Qty) > 0) {

                if (parseFloat(Purtot) > 0) {

                    Purtot = ((parseFloat(Purtot) - parseFloat(DiscAmnt)) / parseFloat(Qty));


                    $("#txtPurchaseRate").val(Purtot.toFixed(2));


                }

            }
            CalculateNetamountofItems();
            CalculateProfitPercentage();

        }
        function CalculatePurchaseRates() {

            var DiscAmnt = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
            var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
            var Purtot = $("#txtPurTotal").val() == "" ? "0" : $("#txtPurTotal").val();
            var Qty = $("#txtActqty").val() == "" ? "0" : $("#txtActqty").val();

            if (DiscPer = '0.00') {

                $("#hfpurrate").val(((parseFloat(Purtot) - parseFloat(0)) / parseFloat(Qty)));

            }
            if (parseFloat(Qty) > 0) {

                if (parseFloat(Purtot) > 0) {

                    Purtot = ((parseFloat(Purtot) - parseFloat(DiscAmnt)) / parseFloat(Qty));


                    $("#txtPurchaseRate").val(Purtot.toFixed(2));




                }

            }
            CalculateNetamountofItems();
            CalculateProfitPercentage();

        }

        function CalculateDiscountAmountPurchase() {


            var DiscAmnt = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
            var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
            var Purtot = $("#txtPurTotal").val() == "" ? "0" : $("#txtPurTotal").val();
            DiscAmnt = ((parseFloat(Purtot) * parseFloat(DiscPer)) / parseFloat(100));
            $("#txtPurDiscAmnt").val(DiscAmnt.toFixed(2));

            CalculateNetPur();

        }
        function CalculateDiscountPercentagePurchase() {
            //Calculate Discount Amount
            var DiscAmnt = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
            var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
            var Purtot = $("#txtPurTotal").val() == "" ? "0" : $("#txtPurTotal").val();
            //if (parseFloat(Purtot) > 0) {
            //    DiscPer = ((parseFloat(DiscAmnt) * parseFloat(100)) / parseFloat(Purtot));
            //    CalculateNetPur();
            //}
            //else {
            //    DiscPer = 0;
            //    CalculateNetPur();
            //}


            CalculateNetPur();

        }


        function CalculateDiscountProfitEdit() {
            try {
                var RetailPrice = $("#txtRetailPrice").val() == "" ? "0" : $("#txtRetailPrice").val();
                var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
                var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                var Profitper = $("#txtProfitPer").val() == "" ? "0" : $("#txtProfitPer").val();

                var DiscAmount = 0;
                var Discpercent = 0;
                var ProfitPer = 0;


                DiscAmount = parseFloat(MRP) - parseFloat(RetailPrice)
                $("#txtDiscountAmt").val(DiscAmount.toFixed(4));
                CalculateDiscountPercentage();
                CalculateProfitPercentage();
                $("#txtWholeSalePrice").val(parseFloat(RetailPrice).toFixed(2));

                $("#txtPrivilagePrice").val(parseFloat(RetailPrice).toFixed(2));



                if (parseFloat(Profitper) > 0) {

                }

            }

            catch (e) {
                alert(e);
            }
        }

        function CalculateProfitPerwhileRetail() {
            try {
                var RetailPrice = $("#txtRetailPrice").val() == "" ? "0" : $("#txtRetailPrice").val();
                var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
                var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();

                var DiscAmount = 0;
                var Discpercent = 0;
                var ProfitPer = 0;


                DiscAmount = parseFloat(MRP) - parseFloat(RetailPrice)
                $("#txtDiscountAmt").val(DiscAmount.toFixed(4));
                CalculateDiscountPercentage();
                CalculateProfitPercentage();
                $("#txtWholeSalePrice").val(parseFloat(RetailPrice).toFixed(2));

                $("#txtPrivilagePrice").val(parseFloat(RetailPrice).toFixed(2));

                CalculateCardPricefromPercentage();
                CalculateWholeSalePricefromPercentage();

            }

            catch (e) {
                alert(e);
            }
        }
        function CalculatePrices() {
            try {


                var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                var DiscAmnt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
                var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
                var Profitper = $("#txtProfitPer").val() == "" ? "0" : $("#txtProfitPer").val();
                var abb = parseFloat(Profitper).toFixed(2);
                var Retprice = (parseFloat(MRP) - parseFloat(DiscAmnt));
                $("#txtRetailPrice").val(parseFloat(Retprice).toFixed(2));



                $("#txtWholeSalePrice").val(parseFloat(Retprice).toFixed(2));
                //  $("#txtRetailPrice").val((MRP - DiscAmnt).toFixed(2));
                $("#txtPrivilagePrice").val(parseFloat(Retprice).toFixed(2));
                CalculateNetamountofItems();



            }
            catch (e) {
                alert(e);
            }
        }

        function CalculatePricesWhenProfitEdit() {
            try {


                var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                var DiscAmnt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
                var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
                var Profitper = $("#txtProfitPer").val() == "" ? "0" : $("#txtProfitPer").val();

                var abb = parseFloat(Profitper).toFixed(2);
                var Retprice = 0;

                Retprice = ((parseFloat(PurchaseRate) * parseFloat(Profitper)) / 100) + parseFloat(PurchaseRate);
                $("#txtRetailPrice").val(parseFloat(Retprice).toFixed(2));
                $("#txtWholeSalePrice").val(parseFloat(Retprice).toFixed(2));
                $("#txtPrivilagePrice").val(parseFloat(Retprice).toFixed(2));
                if (parseFloat(MRP) > 0) {
                    Discpercent = isNaN(((parseFloat(MRP) - parseFloat(Retprice)) * 100) / (parseFloat(MRP))) ? 0 : ((parseFloat(MRP) - parseFloat(Retprice)) * 100) / (parseFloat(MRP));
                    $("#txtDiscountPer").val(Discpercent.toFixed(2));

                    DiscAmount = isNaN((parseFloat(MRP) * parseFloat(Discpercent)) / parseFloat(100)) ? 0 : (parseFloat(MRP) * parseFloat(Discpercent)) / parseFloat(100);
                    $("#txtDiscountAmt").val(DiscAmount.toFixed(2));


                }



                CalculateNetamountofItems();

            }
            catch (e) {
                alert(e);
            }
        }

        function CalculatePopUpBalanceAndBillAmount() {
            try {


                var netAmt = $("#txtpopupNetPayAmount").val() == "" ? "0" : $("#txtpopupNetPayAmount").val();
                var advncAmt = $("#txtpopupAdvanceAmount").val() == "" ? "0" : $("#txtpopupAdvanceAmount").val();
                var BalAmnt = $("#txtpopupBalanceAmount").val() == "" ? "0" : $("#txtpopupBalanceAmount").val();



                if (parseFloat(netAmt) > 0) {
                    if (parseFloat(advncAmt) > parseFloat(netAmt)) {
                        $("#txtpopupAdvanceAmount").val(parseFloat(0).toFixed(2));
                        $("#txtpopupBalanceAmount").val(parseFloat(netAmt).toFixed(2));

                    }

                    else {
                        BalAmnt = (parseFloat(netAmt) - parseFloat(advncAmt))
                        $("#txtpopupBalanceAmount").val(BalAmnt.toFixed(2));


                    }


                }
                else {
                    $("#txtpopupBalanceAmount").val(0);
                }




            }
            catch (e) {
                alert(e);
            }
        }

        function CalculatePopUpNetAmount() {
            try {
                var popbillamount = $("#txtpopupPurchaseAmoount").val() == "" ? "0" : $("#txtpopupPurchaseAmoount").val();
                var popdiscAmount = $("#txtpopupDiscountAmount").val() == "" ? "0" : $("#txtpopupDiscountAmount").val();
                var popupRoundOff = $("#txtpopupRoundOff").val() == "" ? "0" : $("#txtpopupRoundOff").val();




                var totolamnt = 0;
                if (parseFloat(popdiscAmount) > 0) {
                    totolamnt = (parseFloat(popbillamount) - parseFloat(popdiscAmount) + parseFloat(popupRoundOff));
                }
                else {
                    // DiscPer = 0;
                    totolamnt = (parseFloat(popbillamount) - parseFloat(popdiscAmount) + parseFloat(popupRoundOff));
                }

                $("#txtpopupNetPayAmount").val(totolamnt.toFixed(2));
                CalculatePopUpBalanceAndBillAmount();
            }
            catch (e) {
                alert(e);
            }
        }
        function CalculateNetamountofItems() {

            try {



                var NetPur = $("#txtNetPur").val() == "" ? "0" : $("#txtNetPur").val();
                var VatAmount = $("#txtVatAmount").val() == "" ? "0" : $("#txtVatAmount").val();
                var CessAmount = $("#txtCessAmt").val() == "" ? "0" : $("#txtCessAmt").val();

                var NetAmount = parseFloat(NetPur) + parseFloat(VatAmount) + parseFloat(CessAmount);

                $("#txtAmount").val(NetAmount.toFixed(2));
            }
            catch (e) {
                alert(e);
            }
        }

        function CalculaterRetailPrice() {

            try {
                var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                var DiscAmnt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();


                var RetailPrice = (parseFloat(MRP) - parseFloat(DiscAmnt));

                $("#txtRetailPrice").val(RetailPrice.toFixed(2));
                $("#txtWholeSalePrice").val(RetailPrice.toFixed(2));
                $("#txtPrivilagePrice").val(RetailPrice.toFixed(2));
                // alert(RetailPrice);

                CalculateCardPricefromPercentage();
                CalculateWholeSalePricefromPercentage();

            }
            catch (e) {
                alert(e);
            }

        }

        function calculateDiscAmntWhileProfit() {
            try {


                var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
                var PurchaseVat = $("#txtPurchaseVat").val() == "" ? "0" : $("#txtPurchaseVat").val();
                var ProfitPer = $("#txtProfitPer").val() == "" ? "0" : $("#txtProfitPer").val();
                var DiscAmount = 0;
                var cr = 0;


                PurchaseVat = (parseFloat(PurchaseVat) * parseFloat(PurchaseRate) / parseFloat(100));
                DiscAmount = (parseFloat(MRP) - (parseFloat(PurchaseRate) + parseFloat(PurchaseVat)));
                cr = (parseFloat(ProfitPer) * (parseFloat(PurchaseRate) + parseFloat(PurchaseVat)) / parseFloat(100));
                DiscAmount = (parseFloat(DiscAmount) - parseFloat(cr));

                $("#txtDiscountAmt").val(DiscAmount.toFixed(2));
                CalculateDiscountPercentage();
                CalculaterRetailPrice();
            }
            catch (e) {
                alert(e);
            }
        }


        function CalculateProfitPercentage() {

            var DiscAmnt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
            var DiscPer = $("#txtDiscountPer").val() == "" ? "0" : $("#txtDiscountPer").val();
            var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
            var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
            var PurchaseVat = $("#txtPurchaseVat").val() == "" ? "0" : $("#txtPurchaseVat").val();
            var ProfitPer;

            var DiscAmount = 0;
            var Discpercent = 0;
            if (parseFloat(PurchaseRate) > 0) {
                PurchaseVat = (parseFloat(PurchaseVat) * parseFloat(PurchaseRate) / 100)
                ProfitPer = (parseFloat(MRP) - parseFloat(DiscAmnt))
                ProfitPer = (parseFloat(ProfitPer) - (parseFloat(PurchaseRate) + parseFloat(PurchaseVat)));

                ProfitPer = ((parseFloat(ProfitPer) * parseFloat(100)) / (parseFloat(PurchaseRate) + parseFloat(PurchaseVat)));



            }
            else {
                ProfitPer = 0;
                // alert('b');
            }
            //alert(DiscAmnt);

            if (parseFloat(ProfitPer) >= 0) {
                $("#txtProfitPer").val(ProfitPer.toFixed(2));

            }
            else {
                $("#txtProfitPer").val("0");
            }

            var Actqty = $("#txtActqty").val() == "" ? "0" : $("#txtActqty").val();
            if (parseFloat(Actqty) > 0) {

                var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                var Amount = $("#txtAmount").val() == "" ? "0" : $("#txtAmount").val();
                var Profitamount = (parseFloat(MRP) - (parseFloat(Amount) / parseFloat(Actqty)));
                var ProfitPer = ((parseFloat(Profitamount) / parseFloat(MRP)) * parseFloat(100));
                $("#txtProfitAmount").val(Profitamount.toFixed(2));
                $("#txtProfitPer").val(ProfitPer.toFixed(2));
            }
            else {

                $("#txtProfitAmount").val(0);
                $("#txtProfitPer").val(0);
            }



        }

        function floorFigure(figure, decimals) {
            // alert('a');
            if (!decimals) decimals = 2;
            var d = Math.pow(10, decimals);
            return ((figure * d) / d).toFixed(decimals);
        };


        function DecimalOrIntValidation(eventRef) {
            if ($("#hfPacked").val() == "False") {
                CheckNumWithDecimal(eventRef);
            }
            else {
                CheckInteger(eventRef);
            }
        }
        function checkIntOrDecimal(evnt, txtbox) {
            //var input =[0];

            if (window.getSelection().toString().trim() != "") {
                $(txtbox).val("");
            }

            DecimalOrIntValidation(evnt);
        }



        function ShowAddDiv() {

            $("#divAdd").show();
            $("#divView").hide();

        }

        function SearchClear() {

            var dt = new Date();
            $("#txtFromDateSearch").val(dt.format("dd/MM/yyyy"));
            $("#txtToDateSearch").val(dt.format("dd/MM/yyyy"));
            $("#ddlStoreSearch").val("0");
            $("#ddlSupplierSearch").val("0");
            $("#ddlStatusApproveSearch").val("2");
            $("#ddlPaymentModeCashCredit").val("2");
            $("#txtRefnoSrch").val("");
            $("#txtSupplierSearch").val("");
        }



        function CalculateTotal() {
            try {
                var sum = 0;
                $("#gvItems tr").has('td').each(function () {

                    var s = $(this).find("td:eq(24)").html();

                    s = (s == '' || s == null ? 0 : s);
                    sum = parseFloat(sum) + parseFloat(s);

                });

                $("#txtTotalAmount").val(sum.toFixed(2));

            }
            catch (e) {
                alert(e);
            }
        }

        function LoadPurchaseOrder() {
            $("#btnQuation").click();

        }
        function CalcActualQuantity() {


            var qty = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
            var nounit = $("#hfUnitQuantity").val() == "" ? "0" : $("#hfUnitQuantity").val();
            var ActQty = parseFloat(nounit) * parseFloat(qty);


            $("#txtActqty").val(ActQty.toFixed(3));
            $("#txtCheckActQuantity").val(ActQty.toFixed(3));

        }
        function CalcActualLessQuantity() {
            $("#btnHiddenActullessQuantity").click();

        }

        function CalcLessQuantity() {


            var qty = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
            var nounit = $("#hfUnitQuantity").val() == "" ? "0" : $("#hfUnitQuantity").val();
            var Actqty = $("#txtActqty").val() == "" ? "0" : $("#txtActqty").val();
            var Free = $("#txtFree").val() == "" ? "0" : $("#txtFree").val();
            var lessqty = parseFloat(nounit) * parseFloat(qty) - parseFloat(Actqty) + parseFloat(Free);
        }
        function CalcActualQuantityFromUnit() {
            $("#btnHiddenActualQuantityfromUnit").click();

        }
        function SelectMaterialRecieptItems() {
            $("#btnHiddenMaterialRecieptItems").click();

        }
        function btnInvoiceNumberMaterialRecieptItems() {
            $("#btnInvoiceNumber").click();

        }

        function btnStoreMaterialRecieptItems() {
            $("#btnStoreMaterialRecieptSelect").click();

        }




        function CalculatePopDiscountAmount() {
            var DiscAmnt = $("#txtpopupDiscountAmount").val() == "" ? "0" : $("#txtpopupDiscountAmount").val();
            var DiscPer = $("#txtpopupDiscountPer").val() == "" ? "0" : $("#txtpopupDiscountPer").val();
            var MRP = $("#txtpopupPurchaseAmoount").val() == "" ? "0" : $("#txtpopupPurchaseAmoount").val();
            DiscAmnt = ((parseFloat(MRP) * parseFloat(DiscPer)) / parseFloat(100));

            $("#txtpopupDiscountAmount").val(DiscAmnt.toFixed(2));


        }


        function CalculatePopDiscountPercentage() {

            var DiscAmnt = $("#txtpopupDiscountAmount").val() == "" ? "0" : $("#txtpopupDiscountAmount").val();
            var DiscPer = $("#txtpopupDiscountPer").val() == "" ? "0" : $("#txtpopupDiscountPer").val();
            var MRP = $("#txtpopupPurchaseAmoount").val() == "" ? "0" : $("#txtpopupPurchaseAmoount").val();
            if (parseFloat(MRP) > 0) {
                DiscPer = ((parseFloat(DiscAmnt) * parseFloat(100)) / parseFloat(MRP));
            }
            else {
                DiscPer = 0;
            }

            $("#txtpopupDiscountPer").val(DiscPer.toFixed(2));


        }

        function ValidateCheckBox(sender, args) {
            //  alert('a');
            var p = $("#txtpopupPurchaseAmoount").val() == "" ? "0" : $("#txtpopupPurchaseAmoount").val();
            var DiscAmnt = $("#txtpopupDiscountAmount").val() == "" ? "0" : $("#txtpopupDiscountAmount").val();

            if (parseFloat(p) >= parseFloat(DiscAmnt)) {
                args.IsValid = true;

            } else {
                args.IsValid = false;

            }
        }


        function ShowProgress() {
            if (Page_ClientValidate("submit")) {
                setTimeout(function () {


                    var loading = $(".loading");
                    loading.show();
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);
            }
        }




    </script>
</head>
<body>
    <form id="frmInters" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5">
                    <div class="heading2">
                        <div class="user">Interstate Purchase</div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                                <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle" TabIndex="44" runat="server" OnClick="imgbtnAdd_Click" OnClientClick="ClearAdd();ShowAddDiv();  " />

                                <asp:ImageButton ID="ImageButton1" ImageUrl="images/view.png" CssClass="imagebuttonStyle" TabIndex="45" runat="server" OnClientClick="SearchClear(); " OnClick="ImageButton1_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg" style="width: 100%">
                            <div id="divAdd" runat="server" style="display: block;">
                                <ul class="contentul">
                                    <div class="divL" style="width: 430px;">

                                        <li style="display: none;">
                                            <asp:Label ID="Label2" runat="server" CssClass="label" Width="80px" Text="Supplier"></asp:Label>
                                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="dropdownMandatory" Width="340px" Onblur="SelectMaterialRecieptItems();"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select a Supplier" ControlToValidate="ddlSupplier" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                            </cc:ValidatorCalloutExtender>


                                            <asp:Button ID="btnHiddenMaterialRecieptItems" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnHiddenMaterialRecieptItems_Click" />

                                        </li>
                                        <li>
                                            <asp:Label ID="Label61" runat="server" CssClass="label" Width="80px" Text="Supplier"></asp:Label>
                                            <asp:TextBox ID="txtSupplierName" runat="server" AutoPostBack="false" TabIndex="1" CssClass="textboxMandatory" onBlur="popUpSupplierNameSelectSelect();" Width="295px" MaxLength="50" onkeydown="return EnterTab(event,'txtPurchaseOrderNo');"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Select a Supplier" ControlToValidate="txtSupplierName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender59" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator32">
                                            </cc:ValidatorCalloutExtender>

                                        </li>


                                        <li>

                                            <asp:Label ID="Label56" runat="server" CssClass="label" Width="80px" Text="Pur. Order"></asp:Label>
                                            <asp:TextBox ID="txtPurchaseOrderNo" TabIndex="2" runat="server" AutoPostBack="false" CssClass="textbox" onkeydown="return EnterTab(event,'txtGSTIN');" onKeypress="CheckInteger(event);" Width="295px" MaxLength="12"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnPurchaseOrder" runat="server" OnClick="imgbtnPurchaseOrder_Click" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute;" ValidationGroup="PopupSupplier" />

                                            <asp:TextBox ID="txtPurchaseOrder" runat="server" Width="80px" CssClass="textbox" Style="margin-left: 30px; display: none;"></asp:TextBox>
                                            <asp:Button ID="btnLoadPurchaseOrder" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnLoadPurchaseOrder_Click" />

                                        </li>
                                        <li id="liPO" style="display: none;" runat="server">
                                            <asp:Label ID="Label17" runat="server" CssClass="label" Text="Purchase order"></asp:Label>
                                            <asp:DropDownList ID="ddlPurchaseOrder" runat="server" OnSelectedIndexChanged="ddlPurchaseOrder_SelectedIndexChanged" CssClass="dropdown"></asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label74" runat="server" CssClass="label" Width="80px" Text="GSTIN"></asp:Label>
                                            <asp:TextBox ID="txtGSTIN" CssClass="textbox" Width="295px" runat="server" TabIndex="3" onkeydown="return EnterTab(event,'txtInvoiceNo');"></asp:TextBox>
                                        </li>
                                        <li id="liRefNo" runat="server" style="display: none;">
                                            <asp:Label ID="Label38" runat="server" CssClass="label" Width="80px" Text="Ref No."></asp:Label>
                                            <asp:Label ID="lblRefno" CssClass="label purrefno" Width="295px" runat="server" TabIndex="3"></asp:Label>
                                        </li>
                                    </div>

                                    <div class="divL" style="width: 170px;">

                                        <li>
                                            <asp:Label ID="Label18" runat="server" CssClass="label" Width="80px" Text="Invoice No"></asp:Label>
                                            <asp:TextBox runat="server" CssClass="textboxMandatory" Width="70px" ID="txtInvoiceNo" TabIndex="4" MaxLength="25" onkeydown="return EnterTab(event,'txtInvoiceDate');" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Required Invoice No" ControlToValidate="txtInvoiceNo" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                            </cc:ValidatorCalloutExtender>


                                        </li>

                                        <li>
                                            <asp:Label ID="Label19" runat="server" CssClass="label" Width="80px" Text="Inv. Date"></asp:Label>
                                            <asp:TextBox runat="server" Width="70px" CssClass="textboxMandatory" ID="txtInvoiceDate" TabIndex="5" AutoComplete="off" onkeydown="return EnterTab(event,'ddlCategory');" />
                                            <cc:CalendarExtender ID="txtInvoiceDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtInvoiceDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtInvoiceDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                            </cc:ValidatorCalloutExtender>
                                        </li>

                                        <li class="clear"></li>
                                        <li class="clear"></li>


                                    </div>

                                    <div style="float: left; width: 330px;">


                                        <li>
                                            <asp:Label ID="Label3" runat="server" CssClass="label" Width="80" Text="Store"></asp:Label>
                                            <asp:DropDownList ID="ddlStore" runat="server" TabIndex="6" Width="245px" Onblur="btnStoreMaterialRecieptItems();" CssClass="dropdownMandatory"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select a Store" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:Button ID="btnStoreMaterialRecieptSelect" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnStoreMaterialRecieptSelect_Click" />

                                        </li>
                                        <li>
                                            <div class="rbtn" style="display: none">
                                                <asp:Label ID="Label10" runat="server" CssClass="label" Width="80px" Text="Inter State Purchase"></asp:Label>
                                                <asp:CheckBox ID="chkInterState" runat="server" AutoPostBack="true" OnCheckedChanged="chkInterState_CheckedChanged" Style="width: 100px; padding-right: 50px;" TabIndex="7" />
                                            </div>
                                        </li>
                                    </div>
                                    <li class="clear"></li>
                                    <div class="divSeparation"></div>
                                    <li class="clear"></li>
                                    <div class="divL" style="width: 370px;">

                                        <li>
                                            <asp:Label ID="Label23" runat="server" CssClass="label" Width="80" Text="Category"></asp:Label>
                                            <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="8" CssClass="dropdown"   onkeydown="return EnterTab(event,'txtItemShortName');"></asp:DropDownList>

                                        </li>

                                        <li>
                                            <asp:Label ID="Label1" runat="server" Width="80px" CssClass="label" Text="Item"></asp:Label>
                                            <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtItemShortName" onBlur="popUpStockSelect();" Width="182px" MaxLength="150" TabIndex="9" onkeydown="return EnterTab(event,'ddlUnit');" />
                                            <asp:Button ID="btnItem" runat="server" Text="Item" OnClick="btnItem_Click" Style="display: none;" />
                                            <asp:ImageButton ID="imgPopUp" runat="server" OnClick="imgPopUp_Click" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute" />
                                            <%--OnClientClick="return  popUpStockSelectClick();"--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Item Required " ControlToValidate="txtItemShortName" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender117" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                                            </cc:ValidatorCalloutExtender>
                                        </li>
                                        <asp:Label ID="lblQuotation" runat="server"></asp:Label>
                                        <li style="display: none;">
                                            <asp:Label ID="Label6" runat="server" Width="80px" CssClass="label" Text="Item"></asp:Label>
                                            <asp:DropDownList ID="ddlItem" runat="server" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdownMandatory"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select an Item" ControlToValidate="ddlItem" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                            </cc:ValidatorCalloutExtender>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label16" runat="server" CssClass="label" Width="80px" Text="Unit"></asp:Label>
                                            <asp:DropDownList ID="ddlUnit" runat="server" TabIndex="10" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdownMandatory" onkeydown="return EnterTab(event,'ddlSubunit');"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ErrorMessage="Select a Unit" ControlToValidate="ddlUnit" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender61" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator61">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:Button ID="btnHiddenActualQuantityfromUnit" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnHiddenActualQuantityfromUnit_Click" />
                                        </li>
                                        <li>
                                            <asp:Label ID="Label55" runat="server" CssClass="label" Width="80px" Text="Subunit"></asp:Label>
                                            <asp:DropDownList ID="ddlSubunit" OnSelectedIndexChanged="ddlSubunit_SelectedIndexChanged" runat="server" TabIndex="11" AutoPostBack="true" CssClass="dropdown" onkeydown="return EnterTab(event,'txtBatch');"></asp:DropDownList>


                                        </li>
                                        <li>
                                            <asp:Label ID="Label7" runat="server" CssClass="label" Width="80px" Text="Batch"></asp:Label>
                                            <asp:TextBox runat="server" CssClass="textboxMandatory" TabIndex="12" Width="215px" ID="txtBatch" MaxLength="20" onkeydown="return EnterTab(event,'txtExpiryDate');" />


                                            <asp:RequiredFieldValidator ID="Reqbatc" runat="server" ErrorMessage="Enter Batch name" ControlToValidate="txtBatch" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="Reqbatc">
                                            </cc:ValidatorCalloutExtender>


                                        </li>
                                        <li>
                                            <asp:Label ID="Label11" runat="server" Width="80px" CssClass="label" Text="Expiry Date"></asp:Label>

                                            <%-- <asp:TextBox ID="txtExpiryDate" onkeypress="return Validate(event);" CssClass="textboxMandatory" Width="215px" TabIndex="13"  runat="server" Height="24px" onkeydown="return EnterTab(event,'txtHSNSAC');"></asp:TextBox>
                                            <cc:CalendarExtender ID="txtExpiryDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtExpiryDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="reqDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtExpiryDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" Enabled="True" TargetControlID="reqDate">
                                            </cc:ValidatorCalloutExtender>--%>

                                            <asp:TextBox ID="txtExpiryDate" onkeydown="EnterTab(event,'txtHSNSAC');" CssClass="textboxMandatory" Width="215px" TabIndex="13" runat="server"></asp:TextBox>
                                            <cc:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtExpiryDate"
                                                Mask="99/99/9999"
                                                MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError"
                                                MaskType="Date"
                                                InputDirection="RightToLeft"
                                                AcceptNegative="None"
                                                DisplayMoney="None"
                                                AutoComplete="true"
                                                AutoCompleteValue="Date"
                                                runat="server">
                                            </cc:MaskedEditExtender>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator178" runat="server" ValidationGroup="add" Display="None" SetFocusOnError="True"
                                                ErrorMessage="Please enter date in dd/mm/yyyy format" ForeColor="Red" Font-Bold="true" ControlToValidate="txtExpiryDate"
                                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/\d{4}"></asp:RegularExpressionValidator>

                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender58" runat="server" Enabled="True" TargetControlID="RegularExpressionValidator178">
                                            </cc:ValidatorCalloutExtender>
                                            <%-- <asp:RequiredFieldValidator ID="reqDate" runat="server" ErrorMessage="Date requiredwww" ControlToValidate="txtExpiryDate" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" Enabled="True" TargetControlID="reqDate">
                                            </cc:ValidatorCalloutExtender>--%>

                                        </li>

                                        <li>
                                            <asp:Label ID="Label75" Text="HSN/SAC" CssClass="label" Width="80px" runat="server" />
                                            <asp:TextBox runat="server" ID="txtHSNSAC" TabIndex="14" Width="218px" CssClass="textbox" MaxLength="25" onkeydown="return EnterTab(event,'txtQuantity');" />
                                        </li>
                                        <br />
                                        <br />
                                        <li class="clear"></li>


                                    </div>
                                    <div class="divL" style="width: 160px;">

                                        <li>
                                            <asp:Label ID="Label9" runat="server" CssClass="label" Width="70px" Text="Quantity"></asp:Label>
                                            <asp:TextBox ID="txtQuantity" CssClass="textboxMandatory" Width="70px" runat="server" TabIndex="15" MaxLength="10" Onkeyup="calculateActualQuantity();" onblur=" getpurchaseAmountOrPer('Amount');LoadQuotationDetails();" onKeypress="checkIntOrDecimal(event,this);" Style="text-align: right;" onkeydown="return EnterTab(event,'txtFree');"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>

                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:Button ID="btnHiddenActulaQuantity" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnHiddenActulaQuantity_Click" />



                                        </li>
                                        <li>
                                            <asp:Button ID="btnQuation" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnQuation_Click" />
                                        </li>
                                        <li>
                                            <asp:Button ID="btnHiddenActullessQuantity" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnHiddenActullessQuantity_Click" />
                                        </li>
                                        <li style="display: none;">

                                            <asp:Label ID="Label52" runat="server" CssClass="label" Text="CheckActQty"></asp:Label>
                                            <asp:TextBox ID="txtCheckActQuantity" CssClass="textboxMandatory" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                        </li>
                                        <li>
                                            <asp:Label ID="Label30" runat="server" CssClass="label" Width="70px" Text="Free"></asp:Label>
                                            <asp:TextBox ID="txtFree" CssClass="textbox" runat="server" Width="70px" MaxLength="11" TabIndex="16" Style="text-align: right;" OnkeyUp="calculateActualQuantity();" onKeypress="checkIntOrDecimal(event,this);" onkeydown="return EnterTab(event,'txtActqty');"></asp:TextBox>

                                        </li>

                                        <li>
                                            <asp:Label ID="Label29" runat="server" CssClass="label" Width="70px" Text="Act Qty"></asp:Label>
                                            <asp:TextBox ID="txtActqty" CssClass="textboxMandatory" Width="70px" runat="server" TabIndex="17" MaxLength="11" Style="text-align: right;" onKeypress="checkIntOrDecimal(event);" onkeydown="return EnterTab(event,'txtPurchaseRate');"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtActqty" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0" Type="Double"></asp:RangeValidator>

                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="RangeValidator3">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Actual Quantity required" ControlToValidate="txtActqty" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator15">
                                            </cc:ValidatorCalloutExtender>

                                            <asp:Button ID="btnHiddenlessQuantity" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnHiddenlessQuantity_Click" />

                                        </li>
                                        <li>
                                            <asp:Label ID="Label22" runat="server" Width="70px" CssClass="label" Text="Pur Rate"></asp:Label>
                                            <asp:TextBox ID="txtPurchaseRate" AutoComplete="off" Width="70px" CssClass="textboxMandatory" runat="server" TabIndex="18" MaxLength="10" Style="text-align: right;" onKeypress="checkNumAndDecimal(event,this) ;" onkeydown="return EnterTab(event,'chkInctax');" onblur="getpurchaseAmountOrPer('Amount'); getSalesPrice(0);"></asp:TextBox>


                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPurchaseRate" ErrorMessage="Enter a valid PurchaseRate" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                            <cc:ValidatorCalloutExtender ID="ValtxidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                            </cc:ValidatorCalloutExtender>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="PurchaseRate required" ControlToValidate="txtPurchaseRate" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                            </cc:ValidatorCalloutExtender>
                                        </li>

                                        <li>

                                            <asp:Label ID="Label32" runat="server" Width="80px" CssClass="label" Text="Include Tax"></asp:Label>
                                            <asp:CheckBox ID="chkInctax" onclick="getpurchaseAmountOrPer('Amount');getSalesPrice(0);" runat="server" Width="15px" TabIndex="19" onkeydown="return EnterTab(event,'chkDiscounttax');" />
                                            <asp:Button Text="PTax" ToolTip="PurchaseTax" ID="btnS" Style="padding: 0px; width: 50px; height: 18px; margin: 9px 0px 0px 5px; position: absolute" CssClass="button" OnClick="btnS_Click" runat="server" />
                                        </li>

                                        <li>
                                            <asp:Label ID="Label31" runat="server" CssClass="label" Width="70px" Text="Tax Amount"></asp:Label>
                                            <asp:TextBox ID="txttaxAmount" CssClass="textbox" TabIndex="20" Width="70px" runat="server" MaxLength="11" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);" onkeydown="return EnterTab(event,'chkDiscounttax');"></asp:TextBox>

                                        </li>
                                        <li>
                                            <div class="rbtn">
                                                <asp:Label ID="Label53" runat="server" CssClass="label" Text="Discount On Tax"></asp:Label>
                                                <asp:CheckBox ID="chkDiscounttax" onclick="getpurchaseAmountOrPer('Amount');" runat="server" Style="width: 70px;" TabIndex="21" onkeydown="return EnterTab(event,'txtPurDiscPer');" />


                                            </div>
                                        </li>
                                    </div>



                                    <div style="float: left; width: 170px;">




                                        <li>
                                            <asp:Label ID="Label62" runat="server" CssClass="label" Width="80px" Text="Pur Disc%"></asp:Label>
                                            <asp:TextBox ID="txtPurDiscPer" CssClass="textbox" TabIndex="22" Width="70px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="checkNumAndDecimal(event,this) ; " onblur="getpurchaseAmountOrPer('Amount');" onkeydown="return EnterTab(event,'txtPurDiscAmnt');"></asp:TextBox>

                                        </li>

                                        <li>
                                            <asp:Label ID="Label63" runat="server" CssClass="label" Width="80px" Text="Pur DiscAmt"></asp:Label>
                                            <asp:TextBox ID="txtPurDiscAmnt" CssClass="textbox" TabIndex="23" Width="70px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="checkNumAndDecimal(event,this) ;" onblur="getpurchaseAmountOrPer('Perc');" onkeydown="return EnterTab(event,'txtMRP');"></asp:TextBox>

                                        </li>
                                        <li>
                                            <asp:Label ID="Label65" runat="server" CssClass="label" Width="80px" Text="Net Pur"></asp:Label>
                                            <asp:TextBox ID="txtNetPur" CssClass="textboxMandatory" TabIndex="24" Width="70px"  runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);" onkeydown="return EnterTab(event,'txtMRP');"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="NetPurchase required" ControlToValidate="txtNetPur" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator30">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RangeValidator ID="RangeValidator9" runat="server" ControlToValidate="txtNetPur" ErrorMessage="Enter a valid NetPurchase" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender53" runat="server" Enabled="True" TargetControlID="RangeValidator9">
                                            </cc:ValidatorCalloutExtender>
                                        </li>


                                        <li>
                                            <asp:Label ID="Label15" runat="server" CssClass="label" Width="80px" Text="MRP"></asp:Label>
                                            <asp:TextBox ID="txtMRP" CssClass="textboxMandatory" TabIndex="25" Width="70px" runat="server" MaxLength="11" Style="text-align: right;" onblur="getSalesPrice(0);TaxAmount();" onKeypress="checkNumAndDecimal(event,this) ;" onkeydown="return EnterTab(event,'txtProfitPer');">

                                            </asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label25" runat="server" CssClass="label" Width="80px" Text="Profit %"></asp:Label>
                                            <asp:TextBox ID="txtProfitPer" CssClass="textboxMandatory" Width="70px" TabIndex="26" runat="server" MaxLength="11" Style="text-align: right;" onblur="getSalesPrice(1);TaxAmount(); " onKeypress="checkNumAndDecimal(event,this) ;" onkeydown="return EnterTab(event,'txtDiscountPer');"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Profit % required" ControlToValidate="txtProfitPer" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender25" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator13">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Profit % required" ControlToValidate="txtProfitPer" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender33" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator20">
                                            </cc:ValidatorCalloutExtender>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label66" runat="server" CssClass="label" Width="80px" Text="Profit Amt"></asp:Label>
                                            <asp:TextBox ID="txtProfitAmount"  CssClass="textbox" TabIndex="27" Width="70px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="checkNumAndDecimal(event,this) ;" Onblur="getSalesPrice(3);TaxAmount();" onkeydown="return EnterTab(event,'txtDiscountPer');"></asp:TextBox>

                                        </li>
                                    </div>
                                    <div style="float: left; width: 170px;">
                                        <li>
                                            <asp:Label ID="Label12" runat="server" CssClass="label" Width="80px" Text="Discount %"></asp:Label>
                                            <asp:TextBox ID="txtDiscountPer" CssClass="textbox" TabIndex="28" Width="70px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="checkNumAndDecimal(event,this) ;" onblur="getSalesPrice(2);TaxAmount();" onkeydown="return EnterTab(event,'txtDiscountAmt');"></asp:TextBox>

                                        </li>
                                        <li>
                                            <asp:Label ID="Label24" runat="server" CssClass="label" Width="80px" Text="Disc Amt" Height="16px"></asp:Label>
                                            <asp:TextBox ID="txtDiscountAmt" CssClass="textbox" Width="70px" runat="server" TabIndex="29" MaxLength="11" Style="text-align: right;" onKeypress="checkNumAndDecimal(event,this) ;" onblur="getSalesPrice(4);TaxAmount();" onkeydown="return EnterTab(event,'ChkIncStax');"></asp:TextBox>

                                        </li>
                                        <li>

                                            <asp:Label ID="Label4" runat="server" Width="90px" CssClass="label" Text="Include STax"></asp:Label>
                                            <asp:CheckBox ID="ChkIncStax" runat="server" Width="15px" onclick="TaxAmount();" TabIndex="30" onkeydown="return EnterTab(event,'txtSalesPrice');" />
                                            <asp:Button Text="STax" ToolTip="SalesTax" ID="btSubmit" Style="padding: 0px; width: 50px; height: 18px; margin: 9px 0px 0px 6px; position: absolute" CssClass="button" OnClick="btSubmit_Click" runat="server" />
                                        </li>


                                        <li>
                                            <asp:Label ID="Label5" runat="server" CssClass="label" Width="80px" Text="Tax Amount"></asp:Label>
                                            <asp:TextBox ID="txtSTaxAmount"   CssClass="textbox" TabIndex="30" Width="70px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);" onkeydown="return EnterTab(event,'txtSalesPrice');"></asp:TextBox>

                                        </li>

                                        <li>
                                            <asp:Label ID="Label27" runat="server" CssClass="label" Width="80px" Text="Sales Price"></asp:Label>
                                            <asp:TextBox ID="txtSalesPrice" CssClass="textboxMandatory" Width="70px" TabIndex="31" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="checkNumAndDecimal(event,this) ;" onkeydown="return EnterTab(event,'btnAdd');" onblur="getSalesPrice(5);"></asp:TextBox>
                                            <%-- <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtSalesPrice" SetFocusOnError="true" Type="Double" ControlToCompare="txtPurchaseRate" ValidationGroup="add" Display="None" ErrorMessage="Sales Price rate Should not be less  than  Purchase Rate" Operator="GreaterThanEqual"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" Enabled="True" TargetControlID="CompareValidator2">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtSalesPrice" SetFocusOnError="true" Type="Double" ControlToCompare="txtMRP" ValidationGroup="add" Display="None" ErrorMessage="Sales Price rate Should not  be greater than MRP" Operator="LessThanEqual"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" Enabled="True" TargetControlID="CompareValidator5">
                                            </cc:ValidatorCalloutExtender>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Sales Price required" ControlToValidate="txtSalesPrice" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator19">
                                            </cc:ValidatorCalloutExtender>
                                        </li>
                                    </div>

                                    <li class="clear"></li>
                                    <div id="divEcelFormat" runat="server" style="display: none; background-color: #FFD6CA; position: relative; width: 50%; height: 100px; margin-left: 188px; margin-top: -796px; box-shadow: 0px 0px 15px #888;">
                                        <div style="left: 0px; margin-left: 7px; height: 30px; background-color: #2b737b;">
                                            <asp:Label ID="Label79" CssClass="label" runat="server" Style="width: 134%; margin-right: 0px; font-family: 'Segoe Print'; font-weight: bold; color: black; background-color: #2b737b; height: 22px; padding-top: 3px; text-align: left; margin-top: -116px"></asp:Label>
                                            <asp:ImageButton ID="ibtnExcelClose" runat="server" Width="4%" Height="64%" TabIndex="18"
                                                Style="float: right; margin-top: -118px; margin-right: -35%;" ToolTip="Close" ImageUrl="~/images/cross.png"
                                                OnClientClick="HideDivOnButtonClick();return false;" />
                                        </div>
                                        <div style="left: 0px; margin-left: 7px;">
                                            <asp:Label ID="Label80" CssClass="label" runat="server" Text="Excel Document Should Contain  
                        ItemCode,Qty,Free,MRP,PurchaseRate,Unit,Subunit,BatchNo, HSN,Category,SalesPrice,IncludePtax,
                        DiscountOntax,IncludeStax
                        ExpiryDate (dd/mm/yyyy)"
                                                Style="width: 134%; margin-right: 0px; color: white; font-family: 'Tahoma'; background-color: #2b737b; font-size: 15px; height: 62px; padding-top: 3px; text-align: left; margin-top: -91px"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="width: 50%; float: left">
                                        <cc:AsyncFileUpload ID="FileUploadExcel" Style="float: left; width: 40%; margin-left: 3%;" TabIndex="41" runat="server" CssClass="imageUploaderField" OnUploadedComplete="FileUploadExcel_UploadedComplete" OnClientUploadStarted="uploadStartedabdc" />

                                        <asp:Button ID="btnExcelFormat" runat="server" Text="?" TabIndex="42" ToolTip="Excel Format" Style="width: 35px;" OnClientClick="ShowDivOnButtonClick();return false;" />
                                        <asp:Button ID="btnShowData" runat="server" OnClick="btnShowData_Click" CssClass="button"
                                            TabIndex="43" Text="Show Data" UseSubmitBehavior="False" Width="85px" />
                                    </div>
                                    <div class="divL" style="width: 370px; padding-right: 0px;">
                                        <li style="margin-left: 10px">
                                            <asp:Button Text="Insert" ID="btnAdd" runat="server" OnClick="btnAdd_Click" TabIndex="32" ValidationGroup="add" CssClass="button" />
                                            <asp:Button Text="Clear" ID="btnClearAdd" runat="server" CssClass="button" TabIndex="33" OnClick="btnClearAdd_Click" OnClientClick="ItemClear();" />
                                            <asp:Button Text="Previous Purchase" ID="btnPurchase" OnClientClick="return  popUpPreviousPurchaseSelect();" runat="server" CssClass="button" Width="150px" OnClick="btnPurchase_Click" />


                                        </li>
                                    </div>
                                    <li class="clear"></li>
                                    <li class="clear"></li>
                                    <div class="divSeparation"></div>


                                    <div style="width: 940px; overflow: auto; height: 150px; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">
                                        <%--  <div style="width: 930px; overflow: auto; height: 100px; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">--%>

                                        <li>



                                            <asp:GridView ID="gvItems" runat="server" CssClass="grid" OnRowDataBound="gvItems_RowDataBound" Style="margin-left: 5px;"
                                                EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="920px"
                                                BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items"
                                                PageSize="100" OnSelectedIndexChanging="gvItems_SelectedIndexChanging" OnRowDeleting="gvItems_RowDeleting">
                                                <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                                <Columns>



                                                    <%--<asp:BoundField HeaderText="Net Amount" DataField="NetAmount" ItemStyle-HorizontalAlign="Right" />--%>


                                                    <%-- <asp:TemplateField HeaderText="Actual Purchase Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvActualPurRate" runat="server" Text='<%# Bind("ActualPurRate") %>' Style="text-align: right; float: right;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                        <ItemTemplate>

                                                            <asp:ImageButton ID="ImageButton1q2" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Edit_bg1.png" Width="20px" Height="20px" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton232" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sl.No" ItemStyle-HorizontalAlign="Center">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox21" runat="server" Text='<%# Bind("SLNO") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSLNO" runat="server" Text='<%# Bind("SLNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox41" runat="server" Text='<%# Bind("Item") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <div style="overflow: auto; width: 250px;">
                                                                <asp:Label ID="lblgvItem" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Batch">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox493" runat="server" Text='<%# Bind("BatchNo") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <div style="overflow: auto; width: 120px;">
                                                                <asp:Label ID="lblgvBatchNo" runat="server" Text='<%# Bind("BatchNo") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Exp.Date">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox66" runat="server" Text='<%# Bind("ExpiryDate") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <div style="width: 80px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">

                                                                <asp:Label ID="lblgvExpiryDate" runat="server" Text='<%# Bind("ExpiryDate") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HSN/SAC">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtHSn" runat="server" Text='<%# Bind("HSN") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <div style="width: 80px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">

                                                                <asp:Label ID="lblgvhsn" runat="server" Text='<%# Bind("HSN") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Qty">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox67" Style="width: 20px; text-align: right;" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblgvquty" runat="server" Text='<%# Bind("Quantity") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Free">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgvfree" runat="server" Text='<%# Bind("Free") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvFree" runat="server" Text='<%# Bind("Free") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Act.Qty">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgvactqty" runat="server" Text='<%# Bind("ActQty") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActQty" runat="server" Text='<%# Bind("ActQty") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pur.Rate">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox68" runat="server" Text='<%# Bind("PurchaseRate") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPurchaseRate" runat="server" Text='<%# Bind("PurchaseRate") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Purch.TaxAmnt">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtPurchasetax" runat="server" Text='<%# Bind("PurchasetaxAmount") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPurchaseTax" runat="server" Text='<%# Bind("PurchasetaxAmount") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Pur.Disc%">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgvPurDiscPer" runat="server" Text='<%# Bind("PurchaseDiscPercentage") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPurDiscPer" runat="server" Text='<%# Bind("PurchaseDiscPercentage") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Purch.Disc.Amnt">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgvPurchaseDiscAmount" runat="server" Text='<%# Bind("PurchaseDiscAmount") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPurchaseDiscAmount" runat="server" Text='<%# Bind("PurchaseDiscAmount") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Net.Purch">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgvNetPurchase" runat="server" Text='<%# Bind("NetPurchase") %>' Style="text-align: right;"></asp:TextBox>

                                                            />
                                                        </EditItemTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvNetPurchase" runat="server" Text='<%# Bind("NetPurchase") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRP">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgvMRP" runat="server" Text='<%# Bind("MRP") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvMRP" runat="server" Text='<%# Bind("MRP") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Profit % ">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgvProfitPer" runat="server" Text='<%# Bind("ProfitPer") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvProfitPer" runat="server" Text='<%# Bind("ProfitPer") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Profit Amount">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgvProfitAmount" runat="server" Text='<%# Bind("ProfitAmount") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvProfitAmount" runat="server" Text='<%# Bind("ProfitAmount") %>' Style="text-align: right; float: right"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Disc %">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgvDiscountPer" runat="server" Text='<%# Bind("DiscountPer") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDiscountPer" runat="server" Text='<%# Bind("DiscountPer") %>' Style="text-align: right; float: right"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Disc.Amt">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgvDiscountAmt" runat="server" Text='<%# Bind("DiscountAmt") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDiscountAmt" runat="server" Text='<%# Bind("DiscountAmt") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Tax Amount">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgvSalestaxAmountg" runat="server" Text='<%# Bind("SaletaxAmount") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSalesTaxAmt" runat="server" Text='<%# Bind("SaletaxAmount") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Price">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgvSalesPrice" runat="server" Text='<%# Bind("SalePrice") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSalesPrice" runat="server" Text='<%# Bind("SalePrice") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Category">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtCategory" runat="server" Text='<%# Bind("Category") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <div style="overflow: auto; width: 250px;">
                                                                <asp:Label ID="lblgvCategory" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unit">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox45513" runat="server" Text='<%# Bind("Units") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <div style="overflow: auto; width: 60px;">
                                                                <asp:Label ID="lblgvUnits" runat="server" Text='<%# Bind("Units") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Subunit">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtSub" runat="server" Text='<%# Bind("SubUnit") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <div style="overflow: auto; width: 60px;">
                                                                <asp:Label ID="lblgvSubUnits" runat="server" Text='<%# Bind("SubUnit") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="FK_Item" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox4322" runat="server" Text='<%# Bind("FK_Items") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvFK_Items" runat="server" Text='<%# Bind("FK_Items") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="FK_Unit" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox43229" runat="server" Text='<%# Bind("FK_Units") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvFK_Units" runat="server" Text='<%# Bind("FK_Units") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FK_SubUnit" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtSubunit" runat="server" Text='<%# Bind("FK_SubUnit") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvFK_SubUnit" runat="server" Text='<%# Bind("FK_SubUnit") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FK_Category" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtFK_Category" runat="server" Text='<%# Bind("FK_Category") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvFK_Category" runat="server" Text='<%# Bind("FK_Category") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IncludePtax" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtIncludeptax" runat="server" Text='<%# Bind("IncludePtax") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvIncludeptax" runat="server" Text='<%# Bind("IncludePtax") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IncludeStax" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtIncludeStax" runat="server" Text='<%# Bind("IncludeStax") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvIncludeStax" runat="server" Text='<%# Bind("IncludeStax") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DiscountOnTax" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtDiscountOntax" runat="server" Text='<%# Bind("DiscountOnTax") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDiscountOntax" runat="server" Text='<%# Bind("DiscountOnTax") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Quantity Purchase" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtqtyhidden" runat="server" Text='<%# Bind("Quantity") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgdsaavquty" runat="server" Text='<%# Bind("Quantity") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Actual Purchase Rate" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox70" runat="server" Text='<%# Bind("ActualPurRate") %>' Style="text-align: right;"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActualPurRate" runat="server" Text='<%# Bind("ActualPurRate") %>' Style="text-align: right; float: right;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="30px" />
                                                <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                            </asp:GridView>
                                        </li>
                                    </div>
                                    <div class="divSeparation"></div>
                                    <li class="clear"></li>
                                    <div class="divL" style="width: 140px;">

                                        <li>
                                            <asp:Label ID="Label69" runat="server" CssClass="label" Width="60px" Text="Net.Purch"></asp:Label>
                                            <asp:TextBox ID="txtTotNetPur" TabIndex="34" CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;" OnTextChanged="txtTotalAmount_TextChanged" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                        </li>
                                    </div>

                                    <%--<div class="divL" style="width: 140px;">
                                        <li>
                                            <asp:Label ID="Label8" runat="server" CssClass="label" Width="60px" Text="Tot.Amt"></asp:Label>
                                            <asp:TextBox ID="txtTotalAmount" TabIndex="35" CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;" OnTextChanged="txtTotalAmount_TextChanged" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                        </li>
                                    </div>--%>
                                    <div class="divL" style="width: 140px; display: none">
                                        <li>
                                            <asp:Label ID="Label8" runat="server" CssClass="label" Width="60px" Text="Tot.Amt"></asp:Label>
                                            <asp:TextBox ID="txtTotalAmount" TabIndex="35" CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                        </li>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="divL" style="width: 200px;">
                                                <li>
                                                    <asp:Label ID="Label33" runat="server" CssClass="label" Width="80px" Text="Oth.Charges"></asp:Label>
                                                    <asp:TextBox ID="txtOther" TabIndex="35" onkeydown="EnterTab(event,'txtRoundOff');" CssClass="textbox" Width="100px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;"></asp:TextBox>

                                                </li>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div style="float: left; width: 140px;">


                                        <li>
                                            <asp:Label ID="Label13" runat="server" Width="60px" CssClass="label" Text="Rd.Off"></asp:Label>
                                            <asp:TextBox ID="txtRoundOff" TabIndex="36" CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;"></asp:TextBox>
                                        </li>


                                    </div>
                                    <asp:UpdatePanel ID="udY" runat="server">
                                        <ContentTemplate>
                                            <div style="float: left; width: 140px;">
                                                <li>
                                                    <asp:Label ID="Label37" runat="server" Width="60px" CssClass="label" Text="Net.Amt"></asp:Label>
                                                    <asp:TextBox ID="txtBillNetAmount" TabIndex="37" CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                                </li>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                            </div>
                            <li class="clear"></li>
                            <li class="btnMargin" style="padding-left: 85px">
                                <asp:HiddenField ID="hfMaterialRecieptId" runat="server" />
                                <asp:HiddenField ID="hfClient" runat="server" />
                                <asp:HiddenField ID="hfDate" runat="server" />
                                <asp:HiddenField ID="hfUser" runat="server" />
                                <asp:HiddenField ID="hfIsAdd" runat="server" />
                                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                <asp:HiddenField ID="hfIsDelete" runat="server" />
                                <asp:HiddenField ID="hfItemId" runat="server" />
                                <asp:HiddenField ID="hfPacked" runat="server" />
                                <asp:HiddenField ID="hfID_Purchase" runat="server" />
                                <asp:HiddenField ID="hfApproveOk" runat="server" />
                                <asp:HiddenField ID="hfFk_Store" runat="server" />
                                <asp:HiddenField ID="hfUnitQuantity" runat="server" />
                                <asp:HiddenField ID="hfSlNo" runat="server" />
                                <asp:HiddenField ID="hfDiscSettings" runat="server" Value="0" />
                                <asp:HiddenField ID="hfDiscPer" runat="server" />
                                <asp:HiddenField ID="hfpurrate" runat="server" />
                                <asp:HiddenField ID="hfTaxValue" runat="server" />
                                <asp:HiddenField ID="hfTotTaxPercentage" runat="server" />
                                <asp:HiddenField ID="hfSalesValue" runat="server" />
                                <asp:HiddenField ID="hfTotTaxsalesPercentage" runat="server" />
                                <asp:HiddenField ID="hfPurchaseValue" runat="server" />
                                <asp:HiddenField ID="Hfkeyvalue" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hfuserName" runat="server" />
                                <asp:HiddenField ID="hfMasterID" runat="server" />
                                <asp:HiddenField ID="hfExpirydays" runat="server" />
                                <asp:HiddenField ID="hfSubUnitQuantity" runat="server" />
                                <asp:HiddenField ID="hfGvItemSelected" runat="server" />
                                <asp:HiddenField ID="hfQPurchaseRate" runat="server" />
                                <asp:HiddenField ID="hfQFree" runat="server" />
                                <asp:HiddenField ID="hfQDiscper" runat="server" />
                                <asp:HiddenField ID="hfQDisAmt" runat="server" />
                                <asp:HiddenField ID="hfExpenseAmount" runat="server" />
                                <asp:HiddenField ID="hfBoolValue" runat="server" />
                                <asp:HiddenField ID="hfNetAmount" runat="server" />
                                <asp:HiddenField ID="hfTotalPercentage" runat="server" />
                                <asp:HiddenField ID="hfItemvalue" runat="server" />
                                <asp:HiddenField ID="HfItemsID" runat="server" />
                                <asp:HiddenField ID="hfItem" runat="server" />
                                <asp:HiddenField ID="hfSingleQty" runat="server" />
                                <asp:Button ID="btnSubmit" runat="server" CssClass="button" TabIndex="38" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="submit" />
                                <asp:Button ID="btnClear" Text="Clear" runat="server" TabIndex="39" OnClick="btnClear_Click" OnClientClick="ClearAdd(); " CausesValidation="false" CssClass="button" />
                                <asp:Button ID="btnApprove" runat="server" CssClass="button" TabIndex="40" Text="Approve" ValidationGroup="submit" OnClick="btnApprove_Click" Width="89px" />
                                <asp:Button ID="btnOtherCharges" runat="server" Width="150px" CssClass="button" TabIndex="40" Text="Other Charges" OnClick="btnOtherCharges_Click" />

                            </li>
                        </div>
                        <div class="clear"></div>
                        <div>
                            <cc:ModalPopupExtender ID="MpeApprove" runat="server" BackgroundCssClass="modalBackground" PopupControlID="DivPurchaseApprove" TargetControlID="btnmppChangePriceRemoveItem" CancelControlID="btnmppCancel" Drag="True" Enabled="True"></cc:ModalPopupExtender>
                            <div id="DivPurchaseApprove" class="DivStyle1" style="display: none; background-color: teal; height: 460px; width: 850px;">
                                <div style="width: 100%; text-align: center; height: 60px;">

                                    <li style="text-align: center; padding-top: 20px;">
                                        <asp:Label ID="Label14" runat="server" CssClass="popuplblCentre" Text="Purchase Approve" Font-Underline="true" Font-Bold="true" ForeColor="White" Font-Size="Large"></asp:Label>
                                    </li>

                                </div>
                                <div class="divL" style="float: left; width: 450px;">
                                    <li>
                                        <asp:Label ID="Label34" runat="server" CssClass="Plabel" Text="Bill Date"></asp:Label>
                                        <asp:TextBox ID="txtpopupBillDate" CssClass="textboxMandatory"  AutoComplete="off"  TabIndex="44" runat="server" Width="90px"></asp:TextBox>
                                        <asp:CheckBox ID="chkCredit" Style="padding-left: 0px" runat="server" Text="Credit" AutoPostBack="true" OnCheckedChanged="chkCredit_CheckedChanged" ForeColor="White" Font-Size="15px" TabIndex="9" />
                                        <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtpopupBillDate">
                                        </cc:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="Bill Date required" ControlToValidate="txtpopupBillDate" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender38" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator24">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="txtpopupBillDate" SetFocusOnError="true" ControlToCompare="txtInvoiceDate" Type="Date" ValidationGroup="Approve" Display="None" ErrorMessage="Approve date should not be less than InvoiceDate" Operator="GreaterThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender26" runat="server" Enabled="True" TargetControlID="CompareValidator12">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li>
                                        <asp:Label ID="Label26" runat="server" CssClass="Plabel" Text="Bill Type"></asp:Label>
                                        <asp:DropDownList ID="ddlpopupbillType" Width="175px" TabIndex="45" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpopupbillType_SelectedIndexChanged" CssClass="dropdown"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Select a BillType" ControlToValidate="ddlpopupbillType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender37" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator22">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label20" runat="server" CssClass="Plabel" Text="Bill No"></asp:Label>
                                        <asp:TextBox ID="txtpopupApproveBillNo" runat="server" CssClass="popuptxtMaterialRcpt" TabIndex="46"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label35" runat="server" CssClass="Plabel" Text="Bill Amount"></asp:Label>
                                        <asp:TextBox ID="txtpopupPurchaseAmoount" runat="server" TabIndex="47" CssClass="popuptxtMaterialRcpt" Style="text-align: right;"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtpopupPurchaseAmoount" SetFocusOnError="true" ErrorMessage="Total Discount Amount Should not Exceed the BillAmount" Display="None" EnableClientScript="true" ValidationGroup="Approve" ClientValidationFunction="ValidateCheckBox"></asp:CustomValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender41" runat="server" Enabled="True" TargetControlID="CustomValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li style="display: none;">
                                        <asp:Label ID="Label40" runat="server" CssClass="Plabel" Text="Tax"></asp:Label>
                                        <asp:TextBox ID="txtpopupTax" runat="server" CssClass="popuptxtMaterialRcpt" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label39" runat="server" CssClass="Plabel" Text="Discount %"></asp:Label>
                                        <asp:TextBox ID="txtpopupDiscountPer" runat="server" TabIndex="48" CssClass="popuptxtMaterialRcpt" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);" onkeyUp="CalculatePopDiscountAmount(); PopRoundOffCalc(); CalculatePopUpNetAmount();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Discount % required" ControlToValidate="txtpopupDiscountPer" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender40" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator25">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="txtpopupDiscountPer" ErrorMessage="Enter a valid Discount %" Display="None" MaximumValue="100" ValidationGroup="Approve" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label43" runat="server" CssClass="Plabel" Text="Dis Amount"></asp:Label>
                                        <asp:TextBox ID="txtpopupDiscountAmount" TabIndex="49" runat="server" CssClass="popuptxtMaterialRcpt" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);" onkeyUp="CalculatePopDiscountPercentage(); PopRoundOffCalc();  CalculatePopUpNetAmount();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="Discount Amount required" ControlToValidate="txtpopupDiscountAmount" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender42" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator26">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                </div>
                                <div class="divR" style="float: left; width: 400px;">

                                    <li>
                                        <asp:Label ID="Label36" runat="server" CssClass="Plabel" Text="Round off"></asp:Label>
                                        <asp:TextBox ID="txtpopupRoundOff" Width="143px" TabIndex="55" runat="server" Style="text-align: right;" CssClass="popuptxtMaterialRcpt"></asp:TextBox>
                                    </li>
                                    <li id="DivpopuppaymentMode" runat="server">
                                        <asp:Label ID="Label51" runat="server" CssClass="Plabel" Text="Payment Mode"></asp:Label>
                                        <asp:DropDownList ID="ddlPaymentMode" Width="147px" TabIndex="56" runat="server" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged"></asp:DropDownList>

                                    </li>


                                    <li id="DivpopupBank" runat="server">
                                        <asp:Label ID="Label42" runat="server" CssClass="Plabel" Text="Bank"></asp:Label>
                                        <asp:DropDownList ID="ddlBank" TabIndex="57" Width="147px" runat="server" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlBank" runat="server" ErrorMessage="Select a Bank" ControlToValidate="ddlBank" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender46" runat="server" Enabled="True" TargetControlID="reqddlBank">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li id="DivpopupBankBranch" runat="server">
                                        <asp:Label ID="Label48" runat="server" CssClass="Plabel" Text="Bank Branch"></asp:Label>
                                        <asp:DropDownList ID="ddlBankBranch" Width="147px" TabIndex="58" runat="server" CssClass="dropdown"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlbankBranch" runat="server" ErrorMessage="Select a BankBranch" ControlToValidate="ddlBankBranch" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender47" runat="server" Enabled="True" TargetControlID="reqddlbankBranch">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li id="DivpopupchequeNo" runat="server">
                                        <asp:Label ID="Label44" runat="server" CssClass="Plabel" Text="Cheque No"></asp:Label>
                                        <asp:TextBox ID="txtpopupChequeNo" runat="server" TabIndex="59" CssClass="popuptxtMaterialRcpt"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqtxtchequeNo" runat="server" ErrorMessage="Cheque No Required" ControlToValidate="txtpopupChequeNo" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender50" runat="server" Enabled="True" TargetControlID="reqtxtchequeNo">
                                        </cc:ValidatorCalloutExtender>

                                    </li>
                                    <li id="DivpopupchequeDate" runat="server">
                                        <asp:Label ID="Label45" runat="server" CssClass="Plabel" Text="Cheque Date"></asp:Label>
                                        <asp:TextBox ID="txtpopupChequeDate" TabIndex="60" runat="server" CssClass="popuptxtMaterialRcpt"></asp:TextBox>

                                        <cc:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtpopupChequeDate">
                                        </cc:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Cheque Date required" ControlToValidate="txtpopupChequeDate" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender39" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator23">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="txtpopupChequeDate" SetFocusOnError="true" ControlToCompare="txtpopupBillDate" Type="Date" ValidationGroup="Approve" Display="None" ErrorMessage="Cheque Date Should not be less than BillDate" Operator="GreaterThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender51" runat="server" Enabled="True" TargetControlID="CompareValidator11">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li id="DivpopupAccHead" runat="server">
                                        <asp:Label ID="Label49" runat="server" CssClass="Plabel" Text="Acc Head"></asp:Label>
                                        <asp:DropDownList ID="ddlAcchead" Width="147px" TabIndex="61" runat="server" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlAcchead_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlAccHead" runat="server" ErrorMessage="Select a Account Head" ControlToValidate="ddlAcchead" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender48" runat="server" Enabled="True" TargetControlID="reqddlAccHead">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li id="DivpopupAccSubHead" runat="server">
                                        <asp:Label ID="Label50" runat="server" CssClass="Plabel" Text="Acc SubHead"></asp:Label>
                                        <asp:DropDownList ID="ddlAccSubHead" Width="147px" TabIndex="62" runat="server" CssClass="dropdown"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlAccSubHead" runat="server" ErrorMessage="Select a BankBranch" ControlToValidate="ddlAccSubHead" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender49" runat="server" Enabled="True" TargetControlID="reqddlAccSubHead">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li>
                                        <asp:Label ID="Label41" runat="server" CssClass="Plabel" Text="Net Amount"></asp:Label>
                                        <asp:TextBox ID="txtpopupNetPayAmount" TabIndex="57" runat="server" CssClass="popuptxtMaterialRcpt" Style="width: 142px; margin-top: 9px"  ></asp:TextBox><%--Style="text-align: right;"--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="Cheque Date required" ControlToValidate="txtpopupNetPayAmount" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender45" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator29">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li id="DivpopupAdvance" runat="server">
                                        <asp:Label ID="Label46" runat="server" CssClass="Plabel" Text="Adv Amount"></asp:Label>
                                        <asp:TextBox ID="txtpopupAdvanceAmount" TabIndex="63" runat="server" onKeypress="CheckNumWithDecimal(event);" AutoPostBack="true" OnTextChanged="txtpopupAdvanceAmount_TextChanged" Style="text-align: right;" onKeyup="CalculatePopUpBalanceAndBillAmount();" CssClass="popuptxtMaterialRcpt"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="Cheque Date required" ControlToValidate="txtpopupAdvanceAmount" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender43" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator27">
                                        </cc:ValidatorCalloutExtender>
                                    </li>


                                    <li>
                                        <asp:Label ID="Label47" runat="server" CssClass="Plabel" Text="Balance "></asp:Label>
                                        <asp:TextBox ID="txtpopupBalanceAmount" TabIndex="58" runat="server" Style="width: 142px; margin-top: 9px"  CssClass="popuptxtMaterialRcpt"></asp:TextBox><%--Style="text-align: right;"--%>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="Cheque Date required" ControlToValidate="txtpopupBalanceAmount" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender44" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator28">
                                        </cc:ValidatorCalloutExtender>

                                    </li>
                                    <li>
                                        <asp:Label ID="Label21" runat="server" CssClass="Plabel" Text="Remarks"></asp:Label>
                                        <asp:TextBox ID="txtpopupRemarks" CssClass="multiline" Style="width: 143px; margin-top: 9px" TabIndex="59" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </li>

                                    <li class="clear"></li>
                                    <li class="clear"></li>



                                    <li class="btnMargin" style="padding-left: 175px;">
                                        <asp:ImageButton ID="btnmppPurchaseApproveDone" ImageUrl="~/images/1399986932_accepted_48.png" Width="52px" OnClick="btnmppPurchaseApproveDone_Click" Height="52px" ToolTip="Approve" runat="server" TabIndex="149" ValidationGroup="Approve" />
                                        <asp:ImageButton ID="btnmppCancel" ImageUrl="~/images/1399987015_Delete_01.png" Width="50px" Height="50px" ToolTip="Cancel" TabIndex="156" runat="server" Style="padding-left: 5px;" />

                                    </li>
                                    <li style="display: none;">
                                        <asp:TextBox ID="TextBox6" runat="server" Visible="false"></asp:TextBox>
                                        <asp:Button ID="btnmppChangePriceRemoveItem" runat="server" CssClass="button" Text="RemoveItem" />
                                    </li>

                                </div>
                                <li class="clear"></li>
                                <div style="width: 100%; float: left; padding-left: 40px;">
                                </div>
                            </div>
                        </div>


                        <div class="clear"></div>
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <div class="divL">

                                    <li>
                                        <label class="label">From Date</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtFromDateSearch" TabIndex="1" AutoComplete="off" onkeydown="return EnterTab(event,'txtToDateSearch');" />
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDateSearch" ControlToValidate="txtFromDateSearch" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="srch"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                        </cc:ValidatorCalloutExtender>

                                        <cc:CalendarExtender ID="txtDate_CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDateSearch" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>

                                    </li>

                                    <li>
                                        <label class="label">To Date</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtToDateSearch" TabIndex="2" AutoComplete="off" onkeydown="return EnterTab(event,'ddlStatusApproveSearch');" />
                                        <cc:CalendarExtender ID="txtDate_CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtToDateSearch" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>

                                    </li>

                                    <li>
                                        <label class="label">Status</label>
                                        <asp:DropDownList runat="server" CssClass="dropdown" ID="ddlStatusApproveSearch" TabIndex="3" onkeydown="return EnterTab(event,'ddlStoreSearch');">
                                            <asp:ListItem Text="select" Value="2" />
                                            <asp:ListItem Text="Approved" Value="1" />
                                            <asp:ListItem Text="Pending" Value="0" />
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label54" runat="server" CssClass="label" Text="Ref No."></asp:Label>
                                        <asp:TextBox ID="txtRefnoSrch" MaxLength="20" onkeypress="CheckInteger(event);" CssClass="textbox" runat="server" TabIndex="4"></asp:TextBox>
                                    </li>
                                    <li class="clear"></li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" TabIndex="7" CssClass="button" Text="Search" OnClick="btnSearch_Click" ValidationGroup="srch" />
                                        <asp:Button ID="btnSearchClear" runat="server" TabIndex="8" CssClass="button" Text="Clear" OnClientClick="SearchClear(); return false;" />

                                    </li>
                                </div>
                                <div class="divR">
                                    <li>
                                        <label class="label">Store</label>
                                        <asp:DropDownList ID="ddlStoreSearch" runat="server" CssClass="dropdown" TabIndex="4" onkeydown="return EnterTab(event,'txtSupplierSearch');"></asp:DropDownList>
                                    </li>
                                    <li style="display: none">
                                        <label class="label">Supplier</label>
                                        <asp:DropDownList ID="ddlSupplierSearch" runat="server" CssClass="dropdown" TabIndex="8" onkeydown="return EnterTab(event,'ddlSupplierSearch');"></asp:DropDownList>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label72" runat="server" CssClass="label" Text="Supplier"></asp:Label>
                                        <asp:TextBox ID="txtSupplierSearch" runat="server" TabIndex="5" AutoPostBack="false" CssClass="textbox" Width="183px" onBlur="popUpSupplierSearchNameSelectSelect();" MaxLength="50" onkeydown="return EnterTab(event,'ddlPaymentModeCashCredit');"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" TabIndex="1" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute" />

                                    </li>

                                    <li>
                                        <label class="label">Payment Mode</label>
                                        <asp:DropDownList runat="server" CssClass="dropdown" ID="ddlPaymentModeCashCredit" TabIndex="6" onkeydown="return EnterTab(event,'btnSearch');">
                                            <asp:ListItem Text="select" Value="2" />
                                            <asp:ListItem Text="Credit" Value="1" />
                                            <asp:ListItem Text="Cash" Value="0" />
                                        </asp:DropDownList>
                                    </li>

                                </div>
                            </ul>
                            <br class="clear" />
                            <ul class="contentul">
                                <asp:GridView ID="gvPurchase" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnPageIndexChanging="gvPurchase_PageIndexChanging" AllowPaging="true" PageSize="10" DataKeyNames="ID_Purchase,FK_Store"
                                    OnRowCommand="gvPurchase_RowCommand" OnRowDataBound="gvPurchase_RowDataBound" OnSelectedIndexChanging="gvPurchase_SelectedIndexChanging" OnRowDeleting="gvPurchase_RowDeleting">
                                    <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>

                                        <asp:BoundField DataField="Store" HeaderText="Store" />
                                        <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                                        <asp:BoundField DataField="RefNo" HeaderText="Ref.No" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                        <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" />
                                        <asp:BoundField DataField="NetAmount" HeaderText="Net Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btnApprovedStatus" CssClass="button" Width="75px" Text='<%# Bind("ApprovedStatus") %>' runat="server" OnClientClick="ConfirmPurchaseApprove();" CommandName="ApprovedStatus" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1784" runat="server" CausesValidation="false" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" OnClick="ImageButton2_Click" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton343" runat="server" CommandName="Print" ToolTip="Print" ImageUrl="~/images/1430738477_report.png" Width="20px" Height="20px" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                                <div class="Pager">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' OnClick="Page_Changed" Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ul>
                        </div>
                    </div>
                    <div class="clear"></div>

                    <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                    <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                        <div id="divheader" runat="server" class="PopupHeader">
                            <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head" CssClass="lblPopupHeader"></asp:Label>
                            <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                        </div>

                        <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                    </asp:Panel>
                    <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                        CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                    </cc:ModalPopupExtender>
                    <asp:Button ID="btS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                    <asp:Panel ID="divPop" runat="server" Style="display: none;" CssClass="DivStyle">
                        <div id="divhead" runat="server" class="PopupHeader">
                            <asp:Label ID="lblPopupH" runat="server" CssClass="lblPopupHeader"></asp:Label>
                            <asp:ImageButton ID="btnClse" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                        </div>

                        <uc3:CommonPopupGrid ID="CommonPopupGrid1" runat="server" />
                    </asp:Panel>
                    <cc:ModalPopupExtender ID="mpGrid" runat="server" PopupControlID="divPop" TargetControlID="btS"
                        CancelControlID="btnClse" Drag="true" PopupDragHandleControlID="divhead">
                    </cc:ModalPopupExtender>

                    <asp:Button ID="btnD" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                    <asp:Panel ID="divPopUpdelete" runat="server" Style="display: none;" CssClass="DivStyle">
                        <div id="divPopheader" runat="server" class="PopupHeaderReason">
                            <asp:Label ID="lblPopupHeader1" runat="server" Text="Reason"></asp:Label>
                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                        </div>
                        <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                            <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                        </div>
                    </asp:Panel>
                    <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divPopUpdelete" TargetControlID="btnD"
                        CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divPopheader">
                    </cc:ModalPopupExtender>

                    <asp:Button ID="btnExpen" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                    <asp:Panel ID="divExpense" runat="server" Style="display: none;" CssClass="DivStyle">
                        <div id="divPopH" runat="server" class="PopupHeader" style="width: 572px">
                            <asp:Label ID="lblPopupHeader2" runat="server" CssClass="lblPopupHeader"></asp:Label>
                            <asp:ImageButton ID="btnc" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                        </div>

                        <uc4:CommonPopupExpense ID="CommonPopupExpense" runat="server" />
                    </asp:Panel>
                    <cc:ModalPopupExtender ID="mpExpense" runat="server" PopupControlID="divExpense" TargetControlID="btnExpen"
                        CancelControlID="btnc" Drag="true" PopupDragHandleControlID="divPopH">
                    </cc:ModalPopupExtender>
                    <asp:Button ID="btnP" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                    <asp:Panel ID="pnlPrvlg" runat="server" Style="display: none;" CssClass="DivStyle">
                        <div id="divPrvlg" runat="server" class="PopupHeader">
                            <asp:Label ID="Label28" runat="server" Text="Privilege" CssClass="lblPopupHeader"></asp:Label>
                            <asp:ImageButton ID="btnCP" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                        </div>
                        <uc1:CommonPopupPrivilege runat="server" ID="CommonPopupPrivilege" />
                    </asp:Panel>
                    <cc:ModalPopupExtender ID="mpPrivilege" runat="server" PopupControlID="pnlPrvlg" TargetControlID="btnP"
                        CancelControlID="btnCP" Drag="true" PopupDragHandleControlID="divPrvlg">
                    </cc:ModalPopupExtender>
                    <div class="clear"></div>


                    <div style="display: none">
                        <asp:TextBox ID="txtKey" runat="server"></asp:TextBox>
                    </div>
                    <div class="loading" align="center">
                        Processing. Please wait.<br />
                        <br />
                        <asp:Image ID="Image1" runat="server" Style="margin-top: 50px;"
                            AlternateText="Image text"
                            ImageAlign="Middle"
                            ImageUrl="~/images/loader2.gif" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>

</body>
</html>
