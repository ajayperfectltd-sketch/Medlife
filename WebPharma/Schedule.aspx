


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Schedule.aspx.cs" Inherits="Schedule" %>

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
        function btnScheduleProcessTrigger() {
            $('#btnScheduleProcessTrigger').click();
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


            $("#<%#hfScheduleId.ClientID%>").val('0');

            $('input:checkbox').removeAttr('checked');
            $('#<%#rdProcessing.ClientID%>').prop('checked', true);
            var toDate = new Date();
            $("#txtScheduleFrom").val(toDate.format("dd/MM/yyyy"));
            $("#txtScheduleUpto").val(toDate.format("dd/MM/yyyy"));

            ClearSearch();

        }
        function ClearSearch() {
            var toDate = new Date();
            $("#txtSearchFromScheduleUpto").val(toDate.format("dd/MM/yyyy"));
            $("#txtSearchToScheduleUpto").val(toDate.format("dd/MM/yyyy"));
            $("#<%#txtSearchEmployeeCode.ClientID%>").focus();
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();

        }
        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

        }

    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
        <div>
            <div class="icon_bg">
                <div class="search" style="width: 670px;">Schedule</div>
                <div class="add">Add</div>
                <div class="add_icon">
                    <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" TabIndex="13" runat="server" OnClientClick="ShowAddDiv(); return false;  releasebtn();" />
                </div>

                <div class="search">View</div>
                <div class="search_icon">
                    <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" TabIndex="12" runat="server" OnClick="imgbtnView_Click" />
                </div>

            </div>
            <%-- <div class="user_bg">
                        <div class="user1">Employee</div>
                    </div>--%>
            <div class="clear"></div>
            <div class="group_bg2">
                <div id="divAdd" runat="server" style="display: none;">
                    <ul class="contentul">
                        <div class="divL" style="width: 270px;">


                            <li>
                                <asp:Label ID="Label186" runat="server" CssClass="label" Width="80" Text="Schedule method"></asp:Label>
                                <asp:RadioButton ID="rdProcessing" runat="server" Text="Processing" OnCheckedChanged="rdProcessing_CheckedChanged" GroupName="Schedule" AutoPostBack="true" TabIndex="1" />
                                <asp:RadioButton ID="rdPosting" runat="server" Text="Posting" OnCheckedChanged="rdPosting_CheckedChanged" AutoPostBack="true" GroupName="Schedule" TabIndex="2" />
                            </li>

                        </div>
                        <div class="divL" style="width: 200px;">

                            <li>
                                <asp:Label ID="Label1" runat="server" CssClass="label" Width="80" Text="TransDate"></asp:Label>
                                <asp:TextBox ID="txtScheduleFrom" CssClass="textbox" Width="80" runat="server" TabIndex="3"></asp:TextBox>
                                <%--   <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="DOB cannot be greater than current date" ControlToCompare="txtCurrentDate" ControlToValidate="txtDob" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="cmpCurrentDate">
                                    </cc:ValidatorCalloutExtender>--%>
                                <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtScheduleFrom">
                                </cc:CalendarExtender>
                            </li>

                        </div>
                        <div class="divL" style="width: 180px;">

                            <li>
                                <asp:Label ID="Label3" runat="server" CssClass="label" Width="80" Text="Schedule Upto"></asp:Label>
                                <asp:TextBox ID="txtScheduleUpto" CssClass="textbox" Width="80" runat="server" TabIndex="3"></asp:TextBox>
                                <%-- <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="DOB cannot be greater than current date" ControlToCompare="txtCurrentDate" ControlToValidate="txtDob" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="cmpCurrentDate">
                                    </cc:ValidatorCalloutExtender>--%>
                                <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtScheduleUpto">
                                </cc:CalendarExtender>
                            </li>

                        </div>

                        <div class="divL" style="width: 180px;">

                            <li class="btnMargin">
                                <asp:Button ID="btnProcess" runat="server" CssClass="button" Text="Process" OnClick="btnProcess_Click" />

                                <asp:HiddenField ID="hfScheduleId" runat="server" />
                                <asp:HiddenField ID="hfClient" runat="server" />
                                <asp:HiddenField ID="hfUser" runat="server" />
                                <asp:HiddenField ID="hfIsAdd" runat="server" />
                                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                <asp:HiddenField ID="hfIsDelete" runat="server" />
                            </li>

                        </div>
                        <li class="clear"></li>
                        <div class="divSeparation"></div>
                        <li class="clear"></li>
                        <div class="divL" runat="server" id="DivImportData" style="width: 380px; ">

                            <li>
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Please Select Excel File"></asp:Label>
                                <asp:FileUpload ID="fileuploadExcel" runat="server" />&nbsp;&nbsp;
                                  
                            <asp:Button ID="btnImport" CssClass="button" runat="server" Style="display:none;" Text="Import Data" OnClick="btnImport_Click" />
                                <br />
                                <asp:Label ID="lblMessage" runat="server" Visible="False" Font-Bold="True" ForeColor="#009933"></asp:Label><br />




                            </li>

                            <div style="width: 880px; overflow: auto; scrollbar-face-color: blue; height: 250px; display:none;">
                                <asp:GridView ID="grvExcelData" runat="server" CssClass="grid"
                                    BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Both"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="false">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="CusNumber" HeaderText="CusNumber" />
                                        <asp:BoundField DataField="RecoveredAmount" HeaderText="RecoveredAmount" />

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
                            </div>
                             <div style="width: 880px; overflow: auto; scrollbar-face-color: blue; height: 250px; ">
                                <asp:GridView ID="grvPostingData" runat="server" CssClass="grid"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Both"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="true">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                            </div>

                        </div>



                        <div id="DivProcessingDataGrid" runat="server" style="width: 800px; overflow: auto; scrollbar-face-color: blue; height: 430px;">
                            <asp:GridView ID="GvProcessing" CssClass="grid"
                                BackColor="White" ShowFooter="true" PageSize="100" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Both"
                                EmptyDataText="No Data Found!" AutoGenerateColumns="False" OnRowDataBound="GvProcessing_RowDataBound" OnPreRender="GvProcessing_PreRender" runat="server">

                                <AlternatingRowStyle BackColor="#CCE0EB" />
                                <Columns>
                                    <asp:BoundField DataField="CusNumber" HeaderText="CusNumber" />
                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                    <asp:BoundField DataField="ConsumerAmt" HeaderText="ConsumerAmt" />
                                    <asp:BoundField DataField="NeethiAmt" HeaderText="NeethiAmt" />
                                    <asp:BoundField DataField="LoanNumber" HeaderText="LoanNumber" />
                                    <asp:BoundField DataField="SanctionAmt" HeaderText="SanctionAmt" />
                                    <asp:BoundField DataField="RecoveryAmt" HeaderText="RecoveryAmt" />
                                    <asp:BoundField DataField="Total" HeaderText="Total"  />
                                    <asp:TemplateField Visible="false">

                                        <ItemTemplate>

                                            <asp:Label ID="lblSalary" runat="server" Text='<%# Eval("Total")%>'  />

                                        </ItemTemplate>

                                        <FooterTemplate>

                                            <asp:Label ID="Salary" runat="server" />

                                        </FooterTemplate>

                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                             
                                <FooterStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                        </div>
                        <li class="clear"></li>
                        <div class="divSeparation"></div>
                        <li class="clear"></li>
                        <li class="btnMargin">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click" TabIndex="10" />
                            <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClick="btnClear_Click" OnClientClick="ClearAdd(); " TabIndex="11" />
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                            <asp:HiddenField ID="HiddenField3" runat="server" />
                            <asp:HiddenField ID="HiddenField4" runat="server" />
                            <asp:HiddenField ID="HiddenField5" runat="server" />
                            <asp:HiddenField ID="HiddenField6" runat="server" />
                        </li>
                </div>
                <div id="divPassDate" style="display: none">
                    <asp:TextBox ID="txtCurrentDate" runat="server" CssClass="textbox" TabIndex="0"></asp:TextBox>
                </div>

                <div id="divView" runat="server" style="display: none;">
                    <ul class="contentul">
                        <li>
                            <li  style="display: none;">
                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="Employee Code"></asp:Label>
                                <asp:TextBox ID="txtSearchEmployeeCode" CssClass="textbox" TabIndex="14" runat="server"></asp:TextBox>
                            </li>
                            <li  style="display: none;">
                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" TabIndex="15"></asp:TextBox>
                            </li>
                            <li>
                                <asp:Label ID="Label4" runat="server" CssClass="label"  Text="From SheduleUpto"></asp:Label>
                                <asp:TextBox ID="txtSearchFromScheduleUpto" CssClass="textbox" runat="server" TabIndex="3"></asp:TextBox>
                                <%--   <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="DOB cannot be greater than current date" ControlToCompare="txtCurrentDate" ControlToValidate="txtDob" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="cmpCurrentDate">
                                    </cc:ValidatorCalloutExtender>--%>
                                <cc:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtSearchFromScheduleUpto">
                                </cc:CalendarExtender>
                            </li>
                            <li>
                                <asp:Label ID="Label7" runat="server" CssClass="label"  Text="To SheduleUpto"></asp:Label>
                                <asp:TextBox ID="txtSearchToScheduleUpto" CssClass="textbox"  runat="server" TabIndex="3"></asp:TextBox>
                                <%--   <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="DOB cannot be greater than current date" ControlToCompare="txtCurrentDate" ControlToValidate="txtDob" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="cmpCurrentDate">
                                    </cc:ValidatorCalloutExtender>--%>
                                <cc:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtSearchToScheduleUpto">
                                </cc:CalendarExtender>
                            </li>
                            <li class="btnMargin">
                                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="16" />
                                <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="17" />
                            </li>
                        </li>
                    </ul>
                    <ul class="contentul">
                        <asp:GridView ID="gvSchedule" runat="server" CssClass="grid"
                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_Schedule,ScheduleType " OnRowDeleting="gvSchedule_RowDeleting" OnRowCommand="gvSchedule_RowCommand" OnSelectedIndexChanging="gvSchedule_SelectedIndexChanging">
                            <AlternatingRowStyle BackColor="#CCE0EB" />
                            <Columns>
                                <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                 <asp:BoundField DataField="ScheduleMode" HeaderText="Mode" />
                                <asp:BoundField DataField="TransDate" HeaderText="TransDate" />
                                <asp:BoundField DataField="ScAmount" HeaderText="ScheduleAmount" />
                             
                                <%-- <asp:BoundField DataField="SalesMan" HeaderText="SalesMan" />
                                    <asp:BoundField DataField="Active" HeaderText="Active" />--%>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                              <%--  <asp:Button ID="btnPrint" CssClass="button" Width="75px" Text="Print" runat="server" OnClientClick="ConfirmPurchasePrint();" CommandName="Print" />--%>
                                              <asp:ImageButton ID="ImageButton343" runat="server" CommandName="Print" ToolTip="Print"  ImageUrl="~/images/1430738477_report.png" Width="20px" Height="20px" />
                                             
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
                <%--   <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />--%>
                <%--   <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                 <asp:Button ID="btnScheduleProcessTrigger" Text="text" runat="server" OnClientClick="ConfirmSchedule();" OnClick="btnScheduleProcessTrigger_Click" Style="display: none;" />
              
            </div>
        </div>
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </form>
</body>
</html>
