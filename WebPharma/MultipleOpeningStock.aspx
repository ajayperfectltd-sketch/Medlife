<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultipleOpeningStock.aspx.cs" Inherits="MultipleOpeningStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopUpGridSelection.ascx" TagName="CommonPopupGrid" TagPrefix="uc3" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <style>
        .hidden
        {
            display: none;
        }
    </style>
    <script type="text/javascript">

        function EnterTab(e, Id1) {
            var intKey = window.Event ? e.which : e.KeyCode;


            if (intKey == 13) {
                e.returnValue = false;
                $("#" + Id1).focus();
            }
        }


    </script>
</head>
<body onkeydown="EnterTabBody(event);">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5">
                    <div class="heading2">
                        <div class="user">Multiple Opening Stock</div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                                <%--<asp:ImageButton ID="imgbtnAdd" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle" TabIndex="24" runat="server" OnClick="imgbtnAdd_Click"  />--%>
                            </div>
                        </asp:Panel>
                    </div>




                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">
                            <div id="divAdd" runat="server" style="display: block;">
                                <ul class="contentul">

                                    <li class="clear"></li>
                                    <div class="divSeparation"></div>
                                    <li class="clear"></li>
                                    <div class="divL" style="width: 400px;">

                                        <li>
                                            <asp:Label ID="Label23" runat="server" CssClass="label" Width="80" Text="Category"></asp:Label>
                                            <asp:DropDownList ID="ddlCategory" onkeydown="EnterTab(event,'ddlSubcategory');" runat="server" TabIndex="3" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>

                                        </li>

                                        <li>
                                            <asp:Label ID="Label3" runat="server" CssClass="label" Width="80" Text="Sub Category"></asp:Label>
                                            <asp:DropDownList ID="ddlSubcategory" onkeydown="EnterTab(event,'ddlGeneric');" runat="server" TabIndex="3" CssClass="dropdown" AutoPostBack="true"></asp:DropDownList>

                                        </li>
                                        <li>
                                            <asp:Label ID="Label5" runat="server" CssClass="label" Width="80" Text="Generic"></asp:Label>
                                            <asp:DropDownList ID="ddlGeneric" onkeydown="EnterTab(event,'ddlManufacturer');" runat="server" TabIndex="3" CssClass="dropdown"></asp:DropDownList>


                                        </li>
                                        <li>

                                            <asp:Label ID="Label11" runat="server" CssClass="label" Width="80" Text="Manufacturer"></asp:Label>
                                            <asp:DropDownList ID="ddlManufacturer" onkeydown="EnterTab(event,'ddlRack');" runat="server" TabIndex="3" CssClass="dropdown"></asp:DropDownList>

                                        </li>


                                        <li class="clear"></li>


                                    </div>
                                    <div class="divL" style="width: 310px;">

                                        <li>


                                            <asp:Label ID="Label12" runat="server" CssClass="label" Width="80" Text="Rack"></asp:Label>
                                            <asp:DropDownList ID="ddlRack" onkeydown="EnterTab(event,'ddlBin');" runat="server" TabIndex="3" CssClass="dropdown"></asp:DropDownList>

                                        </li>
                                        <li>


                                            <asp:Label ID="Label13" runat="server" CssClass="label" Width="80" Text="Bin"></asp:Label>
                                            <asp:DropDownList ID="ddlBin" onkeydown="EnterTab(event,'txtItemShortName');" runat="server" TabIndex="3" CssClass="dropdown"></asp:DropDownList>

                                        </li>
                                        <li>
                                            <asp:Label ID="Label1" runat="server" Width="80px" CssClass="label" Text="Item Name"></asp:Label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtItemShortName" onkeydown="EnterTab(event,'txtBatch');" MaxLength="150" TabIndex="4" />

                                        </li>

                                          <li>
                                            <asp:Label ID="Label2" runat="server" Width="80px" CssClass="label" Text="Batch"></asp:Label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtBatch" onkeydown="EnterTab(event,'btnAdd');" MaxLength="20" TabIndex="4" />

                                        </li>










                                    </div>





                                    <li class="clear"></li>



                                    <li class="btnMargin" style="padding-left: 85px">
                                        <asp:Button Text="Search" ID="btnSearch" runat="server" OnClick="btnSearch_Click" TabIndex="4" ValidationGroup="add" CssClass="button" />
                                        <asp:Button Text="Clear" ID="btnClearSearch" runat="server" CssClass="button" TabIndex="5" OnClick="btnClearSearch_Click" />
                                    </li>

                                    <li class="clear"></li>
                                    <li class="clear"></li>
                                    <div class="divSeparation"></div>


                                    <div style="width: 900px; overflow: auto; height: 350px; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">

                                        <asp:GridView ID="gvOpening" runat="server" CssClass="grid"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                            BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowPaging="false" PageSize="10" DataKeyNames="ID_Stock,FK_Items" Style="width: 100% !important;">
                                            <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                            <Columns>

                                                <asp:BoundField DataField="Item" HeaderText="Item" />
                                                <asp:BoundField DataField="BatchNo" HeaderText="Batch No" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="ExpiryDate" HeaderText="Exp.Date" />
                                                <asp:BoundField DataField="PurchaseRate" HeaderText="Pur.Rate" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="ActualPurRate" HeaderText="Act.Purrate" ItemStyle-HorizontalAlign="Right" />

                                                <asp:BoundField DataField="SalPrice" HeaderText="Sale Price" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="ActualSalPrice" HeaderText="Act.Salprice" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="CurrentStock" HeaderText="Cur.Stock" ItemStyle-HorizontalAlign="Right" />
                                                <asp:TemplateField HeaderText="OpeningStock" ItemStyle-Width="150">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOpeningStock" runat="server" Text='<%# Eval("OpeningStock") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="OpeningStock" HeaderText="PrevOpeningStock" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>

                                                <asp:BoundField DataField="PurchaseTaxAmount" HeaderText="PurchaseTaxAmounte" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="MRP" HeaderText="MRP" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"  />
                                                <asp:BoundField DataField="IncludeStax" HeaderText="IncludeStax" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="SaletaxAmount" HeaderText="SaletaxAmount" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="PurchaseTaxAmount" HeaderText="PurchaseTaxAmount" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="ProfitPer" HeaderText="ProfitPer" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="ProfitAmt" HeaderText="ProfitAmt" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="DiscountAmt" HeaderText="DiscountAmt" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="DiscountPer" HeaderText="DiscountPer" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="FK_TaxSettings" HeaderText="FK_TaxSettings" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>

                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />

                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>


                                    </div>
                                    <div class="divSeparation"></div>
                                    <li class="clear"></li>


                                    <li class="clear"></li>
                                    <li class="btnMargin" style="padding-left: 85px">

                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfStore" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />


                                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" TabIndex="21" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="submit" />
                                        <%--<asp:Button ID="btnClear" Text="Clear" runat="server" TabIndex="22"  OnClick="btnClear_Click"  CausesValidation="false" CssClass="button" />--%>
                         

                                    </li>



                                </ul>
                            </div>
                        </div>

                        <asp:Button ID="btnP" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="pnlPrvlg" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divPrvlg" runat="server" class="PopupHeader">
                                <asp:Label ID="Label10" runat="server" Text="Privilege" CssClass="lblPopupHeader"></asp:Label>
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
    </form>
</body>
</html>
