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





namespace WebPharma.DataAccess.Masters
{
    public class DalCategoryFirst
    {

        public Int32 UpdateCategoryFirst(ICategoryFirst icategoryfirst)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCategoryFirstUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, icategoryfirst.UserAction);
            db.AddInParameter(dbCommand, "@ID_Category", DbType.Int64, icategoryfirst.MasterId);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, icategoryfirst.Shortname);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icategoryfirst.Name);
            db.AddInParameter(dbCommand, "@Discount", DbType.Decimal, icategoryfirst.Discount);
            db.AddInParameter(dbCommand, "@Profit", DbType.Decimal, icategoryfirst.Profit);
            db.AddInParameter(dbCommand, "@Commision", DbType.Decimal, icategoryfirst.Commission);
            db.AddInParameter(dbCommand, "@ExpairyDays", DbType.Int32, icategoryfirst.ExpiryDays);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icategoryfirst.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icategoryfirst.UserCode);
            db.AddInParameter(dbCommand, "@NonMedicine", DbType.String, icategoryfirst.NonMedicine);
           
            

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(icategoryfirst, e);
                return 0;
            }
        }

        public ICategoryFirst SelectCategoryFirst(ICategoryFirst icategoryfirst)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCategoryFirstSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Category", DbType.Int64, icategoryfirst.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icategoryfirst.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icategoryfirst.Name);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, icategoryfirst.Shortname);
           
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        icategoryfirst.Shortname = Convert.ToString(dataReader["Shortname"].ToString());
                        icategoryfirst.Name = Convert.ToString(dataReader["CategoryName"].ToString());
                        icategoryfirst.Profit = Convert.ToDecimal(dataReader["Profit"].ToString());
                        icategoryfirst.Discount = Convert.ToDecimal(dataReader["Discount"].ToString());
                        icategoryfirst.Commission = Convert.ToDecimal(dataReader["Commision"].ToString());
                        icategoryfirst.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        icategoryfirst.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        icategoryfirst.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        icategoryfirst.ExpiryDays = Convert.ToInt32(dataReader["ExpairyDays"].ToString());
                        icategoryfirst.NonMedicine = Convert.ToBoolean(dataReader["NonMedicine"].ToString());
                    }
                }
                return icategoryfirst;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategoryfirst, e);
                throw e;
            }
        }

        public DataTable SelectAllCategoryFirst(ICategoryFirst icategoryfirst)
        {
            DataTable dtblCategoryFirst = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCategoryFirstSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Category", DbType.Int64, icategoryfirst.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icategoryfirst.FK_Client);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, icategoryfirst.Shortname);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icategoryfirst.Name);
          
            try
            {
                dtblCategoryFirst = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblCategoryFirst;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategoryfirst, e);
                throw e;
            }
        }

        public int DeleteCategoryFirst(ICategoryFirst icategoryfirst)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCategoryFirstDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Category", DbType.Int64, icategoryfirst.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, icategoryfirst.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, icategoryfirst.FK_Reason);
            ////db.AddInParameter(dbCommand, "@TransMode", DbType.String, icategoryfirst.TransMode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icategoryfirst.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategoryfirst, e);
                throw e;
            }
        }

    }
}
