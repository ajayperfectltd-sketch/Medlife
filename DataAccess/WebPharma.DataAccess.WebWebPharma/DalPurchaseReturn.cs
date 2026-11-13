/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     :Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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
    public class DalPurchaseReturn
    {
        public Int32 UpdatePurchaseReturn(IPurchaseReturn ipurchasereturn)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseReturnUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ipurchasereturn.UserAction);
            db.AddInParameter(dbCommand, "@ID_PurchaseReturn", DbType.Int64, ipurchasereturn.MasterId);
            db.AddInParameter(dbCommand, "@FK_Purchase", DbType.Int64, ipurchasereturn.FK_Purchase);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, ipurchasereturn.FK_Supplier);
            db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, ipurchasereturn.FK_BillType);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, ipurchasereturn.FK_Store);
            db.AddInParameter(dbCommand, "@BillNo", DbType.Int64, ipurchasereturn.BillNo);
            db.AddInParameter(dbCommand, "@Invoice", DbType.String, ipurchasereturn.Invoice);
            db.AddInParameter(dbCommand, "@BillDate", DbType.DateTime, ipurchasereturn.BillDate);
            db.AddInParameter(dbCommand, "@InvDate", DbType.DateTime, ipurchasereturn.InvDate);
            db.AddInParameter(dbCommand, "@Remarks", DbType.String, ipurchasereturn.Remarks);
            db.AddInParameter(dbCommand, "@Credit", DbType.Boolean, ipurchasereturn.Credit);
            db.AddInParameter(dbCommand, "@TotalAmt", DbType.Decimal, ipurchasereturn.TotalAmt);
            db.AddInParameter(dbCommand, "@DiscPer", DbType.Decimal, ipurchasereturn.Discount);
            db.AddInParameter(dbCommand, "@DiscAmt", DbType.Decimal, ipurchasereturn.DiscountAmt);
            db.AddInParameter(dbCommand, "@NetAmount", DbType.Decimal, ipurchasereturn.NetAmount);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipurchasereturn.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchasereturn.FK_Client);
            db.AddInParameter(dbCommand, "@XmlPurchaseReturn", DbType.Xml, ipurchasereturn.XmlPR);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, ipurchasereturn.FK_Category);
            db.AddInParameter(dbCommand, "@AccountPosting", DbType.Boolean, ipurchasereturn.AccountPosting);
            db.AddInParameter(dbCommand, "@TransactionID", DbType.Int64, ipurchasereturn.TransactionID);
            db.AddInParameter(dbCommand, "@Expiry", DbType.Boolean, ipurchasereturn.Expiry);
            db.AddInParameter(dbCommand, "@ReturnMethod", DbType.String, ipurchasereturn.ReturnMethod);
            db.AddInParameter(dbCommand, "@ExpiryDate", DbType.String, ipurchasereturn.ExpiryDate);
            db.AddInParameter(dbCommand, "@OtherCharge", DbType.Decimal, ipurchasereturn.OtherCharge);
           
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
        public Int32 ApprovePurchaseReturn(IPurchaseReturn ipurchasereturn)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseReturnApprove";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);            
            db.AddInParameter(dbCommand, "@ID_PurchaseReturn", DbType.Int64, ipurchasereturn.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchasereturn.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, ipurchasereturn.FK_Store);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipurchasereturn.UserCode);
            db.AddInParameter(dbCommand, "@ReturnMethod", DbType.String, ipurchasereturn.ReturnMethod);
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
        public long DeletePurchasreturn(IPurchaseReturn IpurchaseReturn)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseReturnDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_PurchaseReturn", DbType.Int64, IpurchaseReturn.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IpurchaseReturn.FK_Client);
            db.AddInParameter(dbCommand, "@Cancelleduser", DbType.String, IpurchaseReturn.CancelledBy);
            db.AddInParameter(dbCommand, "@CancelledOn", DbType.DateTime, IpurchaseReturn.CancelledOn);
            try
            {
                return Convert.ToInt64(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                return 0;
            }
        }

        public IPurchaseReturn SelectPurchaseReturn(IPurchaseReturn IpurchaseReturn)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseReturnSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, IpurchaseReturn.FK_Supplier);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IpurchaseReturn.FK_Store);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, IpurchaseReturn.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, IpurchaseReturn.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IpurchaseReturn.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, IpurchaseReturn.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, IpurchaseReturn.ToDate);
            db.AddInParameter(dbCommand, "@ID_PurchaseReturn", DbType.Int64, IpurchaseReturn.MasterId);
            db.AddInParameter(dbCommand, "@Expiry", DbType.Boolean, IpurchaseReturn.Expiry);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        // IpurchaseReturn.PrNo = dataReader["PrNo"].ToString();
                        IpurchaseReturn.BillNo = Convert.ToInt64(dataReader["BillNo"].ToString());
                        IpurchaseReturn.BillDate = Convert.ToDateTime(dataReader["BillDate"].ToString());
                        IpurchaseReturn.Invoice = dataReader["Invoice"].ToString();
                        IpurchaseReturn.InvDate = Convert.ToDateTime(dataReader["InvDate"].ToString());
                        IpurchaseReturn.FK_Supplier = Convert.ToInt64(dataReader["FK_Supplier"].ToString());
                        IpurchaseReturn.SupplierName = Convert.ToString(dataReader["Supplier"].ToString());
                        IpurchaseReturn.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                        IpurchaseReturn.FK_Category = Convert.ToInt64(dataReader["FK_Category"]);
                        IpurchaseReturn.TotalAmt = Convert.ToDecimal(dataReader["TotalAmt"].ToString());
                        IpurchaseReturn.Discount = Convert.ToDecimal(dataReader["DiscPer"].ToString());
                        IpurchaseReturn.DiscountAmt = Convert.ToDecimal(dataReader["DiscAmt"].ToString());
                        IpurchaseReturn.NetAmount = Convert.ToDecimal(dataReader["NetAmount"].ToString());
                        IpurchaseReturn.Remarks = dataReader["Remarks"].ToString();
                        IpurchaseReturn.Credit = (bool)dataReader["Credit"];
                        IpurchaseReturn.Cancelled = (bool)dataReader["Cancelled"];
                        IpurchaseReturn.CancelledBy = dataReader["CancelledBy"].ToString();
                        //IpurchaseReturn.CancelledOn = Convert.ToDateTime(dataReader["CancelledOn"].ToString());
                        IpurchaseReturn.FK_Purchase = Convert.ToInt64(dataReader["FK_Purchase"].ToString());
                        IpurchaseReturn.AccountPosting = Convert.ToBoolean(dataReader["AccountPosting"].ToString());
                        IpurchaseReturn.FK_BillType = Convert.ToInt64(dataReader["FK_BillType"].ToString());
                        IpurchaseReturn.Expiry = Convert.ToBoolean(dataReader["Expiry"].ToString());
                        IpurchaseReturn.TotalAmt = Convert.ToDecimal(dataReader["TotalAmt"].ToString());
                        IpurchaseReturn.ReturnMethod = dataReader["ReturnMethod"].ToString();
                        IpurchaseReturn.OtherCharge = Convert.ToDecimal(dataReader["OtherCharge"]);
                        IpurchaseReturn.GSTIN = dataReader["GSTIN"].ToString();
                        IpurchaseReturn.ExpiryDate = dataReader["ExpiryDate"].ToString();
                        IpurchaseReturn.Approved = Convert.ToBoolean(dataReader["Approved"].ToString());
                        IpurchaseReturn.OtherCharge = Convert.ToDecimal(dataReader["OtherCharge"].ToString());
                    }
                }
                return IpurchaseReturn;
            }
            catch (SqlException e)
            {

                throw e;
            }

        }
        public DataTable SelectPurchaseReturnAll(IPurchaseReturn IpurchaseReturn)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseReturnSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, IpurchaseReturn.FK_Supplier);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IpurchaseReturn.FK_Store);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, IpurchaseReturn.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, IpurchaseReturn.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IpurchaseReturn.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, IpurchaseReturn.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, IpurchaseReturn.ToDate);
            db.AddInParameter(dbCommand, "@ID_PurchaseReturn", DbType.Int64, IpurchaseReturn.MasterId);
            db.AddInParameter(dbCommand, "@Expiry", DbType.Boolean, IpurchaseReturn.Expiry);
            
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
        
        public DataTable SelectPurchaseItems(IPurchaseReturn IpurchaseReturn)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseItems";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@ID_purchase", DbType.Int64, IpurchaseReturn.FK_Purchase);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IpurchaseReturn.FK_Client);
            db.AddInParameter(dbCommand, "@ID_PurchaseReturn", DbType.Int64, IpurchaseReturn.MasterId);
            db.AddInParameter(dbCommand, "@ReturnMethod", DbType.String, IpurchaseReturn.ReturnMethod);

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
        public DataTable SelectNonreturnableItems(IPurchaseReturn IpurchaseReturn)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectNonReturableItem";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, IpurchaseReturn.FK_Items);
          

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
        public DataTable DirectPurchaseReturnItems(IPurchaseReturn ipurchaseReturn)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDirectPurchaseReturnItems";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ipurchaseReturn.FK_Client);
            db.AddInParameter(dbCommand, "@ID_PurchaseReturn", DbType.Int64, ipurchaseReturn.MasterId);
            db.AddInParameter(dbCommand, "@ReturnMethod", DbType.String, ipurchaseReturn.ReturnMethod);
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
        public DataSet GetPurchaseReturnDataSource(long StatusId)
        {
            string sqlCommand = "";
            DbCommand dbCommand;
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            sqlCommand = "ProPurchaseReturnprint";
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
      public DataTable SelectPurchaseWiseItemsSelect(IPurchaseReturn IpurchaseReturn)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseWiseItemsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@ID_purchase", DbType.Int64, IpurchaseReturn.FK_Purchase);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IpurchaseReturn.FK_Client);
            db.AddInParameter(dbCommand, "@ID_PurchaseReturn", DbType.Int64, IpurchaseReturn.MasterId);

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
      public DataTable ExpiryReturnItemWiseRounfOff(IPurchaseReturn ipurchaseReturn)
      {
          DataTable dtbl = new DataTable();
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proExpiryReturnItemWise";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ipurchaseReturn.FK_Client);
          db.AddInParameter(dbCommand, "@ID_PurchaseReturn", DbType.Int64, ipurchaseReturn.MasterId);
          db.AddInParameter(dbCommand, "@ReturnMethod", DbType.String, ipurchaseReturn.ReturnMethod);
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
    }
}
