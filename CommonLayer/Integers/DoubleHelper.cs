using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Integers
{

  // Helperdaki amaç bir hesaplama sonucun döndürmek ve bunu bir çok yerde kullanabilmek.
  // Sipariş Kod oluşturma algoritması SP-04586-AEG-078
  // 10/10/2020 => 10 Kasım 2020 Yılı => DateTime Helper.
  public static class DoubleHelper
  {
    public static int ParseInt(double value)
    {
      return Convert.ToInt32(value);
    } 
  }
}
