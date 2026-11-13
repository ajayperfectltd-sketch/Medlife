<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RackArrangement.aspx.cs" Inherits="RackArrangement" %>

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
        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

        }

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
        function btnSearch_Click() {
            if (e.keyCode == 13)
                $("#btnSearch").Click();
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

            $("#<%#txtitem.ClientID%>").val('');
            $("#<%#txtQuantity.ClientID%>").val('');
            $("#<%#hfItem.ClientID%>").val('0');
            $("#<%#hfFK_Items.ClientID%>").val('0');
            $("#ddlRack").val('0');
           
            document.getElementById("ddlRack").focus();
            $("#ddlRack").focus();
            ClearSearch();
        }
        function Clear() {
           
               $("#<%#hfGroupID.ClientID%>").val('0');
            ClearAdd();
            ClearSearch();
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            document.getElementById("ddlRack").focus();
            $("#ddlRack").focus();

        }
        function ClearSearch() {

            $("#ddlSearchRack").val('0');
            $("#txtSearchItem").val('');
            $("#hfFK_Items").val('0');
            $("#ddlSearchRack").focus();
        }
        function popUpStockSelect(type) {

            try {
                ///alert('a');
                var ItemName = '';
                $("#hfType").val('');
                if (type == 1) {
                    ItemName = $("#txtitem").val();
                }
                else {
                    ItemName = $("#txtSearchItem").val();
                    $("#hfType").val('2');
                }
                if (ItemName.length > 0) {
                    url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName;
                    popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                    ",statusbar=no,menubar=no,resizable=0,width=1000," +
                    "height=600,left = 200,top = 100");
                    popUpObj.focus();

                }

            }
            catch (e) {
                alert(e);
            }
          
        }
        function popUpStockSelectClick(type) {

            try {
             

                var ItemName = '';
                $("#hfType").val('');
                if (type == 1) {
                    ItemName = $("#txtitem").val();
                }
                else {
                    ItemName = $("#txtSearchItem").val();
                    $("#hfType").val('2');
                }
                url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName;
                popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=yes,location=no" +
                ",statusbar=no,menubar=no,resizable=0,width=1000," +
                "height=600,left = 200,top = 100");
                popUpObj.focus();





            return false;
            }
            catch (e) {
                alert(e);
            }

        }


    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        <div class="search" style="width: 670px;">Rack Arrangement</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" TabIndex="11" runat="server" OnClientClick="ShowAddDiv(); return false;  releasebtn();Clear()" />
                        </div>

                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" TabIndex="10" Height="40" runat="server" OnClick="imgbtnView_Click" OnClientClick="Clear()" />
                        </div>
                    </div>
                    <%--  <div class="user_bg">
                        <div class="user1">Rack Arrangement</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="display: none;">
                            <ul class="contentul">


                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Rack Name"></asp:Label>


                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlRack" TabIndex="1" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Rack name required" ControlToValidate="ddlRack" Display="None"
                                        SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>


                                </li>
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item"></asp:Label>
                                    <asp:TextBox ID="txtitem" CssClass="textboxMandatory" runat="server" TabIndex="2" onblur="return popUpStockSelect(1);"></asp:TextBox>

                                    <asp:ImageButton ID="imgPopUp" runat="server" TabIndex="3" OnClientClick="return popUpStockSelectClick(1);" ImageUrl="~/images/serach_icon.png" Style="padding-left: 2px; margin-top: 0px; width: 30px; position: absolute; height: 20px;" />

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Item required" ControlToValidate="txtitem" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="Quantity"></asp:Label>
                                    <asp:TextBox ID="txtQuantity" TabIndex="4" CssClass="textboxMandatory" runat="server" Style="text-align:right"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="999999999999" SetFocusOnError="true" ValidationGroup="add" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                    </cc:ValidatorCalloutExtender>


                                </li>

                                <li class="btnMargin">

                                    <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Add" TabIndex="5" ValidationGroup="add" OnClick="btnAdd_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnAddClear" runat="server" TabIndex="6" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" />

                                    <asp:HiddenField ID="hfFK_Items" Value="0" runat="server" />
                                    <asp:HiddenField ID="hfItem" runat="server" />
                                    <asp:HiddenField ID="hfGroupID" Value="0" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                     <asp:HiddenField ID="hfType" runat="server" />
                                </li>

                            </ul>
                            <div class="divSeparation"></div>
                            <ul class="contentul">
                                <li class="clear"></li>


                                <asp:GridView ID="gvRackItem" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100"
                                    OnRowDeleting="gvRackItem_RowDeleting">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Item" HeaderText="Item" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" />

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

                                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="8" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="9" OnClick="btnClear_Click" OnClientClick="Clear(); " />
                            </ul>
                        </div>
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>

                                    <li>
                                        <asp:Label ID="Label16" runat="server" CssClass="label" Text="Rack Name"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchRack" runat="server" CssClass="dropdownMandatory" AutoPostBack="true" TabIndex="12"></asp:DropDownList>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Item"></asp:Label>
                                          <asp:TextBox ID="txtSearchItem" CssClass="textboxMandatory" runat="server" TabIndex="13" onblur="return popUpStockSelect(2);"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton3" runat="server" TabIndex="3" OnClientClick="return popUpStockSelectClick(2);" ImageUrl="~/images/serach_icon.png"
                                         Style="padding-left: 2px; margin-top: 0px; width: 30px; position: absolute; height: 20px;" />

                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" OnClientClick="return btnSearch_Click();" TabIndex="14" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch();" OnClick="btnSearch_Click" TabIndex="15" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvRackArrangement" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100"
                                    DataKeyNames="FK_Rack,GroupID" OnRowDeleting="gvRackArrangement_RowDeleting" OnSelectedIndexChanging="gvRackArrangement_SelectedIndexChanging">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="RackName" HeaderText="Rack Name" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" />

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
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
