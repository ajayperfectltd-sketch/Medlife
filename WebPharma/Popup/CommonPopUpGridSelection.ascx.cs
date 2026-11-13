using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Popup_CommonPopUpGridSelection : System.Web.UI.UserControl
{
    private const string CalFun = "CallingFunction";
    public string CallingFunction
    {
        get
        {
            if (ViewState[CalFun] == null)
            {
                // do whatever you want here in case it's null 
                // throw an error, return string.empty or whatever
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

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.Page.GetType().InvokeMember(CallingFunction, System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { 1 });
            // hfColumn.Value = "Name";
            // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "Check();", true);
        }
        catch (Exception Ex)
        {
            Populate.ErrorMessageBox(this.Page, "Invalid Input");
        }
    }
    protected void gdvSearch_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            string strName = ((LinkButton)gdvSearch.Rows[e.NewSelectedIndex].Cells[1].FindControl("lbtnName")).Text;
            string strRefno = ((HiddenField)gdvSearch.Rows[e.NewSelectedIndex].Cells[1].FindControl("hdfIDValue")).Value;

            if (CallingFunction == "Sales")
            {

                this.Page.GetType().InvokeMember("setSalesTaxDetails", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { strName, strRefno, ((CheckBox)(gdvSearch.Rows[e.NewSelectedIndex].FindControl("ctl00"))).Checked });

            }

            else
            {
                this.Page.GetType().InvokeMember("SetDetails", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { strName, strRefno });
            }
            //  hfColumn.Value = "Name";
        }
        catch (Exception Ex)
        {
        }
    }
    protected void gdvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        gdvSearch.PageIndex = e.NewPageIndex;

        this.Page.GetType().InvokeMember(CallingFunction, System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { e.NewPageIndex });
        //this.Page.GetType().InvokeMember(ItemTaxFill, System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { 1 });
    }
    protected void btnSumbit_Click(object sender, EventArgs e)
    {
      
       // this.Page.GetType().InvokeMember("setDetails", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { 1 });

        if (CallingFunction == "Sales")
        {

            this.Page.GetType().InvokeMember("setSalesTaxDetails", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { 1 });
            //this.Page.GetType().InvokeMember("setSalesTaxDetails", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { strName, strRefno, ((CheckBox)(gdvSearch.Rows[e.NewSelectedIndex].FindControl("ctl00"))).Checked });

        }

        else
        {
            this.Page.GetType().InvokeMember("setDetails", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { 1 });
           
        }





    }
}