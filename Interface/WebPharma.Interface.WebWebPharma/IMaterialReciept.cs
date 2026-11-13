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

    public interface IMaterialReciept : IGeneral
    {
       

        long FK_Supplier { get; set; }
        long FK_Store { get; set; }
        long FK_Category { get; set; }
        long FK_PurchaseOrder { get; set; }
        long FK_ItemsforPreviousPurchase { get; set; }
        DateTime RecDate { get; set; }
        string InvoiceNo { get; set; }
        DateTime InvoiceDate { get; set; }
        Decimal TotalAmount { get; set; }
        Decimal RoundOff { get; set; }
        Decimal PurAmt { get; set; }
        Decimal Fright { get; set; }
        Decimal NetAmount { get; set; }
        string Cancelleduser { get; set; }
        DateTime CancelledOn { get; set; }
        bool Cancelled { get; set; }
        string GSTIN { get; set; }
        string XmlMaterialRecieptItems { get; set; }
        string XmlSalesTax { get; set; }
        string XmlPurchaseTax { get; set; }
        bool InterStatePurchase { get; set; }
        long FK_Reason { get; set; }
        string xmlGriddata { get; set; }
        long FK_Items { get; set; }
        string xmlExpenseData { get; set; }
        string XmlExcelImport { get; set; }
        long FK_SubCategory { get; set; }
        long FK_Units { get; set; }
        long FK_SubUnits { get; set; }
        long FK_Manufacture { get; set; }
        string ItemName { get; set; }
        string BatchNo { get; set; }
        long FK_Generic { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        Int16 Period { get; set; }
        long TransactionID { get; set; }
        long RefNo { get; set; }
    }

}
