<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonPopupOnStore.ascx.cs" Inherits="Popup_CommonPopupOnStore" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<style>.cd-dropdown, .cd-select {

    position: relative;
    width: 100%;
    margin: 20px auto; height:40px; font-family:Arial, Helvetica, sans-serif; background:#ececec; color: #333;
    display: block;

}

.wrappar2{
    max-width: 532px;
    height:410PX; background:#FFF;
    margin: 140px auto 0px auto;
    padding: 0px auto 0px auto;
    
    -webkit-box-shadow: -2px 0px 15px 3px rgba(0,0,0,0.21);
    -moz-box-shadow: -2px 0px 15px 3px rgba(0,0,0,0.21);
    box-shadow: -2px 0px 15px 3px rgba(0,0,0,0.21);
    border-radius: 4px 4px 4px 4px;
    -moz-border-radius: 4px 4px 4px 4px;
    -webkit-border-radius: 4px 4px 4px 4px;
    border: 0px solid #000000;
}

ul, li {
    margin: 0px;
    list-style-type: none;
    margin-left: 30px;}

.login_fr_bg{width:400px; height:200px; margin:0px; padding:0px; float:left; }
.login100-form-title { font-family:Arial, Helvetica, sans-serif;
    
    font-size: 20px;
    color: #2aaba2;
    line-height: 1.2;
    text-transform: uppercase;
    letter-spacing: 2px;
    text-align: center;
    width: 100%;
    display: block;

}



.label {

    width: 135px;
    float: left;
    padding-left: 2px;
    font-size: 13px;
    color: #0C3C64;
    font-family: tahoma;
    margin-top: 8px;

}


.login100-form-btn1 {
    display: -webkit-box;
    display: -webkit-flex;
    display: -moz-box;
    display: -ms-flexbox;
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 0 20px;
    margin-top: 40px;
    width: 100%;
    height: 50px;
    border-radius: 3px;
    background:#b80c0c;
    -webkit-box-shadow: 0px 0px 17px 0px rgba(0,0,0,0.36);
    -moz-box-shadow: 0px 0px 17px 0px rgba(0,0,0,0.36);
    box-shadow: 0px 0px 17px 0px rgba(0,0,0,0.36);
    font-family: Montserrat-Bold;
    font-size: 8px;
    color: #FFF;
    line-height: 1.2;
    text-transform: uppercase;
    letter-spacing: 1px;
    -webkit-transition: all 0.4s;
    -o-transition: all 0.4s;
    -moz-transition: all 0.4s;
    transition: all 0.4s;
}

[type="reset"], [type="submit"], button, html [type="button"] {
    -webkit-appearance: button;
}
input {
    outline: none;
    border: none;
}
button, input {
    overflow: visible;
}
button, input, optgroup, select, textarea {
    margin: 0;
        margin-top: 0px;
    font-family: inherit;
    font-size: inherit;
    line-height: inherit;
}
[role="button"], a, area, button, input, label, select, summary, textarea {
    -ms-touch-action: manipulation;
    touch-action: manipulation;
}

.store_heading{width:100%; height:10px; margin:0px; text-align:center; padding-top:26px;  font-family:Arial, Helvetica, sans-serif; font-size:24px; color:#828282; }
</style>


 <%--/*------------------------------------------------------------------------------------------------------- */--%>

 <div class="container">
    
    <div class="row">
    
    <div class="col-12 col-md-12 col-xs-12 col-sm-12">
    
    <div class="wrappar2">
    <div class="col-12 col-md-12 col-xs-12 col-sm-12">
     <div class="store_heading">STORE</div>
     <div class="col-5 col-md-5 col-xs-5 col-sm-5">

						
					
     <div class="login_fr_bg">
     
     <ul>
                                <li>
                                       <span class="login100-form-title ">
					
                        	
                                    <span id="CommonPopupOnStore_Label12" class="label" style="color:white; width:70px">Store</span>

                                              <asp:DropDownList ID="ddlStore" CssClass="cd-select" runat="server" TabIndex="4" AutoPostBack="false">
                                    </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="rfvStore" runat="server" ErrorMessage="Select a Store" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Confirm"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="vceStore" runat="server" CssClass="customCalloutStyle" Enabled="True" TargetControlID="rfvStore">
                                    </cc:ValidatorCalloutExtender>

                                
                                  <span id="CommonPopupOnStore_rfvStore" style="display:none;"></span>
                                    <input type="hidden" name="CommonPopupOnStore$vceStore_ClientState" id="CommonPopupOnStore_vceStore_ClientState">
                            
				  </span>
                                </li>
                                
                               <li >
                                  
                                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click"  ValidationGroup="Confirm"
                                    style="text-align: center; margin-top:42px;" Text="Confirm"   CssClass="login100-form-btn" />

                                    <asp:Button ID="btnCancel"  runat="server" onclick="btnCancel_Click"
                                    style="text-align: center; margin-top: 23px; font-size:17px; font-family:Arial, Helvetica, sans-serif;" Text="Close"   CssClass="login100-form-btn1"  />
                               
                               </li>
         <li style="height:50px">
               <div id="kk" runat="server" visible="false">
                                  <asp:Label ID="Label2" runat="server" CssClass="label" style="color:#e94d10; font-weight: bold;  width:300px;padding-left: 35px;" Text=" Close The Window And Login Once Again" ></asp:Label>
                                </div>
         </li>
                                  <div class="login3"><img src="images/fr_logo.jpg" width="141" height="52" style=" margin-left:142px; margin-top:1px;"></div>
                                </ul>
          
     </div>
     </div>
     <div class="col-7 col-md-7 col-xs-7 col-sm-7">
    
   
     </div></div>
    
    </div>
    
    
    
    </div>
    </div>
    
    </div>
  <asp:HiddenField ID="hfUser" runat="server" />
         
     