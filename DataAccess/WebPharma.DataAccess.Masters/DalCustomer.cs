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
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.DataAccess;
using WebPharma.Interface.Masters;

namespace WebPharma.DataAccess.Masters
{
    public class DalCustomer
    {

        public Int32 UpdateCustomer(ICustomer icustomer)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, icustomer.UserAction);
            db.AddInParameter(dbCommand, "@ID_Customer", DbType.Int64, icustomer.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icustomer.Name);
            db.AddInParameter(dbCommand, "@Area", DbType.String, icustomer.Area);
            db.AddInParameter(dbCommand, "@Place", DbType.String, icustomer.Place);
            db.AddInParameter(dbCommand, "@Post", DbType.String, icustomer.Post);
            db.AddInParameter(dbCommand, "@Pin", DbType.String, icustomer.Pin);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, icustomer.Phone);
            db.AddInParameter(dbCommand, "@Mobile", DbType.String, icustomer.Mobile);
            db.AddInParameter(dbCommand, "@LandMark", DbType.String, icustomer.LandMark);
            db.AddInParameter(dbCommand, "@Email", DbType.String, icustomer.Email);
            db.AddInParameter(dbCommand, "@DateOfBirth", DbType.DateTime, icustomer.DateOfBirth);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, icustomer.FK_Country);
            db.AddInParameter(dbCommand, "@FK_State", DbType.Int64, icustomer.FK_State);
            db.AddInParameter(dbCommand, "@FK_District", DbType.Int64, icustomer.FK_District);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, icustomer.Active);
            db.AddInParameter(dbCommand, "@InactiveRemarks", DbType.String, icustomer.InactiveRemarks);
            db.AddInParameter(dbCommand, "@FK_CustomerGroup", DbType.Int64, icustomer.FK_CustomerGroup);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icustomer.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icustomer.UserCode);
            db.AddInParameter(dbCommand, "@IsEmployee", DbType.Boolean, icustomer.IsEmployee);
            db.AddInParameter(dbCommand, "@Idproof", DbType.Int64, icustomer.Idtype);
            db.AddInParameter(dbCommand, "@Cardnumber", DbType.String, icustomer.CardNumber);
            db.AddInParameter(dbCommand, "@CreditDays", DbType.Int64, icustomer.CreditDays);
            db.AddInParameter(dbCommand, "@CreditLimit", DbType.Decimal, icustomer.CreditLimit);
            db.AddInParameter(dbCommand, "@CreditSale", DbType.Boolean, icustomer.CreditSale);
            db.AddInParameter(dbCommand, "@CardPrice", DbType.Boolean, icustomer.CardPrice);
            db.AddInParameter(dbCommand, "@Attachment", DbType.String, icustomer.Attachment);
            db.AddInParameter(dbCommand, "@DiscountPercentage", DbType.Decimal, icustomer.DiscountPercentage);
            db.AddInParameter(dbCommand, "@CustomerCode", DbType.String, icustomer.CustomerCode);
            db.AddInParameter(dbCommand, "@GSTTIN", DbType.String, icustomer.GSTTIN);
          
   
         //   db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, icustomer.FK_Store);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }

        public ICustomer SelectCustomer(ICustomer icustomer)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Customer", DbType.Int64, icustomer.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icustomer.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icustomer.Name);
            db.AddInParameter(dbCommand, "@Mobile", DbType.String, icustomer.Mobile);
            db.AddInParameter(dbCommand, "@Status", DbType.Int16, icustomer.Status);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        icustomer.CustomerCode = Convert.ToString(dataReader["CustomerCode"].ToString());
                        icustomer.Name = Convert.ToString(dataReader["CustomerName"].ToString());
                        icustomer.Area = Convert.ToString(dataReader["Area"].ToString());
                        icustomer.Place = Convert.ToString(dataReader["Place"].ToString());
                        icustomer.Post = Convert.ToString(dataReader["Post"].ToString());
                        icustomer.Pin = Convert.ToString(dataReader["Pin"].ToString());
                        icustomer.LandMark = Convert.ToString(dataReader["LandMark"].ToString());
                        icustomer.Phone = Convert.ToString(dataReader["Phone"].ToString());
                        icustomer.Mobile = Convert.ToString(dataReader["Mobile"].ToString());
                        icustomer.Email = Convert.ToString(dataReader["Email"].ToString());
                        //icustomer.DateOfBirth = Convert.ToDateTime(dataReader["DateOfBirth"].ToString());
                        icustomer.DateOfBirth = dataReader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(dataReader["DateOfBirth"]): DateTime.MinValue;
                        icustomer.FK_Country = Convert.ToInt64(dataReader["FK_Country"].ToString());
                        icustomer.FK_State = Convert.ToInt64(dataReader["FK_State"].ToString());
                        icustomer.FK_District = Convert.ToInt64(dataReader["FK_District"].ToString());
                        icustomer.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                        icustomer.InactiveRemarks = Convert.ToString(dataReader["InactiveRemarks"].ToString());
                        icustomer.FK_CustomerGroup = Convert.ToInt64(dataReader["FK_CustomerGroup"].ToString());
                        icustomer.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        //icustomer.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                          icustomer.EnteredOn = dataReader["EnteredOn"] != DBNull.Value ? Convert.ToDateTime(dataReader["EnteredOn"]) : DateTime.MinValue;
                        icustomer.IsEmployee = Convert.ToBoolean(dataReader["IsEmployee"].ToString());
                        icustomer.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        icustomer.CreditDays = Convert.ToInt64(dataReader["CreditDays"].ToString());
                        icustomer.CreditLimit = Convert.ToDecimal(dataReader["CreditLimit"].ToString());
                        icustomer.CreditSale = Convert.ToBoolean(dataReader["CreditSale"].ToString());
                        icustomer.CardPrice = Convert.ToBoolean(dataReader["CardPrice"].ToString());
                        icustomer.Attachment = Convert.ToString(dataReader["Attachment"].ToString());
                        icustomer.DiscountPercentage = Convert.ToDecimal(dataReader["DiscountPercentage"].ToString());
                        icustomer.GSTTIN = Convert.ToString(dataReader["GSTTIN"].ToString());
                        icustomer.Idtype = Convert.ToInt64(dataReader["Idproof"].ToString());
                        icustomer.CardNumber = Convert.ToString(dataReader["CardNumber"].ToString());
                        icustomer.FK_Reason = Convert.ToInt64(dataReader["FK_Reason"].ToString());

                    }
                }
                return icustomer;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable SelectAllCustomer(ICustomer icustomer)
        {
            DataTable dtblCustomer = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Customer", DbType.Int64, icustomer.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icustomer.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icustomer.Name);
            db.AddInParameter(dbCommand, "@Mobile", DbType.String, icustomer.Mobile);
            db.AddInParameter(dbCommand, "@Status", DbType.String, icustomer.Status);
            
            try
            {
                dtblCustomer = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblCustomer;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable SelectAllCustomerVeiw(ICustomer icustomer)
        {
            DataTable dtblCustomer = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerSelectVeiw";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Customer", DbType.Int64, icustomer.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icustomer.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icustomer.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, icustomer.ShortName);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, icustomer.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, icustomer.PageSize);
            try
            {
                dtblCustomer = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblCustomer;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }


        public DataTable SelectAllDataForReport(ICustomer icustomer)
        {
            DataTable dtblCustomer = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerSelectForReport";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Customer", DbType.Int64, icustomer.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icustomer.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icustomer.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, icustomer.ShortName);

            try
            {
                dtblCustomer = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblCustomer;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public int DeleteCustomer(ICustomer icustomer)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Customer", DbType.Int64, icustomer.MasterId);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icustomer.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icustomer.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, icustomer.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }




        public DataTable SelectCustomerId(ICustomer icustomer)
        {



            DataTable dtblCustomer = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerSelectCId";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_CustomerGroup", DbType.Int64, icustomer.FK_CustomerGroup);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icustomer.FK_Client);
            db.AddInParameter(dbCommand, "@MasterID", DbType.Int32, icustomer.MasterId);


            try
            {
                dtblCustomer = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblCustomer;
            }
            catch (SqlException e)
            {
                throw e;
            }

        }
        public DataTable SelectCustCreditLimit(ICustomer icustomer)
        {



            DataTable dtblCustomer = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerSelectCreditLimit";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_CustomerGroup", DbType.Int64, icustomer.FK_CustomerGroup);

            try
            {
                dtblCustomer = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblCustomer;
            }
            catch (SqlException e)
            {
                throw e;
            }

        }
    }
}
