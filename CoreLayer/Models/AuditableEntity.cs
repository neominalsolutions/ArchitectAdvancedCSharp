using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
  // OOP => Inheritance,Polymorphisim, Abstraction, Encapsulation
  public abstract class AuditableEntity<TKey> : Entity<TKey>, IAuditableEntity
    where TKey : IComparable
  {
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }


    public virtual void Created(string createdBy)
    {
      CreatedBy = createdBy;
      CreatedAt = DateTime.Now;
      // event fırlatma gibi durumlar olursa burayı daha aktif kullanıyoruz
    }

    public virtual void Updated(string by)
    {
      UpdatedBy = by;
      UpdatedAt = DateTime.Now;
      DeletedAt = null;
      DeletedBy = null;
    }

    public virtual void Deleted(string by)
    {
      DeletedBy = by;
      DeletedAt = DateTime.Now;

      // eğer entity soft delete interfaceden implement olmuş bir entity ise isDeleted özelliği true çek.
      if(this is ISoftDeleteEntity)
      {
        var entity = (ISoftDeleteEntity)this;
        entity.IsDeleted = true;
      }
    }
  }
}
