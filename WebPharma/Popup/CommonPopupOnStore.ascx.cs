using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business;

public partial class Popup_CommonPopupOnStore : System.Web.UI.UserControl
{
    private static Int64 k=0;
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
        btnDelete.Enabled = true;
        kk.Visible = false;
        if (!IsPostBack)
        {
       
            Populate populate = new Populate();
            // populate.FillDropdownList(ddlStore, "Store", "Name", "ID_Store", "Name", "     Active='True'   and FK_Client = " + hfClient.Value, "");
            populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Cancelled=0 AND  Active='True' ", "");


            populate = null;
        }

              
        else
        {
            if (string.IsNullOrEmpty((string)Session["incriment"]))
          
            {
                Session["incriment"] = 0;
            }
          

        //    k = Convert.ToInt64(Session["incriment"].ToString() == null?"0":Session["incriment"].ToString() );
            Session["incriment"] = (Convert.ToInt16(Session["incriment"]) + 1).ToString();
            if (Convert.ToInt16(Session["incriment"]) == 1)
            {
                if (!string.IsNullOrEmpty(Session["storetype"] as string))
                {

                    string type = Session["storetype"].ToString();
                    BlFunction blfunctions = new BlFunction();


                    Int16 type1 = Convert.ToInt16(blfunctions.GetScalarValue("Users", " isnull((select FK_Store", "Active=1 and ID_Users=" + Convert.ToInt16(type) + "),0)", ""));
                    ddlStore.SelectedValue = type1.ToString();
                    Session["incriment"] = (Convert.ToInt16(Session["incriment"].ToString()) + 1).ToString(); ;
              
                }
            }
        }
        if (ddlStore.SelectedValue == "0")
        {
            btnDelete.Enabled = false;
            kk.Visible = true;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string store = ddlStore.SelectedValue.ToString();
        ddlStore.SelectedIndex = 0;
        this.Page.GetType().InvokeMember("Storeselect", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { store });
        k = 0;
       


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string store = "0";
        ddlStore.SelectedIndex = 0;
        Session["incriment"] = "0";
        this.Page.GetType().InvokeMember("Storeselect", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { store });
    }
    //////protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    //////{
    //////    string store = "0";
    //////    ddlStore.SelectedIndex = 0;
    //////    this.Page.GetType().InvokeMember("Storeselect", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { store });

    //////}
}