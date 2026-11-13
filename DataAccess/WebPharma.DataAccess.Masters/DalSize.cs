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
    public class DalSize
    {

        public Int32 UpdateSize(ISize isize)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSizeUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, isize.UserAction);
            db.AddInParameter(dbCommand, "@ID_Size", DbType.Int64, isize.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, isize.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, isize.ShortName);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isize.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, isize.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, isize.EnteredOn);


            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(isize, e);
                return 0;
            }
        }

        public ISize SelectSize(ISize isize)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSizeSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Size", DbType.Int64, isize.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isize.FK_Client);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, isize.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, isize.Name);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, isize.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, isize.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        isize.Name = Convert.ToString(dataReader["Name"].ToString());
                        isize.ShortName = Convert.ToString(dataReader["ShortName"].ToString());
                        isize.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        isize.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        isize.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return isize;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(isize, e);
                throw e;
            }
        }

        public DataTable SelectAllSize(ISize isize)
		{
			DataTable dtblSize = new DataTable();
			   Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proSizeSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Size",DbType.Int64,isize.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,isize.FK_Client);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, isize.ShortName );
            db.AddInParameter(dbCommand, "@Name", DbType.String, isize.Name);
			db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, isize.PageIndex);
			db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, isize.PageSize);
			try
			{
				 dtblSize = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblSize;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(isize, e);
                throw e;
			}
		}

        public int DeleteSize(ISize isize)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSizeDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Size", DbType.Int64, isize.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isize.FK_Client);
            //db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, isize.ReasonCode);
            //db.AddInParameter(dbCommand, "@TransMode", DbType.String, isize.TransMode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, isize.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(isize, e);
                throw e;
            }
        }

    }
}
