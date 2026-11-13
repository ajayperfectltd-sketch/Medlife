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
    public class DalStockCorrection
    {
        public Int32 UpdateStockCorrection(IStockCorrection iStockCorrection)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("WebPharma");
                string sqlCommand = "ProStockCorrectionUpdate"; 
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iStockCorrection.UserAction);
                db.AddInParameter(dbCommand, "@ID_StockCorrection", DbType.Int64, iStockCorrection.MasterId);
                db.AddInParameter(dbCommand, "@FK_Stock", DbType.Int64, iStockCorrection.FK_Stock);
                db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iStockCorrection.FK_Store);
                db.AddInParameter(dbCommand, "@ItmOldStock", DbType.Decimal, iStockCorrection.ItmOldStock);
                db.AddInParameter(dbCommand, "@ItmNewStock", DbType.Decimal, iStockCorrection.ItmNewStock);
                db.AddInParameter(dbCommand, "@UsrCode", DbType.String, iStockCorrection.UserCode);
                db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iStockCorrection.EnteredOn);
                db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iStockCorrection.FK_Client);
                db.AddInParameter(dbCommand, "@xmlStockCorrectionItems", DbType.Xml, iStockCorrection.xmlStockCorrectionItems);

                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                return 0;
            }
        }

    }
}
