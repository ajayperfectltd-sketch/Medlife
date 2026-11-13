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
    public class DalEmployee
    {

        public Int32 UpdateEmployee(IEmployee iemployee)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proEmployeeUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iemployee.UserAction);
            db.AddInParameter(dbCommand, "@ID_Employee", DbType.Int64, iemployee.MasterId);
            db.AddInParameter(dbCommand, "@EmployeeCode", DbType.String, iemployee.EmployeeCode);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iemployee.Name);
            db.AddInParameter(dbCommand, "@Address", DbType.String, iemployee.Address);
            db.AddInParameter(dbCommand, "@Dob", DbType.DateTime, iemployee.Dob);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, iemployee.Phone);
            db.AddInParameter(dbCommand, "@Salesman", DbType.Boolean, iemployee.Salesman);
            db.AddInParameter(dbCommand, "@Email", DbType.String, iemployee.Email);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, iemployee.Active);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iemployee.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iemployee.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iemployee.EnteredOn);
            db.AddInParameter(dbCommand, "@XmlStoreAccess", DbType.String, iemployee.XmlStoreAccessDetails);


            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iemployee, e);
                return 0;
            }
        }

        public IEmployee SelectEmployee(IEmployee iemployee)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proEmployeeSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Employee", DbType.Int64, iemployee.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iemployee.FK_Client);
            db.AddInParameter(dbCommand, "@EmployeeCode", DbType.String, iemployee.EmployeeCode);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iemployee.Name);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iemployee.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iemployee.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iemployee.EmployeeCode = Convert.ToString(dataReader["EmployeeCode"].ToString());
                        iemployee.Name = Convert.ToString(dataReader["Name"].ToString());
                        iemployee.Address = Convert.ToString(dataReader["Address"].ToString());
                        iemployee.Dob = Convert.ToDateTime(dataReader["Dob"].ToString());
                        iemployee.Phone = Convert.ToString(dataReader["Phone"].ToString());
                        iemployee.Salesman = Convert.ToBoolean(dataReader["SalesMan"].ToString());
                        iemployee.Email = Convert.ToString(dataReader["Email"].ToString());
                        iemployee.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                        iemployee.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iemployee.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iemployee.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return iemployee;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iemployee, e);
                throw e;
            }
        }

        public DataTable SelectAllEmployee(IEmployee iemployee)
		{
            DataTable dtblEmployee = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proEmployeeSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Employee",DbType.Int64,iemployee.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,iemployee.FK_Client);
			db.AddInParameter(dbCommand, "@EmployeeCode", DbType.String,iemployee.EmployeeCode);
			db.AddInParameter(dbCommand, "@Name", DbType.String, iemployee.Name);
			db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iemployee.PageIndex);
			db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iemployee.PageSize);
			try
			{
				 dtblEmployee = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblEmployee;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(iemployee, e);
                throw e;
			}
		}

        public int DeleteEmployee(IEmployee iemployee)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proEmployeeDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Employee", DbType.Int64, iemployee.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iemployee.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iemployee.UserCode);
            
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iemployee, e);
                throw e;
            }
        }

        public DataTable SelectStores(IEmployee iemployee)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProStoreSelectAll";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iemployee.FK_Client);
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
