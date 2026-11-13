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
    public class DalPurchase
    {
        public long UpdatePurchase(IPurchase ipurchase)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, ipurchase.MasterId);
            db.AddInParameter(dbCommand, "@FK_PurchaseOrder", DbType.Int64, ipurchase.FK_PurchaseOrder);
            db.AddInParameter(dbCommand, "@PurBillNo", DbType.String, ipurchase.PurBillNo);
            db.AddInParameter(dbCommand, "@RefNo", DbType.String, ipurchase.RefNo);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, ipurchase.Date);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, ipurchase.FK_Supplier);
            db.AddInParameter(dbCommand, "@Cost", DbType.Decimal, ipurchase.Cost);
            db.AddInParameter(dbCommand, "@Expense", DbType.Decimal, ipurchase.Expense);
            db.AddInParameter(dbCommand, "@InvoiceAmount", DbType.Decimal, ipurchase.InvoiceAmount);
            db.AddInParameter(dbCommand, "@Description", DbType.String, ipurchase.Description);
            db.AddInParameter(dbCommand, "@Credit", DbType.Boolean, ipurchase.Credit);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, ipurchase.FK_Store);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipurchase.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, ipurchase.EnteredOn);
            db.AddInParameter(dbCommand, "@Cancelled", DbType.Boolean, ipurchase.Cancelled);
            db.AddInParameter(dbCommand, "@Cancelleduser", DbType.String, ipurchase.Cancelleduser);
            db.AddInParameter(dbCommand, "@CancelledOn", DbType.DateTime, ipurchase.CancelledOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.String, ipurchase.FK_Client);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ipurchase.UserAction);
            db.AddInParameter(dbCommand, "@BlPurchaseDetails", DbType.Xml, ipurchase.BlPurchaseDetails);
            db.AddInParameter(dbCommand, "@XmlPurchaseItems", DbType.Xml, ipurchase.XmlPurchaseItems);
            db.AddInParameter(dbCommand, "@ItemPurchase", DbType.Boolean, ipurchase.ItemPurchase);

         



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
        public long DeletePurchaseDetails(IPurchase ipurchase)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseDetailsDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, ipurchase.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchase.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipurchase.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, ipurchase.FK_Reason);
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

        //public IPurchase SelectPurchase(IPurchase ipurchase)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("WebPharma");
        //    string sqlCommand = "promotionSettingsSelect";
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        //    db.AddInParameter(dbCommand, "", DbType, igoodsIssue.MasterId);

        //    //  IPurchase ip = new IPurchase();
        //    //  return ip;

        //}
        public long DeletePurchase(IPurchase ipurchase)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, ipurchase.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchase.FK_Client);
            db.AddInParameter(dbCommand, "@Cancelleduser", DbType.String, ipurchase.Cancelleduser);
            db.AddInParameter(dbCommand, "@CancelledOn", DbType.DateTime, ipurchase.CancelledOn);
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
        public DataTable PurchaseExport(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable(); 
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseExport";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, ipurchase.FK_Supplier);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, ipurchase.FK_Store);
             db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchase.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, ipurchase.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, ipurchase.ToDate);
           
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
        public DataTable GridData(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsTaxSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, ipurchase.itemID);
           db.AddInParameter(dbCommand, "@Purchase", DbType.Boolean, ipurchase.Purchase);
          //  db.AddInParameter(dbCommand, "@GSTin", DbType.Boolean, ipurchase.Purchase);

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
        public DataTable SelectGriddataPurchase(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsTaxSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, ipurchase.itemID);
            db.AddInParameter(dbCommand, "@Purchase", DbType.Boolean, ipurchase.Purchase);
             //db.AddInParameter(dbCommand, "@GSTin", DbType.Boolean, ipurchase.Purchase);

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
        public DataTable ExpenseData(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proExpenseTypeSelectInPurchase";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@PurchaseID", DbType.Int64, ipurchase.MasterId);
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
        public DataTable PurchseTax(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proTaxInPurchase";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@PurchaseID", DbType.Int64, ipurchase.MasterId);
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
        public DataTable SelectGroupID(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsTaxGroupSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, ipurchase.itemID);


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
        public DataTable SelectPurchaseReturnSelect(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsTaxDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, ipurchase.itemID);


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
        public DataTable PurcahseGrid(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsPurchaseTaxSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, ipurchase.itemID);


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
        public DataTable SalesGrid(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsSalesTaxSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, ipurchase.itemID);


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

        public DataTable proMaterialItemsUnderMaterials(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMaterialItemsUnderMaterials";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_MaterialReciept", DbType.Int64, ipurchase.FK_MaterialReciept);

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

        public DataSet GetPurchaseReportData(long StatusId)
        {
            string sqlCommand = "";
            DbCommand dbCommand;
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            sqlCommand = "ProRptPurchaseReceiptprint";
            dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, StatusId);
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
        public IPurchase proPurchaseSelect(IPurchase ipurchase)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, ipurchase.FK_Supplier);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, ipurchase.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchase.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, ipurchase.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, ipurchase.ToDate);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, ipurchase.MasterId);
            try
            {

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        //ipurchase.FK_PurchaseOrder = Convert.ToInt64(dataReader["FK_PurchaseOrder"]);
                        //ipurchase.RefNo = dataReader["RefNo"].ToString();
                        //ipurchase.Date = Convert.ToDateTime(dataReader["dates"]);
                        //ipurchase.FK_Supplier = Convert.ToInt64(dataReader["FK_Supplier"]);
                        //ipurchase.FK_Store = Convert.ToInt64(dataReader["FK_Store"]);
                        //ipurchase.Cost = Convert.ToDecimal(dataReader["Cost"]);
                        //ipurchase.Expense = Convert.ToDecimal(dataReader["Expense"]);
                        //ipurchase.InvoiceAmount = Convert.ToDecimal(dataReader["InvoiceAmount"]);
                        //ipurchase.Description = dataReader["Description"].ToString();
                        //ipurchase.Credit = Convert.ToBoolean(dataReader["Credit"]);
                        //ipurchase.FK_Store = Convert.ToInt64(dataReader["FK_Store"]);
                        //ipurchase.Credit = Convert.ToBoolean(dataReader["Credit"]);
                        //ipurchase.ItemPurchase = Convert.ToBoolean(dataReader["ItemPurchase"]);
                        //ipurchase.FK_PurchaseOrder = Convert.ToInt64(dataReader["FK_PurchaseOrder"]);
                        ipurchase.InvoiceNo = dataReader["InvoiceNo"].ToString();
                        ipurchase.BillDate = Convert.ToDateTime(dataReader["BillDate"]);
                        ipurchase.FK_Supplier = Convert.ToInt64(dataReader["FK_Supplier"]);
                        ipurchase.FK_Store = Convert.ToInt64(dataReader["FK_Store"]);
                        ipurchase.Credit = Convert.ToBoolean(dataReader["Credit"]);
                        ipurchase.DiscountPer = Convert.ToDecimal(dataReader["DiscountPer"].ToString());
                        ipurchase.DiscountAmt = Convert.ToDecimal(dataReader["DiscountAmt"].ToString());
                        ipurchase.SupplierName=Convert.ToString(dataReader["Supplier"]).ToString();

                    }
                }
            }
            catch (SqlException e)
            {

                throw e;
            }
            return ipurchase;
        }

        public DataTable proSelectAllPurchaseApproveEditView(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseSelectApproveEditView";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, ipurchase.FK_Supplier);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, ipurchase.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchase.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, ipurchase.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, ipurchase.ToDate);
            db.AddInParameter(dbCommand, "@StatusApproved", DbType.Int16, ipurchase.Approved);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, ipurchase.MasterId);
        
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
        public DataTable proSelectInterState(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proInterStateSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, ipurchase.FK_Supplier);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, ipurchase.FK_Store);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ipurchase.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ipurchase.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchase.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, ipurchase.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, ipurchase.ToDate);
            db.AddInParameter(dbCommand, "@StatusApproved", DbType.Int16, ipurchase.Approved);
            db.AddInParameter(dbCommand, "@CashCredit", DbType.Int16, ipurchase.CashCredit);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, ipurchase.MasterId);
            db.AddInParameter(dbCommand, "@SupplierName", DbType.String, ipurchase.SupplierName);
            db.AddInParameter(dbCommand, "@RefNo", DbType.String, ipurchase.RefNo);

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
        public DataTable proSelectAllDateForPurchase(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseSelectDateApprove";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, ipurchase.FK_Supplier);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, ipurchase.FK_Store);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ipurchase.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ipurchase.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchase.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, ipurchase.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, ipurchase.ToDate);
            db.AddInParameter(dbCommand, "@StatusApproved", DbType.Int16, ipurchase.Approved);
            db.AddInParameter(dbCommand, "@CashCredit", DbType.Int16, ipurchase.CashCredit);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, ipurchase.MasterId);
            db.AddInParameter(dbCommand, "@SupplierName", DbType.String, ipurchase.SupplierName);
            db.AddInParameter(dbCommand, "@RefNo", DbType.String, ipurchase.RefNo);
        
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
        public DataTable proPurchaseSelectAll(IPurchase ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, ipurchase.FK_Supplier);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, ipurchase.FK_Store);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ipurchase.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ipurchase.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchase.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, ipurchase.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, ipurchase.ToDate);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, ipurchase.MasterId);
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
        public DataTable proPurchaseMaterials(IPurchase purchase)
       {
           DataTable dtbl = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proPurchaseMaterials";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, purchase.MasterId);
              
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
       
        public DataTable proPurchaseMaterialsPurchaseItemsForApprove(IPurchase purchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMaterialRecieptDetailsFromPurchaseSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, purchase.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, purchase.FK_Client);
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
        public DataTable proPurchaseMaterialsPurchaseItems(IPurchase purchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProPurchaseMaterialRecieptItemsPurchaseSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, purchase.MasterId);

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

        public DataTable PurchaseStoreAndSupplier(IPurchase purchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseStoreAndSupplier";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, purchase.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, purchase.FK_Client);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string PurchaseImport(IPurchase purchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proImportPurchase";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@xmlPurchase", DbType.Xml, purchase.XmlPurchaseItems);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, purchase.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, purchase.UserCode);
            db.AddInParameter(dbCommand, "@InvoiceAmount", DbType.Decimal, purchase.InvoiceAmount);
            try
            {
                return Convert.ToString(db.ExecuteScalar(dbCommand));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
    }
}
