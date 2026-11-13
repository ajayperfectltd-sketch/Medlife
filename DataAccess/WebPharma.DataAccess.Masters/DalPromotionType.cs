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
    public class DalPromotionType
    {

        public Int32 UpdatePromotionType(IPromotionType ipromotiontype)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPromotionTypeUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ipromotiontype.UserAction);
            db.AddInParameter(dbCommand, "@ID_PromotionType", DbType.Int64, ipromotiontype.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ipromotiontype.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ipromotiontype.ShortName);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipromotiontype.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipromotiontype.UserCode);


            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(ipromotiontype, e);
                return 0;
            }
        }

        public IPromotionType SelectPromotionType(IPromotionType ipromotiontype)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPromotionTypeSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_PromotionType", DbType.Int64, ipromotiontype.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ipromotiontype.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ipromotiontype.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ipromotiontype.ShortName);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ipromotiontype.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ipromotiontype.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ipromotiontype.Name = Convert.ToString(dataReader["Name"].ToString());
                        ipromotiontype.ShortName = Convert.ToString(dataReader["ShortName"].ToString());
                        ipromotiontype.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        ipromotiontype.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        ipromotiontype.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return ipromotiontype;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(ipromotiontype, e);
                throw e;
            }
        }

        public DataTable SelectAllPromotionType(IPromotionType ipromotiontype)
		{
			DataTable dtblPromotionType = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proPromotionTypeSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_PromotionType",DbType.Int64,ipromotiontype.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,ipromotiontype.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ipromotiontype.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ipromotiontype.ShortName);
			db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ipromotiontype.PageIndex);
			db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ipromotiontype.PageSize);
			try
			{
				 dtblPromotionType = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblPromotionType;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(ipromotiontype, e);
                throw e;
			}
		}

        public int DeletePromotionType(IPromotionType ipromotiontype)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPromotionTypeDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_PromotionType", DbType.Int64, ipromotiontype.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ipromotiontype.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipromotiontype.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(ipromotiontype, e);
                throw e;
            }
        }

    }
}
