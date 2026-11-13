<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Users" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
      <script src="js/Navigation.js"></script>
    <script type="text/javascript">
        function ClearAdd() {
            try {
                $("#<%#ddlCustomer.ClientID%>").val('0');
              
                $("#<%#ddlUserGroup.ClientID%>").val('0');
                $("#<%#ddlStore.ClientID%>").val('0');
                
                $("#<%#txtName.ClientID%>").val('');
                $("#<%#txtUsername.ClientID%>").val('');
                $("#<%#txtPassword.ClientID%>").val('');
                $("#<%#hfUserId.ClientID%>").val('0');
                $('#<%#chkActive.ClientID%>').prop('checked', true);
                $("#<%#ddlCustomer.ClientID%>").focus();
                document.getElementById('imgAttachment').setAttribute('src', 'UploadedItems/DefaultImage.jpg');
            }
            catch (ex) {
                alert(ex);
            }
        }
        function ClearSearch() {
          
            $("#<%#ddlSearchUserGroup.ClientID%>").val('0');
            $("#<%#txtSearchUsername.ClientID%>").val('');
            $("#<%#ddlSearchUserGroup.ClientID%>").focus();
        }
        function ShowAddDiv() {
        
          
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#ddlCustomer.ClientID%>").focus();
        }
        function checkClick() {
            if (document.getElementById('chkReset').checked) {
                document.getElementById('txtPassword').disabled = false;
                var valName = document.getElementById('RequiredFieldValidator3');
                ValidatorEnable(valName, true);
            }
            else {
                document.getElementById('txtPassword').disabled = true;
                $("#<%#txtPassword.ClientID%>").val('');
                var valName = document.getElementById('RequiredFieldValidator3');
                ValidatorEnable(valName, false);
            }
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

        function alphanumeric(alphane) {
           
            var numaric = $("#<%#txtPassword.ClientID%>").val();
           // alert(numaric);
           
            var p = numaric,
            errors = [];
            if (p.length > 0) {


                if (p.search(/[a-z]/i) < 0) {
                    errors.push("Your password must contain at least one letter.");
                   
                }
                if (p.search(/[0-9]/) < 0) {
                    errors.push("Your password must contain at least one digit.");
                  
                }
                if (p.search(/[!@#$%^&*?() "]/) < 0) {
                    errors.push("Your password must contain at least one Special Character.");
                   
                }
                if (errors.length > 0) {
                    $("#<%#txtPassword.ClientID%>").val('');
                    //alert(errors.join("\n"));
                    alert("Your password must contain \nAt least One letter, One digit & One Special Character.");
                    return false;
                }

            }
            return true;

           // return true;
        }
        function pageLoad() {
          //  btnHiddenImage
            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

        }

        function UploadComplete1(sender, args) {
            $("#<%#btnHiddenImage.ClientID%>").click();
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
                        <div class="user">Users</div>
                         <div style="float: right; margin-top: -10px">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle"  TabIndex="11" runat="server" OnClick="imgbtnAdd_Click" />
                    
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle" TabIndex="10" runat="server" OnClick="imgbtnView_Click" />
                      </div>
                    </div>
                        <div class="clear"></div>
                    <%--   <div class="user_bg">
                        <div class="user1">Users</div>
                    </div>--%>
                   
                    <div class="group_bg2">
                         <div class="main_bg">  
                        <div id="divAdd" runat="server" style="display: block;">
                            <div style="float: left; width: 80%">
                                <ul class="contentul">
                                   
                                    <li>
                                        <asp:Label ID="Label9" runat="server" CssClass="label" Text="Staff"></asp:Label>
                                        <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="dropdownMandatory" TabIndex="2"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ErrorMessage="Select Staff" ControlToValidate="ddlCustomer" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="RequiredFieldValidator6_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="rfvEmployee">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtName" CssClass="textboxMandatory" runat="server" MaxLength="100" TabIndex="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li>
                                        <asp:Label ID="Label10" runat="server" CssClass="label" Text="User code"></asp:Label>
                                        <asp:TextBox ID="txtUsername" CssClass="textboxMandatory" runat="server"  AutoCompleteType="Disabled" MaxLength="5" TabIndex="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="User code required" ControlToValidate="txtUsername" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Password"></asp:Label>
                                        <asp:TextBox ID="txtPassword" CssClass="textboxMandatory" TextMode="Password" onblur="alphanumeric(this);" AutoCompleteType="Disabled" runat="server" MaxLength="20" TabIndex="5"></asp:TextBox>
                                        <cc:PasswordStrength ID="txtPassword_PasswordStrength" TextStrengthDescriptions="Weak;Average;Good;Excellent" TextStrengthDescriptionStyles="WeakStrength;AverageStrength;GoodStrength;ExcellentStrength" StrengthIndicatorType="Text" runat="server" Enabled="True" TargetControlID="txtPassword" MinimumSymbolCharacters="1" MinimumUpperCaseCharacters="1" PreferredPasswordLength="8">
                                        </cc:PasswordStrength>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Password required" ControlToValidate="txtPassword" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:CheckBox ID="chkReset" runat="server" CssClass="chckbox" Text="Reset" TabIndex="7" Visible="false" OnClick="checkClick();" />
                                    </li>
                                    <li>
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="User Group"></asp:Label>
                                        <asp:DropDownList ID="ddlUserGroup" runat="server" CssClass="dropdownMandatory" TabIndex="6"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select user group" ControlToValidate="ddlUserGroup" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="RequiredFieldValidator4_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                      <li>
                                        <asp:Label ID="lblstore" runat="server" CssClass="label" Text="Store"></asp:Label>
                                        <asp:DropDownList ID="ddlStore" runat="server" CssClass="dropdownMandatory" TabIndex="6"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="RequiredFieldValidator5_ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                     <%--    <li>
                                     
                                          <asp:Label ID="lblstore" runat="server" CssClass="label" Text="Store"></asp:Label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStore" TabIndex="1">
                                        </asp:DropDownList>
                                       
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>

                                    </li>--%>
                                    <li class="btnMargin">
                                        <asp:CheckBox ID="chkActive" runat="server" CssClass="chckbox" Text="Active" TabIndex="7" />
                                    </li>
                                 
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" TabIndex="9" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClick="btnClear_Click" OnClientClick="ClearAdd();" TabIndex="10" />
                                        <asp:HiddenField ID="hfMasterId" Value="0" runat="server" />
                                        <asp:HiddenField ID="hfUserId" runat="server" />
                                        <asp:HiddenField ID="hfSEC01" runat="server" />
                                        <asp:HiddenField ID="hfSEC02" runat="server" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                        <asp:HiddenField ID="hfUsername" runat="server" />
                                   
                                    </li>
                                </ul>
                            </div>
                            <div id="divImage" runat="server" style="float: right; width: 20%;">
                                <ul class="contentul">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Image ID="imgAttachment" Width="118px" CssClass="AttachImage" ImageUrl="UploadedItems/DefaultImage.jpg" Height="118px" runat="server" BorderWidth="1px" BorderColor="#09A2E6" />
                                            <asp:LinkButton ID="lbtnRemoveAttachment" runat="server" Font-Size="14px" OnClick="lbtnRemoveAttachment_Click" Style="display: none;">Remove</asp:LinkButton>
                                            <div style="width: 200%; font-size: 8px;">
                                                <cc:AsyncFileUpload ID="fupImage" runat="server" CssClass="imageUploaderField" TabIndex="8" OnUploadedComplete="fupImage_UploadedComplete" OnClientUploadComplete="UploadComplete1" />
                                                <asp:Button ID="btnHiddenImage" runat="server" Text="Image" OnClick="btnHiddenImage_Click" CausesValidation="false" Style="display: none; font-size: 8px;" />
                                                <asp:HiddenField ID="hfUserImage" runat="server" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ul>
                            </div>
                        </div>
                             </div>
                         <div class="main_bg">  
                        <div id="divView" runat="server" style="display: none;">
                           <ul class="contentul">
                                <li>
                                   
                                    <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="User Group"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchUserGroup" runat="server" CssClass="dropdown" TabIndex="13"></asp:DropDownList>
                                    </li>
                                    
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="User Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchUsername" CssClass="textbox" runat="server" TabIndex="15"></asp:TextBox>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="16" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="17" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvUsers" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvUsers_PageIndexChanging"
                                    BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="ID_Users" OnRowDeleting="gvUsers_RowDeleting" OnSelectedIndexChanging="gvUsers_SelectedIndexChanging">
                                    <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="CustomerName" HeaderText="Staff" />
                                        <asp:BoundField DataField="Username" HeaderText="User name" />
                                        <asp:BoundField DataField="GroupName" HeaderText="User Group" />
                                        <asp:BoundField DataField="Active" HeaderText="Active" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageBtnDelete" runat="server" OnClick="ImageBtnDelete_Click" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                        <%--   <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>

                        
                         <asp:Button ID="btnReasonShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divReasonPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divReasonheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="Label1" runat="server" Text="Reason" CssClass="lblPopupHeader"></asp:Label>
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
