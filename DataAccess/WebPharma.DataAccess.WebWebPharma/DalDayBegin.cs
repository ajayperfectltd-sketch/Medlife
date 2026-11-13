using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WebPharma.Interface.WebPharma;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace WebPharma.DataAccess.WebPharma
{
   public class DalDayBegin
    {
       public DataTable PreviouusDayEndCheck(IDayBegin idaybegins)
        {
            DataTable dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPreviouusDayEndCheck";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@DbDate", DbType.DateTime, idaybegins.DbDate);
            db.AddInParameter(dbCommand, "@BranchCode", DbType.Int16, idaybegins.BranchCode);
       //     db.AddInParameter(dbCommand, "@BranchCodeUser", DbType.Int16, idaybegins.BranchCodeUser);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, idaybegins.UserCode);
         //   db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int16, idaybegins.MachineCode);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    dt.Load(dataReader);
                    return dt;
                }

            }
            catch (SqlException e)
            {
                throw e;

            }
        }
       //public DataTable DayProcessValidate( string Transmode, Int16 BranchCode, DateTime DayDate, byte ProcessNo)
       //{
       //    DataTable dt = new DataTable();
       //    Database db = DatabaseFactory.CreateDatabase("WebPharma");
       //    string sqlCommand = "ProDayProcessValidate";
       //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
       //    db.AddInParameter(dbCommand, "@Transmode", DbType.String, Transmode);
       //    db.AddInParameter(dbCommand, "@BranchCode", DbType.Int16, BranchCode);
       //    db.AddInParameter(dbCommand, "@DayDate", DbType.DateTime, DayDate);
       //   // db.AddInParameter(dbCommand, "@HeadOffice", DbType.Boolean, HeadOffice);
       //    db.AddInParameter(dbCommand, "@ProcessNo", DbType.Byte, ProcessNo);

       //    try
       //    {

       //        //using (IDataReader dataReader = db.ExecuteReader(dbCommand))
       //        //{
       //        //    dt.Load(dataReader);
       //        //    return dt;
       //        //}
       //        DataSet ds = new DataSet();
       //        ds = db.ExecuteDataSet(dbCommand);
       //        return ds.Tables[0];

       //    }
       //    catch (SqlException e)
       //    {

       //        throw e;
       //    }
       //}


       public Int32 UpdateDayBegin(IDayBegin idaybegin)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proDayBeginUpdate";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, idaybegin.UserAction);          
           db.AddInParameter(dbCommand, "@OfflineEntry", DbType.Boolean, idaybegin.OfflineEntry);
           db.AddInParameter(dbCommand, "@AuditData", DbType.String, idaybegin.AuditData);       
           db.AddInParameter(dbCommand, "@TransMode", DbType.String, idaybegin.TransMode);
       
           db.AddInParameter(dbCommand, "@DbDate", DbType.DateTime, idaybegin.DbDate);
           db.AddInParameter(dbCommand, "@BranchCode", DbType.Int16, idaybegin.BranchCode);            
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, idaybegin.UserCode);
           db.AddInParameter(dbCommand, "@openingbalance", DbType.Decimal, idaybegin.openingbalance);
           db.AddInParameter(dbCommand, "@openingliability", DbType.Decimal, idaybegin.openingliability);
           db.AddInParameter(dbCommand, "@AmntDifference", DbType.Decimal, idaybegin.AmntDifference);
          db.AddInParameter(dbCommand, "@XmlAttendance", DbType.Xml, idaybegin.XmlAttendance);
           db.AddInParameter(dbCommand, "@ID_DayBegin", DbType.Int64, idaybegin.MasterID);
         //  db.AddInParameter(dbCommand, "@SqlUpdateQuery", DbType.String, idaybegin.SqlUpdateQuery);
          // db.AddInParameter(dbCommand, "@DbPreviousDate", DbType.DateTime, idaybegin.DbPreviousDate);
         //  db.AddInParameter(dbCommand, "@SortOrder", DbType.Int32, idaybegin.SortOrder);
          // db.AddInParameter(dbCommand, "@BranchCodeUser", DbType.Int16, idaybegin.BranchCodeUser);
           try

           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {
               // UpdateErrorLog(iaccountheadsubtransaction, e);
               return 0;
           }

       }




    }
}
