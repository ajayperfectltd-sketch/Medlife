using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Popup_CommonPopupPrivilege : System.Web.UI.UserControl
{
    private const string CalFun = "CallingFunction";
    public string CallingFunction
    {
        get
        {
            if (ViewState[CalFun] == null)
            {
               
            }
            return ViewState[CalFun].ToString();
        }
        set
        {
            ViewState[CalFun] = value;
        }
    }
 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMsg.Visible = true; 
        if (!IsPostBack)
        { 
        }
    }
    
    protected void Page_Changed(object sender, EventArgs e)
    {
         
    } 
}