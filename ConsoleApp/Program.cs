// See https://aka.ms/new-console-template for more information
using CommonLayer.Integers;
using ConsoleApp;
using ConsoleApp.Delegates;
using ConsoleApp.Entities;
using ConsoleApp.Types;
using System.Reflection;
using System.Text.Json;

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

// Tupple C# 7.0 - MVC tarafında : Birden fazla veriyi taşıya bilecek bir wrapper type

#region TuppleSample

Tuple<int,string,double> Tvariable = Tuple.Create<int, string, double>(3, "ali", 5);
Console.WriteLine(Tvariable.Item1);

var person = PersonInfo("ali", "can", "ahmet");
Console.WriteLine($"{person.Name} {person.SurName} - {person.CompanyName}");
//person.Name;
//person.SurName;
//person.CompanyName;

static (string Name,string SurName,string CompanyName) PersonInfo(string name,string surname,string companyName)
{
  // name T1
  // surname T2
  // companyName T3
  return (name, surname, companyName);
}


// MVC yada APIda Entityleriden Dto'a dönüşüm sonucu veri döndürüken Tupple tipini kullanırız.
// Products
// Categories
// Suppliers

// class ProductCategorySuppleriViewModel { List<Category>, List<Suppleier> List<Product>}
// var response = Tupple<List<Product>,List<Category>,List<Supplier>.Create(plist,clist,slist);


#endregion
// Record kullanımı C# 9.0 ve ValueType değerlerin değer kıyaslamasında kullanılan bir tip. Normalde Referans type değerler, ram Heap bölgesinde tutulduğundan, compare işlemleri equals işlemleri ramdeki refrerans değer baz alınarak yapılıyor. Record bir Value Object yapısına denk gelir.
#region RecordSample

// Recordaki amaç farklı valuelara sahip nesneler üretmektir Bu sebeple değerler tekrar set edilmez, yani immutable type olarak çalışır. Immutable type init aşamasında değer set ettiğimiz daha sonrasında değer güncellemesi yapmadığımız tiplerdir.
// Not: Entity'ler mutuable type olarak geçer.


// record değer eşitliğine bakar

var money1 = new Money(100, "$");
var money2 = new Money(200, "TL");
var money3 = new Money(100, "$");

Console.WriteLine($" money1 equals money2 = {money1.Equals(money2)}"); // eğer değer eşitliği yoksa false  döner

Console.WriteLine($" money1 equals money3 = {money1.Equals(money3)}"); // eğer değer eşitliği yoksa false  döner

var moneyClass1 = new MoneyClass(100, "$");
var moneyClass2 = new MoneyClass(100, "$");



// burada ise referans eşitliğine bakacağından sonuç false çıkacaktır.
// referans eşitliğine bakar.
Console.WriteLine($" money1Class1 equals moneyClass2 ={moneyClass1.Equals(moneyClass2)}");


// referans olarak heap deki aynı noktayı gösterim
var moneyClass3 = moneyClass1;

Console.WriteLine($" money1Class3 equals moneyClass1 ={moneyClass1.Equals(moneyClass3)}");

// Record kullanılan yerler, 
// 1. DTOlar için kullanırız. (DTO değerler init ile alınmalıdır hiç bir şekilde ilk loaddan sonra değişmemelidir.) CreateProductDto { Name,Price,Stock }

// var request = new CreateProductDto();
// request.Price = 35; // Requesten gelen neyse o olmak zorunda.

// 2. DDD Domain Driven Design ile geliştirme yaparken, ValueObject denelin bir tip ile çalışıyoruz. ValueObject Id'si olmayan içindeki value değerleri ile aynı nesne olup olmadığını kıyasladığımız nesne. ShipAddress Address,County,City,PostalCode, GeoLocaltion=> Lat,Long

#endregion


// Serialization System.Text.Json => Newtonsoft'a göre daha lightweight bir kütüphane

// C# Nenslerini XML,JSON tipine dönüştemek yada geri dönüştürme işlemlerin kullanıyoruz. DeSerialization

#region SystemTextJson


var product = new Product("P-2", 10, 20);
product.Description = "Deneme";

// ProductName, productName

var jsonOptions = new JsonSerializerOptions
{
  NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
  // IgnoreReadOnlyFields = true,
  //  IncludeFields = true,
  PropertyNameCaseInsensitive = false,
  WriteIndented = true,
  PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
  DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
};

// Null geçilen alanları JSON çıktı verme ignore et.

var productJsonString = System.Text.Json.JsonSerializer.Serialize<Product>(product,jsonOptions);


var deSerializeProduct = System.Text.Json.JsonSerializer.Deserialize<Product>(productJsonString, jsonOptions);


Console.ReadKey();




#endregion


// Regex
// HashSet,Dictionary

