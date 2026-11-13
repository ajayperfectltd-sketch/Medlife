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
    public class DalVoucherGeneration
    {

        public string UpdateVoucherGeneration(IVoucherGeneration ivouchergeneration)
        {
            
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proVoucherGenerationUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ivouchergeneration.UserAction);
            db.AddInParameter(dbCommand, "@ID_VoucherGeneration", DbType.Int64, ivouchergeneration.MasterId);
            db.AddInParameter(dbCommand, "@FK_Voucher", DbType.Int64, ivouchergeneration.FK_Voucher);
           // db.AddInParameter(dbCommand, "@VoucherNo", DbType.String, ivouchergeneration.VoucherNo);
            db.AddInParameter(dbCommand, "@ValidFrom", DbType.DateTime, ivouchergeneration.ValidFrom);
           // db.AddInParameter(dbCommand, "@IsUsed", DbType.Boolean, ivouchergeneration.IsUsed);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ivouchergeneration.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ivouchergeneration.UserCode);
            db.AddInParameter(dbCommand, "@Nos", DbType.Int64, ivouchergeneration.Nos);


            try
            {
                return Convert.ToString(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(ivouchergeneration, e);
                return "";
            }
        }

        public IVoucherGeneration SelectVoucherGeneration(IVoucherGeneration ivouchergeneration)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proVoucherGenerationSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_VoucherGeneration", DbType.Int64, ivouchergeneration.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ivouchergeneration.FK_Client);
           
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ivouchergeneration.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ivouchergeneration.PageSize);
            db.AddInParameter(dbCommand, "@IsUsed", DbType.Byte, ivouchergeneration.IsUsed);
            db.AddInParameter(dbCommand, "@ValidFrom", DbType.DateTime, ivouchergeneration.ValidFrom);
            db.AddInParameter(dbCommand, "@ValidUpto", DbType.DateTime, ivouchergeneration.ValidUpto);
            db.AddInParameter(dbCommand, "@VoucherNo", DbType.String, ivouchergeneration.VoucherNo);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ivouchergeneration.FK_Voucher = Convert.ToInt64(dataReader["FK_Voucher"].ToString());
                        ivouchergeneration.VoucherNo = Convert.ToString(dataReader["VoucherNo"].ToString());
                        ivouchergeneration.ValidUpto = Convert.ToDateTime(dataReader["ValidUpto"].ToString());
                       // ivouchergeneration.IsUsed = Convert.ToBoolean(dataReader["IsUsed"].ToString());
                        ivouchergeneration.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        ivouchergeneration.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        ivouchergeneration.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        ivouchergeneration.ValidFrom = Convert.ToDateTime(dataReader["ValidFrom"].ToString());
                       // ivouchergeneration.Nos = Convert.ToInt32(dataReader["Nos"]);

                    }
                }
                return ivouchergeneration;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(ivouchergeneration, e);
                throw e;
            }
        }

        public DataTable SelectAllVoucherGeneration(IVoucherGeneration ivouchergeneration)
		{
			DataTable dtblVoucherGeneration = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proVoucherGenerationSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_VoucherGeneration", DbType.Int64, ivouchergeneration.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ivouchergeneration.FK_Client);
         
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ivouchergeneration.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ivouchergeneration.PageSize);
            db.AddInParameter(dbCommand, "@IsUsed", DbType.Byte, ivouchergeneration.IsUsed);
            db.AddInParameter(dbCommand, "@ValidFrom", DbType.DateTime, ivouchergeneration.ValidFrom);
            db.AddInParameter(dbCommand, "@ValidUpto", DbType.DateTime, ivouchergeneration.ValidUpto);
            db.AddInParameter(dbCommand, "@VoucherNo", DbType.String, ivouchergeneration.VoucherNo);
            db.AddInParameter(dbCommand, "@VoucherType", DbType.Byte, ivouchergeneration.VoucherType);
           
            db.AddInParameter(dbCommand, "@FK_Voucher", DbType.Int64, ivouchergeneration.FK_Voucher);
            try
			{
               dtblVoucherGeneration = db.ExecuteDataSet(dbCommand).Tables[0];
               return dtblVoucherGeneration;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(ivouchergeneration, e);
                throw e;
			}
		}

        public int DeleteVoucherGeneration(IVoucherGeneration ivouchergeneration)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proVoucherGenerationDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_VoucherGeneration", DbType.Int64, ivouchergeneration.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ivouchergeneration.FK_Client);
            //db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, ivouchergeneration);
           // db.AddInParameter(dbCommand, "@TransMode", DbType.String, ivouchergeneration.TransMode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ivouchergeneration.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(ivouchergeneration, e);
                throw e;
            }
        }
        public int VoucherValidUptoDate(IVoucherGeneration ivouchergeneration)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proVoucherValidUptoDate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Voucher", DbType.Int64, ivouchergeneration.FK_Voucher);
          
          
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                
                throw e;
            }
        }
    }
}
