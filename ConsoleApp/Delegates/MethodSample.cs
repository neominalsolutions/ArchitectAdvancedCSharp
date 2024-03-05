using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// void döndüren ve için string parametre alan tüm methodların çalışmasını aşağıdaki delagete üzerinden sağlayabiliriz.
public delegate void SendMessageHandler(string message);

namespace ConsoleApp.Delegates
{
  public class MethodSample
  {
    public static void ShowMessage(string message)
    {
      Console.WriteLine(message);
    }

    public static void ShowMessage2(string message)
    {
      Console.WriteLine(message);
    }
  }
}
