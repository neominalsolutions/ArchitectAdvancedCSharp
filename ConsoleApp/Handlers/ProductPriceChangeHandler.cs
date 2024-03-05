using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Handlers
{
  public delegate void ProductPriceChangeHandler(ProductPriceChangeArgs args);

  // Ürün fiyat değişiminde veri taşıyacağımız nesne
  public class ProductPriceChangeArgs:EventArgs
  {
    public ProductPriceChangeArgs(decimal oldPrice, decimal newPrice, string productId)
    {
      OldPrice = oldPrice;
      NewPrice = newPrice;
      ProductId = productId;
    }

    public decimal OldPrice { get; init; }
    public decimal NewPrice { get; init; }
    public string ProductId { get; init; }

  }
}
