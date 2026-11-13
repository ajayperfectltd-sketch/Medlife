using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business;
using WebPharma.Business.Masters;


public partial class AdminDashBoardDetailed : System.Web.UI.Page
{
    private void Search()
    {

        DataTable dtbl = AccessRight();

        if (dtbl.Rows.Count > 0)
        {
          //  divAnimate.Visible = true;
            // divSearch.Visible = true;
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                switch (Convert.ToInt64(dtbl.Rows[i][0]))
                {
                    case 1: column2.Visible = true;
                        ddlGraphType1.Items.Add(new ListItem("Customer Performance", "CUST"));
                        divGeneral.Attributes.Add("style", "display:block");
                        break;
                    case 2: column3.Visible = true;
                        ddlGraphType1.Items.Add(new ListItem("Store Wise Sales", "STR"));
                        divGeneral.Attributes.Add("style", "display:block");
                        break;
                    case 3: column1.Visible = true;
                        ddlGraphType1.Items.Add(new ListItem("Top Selling Item", "ITM"));
                        divGeneral.Attributes.Add("style", "display:block");
                        break;
                    case 4: column4.Visible = true;
                        ddlGraphType1.Items.Add(new ListItem("Minimum Stock Variation", "MSW"));
                        divGeneral.Attributes.Add("style", "display:block");
                        break;
                    case 5: column5.Visible = true;
                        ddlGraphType1.Items.Add(new ListItem("Credit Supplier", "CRPW"));
                        divGeneral.Attributes.Add("style", "display:block");
                        break;
                    case 6: column6.Visible = true;
                        ddlGraphType1.Items.Add(new ListItem("Credit Customer", "CRCW"));
                        divGeneral.Attributes.Add("style", "display:block");
                        break;
                    case 7: column7.Visible = true;
                        ddlComparisonal.Items.Add(new ListItem("Purchase Compare", "Purchase"));
                        divCompare.Attributes.Add("style", "display:block");
                        break;
                    case 8: column8.Visible = true;
                        ddlComparisonal.Items.Add(new ListItem("Sales Compare", "Sales"));
                        divCompare.Attributes.Add("style", "display:block");
                        break;
                    case 9: column9.Visible = true;
                        ddlGraphType1.Items.Add(new ListItem("Sales Man Performance", "SLMW"));
                        divGeneral.Attributes.Add("style", "display:block");
                        break;

                }
            }

        }
      //  else
          //  divAnimate.Visible = false;



    }
    private DataTable AccessRight()
    {
        BlDashBoard bldashboard = new BlDashBoard();
        bldashboard.FK_Users = Convert.ToInt64(hfUser.Value.Split(',')[0]);
        bldashboard.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);


        return bldashboard.DashBoardAccessRight();

    }
    

    private void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownListDashBoard(ddlItemStore, "Store", "StoName", "ID_Store", "ID_Store", "Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");

        populate = null;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            txtPurStFromDate.Attributes.Add("readonly", "readonly");
            txtPurStEnDate.Attributes.Add("readonly", "readonly");
            txtPurFrmDate.Attributes.Add("readonly", "readonly");
            txtPurToDate.Attributes.Add("readonly", "readonly");
            BindData();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", "ClearSearch();", true);
            Search();
            //hdfCounter.Value = GetAdminDashBoard();

        }

    }
}