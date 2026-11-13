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
    public class DalVoucher
    {

        public Int32 UpdateVoucher(IVoucher ivoucher)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proVoucherUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ivoucher.UserAction);
         
            db.AddInParameter(dbCommand, "@ID_Voucher", DbType.Int64, ivoucher.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ivoucher.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ivoucher.Name);
            db.AddInParameter(dbCommand, "@Type", DbType.Byte, ivoucher.Type);
            db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, ivoucher.Amount);
            db.AddInParameter(dbCommand, "@Percentage", DbType.Decimal, ivoucher.Percentage);
            db.AddInParameter(dbCommand, "@validity", DbType.Int32, ivoucher.validity);
            db.AddInParameter(dbCommand, "@FK_AccountHeadSub", DbType.Int64, ivoucher.FK_AccountHeadSub);
            db.AddInParameter(dbCommand, "@Remarks", DbType.String, ivoucher.Remarks);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, ivoucher.Active);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ivoucher.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ivoucher.UserCode);


            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(ivoucher, e);
                return 0;
            }
        }

        public IVoucher SelectVoucher(IVoucher ivoucher)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proVoucherSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Voucher", DbType.Int64, ivoucher.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ivoucher.FK_Client);

            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ivoucher.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ivoucher.Name);

            db.AddInParameter(dbCommand, "@Type", DbType.Byte, ivoucher.Type);
            db.AddInParameter(dbCommand, "@FK_AccountHeadSub", DbType.Int64, ivoucher.FK_AccountHeadSub);

           
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ivoucher.ShortName = Convert.ToString(dataReader["ShortName"].ToString());
                        ivoucher.Name = Convert.ToString(dataReader["Name"].ToString());
                        ivoucher.Type = Convert.ToByte(dataReader["Type"].ToString());
                        ivoucher.Amount = Convert.ToDecimal(dataReader["Amount"].ToString());
                        ivoucher.Percentage = Convert.ToDecimal(dataReader["Percentage"].ToString());
                        ivoucher.validity = Convert.ToInt32(dataReader["validity"].ToString());
                        ivoucher.FK_AccountHeadSub = Convert.ToInt64(dataReader["FK_AccountHeadSub"].ToString());
                        ivoucher.Remarks = Convert.ToString(dataReader["Remarks"].ToString());
                        ivoucher.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                        ivoucher.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        ivoucher.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        ivoucher.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        ivoucher.IsRefrence = Convert.ToBoolean(dataReader["IsReferable"]);
                    }
                }
                return ivoucher;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(ivoucher, e);
                throw e;
            }
        }

        public DataTable SelectAllVoucher(IVoucher ivoucher)
		{
			DataTable dtblVoucher = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proVoucherSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Voucher",DbType.Int64,ivoucher.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,ivoucher.FK_Client);

            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ivoucher.ShortName);
			db.AddInParameter(dbCommand, "@Name", DbType.String, ivoucher.Name);

            db.AddInParameter(dbCommand, "@Type", DbType.Byte, ivoucher.Type);
            db.AddInParameter(dbCommand, "@FK_AccountHeadSub", DbType.Int64, ivoucher.FK_AccountHeadSub);

			db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ivoucher.PageIndex);
			db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ivoucher.PageSize);
			try
			{
				 dtblVoucher = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblVoucher;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(ivoucher, e);
                throw e;
			}
		}

        public int DeleteVoucher(IVoucher ivoucher)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proVoucherDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Voucher", DbType.Int64, ivoucher.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ivoucher.FK_Client);
          //  db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, ivoucher.ReasonCode);
           // db.AddInParameter(dbCommand, "@TransMode", DbType.String, ivoucher.TransMode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ivoucher.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(ivoucher, e);
                throw e;
            }
        }

    }
}
