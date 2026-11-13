<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Quatation.aspx.cs" Inherits="Quatation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<%@ Register src="Popup/CommonPopup.ascx" tagname="CommonPopup" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
               <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
       <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
        <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
      <script src="js/Custom.js"></script>
      <script src="js/Navigation.js"></script>
        <script src="js/toastr.js"></script>
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />

       <script type="text/javascript">
           var submit = 0;
           function pageLoad() {
               $("#<%#chkPercentage.ClientID%>").change(function () {
                      if (this.checked) {

                          ValidatorEnable($("[id$='rfvfrequantity']")[0], true);
                          ValidatorEnable($("[id$='Validatorquantiy']")[0], true);
                      }
                      else {
                         

                      }
                  });
           }
           function handleChange(input) {

               if (chkPercentage.checked) {
                   if (input.value < 0) input.value = 0;
                   if (input.value > 99.99) input.value = 99.99;
               }
           }
      
           function disablebutn() {
               try {

                   window.setTimeout('delaysubmit()', 50);
               }
               catch (e) {
                   alert(e);
               }
           }
           function popUpQuotationComparisonSelect() {

               try {
                   var ItemName = $("#txtItem").val();
                   var GroupName = $("#ddlGroup").val();
                  // alert(ItemName); alert(GroupName);
                   if (ItemName == "") {
                       alert('Please Select  item');
                       return false;
                   }
                   if (GroupName == "0") {

                       alert('Please Select  Group');
                       return false;
                   }
                   var hfUser = $("#hfUser").val();
                   var hfClient = $("#hfClient").val();
                   var ddlCategory = $("#ddlGroup").val();
                   var ddlSupplier = "0";
                   var txtInvoiceDate = "";
                   var hfItemvalue = $("#hfSelectedItem").val();
                   var ItemType = "2";

                   $.ajax({
                       type: "POST",
                       url: 'Textile.asmx/GetUrlEncrypt',
                       data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','ddlSupplier':'" + ddlSupplier + "','txtInvoiceDate':'" + txtInvoiceDate + "','hfItemvalue':'" + hfItemvalue + "','hfClient':'" + hfClient + "','ItemType':'" + ItemType + "'}",
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function (MenuHtml) {


                           url = "QuotationComparison.aspx?" + MenuHtml.d;

                           //url = "QuotationComparison.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlGroup").val() + '&ID=' + $("#hfSelectedItem").val()

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



                 
                   
                  
                   //$("#ddlUnit").focus();
               }
               catch (e) {
                   alert(e);
               }

           }

           function releasebtn() {


               $("#btnSubmit").prop('disabled', false);

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
           function ClearAdd() {
               submit = 0;

               $("#<%#hfItemsId.ClientID%>").val('0');
               $("#<%#txtDescrption.ClientID%>").val('');
               $("#<%#ddlGroup.ClientID%>").val('0');
               $("#<%#ddlType.ClientID%>").val('0');
               $("#<%#txtDate.ClientID%>").val('');
               $("#<%#txtEffectTo.ClientID%>").val('');
                <%-- $("#<%#txtShortName.ClientID%>").val('');--%>
               <%-- $("#<%#hfBinID.ClientID%>").val('0');--%>
               ItemClear();

           }
           function ItemClear() {
               $("#<%#txtItem.ClientID%>").val('');
               $("#<%#txtRate.ClientID%>").val(0);
               $("#<%#ddlUnit.ClientID%>").val('0');
               $("#<%#ddlSubUnit.ClientID%>").val('0');
               $("#<%#txtFreequantity.ClientID%>").val(0);
               $("#<%#TxtPer.ClientID%>").val(0);
               $("#<%#txtDiscount.ClientID%>").val(0);
               $("#<%#txtPereD.ClientID%>").val(0);
               $("#chkFrequent").prop("checked", false);
               $("#chkPercentage").prop("checked", false);
               $("#chkFr").prop("checked", false);
               $("#chkFrequent").prop("disabled", false);
               $("#chkPercentage").prop("disabled", false);
               $("#chkFr").prop("disabled", false);
               $("#chkIsSupplier").prop("checked", false);

               
               
           }
            function ClearSearch() {

                $("#<%#txtSearchName.ClientID%>").val('');
                $("#<%#ddlSearchGroup.ClientID%>").val('0');
                $("#<%#txtSearchName.ClientID%>").val('');

            }
            function ShowAddDiv() {
                
               
                $("#<%#divView.ClientID%>").hide();
                $("#<%#divAdd.ClientID%>").show();
                $("#<%#ddlGroup.ClientID%>").focus();
               <%-- $("#<%#txtBin.ClientID%>").focus();--%>
            }
           function pageLoad() {

               var ctrContainer = "form";
               setKeynavigation(ctrContainer);

           }

           function popUpSupplierNameSelectSelect() {

               try {
                   // var Cat = $("#ddlCategory").val();

                   var ItemName = $("#txtItem").val();

                   if (ItemName.length > 0) {
                       $("#ImageButton4").click();
                   }
                   else {
                       //    ItemName = "";
                       //    url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val();

                       //    popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                       //    ",statusbar=no,menubar=no,resizable=0,width=1000," +
                       //    "height=600,left = 200,top = 100");
                       //    popUpObj.focus();


                   }
                   //  $("#txtItemShortName").focus();
               }
               catch (e) {
                   alert(e);
               }

           }
            function EnableChecBox(checked) {
                if (chkPercentage.checked) {
                    
                    $('chkFr').disabled = true;
                  
                  //  $("#chkFr").prop('disabled', true);
                   // document.getElementById("chkFr").disabled = true
                } else {
                    $("#chkFr").prop('disabled', false);
                }
            }
            function oncheckchange(checked) {
              //  alert('g');
                var chkFr = document.getElementById('chkFr');
                chkFr.disabled = checked;
                if (chkPercentage.checked) {
                    if (txtDiscount.value < 0) txtDiscount.value = 0;
                    if (txtDiscount.value > 99.99) txtDiscount.value = 99.99;
                }
                //alert('l');
                //(chkFr);
            }
         
         
            function PopupOnReason() {
                try {


                    $("#divPopupReason").css('display', 'block');
                }
                catch (ex) {

                }
            }
            function PopupOnBin() {
                try {

                  
                    $("#divPopupBin").css('display', 'block');
                }
                catch (ex) {
                   
                }
            }
           
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
 <%--  <form id="form1" runat="server" enctype="multipart/form-data" autocomplete="off">--%>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="btn_bg5"> 
                     <div class="heading2">
                                 <div class="user">Quotation</div>
                          <asp:Panel ID="divMenuButtons" runat="server">
                  <div style="float: right; margin-top: -10px">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add.png"  CssClass="imagebuttonStyle"      TabIndex="22" runat="server"  OnClientClick="ShowAddDiv(); releasebtn(); "  OnClick="ImageButton1_Click"  />
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle"      ToolTip="Veiw" runat="server"  OnClick="imgbtnView_Click" TabIndex="21" />
                        </div>
                              </asp:Panel>
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                         <div class="main_bg">  
                        <div id="divAdd" runat="server" style="display: block;">
                                <ul class="contentul">
                                    <div class="divL" style="width: 430px;">
                                       
                                            <li>
                                                <asp:Label ID="Label39" runat="server" CssClass="label" Width="80px" Text="Group"></asp:Label>
                                                <asp:DropDownList ID="ddlGroup" runat="server"   AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" Width="200px" CssClass="dropdownMandatory" TabIndex="1">
                                                  <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                      <asp:ListItem Text="Supplier" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Customer" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqgroup" runat="server" ErrorMessage="Select a Group" ControlToValidate="ddlGroup" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="reqgroup">
                                                </cc:ValidatorCalloutExtender>
                                               <asp:RequiredFieldValidator ID="reqG" runat="server" ErrorMessage="Select a Group" ControlToValidate="ddlGroup" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="AddC"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="reqG">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                            <li>

                                        <asp:Label ID="Label56" runat="server" CssClass="label" Width="80px" Text="Effect From"></asp:Label>
                                                  <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtDate" Width="190px" TabIndex="3" AutoComplete="off" />
                                            <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate"   Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                             <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                             <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                             </cc:ValidatorCalloutExtender>

                                            </li>
                                        </div>
                                        <div class="divL" style="width: 430px;">
                                       
                                                   <li>
                                                <asp:Label ID="Label18" runat="server" Width="80px" CssClass="label" Text="Type"></asp:Label>
                                                <asp:DropDownList ID="ddlType"  Width="185px" runat="server" CssClass="dropdownMandatory" TabIndex="2"></asp:DropDownList>
                                                           <asp:RequiredFieldValidator ID="reqtype" runat="server" ErrorMessage="Select a Type" ControlToValidate="ddlType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="reqtype">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                            
                                               <li>
                                                  <asp:Label ID="Label10" runat="server" Width="80px" CssClass="label" Text="Effect To"></asp:Label>
                                            
                                             <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtEffectTo" TabIndex="4" Width="175px" AutoComplete="off" />
                                            <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtEffectTo" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                             <asp:RequiredFieldValidator ID="reqTopdate" runat="server" ErrorMessage="Date required" ControlToValidate="txtEffectTo" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                             <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="reqTopdate">
                                             </cc:ValidatorCalloutExtender>
                                                     <asp:CompareValidator ID="cmpDate" runat="server" ErrorMessage="ToDate must be greater than or equal to FromDate" Display="None" ControlToCompare="txtDate"
                                                    ControlToValidate="txtEffectTo" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="cmpDate">
                                                </cc:ValidatorCalloutExtender>
                                             </li>
                                        </div>
                                     <li class="clear"></li>
                                    <br />
                                      <div class="divSeparation"></div>
                                        <div class="divL" style="width: 430px;">
                                              <li>
                                        <asp:Label ID="Label9" runat="server" CssClass="label" Width="80px" Text="Item"></asp:Label>
                                        <asp:TextBox ID="txtItem" runat="server" AutoPostBack="false" CssClass="textboxMandatory" onBlur="popUpSupplierNameSelectSelect();" TabIndex="5" Width="160px" MaxLength="50"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton4" OnClick="ImageButton4_Click" runat="server" TabIndex="6" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select a Item" ControlToValidate="txtItem" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>
                                            <asp:RequiredFieldValidator ID="reqi" runat="server" ErrorMessage="Select a Item" ControlToValidate="txtItem" Display="None" SetFocusOnError="True" ValidationGroup="AddC"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="reqi">
                                        </cc:ValidatorCalloutExtender>

                                    </li>
                                            <li>
                                                <asp:Label ID="Label43" Text="Rate" CssClass="label" Width="80px" runat="server" />
                                                <asp:TextBox runat="server" ID="txtRate" TabIndex="7" Style="text-align: right;" Width="190px" onKeypress="CheckNumWithDecimal(event);"  CssClass="textboxMandatory" MaxLength="18" />
                                                <asp:RequiredFieldValidator ID="reqRate" runat="server" ErrorMessage="Select Rate" ControlToValidate="txtRate" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="reqRate">
                                                </cc:ValidatorCalloutExtender>
                                     
                                            </li>
                                             <li>
                                                <asp:Label ID="Label12" runat="server" CssClass="label" Width="80px" Text="Unit"></asp:Label>
                                                <asp:DropDownList ID="ddlUnit"  runat="server"  Width="198px"  CssClass="dropdownMandatory"   TabIndex="8"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select a Unit" ControlToValidate="ddlUnit" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                             <li>
                                                <asp:Label ID="Label37" runat="server" CssClass="label" Width="80px" Text="Sub Unit"></asp:Label>
                                                <asp:DropDownList ID="ddlSubUnit"  Width="198px" runat="server" CssClass="dropdown" TabIndex="9"></asp:DropDownList>
                                               
                                            </li>
                                          
                                        </div>
                                     
                                            <div class="divL" style="width: 370px;">
                                    
                 
                                                  
                                                   <li>
                                                <div style="display:flex;">
                                                <div style="float: left;width: 50%;">
                                               <asp:Label ID="Label1" runat="server" CssClass="label" Width="80px" Text="Free Quantity"></asp:Label>
                                                 <asp:TextBox ID="txtFreequantity" runat="server" AutoPostBack="true"  Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);" CssClass="textbox" ValidationGroup="Add" TabIndex="10"  Width="80px" MaxLength="7" OnTextChanged="txtFreequantity_TextChanged" ></asp:TextBox>
                                                  <asp:RequiredFieldValidator ID="rfvfrequantity" runat="server" ErrorMessage="Credit Account Head Refno required" Enabled="false" ControlToValidate="txtFreequantity"  Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                              <cc:ValidatorCalloutExtender ID="Validatorquantiy" runat="server" Enabled="True" TargetControlID="rfvfrequantity">
                                                  </cc:ValidatorCalloutExtender>
                                                 </div>
                                                    <div style="float: left;width: 50%;">
                                                     <asp:Label ID="Label2" runat="server" CssClass="label" Width="22" Text="Per"></asp:Label>
                                                 <asp:TextBox ID="TxtPer" runat="server" AutoPostBack="true" CssClass="textbox"  Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);" ValidationGroup="Add" TabIndex="11"  Width="50px" MaxLength="5"></asp:TextBox>
                                              
                                                </div></div>
                                            </li>
                                                      <li>
                                               <asp:Label ID="lblFrequent" runat="server" CssClass="label" Text="Frequent"></asp:Label>
                                                    <asp:CheckBox ID="chkFrequent" runat="server" TabIndex="12" />
                                               
                                            </li>
                                
                                            <li>
                                                <div style="display:flex;">
                                                <div style="float: left;width: 50%;">
                                               <asp:Label ID="Label3" runat="server" CssClass="label" Width="80px" Text="Discount"></asp:Label>
                                                 <asp:TextBox ID="txtDiscount"  runat="server" AutoPostBack="true" CssClass="textbox" ValidationGroup="Add" TabIndex="14" Width="80px"  Style="text-align: right;" onchange="handleChange(this)" onKeypress="CheckNumWithDecimal(event);" MaxLength="7"  ></asp:TextBox>
                                                
                                           <%--   <asp:RangeValidator ID="reqDiscount" runat="server" ControlToValidate="txtDiscount" ErrorMessage="Enter a valid Discount" Display="None" MaximumValue="99" ValidationGroup="Add" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                               <cc:ValidatorCalloutExtender ID="reqDiscountV" runat="server" Enabled="True" TargetControlID="reqDiscount">
                                            </cc:ValidatorCalloutExtender>--%>  
                                                 </div><div style="float: left;width: 50%;">
                                                     <asp:Label ID="Label8" runat="server" CssClass="label" Width="22" Text="Per"></asp:Label>
                                                 <asp:TextBox ID="txtPereD" runat="server" AutoPostBack="true"  Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);" CssClass="textbox"  ValidationGroup="Add" TabIndex="15" Width="50px"  MaxLength="5"></asp:TextBox>
                                                 
                                                </div></div>
                                            </li>
                                                           <li>
                                               <asp:Label ID="lblFreqnt" runat="server" CssClass="label" Text="Frequent"></asp:Label>
                                                    <asp:CheckBox ID="chkFr" AutoPostBack="true" OnCheckedChanged="chkFr_CheckedChanged" runat="server" ClientIDMode="Static" TabIndex="16"  />
                                                
                                            </li>
                                                                                                      <li>
                                             
                                              <asp:Label ID="Label4" runat="server" CssClass="label" Text="Percentage"></asp:Label>
                                                <asp:CheckBox   ID="chkPercentage"  OnClick="oncheckchange(this.checked);" runat="server" TabIndex="13"  OnCheckedChanged="chkPercentage_CheckedChanged" AutoPostBack="true" />
  </li>
           
                                                                   <li>
                                                   <asp:Label ID="lblSupplierOnly" runat="server" CssClass="label" Text="Lock Item With Supplier"></asp:Label>
                                                    <asp:CheckBox ID="chkIsSupplier" AutoPostBack="true" runat="server" ClientIDMode="Static" TabIndex="16"  />
                                                
                                            </li>
                      
                                                </div>
                                    <div class="clear"></div>
                                    <br />
                                         <%--  <div class="divL" style="width: 370px;padding-right:30px;" >--%>
                                       
                                    <li class="btnMargin"style="padding-left: 85px;">
                                       
                                                <asp:Button ID="btnAdd" runat="server" CssClass="button" ValidationGroup="Add" Width="80" Text="Insert" TabIndex="17"   OnClick="btnAdd_Click" />
                                           
                                        <asp:Button ID="btnComparison" CssClass="button" ValidationGroup="AddC" OnClientClick="return  popUpQuotationComparisonSelect();"  Text="Comparison" runat="server" />
                                    </li>
                                              <%-- </div>--%>

                                    <div class="Border" style="width:450px">
                                        <br />
                                        <div style="padding-left: 10px;">
                                            
                                            <div style="width:835px; overflow: auto; height: 150px; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">
                                                <li>
                                                    <asp:GridView ID="gvItemsdetails" runat="server" CssClass="grid"
                                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                                        BackColor="White"  BorderStyle="Solid" OnRowDeleting="gvItemsdetails_RowDeleting" BorderWidth="1px" Width="900px" CellPadding="3" ForeColor="Black"
                                                        GridLines="Vertical" DataKeyNames="FK_item">
                                                        <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" >
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ItemName" HeaderText="Items" />
                                                            <asp:BoundField DataField="FK_item" HeaderText="FK_Items" Visible="false" />
                                                            <asp:BoundField DataField="Rate"  ItemStyle-HorizontalAlign="Right" HeaderText="Rate" >
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UnitName" HeaderText="Unit" />
                                                            <asp:BoundField DataField="SubUnitName" HeaderText="Sub.Unit" />
                                                            <asp:BoundField DataField="FreeQuantity"  ItemStyle-HorizontalAlign="Right" HeaderText="Free Qty" >
                                                             <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                             <asp:BoundField DataField="FreeQuantityPer"  ItemStyle-HorizontalAlign="Right" HeaderText="Free QtyPer" >
                                                              <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                              <asp:BoundField DataField="Frequent" HeaderText="Frequent" />
                                                             <asp:BoundField DataField="PercentageWise" HeaderText="Per.Wise" />
                                                              <asp:BoundField DataField="Discount"  ItemStyle-HorizontalAlign="Right" HeaderText="Disc" >
                                                             <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                             <asp:BoundField DataField="DiscountPer" HeaderText="Dis.Per" ItemStyle-HorizontalAlign="Right" />
                                                             <asp:BoundField DataField="FrequentPer" HeaderText="Fqnt.Per" />
                                                             <asp:BoundField DataField="SupplierOnly" HeaderText="Supplier only" />
                                                
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <FooterStyle BackColor="#CCCCCC" />
                                                        <HeaderStyle CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
                                                        <PagerStyle   CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                                    </asp:GridView>
                                                </li>
                                            </div>

                                            <br />
                                        </div>
                                    </div>
                                    <li class="clear"></li>
                                 
                              <%--  <ul class="contentul">--%>

                                      <div class="divL">
                                      
                                         <li>
                                        <asp:Label ID="Label15" runat="server" CssClass="label"  Width="80px" Text="Description"></asp:Label>
                                        <asp:TextBox ID="txtDescrption" CssClass="multiline"  TextMode="MultiLine" TabIndex="18" runat="server"></asp:TextBox>
                                    </li>

                                       
                                       
                                    </div>
                                    <li class="clear"></li>
                                      <div class="clear"></div>
                             <%--      
                                </ul>--%>
                           
                                    <li class="btnMargin"style="padding-left: 85px;">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="19" ValidationGroup="submit"  OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />

                                        <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="20" OnClientClick="ClearAdd();"  OnClick="btnClear_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnBarcode" runat="server" CssClass="button" Text="Barcode" TabIndex="48" Style="display: none;" OnClientClick="return BarcodeGenerate();" />
                                        <asp:HiddenField ID="hfItemsId" runat="server" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                        <asp:HiddenField ID="hfSelectedItem" runat="server" />
                                        <asp:HiddenField ID="hfSelectedTax" runat="server" />
                                         <asp:HiddenField ID="hfuserName" runat="server" />
                                     <asp:HiddenField ID="hfMasterID" runat="server" />
                                        <%--<asp:Button ID="btnUnitBlurTrigger" Text="text" runat="server" Style="display: none;" OnClick="btnUnitBlurTrigger_Click" />--%>
                                    
                                    </li>

                                </ul>

                            

                       
                        </div>
                           </div>
                       <div class="main_bg">  
                        <div id="divView" runat="server" style="display: none;">
                            <div class="divL">
                                <ul class="contentul">
                                  
                                    <li>
                                        <li>
                                                <asp:Label ID="Label6" runat="server" CssClass="label"  Text="Group"></asp:Label>
                                                <asp:DropDownList ID="ddlSearchGroup" runat="server"  CssClass="dropdown" TabIndex="23">
                                                    <asp:ListItem Text="Supplier" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Customer" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                          <li>
                                            <asp:Label ID="Label7" runat="server" CssClass="label" Text="Search Name"></asp:Label>
                                            <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" TabIndex="24"></asp:TextBox>
                                        </li>
                                       
                                       



                                        <li class="btnMargin">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="EnableExport();"  OnClick="btnSearch_Click" TabIndex="25" />
                                            <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" TabIndex="26" OnClientClick="ClearSearch();" />
                                        </li>
                                    </li>
                                </ul>
                            </div>
                            <li class="clear"></li>
                            <ul class="contentul">
                                <asp:GridView ID="gvItems" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AllowPaging="true" OnPageIndexChanging="gvItems_PageIndexChanging" OnSelectedIndexChanging="gvItems_SelectedIndexChanging"  OnRowDeleting="gvItems_RowDeleting" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="GroupID">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="Mastername" HeaderText="Name" />
                                        <asp:BoundField DataField="EffectFrom" HeaderText="EffectFrom" />
                                        <asp:BoundField DataField="EffectTo" HeaderText="EffectTo" />
                                       <%-- <asp:BoundField DataField="Model" HeaderText="Model" />
                                        <asp:BoundField DataField="Unit" HeaderText="Unit" />--%>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                        </div>
                        </div>

                    </div>

               


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


                    <asp:Button ID="btnS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopUpdelete" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divPopheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader1" runat="server" Text="Reason" ></asp:Label>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                            <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                                </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divPopUpdelete" TargetControlID="btnS"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divPopheader">
                        </cc:ModalPopupExtender>
                         <asp:Button ID="btnP" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                            <asp:Panel ID="pnlPrvlg" runat="server" Style="display: none;" CssClass="DivStyle">
                                <div id="divPrvlg" runat="server" class="PopupHeader">
                                    <asp:Label ID="Label5" runat="server" Text="Privilege" CssClass="lblPopupHeader"></asp:Label>
                                    <asp:ImageButton ID="btnCP" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                </div>
                                <uc1:CommonPopupPrivilege runat="server" ID="CommonPopupPrivilege" />
                            </asp:Panel>
                            <cc:ModalPopupExtender ID="mpPrivilege" runat="server" PopupControlID="pnlPrvlg" TargetControlID="btnP"
                                CancelControlID="btnCP" Drag="true" PopupDragHandleControlID="divPrvlg">
                            </cc:ModalPopupExtender>


            </ContentTemplate>
        </asp:UpdatePanel>
         </div>
               
    </form>
</body>
</html>
