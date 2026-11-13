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
    public class DalMaterialReciept
    {

        public Int32 UpdateMaterialReciept(IMaterialReciept imaterialreciept)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, imaterialreciept.UserAction);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, imaterialreciept.MasterId);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, imaterialreciept.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, imaterialreciept.FK_Supplier);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, imaterialreciept.FK_Category);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, imaterialreciept.FK_Client);
            db.AddInParameter(dbCommand, "@FK_PurchaseOrder", DbType.Int64, imaterialreciept.FK_PurchaseOrder);
            db.AddInParameter(dbCommand, "@InterStatePurchase", DbType.Boolean, imaterialreciept.InterStatePurchase);
            db.AddInParameter(dbCommand, "@InvoiceNo", DbType.String, imaterialreciept.InvoiceNo);
            db.AddInParameter(dbCommand, "@InvoiceDate", DbType.DateTime, imaterialreciept.InvoiceDate);
            db.AddInParameter(dbCommand, "@Fright", DbType.Decimal, imaterialreciept.Fright);
            db.AddInParameter(dbCommand, "@RoundOff", DbType.Decimal, imaterialreciept.RoundOff);
            db.AddInParameter(dbCommand, "@PurAmt", DbType.Decimal, imaterialreciept.PurAmt);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, imaterialreciept.UserCode);
            db.AddInParameter(dbCommand, "@XmlPurchase", DbType.Xml, imaterialreciept.XmlMaterialRecieptItems);
            db.AddInParameter(dbCommand, "@XmlTaxSalesDetails", DbType.Xml, imaterialreciept.XmlSalesTax);
            db.AddInParameter(dbCommand, "@XmlTaxPurchaseDetails", DbType.Xml, imaterialreciept.XmlPurchaseTax);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, imaterialreciept.FK_Reason);
            db.AddInParameter(dbCommand, "@GSTIN", DbType.String, imaterialreciept.GSTIN);
            //db.AddInParameter(dbCommand, "@RefNo", DbType.Int64, imaterialreciept.RefNo);
            db.AddInParameter(dbCommand, "@NetAmount", DbType.Decimal, imaterialreciept.NetAmount);
            db.AddInParameter(dbCommand, "@xmlExpenseData", DbType.String, imaterialreciept.xmlExpenseData);
            db.AddInParameter(dbCommand, "@TransactionID", DbType.Int64, imaterialreciept.TransactionID);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(imaterialreciept, e);
                return 0;
            }
        }
        public IMaterialReciept SelectpurchaseOrderStore(IMaterialReciept imaterialreciept)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMaterialRecieptSelectpurchaseOrderStore";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_PurchaseOrder", DbType.Int64, imaterialreciept.FK_PurchaseOrder);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, imaterialreciept.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {

                        //  imaterialreciept.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                        // imaterialreciept.FK_Supplier = Convert.ToInt64(dataReader["FK_Supplier"].ToString());


                    }
                }
                return imaterialreciept;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imaterialreciept, e);
                throw e;
            }
        }


        public IMaterialReciept SelectMaterialStoreSupplier(IMaterialReciept imaterialreciept)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMaterialRecieptSelectStoreSupplier";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_MaterialReciept", DbType.Int64, imaterialreciept.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, imaterialreciept.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {

                        //  imaterialreciept.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                        //   imaterialreciept.FK_Supplier = Convert.ToInt64(dataReader["FK_Supplier"].ToString());


                    }
                }
                return imaterialreciept;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imaterialreciept, e);
                throw e;
            }
        }
        public DataTable PuchaseQuatationSelect(IMaterialReciept imaterialreciept)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseQuatationSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, imaterialreciept.InvoiceDate);
            db.AddInParameter(dbCommand, "@Fk_Item", DbType.Int64, imaterialreciept.FK_Items);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, imaterialreciept.FK_Supplier);


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
        public DataTable previousData(IMaterialReciept imaterialreciept)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProItemWisePreviousPurchase";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, imaterialreciept.FK_Items);
            db.AddInParameter(dbCommand, "@InterStatePurchase", DbType.Int64, imaterialreciept.InterStatePurchase);

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
        public IMaterialReciept SelectMaterialReciept(IMaterialReciept imaterialreciept)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMaterialRecieptSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_MaterialReciept", DbType.Int64, imaterialreciept.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, imaterialreciept.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, imaterialreciept.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, imaterialreciept.FK_Supplier);

            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, imaterialreciept.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, imaterialreciept.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        // imaterialreciept.FK_PurchaseOrder = Convert.ToInt64(dataReader["FK_PurchaseOrder"].ToString());
                        // imaterialreciept.Date = Convert.ToDateTime(dataReader["Date"].ToString());
                        // imaterialreciept.RefNo = Convert.ToString(dataReader["RefNo"].ToString());
                        // imaterialreciept.FK_Supplier = Convert.ToInt64(dataReader["FK_Supplier"].ToString());
                        //imaterialreciept.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                        imaterialreciept.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        imaterialreciept.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        imaterialreciept.Cancelled = Convert.ToBoolean(dataReader["Cancelled"].ToString());

                        imaterialreciept.Cancelleduser = Convert.ToString(dataReader["Cancelleduser"].ToString());
                        imaterialreciept.CancelledOn = Convert.ToDateTime(dataReader["CancelledOn"].ToString());
                        imaterialreciept.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return imaterialreciept;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imaterialreciept, e);
                throw e;
            }
        }
        public string SelectPurchaseRefNo(IMaterialReciept imaterialreciept)
        {

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseRefNoSelect";
           
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, imaterialreciept.MasterId);
           
            try
            {
                DataTable dt = db.ExecuteDataSet(dbCommand).Tables[0];

                return dt.Rows[0][0].ToString();
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imaterialreciept, e);
                throw e;
            }
        }
        public DataTable SelectMaterialRecieptDetails(IMaterialReciept imaterialreciept)
        {
            DataTable dtblMaterialReciept = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMaterialRecieptDetailsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, imaterialreciept.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, imaterialreciept.FK_Supplier);
            db.AddInParameter(dbCommand, "@InvoiceNo", DbType.String, imaterialreciept.InvoiceNo);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, imaterialreciept.FK_Client);

            //db.AddInParameter(dbCommand, "@ID_MaterialReciept",DbType.Int64,imaterialreciept.MasterId);
            //db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, imaterialreciept.PageIndex);
            //db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, imaterialreciept.PageSize);
            try
            {
                dtblMaterialReciept = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblMaterialReciept;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imaterialreciept, e);
                throw e;
            }
        }
        public DataTable SelectMaterialRecieptFright(IMaterialReciept imaterialreciept)
        {
            DataTable dtblMaterialReciept = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMaterialRecieptFrightSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, imaterialreciept.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, imaterialreciept.FK_Supplier);
            db.AddInParameter(dbCommand, "@InvoiceNo", DbType.String, imaterialreciept.InvoiceNo);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, imaterialreciept.FK_Client);

            //db.AddInParameter(dbCommand, "@ID_MaterialReciept",DbType.Int64,imaterialreciept.MasterId);
            //db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, imaterialreciept.PageIndex);
            //db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, imaterialreciept.PageSize);
            try
            {
                dtblMaterialReciept = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblMaterialReciept;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imaterialreciept, e);
                throw e;
            }
        }


        public DataTable SelectPreviousPurchaseDetails(IMaterialReciept imaterialreciept)
        {
            DataTable dtblMaterialReciept = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPreviousPurchaseDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, imaterialreciept.FK_ItemsforPreviousPurchase);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, imaterialreciept.FK_Client);
            db.AddInParameter(dbCommand, "@ItemName", DbType.String, imaterialreciept.ItemName);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int32, imaterialreciept.FK_Category);
            db.AddInParameter(dbCommand, "@FK_SubCategory", DbType.Int32, imaterialreciept.FK_SubCategory);
            db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int32, imaterialreciept.FK_Manufacture);
            db.AddInParameter(dbCommand, "@Fk_Unit", DbType.Int32, imaterialreciept.FK_Units);
            db.AddInParameter(dbCommand, "@Fk_Subunit", DbType.Int32, imaterialreciept.FK_SubUnits);
            db.AddInParameter(dbCommand, "@batchNo", DbType.String, imaterialreciept.BatchNo);
            db.AddInParameter(dbCommand, "@FK_Generic", DbType.String, imaterialreciept.FK_Generic);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.String, imaterialreciept.FK_Supplier);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, imaterialreciept.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, imaterialreciept.ToDate);
            db.AddInParameter(dbCommand, "@Period", DbType.Int16, imaterialreciept.Period);
            //db.AddInParameter(dbCommand, "@ID_MaterialReciept",DbType.Int64,imaterialreciept.MasterId);
            //db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, imaterialreciept.PageIndex);
            //db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, imaterialreciept.PageSize);
            try
            {
                dtblMaterialReciept = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblMaterialReciept;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imaterialreciept, e);
                throw e;
            }
        }
        public DataTable SelectPreviousPurchaseDetailsGraph(IMaterialReciept imaterialreciept)
        {
            DataTable dtblMaterialReciept = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPreviousPurchaseDetailsGraph";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, imaterialreciept.FK_ItemsforPreviousPurchase);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, imaterialreciept.FK_Client);
            db.AddInParameter(dbCommand, "@ItemName", DbType.String, imaterialreciept.ItemName);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int32, imaterialreciept.FK_Category);
            db.AddInParameter(dbCommand, "@FK_SubCategory", DbType.Int32, imaterialreciept.FK_SubCategory);
            db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int32, imaterialreciept.FK_Manufacture);
            db.AddInParameter(dbCommand, "@Fk_Unit", DbType.Int32, imaterialreciept.FK_Units);
            db.AddInParameter(dbCommand, "@Fk_Subunit", DbType.Int32, imaterialreciept.FK_SubUnits);
            db.AddInParameter(dbCommand, "@batchNo", DbType.String, imaterialreciept.BatchNo);
            db.AddInParameter(dbCommand, "@FK_Generic", DbType.String, imaterialreciept.FK_Generic);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.String, imaterialreciept.FK_Supplier);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, imaterialreciept.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, imaterialreciept.ToDate);
            db.AddInParameter(dbCommand, "@Period", DbType.Int16, imaterialreciept.Period);
            //db.AddInParameter(dbCommand, "@ID_MaterialReciept",DbType.Int64,imaterialreciept.MasterId);
            //db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, imaterialreciept.PageIndex);
            //db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, imaterialreciept.PageSize);
            try
            {
                dtblMaterialReciept = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblMaterialReciept;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imaterialreciept, e);
                throw e;
            }
        }


        public DataTable SelectAllMaterialReciept(IMaterialReciept imaterialreciept)
        {
            DataTable dtblMaterialReciept = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMaterialRecieptSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, imaterialreciept.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, imaterialreciept.FK_Supplier);
            db.AddInParameter(dbCommand, "@InvoiceNo", DbType.String, imaterialreciept.InvoiceNo);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, imaterialreciept.FK_Client);

            db.AddInParameter(dbCommand, "@ID_MaterialReciept", DbType.Int64, imaterialreciept.MasterId);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, imaterialreciept.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, imaterialreciept.PageSize);
            try
            {
                dtblMaterialReciept = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblMaterialReciept;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imaterialreciept, e);
                throw e;
            }
        }

        public int DeleteMaterialReciept(IMaterialReciept imaterialreciept)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMaterialRecieptDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_MaterialReciept", DbType.Int64, imaterialreciept.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, imaterialreciept.FK_Client);
            db.AddInParameter(dbCommand, "@Cancelledby", DbType.String, imaterialreciept.UserCode);
            db.AddInParameter(dbCommand, "@CancelledOn", DbType.DateTime, imaterialreciept.CancelledOn);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imaterialreciept, e);
                throw e;
            }
        }
        public DataTable ValidateExcelData(IMaterialReciept Ipurchase)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMaterialRecieptMultipleTransValidateExcel";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@XmlExcelImport", DbType.Xml, Ipurchase.XmlExcelImport);
            db.AddInParameter(dbCommand, "@InterState", DbType.Boolean, Ipurchase.InterStatePurchase);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, Ipurchase.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, Ipurchase.UserCode);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public DataTable GetPartialMaterialRecieptItems(IMaterialReciept IMaterialReciept)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPartialMaterialRecieptSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IMaterialReciept.FK_Client);
            db.AddInParameter(dbCommand, "@FK_PurchaseOrder", DbType.Int64, IMaterialReciept.FK_PurchaseOrder);
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

        public DataTable SelectMaterialRecieptItems(IMaterialReciept Ipurchase)
        {
            DataTable dtblMaterialRecieptItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProMaterialRecieptItemsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_MaterialReciept", DbType.Int64, Ipurchase.MasterId);
            try
            {
                dtblMaterialRecieptItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblMaterialRecieptItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        //public DataTable selectGridData(IMaterialReciept imaterialreciept)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("WebPharma");
        //    string sqlCommand = "proGriddetails";
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        //    db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, imaterialreciept.UserAction);
        //    db.AddInParameter(dbCommand, "@xmlGriddata", DbType.Xml, imaterialreciept.xmlGriddata);
        //    db.AddInParameter(dbCommand, "@InterState", DbType.Boolean, imaterialreciept.InterStatePurchase);

        //    try
        //    {
        //        return Convert.ToInt32(db.ExecuteScalar(dbCommand));
        //    }
        //    catch (SqlException e)
        //    {

        //        return 0;
        //    }
        //}
        public DataTable selectGridData(IMaterialReciept imaterialreciept)
        {
            DataTable dtblMaterialRecieptItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proGriddetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, imaterialreciept.UserAction);
            db.AddInParameter(dbCommand, "@xmlGriddata", DbType.Xml, imaterialreciept.xmlGriddata);
            db.AddInParameter(dbCommand, "@InterState", DbType.Boolean, imaterialreciept.InterStatePurchase);
            try
            {
                dtblMaterialRecieptItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblMaterialRecieptItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }

        public DataTable selectPurchaseOrderExist(IMaterialReciept Ipurchase)
        {
            DataTable dtblMaterialRecieptItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseOrderChecking";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, Ipurchase.MasterId);
            try
            {
                dtblMaterialRecieptItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblMaterialRecieptItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public DataTable SelectPrevPurchaseDetails(IMaterialReciept imaterialreciept)
        {
            DataTable dtblMaterialReciept = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMaterialRecieptPreviousPreviousPurchaseDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, imaterialreciept.FK_ItemsforPreviousPurchase);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, imaterialreciept.FK_Client);
          
            try
            {
                dtblMaterialReciept = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblMaterialReciept;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imaterialreciept, e);
                throw e;
            }
        }
    }
}
