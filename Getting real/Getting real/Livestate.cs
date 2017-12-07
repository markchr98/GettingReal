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
            get {}
            set {}
        }

        public string ImmobilityAlertSetting
        {
            get {}
            set {}
        }

     public string ImmobilityAlertTimer
        {
            get {}
            set {}
        }

     public string BedEmptyTimer
        {
            get {}
            set {}
        }

     public string BedExitAlertTimer
        {
            get {}
            set {}
        }

     public string BedExitAlertSetting
        {
            get {}
            set {}
        }

     public string SystemError
        {
            get {}
            set {}
        }

     public string SystemErrorTimer
        {
            get {}
            set {}
        }

    }
}
