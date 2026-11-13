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
	public class BlPriceConversion : IPriceConversion
	{
 
      

        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize; 
        private string _TransMode;



        private DateTime _TransDate;

       
        private long _FK_Store;
        private long _FK_Category;
        private Decimal _MinimumQty;

      
        private long _PriceTag;
       
        private string _XmlItems;
        private long _FK_Items;

       
        private long _ID_PriceConversion;
	
		
        private string _UserCode;
        private DateTime _EnteredOn;
        private Boolean _Cancelled;
        private string _Cancelleduser;
        private DateTime _CancelledOn;
     
        private long _FK_Client;
      
 
		public BlPriceConversion()
		{
			Initialize();
		}
		public void Initialize()
		{



            _ID_PriceConversion = 0;
            _TransDate = DateTime.Now;
            _FK_Store = 0;
            _FK_Category = 0;
            _MinimumQty = 0;
            _PriceTag = 0;
            _FK_Items = 0;


	        _UserCode = string.Empty;
	        _EnteredOn = DateTime.Now;
	        _Cancelled = false;
	        _Cancelleduser = string.Empty;
	        _CancelledOn = DateTime.Now;
	        _FK_Client = 1;
       
            _UserAction = 1;
            _PageIndex = 1;
            _PageSize = 100;
	        _TransMode = string.Empty; 
			
		}


        public DateTime TransDate
        {
            get { return _TransDate; }
            set { _TransDate = value; }
        }
        public Decimal MinimumQty
        {
            get { return _MinimumQty; }
            set { _MinimumQty = value; }
        }
        public long PriceTag
        {
            get { return _PriceTag; }
            set { _PriceTag = value; }
        }
        public long FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
		public byte UserAction
		{
			get { return _UserAction; }
			set { _UserAction = value; }
		}
 
		public Int16 PageIndex
		{
			get { return _PageIndex; }
			set { _PageIndex = value; }
		}
		public Int16 PageSize
		{
			get { return _PageSize; }
			set { _PageSize = value; }
		}
        public string XmlItems
        {
            get { return _XmlItems; }
            set { _XmlItems = value; }
        }
		public long MasterId
		{
			get { return _ID_PriceConversion; }
			set { _ID_PriceConversion = value; }
		}

     
        public long FK_Store
		{
			get { return _FK_Store; }
			set { _FK_Store = value; }
		}
        public long FK_Category
		{
			get { return _FK_Category; }
			set { _FK_Category = value; }
		}

      
      
     
		public string UserCode
		{
			get { return _UserCode; }
			set { _UserCode = value; }
		}
		public DateTime EnteredOn
		{
			get { return _EnteredOn; }
			set { _EnteredOn = value; }
		}
		public Boolean Cancelled
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
		public long FK_Client
		{
			get { return _FK_Client; }
			set { _FK_Client = value; }
		}

		public long InsertData()
		{
			long status;
			DalPriceConversion dalPriceConversion = new DalPriceConversion();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalPriceConversion.UpdatePriceConversion(this);
				dalPriceConversion=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalPriceConversion dalPriceConversion = new DalPriceConversion();
			long status;
			status = 0;
			try
			{
				status =  dalPriceConversion.UpdatePriceConversion(this);
				dalPriceConversion=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
      
	}

    public class Items
    {

        private Int64 _FK_Items;

        public Items()
        {
            Initialize();
        }
        public void Initialize()
        {
            
             _FK_Items = 0;     
        }
        public Int64 FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
    }
}
