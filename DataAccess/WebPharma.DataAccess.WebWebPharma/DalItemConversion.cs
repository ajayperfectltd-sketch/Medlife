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
using WebPharma.Interface.WebPharma;

namespace WebPharma.DataAccess.WebPharma
{
   public class DalItemConversion
    {
       public Int32  UpdateItemConversion(IItemConversion iItemConversion)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProIndentUpdate"; 
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iItemConversion.UserAction);
            db.AddInParameter(dbCommand, "@Production", DbType.Boolean, iItemConversion.Production);
            db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, iItemConversion.MasterId);
            db.AddInParameter(dbCommand, "@FK_StoreFrom", DbType.Int64, iItemConversion.FK_StoreFrom);
            db.AddInParameter(dbCommand, "@FK_StoreTo", DbType.Int64, iItemConversion.FK_StoreTo);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iItemConversion.TransDate);
            db.AddInParameter(dbCommand, "@Remarks", DbType.String, iItemConversion.Remarks);
            db.AddInParameter(dbCommand, "@FromAmount", DbType.Decimal, iItemConversion.FromAmount);
            db.AddInParameter(dbCommand, "@ToAmount", DbType.Decimal, iItemConversion.ToAmount);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iItemConversion.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iItemConversion.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iItemConversion.FK_Client);
            db.AddInParameter(dbCommand, "@XmlIndendFrom", DbType.Xml, iItemConversion.XmlIndendFrom);
            db.AddInParameter(dbCommand, "@XmlIndendTo", DbType.Xml, iItemConversion.XmlIndendTo);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                return 0;
            }

        }
       public Int32 IndentApprove(IItemConversion iItemConversion)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProIndentApprove";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);           
           db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, iItemConversion.MasterId);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, iItemConversion.UserCode);
           db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iItemConversion.EnteredOn);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iItemConversion.FK_Client);
         
           try
           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {
               return 0;
           }

       }
       public IItemConversion SelectItemConversion(IItemConversion iItemConversion)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProIndentSelect"; 
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_StoreFrom", DbType.Int64, iItemConversion.FK_StoreFrom);
            db.AddInParameter(dbCommand, "@FK_StoreTo", DbType.Int64, iItemConversion.FK_StoreTo);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, iItemConversion.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, iItemConversion.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iItemConversion.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, iItemConversion.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, iItemConversion.ToDate);
            db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, iItemConversion.MasterId);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iItemConversion.FK_StoreFrom = Convert.ToInt64(dataReader["FK_StoreFrom"].ToString());
                        iItemConversion.FK_StoreTo = Convert.ToInt64(dataReader["FK_StoreTo"].ToString());
                        iItemConversion.TransDate = DateTime.Parse(dataReader["TransDate"].ToString());
                        iItemConversion.Remarks = dataReader["Remarks"].ToString();
                        iItemConversion.FromAmount = Convert.ToDecimal(dataReader["FromAmount"].ToString());
                        iItemConversion.ToAmount = Convert.ToDecimal(dataReader["ToAmount"].ToString());
                        iItemConversion.Approved = Convert.ToBoolean(dataReader["Approved"].ToString());
                        
                    }
                }
                return iItemConversion;
            }
            catch (SqlException e)
            {

                throw e;
            }

        }
       public DataTable SelectItemConversionAll(IItemConversion iItemConversion)
       {
           DataTable dtbl = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProIndentSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@FK_StoreFrom", DbType.Int64, iItemConversion.FK_StoreFrom);
           db.AddInParameter(dbCommand, "@FK_StoreTo", DbType.Int64, iItemConversion.FK_StoreTo);
           db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, iItemConversion.PageIndex);
           db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, iItemConversion.PageSize);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iItemConversion.FK_Client);
           db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, iItemConversion.FromDate);
           db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, iItemConversion.ToDate);
           db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, iItemConversion.MasterId);

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
       public DataSet GetPurchaseReportData(long StatusId)
       {
           string sqlCommand = "";
           DbCommand dbCommand;
           DataSet ds = new DataSet();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           sqlCommand = "ProRptIndentReceiptprint";
           dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, StatusId);
           try
           {
               ds = db.ExecuteDataSet(dbCommand);
               return ds;
           }
           catch (SqlException e)
           {
               throw;
           }

       }

       public DataSet GetStockTransferRequestReportData(long StatusId)
       {
           string sqlCommand = "";
           DbCommand dbCommand;
           DataSet ds = new DataSet();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           sqlCommand = "ProRptGetStockTransferRequest";
           dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_IndentRequest", DbType.Int64, StatusId);
           try
           {
               ds = db.ExecuteDataSet(dbCommand);
               return ds;
           }
           catch (SqlException e)
           {
               throw;
           }

       }

       public DataTable SelectItemConversionItems(IItemConversion iItemConversion)
       {
           DataTable dtbl = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proItemConversionItemSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, iItemConversion.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iItemConversion.FK_Client);
           db.AddInParameter(dbCommand, "@IndentMode", DbType.String, iItemConversion.IndentMode);
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
