/*----------------------------------------------------------------------------------------------------------------------------------------------------------

 
 Purpose    : 
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using WebPharma.Interface;
using WebPharma.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;

namespace WebPharma.DataAccess
{
    public class DalFunction
    {
        //DataTable
        public DataTable GetDataTable(IPopulate ipopulate)
        {
            DataTable dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProCmnGetRecords";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "TableName", DbType.String, ipopulate.TableName);
            db.AddInParameter(dbCommand, "ListFields", DbType.String, ipopulate.ListFields);
            db.AddInParameter(dbCommand, "SortField", DbType.String, ipopulate.SortFields);
            db.AddInParameter(dbCommand, "Criteria", DbType.String, ipopulate.Criteria);
            DataSet ds = new DataSet();
            try
            {

                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return dt;
            }
            catch (Exception e)
            {
                string errdetails = "exec ProCmnGetRecords '" + ipopulate.TableName + "','"
                                       + ipopulate.ListFields + ((ipopulate.ListFields.Length <= 0) ? "" : "," + ipopulate.ValueFields) + "','"
                                       + ipopulate.SortFields + "','" + ipopulate.Criteria + "'";
            }

            return ds.Tables[0];
        }
        public ArrayList GetArrayList(IPopulate ipopulate)
        {
            ArrayList arlist = new ArrayList();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProCmnGetRecords";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "TableName", DbType.String, ipopulate.TableName);
            db.AddInParameter(dbCommand, "ListFields", DbType.String, ipopulate.ListFields);
            db.AddInParameter(dbCommand, "SortField", DbType.String, ipopulate.SortFields);
            db.AddInParameter(dbCommand, "Criteria", DbType.String, ipopulate.Criteria);

            try
            {
                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet(dbCommand);
                IDataReader datareader = ds.Tables[0].CreateDataReader();
                ds = null;
                while (datareader.Read())
                {
                    for (int i = 0; i < datareader.FieldCount; i++)
                    {
                        arlist.Add(datareader.GetValue(i));
                    }

                }
                datareader = null;
            }
            catch (Exception e)
            {
                string errdetails = "exec ProCmnGetRecords '" + ipopulate.TableName + "','"
                                       + ipopulate.ListFields + ((ipopulate.ListFields.Length <= 0) ? "" : "," + ipopulate.ValueFields) + "','"
                                       + ipopulate.SortFields + "','" + ipopulate.Criteria + "'";
                //UpdateErrorLog(ipopulate, errdetails, e);
                errdetails = null;
                throw;
            }
            return arlist;
        }


        public long GetTransactionID(IPopulate ipopulate)
        {
            long status = 0;
            ArrayList arlist = new ArrayList();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProMaxId";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Module", DbType.String, ipopulate.Module);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipopulate.Usercode);
            // Output parameters
            db.AddOutParameter(dbCommand, "@ID_Transaction", DbType.Int64, 0);

            try
            {
                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (SqlException e)
                {
                    throw e;
                }
                // Row of data is captured via output parameters

                //status = Convert.ToInt64(db.ExecuteScalar(dbCommand));
                status = Convert.ToInt64(db.GetParameterValue(dbCommand, "@ID_Transaction"));
                //arlist.Add(db.GetParameterValue(dbCommand, "@IntAmount"));
            }
            catch (SqlException e)
            {
                throw e;
            }
            return status;
        }


        public Boolean GetIdentityProof(IPopulate ipopulate)
        {
            Database db;
            if (ipopulate.DbName == "")
                db = DatabaseFactory.CreateDatabase();
            else
                db = DatabaseFactory.CreateDatabase(ipopulate.DbName);
            string sqlCommand = "SELECT dbo.funCheckIdProof('" + ipopulate.SearchCriteria + "','" + ipopulate.Criteria + "')";
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
            //db.AddInParameter(dbCommand, "@ID_IdentityProof", DbType.String, ipopulate.SearchCriteria);
            //db.AddInParameter(dbCommand, "@CardNumber", DbType.String, ipopulate.Criteria);
            try
            {
                bool status = false;
                status = Convert.ToBoolean(db.ExecuteScalar(dbCommand));
                return status;



            }
            catch (Exception e)
            {
                return false;
            }
        }

      

        public string GetScalarValue(IPopulate ipopulate)
        {
            Database db;
            if (ipopulate.DbName == "")
                db = DatabaseFactory.CreateDatabase();
            else
                db = DatabaseFactory.CreateDatabase(ipopulate.DbName);
            string sqlCommand = "ProCmnGetScalarvalue";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "TableName", DbType.String, ipopulate.TableName);
            db.AddInParameter(dbCommand, "ListFields", DbType.String, ipopulate.ListFields);
            db.AddInParameter(dbCommand, "SortField", DbType.String, ipopulate.SortFields);
            db.AddInParameter(dbCommand, "Criteria", DbType.String, ipopulate.Criteria);
            try
            {
                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }
                else
                {
                    return "";
                }


            }
            catch (Exception e)
            {
                string errdetails = "exec ProCmnGetScalarvalue '" + ipopulate.TableName + "','"
                                       + ipopulate.ListFields + ((ipopulate.ListFields.Length <= 0) ? "" : "," + ipopulate.ValueFields) + "','"
                                       + ipopulate.SortFields + "','" + ipopulate.Criteria + "'";
                //UpdateErrorLog(ipopulate, errdetails, e);
                errdetails = null;
                throw;
            }
        }

    }
    
}
