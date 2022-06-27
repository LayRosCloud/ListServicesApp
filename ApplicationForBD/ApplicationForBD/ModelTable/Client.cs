using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class Client
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string GenderCode { get; set; }
        public string PhotoPath { get; set; }
        public Client(int iD, string firstName, string lastName, string patronymic, DateTime birthday, DateTime registrationDate, string email, string phone,string genderCode, string photoPath)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Birthday = birthday;
            RegistrationDate = registrationDate;
            Email = email;
            Phone = phone;
            GenderCode = genderCode;
            PhotoPath = photoPath;
        }
    }
}
