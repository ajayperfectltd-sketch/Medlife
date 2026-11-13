using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WebPharma.Business.Masters;
using System.Web.UI.WebControls;
using WebPharma.Business.WebPharma;
using WebPharma.Business;

public partial class DayEndProcess : System.Web.UI.Page
{
    private const string DIVCOUNT = "DivCount";
    public Int16 DivCount
    {
        get { return Convert.ToInt16(this.ViewState[DIVCOUNT]); }
        set { this.ViewState[DIVCOUNT] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
                steparametetrs();
                CheckUserAccess();
                BindData();
                ddlBranch.SelectedValue = Session["mainstore"].ToString();
                lblcompanyname.Text = ddlBranch.SelectedItem.Text;
                if (Convert.ToBoolean(Session["MultiStore"].ToString()) == true)
                {
                    ddlBranch.Enabled = true;
                }
                else
                {
                    ddlBranch.Enabled = false;
                }

               txtCloseBalanceDate.Text = DateTime.Now.ToShortDateString();
               txtCloseBalanceDate.Attributes.Add("readonly", "readonly");
            }

            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlBranch, "Store", "StoName", "ID_Store", "StoName", "     Active='True'   and FK_Client = " + hfClient.Value, "");
        //populate.FillDropdownList(ddlCategory, "CategoryFirst", "Name", "ID_CategoryFirst", "Name", "  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;
        ddlBranch.Focus();
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
    }

    protected void btnScriptFill_Click(object sender, EventArgs e)
    {
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCashBalance();
        lblcompanyname.Text = ddlBranch.SelectedItem.Text;
    }
    public void PageRedirection()
    {
        Response.Redirect("Default.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        long status = 0;
        BlFunction blfunctions = new BlFunction();
        if (btnSave.Text == "Add" && DivCount==11)
        {  
            btnSave.Text = "Process";
            DivCount = 1;
            Divdayend.Style.Add("display", "none");
            divbranch.Style.Add("display", "block");
        }
         else if (btnSave.Text == "Add" && DivCount == 10)
        {
            divaccountlinking.Style.Add("display", "none");
          
            Divdayend.Style.Add("display", "none");
            divbranch.Style.Add("display", "none");
            lblFinishMessage.Text = " NO PERMISSION  FOR DAY END PROCESS ";
            divdayfinished.Style.Add("display", "block");
            btnSave.Text = "Close";
        }
        else if (String.Compare(btnSave.Text, "Close") == 0)
        {
            PageRedirection();
        }
        else if (btnSave.Text == "Process")
        {
            BlDayEnd blDayEnd = new BlDayEnd();

            blDayEnd.UserCode = hfUser.Value;
            blDayEnd.FK_Client = Convert.ToInt64(hfClient.Value);
            blDayEnd.BranchCode = Convert.ToInt16(ddlBranch.SelectedValue);
            blDayEnd.DbDate = Convert.ToDateTime(txtCloseBalanceDate.Text);

            if (validatefield())
            {

                if (hdfDayend.Value == string.Empty)
                {
                    if (hfIsAdd.Value.ToLower() == "true")
                    {

                        status = blDayEnd.InsertData();
                        if (status > 0)
                        {
                            divbranch.Style.Add("display", "none");
                            divdayfinished.Style.Add("display", "block");
                            btnSave.Text = "Close";
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                            //Populate.InsertMsg(this);
                        }
                        else
                        {
                            validatefields();
                            divbranch.Style.Add("display", "none");
                            lblFinishMessage.Text = "DAY END PROCESS FAILED";
                            divdayfinished.Style.Add("display", "block");
                            btnSave.Text = "Close";
                           // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                           // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    }
                }

            }
          
           
           
        }
    }
    protected void SetCashBalance()
    {
        BlFunction blFunctions = new BlFunction();
        //txtCashBalance.Text = blFunctions.GetScalarValue("Store", "ISNULL( OpeningBalance,0) OpeningBalance", " ID_Store=" + Convert.ToInt64(ddlBranch.SelectedValue) + "", "");

     //   txtCashBalance.Text = blFunctions.GetScalarValue("Store", "ISNULL( OpeningBalance,0) OpeningBalance", " ID_Store=" + Convert.ToInt64(ddlBranch.SelectedValue) + "", "");
       // Txtliability.Text = blFunctions.GetScalarValue("Store", "ISNULL( OpeningLiability,0) OpeningLiability", " ID_Store=" + Convert.ToInt64(ddlBranch.SelectedValue) + "", "");
        // txtCashBalance.Text = blFunctions.GetDataTable("Store", "ISNULL( OpeningBalance,0) OpeningBalance,ISNULL( OpeningLiability,0) OpeningLiability", " ID_Store=" + Convert.ToInt64(ddlBranch.SelectedValue) + "", "","");
       // txtdifference.Text = ((Convert.ToDecimal(txtCashBalance.Text)) - (Convert.ToDecimal(Txtliability.Text))).ToString();
        //txtCashBalance.Enabled = false;
       // Txtliability.Enabled = false;
        //txtdifference.Enabled = false;
        blFunctions = null;

    }
    protected bool validatefield()
    {
        bltErrorList.Items.Clear();
        if (ddlBranch.SelectedValue == "0")
        {
            bltErrorList.Items.Add("*Select Branch Type");
            return false;
        }


        return true;
    }
    protected bool validatefields()
    {
        
            bltErrorList.Items.Add("Canot process");
     


        return true;
    }
    private void steparametetrs()
    {


        hfClient.Value = Request.QueryString["clnt"];
        hfUser.Value = Request.QueryString["uid"];

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        BlFunction blfunctions = new BlFunction();
        Int16 type = Convert.ToInt16(blfunctions.GetScalarValue("Settings", "isnull(( select Value", "Name='SEC06'),0)", ""));
        ViewState["type"] = type.ToString();
        if (type > 0)
        {
            divaccountlinking.Style.Add("display", "block");
            Divdayend.Style.Add("display", "none");
            DivCount = 10;
        }
        else
        {
            divaccountlinking.Style.Add("display", "none");
            Divdayend.Style.Add("display", "block");
            DivCount = 11;
        }
       
        divbranch.Style.Add("display", "none");
        divdayfinished.Style.Add("display", "none");
      
        //  divattendance.Style.Add("display", "none");
        





    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();
        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 89;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
      
        bluseraccess.CheckUserAccess();
        if (Convert.ToBoolean(bluseraccess.IsView) == false)
        {
            // Response.Redirect("Home.aspx?");
        }
        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        bluseraccess = null;
    }
}