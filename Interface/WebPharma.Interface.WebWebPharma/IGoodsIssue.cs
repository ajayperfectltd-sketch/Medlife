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
    public interface IGoodsIssue:IGeneral
    {
      // long ID_GoodsIssue{get;set;}	
       string IssueNo{get;set;}	
       DateTime IssueDate{get;set;}	
      long  FK_FromStore{get;set;}	
      long  FK_ToStore{get;set;}	
      byte  IssueMode{get;set;}	
      string  Description{get;set;}	
     long   FK_Supplier{get;set;}	
     string    UserCode{get;set;}	
      DateTime  EnteredOn{get;set;}	
      bool  Cancelled{get;set;}	
      string  Cancelleduser{get;set;}	
      DateTime  CancelledOn{get;set;}
      DateTime FromDate { get; set; }
      DateTime ToDate { get; set; }
      string XmlGoods { get; set; }
        
 
    }
}
