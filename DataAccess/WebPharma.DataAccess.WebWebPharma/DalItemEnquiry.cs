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
    public  class DalItemEnquiry
    {
              
        public DataTable ItemsSelectEnquiry(IItemEnquiry item)
		{
			DataTable dtblItems = new DataTable();
			Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proItemsEnquiry";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, item.MasterId);
            db.AddInParameter(dbCommand, "@ItemName", DbType.String, item.Name);
             db.AddInParameter(dbCommand, "@BatchNo", DbType.String, item.BatchNo);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, item.FK_Category);
            db.AddInParameter(dbCommand, "@FK_SubCategory", DbType.Int64, item.FK_SubCategory);
            db.AddInParameter(dbCommand, "@FK_Unit", DbType.Int64, item.FK_Units);
            db.AddInParameter(dbCommand, "@FK_SubUnit", DbType.Int64, item.FK_SubUnit);
            db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, item.FK_Supplier);
            db.AddInParameter(dbCommand, "@Fk_Customer", DbType.Int64, item.Fk_Customer);
            db.AddInParameter(dbCommand, "@CustomerName", DbType.String, item.CustomerName);
            db.AddInParameter(dbCommand, "@Fk_Doctor", DbType.Int64, item.Fk_Doctor);
            db.AddInParameter(dbCommand, "@DoctorName", DbType.String, item.DoctorName);
            db.AddInParameter(dbCommand, "@Fk_Store", DbType.Int64, item.FK_Store);
            db.AddInParameter(dbCommand, "@SalesPrice", DbType.Decimal, item.Salesprice);
            db.AddInParameter(dbCommand, "@PurchaseRate", DbType.Decimal, item.PurchaseRate);
            db.AddInParameter(dbCommand, "@MRP", DbType.Decimal, item.MRP);
            db.AddInParameter(dbCommand, "@Fk_Schedule", DbType.Int64, item.FK_Schedule);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, item.FK_Client);
            db.AddInParameter(dbCommand, "@Fk_Generic", DbType.Int64, item.FK_Generic);
            db.AddInParameter(dbCommand, "@Fk_Manufacture", DbType.Int64, item.FK_Manufacturer);
            db.AddInParameter(dbCommand, "@Bin", DbType.Int64, item.FK_Bin);
            db.AddInParameter(dbCommand, "@Rack", DbType.Int64, item.FK_Rack);
            db.AddInParameter(dbCommand, "@CriteriaMRP", DbType.String, item.CriteriaMRP);
            db.AddInParameter(dbCommand, "@CriteriaPurchase", DbType.String, item.CriteriaPurchase);
            db.AddInParameter(dbCommand, "@CreteriaSales", DbType.String, item.CreteriaSales);
            db.AddInParameter(dbCommand, "@ZeroStock", DbType.Boolean, item.Zerostock);
            db.AddInParameter(dbCommand, "@Level", DbType.Int16, item.Level);

           
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
