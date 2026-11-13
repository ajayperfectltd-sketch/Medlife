<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebPharmaTransaction.aspx.cs" Inherits="WebPharmaTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopupTransferToAccount.ascx" TagName="CommonPopupTransferToAccount" TagPrefix="uc1" %>
<%@ Register src="Popup/CommonPopup.ascx" tagname="CommonPopup" tagprefix="uc2" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WebPharmaTransaction </title>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <script src="js/General.js"></script>
    <script type="text/javascript">
        var submit = 0;

        //function disablebutn() {
        //   window.setTimeout('delaysubmit()', 500);
        //}
        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();


        }


        function SearchClear() {

            var fromdate = new Date();
            var toDate = new Date();
            $("#txtFromDate").val(toDate.format("dd/MM/yyyy"));
            $("#txtToDate").val(toDate.format("dd/MM/yyyy"));



        }
        function clearfields() {
            var toDate = new Date();
            $("#txtDate_CalendarExtender_container").val(toDate.format("dd/MM/yyyy"));
            $("#<%#ddlTransType.ClientID%>").val('0');
            $("#<%#ddlCreditAccount.ClientID%>").val('0');

            //document.getElementById("lblCustomerBalanceAmount").innerText=''
            $("#lilblCustomerBalanceAmount").attr('style', 'display:none');
            $("#liCustomerSupplier").attr('style', 'display:none');
            $("#<%#txtAmount.ClientID%>").val('');
            $("#<%#txtRemarks.ClientID%>").val('');

            $("#<%#Txtcusid.ClientID%>").val('');
            $("#<%#txtcustomername.ClientID%>").val('');

        }

        function EnterKeyFilter() {



            $(document).keypress(
                function (event) {
                    if (event.which == '13') {
                        //alert(8);
                        event.preventDefault();
                    }


                });

        }


    </script>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="WebPharmaTransation" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>

                    <div class="icon_bg">
                        <div class="search" style="width: 670px;">WebPharmaTransaction </div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" OnClick="imgbtnAdd_Click" OnClientClick="clearfields(); ShowAddDiv();" />
                        </div>
                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" OnClientClick="ShowAddDiv(); clearfields(); " OnClick="imgbtnView_Click" />
                        </div>
                    </div>

                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server">
                            <div class="divL" style="width: 60%;">
                            <ul class="contentul">
                                
                                    <li>
                                        <asp:Label ID="Label12" runat="server" Text="TransType" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlTransType" CssClass="dropdownMandatory" runat="server" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlTransType_SelectedIndexChanged" EnableViewState="true" Width="375"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTransType" Style="float: right;" runat="server" ErrorMessage="Required TransType" Display="None" Font-Bold="True" Font-Size="Large" SetFocusOnError="True" ToolTip="Required field" ValidationGroup="submit" InitialValue="0" ControlToValidate="ddlTransType">*</asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="rfvTransType">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label7" runat="server" Text="Date" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtDate" CssClass="textbox" runat="server" TabIndex="2"></asp:TextBox>
                                        <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="lblCreditType" runat="server" Text="Credit Account" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlCreditAccount" CssClass="dropdownMandatory" runat="server" AutoPostBack="True" TabIndex="3" OnSelectedIndexChanged="ddlCreditAccount_SelectedIndexChanged" Width="375"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select account" ControlToValidate="ddlCreditAccount" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                   <%-- <div id="divCustomer1" runat="server">--%>


                                
                                    


                                        <li id="liCustomerSupplier" runat="server" style="display: none">
                                    <asp:Label ID="lblCustomer" runat="server" CssClass="label" Text="Customer" Width="135px"></asp:Label>
                                    <asp:TextBox ID="Txtcusid" runat="server" AutoPostBack="false" CssClass="textboxMandatory" TabIndex="102" onKeypress="CheckInteger(event);" Width="50px" onblur="txtInput_onblur();" MaxLength="8"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="btnGos_Click" style="margin-bottom:-8px;" TabIndex="4" />                                    
                                    <asp:TextBox ID="txtcustomername" runat="server" Width="270px" CssClass="textboxMandatory" TabIndex="103" MaxLength="500"></asp:TextBox>

                                

                                </li>













                                   <%--     <li id="liCustomerSupplier" runat="server" style="display: none">
                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer" CssClass="label"></asp:Label>
                                            <asp:DropDownList ID="ddlCustomer" CssClass="dropdownMandatory" runat="server" TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqCus" runat="server" ErrorMessage="Select a Customer" ControlToValidate="ddlCustomer" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="reqCus_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="reqCus">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RequiredFieldValidator ID="reqSup" runat="server" ErrorMessage="Select a Supplier" ControlToValidate="ddlCustomer" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="reqSup_ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqSup">
                                            </cc:ValidatorCalloutExtender>
                                        </li>--%>
                             
                                    <li>
                                        <asp:Label ID="lblamount" runat="server" Text="Amount" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtAmount" CssClass="textboxMandatory" runat="server" TabIndex="5" onkeypress="CheckNumWithDecimal(event);" MaxLength="14" Width="364px"></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtAmount" ErrorMessage="Enter a valid Amount" Display="None" MaximumValue="999999999999"  MinimumValue="1" Type="Double"></asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required Amount" ControlToValidate="txtAmount" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                        </cc:ValidatorCalloutExtender>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label26" runat="server" Text="Remarks" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtRemarks" CssClass="textbox" runat="server" TabIndex="6" TextMode="MultiLine" Width="276px"></asp:TextBox>


                                        <asp:Button ID="btnLoanTransfer" runat="server" CssClass="button" Text="Trsfr To A/c" TabIndex="15"  OnClick="btnLoanTransfer_Click" />


                                    </li>
                                          
                                    <li id="lilblCustomerBalanceAmount" runat="server" style="display: none">
                                        <asp:Label ID="lblCustomerBalanceAmount" runat="server" Text="" CssClass="labelSmall" Width="40%" ForeColor="Black" Style="font-weight: bold; font-size: 13px; float: right;"></asp:Label>
                                        <%--<asp:TextBox ID="txtCustomerBalanceAmount" CssClass="textboxSmall" runat="server" Visible="false"></asp:TextBox>--%>
                                    </li>

                                    <li class="btnMargin">
                                       <%-- <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="7" ValidationGroup="submit" OnClick="btnSubmit_Click" />--%>
                                         <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="7"  OnClick="btnSubmit_Click" />
                                      
                                        <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="8"  OnClientClick="clearfields();  return false;" />
                                     
                                     </li>
                                <li>

                                        <asp:HiddenField ID="hdfWebPharmaTransaction" runat="server" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                     <asp:HiddenField ID="HdValuestore" runat="server" />
                                    </li>
                                  </ul>
                                     </div>
                          



                        </div>

                        <div id="divView" runat="server" style="display: none;">


                            <ul class="contentul">
                                <li id="livoucher" runat="server" style="display: none;">
                                    <asp:Label ID="Label4" runat="server" Text="VoucherNo" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtSearchVoucherNo" runat="server" CssClass="textbox" ToolTip="VoucherNo" TabIndex="20" MaxLength="15" autoComplete="off"></asp:TextBox>
                                    <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtSearchVoucherNo">
                                    </cc:FilteredTextBoxExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label5" runat="server" Text="From Date" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" ToolTip="From Date" TabIndex="9"></asp:TextBox>
                                    <cc:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                    </cc:CalendarExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label23" runat="server" Text="To Date" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" ToolTip="To Date" TabIndex="10"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtFromDate" ControlToValidate="txtToDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="search" InitialValue="0"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                    </cc:ValidatorCalloutExtender>
                                    <cc:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                    </cc:CalendarExtender>
                                    <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtFromDate" ControlToValidate="txtToDate" Display="Dynamic" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="GreaterThanEqual" SetFocusOnError="True" ToolTip="From date should be less than to date" Type="Date" ValidationGroup="search">*</asp:CompareValidator>--%>                                  
                                </li>
                                <br class="clear" />
                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" meta:resourcekey="lblSearch" Text="Search" CssClass="button" TabIndex="11" ValidationGroup="search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnSearchClear" runat="server" meta:resourcekey="lblClear" Text="Clear" CssClass="button" OnClientClick="SearchClear(); return false;" />
                                </li>
                                <br class="clear" />
                                <li>
                                    <asp:GridView ID="gvWebPharmaTransaction" runat="server" CssClass="grid" EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_WebPharmaTransaction" OnSelectedIndexChanging="gvWebPharmaTransaction_SelectedIndexChanging">
                                        <AlternatingRowStyle BackColor="#CCE0EB" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SLNO">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label10" runat="server" meta:resourcekey="lblSlNO" Text="SL NO"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelSlNo" runat="server" Text='<%#Bind("SLNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField Visible="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label11" meta:resourcekey="lblTempNo" Text="TempNo" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblTempNo" runat="server" Text='<%#Bind("TempNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--       <asp:BoundField DataField="VoucherNo" HeaderText="VoucherNo" />--%>
                                            <asp:BoundField DataField="TransType" HeaderText="TransType" />
                                            <asp:BoundField DataField="Name" HeaderText="TransferTo" />
                                            <asp:BoundField DataField="TransDate" HeaderText="Date" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                        <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
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
                                </li>
                            </ul>

                        </div>


                      <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 300px; height: 370px; position: absolute;" >
 
                            <div id="divheader" runat="server" class="PopupHeader">
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Transfer To Account" CssClass="lblPopupHeader"></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>

                            <uc1:CommonPopupTransferToAccount ID="CommonPopup1" runat="server" />
                    
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                        </cc:ModalPopupExtender>













                    <asp:Button ID="btnShows" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup1" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 300px; height: 370px; position: absolute;" >
 
                            <div id="div1" runat="server" class="PopupHeader">
                                <asp:Label ID="lblPopupHeader1" runat="server" Text="Customer/Supplier" CssClass="lblPopupHeader"></asp:Label>
                                <asp:ImageButton ID="btnClose1" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>

                                   <uc2:CommonPopup ID="CommonPopup2" runat="server" />
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpCommon1" runat="server" PopupControlID="divPopup1" TargetControlID="btnShows"
                            CancelControlID="btnClose1" Drag="true" PopupDragHandleControlID="divheader">
                        </cc:ModalPopupExtender>







                        
              











                        </div>


                    </div>
           </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/toastr.js"></script>
    </form>

</body>
</html>
