<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indent.aspx.cs" Inherits="Indent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/PopupIndent.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopUpGridSelection.ascx" TagName="CommonPopupGrid" TagPrefix="uc3" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
       <script src="js/Navigation.js"></script>
    <script src="js/Custom.js"></script>
         <script>
             function ClearAdd() {
                 submit = 0;
                 
             ItemClear();
             }
             var popup;
             function popUpStockSelectClick() {

                 if (Page_ClientValidate("img") == true) {

                     var popup;
                     var ItemName = $("#txtitem").val();
                     //if ($("#hfStore").val() == '') {
                     SetStoreDetail();
                     // BlurStore();
                     // $("#hfStore").val($("#ddlStore").val())
                     //   }
                     popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val() + '&suplr=' + $("#txtSupRefno").val(), "Popup", "width=1000,height=800");
                     popup.focus();

                 }

                 return false;
             }
             function EnterTab(e, Id1) {

                 var intKey = window.Event ? e.which : e.KeyCode;


                 if (intKey == 13) {
                     e.returnValue = false;
                     $("#" + Id1).focus();
                 }
             }
             function popUpStockSelect() {

                 if (Page_ClientValidate("img") == true) {

                     var popup;
                     var ItemName = $("#txtitem").val();
                     var hfUser = $("#hfUser").val();
                     var hfClient = $("#hfClient").val();
                     var ddlCategory = $("#ddlCategory").val();
                     var hfStore = $("#hfStore").val();
                     var txtSupRefno = $("#txtSupRefno").val();
                     //if (ItemName.length > 0) {
                     //    if ($("#hfStore").val() == '') {
                     //        SetStoreDetail();
                     //        //BlurStore();
                     //        //$("#hfStore").val($("#ddlStore").val())
                     //    }

                     //    popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val() + '&suplr=' + $("#txtSupRefno").val(), "Popup", "width=1000,height=800") + '&ctgry=' + $("#ddlCategory").val();
                     //    popup.focus();
                     //}
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
                 }

                 return false;
             }
       
             function SetStoreDetail() {
                 var store = $("#ddlStore option:selected").val();

                 $("#hfStore").val(store);



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
                 $("#txtSalesPrice").val("0");
                 $("#txtQuantity").val("");
                 $("#txtitem").val("");
                 $("#txtCurrentstk").val("0");
                 $("#hfItemId").val("");
                 $("#hfItem").val("");
                 $("#txtReq").val("0");
                 $("#txtToStock").val("0");
                 $("#txtIndentStock").val("0");
                 //$("#hfID_Indent").val("");
                 
                 



             }
             function PopupOnReason() {
                 try {

                     $("#divPopupReason").css('display', 'block');

                 }
                 catch (ex) {

                 }
             }
             function SearchClear() {

                 var dt = new Date();
                
                 $("#txtFromDateSearch").val(dt.format("dd/MM/yyyy"));
                 $("#txtToDateSearch").val(dt.format("dd/MM/yyyy"));
                 //var value = $("#hfStore").val() == "" ? "0" : $("#hfStore").val();
                 //$("#ddlSearchStore").val(value);


             }
             function pageLoad() {

                 var ctrContainer = "form";
                 setKeynavigation(ctrContainer);

             }
              </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <div class="btn_bg5"> 
                      <div class="heading2">
                           <div class="user">Indent</div>
                    <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                      <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/ADD.png"  CssClass="imagebuttonStyle" TabIndex="11" runat="server" CausesValidation="false" OnClientClick="ShowAddDiv(); ClearAdd();releasebtn();"   OnClick="imgbtnAdd_Click"/>
                        <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle" TabIndex="10" CausesValidation="false"  OnClientClick="SearchClear();"  runat="server"  OnClick="imgbtnView_Click"/>
                 
                    </div>
                        </asp:Panel>
                         </div>
                     <div class="clear"></div>
                       <div class="group_bg2">
                        <div class="main_bg"> 
                    <ul class="contentul" id="Container">
                        <div id="divAdd" runat="server" style="display: block;">
                       <div class="divL">
                                   <li>
                                    <label class="label">From Store</label>
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStore"  AutoPostBack="true" onkeydown="return KEyCode();" Style="margin-left: 0px;" >
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="rfvS" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="rfvS">
                                    </cc:ValidatorCalloutExtender>

                                </li>
                                  <li>
                                    <label class="label">Indent Date</label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtDate" TabIndex="2" AutoComplete="off" />
                                    <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>

<%--                                    <asp:CustomValidator ID="cvDatecheck" runat="server"
                                        ClientValidationFunction="ClientValidate" ControlToValidate="txtDate" Display="None"
                                        CssClass="validator"
                                        ErrorMessage="Future date not allowed" SetFocusOnError="True" ValidationGroup="submit"></asp:CustomValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="cvDatecheck">
                                    </cc:ValidatorCalloutExtender>--%>

                                </li>
 
                           </div>
                             <div class="divR">
                                <li>
                                    <label class="label">To Store</label>
                                    <asp:DropDownList runat="server"   OnSelectedIndexChanged="ddlToStore_SelectedIndexChanged" onkeydown="EnterTab(event,'ImageButton3');"  CssClass="dropdownMandatory" ID="ddlToStore" TabIndex="4" AutoPostBack="true"  Style="margin-left: 0px;" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStore" runat="server" ErrorMessage="Store required" ControlToValidate="ddlToStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="rfvStore">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                       <li> 
                                            <label class="label">Req.No</label>
                                        <asp:TextBox ID="txtReqno" runat="server" AutoPostBack="false"  CssClass="textboxMandatory" TabIndex="8" onKeypress="CheckInteger(event);" Width="183px"></asp:TextBox>

                                            <asp:ImageButton ID="imgReqNo" runat="server"   OnClick="imgReqNo_Click"  ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute" ValidationGroup="imgReq" />
                                              <asp:RequiredFieldValidator ID="reqImg" runat="server" ErrorMessage="Store required" ControlToValidate="ddlToStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="imgReq"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="reqImg">
                                    </cc:ValidatorCalloutExtender>

                                        </li>
                            </div>
                            <div class="clear"></div>
                             <div class="divSeparation"></div>
                              <div class="divL">
                                 <li>
                                            <asp:Label ID="lblItem" runat="server" CssClass="label" Text="Item" ></asp:Label>
                                            <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtitem" MaxLength="20" AutoComplete="off" Width="180px"  TabIndex="10"  onblur="return popUpStockSelect();" />
                                           
                                            <asp:ImageButton ID="ImageButton3" runat="server"   onkeydown="EnterTab(event,'txtQuantity');"  TabIndex="11"  onkeypress="return popUpStockSelect();" OnClientClick="return popUpStockSelect();"  ImageUrl="~/images/serach_icon.png" style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute;"  ValidationGroup="img"/>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Item Required " ControlToValidate="txtitem" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                        </cc:ValidatorCalloutExtender>
                                        </li>
                                      <li>
                                            <asp:Label ID="lblqty" runat="server" CssClass="label" Text="Quantity"></asp:Label> 
                                            <asp:TextBox ID="txtQuantity" CssClass="textboxMandatory"  Style="text-align: right;" Width="213px" runat="server" TabIndex="13" MaxLength="11" onkeypress="DecimalOrIntValidation('txtPurQty',event,'items'); " onKeyup="CalculateNetAmount(); ValidateActQty(); "  AutoComplete="off"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" ValidationGroup="add" SetFocusOnError="True" ></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RangeValidator ID="RangeValidator1" ValidationGroup="add" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" SetFocusOnError="True" MaximumValue="999999999999" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:CompareValidator ID="cmpquantity" runat="server" ValidationGroup="add" ErrorMessage="cannot greater than current stock"  SetFocusOnError="True" ControlToCompare="txtCurrentstk" ControlToValidate="txtQuantity" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" Type="Double" ></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="cmpquantity">
                                            </cc:ValidatorCalloutExtender>

                                         <%--   <asp:CustomValidator ID="cvQuantity" runat="server"
                                                ClientValidationFunction="ValidateActQty" ControlToValidate="txtQuantity" Display="None"
                                                CssClass="validator"
                                                ErrorMessage="Invalid Stock" SetFocusOnError="True"></asp:CustomValidator>

                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" Enabled="True" TargetControlID="cvQuantity">
                                            </cc:ValidatorCalloutExtender>--%>
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </li>
                                   <li>
                                      <asp:Label ID="Label2" runat="server" CssClass="label" Width="135px" Text="Requested Quantity"></asp:Label>
                                       <asp:TextBox ID="txtReq" CssClass="textbox"  Width="213px" runat="server" MaxLength="11" Style="text-align: right; font-weight: bold;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                           </li>
                              </div>
                            <div class="divR">
                                <li>
                                      <asp:Label ID="lblSales" runat="server" CssClass="label" Text="Sales Price"></asp:Label>
                                       <asp:TextBox ID="txtSalesPrice" CssClass="textbox" TabIndex="12" Width="215px" runat="server" MaxLength="11" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                      </li>
                                  
                               <li>
                                      <asp:Label ID="lblCrntstk" runat="server" CssClass="label" Text="Current Stock"></asp:Label>
                                       <asp:TextBox ID="txtCurrentstk" CssClass="textbox"  Width="213px" runat="server" MaxLength="11" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                           </li>
                                    <li>
                                      <asp:Label ID="lblToStock" runat="server" CssClass="label" Text="ToStock"></asp:Label>
                                       <asp:TextBox ID="txtToStock" CssClass="textbox"  Width="213px" runat="server" MaxLength="11" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                           </li>
                                <li>
                                      <asp:Label ID="lblIndent" runat="server" CssClass="label" Text="Indent Stock"></asp:Label>
                                       <asp:TextBox ID="txtIndentStock" CssClass="textbox"  Width="213px" runat="server" MaxLength="11" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                           </li>
                                  <div class="divR">
                                    
                                      </div>
                                </div>
                      
                            <div class="clear"></div>
                              <li class="clear"></li>   
                                   <%-- <div style="float: left; width: 90px;">--%>
                                        <li class="btnMargin">
                                            <asp:Button Text="Insert" ID="btnAddItems" runat="server"  OnClick="btnAddItems_Click" TabIndex="18" ValidationGroup="add" CssClass="button" />

                                  
                                            <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" TabIndex="19" OnClientClick="ItemClear();return false;" />
                                        </li>

                                    <div class="clear"></div>
                                    <div class="divSeparation"></div>
                                       
                                <li style="width: 100%;  min-height: 210px; max-height: 210px; overflow-y: auto;">
                                        <asp:GridView ID="gvItem" runat="server"  DataKeyNames="FK_Items,ID_Stock" CssClass="grid"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="1000px"
                                            BackColor="White" BorderStyle="Solid"   BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" OnSelectedIndexChanging="gvItem_SelectedIndexChanging" OnRowDeleting="gvItem_RowDeleting" >
                                            <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle"/>
                                            
                                            <Columns>
                                           <%--     <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" >
                                               
                                                <ItemStyle HorizontalAlign="Center" CssClass="gridviewItemStyle"  />
                                                </asp:BoundField>--%>
                                                  <asp:TemplateField HeaderText="SlNo"   ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle  CssClass="gridviewItemStyle"  />        
                                                        <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item"   HeaderStyle-Width="200px">
                                                    <ItemStyle  CssClass="gridviewItemStyle"  />
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblItem" Style="width: 200px;" Text='<%# Eval("ItemName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Batch" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="70px" ItemStyle-Width="70px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBatchNo" runat="server"><%#Eval("BatchNo") %></asp:Label>
                                                        <asp:HiddenField ID="hdfBatchNo" runat="server" Value='<%#Eval("BatchNo") %>' />
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Exp.Date"  ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="70px" ItemStyle-Width="70px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExpiry"    runat="server" DataFormatString="{0:d}"> <%#Eval("ExpiryDate") %></asp:Label>
                                                        <%--<asp:HiddenField ID="hdfBatchNo" runat="server" Value='<%#Eval("BatchNo") %>' />--%>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Sal.Price" ItemStyle-CssClass="gridviewItemStyle"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSalesPrice" runat="server" Style="float: right;" Text='<%#Eval("SalPrice") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdfSalesPrice" runat="server" Value='<%#Eval("SalPrice") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right"  />
                                                    <ItemStyle HorizontalAlign="Right"  />
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRP" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="70px" ItemStyle-Width="70px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMrp" runat="server" Style="float: right;"><%#Eval("MRP") %></asp:Label>
                                                        <%--<asp:HiddenField ID="hdfBatchNo" runat="server" Value='<%#Eval("BatchNo") %>' />--%>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Req.Qty" HeaderStyle-Width="70px"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">

                                                    <ItemTemplate>
                                                        <%--<asp:TextBox runat="server" ID="txtQuantity"  CssClass="textbox" Style="width: 50px; text-align: right;" onkeypress="DecimalOrIntValidation(id,event,'txtQuantity');" Text='<%# Eval("RqueQuantity") %>' />--%>
                                                       
                                                        <asp:Label ID="lblReqquantity" runat="server"  Style="float: right;" Text='<%#Eval("RqueQuantity") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdfQty" runat="server" Value='<%# Eval("RqueQuantity") %>' />
                                                       
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="gridviewHeaderStyle" HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="gridviewItemStyle" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                  
                                                    <asp:TemplateField HeaderText="Qty" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="50px" ItemStyle-Width="70px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" runat="server" Style="float: right;" Text='<%#Eval("Qty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Cur.Stock" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="70px" ItemStyle-Width="70px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrentStock" runat="server" Style="float: right;" Text='<%#Eval("CurrentStock") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>
                                             <%-- <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="gridviewItemStyle"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" runat="server" Style="float: right;" Text='<%#Eval("Qty") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdfSalesPrice" runat="server" Value='<%#Eval("Qty") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right"  />
                                                    <ItemStyle HorizontalAlign="Right"  />
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="CurrentStock" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                                         <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrentStock" runat="server" Text='<%#Eval("CurrentStock") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>--%>
                                                
                                                <asp:BoundField HeaderText="Tot.Amt" DataField="TotalAmount" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px"/>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgEdit" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Edit_bg1.png" Width="20px" Height="20px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px"   />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle  CssClass="gridviewHeaderStyle" Height="25px" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </li>
                            <asp:HiddenField ID="hfClient" runat="server" />
                              <asp:HiddenField ID="hfUser" runat="server" />
                            <asp:HiddenField ID="hfIsAdd" runat="server" />
                              <asp:HiddenField ID="hfIsUpdate" runat="server" />
                              <asp:HiddenField ID="hfIsDelete" runat="server" />
                           <asp:HiddenField ID="hfuserName" runat="server" />
                               <asp:HiddenField ID="hfStore" runat="server" />
                              <asp:HiddenField ID="hfType" runat="server"  />
                            <asp:HiddenField ID="hdfFKStock" runat="server"  />
                                <asp:HiddenField ID="hdfFkItem" runat="server"  />
                            <asp:HiddenField ID="hfItemId" runat="server"  />
                           <asp:HiddenField ID="hfItem" runat="server"  />
                             <asp:HiddenField ID="hdfCurrentStk" runat="server"  />
                           <asp:HiddenField ID="hfID_Indent" runat="server"  />
                              <asp:HiddenField ID="hdfItem" runat="server"  />
                                <asp:HiddenField ID="hfID" runat="server"  />
                             <asp:HiddenField ID="hfIndentUsercode" runat="server"  />
                            <asp:Button ID="btnStore" Text="Hide" runat="server" Style="display: none;" CausesValidation="false"  OnClick="btnStore_Click" />
                            <li class="btnMargin">
                           <asp:Button ID="btnSubmit" runat="server" CssClass="button" TabIndex="38"  OnClick="btnSubmit_Click"  Text="Submit" ValidationGroup="submit" />
                            <asp:Button ID="btnClear" Text="Clear" runat="server" TabIndex="39" OnClick="btnClear_Click" OnClientClick="ClearAdd(); "  CausesValidation="false" CssClass="button" />
                            <asp:Button ID="btnApproval" Text="Approval" runat="server" TabIndex="40"  OnClick="btnApproval_Click"  CausesValidation="false" CssClass="button" />
                         </li>
                               
                              <asp:Button ID="btnItem" runat="server" Text="Button"  OnClick="btnItem_Click" CausesValidation="false" Style="display: none;" />
                          </div>
                          </ul>
                           </div>
                            <div class="main_bg"> 
                               <div id="divView" runat="server" style="display: none;">
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
                                     <%-- </div>
                                   <div class="divR">
                                   >
                                       </div>--%>
                                    <li class="clear"></li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" TabIndex="7" CssClass="button" Text="Search"  OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchClear" runat="server" TabIndex="8" CssClass="button" Text="Clear" OnClientClick="SearchClear(); return false;" />

                                    </li>
                                         </ul>
                            <ul class="contentul">
                                  <asp:GridView ID="gvIndent" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical"  OnPageIndexChanging="gvIndent_PageIndexChanging" AllowPaging="true" PageSize="10" DataKeyNames="ID_Indent"
                                       OnSelectedIndexChanging="gvIndent_SelectedIndexChanging"  OnRowCommand="gvIndent_RowCommand" OnRowDeleting="gvIndent_RowDeleting"  OnRowDataBound="gvIndent_RowDataBound">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                        <%-- <asp:BoundField DataField="BillNo" HeaderText="BillNo" />--%>
                                        <%-- <asp:TemplateField HeaderText="Name ">
                                                <ItemTemplate>
                                                    <div style="width: 200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                        <%# Eval("Name") %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        <asp:BoundField DataField="StoreFrom" HeaderText="From store" />
                                        <asp:BoundField DataField="ToStore" HeaderText="To Store" />
                                        <asp:BoundField DataField="IndentDate" HeaderText="Date" DataFormatString="{0:d}" />
                                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btnApprovedStatus" CssClass="button" Width="75px" Text='<%# Bind("ApprovedStatus") %>' runat="server" OnClientClick="ConfirmPurchaseApprove();" CommandName="ApprovedStatus" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="UserCode" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfuservalues" Value='<%# Bind("UserCode") %>' runat="server"></asp:HiddenField>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1784" runat="server" CausesValidation="false" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2"  runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" OnClick="ImageButton2_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                  <%--<asp:Button ID="btnPrint" CssClass="button" Width="75px" Text="Print" runat="server" OnClientClick="ConfirmPurchasePrint();" CommandName="Print" />--%>
                                                <asp:ImageButton ID="ImageButton343" runat="server"  CommandName="Print" ToolTip="Print" ImageUrl="~/images/1430738477_report.png" Width="20px" Height="20px" />

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
                              <asp:Button ID="btS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                <asp:Panel ID="divPop" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 300px; height: 370px;">
                    <div id="divhead" runat="server" class="PopupHeader">
                        <asp:Label ID="lblPopupH" runat="server"  CssClass="lblPopupHeader"></asp:Label>
                        <asp:ImageButton ID="btnClse" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                    </div>

                    <uc3:CommonPopupGrid  ID="CommonPopupGrid1" runat="server" />
                </asp:Panel>
                <cc:ModalPopupExtender ID="mpGrid" runat="server" PopupControlID="divPop" TargetControlID="btS"
                    CancelControlID="btnClse" Drag="true" PopupDragHandleControlID="divhead">
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
                </ContentTemplate>
              </asp:UpdatePanel>
           
    </form>
</body>
</html>
