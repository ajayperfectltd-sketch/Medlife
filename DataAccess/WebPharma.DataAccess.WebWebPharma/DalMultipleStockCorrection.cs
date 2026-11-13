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
    public class DalMultipleStockCorrection
    {
        public DataTable SelectStockDetails(IMultipleStockCorrection imultiplestockCorrection)
        {
            DataTable dtblStockDetails = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStockDetailsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, imultiplestockCorrection.FK_Items);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, imultiplestockCorrection.FK_Client);
            try
            {
                dtblStockDetails = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblStockDetails;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public Int32 UpdateMultipleStockEntry(IMultipleStockCorrection imultiplestockCorrection)
        {

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProMultipleStockCorrectionUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, imultiplestockCorrection.UserAction);
            db.AddInParameter(dbCommand, "@ID_MultipleStockCorrection", DbType.Int64, imultiplestockCorrection.MasterId);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, imultiplestockCorrection.FK_Items);
            db.AddInParameter(dbCommand, "@XmlMultipleStocks", DbType.Xml, imultiplestockCorrection.XmlMultipleStocks);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, imultiplestockCorrection.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, imultiplestockCorrection.FK_Client);
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
    }
}
