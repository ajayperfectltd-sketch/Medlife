<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonPopupPrivilege.ascx.cs" Inherits="Popup_CommonPopupPrivilege" %>

<style>
    .gridp {
        width: auto;
        font-family: tahoma;
        border: solid 3px #66babd;
    }

    .txtSearch {
        border-radius: 5px;
        font-family: Arial;
        padding: 2px 3px 2px 3px;
        margin-top: 16px;
        margin-left: 15px;
    }

    .btnSearchPopup {
        /*background-color: #0E3B62;
        color: white;*/
        padding: 2px,3px,2px,3px;
        font-family: Arial;
        font-weight: bold;
        border-radius: 5px;
        background-color: #07928f;
        color: #FFF;
        width: 86px;
        height: 25px;
        text-align: center;
        border: none;
        cursor: pointer;
        float: inherit;
    }

    .popupdiv {
        float: left;
        /*padding-top: 10px;
        padding-left: 16px;*/
        width: 95%;
    }


    .divSeparation {
        width: 96%;
        background-color: #0D3B5F;
        margin: 5px 0px;
        height: 2px;
    }

    .lblHeader {
        width: 100px;
        margin-top: 0px;
        padding: 0px 0px 0px 0px;
    }



    .cbxHeader {
        width: auto;
        height: 10px;
        margin: 0px 0px 0px 0px;
        float: left;
        padding: 0px 0px 0px 0px;
        position: relative;
    }

    .divContainer {
        width: 97%;
        height: 320px;
        overflow: auto;
    }

    .GridViewItemStyle {
        width: 97%;
        height: 320px;
        overflow: auto;
    }

    .popuplabel {
        float: left;
        margin-left: 0px;
        font-family: tahoma;
        font-size: 12px;
        font-weight: bold;
        padding-top: 5px;
        padding-right: 6px;
        padding-bottom: 14px;
    }
</style>
<div align="Center" style="border: 1px solid #88D2FF; height: 10%; width: 508px; padding-bottom: 0px; background-color: #d9e0da; border: 1px solid #12361D; padding-bottom: 50px; box-shadow: 10px 10px 32px #888; border-radius: 0px 0px 2px 2px;">
    
    <div style="padding-top: 10px; padding-left: 16px; width: 95%;">
        <asp:Label ID="popuplabel" runat="server" Text="Name" CssClass="popuplabel"></asp:Label>
        <asp:Label ID="lblErrorMsg" runat="server" Text="Invalid Input " ForeColor="Red" style="display:none"></asp:Label>
    </div>

    <div class="clear"></div>
    <br />  
</div>

