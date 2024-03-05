using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Types
{
  public record Money // Currency $ Amount 10
  {
    public Money(decimal amount, string currency)
    {
      Currency = currency;
      Amount = amount;
    }

    public string Currency { get; init; }
    public decimal Amount { get; init; }


  }



}
