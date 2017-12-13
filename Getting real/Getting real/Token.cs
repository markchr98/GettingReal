using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Getting_real
{
    public class Token
    {
        private JObject tokenResponse;
     
        public string AccessToken
        {
            get { return (string)tokenResponse["access_token"]; }
        }
        public int RefreshSeconds
        {
            get { return (int)tokenResponse["expires_in"]; }
        }
        
        public Token()
        {
            
        }
        
        public void GetNewTokenResponse(string ip)
        {
            using (WebClient client = new WebClient())
            {
                string URL = "http://"+ip+":5000/api/Token";
                string PARM = "grant_type=password&username=Administrator&password=admin";
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                JObject result = JObject.Parse(client.UploadString(URL, PARM));
                tokenResponse = result;
            }
        }
    }
}
