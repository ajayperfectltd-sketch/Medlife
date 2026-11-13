using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.Masters;

namespace WebPharma.DataAccess.Masters
{
    public class DalScheduleMaster
    {

        public Int32 UpdateShedulueMaster(IScheduleMaster iScheduleMaster)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSchedulemasterUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iScheduleMaster.UserAction);
            db.AddInParameter(dbCommand, "@ID_ScheduleMaster", DbType.Int64, iScheduleMaster.MasterId);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, iScheduleMaster.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iScheduleMaster.Name);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iScheduleMaster.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iScheduleMaster.FK_Client);
           
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public IScheduleMaster SelectSchedule(IScheduleMaster iScheduleMaster)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proScheduleMasterSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_ScheduleMaster", DbType.Int64, iScheduleMaster.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iScheduleMaster.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iScheduleMaster.Name);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iScheduleMaster.FK_Client);
          

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iScheduleMaster.ShortName = Convert.ToString(dataReader["Shortname"].ToString());
                        iScheduleMaster.Name = Convert.ToString(dataReader["ScheduleName"].ToString());
                        iScheduleMaster.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iScheduleMaster.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iScheduleMaster.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return iScheduleMaster;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectAllScheduleMaster(IScheduleMaster iScheduleMaster)
        {
            DataTable dtblManufacturer = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proScheduleMasterSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_ScheduleMaster", DbType.Int64, iScheduleMaster.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iScheduleMaster.FK_Client);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iScheduleMaster.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iScheduleMaster.Name);
            try
            {
                dtblManufacturer = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblManufacturer;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public int DeleteSchedule(IScheduleMaster iScheduleMaster)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proScheduleMasterDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_ScheduleMaster", DbType.Int64, iScheduleMaster.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iScheduleMaster.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iScheduleMaster.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, iScheduleMaster.FK_Reason);
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
