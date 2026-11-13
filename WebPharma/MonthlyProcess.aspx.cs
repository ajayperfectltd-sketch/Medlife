/*---------------------------------------------------------------------
  Purpose   : Code-behind – Monthly Process wizard (5 steps)
  Author    : (your name)
  Inherits  : MonthlyProcess (MonthlyProcess.aspx)
  Pattern   : Same as YearEnd.aspx.cs
  Namespace : WebPharma (root)
---------------------------------------------------------------------*/
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using System.Web.UI.HtmlControls;

public partial class MonthlyProcess : System.Web.UI.Page
{
    #region ── Page Events ─────────────────────────────────────────────

    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));

        try
        {
            if (!IsPostBack)
            {
                Page.Header.DataBind();

                // Populate session identifiers from query string (same as YearEnd)
                hfUser.Value   = Request.QueryString["uid"]  ?? "";
                hfClient.Value = Request.QueryString["clnt"] ?? "";

                CheckUserAccess();
                BindStoreDropdown();

                // Initialise wizard at step 1
                ViewState["CurrentStep"] = 1;
                LoadStep1();
                ShowStep(1);
            }
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    #endregion

    #region ── Button Events ────────────────────────────────────────────

    // Store change → reload Step 1 dates
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        try { LoadStep1(); }
        catch (Exception ex) { ShowError(ex.Message); }
    }

    protected void cvRemarks_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = (args.Value ?? string.Empty).Length <= 500;
    }

    // Next / Start
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;
        int step = CurrentStep;
        try
        {
            if (step == 1 && !ValidateStep1()) return;

            step++;
            switch (step)
            {
                case 2: LoadStep2(); break;
                case 3: LoadStep3(); break;
                case 4: ViewState["RectifyCount"] = 0; LoadStep4(); break;
                case 5: LoadStep5(); break;
            }

            ViewState["CurrentStep"] = step;   // only commit AFTER the load succeeds
            ShowStep(step);
        }
        catch (Exception ex)
        {
            ShowStep(CurrentStep);   // re-render whatever step we're actually still on
            ShowError(ex.Message);
        }
    }

    // Back
    // AFTER
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            int step = CurrentStep - 1;
            if (step < 1) step = 1;
            ViewState["CurrentStep"] = step;

           
            btnNext.Enabled = true;

            ShowStep(step);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    // AFTER
    protected void btnRectify_Click(object sender, EventArgs e)
    {
        try
        {
            BlMonthlyProcess bl = BuildBlFromStep1();

            // Run auto-fix
            bl.RunDataIntegrityCheck(0);   // mode 0 = all checks

            // Re-check pending items after auto-fix
            DataTable dt = bl.GetPendingChecks();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "FK_Status = 0";
            DataTable failed = dv.ToTable();
            bool hasPending = failed.Rows.Count > 0;

            // Track rectify attempts for this session/period
            int rectifyCount = ViewState["RectifyCount"] != null
                ? Convert.ToInt32(ViewState["RectifyCount"]) + 1
                : 1;
            ViewState["RectifyCount"] = rectifyCount;

            // Refresh the grid/labels via existing loader
            LoadStep4();

            if (!hasPending)
            {
                ViewState["RectifyCount"] = 0;
                lblRectifyMsg.Text = "All issues resolved. You may proceed.";
                lblRectifyMsg.ForeColor = System.Drawing.Color.FromArgb(0x15, 0x57, 0x24);
                btnRectify.Style.Add("display", "none");
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;

                if (rectifyCount >= 3)
                {
                    // Log errors to DB after 3 failed rectify attempts
                    bl.LogMonthlyProcessErrors();

                    string refId = string.Format("{0}-{1}-{2}",
                        SelectedStore,
                        hfProcessDate.Value.Replace("/", ""),
                        hfUser.Value.ToUpper());

                    lblRectifyMsg.Text =
                        "Some issues could not be auto-resolved after multiple attempts. " +
                        "This appears to be a system issue rather than something you can fix here. " +
                        "Our support team has been notified. " +
                        "Please contact support with Reference ID: " + refId;
                    lblRectifyMsg.ForeColor = System.Drawing.Color.FromArgb(0x72, 0x1c, 0x24);

                    btnRectify.Style.Add("display", "none");
                }
                else
                {
                    lblRectifyMsg.Text =
                        failed.Rows.Count + " item(s) still need attention. " +
                        "Click Rectify again to retry.";
                    lblRectifyMsg.ForeColor = System.Drawing.Color.FromArgb(0x85, 0x64, 0x04);

                    btnRectify.Style.Add("display", "inline-block");
                }
            }

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "RectMsg",
                "toastr.clear(); toastr.info('Rectify completed. Verification refreshed.');", true);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }
    // Process (Step 5) – executes monthly close
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIsAdd.Value.ToLower() != "true")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Perm",
                    "toastr.warning('You do not have permission to execute period close process.');", true);
                return;
            }

            BlMonthlyProcess bl = BuildBlFromHiddenFields();
            bl.UserAction         = 1;
            bl.ID_MonthlyProcess  = long.Parse(hfMonthlyProcessID.Value);

            DataTable dt = bl.SaveMonthlyProcess();
            long result  = dt.Rows.Count > 0 ? Convert.ToInt64(dt.Rows[0][0]) : -1;

            blErrors.Items.Clear(); 

            if (result > 0)
            {
                hfMonthlyProcessID.Value = result.ToString();
                panSuccess.Style.Add("display", "block");
                panFailed.Style.Add("display",  "none");
                blErrors.Style.Add("display", "none"); 

               
                btnProcess.Style.Add("display", "none");
                btnBack.Style.Add("display",    "none");
                btnNext.Style.Add("display",    "none");
                btnRectify.Style.Add("display", "none");
                btnClose.ToolTip = "Log out";

                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Succ",
                //    "toastr.success('Period Close Process completed successfully.');", true);
            }
            else
            {
                // Hide generic failed panel
                panFailed.Style.Add("display", "none");
                panSuccess.Style.Add("display", "none");

                string msg;
                switch (result)
                {
                    case -1:
                        msg = "Period close process record already exists for this period.";
                        break;

                    case -2:
                        msg = "Previous period is not yet closed. Close it first.";
                        break;

                    case -3:
                        msg = "Cannot close a future period.";
                        break;

                    case -4:
                        msg = "Remarks cannot exceed 500 characters.";
                        break;

                    case -5:
                        msg = "This period is out of sequence with the last closed period. Please close periods in order.";
                        break;

                   case -99:
                    msg = "An unexpected database error occurred while saving. Please contact support.";
                         break;

                    default:
                        msg = "An unexpected error occurred.";
                        break;
                }

                blErrors.Items.Clear();
                //blErrors.Items.Add(msg);
                //blErrors.Style.Add("display", "block");

                ScriptManager.RegisterClientScriptBlock(
                    this,
                    GetType(),
                    "Fail",
                    "toastr.error('" + msg + "');",
                    true);
            }
        }
        catch (Exception ex)
        {
            panFailed.Style.Add("display", "none");
            blErrors.Items.Clear();
            //blErrors.Items.Add(ex.Message);
            //blErrors.Style.Add("display", "block");
            ShowError(ex.Message);
        }
    }

    // Close → redirect home
    protected void btnClose_Click(object sender, EventArgs e)
    {
        PageRedirection();
    }


    public void PageRedirection()
    {
        Session.Clear();
        Session.Abandon();

        if (Request.Cookies["ASP.NET_SessionId"] != null)
        {
            HttpCookie sessionCookie = new HttpCookie("ASP.NET_SessionId", "");
            sessionCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(sessionCookie);
        }

        System.Web.Security.FormsAuthentication.SignOut();
        if (Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName] != null)
        {
            HttpCookie authCookie = new HttpCookie(
                System.Web.Security.FormsAuthentication.FormsCookieName, "");
            authCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(authCookie);
        }

        // Break out of the frameset entirely and reload the whole browser window at Default.aspx
        ScriptManager.RegisterStartupScript(this, GetType(), "LogoutRedirect",
            "top.location.href = 'Default.aspx';", true);
    }
    #endregion

    #region ── Step Loaders ────────────────────────────────────────────

    /// <summary>Step 1: Load From/To/FinYear for selected store.</summary>
    private void LoadStep1()
    {
        // Process date is always today's date, regardless of store selection
        txtProcessDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

        if (ddlStore.SelectedValue == "0")
        {
            lblFromDate.Text = "-";
            lblToDate.Text = "-";
            lblFinYear.Text = "";
            lblMonthStatus.Text = "";
            btnNext.Enabled = true;
            hfFromDate.Value = "";
            hfToDate.Value = "";
            hfFinYear.Value = "";
            hfIsFuturePeriod.Value = "0";
            return;
        }

        BlMonthlyProcess bl = new BlMonthlyProcess();
        bl.FK_Store  = SelectedStore;
        bl.FK_Client = FK_Client;

        DataTable dt = bl.GetActiveBusinessDates();
        if (dt == null || dt.Rows.Count == 0) return;

        DataRow row = dt.Rows[0];

        string fromDate = row["FromDate"] != DBNull.Value
            ? Convert.ToDateTime(row["FromDate"]).ToString("dd/MM/yyyy") : "-";
        string toDate   = row["ToDate"] != DBNull.Value
            ? Convert.ToDateTime(row["ToDate"]).ToString("dd/MM/yyyy")   : "-";
        int    finYear  = row["FinYear"] != DBNull.Value
            ? Convert.ToInt32(row["FinYear"]) : 0;
        bool   isClosed = row["IsClosed"] != DBNull.Value
            && Convert.ToBoolean(row["IsClosed"]);

        DateTime toDateVal;
        bool isFuturePeriod = row["ToDate"] != DBNull.Value  && DateTime.TryParse(row["ToDate"].ToString(), out toDateVal) && toDateVal.Date > DateTime.Now.Date;

        lblFromDate.Text    = fromDate;
        lblToDate.Text      = toDate;
        lblFinYear.Text     = finYear > 0 ? finYear.ToString() : "N/A";
        //lblMonthStatus.Text = isClosed
        //    ? "<span style='color:#721c24;font-weight:600;'>Closed</span>"
        //    : "<span style='color:#155724;font-weight:600;'>Open</span>";
        //lblMonthStatus.Text = isClosed
        //    ? "<span style='color:#721c24;font-weight:600;'>Already Closed</span>"
        //    : "<span style='color:#155724;font-weight:600;'>Open - Ready to Process</span>";

         if (isClosed)
        {
            lblMonthStatus.Text = "<span style='color:#721c24;font-weight:600;'>Already Closed</span>";
        }
        else if (isFuturePeriod)
        {
            lblMonthStatus.Text = "<span style='color:#856404;font-weight:600;'>Period Not Yet Elapsed</span>";
        }
        else
        {
            lblMonthStatus.Text = "<span style='color:#155724;font-weight:600;'>Open - Ready to Process</span>";
        }

        btnNext.Enabled = !isFuturePeriod;

        // Pre-fill process date with To Date
        //if (string.IsNullOrEmpty(txtProcessDate.Text))
        //    txtProcessDate.Text = toDate;
      
        // Persist for later steps
        hfFromDate.Value = fromDate;
        hfToDate.Value   = toDate;
        hfFinYear.Value  = finYear.ToString();
        hfIsFuturePeriod.Value = isFuturePeriod ? "1" : "0";
    }

    /// <summary>Step 2: Transaction summary grids + hidden liability/stock values.</summary>
    private void LoadStep2()
    {


        // Save dates from Step 1 fields into hidden fields
        hfProcessDate.Value = txtProcessDate.Text.Trim();

        BlMonthlyProcess bl = BuildBlFromStep1();

        // --- Transaction amounts (result set 0) ---
        DataSet ds = bl.GetTransactionDetails();

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataTable dtTrans = ds.Tables[0];

            // Totals footer row
            decimal totalNet = 0, totalLiab = 0;
            foreach (DataRow r in dtTrans.Rows)
            {
                totalNet  += Convert.ToDecimal(r["NetAmount"]);
                totalLiab += Convert.ToDecimal(r["Liability"]);

                // Cache per-row hidden values for Step 5 SP call
                switch (r["Name"].ToString())
                {
                    case "Purchase":
                        hfPurchaseAmount.Value    = r["NetAmount"].ToString();
                        hfPurchaseLiability.Value = r["Liability"].ToString();
                        break;
                    case "Purchase Return":
                        hfPReturnAmount.Value    = r["NetAmount"].ToString();
                        hfPReturnLiability.Value = r["Liability"].ToString();
                        break;
                    case "Sales":
                        hfSalesAmount.Value    = r["NetAmount"].ToString();
                        hfSalesLiability.Value = r["Liability"].ToString();
                        break;
                    case "Sales Return":
                        hfSReturnAmount.Value    = r["NetAmount"].ToString();
                        hfSReturnLiability.Value = r["Liability"].ToString();
                        break;
                }
            }

            // Add totals row to display table
            DataRow totRow    = dtTrans.NewRow();
            totRow["Name"]    = "TOTAL";
            totRow["NetAmount"] = totalNet;
            totRow["Liability"] = totalLiab;
            dtTrans.Rows.Add(totRow);

            gvTransactions.DataSource = dtTrans;
            gvTransactions.DataBind();
        }

        // --- Indent details (result set 1) ---
        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            DataRow r = ds.Tables[1].Rows[0];
            lblIndentTo.Text   = string.Format("{0:N2}", Convert.ToDecimal(r["CurrentLiability"]));
            lblIndentFrom.Text = string.Format("{0:N2}", Convert.ToDecimal(r["PreviousLiability"]));
            hfIndentTo.Value   = r["CurrentLiability"].ToString();
            hfIndentFrom.Value = r["PreviousLiability"].ToString();
        }

        // --- Damage stock (result set 2) ---
        if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
        {
            string dmg         = ds.Tables[2].Rows[0]["DamageStock"].ToString();
            lblDamageStock.Text = string.Format("{0:N2}", Convert.ToDecimal(dmg));
            hfDamageStock.Value = dmg;
        }

        // --- Hidden: ClosingLiability + StockValue ---
        DataTable dtGrid = bl.GetGridDetails();
        if (dtGrid != null && dtGrid.Rows.Count > 0)
        {
            hfClosingLiability.Value = dtGrid.Rows[0]["ClosingLiability"].ToString();
            hfStockValue.Value       = dtGrid.Rows[0]["StockValue"].ToString();
        }

        lblStep2Period.Text = hfFromDate.Value + " to " + hfToDate.Value;
    }

    /// <summary>Step 3: Cash details from DayCashBook + AccountHeadSubTransaction.</summary>
    // AFTER
    private void LoadStep3()
    {
        BlMonthlyProcess bl = BuildBlFromStep1();

        // Background calc — still required for MonthlyClose persistence in Step 5,
        // not shown on screen anymore.
        DataSet dsCash = bl.GetPaymentReceiptDetails();
        decimal opening = 0, payment = 0, receipt = 0;

        if (dsCash.Tables.Count > 0 && dsCash.Tables[0].Rows.Count > 0)
            opening = Convert.ToDecimal(dsCash.Tables[0].Rows[0][0]);
        if (dsCash.Tables.Count > 1 && dsCash.Tables[1].Rows.Count > 0)
            payment = Convert.ToDecimal(dsCash.Tables[1].Rows[0]["Payment"]);
        if (dsCash.Tables.Count > 2 && dsCash.Tables[2].Rows.Count > 0)
            receipt = Convert.ToDecimal(dsCash.Tables[2].Rows[0]["Receipt"]);

        decimal closing = opening + receipt - payment;

        hfOpening.Value = opening.ToString();
        hfReceipt.Value = receipt.ToString();
        hfPayment.Value = payment.ToString();
        hfClosing.Value = closing.ToString();

        // Displayed content: liability movement + payment mode breakdown
        DataSet ds = bl.GetCashLiabilitySummary();

        decimal prevLiability = 0;
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0
            && ds.Tables[0].Rows[0]["PreviousClosingLiability"] != DBNull.Value)
            prevLiability = Convert.ToDecimal(ds.Tables[0].Rows[0]["PreviousClosingLiability"]);

        decimal currLiability = ParseDecimal(hfClosingLiability.Value);
        decimal liabilityChange = currLiability - prevLiability;
        decimal liabilityChangePct = prevLiability != 0
            ? (liabilityChange / prevLiability) * 100
            : 0;

        lblPrevLiability.Text = string.Format("{0:N2}", prevLiability);
        lblCurrLiability.Text = string.Format("{0:N2}", currLiability);
        lblLiabilityChange.Text = string.Format("{0:N2}", liabilityChange);
        lblLiabilityChangePct.Text = string.Format("{0:N2}%", liabilityChangePct);

        lblLiabilityChange.ForeColor = liabilityChange >= 0
            ? System.Drawing.Color.FromArgb(0x72, 0x1c, 0x24)   // liability increased — flag
            : System.Drawing.Color.FromArgb(0x15, 0x57, 0x24);  // liability decreased — good

        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            gvPaymentModes.DataSource = ds.Tables[1];
            gvPaymentModes.DataBind();
        }
        else
        {
            gvPaymentModes.DataSource = null;
            gvPaymentModes.DataBind();
        }
    }

    /// <summary>Step 4: Verification checks – show only failed rows.</summary>
    private void LoadStep4()
    {
        BlMonthlyProcess bl = BuildBlFromStep1();
        DataTable dt = bl.GetPendingChecks();

        if (dt == null)
        {
            lblStep4Info.Text = "Unable to run verification checks. Please contact support.";
            gvChecks.Visible = false;
            divStep4Clear.Style.Add("display", "none");
            btnNext.Enabled = false;
            return;
        }


        // Filter to only failed rows for display
        DataView dv     = dt.DefaultView;
        dv.RowFilter    = "FK_Status = 0";
        DataTable failed = dv.ToTable();

        bool hasErrors = failed.Rows.Count > 0;
        hfHasPendingErrors.Value = hasErrors ? "1" : "0";

        if (hasErrors)
        {
            gvChecks.Visible    = true;
            divStep4Clear.Style.Add("display", "none");
           lblStep4Info.Text =
         "<span style='color:#721c24;font-weight:600;'>" + failed.Rows.Count + " issue(s) found.</span> " + "Resolve them using the Rectify button or correct manually, then re-check.";

            gvChecks.DataSource = failed;
            gvChecks.DataBind();

            //// Colour the Status column
            //foreach (GridViewRow row in gvChecks.Rows)
            //{
            //    if (row.DataItem == null) continue;

            //    DataRowView drv = row.DataItem as DataRowView;
            //    if (drv == null || drv["ColorCode"] == DBNull.Value) continue;

            //    int colorCode = Convert.ToInt32(drv["ColorCode"]);
            //    Label lbl = (Label)row.FindControl("lblStatus");
            //    if (lbl == null) continue;

            //    switch (colorCode)
            //    {
            //        case 1:
            //            lbl.Text = "OK";
            //            lbl.CssClass = "check-ok";
            //            break;
            //        case 2:
            //            lbl.Text = "Warning";
            //            lbl.CssClass = "check-warn";
            //            break;
            //        default:
            //            lbl.Text = "Error";
            //            lbl.CssClass = "check-error";
            //            break;
            //    }
            //}

            // Block Next; show Rectify
            btnNext.Enabled = false;
            btnRectify.Style.Add("display", "inline-block");
        }
        else
        {
            gvChecks.Visible = false;
            divStep4Clear.Style.Add("display", "block");
            lblStep4Info.Text = "All verification checks passed.";
            btnNext.Enabled   = true;
            btnRectify.Style.Add("display", "none");
        }
    }

    // AFTER
    protected void gvChecks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;

        DataRowView drv = e.Row.DataItem as DataRowView;
        if (drv == null || drv["ColorCode"] == DBNull.Value) return;

        int colorCode = Convert.ToInt32(drv["ColorCode"]);
        Label lbl = (Label)e.Row.FindControl("lblStatus");

        switch (colorCode)
        {
            case 1:
                if (lbl != null) { lbl.Text = "OK"; lbl.CssClass = "check-ok"; }
                e.Row.CssClass = "check-row-ok";
                break;
            case 2:
                if (lbl != null) { lbl.Text = "Warning"; lbl.CssClass = "check-warn"; }
                e.Row.CssClass = "check-row-warn";
                break;
            default:
                if (lbl != null) { lbl.Text = "Error"; lbl.CssClass = "check-error"; }
                e.Row.CssClass = "check-row-error";
                break;
        }
    }

    /// <summary>Step 5: Populate recap labels from hidden fields.</summary>
    private void LoadStep5()
    {
         
        lblRecapStore.Text = ddlStore.SelectedItem != null ? ddlStore.SelectedItem.Text : "";
        lblRecapFinYear.Text   = hfFinYear.Value;
        lblRecapFrom.Text      = hfFromDate.Value;
        lblRecapTo.Text        = hfToDate.Value;
        lblRecapProcess.Text   = hfProcessDate.Value;
        lblRecapLiability.Text = string.Format("{0:N2}", ParseDecimal(hfClosingLiability.Value));
        lblRecapStock.Text     = string.Format("{0:N2}", ParseDecimal(hfStockValue.Value));
        lblRecapOpening.Text   = string.Format("{0:N2}", ParseDecimal(hfOpening.Value));
        lblRecapClosing.Text   = string.Format("{0:N2}", ParseDecimal(hfClosing.Value));
        lblRecapRemarks.Text   = txtRemarks.Text.Trim();
    }

    #endregion

    #region ── Step Navigation & Progress Bar ──────────────────────────

    private void ShowStep(int step)
    {
        // Hide all panels
        divStep1.Style.Add("display", "none");
        divStep2.Style.Add("display", "none");
        divStep3.Style.Add("display", "none");
        divStep4.Style.Add("display", "none");
        divStep5.Style.Add("display", "none");
        // Show target
        switch (step)
        {
            case 1: divStep1.Style.Add("display", "block"); break;
            case 2: divStep2.Style.Add("display", "block"); break;
            case 3: divStep3.Style.Add("display", "block"); break;
            case 4: divStep4.Style.Add("display", "block"); break;
            case 5: divStep5.Style.Add("display", "block"); break;
        }
        // Progress bar states
        SetStepProgress(prgStep1, step, 1);
        SetStepProgress(prgStep2, step, 2);
        SetStepProgress(prgStep3, step, 3);
        SetStepProgress(prgStep4, step, 4);
        SetStepProgress(prgStep5, step, 5);
        // Button visibility
        btnBack.Style.Add("display", step > 1 && step < 5 ? "inline-block" : "none");
        btnNext.Style.Add("display", step < 5 ? "inline-block" : "none");
        btnProcess.Style.Add("display", step == 5 ? "inline-block" : "none");
        // Rectify only relevant in Step 4 when errors exist
        if (step != 4)
            btnRectify.Style.Add("display", "none");
        // Validation group only needed on Step 1 Next
        btnNext.ValidationGroup = step == 1 ? "vgStep1" : "";

        // Reset Close tooltip to default; only the post-Process success branch overrides this
        btnClose.ToolTip = "Exit without saving and log out";

        hfCurrentStep.Value = step.ToString();
    }


    private void SetStepProgress(HtmlGenericControl ctrl, int current, int stepNo)
    {
        // Remove existing classes and set active/done/pending
        string css = current == stepNo ? "mp-step active"
                   : current >  stepNo ? "mp-step done"
                   :                     "mp-step";
        ctrl.Attributes["class"] = css;
    }

    #endregion

    #region ── Validation ──────────────────────────────────────────────


    private bool ValidateStep1()
    {
        if (ddlStore.SelectedValue == "0")
        {
            ShowError("Please select a store.");
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtProcessDate.Text))
        {
            ShowError("Please enter the process date.");
            return false;
        }

        //if (txtRemarks.Text.Trim().Length > 500)
        //{
        //    ShowError("Remarks cannot exceed 500 characters.");
        //    return false;
        //}


        DateTime processDate;
        if (!DateTime.TryParseExact(txtProcessDate.Text.Trim(), "dd/MM/yyyy",
            null, System.Globalization.DateTimeStyles.None, out processDate))
        {
            ShowError("Invalid process date format.");
            return false;
        }

        if (processDate.Date > DateTime.Now.Date)
        {
            ShowError("Process date cannot be a future date.");
            return false;
        }

        DateTime periodFrom;
        if (DateTime.TryParseExact(hfFromDate.Value, "dd/MM/yyyy",
            null, System.Globalization.DateTimeStyles.None, out periodFrom))
        {
            if (processDate.Date < periodFrom.Date)
            {
                ShowError("Process date cannot be earlier than the period start date (" +
                    hfFromDate.Value + ").");
                return false;
            }
        }

        if (lblMonthStatus.Text.Contains("Closed"))
        {
            ShowError("This period is already closed. Select a different period or store.");
            return false;
        }

        if (hfIsFuturePeriod.Value == "1")
        {
            ShowError("This period has not fully elapsed yet and cannot be processed.");
            return false;
        }
        return true;
    }

    #endregion

    #region ── Helpers ─────────────────────────────────────────────────

    
private int CurrentStep
{
    get { return ViewState["CurrentStep"] != null ? (int)ViewState["CurrentStep"] : 1; }
}

private long SelectedStore
{
    get
    {
        long id;
        return long.TryParse(ddlStore.SelectedValue, out id) ? id : 0;
    }
}

private long FK_Client
{
    get
    {
        long id;
        return long.TryParse(hfClient.Value, out id) ? id : 0;
    }
}

private decimal ParseDecimal(string val)
{
    decimal d;
    return decimal.TryParse(val, out d) ? d : 0;
}

    /// <summary>Build a BlMonthlyProcess from Step 1 inputs (dates, store).</summary>
    private BlMonthlyProcess BuildBlFromStep1()
    {
        BlMonthlyProcess bl = new BlMonthlyProcess();
        bl.FK_Store  = SelectedStore;
        bl.FK_Client = FK_Client;
        bl.UserCode  = hfUser.Value;

        DateTime from, to, proc;

        if (DateTime.TryParseExact(hfFromDate.Value, "dd/MM/yyyy",
            null, System.Globalization.DateTimeStyles.None, out from))
            bl.FromDate = from;

        if (DateTime.TryParseExact(hfToDate.Value, "dd/MM/yyyy",
            null, System.Globalization.DateTimeStyles.None, out to))
            bl.ToDate = to;

        if (DateTime.TryParseExact(hfProcessDate.Value.Trim(), "dd/MM/yyyy",
            null, System.Globalization.DateTimeStyles.None, out proc))
            bl.ProcessDate = proc;
        else
            bl.ProcessDate = bl.ToDate;

        return bl;
    }

    /// <summary>Build fully-populated BlMonthlyProcess from all hidden fields (Step 5).</summary>
    private BlMonthlyProcess BuildBlFromHiddenFields()
    {
        BlMonthlyProcess bl = BuildBlFromStep1();
        bl.Remarks           = txtRemarks.Text.Trim();
        bl.Opening           = ParseDecimal(hfOpening.Value);
        bl.Receipt           = ParseDecimal(hfReceipt.Value);
        bl.Payment           = ParseDecimal(hfPayment.Value);
        bl.Closing           = ParseDecimal(hfClosing.Value);
        bl.PurchaseAmount    = ParseDecimal(hfPurchaseAmount.Value);
        bl.PurchaseLiability = ParseDecimal(hfPurchaseLiability.Value);
        bl.PReturnAmount     = ParseDecimal(hfPReturnAmount.Value);
        bl.PReturnLiability  = ParseDecimal(hfPReturnLiability.Value);
        bl.SalesAmount       = ParseDecimal(hfSalesAmount.Value);
        bl.SalesLiability    = ParseDecimal(hfSalesLiability.Value);
        bl.SReturnAmount     = ParseDecimal(hfSReturnAmount.Value);
        bl.SReturnLiability  = ParseDecimal(hfSReturnLiability.Value);
        bl.DamageStock       = ParseDecimal(hfDamageStock.Value);
        bl.IndentFrom        = ParseDecimal(hfIndentFrom.Value);
        bl.IndentTo          = ParseDecimal(hfIndentTo.Value);
        bl.StockValue        = ParseDecimal(hfStockValue.Value);
        bl.ClosingLiability  = ParseDecimal(hfClosingLiability.Value);
        return bl;
    }


    private void ShowError(string message)
    {
        string safeMsg = message.Replace("'", "\\'").Replace("\r", "").Replace("\n", " ");
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Err",
            "toastr.clear(); toastr.warning('" + safeMsg + "');", true);
    }

    private void ShowError(Exception ex)
    {
        string userMessage;

        if (ex is System.Data.SqlClient.SqlException)
        {
            System.Diagnostics.Trace.TraceError("PeriodClose SQL error: " + ex);
            userMessage = "Something went wrong while processing your request. " +
                          "Please try again or contact support if the issue continues.";
        }
        else
        {
            userMessage = ex.Message;
        }

        ShowError(userMessage);
    }
    private void BindStoreDropdown()
    {
        Populate populate = new Populate();
        populate.FillDropdownListAll(ddlStore, "Store", "StoName", "ID_Store",
            "StoName", "Active='True' AND FK_Client=" + hfClient.Value, "");
        populate = null;
        if (ddlStore.Items.FindByValue("0") != null)
        ddlStore.Items.FindByValue("0").Text = "Please Select";
        ddlStore.Focus();
    }

    /// <summary>Check page permission – same pattern as YearEnd CheckUserAccess().</summary>
    private void CheckUserAccess()
    {
        BlUserAccess bl = new BlUserAccess();
        long u;
        bl.FK_Users = long.TryParse(hfUser.Value, out u) ? u : 0;
        bl.FK_Pages  = 82;    // assign the correct PageModule ID for MonthlyProcess
        bl.FK_Client = FK_Client;
        bl.CheckUserAccess();

        hfIsAdd.Value = bl.IsAdd.ToString();

        if (!Convert.ToBoolean(bl.IsView))
            Response.Redirect("Home.aspx");

        bl = null;
    }

    #endregion

    // ── Needed for SetStepProgress to reference runat="server" divs ──
    // The HtmlGenericControl type is accessed via System.Web.UI.HtmlControls.HtmlGenericControl
    // already in scope through the page's using directives.
    // If the compiler cannot resolve it, add:
    //   using System.Web.UI.HtmlControls;
}
