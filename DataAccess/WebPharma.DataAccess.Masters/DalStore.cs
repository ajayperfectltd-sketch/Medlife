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
    public class DalStore
    {

        public Int32 UpdateStore(IStore istore)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStoreUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, istore.UserAction);
            db.AddInParameter(dbCommand, "@ID_Store", DbType.Int64, istore.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, istore.Name);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, istore.Shortname);
            db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, istore.StartDate);
            db.AddInParameter(dbCommand, "@OpeningBalance", DbType.Decimal, istore.OpeningBalance);
            db.AddInParameter(dbCommand, "@OpeningLiability", DbType.Decimal, istore.OpeningLiability);
            db.AddInParameter(dbCommand, "@BranchType", DbType.Int16, istore.BranchType);
            db.AddInParameter(dbCommand, "@BranchCode", DbType.Int16, istore.BranchCode);
            db.AddInParameter(dbCommand, "@Address1", DbType.String, istore.Address1);
            db.AddInParameter(dbCommand, "@Address2", DbType.String, istore.Address2);
            db.AddInParameter(dbCommand, "@Address3", DbType.String, istore.Address3);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, istore.Phone);
            db.AddInParameter(dbCommand, "@Email", DbType.String, istore.Email);
            db.AddInParameter(dbCommand, "@Website", DbType.String, istore.Website);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, istore.Active);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, istore.UserCode);
            //db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, istore.EnteredOn);
            //db.AddInParameter(dbCommand, "@Cancelled", DbType.Byte, istore.Cancelled);
            //db.AddInParameter(dbCommand, "@CancelledBy", DbType.String, istore.CancelledBy);
            //db.AddInParameter(dbCommand, "@CancelledOn", DbType.DateTime, istore.CancelledOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, istore.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Agent", DbType.Int64, istore.FK_Agent);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istore, e);
                return 0;
            }
        }

        public IStore SelectStore(IStore istore)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStoreSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Store", DbType.Int64, istore.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, istore.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, istore.Name);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, istore.Shortname);
            
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        istore.Name = Convert.ToString(dataReader["StoName"].ToString());
                        istore.Shortname = Convert.ToString(dataReader["Shortname"].ToString());
                        istore.StartDate = Convert.ToDateTime(dataReader["StartDate"].ToString());
                        istore.OpeningBalance = Convert.ToDecimal(dataReader["OpeningBalance"].ToString());
                        istore.OpeningLiability = Convert.ToDecimal(dataReader["OpeningLiability"].ToString());
                        istore.BranchType = Convert.ToInt16(dataReader["BranchType"]);
                        istore.BranchCode = Convert.ToInt16(dataReader["BranchCode"].ToString());
                        istore.Address1 = Convert.ToString(dataReader["Address1"].ToString());
                        istore.Address2 = Convert.ToString(dataReader["Address2"].ToString());
                        istore.Address3 = Convert.ToString(dataReader["Address3"].ToString());
                        istore.Phone = Convert.ToString(dataReader["Phone"].ToString());
                        istore.Email = Convert.ToString(dataReader["Email"].ToString());
                        istore.Website = Convert.ToString(dataReader["Website"].ToString());
                        istore.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                        istore.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        //istore.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        //istore.CancelledBy = Convert.ToString(dataReader["CancelledBy"].ToString());
                        //istore.Cancelled = Convert.ToByte(dataReader["Cancelled"].ToString());
                        //istore.CancelledOn = Convert.ToDateTime(dataReader["CancelledOn"].ToString());
                        istore.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        istore.FK_Agent = Convert.ToInt64(dataReader["FK_Agent"].ToString());
                    }
                }
                return istore;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istore, e);
                throw e;
            }
        }

        public DataTable SelectAllStore(IStore istore)
		{
			DataTable dtblStore = new  DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proStoreSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Store",DbType.Int64,istore.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,istore.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, istore.Name);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, istore.Shortname);
			try
			{
				 dtblStore = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblStore;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(istore, e);
                throw e;
			}
		}

        public int DeleteStore(IStore istore)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStoreDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Store", DbType.Int64, istore.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, istore.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, istore.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, istore.FK_Reason);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istore, e);
                throw e;
            }
        }


        public DataTable selectBranch(IStore istore)
        {
            DataTable dtblStore = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBranchSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           

            try
            {
                dtblStore = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblStore;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istore, e);
                throw e;
            }
        }

        public DataTable selectaccountlink(IStore istore)
        {
            DataTable dtblStore = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountlinkSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


            try
            {
                dtblStore = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblStore;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istore, e);
                throw e;
            }
        }
    }
}
