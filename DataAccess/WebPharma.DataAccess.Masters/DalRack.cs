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
    public class DalRack
    {
        public Int32 UpdateRack(IRack iRack)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iRack.UserAction);
            db.AddInParameter(dbCommand, "@ID_Rack", DbType.Int64, iRack.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iRack.ShortName);
            db.AddInParameter(dbCommand, "@RackName", DbType.String, iRack.RackName);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iRack.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iRack.UserCode);

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

        public IRack SelectRack(IRack iRack)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Rack", DbType.Int64, iRack.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iRack.ShortName);
            db.AddInParameter(dbCommand, "@RackName", DbType.String, iRack.RackName);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRack.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iRack.ShortName = Convert.ToString(dataReader["ShortName"].ToString());
                        iRack.RackName = Convert.ToString(dataReader["RackName"].ToString());
                        iRack.ShortName = Convert.ToString(dataReader["ShortName"].ToString());
                        iRack.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iRack.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iRack.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return iRack;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategoryfirst, e);
                throw e;
            }
        }

        public DataTable SelectAllRack(IRack iRack)
        {
            DataTable dtblReason = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Rack", DbType.Int64, iRack.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iRack.ShortName);
            db.AddInParameter(dbCommand, "@RackName", DbType.String, iRack.RackName);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRack.FK_Client);
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
        public DataTable SelectRackPopup(IRack iRack)
        {
            DataTable dtblReason = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackSelectPopup";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Rack", DbType.Int64, iRack.MasterId);
            //db.AddInParameter(dbCommand, "@Code", DbType.String, iRack.Code);
            db.AddInParameter(dbCommand, "@RackName", DbType.String, iRack.RackName);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iRack.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iRack.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRack.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, iRack.FK_Supplier);
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

        public int DeleteRack(IRack iRack)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Rack", DbType.Int64, iRack.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRack.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, iRack.FK_Reason);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iRack.UserCode);
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
