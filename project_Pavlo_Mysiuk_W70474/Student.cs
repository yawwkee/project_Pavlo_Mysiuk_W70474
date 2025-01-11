using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace project_Pavlo_Mysiuk_W70474
{

    namespace SchoolManagement
    {
        public class Student
        {
            public string Id { get; private set; } 
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ClassName { get; set; }
            public List<Grade> Grades { get; set; } = new List<Grade>();
            public List<Attendance> Attendances { get;  set; } = new List<Attendance>();

            public Student(string id, string firstName, string lastName, string className)
            {
                Id = id; 
                FirstName = firstName;
                LastName = lastName;
                ClassName = className;
            }

            public void AddGrade(string subject, double grade)
            {
                Grades.Add(new Grade(subject, grade));
            }

            public void RegisterAttendance(DateTime date, bool isPresent)
            {
                Attendances.Add(new Attendance(date, isPresent));
            }

            public double GetAverageGrade()
            {
                if (Grades.Count == 0) return 0.0;
                return Grades.Average(g => g.Value);
            }
        }

    }

}
