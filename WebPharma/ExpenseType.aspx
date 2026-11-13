<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpenseType.aspx.cs" Inherits="ExpenseType" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>


<%@ Register src="Popup/CommonPopup.ascx" tagname="CommonPopup" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
        <script src="js/Navigation.js"></script>
    <script type="text/javascript">
        var submit = 0;

      
        function validateCheckboxes_ClientValidate(oSrc, args) {
           //alert("inside function");
            var chkPurchase = document.getElementById("chkPurchase");
            var chkSales = document.getElementById("chkSales");
            var chkPurchaseReturn = document.getElementById("chkPurchaseReturn");
            var chkSalesReturn = document.getElementById("chkSalesReturn");
            if (!chkPurchase.checked && !chkSales.checked && !chkPurchaseReturn.checked && !chkSalesReturn.checked) {
            
                args.IsValid = false;
            }
            else {
              
                args.IsValid = true;
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

        function BlurSubHead() {
            $("#btnSubHeadBlurTrigger").click();

        }
        
        function BlurCardSubHead() {
            $("#btnCardSubHeadBlurTrigger").click();

        }
        function BlurChequeSubHead() {
            $("#btnChequeSubHeadBlurTrigger").click();

        }
        
        function BlurVATSubHead() {
            $("#btnVatSubHeadBlurTrigger").click();

        }
        function BlurCGSTSubHead() {
            $("#btnCGSTSubHeadBlurTrigger").click();
        }
        function BlurSGSTSubHead() {
            $("#btnSGSTSubHeadBlurTrigger").click();
        }
        function BlurIGSTSubHead() {
            $("#btnIGSTSubHeadBlurTrigger").click();
        }
        function BlurCESSSubHead() {
            $("#btnCESSSubHeadBlurTrigger").click();
        }
        function BlurRoundingSubHead() {
            $("#btnRoundingSubHeadBlurTrigger").click();
        }
        function BlurFrightSubHead() {
            $("#btnFrightSubHeadBlurTrigger").click();
        }
        function BlurDiscountSubHead() {
            $("#btnDiscountSubHeadBlurTrigger").click();
        }


        function BlurLoadingSubHead() {
            $("#btnLoadingSubHeadBlurTrigger").click();

        }
        function BlurUnloadingSubHead() {
            $("#btnUnloadingSubHeadBlurTrigger").click();

        }
        function BlurDisplaySubHead() {
            $("#btnDisplaySubHeadBlurTrigger").click();

        }
        function BlurOthersSubHead() {
            $("#btnOthersSubHeadBlurTrigger").click();

        }
        function BlurAccountSubReturn() {
            $("#btnAccountSubReturn").click();

        }
        
        function ClearAdd() {
            submit = 0;
           

            $("#<%#txtRefno.ClientID%>").val('');
            $("#<%#txtRefno1.ClientID%>").val('');
      
        
  

            $("#<%#txtAccountHead.ClientID%>").val('');
            $("#<%#txtAccountHeadSub.ClientID%>").val('');
        

            $("#<%#txtAccountHead.ClientID%>").val('');

        

     
         
            $("#<%#txttypeName.ClientID%>").val('');
            $("#<%#hfBillTypeId.ClientID%>").val('0');

            $('#<%#chkPurchase.ClientID%>').prop('checked', true);
            $('#<%#chkSales.ClientID%>').prop('checked', true);
            $('#<%#chkPurchaseReturn.ClientID%>').prop('checked', true);
            $('#<%#chkSalesReturn.ClientID%>').prop('checked', true);
            $('#<%#chkAddless.ClientID%>').prop('checked', true);
           
            $("#txttypeName").focus();

           
        }
       
        function ClearSearch() {

            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").focus();
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#txttypeName").focus();
        }
        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

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
            
         
        
    </script>
   
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                   <div class="btn_bg5"> 
                       <div class="heading2">
                             <div class="user">Expense Type </div>
                            <asp:Panel ID="divMenuButtons" runat="server">
                        <div style="float: right; margin-top: -10px">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add.png" CssClass="imagebuttonStyle"      TabIndex="11" runat="server"  OnClientClick=" EnterKeyFilter(); ShowAddDiv(); ClearAdd();   return true; releasebtn();" OnClick="ImageButton1_Click1" />

                       
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle"   TabIndex="10"     runat="server"  OnClick="imgbtnView_Click" />
                        </div>
                                </asp:Panel>

                    </div>

                    <div class="clear"></div>
                    <div class="group_bg2">
                              <div class="main_bg">  
                        <div id="divAdd" runat="server" >
                            <div class="divL">
                            <ul class="contentul">

                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Style="width:158px;" Text="Expense Name"></asp:Label>
                                    <asp:TextBox ID="txttypeName" CssClass="textboxLongMandatory" runat="server" MaxLength="75" Width="260" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Expense Name required" ControlToValidate="txttypeName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                           
                                 <li>
                                    <asp:Label ID="lblAcvountHead" runat="server" CssClass="label" Text="Account Head" Width="158px"></asp:Label>
                                    <asp:TextBox ID="txtRefno" runat="server" AutoPostBack="true" CssClass="textboxMandatory" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="btnGo" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="btnGo_Click" style="margin-bottom:-8px;" TabIndex="2" />
                                         
                             
                                    <asp:TextBox ID="txtAccountHead" runat="server" Width="167px" CssClass="textboxMandatory" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="rfvAccountHead" runat="server" ErrorMessage="Account Head Refno required" ControlToValidate="txtAccountHead" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="rfvAccountHead">
                                    </cc:ValidatorCalloutExtender>
                                 <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Account Head required" ControlToValidate="txtAccountHead" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc:ValidatorCalloutExtender>--%>

                                </li>
                                <li>
                                    <asp:Label ID="Label18" runat="server" CssClass="label" Style="width:158px;" Text="Acc SubHead"></asp:Label>
                                     <asp:TextBox ID="txtRefno1" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonheadsub" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonheadsub_Click" style="margin-bottom:-8px;" TabIndex="3" />
                                         
                             
                                    <asp:TextBox ID="txtAccountHeadSub" runat="server" Width="167px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                    <%--<asp:DropDownList ID="ddlAccountHeadSub" runat="server" CssClass="dropdown" TabIndex="4"></asp:DropDownList>--%>
                                  
                                </li>
                               
                             <%--  <li class="btnMargin" style="padding-left: 153px;">
                                    <asp:CheckBox ID="chkPurchase" runat="server" Checked="true" CssClass="chckbox" Text="Purchase" TabIndex="3"/>
                                </li>
                                <li class="clear"></li>
                                <li class="btnMargin" style="padding-left: 153px;">
                                    <asp:CheckBox ID="chkSales" runat="server" Checked="true" CssClass="chckbox" Text="Sales" TabIndex="4" />
                                </li>
                                <li class="btnMargin" style="padding-left: 153px;">
                                    <asp:CheckBox ID="chkPurchaseReturn" Width="150px" runat="server" Checked="true" CssClass="chckbox" Text="Purchase Return" TabIndex="5"/>
                                </li>
                                <li class="clear"></li>
                                <li class="btnMargin" style="padding-left: 153px;">
                                    <asp:CheckBox ID="chkSalesReturn" Width="150px" runat="server" Checked="true" CssClass="chckbox" Text="Sales Return" TabIndex="6" />
                                </li>--%>

                                     <li>
                                   <asp:Label ID="lblpurchase" runat="server" Width="158px" CssClass="label" Text="Purchase"></asp:Label>
                                     <asp:CheckBox ID="chkPurchase"  Checked="true" runat="server"  TabIndex="3" /> 

                               </li>
                                    <li>
                                   <asp:Label ID="lblSales" runat="server" Width="158px" CssClass="label" Text="Sales"></asp:Label>
                                     <asp:CheckBox ID="chkSales"  Checked="true" runat="server"  TabIndex="4" /> 

                               </li>
                                    <li>
                                   <asp:Label ID="lblPurRet" runat="server" Width="158px" CssClass="label" Text="Purchase Return"></asp:Label>
                                     <asp:CheckBox ID="chkPurchaseReturn"  Checked="true" runat="server"  TabIndex="5" /> 

                               </li>

                                   <li>
                                   <asp:Label ID="lblSalesRet" runat="server" Width="158px" CssClass="label" Text="Sales Return"></asp:Label>
                                     <asp:CheckBox ID="chkSalesReturn"  Checked="true" runat="server"  TabIndex="6" /> 

                               </li>
                                 <li>
                                   <asp:Label ID="Label1" runat="server" Width="158px" CssClass="label" Text="Add/Less"></asp:Label>
                                     <asp:CheckBox ID="chkAddless"  Checked="true" runat="server"  TabIndex="7" /> 

                               </li>
                               <asp:CustomValidator id="validateCheckboxes" runat="server" ErrorMessage="At least one selection needed" ValidationGroup="submit" ClientValidationFunction="validateCheckboxes_ClientValidate"  OnServerValidate="validateCheckBoxes_ServerValidate"></asp:CustomValidator>
                                
                                <%-- 
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="validateCheckboxes">
                                    </cc:ValidatorCalloutExtender>--%>
                                <div class="clear"></div>

                                <li class="btnMargin" style="padding-left:160px">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" TabIndex="8" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); " TabIndex="9" OnClick="btnClear_Click" />   
                               
                                    <asp:HiddenField ID="hfBillTypeId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                    <asp:HiddenField ID="hfUsername" runat="server" />
                                    <asp:HiddenField ID="hfMasterID" runat="server" />
                                </li>
                            </ul>
                                </div>
                              <div class="divR">
                                   <ul class="contentul">
                                       
                               
                                    </ul>
                                  </div>
                        </div>
                                  </div>
                              <div class="main_bg">  
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <%-- <li>
                                 <li>
                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="ShortName"></asp:Label>
                                <asp:TextBox ID="txtSearchShortName" CssClass="textbox" runat="server"></asp:TextBox>
                            </li>--%>
                                <li>
                                    <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" TabIndex="12"></asp:TextBox>
                                </li>
                                <div class="clear"></div>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="13" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="14" />
                                </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvBillType" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvBillType_PageIndexChanging"
                                    BackColor="White" BorderStyle="Solid" Width="825px" BorderWidth="1px"
                                     CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="ID_ExpenseType" OnRowDeleting="gvBillType_RowDeleting" OnSelectedIndexChanging="gvBillType_SelectedIndexChanging">
                                    <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />

                                        <asp:TemplateField HeaderText="Name ">
                                            <ItemTemplate>
                                                <div style="width: 200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                    <%# Eval("ExpenseType") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      <%--  <asp:BoundField DataField="BillTypeName" HeaderText="BillType" ItemStyle-Width="200px" />--%>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                       
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                          
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageBtnDelete" runat="server" CommandName="Delete" OnClick="ImageBtnDelete_Click" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle  CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
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
                        <%--<asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />--%>
                        <%--<asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                         <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
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


                          <asp:Button ID="btnReasonShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divReasonPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divReasonheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="Label3" runat="server" Text="Reason" ></asp:Label>
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
    </form>
</body>
</html>
