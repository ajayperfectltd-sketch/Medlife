<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemRearrange.aspx.cs" Inherits="ItemRearrange" EnableEventValidation="false" %>

<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>

<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc2" TagName="MedLifePageHelp" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Item Rearrange::</title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/Navigation.js"></script>
    <script src="js/toastr.js"></script>
    <%--<script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>--%>

    <style>
        .dropdown {
            margin-top: 8px !important;
        }

        .panel-info > .panel-heading {
            color: #31708f;
            background-color: #2171a2;
            border-color: #bce8f1;
        }

        .panel-heading {
            padding: 0px 0px;
            border-bottom: 1px solid transparent;
            border-bottom-color: transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }

        .panelnav-tabs {
            border-bottom: 1px solid #ddd;
        }

        .panelnav {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            padding-left: 0;
            margin-bottom: 0;
            list-style: none;
            float: left;
            background-color: #12b4b0;
            width: 100%;
        }

        .panelnav-tabs > li {
            float: left;
            margin-bottom: -1px;
        }

            .panelnav-tabs > li.active > a, .panelnav-tabs > li.active > a:hover, .panelnav-tabs > li.active > a:focus {
                color: #555;
                cursor: default;
                background-color: #fff;
                border: 1px solid #ddd;
                border-bottom-color: rgb(221, 221, 221);
                border-bottom-color: rgb(221, 221, 221);
                border-bottom-color: transparent;
            }

        .panelnav > li > a {
            position: relative;
            display: block;
            padding: 10px 15px;
            color: #fff;
        }

        .panelnav-tabs > li > a {
            margin-right: 2px;
            line-height: 1.42857143;
            border: 1px solid transparent;
            border-radius: 4px 4px 0 0;
            text-decoration: none;
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

        function CheckBoxAllCheck(chk) {
            var checked = $(chk).prop("checked");
            $(".chkbx").find("input:checkbox").prop('checked', checked)



        }
        //function popUpItemsSelect() {

        //    try {

        //        var ItemName = $("#txtitem").val();
        //        if (ItemName.length > 0) {

        //            $("#ImageButton3").click();
        //        }

        //    }
        //    catch (e) {
        //        alert(e);
        //    }

        //}


    </script>
</head>
<body>
    <form id="frmItemRearangement" runat="server" autocomplete="off">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5">
                    <div class="heading2">
                        <div class="user">Item Rearrange</div>

                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">
                            <div id="divAdd" runat="server">
                                <div style="width: 950px">
                                    <div class="divL">
                                        <ul class="contentul">
                                            <div class="divL" style="width: 380px;">


                                                <%--<div class="panel-heading">

                                                    <ul class="panelnav panelnav-tabs" style="width: 100%">
                                                        <li aria-expanded="false" class="active" id="liTab1" runat="server">
                                                            <asp:LinkButton ID="lnkTab1" Text="Tab1" runat="server" class="active" aria-expanded="true" OnClick="lnkTab1_Click">
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li aria-expanded="false" class="" id="liTab2" runat="server">
                                                            <asp:LinkButton ID="lnkTab2" Text="Tab2" runat="server" class="" aria-expanded="true" OnClick="lnkTab2_Click">
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li aria-expanded="false" class="" id="liTab3" runat="server">
                                                            <asp:LinkButton ID="lnkTab3" Text="Tab3" runat="server" class="" aria-expanded="true" OnClick="lnkTab3_Click">
                                                            </asp:LinkButton>
                                                        </li>
                                                    </ul>
                                                    <asp:ImageButton ID="ibtnExcelClose" runat="server"
                                                        Style="float: right; padding-right: 10px; padding-top: 5px;"
                                                        ToolTip="Close" Visible="false"
                                                        ImageUrl="~/images/cross.png"
                                                        OnClientClick="HideDivOnButtonClick();return false;" />

                                                </div>--%>
                                                <li>
                                                    <label class="col-sm-3 col-form-label  label">Unit</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="Drop-control dropdown" TabIndex="1"></asp:DropDownList>
                                                    </div>
                                                </li>
                                                <li class="clear"></li>
                                                <li>
                                                    <asp:Label ID="Label9" runat="server" CssClass="col-sm-3 col-form-label label" Text="Rack"></asp:Label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlRack" runat="server" CssClass="Drop-control dropdown" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlRack_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </li>
                                                <li class="clear"></li>
                                                <%--<li>
                                                        <label class="col-md-3 col-form-label label">Storage</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlStorage" runat="server" CssClass="Drop-control dropdown" TabIndex="1"></asp:DropDownList>
                                                        </div>
                                                    </li>--%>
                                                <li class="clear"></li>
                                                <li>

                                                    <label class="col-sm-4 col-form-label label">Category</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="1" CssClass="Drop-control dropdown"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </li>
                                                <li class="clear"></li>
                                                <li>
                                                    <asp:Label ID="Label3" runat="server" CssClass="col-sm-4 col-form-label label" Text="Sub Category"></asp:Label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlCategorySecond" runat="server" CssClass="Drop-control dropdown" TabIndex="1"></asp:DropDownList>
                                                    </div>
                                                </li>
                                                <li class="clear"></li>


                                                <%--<li>
                                                        <asp:Label ID="Label8" runat="server" CssClass="col-sm-4 col-form-label label" Text="Sub Group"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlSubGroup" runat="server" CssClass="Drop-control dropdown" TabIndex="1"></asp:DropDownList>
                                                        </div>
                                                    </li>--%>
                                                <li class="clear"></li>
                                                <li>
                                                <%--  <label class="col-md-3 col-form-label label"></label>
                                                                        <div class="col-md-8">

                                                                            <asp:CheckBox ID="chkStopSale" runat="server" TabIndex="1" Text="Stop Sales" />

                                                                        </div>--%>

                                                <asp:Label ID="lblStopSale" runat="server" CssClass="label" Text="Stop Sale"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:CheckBox ID="chkStopSale" onkeydown="EnterTab(event,'chkPacked');" Style="padding-right: 2px" runat="server" Width="15" TabIndex="17" />

                                                </div>

                                            </li>
                                            <li class="clear"></li>
                                            <li>

                                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Packed"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:CheckBox ID="chkPacked" onkeydown="EnterTab(event,'chkOnlineitem');" Style="padding-right: 2px" runat="server" Width="15" TabIndex="17" />

                                                </div>



                                                <%-- <label class="col-md-3 col-form-label label"></label>
                                                                        <div class="col-md-8">
                                                                           <asp:CheckBox ID="chkPacked" runat="server" TabIndex="1" Text="" /> 
                                                                        </div>--%>
                                            </li>
                                            <li class="clear"></li>
                                               
                                            </div>
                                        </ul>
                                    </div>
                                    <div class="divR">
                                        <ul class="contentul">
                                            <li>
                                                <asp:Label ID="Label32" runat="server" CssClass="col-sm-3 col-form-label label" Text="Manufacturer"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlManufacturer" runat="server" CssClass="Drop-control dropdown" TabIndex="1"></asp:DropDownList>
                                                </div>
                                            </li>
                                            <li class="clear"></li>
                                            <li>
                                                <asp:Label ID="Label5" runat="server" CssClass="col-sm-3 col-form-label label" Text="Bin"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlBin" runat="server" CssClass="Drop-control dropdown" TabIndex="1"></asp:DropDownList>
                                                </div>
                                                <%-- <asp:Label ID="Label5" runat="server" CssClass="col-sm-3 col-form-label label" Text="Brand"></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlBrand" runat="server" CssClass="Drop-control dropdown" TabIndex="1"></asp:DropDownList>
                                            </div>--%>
                                            </li>
                                            <li class="clear"></li>
                                            <li>
                                                <%--  <asp:Label ID="Label16" runat="server" CssClass="col-sm-3 col-form-label label" Text="Color"></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlColor" runat="server" CssClass="Drop-control dropdown" TabIndex="1"></asp:DropDownList>
                                            </div>--%>

                                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="Generic"></asp:Label>
                                                <asp:DropDownList ID="ddlGeneric" runat="server" AutoPostBack="true" CssClass="dropdown" TabIndex="6"></asp:DropDownList>

                                            </li>

                                            <li class="clear"></li>
                                            <%--<li>
                                            <label class="col-md-3 col-form-label label">Size</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlSize" runat="server" CssClass="Drop-control dropdown" TabIndex="1"></asp:DropDownList>
                                            </div>
                                        </li --%>
                                            <li class="clear"></li>
                                            



                                             <li>
                                                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-4 col-form-label label" Text="Item"></asp:Label>

                                                    <asp:TextBox runat="server" CssClass="form-control textbox" ID="txtitem" MaxLength="20" Width="185px" AutoComplete="off"></asp:TextBox> 
                                                    <asp:ImageButton ID="IbtnItemAdd" runat="server" OnClick="IbtnItemAdd_Click" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 5px 0px 0px 0px; position: absolute;" />

                                                </li>
                                                <li class="clear"></li>
                                                <li style="display:none">
                                                    <asp:Label ID="Label4" runat="server" CssClass="col-sm-4 col-form-label label" Text="Barcode"></asp:Label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtBarcode" CssClass="form-control textbox" TabIndex="1" runat="server"></asp:TextBox>
                                                    </div>
                                                </li>
                                                <li class="clear"></li>





















                                            <%--<li>
                                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Online item"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:CheckBox ID="chkOnlineitem" onkeydown="EnterTab(event,'btnSubmit');" Style="padding-right: 2px" runat="server" Width="15" TabIndex="17" />
                                            </li>--%>
                                            <li class="clear"></li>
                                        </ul>
                                    </div>

                                    <li class="clear"></li>
                                    <li class="btnMargin" style="padding-left: 160px; padding-top: 15px">
                                        <asp:Button Text="Show" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" TabIndex="1" CssClass="btn btn-info button" />




                                         <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                                        <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                                            <div id="divheader" runat="server" class="PopupHeader">
                                                <asp:Label ID="lblPopupHeader" runat="server" Text="Item" CssClass="lblPopupHeader"></asp:Label>
                                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                            </div>
                                            <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                                        </asp:Panel>
                                        <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader"  >
                                        </cc:ModalPopupExtender>
                                    </li>
                                </div>
                                <ul class="contentul">

                                    <li>
                                        <asp:Label ID="Label17" runat="server" CssClass="col-sm-3 col-form-label label" Text="Change Type"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlChangeType" runat="server" TabIndex="1" CssClass="Drop-control dropdown"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlChangeType_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Category" Value="1"></asp:ListItem>
                                                 <asp:ListItem Text="Manufacture" Value="2"></asp:ListItem>
                                                 <asp:ListItem Text="Rack" Value="3"></asp:ListItem>
                                                 <asp:ListItem Text="Unit" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Stop Sale" Value="5"></asp:ListItem><%--8--%>
                                                <asp:ListItem Text="Packed" Value="6"></asp:ListItem><%--9--%>
                                                <asp:ListItem Text="Generic" Value="7"></asp:ListItem><%--10*/--%>
                                            </asp:DropDownList>
                                        </div>
                                    </li>
                                    <li class="clear"></li>
                                    <li>
                                        <div class="form-group row SpaceT" id="DivData" runat="server">
                                            <asp:Label ID="lblData" runat="server" CssClass="col-sm-3 col-form-label label" Text="Data"></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlData" runat="server" TabIndex="1" CssClass=" dropdown"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlData_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </li>
                                    <li class="clear"></li>
                                    <li>

                                        <div class="form-group row SpaceT" id="DivSubData" runat="server">
                                            <asp:Label ID="lblSubData" runat="server" CssClass="col-sm-3 col-form-label label" Text="Sub"></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSubData" runat="server" TabIndex="1" CssClass="Drop-control dropdown">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </li>
                                    <li class="clear"></li>
                                    <li>
                                        <div class="form-group row SpaceT" id="DivChecked" runat="server">
                                            <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 col-form-label label" Text=""></asp:Label>
                                            <asp:Label ID="lblChecked" runat="server" CssClass="col-sm-3 col-form-label" Text="" Visible="false"></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:CheckBox ID="chkChecked" runat="server" TabIndex="1" Text="" />
                                            </div>
                                        </div>
                                    </li>
                                    <li class="clear"></li>
                                    <li>
                                        <div class="form-group row SpaceT" id="DivReason" runat="server">
                                            <asp:Label ID="Label10" runat="server" CssClass="col-sm-3 col-form-label label" Text="Reason"></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtReason" CssClass="form-controlMandatory textboxMandatory" runat="server" TabIndex="3"
                                                    Rows="2" cols="20" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </li>

                                </ul>
                                <ul class="contentul">
                                    <div id="divitems" runat="server" style="height: 450px; width: 950px; overflow: auto; scrollbar-face-color: blue;">
                                        <asp:GridView ID="gvItemdetails" runat="server" CssClass="grid"
                                            EmptyDataText="No Data Found!" Width="900px" AutoGenerateColumns="False" Style="margin-left: 5px;" DataKeyNames="ID_Items"
                                            OnRowCommand="gvItemdetails_RowCommand"
                                            BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100">
                                            <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server" Text="All" onclick="CheckBoxAllCheck(this)" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkBox" ItemStyle-HorizontalAlign="Center" runat="server" onclick="CheckBoxCheck(this);" CssClass="chkbx" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SLNO" HeaderText="SLNO" Visible="false" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                                <asp:BoundField HeaderText="Category" DataField="CategoryName" />
                                                <asp:BoundField HeaderText="Sub Category" DataField="SubCategoryName" />

                                                <asp:BoundField HeaderText="Rack Name" DataField="RackName" />
                                                <asp:BoundField HeaderText="Generic" DataField="GenericName" />
                                                                                <asp:BoundField HeaderText="Unit Name" DataField="UnitName" />
                                <%--<asp:BoundField HeaderText="Stock Center" DataField="Stockcenter" />--%>
                                                <asp:BoundField HeaderText="Manufacture" DataField="Manufactur" />
                                                <asp:BoundField HeaderText="Bin" DataField="Bin" />
                                                <asp:BoundField HeaderText="Packed" DataField="Packed" />
                                                <asp:BoundField HeaderText="Stop Sale" DataField="StopSale" />
                                                <asp:BoundField HeaderText="Stop Sale Remarks" DataField="StopSaleRemarks" />
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle CssClass="hederdesgn" Font-Bold="True" ForeColor="White" Height="30px" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </div>
                                </ul>
                                <ul class="contentul">
                                    <li>
                                        <asp:Button Text="Submit" ID="btnProcess" runat="server" ValidationGroup="Process" OnClick="btnProcess_Click" TabIndex="7" CssClass="btn btn-info button" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-info button" Text="Clear" OnClick="btnClear_Click"   TabIndex="7" />

                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                        <asp:HiddenField ID="hfStore" runat="server" />
                                        <asp:HiddenField ID="hfFK_Items" runat="server" />
                                        <asp:HiddenField ID="hfItem" runat="server" />
                                    </li>
                                </ul>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/toastr.js"></script>

    </form>
</body>
</html>
