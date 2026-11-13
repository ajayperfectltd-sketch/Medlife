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
    public class DalUserGroup
    {

        public Int32 UpdateUserGroup(IUserGroup iusergroup)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUserGroupUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iusergroup.UserAction);
            db.AddInParameter(dbCommand, "@ID_UserGroup", DbType.Int64, iusergroup.MasterId);
            db.AddInParameter(dbCommand, "@GroupName", DbType.String, iusergroup.GroupName);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, iusergroup.Shortname);
            db.AddInParameter(dbCommand, "@Administrator", DbType.Boolean, iusergroup.Administrator);
            db.AddInParameter(dbCommand, "@UgAdd", DbType.Boolean, iusergroup.UgAdd);
            db.AddInParameter(dbCommand, "@UgModify", DbType.Boolean, iusergroup.UgModify);
            db.AddInParameter(dbCommand, "@UgDelete", DbType.Boolean, iusergroup.UgDelete);
            db.AddInParameter(dbCommand, "@UgView", DbType.Boolean, iusergroup.UgView);
            db.AddInParameter(dbCommand, "@UgDiscount", DbType.Boolean, iusergroup.UgDiscount);
            db.AddInParameter(dbCommand, "@UgDayClose", DbType.Boolean, iusergroup.UgDayClose);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, iusergroup.Active);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iusergroup.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iusergroup.UserCode);
            db.AddInParameter(dbCommand, "@UgMultiStoreLogin", DbType.Boolean, iusergroup.UgMultiStoreLogin);
            db.AddInParameter(dbCommand, "@UgPurchase", DbType.Boolean, iusergroup.UgPurchase);
            db.AddInParameter(dbCommand, "@UgSales", DbType.Boolean, iusergroup.UgSales);
            db.AddInParameter(dbCommand, "@UgPurchaseReturn", DbType.Boolean, iusergroup.UgPurchaseReturn);
            db.AddInParameter(dbCommand, "@UgSalesReturn", DbType.Boolean, iusergroup.UgSalesReturn);
            db.AddInParameter(dbCommand, "@UgApprove", DbType.Boolean, iusergroup.UgApprove);
            db.AddInParameter(dbCommand, "@UgPriceCorrection", DbType.Boolean, iusergroup.UgPriceCorrection);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {               
                return 0;
            }
        }

        public IUserGroup SelectUserGroup(IUserGroup iusergroup)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUserGroupSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_UserGroup", DbType.Int64, iusergroup.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iusergroup.FK_Client);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, iusergroup.Shortname);
            db.AddInParameter(dbCommand, "@GroupName", DbType.String, iusergroup.GroupName);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iusergroup.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iusergroup.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iusergroup.GroupName = Convert.ToString(dataReader["GroupName"].ToString());
                        iusergroup.Shortname = Convert.ToString(dataReader["Shortname"].ToString());
                        iusergroup.Administrator = Convert.ToBoolean(dataReader["Administrator"].ToString());
                        iusergroup.UgAdd = Convert.ToBoolean(dataReader["UgAdd"].ToString());
                        iusergroup.UgModify = Convert.ToBoolean(dataReader["UgModify"].ToString());
                        iusergroup.UgDelete = Convert.ToBoolean(dataReader["UgDelete"].ToString());
                        iusergroup.UgView = Convert.ToBoolean(dataReader["UgView"].ToString());
                        iusergroup.UgDiscount = Convert.ToBoolean(dataReader["UgDiscount"].ToString());
                        iusergroup.UgDayClose = Convert.ToBoolean(dataReader["UgDayClose"].ToString());
                        iusergroup.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iusergroup.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iusergroup.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        iusergroup.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                        iusergroup.UgMultiStoreLogin = Convert.ToBoolean(dataReader["UgMultiStoreLogin"].ToString());
                        iusergroup.UgApprove = Convert.ToBoolean(dataReader["UgApprove"].ToString());
                        iusergroup.UgPurchase = Convert.ToBoolean(dataReader["UgPurchase"].ToString());
                        iusergroup.UgPurchaseReturn = Convert.ToBoolean(dataReader["UgPurchaseReturn"].ToString());
                        iusergroup.UgSales = Convert.ToBoolean(dataReader["UgSales"].ToString());
                        iusergroup.UgSalesReturn = Convert.ToBoolean(dataReader["UgSalesReturn"].ToString());
                        iusergroup.UgPriceCorrection = Convert.ToBoolean(dataReader["UgPriceCorrection"].ToString());
                    }
                }
                return iusergroup;
            }
            catch (SqlException e)
            {               
                throw e;
            }
        }

        public DataTable SelectAllUserGroup(IUserGroup iusergroup)
		{
			DataTable dtblUserGroup = new DataTable();
			Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proUserGroupSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_UserGroup",DbType.Int64,iusergroup.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,iusergroup.FK_Client);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, iusergroup.Shortname);
            db.AddInParameter(dbCommand, "@GroupName", DbType.String, iusergroup.GroupName);
			db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iusergroup.PageIndex);
			db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iusergroup.PageSize);
			try
			{
				 dtblUserGroup = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblUserGroup;
			}
			catch (SqlException e) 
			{				
                throw e;
			}
		}

        public int DeleteUserGroup(IUserGroup iusergroup)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUserGroupDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_UserGroup", DbType.Int64, iusergroup.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iusergroup.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iusergroup.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, iusergroup.FK_Reason); 
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
