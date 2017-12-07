using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Getting_real
{
    class Department
    {
        private List<Patient> PatientList = new List<Patient>();
        private string departmentId;
        private string departmentName;
        public string DepartmentId
        {
            get { return departmentId; }
            set { this.departmentId = value; }
        }

        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }
        public Department()
        {

        }        

        public List<Patient> GetPatientList()
        {
            return PatientList;
        }
        public void AddPatient(Patient patient)
        {
            PatientList.Add(patient);
        }
        public static List<Department> GetNewDeparments(string ip)
        {
            using (WebClient client = new WebClient())
            {
                List<Department> departments = new List<Department>();
                string URL = "http://" + ip + ":5000/api/Departments";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + Controller.token.AccessToken;
                JArray response = JArray.Parse(client.DownloadString(URL));
                
                foreach (var department in response.Children())
                {
                    departments.Add(new Department()
                    {
                        departmentId = (string)department["departmentId"],
                        departmentName = (string)department["name"]
                    });
                }
                return departments;
            }
        }
    }
}
