using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
  public abstract class SoftDeleteEntity<TKey> : Entity<TKey>
    where TKey : IComparable
  {
    public bool IsDeleted { get; set; }

   

  }
}
