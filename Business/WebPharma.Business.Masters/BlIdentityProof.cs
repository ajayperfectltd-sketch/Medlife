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
   public class BlIdentityProof : IIdentityProof
    {



        public BlIdentityProof()
        {
            Initialize();
        }
        private long _ID_Identityproof;
        private byte _UserAction;
        private string _Name;
        private Int64 _Type;      
        private Int16 _SortOrder;
        private string _Descrption;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Boolean _AdressProof;
        private Boolean _IdProof;
        private Int64 _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Reason;
       
       

        public void Initialize()
        {

            _UserAction = 1;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _ID_Identityproof = 0;
            _Name = string.Empty;

            _Type = 0;
          
            _SortOrder = 0;
            _Descrption = string.Empty;
            _UserCode = string.Empty;
            _AdressProof = true;
            _IdProof = true;
            _FK_Reason = 0;


        }
        public Int64 FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public Int64 MasterId
        {
            get { return _ID_Identityproof; }
            set { _ID_Identityproof = value; }
        }
       

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }



        public Int64 Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
      
     
       

        public Int16 SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }


        public string Descrption
        {
            get { return _Descrption; }
            set { _Descrption = value; }
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


        public Boolean AdressProof
        {
            get { return _AdressProof; }
            set { _AdressProof = value; }
        }

        public Boolean IdProof
        {
            get { return _IdProof; }
            set { _IdProof = value; }
        }
        public Int64 FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
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


    


        public long InsertData()
        {
            DalIdentityProof dalidproof = new DalIdentityProof();
            long status = 0;
            try
            {
                _UserAction = 1;
                status = dalidproof.UpdateIdentityproof(this);
                dalidproof = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalIdentityProof dalidproof = new DalIdentityProof();
            dtbl = dalidproof.SelectAllidentityproof(this);
            dalidproof = null;
            return dtbl;
        }



        public long DeleteData()
        {
            DalIdentityProof dalidproof = new DalIdentityProof();

            long status;
            status = 0;
            try
            {
                status = dalidproof.Deleteidproof(this);
                dalidproof = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }

        public void SelectData()
        {
            DalIdentityProof dalidproof = new DalIdentityProof();
            IIdentityProof IId = (IIdentityProof)dalidproof.Selectidproof(this);
            _Name = IId.Name;
            _Type = IId.Type;
            _Descrption = IId.Descrption;

            _SortOrder = IId.SortOrder;

            _AdressProof = IId.AdressProof;
            _IdProof = IId.IdProof;
            dalidproof = null;
        }

        public long UpdateData()
        {
            DalIdentityProof dalidproof = new DalIdentityProof();
            long status = 0;
            try
            {
                _UserAction = 2;
                status = dalidproof.UpdateIdentityproof(this);
                dalidproof = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
    }

}
