 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankBranch.aspx.cs" Inherits="BankBranch" %>

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
        function pageLoad() {
            Check();
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
        function ClearHeads() {

            $("#<%#txtAccountHead.ClientID%>").val('');
                $("#<%#txtSubHeadRefno.ClientID%>").val('');
                $("#<%#txtAccountSubhead.ClientID%>").val('');

            }
        function ClearAdd() {
            submit = 0;
            $("#<%#ddlBank.ClientID%>").val('0');
            $("#<%#txtShortName.ClientID%>").val('');
            $("#<%#txtName.ClientID%>").val('');
            $("#<%#ddlCountry.ClientID%>").val('0');
            $("#<%#txtAddress1.ClientID%>").val('');
            $("#<%#txtAddress2.ClientID%>").val('');
            $("#<%#txtAddress3.ClientID%>").val('');
            ClearHeads();
            $("#<%#hfBankBranchId.ClientID%>").val('0');
            $("#<%#txtRefno.ClientID%>").val('');
            $('#<%#chkActive.ClientID%>').prop('checked', true);
            $("#<%#ddlBank.ClientID%>").focus();
        }
        function ClearSearch() {
            $("#<%#ddlSearchBank.ClientID%>").val('0');
            $("#<%#txtSearchShortName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#ddlSearchBank.ClientID%>").focus();
            
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#ddlBank.ClientID%>").focus();
        }

        function txtInput_onblur() {

            ClearHeads();
            return false;

        }
        function txtSubHead_onBlur() {
            $("#txtAccountSubhead").val('');

            return false;
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
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               <div>
                    <div class="icon_bg"> 
                             <div class="search" style="width:670px; " >Bank Branch</div>
                         <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" TabIndex="17" OnClientClick="ShowAddDiv(); ClearAdd(); return false; releasebtn();" />
                        </div>
                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" TabIndex="16" OnClientClick="ClearSearch();"  OnClick="imgbtnView_Click" />
                        </div>
                       
                    </div>
                 <%--   <div class="user_bg">
                        <div class="user1">Bank Branch
                        </div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server">
                            <%--style="display: none;"--%>
                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label8" runat="server" CssClass="label" Text="Bank"></asp:Label>
                                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="dropdownMandatory" TabIndex="1"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select a bank" ControlToValidate="ddlBank" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                    <asp:TextBox ID="txtShortName" CssClass="textboxMandatory" runat="server" MaxLength="10" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Short name required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtName" CssClass="textboxLongMandatory" runat="server" MaxLength="250" TabIndex="3"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label9" runat="server" CssClass="label" Text="Country"></asp:Label>
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="dropdown" TabIndex="4"></asp:DropDownList>
                                </li>
                                <li>
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Address1"></asp:Label>
                                    <asp:TextBox ID="txtAddress1" CssClass="textboxLong" runat="server" MaxLength="100" TabIndex="5"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="Address2"></asp:Label>
                                    <asp:TextBox ID="txtAddress2" CssClass="textboxLong" runat="server" MaxLength="100" TabIndex="6"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label7" runat="server" CssClass="label" Text="Address3"></asp:Label>
                                    <asp:TextBox ID="txtAddress3" CssClass="textboxLong" runat="server" MaxLength="100" TabIndex="7"></asp:TextBox>
                                </li>
                                <%-- <li style="display:none;">
                                <asp:Label ID="Label10" runat="server" CssClass="label" Text="Account Head"></asp:Label>
                                <asp:DropDownList ID="ddlAccountHead" runat="server" CssClass="dropdownMandatory"></asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select a account head" ControlToValidate="ddlAccountHead" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                </cc:ValidatorCalloutExtender>
                            </li>--%>
                                <li>
                                    <asp:Label ID="lblAcvountHead" runat="server" CssClass="label" Text="Account Head"></asp:Label>
                                    <asp:TextBox ID="txtRefno" runat="server" AutoPostBack="false" CssClass="textboxMandatory" TabIndex="8" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();"></asp:TextBox>
                                    <asp:ImageButton ID="btnGo" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="btnGo_Click" style="margin-bottom:-8px;" TabIndex="9" />
                                         
                              <%--      <asp:Button ID="btnGo" runat="server" Text="GO" CausesValidation="false" OnClick="btnGo_Click" TabIndex="9" />--%>
                                    <asp:TextBox ID="txtAccountHead" runat="server" Width="160px" CssClass="textboxMandatory" TabIndex="100"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="rfvAccountHead" runat="server" ErrorMessage="Account Head Refno required" ControlToValidate="txtRefno" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="rfvAccountHead">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Account Head required" ControlToValidate="txtAccountHead" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc:ValidatorCalloutExtender>

                                    <li>
                                        <asp:Label ID="lblAcvountHead1" runat="server" CssClass="label" Text="Account Sub Head"></asp:Label>
                                        <asp:TextBox ID="txtSubHeadRefno" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="11" onblur="txtSubHead_onBlur();" onKeypress="CheckInteger(event);" Width="40px"></asp:TextBox>
                                         <asp:ImageButton ID="btnSubheadGo" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="btnSubheadGo_Click" style="margin-bottom:-8px;"  TabIndex="12" />
                                        <%--<asp:Button ID="btnSubheadGo" runat="server" CausesValidation="false" Text="GO" i OnClick="btnSubheadGo_Click" TabIndex="12" />--%>
                                        <asp:TextBox ID="txtAccountSubhead" runat="server" CssClass="textbox" Width="160px" TabIndex="130"></asp:TextBox>

                                    </li>
                                </li>
                                <li class="btnMargin">
                                    <asp:CheckBox ID="chkActive" runat="server" CssClass="chckbox" Text="Active" TabIndex="13"  />
                                </li>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click"  TabIndex="14" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;"  TabIndex="15" />
                                    <asp:HiddenField ID="hfBankBranchId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                </li>
                            </ul>
                        </div>
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul" style="padding-left:20px;">
                                <li>
                                    <li>
                                        <asp:Label ID="Label11" runat="server" CssClass="label" Text="Bank"></asp:Label>


                                        <asp:DropDownList ID="ddlSearchBank" runat="server" CssClass="dropdown" TabIndex="18"></asp:DropDownList>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchShortName" CssClass="textbox" runat="server" TabIndex="19" MaxLength="10"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchName" CssClass="textbox" TabIndex="20" runat="server" MaxLength="100"></asp:TextBox>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="21" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="22" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul" style="padding-left:60px;">
                                <asp:GridView ID="gvBankBranch" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_BankBranch" OnSelectedIndexChanging="gvBankBranch_SelectedIndexChanging" OnRowDeleting="gvBankBranch_RowDeleting">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                        <asp:TemplateField HeaderText="Name ">
                                            <ItemTemplate>
                                                <div style="width: 200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                    <%# Eval("Name") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bank ">
                                            <ItemTemplate>
                                                <div style="width: 200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                    <%# Eval("Bank") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Active" HeaderText="Active" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                   
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                      
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="#12361D" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
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
                      <%--  <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                          <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                       <asp:Panel ID="divPopup" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 300px; height: 370px;">
                            <div id="divheader" runat="server" class="PopupHeader" >
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head"  CssClass="lblPopupHeader"></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>

                            <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                        </asp:Panel>
                          <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                        </cc:ModalPopupExtender>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
