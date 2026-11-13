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
    public class DalStock
    {

        public Int32 UpdateStock(IStock istock)
        {
            //try
            //{
            //    Database db = DatabaseFactory.CreateDatabase("WebPharma");
            //    string sqlCommand = "ProUpdateStock";
            //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            //    db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, istock.UserAction);
            //    db.AddInParameter(dbCommand, "@FK_Stock", DbType.Int64, istock.MasterId);
            //    db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, istock.FK_Store);
            //    db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, istock.FK_Items);
            //    db.AddInParameter(dbCommand, "@BatchNo", DbType.String, istock.BatchNo);

            //    db.AddInParameter(dbCommand, "@ExpDate", DbType.DateTime, istock.ExpiryDate);
            //    db.AddInParameter(dbCommand, "@PurRate", DbType.Decimal, istock.PurRate);
            //    db.AddInParameter(dbCommand, "@MRP", DbType.Decimal, istock.MRP);
            //    db.AddInParameter(dbCommand, "@RetailPrice", DbType.Decimal, istock.RetailPrice);
            //    db.AddInParameter(dbCommand, "@WholesalePrice", DbType.Decimal, istock.WholeSalePrice);
            //    db.AddInParameter(dbCommand, "@PrivilagePrice", DbType.Decimal, istock.PrivilagePrice);

            //    db.AddInParameter(dbCommand, "@VAT", DbType.Decimal, istock.VAT);
            //    db.AddInParameter(dbCommand, "@Quantity", DbType.Decimal, 0);
            //    db.AddInParameter(dbCommand, "@AddorSub", DbType.Int16, 0);
            //    db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, istock.FK_Client);
            //    db.AddInParameter(dbCommand, "@EnteredBy", DbType.String, istock.UserCode);
            //    db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, istock.TransDate);
            //    db.AddInParameter(dbCommand, "@xmlStockItems", DbType.Xml, istock.xmlStockItems);


            //    return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            //}
            //catch (SqlException e)
            //{
            // UpdateErrorLog(istock, e);
            return 0;
            //}
        }

        public IStock SelectStock(IStock istock)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStockSelectGrid";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Stock", DbType.Int64, istock.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, istock.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, istock.FK_Items);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, istock.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Tostore", DbType.Int64, istock.Tostore);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        istock.FK_Items = Convert.ToInt64(dataReader["FK_Items"].ToString());
                        istock.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                        istock.FK_RackArrangement = Convert.ToInt64(dataReader["FK_Rack"].ToString());
                        istock.BatchNo = dataReader["BatchNo"].ToString();
                        istock.ExpiryDate = Convert.ToDateTime(dataReader["ExpiryDate"].ToString());
                        istock.PurRate = Convert.ToDecimal(dataReader["PurRate"].ToString());
                        istock.MRP = Convert.ToDecimal(dataReader["MRP"].ToString());
                        istock.OpeningStock = Convert.ToDecimal(dataReader["OpeningStock"].ToString());
                        istock.CurrentStock = Convert.ToDecimal(dataReader["CurrentStock"].ToString());
                        istock.SalePrice = Convert.ToDecimal(dataReader["SalPrice"].ToString());
                        istock.ActualSalPrice = Convert.ToDecimal(dataReader["ActualSalPrice"].ToString());
                        istock.Tostore = Convert.ToDecimal(dataReader["ToSTock"].ToString());
                        //istock.Off_Free = Convert.ToDecimal(dataReader["Off_Free"].ToString());
                        //istock.Off_FreePerUnit = Convert.ToDecimal(dataReader["Off_FreePerUnit"].ToString());
                        //istock.Off_FK_Stock = Convert.ToDecimal(dataReader["Off_FK_Stock"].ToString());
                        //istock.Off_Discount = Convert.ToDecimal(dataReader["Off_Discount"].ToString());
                        istock.Unit = Convert.ToString(dataReader["Unit"].ToString());
                        istock.NoOfUnits = Convert.ToInt16(dataReader["NoofUnits"].ToString());
                        istock.FK_Units = Convert.ToInt64(dataReader["FK_Unit"].ToString());
                        istock.PurchaseTaxAmount = Convert.ToDecimal(dataReader["PurchaseTaxAmount"].ToString());
                        istock.PurchaseDiscPercentage = Convert.ToDecimal(dataReader["PurchaseDiscPercentage"].ToString());
                        istock.SalesTaxAmount = Convert.ToDecimal(dataReader["SalesTaxAmount"].ToString());
                        //istock.FK_SubUnit = Convert.ToInt64(dataReader["FK_SubUnit"].ToString());

                    }
                }
                return istock;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public DataTable SelectAllStock(IStock istock)
        {
            DataTable dtblStock = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStockSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Stock", DbType.Int64, istock.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, istock.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, istock.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, istock.FK_Items);
            try
            {
                dtblStock = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblStock;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }


        public DataTable SelectAllStockStockCorrection(IStock istock)
        {
            DataTable dtblStock = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStockSelectStockCorrection";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Stock", DbType.Int64, istock.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, istock.FK_Client);
            //db.AddInParameter(dbCommand, "@Code", DbType.String,istock.Code);
            //db.AddInParameter(dbCommand, "@Name", DbType.String, istock.Name);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, istock.FK_Category);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, istock.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, istock.FK_Items);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, istock.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, istock.PageSize);
            try
            {
                dtblStock = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblStock;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }

        public DataTable SelectAllDataForPriceConversion(IStock istock)
        {
            DataTable dtblStock = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStockSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Stock", DbType.Int64, istock.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, istock.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, istock.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, istock.FK_Items);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, istock.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, istock.PageSize);
            try
            {
                dtblStock = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblStock;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public DataTable SelectItemsPurchaseRate(IStock istock)
        {
            DataTable dtblStock = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectItemsPurchaseRate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            //db.AddInParameter(dbCommand, "@StockCode", DbType.Int64, istock.StockCode);
            //db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, istock.FK_Client);

            try
            {
                dtblStock = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblStock;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }

        //public string ImportData(IStock istock)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("WebPharma");
        //    string sqlCommand = "proImportStocks";
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        //    db.AddInParameter(dbCommand, "@xmlStock", DbType.String, istock.XmlImport);
        //    db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, istock.FK_Client);
        //    try
        //    {
        //        return Convert.ToString(db.ExecuteScalar(dbCommand));
        //    }
        //    catch (SqlException e)
        //    {
        //        //UpdateErrorLog(istock, e);
        //        throw e;
        //    }

        //}
        public int DeleteStock(IStock istock)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStockDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Stock", DbType.Int64, istock.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, istock.FK_Client);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public DataTable SelectStockDatewithPurchase(IStock istock)
        {
            DataTable dtblStock = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStockSelectGrid";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Stock", DbType.Int64, istock.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, istock.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, istock.FK_Items);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, istock.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Tostore", DbType.Int64, istock.Tostore);
            db.AddInParameter(dbCommand, "@Mode", DbType.String, istock.Mode);
            try
            {
                dtblStock = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblStock;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
    }
}
