/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : ExpenseType Data Class
Author     : Praveen
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 19/05/2018
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
    public class DalExpenseType
    {

        public Int32 UpdateExpenseType(IExpenseType iExpenseType)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proExpenseTypeUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iExpenseType.UserAction);
            db.AddInParameter(dbCommand, "@ID_ExpenseType", DbType.Int64, iExpenseType.MasterId);
            db.AddInParameter(dbCommand, "@ExpenseType", DbType.String, iExpenseType.ExpenseType);
            db.AddInParameter(dbCommand, "@FK_AccountHead", DbType.Int64, iExpenseType.FK_AccountHead);
            db.AddInParameter(dbCommand, "@FK_AccountHeadSub", DbType.Int64, iExpenseType.FK_AccountHeadSub);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iExpenseType.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iExpenseType.UserCode);

            db.AddInParameter(dbCommand, "@EtPurchase", DbType.Boolean, iExpenseType.EtPurchase);
            db.AddInParameter(dbCommand, "@EtPurchaseReturn", DbType.Boolean, iExpenseType.EtPurchaseReturn);
            db.AddInParameter(dbCommand, "@EtSales", DbType.Boolean, iExpenseType.EtSales);
            db.AddInParameter(dbCommand, "@EtSalesReturn", DbType.Boolean, iExpenseType.EtSalesReturn);
            db.AddInParameter(dbCommand, "@EtAddorLess", DbType.Boolean, iExpenseType.EtAddorless);
         
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

        public IExpenseType SelectExpenseType(IExpenseType iExpenseType)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proExpenseTypeSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_ExpenseType", DbType.Int64, iExpenseType.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iExpenseType.FK_Client);
            db.AddInParameter(dbCommand, "@ExpenseType", DbType.String, iExpenseType.ExpenseType);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iExpenseType.ExpenseType = Convert.ToString(dataReader["ExpenseType"].ToString());
                        iExpenseType.FK_AccountHead = Convert.ToInt64(dataReader["FK_AccountHead"].ToString());
                        iExpenseType.FK_AccountHeadSub = Convert.ToInt64(dataReader["FK_AccountHeadSub"].ToString());
                        iExpenseType.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iExpenseType.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iExpenseType.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        iExpenseType.EtPurchase = Convert.ToBoolean(dataReader["EtPurchase"].ToString());
                        iExpenseType.EtPurchaseReturn = Convert.ToBoolean(dataReader["EtPurchaseReturn"].ToString());
                        iExpenseType.EtSales = Convert.ToBoolean(dataReader["EtSales"].ToString());
                        iExpenseType.EtSalesReturn = Convert.ToBoolean(dataReader["EtSalesReturn"].ToString());
                        iExpenseType.EtAddorless = Convert.ToBoolean(dataReader["EtAddorless"].ToString());
                        
                    }
                }
                return iExpenseType;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iBillType, e);
                throw e;
            }
        }

        public DataTable SelectAllExpenseType(IExpenseType iExpenseType)
        {
            DataTable dtblExpenseType = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proExpenseTypeSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_ExpenseType", DbType.Int64, iExpenseType.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iExpenseType.FK_Client);
            db.AddInParameter(dbCommand, "@ExpenseType", DbType.String, iExpenseType.ExpenseType);

            try
            {
                dtblExpenseType = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblExpenseType;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iBillType, e);
                throw e;
            }
        }

        public int DeleteExpenseType(IExpenseType iExpenseType)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proExpenseTypeDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_ExpenseType", DbType.Int64, iExpenseType.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iExpenseType.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iExpenseType.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, iExpenseType.FK_Reason);
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
