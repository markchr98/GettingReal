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

        private string deviceId;
        private string serialNumber;
        private string connectionQuality;
        private string lastOnlineTime;
        private string assignedPatientID; 


         public string deviceId
        {
            get { }
            set { }
        }

         public string serialNumber
        {
            get { }
            set { }
        }

         public string connectionQuality
        {
            get { }
            set { }
        }

         public string lastOnlineTime
        {
            get { }
            set { }
        }

         public string assignedPatientID
        {
            get { }
            set { }
        }




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
