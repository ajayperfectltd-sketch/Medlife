/*----------------------------------- Interface Class for Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : TaxType Interface Class
Author     : Praveen
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 18/05/2018
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

    public interface ITaxType : IGeneral
    {
        string TypeName { get; set; }
        long FK_AccountHead { get; set; }
        long FK_AccountHeadSub { get; set; }
        long FK_Reason { get; set; }
        Boolean TaxInterState { get; set; }
        Boolean TaxIntraState { get; set; }
        byte GSTType { get; set; }
    }

}
