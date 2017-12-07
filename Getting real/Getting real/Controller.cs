using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace Getting_real
{
    class Controller
    {
        //change ip to local      
        public const string ip = "localhost";
        public static Token token = new Token();        

        public void Run()
        {            
            token.GetNewTokenResponse(ip);
            Console.WriteLine(token.AccessToken);            
            foreach (Department d in Department.GetNewDeparments(ip))
            {
                Console.WriteLine("Id: " + d.DepartmentId + " Name: " + d.DepartmentName);
            }
        }
        
    }
}
