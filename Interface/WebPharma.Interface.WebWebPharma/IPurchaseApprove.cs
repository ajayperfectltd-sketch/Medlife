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
    public interface IPurchaseApprove : IGeneral
    {
            Int64 FK_Purchase { get; set; }
            Int64 FK_BillType { get; set; }
            Int64 FK_Supplier { get; set; }
            Int64 FK_Store { get; set; }

            Int64 BillNo { get; set; }
            DateTime BillDate { get; set; }
            decimal PurchaseAmt { get; set; }
        
            decimal RoundOff { get; set; }
            decimal Freight { get; set; }
            decimal DiscPer { get; set; }
            decimal DiscAmt { get; set; }
            decimal NetAmount { get; set; }
            decimal Advance { get; set; }

            decimal Loading { get; set; }
            decimal UnLoading { get; set; }
            decimal Display { get; set; }
            decimal Others { get; set; }
       
            string Remarks { get; set; }
            string Chequeno { get; set; }
            DateTime ChequeDate { get; set; }
            bool Credit { get; set; }
            bool Transfer { get; set; }
            Int16 PaymentMode { get; set; }
            Int64 FK_Accounthead { get; set; }
            Int64 FK_AccountheadSub { get; set; }
            Int64 FK_BankBranch { get; set; }
            Int64 FK_Bank { get; set; }
            Int16 State { get; set; }
    }
}
