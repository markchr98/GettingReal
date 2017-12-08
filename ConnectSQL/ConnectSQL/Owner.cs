using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectSQL
{
    class Owner
    {
        private string id;
        private string lastName;
        private string firstName;
        private string phone;
        private string email;

        public string Id
        {
            get { return id; } set { id = value; }
        }
        public string LastName
        {
            get { return lastName; } set { lastName = value; }
        }
        public string FirstName
        {
            get {return firstName; } set { firstName = value; }
        }
        public string Phone
        {
            get { return phone; } set { phone = value; }
        }
        public string Email
        {
            get { return email; } set {email = value; }
        }

    }
}
