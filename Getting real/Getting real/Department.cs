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
        private JObject departmentResponse;
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
    }
}
