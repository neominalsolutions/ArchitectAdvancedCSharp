using ConsoleApp.Handlers;
using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp.Entities
{
  public class Product : AuditableEntity<string>, ISoftDeleteEntity
  {
    public decimal _oldPrice = 0.0M;

    [JsonPropertyOrder(2)]
    public string Name { get; private set; }

    [JsonPropertyName("_desc")]
    // [JsonIgnore]
    public string? Description { get; set; }

    [JsonPropertyOrder(1)]
    public decimal Price { get; private set; }

    [JsonPropertyOrder(3)]
    public int Stock { get; private set; }
    public bool IsDeleted { get; set; }

    //public event EventHandler PriceChanged; // dili geçmiş zaman kullandık.
    public event ProductPriceChangeHandler PriceChanged;

    // Product Category Association
    // public Category Category { get; set; } Navigation Property

    // FK
    public Guid CategoryId { get; set; }


    // Serialize ve Deserialize olurken bu contructor kullan

    // Eğer [JsonConstructor] işaretlenmiş ise boş contructordan devam eder.
    public Product()
    {

    }


    // [JsonConstructor]
    // Hangi contructor üzerinden deserialize edeceğimizi söylememiz lazım
    public Product(string name, decimal price, int stock)
    {
      Id = Guid.NewGuid().ToString();

      if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentException(nameof(name));
      }

      if (price <= 0)
      {
        throw new Exception("Fiyat 0 ve daha küçük olamaz");
      }

      if (stock < 10 || stock > 1000)
      {
        throw new Exception("Stok 10 ile 1000 arasında olabilir");
      }

      Name = name;
      Price = price;
      Stock = stock;


      Created("mert");
    }


    // Nesne davranışaları method
    public override void Deleted(string by)
    {
      // kendi delete alogritmamızı uygula
      // poliprophisim.
      // baseden gelen özelliklerin farklı bir şekilde yapılması olayı.
      base.Deleted(by);
    }

    // Nesnelerin belirli bir duruma karşı bir davranış gösteriler.
    // bu olaylara biz event diyoruz.
    // duyu organlarımız insanlar için bir event görevi görüyorsa, nesnelerde belirli durumların takibi için eventleri kullanırız.
    // fiyat değişti, stok değişti bunların takibi için.

    // Net ortamında eventler tek başlarına bir anlam ifade etmiyor, eventleri çalıştırmak için delegate dediğimiz method elçileri var.

    public void IncreasePrice(decimal newPrice)
    {
      var @args = new ProductPriceChangeArgs(this.Price, newPrice, this.Id);

      Price = newPrice;
      // EventHandler delagate sayesinde çalışmış oluyor.
      PriceChanged.Invoke(@args);

    }

  }
}
