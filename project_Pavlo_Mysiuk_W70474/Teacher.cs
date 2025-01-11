using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Pavlo_Mysiuk_W70474
{
    namespace SchoolManagement
    {
        public class Teacher
        {
            public string Id { get; private set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public List<string> Subjects { get; set; } = new List<string>();
            public Teacher(string id, string firstName, string lastName)
            {
                Id = id;
                FirstName = firstName;
                LastName = lastName;
            }

            public void AddSubject(string subject)
            {
                if (!Subjects.Contains(subject))
                {
                    Subjects.Add(subject);
                    Console.WriteLine($"Dodano przedmiot {subject} dla nauczyciela {FirstName} {LastName}");
                }
                else
                {
                    Console.WriteLine($"Nauczyciel {FirstName} {LastName} już uczy przedmiotu {subject}");
                }

            }
        }

    }
}
