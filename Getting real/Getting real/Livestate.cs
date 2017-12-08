using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Getting_real
{
    class Livestate
    {

        private JObject patientResponse = new JObject();

        private string controlSignal;
        private string immobilityAlertSetting;
        private string immobilityAlertTimer;
        private string bedEmptyTimer;
        private string bedExitAlertTimer;
        private string bedExitAlertSetting;
        private string systemError;
        private string systemErrorTimer;

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

        public static List<Livestate> GetNewLivestates(string ip, string deviceSerial)
        {
            using (WebClient client = new WebClient())
            {
                List<Livestate> livestates = new List<Livestate>();
                string URL = "http://" + ip + ":5000/api/" + deviceSerial + "/Livestates";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + Controller.token.AccessToken;
                JArray response = JArray.Parse(client.DownloadString(URL));

                foreach (var livestate in response.Children())
                {
                    livestates.Add(new Livestate()
                    {
                        controlSignal = (string)livestate["controlSignal"],
                        immobilityAlertSetting = (string)livestate["immobilityAlertSetting"],
                        immobilityAlertTimer = (string)livestate["immobilityAlertTimer"],
                        bedEmptyTimer = (string)livestate["bedEmptyTimer"],
                        bedExitAlertTimer = (string)livestate["bedExitAlertTimer"],
                        bedExitAlertSetting = (string)livestate["bedExitAlertSetting"],
                        systemError = (string)livestate["systemError"],
                        systemErrorTimer = (string)livestate["systemErrorTimer"],
                    });
                }
                return livestates;
            }
        }

    }
}


