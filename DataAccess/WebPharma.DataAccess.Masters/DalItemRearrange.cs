using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.Masters;

namespace WebPharma.DataAccess.Masters
{
    public class DalItemRearrange
    {
        public DataTable SelectData(IItemRearrange iRearrange)
        {
            DataTable dtblItems = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemRearrangeSelection";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, iRearrange.FK_Rack);
            
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, iRearrange.FK_Category);
            db.AddInParameter(dbCommand, "@FK_SubCategory", DbType.Int64, iRearrange.FK_SubCategory);
            
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, iRearrange.MasterId);
            db.AddInParameter(dbCommand, "@ItemName", DbType.String, iRearrange.Name);
            db.AddInParameter(dbCommand, "@Barcode", DbType.String, iRearrange.Barcode);

            db.AddInParameter(dbCommand, "@Fk_Manufacture", DbType.Int64, iRearrange.FK_Manufacturer);
            db.AddInParameter(dbCommand, "@FK_Bin", DbType.Int64, iRearrange.FK_Bin);
              db.AddInParameter(dbCommand, "@FK_Unit", DbType.Int64, iRearrange.FK_Unit);
              db.AddInParameter(dbCommand, "@FK_Generic", DbType.Int64, iRearrange.FK_Generic);
            db.AddInParameter(dbCommand, "@StopSale", DbType.Boolean, iRearrange.StopSale);
            db.AddInParameter(dbCommand, "@Packed", DbType.Boolean, iRearrange.Packed);
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

        public long UpdateItemRearrange(IItemRearrange iRearrange)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proUpdateItemRearrange";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@UserAction", DbType.Int64, iRearrange.UserAction);
            db.AddInParameter(dbCommand, "@ID_ItemRearrange", DbType.Int64, iRearrange.MasterId);
            db.AddInParameter(dbCommand, "@ChangeTypeID", DbType.Int64, iRearrange.ChangeTypeID);
            db.AddInParameter(dbCommand, "@DataID", DbType.Int64, iRearrange.DataID);
            db.AddInParameter(dbCommand, "@SubDataID", DbType.Int64, iRearrange.SubDataID);
            db.AddInParameter(dbCommand, "@Checked", DbType.Boolean, iRearrange.Checked);
            db.AddInParameter(dbCommand, "@Reason", DbType.String, iRearrange.Reason);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, iRearrange.groupingdate);
            db.AddInParameter(dbCommand, "@XmlItems", DbType.Xml, iRearrange.XmlItems);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iRearrange.UserCode);


            try
            {
                return Convert.ToInt64(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iPriceConversion, e);
                return 0;
            }
        }

    }
}
