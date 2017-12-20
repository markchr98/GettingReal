using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Getting_real
{
    class Livestate
    {
        private string deviceId;
        private string controlSignal;
        private string immobilityAlertSetting;
        private string immobilityAlertTimer;
        private string bedEmptyTimer;
        private string bedExitAlertTimer;
        private string bedExitAlertSetting;
        private string systemError;
        private string systemErrorTimer;

        public string DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; }
        }

        public string ControlSignal
        {
            get { return controlSignal; }
            set { controlSignal = value; }
        }

        public string ImmobilityAlertSetting
        {
            get { return immobilityAlertSetting; }
            set { immobilityAlertSetting = value; }
        }

        public string ImmobilityAlertTimer
        {
            get { return immobilityAlertTimer; }
            set { immobilityAlertTimer = value; }
        }

        public string BedEmptyTimer
        {
            get { return bedEmptyTimer; }
            set { bedEmptyTimer = value; }
        }

        public string BedExitAlertTimer
        {
            get { return bedExitAlertTimer; }
            set { bedExitAlertTimer = value; }
        }

        public string BedExitAlertSetting
        {
            get { return bedExitAlertSetting; }
            set { bedExitAlertSetting = value; }
        }

        public string SystemError
        {
            get { return systemError; }
            set { systemError = value; }
        }

        public string SystemErrorTimer
        {
            get { return systemErrorTimer; }
            set { systemErrorTimer = value; }
        }
        
        public static Livestate GetLivestate(string ip, string deviceSerial)
        {
            using (WebClient client = new WebClient())
            {
                Livestate livestate = new Livestate();
                string URL = "http://" + ip + ":5000/api/Devices/" + deviceSerial + "/LiveState";
                client.Headers[HttpRequestHeader.ContentType] = "application/text/plain";
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + Controller.token.AccessToken;
                
                string response = client.DownloadString(URL);
                
                JObject jObject = JObject.Parse(response);                

                livestate.deviceId = (string)jObject["deviceId"];
                livestate.controlSignal = (string)jObject["controlSignal"];
                livestate.immobilityAlertSetting = (string)jObject["immobilityAlertSetting"];
                livestate.immobilityAlertTimer = (string)jObject["immobilityAlertTimer"];
                livestate.bedEmptyTimer = (string)jObject["bedEmptyTimer"];
                livestate.bedExitAlertTimer = (string)jObject["bedExitAlertTimer"];
                livestate.bedExitAlertSetting = (string)jObject["bedExitAlertSetting"];
                livestate.systemError = (string)jObject["systemError"];
                livestate.systemErrorTimer = (string)jObject["systemErrorTimer"];                   
                
                return livestate;
            }
        }

    }
}


