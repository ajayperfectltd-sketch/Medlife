/*---------------------------------------------------------------------
  Purpose   : Interface for Monthly Process Business Layer
  Author    : (your name)
  Namespace : WebPharma.Interface.Masters
  Pattern   : Same as IYearEnd : IGeneral
---------------------------------------------------------------------*/
using System;
using WebPharma.Interface.General;

namespace WebPharma.Interface.Masters
{
    public interface IMonthlyProcess : IGeneral
    {
        long     FK_Store           { get; set; }
        long     ID_MonthlyProcess  { get; set; }
        DateTime FromDate           { get; set; }
        DateTime ToDate             { get; set; }
        DateTime ProcessDate        { get; set; }   // MonthDate parameter
        decimal  Opening            { get; set; }
        decimal  Receipt            { get; set; }
        decimal  Payment            { get; set; }
        decimal  Closing            { get; set; }
        decimal  PurchaseAmount     { get; set; }
        decimal  PurchaseLiability  { get; set; }
        decimal  PReturnAmount      { get; set; }
        decimal  PReturnLiability   { get; set; }
        decimal  SalesAmount        { get; set; }
        decimal  SalesLiability     { get; set; }
        decimal  SReturnAmount      { get; set; }
        decimal  SReturnLiability   { get; set; }
        decimal  DamageStock        { get; set; }
        decimal  IndentFrom         { get; set; }
        decimal  IndentTo           { get; set; }
        decimal  StockValue         { get; set; }
        decimal  ClosingLiability   { get; set; }
        string   Remarks            { get; set; }
        byte     UserAction         { get; set; }
    }
}
