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
using System.Linq;
using System.Text;
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
    public class DalReason
    {
        public Int32 UpdateReason(IReason iReason)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proReasonUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iReason.UserAction);
            db.AddInParameter(dbCommand, "@ID_Reason", DbType.Int64, iReason.MasterId);
            db.AddInParameter(dbCommand, "@Reason", DbType.String, iReason.Reason);
            db.AddInParameter(dbCommand, "@ReasonMode", DbType.String, iReason.ReasonMode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iReason.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iReason.UserCode);
                        
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(icategoryfirst, e);
                return 0;
            }
        }

        public IReason SelectReason(IReason iReason)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proReasonSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Reason", DbType.Int64, iReason.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iReason.FK_Client);
            db.AddInParameter(dbCommand, "@Reason", DbType.String, iReason.Reason);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        
                        iReason.Reason = Convert.ToString(dataReader["Reason"].ToString());
                        iReason.ReasonMode = Convert.ToString(dataReader["ReasonMode"].ToString());
                        iReason.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iReason.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iReason.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                       
                    }
                }
                return iReason;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategoryfirst, e);
                throw e;
            }
        }

        public DataTable SelectAllReason(IReason iReason)
        {
            DataTable dtblReason = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proReasonSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Reason", DbType.Int64, iReason.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iReason.FK_Client);
            db.AddInParameter(dbCommand, "@Reason", DbType.String, iReason.Reason);
            
            try
            {
                dtblReason = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblReason;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategoryfirst, e);
                throw e;
            }

            




        }

        public int DeleteReason(IReason iReason)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proReasonDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Reason", DbType.Int64, iReason.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iReason.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, iReason.Reason);
            ////db.AddInParameter(dbCommand, "@TransMode", DbType.String, icategoryfirst.TransMode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iReason.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategoryfirst, e);
                throw e;
            }
        }
    }
}
