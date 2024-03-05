using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Types
{
  public class MoneyClass
  {
    public MoneyClass(decimal amount, string currency)
    {
      Currency = currency;
      Amount = amount;
    }

    public string Currency { get; init; }
    public decimal Amount { get; init; }


    public override bool Equals(object? obj)
    {
      //if(obj is MoneyClass)
      //{
      //  var moneyObj = (MoneyClass)obj;
      //  if (this.Amount == moneyObj.Amount && this.Currency == moneyObj.Currency)
      //    return true;
      //}


      //return false;

    return  base.Equals(obj);

    }

  }
}
