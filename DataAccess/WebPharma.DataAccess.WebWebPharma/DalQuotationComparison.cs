using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.WebPharma;

namespace WebPharma.DataAccess.WebPharma
{
   public class DalQuotationComparison
    {
       public DataTable ItemsSelectQuotation(IQuotationComparison item)
       {
           DataTable dtblItems = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proQuotationComparison";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, item.FK_Items);
           db.AddInParameter(dbCommand, "@FreeQty", DbType.Int64, item.FreeQty);
           db.AddInParameter(dbCommand, "@PurchaseRate", DbType.Decimal, item.PurchaseRate);
           db.AddInParameter(dbCommand, "@DiscountAmount", DbType.Decimal, item.DiscountAmount);
           db.AddInParameter(dbCommand, "@Fk_Supplier", DbType.Decimal, item.Fk_Supplier);
             db.AddInParameter(dbCommand, "@Fk_Customer", DbType.Decimal, item.Fk_Customer);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, item.FK_Client);
              db.AddInParameter(dbCommand, "@Type", DbType.Int16, item.Type);
             db.AddInParameter(dbCommand, "@Category", DbType.Int16, item.Category);
           db.AddInParameter(dbCommand, "@CategoryName", DbType.String, item.CategoryName);
           try
           {
               dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
               return dtblItems;
           }
           catch (SqlException e)
           {
               //UpdateErrorLog(iitems, e);
               throw e;
           }
         
       }
       public DataTable SelectSupplierHistory(IQuotationComparison item)
       {
           DataTable dtblItems = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proSupplierHistory";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@Fk_Supplier", DbType.Int64, item.Fk_Supplier);
           db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, item.FromDate);
           db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, item.ToDate);
          
           try
           {
               dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
               return dtblItems;
           }
           catch (SqlException e)
           {
               //UpdateErrorLog(iitems, e);
               throw e;
           }

       }
    }
}
