<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategoryFirst.aspx.cs" Inherits="CategoryFirst" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc2" TagName="MedLifePageHelp" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>


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

        function ClearAdd() {
            submit = 0;
            $("#<%#txtShortName.ClientID%>").val('');
            $("#<%#txtName.ClientID%>").val('');
            $("#<%#hfCategoryFirstId.ClientID%>").val('0');
            $("#<%#txtShortName.ClientID%>").focus();
            $("#<%#txtProfit.ClientID%>").val('0.00');
            $("#<%#txtDiscountPercentage.ClientID%>").val('0.00');
            $("#<%#txtCommission.ClientID%>").val('0.00');
            $("#<%#txtExpiryDays.ClientID%>").val('0');
            $('#<%#chkNonMedicine.ClientID%>').prop('checked', false);
        }
        function ClearSearch() {
            $("#<%#txtSearchShortName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchShortName.ClientID%>").focus();
        }
        function ShowAddDiv() {
          
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtShortName.ClientID%>").focus();
        }
        function disc() {
            if ($("#txtDiscountPercentage").val() == '') {
                $("#<%#txtDiscountPercentage.ClientID%>").val('0.000');
            }
            return;
        }

   
        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

        }  
        function PopupOnReason() {
            try {

                $("#divPopupReason").css('display', 'block');
            }
            catch (ex) {
            }
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
                    <div class="user">Category</div>
                  
                       <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                        <uc2:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" OnClick="ImageButton1_Click"  CssClass="imagebuttonStyle"    runat="server" TabIndex="11" OnClientClick="ShowAddDiv(); ClearAdd(); releasebtn();" />
                        
                       
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png"  CssClass="imagebuttonStyle" runat="server"     TabIndex="10" OnClick="imgbtnView_Click" />
                        </div>
                            </asp:Panel>  
                    </div>


                    
                    <div class="clear"></div>
                    <div class="group_bg2">
                          <div class="main_bg">  
                        <div id="divAdd" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                    <asp:TextBox ID="txtShortName" CssClass="textboxMandatory" runat="server" MaxLength="10" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Short name required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtName" CssClass="textboxMandatory" runat="server" MaxLength="75" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label40" runat="server" CssClass="label" Text="Discount %"></asp:Label>
                                    <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" TargetControlID="txtDiscountPercentage" FilterType="Custom , Numbers" ValidChars=".">
                                    </cc:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtDiscountPercentage" Style="text-align: right;" CssClass="textbox" TabIndex="3" runat="server" MaxLength="5"></asp:TextBox>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDiscountPercentage" ErrorMessage="Enter a valid  %" Display="None" MaximumValue="100" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                    <li>
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Profit "></asp:Label>
                                    <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtProfit" FilterType="Custom , Numbers" ValidChars=".">
                                    </cc:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtProfit" Style="text-align: right;" CssClass="textbox" TabIndex="4" runat="server" MaxLength="5"></asp:TextBox>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtProfit" ErrorMessage="Enter a valid  Profit" Display="None" MaximumValue="100" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                    <li>
                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="Expiry Validation Before Days"></asp:Label>
                                <asp:TextBox ID="txtExpiryDays" CssClass="textbox"  ToolTip="Expiry Validation before Days"  Style="text-align:right;" runat="server" MaxLength="5" TabIndex="4"  onKeypress="CheckIntegerAndNeg(event);"></asp:TextBox>
                              
                            </li>
                                       <li>
                                    <asp:Label ID="Label7" runat="server" CssClass="label" style="margin-left: -138px;"  Text="Commission "></asp:Label>
                                    <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtCommission" FilterType="Custom , Numbers" ValidChars=".">
                                    </cc:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtCommission" Style="text-align: right;" CssClass="textbox" TabIndex="6" runat="server" MaxLength="5"></asp:TextBox>
                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtCommission" ErrorMessage="Enter a valid  Commision" Display="None" MaximumValue="100" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RangeValidator3">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                  <li>
                                        <asp:Label ID="lblActive" runat="server" CssClass="label" Text="Non-Medicine"></asp:Label>
                                        <asp:CheckBox ID="chkNonMedicine" runat="server" CssClass="chckbox"   TabIndex="7"/>
                                        </li>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="8" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="9" />
                                    <asp:HiddenField ID="hfCategoryFirstId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                      <asp:HiddenField ID="hfuserName" runat="server" />
                                 <asp:HiddenField ID="hfMasterID" runat="server" />
                                </li>
                            </ul>
                        </div>
                              </div>
                          <div class="main_bg">  
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchShortName" CssClass="textbox" runat="server" TabIndex="11" MaxLength="10"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" MaxLength="75" TabIndex="12"></asp:TextBox>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="13" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="14" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvCategoryFirst" runat="server" CssClass="grid"  ShowFooter="true"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White"  BorderStyle="Solid" Width="825px" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvCategoryFirst_PageIndexChanging" DataKeyNames="ID_Category" OnRowDeleting="gvCategoryFirst_RowDeleting" OnSelectedIndexChanging="gvCategoryFirst_SelectedIndexChanging">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SlNo" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ShortName" HeaderText="Short Name" />

                                        <asp:TemplateField HeaderText="Name ">
                                            <ItemTemplate>
                                                <div style="width: 200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                    <%# Eval("CategoryName") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                               
                                    <HeaderStyle   CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
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
                    </div>
                            <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server" Style="display: none; " CssClass="DivStyle">
                            <div id="divheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Reason"></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                            <uc2:CommonPopupDelete ID="CommonPopup1" runat="server" />
                                </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
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
                            </cc:ModalPopupExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </form>
</body>
</html>
