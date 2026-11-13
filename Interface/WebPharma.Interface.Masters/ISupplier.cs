/*----------------------------------- Interface Class for Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : Interface for Supplier
Author     : Nithin Das H
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 07/02/2014
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

07/02/2014	Nithin Das H		33235
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.General
{

    public interface ISupplier : IGeneral
    {
        string SupplierName { get; set; }
        string ContactPerson { get; set; }
        string Area { get; set; }
        string Place { get; set; }
        string Post { get; set; }
        string Pin { get; set; }
        string LandMark { get; set; }
        long FK_District { get; set; }
        long FK_State { get; set; }
        long FK_Country { get; set; }
        string Fax { get; set; }
        string Phone { get; set; }
        string Mobile { get; set; }
        string Email { get; set; }
        string Website { get; set; }
        string TinNo { get; set; }
        string CstNo { get; set; }
        string KgstNo { get; set; }
      
        Boolean GovernmentSupplier { get; set; }
        Boolean Active { get; set; }
        string UserCode { get; set; }
        DateTime EnteredOn { get; set; }
        long FK_Client { get; set; }
        long FK_Reason { get; set; }
          
         

    }

}
