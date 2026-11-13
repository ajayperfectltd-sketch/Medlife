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
using System.Text;
using System.Linq;
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
    public class DalCounter
    {

        public Int32 UpdateCounter(ICounter icounter)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCounterUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, icounter.UserAction);
            db.AddInParameter(dbCommand, "@ID_Counter", DbType.Int64, icounter.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icounter.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, icounter.ShortName);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, icounter.FK_Store);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, icounter.Active);
            db.AddInParameter(dbCommand, "@FK_AccountHead", DbType.Int64, icounter.FK_AccountHead);
            db.AddInParameter(dbCommand, "@FK_AccountHeadSub", DbType.Int64, icounter.FK_AccountHeadSub);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icounter.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icounter.UserCode);
            db.AddInParameter(dbCommand, "@XmlShiftDetails", DbType.String, icounter.XmlShiftDetails);
           
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(icounter, e);
                return 0;
            }
        }

        public ICounter SelectCounter(ICounter icounter)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCounterSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Counter", DbType.Int64, icounter.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icounter.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icounter.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, icounter.Name);
            
            
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        icounter.Name = Convert.ToString(dataReader["CounterName"].ToString());
                        icounter.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                        icounter.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                        icounter.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        icounter.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        icounter.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        icounter.FK_AccountHead = Convert.ToInt64(dataReader["FK_AccountHead"]);
                        icounter.FK_AccountHeadSub = Convert.ToInt64(dataReader["FK_AccountHeadSub"]);
                    }
                }
                return icounter;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icounter, e);
                throw e;
            }
        }

        public DataTable SelectAllCounter(ICounter icounter)
		{
			DataTable dtblCounter = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proCounterSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Counter",DbType.Int64,icounter.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,icounter.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icounter.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, icounter.ShortName);
			try
			{
				 dtblCounter = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblCounter;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(icounter, e);
                throw e;
			}
		}
        public DataTable selectCounterShiftDetails(ICounter iitems)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCounterShiftDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Counters", DbType.Int32, iitems.MasterId);
            try
            {
                dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblItems;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iitems, e);
                throw e;
            }
        }
        

        public int DeleteCounter(ICounter icounter)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCounterDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Counter", DbType.Int64, icounter.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icounter.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icounter.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, icounter.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icounter, e);
                throw e;
            }
        }

    }
}
