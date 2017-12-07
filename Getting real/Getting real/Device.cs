using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Getting_real
{
    class Device
    {
        JObject deviceResponse = new JObject();

        public Device()
        {

        }

        public void GetNewDeviceResponse(string ip)
        {
            using (WebClient client = new WebClient())
            {
                string URL = "http://" + ip + ":5000/api/Devices";
                string PARM = "";
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers[HttpRequestHeader.Authorization] = "Bearer "+Controller.token.AccessToken;
                JObject result = JObject.Parse(client.UploadString(URL, PARM));
                deviceResponse = result;
            }
        }
    }
}
