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

namespace WebPharma.Interface.WebPharma
{

    public interface IStock : IGeneral
    {
        
        long FK_Items { get; set; }
        long FK_Store { get; set; }
        long FK_RackArrangement { get; set; }
        string BatchNo { get; set; }
        DateTime ExpiryDate { get; set; }
        decimal PurRate { get; set; }
        decimal MRP { get; set; }
        decimal OpeningStock { get; set; }
        decimal CurrentStock { get; set; }
        long FK_Units { get; set; }
         DateTime TransDate { get; set; }
        string xmlStockItems { get; set; }
        long FK_Category { get; set; }
        decimal PurchaseTaxAmount { get; set; }
        decimal PurchaseDiscPercentage { get; set; }
        decimal SalesTaxAmount { get; set; }
        string Unit { get; set; }
        Int16 NoOfUnits { get; set; }
        decimal SalePrice { get; set; }
        decimal Tostore { get; set; }
        decimal ActualSalPrice { get; set; }
        string Mode { get; set; }
    }

}
