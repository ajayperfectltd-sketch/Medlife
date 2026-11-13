<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesReturnOld.aspx.cs" Inherits="SalesReturnOld" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Sales Return::</title>
    <link href="css/main.css" rel="stylesheet" />

    <link href="css/toastr.css" rel="stylesheet" />
    <%--  <script>
        //function PrintSlip() {
        //    try {
        //        if ($("#hdfPrintField").length > 0) {
        //            // var a = "<style>.offerTable{background-color: #496ba6;border:2px solid black; width:100%; margin-top:0px;margin-bottom:3.38 px; }.offerTable tr{boder:1px solid black;}.offerTable td{background-color: #d6e9ff;padding: 3px; }.offerTable th{color: White;padding: 3px;border: 1px solid white;text-align: center;} .table{background-color: #e2dfd6;}.table th{background-color: #003c74;color: White;font-weight: bold;padding: 3px;.table td{background-color: #f4f4f0;padding: 5px;}.table td table td{padding: 0px;} </style>";
        //           // var prtContent = document.getElementById("hdfPrintField");
        //            var abcd = $("#hdfPrintField").val();
        //            alert(abcd);
        //            var WinPrint = window.open('', '', 'left=0,top=0,width=500');
        //            WinPrint.document.write(abcd);
        //            WinPrint.document.close();
        //            WinPrint.focus();
        //            WinPrint.print();
        //            WinPrint.close();
        //            location.reload();
        //        }
        //    }
        //    catch (e) {
        //        alert(e);
        //    }

        //    //return $("#tblTest table").length > 0;
        //    return false;
        //}
      

    </script>--%>
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

</head>
<body>
    <form id="frmSalesReturn" runat="server" autocomplete="off">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        <div class="search" style="width: 670px;">Sales Return</div>
                        <div class="add">Add</div>

                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" TabIndex="29" Width="37" Height="38" runat="server" CausesValidation="false" OnClientClick="ShowAddDiv();  releasebtn();" OnClick="ImageButton1_Click" />
                        </div>
                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" TabIndex="28" runat="server" OnClick="imgbtnView_Click" />
                        </div>


                    </div>

                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server">




                            <ul class="contentul" id="Container">
                                <div class="divL" style="width: 325px;">
                                    <li>
                                        <label class="label">Date</label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtDate" TabIndex="1" AutoComplete="off" />
                                        <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li>

                                        <asp:Label class="label" ID="lbl" runat="server" Text="Store"></asp:Label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStore" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged">
                                            <%--OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqvalStore" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="reqvalStore">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="From Store required" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="From Store required" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="img"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label186" runat="server" CssClass="label" Text="Return Method"></asp:Label>
                                        <asp:RadioButton ID="rdItemSale" runat="server" Text="Item Wise " GroupName="a" AutoPostBack="true" TabIndex="2" OnCheckedChanged="rdItemSale_CheckedChanged" />
                                        <asp:RadioButton ID="rdSales" runat="server" Text="Sales Wise" AutoPostBack="true" GroupName="a" TabIndex="3" OnCheckedChanged="rdSales_CheckedChanged" />
                                    </li>
                                    <li class="clear"></li>




                                </div>
                                <div class="divL" style="width: 325px;">



                                    <li>

                                        <asp:Label class="label" ID="Label5" Width="65px" runat="server" Text="Counter"></asp:Label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlCounter" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlCounter_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Counter required" ControlToValidate="ddlCounter" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                        </cc:ValidatorCalloutExtender>


                                    </li>
                                    <li>

                                        <asp:Label class="label" Width="65px" ID="Label3" runat="server" Text="BillType"></asp:Label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlBillType" TabIndex="5">
                                            <%--OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="BillType required" ControlToValidate="ddlBillType" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                        </cc:ValidatorCalloutExtender>

                                    </li>


                                    <li>

                                        <asp:CheckBox ID="cbxIsCredit" runat="server" Text="Transfer" Style="margin-left: 50px" TabIndex="6" />
                                    </li>



                                </div>
                                <div style="float: left; width: 300px;">
                                    <li id="liCategory" runat="server">
                                        <asp:Label class="label" ID="Label2" Width="100px" runat="server" Text="Category"></asp:Label>
                                        <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="7" CssClass="dropdown" AutoPostBack="true"></asp:DropDownList>

                                    </li>
                                    <li id="liPurchase" runat="server">


                                        <asp:Label ID="lblAcvountHead" runat="server" CssClass="label" Text="Sales" Width="65"></asp:Label>
                                        <asp:TextBox ID="txtSaleRefno" runat="server" AutoPostBack="false" CssClass="textboxMandatory" TabIndex="145" Style="display: none;" onKeypress="CheckInteger(event);" Width="10px" onblur="txtInput_onblur();"></asp:TextBox>
                                        <asp:TextBox ID="txtSales" runat="server" CssClass="textboxMandatory" TabIndex="100" Style="width: 175px; margin-left: 0px;"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnSales" runat="server" OnClick="imgbtnSales_Click" CausesValidation="false" OnClientClick="ClearSales();return true;" TabIndex="8" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: -110px 0px 0px 114px; position: absolute; top: 182px; left: 830px; height: 26px;" />

                                        <asp:RequiredFieldValidator ID="rfvAccountHead" runat="server" ErrorMessage="Sales Refno required" ControlToValidate="txtSaleRefno" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="rfvAccountHead">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="BillNo required" ControlToValidate="txtSales" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                        </cc:ValidatorCalloutExtender>



                                    </li>

                                    <li>

                                        <asp:Label class="label" ID="Label4" runat="server" Width="65px" Text="Customer"></asp:Label>
                                        <asp:TextBox ID="txtCustRefno" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="9" onblur="txtCustRefno_onBlur();" onKeypress="CheckInteger(event);" Width="40px"></asp:TextBox>

                                        <asp:ImageButton ID="imgbtnCustomer" runat="server" OnClick="imgbtnCustomer_Click" CausesValidation="false" OnClientClick="return true;" TabIndex="10" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 0px 0px 0px 3px; position: absolute" />
                                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="textbox" TabIndex="130" Style="margin-left: 0px; width: 120px; float: right; margin-right: 20px;"></asp:TextBox>

                                    </li>


                                    </li>

                                </div>
                                <li class="clear"></li>

                                <div class="divSeparation"></div>



                                <div class="clear"></div>
                                <li class="clear"></li>
                                <div id="divItemPurchaseReturn" runat="server">
                                    <div class="divL" style="width: 250px;">
                                        <li>
                                            <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item" Width="100px"></asp:Label>
                                            <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtitem" MaxLength="20" AutoComplete="off" Width="100px" TabIndex="11" onblur="return popUpStockSelect();" Style="margin-left: 34px;" />
                                            <%--   <asp:Button ID="btnItem" runat="server" Text="Item" OnClick="btnItem_Click" Style="display: none;" />--%>
                                            <asp:ImageButton ID="ImageButton3" runat="server" onkeypress="return popUpStockSelectClick();" OnClientClick="return popUpStockSelectClick();" TabIndex="12" ImageUrl="images/serach_icon.png" Style="padding-left: 2px; margin-top: 0px; position: absolute; width: 30px;" ValidationGroup="img" />
                                        </li>
                                        <li class="clear"></li>
                                    </div>
                                    <div class="divL" style="width: 250px;">

                                        <li>
                                            <asp:Label ID="Label9" runat="server" CssClass="label" Text="Quantity" Style="padding-left: 35px; width: 56px; margin-left: 42px; padding-top: 4px;"></asp:Label>
                                            <asp:TextBox ID="txtQuantity" CssClass="textboxMandatory" Style="text-align: right;" Width="100px" runat="server" TabIndex="13" MaxLength="8" onkeypress="DecimalOrIntValidation('txtQuantity',event,'items');" onKeyup="ActQty();" AutoComplete="off"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" SetFocusOnError="True" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                            </cc:ValidatorCalloutExtender>

                                        </li>

                                        <li style="display: none;">


                                            <asp:TextBox runat="server" ID="txtCurrentStock" TabIndex="140" />
                                        </li>


                                    </div>
                                    <div class="divL" style="width: 100px;">
                                        <asp:Button Text="Add" ID="btnAddItems" runat="server" OnClick="btnAddItems_Click" TabIndex="14" ValidationGroup="add" CssClass="button" Style="margin-top: 4px;" />
                                    </div>
                                    <div class="divL" style="width: 100px;">
                                        <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" TabIndex="15" OnClientClick="ItemClear();return false;" Style="margin-top: 4px;" />
                                    </div>

                                    <div class="clear"></div>
                                    <div class="divL">
                                        <ul style="margin: 0px 0px 0px 0px;">
                                            <li class="btnMargin"></li>
                                        </ul>
                                    </div>

                                    <div class="clear"></div>
                                    <div class="divSeparation"></div>
                                    <li style="width: 930px; height: auto; min-height: 240px; max-height: 240px; overflow-y: auto;">



                                        <asp:GridView ID="gvItems" runat="server" CssClass="grid"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False" Style="width: 99%; margin-left: 1px;"
                                            BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items" OnRowDeleting="gvItems_RowDeleting" PageSize="100">
                                            <AlternatingRowStyle BackColor="#CCE0EB" />
                                            <Columns>

                                                <asp:BoundField DataField="slno" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Item" DataField="Item" />

                                                <asp:BoundField HeaderText="Quantity" DataField="Quantity" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />

                                                <asp:BoundField HeaderText="MRP" DataField="MRP" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="Sales Price" DataField="SalesPrice" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />

                                                <asp:BoundField HeaderText="Total Amount" DataField="TotalAmt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                                <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlReason" runat="server" TabIndex="90" CssClass="dropdownMandatory"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select a Reason"
                                                            ControlToValidate="ddlReason" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                        </cc:ValidatorCalloutExtender>
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
                                            <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                            <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>

                                    </li>




                                </div>
                                <%--------------------------------------------------------------------------------------(  End  )------------------------------------------------------------------------- --%>


                                <li id="gvsi" runat="server" visible="false" style="width: 96%; height: auto; min-height: 290px; max-height: 290px; overflow-y: auto;">



                                    <asp:GridView ID="gvSaleItems" runat="server" CssClass="grid" DataKeyNames="FK_Items,FK_Stock,GroupId,VAT,MRP,SLNO,CESS"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False" Style="width: 99%; margin-left: 1px;"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100"
                                        OnRowDeleting="gvSaleItems_RowDeleting" OnRowDataBound="gvSaleItems_RowDataBound">
                                        <AlternatingRowStyle BackColor="#CCE0EB" />
                                        <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Item" DataField="Item" />
                                            <asp:TemplateField HeaderText="Return Quantity" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtQuantity" onchange="CalculateAmount(id);" AutoComplete="off" CssClass="textbox" Style="width: 120px; text-align: right;" onkeypress="CheckNumWithDecimal(event);" Text='<%# Eval("Quantity") %>' />
                                                    <asp:HiddenField ID="hdfQty" runat="server" Value='<%# Eval("Quantity") %>' />
                                                    <asp:HiddenField ID="hdfVAT" runat="server" Value='<%# Eval("VAT") %>' />
                                                    <asp:HiddenField ID="hdfCESS" runat="server" Value='<%# Eval("CESS") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Quantity" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSaleQty" runat="server"><%#Eval("Quantity") %></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRP" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMRP" runat="server"><%#Eval("MRP") %></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Price" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalesPrice" runat="server" Text='<%#Eval("SalesPrice") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amount" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmt" runat="server" Text='<%#Eval("TotalAmt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlReason" runat="server" TabIndex="90" CssClass="dropdownMandatory"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rqvalReason" runat="server" ErrorMessage="Select a Reason" Enabled="false"
                                                        ControlToValidate="ddlReason" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="rqvalReason">
                                                    </cc:ValidatorCalloutExtender>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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


                                </li>

                                <li class="clear"></li>
                                <br class="clear" />
                                <%--  </ContentTemplate>
                                        
                                    </asp:UpdatePanel>--%>


                                <div class="divSeparation"></div>
                                <div>
                                    <div class="divL">
                                        <li>
                                            <label class="label">GST</label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtVAT" onKeypress="CheckNumWithDecimal(event);" Style="text-align: right;" TabIndex="16" AutoComplete="off" />
                                            <asp:CheckBox ID="chkReturnAll" runat="server" Text="Return All" Style="margin-left: 20px" AutoPostBack="true" TabIndex="5" OnCheckedChanged="chkReturnAll_CheckedChanged" />
                                        </li>
                                        
                                        <li>
                                            <label class="label">CESS</label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtCESS" onKeypress="CheckNumWithDecimal(event);" Style="text-align: right;" TabIndex="16" AutoComplete="off" />
                                        </li>

                                        <li>
                                            <label class="label">Remark</label>
                                            <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" CssClass="multiline" MaxLength="500" TabIndex="17" AutoComplete="off" />
                                        </li>
                                        <li id="cbx" style="display: none;">
                                            <label class="label">Close Remark</label>
                                            <asp:TextBox ID="txtClosedRemark" runat="server" TextMode="MultiLine" MaxLength="500" TabIndex="18" AutoComplete="off" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                            <asp:HiddenField ID="HiddenField4" runat="server" />
                                        </li>
                                        <asp:HiddenField ID="hdfDate" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                        <asp:HiddenField ID="hfStore" runat="server" />
                                        <asp:HiddenField ID="hfFK_unit" runat="server" />
                                        <asp:HiddenField ID="hdfFKStock" runat="server" />

                                        <asp:HiddenField ID="hdfFkItem" runat="server" />
                                        <asp:HiddenField ID="hfItem" runat="server" />
                                        <asp:HiddenField ID="hfBillNo" runat="server" />
                                        <asp:HiddenField ID="hfBillType" runat="server" />
                                    </div>



                                    <div class="divR">


                                        <li>
                                            <label class="label">Total Amount</label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtTotalAmount" onKeypress="CheckNumWithDecimal(event);" Style="text-align: right;" TabIndex="21" AutoComplete="off" />




                                        </li>

                                        <li>
                                            <label class="label">Discount Amount</label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtDiscountAmt" onkeyup="return Total();" Style="text-align: right;" TabIndex="22" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />

                                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtDiscountAmt" ErrorMessage="Enter a valid Discount Amount" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                        </li>
                                        <li>
                                            <label class="label">RoundOff</label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtRoundOff" Style="text-align: right;" TabIndex="23" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />


                                        </li>
                                        <li>
                                            <label class="label">Net Amount</label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtNetAmount" Style="text-align: right;" TabIndex="24" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />


                                        </li>
                                        <li class="clear"></li>

                                        <li class="btnMargin">
                                            <asp:HiddenField ID="hfSaleReturn" runat="server" />
                                            <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="25" OnClientClick="ConfirmSalesReturn(); disablebutn(); ShowProgress();" />
                                            <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" CausesValidation="false" TabIndex="26" OnClick="btnClear_Click1" />
                                            <asp:Button ID="btnPrint" Text="Print" runat="server" CssClass="button" CausesValidation="false" TabIndex="27" OnClick="btnPrint_Click" />
                                            <asp:Button ID="btnStoreBlurTrigger" runat="server" CausesValidation="false" Text="Item" OnClick="ddlStore_SelectedIndexChanged" Style="display: none;" />
                                            <input id="Button1" type="button" value="button" style="display: none;" onclick="return PrintSlip();" />
                                            <asp:HiddenField ID="hfClient" runat="server" />
                                            <asp:HiddenField ID="hfUser" runat="server" />
                                            <asp:HiddenField ID="hdfPrintField" runat="server" />
                                            <asp:HiddenField ID="hdfPacked" runat="server" />
                                        </li>
                                    </div>

                                </div>
                            </ul>
                        </div>

                        <div class="clear"></div>
                        <div id="divView" runat="server" style="display: none; height: 505px">



                            <ul class="contentul">
                                <div class="divL">
                                    <li>
                                        <label class="label">Customer</label>
                                        <asp:DropDownList ID="ddlSearchCustomer" runat="server" CssClass="dropdown" TabIndex="30"></asp:DropDownList>
                                    </li>

                                    <li>
                                        <label class="label">Store</label>
                                        <asp:DropDownList ID="ddlSearchStore" runat="server" CssClass="dropdown" TabIndex="31"></asp:DropDownList>

                                    </li>


                                    <br class="clear" />
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="32" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="33" />
                                    </li>
                                </div>
                                <div class="divR">

                                    <li>
                                        <label class="label">From Date</label>
                                        <asp:TextBox ID="txtFromDate" CssClass="textbox" runat="server" TabIndex="29"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li>
                                        <label class="label">To Date</label>
                                        <asp:TextBox ID="txtTodate" CssClass="textbox" runat="server" TabIndex="30"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtTodate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                    </li>


                                </div>

                            </ul>
                            <br class="clear" />
                            <ul class="contentul">
                                <asp:GridView ID="gvSaleReturn" runat="server" CssClass="grid" Width="95%" Style="margin-left: 1px;"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100"
                                    DataKeyNames="ID_SalesReturn,FK_Store" OnSelectedIndexChanging="gvSaleReturn_SelectedIndexChanging" OnRowDeleting="gvSaleReturn_RowDeleting">
                                    <AlternatingRowStyle BackColor="#E5EFE4" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                                        <asp:BoundField DataField="BillDate" HeaderText="Date" />
                                        <asp:BoundField DataField="Customer" HeaderText="Customer" />
                                        <asp:BoundField DataField="Store" HeaderText="Store" />
                                        <asp:BoundField DataField="NetAmount" HeaderText="Amount" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="View" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="#12361D" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                                <div class="Pager">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" OnClick="Page_Changed" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ul>
                        </div>
                        <div class="clear"></div>
                        <%-- <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                    </div>

                    <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                    <asp:Panel ID="divPopup" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 300px; height: 370px;">
                        <div id="divheader" runat="server" class="PopupHeader">
                            <asp:Label ID="lblPopupHeader" runat="server" Text="Customer" CssClass="lblPopupHeader"></asp:Label>
                            <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                        </div>

                        <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                    </asp:Panel>
                    <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                        CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                    </cc:ModalPopupExtender>
                </div>



                <div class="loading" align="center">
                    Processing. Please wait.<br />
                    <br />
                    <asp:Image ID="Image1" runat="server" Style="margin-top: 50px;"
                        AlternateText="Image text"
                        ImageAlign="Middle"
                        ImageUrl="~/images/loader2.gif" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <script src="js/jquery-1.10.2.js"></script>
        <script src="js/Custom.js"></script>
        <script src="js/toastr.js"></script>
        <script src="js/alertify.min.js"></script>
        <link href="css/alertify.core.css" rel="stylesheet" />
        <link href="css/alertify.default.css" rel="stylesheet" />
        <script src="js/Navigation.js"></script>
        <script type="text/javascript">
            function txtCustRefno_onBlur() {
                $("#txtCustomer").val('');
            }

            function SetStoreDetail() {
                var store = $("#ddlStore option:selected").val();
                $("#hfStore").val(store);

                ItemClear();

            }

            function ClearAdd() {
                submit = 0;
                $("[id*='txt']").not("txtDate").val('');
                $("#txtSaleRefno").val('');


                ClearCustomer();
                ClearSales();

                //$("#ddlStore").val('0');
                var d = new Date();
                $("#txtDate").val(d.format('dd/MM/yyyy'));
                $("#txtTotalAmount").val('0');
                $("#txtDiscount").val('0');
                $("#txtDiscountAmt").val('0');
                $("#txtNetAmount").val('0');
                $("#txtRoundOff").val('0');
                $("#txtRemark").val("");

                $("#cbxIsCredit").prop("checked", false);
                $("#chkReturnAll").prop("checked", false);

                $("#hfSaleReturn").val('');
                $("#hfBillNo").val('0');
                $("#hfBillType").val('0');
                $("#txtFromDate,#txtTodate").val(d.format('dd/MM/yyyy'));
               // $("[id*='ddl']").val('0');
                $("#ddlPurchase").val('0');
                $("#txtCESS").val("");
                ItemClear();
                //  return false;
            }
            function ClearCustomer() {
                $("#txtCustRefno").val('');
                $("#txtCustomer").val('');
            }
            function ClearSales() {
                $("#txtSaleRefno").val('');
                $("#txtSales").val('');
            }


            function CalculateAmount(id) {
                try {

                    var txtQuantity = id;
                    var lblMRP = id.replace('txtQuantity', 'lblMRP');
                    var hdfVAT = id.replace('txtQuantity', 'hdfVAT');
                    var lblsp = id.replace('txtQuantity', 'lblSalesPrice');
                    var lblTotal = id.replace('txtQuantity', 'lblTotalAmt');
                    var hdfQty = id.replace('txtQuantity', 'hdfQty');

                    var reqval = id.replace('txtQuantity', 'rqvalReason');
                    var txtqty = id.replace('txtQuantity', 'txtQuantity');
                    //   alert(reqval);

                    // if ($("#" + hdfpacked).val() == "False") {
                    var qty = $("#" + txtqty).val() == "" ? "0" : $("#" + txtqty).val();
                    // alert(qty);
                    if (parseFloat(qty) != parseFloat(0)) {

                        ValidatorEnable(document.getElementById(reqval), true);
                        // alert('enabled');

                    }
                    else {
                        ValidatorEnable(document.getElementById(reqval), false);
                        // alert('disabled');
                    }

                    var qty = $("#" + txtQuantity).val() == '' || $("#" + txtQuantity).val() == null ? 0 : parseFloat($("#" + txtQuantity).val());
                    var prevqty = $("#" + hdfQty).val();
                    if (parseFloat(qty) > parseFloat(prevqty)) {
                        alertify.alert("Quantity Cannot Exceed Available Stock");
                        $("#" + txtQuantity).val(parseFloat(prevqty))
                        CalculateTotalAmount();
                    }
                    else {

                        if (parseFloat(qty) == 0) {
                            // alertify.alert("Invalid Quantity");
                            // $("#" + txtQuantity).val(parseFloat(prevqty))
                            CalculateTotalAmount();
                        }
                        else {
                            CalculateTotalAmount();
                        }
                    }

                }
                catch (e) {
                    alert(e);
                }
            }

            var submit = 0;
            function disablebutn() {
                try {
                    ConfirmPrint();
                    window.setTimeout('delaysubmit()', 50);
                }
                catch (e) {
                    alert(e);
                }
            }

            function releasebtn() {


                $("#btnSave").prop('disabled', false);

            }
            function delaysubmit() {
                try {
                    if (submit == 1) {
                        return false;

                    }

                    $("#btnSave").prop('disabled', true);

                    window.setTimeout('releasebtn()', 3000);
                    submit = 1;
                }
                catch (e) {
                    alert(e);
                }
            }

            function ShowProgress() {
                if (Page_ClientValidate("submit")) {
                    setTimeout(function () {
                        //  var modal = $('<div />');
                        //  modal.addClass("modal");
                        ////  $('body').append(modal);

                        var loading = $(".loading");
                        loading.show();
                        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                        loading.css({ top: top, left: left });
                    }, 200);
                }
            }

            function CalculateTotalAmount() {

                var Sptotal = 0;
                var MRPTotal = 0;
                var VATTotal = 0;
                var CESSTotal = 0;
                $.each($("table[id*=" + 'gvSaleItems' + "] tr"), function (index, item) {
                    if ($(item).find("input[type=hidden]").length > 0) {

                        var MRP = $(item).find("[id*=lblMRP_]").text();

                        var SalePrice = $(item).find("[id*=gvSaleItems_lblSalesPrice_]").text();

                        var Qty = $(item).find("[id*=txtQuantity_]").val();

                        var spAmt = parseFloat(SalePrice) * parseFloat(Qty);
                        var MrpAmt = parseFloat(MRP) * parseFloat(Qty);

                        var VAT = $(item).find("[id*=hdfVAT_]").val();
                        var CESS = $(item).find("[id*=hdfCESS_]").val();

                        $(this).find("[id*='lblTotalAmt_']").text(MrpAmt.toFixed(2));

                        var p = parseFloat(SalePrice);

                        var VATAmt = p - (p / ((parseFloat(VAT) + 100) / 100));

                        var CESSAmt = p - (p / ((parseFloat(CESS) + 100) / 100));

                        VATAmt = VATAmt * Qty;
                        Sptotal += spAmt;
                        MRPTotal += MrpAmt;
                        VATTotal += VATAmt;
                        CESSAmt = CESSAmt * Qty;
                        CESSTotal += CESSAmt;
                    }
                });


                var disc = MRPTotal - Sptotal;

                $("#txtVAT").val(VATTotal.toFixed(2));
                $("#txtCESS").val(CESSTotal.toFixed(2));
                $("#txtTotalAmount").val(MRPTotal.toFixed(2));
                $("#txtDiscountAmt").val(disc.toFixed(2));
                Total();

            }
            function Total() {
                try {

                    var TotalAmount = $("#txtTotalAmount").val();
                    var DiscountAmt = $("#txtDiscountAmt").val();
                    var RoundOff = $("#txtRoundOff").val();
                    var NetAmount = $("#txtNetAmount").val();

                    if (TotalAmount >= 0 && DiscountAmt >= 0) {
                        var Amount = (parseFloat(TotalAmount) - parseFloat(DiscountAmt));
                        var total = Amount;
                        if (total >= 0) {

                            var fracpart = (((total < 1.0) ? total : (total % Math.floor(total))) * 100).toFixed(2);

                            if (parseFloat(fracpart) >= 50) {

                                var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);

                                var totalnetRoundamount = parseFloat(total) + parseFloat(Balancefract);
                                var balfract = parseFloat(fracpart / 100).toFixed(2);

                                $("#txtNetAmount").val(totalnetRoundamount.toFixed(2));
                                $("#txtRoundOff").val(Balancefract.toFixed(2));


                            }

                            else if (fracpart < 50) {


                                var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);
                                var totalnetRoundamount = parseFloat(total) - parseFloat(fracpart / 100);
                                var balfract = parseFloat(fracpart / 100).toFixed(2);
                                $("#txtNetAmount").val(totalnetRoundamount.toFixed(2));
                                $("#txtRoundOff").val(-balfract);


                            }

                        }
                        else {
                            alertify.alert('Invalid entry');
                            $("#txtDiscountAmt").val(0);
                            Total();
                        }
                    }
                    else {
                        alertify.alert('Invalid entry');
                        $("#txtRoundOff").val(0);
                        $("#txtNetAmount").val(0);
                    }
                }
                catch (ex) {
                    alert(ex.message);
                }
            }

            function CheckKeys() {
                alert(e.keyCode);
            }

            function ClearSearch() {
                $("#ddlSearchCustomer ").val("0");
                $("#ddlSearchStore").val("0");
                var d = new Date();
                $("#txtFromDate,#txtTodate").val(d.format('dd/MM/yyyy'));

            }
            function ShowAddDiv() {

                $("#divAdd").show();
                $("#divView").hide();
            }
            function btnClick() {
                $("#btnHidden2").click();
                return false;
            }

            function buttonClick() {
                $("#btnHidden").click();
                return false;
            }

            function BlurItem() {

                $("#btnItemBlurTrigger").click();

            }
            function BlurStore() {

                $("#btnStoreBlurTrigger").click();
                $("#ImageButton3").focus();
            }
            function pageLoad() {

                var ctrContainer = "form";
                setKeynavigation(ctrContainer);
                if ($("#gvPurchaseItems tr td").length > 1) {

                    CalculateTotalAmount();
                }
                else {
                    CalculateItemWiseTotal();
                }
                Check();


            }

            function blockPostOnTab() {
                if (e.keyCode == 9)
                    return false;

            }
            function ItemClear() {

                $("#txtitem").val("");
                $("#txtQuantity").val("0");
                $("#txtCurrentStock").val("0");

            }
            function CalculateItemWiseTotal() {
                var total = 0;
                $("#gvItems tr").has("td").each(function () {
                    var sum = $(this).find("td:eq(6)").html();
                    sum = (sum == '' || sum == null ? '0' : sum);
                    total = parseFloat(total) + parseFloat(sum);
                });
                $("#txtInvoiceAmount").val(total.toFixed(3));
            }
            var popup;
            function popUpStockSelectClick() {

                if (Page_ClientValidate("img") == true) {

                    var popup;
                    var ItemName = $("#txtitem").val();
                    if ($("#hfStore").val() == '') {

                        SetStoreDetail();
                    }

                    popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val(), "Popup", "width=1000,height=800");
                    popup.focus();

                }

                return false;
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



            function PrintSlip() {
                try {
                    if ($("#hdfPrintField").length > 0) {
                        // var a = "<style>.offerTable{background-color: #496ba6;border:2px solid black; width:100%; margin-top:0px;margin-bottom:3.38 px; }.offerTable tr{boder:1px solid black;}.offerTable td{background-color: #d6e9ff;padding: 3px; }.offerTable th{color: White;padding: 3px;border: 1px solid white;text-align: center;} .table{background-color: #e2dfd6;}.table th{background-color: #003c74;color: White;font-weight: bold;padding: 3px;.table td{background-color: #f4f4f0;padding: 5px;}.table td table td{padding: 0px;} </style>";
                        //  var prtContent = document.getElementById("hdfPrintField");
                        var abcd = $("#hdfPrintField").val();
                        //alert(abcd);

                        var WinPrint = window.open('', '', 'left=0,top=0,width=500');
                        WinPrint.document.write(abcd);
                        WinPrint.document.close();

                        WinPrint.focus();
                        WinPrint.print();

                        WinPrint.close();
                        location.reload();
                    }
                }
                catch (e) {
                    alert(e);
                }

                //return $("#tblTest table").length > 0;
                return false;
            }
            function PrintButtonClick() {

                $("#Button1").click();

            }

            function popUpStockSelect() {

                if (Page_ClientValidate("img") == true) {


                    var ItemName = $("#txtitem").val();

                    if (ItemName.length > 0) {
                        if ($("#hfStore").val() == '') {
                            SetStoreDetail();

                        }

                        popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val(), "Popup", "width=1000,height=800") + '&ctgry=' + $("#ddlCategory").val();
                        $("#ImageButton3").focus();
                    }
                }


                return false;
            }


            function ConfirmSalesReturnOk() {
                if (Page_ClientValidate("submit")) {
                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_Print";
                    if (confirm("Do you want to do salesReturn...?")) {
                        confirm_value.value = "Yes";
                    } else {
                        confirm_value.value = "No";
                    }
                    document.forms[0].appendChild(confirm_value);
                }
            }
            function ConfirmPrint() {
                if (Page_ClientValidate("submit")) {
                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_Print";
                    if (confirm("Do you want to Print?")) {
                        confirm_value.value = "Yes";
                    } else {
                        confirm_value.value = "No";
                    }
                    document.forms[0].appendChild(confirm_value);
                }
            }

        </script>


    </form>
</body>
</html>
