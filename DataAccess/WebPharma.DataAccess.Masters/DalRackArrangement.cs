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
    public class DalRackArrangement
    {
        public Int32 UpdateRackArrangement(IRackArrangement iRackArrangement)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackArrangementUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iRackArrangement.UserAction);
            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iRackArrangement.FK_Rack);
            db.AddInParameter(dbCommand, "@xmlRackItems", DbType.String, iRackArrangement.xmlRackItems);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iRackArrangement.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iRackArrangement.UserCode);
            db.AddInParameter(dbCommand, "@GroupID", DbType.Int64, iRackArrangement.GroupID);
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

        public IRackArrangement SelectRackArrangement(IRackArrangement iRackArrangement)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackArrangementSelect"; 
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_RackArrangement", DbType.Int64, iRackArrangement.MasterId);
            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iRackArrangement.FK_Rack);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, iRackArrangement.FK_Item);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iRackArrangement.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iRackArrangement.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRackArrangement.FK_Client);
            db.AddInParameter(dbCommand, "@GroupID", DbType.Int64, iRackArrangement.GroupID);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        //IRackArrangement.Code = Convert.ToString(dataReader["Code"].ToString());
                        //IRackArrangement.RackName = Convert.ToString(dataReader["RackName"].ToString());
                        //IRackArrangement.Description = Convert.ToString(dataReader["Description"].ToString());
                        iRackArrangement.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iRackArrangement.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iRackArrangement.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return iRackArrangement;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategoryfirst, e);
                throw e;
            }

            
        }

        public DataTable SelectAllRackArrangement(IRackArrangement iRackArrangement)
        {
            DataTable dtblReason = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackArrangementSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_RackArrangement", DbType.Int64, iRackArrangement.MasterId);
            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iRackArrangement.FK_Rack);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, iRackArrangement.FK_Item);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iRackArrangement.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iRackArrangement.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRackArrangement.FK_Client);
            db.AddInParameter(dbCommand, "@GroupID", DbType.Int64, iRackArrangement.GroupID);
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
        public DataTable SelectAllRackArrangementGroupData(IRackArrangement iRackArrangement)
        {
            DataTable dtblReason = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackArrangementGroupSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iRackArrangement.FK_Rack);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, iRackArrangement.FK_Item);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iRackArrangement.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iRackArrangement.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRackArrangement.FK_Client);
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
        

        public int DeleteRackArrangement(IRackArrangement iRackArrangement)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackArrangementDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_RackArrangement", DbType.Int64, iRackArrangement.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRackArrangement.FK_Client);
            ////db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, icategoryfirst.ReasonCode);
            ////db.AddInParameter(dbCommand, "@TransMode", DbType.String, icategoryfirst.TransMode);
            ////db.AddInParameter(dbCommand, "@UserCode", DbType.String, icategoryfirst.UserCode);
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
        public int DeleteRackArrangementGroup(IRackArrangement iRackArrangement)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackArrangementGroupDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@GroupID", DbType.Int64, iRackArrangement.GroupID);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRackArrangement.FK_Client);
            ////db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, icategoryfirst.ReasonCode);
            ////db.AddInParameter(dbCommand, "@TransMode", DbType.String, icategoryfirst.TransMode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iRackArrangement.UserCode);
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
