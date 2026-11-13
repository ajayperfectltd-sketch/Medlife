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
using System.Threading.Tasks;
using WebPharma.Interface.General;
using WebPharma.Interface.Masters;
using WebPharma.DataAccess.Masters;
using System.Data;


namespace WebPharma.Business.Masters
{
    public class BlGiftVoucher : IGiftVoucher
    {

        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _ID_GiftVoucher;
        private string _Shortname;
        private string _Name;
        private Int32 _VoucherType;
        private string _UserCode;
        private DateTime _EnteredOn;
        private long _FK_Client;


        public BlGiftVoucher()
        {
            Initialize();
        }
        public void Initialize()
        {
            _ID_GiftVoucher = 0;
            _Shortname = string.Empty;
            _Name = string.Empty;
            _VoucherType = 0;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _UserAction = 1;
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
        public long MasterId
        {
            get { return _ID_GiftVoucher; }
            set { _ID_GiftVoucher = value; }
        }

        public string Shortname
        {
            get { return _Shortname; }
            set { _Shortname = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public Int32 VoucherType
        {
            get { return _VoucherType; }
            set { _VoucherType = value; }
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
        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }

        public long InsertData()
        {
            long status;
            DalGiftVoucher dalGiftVoucher = new DalGiftVoucher();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalGiftVoucher.UpdateGiftvoucher(this);
               
                dalGiftVoucher = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long UpdateData()
        {
            _UserAction = 2;
            DalGiftVoucher dalGiftVoucher = new DalGiftVoucher();
            long status;
            status = 0;
            try
            {
                status = dalGiftVoucher.UpdateGiftvoucher(this);
                dalGiftVoucher = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long DeleteData()
        {
            _UserAction = 3;
            DalGiftVoucher dalGiftVoucher = new DalGiftVoucher();
            long status;
            status = 0;
            try
            {
                status = dalGiftVoucher.DeleteGiftvoucher(this);
                dalGiftVoucher = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public void SelectData()
        {
            DalGiftVoucher dalGiftVoucher = new DalGiftVoucher();
            IGiftVoucher iGiftVoucher;
            iGiftVoucher = (IGiftVoucher)dalGiftVoucher.SelectGiftvoucher(this);
            _Shortname = iGiftVoucher.Shortname;
            _Name = iGiftVoucher.Name;
            _VoucherType = iGiftVoucher.VoucherType;

        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalGiftVoucher dalGiftVoucher = new DalGiftVoucher();
            dtbl = dalGiftVoucher.SelectAllGiftvoucher(this);
            dalGiftVoucher = null;
            return dtbl;
        }


       
    }
}
