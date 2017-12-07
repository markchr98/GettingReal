using Newtonsoft.Json.Linq;
using System;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Getting_real
{
    class Controller
    {
        //change ip to local      
        public const string ip = "localhost";
        public static Token token = new Token();
        public DepartmentRepository depRepo = new DepartmentRepository();

        public void Run()
        {            
            token.GetNewTokenResponse(ip);
            Console.WriteLine(token.AccessToken);
            GetNewDeparments(ip);
            foreach (Department d in depRepo.GetDepartments())
            {
                Console.WriteLine("Id: " + d.DepartmentId + " Name: " + d.DepartmentName);
            }
        }
        public void GetNewDeparments(string ip)
        {
            using (WebClient client = new WebClient())
            {                
                string URL = "http://" + ip + ":5000/api/Departments";                
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token.AccessToken;
                JArray response = JArray.Parse(client.DownloadString(URL));
                
                //Write check for already existing data
                foreach (var department in response.Children())
                {                    
                    depRepo.AddDepartment(new Department()
                    {
                        DepartmentId = (string)department["departmentId"],
                        DepartmentName = (string)department["name"]
                    });
                }
            }
        }
    }
}
