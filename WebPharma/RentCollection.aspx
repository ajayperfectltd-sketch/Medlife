<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RentCollection.aspx.cs" Inherits="RentCollection" %>
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


            $("#<%#txtTransDate.ClientID%>").val('');
            $("#<%#txtAmount.ClientID%>").val('');
         
            $("#<%#ddlPaymentMode.ClientID%>").val('0');
        
            $("#<%#ddlSupplier.ClientID%>").val('0');
            $("#<%#hfRentCollectionId.ClientID%>").val('0');
            $("#<%#chkVacate.ClientID%>").prop('checked',false);
            var toDate = new Date();
            $("#txtTransDate").val(toDate.format("dd/MM/yyyy"));
          

            $("#<%#hfFK_Rack.ClientID%>").val('0');
            $("#<%#hfRack.ClientID%>").val('0');
            $("#<%#hfRackAllotment.ClientID%>").val('0');
            $("#<%#txtRackName.ClientID%>").val('');
            
            $("#<%#ddlSupplier.ClientID%>").focus();
            ClearSearch();
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#ddlSupplier.ClientID%>").focus();
        }
        function ClearSearch() {

            $("#<%#ddlSearchRack.ClientID%>").val('0');
            $("#<%#ddlSearchRack.ClientID%>").focus();
        }


        function popUpRackSelect() {

            try {
                ///alert('a');
                var RackName = $("#txtRackName").val();
                if (RackName.length > 0) {
                    url = "RackSearch.aspx?FK_Supplier=" + $("#ddlSupplier").val() + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + RackName;
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
        function popUpRackSelectClick() {

            try {
                ///alert('a');
                var RackName = $("#txtRackName").val();
                url = "RackSearch.aspx?FK_Supplier=" + $("#ddlSupplier").val() + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + RackName;
                popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
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
                     <div class="search" style="width:670px; " >Rent Collection</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" TabIndex="10" Height="38" runat="server" OnClientClick="ShowAddDiv();releasebtn();ClearAdd();" OnClick="imgbtnAdd_Click"  />
                        </div>
                           <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" TabIndex="9" runat="server" OnClick="imgbtnView_Click"   />
                        </div>
                    </div>
                   <%-- <div class="user_bg">
                        <div class="user1">Rent Collection</div>
                    </div>--%>
                    <div class="clear"></div>
                      <div class="group_bg2">
                    <div id="divAdd" runat="server" style="display: none;">
                        <ul class="contentul">
                         <asp:Panel ID="panelAdd" runat="server">
                                 <li>
                                    <label class="label">Supplier</label>
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlSupplier" TabIndex="1" >
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqvalSupplier" runat="server" ErrorMessage="Supplier required" ControlToValidate="ddlSupplier" Display="None"  SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="reqvalSupplier">
                                    </cc:ValidatorCalloutExtender>
                                   
                                </li>
                            <li>
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Rack Name"></asp:Label>
                              
                                <asp:TextBox ID="txtRackName" CssClass="textboxMandatory" runat="server" TabIndex="2"   onblur="popUpRackSelect();" ></asp:TextBox>

                                      <asp:ImageButton ID="imgPopUp" runat="server"  TabIndex="3"  OnClientClick="return popUpRackSelectClick();" ImageUrl="~/images/serach_icon.png" Style="padding-left:2px; margin-top: 0px;position: absolute; width: 30px; height: 20px;" />
                              
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Rack Name required" ControlToValidate="txtRackName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                </cc:ValidatorCalloutExtender>



                                  <%--  <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlRack" TabIndex="1" AppendDataBoundItems="True" >
                                       
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Rack name required" ControlToValidate="ddlRack" Display="None" 
                                     SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>--%>


                            </li>
                              <li>
                                    <label class="label">Transaction Date</label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtTransDate" TabIndex="3" AutoComplete="off" />
                                    <cc:CalendarExtender ID="txtTransDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtTransDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="reqvalTransDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtTransDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalTransDate">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                              
                              <li>
                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="Amount"></asp:Label>
                                <asp:TextBox ID="txtAmount"  TabIndex="4" CssClass="textboxMandatory" runat="server" Style="text-align:right" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqvalAmount" runat="server" ErrorMessage="Amount required" ControlToValidate="txtAmount" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="reqvalAmount">
                                </cc:ValidatorCalloutExtender>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAmount" ErrorMessage="Enter  valid  Amount" Display="None" MaximumValue="999999999999"  SetFocusOnError="true" ValidationGroup="submit" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                        </cc:ValidatorCalloutExtender>
                            </li> 

                            
                           

                              <li>
                                    <label class="label">Payment Mode</label>
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlPaymentMode"  TabIndex="5" >
                                    
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqvalPaymentMode" runat="server" ErrorMessage="Payment Mode required" ControlToValidate="ddlPaymentMode" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="reqvalPaymentMode">
                                    </cc:ValidatorCalloutExtender>
                                    
                                </li>
                              <li>
                                    <label class="label">Vacate</label>
                                   <asp:CheckBox ID="chkVacate" runat="server" TabIndex="6" CssClass="chckbox" Text="Vacate"  />
                                    
                                    
                               
                                </li>
                             
                              
                            <li class="btnMargin">

                             
                                <asp:HiddenField ID="hfRentCollectionId" runat="server" />
                                <asp:HiddenField ID="hfClient" runat="server" />
                                <asp:HiddenField ID="hfUser" runat="server" />
                                <asp:HiddenField ID="hfIsAdd" runat="server" />
                                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                <asp:HiddenField ID="hfIsDelete" runat="server" />
                                
                                <asp:HiddenField ID="hfFK_Rack" runat="server" />
                                <asp:HiddenField ID="hfRack" runat="server" />
                                   <asp:HiddenField ID="hfRackAllotment" runat="server" />
                            </li>
                            </asp:Panel>
                             <li class="btnMargin">
                          <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="7" ValidationGroup="submit" OnClick="btnSubmit_Click"   OnClientClick="disablebutn();"/>
                                <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClick="imgbtnAdd_Click" TabIndex="8" OnClientClick="ClearAdd();" />
                             </li>
                    </div>
                    <div id="divView" runat="server" style="display: none;">             
                        <ul class="contentul">
                            <li>
                                
                             <li>
                                   <asp:Label ID="Label1" runat="server" CssClass="label" Text="Rack Name"></asp:Label>
                              

                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlSearchRack" TabIndex="11" AppendDataBoundItems="True" >
                                       
                                    </asp:DropDownList>
                                   
                            </li>
                               
                            <li class="btnMargin">
                                 <asp:Button ID="btnSearch" runat="server" CssClass="button"  Text="Search" OnClick="btnSearch_Click" OnClientClick="return btnSearch_Click();"  TabIndex="12" />
                                <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch();" OnClick="btnSearch_Click" TabIndex="13" />
                            </li>
                            </li>
                        </ul>           
                        <ul class="contentul">
                            <asp:GridView ID="gvRentCollection" runat="server" CssClass="grid"
                                EmptyDataText="No Data Found!" AutoGenerateColumns="False" 
                                BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100"
                                 DataKeyNames="ID_RentCollection" OnRowDeleting="gvRentCollection_RowDeleting" OnSelectedIndexChanging="gvRentCollection_SelectedIndexChanging">
                              
                                  <AlternatingRowStyle BackColor="#E5EFE4" />
                                <Columns>
                                    <asp:BoundField DataField="SLNO" HeaderText="SLNO"  ItemStyle-HorizontalAlign="Center"/>                                   
                                    <asp:BoundField DataField="RackName" HeaderText="Rack Name" />  
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right"/>   
                                    <asp:BoundField DataField="TransDate" HeaderText="TransDate" />                                                             
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">                                 
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="View" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
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
                <%--    <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                    <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                          </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>