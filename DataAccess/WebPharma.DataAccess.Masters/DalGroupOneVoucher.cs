using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.Interface.Masters;
using WebPharma.Interface.General;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace WebPharma.DataAccess.Masters
{
   public class DalGroupOneVoucher
    {
        public Int32 UpdateGroupOneVoucher(IGroupOneVoucher iGroupOneVoucher)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proGroupOneVoucherUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_GroupOneVoucher", DbType.Int64, iGroupOneVoucher.MasterId);
            db.AddInParameter(dbCommand, "@GroupOneName", DbType.String, iGroupOneVoucher.GroupOneName);
            db.AddInParameter(dbCommand, "@GroupOneCode", DbType.String, iGroupOneVoucher.GroupOneCode);
            db.AddInParameter(dbCommand, "@FK_AccountHeadSub", DbType.Int64, iGroupOneVoucher.FK_AccountHeadSub);
            db.AddInParameter(dbCommand, "@Description", DbType.String, iGroupOneVoucher.Description);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iGroupOneVoucher.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iGroupOneVoucher.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iGroupOneVoucher.FK_Client);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, iGroupOneVoucher.Active);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iGroupOneVoucher.UserAction);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }


        }

        public int DeleteGroupOneVoucher(IGroupOneVoucher iGroupOneVoucher)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proGroupOneVoucherDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_GroupOneVoucher", DbType.Int64, iGroupOneVoucher.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iGroupOneVoucher.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public IGroupOneVoucher SelectGroupOneVoucher(IGroupOneVoucher iGroupOneVoucher)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proGroupOneVoucherSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_GroupOneVoucher", DbType.Int64, iGroupOneVoucher.MasterId);
            db.AddInParameter(dbCommand, "@GroupOneName", DbType.String, iGroupOneVoucher.GroupOneName);
            db.AddInParameter(dbCommand, "@GroupOneCode", DbType.String, iGroupOneVoucher.GroupOneCode);
            db.AddInParameter(dbCommand, "@FK_AccountHeadSub", DbType.Int64, iGroupOneVoucher.FK_AccountHeadSub);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iGroupOneVoucher.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iGroupOneVoucher.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iGroupOneVoucher.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {

                        iGroupOneVoucher.GroupOneCode = dataReader["GroupOneCode"].ToString();
                        iGroupOneVoucher.GroupOneName = dataReader["GroupOneName"].ToString();
                        iGroupOneVoucher.FK_AccountHeadSub = Convert.ToInt64(dataReader["FK_AccountHeadSub"]);
                        iGroupOneVoucher.Description = dataReader["Description"].ToString();
                        iGroupOneVoucher.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                    }
                }
                return iGroupOneVoucher;
            }
            catch (SqlException e)
            {
                throw e;
            }




        }
        public DataTable SelectGroupOneVoucherExist(IGroupOneVoucher iGroupOneVoucher)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSelectGroupOneVoucherExist";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@GroupOneVoucherNo", DbType.String, iGroupOneVoucher.GroupOneVoucherNo);
            db.AddInParameter(dbCommand, "@FK_GroupOneVoucher", DbType.String, iGroupOneVoucher.FK_GroupOneVoucher);

            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iGroupOneVoucher.FK_Client);
         

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(idiscountsettings, e);
                throw e;
            }
        }

        public DataTable SelectALLGroupOneVoucher(IGroupOneVoucher iGroupOneVoucher)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proGroupOneVoucherSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_GroupOneVoucher", DbType.Int64, iGroupOneVoucher.MasterId);
            db.AddInParameter(dbCommand, "@GroupOneName", DbType.String, iGroupOneVoucher.GroupOneName);
            db.AddInParameter(dbCommand, "@GroupOneCode", DbType.String, iGroupOneVoucher.GroupOneCode);
            db.AddInParameter(dbCommand, "@FK_AccountHeadSub", DbType.Int64, iGroupOneVoucher.FK_AccountHeadSub);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iGroupOneVoucher.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iGroupOneVoucher.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iGroupOneVoucher.FK_Client);
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
