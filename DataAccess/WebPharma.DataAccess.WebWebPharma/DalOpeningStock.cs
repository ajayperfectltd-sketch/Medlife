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
    public class DalOpeningStock
    {
        
        public Int32 UpdateOpeningStock(IOpeningStock iopeningstock)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("WebPharma");
                string sqlCommand = "proOpeningStocksUpdate";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iopeningstock.UserAction);
                db.AddInParameter(dbCommand, "@ID_OpeningStock", DbType.Int64, iopeningstock.MasterId);
                db.AddInParameter(dbCommand, "@GroupID", DbType.Byte, iopeningstock.GroupID);
                db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iopeningstock.FK_Store);
                db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iopeningstock.FK_Client);
                db.AddInParameter(dbCommand, "@UserCode", DbType.String, iopeningstock.UserCode);
                db.AddInParameter(dbCommand, "@XmlOpening", DbType.String, iopeningstock.xmlOpening);


                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                return 0;
            }
        }
        public Int32 UpdateMultipleOpeningStock(IOpeningStock iopeningstock)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("WebPharma");
                string sqlCommand = "proMultipleOpeningStocksUpdate";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iopeningstock.UserAction);
                db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iopeningstock.FK_Store);
                db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iopeningstock.FK_Client);
                db.AddInParameter(dbCommand, "@UserCode", DbType.String, iopeningstock.UserCode);
                db.AddInParameter(dbCommand, "@XmlOpening", DbType.String, iopeningstock.xmlOpening);


                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                return 0;
            }
        }
        public DataTable ValidateExcelData(IOpeningStock iopeningstock)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proOpeningValidateExcel";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@XmlExcelImport", DbType.Xml, iopeningstock.xmlOpening);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iopeningstock.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iopeningstock.UserCode);
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
        public IOpeningStock SelectOpeningStock(IOpeningStock iopeningstock)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proOpeningStockSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_OpeningStock", DbType.Int64, iopeningstock.MasterId);
            db.AddInParameter(dbCommand, "@ItemName", DbType.String, "");
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iopeningstock.FK_Client);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iopeningstock.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iopeningstock.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iopeningstock.MasterId = Convert.ToInt64(dataReader["ID_OpeningStock"].ToString());
                        //iopeningstock.FK_Items = Convert.ToInt64(dataReader["FK_Items"].ToString());
                        //iopeningstock.ItemName = Convert.ToString(dataReader["Item"].ToString());
                        //iopeningstock.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                        //iopeningstock.StoreName = Convert.ToString(dataReader["Store"].ToString());
                        //iopeningstock.BatchNo = Convert.ToString(dataReader["BatchNo"].ToString());
                        //iopeningstock.ExpiryDate = Convert.ToDateTime(dataReader["ExpiryDate"].ToString());
                        //iopeningstock.PurRate = Convert.ToDecimal(dataReader["PurRate"].ToString());
                        //iopeningstock.MRP = Convert.ToDecimal(dataReader["MRP"].ToString());
                        ////iopeningstock.RetailPrice = Convert.ToDecimal(dataReader["RetailPrice"].ToString());
                        //iopeningstock.WholeSalePrice = Convert.ToDecimal(dataReader["WholeSalePrice"].ToString());
                        //iopeningstock.PrivilagePrice = Convert.ToDecimal(dataReader["PrivilagePrice"].ToString());
                        //iopeningstock.VAT = Convert.ToDecimal(dataReader["VAT"].ToString());
                        //iopeningstock.OpeningStock = Convert.ToDecimal(dataReader["OpeningStock"].ToString());
                        //iopeningstock.BarCode = Convert.ToString(dataReader["BarCode"].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iopeningstock;
        }
        public DataTable SelectAllOpeningStock(IOpeningStock iopeningstock)
        {
            DataTable dtblOpeningStock = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proOpeningStockSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_OpeningStock", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@ID_Store", DbType.Int64, iopeningstock.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iopeningstock.FK_Client);
            db.AddInParameter(dbCommand, "@BatchNo", DbType.String, iopeningstock.BatchNo);
            db.AddInParameter(dbCommand, "@ItemName", DbType.String, iopeningstock.ItemName);

           
            try
            {
                dtblOpeningStock = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblOpeningStock;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable OpeningStockSelectGrid(IOpeningStock iopeningstock)
        {
            DataTable dtblOpening = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proOpeningStockSelectGrid";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Stock", DbType.Int64, iopeningstock.MasterId);
            db.AddInParameter(dbCommand, "@Fk_Store", DbType.Int64, iopeningstock.FK_Store);
            try
            {
                dtblOpening = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblOpening;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public DataTable SelectMultipleOpeningStock(IOpeningStock iopeningstock)
        {
            DataTable dtblOpeningStock = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMultipleOpeningStockSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            //db.AddInParameter(dbCommand, "@FK_OpeningStock", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@ID_Store", DbType.Int64, iopeningstock.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iopeningstock.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, iopeningstock.FK_Category);
            db.AddInParameter(dbCommand, "@FK_SubCategory", DbType.Int64, iopeningstock.FK_SubCategory);
            db.AddInParameter(dbCommand, "@ItemName", DbType.String, iopeningstock.ItemName);
            db.AddInParameter(dbCommand, "@FK_Generic", DbType.Int64, iopeningstock.FK_Generic);
            db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int64, iopeningstock.FK_Manufacturer);
            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iopeningstock.FK_Rack);
            db.AddInParameter(dbCommand, "@FK_Bin", DbType.Int64, iopeningstock.FK_Bin);
            db.AddInParameter(dbCommand, "@BatchNo", DbType.String, iopeningstock.BatchNo);


            try
            {
               var ds = db.ExecuteDataSet(dbCommand);
               if (ds.Tables.Count > 0)
               {
                   dtblOpeningStock = ds.Tables[0];
               }
                return dtblOpeningStock;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
     

    }
}
