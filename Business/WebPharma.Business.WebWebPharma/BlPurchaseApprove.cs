/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Bibin Tc
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using WebPharma.DataAccess.WebPharma;
using WebPharma.Interface.WebPharma;

namespace WebPharma.Business.WebPharma
{
    public class BlPurchaseApprove:IPurchaseApprove 
    {

        private Int64 _FK_Purchase;
        private Int64 _FK_BillType;
        private Int64 _FK_Supplier;
        private byte _UserAction;

      
        private Int64 _BillNo;
        private DateTime _BillDate;
        private decimal _PurchaseAmt;

        private decimal _RoundOff;
        private decimal _Freight;
        private decimal _DiscPer;
        private decimal _DiscAmt;
        private decimal _NetAmount;
        private decimal _Advance;
        private decimal _Loading;

       
        private decimal _UnLoading;

      
        private decimal _Display;
       
        private decimal _Others;

      




        private string _Remarks;
        private string _Chequeno;
        private DateTime _ChequeDate;
        private bool _Credit;
        private bool _Transfer;
        private Int64 _MasterId;
        private string _UserCode;
        private DateTime _EnteredOn;
        private bool _Cancelled;
        private string _Cancelleduser;
        private DateTime _CancelledOn;
        private Int64 _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private Int16 _PaymentMode ;
        private Int64 _FK_Accounthead;
        private Int64 _FK_AccountheadSub;
        private Int64 _FK_Bank;
        private Int64 _FK_BankBranch;
        private Int64 _FK_Store;
        private Int16 _State;

        public BlPurchaseApprove()

      {
          Initialize();
      }

      public void Initialize()
      {


          _UserAction = 1;
          _FK_Client = 1;
            _FK_Purchase = 0;
            _FK_BillType = 0;
            _FK_Supplier = 0;

            _BillNo = 0;
            _BillDate= DateTime.Now;
            _PurchaseAmt= 0;
            _FK_Accounthead=0;
            _FK_AccountheadSub=0;
            _RoundOff= 0;
            _Freight = 0;
            _DiscPer = 0;
            _DiscAmt = 0;
            _NetAmount = 0;
            _Advance = 0;
            _FK_Bank=0;
            _FK_Store = 0;
            _FK_BankBranch=0;
            _PageIndex = 1;
            _PageSize = 10;
       
            _Remarks ="";
            _Chequeno = "";
            _ChequeDate = DateTime.Now;
            _Credit = false;
            _Transfer = false;

            _UserCode = "";
            _EnteredOn = DateTime.Now;
            _Cancelled = false;
            _Cancelleduser = "";
            _PaymentMode = 0;
            _CancelledOn = DateTime.Now;
            _Loading = 0;
            _UnLoading = 0;
            _Display = 0;
            _Others = 0;
            _State = 0;
      }
      public Int16 State
      {
          get { return _State; }
          set { _State = value; }
      }
      public decimal Loading
      {
          get { return _Loading; }
          set { _Loading = value; }
      }
      public decimal UnLoading
      {
          get { return _UnLoading; }
          set { _UnLoading = value; }
      }
      public decimal Display
      {
          get { return _Display; }
          set { _Display = value; }
      }
      public decimal Others
      {
          get { return _Others; }
          set { _Others = value; }
      }
      public Int64 FK_Store
      {
          get { return _FK_Store; }
          set { _FK_Store = value; }
      }
      public Int64 FK_Bank
      {
          get { return _FK_Bank; }
          set { _FK_Bank = value; }
      }
      public Int64 FK_BankBranch
      {
          get { return _FK_BankBranch; }
          set { _FK_BankBranch = value; }
      }
      public Int64 FK_Accounthead
      {
          get { return _FK_Accounthead; }
          set { _FK_Accounthead = value; }
      }

      public Int64 FK_AccountheadSub
      {
          get { return _FK_AccountheadSub; }
          set { _FK_AccountheadSub = value; }
      }

      public Int16 PageSize
      {
          get { return _PageSize; }
          set { _PageSize = value; }
      }

      public Int16 PaymentMode
      {
          get { return _PaymentMode; }
          set { _PaymentMode = value; }
      }

      public Int16 PageIndex
      {
          get { return _PageIndex; }
          set { _PageIndex = value; }
      }
      public Int64 FK_Client
      {
          get { return _FK_Client; }
          set { _FK_Client = value; }
      }

      public bool Cancelled
      {
          get { return _Cancelled; }
          set { _Cancelled = value; }
      }

   

      public string Cancelleduser
      {
          get { return _Cancelleduser; }
          set { _Cancelleduser = value; }
      }
  

      public DateTime CancelledOn
      {
          get { return _CancelledOn; }
          set { _CancelledOn = value; }
      }

      public DateTime EnteredOn
      {
          get { return _EnteredOn; }
          set { _EnteredOn = value; }
      }

      public string UserCode
      {
          get { return _UserCode; }
          set { _UserCode = value; }
      }


      public byte UserAction
      {
          get { return _UserAction; }
          set { _UserAction = value; }
      }

      public Int64 MasterId
      {
          get { return _MasterId; }
          set { _MasterId = value; }
      }
      public Int64 FK_Purchase
      {
          get { return _FK_Purchase; }
          set { _FK_Purchase = value; }
      }
      public Int64 FK_BillType
      {
          get { return _FK_BillType; }
          set { _FK_BillType = value; }
      }
      public Int64 FK_Supplier
      {
          get { return _FK_Supplier; }
          set { _FK_Supplier = value; }
      }
      public Int64 BillNo
      {
          get { return _BillNo; }
          set { _BillNo = value; }
      }
      public DateTime BillDate
      {
          get { return _BillDate; }
          set { _BillDate = value; }
      }
      public decimal PurchaseAmt
      {
          get { return _PurchaseAmt; }
          set { _PurchaseAmt = value; }
      }
      public decimal RoundOff
      {
          get { return _RoundOff; }
          set { _RoundOff = value; }
      }
      public decimal Freight
      {
          get { return _Freight; }
          set { _Freight = value; }
      }
      public decimal DiscPer
      {
          get { return _DiscPer; }
          set { _DiscPer = value; }
      }
      public decimal DiscAmt
      {
          get { return _DiscAmt; }
          set { _DiscAmt = value; }
      }
       public decimal NetAmount
      {
          get { return _NetAmount; }
          set { _NetAmount = value; }
      }
      public decimal Advance
      {
          get { return _Advance; }
          set { _Advance = value; }
      }
         public string Remarks
      {
          get { return _Remarks; }
          set { _Remarks = value; }
      }
      public string Chequeno
      {
          get { return _Chequeno; }
          set { _Chequeno = value; }
      }

      public DateTime ChequeDate
      {
          get { return _ChequeDate; }
          set { _ChequeDate = value; }
      }
      public Boolean Credit
      {
          get { return _Credit; }
          set { _Credit = value; }
      }
        
     public Boolean Transfer
      {
          get { return _Transfer; }
          set { _Transfer = value; }
      }

     public DataTable InsertData()
     {
         DalPurchaseApprove purchaseapprove = new DalPurchaseApprove();
        //_UserAction = 1;
         DataTable status = new DataTable();
         try
         {
             status = purchaseapprove.PurchaseApprove(this);
         }
         catch (Exception ex)
         {

         }
         return status;
     }
     public long proGetPurchaseApproveBillNo()
     {
         long status = 0;
         DalPurchaseApprove purchaseapprove = new DalPurchaseApprove();
         try
         {
             status = purchaseapprove.proGetPurchaseApproveBillNo(this);
         }
         catch (Exception ex)
         {

         }
         return status;
     }
          
    }
}
