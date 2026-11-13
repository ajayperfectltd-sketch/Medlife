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
    public class DalAccountHead
    {
        public Int32 UpdateAccountHead(IAccountHead IAccHead)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAccountHeadUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountHead", DbType.Int64, IAccHead.MasterId);
            db.AddInParameter(dbCommand, "@FK_AccountGroup", DbType.Int64, IAccHead.FK_AccountGroup);
            db.AddInParameter(dbCommand, "@FK_AccountSubGroup", DbType.Int64, IAccHead.FK_AccountSubGroup);
            db.AddInParameter(dbCommand, "@AhCode", DbType.Int64, IAccHead.AhCode);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IAccHead.Name);
            db.AddInParameter(dbCommand, "@ShortName", DbType.String, IAccHead.ShortName);
            db.AddInParameter(dbCommand, "@AccountType", DbType.Int16, IAccHead.AccountType);
            db.AddInParameter(dbCommand, "@SortOrder", DbType.Int32, IAccHead.SortOrder);
            //db.AddInParameter(dbCommand, "@Description", DbType.String, IAccHead.Description);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, IAccHead.UserCode);
            //db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, IAccHead.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IAccHead.FK_Client);
            //db.AddInParameter(dbCommand, "@Active", DbType.Boolean, IAccHead.Active);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, IAccHead.UserAction);
            db.AddInParameter(dbCommand, "@CashinHand", DbType.Boolean, IAccHead.CashinHand);

             try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }


        }
        public int DeleteAccountGroup(IAccountHead IAccHead)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proAccountHeadDelete";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_AccountHead", DbType.Int64, IAccHead.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IAccHead.FK_Client);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, IAccHead.UserCode);
           db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, IAccHead.FK_Reason);
           try
           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {
               throw e;
           }
       }
       public IAccountHead SelectAccountHead(IAccountHead IAccHead)
       {
           DataTable dtbl = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proAccountHeadSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_AccountHead", DbType.Int64, IAccHead.MasterId);
           db.AddInParameter(dbCommand, "@FK_AccountGroup", DbType.Int64, IAccHead.FK_AccountGroup);
           db.AddInParameter(dbCommand, "@FK_AccountSubGroup", DbType.Int64, IAccHead.FK_AccountSubGroup);
           db.AddInParameter(dbCommand, "@AccountType", DbType.Int16, IAccHead.AccountType);
           db.AddInParameter(dbCommand, "@Name", DbType.String, IAccHead.Name);
           db.AddInParameter(dbCommand, "@ShortName", DbType.String, IAccHead.ShortName);
           db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, IAccHead.PageIndex);
           db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, IAccHead.PageSize);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IAccHead.FK_Client);
           try
           {
               using (IDataReader dataReader = db.ExecuteReader(dbCommand))
               {
                   while (dataReader.Read())
                   {
                       IAccHead.FK_AccountGroup = Convert.ToInt64(dataReader["FK_AccountGroup"].ToString());
                       IAccHead.FK_AccountSubGroup = Convert.ToInt64(dataReader["FK_AccountSubGroup"].ToString());
                       IAccHead.AhCode = dataReader["AhCode"].ToString();

                       IAccHead.Name = dataReader["HeadName"].ToString();
                       IAccHead.ShortName = dataReader["ShortName"].ToString();
                       IAccHead.AccountType = Convert.ToByte(dataReader["AccountType"]);
                       IAccHead.SortOrder = int.Parse(dataReader["SortOrder"].ToString());
                       IAccHead.CashinHand = Convert.ToBoolean(dataReader["CashinHand"]); 
                   }
               }
               return IAccHead;
           }
            catch (SqlException e)
            {
                throw e;
            }
       }
       public DataTable SelectAllData(IAccountHead IAccHead)
       {
           DataTable dtbl = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proAccountHeadSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_AccountHead", DbType.Int64, IAccHead.MasterId);
           db.AddInParameter(dbCommand, "@FK_AccountGroup", DbType.Int64, IAccHead.FK_AccountGroup);
           db.AddInParameter(dbCommand, "@FK_AccountSubGroup", DbType.Int64, IAccHead.FK_AccountSubGroup);
           db.AddInParameter(dbCommand, "@AccountType", DbType.Int16, IAccHead.AccountType);
           db.AddInParameter(dbCommand, "@Name", DbType.String, IAccHead.Name);
           db.AddInParameter(dbCommand, "@ShortName", DbType.String, IAccHead.ShortName);
           db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, IAccHead.PageIndex);
           db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, IAccHead.PageSize);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IAccHead.FK_Client);
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
