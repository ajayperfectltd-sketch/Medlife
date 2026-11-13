<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinalAccountProcess.aspx.cs" Inherits="FinalAccountProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc1" TagName="MedLifePageHelp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::FinaAccountProcess::</title>

    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/Navigation.js"></script>
    <script type="text/css">
        .between {
            border: 3px dotted #0099CC;
            margin-left:10px;
            margin-right:10px;
        }
        .parent
        { 
            display:-moz-box; /* Firefox */
            display:-webkit-box; /* Safari and Chrome */
            display:-ms-flexbox; /* Internet Explorer 10 */
            display:box;
            width:70%;
        }
        .child2
        {
            -moz-box-flex:5.0; /* Firefox */
            -webkit-box-flex:5.0; /* Safari and Chrome */
            -ms-flex:9.0; /* Internet Explorer 10 */
            box-flex:9.0;
        }
    </script>

    <script type="text/javascript">



        function DecimalOrIntValidation(eventRef) {

            CheckNumWithDecimal(eventRef);

        }
        function ConfirmSalesOk() {
            if (Page_ClientValidate("sub")) {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to save this Process...?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        }
        function onBlur(txt, colIndex) {
            var row = txt.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            document.getElementById('hfRowindex').value = rowIndex;
            document.getElementById('hfColindex').value = colIndex;

            $("#btnEdit").click();
        }
    </script>

    <style type="text/css">
        .between {
            width: 691px;
            height: -15px;
            margin-left: 0px;
        }
    </style>

</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5">
                    <div class="heading2">
                        <div class="user">Final Account Process </div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                                <uc1:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                                <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" Width="37" Height="38" runat="server" TabIndex="13" OnClientClick="ShowAddDiv(); ClearAdd();releasebtn();" />

                                <%--<asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" TabIndex="12" />--%>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="main_bg" style="width: 120%;">

                        <ul class="contentul">
                            <tr>
                                <div class="divL" style="width: 200px;">

                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfRowindex" runat="server" />
                                    <asp:HiddenField ID="hfColindex" runat="server" />
                                    <td style="width: 100px">

                                        <asp:Label class="label" ID="Label3" runat="server" Text="Type" Width="75"></asp:Label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddltype" TabIndex="1" AutoPostBack="true" Width="100px">
                                            <asp:ListItem Value="TD">Trading</asp:ListItem>
                                            <asp:ListItem Value="PL">Profit & Loss</asp:ListItem>
                                            <asp:ListItem Value="BL">Balance Sheet</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </div>
                                <div class="divL" style="width: 206px;">
                                    <td style="width: 100px">
                                        <label class="label" style="width: 75px">Store</label>
                                        <asp:DropDownList runat="server" Width="100px" CssClass="dropdownMandatory" ID="ddlStore" TabIndex="2">
                                        </asp:DropDownList>
                                    </td>
                                </div>
                                <div class="divL" style="width: 268px;">
                                    <td style="width: 75px">
                                        <label class="label" style="width: 75px">Trans Date</label>
                                        <asp:TextBox ID="txtTransDate" CssClass="textbox" runat="server" Width="100px" TabIndex="3"></asp:TextBox>
                                        <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtTransDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtTransDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                        </cc:ValidatorCalloutExtender>

                                        </li>   <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Account Head Code required" ControlToValidate="txtAccountHeadCode" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                            </cc:ValidatorCalloutExtender>--%>
                           
                                    </td>
                                </div>
                                <div class="divL" style="width: 100px;">
                                    <td style="width: 75px">
                                        <asp:Button ID="btnDisplay" Text="Display" Width="120px" CssClass="button" runat="server"
                                            TabIndex="4" ValidationGroup="SaleSearch" OnClick="btnDisplay_Click" />
                                    </td>
                                </div>

                            </tr>

                        </ul>
                        <ul class="contentul" style="width: 78%">
                            <div style="width: 100%">

                                <hr class="between" />
                            </div>

                        </ul>

                        <ul class="contentul">
                            <tr>
                                <div class="divL" style="width: 345px;">
                                    <td style="width: 100px">

                                        <asp:Label class="label" ID="Label1" runat="server" Text="Name" Width="143px"></asp:Label>
                                        <asp:TextBox CssClass="textboxMandatory" ID="txtName" runat="server" Width="147px" TabIndex="5"
                                            onblur="return popUpStockSelectBarcode();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                    </td>
                                </div>
                                <div class="divL" style="width: 300px;">
                                    <td style="width: 100px">
                                        <asp:Label class="label" ID="lbDebit" runat="server" Text="Debit" Width="50px"></asp:Label>
                                        <asp:TextBox ID="txtDebit" CssClass="textbox" runat="server" Width="100px" TabIndex="6" onkeypress="DecimalOrIntValidation(event);"></asp:TextBox>
                                    </td>
                                </div>

                            </tr>
                        </ul>
                        <div>

                            <ul class="contentul">
                                <tr>
                                    <div class="divL" style="width: 143px;">
                                        <td style="width: 100px">

                                            <asp:Label class="label" ID="Label2" runat="server" Text="AccountGroup" Width="135px"></asp:Label>

                                        </td>
                                    </div>

                                    <div class="divL" style="width: 199px;">
                                        <td style="width: 100px">
                                            <asp:DropDownList runat="server" ID="ddlAccountGroup" Width="154px" CssClass="dropdownMandatory" TabIndex="7">
                                            </asp:DropDownList>
                                        </td>
                                    </div>

                                    <div class="divL" style="width: 268px;">
                                        <td style="width: 75px">
                                            <asp:Label class="label" ID="lbCredit" runat="server" Text="Credit" Width="51px"></asp:Label>
                                            <asp:TextBox ID="txtCredit" CssClass="textbox" runat="server" Width="100px" onkeypress="DecimalOrIntValidation(event);" TabIndex="8"></asp:TextBox>


                                            </li>   <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Account Head Code required" ControlToValidate="txtAccountHeadCode" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                            </cc:ValidatorCalloutExtender>--%>
                           
                                        </td>
                                    </div>
                                    <div class="divL" style="width: 100px;">
                                        <td style="width: 75px">
                                            <asp:Button ID="btnAddItems" Text="Add" Width="120px" CssClass="button" runat="server" TabIndex="9" ValidationGroup="add"
                                                OnClick="btnAddItems_Click" />
                                            <div class="divL" id="ClearD2" style="width: 100px; margin-left: 35px;">
                                                <asp:Button Text="Add" ID="btnEdit" runat="server" TabIndex="10" ValidationGroup="add"
                                                    CssClass="button" OnClick="btnEdit_Click" Style="display: none" />
                                            </div>
                                        </td>
                                    </div>
                                    <li class="clear"></li>
                                    <div class="divSeparation"></div>
                                    <li class="clear"></li>
                                </tr>
                            </ul>
                        </div>
                        <hr class="between" />
                        <div>
                            <ul class="contentul">
                                <%--  <div style="width: 930px; overflow: auto; height: 100px; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">--%>

                                <li>
                                    <asp:GridView ID="gvReport" runat="server" CssClass="grid" Style="margin-left: 5px;"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="920px"
                                        BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical"
                                        PageSize="100" OnRowDataBound="gvReport_RowDataBound" ShowFooter="True">
                                        <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                        <Columns>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdfAhcode" runat="server" Value='<%# Eval("AhCode") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:BoundField DataField="AhCode" HeaderText="Code"/>--%>
                                            <asp:TemplateField HeaderText="Name" FooterStyle-CssClass="active">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="Label4" runat="server" Font-Size="Medium" Text="Total"></asp:Label>

                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Debit" FooterStyle-CssClass="active">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDebit" runat="server" Width="90%" Value='<%# Eval("Debit") %>' onkeypress="DecimalOrIntValidation(event);" onBlur="onBlur(this,2);" Style="text-align: right;" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtFtDebit" Style="text-align: right;" ReadOnly="true" Width="90%" runat="server" Font-Size="Medium" BackColor="#07928f" Font-Bold="true" ForeColor="White"></asp:TextBox>

                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" />

                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Credit">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCredit" runat="server" Value='<%# Eval("Credit") %>' onkeypress="DecimalOrIntValidation(event);" onBlur="onBlur(this,3);" Style="text-align: right;" Width="90%" />
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:TextBox ID="txtFtCredit" Width="90%" ReadOnly="true" runat="server" Style="text-align: right;" Font-Size="Medium" BackColor="#07928f" Font-Bold="true" ForeColor="White"></asp:TextBox>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" />

                                            </asp:TemplateField>
                                        </Columns>

                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="30px" />
                                        <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                        <FooterStyle BackColor="#07928f" Font-Bold="true" ForeColor="White" Height="25px" />
                                    </asp:GridView>
                                </li>
                            </ul>
                        </div>
                        <li class="clear"></li>
                        <li class="clear"></li>
                        <li class="clear"></li>
                        <div class="divSeparation"></div>
                        <hr class="between" />
                        <div class="Pager">
                            <asp:Button ID="btnSave" Text="Save" Width="120px" CssClass="button" OnClick="btnSave_Click" TabIndex="11"
                                OnClientClick="ConfirmSalesOk();" runat="server" />
                        </div>
                        <li class="clear"></li>
                        <div class="divSeparation"></div>
                        <li class="clear"></li>
                    </div>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                    <li class="clear"></li>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
