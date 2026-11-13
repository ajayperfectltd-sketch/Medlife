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
    public interface IUsers : IGeneral
    {
        long FK_Customer { get; set; }
        long FK_Employee { get; set; }
        string Name { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        DateTime LoginTime { get; set; }
        DateTime LogoutTime { get; set; }
        long FK_UserGroup { get; set; }
        DateTime LastPasswordChange { get; set; }
        Boolean Active { get; set; }
        DateTime EnteredOn { get; set; }
        long FK_Client { get; set; }

        string NewPassword { get; set; }


        string Photo { get; set; }
        long FK_Store { get; set; }
        Byte[] PhotoData { get; set; }
        string PhotoSmall { get; set; }
        Byte[] PhotoDataSmall { get; set; }
        string DailyPassword { get; set; }
        long FK_Reason { get; set; }
       // string UserName { get; set; }
        string AuditData { get; set; }

    }
}
