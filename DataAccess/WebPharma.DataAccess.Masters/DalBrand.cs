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
    public class DalBrand
    {

        public Int32 UpdateBrand(IBrand ibrand)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBrandUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ibrand.UserAction);
            db.AddInParameter(dbCommand, "@ID_Brand", DbType.Int64, ibrand.MasterId);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, ibrand.Shortname);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ibrand.Name);
            db.AddInParameter(dbCommand, "@Remarks", DbType.String, ibrand.Remarks);
            db.AddInParameter(dbCommand, "@FK_Manufacturer", DbType.Int64, ibrand.FK_Manufacturer);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ibrand.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ibrand.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, ibrand.EnteredOn);



            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(ibrand, e);
                return 0;
            }
        }

        public IBrand SelectBrand(IBrand ibrand)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBrandSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Brand", DbType.Int64, ibrand.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ibrand.FK_Client);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String,ibrand.Shortname);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ibrand.Name);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ibrand.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ibrand.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ibrand.Shortname = Convert.ToString(dataReader["Shortname"].ToString());
                        ibrand.Name = Convert.ToString(dataReader["Name"].ToString());
                        ibrand.Remarks = Convert.ToString(dataReader["Remarks"].ToString());
                        ibrand.FK_Manufacturer = Convert.ToInt64(dataReader["FK_Manufacturer"].ToString());
                        ibrand.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        ibrand.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        ibrand.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return ibrand;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(ibrand, e);
                throw e;
            }
        }

        public DataTable SelectAllBrand(IBrand ibrand)
		{
			DataTable dtblBrand = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proBrandSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Brand",DbType.Int64,ibrand.MasterId);
			db.AddInParameter(dbCommand, "@ShortName", DbType.String,ibrand.Shortname);
			db.AddInParameter(dbCommand, "@Name", DbType.String, ibrand.Name);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ibrand.FK_Client);
			db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ibrand.PageIndex);
			db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ibrand.PageSize);

			try
			{
				 dtblBrand = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblBrand;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(ibrand, e);
                throw e;
			}
		}

        public int DeleteBrand(IBrand ibrand)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBrandDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Brand", DbType.Int64, ibrand.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ibrand.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ibrand.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(ibrand, e);
                throw e;
            }
        }

    }
}
