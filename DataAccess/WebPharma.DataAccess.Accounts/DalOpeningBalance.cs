using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.Interface.Accounts;

namespace WebPharma.DataAccess.Accounts
{
    public class DalOpeningBalance
    {
        public DataTable FillFinyaer(IOpeningBalance iOpening)
        {
            DataTable dbOpening = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCmnfillFinYear";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iOpening.TransDate);
            db.AddInParameter(dbCommand, "@Fk_Store", DbType.Int32, iOpening.FK_Store);
            try
            {
                dbOpening = db.ExecuteDataSet(dbCommand).Tables[0];
                return dbOpening;
            }
            catch (SqlException e)
            {

                throw e;
            }
        }
        public DataTable Accountbalancegrid(IOpeningBalance iOpening)
        {
            DataTable dbOpening = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountHeadBalanceSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Fk_Store", DbType.Int64, iOpening.FK_Store);
            db.AddInParameter(dbCommand, "@FinalGroup", DbType.String, iOpening.FinalGroup);
            db.AddInParameter(dbCommand, "@FK_AccountGroup", DbType.Int32, iOpening.FK_AccountGroup);
            db.AddInParameter(dbCommand, "@FK_AccountSubGroup", DbType.Int32, iOpening.FK_AccountSubGroup);
            db.AddInParameter(dbCommand, "@FinYear", DbType.Int32, iOpening.FinYear);


            try
            {
                dbOpening = db.ExecuteDataSet(dbCommand).Tables[0];
                return dbOpening;
            }
            catch (SqlException e)
            {

                throw e;
            }
        }
        public Int32 UpdateOpeningbalance(IOpeningBalance iOpening)
        {

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountHeadBalanceUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iOpening.UserAction);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iOpening.UserCode);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iOpening.FK_Store);
            db.AddInParameter(dbCommand, "@FinalGroup", DbType.String, iOpening.FinalGroup);
            db.AddInParameter(dbCommand, "@FK_AccountGroup", DbType.Int64, iOpening.FK_AccountGroup);
            db.AddInParameter(dbCommand, "@FK_AccountSubGroup", DbType.Int64, iOpening.FK_AccountSubGroup);
            db.AddInParameter(dbCommand, "@XmlAccountBalance", DbType.Xml, iOpening.XmlAccountBalance);
            db.AddInParameter(dbCommand, "@FinYear", DbType.Int64, iOpening.FinYear);
            db.AddInParameter(dbCommand, "@BranchCode", DbType.Int64, iOpening.BranchCode);

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
    }
}
