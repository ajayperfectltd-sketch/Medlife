<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Items.aspx.cs" EnableEventValidation="false" Inherits="Items" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc1" TagName="MedLifePageHelp" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/Custom.js"></script>
    <script src="../../js/slidereveal.js"></script>
    <script src="../../js/slide.js"></script>
    <script src="js/toastr.js"></script>
    <script src="js/Navigation.js"></script>
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <style>
        .rbtn
        {
            width: 220px;
            height: 25px;
            float: left;
        }
    </style>
    <style>
        .box
        {
            margin: 0.5em;
            width: 35em;
            height: 17.2em;
            border-style: groove;
        }

        .Border
        {
            margin: 0.5em;
            border-style: groove;
            box-shadow: 0px 0px 2px 2px #e9e9e9;
            border: solid 2px #FFF;
            border-radius: 7px;
            padding-right: 5px;
        }

        .Border1
        {
            margin: 0.5em;
            width: 35em;
            height: 28.8em;
            border-style: groove;
        }

        .border2
        {
            margin: 0.5em;
            width: 35em;
            height: 17em;
        }
    </style>
    <style type="text/css">
        .rptr0
        {
            background-color: #CCE0EB;
        }

        .rptr1
        {
            background-color: #CCE0EB;
        }
    </style>
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
        function EnterTab(e, Id1) {
            var intKey = window.Event ? e.which : e.KeyCode;


            if (intKey == 13) {
                e.returnValue = false;
                $("#" + Id1).focus();
            }
        }

        function releasebtn() {
            $("#btnSubmit").prop('disabled', false);
        }
        $(function () {

            slider = $("#slider").slideReveal({
                push: false,
                position: "left",
                speed: 600,
                trigger: $("#trigger"),
                // autoEscape: false,
                show: function (obj) {
                    console.log(obj);
                },
                shown: function (obj) {
                    console.log(obj);
                },
                hide: function (obj) {
                    console.log(obj);
                },
                hidden: function (obj) {
                    console.log(obj);
                }
            });

        });

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

        function PopupOnReason() {
            try {


                $("#divPopupReason").css('display', 'block');
            }
            catch (ex) {

            }
        }
        function changeHashOnLoad() {
            window.location.href += "#";
            setTimeout("changeHashAgain()", "50");
        }
        function changeHashAgain() {
            window.location.href += "1";
        }

        var storedHash = window.location.hash;
        window.setInterval(function () {
            if (window.location.hash != storedHash) {
                window.location.hash = storedHash;
            }
        }, 50);
        //function pageLoad() {

        //    var ctrContainer = "form";
        //    //alert('h');
        //    setKeynavigation(ctrContainer);
        //    //alert('k');
        //}



        function ClearAdd() {
            submit = 0;
            
            $("#<%#txtExpiryDays.ClientID%>").val('');
            $("#<%#txtHSNSAC.ClientID%>").val('');
            $("#<%#txtHSNSAC.ClientID%>").val('');
            $("#<%#txtItem.ClientID%>").val('');
            $("#<%#txtItemQuantity.ClientID%>").val('0');
            $("#<%#txtTaxType.ClientID%>").val('');
            $("#<%#txtQ.ClientID%>").val('');
            $("#<%#txtName.ClientID%>").val('');
            $("#<%#txtShortName.ClientID%>").val('');
            $("#<%#txtMinLevel.ClientID%>").val('0');
            $("#<%#txtExpiry.ClientID%>").val('0');
            $("#<%#txtMaximumLevel.ClientID%>").val('0');
            $("#<%#txtReorderlevel.ClientID%>").val('0');
            $("#<%#txtStopsale.ClientID%>").val('');
            $("#<%#txtHSNSAC.ClientID%>").val('');
            $("#<%#txtStoragePlace.ClientID%>").val('');
            $("#<%#txtReturnBeforDays.ClientID%>").val('0');
            $("#<%#txtReoqty.ClientID%>").val('0');
            $("#<%#txtDangerlevel.ClientID%>").val('0');
            $("#<%#ddlCategoryFirst.ClientID%>").val('0');
            $("#<%#ddlCategorySecond.ClientID%>").val('0');
            $("#<%#ddlRack.ClientID%>").val('0');
            $("#<%#ddlUnit.ClientID%>").val('0');
            $("#<%#ddlSubUnit.ClientID%>").val('0');
            $("#<%#ddlBin.ClientID%>").val('0');
            $("#<%#ddlGeneric.ClientID%>").val('0');
            $("#<%#ddlManufacturer.ClientID%>").val('0');
            $("#<%#hfItemsId.ClientID%>").val('0');
            $("#<%#hfSelectedItem.ClientID%>").val('0');
            $('#<%#chkActive.ClientID%>').prop('checked', true);
            $('#<%#chkActive.ClientID%>').prop('checked', true);
            $('#<%#chkstopsale.ClientID%>').prop('checked', false);
            $("#<%#ddlSchedule.ClientID%>").val('0');

            $('#<%#chkNotReturnable.ClientID%>').prop('checked', false);
            $("#<%#hdfImage.ClientID%>").val('');
            $("#<%#imgAttachment.ClientID%>").attr("src", "images/DefaultImage.jpg");
            $("#<%#lbtnRemoveAttachment.ClientID%>").attr("style", "display:none;");

            $("#<%#chkShowInCounter.ClientID%>").prop('checked', false);

            $("#<%#chkPacked.ClientID%>").prop('checked', true);
            $("<%#divImport.ClientID%>").hide();
            $("<%#liStoreStock.ClientID%>").hide();
            DisablePrint();

            $("#<%#txtItemQuantity.ClientID%>").val('0');
            $("#<%#txtItem.ClientID%>").val('0');
            $("#<%#ddlItem.ClientID%>").val('0');
            document.getElementById('btnAdd').disabled = false;
            document.getElementById('txtComboRetailPrice').disabled = false;
            document.getElementById('txtComboWholeSalePrice').disabled = false;
            document.getElementById('txtComboCardPrice').disabled = false;
            document.getElementById('ddlItem').disabled = false;
            document.getElementById('txtItemQuantity').disabled = false;
        }
        function ClearSearch() {
            $("#<%#txtSearchShortName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#ddlSearchCategoryFirst.ClientID%>").val('0');
            $("#<%#ddlSearchCategorySecond.ClientID%>").val('0');
            $("#<%#ddlSearchCategoryThird.ClientID%>").val('0');
            $("#<%#ddlSearchManufacturer.ClientID%>").val('0');
            $("#<%#txtBarcodeSearch.ClientID%>").val('');
            $("#<%#txtSearchShortName.ClientID%>").focus();
            $('#<%#chckCombo.ClientID%>').prop('checked', false);
            $("#<%#ddlSearchSceduled.ClientID%>").val('0');


        }

        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("<%#divImport.ClientID%>").hide();
            $("#<%#txtShortName.ClientID%>").focus();

        }
        function BlurUnit() {
            $("#<%#btnUnitBlurTrigger.ClientID%>").click();
            ddlCategorySecond.Focus();

        }
        function ValidatorEnable() {
            ValidatorEnable(document.getElementById("reqvalBarcode"), true);
        }

        function BlurRack() {
            $("#<%#btnRack.ClientID%>").click();
            $("#<%#ddlBin.ClientID%>").focus();
        }





        function popUpItemsSelect() {

            try {

                var ItemName = $("#txtItem").val();
                if (ItemName.length > 0) {

                    $("#IbtnItemAdd").click();
                }

            }
            catch (e) {
                alert(e);
            }

        }
        function popUpTaxItemsSelect() {

            try {

                var ItemName = $("#txtTaxType").val();
                if (ItemName.length > 0) {

                    $("#imgSearch").click();
                }

            }
            catch (e) {
                alert(e);
            }

        }

        function EnterKeyFilter() {

            $(document).keypress(
                function (event) {
                    if (event.which == '13') {

                        event.preventDefault();
                    }
                });

        }

    </script>

</head>


<body onkeydown="EnterTabBody(event);">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5">

                    <div class="heading2">
                        <div class="user">Items</div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                                <<uc1:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                                <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle" TabIndex="29" runat="server" OnClientClick="ShowAddDiv(); releasebtn(); " OnClick="ImageButton1_Click" />
                                <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle" ToolTip="Veiw" runat="server" OnClientClick="EnableExport();" OnClick="imgbtnView_Click" TabIndex="28" />
                            </div>
                        </asp:Panel>

                    </div>

                    <asp:HiddenField ID="hdfSearchCriteria" runat="server" />
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">
                            <div id="divAdd" runat="server" style="display: block; width: 1200px;">
                                <div class="divL">
                                    <ul class="contentul">
                                        <div class="Border">

                                            <div style="padding-left: 10px;">
                                                <li>
                                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                                    <asp:TextBox ID="txtShortName" onkeydown="EnterTab(event,'txtName');" CssClass="textboxMandatory" Width="250px" runat="server" TabIndex="1" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Short name required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True" ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                    </cc:ValidatorCalloutExtender>
                                                    <asp:RequiredFieldValidator ID="reqEx" runat="server" ErrorMessage="Short name required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True" ValidationGroup="CheckItem"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="reqEx">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                                    <asp:TextBox ID="txtName" onkeydown="EnterTab(event,'ddlRack');" CssClass="textboxMandatory" Width="250px" runat="server" TabIndex="2" MaxLength="150"></asp:TextBox>
                                                    <asp:ImageButton ID="btnVerify" ValidationGroup="CheckItem" runat="server" OnClick="btnVerify_Click" ImageUrl="~/images/icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                    </cc:ValidatorCalloutExtender>
                                                    <%--  <asp:RequiredFieldValidator ID="ReqEName" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None"  SetFocusOnError="True" ValidationGroup="CheckItem"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="ReqEName">
                                                </cc:ValidatorCalloutExtender>--%>
                                                </li>
                                                <li class="btnMargin">


                                                    <asp:Label ID="lblTInMsg" Font-Size="15px" runat="server" Font-Bold="true" Text=""></asp:Label>

                                                </li>
                                                <li>
                                                    <asp:Label ID="Label39" runat="server" CssClass="label" Text="Rack"></asp:Label>
                                                    <asp:DropDownList ID="ddlRack" onkeydown="EnterTab(event,'ddlBin');" runat="server" Width="260" onBlur="return BlurRack();" CssClass="dropdown" TabIndex="3"></asp:DropDownList>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label18" runat="server" CssClass="label" Text="Bin"></asp:Label>
                                                    <asp:DropDownList ID="ddlBin" onkeydown="EnterTab(event,'ddlGeneric');" Width="260" runat="server" CssClass="dropdown" TabIndex="4"></asp:DropDownList>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label19" runat="server" CssClass="label" Text="Generic"></asp:Label>
                                                    <asp:DropDownList ID="ddlGeneric" onkeydown="EnterTab(event,'ddlUnit');" runat="server" Width="260" CssClass="dropdownMandatory" TabIndex="5"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Select Generic" ControlToValidate="ddlGeneric" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>


                                                <li class="clear"></li>
                                            </div>
                                            <br />
                                        </div>
                                        <div class="Border">

                                            <div style="padding-left: 10px;">
                                                <li>
                                                    <asp:Label ID="Label12" runat="server" CssClass="label" Text="Unit"></asp:Label>
                                                    <asp:DropDownList ID="ddlUnit" Width="260" runat="server" onkeydown="EnterTab(event,'ddlSubUnit');" CssClass="dropdownMandatory" TabIndex="6"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select a Unit" ControlToValidate="ddlUnit" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label37" runat="server" CssClass="label" Text="Sub Unit"></asp:Label>
                                                    <asp:DropDownList ID="ddlSubUnit" onkeydown="EnterTab(event,'ddlCategoryFirst');" Width="260" runat="server" CssClass="dropdown" TabIndex="7"></asp:DropDownList>

                                                </li>
                                                <li>
                                                    <asp:Label ID="Label17" runat="server" CssClass="label" Text="Category "></asp:Label>
                                                    <asp:DropDownList ID="ddlCategoryFirst" onkeydown="EnterTab(event,'ddlCategorySecond');" Width="260" runat="server" CssClass="dropdownMandatory" TabIndex="8" onblur="BlurUnit();"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select a Category" ControlToValidate="ddlCategoryFirst" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>

                                                <li>
                                                    <asp:Label ID="Label8" runat="server" CssClass="label" Text="Sub Category"></asp:Label>
                                                    <asp:DropDownList ID="ddlCategorySecond" onkeydown="EnterTab(event,'ddlManufacturer');" Width="260" runat="server" CssClass="dropdown" TabIndex="9"></asp:DropDownList>

                                                </li>
                                                <li>
                                                    <asp:Label ID="Label15" runat="server" CssClass="label" Text="Manufacturer"></asp:Label>
                                                    <asp:DropDownList ID="ddlManufacturer" onkeydown="EnterTab(event,'txtMinLevel');" Width="260" runat="server" CssClass="dropdownMandatory" TabIndex="10"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select Manufacturer" ControlToValidate="ddlManufacturer" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>

                                                <br />
                                            </div>
                                        </div>

                                        <div class="Border">
                                            <div style="padding-left: 10px;">

                                                <li>
                                                    <asp:Label ID="Label47" runat="server" CssClass="label" Font-Underline="true" Text="Combo Item Details"></asp:Label>
                                                </li>
                                                <br />


                                                <li style="display: none;">
                                                    <asp:Label ID="Label42" runat="server" CssClass="label" Text="Items"></asp:Label>
                                                    <asp:DropDownList ID="ddlItem" Width="260" runat="server" CssClass="dropdownMandatory"></asp:DropDownList>

                                                </li>
                                                <li>
                                                    <asp:Label ID="Label61" runat="server" CssClass="label" Text="Items"></asp:Label>
                                                    <asp:TextBox ID="txtItem" runat="server" AutoPostBack="false" ValidationGroup="Add" TabIndex="100" CssClass="textboxMandatory" MaxLength="150"></asp:TextBox>
                                                    <asp:ImageButton ID="IbtnItemAdd" runat="server" OnClick="IbtnItemAdd_Click" TabIndex="101" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute;" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Select an Item" ControlToValidate="txtItem" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender59" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator32">
                                                    </cc:ValidatorCalloutExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Select an Item" ControlToValidate="txtItem" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label43" Text="Quantity" CssClass="label" runat="server" />
                                                    <asp:TextBox runat="server" ID="txtItemQuantity" ValidationGroup="Add" Style="text-align: right;" CssClass="textboxMandatory" MaxLength="7" />
                                                    <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True" TargetControlID="txtItemQuantity" FilterType="Custom , Numbers" ValidChars="">
                                                    </cc:FilteredTextBoxExtender>
                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtItemQuantity" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="9999999" ValidationGroup="Add" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>
                                                <li class="btnMargin">
                                                    <asp:Button ID="btnAdd" runat="server" ValidationGroup="Add" CssClass="button" Width="80" Text="Insert" OnClick="btnAdd_Click" />
                                                </li>
                                                <div style="width: 400px; overflow: auto; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">
                                                    <li>
                                                        <asp:GridView ID="gvItemsdetails" runat="server" CssClass="grid"
                                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                                            BackColor="White" AllowPaging="true" BorderWidth="1px" Width="390px" CellPadding="3" ForeColor="Black"
                                                            GridLines="Vertical" PageSize="10" DataKeyNames="FK_Items" OnPageIndexChanging="gvItemsdetails_PageIndexChanging" OnRowDeleting="gvItemsdetails_RowDeleting">
                                                            <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SlNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Item" HeaderText="Items" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="FK_Items" HeaderText="FK_Items" Visible="false" />
                                                                <asp:BoundField DataField="Quantity" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderText="Quantity" />

                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                                                    </li>
                                                </div>

                                                <br />
                                            </div>
                                        </div>
                                        <li class="clear"></li>


                                        <li class="clear"></li>
                                        <li class="clear"></li>
                                        <li class="clear"></li>

                                        <li class="clear"></li>
                                        <li class="clear"></li>
                                        <li class="clear"></li>

                                        <li class="btnMargin" style="padding-left: 160px">

                                            <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="26" ValidationGroup="AddItem" OnClick="btnSubmit_Click" />

                                            <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="27" OnClientClick="ClearAdd();" OnClick="btnClear_Click" />
                                            &nbsp;&nbsp;
                                        <asp:HiddenField ID="hfItemsId" runat="server" />
                                            <asp:HiddenField ID="hfClient" runat="server" />
                                            <asp:HiddenField ID="hfUser" runat="server" />
                                            <asp:HiddenField ID="hfIsAdd" runat="server" />
                                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                            <asp:HiddenField ID="hfIsDelete" runat="server" />
                                            <asp:HiddenField ID="hfSelectedItem" runat="server" />
                                            <asp:HiddenField ID="hfSelectedTax" runat="server" />
                                            <asp:HiddenField ID="hfuserName" runat="server" />
                                            <asp:HiddenField ID="hfMasterID" runat="server" />
                                            <asp:Button ID="btnUnitBlurTrigger" Text="Hide" runat="server" Style="display: none;" CausesValidation="false" OnClick="btnUnitBlurTrigger_Click" />
                                            <asp:Button ID="btnRack" Text="Hide" runat="server" Style="display: none;" CausesValidation="false" OnClick="btnRack_Click" />
                                        </li>

                                    </ul>

                                </div>

                                <div class="divR">
                                    <ul class="contentul">

                                        <div class="Border" style="width: 89%">

                                            <div style="padding-left: 10px;">
                                                <li>
                                                    <asp:Label ID="Label23" runat="server" CssClass="label" Text="Minimum Level"></asp:Label>
                                                    <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtMinLevel" FilterType="Custom , Numbers" ValidChars=".">
                                                    </cc:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtMinLevel" onkeydown="EnterTab(event,'txtMaximumLevel');" Width="250px" CssClass="textbox" Style="text-align: right;" runat="server" TabIndex="11" MaxLength="16"></asp:TextBox>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label24" runat="server" CssClass="label" Text="Maximum Level"></asp:Label>
                                                    <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtMaximumLevel" FilterType="Custom , Numbers" ValidChars=".">
                                                    </cc:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtMaximumLevel" onkeydown="EnterTab(event,'txtReorderlevel');" Width="250px" Style="text-align: right;" CssClass="textbox" TabIndex="12" runat="server" MaxLength="16"></asp:TextBox>
                                                    <asp:CompareValidator ID="cmpvMinMaxValue" runat="server" ErrorMessage="Maximum level must be greater than or equal to of minimum level" Display="None" ControlToCompare="txtMinLevel"
                                                        ControlToValidate="txtMaximumLevel" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Double" ValidationGroup="AddItem"></asp:CompareValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="cmpvMinMaxValue">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label25" runat="server" CssClass="label" Text="Reorder Level"></asp:Label>
                                                    <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" TargetControlID="txtReorderlevel" FilterType="Custom , Numbers" ValidChars=".">
                                                    </cc:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtReorderlevel" onkeydown="EnterTab(event,'txtReoqty');" Width="250px" Style="text-align: right;" CssClass="textbox" TabIndex="13" runat="server" MaxLength="16"></asp:TextBox>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Reorder Quantity"></asp:Label>
                                                    <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True" TargetControlID="txtReoqty" FilterType="Custom , Numbers" ValidChars=".">
                                                    </cc:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtReoqty" onkeydown="EnterTab(event,'txtDangerlevel');" Width="250px" Style="text-align: right;" CssClass="textbox" TabIndex="14" runat="server" MaxLength="16"></asp:TextBox>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label27" runat="server" CssClass="label" Text="Danger Level"></asp:Label>
                                                    <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True" TargetControlID="txtDangerlevel" FilterType="Custom , Numbers" ValidChars=".">
                                                    </cc:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtDangerlevel" onkeydown="EnterTab(event,'txtExpiry');" Width="250px" Style="text-align: right;" CssClass="textbox" TabIndex="15" runat="server" MaxLength="16"></asp:TextBox>

                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Danger level  must be less than or equal to of Maximum level" Display="None" ControlToCompare="txtMaximumLevel"
                                                        ControlToValidate="txtDangerlevel" Operator="LessThanEqual" SetFocusOnError="True" Type="Double" ValidationGroup="AddItem"></asp:CompareValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="Expiry  Validation"></asp:Label>
                                                    <asp:TextBox ID="txtExpiry" onkeydown="EnterTab(event,'chkstopsale');" Width="250px" Style="text-align: right;" CssClass="textbox" TabIndex="16" onKeypress="CheckIntegerAndNeg(event);" runat="server" MaxLength="10"></asp:TextBox>
                                                </li>

                                                <li>
                                                    <asp:Label ID="lblStopSale" runat="server" CssClass="label" Text="Stop Sale"></asp:Label>
                                                    <asp:CheckBox ID="chkstopsale" onkeydown="EnterTab(event,'txtStopsale');" Style="padding-right: 2px" runat="server" Width="15" TabIndex="17" />

                                                </li>
                                                <li class="clear"></li>
                                                <li>
                                                    <asp:Label ID="Label10" runat="server" CssClass="label" Text="Stop Sale Remarks"></asp:Label>
                                                    <asp:TextBox ID="txtStopsale" onkeydown="EnterTab(event,'txtHSNSAC');" Width="250px" CssClass="textbox" ToolTip="Stop Sale Remarks" runat="server" MaxLength="150" TabIndex="18"> </asp:TextBox>

                                                </li>
                                                <li>
                                                    <asp:Label ID="Label52" Text="HSN/SAC" CssClass="label" runat="server" />
                                                    <asp:TextBox runat="server" ID="txtHSNSAC" onkeydown="EnterTab(event,'ddlSchedule');" Width="250px" TabIndex="19" CssClass="textbox" MaxLength="50" />
                                                </li>

                                                <li class="clear"></li>
                                                <li>
                                                    <asp:Label ID="Label13" runat="server" CssClass="label" Text="Schedule"></asp:Label>
                                                    <asp:DropDownList ID="ddlSchedule" onkeydown="EnterTab(event,'txtStoragePlace');" runat="server" Width="258px" CssClass="dropdown" TabIndex="3"></asp:DropDownList>

                                                </li>
                                                <li class="clear"></li>
                                                <li>
                                                    <asp:Label ID="Label16" runat="server" CssClass="label" Text="Storage Place"></asp:Label>
                                                    <asp:TextBox ID="txtStoragePlace" onkeydown="EnterTab(event,'chkNotReturnable');" CssClass="textbox" Width="250px" ToolTip="Storage Place" runat="server" MaxLength="50" TabIndex="22"></asp:TextBox>

                                                </li>
                                                <li>
                                                    <asp:Label ID="lblNonRet" runat="server" CssClass="label" Text="Not Returnable"></asp:Label>
                                                    <asp:CheckBox ID="chkNotReturnable" onkeydown="EnterTab(event,'txtReturnBeforDays');" runat="server" TabIndex="23" />

                                                </li>
                                                <li class="clear"></li>
                                                <li>
                                                    <asp:Label ID="Label20" runat="server" CssClass="label" Text="Return Before Days"></asp:Label>
                                                    <asp:TextBox ID="txtReturnBeforDays" onkeydown="EnterTab(event,'chkPacked');" Width="250px" CssClass="textbox" ToolTip="Return BeforDays" onKeypress="CheckIntegerAndNeg(event);" Style="text-align: right;" runat="server" MaxLength="150" TabIndex="24"></asp:TextBox>

                                                </li>
                                                <li>
                                                    <asp:Label ID="lblPacked" runat="server" CssClass="label" Text="Packed"></asp:Label>
                                                    <asp:CheckBox ID="chkPacked" onkeydown="EnterTab(event,'txtExpiryDays');" runat="server" Style="width: 100px; padding-right: 50px;" TabIndex="25" />

                                                </li>
                                                <li>
                                                    <asp:Label ID="Label29" runat="server" CssClass="label" Text="Expiry  Warning Before"></asp:Label>
                                                    <asp:TextBox ID="txtExpiryDays"  Text="0"  onkeydown="EnterTab(event,'btnSubmit');" Width="250px" Style="text-align: right;" CssClass="textbox" TabIndex="25" onKeypress="CheckIntegerAndNeg(event);" runat="server"  MaxLength="3"></asp:TextBox>
                                                </li>
                                                <li class="clear"></li>

                                            </div>
                                            <br />
                                        </div>
                                        <div class="clear"></div>
                                        <div style="display: none">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>


                                                    <li>
                                                        <asp:Image ID="imgAttachment" Width="150" CssClass="AttachImage" ImageUrl="~/images/DefaultImage.jpg" Height="150" runat="server" Style="float: left;" />
                                                        <br />
                                                        <asp:LinkButton ID="lbtnRemoveAttachment" runat="server" OnClick="lbtnRemoveAttachment_Click" Style="display: none;">Remove</asp:LinkButton>

                                                    </li>
                                                    <li class="clear"></li>
                                                    <li class="clear"></li>
                                                    <li>
                                                        <cc:AsyncFileUpload ID="fupImage" runat="server" OnClientUploadStarted="uploadStarted" CssClass="imageUploaderField" TabIndex="45" OnUploadedComplete="fupImage_UploadedComplete" OnClientUploadComplete="UploadComplete1" />
                                                        <asp:Button ID="btnHiddenImage" runat="server" Text="Image" OnClick="btnHiddenImage_Click" CausesValidation="false" Style="display: none;" />
                                                        <asp:HiddenField ID="hdfImage" runat="server" />
                                                    </li>
                                                    <li class="clear"></li>
                                                    <li class="clear"></li>
                                                    <li>
                                                        <asp:Button ID="btnHidden" Style="display: none; float: right" runat="server" Text="Button" OnClick="btnHidden_Click" />

                                                    </li>
                                                    <li class="clear"></li>
                                                    <li class="clear"></li>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </div>

                                        <div class="Border" style="width: 89%;">
                                            <div style="padding-left: 10px;">

                                                <li>
                                                    <asp:Label ID="Label21" runat="server" CssClass="label" Font-Underline="true" Text="Tax Details"></asp:Label>
                                                </li>
                                                <br />


                                                <li>
                                                    <asp:Label ID="Label26" runat="server" CssClass="label" Text="Tax Type"></asp:Label>
                                                    <asp:TextBox ID="txtTaxType" runat="server" AutoPostBack="false" onBlur="popUpTaxItemsSelect();" CssClass="textboxMandatory" MaxLength="75"></asp:TextBox>
                                                    <asp:ImageButton ID="imgSearch" runat="server" OnClick="imgSearch_Click" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Tax Type" ControlToValidate="txtTaxType" Display="None" SetFocusOnError="True" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label28" Text="Percentage" CssClass="label" runat="server" />
                                                    <asp:TextBox runat="server" ID="txtQ" Style="text-align: right;" CssClass="textboxMandatory" onkeypress="CheckNumWithDecimal(event);" MaxLength="5" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Enter Percentage" ControlToValidate="txtQ" Display="None" SetFocusOnError="True" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                                    </cc:ValidatorCalloutExtender>
                                                    <asp:RangeValidator ID="rngTaxPercentage" runat="server" ControlToValidate="txtQ"
                                                        ErrorMessage="Percentage must be between 0.01 and 100" MinimumValue="0.01" MaximumValue="100"
                                                        Type="Double" Display="None" ValidationGroup="Insert" SetFocusOnError="True"></asp:RangeValidator>
                                                    <cc:ValidatorCalloutExtender ID="vceTaxPct" runat="server" Enabled="True" TargetControlID="rngTaxPercentage">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>

                                                <li class="btnMargin">
                                                    <asp:Button ID="btnSub" runat="server" CssClass="button" Width="80" Text="Insert" ValidationGroup="Insert" OnClick="btnSub_Click" />
                                                </li>

                                                <div style="width: 400px; overflow: auto; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">
                                                    <li>
                                                        <asp:GridView ID="grdTaxtype" runat="server" CssClass="grid"
                                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                                            BackColor="White" BorderColor="Teal" BorderWidth="1px" Width="390px" CellPadding="3" ForeColor="Black"
                                                            GridLines="Vertical" PageSize="10" DataKeyNames="ID_TaxType" OnPageIndexChanging="grdTaxtype_PageIndexChanging" OnRowDeleting="grdTaxtype_RowDeleting" AllowPaging="True">
                                                            <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SlNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Tax Type" DataField="TaxType" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="ID_TaxType" HeaderText="ID_TaxType" Visible="false" />
                                                                <asp:BoundField DataField="TaxPercentage" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderText="Percentage" />

                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                                                    </li>
                                                </div>

                                                <br />
                                            </div>
                                        </div>
                                        <div style="float: left;">

                                            <ul>

                                                <li>
                                                    <div class="rbtn">
                                                        <asp:CheckBox ID="chkActive" runat="server" Visible="false" TabIndex="35" Text="Active" />
                                                    </div>
                                                </li>
                                                <li class="clear"></li>


                                                <li>
                                                    <div class="rbtn">
                                                        <asp:CheckBox ID="chkShowInCounter" Visible="false" runat="server" Style="width: 100px; padding-right: 50px;" TabIndex="34" Text="Show In Counter" />
                                                    </div>
                                                </li>
                                                <li class="clear"></li>
                                                <li></li>
                                                <li class="clear"></li>
                                            </ul>
                                        </div>
                                        <li class="clear"></li>
                                        <div class="clear"></div>
                                        <li id="liStoreStock" runat="server" class="liStoreStock">
                                            <table class="grid" cellspacing="0" cellpadding="0" rules="all" id="Table1" style="color: Black; font-size: 15px; font-weight: bold; background-color: White; border-color: black; border-width: 2px; border-style: Solid; height: 42px; width: 350px;">
                                                <tbody>
                                                    <tr>
                                                        <asp:Label Text="Current Stock" Width="350px" BackColor="#FFFFCE" ForeColor="#0C385D" Font-Bold="true" Font-Size="16px" runat="server" />
                                                    </tr>
                                                    <asp:Repeater ID="rptrStockDetails" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="<%#(Container.ItemIndex%2==0)?"rptr0":"rptr1" %>">
                                                                <td><%#Eval("Store") %></td>
                                                                <td><%#Eval("CurrentStock") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="main_bg">

                            <div id="divView" runat="server" style="display: none;">
                                <div class="divL">
                                    <ul class="contentul">
                                        <li style="display: none;">
                                            <asp:Label ID="Label38" runat="server" CssClass="label" Text="BarCode"></asp:Label>
                                            <asp:TextBox ID="txtBarcodeSearch" MaxLength="50" Width="250" CssClass="textbox" runat="server"></asp:TextBox>
                                        </li>
                                        <li>
                                            <li>
                                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                                <asp:TextBox ID="txtSearchShortName" onkeydown="EnterTab(event,'txtSearchName');" Width="250" CssClass="textbox" runat="server" TabIndex="30"></asp:TextBox>
                                            </li>
                                            <li>
                                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                                <asp:TextBox ID="txtSearchName" onkeydown="EnterTab(event,'ddlSearchCategoryFirst');" Width="250" CssClass="textbox" runat="server" TabIndex="31"></asp:TextBox>
                                            </li>
                                            <li>
                                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Category"></asp:Label>
                                                <asp:DropDownList ID="ddlSearchCategoryFirst" onkeydown="EnterTab(event,'ddlSearchCategorySecond');" AutoPostBack="true" Width="260" runat="server" CssClass="dropdown" TabIndex="32" OnSelectedIndexChanged="ddlSearchCategoryFirst_SelectedIndexChanged"></asp:DropDownList>
                                            </li>



                                            <li class="btnMargin" style="padding-right: 25px">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="EnableExport();" OnClick="btnSearch_Click" TabIndex="36" />
                                                <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" TabIndex="37" OnClientClick="ClearSearch();" />
                                            </li>
                                        </li>
                                    </ul>
                                </div>

                                <div class="divL">
                                    <ul class="contentul">
                                        <li>
                                            <asp:Label ID="Label9" runat="server" CssClass="label" Text="Sub Category"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchCategorySecond" onkeydown="EnterTab(event,'ddlSearchManufacturer');" Width="260" runat="server" CssClass="dropdown" TabIndex="33"></asp:DropDownList>
                                        </li>
                                        <li style="display: none;">
                                            <asp:Label ID="Label11" runat="server" CssClass="labemain_bgl" Text="Category Third"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchCategoryThird" runat="server" Width="260" CssClass="dropdown"></asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label32" runat="server" CssClass="label" Text="Manufacturer"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchManufacturer" onkeydown="EnterTab(event,'ddlSearchSceduled');" AutoPostBack="true" runat="server" Width="260" CssClass="dropdown" TabIndex="34"></asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label14" runat="server" CssClass="label" Text="Scheduled"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchSceduled" onkeydown="EnterTab(event,'btnSearch');" AutoPostBack="true" runat="server" Width="260" CssClass="dropdown" TabIndex="35"></asp:DropDownList>
                                        </li>
                                        <span class="chckbox" style="display: none">
                                            <div class="rbtn" style="margin-left: 133px; margin-top: 7px;">
                                                <asp:CheckBox ID="chckCombo" runat="server" TabIndex="35" Text="Combo" />
                                            </div>
                                        </span>

                                    </ul>
                                </div>
                                <li class="clear"></li>
                                <ul class="contentul">
                                    <asp:GridView ID="gvItems" runat="server" CssClass="grid"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="Teal" BorderStyle="Solid" Width="93%" BorderWidth="1px" CellPadding="3" ForeColor="Black" OnPageIndexChanging="gvItems_PageIndexChanging" GridLines="Vertical" PageSize="10" DataKeyNames="ID_Items" OnRowDeleting="gvItems_RowDeleting" OnSelectedIndexChanging="gvItems_SelectedIndexChanging" AllowPaging="True">
                                        <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                        <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SlNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Barcode" HeaderText="BarCode" Visible="false" />
                                            <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Name" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="True" ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                                                <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' OnClick="Page_Changed" Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ul>
                            </div>
                        </div>
                        <div id="divImport" runat="server" style="display: none;">
                            <br class="clear" />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <cc:AsyncFileUpload ID="FileUploadExcel" runat="server" CssClass="imageUploaderField" OnClientUploadComplete="UploadCompleteadbc" OnClientUploadStarted="uploadStartedabdc" OnUploadedComplete="FileUploadExcel_UploadedComplete" />
                                    <asp:Button ID="btnShowData" runat="server" OnClick="btnShowData_Click" CausesValidation="false"
                                        SkinID="button-process" Text="Show Data" UseSubmitBehavior="False" Style="display: none;"
                                        Width="70px" />
                                    <div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div align="center" style="position: absolute; z-index: auto; top: 574px; left: 370px; width: 254px;">
                            </div>
                        </div>

                    </div>




                    <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                    <asp:Panel ID="divPopup" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 370px;">
                        <div id="divheader" runat="server" class="PopupHeader">
                            <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head" CssClass="lblPopupHeader"></asp:Label>
                            <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                        </div>
                        <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                    </asp:Panel>
                    <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                        CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                    </cc:ModalPopupExtender>


                    <asp:Button ID="btnS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                    <asp:Panel ID="divPopUpdelete" runat="server" CssClass="DivStyle" Style="display: none;">
                        <div id="divPopheader" runat="server" class="PopupHeaderReason">
                            <asp:Label ID="lblPopupHeader1" runat="server" Text="Reason"></asp:Label>
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                        </div>
                        <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                            <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                        </div>
                    </asp:Panel>
                    <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divPopUpdelete" TargetControlID="btnS"
                        CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divPopheader">
                    </cc:ModalPopupExtender>
                    <asp:Button ID="btnP" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                    <asp:Panel ID="pnlPrvlg" runat="server" Style="display: none;" CssClass="DivStyle">
                        <div id="divPrvlg" runat="server" class="PopupHeader">
                            <asp:Label ID="Label22" runat="server" Text="Privilege" CssClass="lblPopupHeader"></asp:Label>
                            <asp:ImageButton ID="btnCP" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                        </div>
                        <uc1:CommonPopupPrivilege runat="server" ID="CommonPopupPrivilege" />
                    </asp:Panel>
                    <cc:ModalPopupExtender ID="mpPrivilege" runat="server" PopupControlID="pnlPrvlg" TargetControlID="btnP"
                        CancelControlID="btnCP" Drag="true" PopupDragHandleControlID="divPrvlg">
                    </cc:ModalPopupExtender>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


    </form>
</body>
</html>
