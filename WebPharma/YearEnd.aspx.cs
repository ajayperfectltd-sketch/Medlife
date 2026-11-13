/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Storage 
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebPharma.Business.Masters;


public partial class YearEnd1 : System.Web.UI.Page
{
    #region Function
    protected void BindData()
    {
        Populate populate = new Populate();
       populate.FillDropdownListAll(ddlOutlet, "Store", "StoName", "ID_Store", "StoName", "Active='True'  and FK_Client = " + hfClient.Value, "");

        populate = null;
        ddlOutlet.Focus();
    }
    protected void GridFill(int currentPage)
    {

        txtToDate.Text = "31/03/" + DateTime.Now.Year;
        txtFromDate.Text = "01/04/" + Convert.ToInt32(DateTime.Now.Year - 1);
        //hftxtToDate.Text = "31/03/" + DateTime.Now.Year;
        //hftxtFromDate.Text = "01/04/" + Convert.ToInt32(DateTime.Now.Year - 1);
        //txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //txtAsOnDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtCurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //txtsddate.Text = DateTime.Now.ToString("dd/MM/yyyy");

        BlYearEnd blYearEnd = new BlYearEnd();
        DataTable dtbl = new DataTable();
        // blYearEnd.ShortName = txtSearchShortName.Text.Trim();
        // blYearEnd.Name = txtSearchName.Text.Trim();
        blYearEnd.FK_Client = Convert.ToInt64(hfClient.Value);
        blYearEnd.PageIndex = Convert.ToInt16(currentPage);
        blYearEnd.UserCode = hfUser.Value;
        // dtbl = blYearEnd.SelectAllData();
        //  gvColor.DataSource = dtbl;
        //gvColor.DataBind();
        //// if (dtbl.Rows.Count > 0)
        //  {
        //    int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
        //     this.PopulatePager(recordCount, currentPage);
        //  }
        blYearEnd = null;
    }



    protected void SaveData()
    {
        DivRH.Style.Add("display", "block");
        //long status = 0;
        BlYearEnd blYearEnd = new BlYearEnd();
        blYearEnd.FK_Outlet = Convert.ToInt64(ddlOutlet.SelectedValue.ToString());
        blYearEnd.FromDate = Convert.ToDateTime(txtFromDate.Text);
        blYearEnd.ToDate = Convert.ToDateTime(txtToDate.Text);
        blYearEnd.FK_Client = Convert.ToInt64(hfClient.Value);
        blYearEnd.UserCode = hfUser.Value;
        blYearEnd.StockUpdateMode = chkStockUpdate.Checked;
        

        if (ViewState["YearEndProcessID"] != null)
        {

            short i = Convert.ToInt16(ViewState["YearEndProcessID"]);
            i++;
            ViewState["YearEndProcessID"] = i;


            if (i <= 6)
            {
                DataTable dt = new DataTable();

                dt = blYearEnd.InsertYearEnd(i);
                btnSubmit.Style["visibility"] = "hidden";
                btnClear.Style["visibility"] = "hidden";
                txtToDate.Enabled = false;
                txtFromDate.Enabled = false;
                int statuscode = Convert.ToInt32(dt.Rows[0][0].ToString());
                if (statuscode > 0)
                {
                    switch (statuscode)
                    {
                        case 1:
                            loop1.Attributes.Remove("class");
                            loop1.Attributes.Add("class", "tick");
                            //loop1.Attributes.CssStyle.Add("background-color", "red");
                            break;
                        case 2:
                            loop2.Attributes.Remove("class");
                            loop2.Attributes.Add("class", "tick");
                            btnSubmit.Text = "processing2";
                            break;
                        case 3:
                            loop3.Attributes.Remove("class");
                            loop3.Attributes.Add("class", "tick");
                            btnSubmit.Text = "processing3";
                            break;
                        case 4:
                            loop4.Attributes.Remove("class");
                            loop4.Attributes.Add("class", "tick");
                            btnSubmit.Text = "processing4";
                            break;
                        case 5:
                            loop5.Attributes.Remove("class");
                            loop5.Attributes.Add("class", "tick");
                            btnSubmit.Text = "processing5";
                            break;
                        case 6:
                            loop5.Attributes.Remove("class");
                            loop5.Attributes.Add("class", "tick");
                            /// btnSubmit.Text = "processing6";
                            Btnfinish.Style.Add("display", "block");
                            pan1.Style.Add("display", "block");
                            break;
                    }
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Inserted Successfully');ClearAdd();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ProcessClick();", true);





                }

                else if (statuscode < 0)
                {
                    switch (statuscode)
                    {
                        case -1:
                            loop1.Attributes.Remove("class");
                            loop1.Attributes.Add("class", "failed");
                            loop2.Attributes.Remove("class");
                            loop2.Attributes.Add("class", "exclamation");
                            loop3.Attributes.Remove("class");
                            loop3.Attributes.Add("class", "exclamation");
                            loop4.Attributes.Remove("class");
                            loop4.Attributes.Add("class", "exclamation");
                            loop5.Attributes.Remove("class");
                            loop5.Attributes.Add("class", "exclamation");

                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Error In Purchase')", true);
                            break;
                        case -2:
                            loop2.Attributes.Remove("class");
                            loop2.Attributes.Add("class", "failed");
                            loop3.Attributes.Remove("class");
                            loop3.Attributes.Add("class", "exclamation");
                            loop4.Attributes.Remove("class");
                            loop4.Attributes.Add("class", "exclamation");
                            loop5.Attributes.Remove("class");
                            loop5.Attributes.Add("class", "exclamation");
                            btnSubmit.Text = "processing-2";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Error In Purchase Return')", true);
                            break;
                        case -3:
                            loop3.Attributes.Remove("class");
                            loop3.Attributes.Add("class", "failed");
                            loop4.Attributes.Remove("class");
                            loop4.Attributes.Add("class", "exclamation");
                            loop5.Attributes.Remove("class");
                            loop5.Attributes.Add("class", "exclamation");

                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Error In Sales')", true);
                            break;
                        case -4:
                            loop4.Attributes.Remove("class");
                            loop4.Attributes.Add("class", "failed");
                            loop5.Attributes.Remove("class");
                            loop5.Attributes.Add("class", "exclamation");

                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Error In Sales Return')", true);

                            break;
                        case -5:
                            loop5.Attributes.Remove("class");
                            loop5.Attributes.Add("class", "failed");
                           
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Error In Indent')", true);
                            break;
                      
                        //case -7:
                        //    loop6.Attributes.Remove("class");
                        //    loop6.Attributes.Add("class", "failed");
                        //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Error In Indent')", true);

                        //    break;
                    }

                    if (statuscode == -8)
                    {
                        pan3.Style.Add("display", "block");
                      // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Financial Year Already Exists')", true);
                        loop1.Attributes.Remove("class");
                        loop1.Attributes.Add("class", "exclamation");
                        loop2.Attributes.Remove("class");
                        loop2.Attributes.Add("class", "exclamation");
                        loop3.Attributes.Remove("class");
                        loop3.Attributes.Add("class", "exclamation");
                        loop4.Attributes.Remove("class");
                        loop4.Attributes.Add("class", "exclamation");
                        loop5.Attributes.Remove("class");
                        loop5.Attributes.Add("class", "exclamation");
                        

                    }
                    else if (statuscode == -545)
                    {
                        pan4.Style.Add("display", "block");
                     //  ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Transaction already exists for this period ')", true);

                    }



                    pan2.Style.Add("display", "block");
                  //  ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Transaction Error')", true);
                    ViewState["YearEndProcessID"] = 0;
                    Btnclose.Style.Add("display", "block");
                    //GridFill(1);




                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('internal server Error')", true);


                }





            }
            else
            {
                ViewState["YearEndProcessID"] = 0;
            }


        }





      


    }

    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 81;
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
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
             if (!IsPostBack)
            {
                Page.Header.DataBind();
                //blMessage.Items.Clear();
                // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                Security security = new Security();
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
              
                Common cmn = new Common();
               // CheckUserAccess();
                BindData();
                GridFill(1);
                ViewState["YearEndProcessID"] = 0;

            }
        }
        catch (Exception ex)
        {
            //  blMessage.Items.Add(ex.Message);
        }
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            // blMessage.Items.Clear();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            // blMessage.Items.Clear();

            //  System.Threading.Thread.Sleep(5000);
            SaveData();
            // btnSubmit.Text = "processing123";


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    #endregion
    protected void Btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlOutlet.SelectedValue = "0";
        var year = DateTime.Now.Year;
        var lastyear = (DateTime.Now.Year) - (1);
        var ToYear = "31/03/" + year;
        var FromYear = "01/04/" + lastyear;
        txtCurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtToDate.Text = ToYear.ToString();
        txtFromDate.Text = FromYear.ToString();
        chkStockUpdate.Checked = false;
    }
}