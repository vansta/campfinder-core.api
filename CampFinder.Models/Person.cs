using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailAdress { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
