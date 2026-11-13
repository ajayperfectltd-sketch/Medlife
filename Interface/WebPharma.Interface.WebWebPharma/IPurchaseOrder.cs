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
using   WebPharma.Interface.General;

namespace WebPharma.Interface.WebPharma
{
    public interface IPurchaseOrder:IGeneral
    {
        string PONo { get; set; }
        DateTime Date { get; set; }
        long FK_Supplier { get; set; }
        decimal totalAmount { get; set; }
        decimal Discount { get; set; }
        decimal RoundAmount { get; set; }
        decimal NetAmount { get; set; }
        decimal Advance { get; set; }
        string Remarks { get; set; }
        decimal CashAmount { get; set; }
        DateTime DeliveryDate { get; set; }
        string DeliveryType { get; set; }
        bool Closed { get; set; }
       string  ClosedUser { get; set; }
       string ClosedRemarks { get; set; }
       bool Cancelled { get; set; }
       string CancelledUser { get; set; }
       DateTime CancelledOn { get; set; }
       long FK_Store { get; set; }
       long FK_ItemType { get; set; }
       string XmlPurchaseItems { get; set; }
       DateTime FromDate { get; set; }
       DateTime ToDate { get; set; }
       string isClosed { get; set; }
       Decimal Quantity { get; set; }
       long FK_Reason { get; set; }
    }
}
