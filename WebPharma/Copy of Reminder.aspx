<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Copy of Reminder.aspx.cs" Inherits="Reminder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
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
            var dt = new Date();
            $("#txtStDate").val(dt.format('dd/MM/yyyy'));
            $("#txtEndDate").val(dt.format('dd/MM/yyyy'));
            $("#txtTopic").val('');
            $("#txtReminder").val('');
            $("#ddlType").val('1');
            $("#hdfReminder").val('');
            $("#chkActive").prop("checked", true);
            $("#txtTopic").focus();

        }
        function ClearSearch() {
            var dtM = new Date().getMonth();
            var dtY = new Date().getFullYear();

            var dt = new Date(dtY, dtM, 1, 0, 0, 0, 0);
            $("#txtFromDate").val(dt.format('dd/MM/yyyy'));
            $("#txtToDate").val(new Date().format('dd/MM/yyyy'));
            $("#txtSearchTopic").val('');
            $("#ddlTypeSearch").val('0');
            $("#txtSearchTopic").focus();

        }
        function ShowAddDiv() {
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#txtTopic").focus();
        }
        function ClientValidate(source, arguments) {
            try {
                var date = document.getElementById("txtStDate").value;
                var arr = date.split('/');
                var start = new Date(arr[2], arr[1], arr[0]);
                var arr2 = arguments.Value.split('/');

                var endDate = new Date(arr2[2], arr2[1], arr2[0]);

                if (start > endDate) {
                    arguments.IsValid = false;
                }
                else {
                    arguments.IsValid = true;
                }

            }
            catch (e) {
                alert(e);
            }

        }
        function TypeBlurTrigger() {


            document.getElementById("btnTypeBlurTrigger").click();

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
    <title>::Reminder::</title>
    <style type="text/css">
        #divUser {
            width: 937px;
        }
    </style>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server"  autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                  <div class="btn_bg5"> 
            <div class="heading2">
                    <div class="user">Reminder</div>
                 <asp:Panel ID="divMenuButtons" runat="server">
                        <div style="float: right; margin-top: -10px">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle" onmouseout="this.src='../../images/ADD.png'"   onmouseover="this.src='../../images/ADDOver.png'" runat="server" OnClientClick="ShowAddDiv(); ClearAdd(); return false;  releasebtn();" TabIndex="6" />
                        
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle" onmouseout="this.src='../../images/view.png'"   onmouseover="this.src='../../images/viewOver.png'" TabIndex="5" runat="server" OnClick="imgbtnView_Click" />
                        </div>
                       </asp:Panel>
                    </div>
                       
                  
                <%--    <div class="user_bg">
                        <div class="user1">Reminder</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                         <div class="main_bg">  
                        <div id="divAdd" runat="server">
                            <%--style="display:none;"--%>
                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Topic"></asp:Label>
                                    <asp:TextBox ID="txtTopic" CssClass="textboxMandatory" runat="server" MaxLength="30" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Topic required" ControlToValidate="txtTopic" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Reminder"></asp:Label>
                                    <asp:TextBox ID="txtReminder" CssClass="textboxLongMandatory" runat="server" MaxLength="250" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Reminder required" ControlToValidate="txtReminder" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>

                                </li>
                                <li>
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="From Date"></asp:Label>
                                    <asp:TextBox ID="txtStDate" CssClass="textbox" runat="server" TabIndex="3"></asp:TextBox>
                                    <cc:CalendarExtender ID="txtStDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtStDate">
                                    </cc:CalendarExtender>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than To date" ControlToCompare="txtEndDate" ControlToValidate="txtStDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                    </cc:ValidatorCalloutExtender>

                                    <asp:CustomValidator ID="cvDatecheck" runat="server"
                                        ClientValidationFunction="ClientValidate" ControlToValidate="txtEndDate" Display="None"
                                        CssClass="validator"
                                        ErrorMessage="To date must be greater than from date" SetFocusOnError="True" ValidationGroup="submit"></asp:CustomValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="cvDatecheck">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="To Date"></asp:Label>
                                    <asp:TextBox ID="txtEndDate" CssClass="textbox" runat="server" TabIndex="4"></asp:TextBox>
                                    <cc:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtEndDate">
                                    </cc:CalendarExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label6" runat="server" CssClass="label" Text="Type"></asp:Label>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="dropdown" TabIndex="5" onchange="TypeBlurTrigger();">

                                        <asp:ListItem Selected="True" Value="1">Private</asp:ListItem>
                                        <asp:ListItem Value="2">Public</asp:ListItem>
                                        <asp:ListItem Value="3">UserWise</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li class="btnMargin">
                                    <asp:CheckBox ID="chkActive" runat="server" CssClass="chckbox" Text="Active" TabIndex="6" />
                                </li>
                                <li class="clear"></li>
                                <li class="clear"></li>
                                <li class="clear"></li>
                                <li id="liUserGroup" runat="server">
                                    <label class="label">User Group</label>
                                    <asp:DropDownList ID="ddlUserGroup" runat="server" TabIndex="7"  CssClass="dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlUserGroup_SelectedIndexChanged"></asp:DropDownList>
                                </li>
                                <li>


                                    <%-- User--%>
                                    <div id="divUser" style="height: auto; overflow-y: auto; max-height: 250px;">
                                        <asp:DataList ID="dlUser" runat="server" RepeatColumns="3" TabIndex="8" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="5" Style="padding: 5px 0px 2px 0px; margin: 0px 0px 0px 0px;" Width="932px">
                                            <ItemStyle ForeColor="Black" />
                                            <ItemTemplate>

                                                <asp:HiddenField ID="hfID_User" Value='<%#Eval("ID_Users") %>' runat="server" />
                                                <asp:CheckBox ID="chkUser" Height="35px" runat="server" Checked='<%#Convert.ToBoolean(Eval("isReminder"))%>' CssClass="chckboxNew" Style="margin-right: 5px; margin-left: 20px; margin-right: 20px; font-size: 16px; padding: 10px 0px 0px 40px; font-weight: bold;"
                                                    Text='<%#" "+Eval("Username")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="33.3%" />

                                        </asp:DataList>
                                    </div>
                                </li>


                                <li class="clear"></li>

                                <li class="clear"></li>
                                <li class="clear"></li>
                                <li class="btnMargin">
                                    <asp:HiddenField ID="hdfReminder" runat="server" />
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" TabIndex="9" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click"  OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" TabIndex="10" Text="Clear" OnClientClick="ClearAdd();" OnClick="btnClear_Click" />
                                    <asp:Button Text="Hidded" runat="server" ID="btnTypeBlurTrigger" Style="display: none;" CausesValidation="false" OnClick="btnTypeBlurTrigger_Click" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />

                                </li>
                                <li class="clear"></li>
                                <li class="clear"></li>
                            </ul>
                        </div>
                             </div>
                         <div class="main_bg">
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Topic"></asp:Label>
                                        <asp:TextBox ID="txtSearchTopic" CssClass="textbox" MaxLength="50" TabIndex="13" runat="server"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label class="label">From Date</label>
                                        <asp:TextBox ID="txtFromDate" CssClass="textbox" runat="server" TabIndex="14"></asp:TextBox>
                                        <cc:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                        </cc:CalendarExtender>

                                    </li>
                                    <li>
                                        <label class="label">To Date</label>
                                        <asp:TextBox ID="txtToDate" CssClass="textbox" runat="server" TabIndex="15"></asp:TextBox>
                                        <cc:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                        </cc:CalendarExtender>

                                    </li>
                                    <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Type"></asp:Label>
                                        <asp:DropDownList ID="ddlTypeSearch" runat="server" CssClass="dropdown" TabIndex="16">
                                            <asp:ListItem Value="0">select</asp:ListItem>
                                            <asp:ListItem Value="1">Private</asp:ListItem>
                                            <asp:ListItem Value="2">Public</asp:ListItem>
                                            <asp:ListItem Value="3">UserWise</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="17" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="18" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvReminder" runat="server" CssClass="grid" DataKeyNames="ID_Reminder"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" OnRowDeleting="gvReminder_RowDeleting" OnSelectedIndexChanging="gvReminder_SelectedIndexChanging">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Right"/>
                                        <asp:BoundField DataField="Topic" HeaderText="Topic" />

                                        <asp:BoundField DataField="frmdate" HeaderText="From Date" />
                                        <asp:BoundField DataField="todates" HeaderText="To Date" />
                                        <asp:BoundField DataField="Type1" HeaderText="Type" />
                                        <asp:BoundField DataField="Active" HeaderText="Active" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
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
                                    <HeaderStyle BackColor="#07928f" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                           
                            </ul>
                        </div>
                              </div>
                      <%--  <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
