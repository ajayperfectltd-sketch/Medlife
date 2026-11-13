using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.Interface.Masters;

namespace WebPharma.DataAccess.Masters
{
    public class DalMonthlyProcess
    {
        public DataTable GetActiveBusinessDates(IMonthlyProcess obj)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "ProGetActiveBusinessDates";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);

            db.AddInParameter(dbcmd, "@FK_Store", DbType.Int64, obj.FK_Store);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, obj.FK_Client);

            try
            {
                DataTable dt = new DataTable();
                dt = db.ExecuteDataSet(dbcmd).Tables[0];
                return dt;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataSet GetTransactionDetails(IMonthlyProcess obj)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "ProMonthlyTransactionDetails";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);

            db.AddInParameter(dbcmd, "@FK_Store", DbType.Int64, obj.FK_Store);
            db.AddInParameter(dbcmd, "@FromDate", DbType.DateTime, obj.FromDate);
            db.AddInParameter(dbcmd, "@ToDate", DbType.DateTime, obj.ToDate);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, obj.FK_Client);

            try
            {
                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable GetGridDetails(IMonthlyProcess obj)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "ProMonthlyProcessGridDetails";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);

            db.AddInParameter(dbcmd, "@FK_Store", DbType.Int64, obj.FK_Store);
            db.AddInParameter(dbcmd, "@FromDate", DbType.DateTime, obj.FromDate);
            db.AddInParameter(dbcmd, "@ToDate", DbType.DateTime, obj.ToDate);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, obj.FK_Client);

            try
            {
                DataTable dt = new DataTable();
                dt = db.ExecuteDataSet(dbcmd).Tables[0];
                return dt;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataSet GetCashLiabilitySummary(IMonthlyProcess obj)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "ProMonthlyCashLiabilitySummary";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);

            db.AddInParameter(dbcmd, "@FromDate", DbType.DateTime, obj.FromDate);
            db.AddInParameter(dbcmd, "@ToDate", DbType.DateTime, obj.ToDate);
            db.AddInParameter(dbcmd, "@FK_Store", DbType.Int64, obj.FK_Store);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, obj.FK_Client);

            try
            {
                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataSet GetPaymentReceiptDetails(IMonthlyProcess obj)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "ProMonthlyPaymentReceiptDetails";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);

            db.AddInParameter(dbcmd, "@FromDate", DbType.DateTime, obj.FromDate);
            db.AddInParameter(dbcmd, "@ToDate", DbType.DateTime, obj.ToDate);
            db.AddInParameter(dbcmd, "@FK_Store", DbType.Int64, obj.FK_Store);

            try
            {
                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable GetPendingChecks(IMonthlyProcess obj)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "ProMonthlyProcess";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);

            db.AddInParameter(dbcmd, "@FromDate", DbType.DateTime, obj.FromDate);
            db.AddInParameter(dbcmd, "@ToDate", DbType.DateTime, obj.ToDate);
            db.AddInParameter(dbcmd, "@FK_Store", DbType.Int64, obj.FK_Store);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, obj.FK_Client);

            try
            {
                DataTable dt = new DataTable();
                dt = db.ExecuteDataSet(dbcmd).Tables[0];
                return dt;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataSet RunDataIntegrityCheck(long fkStore, int mode)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "ProDataIntegrityCheck";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);

            db.AddInParameter(dbcmd, "@Mode", DbType.Int32, mode);
            db.AddInParameter(dbcmd, "@Update", DbType.Boolean, true);
            db.AddInParameter(dbcmd, "@FK_Store", DbType.Int64, fkStore);

            try
            {
                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet(dbcmd);
                return ds;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }


        public int LogMonthlyProcessErrors(IMonthlyProcess obj)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "ProMonthlyProcessErrorLog";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);

            db.AddInParameter(dbcmd, "@FK_Store", DbType.Int64, obj.FK_Store);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, obj.FK_Client);
            db.AddInParameter(dbcmd, "@FromDate", DbType.DateTime, obj.FromDate);
            db.AddInParameter(dbcmd, "@ToDate", DbType.DateTime, obj.ToDate);
            db.AddInParameter(dbcmd, "@LoggedBy", DbType.String, obj.UserCode);

            try
            {
                object result = db.ExecuteScalar(dbcmd);
                return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns the last successfully completed Monthly Process date (MonthlyClose.Todate) for the store/client.
        /// </summary>
        public DateTime? GetLastMonthlyProcessDate(IMonthlyProcess obj)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");

            // Try store+client first, then store only, then client only.
            string[] criteriaList = new string[]
            {
                "FK_Store=" + obj.FK_Store + " AND FK_Client=" + obj.FK_Client,
                "FK_Store=" + obj.FK_Store,
                "FK_Client=" + obj.FK_Client
            };

            foreach (string criteria in criteriaList)
            {
                try
                {
                    string sqlCommand = "ProCmnGetScalarvalue";
                    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                    db.AddInParameter(dbCommand, "TableName", DbType.String, "MonthlyClose");
                    db.AddInParameter(dbCommand, "ListFields", DbType.String, "MAX(Todate)");
                    db.AddInParameter(dbCommand, "SortField", DbType.String, string.Empty);
                    db.AddInParameter(dbCommand, "Criteria", DbType.String, criteria);

                    DataSet ds = db.ExecuteDataSet(dbCommand);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0
                        && ds.Tables[0].Rows[0][0] != null && ds.Tables[0].Rows[0][0] != DBNull.Value
                        && !string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][0].ToString()))
                    {
                        return Convert.ToDateTime(ds.Tables[0].Rows[0][0]);
                    }
                }
                catch (SqlException)
                {
                    // try next criteria
                }
            }
            return null;
        }

        public DataTable SaveMonthlyProcess(IMonthlyProcess obj)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "ProMonthlyProcessUpdate";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);

            db.AddInParameter(dbcmd, "@UserAction", DbType.Byte, obj.UserAction);
            db.AddInParameter(dbcmd, "@ID_MonthlyProcess", DbType.Int64, obj.ID_MonthlyProcess);
            db.AddInParameter(dbcmd, "@FK_Store", DbType.Int64, obj.FK_Store);
            db.AddInParameter(dbcmd, "@MonthDate", DbType.DateTime, obj.ProcessDate);
            db.AddInParameter(dbcmd, "@FromDate", DbType.DateTime, obj.FromDate);
            db.AddInParameter(dbcmd, "@ToDate", DbType.DateTime, obj.ToDate);
            db.AddInParameter(dbcmd, "@Opening", DbType.Decimal, obj.Opening);
            db.AddInParameter(dbcmd, "@Receipt", DbType.Decimal, obj.Receipt);
            db.AddInParameter(dbcmd, "@Payment", DbType.Decimal, obj.Payment);
            db.AddInParameter(dbcmd, "@Closing", DbType.Decimal, obj.Closing);
            db.AddInParameter(dbcmd, "@PurchaseAmount", DbType.Decimal, obj.PurchaseAmount);
            db.AddInParameter(dbcmd, "@PurchaseLiability", DbType.Decimal, obj.PurchaseLiability);
            db.AddInParameter(dbcmd, "@PReturnAmount", DbType.Decimal, obj.PReturnAmount);
            db.AddInParameter(dbcmd, "@PReturnLiability", DbType.Decimal, obj.PReturnLiability);
            db.AddInParameter(dbcmd, "@SalesAmount", DbType.Decimal, obj.SalesAmount);
            db.AddInParameter(dbcmd, "@SalesLiability", DbType.Decimal, obj.SalesLiability);
            db.AddInParameter(dbcmd, "@SReturnAmount", DbType.Decimal, obj.SReturnAmount);
            db.AddInParameter(dbcmd, "@SReturnLiability", DbType.Decimal, obj.SReturnLiability);
            db.AddInParameter(dbcmd, "@DamageStock", DbType.Decimal, obj.DamageStock);
            db.AddInParameter(dbcmd, "@IndentFrom", DbType.Decimal, obj.IndentFrom);
            db.AddInParameter(dbcmd, "@IndentTo", DbType.Decimal, obj.IndentTo);
            db.AddInParameter(dbcmd, "@StockValue", DbType.Decimal, obj.StockValue);
            db.AddInParameter(dbcmd, "@ClosingLiability", DbType.Decimal, obj.ClosingLiability);
            db.AddInParameter(dbcmd, "@Remarks", DbType.String, obj.Remarks);
            db.AddInParameter(dbcmd, "@UserCode", DbType.String, obj.UserCode);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, obj.FK_Client);

            try
            {
                DataTable dt = new DataTable();
                dt = db.ExecuteDataSet(dbcmd).Tables[0];
                return dt;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}