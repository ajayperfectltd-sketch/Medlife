<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DatabaseBackup.aspx.cs" Inherits="DatabaseBackup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <%--<link href="css/POS.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        function ClearAdd() {
            $("#txtBackupLocation").val('');
            $("#txtBackupLocation").focus();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                          <div class="search" style="width:670px; " >Database Backup</div>
                     
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" Visible="false" runat="server" />
                        </div>
                        <div class="add" style="display: none;">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" Visible="false" runat="server" />
                        </div>
                    </div>
                  <%--  <div class="user_bg">
                        <div class="user1">Database Backup</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server">
                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="BackUp Location"></asp:Label>
                                    <asp:TextBox ID="txtBackupLocation" CssClass="textboxLongMandatory" runat="server" MaxLength="1000"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="BackUp location required" ControlToValidate="txtBackupLocation" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>                                 
                                </li>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" />                                  
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                </li>
                            </ul>
                        </div>
                        <div id="divLog" runat="server">
                            <ul class="contentul">
                                <li>
                                    <asp:GridView ID="gvLog" runat="server" CssClass="grid"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" OnRowCommand="gvLog_RowCommand">
                                        <AlternatingRowStyle BackColor="#CCE0EB" />
                                        <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SLNO" />
                                            <asp:BoundField DataField="Date" HeaderText="Date" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                            <asp:TemplateField ItemStyle-Width="33px" Visible="false">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDownload" CommandName="download" ImageUrl="~/images/1418395298_143830.ico" ToolTip="download" Width="30px" Height="25px" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                        <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                    <div class="Pager">
                                        <asp:Repeater ID="rptPager" runat="server">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server"  />
                        <asp:HiddenField ID="hfFilename" runat="server" />
                    </div>
                </div>
                <asp:Button ID="btnShowPopup" runat="server" Text="Button" Style="display: none;" />
                <cc:ModalPopupExtender ID="MpeDownloadLocation" runat="server" PopupControlID="DivDownloadLocation" BackgroundCssClass="modalBackground" TargetControlID="btnShowPopup" CancelControlID="btnCancelDownload" Drag="True" Enabled="True"></cc:ModalPopupExtender>
                <div id="DivDownloadLocation" class="modalpopup" style="background-color: #0C3A5E; display: none; height: auto; width: 530px;">
                    <div style="width: 100%;">
                        <ul>
                            <li style="text-align: center;">
                                <asp:Label ID="Label24" runat="server" CssClass="label" Text="Download Location" ForeColor="#91C5F0"></asp:Label>
                            </li>
                        </ul>
                    </div>
                    <div style="width: 100%; float: left;">
                        <ul style="width: 100%; margin-left: 50px;">
                            <li>
                                <asp:TextBox ID="txtDownloadLocation" CssClass="textboxLongMandatory" runat="server" MaxLength="1000"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Location required" ControlToValidate="txtDownloadLocation" Display="None" SetFocusOnError="True" ValidationGroup="download"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <li>
                                <asp:Button ID="btnSubmitDownload" runat="server" CssClass="button" BackColor="#91C5F0" ValidationGroup="download" ForeColor="Black" Text="Submit" OnClick="btnSubmitDownload_Click" />
                                <asp:Button ID="btnCancelDownload" runat="server" CssClass="button" BackColor="#91C5F0" ForeColor="Black" Text="Cancel" />
                            </li>
                        </ul>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
