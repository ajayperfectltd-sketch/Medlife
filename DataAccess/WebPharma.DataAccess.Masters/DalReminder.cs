using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using WebPharma.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.Interface.Masters;
using WebPharma.Interface.General;

namespace WebPharma.DataAccess.Masters
{
    public class DalReminder
    {
        public Int32 UpdateReminder(IReminder iReminder)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "proReminderUpdate";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);
            db.AddInParameter(dbcmd, "@UserAction", DbType.Byte, iReminder.UserAction);
            db.AddInParameter(dbcmd, "@ID_Reminder", DbType.Int64, iReminder.MasterId);
            db.AddInParameter(dbcmd, "@Topic", DbType.String, iReminder.Topic);
            db.AddInParameter(dbcmd, "@Message", DbType.String, iReminder.Message);
            db.AddInParameter(dbcmd, "@FromDate", DbType.DateTime, iReminder.FromDate);
            db.AddInParameter(dbcmd, "@ToDate", DbType.DateTime, iReminder.ToDate);
            db.AddInParameter(dbcmd, "@Type", DbType.Byte, iReminder.Type);
            db.AddInParameter(dbcmd, "@FK_User", DbType.Int64, iReminder.FK_User);
            db.AddInParameter(dbcmd, "@Active", DbType.Boolean, iReminder.Active);
            db.AddInParameter(dbcmd, "@UserCode", DbType.String, iReminder.UserCode);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, iReminder.FK_Client);
            db.AddInParameter(dbcmd, "@XmlUsers", DbType.Xml, iReminder.XmlUser);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbcmd));
            }
            catch (SqlException e)
            {
                return 0;
            }

        }
        public int DeleteReminder(IReminder iReminder)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "proReminderDelete";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);
            db.AddInParameter(dbcmd, "@ID_Reminder", DbType.Int64, iReminder.MasterId);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, iReminder.FK_Client);
            db.AddInParameter(dbcmd, "@UserCode", DbType.String, iReminder.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbcmd));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public DataTable GetReminderNew(IReminder iReminder)
        {
            string sqlCommand = "";
            DbCommand dbCommand;
            DataTable dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            sqlCommand = "ProGetReminderNew";
            dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, iReminder.FK_User);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iReminder.FK_Client);
            db.AddInParameter(dbCommand, "@date", DbType.DateTime, iReminder.ToDate);

            try
            {
                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public DataTable GetReminderDetails(IReminder iReminder)
        {
            string sqlCommand = "";
            DbCommand dbCommand;
            DataTable dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            sqlCommand = "ProGetReminderDetails";
            dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, iReminder.FK_User);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iReminder.FK_Client);
            db.AddInParameter(dbCommand, "@date", DbType.DateTime, iReminder.ToDate);
            try
            {
                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public IReminder SelectReminder(IReminder iReminder)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "proReminderSelect";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);
            db.AddInParameter(dbcmd, "@ID_Reminder", DbType.Int64, iReminder.MasterId);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, iReminder.FK_Client);
            db.AddInParameter(dbcmd, "@Topic", DbType.String, iReminder.Topic);
            db.AddInParameter(dbcmd, "@FromDate", DbType.DateTime, iReminder.FromDate);
            db.AddInParameter(dbcmd, "@ToDate", DbType.DateTime, iReminder.ToDate);
            db.AddInParameter(dbcmd, "@PageIndex", DbType.Int16, iReminder.PageIndex);
            db.AddInParameter(dbcmd, "@PageSize", DbType.Int16, iReminder.PageSize);
            db.AddInParameter(dbcmd, "@Type", DbType.Byte, iReminder.Type);
           // db.AddInParameter(dbcmd, "@FK_User", DbType.Int64, iReminder.FK_User);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbcmd))
                {
                    while (dataReader.Read())
                    {
                        iReminder.Topic = dataReader["Topic"].ToString();
                        iReminder.Message = dataReader["Message"].ToString();
                        iReminder.FromDate = Convert.ToDateTime(dataReader["FromDate"]);
                        iReminder.ToDate = Convert.ToDateTime(dataReader["ToDate"]);
                        iReminder.Type = Convert.ToByte(dataReader["Type"]);
                        iReminder.Active = Convert.ToBoolean(dataReader["Active"]);
                    }
                }
                return iReminder;
            }
            catch (SqlException e)
            {
                throw e;
            }

        }
        public DataTable SelectAllReminder(IReminder iReminder)
        {
            DataTable dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "proReminderSelect";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);
            db.AddInParameter(dbcmd, "@ID_Reminder", DbType.Int64, iReminder.MasterId);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, iReminder.FK_Client);
            db.AddInParameter(dbcmd, "@Topic", DbType.String, iReminder.Topic);
            db.AddInParameter(dbcmd, "@FromDate", DbType.DateTime, iReminder.FromDate);
            db.AddInParameter(dbcmd, "@ToDate", DbType.DateTime, iReminder.ToDate);
            db.AddInParameter(dbcmd, "@PageIndex", DbType.Int16, iReminder.PageIndex);
            db.AddInParameter(dbcmd, "@PageSize", DbType.Int16, iReminder.PageSize);
            db.AddInParameter(dbcmd, "@Type", DbType.Byte, iReminder.Type);
            //db.AddInParameter(dbcmd, "@FK_User", DbType.Int64, iReminder.FK_User);
            try
            {
                dt = db.ExecuteDataSet(dbcmd).Tables[0];
                return dt;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable GetReminder(IReminder iReminder)
        {
            DataTable dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "proGetReminder";

            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);
            db.AddInParameter(dbcmd, "@FK_User", DbType.Int64, iReminder.FK_User);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, iReminder.FK_Client);
            db.AddInParameter(dbcmd, "@date", DbType.DateTime, iReminder.ToDate);
            try
            {
                dt = db.ExecuteDataSet(dbcmd).Tables[0];
                return dt;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectUserForReminder(IReminder IReminder)
        {
            DataTable dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "proSelectUserForReminder";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, IReminder.FK_Client);
            db.AddInParameter(dbcmd, "@ID_Reminder", DbType.Int64, IReminder.FK_User);
            db.AddInParameter(dbcmd, "@FK_UserGroup", DbType.Int64, IReminder.FK_UserGroup);
            try
            {
                dt = db.ExecuteDataSet(dbcmd).Tables[0];
                return dt;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable GetReminderDetailsView(IReminder iReminder)
        {
            string sqlCommand = "";
            DbCommand dbCommand;
            DataTable dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            sqlCommand = "ProGetReminderDetailsView";
            dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, iReminder.FK_User);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iReminder.FK_Client);
            db.AddInParameter(dbCommand, "@Fromdate", DbType.DateTime, iReminder.FromDate);
            db.AddInParameter(dbCommand, "@Todate", DbType.DateTime, iReminder.ToDate);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iReminder.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iReminder.PageSize);
            try
            {
                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }

        }

    }
}
