/*----------------------------------- DataAccess Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     :bibin  tc
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using WebPharma.DataAccess;
using WebPharma.Interface.Licensing;

namespace WebPharma.DataAccess.Licensing
{
    public class DalLicensing
    {
        public bool InsertLicense(string License)
        {
            bool result = false;
            int count = 0;
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = License;
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
            count = Convert.ToInt32(db.ExecuteScalar(dbCommand));
            if (count > 0)
            { 
                result = true;
            }
            return result;
        }
        public int CheckLicensedOrNot()
        {
            try
            {
                int count = 0;
                Database db = DatabaseFactory.CreateDatabase("WebPharma");
                string sqlCommand = "SELECT COUNT(1) FROM PLCSettings";
                DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
                count = Convert.ToInt32(db.ExecuteScalar(dbCommand));
                return count;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable GetLicense()
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPssGetLicense";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            try
            {
                //DataTable dt = new DataTable();
                //IDataReader dr;
                //dr = db.ExecuteReader(dbCommand);
                //dt.Load(dr);
                //return dt;

                DataTable dt = new DataTable();
                DataSet ds = db.ExecuteDataSet(dbCommand);
                using (IDataReader dataReader = ds.CreateDataReader())
                {
                    dt.Load(dataReader);
                    return dt;
                }



            }
            catch (SqlException e)
            {
                //   UpdateErrorLog(ilicense, e);
                throw e;
            }
        }        
        public DataTable CheckLicense(ILicensing ilicensing)
        {
//            string sqlCommand="";
//            Database db = DatabaseFactory.CreateDatabase("SETUP");
//            if( ilicensing.LicenseMode=="User")
//            {
//                    sqlCommand=@"SELECT	
//				                        (SELECT COUNT(ID_Users) FROM Users WHERE UseCode<>'Pss' AND Passed=1 AND Cancelled=0) TotalUsers,
//				                        (SELECT COUNT(ID_Users) FROM Users WHERE UseCode<>'Pss' AND (UseLogOutDateTime IS NULL AND UseLogInDateTime IS NOT NULL) AND Passed=1 AND Cancelled=0) TotalLoginUsers,
//				                        (SELECT BhName FROM SCOREDATA.dbo.BankHO WHERE Cancelled=0 AND Passed=1) BankHO";
//            }			
//            if( ilicensing.LicenseMode=="Branch")
//            {
//                    sqlCommand=@"SELECT	
//				                        (SELECT COUNT(ID_Branch) FROM SCOREDATA.dbo.Branch WHERE Passed=1 AND Cancelled=0) TotalBranches,
//				                        (SELECT COUNT(DISTINCT(m.BranchCode)) FROM 
//					                        (SELECT  (UseLogInMachine) FROM Users WHERE UseCode<>'Pss' AND (UseLogOutDateTime IS NULL AND UseLogInDateTime IS NOT NULL) AND Passed=1 AND Cancelled=0) t 
//					                        JOIN Machine m ON t.UseLogInMachine=m.ID_Machine) TotalLoginBranches,
//				                        (SELECT BhName FROM SCOREDATA.dbo.BankHO WHERE Cancelled=0 AND Passed=1) BankHO";
//            }
//            if( ilicensing.LicenseMode=="Machine")
//            {
//                    sqlCommand=@"SELECT	
//				                        (SELECT COUNT(ID_Machine) FROM Machine WHERE Passed=1 AND Cancelled=0) TotalMachines,
//				                        (SELECT COUNT(DISTINCT(UseLogInMachine)) FROM SCORESETUP.dbo.Users WHERE UseCode<>'Pss' AND (UseLogOutDateTime IS NULL AND UseLogInDateTime IS NOT NULL) 
//					                        AND Passed=1 AND Cancelled=0) TotalLoginMachines,
//				                        (SELECT BhName FROM SCOREDATA.dbo.BankHO WHERE Cancelled=0 AND Passed=1) BankHO";
//            }

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPssCheckLicense";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@LicenseMode", DbType.String, ilicensing.LicenseMode);
            //db.AddInParameter(dbCommand, "@ClientName", DbType.String, ilicensing.ClientName);            
            try
            {
                //DataTable dt = new DataTable();
                //IDataReader dr;
                //dr = db.ExecuteReader(dbCommand);
                //dt.Load(dr);
                //return dt;
                DataTable dt = new DataTable();
                DataSet ds = db.ExecuteDataSet(dbCommand);
                using (IDataReader dataReader = ds.CreateDataReader())
                {
                    dt.Load(dataReader);
                    return dt;
                }

            }
            catch (SqlException e)
            {                
                throw e;
            }
        }
        public bool InsertSerialKey(string Field, string Value, string Field1, string Value1)
        {
            bool result = false;
            int count = 0;
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPssInsertLicenseKey";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Field", DbType.String, Field);
            db.AddInParameter(dbCommand, "@Value", DbType.String, Value);
            db.AddInParameter(dbCommand, "@Field1", DbType.String, Field1);
            db.AddInParameter(dbCommand, "@Value1", DbType.String, Value1);

            count = Convert.ToInt32(db.ExecuteScalar(dbCommand));
            if (count > 0)
            {
                result = true;
            }
            return result;
        }
        public bool InsertEvaluationPeriod(string Field1, string Value1, string Field2, string Value2, string Field3, string Value3, string Field4, string Value4)
        {
            bool result = false;
            int count = 0;
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPssInsertEvaluationLicense";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Field1", DbType.String, Field1);
            db.AddInParameter(dbCommand, "@Value1", DbType.String, Value1);
            db.AddInParameter(dbCommand, "@Field2", DbType.String, Field2);
            db.AddInParameter(dbCommand, "@Value2", DbType.String, Value2);
            db.AddInParameter(dbCommand, "@Field3", DbType.String, Field3);
            db.AddInParameter(dbCommand, "@Value3", DbType.String, Value3);
            db.AddInParameter(dbCommand, "@Field4", DbType.String, Field4);
            db.AddInParameter(dbCommand, "@Value4", DbType.String, Value4);

            count = Convert.ToInt32(db.ExecuteScalar(dbCommand));
            if (count > 0)
            {
                result = true;
            }
            return result;
        }
    }
}
