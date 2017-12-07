using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Getting_real
{
    class Patient
    {
        private JObject patientResponse = new JObject();
        
        public Patient()
        {

        }

        public void GetNewPatientResponse(string ip,string departmentId)
        {
            using (WebClient client = new WebClient())
            {
                string URL = "http://" + ip + ":5000/api/"+departmentId+"/Patients";
                string PARM = "";
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + Controller.token.AccessToken;
                JObject result = JObject.Parse(client.UploadString(URL, PARM));
                patientResponse = result;
            }
        }
    }
}
