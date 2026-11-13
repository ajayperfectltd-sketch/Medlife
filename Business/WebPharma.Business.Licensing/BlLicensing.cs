/*----------------------------------- businesslayer Layer Class -----------------------------------------------------------------------------------------------------------------------
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
using System.Data;
using WebPharma.Business;
using WebPharma.DataAccess.Licensing;
using WebPharma.Interface.Licensing;

namespace WebPharma.Business.Licensing
{
    public class BlLicensing : ILicensing
    {
        private Int16 _Reseller;        
        private string _ClientName;
        private string _Product;
        private string _Edition;
        private string _LicenseMethod;
        private string _LicenseMode;
        private int _LicenseCount;
        private DateTime _InstallationDate;
        private DateTime _ExpiryDate;
        private string _DbName;

        public Int16 Reseller
        {
            get { return _Reseller; }
            set { _Reseller = value; }
        }
        public string ClientName
        {
            get { return _ClientName; }
            set { _ClientName = value; }
        }
        public string Product
        {
            get { return _Product; }
            set { _Product = value; }
        }
        public string Edition
        {
            get { return _Edition; }
            set { _Edition = value; }
        }
        public string LicenseMethod
        {
            get { return _LicenseMethod; }
            set { _LicenseMethod = value; }
        }
        public string LicenseMode
        {
            get { return _LicenseMode; }
            set { _LicenseMode = value; }
        }
        public int LicenseCount
        {
            get { return _LicenseCount; }
            set { _LicenseCount = value; }
        }
        public DateTime InstallationDate
        {
            get { return _InstallationDate; }
            set { _InstallationDate = value; }
        }
        public DateTime ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        public string DbName
        {
            get { return _DbName; }
            set { _DbName = value; }
        }






        public BlLicensing()
        {
            Initialize();
        }
        public void Initialize()
        {
            _Reseller = 1;
            _ClientName=string.Empty;
            _Product = string.Empty;
            _Edition = string.Empty;
            _LicenseMethod = string.Empty;
            _LicenseMode = string.Empty;
            _LicenseCount = 0;
            _InstallationDate = DateTime.Now;
            _ExpiryDate = DateTime.Now;
            _DbName = string.Empty;
        }
        public bool InsertLicense(string License)
        {
            DalLicensing dallicensing = new DalLicensing();
            return dallicensing.InsertLicense(License);
        }
        public int CheckLicensedOrNot()
        {
            int count = 0;
            DalLicensing dallicensing = new DalLicensing();
            count = dallicensing.CheckLicensedOrNot();
            return count;
        }
        public DataTable GetLicense()
        {
            DalLicensing dallicensing = new DalLicensing();
            DataTable dt = new DataTable();
            dt = dallicensing.GetLicense();
            return dt;
        }        
        public DataTable CheckLicense()
        {
            DalLicensing dallicensing = new DalLicensing();
            DataTable dt = new DataTable();
            dt = dallicensing.CheckLicense(this);
            return dt;
        }
        public bool InsertSerialKey(string Field, string Value, string Field1, string Value1)
        {
            DalLicensing dallicensing = new DalLicensing();

            return dallicensing.InsertSerialKey(Field, Value, Field1, Value1);
        }
        public bool InsertEvaluationPeriod(string Field1, string Value1, string Field2, string Value2, string Field3, string Value3, string Field4, string Value4)
        {
            DalLicensing dallicensing = new DalLicensing();

            return dallicensing.InsertEvaluationPeriod(Field1, Value1, Field2, Value2, Field3, Value3, Field4, Value4);
        }
        //public DataTable CheckMachineLicense()
        //{
        //    DalLicensing dallicensing = new DalLicensing();
        //    DataTable dt = new DataTable();
        //    dt = dallicensing.CheckMachineLicense(this);
        //    return dt;
        //}
    }
}
