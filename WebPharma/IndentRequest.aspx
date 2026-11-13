<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndentRequest.aspx.cs" Inherits="IndentRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
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
    <script src="js/Custom.js"></script>
     <script>
         function ClearAdd() {
             submit = 0;
             $("#<%#txtDate.ClientID%>").val(new Date().format("dd/MM/yyyy"));
                   $("#<%#ddlStore.ClientID%>").val('0');
             $("#<%#ddlToStore.ClientID%>").val('0');

                   
             ItemClear();
         }
         function EnterTab(e, Id1) {

             var intKey = window.Event ? e.which : e.KeyCode;


             if (intKey == 13) {
                 e.returnValue = false;
                 $("#" + Id1).focus();
             }
         }

         function ClearSearch() {

             var dt = new Date();
             $("#txtFromDate").val(new Date().format("dd/MM/yyyy"));
             $("#txtTodate").val(new Date().format("dd/MM/yyyy"));
             var value = $("#hfStore").val() == "" ? "0" : $("#hfStore").val();
             $("#ddlSearchStore").val(value);

         }
         function ItemClear() {


             $("#hfItemId").val("");
             $("#ddlItem").val("0");
             $("#txtQuantity").val("0");
             $("#txtItemShortName").val("");
             $("#hdfItem").val("");
             var toDate = new Date();
             $("#txtDate").val(toDate.format("dd/MM/yyyy"));
             //alert('l');
            
         }
         function ItemDataClear() {


             $("#hfItemId").val("");
             $("#ddlItem").val("0");
             $("#txtQuantity").val("0");
             $("#txtItemShortName").val("");
             $("#hdfItem").val("");
             //var toDate = new Date();
             //$("#txtDate").val(toDate.format("dd/MM/yyyy"));
             //alert('l');

         }
         function PopupOnReason() {
             try {


                 $("#divPopupReason").css('display', 'block');
             }
             catch (ex) {

             }
         }
        
          function SetStoreDetail() {
              var store = $("#ddlStore option:selected").val();
              $("#hfStore").val(store);

             

          }
          function popUpStockSelectClick() {

              try {
                  var ItemName = $("#txtItemShortName").val();

                  var hfUser = $("#hfUser").val();
                  var hfClient = $("#hfClient").val();
                  var ddlCategory = $("#ddlCategory").val();
                  var ddlSupplier = "0";
                  var txtInvoiceDate = $("#txtDate").val();
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
                  //alert(ItemName.toString());
                  //url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString()

                  //popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                  //",statusbar=no,menubar=no,resizable=0,width=1015," +
                  //"height=650,left = 200,top = 100");
                  //popUpObj.focus();

                  $("#ddlUnit").focus();
              }
              catch (e) {
                  alert(e);
              }

          }
          function popUpStockSelect() {

              try {
                  var Cat = $("#ddlCategory").val();

                  var ItemName = $("#txtItemShortName").val();
                  
                  if (ItemName.length > 0) {
                      url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString();

                      popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                      ",statusbar=no,menubar=no,resizable=0,width=1015," +
                      "height=650,left = 200,top = 100");
                      popUpObj.focus();
                  }
                  else {
                

                  }
                
              }
              catch (e) {
                  alert(e);
              }

          }
    
         </script>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <div class="btn_bg5"> 
                      <div class="heading2">
                           <div class="user">Indent Request</div>
                    <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                      <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle"  TabIndex="11" runat="server" CausesValidation="false" OnClientClick="ShowAddDiv(); ClearAdd();releasebtn();"  OnClick="imgbtnAdd_Click"/>
                        <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle" TabIndex="10" CausesValidation="false" OnClientClick="ClearSearch();"  runat="server"  OnClick="imgbtnView_Click"/>
                     
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
                                    <label class="label"> From Store</label>
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStore"  AutoPostBack="true" onkeydown="return KEyCode();" Style="margin-left: 0px;" >
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="rfvS" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="rfvS">
                                    </cc:ValidatorCalloutExtender>

                                </li>
                                <li>
                                    <label class="label">Date</label>
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
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlToStore" TabIndex="4" AutoPostBack="true" onkeydown="return KEyCode();" Style="margin-left: 0px;" >
                                    </asp:DropDownList>


                                    <asp:RequiredFieldValidator ID="rfvStore" runat="server" ErrorMessage="Store required" ControlToValidate="ddlToStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="rfvStore">
                                    </cc:ValidatorCalloutExtender>




                                </li>
                            </div>
                            <div class="clear"></div>
                                <div class="divL">
                                             <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtItemShortName" onBlur="popUpStockSelect();" Width="182px" MaxLength="150" TabIndex="9" />
                                        <asp:Button ID="btnItem" runat="server" Text="Item" Style="display: none;" />
                                        <asp:ImageButton ID="imgPopUp" runat="server" OnClientClick="return  popUpStockSelectClick();" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Item Required " ControlToValidate="txtItemShortName" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender117" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                      <li style="display: none;">
                                        <asp:Label ID="Label6" runat="server" Width="80px" CssClass="label" Text="Item"></asp:Label>
                                        <asp:DropDownList ID="ddlItem" runat="server" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdownMandatory"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfItem" runat="server" ErrorMessage="Select an Item" ControlToValidate="ddlItem" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="rfItem">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                             
                           
                              
                            </div>
                                <div class="divR">
                                  <li>
                                     
                                        <asp:Label ID="Label9" runat="server" CssClass="label" Text="Quantity"></asp:Label>
                                        <asp:TextBox ID="txtQuantity" CssClass="textboxMandatory"  onKeypress="DecimalOrIntValidation(event);" runat="server" TabIndex="5" MaxLength="10" style="text-align: right;" AutoComplete="off"></asp:TextBox>
                                        <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" TargetControlID="txtQuantity" FilterType="Custom , Numbers" ValidChars=".">
                                        </cc:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="999999999999" SetFocusOnError="true" ValidationGroup="add" MinimumValue="0.001" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                            </div>
                                 <li class="btnMargin">
                                    <asp:Button Text="Insert" ID="btnAddItems" runat="server"  OnClick="btnAddItems_Click" TabIndex="6"  ValidationGroup="add" CssClass="button" />
                                    <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" TabIndex="7" OnClientClick="ItemClear();return false;" CausesValidation="false" />
                                </li>
                              <div class="divSeparation"></div>
                                <li>

                                    <div style="height: 200px; width: 890px; overflow-y: auto; margin: 0px 0px 0px 0px;">
                                        <asp:GridView ID="gvItems" runat="server" CssClass="grid"
                                           AutoGenerateColumns="False" Width="850px"
                                            BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items,Quantity" OnRowDeleting="gvItems_RowDeleting" OnSelectedIndexChanging="gvItems_SelectedIndexChanging"  PageSize="10"  EmptyDataText="No Data Found!">
                                            <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />

                                            <Columns>
                                              
                                                <asp:TemplateField HeaderText="SlNo" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Item">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox41" runat="server" Text='<%# Bind("Item") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <div style="overflow: auto;">
                                                            <asp:Label ID="lblgvItem" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox67" Style=" text-align: right;" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblgvquty" runat="server" Text='<%# Bind("Quantity") %>' Style="text-align: right; float: right;"></asp:Label>
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
                                              
                                                      <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                       
                                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton2" runat="server"  CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="30px" />
                                            <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />

                                        </asp:GridView>
                                    </div>
                                </li>
                                <li class="btnMargin">
                                        <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit"  OnClick="btnSave_Click" TabIndex="8" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" CausesValidation="false" TabIndex="9" OnClick="btnClear_Click" />
                                    </li>
                            </div>
                        </div>
                              <asp:HiddenField ID="hfClient" runat="server" />
                              <asp:HiddenField ID="hfUser" runat="server" />
                              <asp:HiddenField ID="hfIsAdd" runat="server" />
                              <asp:HiddenField ID="hfIsUpdate" runat="server" />
                              <asp:HiddenField ID="hfIsDelete" runat="server" />
                              <asp:HiddenField ID="hdfFkItem" runat="server" />
                              <asp:HiddenField ID="hdfCurrentStock" runat="server" />
                              <asp:HiddenField ID="hdfRowindex" runat="server" />
                              <asp:HiddenField ID="hdfFKStock" runat="server" />
                                 <asp:HiddenField ID="hfIndentreq" runat="server" />
                            <asp:HiddenField ID="hfuserName" runat="server" />
                               <asp:HiddenField ID="hfStore" runat="server" />
                               <asp:HiddenField ID="hfItem" runat="server" />
                              <asp:HiddenField ID="hfItemId" runat="server"  />
                                  <asp:HiddenField ID="hdfItem" runat="server"  />
                                  <asp:HiddenField ID="hfSlNo" runat="server"  />
                                  <asp:HiddenField ID="HiddenField3" runat="server"  />
                             <asp:HiddenField ID="HfItemsID" runat="server"  />
                             <div class="main_bg"> 
                            <div id="divView" runat="server" style="display: none;">
                                   <ul class="contentul">
                                 <div class="divL">
                                <li>
                                    <label class="label">Store</label>
                                    <asp:DropDownList ID="ddlSearchStore" runat="server" CssClass="dropdown" TabIndex="12"></asp:DropDownList>


                                </li>
                                <li>
                                    <label class="label">From Date</label>
                                    <asp:TextBox ID="txtFromDate" CssClass="textbox" runat="server" TabIndex="13" AutoComplete="off"></asp:TextBox>
                                    <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                    </cc:CalendarExtender>
                                </li>
                            </div>
                                    <div class="divR">
                                       <li>
                                              <label class="label" style="display:none">To Date</label>
                                            </li>
                                                 <li>
                                    <asp:TextBox ID="TextBox1" style="display:none" CssClass="textbox" runat="server" TabIndex="14" AutoComplete="off"></asp:TextBox>
                                    
                                              </li>
                                          </div>
                                <div class="clear"></div>
                        <div>
                                <li>
                                    <label class="label">To Date</label>
                                    <asp:TextBox ID="txtTodate" CssClass="textbox" runat="server" TabIndex="14" AutoComplete="off"></asp:TextBox>
                                    <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtTodate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                    </cc:CalendarExtender>

                                </li>

                            </div>
                                 <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="15"  OnClick="btnSearch_Click" CausesValidation="false" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="16" CausesValidation="false" />
                                </li>
                                </ul>
                                        <ul class="contentul">
                                <%--EmptyDataText="No Data Found!"--%>
                                <asp:GridView ID="gvViewIndent" runat="server" CssClass="grid"
                                    AutoGenerateColumns="False" DataKeyNames="ID_IndentRequest" EmptyDataText="No Data Found!" OnRowDeleting="gvViewIndent_RowDeleting"  OnSelectedIndexChanging="gvViewIndent_SelectedIndexChanging"
                                    BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3"   OnPageIndexChanging="gvViewIndent_PageIndexChanging" style="width:940px;" ForeColor="Black" AllowPaging="true"  GridLines="Vertical" PageSize="10">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle"/>
                                    <Columns>
                                       
                                        <asp:BoundField DataField="SlNo" HeaderText="SlNo" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Date" HeaderText="TransDate" />
                                        <asp:BoundField DataField="Store" HeaderText="Store" />
                                      <%--  <asp:BoundField DataField="TotalAmt" HeaderText="Total Amt" ItemStyle-HorizontalAlign="Right" />--%>
                                         <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete"  OnClick="ImageButton2_Click" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                             
                            </ul>
                                </div>
                                 </div>
                </div>
                     </div>
                
                                 <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                <asp:Panel ID="divPopup" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 370px;">
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
