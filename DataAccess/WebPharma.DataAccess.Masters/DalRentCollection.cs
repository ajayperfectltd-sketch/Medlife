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
using System.Linq;
using System.Text;
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
    public class DalRentCollection
    {
        public Int32 UpdateRentCollection(IRentCollection iRentCollection)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRentCollectionUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
         
            db.AddInParameter(dbCommand, "@ID_RentCollection", DbType.Int64, iRentCollection.MasterId);
            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iRentCollection.FK_Rack);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iRentCollection.TransDate);
            db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, iRentCollection.Amount);
            db.AddInParameter(dbCommand, "@PaymentMode", DbType.Int32, iRentCollection.PaymentMode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iRentCollection.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iRentCollection.UserCode);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iRentCollection.UserAction);
            db.AddInParameter(dbCommand, "@Vacate", DbType.Boolean, iRentCollection.Vacate);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, iRentCollection.FK_Supplier);
            db.AddInParameter(dbCommand, "@FK_RackAllotment", DbType.Int64, iRentCollection.FK_RackAllotment);
            
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

        public IRentCollection SelectRentCollection (IRentCollection iRentCollection)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRentCollectionSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_RentCollection", DbType.Int64, iRentCollection.MasterId);
            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iRentCollection.FK_Rack);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iRentCollection.TransDate);         
            db.AddInParameter(dbCommand, "@PaymentMode", DbType.Int32, iRentCollection.PaymentMode);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iRentCollection.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iRentCollection.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRentCollection.FK_Client);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iRentCollection.FK_Rack = Convert.ToInt64(dataReader["FK_Rack"].ToString());
                        iRentCollection.TransDate = Convert.ToDateTime(dataReader["TransDate"].ToString());
                        iRentCollection.PaymentMode = Convert.ToInt32(dataReader["PaymentMode"].ToString());
                        iRentCollection.Amount = Convert.ToDecimal(dataReader["Amount"].ToString());
                        iRentCollection.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iRentCollection.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iRentCollection.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        iRentCollection.Vacate = Convert.ToBoolean(dataReader["Vacate"].ToString());
                        iRentCollection.FK_Supplier = Convert.ToInt64(dataReader["FK_Supplier"].ToString());
                        iRentCollection.RackName = dataReader["RackName"].ToString();
                        
                    }
                }
                return iRentCollection;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategoryfirst, e);
                throw e;
            }


        }

        public DataTable SelectAllRentCollection(IRentCollection iRentCollection)
        {
            DataTable dtblReason = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRentCollectionSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@ID_RentCollection", DbType.Int64, iRentCollection.MasterId);
            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iRentCollection.FK_Rack);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iRentCollection.TransDate);
            db.AddInParameter(dbCommand, "@PaymentMode", DbType.Int32, iRentCollection.PaymentMode);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iRentCollection.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iRentCollection.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iRentCollection.FK_Client);



            try
            {
                dtblReason = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblReason;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(icategoryfirst, e);
                throw e;
            }


        }



     
  
    }
}
