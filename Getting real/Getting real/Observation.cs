using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Getting_real
{
    class Observation
    {
        private string observationId;
        private string patientId;
        private string time;
        private string durationInMinutes;
        private string userId;

        public string ObservationId { get => observationId; set => observationId = value; }
        public string PatientId { get => patientId; set => patientId = value; }
        public string Time { get => time; set => time = value; }
        public string DurationInMinutes { get => durationInMinutes; set => durationInMinutes = value; }
        public string UserId { get => userId; set => userId = value; }

        public static List<Observation> GetNewDeviceResponse(string ip, string patientId)
        {
            using (WebClient client = new WebClient())
            {
                List<Observation> observations = new List<Observation>();
                string URL = "http://" + ip + ":5000/api/Patients/" + patientId + "/Observations";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + Controller.token.AccessToken;

                JArray response = JArray.Parse(client.DownloadString(URL));

                foreach (var device in response.Children())
                {
                    observations.Add(new Observation()
                    {
                        observationId = (string)device["observationId"],
                        patientId = (string)device["patientId"],                        
                        time = (string)device["time"],
                        durationInMinutes = (string)device["durationInMinutes"],
                        userId = (string)device["userId"]
                        
                    });
                }
                return observations;
            }
        }
    }
}
