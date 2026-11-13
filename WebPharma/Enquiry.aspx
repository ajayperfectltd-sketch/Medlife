<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Enquiry.aspx.cs" Inherits="Enquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>

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
    <%--<script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>--%>
    <script type="text/javascript">


        function ClearSearchClick() {
            try {

                $("#txtSearchName").val('');
                $("#ddlUnit").val("0");
                $("#ddlSubUnit").val("0");
                $("#ddlSupplier").val("0");
                $("#ddlStore").val("0");
                $("#ddlGeneric").val("0");
                $("#ddlScedule").val("0");
                $("#ddlCriteria").val("0");
                $("#ddlCategory").val("0");
                $("#ddlSubcategory").val("0");
                $("#ddlManufacturer").val("0");
                $("#ddlReorder").val("0");
                $("#ddlRack").val("0");
                $("#ddlbin").val("0");
                $("#txtBatchNo").val('');
                $("#txtPurchaseRate").val('');
                $("#txtSalesPrice").val('');
                $("#txtMrp").val('');
                $("#txtDoctor").val('');
                $("#txtCustomer").val('');
                $("#ddlCriteria").val("0");
                $("#ddlPurchse").val("0");
                $("#ddlSalesPrice").val("0");
                $('#<%#chkzeorstock.ClientID%>').prop('checked', false);






                }
                catch (ex) {
                    alert(ex);
                }
            }
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
            function pageLoad() {
                //  alert('g');
                //var ctrContainer = "form";
                //setKeynavigation(ctrContainer);



            }
            function BlurRack() {

                var clickButton = document.getElementById("<%= btnRack.ClientID %>");
                clickButton.click();
                $("#<%#ddlbin.ClientID%>").focus();
            }
            function Blurcategory() {

                var clickButton = document.getElementById("<%= btnSubCategor.ClientID %>");
                clickButton.click();
                $("#<%#ddlSubcategory.ClientID%>").focus();
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

                        <div class="user">Item Enquiry</div>
                    </div>
                    <div class="clear"></div>

                    <div class="group_bg2">

                        <div id="divView" runat="server" style="height: 750px;">
                            <div style="width: 950px">
                                <div class="divL">
                                    <ul class="contentul">
                                        <div class="divL" style="width: 380px;">
                                            <li>

                                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                                <asp:TextBox ID="txtSearchName" Width="185px" CssClass="textbox" onBlur="popUpItemsSelect();" TabIndex="1" runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="IbtnItemAdd" runat="server" OnClick="IbtnItemAdd_Click" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 0px 0px 0px 0px; position: absolute;" />


                                            </li>
                                            <li class="clear"></li>

                                        </div>

                                        <li class="clear"></li>
                                        <li>
                                            <asp:Label ID="Label4" runat="server" CssClass="label" Text="Unit"></asp:Label>
                                            <asp:DropDownList ID="ddlUnit" runat="server" TabIndex="2" CssClass="dropdown"></asp:DropDownList>
                                        </li>

                                        <li>
                                            <asp:Label ID="Label12" runat="server" CssClass="label" Text="Sub Unit"></asp:Label>
                                            <asp:DropDownList ID="ddlSubUnit" runat="server" CssClass="dropdown" TabIndex="3"></asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label2" runat="server" CssClass="label" Text="Supplier"></asp:Label>
                                            <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="true" CssClass="dropdown" TabIndex="4"></asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label3" runat="server" CssClass="label" Text="Store"></asp:Label>
                                            <asp:DropDownList ID="ddlStore" runat="server" AutoPostBack="true" CssClass="dropdown" TabIndex="5"></asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label8" runat="server" CssClass="label" Text="Generic"></asp:Label>
                                            <asp:DropDownList ID="ddlGeneric" runat="server" AutoPostBack="true" CssClass="dropdown" TabIndex="6"></asp:DropDownList>
                                        </li>



                                        <div class="divL" style="width: 380px;">
                                            <li>
                                                <asp:DropDownList ID="ddlCriteria" runat="server" Style="padding: 0px; width: 40px; height: 22px; margin: 0px 0px 0px 0px;" AutoPostBack="true" CssClass="dropdown" TabIndex="7">
                                                    <asp:ListItem Text=">" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text=">=" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="<" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="<=" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="=" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="lblMRP" runat="server" CssClass="label" Text="MRP"></asp:Label>
                                                <asp:TextBox ID="txtMrp" CssClass="textbox" Width="169px" TabIndex="8" runat="server"></asp:TextBox>



                                            </li>
                                            <li class="clear"></li>

                                        </div>
                                        <div class="divL" style="width: 380px;">
                                            <li>
                                                <asp:DropDownList ID="ddlPurchse" runat="server" Style="padding: 0px; width: 40px; height: 22px; margin: 0px 0px 0px 0px;" AutoPostBack="true" CssClass="dropdown" TabIndex="9">
                                                    <asp:ListItem Text=">" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text=">=" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="<" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="<=" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="=" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="Label13" runat="server" CssClass="label" Text="Purchase Rate"></asp:Label>
                                                <asp:TextBox ID="txtPurchaseRate" CssClass="textbox" Width="169px" TabIndex="10" runat="server"></asp:TextBox>



                                            </li>
                                            <li class="clear"></li>

                                        </div>
                                        <li class="clear"></li>
                                        <div class="divL" style="width: 380px;">
                                            <li>

                                                <asp:DropDownList ID="ddlSalesPrice" runat="server" Style="padding: 0px; width: 40px; height: 22px; margin: 0px 0px 0px 0px;" AutoPostBack="true" CssClass="dropdown" TabIndex="11">
                                                    <asp:ListItem Text=">" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text=">=" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="<" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="<=" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="=" Value="4"></asp:ListItem>
                                                </asp:DropDownList>


                                                <asp:Label ID="Label14" runat="server" CssClass="label" Text="Sales Price"></asp:Label>
                                                <asp:TextBox ID="txtSalesPrice" CssClass="textbox" Width="169px" TabIndex="12" runat="server"></asp:TextBox>


                                            </li>
                                            <li class="clear"></li>

                                            <li>
                                                <asp:Label ID="Label17" runat="server" CssClass="label" Text="Reorder"></asp:Label>
                                                <asp:DropDownList ID="ddlReorder" runat="server" TabIndex="13" CssClass="dropdown">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Minimum Level" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Maximum Level" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Danger Level" Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                            </li>

                                        </div>

                                    </ul>
                                </div>



                                <%-- </div>--%>

                                <div class="divR">
                                    <ul class="contentul">
                                        <li>
                                            <asp:Label ID="Label10" runat="server" CssClass="label" Text="Category"></asp:Label>
                                            <asp:DropDownList ID="ddlCategory" runat="server" onBlur="return Blurcategory();" CssClass="dropdown" TabIndex="14"></asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label1" runat="server" CssClass="label" Text="Sub Category"></asp:Label>
                                            <asp:DropDownList ID="ddlSubcategory" runat="server" AutoPostBack="true" CssClass="dropdown" TabIndex="15"></asp:DropDownList>
                                        </li>

                                        <li class="clear"></li>

                                        <li>
                                            <asp:Label ID="Label32" runat="server" CssClass="label" Text="Manufacturer"></asp:Label>
                                            <asp:DropDownList ID="ddlManufacturer" runat="server" CssClass="dropdown" TabIndex="16"></asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label9" runat="server" CssClass="label" Text="Rack"></asp:Label>
                                            <asp:DropDownList ID="ddlRack" runat="server" CssClass="dropdown" onBlur="return BlurRack();" TabIndex="17"></asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label5" runat="server" CssClass="label" Text="Bin"></asp:Label>
                                            <asp:DropDownList ID="ddlbin" runat="server" CssClass="dropdown" TabIndex="18"></asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label7" runat="server" CssClass="label" Text="Batch No"></asp:Label>
                                            <asp:TextBox ID="txtBatchNo" CssClass="textbox" TabIndex="19" runat="server"></asp:TextBox>

                                        </li>
                                        <li class="clear"></li>
                                        <li>
                                            <asp:Label ID="Label11" runat="server" CssClass="label" Text="Schedule"></asp:Label>
                                            <asp:DropDownList ID="ddlScedule" runat="server" AutoPostBack="true" CssClass="dropdown" TabIndex="20"></asp:DropDownList>
                                        </li>
                                        <li class="clear"></li>
                                        <div class="divL" style="width: 380px;">
                                            <li>

                                                <asp:Label ID="Label15" runat="server" CssClass="label" Text="Doctor Name"></asp:Label>
                                                <asp:TextBox ID="txtDoctor" Width="185px" CssClass="textbox" onBlur="popUpDoctorSelect();" TabIndex="22" runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="imgDoctor" runat="server" TabIndex="21" OnClick="imgDoctor_Click" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 0px 0px 0px 0px; position: absolute;" />


                                            </li>
                                            <li class="clear"></li>

                                        </div>
                                        <div class="divL" style="width: 380px;">
                                            <li>

                                                <asp:Label ID="Label16" runat="server" CssClass="label" Text="Customer Name"></asp:Label>
                                                <asp:TextBox ID="txtCustomer" Width="185px" CssClass="textbox" onBlur="popUpCustomerSelect();" TabIndex="24" runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="imgCustomer" TabIndex="23" runat="server" OnClick="imgCustomer_Click" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 0px 0px 0px 0px; position: absolute;" />


                                            </li>
                                            <%--<li>
                                               <asp:CheckBox ID="chkzeorstock"  style="font-size:15px" Text="ZeroStock" runat="server" />
                                               </li>--%>
                                        </div>
                                        <div class="divL" style="width: 380px; margin-top: 5px">
                                            <li>
                                                <asp:Label ID="lblzero" runat="server" CssClass="label" Style="margin-top: 0px;" Text="Zero Stock"></asp:Label>

                                                <asp:CheckBox ID="chkzeorstock" runat="server" Style="margin-top: 10px;" TabIndex="25" />

                                            </li>
                                        </div>
                                        <li class="clear"></li>


                                        <%--  <li>
                                            <asp:Label ID="Label15" runat="server" CssClass="label"  Text="PurchaseRate"></asp:Label>
                                            <asp:TextBox ID="txtPurchaseRate" CssClass="textbox" TabIndex="1" runat="server" ></asp:TextBox>
                                        </li>--%>


                                        <%-- <li>

                                            <asp:Label ID="Label14" runat="server" CssClass="label"  Text="SalesPrice"></asp:Label>
                                            <asp:TextBox ID="txtSalesPrice" CssClass="textbox" TabIndex="1" runat="server" ></asp:TextBox>
                                        </li>--%>
                                    </ul>

                                </div>
                                <li class="clear"></li>
                                <li class="btnMargin" style="padding-left: 160px; padding-top: 15px">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="26" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearchClick();" OnClick="btnSearchClear_Click" TabIndex="27" />
                                </li>

                            </div>
                            <ul class="contentul">
                                <asp:GridView ID="gvEnquiry" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" Width="900px" BorderStyle="Solid" OnSorting="gvPriviousPurchase_Sorting" AllowPaging="True" OnPageIndexChanging="gvEnquiry_PageIndexChanging" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowSorting="True">
                                    <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" Visible="false" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier" SortExpression="SupplierName" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Item" SortExpression="ItemName" />
                                        <asp:BoundField DataField="InvoiceDate" HeaderText="Date" Visible="false" />
                                        <asp:BoundField DataField="BatchNo" HeaderText="BatchNo" SortExpression="BatchNo" />
                                        <asp:BoundField DataField="ExpiryDate" HeaderText="Exp.Date" SortExpression="ExpiryDate" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="CurrentStock" HeaderText="CurrentStock" SortExpression="CurrentStock" />
                                        <asp:BoundField DataField="PurRate" ItemStyle-HorizontalAlign="Right" HeaderText="Pur.Rate" SortExpression="PurRate" />
                                        <asp:BoundField DataField="MRP" ItemStyle-HorizontalAlign="Right" HeaderText="MRP" SortExpression="MRP" />
                                        <asp:BoundField DataField="SalPrice" ItemStyle-HorizontalAlign="Right" HeaderText="SalesPrice" SortExpression="SalPrice" />



                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle CssClass="hederdesgn" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>

                            </ul>
                            <li class="clear"></li>


                        </div>

                    </div>


                    <asp:HiddenField ID="hfClient" runat="server" />
                    <asp:HiddenField ID="hfUser" runat="server" />
                    <asp:HiddenField ID="hdfValue" runat="server" />
                    <asp:HiddenField ID="hfSelectedItem" runat="server" />
                    <asp:HiddenField ID="hfDoctoritem" runat="server" />
                    <asp:HiddenField ID="hfCustomeritem" runat="server" />

                    <asp:Button ID="btnRack" Text="Hide" runat="server" Style="display: none;" CausesValidation="false" OnClick="btnRack_Click" />
                    <asp:Button ID="btnSubCategor" runat="server" Style="display: none;" Text="Hide" OnClick="btnSubCategor_Click" CausesValidation="false" />


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
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
