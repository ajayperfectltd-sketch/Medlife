/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Nithin Das H
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 07/02/2014
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

07/02/2014	Nithin Das H			33235
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
    public class DalSupplier
    {

        public Int32 UpdateSupplier(ISupplier isupplier)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSupplierUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, isupplier.UserAction);
            db.AddInParameter(dbCommand, "@ID_Supplier", DbType.Int64, isupplier.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, isupplier.SupplierName);
            db.AddInParameter(dbCommand, "@ContactPerson", DbType.String, isupplier.ContactPerson);
            db.AddInParameter(dbCommand, "@Area", DbType.String, isupplier.Area);
            db.AddInParameter(dbCommand, "@Place", DbType.String, isupplier.Place);
            db.AddInParameter(dbCommand, "@Post", DbType.String, isupplier.Post);
            db.AddInParameter(dbCommand, "@Pin", DbType.String, isupplier.Pin);
            db.AddInParameter(dbCommand, "@LandMark", DbType.String, isupplier.LandMark);
            db.AddInParameter(dbCommand, "@FK_District", DbType.Int64, isupplier.FK_District);
            db.AddInParameter(dbCommand, "@FK_State", DbType.Int64, isupplier.FK_State);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, isupplier.FK_Country);
            db.AddInParameter(dbCommand, "@Fax", DbType.String, isupplier.Fax);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, isupplier.Phone);
            db.AddInParameter(dbCommand, "@Mobile", DbType.String, isupplier.Mobile);
            db.AddInParameter(dbCommand, "@Website", DbType.String, isupplier.Website);
            db.AddInParameter(dbCommand, "@Email", DbType.String, isupplier.Email);
            db.AddInParameter(dbCommand, "@TinNo", DbType.String, isupplier.TinNo);
            db.AddInParameter(dbCommand, "@CstNo", DbType.String, isupplier.CstNo);
            db.AddInParameter(dbCommand, "@KgstNo", DbType.String, isupplier.KgstNo);
            db.AddInParameter(dbCommand, "@GovernmentSupplier", DbType.Boolean, isupplier.GovernmentSupplier);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, isupplier.Active);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, isupplier.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isupplier.FK_Client);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(isupplier, e);
                return 0;
            }
        }

        public ISupplier SelectSupplier(ISupplier isupplier)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSupplierSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Supplier", DbType.Int64, isupplier.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isupplier.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, isupplier.SupplierName);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        isupplier.SupplierName = Convert.ToString(dataReader["SupplierName"].ToString());
                        isupplier.ContactPerson = Convert.ToString(dataReader["ContactPerson"].ToString());
                        isupplier.Area = Convert.ToString(dataReader["Area"].ToString());
                        isupplier.Place = Convert.ToString(dataReader["Place"].ToString());
                        isupplier.Post = Convert.ToString(dataReader["Post"].ToString());
                        isupplier.Pin = Convert.ToString(dataReader["Pin"].ToString());
                        isupplier.LandMark = Convert.ToString(dataReader["LandMark"].ToString());
                        isupplier.FK_District = Convert.ToInt64(dataReader["FK_District"].ToString());
                        isupplier.FK_State = Convert.ToInt64(dataReader["FK_State"].ToString());
                        isupplier.FK_Country = Convert.ToInt64(dataReader["FK_Country"].ToString());
                        isupplier.Fax = Convert.ToString(dataReader["Fax"].ToString());
                        isupplier.Phone = Convert.ToString(dataReader["Phone"].ToString());
                        isupplier.Mobile = Convert.ToString(dataReader["Mobile"].ToString());
                        isupplier.Email = Convert.ToString(dataReader["Email"].ToString());
                        isupplier.Website = Convert.ToString(dataReader["Website"].ToString());
                        isupplier.TinNo = Convert.ToString(dataReader["TinNo"].ToString());
                        isupplier.CstNo = Convert.ToString(dataReader["CstNo"].ToString());
                        isupplier.KgstNo = Convert.ToString(dataReader["KgstNo"].ToString());
                        isupplier.GovernmentSupplier = Convert.ToBoolean(dataReader["GovernmentSupplier"]);
                        isupplier.Active = Convert.ToBoolean(dataReader["Active"].ToString());
                        isupplier.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        isupplier.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        isupplier.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        isupplier.FK_Reason = Convert.ToInt64(dataReader["FK_Reason"].ToString());
                       
                       
                    }
                }
                return isupplier;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(isupplier, e);
                throw e;
            }
        }

        public DataTable SelectAllSupplier(ISupplier isupplier)
		{
			DataTable dtblSupplier = new DataTable();
			   Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proSupplierSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Supplier",DbType.Int64,isupplier.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,isupplier.FK_Client);
            db.AddInParameter(dbCommand, "@Name", DbType.String, isupplier.SupplierName);
           
			try
			{
				 dtblSupplier = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblSupplier;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(isupplier, e);
                throw e;
			}
		}

        public int DeleteSupplier(ISupplier isupplier)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSupplierDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Supplier", DbType.Int64, isupplier.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isupplier.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int32, isupplier.FK_Reason);
            //db.AddInParameter(dbCommand, "@TransMode", DbType.String, isupplier.TransMode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, isupplier.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(isupplier, e);
                throw e;
            }
        }
        public DataTable SelectTinNumber(ISupplier isupplier)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSelectSupplierTinNumber";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@TinNo", DbType.String, isupplier.TinNo);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isupplier.FK_Client);
            db.AddInParameter(dbCommand, "@ID_Supplier", DbType.Int64, isupplier.MasterId);

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }

    }
}
