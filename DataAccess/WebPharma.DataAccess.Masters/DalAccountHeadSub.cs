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
    public class DalAccountHeadSub
    {
        public Int32 UpdateAccountHeadSub(IAccountHeadSub iAccSub)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProAccountHeadSubUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountHeadSub", DbType.Int64, iAccSub.MasterId);
            db.AddInParameter(dbCommand, "@FK_AccountHead", DbType.Int64, iAccSub.Fk_AccountHead);
            db.AddInParameter(dbCommand, "@AhCode", DbType.String ,iAccSub.Ahcode);
            db.AddInParameter(dbCommand, "@AhsCode", DbType.String, iAccSub.AhsCode);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, iAccSub.ShortName);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iAccSub.Name);
            db.AddInParameter(dbCommand, "@SortOrder", DbType.Int32, iAccSub.SortOrder);
            db.AddInParameter(dbCommand, "@Description", DbType.String, iAccSub.Description);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iAccSub.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iAccSub.EnteredOn);
            db.AddInParameter(dbCommand, "@Active", DbType.Boolean, iAccSub.Active);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iAccSub.FK_Client);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iAccSub.UserAction);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int16, iAccSub.Fk_Store);
            db.AddInParameter(dbCommand, "@PettyCash", DbType.Boolean, iAccSub.PettyCash);
            db.AddInParameter(dbCommand, "@OpeningDate", DbType.DateTime, iAccSub.OpeningDate);
            db.AddInParameter(dbCommand, "@OpeningBalance", DbType.Decimal, iAccSub.OpeningBalance);
            db.AddInParameter(dbCommand, "@Fk_Cuscode", DbType.Int64, iAccSub.Fk_Cuscode);
           
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }

        }

        public int AccountHeadSubDelete(IAccountHeadSub iAccSub)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProAccountHeadSubDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountHeadSub", DbType.Int64, iAccSub.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iAccSub.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iAccSub.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, iAccSub.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public IAccountHeadSub SelectAccountSub(IAccountHeadSub IAccountHeadSub)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProAccountHeadSubSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountHeadSub", DbType.Int64, IAccountHeadSub.MasterId);
         
            db.AddInParameter(dbCommand, "@FK_AccountHead", DbType.Int64, IAccountHeadSub.Fk_AccountHead);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IAccountHeadSub.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, IAccountHeadSub.ShortName);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, IAccountHeadSub.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, IAccountHeadSub.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IAccountHeadSub.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        IAccountHeadSub.Fk_AccountHead = Convert.ToInt64(dataReader["Ahcode"].ToString());

                        IAccountHeadSub.Ahcode = dataReader["AccountName"].ToString();
                        IAccountHeadSub.AhsCode = dataReader["AhsCode"].ToString();
                        IAccountHeadSub.Name = dataReader["HeadSubName"].ToString();
                        IAccountHeadSub.ShortName = dataReader["ShortName"].ToString();
                        IAccountHeadSub.SortOrder =int.Parse(dataReader["SortOrder"].ToString());
                        IAccountHeadSub.Fk_Cuscode = Int64.Parse(dataReader["Fk_Cuscode"].ToString());
                        //IAccountHeadSub.Fk_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                     
                        IAccountHeadSub.OpeningDate = Convert.ToDateTime(dataReader["OpeningDate"].ToString());
                        IAccountHeadSub.OpeningBalance = Convert.ToDecimal(dataReader["OpeningBalance"].ToString());
                       
                        
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return IAccountHeadSub;
        }

        public DataTable SelectAllData(IAccountHeadSub IAccountHeadSub)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProAccountHeadSubSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountHeadSub", DbType.Int64, IAccountHeadSub.MasterId);
           
            db.AddInParameter(dbCommand, "@FK_AccountHead", DbType.Int64, IAccountHeadSub.Fk_AccountHead);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IAccountHeadSub.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, IAccountHeadSub.ShortName);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, IAccountHeadSub.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, IAccountHeadSub.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IAccountHeadSub.FK_Client);
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



        public DataTable SelectsubheadId(IAccountHeadSub IAccountHeadSub)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProAccountHeadSubIdSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Ahcode", DbType.String, IAccountHeadSub.Ahcode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, IAccountHeadSub.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IAccountHeadSub.FK_Client);

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

        public DataTable SelectAccountHeadSub(IAccountHeadSub IAccountHeadSu)
        {
            DataTable dtbl = new DataTable();
                    Database db = DatabaseFactory.CreateDatabase("WebPharma");
                    string sqlCommand = "ProAccountHeadSubSelection";
                    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                    db.AddInParameter(dbCommand, "@Ahcode", DbType.String, IAccountHeadSu.Ahcode);
                    db.AddInParameter(dbCommand, "@UserCode", DbType.String, IAccountHeadSu.UserCode);
                    db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IAccountHeadSu.FK_Client);

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




        public DataTable selectSubHeadCustomerDetails(IAccountHeadSub IAccountHeadSu)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountHeadSubCusSelection";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountHeadSub", DbType.Int64, IAccountHeadSu.AhsCode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, IAccountHeadSu.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IAccountHeadSu.FK_Client);

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


        public DataTable selectfkAccounthead(IAccountHeadSub IAccountHeadSub)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProselectfkAccounthead";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Ahcode", DbType.String, IAccountHeadSub.Ahcode);
           // db.AddInParameter(dbCommand, "@UserCode", DbType.String, IAccountHeadSub.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IAccountHeadSub.FK_Client);

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

        public DataTable selectfkAccountheadsub(IAccountHeadSub IAccountHeadSub)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProselectfkAccountheadSub";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@AhCode", DbType.String, IAccountHeadSub.Ahcode);
            db.AddInParameter(dbCommand, "@AhsCode", DbType.Int64, IAccountHeadSub.AhsCode);
            // db.AddInParameter(dbCommand, "@UserCode", DbType.String, IAccountHeadSub.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IAccountHeadSub.FK_Client);

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
