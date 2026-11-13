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
    public class DalShift
    {

        public Int32 UpdateShift(IShift ishift)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proShiftUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ishift.UserAction);
            db.AddInParameter(dbCommand, "@ID_Shift", DbType.Int64, ishift.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ishift.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ishift.ShortName);
            db.AddInParameter(dbCommand, "@ShiftFrom", DbType.DateTime, ishift.ShiftFrom);
            db.AddInParameter(dbCommand, "@ShiftTo", DbType.DateTime, ishift.ShiftTo);
            db.AddInParameter(dbCommand, "@Active", DbType.Byte, ishift.Active);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ishift.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ishift.UserCode);


            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(ishift, e);
                return 0;
            }
        }

        public IShift SelectShift(IShift ishift)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proShiftSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Shift", DbType.Int64, ishift.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ishift.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ishift.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ishift.ShortName);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ishift.Name = Convert.ToString(dataReader["ShiftName"].ToString());
                        ishift.ShortName = Convert.ToString(dataReader["ShortName"].ToString());
                        ishift.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                        ishift.ShiftFrom = Convert.ToDateTime(dataReader["ShiftFrom"].ToString());
                        ishift.ShiftTo = Convert.ToDateTime(dataReader["ShiftTo"].ToString());
                        ishift.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        ishift.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        ishift.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return ishift;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(ishift, e);
                throw e;
            }
        }

        public DataTable SelectAllShift(IShift ishift)
		{
			DataTable dtblShift = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proShiftSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Shift",DbType.Int64,ishift.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,ishift.FK_Client);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ishift.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ishift.Name);
           
			try
			{
				 dtblShift = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblShift;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(ishift, e);
                throw e;
			}
		}

        public int DeleteShift(IShift ishift)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proShiftDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Shift", DbType.Int64, ishift.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ishift.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ishift.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, ishift.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(ishift, e);
                throw e;
            }
        }

        public DataSet SelectShiftCloseDetails(IShift ishift)
        {
            DataSet dset = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proShiftCloseDetailsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Shift", DbType.Int64, ishift.MasterId);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, ishift.Date);
            db.AddInParameter(dbCommand, "@Type", DbType.Int16, ishift.Type);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ishift.FK_Client);
            try
            {
                dset = db.ExecuteDataSet(dbCommand);
                return dset;
            }
            catch (SqlException e)
            {               
                throw e;
            }
        }

    }
}
