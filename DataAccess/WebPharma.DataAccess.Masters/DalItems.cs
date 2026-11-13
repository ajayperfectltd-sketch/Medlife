/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 14/02/2014
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
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
using WebPharma.Interface.Masters;

namespace WebPharma.DataAccess.Masters
{
    public class DalItems
    {

        public Int32 UpdateItems(IItems iitems)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iitems.UserAction);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, iitems.MasterId);
            db.AddInParameter(dbCommand, "@ItemName", DbType.String, iitems.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iitems.ShortName);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, iitems.FK_CategoryFirst);
            db.AddInParameter(dbCommand, "@FK_SubCategory", DbType.Int64, iitems.FK_CategorySecond);
            db.AddInParameter(dbCommand, "@FK_Units", DbType.Int64, iitems.FK_Units);
            db.AddInParameter(dbCommand, "@FK_SubUnit", DbType.Int64, iitems.FK_SubUnit);
            db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int64, iitems.FK_Manufacturer);
            db.AddInParameter(dbCommand, "@FK_Generic", DbType.Int64, iitems.FK_Generic);
            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iitems.FK_Rack);
            db.AddInParameter(dbCommand, "@FK_Bin", DbType.Int64, iitems.FK_Bin);
            db.AddInParameter(dbCommand, "@MinLevel", DbType.Decimal, iitems.MinLevel);
            db.AddInParameter(dbCommand, "@MaxLevel", DbType.Decimal, iitems.MaxLevel);
            db.AddInParameter(dbCommand, "@ROLevel", DbType.Decimal, iitems.ROLevel);
            db.AddInParameter(dbCommand, "@ROQuantity", DbType.Decimal, iitems.ROQuantity);
            db.AddInParameter(dbCommand, "@DangerLevel", DbType.Decimal, iitems.DangerLevel);
            db.AddInParameter(dbCommand, "@ExpairyValidation", DbType.Int32, iitems.ExpairyValidation);
            db.AddInParameter(dbCommand, "@StopSale", DbType.Boolean, iitems.StopSale);
            db.AddInParameter(dbCommand, "@StopSaleRemarks", DbType.String, iitems.StopSaleRemarks);
            db.AddInParameter(dbCommand, "@HSNCode", DbType.String, iitems.HSNCode);
            db.AddInParameter(dbCommand, "@FK_Schedule", DbType.Int64, iitems.FK_Schedule);
            db.AddInParameter(dbCommand, "@StoragePlace", DbType.String, iitems.StoragePlace);
            db.AddInParameter(dbCommand, "@NotReturnable", DbType.String, iitems.NotReturnable);
            db.AddInParameter(dbCommand, "@ReturnBeforDays", DbType.Int32, iitems.ReturnBeforDays);
            db.AddInParameter(dbCommand, "@Packed", DbType.Boolean, iitems.Packed);
            db.AddInParameter(dbCommand, "@Combo", DbType.Boolean, iitems.Combo);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iitems.UserCode);
            db.AddInParameter(dbCommand, "@XmlItemsDetails", DbType.Xml, iitems.XmlItemsDetails);
            db.AddInParameter(dbCommand, "@XmlTaxDetails", DbType.Xml, iitems.XmlTaxDetails);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iitems.FK_Client);
            db.AddInParameter(dbCommand, "@ExpiryWarningBefore", DbType.Int32, iitems.ExpiryWarningBefore);
         
            

           
          

         

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
                
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iitems, e);
                return 0;
            }
        }

        public IItems SelectItems(IItems iitems)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, iitems.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
            db.AddInParameter(dbCommand, "@ItemName", DbType.String, iitems.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iitems.ShortName);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, iitems.FK_CategoryFirst);
            db.AddInParameter(dbCommand, "@FK_SubCategory", DbType.Int64, iitems.FK_CategorySecond);
            db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int64, iitems.FK_Manufacturer);
            db.AddInParameter(dbCommand, "@FK_Generic", DbType.Int64, iitems.FK_Generic);
            db.AddInParameter(dbCommand, "@Combo", DbType.Int32, iitems.Combo);
            db.AddInParameter(dbCommand, "@FK_Schedule", DbType.Int64, iitems.FK_Schedule);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iitems.Name = Convert.ToString(dataReader["ItemName"].ToString());
                        iitems.ShortName = Convert.ToString(dataReader["ShortName"].ToString());
                        iitems.FK_CategoryFirst = Convert.ToInt64(dataReader["FK_Category"].ToString());
                        iitems.FK_CategorySecond = Convert.ToInt64(dataReader["FK_SubCategory"].ToString());
                        iitems.FK_Units = Convert.ToInt64(dataReader["FK_Units"].ToString());
                        iitems.FK_Manufacturer = Convert.ToInt64(dataReader["FK_Manufacturer"].ToString());
                        iitems.FK_Rack = Convert.ToInt64(dataReader["FK_Rack"].ToString());
                        iitems.MinLevel = Convert.ToDecimal(dataReader["MinLevel"].ToString());
                        iitems.MaxLevel = Convert.ToDecimal(dataReader["MaxLevel"].ToString());
                        iitems.ROLevel = Convert.ToDecimal(dataReader["ReOrderLevel"].ToString());
                        iitems.ROQuantity = Convert.ToDecimal(dataReader["ReOrderQuantity"].ToString());
                        iitems.DangerLevel = Convert.ToDecimal(dataReader["DangerLevel"].ToString());
                        iitems.StopSale = Convert.ToBoolean(dataReader["StopSale"].ToString());
                        iitems.StopSaleRemarks = Convert.ToString(dataReader["StopSaleRemarks"].ToString());
                        iitems.FK_Generic = Convert.ToInt64(dataReader["FK_Generic"].ToString());
                        iitems.FK_Bin = Convert.ToInt64(dataReader["FK_Bin"].ToString());
                        iitems.FK_SubUnit = Convert.ToInt64(dataReader["FK_SubUnit"].ToString());
                        iitems.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iitems.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iitems.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        iitems.HSNCode = Convert.ToString(dataReader["HSNCode"]);
                        iitems.Packed = Convert.ToBoolean(dataReader["Packed"]);
                        iitems.ExpairyValidation = Convert.ToInt32(dataReader["ExpairyValidation"]);
                        iitems.FK_Schedule = Convert.ToInt64(dataReader["FK_Schedule"].ToString());
                        iitems.ReturnBeforDays = Convert. ToInt32(dataReader["ReturnBeforDays"]);
                        iitems.NotReturnable = Convert.ToBoolean(dataReader["NotReturnable"]);
                        iitems.StoragePlace = Convert.ToString(dataReader["StoragePlace"]);
                        iitems.ExpiryWarningBefore = Convert.ToInt32(dataReader["ExpiryWarningBefore"]);
                    }
                }
                return iitems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }


        public DataTable SelectExpiryItems(IItems iitems)
		{
			DataTable dtblItems = new DataTable();
			Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proExpiryItemsSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,iitems.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, iitems.FK_Supplier);
            db.AddInParameter(dbCommand, "@ExpiryDate", DbType.String, iitems.ExpiryDate.ToString().Trim() == "" ? "" : Convert.ToDateTime(iitems.ExpiryDate).ToString("yyyy/MM/dd"));
            try
			{
				 dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblItems;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(iitems, e);
                throw e;
			}
        }



        public DataTable SelectAllItems(IItems iitems)
		{
			DataTable dtblItems = new DataTable();
			Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proItemsSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Items",DbType.Int64,iitems.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,iitems.FK_Client);
            db.AddInParameter(dbCommand, "@ItemName", DbType.String, iitems.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iitems.ShortName);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, iitems.FK_CategoryFirst);
            db.AddInParameter(dbCommand, "@FK_SubCategory", DbType.Int64, iitems.FK_CategorySecond);
            db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int64, iitems.FK_Manufacturer);
           db.AddInParameter(dbCommand, "@FK_Generic", DbType.Int64, iitems.FK_Generic);
            db.AddInParameter(dbCommand, "@Combo", DbType.Int32, iitems.Combo);
            db.AddInParameter(dbCommand, "@FK_Schedule", DbType.Int64, iitems.FK_Schedule);
			try
			{
				 dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblItems;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(iitems, e);
                throw e;
			}
        }
        public DataTable proShowInCounterItems(IItems iitems)
        {
            DataTable dtblCounter = new DataTable(); 
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proShowInCounterItems";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
          // db.AddInParameter(dbCommand, "@FK_Shift", DbType.Int64, iitems.FK_Shift);
           try
           {
               dtblCounter = db.ExecuteDataSet(dbCommand).Tables[0];
               return dtblCounter;
           }
           catch (SqlException e)
           {
               //UpdateErrorLog(iitems, e);
               throw e;
           }
        }
        public DataTable BarcodeGeneration(IItems iitems)
        {
            DataTable dtbl = new DataTable();
             Database db = DatabaseFactory.CreateDatabase("WebPharma");
             string sqlCommand = "proBarcodeGeneration";
             DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
             db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
             db.AddInParameter(dbCommand, "@ID_item", DbType.Int64, iitems.MasterId);
             try
             {
                 dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                 return dtbl;
             }
             catch (SqlException e)
             {
                 //UpdateErrorLog(iitems, e);
                 throw e;
             }            
        }
        public DataTable proItemsSelectPopUp(IItems iitems)
		{
			DataTable dtblItems = new DataTable();
			Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsSelectPopUp";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Items",DbType.Int64,iitems.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iitems.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iitems.Name);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, iitems.FK_CategoryFirst);
            db.AddInParameter(dbCommand, "@FK_SubCategory", DbType.Int64, iitems.FK_CategorySecond);
            db.AddInParameter(dbCommand, "@FK_Units", DbType.Int64, iitems.FK_Units);
            db.AddInParameter(dbCommand, "@FK_SubUnit", DbType.Int64, iitems.FK_SubUnit);
            db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int64, iitems.FK_Manufacturer);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,iitems.FK_Client);
			try
			{
				 dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblItems;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(iitems, e);
                throw e;
			}
        }
        public DataTable proItemsSelectPopUpFromQuotation(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsSelectPopUpPurchaseQuotation";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, iitems.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iitems.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iitems.Name);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, iitems.FK_CategoryFirst);
            db.AddInParameter(dbCommand, "@FK_SubCategory", DbType.Int64, iitems.FK_CategorySecond);
            db.AddInParameter(dbCommand, "@FK_Units", DbType.Int64, iitems.FK_Units);
            db.AddInParameter(dbCommand, "@FK_SubUnit", DbType.Int64, iitems.FK_SubUnit);
            db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int64, iitems.FK_Manufacturer);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, iitems.FK_Supplier);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, iitems.Date);
            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }

        public DataTable ItemsSearchForPriceCheck(IItems iitems)
        {
            DataTable dtblCounter = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsSearchForPriceCheck";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
           // db.AddInParameter(dbCommand, "@Barcode", DbType.String, iitems.Barcode);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iitems.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iitems.Name);
            try
            {
                dtblCounter = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblCounter;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
         public DataTable StockItemsSelect(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStockItemsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iitems.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iitems.ShortName);
           // db.AddInParameter(dbCommand, "@Barcode", DbType.String, iitems.Barcode);
           // db.AddInParameter(dbCommand, "@Batch", DbType.String, iitems.Batch);
          //  db.AddInParameter(dbCommand, "@SalesPrice", DbType.Decimal, iitems.SalesPrice);
         //   db.AddInParameter(dbCommand, "@FK_Shift", DbType.Int64, iitems.FK_Shift);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iitems.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iitems.PageSize);
           // db.AddInParameter(dbCommand, "@FK_Color", DbType.Int64, iitems.FK_Color);
           // db.AddInParameter(dbCommand, "@FK_Size", DbType.Int64, iitems.FK_Size);
          //  db.AddInParameter(dbCommand, "@FK_Brand", DbType.Int64, iitems.FK_Brand);
          //  db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, iitems.FK_supplier);
            db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int64, iitems.FK_Manufacturer);
            db.AddInParameter(dbCommand, "@FK_CategoryFirst", DbType.Int64, iitems.FK_CategoryFirst);
            db.AddInParameter(dbCommand, "@FK_CategorySecond", DbType.Int64, iitems.FK_CategorySecond);
            db.AddInParameter(dbCommand, "@FK_CategoryThird", DbType.Int64, iitems.FK_CategoryThird);
           // db.AddInParameter(dbCommand, "@FK_Storage", DbType.Int64, iitems.FK_Storage);
      





            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
         public DataTable SelectCurrentStock(IItems iitems)
         {
             DataTable dtblItems = new DataTable();
             Database db = DatabaseFactory.CreateDatabase("WebPharma");
             string sqlCommand = "proSelectCurrentStockItem";
             DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
             db.AddInParameter(dbCommand, "@Name", DbType.String, iitems.Name);
             db.AddInParameter(dbCommand, "@ShortName", DbType.String, iitems.ShortName);     
             db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
             db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, iitems.FK_Supplier);
             db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int64, iitems.FK_Manufacturer);
             db.AddInParameter(dbCommand, "@FK_CategoryFirst", DbType.Int64, iitems.FK_CategoryFirst);
             db.AddInParameter(dbCommand, "@Batch", DbType.String, iitems.BatchNo);
             db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iitems.FK_Store);
             db.AddInParameter(dbCommand, "@MRP", DbType.Decimal, iitems.MRP);
             db.AddInParameter(dbCommand, "@NotReturnable", DbType.Decimal, iitems.NotReturnable);
             db.AddInParameter(dbCommand, "@ExpiryDate", DbType.String, iitems.ExpiryDate.ToString().Trim()==""?"":Convert.ToDateTime(iitems.ExpiryDate).ToString("yyyy/MM/dd"));
             db.AddInParameter(dbCommand, "@Mode", DbType.String, iitems.Mode.ToString().Trim() == "" ? "" : (iitems.Mode.ToString()));
             try
             {
                 dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                 return dtblItems;
             }
             catch (SqlException e)
             {
                 //UpdateErrorLog(iitems, e);
                 throw e;
             }
         }
         public DataTable SelectCurrentStockWithBatch(IItems iitems)
         {
             DataTable dtblItems = new DataTable();
             Database db = DatabaseFactory.CreateDatabase("WebPharma");
             string sqlCommand = "proSelectCurrentStockWithBatch";
             DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
             db.AddInParameter(dbCommand, "@Name", DbType.String, iitems.Name);
             db.AddInParameter(dbCommand, "@ShortName", DbType.String, iitems.ShortName);
             db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
             db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iitems.PageIndex);
             db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iitems.PageSize);
             db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int64, iitems.FK_Manufacturer);
             db.AddInParameter(dbCommand, "@FK_CategoryFirst", DbType.Int64, iitems.FK_CategoryFirst);
             db.AddInParameter(dbCommand, "@FK_CategorySecond", DbType.Int64, iitems.FK_CategorySecond);
             db.AddInParameter(dbCommand, "@FK_CategoryThird", DbType.Int64, iitems.FK_CategoryThird);
//db.AddInParameter(dbCommand, "@FK_Storage", DbType.Int64, iitems.FK_Storage);
            // db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iitems.FK_Store);
             db.AddInParameter(dbCommand, "@FK_Units", DbType.Int64, iitems.FK_Units);
             try
             {
                 dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                 return dtblItems;
             }
             catch (SqlException e)
             {
                 //UpdateErrorLog(iitems, e);
                 throw e;
             }
         }
        public DataTable ItemsSelectForReport(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsSelectForReport";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iitems.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iitems.ShortName);
            db.AddInParameter(dbCommand, "@FK_CategoryFirst", DbType.Int64, iitems.FK_CategoryFirst);
            db.AddInParameter(dbCommand, "@FK_CategorySecond", DbType.Int64, iitems.FK_CategorySecond);
            db.AddInParameter(dbCommand, "@FK_CategoryThird", DbType.Int64, iitems.FK_CategoryThird);
           // db.AddInParameter(dbCommand, "@FK_Brand", DbType.Int64, iitems.FK_Brand);
            db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int64, iitems.FK_Manufacturer);
           // db.AddInParameter(dbCommand, "@Barcode", DbType.String, iitems.Barcode);

            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        public DataTable ItemsSelectCurrentStock(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ItemsSelectCurrentStockforPurchaseOrder";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, iitems.MasterId);

            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        public DataTable selectUsercode(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "Proselectusercode";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@userID", DbType.Int32, iitems.FK_User);
            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        public DataTable ItemsSelectForStockCheck(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsSelectForStockCheck";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iitems.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iitems.ShortName);
            //db.AddInParameter(dbCommand, "@Barcode", DbType.String, iitems.Barcode);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iitems.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iitems.PageSize);
            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        public DataTable ItemsSelectForStockInPriceConversion(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsSelectForStockInPriceConversion";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
           // db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iitems.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, iitems.MasterId);


            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        public DataTable ItemsSelectForStockDetails(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsSelectForStockDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iitems.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iitems.ShortName);
           // db.AddInParameter(dbCommand, "@Barcode", DbType.String, iitems.Barcode);
           
            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        public DataTable ItemsSelectForDropdownFill(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsSelectForDropdownFill";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
          
           
            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        public DataTable ItemsSelectForDropdownFillFromStore(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsSelectForDropdownFillFromStore";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
           // db.AddInParameter(dbCommand, "@FK_Store", DbType.Int32, iitems.FK_Store);

            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        public DataTable SelectItemBatch(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectItemBatch";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
          
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int32, iitems.MasterId);
           // db.AddInParameter(dbCommand, "@FK_Store", DbType.Int32, iitems.FK_Store);
            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        public DataTable SelectItemsDetails(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectItemsDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int32, iitems.MasterId);          
            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        public DataTable SelectTaxDetails(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsTaxSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int32, iitems.MasterId);
            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        
        public int DeleteItems(IItems iitems)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, iitems.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iitems.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, iitems.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        public int GetPartNo(IItems IItems)
        {
              Database db = DatabaseFactory.CreateDatabase("WebPharma");
              string sqlCommand = "proCheckPartNo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           // db.AddInParameter(dbCommand, "@PartNo", DbType.String,IItems.PartNo);
            try
            {
                return int.Parse((db.ExecuteScalar(dbCommand).ToString()));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }

        public int GetMinitemCount(IItems IItems)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSetMinItemLevelNotification";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IItems.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.Int64, IItems.UserCode);
            try
            {
                return int.Parse(db.ExecuteScalar(dbCommand).ToString());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public DataTable GetMinitemName(IItems IItems)
        {
           string sqlCommand = "";
           DbCommand dbCommand;
           DataTable dt = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           sqlCommand = "ProGetMinLevelItemName";
           dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64,IItems.FK_Client);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, IItems.UserCode);
           try
           {
               DataSet ds = new DataSet();
               ds = db.ExecuteDataSet(dbCommand);
               if (ds.Tables.Count > 0)
               {
                   dt = ds.Tables[0];
               }
               return dt;
           }
           catch (Exception e)
           {
               throw;
           }
       
        }
      
        public string ImportItems(IItems iitems)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proImportItems";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@xmlItems", DbType.Xml, iitems.XmlImport);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iitems.UserCode);
           try
            {
                return Convert.ToString(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }


        public DataTable SelectBarcode(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBarcodeGenerationItem";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            //db.AddInParameter(dbCommand, "@ID_Items", DbType.Int32, iitems.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iitems.FK_Client);
            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        public int ItemExistenceChecking(IItems IItems)
        {

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemNameExistence";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ItemName", DbType.String, IItems.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, IItems.ShortName);
            

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectItemWiseTaxDetails(IItems iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemWiseTaxSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, iitems.MasterId);
            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
    }
}
