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
    public class DalStockAdjustment
    {
        public Int32 UpdateStockAdjustment(IStockAdjustment iStockAdjustment)
        {

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProStockAdjustmentUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iStockAdjustment.UserAction);
            db.AddInParameter(dbCommand, "@ID_StockAdjustment", DbType.Int64, iStockAdjustment.MasterId);
            db.AddInParameter(dbCommand, "@Module", DbType.String, iStockAdjustment.Module);
            db.AddInParameter(dbCommand, "@FK_Module", DbType.Int64, iStockAdjustment.FK_Module);
         //   db.AddInParameter(dbCommand, "@FK_Stock", DbType.Int64, iStockAdjustment.FK_Stock);
            db.AddInParameter(dbCommand, "@FK_Item", DbType.Int64, iStockAdjustment.FK_Item);
            db.AddInParameter(dbCommand, "@Quantity", DbType.Int64, iStockAdjustment.Quantity);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iStockAdjustment.FK_Store);           
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iStockAdjustment.FK_Client);
            db.AddInParameter(dbCommand, "@AdjustmentID", DbType.Int64, iStockAdjustment.AdjustmentID);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, iStockAdjustment.FK_Category);
            db.AddInParameter(dbCommand, "@EnteredBy", DbType.String, iStockAdjustment.UserCode);
            db.AddInParameter(dbCommand, "@Approved", DbType.Boolean, iStockAdjustment.Approved);
            db.AddInParameter(dbCommand, "@ApprovedBy", DbType.String, iStockAdjustment.ApprovedBy);
             db.AddInParameter(dbCommand, "@ApprovedOn", DbType.DateTime, iStockAdjustment.EnteredOn);
            db.AddInParameter(dbCommand, "@Transdate", DbType.DateTime, iStockAdjustment.TransDate);
            db.AddInParameter(dbCommand, "@xmlStockAdjustmentItems", DbType.Xml, iStockAdjustment.XmlStockAdjustmentItems);
            
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
        
        public DataTable SelectStockAdjustmentItem(IStockAdjustment iStockAdjustment)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStockAdjustmentItemSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);                    
           
            db.AddInParameter(dbCommand, "@AdjustmentID", DbType.Int64, iStockAdjustment.AdjustmentID);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, iStockAdjustment.FK_Category);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iStockAdjustment.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iStockAdjustment.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, iStockAdjustment.FK_Item);
            db.AddInParameter(dbCommand, "@ItemName", DbType.String, iStockAdjustment.Name);
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
        public DataTable SelectAllStockAdjustment(IStockAdjustment iStockAdjustment)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStockAdjustmentSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_StockAdjustment", DbType.Int64, iStockAdjustment.MasterId);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, iStockAdjustment.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, iStockAdjustment.ToDate);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, iStockAdjustment.FK_Category);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iStockAdjustment.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iStockAdjustment.FK_Client);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iStockAdjustment.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iStockAdjustment.PageSize);

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
