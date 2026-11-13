using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.DataAccess;
using WebPharma.Interface.General;
using WebPharma.Interface.WebPharma;

namespace WebPharma.DataAccess.WebPharma
{
    public class DalSaleReturn
    {
        public Int32 UpdateSaleReturn(ISaleReturn iSaleReturn)
        {

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSalesReturnUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iSaleReturn.UserAction);
            db.AddInParameter(dbCommand, "@ID_SalesReturn", DbType.Int64, iSaleReturn.MasterId);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iSaleReturn.FK_Store);

            db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, iSaleReturn.FK_BillType);
            db.AddInParameter(dbCommand, "@BillNo", DbType.Int64, iSaleReturn.BillNo);
            db.AddInParameter(dbCommand, "@BillDate", DbType.DateTime, iSaleReturn.BillDate);

            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, iSaleReturn.FK_Customer);

            db.AddInParameter(dbCommand, "@CustomerName", DbType.String, iSaleReturn.CustomerName);
            db.AddInParameter(dbCommand, "@BillTotal", DbType.Decimal, iSaleReturn.BillTotal);

            db.AddInParameter(dbCommand, "@Discount", DbType.Decimal, iSaleReturn.Discount);
            db.AddInParameter(dbCommand, "@RountOff", DbType.Decimal, iSaleReturn.RoundOff);
            db.AddInParameter(dbCommand, "@NetAmount", DbType.Decimal, iSaleReturn.NetAmount);


            db.AddInParameter(dbCommand, "@Remarks", DbType.String, iSaleReturn.Remarks);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iSaleReturn.UserCode); 

            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, iSaleReturn.FK_Counter);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iSaleReturn.FK_Client);
            db.AddInParameter(dbCommand, "@Fk_Sales", DbType.Int64, iSaleReturn.Fk_Sale);

            db.AddInParameter(dbCommand, "@XmlSalesReturn", DbType.Xml, iSaleReturn.XmlSR);

            db.AddInParameter(dbCommand, "@Credit", DbType.Boolean, iSaleReturn.Credit);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, iSaleReturn.FK_Reason);
            db.AddInParameter(dbCommand, "@TransactionID", DbType.String, iSaleReturn.TransactionID);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                return 0;
            }
        }
        public ISaleReturn SelectSaleReturn(ISaleReturn iSaleReturn)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSaleReturnSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, iSaleReturn.FK_Customer);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iSaleReturn.FK_Store);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, iSaleReturn.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, iSaleReturn.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iSaleReturn.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, iSaleReturn.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, iSaleReturn.ToDate);
            db.AddInParameter(dbCommand, "ID_SalesReturn", DbType.Int64, iSaleReturn.MasterId);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {

                        iSaleReturn.BillNo = Convert.ToInt64(dataReader["BillNo"].ToString());
                        iSaleReturn.BillDate = Convert.ToDateTime(dataReader["BillDate"].ToString());
                        iSaleReturn.FK_Customer = Convert.ToInt64(dataReader["FK_Customer"].ToString());
                        iSaleReturn.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                        iSaleReturn.BillTotal = Convert.ToDecimal(dataReader["BillTotal"].ToString());
                        iSaleReturn.Discount = Convert.ToDecimal(dataReader["Discount"].ToString());
                        iSaleReturn.RoundOff = Convert.ToDecimal(dataReader["RoundOff"].ToString());
                        iSaleReturn.NetAmount = Convert.ToDecimal(dataReader["NetAmount"].ToString());
                        iSaleReturn.Remarks = dataReader["Remarks"].ToString();
                        iSaleReturn.Cancelled = (bool)dataReader["Cancelled"];
                        iSaleReturn.Fk_Sale = Convert.ToInt64(dataReader["Fk_Sale"].ToString());
                        iSaleReturn.Credit = Convert.ToBoolean(dataReader["Credit"].ToString());
                        iSaleReturn.SaleNo = dataReader["SaleNo"].ToString();
                        iSaleReturn.CustomerName = dataReader["CustomerName"].ToString();
                        iSaleReturn.FK_BillType = Convert.ToInt64(dataReader["FK_BillType"].ToString());
                        iSaleReturn.FK_Counter = Convert.ToInt64(dataReader["FK_Counter"].ToString());
                    }
                }
                return iSaleReturn;
            }
            catch (SqlException e)
            {

                throw e;
            }

        }
        public DataTable SelectSaleReturnAll(ISaleReturn iSaleReturn)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSaleReturnSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, iSaleReturn.FK_Customer);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iSaleReturn.FK_Store);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, iSaleReturn.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, iSaleReturn.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iSaleReturn.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, iSaleReturn.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, iSaleReturn.ToDate);
            db.AddInParameter(dbCommand, "ID_SalesReturn", DbType.Int64, iSaleReturn.MasterId);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {

                throw e;
            }
        }
        public DataTable SelectSaleReturnAllForPrint(ISaleReturn iSaleReturn)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSaleReturnSelectPrint";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, iSaleReturn.FK_Customer);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iSaleReturn.FK_Store);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, iSaleReturn.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, iSaleReturn.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iSaleReturn.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, iSaleReturn.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, iSaleReturn.ToDate);
            db.AddInParameter(dbCommand, "ID_SalesReturn", DbType.Int64, iSaleReturn.MasterId);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {

                throw e;
            }
        }
        public DataTable SelectSaleReturnItems(ISaleReturn ISaleReturn)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSaleReturnItemsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@ID_Sale", DbType.Int64, ISaleReturn.Fk_Sale);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ISaleReturn.FK_Client);
            db.AddInParameter(dbCommand, "@ID_SaleReturn", DbType.Int64, ISaleReturn.MasterId);

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {

                throw e;
            }
        }
        public DataTable SelectSaleReturnItemsforPrint(ISaleReturn ISaleReturn)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSaleReturnItemsSelectPrint";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@ID_Sale", DbType.Int64, ISaleReturn.Fk_Sale);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ISaleReturn.FK_Client);
            db.AddInParameter(dbCommand, "@ID_SaleReturn", DbType.Int64, ISaleReturn.MasterId);

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {

                throw e;
            }
        }
        public DataTable DirectSaleReturnItems(ISaleReturn iSaleReturn)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDirectSaleReturnItems";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iSaleReturn.FK_Client);
            db.AddInParameter(dbCommand, "@ID_SaleReturn", DbType.Int64, iSaleReturn.MasterId);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {

                throw e;
            }
        }
        public DataSet GetSalesReturnDataSource(long StatusId)
        {
            string sqlCommand = "";
            DbCommand dbCommand;
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            sqlCommand = "ProRptSalesReturnprint";
            dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_PurchaseReturn", DbType.Int64, StatusId);
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
                return ds;
            }
            catch (SqlException e)
            {
                throw;
            }

        }

        public String SalesReturnPrint(ISaleReturn isalereturn)
        {
            String s = "";

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSRHtmlLayout";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_SaleReturn", DbType.Int64, isalereturn.MasterId);
            try
            {
                return Convert.ToString(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                return "";
            }
        }

        public DataTable GridData(ISaleReturn SaleReturn)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsSalesTaxSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, SaleReturn.FK_ItemId);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        public DataTable TaxGridData(ISaleReturn SaleReturn)
        {
             DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProGetSalesTaxAmount";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, SaleReturn.FK_ItemId);
            db.AddInParameter(dbCommand, "@FK_Stock", DbType.Int64, SaleReturn.FK_Stock);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        } 

    }

}
