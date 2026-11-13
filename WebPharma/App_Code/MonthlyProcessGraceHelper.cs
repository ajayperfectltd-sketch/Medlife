using System;
using System.Data;
using System.Globalization;
using WebPharma.Business;
using WebPharma.Business.Masters;

/// <summary>
/// Shared Monthly Process grace-period evaluation for login warnings and transaction blocking.
/// NextDueDate = LastMonthlyCloseDate + GracePeriodDays.
/// </summary>
public class MonthlyProcessGraceInfo
{
    public bool HasStatus { get; set; }
    public bool ShowWarning { get; set; }
    public bool IsOverdue { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? LastProcessDate { get; set; }
    public string Message { get; set; }

    public MonthlyProcessGraceInfo()
    {
        HasStatus = false;
        ShowWarning = false;
        IsOverdue = false;
        DueDate = null;
        LastProcessDate = null;
        Message = string.Empty;
    }
}

public static class MonthlyProcessGraceHelper
{
    public static MonthlyProcessGraceInfo Evaluate(long fkClient, long fkStore)
    {
        MonthlyProcessGraceInfo status = new MonthlyProcessGraceInfo();

        if (fkClient <= 0)
            return status;
        if (fkStore <= 0)
            fkStore = 1;

        int gracePeriodDays = GetGracePeriodDays(fkClient);
        if (gracePeriodDays < 0)
            return status;

        DateTime? lastProcessDate = GetLastMonthlyCloseDate(fkStore, fkClient);
        DateTime? periodFromDate = null;
        DateTime? periodToDate = null;

        try
        {
            BlMonthlyProcess blMonthlyProcess = new BlMonthlyProcess();
            blMonthlyProcess.FK_Store = fkStore;
            blMonthlyProcess.FK_Client = fkClient;
            DataTable dtActive = blMonthlyProcess.GetActiveBusinessDates();
            if (dtActive != null && dtActive.Rows.Count > 0)
            {
                DataRow row = dtActive.Rows[0];
                if (row.Table.Columns.Contains("FromDate") && row["FromDate"] != DBNull.Value)
                    periodFromDate = Convert.ToDateTime(row["FromDate"]).Date;
                if (row.Table.Columns.Contains("ToDate") && row["ToDate"] != DBNull.Value)
                    periodToDate = Convert.ToDateTime(row["ToDate"]).Date;
            }
        }
        catch
        {
        }

        DateTime? referenceDate = lastProcessDate;
        if (!referenceDate.HasValue)
            referenceDate = periodToDate;
        if (!referenceDate.HasValue)
            return status;

        DateTime dueDate = referenceDate.Value.Date.AddDays(gracePeriodDays);
        DateTime today = DateTime.Today;

        status.HasStatus = true;
        status.DueDate = dueDate;
        status.LastProcessDate = lastProcessDate;

        // Already completed for current open cycle and still before due → no warning / no block.
        if (lastProcessDate.HasValue && periodFromDate.HasValue
            && lastProcessDate.Value.Date >= periodFromDate.Value.Date
            && today < dueDate)
        {
            return status;
        }

        string dueDateText = dueDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
        status.IsOverdue = today >= dueDate;
        status.ShowWarning = true;
        status.Message = status.IsOverdue
            ? "Urgent: Period Close is pending. It must be completed before " + dueDateText + ". Due date has been reached/exceeded."
            : "Warning: Period Close needs to be completed before " + dueDateText + " to avoid interruption of transactions.";

        return status;
    }

    private static int GetGracePeriodDays(long fkClient)
    {
        int gracePeriodDays = -1;
        try
        {
            BlSettings blsettings = new BlSettings();
            blsettings.Module = "GEN";
            blsettings.SubModule = "MGP";
            blsettings.FK_Client = fkClient;
            DataTable dtGrace = blsettings.SelectSettings();
            if (dtGrace != null && dtGrace.Rows.Count > 0)
            {
                for (int i = 0; i < dtGrace.Rows.Count; i++)
                {
                    string settingName = Convert.ToString(dtGrace.Rows[i]["Name"]);
                    string settingValue = Convert.ToString(dtGrace.Rows[i]["Value"]);
                    if (string.Equals(settingName, "DAYS", StringComparison.OrdinalIgnoreCase)
                        || string.IsNullOrEmpty(settingName))
                    {
                        if (int.TryParse(settingValue, out gracePeriodDays))
                            return gracePeriodDays;
                    }
                }
                if (int.TryParse(Convert.ToString(dtGrace.Rows[0]["Value"]), out gracePeriodDays))
                    return gracePeriodDays;
            }
        }
        catch
        {
        }

        try
        {
            BlFunction blFn = new BlFunction();
            string graceValue = blFn.GetScalarValue(
                "Settings",
                "ISNULL(Value,'')",
                "Module='GEN' AND SubModule='MGP' AND Name='DAYS' AND FK_Client=" + fkClient,
                "");
            if (int.TryParse(graceValue, out gracePeriodDays))
                return gracePeriodDays;
        }
        catch
        {
        }

        return -1;
    }

    private static DateTime? GetLastMonthlyCloseDate(long fkStore, long fkClient)
    {
        BlFunction blFn = new BlFunction();
        string[] criteriaList = new string[]
        {
            "FK_Store=" + fkStore + " AND FK_Client=" + fkClient,
            "FK_Store=" + fkStore,
            "FK_Client=" + fkClient,
            ""
        };

        foreach (string criteria in criteriaList)
        {
            try
            {
                string value = blFn.GetScalarValue("MonthlyClose", "MAX(Todate)", criteria, "");
                DateTime parsed;
                if (!string.IsNullOrWhiteSpace(value) && DateTime.TryParse(value, out parsed))
                    return parsed.Date;
            }
            catch
            {
            }
        }
        return null;
    }
}
