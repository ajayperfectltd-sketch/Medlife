/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     :Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
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
using WebPharma.Interface.WebPharma;



namespace WebPharma.DataAccess.WebPharma
{
    public class DalPromotionSettings
    {
        public Int32 UpdatePromotionSettings(IPromotionSetting ipromotion)
        {

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPromotionSettingsUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ipromotion.UserAction);
            db.AddInParameter(dbCommand, "@ID_PromotionSettings", DbType.Int64, ipromotion.MasterId);           
            db.AddInParameter(dbCommand, "@FK_PromotionType", DbType.Int64, ipromotion.FK_PromotionType);
            db.AddInParameter(dbCommand, "@EffectDate", DbType.DateTime, ipromotion.EffectDate);
            db.AddInParameter(dbCommand, "@DiscountPercent", DbType.Decimal, ipromotion.DiscountPercent);
            db.AddInParameter(dbCommand, "@Buy", DbType.Int64, ipromotion.Buy);
            db.AddInParameter(dbCommand, "@Get", DbType.Int64, ipromotion.Get);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipromotion.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, ipromotion.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipromotion.FK_Client);
            db.AddInParameter(dbCommand, "@Closed", DbType.Boolean, ipromotion.Closed);
            db.AddInParameter(dbCommand, "@CloseDate", DbType.DateTime, ipromotion.CloseDate);

            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, ipromotion.FK_Items);
            db.AddInParameter(dbCommand, "@FK_CategoryFirst", DbType.Int64, ipromotion.FK_CategoryFirst);
            db.AddInParameter(dbCommand, "@FK_Brand", DbType.Int64, ipromotion.FK_Brand);
            db.AddInParameter(dbCommand, "@Type", DbType.Int16, ipromotion.Type);
        

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                return 0;
            }



        }

        public IPromotionSetting SelectPromotionSettings(IPromotionSetting ipromotion)
        {

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "promotionSettingsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_PromotionSettings", DbType.Int64, ipromotion.MasterId);
            db.AddInParameter(dbCommand, "@FK_PromotionType", DbType.Int64, ipromotion.FK_PromotionType);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ipromotion.PageSize);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ipromotion.PageIndex);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipromotion.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, ipromotion.FK_Items);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ipromotion.FK_PromotionType = Convert.ToInt64(dataReader["FK_PromotionType"]);
                       
                        ipromotion.EffectDate = Convert.ToDateTime(dataReader["EffectDate"]);
                        ipromotion.DiscountPercent = Convert.ToDecimal(dataReader["DiscountPercent"]);
                        ipromotion.Buy = Convert.ToInt64(dataReader["Buy"]);
                        ipromotion.Get = Convert.ToInt64(dataReader["Get"]);
                        ipromotion.Closed = Convert.ToBoolean(dataReader["Closed"]);
                        ipromotion.CloseDate = Convert.ToDateTime(dataReader["CloseDate"]);
                        ipromotion.Type = Convert.ToInt16(dataReader["Type"]);

                        ipromotion.FK_Items = Convert.ToInt64(dataReader["FK_Items"]);
                        ipromotion.FK_CategoryFirst = Convert.ToInt64(dataReader["FK_CategoryFirst"]);
                        ipromotion.FK_Brand = Convert.ToInt64(dataReader["FK_Brand"]);
                        
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return ipromotion;

        }

        public DataTable SelectAllData(IPromotionSetting ipromotion)
        {
            DataTable dtblPromotion = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "promotionSettingsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_PromotionSettings", DbType.Int64, ipromotion.MasterId);
            db.AddInParameter(dbCommand, "@FK_PromotionType", DbType.Int64, ipromotion.FK_PromotionType);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ipromotion.PageSize);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ipromotion.PageIndex);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipromotion.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, ipromotion.FK_Items);
            try
            {
                dtblPromotion = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblPromotion;
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        public DataTable SelectPromotionSettingsItemGetQuantity(IPromotionSetting ipromotion)
        {
            DataTable dtblPromotion = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSelectPromotionSettingsItemGetQuantity";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, ipromotion.FK_Items);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipromotion.FK_Client);
            db.AddInParameter(dbCommand, "@Buy", DbType.Int64 , ipromotion.Buy);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, ipromotion.Date);

            try
            {
                dtblPromotion = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblPromotion;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(idiscountsettings, e);
                throw e;
            }
        }

        public int DeletepromotionSettings(IPromotionSetting ipromotion)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "promotionSettingsDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);   
            db.AddInParameter(dbCommand, "@ID_PromotionSettings", DbType.Int64, ipromotion.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipromotion.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
    }
}
