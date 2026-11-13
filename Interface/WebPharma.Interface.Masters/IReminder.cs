

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.Masters
{
    public interface IReminder : IGeneral
    {
        
            string Topic{get; set;}
            string Message{get; set;}	
            DateTime FromDate{get; set;}
            DateTime ToDate{get; set;}
            Byte Type{get; set;}	
            long FK_User	{get; set;}
            Boolean Active{get; set;}
            //string UserCode { get; set; }
            string XmlUser { get; set; }
            long FK_UserGroup { get; set; }
           
    }
}
