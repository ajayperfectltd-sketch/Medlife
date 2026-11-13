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
    public class DalAccountGroup
    {
        public Int32 UpdateAccountGroup(IAccountGroup iAccGrp)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccouptGroupUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccoutGroup", DbType.Int64, iAccGrp.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iAccGrp.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iAccGrp.ShortName);
            db.AddInParameter(dbCommand, "@AccountType", DbType.String, iAccGrp.AccountType);

            db.AddInParameter(dbCommand, "@groupType", DbType.String, iAccGrp.GroupType);
            db.AddInParameter(dbCommand, "@SortOrder", DbType.Int32, iAccGrp.SortOrder);
            db.AddInParameter(dbCommand, "@Description", DbType.String, iAccGrp.Description);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iAccGrp.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iAccGrp.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iAccGrp.FK_Client);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, iAccGrp.Active);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iAccGrp.UserAction);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }


        }

        public int DeleteAccountGroup(IAccountGroup iAccGrp)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountGroupDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccoutGroup", DbType.Int64, iAccGrp.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iAccGrp.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iAccGrp.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, iAccGrp.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public IAccountGroup SelectAccountGroup(IAccountGroup iAccGrp)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountGroupSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountGroup", DbType.Int64, iAccGrp.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iAccGrp.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iAccGrp.ShortName);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iAccGrp.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iAccGrp.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iAccGrp.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                       
                        iAccGrp.Name = dataReader["GroupName"].ToString();
                        iAccGrp.AccountType = dataReader["AccountType"].ToString();
                        iAccGrp.GroupType = dataReader["groupType"].ToString();
                        iAccGrp.SortOrder = int.Parse(dataReader["SortOrder"].ToString());
                       // iAccGrp.Description = dataReader["Description"].ToString();
                       // iAccGrp.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                    }
                }
                return iAccGrp;
            }
            catch (SqlException e)
            {
                throw e;
            }




        }
      
        public DataTable SelectAllAccountGroup(IAccountGroup iAccGrp)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountGroupSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountGroup", DbType.Int64, iAccGrp.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iAccGrp.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iAccGrp.ShortName);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iAccGrp.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iAccGrp.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iAccGrp.FK_Client);
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
    }


}