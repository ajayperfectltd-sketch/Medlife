<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuotationComparison.aspx.cs" Inherits="QuotationComparison" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title></title>
        <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
     <script src="js/Navigation.js"></script>
       <script src="js/toastr.js"></script>

     <script type="text/javascript">
         function pageLoad() {

             var ctrContainer = "form";
             setKeynavigation(ctrContainer);

         }
         //$(function () {
         //    $('[id*=gvQuotationComparison] tr').each(function () {
         //        var toolTip = $(this).attr("title");
         //        alert(toolTip);
         //        $(this).find("td").each(function () {
         //            $(this).simpletip({
         //                content: toolTip
         //            });
         //        });
         //        $(this).removeAttr("title");
         //    });
         //});
        
         function popUpItemsSelect() {

             try {

                 var ItemName = $("#txtSearchName").val();
                 if (ItemName.length > 0) {

                     $("#IbtnItemAdd").click();
                 }

             }
             catch (e) {
                 alert(e);
             }

         }
         function ClearSearchClick() {
             try {

                 $("#txtSearchName").val('');
                 $("#ddlCategory").val("3");
                 $("#ddlType").val("0");
                 $("#txtFreeqty").val('');
                 $("#txtPurchaseRate").val('');
                 $("#txtDiscountAmount").val('');
                
                 
             }
             catch (ex) {
                 alert(ex);
             }
         }
         function ButtonClick() {

            // $("#<%#btnQuotation.ClientID%>").click();
             var clickButton = document.getElementById("<%= btnQuotation.ClientID %>");
             clickButton.click();
             return false;




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

                            <div class="user">Quotation Comparison</div>
                           </div>
                    <div class="clear"></div>
                    
                    <div class="group_bg2">
                     <div class="main_bg"> 
                        <div id="divView" runat="server"  style="height:750px;" >
                               <div  style="width:950px">
                            <div class="divL">
                                <ul class="contentul">
                                    <li>
                                            <asp:Label ID="lbkCategory" runat="server" CssClass="label" Text="Category"></asp:Label>
                                            <asp:DropDownList ID="ddlCategory" runat="server" onBlur="return ButtonClick();"  CssClass="dropdown" TabIndex="1">
                                                 <asp:ListItem Text="Supplier" Value="1" ></asp:ListItem>
                                                 <asp:ListItem Text="Customer" Value="2"></asp:ListItem>
                                                  <asp:ListItem Text="Item" Value="3"  Selected="True"></asp:ListItem>

                                            </asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label ID="lblType" runat="server" CssClass="label" Text="Type"></asp:Label>
                                            <asp:DropDownList ID="ddlType" runat="server"  AutoPostBack="true" CssClass="dropdown" >
                                                <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                                                 <asp:ListItem Text="Supplier" Value="1"></asp:ListItem>
                                                 <asp:ListItem Text="Customer" Value="2"></asp:ListItem>

                                            </asp:DropDownList>
                                        </li>
                                         <li>
                                              
                                     <asp:Label ID="lblName" runat="server" CssClass="label"  Text="Name"></asp:Label>
                                            <asp:TextBox ID="txtSearchName" Width="185px" CssClass="textbox" onBlur="popUpItemsSelect();"  TabIndex="2" runat="server" ></asp:TextBox>
                                      <asp:ImageButton ID="IbtnItemAdd"   runat="server"   OnClick="IbtnItemAdd_Click"  ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute;" />
                               
                                            
                                    </li>
                                
                                 
                                    
                                         
                                  
                                        
                                    </ul>
                                        </div>
                                    
                                   
                              
                           <%-- </div>--%>
                           
                        <div class="divR">
                               <ul class="contentul">
                            
                                 <li>
                                            <asp:Label ID="Label2" runat="server" CssClass="label"  Text="Purchase Rate"></asp:Label>
                                            <asp:TextBox ID="txtPurchaseRate" CssClass="textbox" TabIndex="3" runat="server" ></asp:TextBox>
                                        </li>
                                    
                                                                    <li>
                                            <asp:Label ID="Label3" runat="server" CssClass="label"  Text="Discount Amount"></asp:Label>
                                            <asp:TextBox ID="txtDiscountAmount" CssClass="textbox" TabIndex="4" runat="server" ></asp:TextBox>
                                        </li>
                                     
                                      <li>
                                            <asp:Label ID="Label1" runat="server" CssClass="label"  Text="Free Qty"></asp:Label>
                                            <asp:TextBox ID="txtFreeqty" CssClass="textbox" TabIndex="5" runat="server" ></asp:TextBox>
                                        </li>
                                  
                                  
                                  </ul>
                             
                           </div>
                                         <li class="clear"></li>
                                        <li class="btnMargin"  style="padding-left:160px;padding-top:15px">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search"  OnClick="btnSearch_Click" TabIndex="6" />
                                            <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear"   OnClientClick="ClearSearchClick();" OnClick="btnSearchClear_Click"  TabIndex="7" />
                                        </li>
                                     
                                    </div>
                            <ul class="contentul">
                                <asp:GridView ID="gvQuotationComparison" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" Width="900px"  BorderStyle="Solid" OnSorting="gvQuotationComparison_Sorting" OnPageIndexChanging="gvQuotationComparison_PageIndexChanging"  OnRowDataBound="OnRowDataBound"    AllowPaging="True" PageSize="10"    BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowSorting="True">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" Visible="false"  ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                          <asp:BoundField  HeaderText="Supplier"  DataField="SupplierName" SortExpression="SupplierName" />
                                          <asp:BoundField  HeaderText="Customer"  DataField="CustomerName" SortExpression="CustomerName" />
                                          <asp:BoundField  HeaderText="Free Qty"  DataField="FreeQuantity"   SortExpression="FreeQuantity" />
                                        <asp:BoundField  HeaderText="Effect From" DataField="EffectFrom" SortExpression="EffectFrom" DataFormatString="{0:d}"  />
                                             <asp:BoundField  HeaderText="Effect To" DataField="EffectTo" SortExpression="EffectTo" DataFormatString="{0:d}"  />
                                        <asp:BoundField  HeaderText="Pur.Rate" ItemStyle-HorizontalAlign="Right" DataField="Rate" SortExpression="Rate" />
                                           <asp:BoundField  HeaderText="Disc%"     ItemStyle-HorizontalAlign="Right" DataField="Discount" SortExpression="DiscountPer" />
                                        <asp:BoundField  HeaderText="Disc.Amt" ItemStyle-HorizontalAlign="Right" DataField="DiscountPer" SortExpression="Discount" />
                                          <asp:BoundField  HeaderText="Narrartion Dis" ItemStyle-HorizontalAlign="Right" DataField="NarrartionDiscount"  />
                                         <asp:BoundField  HeaderText="Narrartion Free" ItemStyle-HorizontalAlign="Right" DataField="NarrartionFree"  />
               

                                        
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle CssClass="hederdesgn"  Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                             
                            </ul>
                                <li class="clear"></li>
                              
                        </div>
                         </div>
                           
                        </div>

                    </div>
                    <asp:HiddenField ID="hfClient" runat="server" />
                    <asp:HiddenField ID="hfUser" runat="server" />
                 <asp:HiddenField ID="hdfValue" runat="server" />
                 <asp:HiddenField ID="hfSelectedItem" runat="server" />
                 <asp:HiddenField ID="hfSelectedSupplier" runat="server" />
                 <asp:HiddenField ID="hfSelectedCustomer" runat="server" />
               <li class="btnMargin">
               <asp:Button ID="btnQuotation" runat="server" Text="Hide" OnClick="btnQuotation_Click" Style="display: none;" CausesValidation="false"  />
                                </li>
                         <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle" >
                    <div id="divheader" runat="server" class="PopupHeader">
                        <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head" CssClass="lblPopupHeader"></asp:Label>
                        <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                    </div>
                    <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                </asp:Panel>
                         <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                    CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                </cc:ModalPopupExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
