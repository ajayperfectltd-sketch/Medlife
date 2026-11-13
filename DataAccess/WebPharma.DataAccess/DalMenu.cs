using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using WebPharma.Interface;
using WebPharma.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace WebPharma.DataAccess
{
   public class DalMenu
    {
       public DataTable FillModules()
       {
           DataTable dtbl = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proModuleSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);          
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
       public DataTable FillPages(IMenu imenu)
       {
           string sqlCommand = "";
           DbCommand dbCommand;
           DataTable dt = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           sqlCommand = "proPagesSelect";
           dbCommand = db.GetStoredProcCommand(sqlCommand);           
           db.AddInParameter(dbCommand, "@FK_PageModule", DbType.Int64, imenu.ID_PageModule);
           db.AddInParameter(dbCommand, "@FK_Users", DbType.Int64, imenu.FK_User);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, imenu.FK_Client);
           try
           {
               DataSet ds = new DataSet();
               ds = db.ExecuteDataSet(dbCommand);
               if (ds.Tables.Count > 0)
               {
                   dt = ds.Tables[0];
               }
               return dt;
           }
           catch (Exception e)
           {
               throw;
           }
       }


       public DataTable FillHelpContent(IMenu imenu)
       {
           string sqlCommand = "";
           DbCommand dbCommand;
           DataTable dt = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           sqlCommand = "proPagesSelectHelpContent";
           dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@Url", DbType.String, imenu.Url);

           try
           {
               DataSet ds = new DataSet();
               ds = db.ExecuteDataSet(dbCommand);
               if (ds.Tables.Count > 0)
               {
                   dt = ds.Tables[0];
               }
               return dt;
           }
           catch (Exception e)
           {
               throw;
           }
       }
    }
}
