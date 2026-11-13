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
    public class DalManufacturer
    {

        public Int32 UpdateManufacturer(IManufacturer imanufacturer)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proManufacturerUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, imanufacturer.UserAction);      
            db.AddInParameter(dbCommand, "@ID_Manufacturer", DbType.Int64, imanufacturer.MasterId);
            db.AddInParameter(dbCommand, "@Shortname", DbType.String, imanufacturer.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, imanufacturer.Name);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, imanufacturer.Phone);
            db.AddInParameter(dbCommand, "@Address1", DbType.String, imanufacturer.Address1);
            db.AddInParameter(dbCommand, "@Address2", DbType.String, imanufacturer.Address2);
            db.AddInParameter(dbCommand, "@Address3", DbType.String, imanufacturer.Address3);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, imanufacturer.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, imanufacturer.UserCode);
            db.AddInParameter(dbCommand, "@Mobile", DbType.String, imanufacturer.MobileNo);
         
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {                
                return 0;
            }
        }

        public IManufacturer SelectManufacturer(IManufacturer imanufacturer)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proManufacturerSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Manufacturer", DbType.Int64, imanufacturer.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, imanufacturer.FK_Client);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, imanufacturer.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, imanufacturer.Name);
            
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        imanufacturer.ShortName = Convert.ToString(dataReader["Shortname"].ToString());
                        imanufacturer.Name = Convert.ToString(dataReader["ManufactureName"].ToString());
                        imanufacturer.Phone = Convert.ToString(dataReader["Phone"].ToString());
                        imanufacturer.Address1 = Convert.ToString(dataReader["Address1"].ToString());
                        imanufacturer.Address2 = Convert.ToString(dataReader["Address2"].ToString());
                        imanufacturer.Address3 = Convert.ToString(dataReader["Address3"].ToString());
                        imanufacturer.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        imanufacturer.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        imanufacturer.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        imanufacturer.MobileNo = Convert.ToString(dataReader["Mobile"].ToString());
                        
                    }
                }
                return imanufacturer;
            }
            catch (SqlException e)
            {                
                throw e;
            }
        }

        public DataTable SelectAllManufacturer(IManufacturer imanufacturer)
		{
			DataTable dtblManufacturer = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proManufacturerSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Manufacturer",DbType.Int64,imanufacturer.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,imanufacturer.FK_Client);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, imanufacturer.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, imanufacturer.Name);
			try
			{
				 dtblManufacturer = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblManufacturer;
			}
			catch (SqlException e) 
			{			
                throw e;
			}
		}

        public int DeleteManufacturer(IManufacturer imanufacturer)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proManufacturerDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Manufacturer", DbType.Int64, imanufacturer.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, imanufacturer.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, imanufacturer.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, imanufacturer.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

    }
}
