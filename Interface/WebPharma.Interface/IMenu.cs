using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPharma.Interface
{
   public interface IMenu
    {
       long ID_PageModule { get; set; }
       string ModuleName { get; set; }
       long ID_Pages { get; set; }
       string PageName { get; set; }
       string Url { get; set; }
       Int64 FK_User { get; set; }
       Int64 FK_Client { get; set; }
    }
}
