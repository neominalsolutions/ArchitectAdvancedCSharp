// See https://aka.ms/new-console-template for more information
using CommonLayer.Integers;
using ConsoleApp.Delegates;
using ConsoleApp.Entities;
using System.Reflection;

#region ProductCategorySample

Console.WriteLine("Hello, World!");

Category category = new Category(name:"sadsadsa");
// c.Name = "Kategori-1";
category.SetName("Category-1");

// daha kontrollü bir şekilde kategoriye ürün ekletmemiz gerekiyor yada 
// ürün ekleme işlemini hiç category nesnesi üzerinden yapmayacağız.

category.Products.Add(new Product("P-2", 10, 10));
// c.ReadOnlyProducts.Add(); sadece ürünler okunabili oldu
category.AddProduct("P-3", 25, 10);
var plist = category.PrivateSetProducts;


// Id init tanımlandığı için sadece consturctorda üretebiliriz.
// c.Id = Guid.NewGuid();

Product p = new Product(name:"P-1",price:10,stock:10);
p.Deleted("ali");
p.PriceChanged += P_PriceChanged1;

void P_PriceChanged1(ConsoleApp.Handlers.ProductPriceChangeArgs args)
{
  Console.WriteLine($"Yeni Fiyat:{args.NewPrice}");
  Console.WriteLine($"Eski Fiyat: {args.OldPrice}");
  Console.WriteLine($"Yeni Fiyat Eski Fiyata oranı: %{(args.NewPrice - args.OldPrice)/args.OldPrice} ");
}

//p.PriceChanged += P_PriceChanged; // event ataması yaptım, listener tanımladık.
p.IncreasePrice(25); // event tetikleme, JS göre bir davranış sonrası state değişiminde callback ile değişen state fırlatıp takibe aldık.


// Event çalıştığında tetiklenecek olan method.
void P_PriceChanged(object? sender, EventArgs e)
{
  Console.WriteLine("Fiyat Değişti");
}

Console.WriteLine(p.IsDeleted);

//p.Price = 10;


// save etmeye kalktı.

#endregion

#region DelegateSample






SendMessageHandler m1 = MethodSample.ShowMessage;
m1.Invoke("mesaj-1"); // method çalıştır tetikle yani delegate üzerinden method çalıştırma.

m1 = MethodSample.ShowMessage2;
m1.Invoke("Mesaj-2");

// Reflection C# uygulama çalışken yani çalışma zamanında uygulamaya müdehale etmemizi sağlayan bir kod geliştirme tekniğidir. genelde dll scan, class scan, assembly load, ve uygulama çalışırken bir davranış sergile  dynamic.invoke (method) kullanılır. Bir class propertylerinde methodların veya herhangi üyesinde gezinti yapma gibi durumlarda tercih edililir.

MethodInfo[] methods = new MethodSample().GetType().GetMethods();

foreach (var method in methods)
{
  if(method.IsStatic) // sadece static olan methodları baz al
  {
    var m = method.CreateDelegate<SendMessageHandler>(); // bu static method için SendMessageHandler tipinde bir delegate oluştur
    m.Invoke("m-1"); // delegate üzerinden method çalışıtır.
  }

}

//var s = Activator.CreateInstance(typeof(SendMessage));
//((SendMessage)s).Invoke("sasf");


#endregion

#region Extension

double n1 = 15.4;

//"ali".First<char>();

// double tipine yeni bir özellik kazandırdık. bütün double tipleir için geçerli
int nm2 = (15.4).ParseInt(); // 15
Console.WriteLine(nm2);

// var olan tipi helper yardımcı methodu ile başka bir tipe dönüştürdük.
int nm3 = DoubleHelper.ParseInt(n1);
Console.WriteLine(nm3);

#endregion

