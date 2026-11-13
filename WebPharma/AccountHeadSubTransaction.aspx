<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountHeadSubTransaction.aspx.cs" Inherits="AccountHeadSubTransaction" EnableEventValidation="false" %>

<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc1" TagName="MedLifePageHelp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AccountHeadSubTransaction </title>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <script src="js/Navigation.js"></script>
    <script src="js/General.js"></script>
    <script type="text/javascript">
        var submit = 0;
        //function disablebutn() {
        //    try {

        //        window.setTimeout('delaysubmit()', 50);
        //    }
        //    catch (e) {
        //        alert(e);
        //    }
        //}
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
        function pageLoad() {

            var ctrContainer = "form";
            //alert('h');
            setKeynavigation(ctrContainer);
            //alert('k');
        }
        function PopupOnReason() {
            try {

                //$("#ContentPlaceHolder1_divPopupOnstores").css('display', 'block');
                $("#divPopupReason").css('display', 'block');
            }
            catch (ex) {
                //alert('popup' + ex);  
            }
        }
        function ClearAdd() {
            submit = 0;

            var toDate = new Date();
            $("#txtTransDate").val(toDate.format("dd/MM/yyyy"));
            $("#txtEffectDate").val(toDate.format("dd/MM/yyyy"));
            $("#txtAhstBillDate").val(toDate.format("dd/MM/yyyy"));
            $("#txtAhstChequeDate").val(toDate.format("dd/MM/yyyy"));



            $("#liacountsubhead").attr('style', 'display:none');

            $("#litxtCusAddress").attr('style', 'display:none');


            $("#<%#txtAhCode.ClientID%>").val('');

            $("#<%#txtAhsNumber.ClientID%>").val('');
            $("#<%#txtAhName.ClientID%>").val('');
            $("#<%#txtCusAddress.ClientID%>").val('');

            $("#<%#txtAhSubName.ClientID%>").val('');
            $("#<%#txtAhstParty.ClientID%>").val('');
            $("#<%#txtAhstServicetax.ClientID%>").val('');
            $("#<%#txtTotalAmount.ClientID%>").val('');
            $("#<%#ddlTransType.ClientID%>").val("0");
            $("#<%#txtAhstAmount.ClientID%>").val('0');
            $("#<%#txtAmountInWords.ClientID%>").val('');
            $("#<%#txtAhstBillNo.ClientID%>").val('');
            $("#<%#txtAhstChequeNo.ClientID%>").val('');
            $("#<%#txtAhstNarration.ClientID%>").val('');
            $("#<%#txtClrBalance.ClientID%>").val('');
            $("#<%#txtClsBalance.ClientID%>").val('');
            $("#<%#hdfAcnthedsubtrn.ClientID%>").val('');

            $("#<%#txtSearchAhName.ClientID%>").val('');
            $("#<%#txtSearchAhsName.ClientID%>").val('');
            $("#<%#btnSearchAccountHead.ClientID%>").focus();

        }
        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();


        }

        function ClearSearch() {

            $("#<%#txtSearchAhName.ClientID%>").val('');
            $("#<%#txtSearchAhsName.ClientID%>").val('');

            $("#<%#txtSearchAhName.ClientID%>").focus();
        }



        function InWords() {

            var n = document.getElementById('txtAhstAmount').value.replace(/,/gi, "");

            var result = parseFloat(n).toFixed(2);

            if (result >= 0) {
                //Amount in Words
                document.getElementById('txtAmountInWords').value = amtInWords(result);
            }
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
        function ValidateChequePair(sender, args) {
            var chqNo = $("#<%#txtAhstChequeNo.ClientID%>").val();
            var chqDate = $("#<%#txtAhstChequeDate.ClientID%>").val();
            args.IsValid = !(chqNo && !chqDate);
        }
    </script>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="AccountHeadSubTransactions" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5">
                    <div class="heading2">
                        <div class="user">Cash Transaction</div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                                <uc1:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                                <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" Width="37" Height="38" runat="server" OnClientClick=" EnterKeyFilter(); ClearAdd(); ShowAddDiv();  releasebtn()" OnClick="imgbtnAdd_Click" />
                                <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" OnClientClick="ShowAddDiv(); ClearAdd(); " OnClick="imgbtnView_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                    <%--<div class="search" style="width: 670px;">AccountHeadSubTransaction </div>--%>

                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">
                            <div id="divAdd" runat="server">
                                <ul class="contentul">
                                    <div class="divL" style="width: 60%;">
                                        <li>
                                            <asp:Label ID="lblAhCode" runat="server" CssClass="label" Text="Account Code" Width="15%"></asp:Label>
                                            <asp:TextBox ID="txtAhCode" runat="server" AutoPostBack="false" CssClass="textboxMandatory" onKeypress="CheckInteger(event);" MaxLength="8" Width="5%"></asp:TextBox>
                                            <asp:ImageButton ID="btnSearchAccountHead" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="btnSearchAccountHead_Click" Style="margin-bottom: -8px;" TabIndex="1" />
                                            <asp:TextBox ID="txtAhName" runat="server" CssClass="textboxMandatory" MaxLength="225" Width="59%"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="rfvAhCode" runat="server" ErrorMessage="Account Code required" ControlToValidate="txtAhCode" Display="None" SetFocusOnError="True" ValidationGroup="process"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="vceAhCode" runat="server" Enabled="True" TargetControlID="rfvAhCode">
                                            </cc:ValidatorCalloutExtender>


                                        </li>


                                        <li id="liacountsubhead" runat="server" style="display: none">
                                            <asp:Label ID="lblAhsNumber" runat="server" CssClass="label" Text="Subhead" Width="15%"></asp:Label>
                                            <asp:TextBox ID="txtAhsNumber" runat="server" AutoPostBack="false" CssClass="textbox" onKeypress="CheckInteger(event);" Width="5%" MaxLength="8"></asp:TextBox>
                                            <asp:ImageButton ID="btnSearchSubHead" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="btnSearchAccountHeadSub_Click" Style="margin-bottom: 0px;" TabIndex="2" />
                                            <asp:TextBox ID="txtAhSubName" runat="server" Width="60%" CssClass="textbox" MaxLength="40"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="lblAhstParty" runat="server" CssClass="label" Text="Party" Width="15%"></asp:Label>
                                            <asp:TextBox ID="txtAhstParty" runat="server" CssClass="textbox" TabIndex="3" Width="71%" MaxLength="200"></asp:TextBox>
                                        </li>

                                        <li id="liservicetax" runat="server" style="display: none">
                                            <asp:Label ID="lblAhstServiceTax" runat="server" CssClass="label" Text="Service Tax" Width="15%"></asp:Label>
                                            <asp:TextBox ID="txtAhstServicetax" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="40" onKeypress="CheckInteger(event);" Width="21%" MaxLength="8" Visible="True"></asp:TextBox>
                                            <asp:ImageButton ID="btnTax" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="btnTax_Click" Style="margin-bottom: 0px;" TabIndex="2" />
                                        </li>

                                        <li id="litotalamnt" runat="server" style="display: none">
                                            <asp:Label ID="lblTax0" runat="server" CssClass="label" Text="Total Amount" Width="15%"></asp:Label>

                                            <asp:TextBox runat="server" ID="txtTotalAmount" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="6" MaxLength="5" Width="21%" />

                                        </li>
                                    </div>
                                    <div class="divR" style="width: 40%;">

                                        <li id="litxtCusAddress" runat="server" style="display: none">
                                            <asp:TextBox ID="txtCusAddress" runat="server" AutoComplete="off" CssClass="multiline" TabIndex="50" TextMode="MultiLine" Visible="True" Height="100%" Width="65%"></asp:TextBox>
                                           <%-- <asp:RequiredFieldValidator ID="rfvAhName" runat="server" ControlToValidate="txtAhCode" Display="None" ErrorMessage="Account Code Required" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="rfvAhName">
                                            </cc:ValidatorCalloutExtender>--%>
                                        </li>
                                    </div>

                                    <div class="divL" style="width: 60%;">

                                        <div class="divL" style="width: 50%;">
                                            <li>
                                                <asp:Label ID="lblTransDate" runat="server" CssClass="label" Text="Trans.Date" Width="30%"></asp:Label>
                                                <asp:TextBox ID="txtTransDate" CssClass="textbox" TabIndex="4" runat="server" Width="50%"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtTransDate">
                                                </cc:CalendarExtender>
                                                <asp:CompareValidator ID="CompareValidator130" runat="server" ErrorMessage="Trans.Date should be lesser than Effect Date" Display="None" ControlToCompare="txtEffectDate" ControlToValidate="txtTransDate" ValidationGroup="submit" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="CompareValidator130">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:RequiredFieldValidator ID="rfvTransDate" runat="server" ErrorMessage="Trans.Date required" ControlToValidate="txtTransDate" Display="None" SetFocusOnError="True" ValidationGroup="process"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="vceTransDate" runat="server" Enabled="True" TargetControlID="rfvTransDate">
                                                </cc:ValidatorCalloutExtender>
                                            </li>

                                            <li>
                                                <asp:Label ID="lblTransType" runat="server" CssClass="label" Text="Trans.Type" Width="30%"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlTransType" CssClass="dropdownMandatory" TabIndex="6" Width="54%">
                                                    <asp:ListItem Text="select" Value="0" />
                                                    <asp:ListItem Text="Receipt" Value="R" />
                                                    <asp:ListItem Text="Payment" Value="P" />

                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage=" Trans.Type required" ControlToValidate="ddlTransType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="process"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                                </cc:ValidatorCalloutExtender>

                                            </li>

                                        </div>
                                        <div class="divR" style="width: 50%;">

                                            <li>
                                                <asp:Label ID="lblEffectDate" runat="server" CssClass="label" Text="Effect Date" Width="25%"></asp:Label>
                                                <asp:TextBox ID="txtEffectDate" CssClass="textboxMandatory" TabIndex="5" runat="server" Width="47%"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtEffectDate">
                                                </cc:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvEffectDate" runat="server" ErrorMessage="Effect Date required" ControlToValidate="txtEffectDate" Display="None" SetFocusOnError="True" ValidationGroup="process"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="vceEffectDate" runat="server" Enabled="True" TargetControlID="rfvEffectDate">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                            <li>
                                                <asp:Label ID="lblAhstAmount" runat="server" CssClass="label" Text="Amount" Width="25%"></asp:Label>


                                                <asp:TextBox runat="server" ID="txtAhstAmount" onkeypress="CheckNumWithDecimal(event);" CssClass="textbox" TabIndex="7" MaxLength="10" Width="47%" onkeyup="InWords();" />
                                                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Amount required" ControlToValidate="txtAhstAmount" Display="None" SetFocusOnError="True" ValidationGroup="process"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="vceAmount" runat="server" Enabled="True" TargetControlID="rfvAmount">
                                                </cc:ValidatorCalloutExtender>

                                                <asp:CompareValidator ID="cvAmountGtZero" runat="server" ErrorMessage="Amount must be greater than zero" ControlToValidate="txtAhstAmount" Operator="GreaterThan" ValueToCompare="0" Type="Currency" Display="None" SetFocusOnError="True" ValidationGroup="process"></asp:CompareValidator>
                                                <cc:ValidatorCalloutExtender ID="vceAmountGtZero" runat="server" Enabled="True" TargetControlID="cvAmountGtZero">
                                                </cc:ValidatorCalloutExtender>

                                            </li>



                                        </div>

                                        <div class="divL" style="width: 100%;">

                                            <li>
                                                <asp:Label ID="lblAhstNarration" runat="server" CssClass="label" Text="InWords" Width="15%" Height="16px"></asp:Label>

                                                <asp:TextBox ID="txtAmountInWords" CssClass="textbox" TabIndex="12" runat="server" Width="71%" Height="0%"></asp:TextBox>
                                            </li>
                                        </div>

                                        <div class="divL" style="width: 50%;">
                                            <li id="liBillno" runat="server" style="display: none">
                                                <asp:Label ID="lblBillNo" runat="server" CssClass="label" Text="Bill.No" Width="28%"></asp:Label>
                                                <asp:TextBox runat="server" ID="txtAhstBillNo" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="80" MaxLength="5" Width="50%" />


                                            </li>
                                            <li>
                                                <asp:Label ID="lblChqNoDate0" runat="server" CssClass="label" Text="Cheq.  No" Width="30%"></asp:Label>

                                                <asp:TextBox runat="server" ID="txtAhstChequeNo" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="8" MaxLength="10" Width="50%" />
                                            </li>

                                        </div>
                                        <div class="divR" style="width: 50%;">

                                            <li id="lblBillDates" runat="server" style="display: none">
                                                <asp:Label ID="lblBillDate" runat="server" CssClass="label" Text="Bill. Date " Width="25%"></asp:Label>
                                                <asp:TextBox ID="txtAhstBillDate" CssClass="textbox" TabIndex="9" runat="server" Width="48%"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtAhstBillDate">
                                                </cc:CalendarExtender>
                                            </li>
                                            <li>
                                                <asp:Label ID="lblChqNoDate" runat="server" CssClass="label" Text="Cheq. Date" Width="25%"></asp:Label>
                                                <asp:TextBox ID="txtAhstChequeDate" CssClass="textbox" TabIndex="9" runat="server" Width="47%"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtAhstChequeDate">
                                                </cc:CalendarExtender>
                                                <asp:CustomValidator ID="cvChequePair" runat="server" ErrorMessage="Cheque Date required when Cheque No is entered" Display="None" SetFocusOnError="True" ValidationGroup="process" ClientValidationFunction="ValidateChequePair"></asp:CustomValidator>
                                                <cc:ValidatorCalloutExtender ID="vceChequePair" runat="server" Enabled="True" TargetControlID="cvChequePair">
                                                </cc:ValidatorCalloutExtender>
                                        </div>

                                        <div class="divL" style="width: 100%;">

                                            <li>
                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Narration" Width="15%" Height="16px"></asp:Label>

                                                <asp:TextBox ID="txtAhstNarration" CssClass="textbox" TabIndex="10" runat="server" Width="71%" Height="0%" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                                <asp:Button ID="Btnprocess" runat="server" CssClass="button" Text="Process" Width="10%" TabIndex="11" ValidationGroup="process" OnClick="btnProcess_Click" OnClientClick="disablebutn();" />

                                            </li>

                                        </div>


                                    </div>

                                    <div class="divR" style="width: 40%; height: 424px;">
                                        <div class="divL" style="width: 55%; height: 424px;">

                                            <li id="lilclarbalnce" runat="server" style="width: 110%">
                                                <asp:Label ID="lblClearedBalance" runat="server" CssClass="label" Text="Clr Bal" Width="22%" Visible="true"></asp:Label>

                                                <asp:TextBox runat="server" ID="txtClrBalance" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="50" MaxLength="10" Width="60%" Visible="true" />

                                            </li>
                                            <%-- <li>

                                            <asp:Label ID="lblClearedBalance0" runat="server" CssClass="label" Text="UnClr Bal" Width="22%"></asp:Label>

                                            <asp:TextBox runat="server" ID="txtUnClrBalance" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="50" MaxLength="10" Width="60%" />
                                            <%-- <asp:Label ID="lblunclrbal" runat="server" CssClass="label"  Text="DR"    Width="40%"></asp:Label>--%>



                                            <%-- <li>

                                            <asp:Label ID="lblbranchbal" runat="server" CssClass="label" Text="BrCls Bal" Width="18%"></asp:Label>
                                        
                    <asp:TextBox runat="server" ID="txtBrBalance" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="50" MaxLength="5"  Width="38%" />
                        <asp:Label ID="lblbrbal" runat="server" CssClass="label"  Visible="false"  Width="40%"></asp:Label>

                          </li>--%>
                                            <li id="Liclosingbalance" runat="server" style="width: 110%">
                                                <%--    <li style="width:110%">--%>

                                                <asp:Label ID="lblClosingBalance" runat="server" CssClass="label" Text="Cls Bal" Width="22%" Visible="true"></asp:Label>

                                                <asp:TextBox runat="server" ID="txtClsBalance" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="50" MaxLength="10" Width="60%" Visible="true" />


                                            </li>
                                            <li class="clear"></li>
                                            <li class="clear"></li>
                                            <li class="clear"></li>



                                            <%--<li style="height: 322px">
                                            <asp:GridView ID="gvTransaction" runat="server" CssClass="grid"
                                                EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="Teal" BorderStyle="Solid" AllowPaging="true" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="8" DataKeyNames="ID_AccountHeadSubTransaction" Height="199px">
                                                <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                                <Columns>
                                                    <asp:BoundField DataField="TransDate" HeaderText="TransDate" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                    <asp:BoundField DataField="TransType" HeaderText="TransType" />
                                                    <asp:BoundField DataField="Balance" HeaderText="Balance" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField DataField="ID_AccountHeadSubTransaction" HeaderText="ID_AccountHeadSubTransaction" Visible="false"  />--%>
                                            <%--   </Columns>
                                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="8px" />
                                                <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                            </asp:GridView>


                                        </li>--%>
                                        </div>
                                        <div class="divL" style="width: 20%; height: 424px;">
                                            <li>

                                                <asp:Label ID="lblclrbal0" runat="server" CssClass="label" Visible="False" Width="40%" ForeColor="Red"></asp:Label>
                                            </li>


                                            <%-- <li>
                                            <asp:Label ID="lblunclrbal" runat="server" CssClass="label" Visible="False" Width="40%" ForeColor="Red"></asp:Label>
                                        </li>--%>
                                            <br />

                                            <li>
                                                <%--           <asp:Label ID="lblclbal" runat="server" CssClass="label" Text="DR"   Width="40%"></asp:Label>--%>
                                                <asp:Label ID="lblclbal" runat="server" CssClass="label" Visible="False" Width="40%" ForeColor="Red"></asp:Label>
                                            </li>
                                        </div>
                                    </div>
                                    <div class="divR" style="width: 51%;">
                                        <%-- <asp:Button ID="Btnprocess" runat="server" CssClass="button" Text="Process" TabIndex="80" ValidationGroup="submit" OnClick="btnProcess_Click" OnClientClick="disablebutn();" />--%>

                                        <%--      <li class="btnMargin">--%>
                                        <li>
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="12" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                            <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="13" OnClick="btnClear_Click" OnClientClick="ClearAdd(); ShowAddDiv();" />
                                            <asp:Button ID="btnprint" runat="server" CssClass="button" Text="Print" TabIndex="19" OnClick="btnPrint_Click" Visible="false" OnClientClick="ClearAdd(); ShowAddDiv();" />
                                            <%--<asp:Button ID="Btntrns" runat="server" CssClass="button" Text="Show" TabIndex="19" OnClick="Btntrns_Click" />--%>
                                            <asp:HiddenField ID="hdfAcnthedsubtrn" runat="server" />
                                            <asp:HiddenField ID="hfClient" runat="server" />
                                            <asp:HiddenField ID="hfUser" runat="server" />
                                            <asp:HiddenField ID="hfIsAdd" runat="server" />
                                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                            <asp:HiddenField ID="hfIsDelete" runat="server" />
                                            <asp:HiddenField ID="HdValuestore" runat="server" />
                                            <asp:HiddenField ID="hfA" runat="server" />
                                            <asp:HiddenField ID="hfS" runat="server" />
                                            <asp:HiddenField ID="hfMasterID" runat="server" />

                                        </li>
                                    </div>


                                </ul>
                            </div>
                        </div>
                        <div class="main_bg">
                            <div id="divView" runat="server" style="display: none;">
                                <ul class="contentul">
                                    <li>
                                        <li>
                                            <asp:Label ID="Label5" runat="server" CssClass="label" Text="Account Head"></asp:Label>
                                            <asp:TextBox ID="txtSearchAhName" CssClass="textbox" TabIndex="15" runat="server"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label6" runat="server" CssClass="label" Text="Account Sub Head "></asp:Label>
                                            <asp:TextBox ID="txtSearchAhsName" CssClass="textbox" TabIndex="16" runat="server"></asp:TextBox>
                                        </li>
                                        <%--  <li class="clear"></li>
                                    <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Account Head Code"></asp:Label>
                                        <asp:TextBox ID="txtSearchAhCode" CssClass="textbox" TabIndex="17" runat="server"></asp:TextBox>
                                    </li>--%>




                                        <li class="btnMargin">

                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="18" Text="Search" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnSearchClear" runat="server" CssClass="button" TabIndex="19" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                        </li>
                                    </li>
                                </ul>
                                <ul class="contentul">
                                    <asp:GridView ID="gvAccountHeadSubTrn" runat="server" CssClass="grid"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="Teal" BorderStyle="Solid" AllowPaging="true" OnPageIndexChanging="gvAccountHeadSubTrn_PageIndexChanging" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="ID_AccountHeadSubTransaction" OnRowDeleting="gvAccountHeadSubTrn_RowDeleting" OnSelectedIndexChanging="gvAccountHeadSubTrn_SelectedIndexChanging">
                                        <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                        <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="AhCode" HeaderText="Account Head" />
                                            <asp:BoundField DataField="AhsCode" HeaderText="Account Sub Head" />
                                            <asp:BoundField DataField="TransDate" HeaderText="TransDate" />
                                            <asp:BoundField DataField="EffectDate" HeaderText="EffectDate" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                            <asp:BoundField DataField="TransType" HeaderText="TransType" />
                                            <%-- <asp:BoundField DataField="ID_AccountHeadSubTransaction" HeaderText="ID_AccountHeadSubTransaction" Visible="false"  />--%>



                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ToolTip="Delete" OnClick="ImageButton2_Click" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
                                        <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
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
                                </ul>


                            </div>
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

                        <asp:Button ID="btnReasonShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divReasonPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divReasonheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="Label2" runat="server" Text="Reason"></asp:Label>
                                <asp:ImageButton ID="btnReasonClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                                <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                            </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divReasonPopup" TargetControlID="btnReasonShow"
                            CancelControlID="btnReasonClose" Drag="true" PopupDragHandleControlID="divReasonheader">
                        </cc:ModalPopupExtender>


                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/toastr.js"></script>
    </form>
</body>



</html>
