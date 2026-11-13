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
    public class DalRackAllotment
    {
        public Int32 UpdateRackAllotment(IRackAllotment iRackAllotment)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackAllotmentUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iRackAllotment.FK_Rack);
            db.AddInParameter(dbCommand, "@ID_RackAllotment", DbType.Int64, iRackAllotment.MasterId);
            db.AddInParameter(dbCommand, "@AllotmentDate", DbType.DateTime, iRackAllotment.AllotmentDate);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, iRackAllotment.FK_Supplier);
            db.AddInParameter(dbCommand, "@PeriodType", DbType.Int32, iRackAllotment.PeriodType);
            db.AddInParameter(dbCommand, "@Period", DbType.Int32, iRackAllotment.Period);
            db.AddInParameter(dbCommand, "@DueDate", DbType.DateTime, iRackAllotment.DueDate);
            db.AddInParameter(dbCommand, "@Rent", DbType.Decimal, iRackAllotment.Rent);
            db.AddInParameter(dbCommand, "@Remarks", DbType.String, iRackAllotment.Remarks);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iRackAllotment.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iRackAllotment.UserCode);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iRackAllotment.UserAction);
            db.AddInParameter(dbCommand, "@Vacate", DbType.Boolean, iRackAllotment.Vacate);
            
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

        public IRackAllotment SelectRackAllotment(IRackAllotment iRackAllotment)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackAllotmentSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_RackAllotment", DbType.Int64, iRackAllotment.MasterId);
            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iRackAllotment.FK_Rack);
            db.AddInParameter(dbCommand, "@AllotmentDate", DbType.DateTime, iRackAllotment.AllotmentDate);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, iRackAllotment.FK_Supplier);
            db.AddInParameter(dbCommand, "@PeriodType", DbType.Int32, iRackAllotment.PeriodType);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iRackAllotment.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iRackAllotment.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRackAllotment.FK_Client);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    
                    {
                        iRackAllotment.FK_Rack = Convert.ToInt64(dataReader["FK_Rack"].ToString());
                        iRackAllotment.AllotmentDate = Convert.ToDateTime(dataReader["AllotmentDate"].ToString());
                        iRackAllotment.FK_Supplier = Convert.ToInt64(dataReader["FK_Supplier"].ToString());
                        iRackAllotment.PeriodType = Convert.ToInt32(dataReader["PeriodType"].ToString());
                        iRackAllotment.Period = Convert.ToInt32(dataReader["Period"].ToString());
                        iRackAllotment.DueDate = Convert.ToDateTime(dataReader["DueDate"].ToString());
                        iRackAllotment.Rent = Convert.ToDecimal(dataReader["Rent"].ToString());
                        iRackAllotment.Remarks =dataReader["Remarks"].ToString();

                        iRackAllotment.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iRackAllotment.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iRackAllotment.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return iRackAllotment;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategoryfirst, e);
                throw e;
            }

            	
        }

        public DataTable SelectAllRackAllotment(IRackAllotment iRackAllotment)
        {
            DataTable dtblReason = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackAllotmentSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@ID_RackAllotment", DbType.Int64, iRackAllotment.MasterId);
            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iRackAllotment.FK_Rack);
            db.AddInParameter(dbCommand, "@AllotmentDate", DbType.DateTime, iRackAllotment.AllotmentDate);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, iRackAllotment.FK_Supplier);
            db.AddInParameter(dbCommand, "@PeriodType", DbType.Int32, iRackAllotment.PeriodType);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iRackAllotment.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iRackAllotment.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRackAllotment.FK_Client);

                        
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



        public int DeleteRackAllotment(IRackAllotment iRackAllotment)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRackAllotmentDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_RackAllotment", DbType.Int64, iRackAllotment.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRackAllotment.FK_Client);
            ////db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, icategoryfirst.ReasonCode);
            ////db.AddInParameter(dbCommand, "@TransMode", DbType.String, icategoryfirst.TransMode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iRackAllotment.UserCode);
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
