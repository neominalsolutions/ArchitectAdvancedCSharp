using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
  // TKEY : INT,LONG,GUID,STRING
  // IComparable value olarak değer eşitlik kontrolü yapılabilir tipler.
  // code defensing geliştirici yanlış tiple çalışmasını engellemek amaçlı kodda yapılan işaretlemeler.
  public abstract class Entity<TKey>
    where TKey:IComparable // where keyword kullanılarak yapılan işaretlemedye code defensing
  {
    public TKey Id { get; init; } // Constructor üzerinden Id değerini set edecedğim.
    // private set, init
  }
}
