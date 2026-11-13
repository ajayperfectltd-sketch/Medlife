<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CounterClose.aspx.cs" Inherits="CounterClose" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<%@ Register src="Popup/CommonPopup.ascx" tagname="CommonPopup" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/Custom.js"></script>
     <script src="js/toastr.js"></script>
    <script src="js/alertify.min.js"></script>
       <script src="js/Navigation.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
     <style>
            .Border {
            /*margin: 0.5em;*/
            border-style: groove;
            box-shadow: 0px 0px 2px 2px #888888;
            border: solid 2px #FFF;
            border-radius: 7px;
            padding-right: 5px;
        }
          </style>
    <script type="text/javascript">
        var submit = 0;
        function pageLoad() {
            var ctrContainer = "form";
            setKeynavigation(ctrContainer);
            if ($("#gvDenomination tr td").length > 1) {

                CalculateTotalAmount();
            }
            else {
                CalculateTotalAmount();
            }
        }
        function disablebutn() {
            try {

                window.setTimeout('delaysubmit()', 50);
            }
            catch (e) {
                alert(e);
            }
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
        function releasebtn() {


            $("#btnSubmit").prop('disabled', false);

        }
        function CheckKeys() {
            alert(e.keyCode);
        }
        function CalculateAmount(id) {
            try {
                var txtPieces = id;
                var lblTotal = id.replace('txtPieces', 'lblTotal');
                var hdfPieces = id.replace('txtPieces', 'hdfPieces');
                var txtPieces = id.replace('txtPieces', 'txtPieces');
                var Pieces = $("#" + txtPieces).val() == '' || $("#" + txtPieces).val() == null ? 0 : parseFloat($("#" + txtPieces).val());

                if (parseFloat(Pieces) == 0) {
                    CalculateTotalAmount();
                }
                else {
                    CalculateTotalAmount();
                }
            }
            catch (e) {
                alert(e);
            }
        }

        function CalculateTotalAmount() {

            var Opening = $("#LblCloseCounterOpening").val() == '' ? '0' : $("#LblCloseCounterOpening").val();
            var Sales = $("#LblCloseCounterSales").val() == '' ? '0' : $("#LblCloseCounterSales").val();
            var Salesreturn = $("#lblCloseCounterSalesReturn").val() == '' ? '0' : $("#lblCloseCounterSalesReturn").val();
            var CardSales = $("#LblCloseCounterSales").val() == '' ? '0' : $("#LblCloseCounterSales").val();
            var ChqSales = $("#LblCloseCounterSales").val() == '' ? '0' : $("#LblCloseCounterSales").val();
            var VouSales = $("#LblCloseCounterSales").val() == '' ? '0' : $("#LblCloseCounterSales").val();
            var CashSales = $("#lblCloseCounterCash").val() == '' ? '0' : $("#lblCloseCounterCash").val();

            var GrndTotal = 0;
            $.each($("table[id*=" + 'gvDenomination' + "] tr"), function (index, item) {
                if ($(item).find("input[type=hidden]").length > 0) {
                    var Dnmtn = $(item).find("[id*=lblDenominationValue_]").val();
                    ///var Dnmtn = $(item).find("[id*=lblDenominationValue_]").text();
                    var ps = $(item).find("[id*=txtPieces_]").val();
                    var Total = parseFloat(Dnmtn) * parseFloat(ps);
                    $(this).find("[id*='lblTotal_']").text(Total.toFixed(2));
                    GrndTotal = GrndTotal + Total;
                }
            });

            var ClosingBalance = parseFloat(Opening) + parseFloat(CashSales) - parseFloat(Salesreturn);
            $("#LblCloseCounterClosingBalance").val(ClosingBalance);

            $("#txtCounterCloseRemittance").val("0");
            $("#lblCloseCounterDenomination").val(GrndTotal);

            var Difference = parseFloat(ClosingBalance) - parseFloat(GrndTotal);
            $("#lblCloseCounterDifference").val(Difference);

        }


        </script>
</head>

<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <div class="btn_bg5"> 
                      <div class="heading2">
                   <div class="user">Counter</div>
                          
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">  
                        <div id="divAdd" runat="server" style="display: block;">
                             <div class="divL">
                            <ul class="contentul">

                                  <li>
                                    <asp:Label ID="Label17" runat="server" CssClass="label" Text="Counter"></asp:Label>
                                    <asp:DropDownList ID="ddlCounter" width="225px"    AutoPostBack="true" OnSelectedIndexChanged="ddlCounter_SelectedIndexChanged" runat="server" CssClass="dropdownMandatory" TabIndex="2"></asp:DropDownList>
                                      
                                       <asp:RequiredFieldValidator ID="reqCounter" runat="server" ErrorMessage="Short name required" ControlToValidate="ddlCounter" Display="None"  SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="reqCounter">
                                                </cc:ValidatorCalloutExtender>
                                   
                                </li>
                                <li>
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Opening"></asp:Label>
                                        <asp:TextBox ID="LblCloseCounterOpening" Style="text-align:right" CssClass="textbox" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Sales"></asp:Label>
                                        <asp:TextBox ID="LblCloseCounterSales" Style="text-align:right" CssClass="textbox" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Cash Amount"></asp:Label>
                                        <asp:TextBox ID="lblCloseCounterCash" Style="text-align:right" CssClass="textbox" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Cheque Amount"></asp:Label>
                                        <asp:TextBox ID="lblCloseCounterCheque" Style="text-align:right" CssClass="textbox" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="Card Amount"></asp:Label>
                                        <asp:TextBox ID="lblCloseCounterCard" Style="text-align:right" CssClass="textbox" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label9" runat="server" Style="display:none" CssClass="label" Text="Voucher Amount"></asp:Label>
                                        <asp:TextBox ID="lblCloseCounterVoucher"  Style="display:none" CssClass="textbox" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label10" runat="server" CssClass="label" Text="Sales Return"></asp:Label>
                                        <asp:TextBox ID="lblCloseCounterSalesReturn" Style="text-align:right" CssClass="textbox" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label11" runat="server" CssClass="label" Text="Closing"></asp:Label>
                                        <asp:TextBox ID="LblCloseCounterClosingBalance" Style="text-align:right" CssClass="textbox" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label12" runat="server" CssClass="label" Text="Denomination"></asp:Label>
                                        <asp:TextBox ID="lblCloseCounterDenomination" Style="text-align:right" CssClass="textbox" runat="server" Enabled="false" ReadOnly="false"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label13" runat="server" CssClass="label" Text="Difference"></asp:Label>
                                        <asp:TextBox ID="lblCloseCounterDifference" Style="text-align:right" CssClass="textbox" runat="server" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label14" runat="server" CssClass="label" Text="Remittance Amount"></asp:Label>
                                        <asp:TextBox ID="txtCounterCloseRemittance" Style="text-align:right" CssClass="textbox" runat="server" TabIndex="1" onKeypress="CheckNumWithDecimal(event);"
                                            onfocus="javascript:if(this.value=='0' || this.value=='0.00') {this.value='';}"
                                            onblur="javascript:if(this.value==''){this.value='';}"></asp:TextBox>
                                    </li>
                                <li></li>
                               <%-- <li class="btnMargin">
                                    <asp:CheckBox Text="Active" ID="cbxAcctive" runat="server" CssClass="chckbox" TabIndex="7" />
                                </li>--%>
                             

                                <li class="btnMargin" style="padding-left:145px">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" TabIndex="15" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd();"   OnClick="btnClear_Click" TabIndex="16" />
                                    <asp:HiddenField ID="hfCounterId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                      <asp:HiddenField ID="hfuserName" runat="server" />
                                     <asp:HiddenField ID="hfMasterID" runat="server" />
                                     <asp:HiddenField ID="hfStore" runat="server" />
                                      <asp:HiddenField ID="hfCounter" runat="server" />
                                    
                                </li>
                            </ul>
                                 </div>
                              <div class="divR">
                                <ul class="contentul">
                                    <li>
                                        <asp:GridView ID="gvDenomination" runat="server" Width="100%" Height="100%" CssClass="grid"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="Teal" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                            ForeColor="Black" GridLines="Vertical" PageSize="100"
                                            DataKeyNames="ID_DenominationMaster" AllowPaging="true"
                                            OnPageIndexChanging="gvDenomination_PageIndexChanging" Font-Bold="true" Font-Size="12px">
                                                <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Denomination" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="12px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDenominationName" runat="server"><%#Eval("DenominationName") %></asp:Label>
                                                        <asp:HiddenField ID="lblDenominationValue" runat="server" Value='<%# Eval("DenominationValue") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="x" HeaderText=" " ItemStyle-HorizontalAlign="Center" ControlStyle-Width="20px" />
                                                <asp:TemplateField HeaderText="Pieces" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtPieces" onchange="CalculateAmount(id);" AutoComplete="off" CssClass="textbox" Style="width: 120px; text-align: right;" onkeypress="CheckNumWithDecimal(event);"
                                                            Text='<%# Eval("Pieces") %>' onfocus="javascript:if(this.value=='0' || this.value=='0.00') {this.value='';}"
                                                            onblur="javascript:if(this.value==''){this.value='0';}" />
                                                        <asp:HiddenField ID="hdfPieces" runat="server" Value='<%# Eval("Pieces") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="DenominationValue" HeaderText="DenominationValue" />
                                                <asp:BoundField DataField="DenominationName" HeaderText="Denomination" />
                                                <asp:BoundField DataField="Pieces" HeaderText="Pieces" />
                                                <asp:BoundField DataField="Total" HeaderText="Total" />--%>
                                            </Columns>
                                           <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle  CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
                                            <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </li>
                                </ul>
                            </div>
                        </div>
                            </div>
                     



<%--                            <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                           <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divheader" runat="server" class="PopupHeader" >
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head"  CssClass="lblPopupHeader"></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>

                            <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                        </asp:Panel>
                          <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                        </cc:ModalPopupExtender>
                        
                    <asp:Button ID="btnS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopUpdelete" runat="server" CssClass="DivStyle" Style="display: none;">
                            <div id="divPopheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader1" runat="server" Text="Reason"></asp:Label>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                            <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                                </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divPopUpdelete" TargetControlID="btnS"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divPopheader">
                        </cc:ModalPopupExtender>
                             <asp:Button ID="btnP" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                            <asp:Panel ID="pnlPrvlg" runat="server" Style="display: none;" CssClass="DivStyle">
                                <div id="divPrvlg" runat="server" class="PopupHeader">
                                    <asp:Label ID="Label15" runat="server" Text="Privilege" CssClass="lblPopupHeader"></asp:Label>
                                    <asp:ImageButton ID="btnCP" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                </div>
                                <uc1:CommonPopupPrivilege runat="server" ID="CommonPopupPrivilege" />
                            </asp:Panel>
                            <cc:ModalPopupExtender ID="mpPrivilege" runat="server" PopupControlID="pnlPrvlg" TargetControlID="btnP"
                                CancelControlID="btnCP" Drag="true" PopupDragHandleControlID="divPrvlg">
                            </cc:ModalPopupExtender>--%>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
