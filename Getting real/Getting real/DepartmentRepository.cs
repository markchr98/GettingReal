using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_real
{
    class DepartmentRepository
    {
        private List<Department> departmentList = new List<Department>();

        public List<Department> GetDepartments()
        {
            return departmentList;
        }
        public void AddDepartment(Department department)
        {
            departmentList.Add(department);
        }
    }
}
