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

    public interface IVoucher : IGeneral
    {
        string ShortName { get; set; }
        string Name { get; set; }
        byte Type { get; set; }
        decimal Amount { get; set; }
        decimal Percentage { get; set; }
        Int32 validity { get; set; }
        long FK_AccountHeadSub { get; set; }
        string Remarks { get; set; }
        Boolean Active { get; set; }
        bool IsRefrence { get; set; }
       

    }

}
