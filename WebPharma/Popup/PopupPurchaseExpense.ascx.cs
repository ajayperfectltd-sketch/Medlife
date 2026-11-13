using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Popup_PopupPurchaseExpense : System.Web.UI.UserControl
{
    private const string CalFun = "CallingFunction";
    decimal TotalAdd,TotalLess;
    
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
    protected void btnSumbit_Click(object sender, EventArgs e)
    {

        if (CallingFunction == "PurchaseExpense")
        {

            this.Page.GetType().InvokeMember("setExpenseDetails", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { 1 });
           

        }
    }
    protected void gdvSearch_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        this.Page.GetType().InvokeMember("setExpenseDetails", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { 1 });
    }
   
    public void btnTotalAmount_Click(object sender, EventArgs e)
    {
        
            calculateTotalAmount();
        
       
    }

    public void calculateTotalAmount()
    {
        foreach (GridViewRow row in gdvSearch.Rows)
        {
            string AddLess = ((HiddenField)row.Cells[0].FindControl("hfAddLess")).Value.ToString();
            if (Convert.ToString(AddLess) == "Add")
            {
                string Amount = ((TextBox)row.Cells[0].FindControl("txtAmount")).Text;
                TotalAdd += Convert.ToDecimal(Amount == "" ? "0" : Amount);
            }
            else if (Convert.ToString(AddLess) == "Less")
            {
                string Amount = ((TextBox)row.Cells[0].FindControl("txtAmount")).Text;
                TotalLess += Convert.ToDecimal(Amount == "" ? "0" : Amount);
            }
            hfTotalExpense.Value = Convert.ToString(TotalAdd - TotalLess);
            txtTo.Text = hfTotalExpense.Value;

        }
       // calculateTotalAmount();

    }
    //public void calculatAmount()
    //{
    //    GridView gvNew = (GridView)FindControl("gdvSearch");
        
    //    foreach (GridViewRow row in gdvSearch.Rows)
    //    {
    //        string AddLess = ((HiddenField)row.Cells[0].FindControl("hfAddLess")).Value;
    //        if (Convert.ToBoolean(AddLess) == true)
    //        {
    //            string Amount = ((TextBox)row.Cells[0].FindControl("txtAmount")).Text;
    //            TotalAdd += Convert.ToDecimal(Amount == "" ? "0" : Amount);
    //        }
    //        else if (Convert.ToBoolean(AddLess) == false)
    //        {
    //            string Amount = ((TextBox)row.Cells[0].FindControl("txtAmount")).Text;
    //            TotalLess += Convert.ToDecimal(Amount == "" ? "0" : Amount);
    //        }
    //        hfTotalExpense.Value = Convert.ToString(TotalAdd - TotalLess);
    //        txtTo.Text = hfTotalExpense.Value;

    //    }

    //}


    //public string  TotalAmount()
    //{
    //    string result = string.Empty;
    //    EventArgs e=new EventArgs();
    //    object sender=new object();
    //   // btnTotalAmount_Click(sender, e);
    //    calculateTotalAmoun();
    //    return result;
    //}

    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {

        this.Page.GetType().InvokeMember("setExpenseDetails", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { 1 });
    }


    protected void gdvSearch_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        //gdvSearch.PageIndex = e.NewPageIndex;
        //this.Page.GetType().InvokeMember("setExpenseDetails", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { 1 });
       // this.Page.GetType().InvokeMember("CallingFunction", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { 1 });
    }
}