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

        public string PatientId
        {
            get { }
            set { }
        }

        public string Firstname
        {
            get { }
            set { }
        }

       public string Lastname
        {
            get { }
            set { }
        }

      public string BirthDate
        {
            get { }
            set { }
        }

    public string DepartmentId
        {
            get { }
            set { }
        }

    public string PatientNumber
        {
            get { }
            set { }
        }

    public string EntryDate
        {
            get { }
            set { }
        }

    public string DichargeDate
        {
            get { }
            set { }
        }

    public string EditedOn
        {
            get { }
            set { }
        }

    public string EditedBy
        {
            get { }
            set { }
        }
        
        


        public Patient()
        {

        }

        public List<Patient> GetNewPatientResponse(string ip)
        {
            using (WebClient client = new WebClient())
            {
                List<Patient> patients = new List<Patient>();
                string URL = "http://" + ip + ":5000/api/Patients";
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + Controller.token.AccessToken;

                JArray response = JArray.Parse(client.DownloadString(URL));

                foreach (var patient in response.Children())
                {
                    patients.Add(new Patient()
                    {
                        patientId = (string)patient["patientId"],
                        firstname = (string)patient["firstname"],
                        lastname = (string)patient["lastname"],
                        birthDate = (string)patient["birthDate"],
                        departmentId = (string)patient["departmentId"],
                        patientNumber = (string)patient["patientNumber"],
                        entryDate = (string)patient["entryDate"],
                        dichargeDate = (string)patient["dichargeDate"],
                        editedOn = (string)patient["editedOn"],
                        editedBy = (string)patient["editedBy"]
                    });
                }

                return patients;
            }
        }
    }
}
