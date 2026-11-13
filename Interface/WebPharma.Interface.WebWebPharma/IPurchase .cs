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
    public interface IPurchase : IGeneral
    {

        long FK_Supplier { get; set; }
        long FK_Store { get; set; }
        long FK_PurchaseOrder { get; set; }
        Int16 Approved { get; set; }

        Int16 CashCredit { get; set; }
       // long FK_BillType { get; set; }
        string PurBillNo { get; set; }
        DateTime BillDate { get; set; }
        string InvoiceNo { get; set; }
     //   DateTime InvoiceDate { get; set; }
      //  decimal PurchaseAmt { get; set; }
      //  decimal RoundOff { get; set; }
       // decimal Fright { get; set; }
        decimal DiscountPer { get; set; }
        decimal DiscountAmt { get; set; }
     //   decimal NetAmount { get; set; }
      //  decimal Advance { get; set; }
        bool Credit { get; set; }
     //   string Remarks { get; set; }
      //  bool Approved { get; set; }
       // DateTime ApprovedOn { get; set; }
        string RefNo { get; set; }
        DateTime Date { get; set; }
      //  string ApprovedBy { get; set; }
        decimal Cost { get; set; }
        decimal Expense { get; set; }
        decimal InvoiceAmount { get; set; }
        string Description { get; set; }
      //  long VoucherNo { get; set; }
        bool Cancelled { get; set; }
        string Cancelleduser { get; set; }
        DateTime CancelledOn { get; set; }
        long FK_MaterialReciept { get; set; }
        //string XmlMaterialRecieptItems { get; set; }
        string BlPurchaseDetails { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        Boolean ItemPurchase { get; set; }
        string XmlPurchaseItems { get; set; }
        Int64 ReferenceNo { get; set; }

        string SupplierName { get; set; }
        long itemID { get; set; }
        Boolean Purchase { get; set; }
        long FK_Reason { get; set; }
    }
}
