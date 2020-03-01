using System;
using System.Collections.Generic;
using System.Text;
using CampFinder.Models;
using CampFinder.ViewModels;
using CampFinder.Repositories;

namespace CampFinder.Managers
{
    public class PersonManager
    {
        public Person MapViewModelToPerson(PersonViewModel person)
        {
            return new Person
            {
                Id = Guid.NewGuid(),
                FirstName = person.FirstName,
                LastName = person.LastName,
                MailAdress = person.MailAdress,
                TelephoneNumber = person.TelephoneNumber
            };
        }

        public PersonViewModel MapPersonToViewModel(Person person)
        {
            return new PersonViewModel
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                MailAdress = person.MailAdress,
                TelephoneNumber = person.TelephoneNumber
            };
        }
    }
}
