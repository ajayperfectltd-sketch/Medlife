<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidaySettings.aspx.cs" Inherits="HolidaySettings" EnableEventValidation="false" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Holiday Settings::</title>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
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
        function pageLoad() {
            TimeSpan();
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
            $("#<%#txtReason.ClientID%>").val('');
            $("#<%#txtDate.ClientID%>").val(new Date().format('dd/MM/yyyy'));
            $("#<%#hdfHolyDayId.ClientID%>").val('0');
            $("#<%#ddleavType.ClientID%>").val('0');
            TimeSpan();
            ClearSearch();
            $("#txtReason").focus();
            /// alert(GetInto24hrFormat());
            // var now = new Date();

            //$("#TmsShiftFrom_txtHour").val(now.getHours());
            //$("#TmsShiftFrom_txtMinute").val(now.getMinutes());
            //$("#TmsShiftFrom_txtSecond").val(now.getSeconds());
        }
        function ClearSearch() {

            $("#<%#txtSearchReason.ClientID%>").val('');
            $("#<%#ddlSearchLeaveType.ClientID%>").val('2');
            $("#<%#txtDateFrom.ClientID%>").val(new Date().format('dd/MM/yyyy'));
            $("#<%#txtToDate.ClientID%>").val(new Date().format('dd/MM/yyyy'));
            $("#lblValidate").attr("style", "display:none;");
            txtSearchReason.focus();
        }
        function BlurItem() {


        }
        function GetInto24hrFormat(hours, minutes, seconds, AMPM) {





            if (AMPM == "PM" && hours < 12) hours = hours + 12;
            if (AMPM == "AM" && hours == 12) hours = hours - 12;
            var sHours = hours.toString();
            var sMinutes = minutes.toString();
            var sSeconds = seconds.toString();
            if (hours < 10) sHours = "0" + sHours;
            if (minutes < 10) sMinutes = "0" + sMinutes;
            if (seconds < 10) sSeconds = "0" + sSeconds;
           
            return sHours + ":" + sMinutes + ":" + sSeconds;
        }

        function ClientValidate() {
            try {




                var confirm_validate = document.createElement("INPUT");
                confirm_validate.type = "hidden";
                confirm_validate.name = "confirm_validate";
                var hours = parseInt($("#TmsShiftFrom_txtHour").val());
                var minutes = parseInt($("#TmsShiftFrom_txtMinute").val());
                var seconds = parseInt($("#TmsShiftFrom_txtSecond").val());


                var AMPM = $("#TmsShiftFrom_txtAmPm").val();
                var strTime = GetInto24hrFormat(hours, minutes, seconds, AMPM);


                var hoursTo = parseInt($("#TmsShiftTo_txtHour").val());
                var minutesTo = parseInt($("#TmsShiftTo_txtMinute").val());
                var secondsTo = parseInt($("#TmsShiftTo_txtSecond").val());
                var AMPMTo = $("#TmsShiftTo_txtAmPm").val();

                var endTime = GetInto24hrFormat(hoursTo, minutesTo, secondsTo, AMPMTo);



                var startDate = new Date("1/1/1977 " + strTime).getTime();
                var endDate = new Date("1/1/1977 " + endTime).getTime();


               
                /// 60000
                if (((endDate - startDate)) >= 0) {
                    confirm_validate.value = "Yes";
                    $("#lblValidate").attr("style", "display:none;color:red;");
                }
                else {
                    confirm_validate.value = "No";
                    $("#lblValidate").attr("style", "display:block;color:red;");
                    ShowAddDiv();
                }

                document.forms[0].appendChild(confirm_validate);

            }
            catch (e) {
                alert(e);
            }

        }


        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#txtReason").focus();
        }
        function TimeSpan() {

            var Period = $('#ddleavType').find("option:selected").val();
            if (Period == "0") {
                $("#divTimeSpan").attr("style", "display:none;");
            }
            else {
                $("#divTimeSpan").attr("style", "display:block;");
            }
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
                      <div class="search" style="width:670px; " >Holiday Settings</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" TabIndex="10" CausesValidation="false" OnClientClick="ShowAddDiv(); return false;  releasebtn();" />
                        </div>

                          <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" OnClick="imgbtnView_Click" TabIndex="9" />
                        </div>
                    </div>
                  <%--  <div class="user_bg">
                        <div class="user1">Holiday Settings</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="display: none;">
                            <%-- style="display: none;"--%>
                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Reason"></asp:Label>
                                    <asp:TextBox ID="txtReason" CssClass="textboxLongMandatory" runat="server" MaxLength="330" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Reason required" ControlToValidate="txtReason" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Date"></asp:Label>
                                    <asp:TextBox ID="txtDate" CssClass="textboxMandatory" runat="server" TabIndex="2"></asp:TextBox>
                                    <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDate">
                                    </cc:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Leave Type"></asp:Label>
                                    <asp:DropDownList ID="ddleavType" runat="server" CssClass="dropdown" onchange="return TimeSpan();" TabIndex="3">
                                        <asp:ListItem Value="0">Full Day</asp:ListItem>
                                        <asp:ListItem Value="1">Required Time</asp:ListItem>
                                    </asp:DropDownList>



                                </li>
                                <div id="divTimeSpan" style="display: none;">
                                    <li>
                                        <asp:Label ID="Label22" runat="server" Text="Shift From " CssClass="label"></asp:Label>
                                        <MKB:TimeSelector ID="TmsShiftFrom" runat="server" AllowSecondEditing="True" ClientIDMode="Inherit" BorderStyle="Solid" BackColor="White" BorderColor="White" MinuteIncrement="1" TabIndex="4"></MKB:TimeSelector>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label9" runat="server" Text="Shift To " CssClass="label"></asp:Label>
                                        <MKB:TimeSelector ID="TmsShiftTo" runat="server" AllowSecondEditing="True" ClientIDMode="Inherit" BorderStyle="Solid" BackColor="White" BorderColor="White" MinuteIncrement="1" TabIndex="5"></MKB:TimeSelector>
                                    </li>


                                    <asp:HiddenField ID="hdfValid" runat="server" />

                                </div>

                                <li class="btnMargin">
                                    <asp:HiddenField ID="hdfReminder" runat="server" />
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="ClientValidate();disablebutn();" TabIndex="6" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd();return false;" TabIndex="7" />

                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                    <asp:HiddenField ID="hdfHolyDayId" runat="server" />
                                </li>

                            </ul>
                        </div>
                        <div id="divView" runat="server" style="display: none;">

                            <ul class="contentul">
                                <div class="divL">
                                    <li>


                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Reason"></asp:Label>
                                        <asp:TextBox ID="txtSearchReason" CssClass="textbox" runat="server" TabIndex="11"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="From Date"></asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox" TabIndex="13"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDateFrom">
                                        </cc:CalendarExtender>

                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="15" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="16" />
                                    </li>
                                </div>
                                <div class="divR">
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Leave Type"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchLeaveType" runat="server" CssClass="dropdown" TabIndex="12">
                                            <asp:ListItem Value="2" Selected="True">select</asp:ListItem>
                                            <asp:ListItem Value="0">Full Day</asp:ListItem>
                                            <asp:ListItem Value="1">Required Time</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="To Date"></asp:Label>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" TabIndex="14"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                        </cc:CalendarExtender>
                                    </li>
                                </div>


                            </ul>
                            <br class="clear" />
                             <br class="clear" />
                            <ul class="contentul">
                                <asp:GridView ID="gvHolidaySettings" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_HolidaySettings" OnRowDeleting="gvHolidaySettings_RowDeleting" OnSelectedIndexChanging="gvHolidaySettings_SelectedIndexChanging">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" />
                                        <asp:BoundField DataField="LeaveMode" HeaderText="Leave Type" />
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
                                            <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' OnClick="Page_Changed" Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ul>

                        </div>
                   <%--     <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                        <asp:Label ID="lblValidate" runat="server" Text="End time must be greater than start time " ForeColor="Red" Style="display: none; color: red;"></asp:Label>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/toastr.js"></script>
    </form>
</body>
</html>
