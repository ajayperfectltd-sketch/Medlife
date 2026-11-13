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
using WebPharma.Interface.General;


namespace WebPharma.DataAccess.Masters
{
    public class DalUserAccess
    {
        public Int32 UpdateUserAccess(IUserAccess iuseraccess)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUserAccessUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_UserGroup", DbType.Int64, iuseraccess.FK_UserGroup);
            db.AddInParameter(dbCommand, "@XmlUserAccess", DbType.String, iuseraccess.XmlUserAccess);
            db.AddInParameter(dbCommand, "@ID_UserAccess", DbType.Int64, iuseraccess.MasterId);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iuseraccess.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iuseraccess.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iuseraccess.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Pages1", DbType.Int64, iuseraccess.FK_Pages1);
            db.AddInParameter(dbCommand, "@FK_Pages2", DbType.Int64, iuseraccess.FK_Pages2);
            db.AddInParameter(dbCommand, "@FK_Pages3", DbType.Int64, iuseraccess.FK_Pages3);
            db.AddInParameter(dbCommand, "@FK_Pages4", DbType.Int64, iuseraccess.FK_Pages4);
            db.AddInParameter(dbCommand, "@FK_Pages5", DbType.Int64, iuseraccess.FK_Pages5);
            db.AddInParameter(dbCommand, "@FK_Pages6", DbType.Int64, iuseraccess.FK_Pages6);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public Int32 UpdateQuickmenu(IUserAccess iuseraccess)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUserQuickMenuUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, iuseraccess.FK_Users);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iuseraccess.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iuseraccess.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iuseraccess.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Pages1", DbType.Int64, iuseraccess.FK_Pages1);
            db.AddInParameter(dbCommand, "@FK_Pages2", DbType.Int64, iuseraccess.FK_Pages2);
            db.AddInParameter(dbCommand, "@FK_Pages3", DbType.Int64, iuseraccess.FK_Pages3);
            db.AddInParameter(dbCommand, "@FK_Pages4", DbType.Int64, iuseraccess.FK_Pages4);
            db.AddInParameter(dbCommand, "@FK_Pages5", DbType.Int64, iuseraccess.FK_Pages5);
            db.AddInParameter(dbCommand, "@FK_Pages6", DbType.Int64, iuseraccess.FK_Pages6);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public DataSet SelectUserAccess(IUserAccess iuseraccess)
        {
            //DataTable dtblusers = new DataTable();
            DataSet dsetUserAccess = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUserAccessSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Users", DbType.Int64, iuseraccess.FK_Users);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iuseraccess.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Pages", DbType.Int64, iuseraccess.FK_Pages);
            try
            {
                dsetUserAccess = db.ExecuteDataSet(dbCommand);
                return dsetUserAccess;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataSet SelectUserAccessMenu(IUserAccess iuseraccess)
        {
            //DataTable dtblusers = new DataTable();
            DataSet dsetUserAccess = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUserAccessSelection";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_UserGroup", DbType.Int64, iuseraccess.FK_UserGroup);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iuseraccess.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Pages", DbType.Int64, iuseraccess.FK_Pages);
            try
            {
                dsetUserAccess = db.ExecuteDataSet(dbCommand);
                return dsetUserAccess;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectPages(IUserAccess iuseraccess)
        {
            DataTable dtblusers = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPageSelect"; //"proPagesSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            // db.AddInParameter(dbCommand, "@FK_PageModule", DbType.Int64, iuseraccess.FK_PageModule);
            //db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iuseraccess.FK_Client);
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
        public DataTable SelectQuickMenu(IUserAccess iuseraccess)
        {
            DataTable dtblusers = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proQuickMenuSelect"; //"proPagesSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Users", DbType.Int64, iuseraccess.FK_Users);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iuseraccess.FK_Client);
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
        public IUserAccess SelectPageSettings(IUserAccess iuseraccess)
        {
            //DataTable dtblusers = new DataTable();
            DataSet dsetUserAccess = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUserAccessSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Users", DbType.Int64, iuseraccess.FK_Users);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iuseraccess.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Pages", DbType.Int64, iuseraccess.FK_Pages);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iuseraccess.IsAdd = Convert.ToBoolean(dataReader["IsAdd"].ToString());
                        iuseraccess.IsDelete = Convert.ToBoolean(dataReader["IsDelete"].ToString());
                        iuseraccess.IsUpdate = Convert.ToBoolean(dataReader["IsUpdate"].ToString());
                        iuseraccess.IsView = Convert.ToBoolean(dataReader["IsView"].ToString());
                    }
                }
                return iuseraccess;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public IUserAccess CheckUserAccess(IUserAccess iuseraccess)
        {
            //DataTable dtblusers = new DataTable();
            DataSet dsetUserAccess = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUserAccessCheck";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Users", DbType.Int64, iuseraccess.FK_Users);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iuseraccess.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Pages", DbType.Int64, iuseraccess.FK_Pages);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iuseraccess.IsAdd = Convert.ToBoolean(dataReader["IsAdd"].ToString());
                        iuseraccess.IsDelete = Convert.ToBoolean(dataReader["IsDelete"].ToString());
                        iuseraccess.IsUpdate = Convert.ToBoolean(dataReader["IsUpdate"].ToString());
                        iuseraccess.IsView = Convert.ToBoolean(dataReader["IsView"].ToString());
                        iuseraccess.IsPurchase = Convert.ToBoolean(dataReader["Purchase"].ToString());
                        iuseraccess.IsPurReturn = Convert.ToBoolean(dataReader["PurReturn"].ToString());
                        iuseraccess.IsSales = Convert.ToBoolean(dataReader["Sales"].ToString());
                        iuseraccess.IsSalesReturn = Convert.ToBoolean(dataReader["SalReturn"].ToString());
                        iuseraccess.IsApprove = Convert.ToBoolean(dataReader["Approve"].ToString());
                    }
                }
                return iuseraccess;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}
