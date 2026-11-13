<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RackSearch.aspx.cs" Inherits="RackSearch" %>

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
       

    </script>

</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        <div class="search" style="width: 670px;">Rack </div>
                    </div>
                    <%--  <div class="user_bg">
                        <div class="user1">Rack</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divView" runat="server">
                            <li class="clear"></li>
                            <ul class="contentul">
                                <asp:GridView ID="gvRack" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_Rack"
                                    OnSelectedIndexChanging="gvRack_SelectedIndexChanging">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />

                                        <asp:BoundField DataField="RackName" HeaderText="Rack Name" />
                                        <asp:BoundField DataField="AllotmentDate" HeaderText="Allotment Date" />
                                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" />
                                        <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="return SetName(id);" ToolTip="Select" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                                <asp:HiddenField ID="hdfValue" runat="server" Value='<%#Eval("ID_Rack") %>' />
                                                <asp:HiddenField ID="hdfRack" runat="server" Value='<%#Eval("RackName") %>' />
                                                <asp:HiddenField ID="hdfSupplier" runat="server" Value='<%#Eval("FK_Supplier") %>' />
                                                <asp:HiddenField ID="hdfIDRackAllotment" runat="server" Value='<%#Eval("ID_RackAllotment") %>' />


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
                            <li class="clear"></li>
                            <div class="divL">
                                <ul class="contentul">
                                    <li>

                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Rack Name"></asp:Label>
                                        <asp:TextBox ID="txtRackName" CssClass="textboxLongMandatory" runat="server" MaxLength="50"></asp:TextBox>


                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="9" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearchClick();return false;" TabIndex="10" />
                                    </li>
                                </ul>
                            </div>

                        </div>

                    </div>
                    <asp:HiddenField ID="hfClient" runat="server" />
                    <asp:HiddenField ID="hfUser" runat="server" />

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/jquery-1.10.2.js"></script>
        <script src="js/Custom.js"></script>



        <%-- <script src="js/toastr.js"></script>--%>
        <script type="text/javascript">
            function ClearSearch() {
                try {

                    $("#txtRackName").val('');




                }
                catch (ex) {
                    alert(ex);
                }
            }
            function ClearSearchClick() {
                try {

                    $("#txtRackName").val('');




                }
                catch (ex) {
                    alert(ex);
                }
            }


            function SetName(id) {
                try {
                    if (window.opener != null && !window.opener.closed) {
                        //     var ddlItem = window.opener.document.getElementById("ddlItem");

                        var hdfValue = $("#" + id).attr("id").replace("ImageButton1", "hdfValue");
                        var hdfRack = $("#" + id).attr("id").replace("ImageButton1", "hdfRack");

                        var hdfSupplier = $("#" + id).attr("id").replace("ImageButton1", "hdfSupplier");
                        var hdfIDRackAllotment = $("#" + id).attr("id").replace("ImageButton1", "hdfIDRackAllotment");
                        //if (ddlItem != null)
                        //    ddlItem.value = document.getElementById(hdfValue).value;

                        var txtName = window.opener.document.getElementById("txtRackName");
                        //var txtUnit = window.opener.document.getElementById("txtUnit");
                        // var btnItem = window.opener.document.getElementById("btnItem");

                        var hfFK_Rack = window.opener.document.getElementById("hfFK_Rack");
                        var hfRack = window.opener.document.getElementById("hfRack");
                        var txtRackName = window.opener.document.getElementById("txtRackName");
                        var ddlSupplier = window.opener.document.getElementById("ddlSupplier");
                        var hfRackAllotment = window.opener.document.getElementById("hfRackAllotment");

                        var divAdd = window.opener.document.getElementById("divAdd");
                        var divView = window.opener.document.getElementById("divView");

                        //var txtQuantity = window.opener.document.getElementById("txtQuantity");

                        //if (txtName != null ) {
                        //    txtName.value = $("#" + id).closest('tr').find('td:eq(2)').text();
                        //    txtUnit.value = $("#" + id).closest('tr').find('td:eq(4)').text();
                        //}
                        //if (btnItem != null) {
                        //    btnItem.click();
                        //}

                        //if (ddlItem != null)
                        //    ddlItem.focus();

                        if (hfFK_Rack != null) {

                            hfFK_Rack.value = document.getElementById(hdfValue).value;
                            hfRack.value = document.getElementById(hdfRack).value;
                            txtRackName.value = hfRack.value;
                            ddlSupplier.value = document.getElementById(hdfSupplier).value;
                            hfRackAllotment.value = document.getElementById(hdfIDRackAllotment).value;

                        }
                    }
                    window.close();
                    return false;
                }
                catch (e) {
                    alert(e);
                }
                return false;
            }

        </script>
    </form>
</body>
</html>
