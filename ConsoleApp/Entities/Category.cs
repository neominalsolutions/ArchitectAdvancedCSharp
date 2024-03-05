using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Entities
{
  public class Category:SoftDeleteEntity<Guid>
  {
    public string Name { get; private set; } // Required

    // Categoryde birden fazla Products var association
    public List<Product> Products { get; set; } = new List<Product>();
    
    // sadece read only
    public IReadOnlyList<Product> ReadOnlyProducts { get; set; }

    // field üzerinden readonly list ile çalışma 
    // eklemeler field üzerinden olacak yani kontrolü olucak
    private List<Product> _products = new List<Product>();

    // PrivateSetProducts dan ise sadece okuma işlemi yapıcaz.
    public IReadOnlyList<Product> PrivateSetProducts => _products;


    // ReadOnly code-defensing
    // Event Delegate kavramına geçiş yapalım.

    // Entityler arasındaki tek taraflı ilişkilere uni-directional association ismini veririz.
    // Her iki entityde de Navigation Propery varsa buna da bi-directional association ismini veririz.
    // Kaotik bir ilişki olmaması için tercihimiz uni-directional association'dır. (recommended)


    // Required alanları genelde setter'ını private set yapıp constructorda göndermeye zorlarız.
    // bude bir code-defensing tekniğidir.

    public Category(string name)
    {
      // sadece Init initialize işleminde oluşan ve bidaha değiştirilemeyecek olan şeyler için kullanılır. AccountNumber, TCNo, Credit Card Number, init olarak işaretleyelim.
      Id = Guid.NewGuid();
      SetName(name);
    }

    public void SetName(string name)
    {

      if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
      {
        // arguman doğru bir şekilde gönderilmedi hatası verdik.
        throw new ArgumentException(nameof(Name));
      }

      Name = name.Trim();
    }

    // Minimum stok adeti 10 olmalı ve maksimum tek seferde 1000 adet stok üzerinde ürün tedarik etmemeliyiz, fiyat ise 0 dan küçük ve eşit olmamalı
    public void AddProduct(string productName, decimal price, int stock)
    {

      var product = new Product(name: productName, price: price, stock: stock);
      product.CategoryId = this.Id;
      _products.Add(product);
    }
  }
}
