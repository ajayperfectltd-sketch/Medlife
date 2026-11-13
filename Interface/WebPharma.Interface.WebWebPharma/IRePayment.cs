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

namespace WebPharma.Interface.WebPharma
{
    public interface IRePayment:IGeneral
    {
        long FK_Customer { get; set; }
        string TransType { get; set; }
        decimal Amount { get; set; }
        long FK_Client { get; set; }
        string CustName { get; set; }
        long FK_Store { get; set; }
        long FK_Counter { get; set; }
    }
}
