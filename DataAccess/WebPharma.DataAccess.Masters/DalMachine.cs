/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 14/02/2014
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
23-01-2019  VINISHA Removed PosModule,DisableNegativeStock,PolePortName And Added Shift,BillType
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
    public class DalMachine
    {

        public Int32 UpdateMachine(IMachine imachine)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMachineUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, imachine.UserAction);
            db.AddInParameter(dbCommand, "@ID_Machine", DbType.Int64, imachine.MasterId);
            db.AddInParameter(dbCommand, "@MecName", DbType.String, imachine.MecName);
            db.AddInParameter(dbCommand, "@MecIP", DbType.String, imachine.MecIP);
            db.AddInParameter(dbCommand, "@MecAddress", DbType.String, imachine.MecAddress);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, imachine.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, imachine.FK_Counter);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, imachine.FK_Store );
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, imachine.UserCode);
            db.AddInParameter(dbCommand, "@PrintModule", DbType.String, imachine.PrintModule);
            db.AddInParameter(dbCommand, "@AutoPrint", DbType.Boolean, imachine.AutoPrint);
            db.AddInParameter(dbCommand, "@FK_Shift", DbType.Int64, imachine.FK_Shift);
            db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, imachine.FK_BillType);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(imachine, e);
                return 0;
            }
        }

        public IMachine SelectMachine(IMachine imachine)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMachineSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Machine", DbType.Int64, imachine.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, imachine.FK_Client);
            db.AddInParameter(dbCommand, "@MecName", DbType.String, imachine.MecName);
            db.AddInParameter(dbCommand, "@MecIP", DbType.String, imachine.MecIP);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        imachine.MecName = Convert.ToString(dataReader["MecName"].ToString());
                        imachine.MecIP = Convert.ToString(dataReader["MecIP"].ToString());
                        imachine.MecAddress = Convert.ToString(dataReader["MecAddress"].ToString());
                        imachine.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                        imachine.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                        imachine.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                        imachine.FK_Counter = Convert.ToInt64(dataReader["FK_Counter"].ToString());
                        imachine.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                        imachine.PrintModule = Convert.ToString(dataReader["PrintModule"].ToString());
                        imachine.AutoPrint = Convert.ToBoolean(dataReader["AutoPrint"].ToString());
                        imachine.FK_Shift = Convert.ToInt64(dataReader["FK_Shift"].ToString());
                        imachine.FK_BillType = Convert.ToInt64(dataReader["FK_BillType"].ToString());
                    }
                }
                return imachine;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imachine, e);
                throw e;
            }
        }

        public DataTable SelectAllMachine(IMachine imachine)
		{
			DataTable dtblMachine = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
			string sqlCommand = "proMachineSelect";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "@ID_Machine",DbType.Int64,imachine.MasterId);
			db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32,imachine.FK_Client);
		    db.AddInParameter(dbCommand, "@MecName", DbType.String, imachine.MecName);
            db.AddInParameter(dbCommand, "@MecIP", DbType.String, imachine.MecIP);
			try
			{
				 dtblMachine = db.ExecuteDataSet(dbCommand).Tables[0];
				return dtblMachine;
			}
			catch (SqlException e) 
			{
				//UpdateErrorLog(imachine, e);
                throw e;
			}
		}

        public int DeleteMachine(IMachine imachine)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proMachineDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Machine", DbType.Int64, imachine.MasterId);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, imachine.FK_Reason);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, imachine.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, imachine.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(imachine, e);
                throw e;
            }
        }

    }
}
