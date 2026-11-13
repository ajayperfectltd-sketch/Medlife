using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using WebPharma.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.Interface.Masters;
using WebPharma.Interface.General;

namespace WebPharma.DataAccess.Masters
{
   public class DalBank
    {
        public Int32 UpdateBank(IBank ibank)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBankUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ibank.UserAction);
            db.AddInParameter(dbCommand, "@ID_Bank", DbType.Int64, ibank.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ibank.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ibank.ShortName);
            db.AddInParameter(dbCommand, "@Address1", DbType.String, ibank.Address1);
            db.AddInParameter(dbCommand, "@Address2", DbType.String, ibank.Address2);
            db.AddInParameter(dbCommand, "@Address3", DbType.String, ibank.Address3);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, ibank.Active);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ibank.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, ibank.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ibank.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public int DeleteBank(IBank ibank)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBankDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Bank", DbType.Int64, ibank.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ibank.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ibank.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public IBank SelectBank(IBank ibank)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBankSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Bank", DbType.Int64, ibank.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ibank.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ibank.Name);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ibank.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ibank.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ibank.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ibank.ShortName = dataReader["ShortName"].ToString();
                        ibank.Name = dataReader["Name"].ToString();
                        ibank.Address1 = dataReader["Address1"].ToString();
                        ibank.Address2 = dataReader["Address2"].ToString();
                        ibank.Address3 = dataReader["Address3"].ToString();
                        ibank.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                    }
                }
                return ibank;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectAllBank(IBank ibank)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBankSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Bank", DbType.Int64, ibank.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ibank.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ibank.Name);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ibank.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ibank.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ibank.FK_Client);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}
