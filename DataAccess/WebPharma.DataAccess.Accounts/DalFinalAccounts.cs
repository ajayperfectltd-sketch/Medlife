using Inventory.Interface.Accounts;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Accounts
{
    public class DalFinalAccounts
    {
        public DataTable SelectFinalAccounts(IFinalAccounts iFinalAccounts)
        {
            DataTable dbiFinalAccounts = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProFinalAccountProcess";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iFinalAccounts.FK_Store);
            db.AddInParameter(dbCommand, "@Mode", DbType.String, iFinalAccounts.Mode);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iFinalAccounts.TransDate);
            db.AddInParameter(dbCommand, "@ID_FinalAccount", DbType.Int64, iFinalAccounts.ID_FinalAccount);
            db.AddInParameter(dbCommand, "@Criteria", DbType.Int64, iFinalAccounts.Criteria);
            try
            {
                dbiFinalAccounts = db.ExecuteDataSet(dbCommand).Tables[0];
                return dbiFinalAccounts;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iaccountheadsubtransaction, e);
                throw e;
            }
        }
        public Int32 FinalAccountUpdate(IFinalAccounts iFinalAccounts)
        {

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProFinalAccountUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iFinalAccounts.TransDate);
            db.AddInParameter(dbCommand, "@Mode", DbType.String, iFinalAccounts.Mode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iFinalAccounts.UserCode);
            // db.AddInParameter(dbCommand, "@FinalAccount", DbType.Object, iFinalAccounts.dtFinalAccount);

            dbCommand.Parameters.Add(new SqlParameter("@FinalAccount", iFinalAccounts.dtFinalAccount) { SqlDbType = SqlDbType.Structured });

            //    db.AddInParameter(dbCommand, "@FinalAccount", SqlDbType.Structured, iFinalAccounts.dtFinalAccount);
            db.AddInParameter(dbCommand, "@Fk_Store", DbType.Int64, iFinalAccounts.FK_Store);
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
    }
}
