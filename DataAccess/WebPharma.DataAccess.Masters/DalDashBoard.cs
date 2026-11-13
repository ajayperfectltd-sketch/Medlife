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
    public class DalDashBoard
    {
        public Int32 UpdateDashBoardAccess(IDashBoard IDashboard)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDashBoardUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_UserGroup", DbType.Int64, IDashboard.FK_UserGroup);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IDashboard.FK_Client);
            db.AddInParameter(dbCommand, "@ID_DashboardAccess", DbType.Int64, IDashboard.MasterId);
            db.AddInParameter(dbCommand, "@XmlUserAccess", DbType.Xml, IDashboard.XmlUserAccess);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, IDashboard.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, IDashboard.EnteredOn);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }


        }
        public DataTable SelectDashBoardAccess(IDashBoard IDashboard)
        {
            DataTable dtblusers = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDashBoardAccessSelect"; 
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Fk_UserGroup", DbType.Int64, IDashboard.FK_UserGroup);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IDashboard.FK_Client);
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

        public DataTable DashBoardAccessRight(IDashBoard IDashboard)
        {
            DataTable dtblusers = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDashBoardAccessRight"; 
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Users", DbType.Int64, IDashboard.FK_Users);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IDashboard.FK_Client);
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
