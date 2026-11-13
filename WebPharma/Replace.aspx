<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Replace.aspx.cs" Inherits="Replace" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopUpGridSelection.ascx" TagName="CommonPopupGrid" TagPrefix="uc3" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Replace::</title>
<%--         <link href="css/main.css" rel="stylesheet" />
   <script src="js/jquery-1.10.2.js"></script
    >
  
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/alertify.min.js"></script>
   
   <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
  
    <script src="js/toastr.js"></script>
    <script src="js/Navigation.js"></script>--%>
        <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <script src="js/Navigation.js"></script>
     <script src="js/jquery-1.10.2.js"></script>
      <script src="js/Custom.js"></script>
    <script src="AssetLogin/js/jquery.min.js"></script>
       
        <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
            height:150px;
            width:940px;
        }     
    </style>  
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
       <script type='text/javascript' src='Styles/xtableheaderfixed.js'></script>
 
    <script type='text/javascript'>
        xAddEventListener(window, 'load',
            function () { new xTableHeaderFixed('gvItems', 'table-container', 0); }, false);
    </script>
      <script type="text/javascript">


          function ItemClear() {

              $("#hdfItem").val("");
              $("#hfID_Replace").val("");
              $("#hfSlNo ").val("");
              $("#DivPreviousPurchase").hide();
              $("#ddlItem").val("0");
              $("#ddlCategory").val("0");
              $("#txtItmShortName").val("");
              $("#ddlUnit").val("0");
              $("#ddlSubunit").val("0");
              $("#txtBatch").val("");
              var toDate = new Date();
              $("#txtExpiryDate").val(toDate.format("dd/MM/yyyy"));
              $("#txtHSNSAC").val("");
              $("#txtQuantity").val("0");
              $("#txtFree").val("0");
              $("#txtActqty").val("0");
              $("#txtActq").val("0");
              $("#txtPurchaseRate").val("0");
              $("#chkInctax").prop("checked", false);
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
              $("#hfItemId").val("");

          }
          function PopupOnReason() {
              try {

                  //  alert('k');
                  $("#divPopupReason").css('display', 'block');
                  //  alert('j');
              }
              catch (ex) {

              }
          }
          function ItemClearPurRet() {

              $("#txtitem").val("");
              $("#txtPurQty").val("0");
              $("#txtPurFree").val("0");
              $("#txtPurchaseRateItem ").val("0");
              $("#txttaxAmnt").val("0");
              $("#txtCurrentStock").val("0");
              $("#txtTotalAmountItem").val("0");
              $("#txtDiscAmntFrom").val("0");
              
              $("#hfItemId").val("");


          }
          function CalculateNetT() {


              if ($("#txtToTotal").val() == "") {
                  $("#txtToTotal").val("0");
              }
              if ($("#txtToRoundOff").val() == "") {
                  $("#txtToRoundOff").val("0");
              }
              if ($("#txtToTotal").val() != "" && $("#txtToRoundOff").val() != "") {

                  var totalAmount = $("#txtToTotal").val() == "" ? "0" : $("#txtToTotal").val();
                  sum = parseFloat(totalAmount);
                  var total = $("#txtToNetAmount").val() == "" ? "0" : $("#txtToNetAmount").val();

                  var fracpart = (((total < 1.0) ? total : (total % Math.floor(total))) * 100).toFixed(2);
                  if (parseFloat(fracpart) >= 50) {

                      var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);
                      var totalnetRoundamount = parseFloat(total) + parseFloat(Balancefract);

                      var balfract = parseFloat(fracpart / 100).toFixed(2);
                      $("#txtToNetAmount").val(totalnetRoundamount.toFixed(4));
                      $("#txtToRoundOff").val(Balancefract.toFixed(4));
                  }

                  else if (fracpart < 50) {

                      var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);
                      var totalnetRoundamount = parseFloat(total) - parseFloat(fracpart / 100);
                      var balfract = parseFloat(fracpart / 100).toFixed(2);

                      $("#txtToNetAmount").val(totalnetRoundamount.toFixed(4));
                      $("#txtToRoundOff").val(-balfract.toFixed(4));

                  }


              }


              // ItemClear();

          }

          function txtLinkbutton() {

              $("#btnLinkbutton").click();
          }

          function CalculateNetAmount() {
              try {

                  var Quantity = $("#txtPurQty").val() == "" ? "0" : $("#txtPurQty").val();
                  // var ItemVat = $("#txtItemVat").val() == "" ? "0" : $("#txtItemVat").val();
                  var PurchaseRateItem = $("#txtPurchaseRateItem").val() == "" ? "0" : $("#txtPurchaseRateItem").val();
                  var TotalAmountItem = $("#txtTotalAmountItem").val() == "" ? "0" : $("#txtTotalAmountItem").val();
                  //  var ItemVatAmount = $("#txtItemVatAmount").val() == "" ? "0" : $("#txtItemVatAmount").val();


                  if (parseFloat(Quantity) > parseFloat(0)) {
                      if (parseFloat(PurchaseRateItem) > parseFloat(0)) {
                          //if (parseFloat(ItemVat) > parseFloat(0)) {
                          //    ItemVatAmount = parseFloat(Quantity) * parseFloat(PurchaseRateItem) * parseFloat(ItemVat) / parseFloat(100);
                          //}
                          //else {

                          //    ItemVatAmount = parseFloat(0);
                          //}

                          //if (parseFloat(CESS) > parseFloat(0)) {
                          //    CESSAmt = parseFloat(Quantity) * parseFloat(PurchaseRateItem) * parseFloat(CESS) / parseFloat(100);
                          //}
                          TotalAmountItem = parseFloat(Quantity) * parseFloat(PurchaseRateItem);
                          // $("#txtItemVatAmount").val(ItemVatAmount.toFixed(2));
                          $("#txtTotalAmountItem").val(TotalAmountItem.toFixed(2));
                          //  $("#txtCESSAmt").val(CESSAmt.toFixed(2));

                      }
                      else {
                          // $("#txtItemVatAmount").val('0');
                          $("#txtTotalAmountItem").val('0');
                      }
                  }
                  else {
                      //$("#txtItemVatAmount").val('0');
                      $("#txtTotalAmountItem").val('0');
                  }
              }

              catch (e) {
                  alert(e);
              }
          }



          function ClearAdd() {
              try {
                  var dt = new Date();
                  $("#hfDate").val(dt.format("dd/MM/yyyy"));
                  $("#txtDate").val(dt.format("dd/MM/yyyy"));
                  $("#txtInvoiceDate").val(dt.format("dd/MM/yyyy"));
                  $("#txtReplace").val("0");
                  $("#ddlBillType").val("0");
                  // $("#ddlStore").val("0");
                  $("#ddlSupplier").val("0");
                  $("#txtInvoiceNo").val("");
                  //  $('#txtPurBillNo').val("");
                  $("#chkLinkbutton").prop("checked", false);
                  //  $("#txtPurBillNo").val("0");
                  ItemClear();
                  ItemClearPurRet();
              }
              catch (e) {
                  alert(e);
              }
          }
          function ClearSearch() {
              var dt = new Date();
              $("#ddlSearchSupplier").val("0");
              $("#txtFromDateSearch").val(dt.format("dd/MM/yyyy"));
              $("#txtToDateSearch").val(dt.format("dd/MM/yyyy"));
              $("#ddlStoreSearch").val("0");
            
              $("#ddlStatusApproveSearch").val("2");
          }

          function calculateActualQuantity() {
              var qty = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
              var free = $("#txtFree").val() == "" ? "0" : $("#txtFree").val();
              var nounit = $("#hfUnitQuantity").val() == "" ? "0" : $("#hfUnitQuantity").val();
              var ActQty = parseFloat(nounit) * parseFloat(qty);

              var SubunitNo = $("#hfSubUnitQuantity").val() == "" ? "0" : $("#hfSubUnitQuantity").val();
              $("#txtCheckActQuantity").val(ActQty.toFixed(4));
              // alert(qty); alert(free); alert(nounit); alert(ActQty); alert(SubunitNo);
              if (SubunitNo != 0)
                  var Actquantity = (parseFloat(qty) + parseFloat(free)) * (parseFloat(nounit)) * (parseFloat(SubunitNo));
              else
                  var Actquantity = (parseFloat(qty) + parseFloat(free)) * (parseFloat(nounit));
              $("#txtActq").val(Actquantity.toFixed(4));
          }

          function getpurchaseamount() {
              var PurchaseRate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
              var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
              if ($('#chkInctax').is(":checked")) {
                  var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
                  //alert(TotalTaxPercetage);
                  var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));
                  // alert(taxAmount);                                                                   
                  $("#txttaxAmount").val(parseFloat(taxAmount).toFixed(4));
                  var PurchaseValue = (parseFloat(PurchaseRate) * parseFloat(Quantity)) - (parseFloat(taxAmount));
                  $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));
              }
              else {

                  var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
                  var taxAmount = ((parseFloat(PurchaseRate) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
                  $("#txttaxAmount").val(taxAmount.toFixed(4));
                  var PurchaseValue = parseFloat(PurchaseRate) * parseFloat(Quantity);
                  $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));

              }
              if ($('#chkDiscounttax').is(":checked")) {

                  var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
                  var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
                  var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
                  var DiscountAmount = ((parseFloat(PurchaseValue) + parseFloat(taxAmount)) * (DiscPer)) / parseFloat(100);

                  $("#txtPurDiscAmnt").val(parseFloat(DiscountAmount).toFixed(4));
              }

              else {


                  var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
                  var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
                  var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
                  var DiscountAmount = (parseFloat(PurchaseValue) * parseFloat(DiscPer)) / parseFloat(100);
                  $("#txtPurDiscAmnt").val(DiscountAmount.toFixed(4));
              }
              var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
              var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
              var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
              var NetPurchase = (parseFloat(PurchaseValue) + parseFloat(taxAmount)) - parseFloat(DiscountAmount);
              $("#txtNetPur").val(NetPurchase.toFixed(4));
          }
          function getSalesPrice(parameter1) {
              var a = $('#txtKey').val();
              if (a != 0 && parameter1 == "0")
                  parameter1 = a;
              if (a == 0)
                  parameter1 = parameter1
              if (parameter1 == "0") {
                  var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                  var DiscountAmt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
                  var SalesPrice = (parseFloat(MRP)) - (parseFloat(DiscountAmt));
                  $("#txtSalesPrice").val(SalesPrice.toFixed(4));
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
                  $("#txtProfitPer").val(ProfitPercentage.toFixed(4));

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

                  var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                  var DiscountAmt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
                  var DiscountPer = isNaN((parseFloat(DiscountAmt) * parseFloat(100)) / parseFloat(MRP)) ? 0 : (parseFloat(DiscountAmt) * parseFloat(100)) / parseFloat(MRP);
                  $("#txtDiscountPer").val(DiscountPer.toFixed(4));

                  SalesPriceDisc();
                  ProfitDiscnCalculation1();
                  $('#txtKey').val(parameter1);
              }

          }
          function Discount1() {
              var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
              var SalesPrice = $("#txtSalesPrice").val() == "" ? "0" : $("#txtSalesPrice").val();
              var DiscountAmount = isNaN(parseFloat(MRP) - parseFloat(SalesPrice)) ? 0 : parseFloat(MRP) - parseFloat(SalesPrice);
              $("#txtDiscountAmt").val(DiscountAmount.toFixed(4));
              var DiscountPer = isNaN((parseFloat(DiscountAmount) * parseFloat(100)) / parseFloat(MRP)) ? 0 : (parseFloat(DiscountAmount) * parseFloat(100)) / parseFloat(MRP);
              $("#txtDiscountPer").val(DiscountPer.toFixed(4));
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
                  $("#txtProfitAmount").val(ProfitAmount.toFixed(4));
                  var SalesPe = (parseFloat(PurchaseRateAmount)) + (parseFloat(ProfitAmount));
                  $("#txtSalesPrice").val(SalesPe.toFixed(4));

              }
              var salePrice = $("#txtSalesPrice").val() == "" ? "0" : $("#txtSalesPrice").val();

              //if (parseFloat(salePrice) > parseFloat(MRP))
              //    alert('Sales Price not greater than MRP');




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
                  $("#txtDiscountAmt").val(Discount.toFixed(4));
                  var DiscountAmt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
                  var SalesPrice = isNaN((parseFloat(MRP)) - (parseFloat(DiscountAmt))) ? 0 : (parseFloat(MRP)) - (parseFloat(DiscountAmt));
                  $("#txtSalesPrice").val(SalesPrice.toFixed(4));
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

              if (PurchaseRateNew > 0)
                  var ProfitPer = isNaN((parseFloat(ProfitAmount) * parseFloat(100)) / parseFloat(PurchaseRateNew)) ? 0 : (parseFloat(ProfitAmount) * parseFloat(100)) / parseFloat(PurchaseRateNew);
              if (ProfitPer < 0)
                  $("#txtProfitPer").val(0);
              if (ProfitPer > 0)
                  $("#txtProfitPer").val(ProfitPer.toFixed(4));
              if (ProfitAmount < 0)
                  $("#txtProfitAmount").val(0);
              if (ProfitAmount > 0)
                  $("#txtProfitAmount").val(ProfitAmount.toFixed(4));


          }

          function TaxAmount() {

              var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
              var DiscountAmt = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
              var SalesPrice = MRP - DiscountAmt
              var TaxPercentage = $("#hfTotTaxsalesPercentage").val() == "" ? "0" : $("#hfTotTaxsalesPercentage").val();
              if (MRP == "" || MRP == 0) {
                  alert("MRP can't not be Empty and Zero");
                  $("#txtMRP").focus();
              }
              if ($('#ChkIncStax').is(":checked")) {

                  var taxAmount = ((parseFloat(SalesPrice)) * (parseFloat(TaxPercentage))) / ((parseFloat(TaxPercentage) + parseFloat(100)));
                  if (taxAmount != null)
                      $("#txtSTaxAmount").val(parseFloat(taxAmount).toFixed(4));
                  $("#txtSalesPrice").val(parseFloat(SalesPrice).toFixed(4));
              }
              else {
                  var taxAmount = (parseFloat(SalesPrice) * parseFloat(TaxPercentage)) / parseFloat(100);
                  if (taxAmount != null)
                      $("#txtSTaxAmount").val(taxAmount.toFixed(4));
                  var Sale = parseFloat(SalesPrice) + parseFloat(taxAmount)
                  // alert(Sale);
                  $("#txtSalesPrice").val(parseFloat(Sale).toFixed(4));
                  if (parseFloat(MRP) == parseFloat(SalesPrice)) {
                      alert('Sales Price not greater than MRP');
                      return false;
                  }
              }
          }

          //function popUpStockSelectClick() {

          //    try {
          //        alert('g1');
          //        var ItemName = $("#txtItemShortName").val();
          //        url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val()

          //        popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
          //        ",statusbar=no,menubar=no,resizable=0,width=1015," +
          //        "height=650,left = 200,top = 100");
          //        popUpObj.focus();

          //        $("#ddlUnit").focus();
          //    }
          //    catch (e) {
          //        alert(e);
          //    }

          //}
          //function popUpStockSelect() {

          //    try {
          //        alert('g');
          //        var Cat = $("#ddlCategory").val();

          //        var ItemName = $("#txtItemShortName").val();

          //        if (ItemName.length > 0) {
          //            url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val();

          //            popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
          //            ",statusbar=no,menubar=no,resizable=0,width=1015," +
          //            "height=650,left = 200,top = 100");
          //            popUpObj.focus();
          //        }
          //        else {


          //        }

          //    }
          //    catch (e) {
          //        alert(e);
          //    }

          //}
          //var popup;
          //function popUpStockSelectClick1() {

          //    if (Page_ClientValidate("img") == true) {

          //        var popup;
          //        var ItemName = $("#txtitem").val();
          //        //if ($("#hfStore").val() == '') {
          //        SetStoreDetail();
          //        // BlurStore();
          //        // $("#hfStore").val($("#ddlStore").val())
          //        //   }
          //        popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val() + '&suplr=' + $("#txtSupRefno").val(), "Popup", "width=1000,height=800");
          //        popup.focus();

          //    }

          //    return false;
          //}
          function SetStoreDetail() {
              var store = $("#ddlStore option:selected").val();
              $("#hfStore").val(store);
          }
          var popup;
          function popUpStockSelectClick(p) {
              
              $("#hfType ").val(p);
              if (Page_ClientValidate("img") == true) {

                  var popup;
                  var ItemName = $("#txtitem").val();
                  var hfUser = $("#hfUser").val();
                  var hfClient = $("#hfClient").val();
                  var ddlCategory = $("#ddlCategory").val();
                  var hfStore = $("#hfStore").val();
                  var txtSupRefno = $("#ddlSupplier").val();
                  var ddlSupplier = $("#ddlSupplier").val();
                  var txtInvoiceDate = $("#txtInvoiceDate").val();
                  var hfItemvalue = $("#hfItemvalue").val();
                  var ItemType = "2";

                  if (p == 1) {
                      var ItemName = $("#txtitem").val();
                      //  $("#txtitemConv").val('');
                  }
                  else {
                      var ItemName = $("#txtItmShortName").val();
                      // $("#txtitem").val('');
                  }
                  //     if ($("#hfFromStore").val() == '') {
                  SetStoreDetail();

                  //  }
                  //if (($("#hfFromStore").val().length) > 0) {
                  if (p == 1) {

                      $.ajax({
                          type: "POST",
                          url: 'Textile.asmx/GetUrlEncryptStock',
                          data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','txtSupRefno':'" + txtSupRefno + "','hfStore':'" + hfStore + "','hfClient':'" + hfClient + "'}",
                          contentType: "application/json; charset=utf-8",
                          dataType: "json",
                          success: function (MenuHtml) {

                              // url = "PreviousPurchase.aspx?" + MenuHtml.d;
                              SetStoreDetail();
                              url = "StockItems.aspx?" + MenuHtml.d;

                              popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                              ",statusbar=no,menubar=no,resizable=0,width=1000," +
                              "height=800,left = 200,top = 100");
                              popUpObj.focus();
                              //popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val() + '&suplr=' + $("#txtSupRefno").val(), "Popup", "width=1000,height=800");
                              //popup = window.open("StockItems.aspx?" + +MenuHtml.d, "Popup", "width=1000,height=800");
                              //popup.focus();
                          },
                          error: function (MenuHtml) {
                              // alert('GetMinItemStockName');
                              //alert('webservice_error');
                              // $("#hfEncryptvalue").val("");
                          }
                      });
                      //popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName+ '&ctgry=' + $("#ddlCategory").val() , "Popup", "width=1000,height=800");
                      //popup.focus();
                  }
                  else {





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

                      //url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName+ '&ctgry=' + $("#ddlCategory").val();

                      //popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=yes,location=no" +
                      //",statusbar=no,menubar=no,resizable=0,width=1015," +
                      //"height=650,left = 200,top = 100");
                      //popUpObj.focus();



                  }

                  //}
              }

              return false;
          }

          //function popUpStockSelect1() {

          //    if (Page_ClientValidate("img") == true) {

          //        var popup;
          //        var ItemName = $("#txtitem").val();

          //        if (ItemName.length > 0) {
          //            if ($("#hfStore").val() == '') {
          //                SetStoreDetail();
          //                //BlurStore();
          //                //$("#hfStore").val($("#ddlStore").val())
          //            }

          //            popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val() + '&suplr=' + $("#txtSupRefno").val(), "Popup", "width=1000,height=800") + '&ctgry=' + $("#ddlCategory").val();
          //            popup.focus();
          //        }
          //    }

          //    return false;
          //}
          function CalculateAmount(id, type) {
              //  alert(type);
              if (type == 'txtFreeQty') {

                  var txtFreeQty = id;
                  var txtQuantity = id.replace(type, 'txtQuantity');
                  var txtActQty = id.replace(type, 'txtActQty');
              }
              else if (type == 'txtActQty') {
                  var txtActQty = id;
                  var txtFreeQty = id.replace(type, 'txtFreeQty');
                  var txtQuantity = id.replace(type, 'txtQuantity');

              }
              else {
                  var txtQuantity = id;
                  var txtFreeQty = id.replace(type, 'txtFreeQty');
                  var txtActQty = id.replace(type, 'txtActQty');
              }

              var lblPr = id.replace(type, 'lblPurchaseRate');
              var lblPrN = id.replace(type, 'lblNewat');
              var lblTotal = id.replace(type, 'lblTotalAmt');
              var hdfTotalAmt = id.replace(type, 'hdfTotalAmt');

              var hdfQty = id.replace(type, 'hdfQty');
              var hdfFree = id.replace(type, 'hdfFree');
              var hdfActQty = id.replace(type, 'hdfActQty');
              //alert('29');
              var qty = $("#" + txtQuantity).val() == '' || $("#" + txtQuantity).val() == null ? 0 : parseFloat($("#" + txtQuantity).val());

              var Free = $("#" + txtFreeQty).val() == '' || $("#" + txtFreeQty).val() == null ? 0 : parseFloat($("#" + txtFreeQty).val());

              var ActQty = $("#" + txtActQty).val() == '' || $("#" + txtActQty).val() == null ? 0 : parseFloat($("#" + txtActQty).val());
              var pr = $("#" + lblPr).text() == '' || $("#" + lblPr).text() == null ? 0 : parseFloat($("#" + lblPr).text());

              var prevqty = $("#" + hdfQty).val();
              var prevFreeqty = $("#" + hdfFree).val();
              var prevActqty = $("#" + hdfActQty).val();


              if ($("#hfPurchaseReturn").val() != '' || $("#" + txtQuantity).val() != null) {

                  var hdfBalQty = id.replace(type, 'hdfBalanceQuantity');
                  var hdfBalFree = id.replace(type, 'hdfBalanceFree');
                  var hdfBalActQty = id.replace(type, 'hdfBalanceActQty');

                  prevqty = parseFloat(prevqty) + parseFloat($("#" + hdfBalQty).val());
                  prevFreeqty = parseFloat(prevFreeqty) + parseFloat($("#" + hdfBalFree).val());
                  prevActqty = parseFloat(prevActqty) + parseFloat($("#" + hdfBalActQty).val());

              }

              var NoofUnits = id.replace(type, 'hdfNoofUnits');

              var ActQty_ = parseFloat(qty) * parseFloat($("#" + NoofUnits).val());
              //alert(qty);  alert(ActQty_);

              if (qty == 0) {
                  $("#" + txtQuantity).val(qty);
              }
              if (Free == 0) {
                  $("#" + txtFreeQty).val(Free);
              }
              if (ActQty == 0) {
                  $("#" + txtActQty).val(ActQty);
              }

              if (parseFloat(qty) > parseFloat(prevqty) || parseFloat(Free) > parseFloat(prevFreeqty) || parseFloat(ActQty) > parseFloat(prevActqty) || (parseFloat(ActQty) > ActQty_ && type == 'txtActQty')) {
                  alertify.alert("Quantity Cannot Exceed Available Stock");

                  if (type == 'txtFreeQty') {
                      $("#" + txtFreeQty).val(parseFloat(prevFreeqty));
                  }
                  else if (type == 'txtActQty') {
                      ActQty_ = parseFloat(qty) * parseFloat($("#" + NoofUnits).val());
                      if (parseFloat(ActQty) > ActQty_) {
                          $("#" + txtActQty).val(parseFloat(ActQty_));
                      }
                      else {
                          $("#" + txtActQty).val(parseFloat(prevActqty));
                      }
                  }
                  else {
                      $("#" + txtQuantity).val(parseFloat(prevqty));
                      $("#" + txtActQty).val(parseFloat(prevActqty));
                  }
              }
              else {

                  if (type != 'txtFreeQty') {


                  }
              }
          }

          function CalculateTotalAmount(Type) {
              // alert(Type);
              alert('g');
              var Totalamt = 0;
              var TotalDisc = 0;
              var VATTotal = 0;

              var CESSTotal = 0;

              $.each($("table[id*=" + 'gvPurchaseReturn' + "] tr"), function (index, item) {
                  // alert('v1');
                  if ($(item).find("input[type=hidden]").length > 0) {

                      // alert('g1');
                      var PurPrice = $(item).find("[id*=gvPurchaseReturn_lblPurchaseRate_]").text();

                      var Qty = $(item).find("[id*=txtQuantity_]").val();

                      var ActQty = $(item).find("[id*=txtActQty_]").val();

                      var NoofUnits = $(item).find("[id*=hdfNoofUnits_]").val();
                      //alert(PurPrice); alert(Qty); alert(ActQty);
                      ActQty = parseFloat(Qty) * parseFloat(NoofUnits);
                      if (Type != 'txtActQty') {

                          var prevActqty = 0;

                          if ($("#hfPurchaseReturn").val() == '') {
                              prevActqty = $(item).find("[id*=hdfActQty_]").val();
                          }
                          else {
                              prevActqty = parseFloat($(item).find("[id*=hdfActQty_]").val()) + parseFloat($(item).find("[id*=hdfBalanceActQty_]").val());
                          }


                          $(item).find("[id*=txtActQty_]").val(ActQty);

                      }




                      var Amt = (parseFloat(PurPrice) * parseFloat(Qty))
                      //alert(Amt);
                      $(item).find("[id*='lblTotalAmt_']").text((Amt).toFixed(4));
                      Totalamt += Amt;
                      // alert(Amt);

                      //if ($('#rdPurchase').is(":checked")) {

                      var Qty = $(item).find("[id*=txtQuantity_]").val();

                      var SinglAmount = parseFloat($(item).find("[id*=lblSingleDiscount_]").text());
                      var DiscAmount = parseFloat(Qty) * parseFloat(SinglAmount)
                      // alert(DiscAmount);

                      // $(item).find("[id*='lblNewat_']").text((DiscAmount).toFixed(4));
                      $(item).find("[id*='lblDiscAmt_']").val((DiscAmount).toFixed(4));
                      //  alert(Qty); alert(DiscAmount); alert(SinglAmount);
                      TotalDisc += parseFloat(DiscAmount);

                      //  }

                  }
              });

              $("#txtTotalAmount").val(Totalamt.toFixed(4));
              //if ($('#rdPurchase').is(":checked")) {

              $("#txtDiscAmnt").val(TotalDisc.toFixed(4));
              //}

              Total();

          }
          function EnterTab(e, Id1) {

              var intKey = window.Event ? e.which : e.KeyCode;


              if (intKey == 13) {
                  e.returnValue = false;
                  $("#" + Id1).focus();
              }
          }

          function Total() {
              try {

                  var TotalAmount = $("#txtTotalAmount").val();
                  var DiscountAmt = $("#txtDiscAmnt").val();
                  var Discount = $("#txtDiscountP").val();
                  var NetAmount = $("#txtNetAmnt").val();

                  if (TotalAmount >= 0 && Discount >= 0) {

                      if ($('#rdPurchase').is(":checked")) {

                          var Amount = ((parseFloat(TotalAmount)) - parseFloat(DiscountAmt));
                          var DiscountPer = (DiscountAmt) * (100) / (TotalAmount)
                          $("#txtDiscountP").val(DiscountPer.toFixed(4));
                          var total = Amount;
                      }
                      else {
                          var dAmt = (parseFloat(TotalAmount) * parseFloat(Discount)) / (100);

                          var Amount = ((parseFloat(TotalAmount)) - parseFloat(dAmt));

                          var total = Amount;

                      }

                      if (total >= 0) {

                          if ($('#rdPurchase').is(":checked")) {
                          }
                          else
                              $("#txtDiscAmnt").val(dAmt.toFixed(4));
                          var fracpart = (((total < 1.0) ? total : (total % Math.floor(total))) * 100).toFixed(2);
                          // alert(fracpart);
                          if (parseFloat(fracpart) >= 50) {

                              var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);
                              // alert(Balancefract);
                              var totalnetRoundamount = parseFloat(total) + parseFloat(Balancefract);
                              var balfract = parseFloat(fracpart / 100).toFixed(2);

                              $("#txtNetAmnt").val(totalnetRoundamount.toFixed(2));

                              $("#txtRoundOff").val(Balancefract);
                          }

                          else if (fracpart < 50) {
                              var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);
                              var totalnetRoundamount = parseFloat(total) - parseFloat(fracpart / 100);
                              var balfract = parseFloat(fracpart / 100).toFixed(2);

                              //alert(totalnetRoundamount.toFixed(2));
                              $("#txtNetAmnt").val(totalnetRoundamount.toFixed(2));
                              $("#txtRoundOff").val(-balfract);
                          }

                      }
                      else {
                          //alert('Invalid entry');
                          $("#txtDiscountP").val(0);
                          $("#txtRoundOff").val(0);
                          //  Total();
                      }
                  }
                  else {
                      $("#txtDiscAmnt").val(0);
                      $("#txtNetAmnt").val(0);
                  }
              }
              catch (ex) {
                  alert(ex.message);
              }
          }
          function DecimalOrIntValidation(id, eventRef, type) {
              var hdfpacked = 'hdfPacked';

              if (type != 'items') {
                  hdfpacked = id.replace(type, 'hdfPacked');
              }

              if ($("#" + hdfpacked).val() == "False") {
                  CheckNumWithDecimal(eventRef);
              }
              else {
                  CheckInteger(eventRef);
              }
          }
          $(document).ready(function () {

              // Safe for UpdatePanel partial postbacks
              $(document).on('blur', '[id$="txtInvoiceDate"]', function () {

                  var val = $(this).val();

                  if (val !== '') {

                      ValidateMaterialReceiptDate();
                  }
              });
              $(document).on('blur', '[id$="txtDate"]', function () {

                  var val = $(this).val();

                  if (val !== '') {

                      ValidateReceiveDate();
                  }
              });
          });
          function ValidateMaterialReceiptDate() {

              var dateStr = $("#txtInvoiceDate").val();

              if (!dateStr || dateStr.trim() === '') {

                  alertify.alert("Please enter Invoice Date.");

                  $("#txtInvoiceDate").focus();

                  return false;
              }

              var selectedDate = parseDate_DMY(dateStr);

              if (!selectedDate) {

                  alertify.alert(
      "Invalid date format. Please enter date as dd/MM/yyyy.",
      function () {

          $("#txtInvoiceDate").val('');
          $("#txtInvoiceDate").focus();
      }
     );

                  return false;
              }

              var today = getToday();

              var finStart = getFinYearStart();

              // No future dates
              if (selectedDate > today) {

                  alertify.alert("Invoice date cannot be a future date.");

                  $("#txtInvoiceDate").val('');

                  $("#txtInvoiceDate").focus();

                  return false;
              }

              // No previous financial year dates
              if (selectedDate < finStart) {

                  var finStartStr =
                      ('0' + finStart.getDate()).slice(-2) + '/'
                      + ('0' + (finStart.getMonth() + 1)).slice(-2) + '/'
                      + finStart.getFullYear();

                  alertify.alert(
                      "Invoice date cannot be from a previous financial year.\n" +
                      "Earliest allowed date is " + finStartStr + "."
                  );

                  $("#txtInvoiceDate").val('');

                  $("#txtInvoiceDate").focus();

                  return false;
              }

              return true;
          }

          function ValidateReceiveDate() {

              var dateStr = $("#txtDate").val();

              var selectedDate = parseDate_DMY(dateStr);

              if (!selectedDate) {

                  alertify.alert(
      "Invalid date format. Please enter date as dd/MM/yyyy.",
      function () {

          $("#txtDate").val('');
          $("#txtDate").focus();
      }
     );

                  return false;
              }

              var today = getToday();

              var finStart = getFinYearStart();

              // No future dates
              if (selectedDate > today) {

                  alertify.alert("Receive date cannot be a future date.");

                  $("#txtDate").val('');

                  $("#txtDate").focus();

                  return false;
              }

              // No previous financial year dates
              if (selectedDate < finStart) {

                  var finStartStr =
                      ('0' + finStart.getDate()).slice(-2) + '/'
                      + ('0' + (finStart.getMonth() + 1)).slice(-2) + '/'
                      + finStart.getFullYear();

                  alertify.alert(
                      "Receive date cannot be from a previous financial year.\n" +
                      "Earliest allowed date is " + finStartStr + "."
                  );

                  $("#txtDate").val('');

                  $("#txtDate").focus();

                  return false;
              }

              return true;
          }


          function getFinYearStart() {

              var today = getToday();

              var yr = today.getFullYear();

              // April 1 financial year start
              var finStart = new Date(yr, 3, 1, 0, 0, 0, 0);

              if (today < finStart) {
                  finStart = new Date(yr - 1, 3, 1, 0, 0, 0, 0);
              }

              return finStart;
          }
          function getToday() {

              var t = new Date();

              return new Date(
                  t.getFullYear(),
                  t.getMonth(),
                  t.getDate(),
                  0, 0, 0, 0
              );
          }
          function parseDate_DMY(str) {

              if (!str || str.trim() === '')
                  return null;

              var parts = str.trim().split('/');

              if (parts.length !== 3)
                  return null;

              var d = parseInt(parts[0], 10);
              var m = parseInt(parts[1], 10) - 1;
              var y = parseInt(parts[2], 10);

              if (isNaN(d) || isNaN(m) || isNaN(y))
                  return null;

              var dt = new Date(y, m, d, 0, 0, 0, 0);

              // Prevent invalid rollover dates like 31/02/2026
              if (
                  dt.getFullYear() !== y ||
                  dt.getMonth() !== m ||
                  dt.getDate() !== d
              ) {
                  return null;
              }

              return dt;
          }
          $(document).on('keydown', '[id*=lblDiscAmt]', function (e) {
              if (e.keyCode == 13) {
                  e.preventDefault();
                  return false;
              }
          });



          </script>     
</head>
<body onkeydown="EnterTabBody(event);">
    <form id="frmReplace" runat="server" autocomplete="off">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

  <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"  runat="server" >
            <ContentTemplate>
                  <div class="btn_bg5"> 
                      <div class="heading2">
                           <div class="user">Replace</div>
                    <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                      
                        <asp:ImageButton ID="ImageButton1"  ImageUrl="images/ADD.png"  CssClass="imagebuttonStyle"  TabIndex="58"    runat="server" CausesValidation="false" OnClientClick="ClearAdd();releasebtn();"  OnClick="ImageButton1_Click" />
                         <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png"   CssClass="imagebuttonStyle"  TabIndex="57"    runat="server"  OnClick="imgbtnView_Click" />
                       
                    </div>
                        </asp:Panel>
                         </div>   

                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">  
                        <div id="divAdd" runat="server" style="display: block; height: 985px">


                            <ul class="contentul" id="Container">

                                <asp:Panel ID="PanelHead" runat="server">
                                    <div class="divL" style="width: 295px;">
                                     <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Bill No"></asp:Label>
                                        <asp:TextBox ID="txtPurBillNo" Width="115px" onkeydown="EnterTab(event,'ddlBillType');" runat="server" CssClass="textboxMandatory"  TabIndex="1"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Enter BillNo" ControlToValidate="txtPurBillNo" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                       <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender59" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator32">
                                        </cc:ValidatorCalloutExtender>
                                  </li>
                                              <li id="liBillType" runat="server">

                                            <asp:Label class="label" ID="Label2" runat="server" Text="Bill Type"></asp:Label>
                                         <asp:DropDownList runat="server"  onkeydown="EnterTab(event,'chkLinkbutton');"  Width="125px" CssClass="dropdownMandatory" ValidationGroup="submit" ID="ddlBillType" TabIndex="2"></asp:DropDownList>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Bill Type required" ControlToValidate="ddlBillType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                       <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                                        </cc:ValidatorCalloutExtender>

                                        </li>
                                                       <li>
                                            <asp:Label class="label" ID="lbl" runat="server" Text="Link with Pur.Ret" Width="132px"></asp:Label>
                                            <asp:CheckBox ID="chkLinkbutton" onkeydown="EnterTab(event,'txtDate');"  TabIndex="3"  onclick="txtLinkbutton();" runat="server" Style="padding-right:0px;"  Width="10px" />
                                        <asp:TextBox ID="txtPurRefno" runat="server" AutoPostBack="false" onblur="txtPurRefno_onblur()" CssClass="textboxMandatory" TabIndex="8" onKeypress="CheckInteger(event);" Width="65px" Style="margin-left: 4px;"></asp:TextBox>

                                            <asp:ImageButton ID="imgbtnPurReturn" runat="server" OnClick="imgbtnPurReturn_Click"  ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute" ValidationGroup="imgPurchase" />


                                        </li>
                                   
                                     
                                        

                                    </div>
                                        <div class="divL" style="width: 285px;">
                                       <li>
                                            <label class="label">Receive Date</label>
                                 <asp:TextBox runat="server" onkeydown="EnterTab(event,'txtInvoiceDate');" CssClass="textboxMandatory" Width="100px" ValidationGroup="submit" ID="txtDate" TabIndex="4" AutoComplete="off" />
                                            <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                            </cc:ValidatorCalloutExtender>

                                        </li>
                     
                                     
                                            <li>

                                            <label class="label">Invoice Date</label>
                                 <asp:TextBox runat="server" onkeydown="EnterTab(event,'ddlSupplier');" CssClass="textboxMandatory" Width="100px"
                                      ValidationGroup="submit" ID="txtInvoiceDate" TabIndex="5" AutoComplete="off" />
                                            <cc:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" TargetControlID="txtInvoiceDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Date required" ControlToValidate="txtInvoiceDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                            </cc:ValidatorCalloutExtender>

                                        </li>
                                                     <li id="liSupplier" runat="server">
                                           <asp:Label  ID="lblcat" runat="server" class="label" Width="136px" Text="Supplier"></asp:Label>
                                            <asp:DropDownList ID="ddlSupplier" Width="110px"   onkeydown="EnterTab(event,'txtInvoiceNo');"   runat="server" TabIndex="6" CssClass="dropdownMandatory"></asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="reqSupplier" runat="server" ErrorMessage="Suppler required" ControlToValidate="ddlSupplier" InitialValue="0"  Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <%--        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server" Enabled="True" TargetControlID="reqSupplier">
                                            </cc:ValidatorCalloutExtender>--%>

                                               <cc:ValidatorCalloutExtender  ID="v" runat="server" Enabled="true" TargetControlID="reqSupplier">
                                               </cc:ValidatorCalloutExtender>
                                        </li>
                                     

                                    </div>
                                  
                                     <div style="float: left; width: 285px;">
                                              <li>
                                        <asp:Label ID="Label18" runat="server" CssClass="label"  Text="Invoice No"></asp:Label>
                                        <asp:TextBox runat="server" Width="135px" CssClass="textboxMandatory" onkeydown="EnterTab(event,'ddlStore');"  ID="txtInvoiceNo"  TabIndex="7" MaxLength="25" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Required Invoice No" ControlToValidate="txtInvoiceNo" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                         <li>
                                            <asp:Label ID="Label186" runat="server" Width="135px" CssClass="label" Text="Store"></asp:Label>
                                            <asp:DropDownList runat="server" Width="143px" onkeydown="EnterTab(event,'txtitem');" ValidationGroup="submit" CssClass="dropdownMandatory" ID="ddlStore" TabIndex="8"></asp:DropDownList>
                                        </li>
                                         </div>
                                   
                                  
                                    <div  class="divL"  style="width: 200px;">
                       
                                     
                                        <asp:Button ID="btnLinkbutton" runat="server" Text="Button"  OnClick="btnLinkbutton_Click" CausesValidation="false" Style="display: none;" />
                                         <%--  <li id="liPurchase" runat="server">

                                            <asp:Label class="label" ID="Label9" runat="server" Text="Purchase" Width="100"></asp:Label>
                                            
                                        </li>--%>
                                      
                                     
                                            <asp:HiddenField ID="hdfPacked" runat="server" />
                                      

                                    </div>
                                </asp:Panel>
                                <li class="clear"></li>

                                <div class="divSeparation"></div>

        <div class="divL" style="width: 370px;">
            <li>
                        <asp:Label ID="lblItem" runat="server" CssClass="label" Text="Item" ></asp:Label>
                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtitem" onkeydown="EnterTab(event,'ImageButton3');" MaxLength="20" AutoComplete="off" Width="150px"  TabIndex="9" />
                       <%-- <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtitem" onkeydown="EnterTab(event,'ImageButton3');" MaxLength="20" AutoComplete="off" Width="150px"  TabIndex="9" onblur="return popUpStockSelect1();" />--%>
                        <%--   <asp:Button ID="btnItem" runat="server" Text="Item" OnClick="btnItem_Click" Style="display: none;" />--%>
                        <asp:ImageButton ID="ImageButton3" onkeydown="EnterTab(event,'txttaxAmnt');" runat="server" TabIndex="10"  OnClientClick="return popUpStockSelectClick(1);"  ImageUrl="~/images/serach_icon.png" style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute;" ValidationGroup="img" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Item Required " ControlToValidate="txtitem" Display="None" SetFocusOnError="True" ValidationGroup="addPurchase"></asp:RequiredFieldValidator>
                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                    </cc:ValidatorCalloutExtender>
            </li>
                                   
                              
                <asp:Label ID="lblTaxAmnt" runat="server" CssClass="label" Text="Tax Amt"></asp:Label>
                <asp:TextBox ID="txttaxAmnt" onkeydown="EnterTab(event,'txtPurQty');" CssClass="textbox" TabIndex="11" Width="120px" runat="server" MaxLength="11" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                <asp:Button Text="PTax"   ToolTip="PurchaseTax" ID="btnPtax" Style="padding: 0px; width:50px;height:18px; margin: 0px 0px 0px 6px;" CssClass="button"  OnClick="btnPtax_Click" runat="server" />
                                       
        </div>
        <div class="divL" style="width: 160px;">
            <li style="width: 150px;float: left;">
                <asp:Label ID="lblqty" runat="server" CssClass="label" Text="Qty" Width="60px"></asp:Label> 
                <asp:TextBox ID="txtPurQty"  onkeydown="EnterTab(event,'txtPurchaseRateItem');" CssClass="textboxMandatory" Style="text-align: right;" Width="80px" runat="server" TabIndex="12" MaxLength="11" onkeypress="DecimalOrIntValidation('txtPurQty',event,'items'); " onKeyup="CalculateNetAmount(); "  AutoComplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtPurQty" Display="None" SetFocusOnError="True" ValidationGroup="addPurchase"></asp:RequiredFieldValidator>
                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                </cc:ValidatorCalloutExtender>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPurQty" ErrorMessage="Enter a valid Quantity" Display="None" SetFocusOnError="True" MaximumValue="999999999999" ValidationGroup="addPurchase" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                </cc:ValidatorCalloutExtender>
                <asp:CompareValidator ID="cmpquantity" runat="server" ErrorMessage="cannot greater than current stock" ControlToCompare="txtCurrentStock" SetFocusOnError="True" ControlToValidate="txtPurQty" Display="None" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" Type="Double" ValidationGroup="addPurchase"></asp:CompareValidator>
                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="cmpquantity">
                </cc:ValidatorCalloutExtender>

                <asp:CustomValidator ID="cvQuantity" runat="server"
                ClientValidationFunction="ValidateActQty" ControlToValidate="txtPurQty" Display="None"
                CssClass="validator"
                ErrorMessage="Invalid Stock" SetFocusOnError="True" ValidationGroup="add"></asp:CustomValidator>

                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" Enabled="True" TargetControlID="cvQuantity">
                </cc:ValidatorCalloutExtender>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </li>
            <li style="width: 150px;float: left;">
                <asp:Label ID="lblPurRate" runat="server" CssClass="label" Width="60px" Text="Pur.Rate"></asp:Label>
                <asp:TextBox ID="txtPurchaseRateItem" onkeydown="EnterTab(event,'txtPurFree');" Style="text-align: right;"  CssClass="textboxMandatory" Width="80px" runat="server" TabIndex="13" MaxLength="14" onKeypress="CheckNumWithDecimal(event);" onkeyup="CalculateNetAmount();" AutoComplete="off"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Purchase Rate required" ControlToValidate="txtPurchaseRateItem" Display="None" SetFocusOnError="True" ValidationGroup="addPurchase"></asp:RequiredFieldValidator>
                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                </cc:ValidatorCalloutExtender>
                <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtPurchaseRateItem" ErrorMessage="Enter a valid Rate" Display="None" SetFocusOnError="True" MaximumValue="999999999999" ValidationGroup="addPurchase" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" Enabled="True" TargetControlID="RangeValidator5">
                </cc:ValidatorCalloutExtender>
            </li>
        </div>
        <div style="float: left; width: 170px;">
            <li>
                    <asp:Label ID="lblFree" runat="server" CssClass="label" Width="70px" Text="Free"></asp:Label>
                   <%-- <asp:TextBox ID="txtPurFree"  onkeydown="EnterTab(event,'txtTotalAmountItem');" Style="text-align: right;" CssClass="textbox" Width="80px" runat="server" TabIndex="14" MaxLength="12" onKeyup="ValidateActQty();" onkeypress="DecimalOrIntValidation(id,event,'items');" AutoComplete="off"></asp:TextBox>--%>
                 <asp:TextBox ID="txtPurFree"  onkeydown="EnterTab(event,'txtTotalAmountItem');" Style="text-align: right;" CssClass="textbox" Width="80px" runat="server" TabIndex="14" MaxLength="12"  onkeypress="DecimalOrIntValidation(id,event,'items');" AutoComplete="off"></asp:TextBox>

                    <asp:CompareValidator ID="cmpFree" runat="server" ErrorMessage="cannot greater than current stock" ControlToCompare="txtCurrentStock" ControlToValidate="txtPurFree" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Double" ValidationGroup="add"></asp:CompareValidator>
                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server" Enabled="True" TargetControlID="cmpFree">
                    </cc:ValidatorCalloutExtender>
            </li>
            <li>
                    <asp:Label ID="lblTotAmnt" runat="server" CssClass="label" Width="70px" Text="Tot.Amt"></asp:Label>
                        <asp:TextBox ID="txtTotalAmountItem" onkeydown="EnterTab(event,'txtDiscAmntFrom');" Style="text-align: right;" CssClass="textbox" Width="80px" runat="server" TabIndex="15" MaxLength="12" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>
                   
            </li>
            
        </div>
            <div style="float: left; width: 170px;">
                                      
                <li>
                    <asp:Label ID="lblCurrentStock" runat="server" CssClass="label" Width="56px" Text="Cur.Stock"></asp:Label>
                    <asp:TextBox runat="server" ID="txtCurrentStock"   Width="90px" ReadOnly="true"  CssClass="textbox" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"/>
                </li>
                 <li>
                    <asp:Label ID="Label33" runat="server" CssClass="label" Width="56px" Text="Disc Amt"></asp:Label>
                        <asp:TextBox ID="txtDiscAmntFrom"  TabIndex="16" Style="text-align: right;" CssClass="textbox" onkeydown="EnterTab(event,'btnAddItems');" Width="90px" runat="server"  MaxLength="12"  onKeypress="CheckNumWithDecimal1(event);" AutoComplete="off"></asp:TextBox>
                   
            </li>
            </div>


             <div class="clear"></div>
                        <li class="btnMargin">
                        <%--          <div style="float: left; width: 90px;">
                            <li>--%>
                                <asp:Button Text="Insert" ID="btnAddItems" runat="server" CausesValidation="true"  OnClick="btnAddItems_Click" TabIndex="16" ValidationGroup="addPurchase" CssClass="button" />

                        <%--                                        </li>
                        </div>--%>
                        <%--   <div style="float: left; width: 80px; margin-right: 4px; height: 28px;">
                            <li>--%>
                                <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" TabIndex="17" OnClientClick="ItemClearPurRet();return false;" />
                        <%--                                        </li>
                        </div>--%>
                        </li>  
                         <li class="clear"></li>
                          
                              
                                
                                           
                                       
                        <li style="width: 100%;  min-height: 210px; max-height: 210px; overflow-y: auto;">
                                <asp:GridView ID="gvPurchaseReturn" CssClass="grid" runat="server"  DataKeyNames="FK_Items,FK_Unit,FK_Stock,ExpiryDate,ActQty,Free,MRP"
                                EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="860px" OnRowDataBound="gvPurchaseReturn_RowDataBound"   OnSelectedIndexChanging="gvPurchaseReturn_SelectedIndexChanging"
                                BackColor="White"  BorderStyle="Solid"  OnRowDeleting="gvPurchaseReturn_RowDeleting" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" >
                                <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle"/>
                                            
                                <Columns>
                              <%--  <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Right" >
                                               
                                <ItemStyle HorizontalAlign="Right" CssClass="gridviewItemStyle"  />
                                </asp:BoundField>--%>

                                         <asp:TemplateField HeaderText="SlNo" ItemStyle-HorizontalAlign="Right">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtSlNo" runat="server" Text='<%# Bind("SLNO") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <asp:Label ID="lblgvSerialNO" runat="server" Text='<%# Bind("SLNO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item" >
                                                    
                                <ItemTemplate>
                                    <div style="overflow: auto; width: 250px;">
                                <asp:Label runat="server" ID="lblItem" Style="width: 250px;" Text='<%# Eval("Item") %>' />
                                </div>
                                        </ItemTemplate>
                                                 
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">

                                <ItemTemplate>
                               <%-- <asp:Label runat="server" ID="txtQuantity" onBlur="CalculateAmount(id,'txtQuantity');" AutoComplete="off" CssClass="textbox" Style="width: 50px; text-align: right;" onkeypress="DecimalOrIntValidation(id,event,'txtQuantity');" Text='<%# Eval("Quantity") %>' />--%>
                                <asp:Label runat="server" ID="txtQuantity"   Style="width: 50px; text-align: right;"  Text='<%# Eval("Quantity") %>' />                   
                                <asp:HiddenField ID="hfStockID" runat="server" Value='<%# Eval("FK_Stock") %>' />
                                <asp:HiddenField ID="hdfQty" runat="server" Value='<%# Eval("Quantity") %>' />
                                    <asp:HiddenField ID="hfCurrentStk" runat="server" Value='<%# Eval("CurrentStock") %>' />
                                        <asp:HiddenField ID="hiddenFk_Item" runat="server" Value='<%# Eval("FK_Items") %>' />                  
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Free" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">

                                <ItemTemplate>
                               <%-- <asp:TextBox runat="server" ID="txtFreeQty" onBlur="CalculateAmount(id,'txtFreeQty');" onkeypress="DecimalOrIntValidation(id,event,'txtFreeQty');" AutoComplete="off" CssClass="textbox" Style="width: 50px; text-align: right;"
                                    Text='<%# Eval("Free") %>' />--%>
                                      <asp:label runat="server" ID="txtFreeQty"    Style="width: 50px; text-align: right;"
                                    Text='<%# Eval("Free") %>' />

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right"  />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Act.Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                <ItemTemplate>
                                <%--<asp:TextBox runat="server" ID="txtActQty" onBlur="CalculateAmount(id,'txtActQty');" onkeypress="DecimalOrIntValidation(id,event,'txtActQty');" AutoComplete="off" CssClass="textbox" Style="width: 50px; text-align: right;"
                                    Text='<%# Eval("ActQty") %>' />--%>
                                    <asp:Label runat="server" ID="txtActQty"  Style="width: 50px; text-align: right;"
                                    Text='<%# Eval("ActQty") %>' />
                                                   
                              <%--  <asp:CustomValidator ID="cv1" runat="server" ControlToValidate="txtActQty" ErrorMessage="Enter a valid Quatity" 
                                    Display="None" ValidationGroup="submit" ClientValidationFunction="ClientValidate" ></asp:CustomValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="cv1">
                                </cc:ValidatorCalloutExtender>--%>
                                <asp:HiddenField ID="hdfNoofUnits" runat="server" Value='<%# Eval("NoofUnits") %>' />
                                <asp:HiddenField ID="hdfActQty" runat="server" Value='<%#Eval("ActQty") %>' />
                                    <asp:HiddenField ID="HiddenField5" runat="server" Value='<%# Eval("NoofUnits") %>' />
                                  
                                <%--<asp:HiddenField ID="hdtaxAmount" runat="server" Value='<%# Eval("PurchasetaxAmount") %>' />--%>
                                    <%--<asp:HiddenField ID="hdfBalanceActQty" runat="server" Value='<%# Eval("BalanceActQty") %>' />--%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Right"  />
                                </asp:TemplateField>



                                <asp:BoundField HeaderText="Unit" DataField="Unit"  ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="30px" ItemStyle-Width="30px" >

                                               
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Batch" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="70px" ItemStyle-Width="70px">

                                <ItemTemplate>
                                <asp:Label ID="lblBatchNo" runat="server"><%#Eval("BatchNo") %></asp:Label>
                                <asp:HiddenField ID="hdfBatchNo" runat="server" Value='<%#Eval("BatchNo") %>' />
                                </ItemTemplate>
                                                    
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pur.Rate" ItemStyle-CssClass="gridviewItemStyle"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">

                                <ItemTemplate>

                                <asp:Label ID="lblPurchaseRate" runat="server" Style="float: right;" Text='<%#Eval("PurchaseRate") %>'></asp:Label>


                                                       
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Right"  />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tot.Amt" ItemStyle-CssClass="gridviewItemStyle"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                <ItemTemplate>
                                <asp:Label ID="lblTotalAmt" runat="server"><%#Eval("TotalAmt") %></asp:Label>
                                    <asp:HiddenField ID="hdfNetPurchase" runat="server" Value='<%#Eval("TotalAmt") %>' />                     
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sal.Amt" ItemStyle-CssClass="gridviewItemStyle"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                <ItemTemplate>
                                <asp:Label ID="lblSalesPrice" runat="server" Style="float: right;" Text='<%#Eval("SalesPrice") %>'></asp:Label>
                                <asp:HiddenField ID="hdfSalesPrice" runat="server" Value='<%#Eval("SalesPrice") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Right"  />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                                <ItemTemplate>
                                <asp:DropDownList ID="ddlReason" runat="server" CssClass="dropdownMandatory" Width="100px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlReason" Display="None" ErrorMessage="Select a Reason" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </cc:ValidatorCalloutExtender>
                                </ItemTemplate>
                                <HeaderStyle  />
                                <ItemStyle HorizontalAlign="Center"  />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Discount"  Visible="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">

                                <ItemTemplate>
                                <asp:Label ID="lblDisc" runat="server" Style="float: right;" Text='<%#Eval("PurchaseDiscPercentage") %>'></asp:Label>
        
                                </ItemTemplate>
                                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dis.Amt"   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10px">

                                <ItemTemplate>
                                                      
                                                       
                                <asp:Label ID="lblDiscAmt" Width="50px"  runat="server" Style="float: right;" Text='<%#Eval("PurchaseDiscAmnt") %>'>
                                    </asp:Label>
                                    <asp:HiddenField ID="hdfDiscAmnt" runat="server" Value='<%# Eval("PurchaseDiscAmnt") %>' />
                                                        
                                </ItemTemplate>
                                                   
                                </asp:TemplateField>
                                          
                                <asp:TemplateField HeaderStyle-Width="0px">

                                <ItemTemplate >
                                <asp:Label ID="lblSingleDiscount" runat="server" Style="float: right;display:none" Text='<%#Eval("SingleDisAmnt") %>'></asp:Label>
                                                        
                                                        
                                </ItemTemplate>
                                                   
                                </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                       
                                                <asp:ImageButton ID="imagebuttonedit" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Edit_bg1.png" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>                 

                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle  CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="30px" />
                                <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                        </li>
                                
                                  
            <div class="divL" style="width: 170px;">

                            <li>
                                <asp:Label ID="Label9" runat="server" CssClass="label" Width="80px" Text="Tot.Amt"></asp:Label>
                                <asp:TextBox ID="txtTotalAmount" TabIndex="20" CssClass="textbox" Width="70px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;" OnTextChanged="txtTotNetPur_TextChanged" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                            </li>
            </div>
            <div class="divL" style="width: 140px;">

                        <li>
                            <asp:Label ID="Label10" runat="server" CssClass="label" Width="60px" Text="Disc%"></asp:Label>
                            <asp:TextBox ID="txtDiscountP" TabIndex="21" CssClass="textbox" Width="60px" runat="server" onkeyup="return Total();" MaxLength="10" Style="text-align: right; font-weight: bold;"   OnTextChanged="txtDiscountP_TextChanged" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                        </li>
            </div>
            <div class="divL" style="width: 170px;">

                        <li>
                            <asp:Label ID="Label69" runat="server" CssClass="label" Width="90px" Text="Disc.Amt"></asp:Label>
                            <asp:TextBox ID="txtDiscAmnt" TabIndex="22" CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;"  OnTextChanged="txtDiscAmnt_TextChanged" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                        </li>
            </div>
                           
                               
            <div style="float: left; width: 140px;">

                        <li>
                            <asp:Label ID="Label26" runat="server" Width="60px" CssClass="label" Text="Rd.Off"></asp:Label>
                            <asp:TextBox ID="txtRoundOff" TabIndex="23" CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;"></asp:TextBox>
                        </li>


            </div>
            <div class="divL" style="width: 140px;">
                        <li>
                                <asp:Label ID="Label21" runat="server" CssClass="label" Width="60px" Text="Net.Amt"></asp:Label>
                                <asp:TextBox ID="txtNetAmnt" TabIndex="24" CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;"  onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                            
                        </li>
            </div>
                           
                                <br class="clear" />
                                <%--  </ContentTemplate>
                                        
                                    </asp:UpdatePanel>--%>

            <div class="divSeparation"></div>

            <div class="divL" style="width: 370px;">

                        <li>
                            <asp:Label ID="Label23" runat="server" CssClass="label" Width="80" Text="Category"></asp:Label>
                            <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="25" onkeydown="EnterTab(event,'imgPopUp');" CssClass="dropdown"  OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                            
                        </li>
                                
                        <li>
                            <asp:Label ID="Label1" runat="server" Width="80px" CssClass="label" Text="Item"></asp:Label>
                            <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtItmShortName"  Width="183px" MaxLength="150" />
                            <%--<asp:Button ID="btnItem" runat="server" Text="Item" Style="display: none;" />--%>
                            <asp:ImageButton ID="imgPopUp" runat="server" onkeydown="EnterTab(event,'ddlUnit');" OnClientClick="return popUpStockSelectClick(2);" TabIndex="26"   ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute;" ValidationGroup="img"  />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Item Required " ControlToValidate="txtItmShortName" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender117" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                            </cc:ValidatorCalloutExtender>
                        </li>
                                 <asp:Label ID="lblQuotation" runat="server"></asp:Label>
                        <li style="display: none;">
                                <asp:Label ID="Label3" runat="server" Width="80px" CssClass="label" Text="Item"></asp:Label>
                                <asp:DropDownList ID="ddlItem" runat="server"   OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdownMandatory"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select an Item" ControlToValidate="ddlItem" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </cc:ValidatorCalloutExtender>
                        </li>
                        <li>
                                <asp:Label ID="Label16" runat="server" CssClass="label" Width="80px" Text="Unit"></asp:Label>
                                <asp:DropDownList ID="ddlUnit" onkeydown="EnterTab(event,'ddlSubunit');"  runat="server" TabIndex="27" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdownMandatory"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ErrorMessage="Select a Unit" ControlToValidate="ddlUnit" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender61" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator61">
                                </cc:ValidatorCalloutExtender>
                                <asp:Button ID="btnHiddenActualQuantityfromUnit" runat="server" CausesValidation="false" Text="Hide" Style="display: none;"  OnClick="btnHiddenActualQuantityfromUnit_Click" />
                        </li>
                        <li>
                                <asp:Label ID="Label55" runat="server" CssClass="label" Width="80px" Text="Subunit"></asp:Label>
                                <asp:DropDownList ID="ddlSubunit" onkeydown="EnterTab(event,'txtBatch');"  OnSelectedIndexChanged="ddlSubunit_SelectedIndexChanged"  runat="server" TabIndex="28" AutoPostBack="true" CssClass="dropdown"></asp:DropDownList>
                                        

                        </li>
                        <li>
                                <asp:Label ID="Label7" runat="server" CssClass="label" Width="80px" Text="Batch"></asp:Label>
                                <asp:TextBox runat="server"  onkeydown="EnterTab(event,'txtExpiryDate');" CssClass="textboxMandatory" TabIndex="29" Width="215px" ID="txtBatch" MaxLength="20" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Select a Batch" ControlToValidate="txtBatch"  Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator18">
                                </cc:ValidatorCalloutExtender>
                      

                        </li>
                        <li>
                                <asp:Label ID="Label11" runat="server" Width="80px" CssClass="label" Text="Expiry Date"></asp:Label>

                                <asp:TextBox ID="txtExpiryDate" onkeydown="EnterTab(event,'txtHSNSAC');"   CssClass="textboxMandatory" Width="215px" TabIndex="30"  runat="server" ></asp:TextBox>
                                <cc:CalendarExtender ID="txtExpiryDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtExpiryDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                <asp:RequiredFieldValidator ID="reqDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtExpiryDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" Enabled="True" TargetControlID="reqDate">
                                </cc:ValidatorCalloutExtender>
                        </li>

                        <li style="width: 270px; float: left;">
                                <asp:Label ID="Label75" Text="HSN/SAC" CssClass="label"  Width="80px" runat="server" />
                                <asp:TextBox runat="server" ID="txtHSNSAC" onkeydown="EnterTab(event,'txtQuantity');"   TabIndex="31" Width="165px" CssClass="textbox" MaxLength="25" />
                        </li>
                                <br />
                                <br />
                        <li  class="clear"></li>


            </div>
            <div class="divL" style="width: 160px;">

                        <li>
                                <asp:Label ID="Label4" runat="server" CssClass="label" Width="70px" Text="Quantity"></asp:Label>
                                <asp:TextBox ID="txtQuantity" CssClass="textboxMandatory" onkeydown="EnterTab(event,'txtFree');" Width="70px" runat="server" TabIndex="32" MaxLength="10" Onkeyup="calculateActualQuantity();getpurchaseamount();" onblur="getpurchaseamount();"  onKeypress="DecimalOrIntValidation(event);" Style="text-align: right;"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                </cc:ValidatorCalloutExtender>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>

                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                </cc:ValidatorCalloutExtender>
                                <asp:Button ID="btnHiddenActulaQuantity" runat="server" CausesValidation="false" Text="Hide" Style="display: none;"  OnClick="btnHiddenActulaQuantity_Click" />



                        </li>
                        <li>
                                <asp:Button ID="btnQuation" runat="server" CausesValidation="false" Text="Hide" Style="display: none;"   OnClick="btnQuation_Click" />
                        </li>
                        <li>
                                 <asp:Button ID="btnHiddenActullessQuantity" runat="server" CausesValidation="false" Text="Hide" Style="display: none;"  OnClick="btnHiddenActullessQuantity_Click" />
                        </li>
                        <li style="display: none;">

                                <asp:Label ID="Label52" runat="server" CssClass="label" Text="CheckActQty"></asp:Label>
                                <asp:TextBox ID="txtCheckActQuantity" CssClass="textboxMandatory" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                        </li>
                        <li>
                                <asp:Label ID="Label30" runat="server" CssClass="label" Width="70px" Text="Free"></asp:Label>
                                <asp:TextBox ID="txtFree" CssClass="textbox" onkeydown="EnterTab(event,'txtActq');" runat="server" Width="70px" MaxLength="11" TabIndex="33" Style="text-align: right;" OnkeyUp="calculateActualQuantity();"  onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                             
                        </li>
                                  
                        <li>
                                <asp:Label ID="Label29" runat="server" CssClass="label" Width="70px" Text="Act Qty"></asp:Label>
                                <asp:TextBox ID="txtActq" onkeydown="EnterTab(event,'txtPurchaseRate');" CssClass="textboxMandatory" Width="70px" runat="server" TabIndex="34" MaxLength="11" Style="text-align: right;" onKeypress="DecimalOrIntValidation(event);"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtActq" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0" Type="Double"></asp:RangeValidator>

                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="RangeValidator3">
                                </cc:ValidatorCalloutExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Actual Quantity required" ControlToValidate="txtActq" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator15">
                                </cc:ValidatorCalloutExtender>
                                      
                                <asp:Button ID="btnHiddenlessQuantity" runat="server" CausesValidation="false" Text="Hide" Style="display: none;"  OnClick="btnHiddenlessQuantity_Click" />

                        </li>
                        <li>
                                    <asp:Label ID="Label22" runat="server" Width="70px" CssClass="label" Text="Pur Rate"></asp:Label>
                                    <asp:TextBox ID="txtPurchaseRate"  onkeydown="EnterTab(event,'chkInctax');" AutoComplete="off" Width="70px" CssClass="textboxMandatory" runat="server" TabIndex="35" MaxLength="10" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);" onkeyup="getpurchaseamount();" onblur="getSalesPrice(0);"></asp:TextBox>


                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtPurchaseRate" ErrorMessage="Enter a valid PurchaseRate" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                    <cc:ValidatorCalloutExtender ID="ValtxidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                    </cc:ValidatorCalloutExtender>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="PurchaseRate required" ControlToValidate="txtPurchaseRate" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                    </cc:ValidatorCalloutExtender>
                        </li>
                                 
                        <li>
                                          
                                    <asp:Label ID="Label32" runat="server" Width="80px" CssClass="label" Text="Include Tax"></asp:Label>
                                        <asp:CheckBox ID="chkInctax"  onkeydown="EnterTab(event,'txttaxAmount');"  onclick="getpurchaseamount();getSalesPrice(0);" runat="server" Width="15px"  TabIndex="36" />
                                    <asp:Button Text="PTax"   ToolTip="PurchaseTax" ID="btnS" Style="padding: 0px; width:50px;height:18px; margin: 9px 0px 0px 6px; position: absolute" CssClass="button"  OnClick="btnS_Click" runat="server" />
                       
                         </li>
                                   
                        <li>
                                    <asp:Label ID="Label31" runat="server" CssClass="label" Width="70px" Text="Tax Amt"></asp:Label>
                                    <asp:TextBox ID="txttaxAmount" onkeydown="EnterTab(event,'chkDiscounttax');" CssClass="textbox" TabIndex="37" Width="70px" runat="server" MaxLength="11" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>

                        </li>
                        <li>
                                <div class="rbtn">
                                        <asp:Label ID="Label53" runat="server" CssClass="label" Text="Discount On Tax"></asp:Label>
                                        <asp:CheckBox ID="chkDiscounttax"  onkeydown="EnterTab(event,'txtPurDiscPer');"  AutoPostBack="true" TabIndex="38"  onclick="getpurchaseamount();" runat="server" Style="width: 70px;" />
                                
                                </div>
                        </li>
            </div>
            <div style="float: left; width: 170px;">

                     
                    <li>
                            <asp:Label ID="Label62" runat="server" CssClass="label" Width="80px" Text="Pur Disc%"></asp:Label>
                            <asp:TextBox ID="txtPurDiscPer"  onkeydown="EnterTab(event,'txtPurDiscAmnt');" CssClass="textbox" TabIndex="39"  Width="70px" runat="server" MaxLength="11" Style="text-align: right;" onKeyup="getpurchaseamount();"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>

                    </li>

                    <li>
                            <asp:Label ID="Label63" runat="server" CssClass="label" Width="80px" Text="Pur Disc Amt"></asp:Label>
                           <%-- <asp:TextBox ID="txtPurDiscAmnt"  onkeydown="EnterTab(event,'txtNetPur');"  CssClass="textbox" TabIndex="40"  Width="70px" runat="server" MaxLength="11" Style="text-align: right;" onblur="getpurchaseper();"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>--%>
                         <asp:TextBox ID="txtPurDiscAmnt"  onkeydown="EnterTab(event,'txtNetPur');"  CssClass="textbox" TabIndex="40"  Width="70px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                    </li>
                    <li>
                            <asp:Label ID="Label65" runat="server" CssClass="label" Width="80px" Text="Net Pur"></asp:Label>
                            <asp:TextBox ID="txtNetPur" onkeydown="EnterTab(event,'txtMRP');" CssClass="textboxMandatory" TabIndex="41"  Width="70px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="NetPurchase required" ControlToValidate="txtNetPur" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator30">
                            </cc:ValidatorCalloutExtender>
                            <asp:RangeValidator ID="RangeValidator9" runat="server" ControlToValidate="txtNetPur" ErrorMessage="Enter a valid NetPurchase" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="1" Type="Double"></asp:RangeValidator>
                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender53" runat="server" Enabled="True" TargetControlID="RangeValidator9">
                            </cc:ValidatorCalloutExtender>
                    </li>
               
                            
                    <li>
                            <asp:Label ID="Label15" runat="server" CssClass="label" Width="80px" Text="MRP"></asp:Label>
                            <asp:TextBox ID="txtMRP" onkeydown="EnterTab(event,'txtProfitPer');" CssClass="textboxMandatory" TabIndex="42"  Width="70px" runat="server" MaxLength="11" Style="text-align: right;" onKeyup="getSalesPrice(0);TaxAmount();" onKeypress="CheckNumWithDecimal1(event);">

                            </asp:TextBox>
                    </li>
                    <li>
                            <asp:Label ID="Label25" runat="server" CssClass="label" Width="80px" Text="Profit %"></asp:Label>
                            <asp:TextBox ID="txtProfitPer"  onkeydown="EnterTab(event,'txtProfitAmount');" CssClass="textboxMandatory" Width="70px"  TabIndex="43" runat="server" MaxLength="11" Style="text-align: right;"  onKeyup="getSalesPrice(1);TaxAmount(); " onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Profit % required" ControlToValidate="txtProfitPer" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender25" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator13">
                            </cc:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Profit % required" ControlToValidate="txtProfitPer" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender33" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator20">
                            </cc:ValidatorCalloutExtender>
                    </li>
                    <li>
                            <asp:Label ID="Label66" runat="server" CssClass="label" Width="80px" Text="Profit Amt"></asp:Label>
                            <asp:TextBox ID="txtProfitAmount" onkeydown="EnterTab(event,'txtDiscountPer');" CssClass="textbox" TabIndex="44"  Width="70px" runat="server" MaxLength="11" Style="text-align: right;"   onKeypress="CheckNumWithDecimal1(event);" onKeyup="getSalesPrice(3);TaxAmount();"></asp:TextBox>

                    </li>
            </div>
            <div style="float: left; width: 170px;">
                        <li>
                                <asp:Label ID="Label12" runat="server" CssClass="label" Width="80px" Text="Discount %"></asp:Label>
                                <asp:TextBox ID="txtDiscountPer" onkeydown="EnterTab(event,'txtDiscountAmt');" CssClass="textbox"  TabIndex="45" Width="70px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);" onKeyup="getSalesPrice(2);TaxAmount();"></asp:TextBox>
                               
                        </li>
                        <li>
                                <asp:Label ID="Label24" runat="server" CssClass="label" Width="80px" Text="Disc Amt" Height="16px"></asp:Label>
                                <asp:TextBox ID="txtDiscountAmt"  onkeydown="EnterTab(event,'ChkIncStax');" TabIndex="46" CssClass="textbox" Width="70px" runat="server"  MaxLength="11" Style="text-align: right;"   onKeypress="CheckNumWithDecimal1(event);" onKeyup="getSalesPrice(4);TaxAmount();"></asp:TextBox>
                               
                        </li>
                        <li>
                                          
                                <asp:Label ID="Label5" runat="server" Width="90px" CssClass="label" Text="Include STax"></asp:Label>
                                <asp:CheckBox ID="ChkIncStax"  onkeydown="EnterTab(event,'txtSTaxAmount');" TabIndex="47"  runat="server" Width="15px" onclick="TaxAmount();"   />
                                <asp:Button Text="STax"   ToolTip="SalesTax" ID="btSubmit" Style="padding: 0px; width:50px;height:18px; margin: 9px 0px 0px 6px; position: absolute" CssClass="button"  OnClick="btSubmit_Click" runat="server" /> 
                        
                        </li>

           
                        <li>
                                <asp:Label ID="Label8" runat="server" CssClass="label" Width="80px" Text="Tax Amount"></asp:Label>
                                <asp:TextBox ID="txtSTaxAmount" onkeydown="EnterTab(event,'txtSalesPrice');" TabIndex="48" CssClass="textbox"  Width="70px" runat="server" MaxLength="11" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>

                        </li>
                                    
                        <li>
                                <asp:Label ID="Label27" runat="server" CssClass="label" Width="80px" Text="Sales Price"></asp:Label>
                                <asp:TextBox ID="txtSalesPrice"  onkeydown="EnterTab(event,'btnAdd');" CssClass="textboxMandatory" Width="70px" TabIndex="49"  runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                <%--  <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtSalesPrice" SetFocusOnError="true" Type="Double" ControlToCompare="txtPurchaseRate" ValidationGroup="add" Display="None" ErrorMessage="Sales Price rate Should not be less  than  Purchase Rate" Operator="GreaterThanEqual"></asp:CompareValidator>
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
                                <li  class="btnMargin">
                                        <asp:Button Text="Insert" ID="btnAdd" runat="server"  OnClick="btnAdd_Click"   TabIndex="50" ValidationGroup="add" CssClass="button" />
                                        <asp:Button Text="Clear" ID="btnClearAdd" runat="server" CssClass="button"  TabIndex="51"  OnClick="btnClearAdd_Click" OnClientClick="ItemClear();" />
                                      
                                </li>
                               <%-- </div>--%>
                                        <li class="clear"></li>
                                        <li class="clear"></li>
                                <div class="divSeparation"></div>
            <div style="width: 870px; overflow: auto;  height: 150px; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">
                      

                                <li>
                                        
                                 
                                       
                                                <asp:GridView ID="gvItems" runat="server" CssClass="grid"  OnRowDataBound="gvItems_RowDataBound" Style="margin-left: 5px;"
                                                EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="920px"
                                                BackColor="White"  BorderStyle="Solid" BorderWidth="1px"   CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items"
                                                PageSize="10" OnPreRender ="gvItems_PreRender" OnSelectedIndexChanging="gvItems_SelectedIndexChanging" OnRowDeleting="gvItems_RowDeleting">
                                                <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                                <Columns>
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
                                                <asp:TemplateField HeaderText="SlNo" ItemStyle-HorizontalAlign="Right">
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
                                                <asp:TemplateField    HeaderText="Exp.Date">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="TextBox66" runat="server" Text='<%# Bind("ExpiryDate") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <div style="width: 80px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">

                                                <asp:Label ID="lblgvExpiryDate" runat="server" Text='<%# Bind("ExpiryDate") %>'></asp:Label>
                                                </div>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HSN">
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
                                                <asp:TemplateField HeaderText="PurTax.Amt">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtPurchasetax" runat="server" Text='<%# Bind("PurchasetaxAmount") %>' Style="text-align: right;"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <asp:Label ID="lblgvPurchaseTax" runat="server" Text='<%# Bind("PurchasetaxAmount") %>' Style="text-align: right; float: right;"></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PurDisc.Per">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtgvPurDiscPer" runat="server" Text='<%# Bind("PurchaseDiscPercentage") %>' Style="text-align: right;"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <asp:Label ID="lblgvPurDiscPer" runat="server" Text='<%# Bind("PurchaseDiscPercentage") %>' Style="text-align: right; float: right;"></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PurDisc.Amt">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtgvPurchaseDiscAmount" runat="server" Text='<%# Bind("PurchaseDiscAmount") %>' Style="text-align: right;"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <asp:Label ID="lblgvPurchaseDiscAmount" runat="server" Text='<%# Bind("PurchaseDiscAmount") %>' Style="text-align: right; float: right;"></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net.Pur">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtgvNetPurchase" runat="server" Text='<%# Bind("NetPurchase") %>' Style="text-align: right;"></asp:TextBox>

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

                                                <asp:TemplateField HeaderText="Prf% ">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtgvProfitPer" runat="server" Text='<%# Bind("ProfitPer") %>' Style="text-align: right;"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <asp:Label ID="lblgvProfitPer" runat="server" Text='<%# Bind("ProfitPer") %>' Style="text-align: right;float:right;"></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="ProAmt">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtgvProfitAmount" runat="server" Text='<%# Bind("ProfitAmount") %>' Style="text-align: right;"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <asp:Label ID="lblgvProfitAmount" runat="server" Text='<%# Bind("ProfitAmount") %>' Style="text-align: right;float:right"></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Disc%">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtgvDiscountPer" runat="server" Text='<%# Bind("DiscountPer") %>' Style="text-align: right;"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <asp:Label ID="lblgvDiscountPer" runat="server" Text='<%# Bind("DiscountPer") %>' Style="text-align: right;float:right"></asp:Label>
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
                                                <asp:TemplateField HeaderText="Sal.TaxAmt">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtgvSalestaxAmountg" runat="server" Text='<%# Bind("SaletaxAmount") %>' Style="text-align: right;"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <asp:Label ID="lblgvSalesTaxAmt" runat="server" Text='<%# Bind("SaletaxAmount") %>' Style="text-align: right; float: right;"></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sal.Amt">
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
                                                <div style="overflow: auto; width: 80px;">
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
                                                
                                                <asp:TemplateField HeaderText="Subunit" HeaderStyle-Width="30px">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtSub" runat="server" Text='<%# Bind("Subunit") %>'></asp:TextBox>
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
                                                <asp:TextBox ID="TextBox43229" runat="server" Text='<%# Bind("FK_Unit") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <asp:Label ID="lblgvFK_Units" runat="server" Text='<%# Bind("FK_Unit") %>'></asp:Label>
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
                                                <asp:TemplateField HeaderText="IncPtax" Visible="false">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtIncludeptax" runat="server" Text='<%# Bind("IncludePtax") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <asp:Label ID="lblgvIncludeptax" runat="server" Text='<%# Bind("IncludePtax") %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IncStax" Visible="false">
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtIncludeStax" runat="server" Text='<%# Bind("IncludeStax") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <asp:Label ID="lblgvIncludeStax" runat="server" Text='<%# Bind("IncludeStax") %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DisOnTax" Visible="false">
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
                                                <HeaderStyle  CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="30px" />
                                                <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                                </asp:GridView>

                                 </li>
            </div>
            <div class="divL" style="width: 190px;">

                        <li>
                                    <asp:Label ID="Label13" runat="server" CssClass="label" Width="75px" Text="Tot.Amt"></asp:Label>
                                    <asp:TextBox ID="txtToTotal" TabIndex="52" CssClass="textbox" Width="70px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;"  onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                        </li>
            </div>
            <div class="divL" style="width: 140px;display:none">

                        <li>
                                    <asp:Label ID="Label14" runat="server" CssClass="label" Width="60px" Text="Disc%"></asp:Label>
                                    <asp:TextBox ID="txtToDisc" CssClass="textbox" Width="60px" runat="server" onkeyup="return Total();" MaxLength="10" Style="text-align: right; font-weight: bold;"    onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                        </li>
            </div>
            <div class="divL" style="width: 140px;display:none">

                        <li>
                                    <asp:Label ID="Label17" runat="server" CssClass="label" Width="60px" Text="Dis.Amt"></asp:Label>
                                    <asp:TextBox ID="txtToDiscAmnt"  CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;"   onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                        </li>
            </div>
                           
                               
            <div style="float: left; width: 140px;">
                        <li>
                                    <asp:Label ID="Label19" runat="server" Width="60px" CssClass="label" Text="Rd.Off"></asp:Label>
                                    <asp:TextBox ID="txtToRoundOff" TabIndex="53" CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;"></asp:TextBox>
                        </li>


            </div>
            <div style="float: left; width: 170px;">


                        <li>
                                    <asp:Label ID="Label20" runat="server" Width="70px" CssClass="label" Text="Net.Amt"></asp:Label>
                                    <asp:TextBox ID="txtToNetAmount" TabIndex="54" CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right; font-weight: bold;"></asp:TextBox>
                        </li>


            </div>
                            </ul>
                        
                        

                        <div class="clear"></div>
                             
                        <%--<div id="div1" runat="server" style="display: none; height: 485px">



                            <ul class="contentul">
                                <div class="divL">
                                    <li>
                                        <label class="label">Supplier</label>
                                        <asp:DropDownList ID="ddlSeracSupplier" runat="server" CssClass="dropdown" TabIndex="30"></asp:DropDownList>
                                    </li>

                                    <li style="display:none">
                                        <label class="label">Store</label>
                                        <asp:DropDownList ID="ddlSearchStore" runat="server" CssClass="dropdown" TabIndex="31"></asp:DropDownList>

                                    </li>


                                    <br class="clear" />
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearchView" runat="server" CssClass="button" Text="Search"  OnClick="btnSearchView_Click" TabIndex="34" />
                                        <asp:Button ID="btnSerhClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="35" />
                                    </li>
                                </div>
                                <div class="divR">

                                    <li>
                                        <label class="label">From Date</label>
                                        <asp:TextBox ID="txtFromDate" CssClass="textbox" runat="server" TabIndex="32"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li>
                                        <label class="label">To Date</label>
                                        <asp:TextBox ID="txtTodate" CssClass="textbox" runat="server" TabIndex="33"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtTodate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                    </li>


                                </div>

                            </ul>
                            <br class="clear" />
                         
                        </div>--%>
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfuserName" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                        <asp:HiddenField ID="hfIsApprove" runat="server" />
                                        <asp:HiddenField ID="hfStore" runat="server" />
                                        <asp:HiddenField ID="hfPurchaseReturn" runat="server" />
                                        <asp:HiddenField ID="hfUnitQuantity" runat="server" />
                                        <asp:HiddenField ID="hfTotTaxPercentage" runat="server" />
                                        <asp:HiddenField ID="hfSubUnitQuantity" runat="server" />
                                        <asp:HiddenField ID="hfPurchaseValue" runat="server" />
                                        <asp:HiddenField ID="hfTotTaxsalesPercentage" runat="server" />
                                        <asp:HiddenField ID="hfpurrate" runat="server" />
                                        <asp:HiddenField ID="hfDate" runat="server" />
                                        <asp:HiddenField ID="hfExpirydays" runat="server" />
                                        <asp:HiddenField ID="hfSlNo" runat="server" />
                                        <asp:HiddenField ID="hfItemId" runat="server" />
                                        <asp:HiddenField ID="hfID_Replace" runat="server" />
                                        <asp:HiddenField ID="hfGvItemSelected" runat="server" />
                                        <asp:HiddenField ID="hdfFKStock" runat="server"   />
                                        <asp:HiddenField ID="hdfFkItem" runat="server"  />
                                        <asp:HiddenField ID="hfType" runat="server" />
                                        <asp:HiddenField ID="hfItem" runat="server"  />
                                        <asp:HiddenField ID="hdfItem" runat="server"  />
                                        <asp:HiddenField ID="hfTotalDiscAmnt" runat="server"  />
                                        <asp:HiddenField ID="hfMasterID" runat="server"  />
                                        <asp:HiddenField ID="hfFK_PurchaseReturn" runat="server"  />
                                        <asp:HiddenField ID="HfItemsID" runat="server"  />
                                       <asp:HiddenField ID="hfItemName" runat="server"  />
                                        <asp:HiddenField ID="hfFromTotTaxPercentage" runat="server" />
                                        <asp:HiddenField ID="hfFromTotTaxsalesPercentage" runat="server" />
                             <asp:HiddenField ID="hdfCurrentStock" runat="server" />
                            
                                        <asp:Button ID="btnItem" runat="server" Text="Button" OnClick="btnItem_Click" CausesValidation="false" Style="display: none;" />
                <li class="btnMargin">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="button" TabIndex="55"  OnClick="btnSubmit_Click"  Text="Submit" ValidationGroup="submit" />
                                <asp:Button ID="btnClear" Text="Clear" runat="server" TabIndex="56"  OnClick="btnClear_Click" OnClientClick="ClearAdd();ItemClearPurRet(); " CausesValidation="false" CssClass="button" />
                                <asp:Button ID="btnApprove" Text="Approve" runat="server" TabIndex="57"  Visible="false"  OnClick="btnApprove_Click"    CausesValidation="false" CssClass="button" />
                </li>
                        </div>
                            </div>
                        <div class="main_bg">  
                                <div id="divView" runat="server" style="display: none; height: 485px">

                                            <ul class="contentul">
                                                            <%--<div class="divL">
                                                                    <li>
                                                                            <label class="label">Supplier</label>
                                                                            <asp:DropDownList ID="ddlSearchSupplier" runat="server" CssClass="dropdown" TabIndex="59"></asp:DropDownList>
                                                                    </li>

                                                                            <br class="clear" />
                                                                        <li class="btnMargin">
                                                                                    <asp:Button ID="btnSearch" runat="server" TabIndex="60" CssClass="button" Text="Search" OnClick="btnSearch_Click"  />
                                                                                    <asp:Button ID="btnSearchClear" runat="server" TabIndex="61" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;"  />
                                                                        </li>
                                                            </div>--%>
                                                 <div class="divL">
                                                     <li>
                                        <label class="label">From Date</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtFromDateSearch" onkeydown="return EnterTab(event,'txtToDateSearch');" TabIndex="59" AutoComplete="off" />
                                        <cc:CalendarExtender ID="txtDate_CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDateSearch" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                       
                                                    </li>

                                         <li>
                                            <label class="label">Supplier</label>
                                               <asp:DropDownList ID="ddlSearchSupplier" runat="server" CssClass="dropdown" onkeydown="return EnterTab(event,'ddlStoreSearch');" TabIndex="61"></asp:DropDownList>
                                           <%-- <asp:DropDownList ID="ddlSearchSupplier" runat="server" CssClass="dropdown" TabIndex="61"></asp:DropDownList>--%>
                                          </li>


                                    <li>
                                        <label class="label">Status</label>
                                        <asp:DropDownList runat="server"  CssClass="dropdown" ID="ddlStatusApproveSearch" TabIndex="63">
                                            <asp:ListItem Text="select" Value="2" />
                                            <asp:ListItem Text="Approved" Value="1" />
                                            <asp:ListItem Text="Pending" Value="0" />
                                        </asp:DropDownList>
                                    </li>
                                   
                                    <li class="clear"></li>
                                    <li class="btnMargin">
                         
                            <asp:Button ID="btnSearch" runat="server" TabIndex="63" CssClass="button" Text="Search" OnClick="btnSearch_Click"  />
                            <asp:Button ID="btnSearchClear" runat="server" TabIndex="64" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;"  />
                                                                    

                                    </li>
                                </div>
                                <div class="divR">
                                     <li>
                                        <label class="label">To Date</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtToDateSearch" onkeydown="return EnterTab(event,'ddlSearchSupplier');" TabIndex="60" AutoComplete="off" />
                                        <cc:CalendarExtender ID="txtDate_CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtToDateSearch" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Date required" ControlToValidate="txtToDateSearch" Display="None" SetFocusOnError="True" ></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender58" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator32">
                                    </cc:ValidatorCalloutExtender>--%>
                                    </li>
                                    <li>
                                        <label class="label">Store</label>
                                        <asp:DropDownList ID="ddlStoreSearch" TabIndex="62" onkeydown="return EnterTab(event,'ddlStatusApproveSearch');" runat="server" CssClass="dropdown"></asp:DropDownList>
                                    </li>
                                     
                      

                                </div>
                                            </ul>
                                         <ul class="contentul">
                                            <asp:GridView ID="grdReplace" runat="server" CssClass="grid" Style="margin-left: 1px;" Width="95%"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                            BackColor="White"  BorderStyle="Solid" BorderWidth="1px"  OnRowDataBound="grdReplace_RowDataBound" CellPadding="3" AllowPaging="true" ForeColor="Black" GridLines="Vertical" PageSize="10"  OnPageIndexChanging="grdReplace_PageIndexChanging" DataKeyNames="ID_Replace" OnSelectedIndexChanging="grdReplace_SelectedIndexChanging" OnRowDeleting="grdReplace_RowDeleting" OnRowCommand="grdReplace_RowCommand">
                                            <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                            <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                                            <asp:BoundField DataField="BillDate" HeaderText="Date" />

                                            <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                                            <asp:BoundField DataField="Store" HeaderText="Store" />

                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                            <asp:Button ID="btnApprovedStatus" CssClass="button" Width="75px" Text='<%# Bind("ApprovedStatus") %>' runat="server" OnClientClick="ConfirmPurchaseApprove();" CommandName="ApprovedStatus" />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server"  CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton5" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" OnClick="ImageButton5_Click" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                            <%--  <asp:Button ID="btnPrint" CssClass="button" Width="75px" Text="Print" runat="server" OnClientClick="ConfirmPurchasePrint();" CommandName="Print" />--%>
                                            <asp:ImageButton ID="ImageButton343" runat="server" CommandName="Print" ToolTip="Print"  ImageUrl="~/images/1430738477_report.png" Width="20px" Height="20px" />
                                             
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
                                            <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                            </asp:GridView>
                            
                                </ul>
                                            <br class="clear" />
                    
                                </div>
                    </div>
                        <div class="clear"></div>
                        
      <asp:Button ID="btnSt" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                                <asp:Panel ID="divPopUpdelete" runat="server" Style="display: none;" CssClass="DivStyle">
                                            <div id="divPopheader" runat="server" class="PopupHeaderReason">
                                                <asp:Label ID="lblPopupHeader1" runat="server" Text="Reason"></asp:Label>
                                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                            </div>
                                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                                            <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                                                </div>
                                </asp:Panel>
                                <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divPopUpdelete" TargetControlID="btnSt"
                                    CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divPopheader">
                                </cc:ModalPopupExtender>
                </div>
      <asp:Button ID="btS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                                <asp:Panel ID="divPop" runat="server" Style="display: none;" CssClass="DivStyle" >
                                                <div id="divhead" runat="server" class="PopupHeader">
                                                    <asp:Label ID="lblPopupH" runat="server"  CssClass="lblPopupHeader"></asp:Label>
                                                    <asp:ImageButton ID="btnClse" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                                </div>

                                                    <uc3:CommonPopupGrid  ID="CommonPopupGrid1" runat="server" />
                                                         <cc:ModalPopupExtender ID="mpGrid" runat="server" PopupControlID="divPop" TargetControlID="btS"
                                                    CancelControlID="btnClse" Drag="true" PopupDragHandleControlID="divhead">
                                                </cc:ModalPopupExtender>
                                </asp:Panel>
       <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                                <asp:Panel ID="divPopup" Style="display: none;" runat="server" CssClass="DivStyle">
                                        <div id="divheader" runat="server" class="PopupHeader">
                                                <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head" CssClass="lblPopupHeader"></asp:Label>
                                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                        </div>

                                                 <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                                </asp:Panel>
                       
                                        <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
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
                        <div style="display:none">
                                    <asp:TextBox ID="txtKey" runat="server" ></asp:TextBox>
                        </div>


                    </div>
              <%--  </div>
                </div>--%>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
