using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
  // insert,update,delete işlemlerini takip edebileceğim bir entity yaptık.
  public interface IAuditableEntity
  {

    DateTime? UpdatedAt { get; set; }
    string? UpdatedBy { get; set; }

    DateTime CreatedAt { get; set; }
    string CreatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }


  }
}
