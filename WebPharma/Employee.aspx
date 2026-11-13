<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Employee.aspx.cs" Inherits="Employee" %>

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

        function ClearAdd()
        {
            submit = 0;
            $("#<%#txtEmployeeCode.ClientID%>").val('');
            $("#<%#txtName.ClientID%>").val('');
            $("#<%#txtAddress.ClientID%>").val('');
            $("#<%#txtPhone.ClientID%>").val('');
            $("#<%#txtEmail.ClientID%>").val('');
            $("#<%#txtInactiveRemarks.ClientID%>").val('');
            $("#<%#hfEmployeeId.ClientID%>").val('0');
            $('#<%#ChkSalesMan.ClientID%>').prop('checked', true);
            $('input:checkbox').removeAttr('checked');
            $('#<%#chkActive.ClientID%>').prop('checked', true);
            var toDate = new Date();
            $("#txtDob").val(toDate.format("dd/MM/yyyy"));
            $("#txtCurrentDate").val(toDate.format("dd/MM/yyyy"));
            ClearSearch();
            $("#<%#txtEmployeeCode.ClientID%>").focus();
        }
        function ClearSearch() {
            $("#<%#txtSearchEmployeeCode.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchEmployeeCode.ClientID%>").focus();
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtEmployeeCode.ClientID%>").focus();
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
    <form id="form1" runat="server"  autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                     <div class="btn_bg5"> 
                          <div class="heading2">
                                    <div class="user">Employee</div>
                               <asp:Panel ID="divMenuButtons" runat="server">
                                     <div style="float: right; margin-top: -10px">
                                         <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" Width="37" Height="20" onmouseout="this.src='../../images/ADD.png'"   onmouseover="this.src='../../images/ADDOver.png'"  TabIndex="13" runat="server" OnClientClick="ShowAddDiv(); EnterKeyFilter();ClearAdd(); return false;  releasebtn();" />
                                         <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="20" TabIndex="12" onmouseout="this.src='../../images/view.png'"   onmouseover="this.src='../../images/viewOver.png'" runat="server" OnClick="imgbtnView_Click" />
                                            </div>
                                   </asp:Panel>
                            
                        </div>
                   
                    <div class="clear"></div>
                    <div class="group_bg2">
                          <div class="main_bg">  
                        <div id="divAdd" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Employee Code"></asp:Label>
                                    <asp:TextBox ID="txtEmployeeCode" CssClass="textboxMandatory" runat="server" MaxLength="10" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Short name required" ControlToValidate="txtEmployeeCode" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtName" CssClass="textboxLongMandatory" runat="server" MaxLength="250" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="Address"></asp:Label>
                                    <asp:TextBox ID="txtAddress" CssClass="multiline" TextMode="MultiLine" TabIndex="3" runat="server"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label11" runat="server" CssClass="label" Text="DOB"></asp:Label>
                                    <asp:TextBox ID="txtDob" CssClass="textbox" runat="server" TabIndex="4"></asp:TextBox>
                                    <asp:CompareValidator ID="cmpCurrentDate" runat="server" ErrorMessage="DOB cannot be greater than current date" ControlToCompare="txtCurrentDate" ControlToValidate="txtDob" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="cmpCurrentDate">
                                    </cc:ValidatorCalloutExtender>
                                    <cc:CalendarExtender ID="txtDob_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDob">
                                    </cc:CalendarExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label7" runat="server" CssClass="label" Text="Phone"></asp:Label>
                                    <asp:TextBox ID="txtPhone" CssClass="textbox" runat="server" MaxLength="20" TabIndex="5"></asp:TextBox>
                                    <cc:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtPhone" ValidChars="+, -">
                                    </cc:FilteredTextBoxExtender>
                                </li>
                                <li class="btnMargin">
                                    <asp:CheckBox ID="ChkSalesMan" runat="server" CssClass="chckbox" Text="SalesMan" TabIndex="6" />
                                </li>
                                <li>
                                    <asp:Label ID="Label9" runat="server" CssClass="label" Text="Email"></asp:Label>
                                    <asp:TextBox ID="txtEmail" CssClass="textbox" runat="server" MaxLength="100" TabIndex="7" ></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid email format" ControlToValidate="txtEmail" Display="None" ValidationGroup="submit" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    <cc:ValidatorCalloutExtender ID="RegularExpressionValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RegularExpressionValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <%--  <li>
                                <asp:Label ID="Label17" runat="server" CssClass="label" Text="SalesMan"></asp:Label>
                                <asp:DropDownList ID="ddlSalesMan" runat="server" CssClass="dropdown"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select a SalesMan" ControlToValidate="ddlSalesMan" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                </cc:ValidatorCalloutExtender>
                            </li>--%>

                                <li class="btnMargin">
                                    <asp:CheckBox ID="chkActive" runat="server" CssClass="chckbox" Text="Active"  TabIndex="8"/>
                                </li>
                                <li id="liInactiveRemarks" runat="server" style="display: none;">
                                    <asp:Label ID="Label14" runat="server" CssClass="label" Text="Inactive Remarks" ></asp:Label>
                                    <asp:TextBox ID="txtInactiveRemarks" CssClass="multiline" TextMode="MultiLine" TabIndex="9" runat="server"></asp:TextBox>
                                </li>
                                <%-- <li>
                                <asp:Label ID="Label17" runat="server" CssClass="label" Text="Default Store"></asp:Label>
                                <asp:DropDownList ID="ddlDefaultStore" runat="server" CssClass="dropdown"   ></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select a Store" ControlToValidate="ddlDefaultStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                </cc:ValidatorCalloutExtender>
                            </li>
                             <li>
                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Store Access"></asp:Label>
                                <asp:CheckBoxList ID="chklistStores" runat="server" CssClass="chckbox"  ToolTip="Store Access" onkeydown = "return (event.keyCode!=13);"
                                     RepeatDirection="Horizontal" RepeatColumns="2" CellSpacing="2" CellPadding="1" Width="450px">
                                </asp:CheckBoxList>

                            </li>--%>

                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" TabIndex="10" OnClick="btnSubmit_Click"  OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="11" />
                                    <asp:HiddenField ID="hfEmployeeId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                </li>
                            </ul>
                        </div>
                              </div>
                        <div id="divPassDate" style="display: none">
                            <asp:TextBox ID="txtCurrentDate" runat="server" CssClass="textbox" TabIndex="0"></asp:TextBox>
                        </div>
                          <div class="main_bg">  
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Employee Code"></asp:Label>
                                        <asp:TextBox ID="txtSearchEmployeeCode" CssClass="textbox" TabIndex="14" runat="server"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" TabIndex="15"></asp:TextBox>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click"  TabIndex="16"/>
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="17" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvEmployee" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_Employee" OnRowDeleting="gvEmployee_RowDeleting" OnSelectedIndexChanging="gvEmployee_SelectedIndexChanging">
                                    <AlternatingRowStyle BackColor="#E5EFE4" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Address" HeaderText="Address" />
                                        <%-- <asp:BoundField DataField="SalesMan" HeaderText="SalesMan" />
                                    <asp:BoundField DataField="Active" HeaderText="Active" />--%>
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
                                </div>
                     <%--   <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />--%>
                     <%--   <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
