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
using WebPharma.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.Interface.Masters;
using WebPharma.Interface.General;

namespace WebPharma.DataAccess.Masters
{
    public class DalUnit
    {
        public Int32 UpdateUnit(IUnits iUnit)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUnitsUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iUnit.UserAction);
            db.AddInParameter(dbCommand, "@ID_Units", DbType.Int64, iUnit.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iUnit.Name);
            db.AddInParameter(dbCommand, "@NoofUnits", DbType.Int16, iUnit.NoofUnits);
            db.AddInParameter(dbCommand, "@BaseUnit", DbType.Boolean, iUnit.BaseUnit);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iUnit.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Byte, iUnit.FK_Client);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }



        }
        public IUnits SelectUnit(IUnits iUnit)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUnitSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Units", DbType.Int64, iUnit.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iUnit.Name);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iUnit.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iUnit.Name = Convert.ToString(dataReader["UnitName"].ToString());
                        iUnit.NoofUnits = Convert.ToInt16(dataReader["NoofUnits"].ToString());
                        iUnit.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        iUnit.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        iUnit.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                    }
                   

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iUnit;
        }
        public DataTable SelectAllUnits(IUnits iUnit)
        {
            DataTable dtblUnit = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUnitSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Units", DbType.Int64, iUnit.MasterId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iUnit.Name);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iUnit.FK_Client);
           
            try
            {
                dtblUnit = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblUnit;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(ibrand, e);
                throw e;
            }
        }
        public int DeleteUnits(IUnits iUnit)
        {
            Int32 status = 0;
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUnitDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Units", DbType.Int64, iUnit.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iUnit.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iUnit.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, iUnit.FK_Reason);
            try
            {
                status = Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {

                

            }
            return status;
        }
        public string GetCategoryBaseUnit(long FK_CategoryFirst)
        {
            string status = "";
             Database db = DatabaseFactory.CreateDatabase("WebPharma");
             string sqlCommand = "proCategoryFirstBaseUnit";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_CategoryFirst", DbType.Int64, FK_CategoryFirst);
             try
            {
                 object obj=db.ExecuteScalar(dbCommand);
                 if (obj != null)
                     status = obj.ToString();
            }
            catch (SqlException e)
            {

                

            }
            return status;
        }

    }
  
}