using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Getting_real
{
    class Statistic
    {
        private string patientId;
        private string from;
        private string to;
        private string totalTimeInBed;
        private string maxTimeWithoutMobility;
        private string numberOfBedExits;
        private string numberOfBedExitWarnings;
        private string numberOfConfirmedBedExitWarnings;
        private string numberOfManualRegisteredRepositionings;
        private string numberOfMovementsPerHour;
        private string numberOfImmobilityWarnings;

        public string PatientId { get => patientId; set => patientId = value; }
        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public string TotalTimeInBed { get => totalTimeInBed; set => totalTimeInBed = value; }
        public string MaxTimeWithoutMobility { get => maxTimeWithoutMobility; set => maxTimeWithoutMobility = value; }
        public string NumberOfBedExits { get => numberOfBedExits; set => numberOfBedExits = value; }
        public string NumberOfBedExitWarnings { get => numberOfBedExitWarnings; set => numberOfBedExitWarnings = value; }
        public string NumberOfConfirmedBedExitWarnings { get => numberOfConfirmedBedExitWarnings; set => numberOfConfirmedBedExitWarnings = value; }
        public string NumberOfManualRegisteredRepositionings { get => numberOfManualRegisteredRepositionings; set => numberOfManualRegisteredRepositionings = value; }
        public string NumberOfMovementsPerHour { get => numberOfMovementsPerHour; set => numberOfMovementsPerHour = value; }
        public string NumberOfImmobilityWarnings { get => numberOfImmobilityWarnings; set => numberOfImmobilityWarnings = value; }

        public static Statistic GetNewDeviceResponse(string ip, string patientId)
        {
            Statistic statistic = new Statistic();
            using (WebClient client = new WebClient())
            {
                List<Statistic> statistics = new List<Statistic>();
                string URL = "http://" + ip + ":5000/api/Patients/" + patientId + "/Statistics/SimpleCounts";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + Controller.token.AccessToken;

                JObject response = JObject.Parse(client.DownloadString(URL));

                statistic.patientId = (string)response["patientId"];
                statistic.from = (string)response["from"];
                statistic.to = (string)response["to"];
                statistic.totalTimeInBed = (string)response["totalTimeInBed"];
                statistic.maxTimeWithoutMobility = (string)response["maxTimeWithoutMobility"];
                statistic.numberOfBedExits = (string)response["numberOfBedExits"];
                statistic.numberOfBedExitWarnings = (string)response["numberOfBedExitWarnings"];
                statistic.numberOfConfirmedBedExitWarnings = (string)response["numberOfConfirmedBedExitWarnings"];
                statistic.numberOfManualRegisteredRepositionings = (string)response["numberOfManualRegisteredRepositionings"];
                statistic.numberOfMovementsPerHour = (string)response["numberOfMovementsPerHour"];
                statistic.numberOfImmobilityWarnings = (string)response["numberOfImmobilityWarnings"];
            }
            return statistic;
        }
    }
}

