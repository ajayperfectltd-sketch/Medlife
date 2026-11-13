/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : DAL class for District
Author     : Praveen MP
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 21/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
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
    public class DalDistrict
    {
        public Int32 UpdateDistrict(IDistrict iDistrict)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDistrictUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iDistrict.UserAction);
            db.AddInParameter(dbCommand, "@ID_District", DbType.Int64, iDistrict.MasterId);
            db.AddInParameter(dbCommand, "@DistrictName", DbType.String, iDistrict.DistrictName);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, iDistrict.FK_Country);
            db.AddInParameter(dbCommand, "@FK_States", DbType.Int64, iDistrict.FK_States);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iDistrict.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iDistrict.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public int DeleteDistrict(IDistrict iDistrict)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDistrictDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_District", DbType.Int64, iDistrict.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iDistrict.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iDistrict.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, iDistrict.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public IDistrict SelectDistrict(IDistrict iDistrict)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDistrictSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_District", DbType.Int64, iDistrict.MasterId);
            db.AddInParameter(dbCommand, "@DistrictName", DbType.String, iDistrict.DistrictName);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iDistrict.FK_Client);
            db.AddInParameter(dbCommand, "@ID_Country", DbType.Int64, iDistrict.FK_Country);
            db.AddInParameter(dbCommand, "@ID_States", DbType.Int64, iDistrict.FK_States);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iDistrict.FK_Country = Convert.ToInt32(dataReader["FK_Country"].ToString());
                        iDistrict.FK_States = Convert.ToInt32(dataReader["FK_States"].ToString());
                        iDistrict.DistrictName = dataReader["DistrictName"].ToString();    
                   
                      
                    }
                }
                return iDistrict;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectAllDistrict(IDistrict iDistrict)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDistrictSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_District", DbType.Int64, iDistrict.MasterId);
            db.AddInParameter(dbCommand, "@ID_Country", DbType.Int64, iDistrict.FK_Country);
            db.AddInParameter(dbCommand, "@ID_States", DbType.Int64, iDistrict.FK_States);
            db.AddInParameter(dbCommand, "@DistrictName", DbType.String, iDistrict.DistrictName);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iDistrict.FK_Client);
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
