/*----------------------------------- Interface Class for Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
23-01-2019  VINISHA Removed PosModule,DisableNegativeStock,PolePortName And Added Shift,BillType
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.General
{

    public interface IMachine : IGeneral
    {
        string MecName { get; set; }
        string MecIP { get; set; }
        //Boolean PosModule { get; set; }
        //Boolean DisableNegativeStock { get; set; }
        string MecAddress { get; set; }
        DateTime EnteredOn { get; set; }
        long FK_Client { get; set; }
        long FK_Counter { get; set; }
        long FK_Store { get; set; }
        long FK_Shift { get; set; }
        long FK_BillType { get; set; }
        string PrintModule { get; set; }
        Boolean AutoPrint { get; set; }
        //string PolePortName { get; set; }
        long FK_Reason { get; set; }
    }

}
