<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonPopupOnDelete.ascx.cs" Inherits="Popup_CommonPopupOnDelete" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<div align="center" style="border: 1px solid #88D2FF; height: 109px; width: 398px; padding-bottom: 0px; background-color: #d9e0da; border: 1px solid #0f3a5d; padding-bottom: 50px; box-shadow: 10px 10px 32px #888; border-radius: 0px 0px 2px 2px;">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <%-- <div id="divouterline" align="left">--%>

    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <script src="js/toastr.js"></script>
    <style>
        .popupdiv {
            float: left;
            padding-top: 10px;
            padding-left: 16px;
            width: 95%;
        }
    </style>
    <div class="popupdiv">
        <li>
            <asp:Label ID="Label19" runat="server" Text="Reason"></asp:Label>
            <asp:DropDownList ID="ddlReason" DataTextField="Reason" DataValueField="ID_Reason" runat="server" CssClass="dropdown" TabIndex="23"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Reason required" ControlToValidate="ddlReason" Display="None" InitialValue="0" SetFocusOnError="True" ValidationGroup="sbmtDel"></asp:RequiredFieldValidator>
            <cc:validatorcalloutextender id="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" enabled="True" targetcontrolid="RequiredFieldValidator1">
                                        </cc:validatorcalloutextender>
        </li>
        <br />

        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" ValidationGroup="sbmtDel" CausesValidation="true" OnClick="btnConfirm_Click" CssClass="button" Style="margin-left: 10px;" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="button" OnClientClick="return ClearPopu8SearchReason();" />
        <%--<asp:Label ID="lblErrorMsg" runat="server" Text="Invalid Input " ForeColor="Red"></asp:Label>--%>
    </div>
    <asp:HiddenField ID="hfClient" runat="server" />
    <asp:HiddenField ID="hfUser" runat="server" />
    <div class="clear"></div>
    <br />
    <asp:HiddenField ID="hfColumn" runat="server" />


</div>
