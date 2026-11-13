<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bin.aspx.cs" Inherits="Bin" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc2" TagName="MedLifePageHelp" %>


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
                
                $("#<%#txtBin.ClientID%>").val('');
                $("#<%#txtShortName.ClientID%>").val('');
                $("#<%#txtShortName.ClientID%>").focus();
                $("#<%#hfBinID.ClientID%>").val('0');
                $("#<%#ddlRack.ClientID%>").val('0');


            }
            function ClearSearch() {

                $("#<%#txtSearchShortName.ClientID%>").val('');
                $("#<%#txtSearchName.ClientID%>").val('');

        }

            function ShowAddDiv() {

                $("#<%#divView.ClientID%>").hide();
                $("#<%#divAdd.ClientID%>").show();
                $("#<%#txtBin.ClientID%>").focus();
            }
            function pageLoad() {

                var ctrContainer = "form";
                setKeynavigation(ctrContainer);

            }
        
            function PopupOnBin() {
                try {

                    //$("#ContentPlaceHolder1_divPopupOnstores").css('display', 'block');
                    $("#divPopupBin").css('display', 'block');
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
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                     <div class="btn_bg5"> 
                       <div class="heading2">
                             <div class="user">Bin </div>
               <asp:Panel ID="divMenuButtons" runat="server">
                        <div style="float: right; margin-top: -10px">
                            <uc2:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add.png" CssClass="imagebuttonStyle"   OnClick="ImageButton1_Click"  TabIndex="7"  runat="server" OnClientClick="ShowAddDiv(); ClearAdd();   releasebtn();" />
                    
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle"      TabIndex="6" runat="server"  OnClick="imgbtnView_Click" />
                        </div>
                   </asp:Panel>
                  
                    <div class="clear"></div>
                     <div class="group_bg2">
                          <div class="main_bg">  
                    <div id="divAdd" runat="server" style="display: none;">         
                        <ul class="contentul">
                           
                              <li>
                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                <asp:TextBox ID="txtShortName" CssClass="textboxMandatory" runat="server" MaxLength="5" TabIndex="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="ShortName required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <li>
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Bin Name"></asp:Label>
                                <asp:TextBox ID="txtBin" CssClass="textboxMandatory" runat="server" MaxLength="25" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtBin" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <li>
                                 <asp:Label ID="Label19" runat="server" CssClass="label" Text="Rack"></asp:Label>
                                 <asp:DropDownList ID="ddlRack" runat="server" CssClass="dropdownMandatory" TabIndex="3"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="revUnit" runat="server" ErrorMessage="Select Rack" ControlToValidate="ddlRack" Display="None" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="revUnit">
                                    </cc:ValidatorCalloutExtender>
                              </li>
                          
                       
                           
                            <li class="btnMargin">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit"  OnClick="btnSubmit_Click" ValidationGroup="submit"  TabIndex="4" OnClientClick="disablebutn();" />
                                <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="5" />
                                <asp:HiddenField ID="hfBinID" runat="server" />
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
                        <div class="divL">
                        <ul class="contentul">
                            <li>
                                 <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchShortName" MaxLength="5" CssClass="textbox" runat="server" TabIndex="8"></asp:TextBox>
                                </li>
                               
                                <li>
                                    <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchName" MaxLength="25" CssClass="textbox" runat="server" TabIndex="9"></asp:TextBox>
                                </li>
                               
                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="10" Text="Search"   OnClick="btnSearch_Click"/>
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" TabIndex="11" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                     
                                </li>
                            </li>
                        </ul>
                            </div>
                        <ul class="contentul">
                            <asp:GridView ID="grdBin" runat="server" CssClass="grid"
                                EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                BackColor="White" Width="75%"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" OnPageIndexChanging="grdBin_PageIndexChanging"  DataKeyNames="ID_Bin" OnRowDeleting="grdBin_RowDeleting"  OnSelectedIndexChanging="grdBin_SelectedIndexChanging" AllowPaging="True">
                                <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                     <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                    <asp:BoundField DataField="BinName" HeaderText="Name" />
                                    <asp:BoundField DataField="RackName" HeaderText="Rack" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">            
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" OnClick="ImageButton2_Click" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                                        <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'   OnClick="lnkPage_Click" Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ul>
                    </div>
                              </div>
                  
                          </div>
                </div>
                           <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Reason" ></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupBin" runat="server" class="divPopupOnstores" style="display: none;">
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
          
    <div>
    
    </div>
    </form>
</body>
</html>
