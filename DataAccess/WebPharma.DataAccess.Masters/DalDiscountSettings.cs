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
    public class DalDiscountSettings
    {

        public Int32 UpdateDiscountSettings(IDiscountSettings idiscountsettings)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDiscountSettingsUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, idiscountsettings.UserAction);
  
            db.AddInParameter(dbCommand, "@ID_DiscountSettings", DbType.Int64, idiscountsettings.MasterId);
            db.AddInParameter(dbCommand, "@FK_Discount", DbType.Int64, idiscountsettings.FK_Discount);
            db.AddInParameter(dbCommand, "@DiscountType", DbType.Byte, idiscountsettings.DiscountType);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, idiscountsettings.FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, idiscountsettings.FK_Customer);
            
            db.AddInParameter(dbCommand, "@StockCode", DbType.Int64, idiscountsettings.StockCode);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, idiscountsettings.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, idiscountsettings.ToDate);
            db.AddInParameter(dbCommand, "@Closed", DbType.Boolean, idiscountsettings.Closed);
            db.AddInParameter(dbCommand, "@Percentage", DbType.Decimal, idiscountsettings.Percentage);
            db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, idiscountsettings.Amount);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, idiscountsettings.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, idiscountsettings.UserCode);
            db.AddInParameter(dbCommand, "@FK_SalesMan", DbType.Int64, idiscountsettings.FK_SalesMan);
            db.AddInParameter(dbCommand, "@DisName", DbType.String, idiscountsettings.DisName);
            db.AddInParameter(dbCommand, "@FK_CustomerGroup", DbType.Int64, idiscountsettings.FK_CustomerGroup);

            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, idiscountsettings.FK_Items);
            db.AddInParameter(dbCommand, "@FK_CategoryFirst", DbType.Int64, idiscountsettings.FK_CategoryFirst);
            db.AddInParameter(dbCommand, "@FK_Brand", DbType.Int64, idiscountsettings.FK_Brand);
         
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(idiscountsettings, e);
                return 0;
            }
        }

        public IDiscountSettings SelectDiscountSettings(IDiscountSettings idiscountsettings)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDiscountSettingsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_DiscountSettings", DbType.Int64, idiscountsettings.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, idiscountsettings.FK_Client);
            //db.AddInParameter(dbCommand, "@Code", DbType.String,idiscountsettings.Code);
            //db.AddInParameter(dbCommand, "@Name", DbType.String, idiscountsettings.Name);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, idiscountsettings.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, idiscountsettings.ToDate);
            db.AddInParameter(dbCommand, "@DiscountType", DbType.Byte, idiscountsettings.DiscountType);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, idiscountsettings.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, idiscountsettings.PageSize);
            db.AddInParameter(dbCommand, "@FK_Discount", DbType.Int64, idiscountsettings.FK_Discount);
            db.AddInParameter(dbCommand, "@DisName", DbType.String, idiscountsettings.DisName);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        idiscountsettings.FK_Discount = Convert.ToInt64(dataReader["FK_Discount"].ToString());
                        idiscountsettings.DiscountType = Convert.ToByte(dataReader["DiscountType"].ToString());
                        idiscountsettings.FK_Employee = Convert.ToInt64(dataReader["FK_Employee"].ToString());
                        idiscountsettings.FK_Customer = Convert.ToInt64(dataReader["FK_Customer"].ToString());
                       
                        idiscountsettings.StockCode = Convert.ToInt64(dataReader["StockCode"].ToString());
                        idiscountsettings.FromDate = Convert.ToDateTime(dataReader["FromDate"].ToString());
                        idiscountsettings.ToDate = Convert.ToDateTime(dataReader["ToDate"].ToString());
                        idiscountsettings.Closed = Convert.ToBoolean(dataReader["Closed"].ToString());
                        idiscountsettings.Percentage = Convert.ToDecimal(dataReader["Percentage"].ToString());
                        idiscountsettings.Amount = Convert.ToDecimal(dataReader["Amount"].ToString());
                        idiscountsettings.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        idiscountsettings.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        idiscountsettings.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        idiscountsettings.FK_SalesMan = Convert.ToInt64(dataReader["FK_SalesMan"].ToString());
                        idiscountsettings.DisName = dataReader["Discount"].ToString();
                        idiscountsettings.FK_CustomerGroup = Convert.ToInt64(dataReader["FK_CustomerGroup"].ToString());

                        idiscountsettings.FK_Items = Convert.ToInt64(dataReader["FK_Items"].ToString());
                        idiscountsettings.FK_CategoryFirst = Convert.ToInt64(dataReader["FK_CategoryFirst"].ToString());
                        idiscountsettings.FK_Brand = Convert.ToInt64(dataReader["FK_Brand"].ToString());
                    }
                }
                return idiscountsettings;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(idiscountsettings, e);
                throw e;
            }
        }

        public DataTable SelectAllDiscountSettings(IDiscountSettings idiscountsettings)
		{
			DataTable dtblDiscountSettings = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proDiscountSettingsSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_DiscountSettings", DbType.Int64, idiscountsettings.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,idiscountsettings.FK_Client);
			//db.AddInParameter(dbCommand, "@Code", DbType.String,idiscountsettings.Code);
			//db.AddInParameter(dbCommand, "@Name", DbType.String, idiscountsettings.Name);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, idiscountsettings.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, idiscountsettings.ToDate);
            db.AddInParameter(dbCommand, "@DiscountType", DbType.Byte, idiscountsettings.DiscountType);
			db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, idiscountsettings.PageIndex);
			db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, idiscountsettings.PageSize);
            db.AddInParameter(dbCommand, "@FK_Discount", DbType.Int64, idiscountsettings.FK_Discount);
            db.AddInParameter(dbCommand, "@DisName", DbType.String, idiscountsettings.DisName);
            
			try
			{
				 dtblDiscountSettings = db.ExecuteDataSet(dbCommand).Tables[0];
				 return dtblDiscountSettings;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(idiscountsettings, e);
                throw e;
			}
		}
        public DataTable SelectDiscountSettingsItems(IDiscountSettings idiscountsettings)
        {
            DataTable dtblDiscountSettings = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectDiscountSettingsItems";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
         
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, idiscountsettings.FK_Items);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, idiscountsettings.FK_Client);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, idiscountsettings.Date);
            
            try
            {
                dtblDiscountSettings = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblDiscountSettings;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(idiscountsettings, e);
                throw e;
            }
        }
        public DataTable SelectDiscountSettingsCustomer(IDiscountSettings idiscountsettings)
        {
            DataTable dtblDiscountSettings = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectDiscountSettingsCustomer";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, idiscountsettings.FK_Customer);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, idiscountsettings.FK_Client);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, idiscountsettings.Date);

            try
            {
                dtblDiscountSettings = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblDiscountSettings;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(idiscountsettings, e);
                throw e;
            }
        }

        public DataTable SelectDiscountSettingsCustomerGroup(IDiscountSettings idiscountsettings)
        {
            DataTable dtblDiscountSettings = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectDiscountSettingsCustomerGroup";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, idiscountsettings.FK_Customer);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, idiscountsettings.FK_Client);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, idiscountsettings.Date);

            try
            {
                dtblDiscountSettings = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblDiscountSettings;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(idiscountsettings, e);
                throw e;
            }
        }
        public DataTable SelectDiscountSettingsSalesMan(IDiscountSettings idiscountsettings)
        {
            DataTable dtblDiscountSettings = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectDiscountSettingsSalesMan";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_SalesMan", DbType.Int64, idiscountsettings.FK_SalesMan);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, idiscountsettings.FK_Client);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, idiscountsettings.Date);

            try
            {
                dtblDiscountSettings = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblDiscountSettings;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(idiscountsettings, e);
                throw e;
            }
        }
        public DataTable SelectDiscountSettingsEmployee(IDiscountSettings idiscountsettings)
        {
            DataTable dtblDiscountSettings = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectDiscountSettingsEmployee";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, idiscountsettings.FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, idiscountsettings.FK_Client);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, idiscountsettings.Date);

            try
            {
                dtblDiscountSettings = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblDiscountSettings;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(idiscountsettings, e);
                throw e;
            }
        }
        public DataTable SelectVoucherDiscount(IDiscountSettings idiscountsettings)
        {
            DataTable dtblDiscountSettings = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSelectVoucherDiscount";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@VoucherNo", DbType.String, idiscountsettings.VoucherNo);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, idiscountsettings.FK_Client);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, idiscountsettings.Date);

            try
            {
                dtblDiscountSettings = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblDiscountSettings;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(idiscountsettings, e);
                throw e;
            }
        }
        public DataTable SelectVoucherDiscountGift(IDiscountSettings idiscountsettings)
        {
            DataTable dtblDiscountSettings = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSelectVoucherDiscountGift";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@VoucherNo", DbType.String, idiscountsettings.VoucherNo);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, idiscountsettings.FK_Client);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, idiscountsettings.Date);

            try
            {
                dtblDiscountSettings = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblDiscountSettings;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(idiscountsettings, e);
                throw e;
            }
        }
      

        public int DeleteDiscountSettings(IDiscountSettings idiscountsettings)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proDiscountSettingsDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_DiscountSettings", DbType.Int64, idiscountsettings.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, idiscountsettings.FK_Client);
           
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, idiscountsettings.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(idiscountsettings, e);
                throw e;
            }
        }

    }
}
