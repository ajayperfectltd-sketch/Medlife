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
    public class DalCessSettings
    {
        public Int32 UpdateCessSettings(ICessSettings icesssettings)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCessSettingsUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, icesssettings.UserAction);
            db.AddInParameter(dbCommand, "@ID_CessSettings", DbType.Int64, icesssettings.MasterId);
            db.AddInParameter(dbCommand, "@EffectFrom", DbType.DateTime, icesssettings.EffectFrom);
            db.AddInParameter(dbCommand, "@CGST", DbType.Decimal, icesssettings.CGST);
            db.AddInParameter(dbCommand, "@SGST", DbType.Decimal, icesssettings.SGST);
            db.AddInParameter(dbCommand, "@IGST", DbType.Decimal, icesssettings.IGST);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icesssettings.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icesssettings.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, icesssettings.EnteredOn);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public ICessSettings SelectCessSettings(ICessSettings icesssettings)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCessSettingsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_CessSettings", DbType.Int64, icesssettings.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icesssettings.FK_Client);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, icesssettings.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, icesssettings.ToDate);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, icesssettings.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, icesssettings.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        icesssettings.EffectFrom = Convert.ToDateTime(dataReader["EffectFrom"].ToString());
                        icesssettings.CGST = Convert.ToDecimal(dataReader["CGST"].ToString());
                        icesssettings.SGST = Convert.ToDecimal(dataReader["SGST"].ToString());
                        icesssettings.IGST = Convert.ToDecimal(dataReader["IGST"].ToString());
                        icesssettings.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        icesssettings.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        icesssettings.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return icesssettings;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable SelectAllCessSettings(ICessSettings icesssettings)
        {
            DataTable dtblBrand = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCessSettingsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_CessSettings", DbType.Int64, icesssettings.MasterId);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, icesssettings.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, icesssettings.ToDate);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icesssettings.FK_Client);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, icesssettings.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, icesssettings.PageSize);

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
    }
}
