using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Inventory.Business.Accounts;
using System.Collections;
using WebPharma.Business.Accounts;

public partial class OpeningBalance : System.Web.UI.Page
{
    public void BindData()
    {


        new Populate().FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active=1 and FK_Client = " + hfClient.Value, "");
        bindAccountgroup();
        BinSubAccount();
        FillFinyear();
    }
    public void BinSubAccount()
    {

        new Populate().FillDropdownList(ddlAccountsubgrp, "AccountSubGroup", "SubGroupName", "ID_AccountSubGroup", "ID_AccountSubGroup",
            "FK_AccountGroup = " + ddlAccountgrp.SelectedValue + " and  cancelled=0   and FK_Client = " + hfClient.Value, "");

    }
    public void bindAccountgroup()
    {
        new Populate().FillDropdownList(ddlAccountgrp, "AccountGroup", "GroupName", "ID_AccountGroup", "ID_AccountGroup", "AccountType="
            + "'" + ddlAccounttype.SelectedValue + "'" + " and cancelled=0  and FK_Client = " + hfClient.Value, "");
    }
    public void FillFinyear()
    {
        DataTable dt = new DataTable();
        BlOpeningbalance Opening = new BlOpeningbalance();
        Opening.TransDate = DateTime.Now;
        //  Opening.FK_Store=
        dt = Opening.FillFinYear();
        ddlFinyear.DataSource = dt;
        ddlFinyear.DataTextField = "FinYear";
        ddlFinyear.DataValueField = "FinYear";
        ddlFinyear.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];

            //  ddlStore.SelectedValue = Session["mainstore"].ToString();
            BindData();
            ddlFinyear.Enabled = false;
            ddlStore.SelectedValue = Session["mainstore"].ToString();
        }
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {

        ClearAll();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        BlOpeningbalance blopening = new BlOpeningbalance();
        blopening.FinalGroup = Convert.ToString(ddlAccounttype.SelectedValue);
        blopening.FK_AccountGroup = Convert.ToInt32(ddlAccountgrp.SelectedValue);
        blopening.FK_AccountSubGroup = Convert.ToInt32(ddlAccountsubgrp.SelectedValue);
        blopening.FinYear = Convert.ToInt32(ddlFinyear.SelectedValue);
        dt = blopening.Accountbalancegrid();
        gdvBalance.DataSource = dt;
        gdvBalance.DataBind();
        Session["Temptable"] = dt;
        if (dt.Rows.Count > 0)
        {
            object sumCr;
            sumCr = dt.Compute("Sum(CreditBal)", string.Empty);
            object sumDr;
            sumDr = dt.Compute("Sum(DebitBal)", string.Empty);
            TextBox footxtDebit = (TextBox)gdvBalance.FooterRow.FindControl("txtFtDebit");
            footxtDebit.Text = sumDr.ToString();
            TextBox footxtCredit = (TextBox)gdvBalance.FooterRow.FindControl("txtFtCredit");
            footxtCredit.Text = sumCr.ToString();
            //blFinalAccounts = null;
            //hfRowindex.Value = null;
            //hfColindex.Value = null;
        }


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save();
        // ClearAll();
    }
    public void Save()
    {
        long status = 0;
        BlOpeningbalance blopening = new BlOpeningbalance();
        blopening.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blopening.FinYear = Convert.ToInt32(ddlFinyear.SelectedValue);
        blopening.FinalGroup = Convert.ToString(ddlAccounttype.SelectedValue);
        blopening.FK_AccountGroup = Convert.ToInt32(ddlAccountgrp.SelectedValue);
        blopening.FK_AccountSubGroup = Convert.ToInt32(ddlAccountsubgrp.SelectedValue);
        DataTable dtbl = new DataTable();
        dtbl = (DataTable)Session["Temptable"];
        AccountHeadBalance Abalance;

        ArrayList arlist = new ArrayList();
        for (int i = 0; i < gdvBalance.Rows.Count; i++)
        {
            Abalance = new AccountHeadBalance();
            Abalance.AhCode = Convert.ToInt64(((Label)gdvBalance.Rows[i].FindControl("lblCode")).Text);
            //  Abalance.AhCode = Convert.ToInt64(dtbl.Rows[i]["Code"].ToString());
            // decimal CreditBal = Convert.ToDecimal(dtbl.Rows[i]["CreditBal"].ToString());
            // decimal DebitBal = Convert.ToDecimal(dtbl.Rows[i]["DebitBal"].ToString());
            decimal CreditBal = Convert.ToDecimal(((TextBox)gdvBalance.Rows[i].FindControl("txtCreditbalance")).Text == "" ? "0" : ((TextBox)gdvBalance.Rows[i].FindControl("txtCreditbalance")).Text);
            decimal DebitBal = Convert.ToDecimal(((TextBox)gdvBalance.Rows[i].FindControl("txtDebitbalance")).Text);
            Abalance.AhBalance = Convert.ToDecimal(CreditBal - DebitBal);
            arlist.Add(Abalance);
            Abalance = null;
        }
        blopening.XmlAccountBalance = Common.WriteToXml(arlist);
        status = blopening.InsertData();
        if (status > 0)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');", true);
            cleardata();
        }
        else {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Insertion Failed');", true);
        
        }
    }


    protected void cleardata()
    {
        ddlAccounttype.SelectedValue = "ALL";
        //  ddlAccountgrp.sele
        gdvBalance.DataSource = null;
        gdvBalance.DataBind();

    }


    protected void btnbindAccountgroup_Click(object sender, EventArgs e)
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlAccountgrp, "AccountGroup", "GroupName", "ID_AccountGroup", "ID_AccountGroup", "AccountType="
          + "'" + ddlAccounttype.SelectedValue + "'" + "and cancelled=0 and FK_Client = " + hfClient.Value, "");

        //populate.FillDropdownList(ddlCategorySecond, "CategorySecond", "Name", "ID_CategorySecond", "Name", " FK_Client=" + hfClient.Value + " AND FK_CategoryFirst= " + ddlCategoryFirst.SelectedValue.ToString(), "");
        // populate = null;
        // new Populate().FillDropdownList(ddlAccountgrp, "AccountGroup", "Name", "ID_AccountGroup", "ID_AccountGroup", "AccountType="
        //  + "'" + ddlAccounttype.SelectedValue + "'" + " and Active=1 and FK_Client = " + hfClient.Value, "");

        //  bindAccountgroup();
    }
    protected void btnbindAccountSubgroup_Click(object sender, EventArgs e)
    {
        new Populate().FillDropdownList(ddlAccountsubgrp, "AccountSubGroup", "SubGroupName", "ID_AccountSubGroup", "ID_AccountSubGroup",
          "FK_AccountGroup = " + ddlAccountgrp.SelectedValue + " and Cancelled=0 and FK_Client = " + hfClient.Value, "");
    }
    protected void btnClearItems_Click(object sender, EventArgs e)
    {
        ClearAll();

    }
    public void ClearAll()
    {
        gdvBalance.DataSource = null;
        gdvBalance.DataBind();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
        {

        DataTable opdt = Session["Temptable"] as DataTable;
        int Rowindex = Convert.ToInt32(hfRowindex.Value);
        int Colindex = Convert.ToInt32(hfColindex.Value);
        string val;
        if (Colindex == 2)
            val = ((TextBox)(gdvBalance.Rows[Rowindex].FindControl("txtDebitbalance"))).Text;
        else
            val = ((TextBox)(gdvBalance.Rows[Rowindex].FindControl("txtCreditbalance"))).Text;
        opdt.Rows[Rowindex][Colindex] = Convert.ToDecimal(val == "" ? "0.00" : val);

        Session["Temptable"] = opdt;

        gdvBalance.DataSource = opdt;
        gdvBalance.DataBind();
        object sumCr;
        sumCr = opdt.Compute("Sum(CreditBal)", string.Empty);
        object sumDr;
        sumDr = opdt.Compute("Sum(DebitBal)", string.Empty);
        TextBox footxtDebit = (TextBox)gdvBalance.FooterRow.FindControl("txtFtDebit");
        footxtDebit.Text = sumDr.ToString();
        TextBox footxtCredit = (TextBox)gdvBalance.FooterRow.FindControl("txtFtCredit");
        footxtCredit.Text = sumCr.ToString();

    }
}