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
using WebPharma.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.Interface.Masters;

namespace WebPharma.DataAccess.Masters
{
    public class DalSettings
    {
        public Int32 UpdateSettings(ISettings isettings)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSettingsUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@DeleteFlag", DbType.Byte, isettings.DeleteFlag);
            db.AddInParameter(dbCommand, "@ID_Settings", DbType.Int64, isettings.MasterId);
            db.AddInParameter(dbCommand, "@Module", DbType.String, isettings.Module);
            db.AddInParameter(dbCommand, "@SubModule", DbType.String, isettings.SubModule);
            db.AddInParameter(dbCommand, "@Name", DbType.String, isettings.Name);
            db.AddInParameter(dbCommand, "@Value", DbType.String, isettings.Value);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, isettings.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, isettings.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isettings.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public int DeleteSettings(ISettings isettings)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSettingsDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Settings", DbType.Int64, isettings.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isettings.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectSettings(ISettings isettings)
        {
            DataTable dtblusers = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSettingsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Module", DbType.String, isettings.Module);
            db.AddInParameter(dbCommand, "@SubModule", DbType.String, isettings.SubModule);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isettings.FK_Client);
            try
            {
                dtblusers = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblusers;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public string GetModuleSettings(ISettings isettings)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proGetModuleSettings";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);        
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isettings.FK_Client);
            try
            {
                return Convert.ToString(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public int CreateDBBackup(string DBLocation, string UserCode, Int64 FK_Client)
        {
            int result = 0;
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDBBackUp";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Location", DbType.String, DBLocation);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.String, FK_Client);
            try
            {
                result = Convert.ToInt16(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
            return result;
        }
        public DataTable SelectDBBackup(int PageIndex, int PageSize, Int64 FK_Client)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDBBackupSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, FK_Client);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public string GetLastBackupLocation()
        {
            string location;
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proGetLastBackupLocation";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            try
            {
                location = Convert.ToString(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
            return location;
        }

        public DataTable SelectBarcodeLocationPath(ISettings isettings)
        {
            DataTable dtblusers = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBarcodeLocation";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Module", DbType.String, isettings.Module);
            db.AddInParameter(dbCommand, "@SubModule", DbType.String, isettings.SubModule);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isettings.FK_Client);
            try
            {
                dtblusers = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblusers;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}
