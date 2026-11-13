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
    public class DalCategorySecond
    {

        public Int32 UpdateCategorySecond(ICategorySecond icategorysecond)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCategorySecondUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, icategorysecond.UserAction);
            db.AddInParameter(dbCommand, "@ID_SubCategory", DbType.Int64, icategorysecond.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icategorysecond.Name);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, icategorysecond.FK_Category);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, icategorysecond.Shortname);
            db.AddInParameter(dbCommand, "@ReturnPeriod", DbType.Int32, icategorysecond.ReturnPeriod);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icategorysecond.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icategorysecond.UserCode);


            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(icategorysecond, e);
                return 0;
            }
        }

        public ICategorySecond SelectCategorySecond(ICategorySecond icategorysecond)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCategorySecondSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_SubCategory", DbType.Int64, icategorysecond.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icategorysecond.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icategorysecond.Name);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, icategorysecond.Shortname);
          
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        icategorysecond.Name = Convert.ToString(dataReader["SubCategoryName"].ToString());
                        icategorysecond.FK_Category = Convert.ToInt64(dataReader["FK_Category"].ToString());
                        icategorysecond.Shortname = Convert.ToString(dataReader["Shortname"].ToString());
                        icategorysecond.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        icategorysecond.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        icategorysecond.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        

                    }
                }
                return icategorysecond;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategorysecond, e);
                throw e;
            }
        }

        public DataTable SelectAllCategorySecond(ICategorySecond icategorysecond)
		{
			DataTable dtblCategorySecond = new DataTable();
			  Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proCategorySecondSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_SubCategory", DbType.Int64, icategorysecond.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,icategorysecond.FK_Client);
			db.AddInParameter(dbCommand, "@Name", DbType.String, icategorysecond.Name);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, icategorysecond.Shortname);
			
			try
			{
				 dtblCategorySecond = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblCategorySecond;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(icategorysecond, e);
                throw e;
			}
		}

        public int DeleteCategorySecond(ICategorySecond icategorysecond)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCategorySecondDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_SubCategory", DbType.Int64, icategorysecond.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icategorysecond.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icategorysecond.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, icategorysecond.FK_Reason);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategorysecond, e);
                throw e;
            }
        }

    }
}
