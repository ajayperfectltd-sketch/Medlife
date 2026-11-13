<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategorySecond.aspx.cs" Inherits="CategorySecond" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
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
            $("#<%#txtName.ClientID%>").val(''); 
            $("#<%#txtShortName.ClientID%>").val(''); 
            $("#<%#hfCategorySecondId.ClientID%>").val('0');
            $("#<%#ddlCategory.ClientID%>").val('0');
            $("#<%#txtShortName.ClientID%>").focus();
        }
        function ClearSearch() {
           
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").focus();
         }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtShortName.ClientID%>").focus();
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
    <form id="form1" runat="server"  autocomplete="off">
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <div class="btn_bg5"> 
                       <div class="heading2">
                    <div class="user">Sub Category</div>
                       <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                        <uc2:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle"  OnClick="ImageButton1_Click"   runat="server" TabIndex="7" ToolTip="Add" OnClientClick="ShowAddDiv(); ClearAdd();  releasebtn();" />
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle" TabIndex="6" runat="server"      OnClick="imgbtnView_Click"  ToolTip="View" OnClientClick=" ClearSearch();"  />
                        </div>
                           </asp:Panel>
                         
                    </div>
                
                    <div class="clear"></div>
                      <div class="group_bg2">
                            <div class="main_bg">  
                    <div id="divAdd" runat="server" style="display: none;">
                        <ul class="contentul">
                                  <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                    <asp:TextBox ID="txtShortName" CssClass="textboxMandatory" runat="server" MaxLength="10" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Short name required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                            <li>
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                <asp:TextBox ID="txtName" CssClass="textboxMandatory" runat="server" MaxLength="75" ToolTip="Name" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <li>
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Category"></asp:Label>
                                <asp:DropDownList ID="ddlCategory" runat="server" ToolTip="Category" CssClass="dropdownMandatory" TabIndex="3"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqvalCategory" runat="server" ErrorMessage="Category required" ControlToValidate="ddlCategory" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalCategory">
                                </cc:ValidatorCalloutExtender>
                             </li>     
                            <li class="btnMargin">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ToolTip="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="4" OnClientClick="disablebutn();" />
                                <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" ToolTip="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="5" />
                                <asp:HiddenField ID="hfCategorySecondId" runat="server" />
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
                           <%-- <li>
                                 <li>
                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="ShortName"></asp:Label>
                                <asp:TextBox ID="txtSearchShortName" CssClass="textbox" runat="server"></asp:TextBox>
                            </li>--%>
                             <li>
                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                <asp:TextBox ID="txtSearchName" CssClass="textbox" ToolTip="Name" MaxLength="75" TabIndex="8"  runat="server"></asp:TextBox>
                            </li>
                            <li class="btnMargin">
                                 <asp:Button ID="btnSearch" runat="server" CssClass="button" ToolTip="Search" Text="Search" OnClick="btnSearch_Click" TabIndex="9"  />
                                <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" ToolTip="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="10" />
                            </li>
                            </li>
                        </ul>           
                        <ul class="contentul">
                            <asp:GridView ID="gvCategorySecond" ToolTip="Sub Category" runat="server" CssClass="grid"
                                EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                BackColor="White"  BorderStyle="Solid" Width="825px"  BorderWidth="1px"  CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10"  AllowPaging="true"  OnPageIndexChanging="gvCategorySecond_PageIndexChanging" DataKeyNames="ID_SubCategory" OnRowDeleting="gvCategorySecond_RowDeleting" OnSelectedIndexChanging="gvCategorySecond_SelectedIndexChanging">
                                <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle"  />
                                <Columns>
                                    <asp:BoundField DataField="SlNo" HeaderText="SlNo" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"  />
                                   
                                    <asp:TemplateField HeaderText="Sub Category ">
                                    <ItemTemplate>
                                        <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                            <%# Eval("SubCategoryName") %>
                                        </div>
                                    </ItemTemplate>
                                  </asp:TemplateField>  
                                     <asp:TemplateField HeaderText="Category ">
                                    <ItemTemplate>
                                        <div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                            <%# Eval("Category") %>
                                        </div>
                                    </ItemTemplate>
                                  </asp:TemplateField>                                                            
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server"  OnClick="ImageButton2_Click"  CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                            <div class="Pager" style ="text-align:center;">
                                <asp:Repeater ID="rptPager" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' OnClick="Page_Changed" Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ul>
                    </div>
                                </div>
                  <%--  <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="A" />--%>
                    <%--<asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                          </div>
                         <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Reason" ></asp:Label>
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
