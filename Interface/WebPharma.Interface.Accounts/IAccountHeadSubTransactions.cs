/*----------------------------------- Interface Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     :bibin  tc
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

namespace WebPharma.Interface.Accounts
{

    public interface IAccountHeadSubTransactions : IAccounts
    {
        long FK_AccountHead { get; set; }
        DateTime? EffectDate { get; set; }
        long FK_AccountHeadSub { get; set; }
        string AhCode { get; set; }
        string AhsCode { get; set; }
        string headName { get; set; }
        string Subheadname { get; set; }
        decimal Amount { get; set; }
        string ChequeNo { get; set; }
        DateTime? ChequeDate { get; set; }
        Int64 VoucherNo { get; set; }
        string Party { get; set; }
        string Narration { get; set; }
        string Remarks { get; set; }
        long FK_Transaction { get; set; }
        string CancelledUser { get; set; }
        DateTime CancelledOn { get; set; }
        long FK_Store { get; set; }
        long FK_FinYear { get; set; }
        Int16 MachineCode { get; set; }
        Boolean OfflineEntry { get; set; }
        Int16 BranchCode { get; set; }
        Boolean InterBranch { get; set; }
        string AccountNo { get; set; }
        string AccountHolderName { get; set; }
        DateTime? TransDate { get; set; }    
        string TransType { get; set; }
        string TransMode { get; set; }
        Boolean Transfer { get; set; }
        Boolean Cancelled { get; set; }
        Int16 EntryType { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        string  BillNo { get; set; }
        long FK_Party { get; set; }
        string SubId { get; set; }
        decimal BalanceAmount { get; set; }
        string XmlAccountTransMultiple { get; set; }
        Int64 groupid { get; set; }
        string XmlExcelImport { get; set; }
        long Fk_Reason { get; set; }
        
      
    }
    public interface IAccountTransaction:IAccounts
    {
        Int16 MachineCode { get; set; }
        Boolean OfflineEntry { get; set; }
        Int16 BranchCode { get; set; }
        Boolean InterBranch { get; set; }
        string TransType { get; set; }
        DateTime? TransDate { get; set; }
        Int64 VoucherNo { get; set; }
        Int16 BankTransType { get; set; }
        long Bank { get; set; }
        long BankBranch { get; set; }
        string ChequeNo { get; set; }
        DateTime? ChequeDate { get; set; }
        string NetBankAccount { get; set; }
        Int16 CreditAccount { get; set; }
        long InvoiceNo { get; set; }
        long CustomerNo { get; set; }
        long AccountHead { get; set; }
        long AccountHeadSub { get; set; }
        decimal Amount { get; set; }
        string Remarks { get; set; }
        decimal CustBalanceAmount { get; set; }
        string SubId { get; set; }  
        string Narration { get; set; }
        string XmlJournalDetails { get; set; }
        string XmlInvoiceDetails { get; set; }
        byte PaymentMode { get; set; }
        string CardNo { get; set; }
        decimal Commission { get; set; }
        Int64 groupid { get; set; }
        long Fk_Reason { get; set; }

    }
}


