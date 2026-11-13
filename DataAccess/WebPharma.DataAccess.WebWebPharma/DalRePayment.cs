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
    public class DalRePayment
    {
        public Int32 UpdateRepayment(IRePayment irepayment)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerRePaymentUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, irepayment.UserAction);
            db.AddInParameter(dbCommand, "@ID_CustomerRePayment", DbType.Int64, irepayment.MasterId);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, irepayment.FK_Customer);
            db.AddInParameter(dbCommand, "@TransType", DbType.String, irepayment.TransType);
            db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, irepayment.Amount);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, irepayment.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, irepayment.UserCode);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, irepayment.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, irepayment.FK_Counter);
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
        public DataTable SelectAllRePayments(IRePayment irepayment)
        {
            DataTable dtblRePayment = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRePaymentSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_CustomerRePayment", DbType.Int64, irepayment.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, irepayment.FK_Client);
            db.AddInParameter(dbCommand, "@CustName", DbType.String, irepayment.CustName);
            db.AddInParameter(dbCommand, "@TransType", DbType.String, irepayment.TransType);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, irepayment.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, irepayment.PageSize);
            try
            {
                dtblRePayment = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblRePayment;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icolor, e);
                throw e;
            }
        }
        public IRePayment SelectRePayments(IRePayment irepayment)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proColorSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_CustomerRePayment", DbType.Int64, irepayment.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, irepayment.FK_Client);
            db.AddInParameter(dbCommand, "@CustName", DbType.String, irepayment.CustName);
            db.AddInParameter(dbCommand, "@TransType", DbType.String, irepayment.TransType);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, irepayment.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, irepayment.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        irepayment.FK_Customer = Convert.ToInt64(dataReader["FK_Customer"].ToString());
                        irepayment.CustName = Convert.ToString(dataReader["CustName"].ToString());
                        irepayment.TransType = Convert.ToString(dataReader["TransType"].ToString());
                        irepayment.Amount = Convert.ToDecimal(dataReader["Amount"].ToString());
                        irepayment.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        irepayment.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        irepayment.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        irepayment.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                        irepayment.FK_Counter = Convert.ToInt64(dataReader["FK_Counter"].ToString());
                    }
                }
                return irepayment;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icolor, e);
                throw e;
            }
        }
        public DataSet RepaymentReportData(long StatusId)
        {
            string sqlCommand = "";
            DbCommand dbCommand;
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            sqlCommand = "ProRptGetRepayment";
            dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_CustomerRePayment", DbType.Int64, StatusId);
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
                return ds;
            }
            catch (SqlException e)
            {
                throw;
            }

        }
    }
}



