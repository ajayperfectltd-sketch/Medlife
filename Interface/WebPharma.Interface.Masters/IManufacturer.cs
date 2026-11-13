/*----------------------------------- Interface Class for Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Bibin TC
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
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.General
{
    public interface IManufacturer : IGeneral
    {
        string ShortName { get; set; }
        string Name { get; set; }
        string Phone { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string Address3 { get; set; }
        long FK_Reason { get; set; }
        string MobileNo { get; set; }
       
    }
}
