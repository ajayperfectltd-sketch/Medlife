<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultipleStockCorrection.aspx.cs" Inherits="MultipleStockCorrection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Multiple Stock Entry::</title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <script type="text/javascript">
        var submit = 0;
        function disablebutn() {
            try {

                window.setTimeout('delaysubmit()', 50);
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
            $("#<%#txtItemShortName.ClientID%>").val('');
            $("#<%#ddlItem.ClientID%>").val('0');
            $('#GvStockDetails').remove();
            var toDate = new Date();
            $("#txtExpiryDate").val(toDate.format("dd/MM/yyyy"));
        }
        function ClearSearch() {
           
        }
        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#imgPopUp.ClientID%>").focus();
        }

        function popUpStockSelectClick() {

            try {
                var ItemName = $("#txtItemShortName").val();
                url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val();
                popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                ",statusbar=no,menubar=no,resizable=0,width=1015," +
                "height=650,left = 200,top = 100");
                popUpObj.focus();
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
                    url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val();

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
    <form id="frmmultipleEntry" runat="server" autocomplete="off">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        <div class="search" style="width: 670px;">Multiple Stock Correction</div>
                       <%-- <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/add1.png" Width="37" Height="38" TabIndex="22" runat="server" OnClick="imgbtnAdd_Click" />
                        </div>
                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" TabIndex="21" runat="server" OnClick="imgbtnView_Click" />
                        </div>--%>
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="display: none;">
                            <div class="divL">
                                <ul class="contentul">
                                    <li >
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Item"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory"  ID="txtItemShortName" onBlur="popUpStockSelect();" Width="130px" MaxLength="150" TabIndex="1"  /> 
                                        <asp:Button ID="btnItem" runat="server" Text="Item" Style="display: none;" />
                                        <asp:ImageButton ID="imgPopUp" runat="server" OnClientClick="return  popUpStockSelectClick();" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 0px 0px 0px 6px;position:absolute " TabIndex="2" />
                                         <asp:RequiredFieldValidator ID="rfvitem" runat="server" ErrorMessage="Select an Item" ControlToValidate="txtItemShortName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="rfvitem"></cc:ValidatorCalloutExtender>
                                    </li>
                                    <li style="display:none;">
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item"></asp:Label>
                                        <asp:DropDownList ID="ddlItem" runat="server" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true"  CssClass="dropdownMandatory"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select an Item" ControlToValidate="ddlItem" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2"></cc:ValidatorCalloutExtender>
                                    </li>
                                    <li style="display:none;">
                                        <asp:Label ID="Label16" runat="server" CssClass="label" Text="Unit"></asp:Label>
                                        <asp:DropDownList ID="ddlUnit" runat="server" TabIndex="2" AutoPostBack="true" CssClass="dropdownMandatory"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ErrorMessage="Select a Unit" ControlToValidate="ddlUnit" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender61" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator61"></cc:ValidatorCalloutExtender>
                                    </li>
                                    
                                </ul>
                            </div>
                            <asp:HiddenField ID="hfClient" runat="server" />
                            <asp:HiddenField ID="hfUser" runat="server" />
                            <asp:HiddenField ID="hfIsAdd" runat="server" />
                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                            <asp:HiddenField ID="hfIsDelete" runat="server" />
                            <li class="clear"></li>
                                <li class="clear"></li>
                            <div id="DivStockDetails" runat="server" style="width: 1000px; overflow: auto; scrollbar-face-color: blue; height: 350px;">


                                    <asp:GridView ID="GvStockDetails" runat="server" CssClass="grid"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="950px" Style="margin-left: 5px;"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100">
                                        <AlternatingRowStyle BackColor="#CCE0EB" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID_Stock" Visible="false" >
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblStock" CssClass="label" Style="width: 100px; text-align: right;" Text='<%# Eval("ID_Stock") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FK_Store" Visible="false" >
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblStore" CssClass="label" Style="width: 100px; text-align: right;" Text='<%# Eval("FK_Store") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Item" DataField="Item" Visible="false"/>
                                            <asp:BoundField HeaderText="Store" DataField="Store" />
                                            <asp:BoundField HeaderText="Pur. Rate" DataField="PurRate" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="MRP" DataField="MRP" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="R.Price" DataField="RetailPrice" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="W.Price" DataField="WholeSalePrice" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="C.Price" DataField="PrivilagePrice" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="BatchNo" DataField="BatchNo" />
                                            <asp:BoundField HeaderText="ExpiryDate" DataField="ExpiryDate" Visible="false" />
                                            <asp:TemplateField HeaderText="OpeningStock" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblOpeningStock" CssClass="label" Style="width: 100px; text-align: right;" Text='<%# Eval("OpeningStock") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CurrentStock" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCurrentStock" CssClass="label" Style="width: 100px; text-align: right;" Text='<%# Eval("CurrentStock") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ActualStock" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtActualStock" onkeypress="CheckNumWithDecimal(event);" AutoComplete="off" CssClass="textbox" Style="width: 100px; text-align: right;" Text='<%# Eval("OpeningStock") %>' TabIndex="3" />
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
                                </div>
                            <li class="clear"></li>
                                <li class="clear"></li>
                            <li style="text-align:right;">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="19" OnClientClick="disablebutn();" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="20" />
                                </li>
                        </div>
                        <div id="divView" runat="server" style="display: none;">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/toastr.js"></script>
    </form>
</body>
</html>
