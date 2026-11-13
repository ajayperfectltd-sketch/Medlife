<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PriceCorrection.aspx.cs" Inherits="PriceCorrection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopUpGridSelection.ascx" TagName="CommonPopupGrid" TagPrefix="uc3" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PriceCorrection</title>
            <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <script src="js/Navigation.js"></script>
     <script src="js/jquery-1.10.2.js"></script>
      <script src="js/Custom.js"></script>
    <script src="AssetLogin/js/jquery.min.js"></script>
      <script type="text/javascript">
          function popUpStockSelectClick() {

              if (Page_ClientValidate("img") == true) {

                  var popup;
                  var ItemName = $("#txtitem").val();
                  //if ($("#hfStore").val() == '') {
                  SetStoreDetail();
                  // BlurStore();
                  // $("#hfStore").val($("#ddlStore").val())
                  //   }

                  popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCat").val(), "Popup", "width=1000,height=800");
                  popup.focus();

              }

              return false;
          }
          function ConfirmApprove() {

              var confirm_value = document.createElement("INPUT");
              confirm_value.type = "hidden";
              confirm_value.name = "confirm_value";
              if (confirm("Do you want to Approve data?")) {
                  confirm_value.value = "Yes";
              } else {
                  confirm_value.value = "No";
              }
              document.forms[0].appendChild(confirm_value);
          }
          function PopupOnReason() {
              try {

                  $("#divPopupReason").css('display', 'block');

              }
              catch (ex) {

              }
          }
          function ClearAdd() {
              try {
                  var dt = new Date();
                  $("#hfDate").val(dt.format("dd/MM/yyyy"));
                  $("#txtDate").val(dt.format("dd/MM/yyyy"));
                  $("#txtDate").focus();
                  ItemClear()

              }
              catch (ex) {

              }
          }

          function ItemClear() {


              $("#hfItemId").val("");
              $("#ddlItem").val("0");
              $("#ddlCat").val("0");
              var toDate = new Date();
              $("#txtDate").val(toDate.format("dd/MM/yyyy"));
              $("#txtMRP").val("0");
              $("#txtQuantity").val("");
              $("#txtSalesPrice").val("0");
              $("#txtNewSalePrice").val("");
              $("#txtitem").val("");
              $("#txtQ").val("");
              $("#hdfItem").val("");
              $("#hfID_Price").val("");





          }
          function SearchClear() {

              var dt = new Date();

              $("#txtFromDateSearch").val(dt.format("dd/MM/yyyy"));
              $("#txtToDateSearch").val(dt.format("dd/MM/yyyy"));
              var value = $("#hfStore").val() == "" ? "0" : $("#hfStore").val();
              $("#ddlSearchStore").val(value);


          }
          var popup;
          function popUpStockSelectClick(p) {
              $("#hfType ").val(p);
              if (Page_ClientValidate("img") == true) {

                  var popup;
                  if (p == 1) {
                      var ItemName = $("#txtitem").val();
                  }
                  else {
                      var ItemName = $("#txtItmShortName").val();

                  }

                  SetStoreDetail();

                  if (p == 1) {

                      var ItemName = $("#txtitem").val();
                      var hfUser = $("#hfUser").val();
                      var hfClient = $("#hfClient").val();
                      var ddlCategory = $("#ddlCat").val();
                      var hfStore = $("#hfStore").val();
                      var txtSupRefno = $("#txtSupRefno").val();
                      $.ajax({
                          type: "POST",
                          url: 'Textile.asmx/GetUrlEncryptStock',
                          data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','txtSupRefno':'" + txtSupRefno + "','hfStore':'" + hfStore + "','hfClient':'" + hfClient + "'}",
                          contentType: "application/json; charset=utf-8",
                          dataType: "json",
                          success: function (MenuHtml) {

                              SetStoreDetail();
                              url = "StockItems.aspx?" + MenuHtml.d;
                              popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                              ",statusbar=no,menubar=no,resizable=0,width=1000," +
                              "height=800,left = 200,top = 100");
                              popUpObj.focus();

                          },
                          error: function (MenuHtml) {

                          }
                      });
                      // popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCat").val(), "Popup", "width=1000,height=800");
                      // popup.focus();
                  }
                  else {
                      url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName;

                      popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=yes,location=no" +
                      ",statusbar=no,menubar=no,resizable=0,width=1015," +
                      "height=650,left = 200,top = 100");
                      popUpObj.focus();

                  }


              }

              return false;
          }
          function SetStoreDetail() {
              var store = $("#ddlStore option:selected").val();

              $("#hfStore").val(store);



          }
           </script> 
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="frmPriceCorrection" runat="server" autocomplete="off">
             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"  runat="server" >
              <ContentTemplate>
                   <div class="btn_bg5"> 
                      <div class="heading2">
                           <div class="user">Price Correction</div>
                    <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                      
                        <asp:ImageButton ID="ImageButton1"  ImageUrl="images/ADD.png" CssClass="imagebuttonStyle"  OnClick="ImageButton1_Click"  TabIndex="13"   runat="server" CausesValidation="false" OnClientClick="ClearAdd();ShowAddDiv();  releasebtn();"   />
                         <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png"  CssClass="imagebuttonStyle"  OnClick="imgbtnView_Click" OnClientClick="SearchClear();" TabIndex="12"    runat="server"   />
                       
                    </div>
                        </asp:Panel>
                         </div>
                        <div class="clear"></div>
                            <div class="group_bg2">
                        <div class="main_bg"> 
                            <div id="divAdd" runat="server" style="display: block; height: 785px">
                               
                            <ul class="contentul" id="Container" style="margin: 11px 0 0 0px;">
                                
                               <div class="divL">
                                         <li>
                                    <asp:Label ID="Label7" runat="server"  CssClass="label" Text="Store"></asp:Label>
                                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="dropdownMandatory"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqvalidatorStore" runat="server" ErrorMessage="Select a Store" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="reqvalidatorStore">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                    </div>
                                 <div class="divR">
                                       
                                           <li>
                                                <asp:Label ID="Label5" runat="server" Width="139px"  CssClass="label" Text="Date"></asp:Label>
                                          
                                 <asp:TextBox runat="server" CssClass="textboxMandatory" Width="130px"  ValidationGroup="submit" ID="txtDate" TabIndex="1" AutoComplete="off" />
                                            <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                            </cc:ValidatorCalloutExtender>

                                     
                                    

                                </li>
                                     </div>
                                  
                              
                               
                                 <div class="clear"></div>
                                <li  class="clear"></li>
                                <div class="divSeparation"></div>
                                       <div class="divL" style="width: 400px;">
                                                                <li id="liCategory" runat="server">
                                           <asp:Label  ID="lblcat" runat="server" class="label" Width="135" Text="Category"></asp:Label>
                                            <asp:DropDownList ID="ddlCat" runat="server" TabIndex="2"  CssClass="dropdown"></asp:DropDownList>

                                        </li>
                                            <li>
                                            <asp:Label ID="lblItem" runat="server" CssClass="label" Text="Item" ></asp:Label>
                                            <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtitem" MaxLength="20" AutoComplete="off" Width="181px"  TabIndex="3" onblur="return popUpStockSelect();" />
                                            <%--   <asp:Button ID="btnItem" runat="server" Text="Item" OnClick="btnItem_Click" Style="display: none;" />--%>
                                            <asp:ImageButton ID="ImageButton3" runat="server" TabIndex="11"  OnClientClick="return popUpStockSelectClick(1);"  ImageUrl="~/images/serach_icon.png" style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute;" ValidationGroup="img" />
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Item Required " ControlToValidate="txtitem" Display="None" SetFocusOnError="True" ValidationGroup="addItem"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                        </cc:ValidatorCalloutExtender>
                                        </li>
                                   
                                        <li>  
                                     <asp:Label ID="Label31" runat="server" CssClass="label"  Text="Sales Price"></asp:Label>
                                       <asp:TextBox ID="txtSalesPrice" CssClass="textboxMandatory" TabIndex="4" Width="215px" runat="server" MaxLength="11" Style="text-align: right; font-weight: bold;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="ReqSalesPrice" runat="server" ErrorMessage="SalesPrice" ControlToValidate="txtSalesPrice" Display="None" SetFocusOnError="True" ValidationGroup="addItem"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="ReqSalesPrice">
                                        </cc:ValidatorCalloutExtender>
                                             
                                    </li>
                               

                                        <li class="clear"></li>

                                    </div>
                                  <div class="divR">
                                    
                                      <li >
                                      <asp:Label ID="Label2" runat="server" CssClass="label" Width="138px" Text="Quantity"></asp:Label>
                                       <asp:TextBox ID="txtQuantity" CssClass="textbox" TabIndex="5"  Width="130px" runat="server" MaxLength="11" Style="text-align: right; font-weight: bold;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                          
                                      <asp:CompareValidator ID="cmpQuan" runat="server" ErrorMessage="cannot be Greater than CurrentStock" ControlToCompare="txtQ" SetFocusOnError="True" ControlToValidate="txtQuantity" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" Type="Double" ValidationGroup="addItem"></asp:CompareValidator>
                                  <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="cmpQuan">
                                            </cc:ValidatorCalloutExtender>
                                              
                                   </li>
                                        <li style="display:none">
                                      <asp:Label ID="Label1" runat="server" CssClass="label" Width="138px" Text="Qty"></asp:Label>
                                       <asp:TextBox ID="txtQ" CssClass="textbox"  Width="130px" runat="server" MaxLength="11" Style="text-align: right; font-weight: bold;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                           </li>
                                       
                                           <li >
                                      <asp:Label ID="Label3" runat="server" CssClass="label" Width="138px" Text="MRP"></asp:Label>
                                       <asp:TextBox ID="txtMRP" CssClass="textboxMandatory" TabIndex="6" Width="130px" runat="server" MaxLength="11" Style="text-align: right; font-weight: bold;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                               <asp:HiddenField ID="HdnOldMRP" runat="server" />
                                                <asp:RequiredFieldValidator ID="Reqmrp" runat="server" ErrorMessage="MRP Required " ControlToValidate="txtMRP" Display="None" SetFocusOnError="True" ValidationGroup="addItem"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="Reqmrp">
                                        </cc:ValidatorCalloutExtender> 
                                           </li>
                                                <li>
                                      <asp:Label ID="Label4" runat="server" CssClass="label" Width="138px" Text="New Sal.Price"></asp:Label>
                                       <asp:TextBox ID="txtNewSalePrice" CssClass="textboxMandatory" TabIndex="7" Width="130px" runat="server" MaxLength="11" Style="text-align: right; font-weight: bold;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ReqNew" runat="server" ErrorMessage="New SalesPrice Required " ControlToValidate="txtNewSalePrice" Display="None" SetFocusOnError="True" ValidationGroup="addItem"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="ReqNew">
                                        </cc:ValidatorCalloutExtender> 
                                      
                                    <%--        <asp:CompareValidator ID="cmpquantity" runat="server" ErrorMessage="cannot be less than sales Price" ControlToCompare="txtSalesPrice" SetFocusOnError="True" ControlToValidate="txtNewSalePrice" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="GreaterThan" Type="Double" ValidationGroup="addItem"></asp:CompareValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="cmpquantity">
                                            </cc:ValidatorCalloutExtender>--%>
                                                    
                                            <asp:CompareValidator ID="cmpMRP" runat="server" ErrorMessage="cannot be greater than MRP" ControlToCompare="txtMRP" SetFocusOnError="True" ControlToValidate="txtNewSalePrice" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" Type="Double" ValidationGroup="addItem"></asp:CompareValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="cmpMRP">
                                            </cc:ValidatorCalloutExtender>
                                   </li>

                                   </div>
                                 </ul>
                                <div class="clear"></div>
                                    
                                  <li class="btnMargin" style="padding-left:140px;">
                                            <asp:Button Text="Add" ID="btnAddItems" runat="server"  OnClick="btnAddItems_Click" TabIndex="8" ValidationGroup="addItem" CssClass="button" />

                                            <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" TabIndex="9" OnClientClick="ItemClear();return false;" />
                                         </li>
                                   
                                <div class="clear"></div>
                                  <div class="divSeparation"></div>
                                    <li style="width: 100%;  min-height: 210px; max-height: 210px; overflow-y: auto;">
                                        <asp:GridView ID="gvItems" runat="server" Width="95%" CssClass="grid"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                            BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items"  OnRowDeleting="gvItems_RowDeleting" OnSelectedIndexChanging="gvItems_SelectedIndexChanging"  PageSize="10">
                                            
                                            <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                            <Columns>

                                                   <asp:TemplateField HeaderText="SlNo" ItemStyle-HorizontalAlign="Right">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox21" runat="server" Text='<%# Eval("SLNO") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                          <ItemStyle  CssClass="gridviewItemStyle"  />
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblgvSLNO" runat="server" Text='<%# Eval("SLNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center">
                                                             <ItemStyle  CssClass="gridviewItemStyle"  />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox21" runat="server" Text='<%# Eval("Item") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItem" runat="server" Text='<%# Eval("Item") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                        
                                                         <asp:TemplateField HeaderText="Quantity">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox67" Style="width: 20px; text-align: right;" runat="server" Text='<%# Eval("Quantity") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                             <ItemStyle  CssClass="gridviewItemStyle"  />
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblgvquty" runat="server" Text='<%# Eval("Quantity") %>' Style="text-align: right; float: right;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="FK_Item" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox4322" runat="server" Text='<%# Eval("FK_Items") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvFK_Items" runat="server" Text='<%# Eval("FK_Items") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="MRP">
                                                           <ItemStyle  CssClass="gridviewItemStyle"  />
                                                    <EditItemTemplate>
                                                      
                                                        <asp:TextBox ID="txtMRP" runat="server" Text='<%# Eval("NewMRP") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMrp" runat="server" Text='<%# Eval("NewMRP") %>' style="text-align: right; float: right;"></asp:Label>
                                                         <asp:HiddenField ID="gvHdnOldMRP"  Value='<%# Eval("OldMRP") %>' runat="server" /> 
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Sales Price">
                                                         <ItemStyle  CssClass="gridviewItemStyle"  />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtSalePriceText" runat="server" Text='<%# Eval("SalesPrice") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSalesPrice" runat="server" Text='<%# Eval("SalesPrice") %>' style="text-align: right; float: right;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="New Sale.Price" >
                                                         <ItemStyle  CssClass="gridviewItemStyle"  />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtNewSalePriceText" runat="server" Text='<%# Eval("NewSalesPrice") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvNewSalesPrice" runat="server" Text='<%# Eval("NewSalesPrice") %>' style="text-align: right; float: right;"></asp:Label>
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


                                            <%--    <asp:BoundField DataField="slno" HeaderText="SLNO" HeaderStyle-Width="5px"  ItemStyle-CssClass="gridviewItemStyle" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Item" DataField="Item"  ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="300px" />--%>
                                               <%-- <asp:BoundField HeaderText="Quantity" DataField="Quantity" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />--%>
                                                <%--<asp:BoundField HeaderText="MRP" DataField="MRP" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />--%>
                                                <%--<asp:BoundField HeaderText="SalePrice" DataField="SalesPrice"  ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" />--%>
                                                <%--<asp:BoundField HeaderText="NewSalePrice" DataField="NewSalesPrice" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px"/>--%>
                                         
                                               
                                             
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton125" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                   
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton255" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle  CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="1px" />
                                            <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </li>
                                  <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfStore" runat="server" />
                                
                                         <asp:HiddenField ID="hfuserName" runat="server" />
                                      <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                             <asp:HiddenField ID="hfFK_PurchaseReturn" runat="server"  />
                                   <asp:HiddenField ID="hdfFkItem" runat="server"  />
                                 <asp:HiddenField ID="hdfFKStock" runat="server"  />
                                 <asp:HiddenField ID="hfItem" runat="server"  />
                                 <asp:HiddenField ID="hfID_Price" runat="server"  />
                                  <asp:HiddenField ID="hfItemId" runat="server"  />
                                   <asp:HiddenField ID="hfType" runat="server"  />
                                  <asp:HiddenField ID="hfSlNo" runat="server"  />
                                  <asp:HiddenField ID="hdfItem" runat="server"  />
                                     <asp:HiddenField ID="hfMasterID" runat="server"  />
                                  <asp:HiddenField ID="hfC" runat="server"  />
                               

                                    <li style="display: none;">
                                        <asp:Label ID="Label33" runat="server" Width="80px" CssClass="label" Text="Item"></asp:Label>
                                        <asp:DropDownList ID="ddlItem" runat="server"  OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdownMandatory"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select an Item" ControlToValidate="ddlItem" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="addItem"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

 <asp:Button ID="btnItem" runat="server" Text="Item" Style="display: none;" />
   <li class="btnMargin" style="padding-left:140px;">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" TabIndex="10"   OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="submit" />
                        <asp:Button ID="btnClear" Text="Clear" runat="server" TabIndex="11"   OnClientClick="ClearAdd(); " OnClick="btnClear_Click" CausesValidation="false" CssClass="button" />
                            </li>     
                        </%--div>
                            </div>
                                  <div class="main_bg">  
                        <div id="divView" runat="server" style="display: none; height: 485px">

                            <ul class="contentul">
                                <div class="divL">
                                    
                                    <li>
                                        <label class="label">From Date</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtFromDateSearch" TabIndex="1" AutoComplete="off" />
                                        <cc:CalendarExtender ID="txtDate_CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDateSearch" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                    
                                    </li>

                                    <li>
                                        <label class="label">To Date</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtToDateSearch" TabIndex="2" AutoComplete="off" />
                                        <cc:CalendarExtender ID="txtDate_CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtToDateSearch" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                      
                                    </li>
                         <li>
                                        <label class="label">Store</label>
                                        <asp:DropDownList ID="ddlSearchStore" runat="server" CssClass="dropdown"  TabIndex="3"></asp:DropDownList>
                                    </li>
                                    <li class="clear"></li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" TabIndex="4" CssClass="button" Text="Search"  OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchClear" runat="server" TabIndex="5" CssClass="button" Text="Clear" OnClientClick="SearchClear(); return false;" />

                                    </li>
                                   
                                         </div>
                            </ul>
                                 <ul class="contentul">
                                <asp:GridView ID="grdPrice" runat="server" CssClass="grid" Style="margin-left: 1px;" Width="95%"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AllowPaging="true" ForeColor="Black" GridLines="Vertical" PageSize="10"  OnPageIndexChanging="grdPrice_PageIndexChanging"  DataKeyNames="ID_priceCorrection,Approved"  OnRowCommand="grdPrice_RowCommand" OnSelectedIndexChanging="grdPrice_SelectedIndexChanging" OnRowDataBound="grdPrice_RowDataBound" OnRowDeleting="grdPrice_RowDeleting">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="PricecorrectionDate" HeaderText="Date" />
                                        <asp:BoundField DataField="Store" HeaderText="Store" />
                                         <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btnApprovedStatus" OnClientClick="ConfirmApprove()" CssClass="button" Width="75px" Text='<%# Bind("ApprovedStatus") %>' runat="server"  CommandName="ApprovedStatus" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server"  CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
  
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton5" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png"  OnClick="ImageButton5_Click" Width="20px" Height="20px" />
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
                                        <div class="clear"></div> 

                               
                             <div class="clear"></div>
                           
                  <asp:Button ID="btnS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopUpdelete" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divPopheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader1" runat="server" Text="Reason"></asp:Label>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                            <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                                </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divPopUpdelete" TargetControlID="btnS"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divPopheader">
                        </cc:ModalPopupExtender>
       <%--                <asp:Button ID="btS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                <asp:Panel ID="divPop" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 300px; height: 370px;">
                    <div id="divhead" runat="server" class="PopupHeader">
                        <asp:Label ID="lblPopupH" runat="server"  CssClass="lblPopupHeader"></asp:Label>
                        <asp:ImageButton ID="btnClse" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                    </div>

                    <uc3:CommonPopupGrid  ID="CommonPopupGrid1" runat="server" />
                         <cc:ModalPopupExtender ID="mpGrid" runat="server" PopupControlID="divPop" TargetControlID="btS"
                    CancelControlID="btnClse" Drag="true" PopupDragHandleControlID="divhead">
                </cc:ModalPopupExtender>
                </asp:Panel>--%>
                          <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server">
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
                                    <asp:Label ID="Label15" runat="server" Text="Privilege" CssClass="lblPopupHeader"></asp:Label>
                                    <asp:ImageButton ID="btnCP" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                </div>
                                <uc1:CommonPopupPrivilege runat="server" ID="CommonPopupPrivilege" />
                            </asp:Panel>
                            <cc:ModalPopupExtender ID="mpPrivilege" runat="server" PopupControlID="pnlPrvlg" TargetControlID="btnP"
                                CancelControlID="btnCP" Drag="true" PopupDragHandleControlID="divPrvlg">
                            </cc:ModalPopupExtender>

                          </div>  

              </ContentTemplate>
              </asp:UpdatePanel>
    <div>
    
    </div>
    </form>
</body>
</html>
