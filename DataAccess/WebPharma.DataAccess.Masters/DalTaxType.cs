/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : TaxType Data Class
Author     : Praveen
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 18/05/2018
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
    public class DalTaxType
    {

        public Int32 UpdateTaxType(ITaxType iTaxType)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proTaxTypeUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iTaxType.UserAction);
            db.AddInParameter(dbCommand, "@ID_TaxType", DbType.Int64, iTaxType.MasterId);
            db.AddInParameter(dbCommand, "@TaxType", DbType.String, iTaxType.TypeName);
            db.AddInParameter(dbCommand, "@FK_AccountHead", DbType.Int64, iTaxType.FK_AccountHead);
            db.AddInParameter(dbCommand, "@FK_AccountHeadSub", DbType.Int64, iTaxType.FK_AccountHeadSub);
         
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iTaxType.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iTaxType.UserCode);
            db.AddInParameter(dbCommand, "@TaxInterState", DbType.Boolean, iTaxType.TaxInterState);
            db.AddInParameter(dbCommand, "@TaxIntraState", DbType.Boolean, iTaxType.TaxIntraState);
            db.AddInParameter(dbCommand, "@GSTType", DbType.Byte, iTaxType.GSTType);
         
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iBillType, e);
                return 0;
            }
        }

        public ITaxType SelectTaxType(ITaxType iTaxType)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proTaxTypeSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_TaxType", DbType.Int64, iTaxType.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iTaxType.FK_Client);
            db.AddInParameter(dbCommand, "@TaxType", DbType.String, iTaxType.TypeName);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iTaxType.TypeName = Convert.ToString(dataReader["TaxType"].ToString());
                        iTaxType.FK_AccountHead = Convert.ToInt64(dataReader["FK_AccountHead"].ToString());
                        iTaxType.FK_AccountHeadSub = Convert.ToInt64(dataReader["FK_AccountHeadSub"].ToString());
                        iTaxType.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iTaxType.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iTaxType.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        iTaxType.TaxInterState = Convert.ToBoolean(dataReader["TaxInterState"].ToString());
                        iTaxType.TaxIntraState = Convert.ToBoolean(dataReader["TaxIntraState"].ToString());
                        iTaxType.GSTType = Convert.ToByte(dataReader["GSTType"].ToString());
                    }
                }
                return iTaxType;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iBillType, e);
                throw e;
            }
        }

        public DataTable SelectAllTaxType(ITaxType iTaxType)
		{
			DataTable dtblBillType = new DataTable();
			  Database db = DatabaseFactory.CreateDatabase("WebPharma");
              string sqlCommand = "proTaxTypeSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_TaxType",DbType.Int64,iTaxType.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,iTaxType.FK_Client);
			db.AddInParameter(dbCommand, "@TaxType", DbType.String, iTaxType.TypeName);
			
			try
			{
				 dtblBillType = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblBillType;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(iBillType, e);
                throw e;
			}
		}

        public int DeleteTaxType(ITaxType iTaxType)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proTaxTypeDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_TaxType", DbType.Int64, iTaxType.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iTaxType.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iTaxType.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, iTaxType.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iBillType, e);
                throw e;
            }
        }

    }
}
