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
    public class DalStorage
    {

        public Int32 UpdateStorage(IStorage istorage)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStorageUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, istorage.UserAction);

            db.AddInParameter(dbCommand, "@ID_Storage", DbType.Int64, istorage.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, istorage.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, istorage.ShortName);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, istorage.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, istorage.UserCode);


            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istorage, e);
                return 0;
            }
        }

        public IStorage SelectStorage(IStorage istorage)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStorageSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Storage", DbType.Int64, istorage.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, istorage.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, istorage.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, istorage.ShortName);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, istorage.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, istorage.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        istorage.Name = Convert.ToString(dataReader["Name"].ToString());
                        istorage.ShortName = Convert.ToString(dataReader["ShortName"].ToString());
                        istorage.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        istorage.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        istorage.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return istorage;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istorage, e);
                throw e;
            }
        }

        public DataTable SelectAllStorage(IStorage istorage)
		{
			DataTable dtblStorage = new DataTable();
		    Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proStorageSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Storage",DbType.Int64,istorage.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,istorage.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, istorage.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, istorage.ShortName);
			db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, istorage.PageIndex);
			db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, istorage.PageSize);
			try
			{
				 dtblStorage = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblStorage;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(istorage, e);
                throw e;
			}
		}

        public int DeleteStorage(IStorage istorage)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStorageDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Storage", DbType.Int64, istorage.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, istorage.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, istorage.UserCode);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istorage, e);
                throw e;
            }
        }

    }
}
