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
            get { return patientId; }
            set { patientId = value; }
        }

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

       public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

      public string BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

    public string DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value; }
        }

    public string PatientNumber
        {
            get { return patientNumber; }
            set { patientNumber = value; }
        }

    public string EntryDate
        {
            get { return entryDate; }
            set { entryDate = value; }
        }

    public string DichargeDate
        {
            get { return dichargeDate; }
            set { dichargeDate = value; }
        }

    public string EditedOn
        {
            get { return editedOn; }
            set { editedOn = value; }
        }

    public string EditedBy
        {
            get { return editedBy; }
            set { editedBy = value; }
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
