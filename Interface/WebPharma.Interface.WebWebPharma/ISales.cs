/*----------------------------------- Interface Class for Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 15/11/2021
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
    public interface ISales : IGeneral
    {



        long FK_Store { get; set; }
        long FK_BillType { get; set; }
        long BillNo { get; set; }
        DateTime BillDate { get; set; }
        byte CustomerType { get; set; }
        long FK_Customer { get; set; }
        string CustomerName { get; set; }
        string CardNumber { get; set; }
        decimal BillTotal { get; set; }
        decimal Discount { get; set; }
        decimal DiscountTest { get; set; }
        decimal RountOff { get; set; }
        decimal NetAmount { get; set; }
        decimal Balance { get; set; }
        decimal PayAmount { get; set; }
        long FK_Counter { get; set; }
        long FK_Hold { get; set; }
        long Fk_Shift { get; set; }
        string Remarks { get; set; }
        string UserCode { get; set; }
        Int16 PriceType { get; set; }
        DateTime EnteredOn { get; set; }
        decimal CashAmount { get; set; }
        decimal CrdAmount { get; set; }
        decimal ChqAmount { get; set; }
        string CrdNumber { get; set; }
        string CrdSlipNo { get; set; }
        string CrdBank { get; set; }
        string CrdRemarks { get; set; }
        string ChqNumber { get; set; }
        string ChqAccount { get; set; }
        DateTime ChqDate { get; set; }
        bool Credit { get; set; }
        string ChqBank { get; set; }
        string ChqRemarks { get; set; }
        string XmlGiftDetails { get; set; }
        string XmlDiscDetails { get; set; }
        string XmlSalesDetails { get; set; }
        byte StockUpdation { get; set; }
        byte Invoice { get; set; }
        long FK_Sales { get; set; }
        decimal OpeningCounterClose { get; set; }
        decimal SalesAmountCounterClose { get; set; }
        decimal SalesRetAmountCounterClose { get; set; }
        decimal PayInAmountCounterClose { get; set; }
        decimal PayOutAmountCounterClose { get; set; }
        decimal ClosingAmountCounterClose { get; set; }
        decimal CloseCounterRemittance { get; set; }
        string XmlCloseCounterDetails { get; set; }
        string PriLayout { get; set; }
        string BillNoSearch { get; set; }
        string AmountSearch { get; set; }
        Int16 NegativeStock { get; set; }
        long FK_RecoveryType { get; set; }
        Int32 InstallmentPeriod { get; set; }
        Int32 InstallmentDuration { get; set; }
        decimal InstallmentAmount { get; set; }
        DateTime RecoveryDate { get; set; }
        DateTime SubsDate { get; set; }
        decimal SubsPercentage { get; set; }
        decimal SubsAmount { get; set; }
        string SubsNumber { get; set; }
        string SubsAuthority { get; set; }
        string SubsRemarks { get; set; }
        bool InterState { get; set; }
        decimal RedeemAmount { get; set; }
        decimal RedeemDiscount { get; set; }
        Int64 RedeemPoint { get; set; }
        string Status { get; set; }

        string TableName { get; set; }
        string ListFields { get; set; }
        string ValueFields { get; set; }
        string SortFields { get; set; }
        string Criteria { get; set; }
        string UpdateField { get; set; }
        string UpdateValue { get; set; }
        decimal BillAmount { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        string GSTIN { get; set; }
        Int64 TransID { get; set; }
        Int64 ReturnId { get; set; }
        string ReturnName { get; set; }
        byte IsExclude { get; set; }
        string Mode { get; set; }
        string XmlOtherDetails { get; set; }
        long ID_Items { get; set; }
        long FK_Vehicle { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string LicennseNo { get; set; }
        string DriverName { get; set; }
        string VehicleNo { get; set; }
        string RBForm { get; set; }
        Boolean DrcDet { get; set; }
        long FK_State { get; set; }
        string BuyGSTIN { get; set; }
        string BuyPAN { get; set; }
        long FK_BuyState { get; set; }
        Boolean ExcludeTax { get; set; }
    }
}
