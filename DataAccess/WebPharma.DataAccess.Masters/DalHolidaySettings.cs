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
    public class DalHolidaySettings
    {
        public Int32 UpdateHolidaySettings(IHolidaySettings iHoliday)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proHolidaySettingsUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iHoliday.UserAction);
            db.AddInParameter(dbCommand, "@ID_HolidaySettings", DbType.Int64, iHoliday.MasterId);
            db.AddInParameter(dbCommand, "@Reason", DbType.String, iHoliday.Reason);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, iHoliday.Date);
            db.AddInParameter(dbCommand, "@Type", DbType.Byte, iHoliday.Type);
            db.AddInParameter(dbCommand, "@FromTime", DbType.DateTime, iHoliday.FromTime);
            db.AddInParameter(dbCommand, "@ToTime", DbType.DateTime, iHoliday.ToTime);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iHoliday.EnteredOn);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iHoliday.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iHoliday.FK_Client);
          
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iitems, e);
                return 0;
            }



        }
        public IHolidaySettings SelectHolidaySettings(IHolidaySettings Iholiday)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proHolidaySettingsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_HolidaySettings", DbType.Int64, Iholiday.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, Iholiday.FK_Client);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, Iholiday.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, Iholiday.PageSize);
            db.AddInParameter(dbCommand, "@Reason", DbType.String, Iholiday.Reason);
            db.AddInParameter(dbCommand, "@Type", DbType.String, Iholiday.Type);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, Iholiday.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, Iholiday.ToDate);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        Iholiday.Reason=Convert.ToString(dataReader["Reason"].ToString());
                        Iholiday.Date=DateTime.Parse(dataReader["Date"].ToString());
                        Iholiday.Type=Convert.ToByte(dataReader["Type"].ToString());
                        Iholiday.FromTime=Convert.ToDateTime(dataReader["FromTime"].ToString());
                        Iholiday.ToTime=Convert.ToDateTime(dataReader["ToTime"].ToString());
                    }
                }
                return Iholiday;   
            }
            catch (SqlException e)
            {


                throw e;
            }
        }
        public DataTable SelectAllHolidaySettings(IHolidaySettings Iholiday)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proHolidaySettingsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_HolidaySettings", DbType.Int64, Iholiday.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, Iholiday.FK_Client);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, Iholiday.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, Iholiday.PageSize);
            db.AddInParameter(dbCommand, "@Reason", DbType.String, Iholiday.Reason);
            db.AddInParameter(dbCommand, "@Type", DbType.String, Iholiday.Type);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, Iholiday.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, Iholiday.ToDate);
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

        public int DeleteHoliday(IHolidaySettings Iholiday)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proHolidaySettingsDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_HolidaySettings", DbType.Int64, Iholiday.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, Iholiday.FK_Client);
            db.AddInParameter(dbCommand, "@Cancelleduser", DbType.String, Iholiday.Cancelleduser);
            db.AddInParameter(dbCommand, "@CancelledOn", DbType.DateTime, Iholiday.CancelledOn);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
    
                throw e;
            }
        }


    }
}
