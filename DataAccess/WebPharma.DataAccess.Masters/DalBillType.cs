/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 14/02/2014
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			    OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
20-02-2019  Praveen MP      Add Card & Cheque in proBillTypeUpdate
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
    public class DalBillType
    {

        public Int32 UpdateBillType(IBillType iBillType)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBillTypeUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iBillType.UserAction);
            db.AddInParameter(dbCommand, "@ID_BillType", DbType.Int64, iBillType.MasterId);
            db.AddInParameter(dbCommand, "@TypeName", DbType.String, iBillType.TypeName);
            db.AddInParameter(dbCommand, "@BillType", DbType.Int32, iBillType.BillType);
            db.AddInParameter(dbCommand, "@FK_AccountHead", DbType.Int64, iBillType.FK_AccountHead);
            db.AddInParameter(dbCommand, "@FK_AccountHeadSub", DbType.Int64, iBillType.FK_AccountHeadSub);
            db.AddInParameter(dbCommand, "@GovtSuppliers", DbType.Boolean, iBillType.GovtSuppliers);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iBillType.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iBillType.UserCode);

            db.AddInParameter(dbCommand, "@FK_AccountReturn", DbType.Int64, iBillType.FK_AccountReturn);
            db.AddInParameter(dbCommand, "@FK_AccountSubReturn", DbType.Int64, iBillType.FK_AccountSubReturn);
          
            db.AddInParameter(dbCommand, "@Credit", DbType.Boolean, iBillType.Credit);
                      
            db.AddInParameter(dbCommand, "@FK_DiscountAccountHead", DbType.Int64, iBillType.FK_AccountHeadDiscount);
            db.AddInParameter(dbCommand, "@FK_DiscountAccountHeadSub", DbType.Int64, iBillType.FK_AccountHeadSubDiscount);
            db.AddInParameter(dbCommand, "@FK_RoundAccountHead", DbType.Int64, iBillType.FK_AccountHeadRounding);
            db.AddInParameter(dbCommand, "@FK_RoundtAccountHeadSub", DbType.Int64, iBillType.FK_AccountHeadSubRounding);
            db.AddInParameter(dbCommand, "@FK_CardAccountHead", DbType.Int64, iBillType.FK_AccountHeadCard);
            db.AddInParameter(dbCommand, "@FK_CardAccountHeadSub", DbType.Int64, iBillType.FK_AccountHeadSubCard);
            db.AddInParameter(dbCommand, "@FK_ChequeAccountHead", DbType.Int64, iBillType.FK_AccountHeadCheque);
            db.AddInParameter(dbCommand, "@FK_ChequeAccountHeadSub", DbType.Int64, iBillType.FK_AccountHeadSubCheque);

            db.AddInParameter(dbCommand, "@FK_AccountCredit", DbType.Int64, iBillType.FK_AccountCredit);
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

        public IBillType SelectBillType(IBillType iBillType)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBillTypeSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_BillType", DbType.Int64, iBillType.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iBillType.FK_Client);
            db.AddInParameter(dbCommand, "@TypeName", DbType.String, iBillType.TypeName);
           
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iBillType.TypeName = Convert.ToString(dataReader["TypeName"].ToString());
                        iBillType.BillType = Convert.ToInt32(dataReader["BillType"].ToString());
                        iBillType.FK_AccountHead = Convert.ToInt64(dataReader["FK_AccountHead"].ToString());
                        iBillType.FK_AccountHeadSub = Convert.ToInt64(dataReader["FK_AccountHeadSub"].ToString());
                        iBillType.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iBillType.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iBillType.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        iBillType.GovtSuppliers = Convert.ToBoolean(dataReader["GovtSuppliers"].ToString());
                      
                        iBillType.FK_AccountReturn = Convert.ToInt64(dataReader["FK_AccountReturn"].ToString());
                        iBillType.FK_AccountSubReturn = Convert.ToInt64(dataReader["FK_AccountSUBReturn"].ToString());
                      
                        iBillType.Credit = Convert.ToBoolean(dataReader["Credit"].ToString());
                       
                        iBillType.FK_AccountHeadRounding = Convert.ToInt64(dataReader["FK_AccountHeadRounding"].ToString());
                        iBillType.FK_AccountHeadSubRounding = Convert.ToInt64(dataReader["FK_AccountHeadSubRounding"].ToString());

                      
                        iBillType.FK_AccountHeadDiscount = Convert.ToInt64(dataReader["FK_AccountHeadDiscount"].ToString());
                        iBillType.FK_AccountHeadSubDiscount = Convert.ToInt64(dataReader["FK_AccountHeadSubDiscount"].ToString());
                        iBillType.FK_AccountCredit = Convert.ToInt64(dataReader["FK_AccountCredit"].ToString());

                        iBillType.FK_AccountHeadCard = Convert.ToInt64(dataReader["FK_AccountHeadCard"].ToString());
                        iBillType.FK_AccountHeadSubCard = Convert.ToInt64(dataReader["FK_AccountHeadSubCard"].ToString());
                        iBillType.FK_AccountHeadCheque = Convert.ToInt64(dataReader["FK_AccountHeadCheque"].ToString());
                        iBillType.FK_AccountHeadSubCheque = Convert.ToInt64(dataReader["FK_AccountHeadSubCheque"].ToString());
                    }
                }
                return iBillType;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iBillType, e);
                throw e;
            }
        }

        public DataTable SelectAllBillType(IBillType iBillType)
		{
			DataTable dtblBillType = new DataTable();
			  Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proBillTypeSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_BillType",DbType.Int64,iBillType.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,iBillType.FK_Client);
			db.AddInParameter(dbCommand, "@TypeName", DbType.String, iBillType.TypeName);
		
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

        public int DeleteBillType(IBillType iBillType)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBillTypeDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_BillType", DbType.Int64, iBillType.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iBillType.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iBillType.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, iBillType.FK_Reason);
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
