using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace project_Pavlo_Mysiuk_W70474
{
    namespace SchoolManagement
    {
    public class Grade
    {
        public string Subject { get; set; }
        public double Value { get; set; }

        public Grade(string subject, double value)
        {
            Subject = subject;
            Value = value;
        }
    }
}

}
