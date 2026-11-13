using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;
namespace WebPharma.Interface.Masters
{
    public interface IUserAccess : IGeneral
    {



        long FK_Users { get; set; }
        long FK_Pages { get; set; }
        bool IsAdd { get; set; }
        bool IsUpdate { get; set; }
        bool IsDelete { get; set; }
        bool IsView { get; set; }
        bool IsPurchase { get; set; }
        bool IsPurReturn { get; set; }
        bool IsSales { get; set; }
        bool IsSalesReturn { get; set; }
        bool IsApprove { get; set; }
        string XmlUserAccess { get; }
        Int64 FK_Pages1 { get; set; }
        Int64 FK_Pages2 { get; set; }
        Int64 FK_Pages3 { get; set; }
        Int64 FK_Pages4 { get; set; }
        Int64 FK_Pages5 { get; set; }
        Int64 FK_Pages6 { get; set; }
        Int64 FK_PageModule { get; set; }
        Int64 FK_UserGroup { get; set; }


    }
}
