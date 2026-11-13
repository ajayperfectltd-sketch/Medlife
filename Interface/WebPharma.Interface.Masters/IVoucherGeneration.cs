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

    public interface IVoucherGeneration : IGeneral
    {
        long FK_Voucher { get; set; }
        string VoucherNo { get; set; }
        DateTime ValidUpto { get; set; }
        Byte IsUsed { get; set; }
        DateTime EnteredOn { get; set; }
        long FK_Client { get; set; }
        DateTime ValidFrom { get; set; }
        long Nos { get; set; }
        byte VoucherType { get; set; }
    }

}