<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stock.aspx.cs" Inherits="Stock" EnableEventValidation="false" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Stock Correction::</title>
    <link href="css/main.css" rel="stylesheet" />

    <link href="css/toastr.css" rel="stylesheet" />
    <style>
        .PopupHeader {
            background-color: #0f3a5d;
            width: 478px;
        }

        .imgPopup {
            margin-left: 425px;
        }
    </style>
</head>
<body>
    <form id="frmItemConversion" runat="server" autocomplete="off">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">


                        <div class="search" style="width: 670px;">Stock Correction</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/add1.png" TabIndex="25" Width="37" Height="38" runat="server" CausesValidation="false" OnClientClick="ShowAddDiv();  releasebtn();" OnClick="imgbtnAdd_Click" />
                        </div>


                    </div>
                    <%--   <div class="user_bg">
                        <div class="user1">Item Conversion</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="display: block; height: 485px">




                            <ul class="contentul" id="Container">

                                <div class="divL" style="width: 250px;">

                                    <li>
                                        <label class="label" style="width: 50px;">Store</label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStore" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="img"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>

                                    </li>


                                </div>
                                <div style="float: left; width: 270px;">
                                    <li>

                                        <label class="label" style="width: 70px;">Category</label>
                                        <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="2" CssClass="dropdown"></asp:DropDownList>

                                    </li>



                                </div>
                                <div class="divL" style="width: 370px;">
                                    <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item" Width="100px"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtitem" MaxLength="20" AutoComplete="off" Width="200px" onblur="return popUpStockSelect();" TabIndex="3" Style="margin-left: 34px;" />
                                        <asp:ImageButton ID="ImageButton3" runat="server" onkeypress="return popUpStockSelectClick();" OnClientClick="return popUpStockSelectClick();" TabIndex="4" ImageUrl="~/images/serach_icon.png" Style="padding-left: 2px; margin-top: 0px; height: 23px; position: absolute" />


                                    </li>
                                    <li style="float: right; padding-top: 9px;">

                                        <asp:Button Text="Process" ID="btnItem" runat="server" OnClick="btnProcess_Click" TabIndex="500" ValidationGroup="img" CssClass="button" />

                                    </li>


                                    <li class="clear"></li>




                                </div>
                                <div style="float: left; width: 100px;">
                                </div>
                                <li class="clear"></li>

                                <div class="divSeparation"></div>



                                <div class="clear"></div>
                                <li class="clear"></li>

                                <li style="width: 920px; padding-left: 10px; height: 430px; max-height: 540px; overflow-y: auto;">


                                    <asp:GridView ID="gvItems" runat="server" CssClass="grid" DataKeyNames="ID_Stock,CurrentStock"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="100%"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical"
                                        PageSize="100">
                                        <AlternatingRowStyle BackColor="#CCE0EB" />
                                        <Columns>
                                            <asp:BoundField DataField="Store" HeaderText="Store" ItemStyle-Width="100px" />
                                            <asp:BoundField DataField="Name" HeaderText="Item" ItemStyle-Width="200px" />
                                            <asp:BoundField DataField="BatchNo" HeaderText="BatchNo" />
                                            <asp:BoundField DataField="ExpiryDate" HeaderText="ExpiryDate" />
                                            <asp:BoundField DataField="PurRate" HeaderText="PurRate" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="MRP" HeaderText="MRP" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="VAT" HeaderText="GST" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="RetailPrice" HeaderText="Retail Price" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="WholeSalePrice" HeaderText="Whole Sale Price" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="BarCode" HeaderText="BarCode" />
                                            <asp:BoundField DataField="CurrentStock" HeaderText="Old Stock" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField HeaderText="New Stock" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCurrentStock" Style="text-align: right;" TabIndex="5" onkeypress="DecimalOrIntValidation(event);" runat="server" Width="60px" Text='<%#Eval("CurrentStock") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtCurrentStock" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                                    </cc:ValidatorCalloutExtender>
                                                    <asp:HiddenField ID="hdfPacked" runat="server" Value='<%# Eval("Packed") %>' />
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



                                </li>





                                <div class="divR">


                                    <li class="clear"></li>

                                    <li class="btnMargin">
                                        <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="6" OnClientClick="disablebutn(); ShowProgress();" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" CausesValidation="false" TabIndex="7" OnClick="btnClear_Click1" />

                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                    </li>
                                </div>

                            </ul>
                        </div>

                        <div class="clear"></div>




                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                        <asp:HiddenField ID="hfStore" runat="server" />
                        <asp:HiddenField ID="hfFK_Items" runat="server" />
                        <asp:HiddenField ID="hfItem" runat="server" />
                        <asp:HiddenField ID="hfgvItemsRowIndex" runat="server" />


                        <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 300px; height: 370px;">
                            <div id="divheader" runat="server" class="PopupHeader">
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>

                            <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                        </cc:ModalPopupExtender>
                    </div>
                </div>
                <div class="loading" align="center">
                    Processing. Please wait.<br />
                    <br />
                    <asp:Image ID="Image1" runat="server" Style="margin-top: 50px;"
                        AlternateText="Image text"
                        ImageAlign="Middle"
                        ImageUrl="~/images/loader2.gif" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <script src="js/jquery-1.10.2.js"></script>
        <script src="js/Custom.js"></script>
        <script src="js/toastr.js"></script>
        <script src="js/alertify.min.js"></script>
        <link href="css/alertify.core.css" rel="stylesheet" />
        <link href="css/alertify.default.css" rel="stylesheet" />
        <script src="js/Navigation.js"></script>
        <script src="AssetLogin/js/jquery.min.js"></script>
          <style type="text/css">
            .modal {
                position: fixed;
                top: 0;
                left: 0;
                background-color: black;
                z-index: 99;
                opacity: 0.8;
                filter: alpha(opacity=80);
                -moz-opacity: 0.8;
                min-height: 100%;
                width: 100%;
            }

            .loading {
                font-family: Arial;
                font-size: 10pt;
                border: 5px solid #67CFF5;
                padding-top: 180px;
                min-height: 100%;
                width: 100%;
                display: none;
                position: fixed;
                opacity: 0.5;
                filter: alpha(opacity=50);
                -moz-opacity: 0.5;
                background-color: White;
                z-index: 999;
            }
        </style>
        <script type="text/javascript">



            var popup;
            function popUpStockSelectClick() {

                if (Page_ClientValidate("img") == true) {

                    var popup;
                    var ItemName = $("#txtitem").val();
                    if ($("#hfStore").val() == '') {

                        SetStoreDetail();
                    }

                    popup = window.open("ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val() + '&Str=' + $("#ddlStore").val(), "Popup", "width=1000,height=800");
                    popup.focus();
                }

                return false;
            }
            function popUpStockSelect() {


                if (Page_ClientValidate("img") == true) {



                    var popup;
                    var ItemName = $("#txtitem").val();
                    if (ItemName.length > 0) {
                        if ($("#hfStore").val() == '') {
                            SetStoreDetail();

                        }
                        popup = window.open("ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val() + '&Str=' + $("#ddlStore").val(), "Popup", "width=1000,height=800");
                        popup.focus();
                    }
                }

                return false;
            }


            function SetStoreDetail() {
                var store = $("#ddlStore option:selected").val();
                $("#hfStore").val(store);
            }



            function ClearAdd() {
                submit = 0;
                $("[id*='ddl']").val('0');
                $("#hfFK_Items").val('0');
                $("#txtitem").val('');
                $("#hfItem").val('');
                $("#ddlStore").focus();
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


                $("#btnSave").prop('disabled', false);

            }
            function delaysubmit() {
                try {
                    if (submit == 1) {
                        return false;

                    }

                    $("#btnSave").prop('disabled', true);

                    window.setTimeout('releasebtn()', 3000);
                    submit = 1;
                }
                catch (e) {
                    alert(e);
                }
            }



            function CheckKeys() {
                alert(e.keyCode);
            }


            function ShowAddDiv() {

                $("#divAdd").show();
                $("#divView").hide();
            }



            function pageLoad() {

                var ctrContainer = "form";
                setKeynavigation(ctrContainer);

            }




            function blockPostOnTab() {
                if (e.keyCode == 9)
                    return false;

            }
            function ShowProgress() {
                if (Page_ClientValidate("submit")) {
                    setTimeout(function () {
                        //  var modal = $('<div />');
                        //  modal.addClass("modal");
                        ////  $('body').append(modal);

                        var loading = $(".loading");
                        loading.show();
                        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                        loading.css({ top: top, left: left });
                    }, 200);
                }
            }

            function DecimalOrIntValidation(eventRef) {
                try {

                    var id = document.getElementById((eventRef.target || eventRef.srcElement).id);
                    var c = $(id).attr('id');
                    var hdfpacked = c.replace('txtCurrentStock', 'hdfPacked');
                    if ($("#" + hdfpacked).val() == "False") {

                        CheckNumWithDecimal(eventRef);
                    }
                    else {
                        CheckInteger(eventRef);
                    }
                }
                catch (e)
                { alert(e); }

            }




        </script>


    </form>
</body>
</html>
