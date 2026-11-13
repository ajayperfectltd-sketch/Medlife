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
    public class DalUsers
    {

        public Int32 UpdateUsers(IUsers iusers)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUsersUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iusers.UserAction);
            db.AddInParameter(dbCommand, "@ID_Users", DbType.Int64, iusers.MasterId);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, iusers.FK_Customer);

            db.AddInParameter(dbCommand, "@Username", DbType.String, iusers.Name);
            db.AddInParameter(dbCommand, "@UseCode", DbType.String, iusers.Username);
            db.AddInParameter(dbCommand, "@Password", DbType.String, iusers.Password);
            db.AddInParameter(dbCommand, "@LoginTime", DbType.DateTime, iusers.LoginTime);
            db.AddInParameter(dbCommand, "@LogoutTime", DbType.DateTime, iusers.LogoutTime);
            db.AddInParameter(dbCommand, "@FK_UserGroup", DbType.Int64, iusers.FK_UserGroup);
            db.AddInParameter(dbCommand, "@LastPasswordChange", DbType.DateTime, iusers.LastPasswordChange);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, iusers.Active);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iusers.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iusers.UserCode);
         
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iusers.FK_Store);
            db.AddInParameter(dbCommand, "@Photo", DbType.String, iusers.Photo);
            db.AddInParameter(dbCommand, "@PhotoData", DbType.Binary, iusers.PhotoData);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iusers, e);
                return 0;
            }
        }

        public IUsers SelectUsers(IUsers iusers)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUsersSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Users", DbType.Int64, iusers.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iusers.FK_Client);
            db.AddInParameter(dbCommand, "@Username", DbType.String, iusers.Username);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iusers.FK_Customer = Convert.ToInt64(dataReader["FK_Customer"].ToString());
                      
                        iusers.Name = Convert.ToString(dataReader["Username"].ToString());
                        iusers.Username = Convert.ToString(dataReader["UseCode"].ToString());
                        iusers.Password = Convert.ToString(dataReader["Password"].ToString());
                        iusers.LoginTime = Convert.ToDateTime(dataReader["LoginTime"].ToString());
                        iusers.LogoutTime = Convert.ToDateTime(dataReader["LogoutTime"].ToString());
                        iusers.FK_UserGroup = Convert.ToInt64(dataReader["FK_UserGroup"].ToString());
                        iusers.LastPasswordChange = Convert.ToDateTime(dataReader["LastPasswordChange"].ToString());
                        iusers.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                        iusers.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iusers.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iusers.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        iusers.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());

                    }
                }
                return iusers;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iusers, e);
                throw e;
            }
        }

        public DataTable SelectAllUsers(IUsers iusers)
		{
			DataTable dtblUsers = new DataTable();         
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proUsersSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Users",DbType.Int64,iusers.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,iusers.FK_Client);
			db.AddInParameter(dbCommand, "@Username", DbType.String, iusers.Username);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int32, iusers.FK_Customer);
            db.AddInParameter(dbCommand, "@FK_UserGroup", DbType.Int32, iusers.FK_UserGroup);
			try
			{
				 dtblUsers = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblUsers;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(iusers, e);
                throw e;
			}
		}
        public DataTable selectFaildLogindetails(IUsers iusers)
        {
            DataTable dtblUsers = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "Profailedlogindetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iusers.Username);
            try
            {
                dtblUsers = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblUsers;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iusers, e);
                throw e;
            }
        }

        public Int32 UpdateAuditTrail(IUsers iusers)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAuditrailUpdatefaildLogin";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iusers.UserAction);
           db.AddInParameter(dbCommand, "@User", DbType.String, iusers.Username);
            db.AddInParameter(dbCommand, "@AuditData", DbType.String, iusers.AuditData);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iusers.FK_Client);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istore, e);
                return 0;
            }
        }

        public string SelectPassword(IUsers iusers)
        {
            DataTable dtblUsers = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectPassword";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Users", DbType.Int64, iusers.MasterId);
            try
            {
                dtblUsers = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblUsers.Rows[0]["Password"].ToString();
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iusers, e);
                throw e;
            }
        }

        public int DeleteUsers(IUsers iusers)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUsersDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Users", DbType.Int64, iusers.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iusers.FK_Client);      
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iusers.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, iusers.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iusers, e);
                throw e;
            }
        }

        public IUsers LoginCheck(IUsers iusers)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");            
            string sqlCommand = "proLoginCheck";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UseCode", DbType.String, iusers.Username);
            db.AddInParameter(dbCommand, "@Password", DbType.String, iusers.Password);
            db.AddInParameter(dbCommand, "@DailyPassword", DbType.String, iusers.DailyPassword!=""?iusers.DailyPassword:"");
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iusers.MasterId = Convert.ToInt64(dataReader["ID_Users"].ToString());
                        iusers.Name = Convert.ToString(dataReader["Username"].ToString());                       
                        iusers.LoginTime = Convert.ToDateTime(dataReader["LoginTime"].ToString());
                        iusers.LogoutTime = Convert.ToDateTime(dataReader["LogoutTime"].ToString());
                        iusers.FK_UserGroup = Convert.ToInt64(dataReader["FK_UserGroup"].ToString());
                        iusers.LastPasswordChange = Convert.ToDateTime(dataReader["LastPasswordChange"].ToString());             
                        iusers.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                    }
                }
                return iusers;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public int UpdateLoginDetails(IUsers iusers)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proLoginDetailsUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iusers.UserAction);
            db.AddInParameter(dbCommand, "@ID_Users", DbType.Int64, iusers.MasterId);           
            db.AddInParameter(dbCommand, "@LoginTime", DbType.DateTime, iusers.LoginTime);
            db.AddInParameter(dbCommand, "@LogoutTime", DbType.DateTime, iusers.LogoutTime);          
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iusers.FK_Client);  
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;              
            }
        }
        public int ChangePassword(IUsers iusers)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proChangePassword";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Users", DbType.Int64, iusers.MasterId);
            db.AddInParameter(dbCommand, "@CurrentPassword", DbType.String, iusers.Password);
            db.AddInParameter(dbCommand, "@NewPassword", DbType.String, iusers.NewPassword);
            db.AddInParameter(dbCommand, "@LastPasswordChange", DbType.DateTime, iusers.LastPasswordChange);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iusers.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable SelectUserPhoto(Int64 FK_User)
        {
            DataTable dtblPhoto = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUserPhotoSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_User", DbType.Int64, FK_User);
            try
            {
                dtblPhoto = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblPhoto;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
    }
}
