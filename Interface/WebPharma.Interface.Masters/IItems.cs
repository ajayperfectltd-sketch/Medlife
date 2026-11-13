/*----------------------------------- Interface Class for Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.General
{

    public interface IItems : IGeneral
    {
        string Name { get; set; }
        string ShortName { get; set; }
        long FK_CategoryFirst { get; set; }
        long FK_CategorySecond { get; set; }
        long FK_CategoryThird { get; set; }
        long FK_Units { get; set; }
        long FK_SubUnit { get; set; }
        long FK_Manufacturer { get; set; }
        long FK_Generic { get; set; }
        long FK_Rack { get; set; }
        long FK_Bin { get; set; }
        long FK_Schedule { get; set; }
        decimal MinLevel { get; set; }
        decimal MaxLevel { get; set; }
        decimal ROLevel { get; set; }
        decimal ROQuantity { get; set; }
        decimal DangerLevel { get; set; }
        int ExpairyValidation { get; set; }
        Boolean StopSale { get; set; }
        string StopSaleRemarks { get; set; }
        string HSNCode { get; set; }
         string StoragePlace { get; set; }
         Boolean NotReturnable{ get; set; }
         int ReturnBeforDays{ get; set; }
         int ExpiryWarningBefore { get; set; }
        string XmlImport { get; set; }
        string XmlItemsDetails { get; set; }
        string XmlTaxDetails { get; set; }
        Boolean Combo { get; set; }
        Boolean Packed { get; set; }
        string PartNo { get; set; }
        decimal DiscPer { get; set; }
        long FK_Reason { get; set; }
        long FK_Supplier { get; set; }
        string  BatchNo { get; set; }
        long FK_Store { get; set; }
        decimal MRP { get; set; }
        DateTime Date { get; set; }
        long FK_User { get; set; }
        DateTime ExpiryDate { get; set; }
        string Mode { get; set; }
    }

}
