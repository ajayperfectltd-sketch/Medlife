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

namespace WebPharma.Interface.Masters
{
    public interface IRentCollection:IGeneral
    {
        long FK_Rack { get; set; }
        DateTime TransDate { get; set; }
        decimal Amount { get; set; }
        Int32 PaymentMode { get; set; }
        Boolean Vacate { get; set; }
        long FK_Supplier { get; set; }
        long FK_RackAllotment { get; set; }
        string RackName { get; set; }
    }
}
