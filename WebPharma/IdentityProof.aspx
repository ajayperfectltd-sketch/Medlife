<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IdentityProof.aspx.cs" Inherits="IdentityProof" EnableEventValidation="false" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc2" TagName="MedLifePageHelp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add New IdentityProof</title>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
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
        function PopupOnReason() {
            try {

                $("#divPopupReason").css('display', 'block');
            }
            catch (ex) {

            }
        }

        function ClearAdd() {

          
            submit = 0;
            $("#<%#txtIdpName.ClientID%>").val('');
            $("#<%#ddlIdpType.ClientID%>").val("0");
         $("#<%#txtIdpDescription.ClientID%>").val('');
            $("#<%#txtSortOrder.ClientID%>").val('');
            $("#<%#hdfIdentityprrof.ClientID%>").val('');

         $('#<%#chkAddressProof.ClientID%>').prop('checked', true);
         $('#<%#chkIdProof.ClientID%>').prop('checked', true);
         $("#<%#txtIdpName.ClientID%>").focus();

     }
   

        function ShowAddDiv() {
           

                    $("#<%#divAdd.ClientID%>").show();
                    $("#<%#divView.ClientID%>").hide();
                    $("#<%#txtIdpName.ClientID%>").focus();
                }


         function ClearSearch() {

             $("#<%#txtSearchName.ClientID%>").val('');
             $("#<%#txtSearchName.ClientID%>").focus();
         }

        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

        }
        function EnterTab(e, Id1) {

            var intKey = window.Event ? e.which : e.KeyCode;


            if (intKey == 13) {
                e.returnValue = false;
                $("#" + Id1).focus();
            }
        }

    </script>






</head>
<body>
    <form id="frmIdentityProof" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                   <div class="btn_bg5"> 
                         <div class="heading2">
                               <div class="user">Identity Proof </div>
                    <asp:Panel ID="divMenuButtons" runat="server">
                        <div style="float: right; margin-top: -10px">
                            <uc2:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                            <asp:ImageButton ID="ImageButton1" CssClass="imagebuttonStyle" ImageUrl="images/ADD.png"  OnClick="ImageButton1_Click"   runat="server" OnClientClick="ShowAddDiv(); ClearAdd();  releasebtn();" TabIndex="10" />
                        
                            <asp:ImageButton ID="imgbtnView" CssClass="imagebuttonStyle" ImageUrl="images/view.png"   TabIndex="9" runat="server" OnClick="imgbtnView_Click" />
                        </div>
                        </asp:Panel>

                    </div>

                    <div class="clear"></div>
                    <div class="group_bg2">
                         <div class="main_bg">  
                        <div id="divAdd" runat="server">



                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtIdpName" onkeydown="EnterTab(event,'ddlIdpType');" CssClass="textboxMandatory" runat="server" MaxLength="100" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage=" Name required" ControlToValidate="txtIdpName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                <li>
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Type"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlIdpType" onkeydown="EnterTab(event,'txtIdpDescription');" CssClass="dropdownMandatory" TabIndex="2">
                                        <asp:ListItem Text="select" Value="0" />
                                        <asp:ListItem Text="Passport" Value="1" />
                                        <asp:ListItem Text="Driving Licence" Value="2" />
                                        <asp:ListItem Text="Election Id" Value="3" />
                                        <asp:ListItem Text="PAN Card" Value="4" />
                                        <asp:ListItem Text="Aadhaar(UID)" Value="5" />
                                        <asp:ListItem Text="Ration Card" Value="6" />
                                        <asp:ListItem Text="Others" Value="7"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Type required" ControlToValidate="ddlIdpType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Description"></asp:Label>
                                    <asp:TextBox ID="txtIdpDescription" onkeydown="EnterTab(event,'txtSortOrder');" CssClass="textbox" runat="server" MaxLength="100" TabIndex="3"></asp:TextBox>
                                   


                                </li>


                                <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="Sort Order"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtSortOrder"  onkeydown="EnterTab(event,'btnSubmit');" Style="text-align: right;" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="4" MaxLength="4" />
                             
                                </li>

                                      <li>
                                   <asp:Label ID="lblActive" runat="server" CssClass="label" Text="Address Proof"></asp:Label>
                                     <asp:CheckBox ID="chkAddressProof" runat="server" Width="15"  TabIndex="5" /> 

                               </li>
                                    <li>
                                   <asp:Label ID="Label5" runat="server" CssClass="label" Text="Id Proof"></asp:Label>
                                     <asp:CheckBox ID="chkIdProof" runat="server" Width="15"  TabIndex="6" /> 

                               </li>
                               <%-- <li class="btnMargin">--%>

                                    <%--<asp:CheckBox ID="chkAddressProof" runat="server" CssClass="chckbox" TabIndex="5" Text="Address Proof" style="padding-right:0px;"/>--%>
                                    <%--<asp:CheckBox ID="chkIdProof" runat="server" CssClass="chckbox" TabIndex="6" Text="Id Proof" style="padding-right:0px;"/>--%>
                               <%-- </li>--%>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="7" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="8" OnClientClick="ClearAdd(); return false;" />
                                    <asp:HiddenField ID="hdfIdentityprrof" runat="server" />
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
                            <%--style="display: none;"--%>
                            <ul class="contentul">
                                <li>

                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchName" CssClass="textbox" TabIndex="12" runat="server"></asp:TextBox>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="13" />
                                        <asp:Button ID="btnSearchClear" TabIndex="14" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="grididentityproofs" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="white"  BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" AllowPaging="true" OnPageIndexChanging="grididentityproofs_PageIndexChanging" GridLines="Vertical" PageSize="10" DataKeyNames="ID_IdentityProof" OnRowDeleting="identityproofs_RowDeleting" OnSelectedIndexChanging="identityproofs_SelectedIndexChanging">
                                    <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SlNO" HeaderText="SlNo"  ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Type" HeaderText="Type" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="SortOrder" ItemStyle-HorizontalAlign="Right" HeaderText="Sort Order" />
                                        <asp:BoundField DataField="AddressProof" HeaderText="Address Proof" />
                                        <asp:BoundField DataField="IdProof" HeaderText="ID Proof" />

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                            </ul>
                        </div>
                             </div>



                    </div>
                </div>
                  <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Reason" CssClass="lblPopupHeader"></asp:Label>
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
            </ContentTemplate>
        </asp:UpdatePanel>
          <script src="js/toastr.js"></script>
    </form>
</body>
</html>
