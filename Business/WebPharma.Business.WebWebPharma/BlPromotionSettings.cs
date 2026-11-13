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
    public  class BlPromotionSettings:IPromotionSetting
    {
        #region properties
        public BlPromotionSettings()
        {
            Initialize();
        }
        private long _ID_PromotionSettings;
        private DateTime _CloseDate;

        public DateTime CloseDate
        {
            get { return _CloseDate; }
            set { _CloseDate = value; }
        }
        private bool _Closed;

        public bool Closed
        {
            get { return _Closed; }
            set { _Closed = value; }
        }
        public long MasterId
        {
            get { return _ID_PromotionSettings; }
            set { _ID_PromotionSettings = value; }
        }
        private Int16 _PageIndex;

        public Int16 PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }
        private Int16 _PageSize;

        public Int16 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        private long _FK_Client;

        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }

        private long _FK_PromotionType;

        public long FK_PromotionType
        {
            get { return _FK_PromotionType; }
            set { _FK_PromotionType = value; }
        }
        public long _FK_Items;
       
        public long FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        private DateTime _EffectDate;

        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        private decimal _DiscountPercent;

        public decimal DiscountPercent
        {
            get { return _DiscountPercent; }
            set { _DiscountPercent = value; }
        }
        private long _Buy;

        public long Buy
        {
            get { return _Buy; }
            set { _Buy = value; }
        }
        private long _Get;

        public long Get
        {
            get { return _Get; }
            set { _Get = value; }
        }
        private string _UserCode;

        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        private DateTime _EnteredOn;

        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
        }

        private DateTime _Date;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

      
        private byte _UserAction;

        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }

        private Int64 _FK_CategoryFirst;

        public Int64 FK_CategoryFirst
        {
            get { return _FK_CategoryFirst; }
            set { _FK_CategoryFirst = value; }
        }
        private Int64 _FK_Brand;

        public Int64 FK_Brand
        {
            get { return _FK_Brand; }
            set { _FK_Brand = value; }
        }
        private short _Type;

        public short Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public void Initialize()
        {
            _ID_PromotionSettings = 0;
            _PageIndex = 1;
            _PageSize = 10;
            _FK_Client = 1;
            _FK_PromotionType = 0;
            _FK_Items = 0;
            _EffectDate = DateTime.Now;
            _DiscountPercent = 0;
            _Buy = 0;
            _Get = 0;
            _UserCode = "";
            _EnteredOn = DateTime.Now;
            _UserAction = 1;
            _CloseDate = DateTime.Now;
            _Closed = false;
            _Date = DateTime.Now;

            _FK_CategoryFirst = 0;           
            _FK_Brand = 0;
           

        }

        #endregion

        #region Process
        public long InsertData()
        {
            long status;
            DalPromotionSettings dalPromotion = new DalPromotionSettings();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalPromotion.UpdatePromotionSettings(this);
                dalPromotion = null;
            
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            long status;
            DalPromotionSettings dalPromotion = new DalPromotionSettings();
            status = 0;
            try
            {
                _UserAction = 2;
                status = dalPromotion.UpdatePromotionSettings(this);
                dalPromotion = null;

            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            long status;
            DalPromotionSettings dalPromotion = new DalPromotionSettings();
            status = 0;
            try
            {
              
                status = dalPromotion.DeletepromotionSettings (this);
                dalPromotion = null;

            }
            catch (Exception ex)
            {
            }
            return status;
        }

        public void SelectData()
        {
            DalPromotionSettings dalPromotion = new DalPromotionSettings();
            IPromotionSetting IPromo;
            IPromo = (IPromotionSetting)dalPromotion.SelectPromotionSettings(this);
            _FK_PromotionType = IPromo.FK_PromotionType;
            
            _EffectDate = IPromo.EffectDate;
            _DiscountPercent = IPromo.DiscountPercent;
            _Buy = IPromo.Buy;
            _Get = IPromo.Get;
            _FK_CategoryFirst = IPromo.FK_CategoryFirst;
            _FK_Brand = IPromo.FK_Brand;
            _FK_Items = IPromo.FK_Items;
        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalPromotionSettings dalPromotion = new DalPromotionSettings();
            dtbl = dalPromotion.SelectAllData(this);
            dalPromotion = null;
            return dtbl;
        }
        public DataTable SelectPromotionSettingsItemGetQuantity()
        {
            DataTable dtbl = new DataTable();
            DalPromotionSettings dalPromotion = new DalPromotionSettings();
            dtbl = dalPromotion.SelectPromotionSettingsItemGetQuantity(this);
            dalPromotion = null;
            return dtbl;
        }

        #endregion
    }
}
