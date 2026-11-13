<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReminderView.aspx.cs" Inherits="ReminderView" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script type="text/javascript">

        function ClearSearch() {
            var dtnow = new Date();
            $("#hfDate").val(dtnow.format('dd/MM/yyyy'));
            $("#btnPageLoadFunctions").click();
        }


    </script>
    <title>::Reminder::</title>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server"  autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <div class="btn_bg5"> 
                    <div class="heading2">
                        <div class="user">Reminder</div>
                     
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                         <div class="main_bg">  
                        <div id="divView" runat="server">
                            <ul class="contentul">


                                <li>
                                    <label class="label">From Date</label>
                                    <asp:TextBox ID="txtFromDate" CssClass="textbox" runat="server"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                    </cc:ValidatorCalloutExtender>
                                    <cc:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate" TodaysDateFormat="dd/MM/yyyy">
                                    </cc:CalendarExtender>
                                </li>
                                <li>
                                    <label class="label">To Date</label>
                                    <asp:TextBox ID="txtToDate" CssClass="textbox" runat="server"></asp:TextBox>
                                    <cc:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                    </cc:CalendarExtender>
                                </li>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                </li>

                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvReminder" runat="server" CssClass="grid" DataKeyNames="ID_Reminder"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanging="gvReminder_SelectedIndexChanging" PageSize="100">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                        <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                        <asp:BoundField DataField="Topic" HeaderText="Topic">
                                         <ItemStyle Width="50%" />
                                        </asp:BoundField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            
                                            <ItemTemplate >
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="View" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkView" runat="server" ToolTip="View" Text="View" OnClick="lnkView_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Teal" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <RowStyle Height="30px" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>


                                <cc:ModalPopupExtender ID="MpeReminderShow" runat="server" BackgroundCssClass="modalBackground" PopupControlID="DivReminderShow" TargetControlID="btnmppChangeQuantityRemoveItem" CancelControlID="btnmppChangeQuantityCancel" Drag="True" Enabled="True"></cc:ModalPopupExtender>
                                <div id="DivReminderShow" class="modalpopup" style="display: none; background-color: #0C3A5E; height: 300px; width: 450px;">
                                    <div style="width: 100%;">
                                        <li style="float: right;">
                                            <asp:ImageButton ID="btnmppChangeQuantityCancel" ImageUrl="~/images/1399987015_Delete_01.png" Width="25px" Height="25px" runat="server" />
                                        </li>
                                    </div>
                                    <div style="width: 100%;">
                                        <ul>
                                            <li style="text-align: center;">
                                                <asp:Label ID="Label22" runat="server" ForeColor="WhiteSmoke" Text="Reminder" Font-Size="Large"></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="clear"></div>
                                    <div style="float:left;">
                                        <ul style="margin:20px 0px 0px 5px;">
                                            <li>
                                                <asp:Label ID="Label29" runat="server" ForeColor="WhiteSmoke" CssClass="popuplbl" Text="Topic"></asp:Label>
                                                <asp:TextBox ID="txtTopicReminder" CssClass="popuptxt" runat="server"></asp:TextBox>
                                            </li>
                                            <li>
                                                <asp:Label ID="Label1" runat="server" ForeColor="WhiteSmoke" CssClass="popuplbl" Text="Message"></asp:Label>
                                                <asp:TextBox ID="txtMessageReminder" runat="server" TextMode="MultiLine" CssClass="popuptxt" Width="265px" Height="180px" ></asp:TextBox>
                                            </li>

                                            <li style="display: none;">
                                                <asp:TextBox ID="txtMppChangeQuantityGrdItemRowIndex" runat="server" Visible="false"></asp:TextBox>
                                                <asp:Button ID="btnmppChangeQuantityRemoveItem" runat="server" CssClass="button" Text="RemoveItem" />
                                            </li>
                                        </ul>
                                    </div>
                                </div>

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
                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>
                        <asp:HiddenField ID="hfClient" runat="server" />
                        <asp:HiddenField ID="hfUser" runat="server" />
                        <asp:HiddenField ID="hfDate" runat="server" />
                        <asp:Button Text="text" ID="btnPageLoadFunctions" runat="server" Style="display: none;" OnClick="btnPageLoadFunctions_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

