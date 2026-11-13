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
   public class DalCountry
    {
        public Int32 UpdateCountry(ICountry icountry)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCountryUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, icountry.UserAction);
            db.AddInParameter(dbCommand, "@ID_Country", DbType.Int64, icountry.MasterId);
            db.AddInParameter(dbCommand, "@CountryName", DbType.String, icountry.CountryName);
            db.AddInParameter(dbCommand, "@CountryCode", DbType.String, icountry.CountryCode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icountry.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icountry.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public int DeleteCountry(ICountry icountry)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCountryDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Country", DbType.Int64, icountry.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icountry.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icountry.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, icountry.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public ICountry SelectCountry(ICountry icountry)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCountrySelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Country", DbType.Int64, icountry.MasterId);
            db.AddInParameter(dbCommand, "@CountryName", DbType.String, icountry.CountryName);
            db.AddInParameter(dbCommand, "@CountryCode", DbType.String, icountry.CountryCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icountry.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        icountry.CountryCode = dataReader["CountryCode"].ToString();
                        icountry.CountryName = dataReader["CountryName"].ToString();                       
                      
                    }
                }
                return icountry;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectAllCountry(ICountry icountry)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCountrySelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Country", DbType.Int64, icountry.MasterId);
            db.AddInParameter(dbCommand, "@CountryName", DbType.String, icountry.CountryName);
            db.AddInParameter(dbCommand, "@CountryCode", DbType.String, icountry.CountryCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icountry.FK_Client);
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
