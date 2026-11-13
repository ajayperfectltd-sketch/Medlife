<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VoucherMaster.aspx.cs" Inherits="VoucherMaster" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Voucher Master::</title>
    <link href="css/main.css" rel="stylesheet" />

    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <script src="js/toastr.js"></script>
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

        function ClearAdd() {
            submit = 0;
            $("#txtShortName").val('');
            $("#txtName").val('');
            $("#txtDiscPercentage").val('0');
            $("#txtDiscAmount").val('0');
            $("#txtAmount").val('0');
            $("#txtValidity").val('0');
            $("#ddlAcSubHead").val('0');
            $("#txtRemarks").val('');
            $("#cbxActive").prop("checked", true);
            $("#hdfVoucherMaster").val("");
            $("#rbtnDiscount").prop('checked', true)
            GetDiscountDetails();
            $("#liDiscperc").show();
            $("#liDiscAmount").show();
            ValidatorEnable(document.getElementById("rfvPercentage"), true);
            ValidatorEnable(document.getElementById("rfvDiscAmount"), true);
            ValidatorEnable(document.getElementById("rgvDiscPerc"), true);
            ValidatorEnable(document.getElementById("rgvDiscAmt"), true);
            $("#txtValidity").removeAttr('readonly');
            $("#hdfIsReferance").val("0");
            RemoveAttribute();
            $("#txtShortName").focus();
            return false;
        }
        function ClearSearch() {
            $("#txtSearchShortName").val('');
            $("#txtSearchName").val('');
            $("#ddlSearchAccountHead").val('0');
            $("#ddlSearchType").val('0');
            $("#txtSearchShortName").focus();

        }
        function ShowAddDiv() {

            $("#divAdd").show();
            $("#divView").hide();
            $("#txtShortName").focus();
            return false;
        }

        function GetDiscountDetails() {
            try {
                $("#divDiscount").show();
                $("#liDiscperc").show();
                $("#liDiscAmount").show();
                $("#liGift").hide();
                ValidatorEnable(document.getElementById("rfvPercentage"), true);
                ValidatorEnable(document.getElementById("rfvDiscAmount"), true);
                ValidatorEnable(document.getElementById("rfvAmount"), false);
                ValidatorEnable(document.getElementById("rgvgift"), false);
                ValidatorEnable(document.getElementById("rgvDiscPerc"), true);
                ValidatorEnable(document.getElementById("rgvDiscAmt"), true);
                var perc = ($("#txtDiscPercentage").val() == "" || $("#txtDiscPercentage").val() == null) ? "0" : $("#txtDiscPercentage").val();
                var amt = ($("#txtDiscAmount").val() == "" || $("#txtDiscAmount").val() == null) ? "0" : $("#txtDiscAmount").val();
                if (parseFloat(amt) > 0) {

                    Amount();
                }
                else if (parseFloat(perc) > 0) {

                    Percentage();
                }
            }
            catch (e) {
                alert(e);
            }
        }
        function GetGiftDetals() {
            try {
                $("#divDiscount").hide();
                $("#liGift").show();
                ValidatorEnable(document.getElementById("rfvPercentage"), false);
                ValidatorEnable(document.getElementById("rfvDiscAmount"), false);
                ValidatorEnable(document.getElementById("rfvAmount"), true);
                ValidatorEnable(document.getElementById("rgvgift"), true);
                ValidatorEnable(document.getElementById("rgvDiscPerc"), false);
                ValidatorEnable(document.getElementById("rgvDiscAmt"), false);
                ValidatorEnable(document.getElementById("rgvDiscAmt"), false);
            }
            catch (e) {
                alert(e);
            }
        }
        function GetReturnDetails() {
            try {
                $("#divDiscount").hide();
                $("#liGift").hide();
                ValidatorEnable(document.getElementById("rfvPercentage"), false);
                ValidatorEnable(document.getElementById("rfvDiscAmount"), false);
                ValidatorEnable(document.getElementById("rfvAmount"), false);
                ValidatorEnable(document.getElementById("rgvgift"), false);
                ValidatorEnable(document.getElementById("rgvDiscPerc"), false);
                ValidatorEnable(document.getElementById("rgvDiscAmt"), false);
                ValidatorEnable(document.getElementById("rgvDiscAmt"), false);
            }
            catch (e) {
                alert(e);
            }
        }
        function CommonDiscGift(evt) {
            var e = (window.event) ? window.event : evt;
            if (e.target.id == 'rbtnDiscount') {
                GetDiscountDetails();
            }
            else if (e.target.id == 'rbtnReturn') {
                GetReturnDetails();
            }
            else {
                GetGiftDetals();
            }
        }

        function AmountOrPerc(evt) {
            try {
                var e = (window.event) ? window.event : evt;
                var perc = ($("#txtDiscPercentage").val() == "" || $("#txtDiscPercentage").val() == null) ? "0" : $("#txtDiscPercentage").val();
                var amt = ($("#txtDiscAmount").val() == "" || $("#txtDiscAmount").val() == null) ? "0" : $("#txtDiscAmount").val();

                if ((parseFloat(perc) == "0") && (parseFloat(amt) == "0")) {

                    $("#txtDiscPercentage").focus();
                    $("#liDiscperc").show();
                    $("#liDiscAmount").show();
                    ValidatorEnable(document.getElementById("rfvPercentage"), true);
                    ValidatorEnable(document.getElementById("rfvDiscAmount"), true);
                    ValidatorEnable(document.getElementById("rgvDiscPerc"), true);
                    ValidatorEnable(document.getElementById("rgvDiscAmt"), true);
                }

                else {

                    if (e.target.id == 'txtDiscPercentage') {

                        Percentage();


                    }
                    else {
                        Amount();


                    }
                }


            }
            catch (e) {
                alert(e);
            }
        }


        function Percentage() {

            $("#liDiscperc").show();



            $("#liDiscAmount").hide();
            ValidatorEnable(document.getElementById("rfvPercentage"), true);
            ValidatorEnable(document.getElementById("rfvDiscAmount"), false);
            ValidatorEnable(document.getElementById("rgvDiscPerc"), true);
            ValidatorEnable(document.getElementById("rgvDiscAmt"), false);
            $("#txtDiscAmount").val("0");
            $("#txtDiscPercentage").focus();
        }
        function Amount() {


            $("#liDiscperc").hide();
            $("#liDiscAmount").show();
            ValidatorEnable(document.getElementById("rfvPercentage"), false);
            ValidatorEnable(document.getElementById("rfvDiscAmount"), true);
            ValidatorEnable(document.getElementById("rgvDiscPerc"), false);
            ValidatorEnable(document.getElementById("rgvDiscAmt"), true);
            $("#txtDiscPercentage").val("0");
            $("#txtDiscAmount").focus();
        }
        function pageLoad() {

            try {

                $('input:radio').change(function () {

                    CommonDiscGift(event);
                });
                if ($("#hdfVoucherMaster").val().split(',')[0] != "" && $("#hdfVoucherMaster").val() != null) {
                    var perc = $("#txtDiscPercentage").val();
                    var amt = $("#txtDiscAmount").val();
                    var gift = $("#txtAmount").val();

                    amt = amt == "" || amt == null ? "0" : amt;

                    perc = perc == "" || perc == null ? "0" : perc;
                    gift = gift == "" || gift == null ? "0" : gift;

                    if (parseFloat(amt) > 0) {

                        Amount();
                    }
                    else if (parseFloat(perc) > 0) {

                        Percentage()
                    }
                    else if (parseFloat(gift) > 0) {

                        GetGiftDetals();
                    }


                }
                if ($("#hdfIsReferance").val().split(',')[0] == "1") {
                    SetReadonly();
                }
                else {
                    RemoveAttribute();
                }
            }
            catch (e) {
                alert(e);
            }
        }

        function SetReadonly() {
            $("#txtValidity").attr('readonly', 'readonly');
            $("#txtShortName").attr('readonly', 'readonly');
            $("#txtName").attr('readonly', 'readonly');
            document.getElementById("rbtnDiscount").disabled = true;
            document.getElementById("rbtnGift").disabled = true;
            document.getElementById("rbtnReturn").disabled = true;
            $("#txtDiscPercentage").attr('readonly', 'readonly');
            $("#txtDiscAmount").attr('readonly', 'readonly');
            document.getElementById('ddlAcSubHead').disabled = true;
            $("#txtRemarks").attr("readonly", "readonly");
            $("#txtDiscAmount").attr("readonly", "readonly");
            $("#txtDiscPercentage").attr('readonly', 'readonly');
            $("#txtAmount").attr('readonly', 'readonly');


        }
        function RemoveAttribute() {
            $("#txtValidity").removeAttr('readonly');
            $("#txtShortName").removeAttr('readonly');
            $("#txtName").removeAttr('readonly');
            document.getElementById("rbtnDiscount").disabled = false;
            document.getElementById("rbtnGift").disabled = false;
            document.getElementById("rbtnReturn").disabled = false;
            $("#txtDiscPercentage").removeAttr('readonly');
            $("#txtDiscAmount").removeAttr('readonly');
            document.getElementById('ddlAcSubHead').disabled = false;
            $("#txtRemarks").removeAttr('readonly');
            $("#txtDiscAmount").removeAttr('readonly');
            $("#txtDiscPercentage").removeAttr("readonly");
            $("#txtAmount").removeAttr("readonly");
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
    <form id="frmVoucherMaster" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="icon_bg">
                    <div class="search" style="width:670px; " >Voucher Master</div>
                    <div class="add">Add</div>
                    <div class="add_icon">
                        <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" CausesValidation="false" OnClientClick=" ShowAddDiv(); ClearAdd(); return false; releasebtn();" TabIndex="16" />
                    </div>
                    <div class="search">View</div>
                    <div class="search_icon">
                        <asp:ImageButton ID="imgbtnView" TabIndex="15" ImageUrl="images/view.png" Width="37"  Height="40" runat="server" CausesValidation="false" OnClick="imgbtnView_Click" />
                    </div>


                </div>
               <%-- <div class="user_bg">
                    <div class="user1">Voucher Master</div>
                </div>--%>
                <div class="clear"></div>
                <div class="group_bg2">
                    <div id="divAdd" runat="server" style="display: none;">
                        <ul class="contentul" id="Container">
                            <li>
                                <label class="label">Short Name</label>
                                <asp:TextBox runat="server" ID="txtShortName" CssClass="textboxMandatory" TabIndex="1" MaxLength="10" AutoComplete="off" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Enabled="True" ErrorMessage="Short Name required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <li>
                                <label class="label">Name</label>
                                <asp:TextBox runat="server" ID="txtName" CssClass="textboxMandatory" MaxLength="250" AutoComplete="off" TabIndex="2" />
                                <asp:RequiredFieldValidator ID="reqvalName" runat="server" Enabled="True" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="reqvalName">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <li style="height: 3px;"></li>
                            <li>
                                <label class="label">Type</label>
                                <asp:RadioButton ID="rbtnDiscount" runat="server" Text="Discount" onclick="javascript:CommonDiscGift(event);" GroupName="rbtn" Checked="True" TabIndex="3" />
                                <asp:RadioButton ID="rbtnGift" runat="server" Text="Gift" GroupName="rbtn" onclick="javascript:CommonDiscGift(event);" TabIndex="4" />
                                <asp:RadioButton ID="rbtnReturn" runat="server" Text="Return" GroupName="rbtn" onclick="javascript:CommonDiscGift(event);" TabIndex="5" />
                            </li>
                            <li style="height: 3px;"></li>
                            <li id="liGift" style="display: none;">
                                <label class="label">Amount</label>
                                <asp:TextBox runat="server" ID="txtAmount" CssClass="textbox" Style="text-align: right;" onkeypress="CheckNumWithDecimal(event);" AutoComplete="off" MaxLength="15"  TabIndex="6"/>
                                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" Enabled="false" ErrorMessage="Amount required" ControlToValidate="txtAmount" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="rfvAmount">
                                </cc:ValidatorCalloutExtender>
                                <asp:RangeValidator ID="rgvgift" Enabled="false" runat="server" ControlToValidate="txtAmount" ErrorMessage="Enter a valid Gift Amount" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="rgvgift">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <div id="divDiscount">
                                <li id="liDiscperc">
                                    <label class="label">Discount %</label>
                                    <asp:TextBox runat="server" ID="txtDiscPercentage" TabIndex="7" Style="text-align: right;" CssClass="textboxMandatory" onkeypress="CheckNumWithDecimal(event);" onchange="AmountOrPerc(event);" AutoComplete="off" MaxLength="10" />
                                    <asp:RequiredFieldValidator ID="rfvPercentage" Enabled="false" runat="server" ErrorMessage="Discount percenatge required" ControlToValidate="txtDiscPercentage" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="rfvPercentage">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:RangeValidator ID="rgvDiscPerc" runat="server" Enabled="false" ControlToValidate="txtDiscPercentage" ErrorMessage="Enter a valid Percentage" Display="None" MaximumValue="100" ValidationGroup="submit" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="rgvDiscPerc">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li id="liDiscAmount">
                                    <label class="label">Discount Amount</label>
                                    <asp:TextBox runat="server" ID="txtDiscAmount" TabIndex="8" Style="text-align: right;" CssClass="textboxMandatory" onkeypress="CheckNumWithDecimal(event);" onchange="AmountOrPerc(event);" AutoComplete="off" MaxLength="10" />
                                    <asp:RequiredFieldValidator ID="rfvDiscAmount" Enabled="false" runat="server" ErrorMessage="Discount amount required" ControlToValidate="txtDiscAmount" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="rfvDiscAmount">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:RangeValidator ID="rgvDiscAmt" Enabled="false" runat="server" ControlToValidate="txtDiscAmount" ErrorMessage="Enter a valid Discount Amount" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="rgvDiscAmt">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                            </div>
                            <li>
                                <label class="label">Validity</label>
                                <asp:TextBox runat="server" ID="txtValidity"  TabIndex="9" Style="text-align: right;" CssClass="textboxMandatory" onkeypress="CheckInteger(event)" AutoComplete="off" MaxLength="5" />
                                <label>(Days)</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Enabled="true" runat="server" ErrorMessage="Enter valid period" ControlToValidate="txtValidity" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                </cc:ValidatorCalloutExtender>
                                <asp:RangeValidator ID="RangeValidator1" Enabled="true" runat="server" ControlToValidate="txtValidity" ErrorMessage="Enter valid period" Display="None" MaximumValue="2000" ValidationGroup="submit" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                </cc:ValidatorCalloutExtender>

                            </li>
                            <li>
                                <label class="label">Account Sub Head</label>
                                <asp:DropDownList ID="ddlAcSubHead" runat="server" CssClass="dropdownMandatory" TabIndex="10"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Enabled="True" ErrorMessage="Sub Head required" ControlToValidate="ddlAcSubHead" Display="None" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <li>
                                <label class="label">Remarks</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemarks" CssClass="multiline" TabIndex="11" />
                            </li>
                            <li class="btnMargin">
                                <asp:CheckBox Text="Active" runat="server" CssClass="chckbox" ID="cbxActive" TabIndex="12" />
                            </li>
                            <li class="btnMargin">
                                <asp:HiddenField ID="hfPurchaseReturn" runat="server" />
                                <asp:HiddenField ID="hdfIsReferance" runat="server" />
                                <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" OnClientClick="disablebutn();"  TabIndex="13" />
                                <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="return ClearAdd();" CausesValidation="false" TabIndex="14" />
                                <asp:HiddenField ID="hfClient" runat="server" />
                                <asp:HiddenField ID="hfUser" runat="server" />
                                <asp:HiddenField ID="hfIsAdd" runat="server" />
                                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                <asp:HiddenField ID="hfIsDelete" runat="server" />
                                <asp:HiddenField ID="hdfVoucherMaster" runat="server" />
                            </li>

                        </ul>
                    </div>
                    <div id="divView" runat="server" style="display: none;">



                        <ul class="contentul">
                            <div class="divL">


                                <li>
                                    <label class="label">Short Name</label>
                                    <asp:TextBox runat="server" ID="txtSearchShortName" TabIndex="18" CssClass="textbox" AutoComplete="off" />
                                </li>
                                <li>
                                    <label class="label">Type</label>
                                    <asp:DropDownList runat="server" ID="ddlSearchType" TabIndex="19" CssClass="dropdown">
                                        <asp:ListItem Text="select" Value="0" />
                                        <asp:ListItem Text="Discount" Value="1" />
                                        <asp:ListItem Text="Gift" Value="2" />
                                        <asp:ListItem Text="Return" Value="3" />
                                    </asp:DropDownList>
                                </li>

                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="22" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="23" />
                                </li>
                            </div>
                            <div class="divR">



                                <li>
                                    <label class="label">Name</label>
                                    <asp:TextBox runat="server" ID="txtSearchName" CssClass="textbox" TabIndex="20" AutoComplete="off" />
                                </li>

                                <li>
                                    <label class="label">Account Sub Head</label>
                                    <asp:DropDownList runat="server" CssClass="dropdown" ID="ddlSearchAccountHead" TabIndex="21"></asp:DropDownList>
                                </li>

                            </div>

                        </ul>
                        <br class="clear" />
                        <ul class="contentul">
                            <asp:GridView ID="gvVoucher" runat="server" CssClass="grid"
                                EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_Voucher" OnRowDeleting="gvVoucher_RowDeleting" OnSelectedIndexChanging="gvVoucher_SelectedIndexChanging">
                                <AlternatingRowStyle BackColor="#CCE0EB" />
                                <Columns>
                                    <asp:BoundField DataField="SlNo" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                    <asp:BoundField DataField="VoucherType" HeaderText="Voucher Type" />
                                    <asp:BoundField DataField="validity" HeaderText="Validity(Days)" />
                                    <asp:BoundField DataField="AccountName" HeaderText="Account Sub Head" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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

                                        <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" OnClick="Page_Changed" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ul>
                    </div>
                    <div class="clear"></div>
               <%--     <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />--%>
                   <%-- <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


    </form>

</body>
</html>
