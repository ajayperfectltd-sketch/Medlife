<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpeningBalance.aspx.cs" Inherits="OpeningBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc1" TagName="MedLifePageHelp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::OpeningBalance::</title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/Navigation.js"></script>
    <script type="text/javascript">

        function GridTotal() {
            alert('h');

            var sum = 0, debit = 0;
            $.each($("table[id*=" + 'gdvBalance' + "] tr"), function (index, item) {
                // alert('c');
                var val = $(item).find("[id*=gdvBalance_txtDebitbalance_]").val() == "" ? "0" : $(item).find("[id*=gdvBalance_txtDebitbalance_]").val();

                alert(val);
                // var val = $(item).find("[id*=gdvBalance_txtDebitbalance_]").text();
                // alert(val);
                var Creditval = $(item).find("[id*=gdvBalance_txtCreditbalance_]").val() == "" ? "0" : $(item).find("[id*=gdvBalance_txtCreditbalance_]").val();
                sum += parseFloat(val != null ? (val != '' ? val : '0') : '0');
                debit += parseFloat(val != null ? (Creditval != '' ? Creditval : '0') : '0');
                alert(sum);
                alert(debit);


            });

            $("#lblDebit").text(sum.toFixed(3));
            $("#lblCredit").text(debit.toFixed(3));

            $("#lblToatCredit").text(sum.toFixed(3));


        }
        //function LoadGrid() {
        //    $("#btnGriddata").click();

        //}
        function LoadAccountgroup() {
            $("#btnbindAccountgroup").click();

        }
        function ClearAdd() {

            $("#ddlAccounttype").val("0");
            $("#ddlAccountgrp").val("0");
            $("#ddlAccountsubgrp").val("0");

        }
        function ShowAddDiv() {

            $("#divAdd").show();
            $("#divView").hide();
        }

        function LoadAccountSubgroup() {
            $("#btnbindAccountSubgroup").click();

        }
        function CredibalanceChange(id, type, txt, colIndex) {
            //alert('1');
            // alert(type);
            var sum = 0, debit = 0;
            $.each($("table[id*=" + 'gdvBalance' + "] tr"), function (index, item) {

                var val = $(item).find("[id*=gdvBalance_txtDebitbalance_]").val() == "" ? "0" : $(item).find("[id*=gdvBalance_txtDebitbalance_]").val();
                var valCredit = $(item).find("[id*=gdvBalance_txtCreditbalance_]").val() == "" ? "0" : $(item).find("[id*=gdvBalance_txtCreditbalance_]").val();
                if (valCredit != 0 && colIndex == 2) {
                    $(item).find("[id*=txtDebitbalance_]").val(0.00);
                    return;
                }
                else if (val != 0 && colIndex == 3) {
                    $(item).find("[id*=txtCreditbalance_]").val(0.00);
                    return;
                }
                //return false;
                // else
                // return true;
                // var PurPrice = $(item).find("[id*=gvPurchaseItems_lblPurchaseRate_]").text();
               

            });
            var row = txt.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            document.getElementById('hfRowindex').value = rowIndex;
            document.getElementById('hfColindex').value = colIndex;

            $("#btnEdit").click();
        }
        //function onBlur(txt, colIndex) {
        //    var row = txt.parentNode.parentNode;
        //    var rowIndex = row.rowIndex - 1;
        //    document.getElementById('hfRowindex').value = rowIndex;
        //    document.getElementById('hfColindex').value = colIndex;

        //    $("#btnEdit").click();
        //}

    </script>
    <style type="text/css">
        .dropdow {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5">
                    <div class="heading2">
                        <div class="user">Opening Balance </div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                                <uc1:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                                <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" Width="37" Height="38" runat="server"  OnClientClick="ShowAddDiv(); ClearAdd();releasebtn();" />

                                <%--<asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server"  />--%>
                            </div>
                        </asp:Panel>
                        <div class="main_bg" style="width: 120%;">

                            <ul class="contentul" id="Container">


                                <div id="divAdd" runat="server" style="display: block;">
                                      <asp:HiddenField ID="hfRowindex" runat="server" />
                                    <asp:HiddenField ID="hfColindex" runat="server" />

                                    <div class="divL">
                                        <li>
                                            <label class="label">Final Account Type</label>

                                            <asp:DropDownList runat="server" CssClass="dropdown" ID="ddlAccounttype" onchange="LoadAccountgroup();" TabIndex="1">
                                                <asp:ListItem Text="select" Value="ALL" />
                                                <asp:ListItem Text="Asset" Value="A" />
                                                <asp:ListItem Text="Liability" Value="L" >
                                               </asp:ListItem>
                                            </asp:DropDownList>
                                        </li>


                                        <li>
                                            <label class="label">Account Group</label>
                                            <asp:DropDownList runat="server" CssClass="dropdown" onchange="LoadAccountSubgroup();" ID="ddlAccountgrp" TabIndex="2">
                                            </asp:DropDownList>
                                        </li>
                                        <li>
                                            <label class="label">Account sub Group</label>
                                            <asp:DropDownList runat="server" Width="219px" CssClass="dropdow" ID="ddlAccountsubgrp" TabIndex="3">
                                            </asp:DropDownList>
                                        </li>
                                    </div>
                                    <div class="divL">
                                        <li>
                                            <label class="label">Fin Year</label>
                                            <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlFinyear" TabIndex="4" AutoPostBack="true" onkeydown="return KEyCode();" Style="margin-left: 10px;">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvFinyear" runat="server" ErrorMessage="Store required" ControlToValidate="ddlFinyear" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="rfvFinyear">
                                            </cc:ValidatorCalloutExtender>

                                        </li>
                                        <li style="display: none">
                                            <label class="label">Store</label>
                                            <asp:DropDownList runat="server" CssClass="dropdown" ID="ddlStore" TabIndex="5" AutoPostBack="true" Style="margin-left: 10px;">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="btnMargin">

                                            <asp:Button Text="Show" ID="btnShow" runat="server" CssClass="button" TabIndex="6" OnClick="btnShow_Click" CausesValidation="false" Width="100px" />
                                        </li>

                                    </div>


                                    <br class="clear" />
                                    <br class="clear" />
                                    <div class="divSeparation"></div>
                                    <div style="width: 65%">

                                        <hr class="between" />
                                    </div>
                                    <div class="divL" id="ClearD2" style="width: 100px; margin-left: 35px;">
                                                <asp:Button Text="Add" ID="btnEdit" runat="server" TabIndex="7" ValidationGroup="add"
                                                    CssClass="button" OnClick="btnEdit_Click" Style="display: none" />
                                            </div>

                                    <div>
                                        <asp:GridView ID="gdvBalance" runat="server" CssClass="grid" Style="margin-left: 5px;"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="920px"
                                            BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical"
                                            PageSize="100" ShowFooter="True">
                                            <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Code">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    <asp:Label ID="Label4" runat="server" Font-Size="Medium" Text="Total"></asp:Label>

                                                </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                                <asp:TemplateField HeaderText="Debit Balance" HeaderStyle-HorizontalAlign="Center" 
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" ItemStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDebitbalance" onblur="CredibalanceChange(id,'txtDebitbalance',this,2);"  Style="text-align: right;" TabIndex="8" runat="server" Width="120px" Text='<%#Eval("DebitBal") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    <asp:TextBox ID="txtFtDebit" Style="text-align: right;" ReadOnly="true" Width="100%" runat="server" Font-Size="Medium" BackColor="#07928f" Font-Bold="true" ForeColor="White"></asp:TextBox>

                                                </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit Balance" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" ItemStyle-Width="120px">
                                    
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCreditbalance" onBlur="CredibalanceChange(id,'txtCreditbalance',this,3);" Style="text-align: right;" TabIndex="9" runat="server" Width="120px" Text='<%#Eval("CreditBal") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>


                                                    <asp:TextBox ID="txtFtCredit" Width="100%" ReadOnly="true" runat="server" Style="text-align: right;" Font-Size="Medium" BackColor="#07928f" Font-Bold="true" ForeColor="White"></asp:TextBox>
                                                </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
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

                                        <asp:Label ID="lblToatCredit" runat="server"></asp:Label>

                                        </li>

                                        <li class="btnMargin">
                                            <asp:Button Text="Save" ID="btnSave" runat="server" OnClick="btnSave_Click" TabIndex="9" CssClass="button" />
                                            <asp:Button Text="Clear" ID="btnClearItems" runat="server" OnClientClick="ClearAdd();" OnClick="btnClearItems_Click" CssClass="button" TabIndex="10" CausesValidation="false" />
                                        </li>



                                    </div>
                                    <div>
                                        <asp:Button ID="btnbindAccountgroup" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnbindAccountgroup_Click" />
                                        <asp:Button ID="btnbindAccountSubgroup" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnbindAccountSubgroup_Click" />

                                        <%--<asp:Button ID="btnGriddata" runat="server" CausesValidation="false" Text="Hide" Style="display: none;"    OnClick="btnGriddata_Click" />--%>
                                        <div class="divR">
                                            <asp:HiddenField ID="hfClient" runat="server" />
                                            <asp:HiddenField ID="hfUser" runat="server" />
                                            <asp:HiddenField ID="hfIsAdd" runat="server" />
                                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                            <asp:HiddenField ID="hfIsDelete" runat="server" />
                                            <asp:HiddenField ID="hfIssueOut" runat="server" />
                                            <asp:HiddenField ID="hdfFKStock" runat="server" />
                                            <asp:HiddenField ID="hdfRowindex" runat="server" />
                                            <asp:HiddenField ID="hdfFkItem" runat="server" />
                                            <asp:HiddenField ID="hdfcategory" runat="server" />
                                            <asp:HiddenField ID="hdfCurrentStock" runat="server" />
                                            <asp:HiddenField ID="hdfPacked" runat="server" />
                                            <asp:HiddenField ID="hfStore" runat="server" />

                                        </div>

                                    </div>
                                </div>
                                <div id="divView" runat="server" style="display: none;">
                                </div>
                            </ul>


                        </div>

                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
        </div>
    </form>
</body>
</html>
