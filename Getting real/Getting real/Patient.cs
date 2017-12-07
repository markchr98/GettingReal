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
        
        private string patientId;
        private string firstname;
        private string lastname;
        private string birthDate;
        private string departmentId;
        private string patientNumber;
        private string entryDate;
        private string dichargeDate;
        private string editedOn;
        private string editedBy; 

        public string patientId
        {
            get { }
            set { }
        }

        public string firstname
        {
            get { }
            set { }
        }

       public string lastname
        {
            get { }
            set { }
        }

      public string birthDate
        {
            get { }
            set { }
        }

    public string departmentId
        {
            get { }
            set { }
        }

    public string patientNumber
        {
            get { }
            set { }
        }

    public string entryDate
        {
            get { }
            set { }
        }

    public string dichargeDate
        {
            get { }
            set { }
        }

    public string editedOn
        {
            get { }
            set { }
        }

    public string editedBy
        {
            get { }
            set { }
        }
        
        


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
