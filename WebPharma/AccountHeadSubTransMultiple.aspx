<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountHeadSubTransMultiple.aspx.cs" Inherits="AccountHeadSubTransMultiple" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc1" TagName="MedLifePageHelp" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>


<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AccountHeadSubTransaction Multiple </title>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <script src="js/Navigation.js"></script>
    <script src="js/General.js"></script>
    <script type="text/javascript">

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

            $("#Divinsert").attr('style', 'display:block');


            $("#liacountsubhead").attr('style', 'display:none');

            $("#litxtCusAddress").attr('style', 'display:none');


            $("#<%#txtAhCode.ClientID%>").val('');
            $("#<%#txtAhName.ClientID%>").val('');
            $("#<%#txtCusAddress.ClientID%>").val('');
            $("#<%#txtAhsNumber.ClientID%>").val('');
            $("#<%#txtAhSubName.ClientID%>").val('');
            $("#<%#txtAhstParty.ClientID%>").val('');
            $("#<%#ddlTransType.ClientID%>").val("0");
            $("#<%#txtAhstAmount.ClientID%>").val('0');
            $("#<%#txtAmountInWords.ClientID%>").val('');
            $("#<%#txtAhstBillNo.ClientID%>").val('');
            $("#<%#txtAhstChequeNo.ClientID%>").val('');
            $("#<%#txtAhstNarration.ClientID%>").val('');
            $("#<%#txtClrBalance.ClientID%>").val('');
            $("#<%#txtClsBalance.ClientID%>").val('');
            $("#<%#hdfAcnthedsubtrn.ClientID%>").val('');
            $("#<%#txtDebitAmount.ClientID%>").val('0.00');
            $("#<%#txtCreditAmount.ClientID%>").val('0.00');
            $("#<%#txtTotalAmount.ClientID%>").val('0.00');
            $("#<%#txtSearchAhName.ClientID%>").val('');
            $("#<%#txtSearchAhsName.ClientID%>").val('');
            $("#liacountsubhead").attr('style', 'display:none');
            $("#<%#btnSearchAccountHead.ClientID%>").focus();
        }
        function InWords() {

            var n = document.getElementById('txtAhstAmount').value.replace(/,/gi, "");

            var result = parseFloat(n).toFixed(2);

            if (result >= 0) {
                //Amount in Words
                document.getElementById('txtAmountInWords').value = amtInWords(result);
            }

        }

        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();


        }
        function disablebutn() {
            try {

                window.setTimeout('delaysubmit()', 50);
            }
            catch (e) {
                alert(e);
            }
        }
        function ClearSearch() {

            $("#<%#txtSearchAhName.ClientID%>").val('');
            $("#<%#txtSearchAhsName.ClientID%>").val('');
            $("#<%#txtSearchAhName.ClientID%>").focus();
        }

        //Excel Import


        function uploadStartedabdc(sender, args) {

            var filename = args.get_fileName();
            var filext = filename.substring(filename.lastIndexOf(".") + 1).toLowerCase();

            if (filext == "xlsx" || filext == "xls") {
                Hideinsert();
                return true;

            }
            else {
                alert('Please upload only Excel files ')
                $('#FileUploadExcel').val('');
            }

        }
        function ShowDivOnButtonClick() {
            var x = document.getElementById("divEcelFormat");
            x.style.display = 'block';
        }
        function HideDivOnButtonClick() {
            var x = document.getElementById("divEcelFormat");
            x.style.display = 'none';
        }
        function Hideinsert() {

            var x = document.getElementById("Btninsert");
            x.style.display = 'none';
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
        function ValidateBillPair(sender, args) {
            var billNo = $("#<%#txtAhstBillNo.ClientID%>").val();
            var billDate = $("#<%#txtAhstBillDate.ClientID%>").val();
            args.IsValid = !(billNo && !billDate);
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
                        <div class="user">Journal Entry</div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                                <uc1:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                                <asp:ImageButton ID="imgNew" ImageUrl="images/ADD.png" Width="37" Height="38" runat="server" OnClick="imgNew_Click" OnClientClick="ClearAdd(); ShowAddDiv();   releasebtn()" />
                                <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" OnClientClick="ShowAddDiv(); ClearAdd(); " OnClick="imgbtnView_Click" />
                            </div>
                        </asp:Panel>
                    </div>


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
                                            <asp:TextBox ID="txtAhName" runat="server" CssClass="textboxMandatory" MaxLength="225" Width="57.5%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAccountHead" runat="server" ErrorMessage="Account Head  required" ControlToValidate="txtAhName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="rfvAccountHead">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RequiredFieldValidator ID="rfvAhCode" runat="server" ErrorMessage="Account Code required" ControlToValidate="txtAhCode" Display="None" SetFocusOnError="True" ValidationGroup="InsertRow"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="vceAhCode" runat="server" Enabled="True" TargetControlID="rfvAhCode">
                                            </cc:ValidatorCalloutExtender>

                                        </li>


                                        <li id="liacountsubhead" runat="server" style="display: none">
                                            <asp:Label ID="lblAhsNumber" runat="server" CssClass="label" Text="Subhead" Width="15%"></asp:Label>
                                            <asp:TextBox ID="txtAhsNumber" runat="server" AutoPostBack="false" CssClass="textbox" onKeypress="CheckInteger(event);" Width="5%" MaxLength="8"></asp:TextBox>
                                            <asp:ImageButton ID="btnSearchSubHead" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="btnSearchAccountHeadSub_Click" Style="margin-bottom: 0px;" TabIndex="2" />
                                            <asp:TextBox ID="txtAhSubName" runat="server" Width="58.5%" CssClass="textbox" MaxLength="225"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="lblAhstParty" runat="server" CssClass="label" Text="Party" Width="15%"></asp:Label>
                                            <asp:TextBox ID="txtAhstParty" runat="server" CssClass="textbox" TabIndex="3" Width="71.5%" MaxLength="200"></asp:TextBox>
                                        </li>


                                    </div>
                                    <div class="divR" style="width: 40%;">

                                        <li id="litxtCusAddress" runat="server" style="display: none">
                                            <asp:TextBox ID="txtCusAddress" runat="server" AutoComplete="off" CssClass="multiline" TabIndex="50" TextMode="MultiLine" Visible="True" Height="100%" Width="65%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAhName" runat="server" ControlToValidate="txtAhCode" Display="None" ErrorMessage="Account Code Required" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="rfvAhName">
                                            </cc:ValidatorCalloutExtender>
                                        </li>
                                    </div>

                                    <div class="divL" style="width: 60%;">

                                        <div class="divL" style="width: 50%;">
                                            <li>
                                                <asp:Label ID="lblTransDate" runat="server" CssClass="label" Text="Trans.Date" Width="30%"></asp:Label>
                                                <asp:TextBox ID="txtTransDate" CssClass="textbox" TabIndex="5" runat="server" Width="50%"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtTransDate">
                                                </cc:CalendarExtender>
                                                <asp:CompareValidator ID="CompareValidator130" runat="server" ErrorMessage="Trans.Date should be lesser than Effect Date" Display="None" ControlToCompare="txtEffectDate" ControlToValidate="txtTransDate" ValidationGroup="submit" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="CompareValidator130">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:RequiredFieldValidator ID="rfvTransDate" runat="server" ErrorMessage="Trans.Date required" ControlToValidate="txtTransDate" Display="None" SetFocusOnError="True" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="vceTransDate" runat="server" Enabled="True" TargetControlID="rfvTransDate">
                                                </cc:ValidatorCalloutExtender>
                                            </li>

                                            <li>
                                                <asp:Label ID="lblTransType" runat="server" CssClass="label" Text="Trans.Type" Width="30%"></asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlTransType" CssClass="dropdownMandatory" TabIndex="7" Width="54%" OnSelectedIndexChanged="ddlTransType_SelectedIndexChanged">
                                                    <asp:ListItem Text="select" Value="0" />
                                                    <asp:ListItem Text="Receipt" Value="R" />
                                                    <asp:ListItem Text="Payment" Value="P" />

                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage=" Trans.Type required" ControlToValidate="ddlTransType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                                </cc:ValidatorCalloutExtender>

                                            </li>

                                        </div>
                                        <div class="divR" style="width: 50%;">

                                            <li>
                                                <asp:Label ID="lblEffectDate" runat="server" CssClass="label" Text="Effect Date" Width="25%"></asp:Label>
                                                <asp:TextBox ID="txtEffectDate" CssClass="textboxMandatory" TabIndex="6" runat="server" Width="48%"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtEffectDate">
                                                </cc:CalendarExtender>

                                                <asp:RequiredFieldValidator ID="rfvEffectDate" runat="server" ErrorMessage="Effect Date required" ControlToValidate="txtEffectDate" Display="None" SetFocusOnError="True" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="vceEffectDate" runat="server" Enabled="True" TargetControlID="rfvEffectDate">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                            <li>
                                                <asp:Label ID="lblAhstAmount" runat="server" CssClass="label" Text="Amount" Width="25%"></asp:Label>


                                                <asp:TextBox runat="server" ID="txtAhstAmount" onkeypress="CheckNumWithDecimal(event);" CssClass="textbox" TabIndex="8" MaxLength="10" Width="48%" onkeyup="InWords();" />

                                                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Amount required" ControlToValidate="txtAhstAmount" Display="None" SetFocusOnError="True" ValidationGroup="InsertRow"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="vceAmount" runat="server" Enabled="True" TargetControlID="rfvAmount">
                                                    </cc:ValidatorCalloutExtender>

                                                    <asp:CompareValidator ID="cvAmountGtZero" runat="server" ErrorMessage="Amount must be greater than zero" ControlToValidate="txtAhstAmount" Operator="GreaterThan" ValueToCompare="0" Type="Currency" Display="None" SetFocusOnError="True" ValidationGroup="InsertRow"></asp:CompareValidator>
                                                    <cc:ValidatorCalloutExtender ID="vceAmountGtZero" runat="server" Enabled="True" TargetControlID="cvAmountGtZero">
                                                    </cc:ValidatorCalloutExtender>
                                            </li>


                                        </div>

                                        <div class="divL" style="width: 100%;">

                                            <li>
                                                <asp:Label ID="lblAhstNarration" runat="server" CssClass="label" Text="InWords" Width="15%" Height="16px"></asp:Label>

                                                <asp:TextBox ID="txtAmountInWords" CssClass="textbox" TabIndex="21" runat="server" Width="71.5%" Height="0%"></asp:TextBox>
                                            </li>
                                        </div>



                                        <div class="divL" style="width: 50%;">
                                            <li>
                                                <asp:Label ID="lblBillNo" runat="server" CssClass="label" Text="Bill.No" Width="30%"></asp:Label>
                                                <asp:TextBox runat="server" ID="txtAhstBillNo" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="9" MaxLength="10" Width="50%" />


                                            </li>
                                            <li>
                                                <asp:Label ID="lblChqNoDate0" runat="server" CssClass="label" Text="Cheq.  No" Width="30%"></asp:Label>

                                                <asp:TextBox runat="server" ID="txtAhstChequeNo" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="11" MaxLength="10" Width="50%" />
                                            </li>
                                            <li id="li1" runat="server" style="display: none">
                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Bill.No" Width="28%"></asp:Label>
                                                <asp:TextBox runat="server" ID="TextBox1" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="50" MaxLength="5" Width="50%" />


                                            </li>

                                        </div>
                                        <div class="divR" style="width: 50%;">

                                            <li>
                                                <asp:Label ID="lblBillDate" runat="server" CssClass="label" Text="Bill. Date " Width="25%"></asp:Label>
                                                <asp:TextBox ID="txtAhstBillDate" CssClass="textbox" TabIndex="10" runat="server" Width="48%"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtAhstBillDate">
                                                </cc:CalendarExtender>
                                                <asp:CustomValidator ID="cvBillPair" runat="server" ErrorMessage="Bill Date required when Bill No is entered" Display="None" SetFocusOnError="True" ValidationGroup="InsertRow" ClientValidationFunction="ValidateBillPair"></asp:CustomValidator>
                                                <cc:ValidatorCalloutExtender ID="vceBillPair" runat="server" Enabled="True" TargetControlID="cvBillPair"></cc:ValidatorCalloutExtender>
                                            </li>
                                            <li>
                                                <asp:Label ID="lblChqNoDate" runat="server" CssClass="label" Text="Cheq. Date" Width="25%"></asp:Label>
                                                <asp:TextBox ID="txtAhstChequeDate" CssClass="textbox" TabIndex="12" runat="server" Width="48%"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtAhstChequeDate">
                                                </cc:CalendarExtender>
                                                
                                                    <asp:CustomValidator ID="cvChequePair" runat="server" ErrorMessage="Cheque Date required when Cheque No is entered" Display="None" SetFocusOnError="True" ValidationGroup="InsertRow" ClientValidationFunction="ValidateChequePair"></asp:CustomValidator>
                                                    <cc:ValidatorCalloutExtender ID="vceChequePair" runat="server" Enabled="True" TargetControlID="cvChequePair"></cc:ValidatorCalloutExtender>
                                            </li>
                                        </div>






                                        <div class="divL" style="width: 100%;">

                                            <li>
                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Narration" Width="15%" Height="16px"></asp:Label>

                                                <asp:TextBox ID="txtAhstNarration" CssClass="textbox" TabIndex="13" runat="server" Width="71.5%" Height="0%" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                                <%--<asp:Button ID="Btninsert" runat="server" CssClass="button" Text="Insert"  Width="10%" TabIndex="11" ValidationGroup="submit" OnClick="btnInsert_Click" OnClientClick="disablebutn();" />--%>
                                            
                                            </li>
                                        </div>
                                        <div class="divR" style="width: 86%;">

                                            <li>
                                                <div style="width: 50%; float: left">
                                                    <cc:AsyncFileUpload ID="FileUploadExcel" Style="float: left; width: 40%; margin-left: 3%;" TabIndex="40" runat="server" CssClass="imageUploaderField" OnClientUploadStarted="uploadStartedabdc" OnUploadedComplete="FileUploadExcel_UploadedComplete" />

                                                    <asp:Button ID="btnExcelFormat" runat="server" Text="?" TabIndex="40" ToolTip="Excel Format" Style="width: 35px;" OnClientClick="ShowDivOnButtonClick();return false;" />
                                                </div>


                                                <div id="Div2" style="float: left; width: 20%">
                                                    <asp:Button ID="btnShowData" runat="server" OnClick="btnShowData_Click" CssClass="button"
                                                        TabIndex="41" Text="Show Data" UseSubmitBehavior="False" Width="85px" />
                                                </div>
                                                <div id="Divinsert" style="float: left; width: 20%">
                                                    <asp:Button ID="Btninsert" runat="server" CssClass="button" Text="Insert" TabIndex="42" ValidationGroup="submit" OnClick="btnInsert_Click" OnClientClick="disablebutn();" />

                                                </div>

                                            </li>
                                            <div class="divSeparation" style="width: 190%; margin: 6%; margin-left: -20%; margin-bottom: 1%"></div>
                                        </div>
                                    </div>

                                    <div class="divR" style="width: 40%; height: 124px;">



                                        <div class="divL" style="width: 55%; height: 124px;">

                                            <li id="lilclarbalnce" runat="server" style="width: 110%">
                                                <asp:Label ID="lblClearedBalance" runat="server" CssClass="label" Text="Clr Bal" Width="22%" Visible="false"></asp:Label>

                                                <asp:TextBox runat="server" ID="txtClrBalance" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="50" MaxLength="10" Width="60%" Visible="false" />

                                            </li>

                                            <li id="Liclosingbalance" runat="server" style="width: 110%">
                                                <%--    <li style="width:110%">--%>

                                                <asp:Label ID="lblClosingBalance" runat="server" CssClass="label" Text="Cls Bal" Width="22%" Visible="false"></asp:Label>

                                                <asp:TextBox runat="server" ID="txtClsBalance" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="50" MaxLength="10" Width="60%" Visible="false" />


                                            </li>
                                        </div>

                                        <div class="divL" style="width: 20%; height: 124px;">
                                            <li>

                                                <asp:Label ID="lblclrbal0" runat="server" CssClass="label" Visible="False" Width="40%" ForeColor="Red"></asp:Label>
                                            </li>
                                            <br />

                                            <%-- <li>
                                            <asp:Label ID="lblunclrbal" runat="server" CssClass="label" Visible="False" Width="40%" ForeColor="Red"></asp:Label>
                                        </li>--%>

                                            <li>
                                                <%--           <asp:Label ID="lblclbal" runat="server" CssClass="label" Text="DR"   Width="40%"></asp:Label>--%>
                                                <asp:Label ID="lblclbal" runat="server" CssClass="label" Visible="False" Width="40%" ForeColor="Red"></asp:Label>
                                            </li>

                                        </div>


                                    </div>


                                    <div class="divL" style="width: 92%; height: 60%;">

                                        <li id="LIGRID" runat="server" style="height: 60%; width: 95%; border: thin solid #008080; display: none">






                                            <asp:GridView ID="gdvTrans" runat="server" CssClass="grid" EmptyDataText="No Data Found!"
                                                BackColor="White" BorderColor="Teal" BorderStyle="Ridge" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10"
                                                OnRowDataBound="gdvTrans_RowDataBound"
                                                OnRowDeleting="gdvTrans_RowDeleting" Width="95%" SkinID="gridview" OnRowCreated="gdvTrans_RowCreated"
                                                OnRowCommand="gdvTrans_RowCommand1">
                                                <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                                <Columns>
                                                    <asp:ButtonField CommandName="rowdelete" Text="X" ButtonType="Button" Visible="true">
                                                        <ControlStyle ForeColor="Red" />
                                                    </asp:ButtonField>
                                                </Columns>
                                                <HeaderStyle CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
                                                <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />

                                            </asp:GridView>

                                        </li>
                                    </div>


                                    <div class="divL" style="width: 32%; height: 60%;">
                                        <li>
                                            <asp:Label ID="lblDebit" runat="server" CssClass="label" Text="Debit Amount" Width="28%"></asp:Label>
                                            <asp:TextBox runat="server" ID="txtDebitAmount" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="52" MaxLength="10" Width="30%" ReadOnly="True" />



                                        </li>

                                    </div>
                                    <div class="divR" style="width: 68%; height: 60%;">
                                        <div class="divL" style="width: 50%; height: 60%;">
                                            <asp:Label ID="lblCredit" runat="server" CssClass="label" Text="Credit Amount" Width="28%"></asp:Label>

                                            <asp:TextBox runat="server" ID="txtCreditAmount" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="53" MaxLength="10" Width="30%" ReadOnly="True" />


                                        </div>
                                        <div class="divR" style="width: 50%; height: 60%;">

                                            <asp:Label ID="lblTotalAmount" runat="server" CssClass="label" Text="Difference Amount" Width="35%"></asp:Label>

                                            <asp:TextBox runat="server" ID="txtTotalAmount" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="43" MaxLength="10" Width="30%" ReadOnly="True" />

                                        </div>
                                    </div>
                                    <div class="divR" style="width: 91%;">
                                        <%-- <asp:Button ID="Btnprocess" runat="server" CssClass="button" Text="Process" TabIndex="8" ValidationGroup="submit" OnClick="btnProcess_Click" OnClientClick="disablebutn();" />--%>

                                        <%--      <li class="btnMargin">--%>
                                        <li>
                                            <asp:CheckBox ID="chkTransfer" runat="server" CssClass="chckbox" Text="Transfer" TabIndex="16" />
                                        </li>
                                        <li>


                                            <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="17" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                            <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="18" OnClick="btnClear_Click" OnClientClick="ClearAdd(); ShowAddDiv();" />
                                            <asp:Button ID="btnprint" runat="server" CssClass="button" Text="Print" TabIndex="19" OnClick="btnPrint_Click" Visible="false" OnClientClick="ClearAdd(); ShowAddDiv();" />
                                            <asp:HiddenField ID="hdfAcnthedsubtrn" runat="server" />
                                            <asp:HiddenField ID="hfClient" runat="server" />
                                            <asp:HiddenField ID="hfUser" runat="server" />
                                            <asp:HiddenField ID="hfIsAdd" runat="server" />
                                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                            <asp:HiddenField ID="hfIsDelete" runat="server" />
                                            <asp:HiddenField ID="HdValuestore" runat="server" />
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
                                            <asp:Label ID="Label5" runat="server" CssClass="label" Text="Account Head "></asp:Label>
                                            <asp:TextBox ID="txtSearchAhName" CssClass="textbox" TabIndex="15" runat="server"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label6" runat="server" CssClass="label" Text="Account Sub Head "></asp:Label>
                                            <asp:TextBox ID="txtSearchAhsName" CssClass="textbox" TabIndex="16" runat="server"></asp:TextBox>
                                        </li>
                                        <br />



                                        <li class="btnMargin">

                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="18" Text="Search" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnSearchClear" runat="server" CssClass="button" TabIndex="19" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                        </li>
                                    </li>
                                </ul>

                                <ul class="contentul">
                                    <asp:GridView ID="gvAccountHeadSubTrn" runat="server" CssClass="grid"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="Teal" BorderStyle="Solid" AllowPaging="true" OnPageIndexChanging="gvAccountHeadSubTrn_PageIndexChanging" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="GroupId" OnSelectedIndexChanging="gvAccountHeadSubTrn_SelectedIndexChanging">
                                        <%--   OnRowDeleting="gvAccountHeadSubTrn_RowDeleting" OnSelectedIndexChanging="gvAccountHeadSubTrn_SelectedIndexChanging">--%>
                                        <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                        <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                            <%-- <asp:BoundField DataField="AhName" HeaderText="Account Head" />--%>
                                            <%-- <asp:BoundField DataField="AhsName" HeaderText="Account Sub Head" />--%>
                                            <asp:BoundField DataField="Voucherno" HeaderText="Voucherno" />
                                            <asp:BoundField DataField="GroupId" HeaderText="GroupID" />
                                            <asp:BoundField DataField="TransDate" HeaderText="TransDate" />
                                            <%--<asp:BoundField DataField="EffectDate" HeaderText="EffectDate" />--%>
                                            <asp:BoundField DataField="CreditAmount" HeaderText="CreditAmount" />
                                            <asp:BoundField DataField="DebitAmount" HeaderText="DebitAmount" />
                                            <%-- <asp:BoundField DataField="TransType" HeaderText="TransType" />--%>
                                            <%--<asp:BoundField DataField="ID_AccountHeadSubTransaction" HeaderText="ID_AccountHeadSubTransaction" Visible="false"  />--%>



                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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

                        <div id="divEcelFormat" runat="server" style="display: none; background-color: #FFD6CA; position: relative; width: 50%; height: 100px; margin-left: 279px; margin-top: -796px; box-shadow: 0px 0px 15px #888;">
                            <div style="left: 0px; margin-left: 7px; height: 30px; background-color: skyblue;">
                                <asp:Label ID="Label3" CssClass="label" runat="server" Text="Pharma Says :" Style="width: 66%; margin-right: 0px; font-family: 'Segoe Print'; font-weight: bold; color: black; background-color: skyblue; height: 22px; padding-top: 3px; text-align: left; margin-top: -116px"></asp:Label>
                                <asp:ImageButton ID="ibtnExcelClose" runat="server" Width="4%" Height="64%" TabIndex="18"
                                    Style="float: right; margin-top: -118px; margin-right: 33%;" ToolTip="Close" ImageUrl="~/images/cross.png"
                                    OnClientClick="HideDivOnButtonClick();return false;" />
                            </div>
                            <div style="left: 0px; margin-left: 7px;">
                                <asp:Label ID="Label25" CssClass="label" runat="server" Text="Excel Document Should Contain 
                         Code,Name,SubNo,SubName,Amount,Type,Narration Fields"
                                    Style="width: 66%; margin-right: 0px; color: black; font-family: 'Segoe Print'; background-color: skyblue; font-size: 11px; height: 55px; padding-top: 3px; text-align: left; margin-top: -91px"></asp:Label>
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
                                <asp:Label ID="Label4" runat="server" Text="Reason"></asp:Label>
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
