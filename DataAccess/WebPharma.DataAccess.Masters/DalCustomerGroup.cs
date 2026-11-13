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
    public class DalCustomerGroup
    {
        public Int32 UpdateCustomerGroup(ICustomerGroup icustomergroup)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerGroupUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, icustomergroup.UserAction);
            db.AddInParameter(dbCommand, "@ID_CustomerGroup", DbType.Int64, icustomergroup.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icustomergroup.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, icustomergroup.ShortName);
            db.AddInParameter(dbCommand, "@CreditLimit", DbType.Decimal, icustomergroup.CreditLimit);
            db.AddInParameter(dbCommand, "@CreditDays", DbType.Int32, icustomergroup.CreditDays);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, icustomergroup.Active);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icustomergroup.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, icustomergroup.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icustomergroup.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public int DeleteCustomerGroup(ICustomerGroup icustomergroup)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerGroupDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_CustomerGroup", DbType.Int64, icustomergroup.MasterId);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, icustomergroup.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icustomergroup.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, icustomergroup.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public ICustomerGroup SelectCustomerGroup(ICustomerGroup icustomergroup)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerGroupSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_CustomerGroup", DbType.Int64, icustomergroup.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, icustomergroup.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icustomergroup.Name);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icustomergroup.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        icustomergroup.ShortName = dataReader["ShortName"].ToString();
                        icustomergroup.Name = dataReader["CustGroupName"].ToString();
                        icustomergroup.CreditLimit = Convert.ToDecimal(dataReader["CreditLimit"].ToString());
                        icustomergroup.CreditDays=Convert.ToInt32(dataReader["CreditDays"].ToString());                
                        icustomergroup.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                    }
                }
                return icustomergroup;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectAllCustomerGroup(ICustomerGroup icustomergroup)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCustomerGroupSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_CustomerGroup", DbType.Int64, icustomergroup.MasterId);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, icustomergroup.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, icustomergroup.Name);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, icustomergroup.FK_Client);
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
