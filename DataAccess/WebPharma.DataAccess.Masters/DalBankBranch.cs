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
   public class DalBankBranch
    {
       public Int32 UpdateBankBranch(IBankBranch ibankbranch)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBankBranchUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ibankbranch.UserAction);
            db.AddInParameter(dbCommand, "@ID_BankBranch", DbType.Int64, ibankbranch.MasterId);
            db.AddInParameter(dbCommand, "@FK_Bank", DbType.Int64, ibankbranch.FK_Bank);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ibankbranch.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ibankbranch.ShortName);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, ibankbranch.FK_Country);
            db.AddInParameter(dbCommand, "@Address1", DbType.String, ibankbranch.Address1);
            db.AddInParameter(dbCommand, "@Address2", DbType.String, ibankbranch.Address2);
            db.AddInParameter(dbCommand, "@Address3", DbType.String, ibankbranch.Address3);
            db.AddInParameter(dbCommand, "@FK_AccountHead", DbType.Int64, ibankbranch.FK_AccountHead);
            db.AddInParameter(dbCommand, "@FK_AccountHeadSub", DbType.Int64, ibankbranch.FK_AccountHeadSub);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, ibankbranch.Active);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ibankbranch.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, ibankbranch.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ibankbranch.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
       public int DeleteBankBranch(IBankBranch ibankbranch)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBankBranchDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_BankBranch", DbType.Int64, ibankbranch.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ibankbranch.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ibankbranch.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
       public IBankBranch SelectBankBranch(IBankBranch ibankbranch)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBankBranchSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_BankBranch", DbType.Int64, ibankbranch.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ibankbranch.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ibankbranch.Name);
            db.AddInParameter(dbCommand, "@FK_Bank", DbType.Int64, ibankbranch.FK_Bank);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ibankbranch.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ibankbranch.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ibankbranch.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ibankbranch.FK_Bank = Convert.ToInt64(dataReader["FK_Bank"].ToString());
                        ibankbranch.ShortName = dataReader["ShortName"].ToString();
                        ibankbranch.Name = dataReader["Name"].ToString();
                        ibankbranch.FK_Country = Convert.ToInt64(dataReader["FK_Country"].ToString());
                        ibankbranch.Address1 = dataReader["Address1"].ToString();
                        ibankbranch.Address2 = dataReader["Address2"].ToString();
                        ibankbranch.Address3 = dataReader["Address3"].ToString();
                       
                        ibankbranch.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                        ibankbranch.FK_AccountHead = Convert.ToInt64(dataReader["FK_AccountHead"].ToString());
                        ibankbranch.FK_AccountHeadSub = Convert.ToInt64(dataReader["FK_AccountHeadSub"].ToString());
                        ibankbranch.AccountHead = dataReader["AccountHead"].ToString();
                        ibankbranch.AccountSubHead = dataReader["AccountHeadSub"].ToString();
                    }
                }
                return ibankbranch;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
       public DataTable SelectAllBankBranch(IBankBranch ibankbranch)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBankBranchSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_BankBranch", DbType.Int64, ibankbranch.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, ibankbranch.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, ibankbranch.Name);
            db.AddInParameter(dbCommand, "@FK_Bank", DbType.Int64, ibankbranch.FK_Bank);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, ibankbranch.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, ibankbranch.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ibankbranch.FK_Client);
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
