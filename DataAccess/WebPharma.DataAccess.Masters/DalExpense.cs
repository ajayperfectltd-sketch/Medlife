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
    public class DalExpense
    {

        public Int32 UpdateExpense(IExpense iexpense)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proExpenseUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iexpense.UserAction);
            db.AddInParameter(dbCommand, "@ID_Expense", DbType.Int64, iexpense.MasterId);
            db.AddInParameter(dbCommand, "@ExpName", DbType.String, iexpense.ExpName);
            db.AddInParameter(dbCommand, "@ExpShort", DbType.String, iexpense.ExpShort);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iexpense.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iexpense.UserCode);


            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iexpense, e);
                return 0;
            }
        }

        public IExpense SelectExpense(IExpense iexpense)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proExpenseSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Expense", DbType.Int64, iexpense.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iexpense.FK_Client);
            db.AddInParameter(dbCommand, "@ExpName", DbType.String, iexpense.ExpName);
            db.AddInParameter(dbCommand, "@ExpShort", DbType.String, iexpense.ExpShort);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iexpense.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iexpense.PageSize);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iexpense.ExpName = Convert.ToString(dataReader["ExpName"].ToString());
                        iexpense.ExpShort = Convert.ToString(dataReader["ExpShort"].ToString());
                        iexpense.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iexpense.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iexpense.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());

                    }
                }
                return iexpense;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iexpense, e);
                throw e;
            }
        }

        public DataTable SelectAllExpense(IExpense iexpense)
		{
			DataTable dtblExpense = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proExpenseSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Expense",DbType.Int64,iexpense.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,iexpense.FK_Client);
            db.AddInParameter(dbCommand, "@ExpName", DbType.String, iexpense.ExpName);
            db.AddInParameter(dbCommand, "@ExpShort", DbType.String, iexpense.ExpShort);
			db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iexpense.PageIndex);
			db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iexpense.PageSize);
			try
			{
				 dtblExpense = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblExpense;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(iexpense, e);
                throw e;
			}
		}

        public int DeleteExpense(IExpense iexpense)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proExpenseDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Expense", DbType.Int64, iexpense.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iexpense.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iexpense.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iexpense, e);
                throw e;
            }
        }

    }
}
