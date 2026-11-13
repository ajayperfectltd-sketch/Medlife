/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Bibin Tc
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;
using WebPharma.Interface.Masters;
using WebPharma.DataAccess.Masters;
using System.Data;
 
namespace WebPharma.Business.Masters
{
	public class BlExpense : IExpense
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Expense;
		private string _ExpName;
		private string _ExpShort;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;

 
		public BlExpense()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Expense = 0;
			_ExpName = string.Empty;
			_ExpShort = string.Empty;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
			_UserAction=1;
		}
		public byte UserAction
		{
			get { return _UserAction; }
			set { _UserAction = value; }
		}
 
		public Int16 PageIndex
		{
			get { return _PageIndex; }
			set { _PageIndex = value; }
		}
		public Int16 PageSize
		{
			get { return _PageSize; }
			set { _PageSize = value; }
		}
		public long MasterId
		{
			get { return _ID_Expense; }
			set { _ID_Expense = value; }
		}

		public string ExpName
		{
			get { return _ExpName; }
			set { _ExpName = value; }
		}
		public string ExpShort
		{
			get { return _ExpShort; }
			set { _ExpShort = value; }
		}
		public string UserCode
		{
			get { return _UserCode; }
			set { _UserCode = value; }
		}
		public DateTime EnteredOn
		{
			get { return _EnteredOn; }
			set { _EnteredOn = value; }
		}
		public long FK_Client
		{
			get { return _FK_Client; }
			set { _FK_Client = value; }
		}

		public long InsertData()
		{
			long status;
			DalExpense dalexpense = new DalExpense();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalexpense.UpdateExpense(this);
				dalexpense=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalExpense dalexpense = new DalExpense();
			long status;
			status = 0;
			try
			{
				status =  dalexpense.UpdateExpense(this);
				dalexpense=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalExpense dalexpense = new DalExpense();
			long status;
			status = 0;
			try
			{
				status =  dalexpense.DeleteExpense(this);
				dalexpense=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalExpense dalexpense = new DalExpense();
			IExpense iexpense;
			iexpense= (IExpense)dalexpense.SelectExpense(this);
			_ExpName = iexpense.ExpName;
			_ExpShort = iexpense.ExpShort;

		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
			DalExpense dalExpense = new DalExpense();
			dtbl = dalExpense.SelectAllExpense(this);
			dalExpense = null;
			return dtbl;
		}
	}
}
