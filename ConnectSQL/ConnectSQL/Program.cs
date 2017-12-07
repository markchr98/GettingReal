using System;
using System.Data;
using System.Data.SqlClient;


namespace ConnectSQL
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.Run();
        }
    }
}
