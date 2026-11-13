/*----------------------------------- DataAccess Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     :bibin  tc
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
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
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.DataAccess;
using WebPharma.Interface.Accounts;

using System.Collections;



namespace WebPharma.DataAccess.Accounts
{
   
    public class DalAccountHeadSubTransaction
    {
        public Int64 UpdateAccountHeadSubTransMultiple(IAccountHeadSubTransactions iaccountheadsubtransaction)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");          
            string sqlCommand = "proAccountHeadSubTransMultipleUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iaccountheadsubtransaction.UserAction);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iaccountheadsubtransaction.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iaccountheadsubtransaction.UserCode);
            db.AddInParameter(dbCommand, "@TransDate", DbType.Date, iaccountheadsubtransaction.TransDate);
            db.AddInParameter(dbCommand, "@EffectDate", DbType.Date, iaccountheadsubtransaction.EffectDate);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iaccountheadsubtransaction.FK_Store);
            db.AddInParameter(dbCommand, "@XmlAccountTransMultiple", DbType.String, iaccountheadsubtransaction.XmlAccountTransMultiple);        
            try
            {
                return Convert.ToInt64(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
            
                return 0;
            }
        }

        public DataTable ValidateExcelData(IAccountHeadSubTransactions iaccountheadsubtransaction)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountHeadMultipleTransValidateExcel";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          //  db.AddInParameter(dbCommand, "@SubModule", DbType.String, iRecoveryAmendment.SubModule);
            db.AddInParameter(dbCommand, "@XmlExcelImport", DbType.Xml, iaccountheadsubtransaction.XmlExcelImport);
            //db.AddInParameter(dbCommand, "@FK_RecoveryType", DbType.Int64, iRecoveryAmendment.FK_RecoveryType);
           // db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iRecoveryAmendment.TransDate);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public DataTable SelectAccountHeadSubMutipleTransaction(IAccountHeadSubTransactions iaccountheadsubtransaction)
        {

            Database db = DatabaseFactory.CreateDatabase("WebPharma"); 
            string sqlCommand = "proAccountHeadMutipleSubTransSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@groupid", DbType.Int64, iaccountheadsubtransaction.groupid);
            DataTable dt = new DataTable();
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    dt.Load(dataReader);
                    return dt;
                }
            }
            catch (SqlException e)
            {
              
                throw e;
            }
        }
        public Int32 UpdateAccountHeadSubTransaction(IAccountHeadSubTransactions iaccountheadsubtransaction)
        {
           
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountHeadSubTransactionUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iaccountheadsubtransaction.UserAction);
          // db.AddInParameter(dbCommand, "@FK_AccountHead", DbType.Int64, iaccountheadsubtransaction.FK_AccountHead);
         //  db.AddInParameter(dbCommand, "@FK_AccountHeadSub", DbType.Int64, iaccountheadsubtransaction.FK_AccountHeadSub);
            db.AddInParameter(dbCommand, "@AhCode", DbType.String, iaccountheadsubtransaction.AhCode);
            db.AddInParameter(dbCommand, "@AhsCode", DbType.String, iaccountheadsubtransaction.AhsCode);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iaccountheadsubtransaction.TransDate);
            db.AddInParameter(dbCommand, "@EffectDate", DbType.DateTime, iaccountheadsubtransaction.EffectDate);
            db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, iaccountheadsubtransaction.Amount);
            db.AddInParameter(dbCommand, "@TransType", DbType.String, iaccountheadsubtransaction.TransType);
            db.AddInParameter(dbCommand, "@ChequeNo", DbType.String, iaccountheadsubtransaction.ChequeNo);
            db.AddInParameter(dbCommand, "@ChequeDate", DbType.DateTime, iaccountheadsubtransaction.ChequeDate);
            db.AddInParameter(dbCommand, "@Party", DbType.String, iaccountheadsubtransaction.Party);
            //db.AddInParameter(dbCommand, "@CancelledUser", DbType.String, iaccountheadsubtransaction.CancelledUser);
            //db.AddInParameter(dbCommand, "@CancelledOn", DbType.DateTime, iaccountheadsubtransaction.CancelledOn);
           // db.AddInParameter(dbCommand, "@Cancelled", DbType.Int64, iaccountheadsubtransaction.Cancelled);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iaccountheadsubtransaction.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iaccountheadsubtransaction.UserCode);
            db.AddInParameter(dbCommand, "@Narration", DbType.String, iaccountheadsubtransaction.Narration);
            //db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int16, iaccountheadsubtransaction.MachineCode);
           // db.AddInParameter(dbCommand, "@OfflineEntry", DbType.Boolean, iaccountheadsubtransaction.OfflineEntry);
            //db.AddInParameter(dbCommand, "@BranchCode", DbType.Int16, iaccountheadsubtransaction.BranchCode);
            //db.AddInParameter(dbCommand, "@InterBranch", DbType.Boolean, iaccountheadsubtransaction.InterBranch);
          //  db.AddInParameter(dbCommand, "@AccountNo", DbType.String, iaccountheadsubtransaction.AccountNo);
            //db.AddInParameter(dbCommand, "@AccountHolderName", DbType.String, iaccountheadsubtransaction.AccountHolderName);
            //db.AddInParameter(dbCommand, "@ID_AccountHeadSubTransaction", DbType.Int64, iaccountheadsubtransaction.MasterId);           
           // db.AddInParameter(dbCommand, "@TransMode", DbType.String, iaccountheadsubtransaction.TransMode);
            //db.AddInParameter(dbCommand, "@Transfer", DbType.Boolean, iaccountheadsubtransaction.Transfer);          
           // db.AddInParameter(dbCommand, "@VoucherNo", DbType.Int64, iaccountheadsubtransaction.VoucherNo);                 
          //  db.AddInParameter(dbCommand, "@Remarks", DbType.String, iaccountheadsubtransaction.Remarks);
            //db.AddInParameter(dbCommand, "@FK_Transaction", DbType.Int64, iaccountheadsubtransaction.FK_Transaction);          
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iaccountheadsubtransaction.FK_Store);
            //db.AddInParameter(dbCommand, "@FK_FinYear", DbType.Int64, iaccountheadsubtransaction.FK_FinYear);          

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iaccountheadsubtransaction, e);
                return 0;
            }
        }

        public IAccountHeadSubTransactions SelectAccountHeadSubTransaction(IAccountHeadSubTransactions iaccountheadsubtransaction)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proIncomeExpenseSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountHeadSubTransaction", DbType.Int64, iaccountheadsubtransaction.MasterId);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iaccountheadsubtransaction.UserAction);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iaccountheadsubtransaction.FK_Client);
            db.AddInParameter(dbCommand, "@AhCode", DbType.String, iaccountheadsubtransaction.AhCode);
            db.AddInParameter(dbCommand, "@AhsCode", DbType.String, iaccountheadsubtransaction.AhsCode);
            //db.AddInParameter(dbCommand, "@Code", DbType.String,iaccountheadsubtransaction.Code);
            //db.AddInParameter(dbCommand, "@Name", DbType.String, iaccountheadsubtransaction.Name);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iaccountheadsubtransaction.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iaccountheadsubtransaction.PageSize);
            //db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, iaccountheadsubtransaction.FromDate);
            //db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, iaccountheadsubtransaction.ToDate);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iaccountheadsubtransaction.FK_AccountHead = Convert.ToInt64(dataReader["FK_AccountHead"].ToString());
                        iaccountheadsubtransaction.AhCode = Convert.ToString(dataReader["AhCode"].ToString());
                        iaccountheadsubtransaction.FK_AccountHeadSub = Convert.ToInt64(dataReader["FK_AccountHeadSub"].ToString());                      
                        iaccountheadsubtransaction.AhsCode = Convert.ToString(dataReader["AhsCode"].ToString());
                        iaccountheadsubtransaction.TransDate = Convert.ToDateTime(dataReader["TransDate"].ToString());
                        object TransDates = dataReader["TransDate"];
                        iaccountheadsubtransaction.TransDate = (TransDates == System.DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(TransDates);
                        object EffectDateS = dataReader["EffectDate"];
                        iaccountheadsubtransaction.EffectDate = (EffectDateS == System.DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(EffectDateS);                   
                        iaccountheadsubtransaction.Amount = Convert.ToDecimal(dataReader["Amount"].ToString());
                        iaccountheadsubtransaction.TransType = Convert.ToString(dataReader["TransType"].ToString());
                        iaccountheadsubtransaction.ChequeNo = Convert.ToString(dataReader["ChequeNo"].ToString());
                        object ChequeDates = dataReader["ChequeDate"];
                        iaccountheadsubtransaction.ChequeDate = (ChequeDates == System.DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(ChequeDates);
                        iaccountheadsubtransaction.Party = Convert.ToString(dataReader["Party"].ToString());
                        iaccountheadsubtransaction.Narration = Convert.ToString(dataReader["Narration"].ToString());
                        iaccountheadsubtransaction.groupid = Convert.ToInt64(dataReader["groupid"].ToString());
                       // iaccountheadsubtransaction.headName = Convert.ToString(dataReader["HeadName"].ToString());
                       // iaccountheadsubtransaction.Subheadname = Convert.ToString(dataReader["HeadSubName"].ToString());
                        //object ChequeDateS = dataReader["ChequeDate"];
                        //iaccountheadsubtransaction.ChequeDate = (ChequeDateS == System.DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(ChequeDateS);
                       // iaccountheadsubtransaction.ChequeDate = Convert.ToDateTime(dataReader["ChequeDate"].ToString());                    
                       // iaccountheadsubtransaction.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                       // iaccountheadsubtransaction.TransMode = Convert.ToString(dataReader["TransMode"].ToString());
                        //iaccountheadsubtransaction.Transfer = Convert.ToBoolean(dataReader["Transfer"].ToString());                       
                        //iaccountheadsubtransaction.VoucherNo = Convert.ToInt64(dataReader["VoucherNo"].ToString());                       
                        //iaccountheadsubtransaction.Remarks = Convert.ToString(dataReader["Remarks"].ToString());
                        //iaccountheadsubtransaction.FK_Transaction = Convert.ToInt64(dataReader["FK_Transaction"].ToString());
                        //iaccountheadsubtransaction.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        //iaccountheadsubtransaction.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        //iaccountheadsubtransaction.Cancelled = Convert.ToBoolean(dataReader["Cancelled"].ToString());
                        //iaccountheadsubtransaction.CancelledUser = Convert.ToString(dataReader["CancelledUser"].ToString());
                        //iaccountheadsubtransaction.CancelledOn = Convert.ToDateTime(dataReader["CancelledOn"].ToString());
                        //iaccountheadsubtransaction.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                        //iaccountheadsubtransaction.FK_FinYear = Convert.ToInt64(dataReader["FK_FinYear"].ToString());
                       

                    }
                }
                return iaccountheadsubtransaction;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iaccountheadsubtransaction, e);
                throw e;
            }
        }

        public DataTable SelectAllAccountHeadSubTransaction(IAccountHeadSubTransactions iaccountheadsubtransaction)
        {
            DataTable dtblAccountHeadSubTransaction = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proIncomeExpenseSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountHeadSubTransaction", DbType.Int64, iaccountheadsubtransaction.MasterId);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iaccountheadsubtransaction.UserAction);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iaccountheadsubtransaction.FK_Client);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iaccountheadsubtransaction.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iaccountheadsubtransaction.PageSize);
            db.AddInParameter(dbCommand, "@AhCode", DbType.String, iaccountheadsubtransaction.AhCode);
            db.AddInParameter(dbCommand, "@AhsCode", DbType.String, iaccountheadsubtransaction.AhsCode);
            //db.AddInParameter(dbCommand, "@EntryType", DbType.Int16, iaccountheadsubtransaction.EntryType);
            //db.AddInParameter(dbCommand, "@VoucherNo", DbType.Int64, iaccountheadsubtransaction.VoucherNo);
            //db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, iaccountheadsubtransaction.FromDate);
            //db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, iaccountheadsubtransaction.ToDate);
            try
            {
                dtblAccountHeadSubTransaction = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblAccountHeadSubTransaction;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iaccountheadsubtransaction, e);
                throw e;
            }
        }
        
        
        public int DeleteAccountHeadSubTransaction(IAccountHeadSubTransactions iaccountheadsubtransaction)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proIncomeExpenseDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountHeadSubTransaction", DbType.Int64, iaccountheadsubtransaction.MasterId);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iaccountheadsubtransaction.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iaccountheadsubtransaction.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {

                throw e;
            }
        }

        public ArrayList GetAccountHeadSubBalance(IAccountHeadSubTransactions iaccountheadsubtransaction)
        {
            ArrayList arlist = new ArrayList();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountHeadSubBalance";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@AhCode", DbType.String, iaccountheadsubtransaction.AhCode);
            db.AddInParameter(dbCommand, "@AhsCode", DbType.String, iaccountheadsubtransaction.AhsCode);
            try
            {
                IDataReader datareader = db.ExecuteReader(dbCommand);
                while (datareader.Read())
                {
                    for (int i = 0; i < datareader.FieldCount; i++)
                    {
                        arlist.Add(datareader.GetValue(i));
                    }
                }
                datareader = null;
            }
            catch (SqlException e)
            {
                throw e;
            }
            return arlist;
        }

        public IAccountHeadSubTransactions SelectBalanceAmount(IAccountHeadSubTransactions iaccountheadsubtransactions)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCheckBalanceAmount";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Party", DbType.Int64, iaccountheadsubtransactions.FK_Party);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iaccountheadsubtransactions.FK_Client);
            db.AddInParameter(dbCommand, "@SubID", DbType.String, iaccountheadsubtransactions.SubId);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iaccountheadsubtransactions.BalanceAmount = Convert.ToDecimal(dataReader["CustBalanceAmount"].ToString());
                    }
                }
                return iaccountheadsubtransactions;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public decimal OustandingBalance(IAccountTransaction iaccounttransaction)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCalcOutstandingBalanceSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, iaccounttransaction.CustomerNo);
            try
            {
                return Convert.ToDecimal(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {

                throw e;
            }
        }
        public decimal GetBalance(IAccountHeadSubTransactions IAccountHeadSubTransactions)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountGetBalance";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@AhCode", DbType.String, IAccountHeadSubTransactions.AhCode);
            db.AddInParameter(dbCommand, "@AhsCode", DbType.String, IAccountHeadSubTransactions.AhsCode);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int32, IAccountHeadSubTransactions.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, IAccountHeadSubTransactions.FK_Client);
            try
            {
                return Convert.ToDecimal(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {

                throw e;
            }
        }
 
        public long DeleteMultipleAccountHeadSubTransaction(IAccountHeadSubTransactions iaccountheadsubtransaction)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMultipleAccontheadTransactionDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@groupid", DbType.Int64, iaccountheadsubtransaction.groupid);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iaccountheadsubtransaction.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iaccountheadsubtransaction.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {

                throw e;
            }
        }



        //public decimal GetBalance(global::WebPharma.Business.Accounts.BlAccountHeadSubTransactions globalWebPharmaBusinessAccountsBlAccountHeadSubTransactions)
        //{
        //    throw new NotImplementedException();
        //}

        //public decimal GetBalanceAccounts(global::WebPharma.Business.Accounts.BlAccountHeadSubTransactions globalWebPharmaBusinessAccountsBlAccountHeadSubTransactions)
        //{
        //    throw new NotImplementedException();
        //}

        //public decimal GetBalanceAccounts(global::WebPharma.Business.Accounts.BlAccountHeadSubTransactions globalWebPharmaBusinessAccountsBlAccountHeadSubTransactions)
        //{
        //    throw new NotImplementedException();
        //}

        //public decimal GetBalanceAccounts(global::WebPharma.Business.Accounts.BlAccountHeadSubTransactions globalWebPharmaBusinessAccountsBlAccountHeadSubTransactions)
        //{
        //    throw new NotImplementedException();
        //}
    }
    public class DalAccountTransaction
    {
        public Int32 UpdateAccountTransaction(IAccountTransaction iaccounttransaction)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountPettycash";//"proAccountTransactionUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iaccounttransaction.UserAction);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int16, iaccounttransaction.MachineCode);
            db.AddInParameter(dbCommand, "@OfflineEntry", DbType.Boolean, iaccounttransaction.OfflineEntry);
            db.AddInParameter(dbCommand, "@BranchCode", DbType.Int16, iaccounttransaction.BranchCode);
            db.AddInParameter(dbCommand, "@InterBranch", DbType.Boolean, iaccounttransaction.InterBranch);
            db.AddInParameter(dbCommand, "@TransType", DbType.String, iaccounttransaction.TransType);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iaccounttransaction.TransDate);
            db.AddInParameter(dbCommand, "@VoucherNo", DbType.String, iaccounttransaction.VoucherNo);
            db.AddInParameter(dbCommand, "@BankTransType", DbType.Int16, iaccounttransaction.BankTransType);
            db.AddInParameter(dbCommand, "@Bank", DbType.Int64, iaccounttransaction.Bank);
            db.AddInParameter(dbCommand, "@BankBranch", DbType.Int64, iaccounttransaction.BankBranch);
            db.AddInParameter(dbCommand, "@ChequeNo", DbType.String, iaccounttransaction.ChequeNo);
            db.AddInParameter(dbCommand, "@ChequeDate", DbType.DateTime, iaccounttransaction.ChequeDate);
            db.AddInParameter(dbCommand, "@NetBankAccount", DbType.String, iaccounttransaction.NetBankAccount);
            db.AddInParameter(dbCommand, "@CreditAccount", DbType.Int16, iaccounttransaction.CreditAccount);
            db.AddInParameter(dbCommand, "@InvoiceNo", DbType.Int64, iaccounttransaction.InvoiceNo);
            db.AddInParameter(dbCommand, "@CustomerNo", DbType.Int64, iaccounttransaction.CustomerNo);
            db.AddInParameter(dbCommand, "@AccountHead", DbType.Int64, iaccounttransaction.AccountHead);
            db.AddInParameter(dbCommand, "@AccountHeadSub", DbType.Int64, iaccounttransaction.AccountHeadSub);
            db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, iaccounttransaction.Amount);
            db.AddInParameter(dbCommand, "@Remarks", DbType.String, iaccounttransaction.Remarks);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iaccounttransaction.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iaccounttransaction.UserCode);
            db.AddInParameter(dbCommand, "@XmlInvoiceDetails", DbType.Xml, iaccounttransaction.XmlInvoiceDetails);
            db.AddInParameter(dbCommand, "@PaymentMode", DbType.Byte, iaccounttransaction.PaymentMode);
            db.AddInParameter(dbCommand, "@CardNo", DbType.String, iaccounttransaction.CardNo);
            db.AddInParameter(dbCommand, "@Commission", DbType.Decimal, iaccounttransaction.Commission);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iaccounttransaction, e);
                return 0;
            }
        }
        
        public IAccountTransaction SelectAccountTransaction(IAccountTransaction iaccounttransaction)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proIncomeExpenseSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountHeadSubTransaction", DbType.Int64, iaccounttransaction.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iaccounttransaction.FK_Client);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iaccounttransaction.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iaccounttransaction.PageSize);
            
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iaccounttransaction.TransType = Convert.ToString(dataReader["TransType"].ToString());
                        iaccounttransaction.TransDate = Convert.ToDateTime(dataReader["TransDate"].ToString());
                        iaccounttransaction.VoucherNo = Convert.ToInt64(dataReader["VoucherNo"].ToString());
                       // iaccounttransaction.BankTransType = Convert.ToInt16(dataReader["BankTransType"].ToString());
                        iaccounttransaction.Bank = Convert.ToInt64(dataReader["Bank"].ToString());
                        iaccounttransaction.BankBranch = Convert.ToInt64(dataReader["BankBranch"].ToString());
                        iaccounttransaction.ChequeNo = Convert.ToString(dataReader["ChequeNo"].ToString());
                        iaccounttransaction.ChequeDate = Convert.ToDateTime(dataReader["ChequeDate"].ToString());
                        iaccounttransaction.NetBankAccount = Convert.ToString(dataReader["NetBankAccount"].ToString());
                        //iaccounttransaction.CreditAccount = Convert.ToInt16(dataReader["CreditAccount"].ToString());
                        //iaccounttransaction.InvoiceNo = Convert.ToInt16(dataReader["InvoiceNo"].ToString());
                        //iaccounttransaction.CustomerNo = Convert.ToInt16(dataReader["CustomerNo"].ToString());
                        iaccounttransaction.AccountHead = Convert.ToInt16(dataReader["AccountHead"].ToString());
                        iaccounttransaction.AccountHeadSub = Convert.ToInt16(dataReader["AccountHeadSub"].ToString());
                        iaccounttransaction.Amount = Convert.ToDecimal(dataReader["Amount"].ToString());
                        iaccounttransaction.Remarks = Convert.ToString(dataReader["Remarks"].ToString());
                        //iaccounttransaction.Commission = Convert.ToDecimal(dataReader["Commission"].ToString());
                    }
                }
                return iaccounttransaction;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iaccounttransaction, e);
                throw e;
            }
        }

       

        public long UpdateAccountHeadSubTransaction(IAccountTransaction iaccounttransaction)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountTransactionUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iaccounttransaction.UserAction);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int16, iaccounttransaction.MachineCode);
            db.AddInParameter(dbCommand, "@OfflineEntry", DbType.Boolean, iaccounttransaction.OfflineEntry);
            db.AddInParameter(dbCommand, "@BranchCode", DbType.Int16, iaccounttransaction.BranchCode);
            db.AddInParameter(dbCommand, "@InterBranch", DbType.Boolean, iaccounttransaction.InterBranch);
            db.AddInParameter(dbCommand, "@TransType", DbType.String, iaccounttransaction.TransType);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iaccounttransaction.TransDate);
            db.AddInParameter(dbCommand, "@VoucherNo", DbType.String, iaccounttransaction.VoucherNo);
            db.AddInParameter(dbCommand, "@BankTransType", DbType.Int16, iaccounttransaction.BankTransType);
            db.AddInParameter(dbCommand, "@Bank", DbType.Int64, iaccounttransaction.Bank);
            db.AddInParameter(dbCommand, "@BankBranch", DbType.Int64, iaccounttransaction.BankBranch);
            db.AddInParameter(dbCommand, "@ChequeNo", DbType.String, iaccounttransaction.ChequeNo);
            db.AddInParameter(dbCommand, "@ChequeDate", DbType.DateTime, iaccounttransaction.ChequeDate);
            db.AddInParameter(dbCommand, "@NetBankAccount", DbType.String, iaccounttransaction.NetBankAccount);
            db.AddInParameter(dbCommand, "@CreditAccount", DbType.Int16, iaccounttransaction.CreditAccount);
            db.AddInParameter(dbCommand, "@InvoiceNo", DbType.Int64, iaccounttransaction.InvoiceNo);
            db.AddInParameter(dbCommand, "@CustomerNo", DbType.Int64, iaccounttransaction.CustomerNo);
            db.AddInParameter(dbCommand, "@AccountHead", DbType.Int64, iaccounttransaction.AccountHead);
            db.AddInParameter(dbCommand, "@AccountHeadSub", DbType.Int64, iaccounttransaction.AccountHeadSub);
            db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, iaccounttransaction.Amount);
            db.AddInParameter(dbCommand, "@Remarks", DbType.String, iaccounttransaction.Remarks);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iaccounttransaction.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iaccounttransaction.UserCode);
            db.AddInParameter(dbCommand, "@XmlInvoiceDetails", DbType.Xml, iaccounttransaction.XmlInvoiceDetails);
            db.AddInParameter(dbCommand, "@PaymentMode", DbType.Byte, iaccounttransaction.PaymentMode);
            db.AddInParameter(dbCommand, "@CardNo", DbType.String, iaccounttransaction.CardNo);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iaccounttransaction, e);
                return 0;
            }
        }
    }
}
