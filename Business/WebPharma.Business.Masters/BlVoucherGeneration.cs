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
	public class BlVoucherGeneration : IVoucherGeneration
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_VoucherGeneration;
		private long _FK_Voucher;
		private string _VoucherNo;
		private DateTime _ValidUpto;
		private byte _IsUsed;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private DateTime _ValidFrom;
        private long _Nos;
        private byte _VoucherType;

        public byte VoucherType
        {
            get { return _VoucherType; }
            set { _VoucherType = value; }
        }
        public long Nos
        {
            get { return _Nos; }
            set { _Nos = value; }
        }
        public DateTime ValidFrom
        {
            get { return _ValidFrom; }
            set { _ValidFrom = value; }
        }
 
		public BlVoucherGeneration()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_VoucherGeneration = 0;
			_FK_Voucher = 0;
			_VoucherNo = string.Empty;
			_ValidUpto = DateTime.Now;
            _IsUsed = 0;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 0;
            _ValidFrom = DateTime.Now;
            _UserAction = 1;
            _VoucherType = 0;
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
			get { return _ID_VoucherGeneration; }
			set { _ID_VoucherGeneration = value; }
		}
		
		
		public long FK_Voucher
		{
			get { return _FK_Voucher; }
			set { _FK_Voucher = value; }
		}
		public string VoucherNo
		{
			get { return _VoucherNo; }
			set { _VoucherNo = value; }
		}
		public DateTime ValidUpto
		{
			get { return _ValidUpto; }
			set { _ValidUpto = value; }
		}
		public byte IsUsed
		{
			get { return _IsUsed; }
			set { _IsUsed = value; }
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

		public string InsertData()
		{
			string status;
			DalVoucherGeneration dalvouchergeneration = new DalVoucherGeneration();
            status = "";
			try
			{
				_UserAction = 1;
				status = dalvouchergeneration.UpdateVoucherGeneration(this);
				dalvouchergeneration=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public string UpdateData()
		{
			_UserAction = 2;
			DalVoucherGeneration dalvouchergeneration = new DalVoucherGeneration();
			string status;
			status = "";
			try
			{
				status =  dalvouchergeneration.UpdateVoucherGeneration(this);
				dalvouchergeneration=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalVoucherGeneration dalvouchergeneration = new DalVoucherGeneration();
			long status;
			status = 0;
			try
			{
				status =  dalvouchergeneration.DeleteVoucherGeneration(this);
				dalvouchergeneration=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalVoucherGeneration dalvouchergeneration = new DalVoucherGeneration();
			IVoucherGeneration ivouchergeneration;
			ivouchergeneration= (IVoucherGeneration)dalvouchergeneration.SelectVoucherGeneration(this);
			_FK_Voucher = ivouchergeneration.FK_Voucher;
			_VoucherNo = ivouchergeneration.VoucherNo;
			//_ValidUpto = ivouchergeneration.ValidUpto;
            _ValidFrom = ivouchergeneration.ValidFrom;
           // _Nos = ivouchergeneration.Nos;

		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
            DalVoucherGeneration dalvou = new DalVoucherGeneration();
            dtbl = dalvou.SelectAllVoucherGeneration(this);
			
            dalvou = null;
			return dtbl;
		}
        public long VoucherValidUptoDate()
        {
            long status;
            status = 0;
            DalVoucherGeneration dalvouchergeneration = new DalVoucherGeneration();
            try
            {
                status = dalvouchergeneration.VoucherValidUptoDate(this);
                dalvouchergeneration = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
	}
}
