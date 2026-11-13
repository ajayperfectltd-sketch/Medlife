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
    public interface ISaleReturn : IGeneral
    {
        long FK_Store { get; set; }
        long FK_BillType { get; set; }
        long BillNo { get; set; }
        DateTime BillDate { get; set; }
        short CustomerType { get; set; }
        long FK_Customer { get; set; }
        string CustomerName { get; set; }
        string BatchNo { get; set; }
        string CardNumber { get; set; }
        decimal BillTotal { get; set; }
        decimal Discount { get; set; }
        decimal RoundOff { get; set; }
        decimal NetAmount { get; set; }
        short PaymentMode { get; set; }
        long FK_PaymentDetails { get; set; }
        decimal PayAmount { get; set; }
        string Remarks { get; set; }
        Boolean Cancelled { get; set; }
        string Cancelleduser { get; set; }
        DateTime CancelledOn { get; set; }
        long FK_Counter { get; set; }
        long Fk_Sale { get; set; }
        string XmlSR { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        DateTime ExpiryDate { get; set; }
        bool Credit { get; set; }
        string SaleNo { get; set; }
        string Customer { get; set; }
        long FK_ItemId { get; set; }
        long FK_Stock { get; set; }
        long FK_Reason { get; set; }
        long TransactionID { get; set; }

    }
}
