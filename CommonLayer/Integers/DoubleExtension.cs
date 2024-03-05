using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Integers
{
  // 1. kural extension static sınıf olmalıdır
  public static class DoubleExtension
  {
    // genişledteceğimiz tipte this keyword olucak.
    public static int ParseInt(this double value)
    {
      Console.WriteLine($"value : {value}");
      return Convert.ToInt32(value);
    }
  }
}
