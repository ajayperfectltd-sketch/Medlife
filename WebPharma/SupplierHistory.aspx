<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierHistory.aspx.cs" Inherits="SupplierHistory" %>
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
          function ClearSearchClick() {
              var dt = new Date();

              $("#txtDate").val(dt.format("dd/MM/yyyy"));
              $("#txtTodate").val(dt.format("dd/MM/yyyy"));
              $("#ddlSupplier").val("0");
          }
          function Datetime() {
              var dt = new Date();
              $("#txtDate").val(dt.format("dd/MM/yyyy"));
              $("#txtTodate").val(dt.format("dd/MM/yyyy"));
              
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

                            <div class="user">Supplier History</div>
                           </div>
                    <div class="clear"></div>
                    
                    <div class="group_bg2">
                     <div class="main_bg"> 
                        <div id="divView" runat="server"  style="height:750px;" >
                               <div  style="width:950px">
                            <div class="divL">
                                <ul class="contentul">
                                         
                                    <li>
                                            <asp:Label ID="Label2" runat="server" CssClass="label" Text="Supplier"></asp:Label>
                                            <asp:DropDownList ID="ddlSupplier" runat="server"  AutoPostBack="true" CssClass="dropdown" TabIndex="1"></asp:DropDownList>
                                        </li>
                                      <li>
                                    <label class="label">From Date</label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtDate" TabIndex="2" AutoComplete="off" />
                                    <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                        </li> 
                                      <li>
                                    <label class="label">To Date</label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtTodate" TabIndex="3" AutoComplete="off" />
                                    <cc:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtTodate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                        </li> 
                                    </ul>
                                        </div>
                                    
                                   
                              
                           <%-- </div>--%>
                           
                        <div class="divR">
                               <ul class="contentul">
                            
                                 
                                  
                                  
                                  </ul>
                             
                           </div>
                                         <li class="clear"></li>
                                        <li class="btnMargin"  style="padding-left:160px;padding-top:5px">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search"   OnClick="btnSearch_Click" TabIndex="4" />
                                            <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearchClick();"  OnClick="btnSearchClear_Click" TabIndex="5" />
                                        </li>
                                     
                                    </div>
                            <ul class="contentul">
                                <asp:GridView ID="gvSupplier" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" Width="900px"  BorderStyle="Solid"  OnSorting="gvSupplier_Sorting" PageSize="10"  OnPageIndexChanging="gvSupplier_PageIndexChanging"     AllowPaging="True"    BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowSorting="True">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" Visible="false"  ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                          <asp:BoundField  HeaderText="Order.No"  DataField="OrderNo" SortExpression="OrderNo" />
                                          <asp:BoundField  HeaderText="Order Date"  DataField="OrderDate" SortExpression="OrderDate"   DataFormatString="{0:d}"/>
                                          <asp:BoundField  HeaderText="Bill No"  DataField="BillNo"   SortExpression="BillNo" />
                                        <asp:BoundField  HeaderText="Bill Date" DataField="BillDate" SortExpression="BillDate" DataFormatString="{0:d}"  />
                                           <asp:BoundField  HeaderText="Inv.No"  DataField="InvoiceNo" SortExpression="InvoiceNo" />    
                                         <asp:BoundField  HeaderText="Inv.Date" DataField="InvoiceDate" SortExpression="InvoiceDate" DataFormatString="{0:d}" />
                                          <asp:BoundField  HeaderText="Status" DataField="Status" SortExpression="Status" />
                                      
               

                                        
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
               <asp:Button ID="btnQuotation" runat="server" Text="Hide"  Style="display: none;" CausesValidation="false"  />
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
