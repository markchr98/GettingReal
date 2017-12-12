using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Getting_real
{
    class Program
    {        
        static void Main(string[] args)
        {
            Console.WriteLine("starting");
            Controller controller = new Controller();
            controller.Run();
        }
    }
}
