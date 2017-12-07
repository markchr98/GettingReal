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
            get { immobilityAlertTimer; }
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

    }
}
