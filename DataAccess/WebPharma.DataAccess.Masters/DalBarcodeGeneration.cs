using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using WebPharma.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.Interface.Masters;
using WebPharma.Interface.General;

namespace WebPharma.DataAccess.Masters
{
   public class DalBarcodeGeneration
    {
        public Int32 UpdateBarcodeGeneration(IBarcodeGeneration iBarcodeGeneration)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBarcodeGenerationUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iBarcodeGeneration.UserAction);
            db.AddInParameter(dbCommand, "@ID_BarcodeGeneration", DbType.Int64, iBarcodeGeneration.MasterId);
            db.AddInParameter(dbCommand, "@FK_StockCode", DbType.Int64, iBarcodeGeneration.FK_StockCode);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, iBarcodeGeneration.FK_Items);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iBarcodeGeneration.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iBarcodeGeneration.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iBarcodeGeneration.FK_Client);
            db.AddInParameter(dbCommand, "@BarCode", DbType.String, iBarcodeGeneration.BarCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public int DeleteBarcodeGeneration(IBarcodeGeneration iBarcodeGeneration)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBarcodeGenerationDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_BarcodeGeneration", DbType.Int64, iBarcodeGeneration.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iBarcodeGeneration.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public IBarcodeGeneration SelectBarcodeGeneration(IBarcodeGeneration iBarcodeGeneration)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBarcodeGenerationSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_BarcodeGeneration", DbType.Int64, iBarcodeGeneration.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iBarcodeGeneration.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iBarcodeGeneration.Name);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iBarcodeGeneration.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iBarcodeGeneration.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iBarcodeGeneration.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iBarcodeGeneration.ShortName = dataReader["ShortName"].ToString();
                        iBarcodeGeneration.Name = dataReader["Name"].ToString();
                        iBarcodeGeneration.Address1 = dataReader["Address1"].ToString();
                        iBarcodeGeneration.Address2 = dataReader["Address2"].ToString();
                        iBarcodeGeneration.Address3 = dataReader["Address3"].ToString();
                        iBarcodeGeneration.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                    }
                }
                return iBarcodeGeneration;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectAllBarcodeGeneration(IBarcodeGeneration iBarcodeGeneration)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBarcodeGenerationSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_BarcodeGeneration", DbType.Int64, iBarcodeGeneration.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iBarcodeGeneration.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iBarcodeGeneration.Name);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iBarcodeGeneration.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iBarcodeGeneration.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iBarcodeGeneration.FK_Client);
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
        public DataTable SelectPrintLayout(IBarcodeGeneration iBarcodeGeneration)
        {
            DataTable dtblSales = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSalesPrintLayout";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iBarcodeGeneration.FK_Client);
            db.AddInParameter(dbCommand, "@PriModule", DbType.String, iBarcodeGeneration.PriLayout);
            try
            {
                dtblSales = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblSales;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public DataTable SelectPrintLayoutSalesReturn(IBarcodeGeneration iBarcodeGeneration)
        {
            DataTable dtblSales = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSalesReturnPrintLayout";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iBarcodeGeneration.FK_Client);
            db.AddInParameter(dbCommand, "@PriModule", DbType.String, iBarcodeGeneration.PriLayout);
            try
            {
                dtblSales = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblSales;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public DataTable SelectWeighingMachineItemsSelect(IBarcodeGeneration iBarcodeGeneration)
        {
            DataTable dtblSales = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSelectWeighingMachineItemsSelectPrint";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iBarcodeGeneration.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, iBarcodeGeneration.FK_Items);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iBarcodeGeneration.UserCode);
            try
            {
                dtblSales = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblSales;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }

        public DataTable SelectWeighingMachineHistory(int PageIndex, int PageSize, Int64 FK_Client)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proWeighingMachineHistorySelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, FK_Client);
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
