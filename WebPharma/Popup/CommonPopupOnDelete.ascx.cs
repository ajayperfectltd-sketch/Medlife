using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using WebPharma.Business.Masters;

public partial class Popup_CommonPopupOnDelete : System.Web.UI.UserControl
{
    #region Function
    private static Int64 k = 0;
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
    //protected void BindData()
    //{
    //    Populate populate = new Populate();
    //    populate.FillDropdownList(ddlReason, "Reason", "ReasonMode", "ID_Reason", "ReasonMode", " FK_Client=" + hfClient.Value, "");
    //}
    public void Bind()
    {
        DataTable dt = new DataTable();
        long status = 0;
        BlReason blReason = new BlReason();
        dt = blReason.SelectAllData();
       
        ddlReason.DataSource = dt;
        ddlReason.DataBind();

    }

#endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Populate populate = new Populate();
           // populate.FillDropdownList(ddlReason, "Reason", "Reason", "ID_Reason", "ID_Reason", " FK_Client=" + hfClient.Value, "");
           populate.FillDropdownList(ddlReason, "Reason", "Reason", "ID_Reason", "ID_Reason", "ReasonMode='0'"+"AND Cancelled=0", "");
           // populate = null;
            //Bind();
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string Reason = ddlReason.SelectedValue.ToString();
        ddlReason.SelectedIndex = 0;
        if (Reason=="0")
        {
            
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please Select Reason')", true);
        }
        else
        {
            this.Page.GetType().InvokeMember("DeleteMethod", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { Reason });
            k = 0;
        }
    }
}