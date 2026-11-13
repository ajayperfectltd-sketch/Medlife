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
    public class DalColor
    {

        public Int32 UpdateColor(IColor icolor)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proColorUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, icolor.UserAction);
            db.AddInParameter(dbCommand, "@ID_Color", DbType.Int64, icolor.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icolor.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, icolor.ShortName);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icolor.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icolor.UserCode);


            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(icolor, e);
                return 0;
            }
        }

        public IColor SelectColor(IColor icolor)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proColorSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Color", DbType.Int64, icolor.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icolor.FK_Client);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, icolor.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icolor.Name);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, icolor.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, icolor.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        icolor.Name = Convert.ToString(dataReader["Name"].ToString());
                        icolor.ShortName = Convert.ToString(dataReader["ShortName"].ToString());
                        icolor.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        icolor.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        icolor.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return icolor;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icolor, e);
                throw e;
            }
        }

        public DataTable SelectAllColor(IColor icolor)
		{
			DataTable dtblColor = new DataTable();
			Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proColorSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Color",DbType.Int64,icolor.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,icolor.FK_Client);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, icolor.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icolor.Name);
			db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, icolor.PageIndex);
			db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, icolor.PageSize);
			try
			{
				 dtblColor = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblColor;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(icolor, e);
                throw e;
			}
		}

        public int DeleteColor(IColor icolor)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proColorDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Color", DbType.Int64, icolor.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icolor.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icolor.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icolor, e);
                throw e;
            }
        }

    }
}
