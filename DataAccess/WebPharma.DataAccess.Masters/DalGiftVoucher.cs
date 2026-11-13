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
    public class DalGiftVoucher
    {

        public Int32 UpdateGiftvoucher(IGiftVoucher iGiftvoucher)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proGiftvoucherUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iGiftvoucher.UserAction);
            db.AddInParameter(dbCommand, "@ID_Giftvoucher", DbType.Int64, iGiftvoucher.MasterId);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, iGiftvoucher.Shortname);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iGiftvoucher.Name);
            db.AddInParameter(dbCommand, "@VoucherType", DbType.Int32, iGiftvoucher.VoucherType);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iGiftvoucher.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iGiftvoucher.UserCode);


            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iGiftvoucher, e);
                return 0;
            }
        }

        public IGiftVoucher SelectGiftvoucher(IGiftVoucher iGiftvoucher)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proGiftvoucherSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Giftvoucher", DbType.Int64, iGiftvoucher.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iGiftvoucher.FK_Client);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, iGiftvoucher.Shortname);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iGiftvoucher.Name);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iGiftvoucher.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iGiftvoucher.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iGiftvoucher.Shortname = Convert.ToString(dataReader["Shortname"].ToString());
                        iGiftvoucher.Name = Convert.ToString(dataReader["Name"].ToString());
                        iGiftvoucher.VoucherType = Convert.ToInt32(dataReader["VoucherType"].ToString());
                        iGiftvoucher.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iGiftvoucher.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iGiftvoucher.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return iGiftvoucher;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iGiftvoucher, e);
                throw e;
            }
        }

        public DataTable SelectAllGiftvoucher(IGiftVoucher iGiftvoucher)
        {
            DataTable dtblGiftvoucher = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proGiftvoucherSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Giftvoucher", DbType.Int64, iGiftvoucher.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iGiftvoucher.FK_Client);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, iGiftvoucher.Shortname);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iGiftvoucher.Name);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iGiftvoucher.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iGiftvoucher.PageSize);
            try
            {
                dtblGiftvoucher = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblGiftvoucher;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iGiftvoucher, e);
                throw e;
            }
        }

        public int DeleteGiftvoucher(IGiftVoucher iGiftvoucher)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proGiftvoucherDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Giftvoucher", DbType.Int64, iGiftvoucher.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iGiftvoucher.FK_Client);
            ////db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, iGiftvoucher.ReasonCode);
            ////db.AddInParameter(dbCommand, "@TransMode", DbType.String, iGiftvoucher.TransMode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iGiftvoucher.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iGiftvoucher, e);
                throw e;
            }
        }





       
    }
    
}
