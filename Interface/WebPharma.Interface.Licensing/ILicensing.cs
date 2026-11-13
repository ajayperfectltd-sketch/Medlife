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

namespace WebPharma.Interface.Licensing
{
    public interface ILicensing
    {
        Int16 Reseller { get; set; }
        string ClientName { get;set; }
        string Product { get; set; }
        string Edition { get; set; }
        string LicenseMethod { get; set; }
        string LicenseMode { get; set; }
        int LicenseCount { get; set; }
        DateTime InstallationDate { get; set; }
        DateTime ExpiryDate { get; set; }
        string DbName { get; set; }
    }
}
