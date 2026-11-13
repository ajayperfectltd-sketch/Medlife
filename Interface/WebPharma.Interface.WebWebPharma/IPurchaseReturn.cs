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
    public interface IPurchaseReturn : IGeneral
    {

        long BillNo { get; set; }       
        DateTime BillDate { get; set; }
        string Invoice { get; set; }
        DateTime InvDate { get; set; }
        long FK_Supplier { get; set; }      
        long FK_Store { get; set; }  
        long FK_Category { get; set; }
        decimal Discount { get; set; }
        decimal DiscountAmt { get; set; }
        decimal NetAmount { get; set; }
        decimal TotalAmt { get; set; }  
        decimal OtherCharge { get; set; } 
        string Remarks{ get; set; }
        bool Credit { get; set; }
        bool Cancelled { get; set; }
        string CancelledBy { get; set; }
        DateTime CancelledOn { get; set; }
        string XmlPR { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        Boolean DirectReturn { get; set; }
        Boolean AccountPosting { get; set; }
        long FK_BillType { get; set; }
         long FK_Purchase { get; set; }
        decimal PurchaseDiscPercentage { get; set; }
        string SupplierName { get; set; }
        long FK_Items { get; set; }
        long TransactionID { get; set; }
        bool Expiry { get; set; }
        string ReturnMethod { get; set; }
        string GSTIN { get; set; }
        string ExpiryDate { get; set; }
        bool Approved { get; set; }
      
    }
}
