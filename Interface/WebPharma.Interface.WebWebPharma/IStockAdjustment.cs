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
    public interface IStockAdjustment : IGeneral
    {
        string Module { get; set; }
        long FK_Module { get; set; }
      //  long FK_Stock { get; set; }
       // long FK_StockTo { get; set; }
        long FK_Item { get; set; }
        decimal Quantity { get; set; }      
        long FK_Store { get; set; }
        Boolean Approved { get; set; }
        string ApprovedBy { get; set; }
        DateTime ApprovedOn { get; set; }
        DateTime TransDate { get; set; }
        long FK_Category { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        string XmlStockAdjustmentItems { get; set; }
        long AdjustmentID { get; set; }
        string Name { get; set; }
    }
}