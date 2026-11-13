<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaxType.aspx.cs" Inherits="TaxType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>

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
            $("#<%#ddlGSTType.ClientID%>").val('0');
         
            $('#<%#chkTaxInterState.ClientID%>').prop('checked', false);
            $('#<%#chkTaxIntraState.ClientID%>').prop('checked', false);
           
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
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <div class="btn_bg5"> 
                       <div class="heading2">
                             <div class="user">Tax Type </div>
                           <asp:Panel ID="divMenuButtons" runat="server">
                        <div style="float: right; margin-top: -10px">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add.png"  CssClass="imagebuttonStyle"  TabIndex="9" runat="server"  OnClientClick=" EnterKeyFilter(); ShowAddDiv(); ClearAdd();   return true; releasebtn();" OnClick="ImageButton1_Click1" />
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png"  CssClass="imagebuttonStyle"    TabIndex="8"  runat="server"  OnClick="imgbtnView_Click" />
                        </div>
                               </asp:Panel>

                    </div>


                  <%--  <div class="user_bg">
                        <div class="user1">Bill Type</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                            <div class="main_bg">  
                        <div id="divAdd" runat="server" >
                            <div class="divL">
                            <ul class="contentul">

                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Style="width:158px;" Text="Tax Name"></asp:Label>
                                    <asp:TextBox ID="txttypeName" CssClass="textboxLongMandatory" runat="server" MaxLength="75" Width="259" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Tax Name required" ControlToValidate="txttypeName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                              <%--  <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Style="width:160px;" Text="Type"></asp:Label>
                                    <asp:DropDownList ID="ddlBillType" runat="server" CssClass="dropdownMandatory" TabIndex="2"  Style="width:260px;"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqvalddlBillType" runat="server" ErrorMessage="Type required" ControlToValidate="ddlBillType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalddlBillType">
                                    </cc:ValidatorCalloutExtender>
                                </li>--%>
                                <%--<li>
                                    <asp:Label ID="Label17" runat="server" CssClass="label" Style="width:160px;" Text="Account Head"></asp:Label>
                                    <asp:DropDownList ID="ddlAccountHead" runat="server" onblur="BlurSubHead();" TabIndex="3" CssClass="dropdownMandatory"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select an account  head" ControlToValidate="ddlAccountHead" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>--%>
                                <%--<li>
                                    <asp:Label ID="Label24" runat="server" Text="Tax Type" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlTaxType" runat="server" CssClass="dropdown" TabIndex="2">
                                     <asp:ListItem Selected="True" Value="1">CGST</asp:ListItem>
                                     <asp:ListItem Selected="True" Value="2">SGST</asp:ListItem>
                                     <asp:ListItem Selected="True" Value="3">IGST</asp:ListItem>
                                     <asp:ListItem Selected="True" Value="4">CESS</asp:ListItem>
                                     <asp:ListItem Selected="True" Value="5">Others</asp:ListItem>                
                                    </asp:DropDownList>
                                </li>--%>
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Style="width:158px;" Text="Tax Type" ></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlGSTType"  CssClass="dropdownMandatory" MaxLength="75" Width="265" TabIndex="2">
                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                        <asp:ListItem   Value="1">CGST</asp:ListItem>
                                        <asp:ListItem   Value="2">SGST</asp:ListItem>
                                        <asp:ListItem   Value="3">IGST</asp:ListItem>
                                        <asp:ListItem   Value="4">CESS</asp:ListItem>
                                          <asp:ListItem   Value="5">FCESS</asp:ListItem>
                                        <asp:ListItem   Value="6">Others</asp:ListItem> 
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Tax Type required" ControlToValidate="ddlGSTType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
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
                                    <asp:ImageButton ID="ImageButtonheadsub" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonheadsub_Click" style="margin-bottom:-8px;" TabIndex="2" />
                                         
                             
                                    <asp:TextBox ID="txtAccountHeadSub" runat="server" Width="167px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                    <%--<asp:DropDownList ID="ddlAccountHeadSub" runat="server" CssClass="dropdown" TabIndex="4"></asp:DropDownList>--%>
                                  
                                </li>
                           <%--      <li class="btnMargin" style="padding-left:157px">
                                    <asp:CheckBox ID="chkTaxInterState" Width="160px" runat="server" CssClass="chckbox" TabIndex="12" Text="Inter State" />
                                    <asp:CheckBox ID="chkTaxIntraState"  Width="160px" runat="server" CssClass="chckbox" Style="float:left" TabIndex="13" Text="Intra State" />
                                    
                                </li>--%>
                                    <li>
                                   <asp:Label ID="lblInterState" runat="server" Width="158px" CssClass="label" Text="Interstate"></asp:Label>
                                     <asp:CheckBox ID="chkTaxInterState" runat="server"  TabIndex="12" /> 

                               </li>
                                  <li>
                                   <asp:Label ID="lblIntraState" runat="server" Width="158px" CssClass="label" Text="Intrastate"></asp:Label>
                                     <asp:CheckBox ID="chkTaxIntraState" runat="server"  Width="15px" TabIndex="13" /> 

                               </li>
                             


                                <div class="clear"></div>

                                <li class="btnMargin" style="padding-left:160px">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" TabIndex="2" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); " TabIndex="2" OnClick="btnClear_Click" />   
                               
                                    <asp:HiddenField ID="hfBillTypeId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                     <asp:HiddenField ID="hfUserName" runat="server" />
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
                                    <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" TabIndex="10"></asp:TextBox>
                                </li>
                                <div class="clear"></div>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="11" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="12" />
                                </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvBillType" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White"  BorderStyle="Solid" Width="825px" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowPaging="true" OnPageIndexChanging="gvBillType_PageIndexChanging" PageSize="10" DataKeyNames="ID_TaxType" OnRowDeleting="gvBillType_RowDeleting" OnSelectedIndexChanging="gvBillType_SelectedIndexChanging">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />

                                        <asp:TemplateField HeaderText="Name ">
                                            <ItemTemplate>
                                                <div style="width: 200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                    <%# Eval("TaxType") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      <%--  <asp:BoundField DataField="BillTypeName" HeaderText="TaxType" ItemStyle-Width="200px" />--%>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                       
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                          
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageBtnDelete" runat="server" CommandName="Delete" ToolTip="Delete" OnClick="ImageBtnDelete_Click" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
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
                                <asp:Label ID="Label3" runat="server" Text="Reason"></asp:Label>
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
