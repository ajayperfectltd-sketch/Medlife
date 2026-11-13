using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Popup_CommonPopup : System.Web.UI.UserControl
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
        
        lblErrorMsg.Visible = false;
     //   ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "Check();", true);
        if (!IsPostBack)
        {
            // GridFill(1);

               
            //}
        }
    }
    protected void gdvSearch_RowCreated(object sender, GridViewRowEventArgs e)
    {

        try
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
           
            
        }
        catch (Exception ex)
        {
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {

       
   //  ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Check();", true);

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

            if (this.Page.Form.Name == "frmSalesReturn" && CallingFunction == "BillNoFill")
            {
               // bool val = Convert.ToBoolean(gdvSearch.Rows[e.NewSelectedIndex].Cells[gdvSearch.Columns.Count - 1].Text);
                // ctl00 is checkbox identity if u added one more boolean binding the id may change
                this.Page.GetType().InvokeMember("SaleDetail", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { strName, strRefno, ((CheckBox)(gdvSearch.Rows[e.NewSelectedIndex].FindControl("ctl00"))).Checked });

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
    protected void gdvSearch_PreRender(object sender, EventArgs e)  
    {
        try
        {
                int i = 0;
                if (gdvSearch.HeaderRow != null)
                {
                    foreach (TableCell tc in gdvSearch.HeaderRow.Cells)
                    {

                        if (i != 0 && i != 2 && i != 3 && i != 4)
                        {
                            string STR = hfColumn.Value;
                            CheckBox cbx = new CheckBox();
                            Label lbl = new Label();
                            lbl.Text = gdvSearch.HeaderRow.Cells[i].Text;
                            if (STR != string.Empty)
                            {
                                cbx.Checked = (lbl.Text.Trim() == STR.Trim());
                                popuplabel.Text = STR;

                            }
                            cbx.ID = "cbxGridSearch_" + (i).ToString();
                            lbl.ID = "lblGridName_" + i.ToString();


                            cbx.CssClass = "cbxHeader";
                            cbx.Attributes.Add("onchange", "Check();");

                            lbl.CssClass = "lblHeader";
                            tc.Controls.Add(cbx);
                            tc.Controls.Add(lbl);
                            cbx.Visible = false;

                        }
                        //int A = gdvSearch.HeaderRow.Cells.GetCellIndex(tc);
                        //if (lb != null && (!lb.Text.Equals("Date")))
                        //{
                        //    tc.ApplyStyle(s);
                        //}

                        i++;
                    }
                    if (this.Page.Form.Name == "frmSalesReturn" && CallingFunction == "BillNoFill")
                    {
                        gdvSearch.HeaderRow.Cells[1].Text = "BillNo";
                    }

                }
          
        }
        catch (Exception EX)
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
}