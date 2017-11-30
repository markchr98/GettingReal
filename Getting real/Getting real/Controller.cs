using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Getting_real
{    
    class Controller
    {
        //change ip to local      
        const string ip = "10.140.67.116";
        public int RefreshSeconds
        {
            get { return (int)TokenRespone["expires_in"]; }            
        }
        public string AccessToken
        {
            get { return (string)TokenRespone["access_token"]; }
        }

        public JObject TokenRespone = new JObject();                       

        public void Run()
        {
            TokenRespone = GetNewTokenResponse(ip);
            var start = DateTime.UtcNow;
            while (true)
            {                              
                if (start.AddSeconds((RefreshSeconds-60)) < DateTime.UtcNow)
                {
                    TokenRespone = GetNewTokenResponse(ip);
                    start = DateTime.UtcNow;
                    Console.WriteLine(AccessToken);
                }
                
            }
        }
        static public JObject GetNewTokenResponse(string ip)
        {
            using (WebClient client = new WebClient())
            {                
                string yourURL = "http://"+ip+":5000/api/Token";
                string PARM = "grant_type=password&username=Administrator&password=admin";
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                JObject result = JObject.Parse(client.UploadString(yourURL, PARM));
                
                return (result);
            }
        }
    }
}
