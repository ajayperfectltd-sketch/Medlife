using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventory.Business.Accounts;


public partial class FinalAccountProcess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["dtFinalAccount"] = null;
            hfUser.Value = Request.QueryString["uid"];
            txtTransDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            hfClient.Value = Request.QueryString["clnt"];
            BindData();
            //GridFill();
            ViewState["ID_FinalAccount"] = null;
            hfRowindex.Value = null;
            hfColindex.Value = null;

        }
    }
    protected void GridFill()
    {
        ViewState["ProcessType"] = ddltype.SelectedValue.ToString() == "TD" ? "T" : ddltype.SelectedValue.ToString() == "PL" ? "P" : ddltype.SelectedValue.ToString() == "BL" ? "B" : ""; ;

        BlFinalAccounts blFinalAccounts = new BlFinalAccounts();
        DataTable dtbl = new DataTable();
        blFinalAccounts.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blFinalAccounts.TransDate = Convert.ToDateTime(txtTransDate.Text == "" ? "" + DateTime.Now.Date + "" : txtTransDate.Text);
        blFinalAccounts.Mode = ddltype.SelectedValue.ToString();

        if (Convert.ToInt64(ViewState["ID_FinalAccount"]) > 0)
        {
            blFinalAccounts.ID_FinalAccount = Convert.ToInt64(ViewState["ID_FinalAccount"]);
            dtbl = blFinalAccounts.SelectFinalAccountProcess();
        }
        else
        {
            dtbl = blFinalAccounts.SelectFinalAccounts();
        }
        if (dtbl.Rows.Count > 0)
        {
            if (ViewState["ProcessType"].ToString() != "B")
            {
                object sumCredit;
                sumCredit = dtbl.Compute("Sum(Credit)", string.Empty);
                object sumDebit;
                sumDebit = dtbl.Compute("Sum(Debit)", string.Empty);
                decimal Profit = Convert.ToDecimal(sumDebit) - Convert.ToDecimal(sumCredit);
                DataRow dr = dtbl.NewRow();
                if (Profit < 0)
                {
                    dr["Debit"] = (Math.Abs(Profit));
                    dr["Name"] = "Net Profit";
                    dr["AhCode"] = -2;
                    dr["Mode"] = "P";
                    dr["GroupType"] = ddltype.SelectedValue.ToString() == "TD" ? "T" : ddltype.SelectedValue.ToString() == "PL" ? "P" : ddltype.SelectedValue.ToString() == "BL" ? "B" : "";
                    dr["AhGroup"] = -2;
                }
                else if (Profit >= 0)
                {
                    dr["Credit"] = (Math.Abs(Profit));
                    dr["Name"] = "Net Loss";
                    dr["AhCode"] = -2;
                    dr["Mode"] = "P";
                    dr["GroupType"] = ddltype.SelectedValue.ToString() == "TD" ? "T" : ddltype.SelectedValue.ToString() == "PL" ? "P" : ddltype.SelectedValue.ToString() == "BL" ? "B" : "";
                    dr["AhGroup"] = -2;
                }
                dtbl.Rows.Add(dr);
            }
        }
        Session["dtFinalAccount"] = dtbl;
        gvReport.DataSource = dtbl;
        gvReport.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            object sumCr;
            sumCr = dtbl.Compute("Sum(Credit)", string.Empty);
            object sumDr;
            sumDr = dtbl.Compute("Sum(Debit)", string.Empty);
            TextBox footxtDebit = (TextBox)gvReport.FooterRow.FindControl("txtFtDebit");
            footxtDebit.Text = sumDr.ToString();
            TextBox footxtCredit = (TextBox)gvReport.FooterRow.FindControl("txtFtCredit");
            footxtCredit.Text = sumCr.ToString();
            blFinalAccounts = null;
            hfRowindex.Value = null;
            hfColindex.Value = null;
        }
        if (ViewState["ProcessType"].ToString() == "T")
        {
            lbDebit.Text = "Debit";
            lbCredit.Text = "Credit";
        }
        else if (ViewState["ProcessType"].ToString() == "P")
        {
            lbDebit.Text = "Expense";
            lbCredit.Text = "Income";
        }
        else if (ViewState["ProcessType"].ToString() == "B")
        {
            lbDebit.Text = "Asset";
            lbCredit.Text = "Liability";
        }
    }
    //protected void CheckUserAccess()
    //{
    //    BlUserAccess bluseraccess = new BlUserAccess();
    //    Populate populate = new Populate();

    //    bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
    //    bluseraccess.FK_Pages = 24;
    //    bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
    //    bluseraccess.CheckUserAccess();
    //    if (Convert.ToBoolean(bluseraccess.IsView) == false)
    //    {
    //        // Response.Redirect("Home.aspx?");

    //    }

    //    hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
    //    hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
    //    hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
    //    bluseraccess = null;
    //}
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "ID_Store", "     Active='True'   and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlAccountGroup, "AccountGroup", "GroupName", "ID_AccountGroup", "ID_AccountGroup", " Cancelled=0 and   FK_Client = " + hfClient.Value, "");
        populate = null;

    }
    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        ViewState["ID_FinalAccount"] = null;
        GridFill();
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["ProcessType"].ToString() == "T")
            {
                e.Row.Cells[2].Text = "Debit";
                e.Row.Cells[3].Text = "Credit";
            }
            else if (ViewState["ProcessType"].ToString() == "P")
            {
                e.Row.Cells[2].Text = "Expense";
                e.Row.Cells[3].Text = "Income";
            }
            else if (ViewState["ProcessType"].ToString() == "B")
            {
                e.Row.Cells[2].Text = "Asset";
                e.Row.Cells[3].Text = "Liability";
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdfAhcode = e.Row.FindControl("hdfAhcode") as HiddenField;
            Label lbName = e.Row.FindControl("lbName") as Label;
            TextBox txtCredit = e.Row.FindControl("txtCredit") as TextBox;
            TextBox txtDebit = e.Row.FindControl("txtDebit") as TextBox;

            if (hdfAhcode.Value.Equals("0"))
            {
                e.Row.ForeColor = System.Drawing.Color.Blue;
                txtCredit.Visible = false;
                txtDebit.Visible = false;
                e.Row.Font.Bold = true;
            }

            else if (hdfAhcode.Value.Equals("-2"))
            {
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                e.Row.Font.Bold = true;
                txtCredit.ReadOnly = true;
                txtDebit.ReadOnly = true;
                txtCredit.Font.Bold = true;
                txtDebit.Font.Bold = true;
                txtCredit.Font.Size = 10;
                txtDebit.Font.Size = 10;
                e.Row.Cells[1].ControlStyle.Font.Size = 10;
            }
            //else
            //{
            //    if (Convert.ToDecimal(txtCredit.Text) == 0 && Convert.ToDecimal(txtDebit.Text) == 0)
            //    {
            //        txtCredit.ReadOnly = false;
            //        txtDebit.ReadOnly = false;
            //    }
            //    else
            //    {
            //        txtCredit.ReadOnly = true;
            //        txtDebit.ReadOnly = true;
            //    }

            //}
            txtCredit.ReadOnly = true;
            txtDebit.ReadOnly = true;

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue.Length > 3)
            confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
        if (confirmValue == "Yes")
        {

            DataTable dtFinAc = Session["dtFinalAccount"] as DataTable; 
            BlFinalAccounts blFinalAccounts = new BlFinalAccounts();
            blFinalAccounts.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
            blFinalAccounts.TransDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            blFinalAccounts.Mode = ddltype.SelectedValue.ToString();
            blFinalAccounts.UserCode = hfUser.Value;
            blFinalAccounts.dtFinalAccount = dtFinAc;
            long i = blFinalAccounts.FinalAccountUpdate();
            hfRowindex.Value = null;
            hfColindex.Value = null;
            if (i > 0)
            {
                ViewState["ID_FinalAccount"] = i;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Account Processed Successfully');", true);
                GridFill();
                gvReport.DataSource = null;
                gvReport.DataBind();
                //Response.Redirect("FinalAccounts.aspx");
            }
        }
    }
    protected void btnAddItems_Click(object sender, EventArgs e)
    {
        DataTable dtFinAc = Session["dtFinalAccount"] as DataTable;
        DataRow dr = dtFinAc.NewRow();
        int SelectedIndex = 0;
        int RowCount = 0;
        if (ddlAccountGroup.SelectedIndex > 0)
        {
            DataRow drn = dtFinAc.AsEnumerable().SingleOrDefault(r => r.Field<String>("Name") == ddlAccountGroup.SelectedItem.ToString());


            if (drn != null)
            {
                SelectedIndex = dtFinAc.Rows.IndexOf(drn) + 1;
                dr["AhCode"] = -1;
                dr["Name"] = txtName.Text;
                try
                {
                    dr["Debit"] = txtDebit.Text;
                }
                catch
                {
                    dr["Debit"] = "0.00";
                }
                try
                {
                    dr["Credit"] = txtCredit.Text;
                }
                catch
                {
                    dr["Credit"] = "0.00";
                }
                dr["AhGroup"] = drn["AhGroup"];
                dr["SortOrder"] = drn["SortOrder"];
                dr["AccountType"] = drn["AccountType"];
                dr["Header"] = drn["Header"];
                dr["Mode"] = drn["Mode"];
                dr["GroupType"] = dr["GroupType"];

            }
            else
            {
                SelectedIndex = dtFinAc.Rows.Count + 1;
                dr["AhCode"] = -1;
                dr["Name"] = txtName.Text;
                try
                {
                    dr["Debit"] = txtDebit.Text;
                }
                catch
                {
                    dr["Debit"] = "0.00";
                }
                try
                {
                    dr["Credit"] = txtCredit.Text;
                }
                catch
                {
                    dr["Credit"] = "0.00";
                }
                dr["AhGroup"] = "0";
                dr["SortOrder"] = "0";
                dr["AccountType"] = "";
                dr["Header"] = "S";
                dr["Mode"] = "A";
                dr["GroupType"] = ViewState["ProcessType"].ToString();
            }
        }
        else
        {
            SelectedIndex = dtFinAc.Rows.Count + 1;
            dr["AhCode"] = -1;
            dr["Name"] = txtName.Text;
            try
            {
                dr["Debit"] = txtDebit.Text;
            }
            catch
            {
                dr["Debit"] = "0.00";
            }
            try
            {
                dr["Credit"] = txtCredit.Text;
            }
            catch
            {
                dr["Credit"] = "0.00";
            }
            dr["AhGroup"] = "0";
            dr["SortOrder"] = "0";
            dr["AccountType"] = "";
            dr["Header"] = "S";
            dr["Mode"] = "A";
            dr["GroupType"] = ViewState["ProcessType"].ToString();
        }

        RowCount = dtFinAc.Rows.Count;
        dtFinAc.Rows.RemoveAt(RowCount - 1);
        dtFinAc.Rows.InsertAt(dr, SelectedIndex);
        if (ViewState["ProcessType"].ToString() != "B")
        {
            object sumCredit;
            sumCredit = dtFinAc.Compute("Sum(Credit)", string.Empty);
            object sumDebit;
            sumDebit = dtFinAc.Compute("Sum(Debit)", string.Empty);
            decimal Profit = Convert.ToDecimal(sumDebit) - Convert.ToDecimal(sumCredit);
            DataRow dNr = dtFinAc.NewRow();
            if (Profit < 0)
            {
                dNr["Debit"] = (Math.Abs(Profit));
                dNr["Name"] = "Net Profit";
                dNr["AhCode"] = -2;
                dNr["Mode"] = "P";
                dNr["GroupType"] = ViewState["ProcessType"].ToString();
                dr["AhGroup"] = -2;
            }
            else if (Profit >= 0)
            {
                dNr["Credit"] = (Math.Abs(Profit));
                dNr["Name"] = "Net Loss";
                dNr["AhCode"] = -2;
                dNr["Mode"] = "P";
                dNr["GroupType"] = ViewState["ProcessType"].ToString();
                dr["AhGroup"] = -2;
            }
            dtFinAc.Rows.Add(dNr);
        }
        Session["dtFinalAccount"] = dtFinAc;
        gvReport.DataSource = dtFinAc;
        gvReport.DataBind();
        object sumCr;
        sumCr = dtFinAc.Compute("Sum(Credit)", string.Empty);
        object sumDr;
        sumDr = dtFinAc.Compute("Sum(Debit)", string.Empty);
        TextBox footxtDebit = (TextBox)gvReport.FooterRow.FindControl("txtFtDebit");
        footxtDebit.Text = sumDr.ToString();
        TextBox footxtCredit = (TextBox)gvReport.FooterRow.FindControl("txtFtCredit");
        footxtCredit.Text = sumCr.ToString();
        hfRowindex.Value = null;
        hfColindex.Value = null;
        txtCredit.Text = "";
        txtDebit.Text = "";
        ddlAccountGroup.SelectedValue = "0";
        txtName.Text = "";
    }

    protected void gvReport_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DataTable dtFinAc = Session["dtFinalAccount"] as DataTable;
        int Rowindex = Convert.ToInt32(hfRowindex.Value);
        int Colindex = Convert.ToInt32(hfColindex.Value);
        string val;
        if (Colindex == 2)
            val = ((TextBox)(gvReport.Rows[Rowindex].FindControl("txtDebit"))).Text;
        else
            val = ((TextBox)(gvReport.Rows[Rowindex].FindControl("txtCredit"))).Text;
        dtFinAc.Rows[Rowindex][Colindex] = Convert.ToDecimal(val == "" ? "0.00" : val);
        if (ViewState["ProcessType"].ToString() != "B")
        {
            int RowCount = dtFinAc.Rows.Count;
            dtFinAc.Rows.RemoveAt(RowCount - 1);

            object sumCredit;
            sumCredit = dtFinAc.Compute("Sum(Credit)", string.Empty);
            object sumDebit;
            sumDebit = dtFinAc.Compute("Sum(Debit)", string.Empty);
            decimal Profit = Convert.ToDecimal(sumDebit) - Convert.ToDecimal(sumCredit);
            DataRow dNr = dtFinAc.NewRow();

            if (Profit < 0)
            {
                dNr["Debit"] = (Math.Abs(Profit));
                dNr["Name"] = "Net Profit";
                dNr["AhCode"] = -2;
                dNr["Mode"] = "P";
                dNr["AhGroup"] = -2;
            }
            else if (Profit >= 0)
            {
                dNr["Credit"] = (Math.Abs(Profit));
                dNr["Name"] = "Net Loss";
                dNr["AhCode"] = -2;
                dNr["Mode"] = "P";
                dNr["AhGroup"] = -2;
            }
            dtFinAc.Rows.Add(dNr);
        }
        Session["dtFinalAccount"] = dtFinAc;

        gvReport.DataSource = dtFinAc;
        gvReport.DataBind();
        object sumCr;
        sumCr = dtFinAc.Compute("Sum(Credit)", string.Empty);
        object sumDr;
        sumDr = dtFinAc.Compute("Sum(Debit)", string.Empty);
        TextBox footxtDebit = (TextBox)gvReport.FooterRow.FindControl("txtFtDebit");
        footxtDebit.Text = sumDr.ToString();
        TextBox footxtCredit = (TextBox)gvReport.FooterRow.FindControl("txtFtCredit");
        footxtCredit.Text = sumCr.ToString();

    }

}