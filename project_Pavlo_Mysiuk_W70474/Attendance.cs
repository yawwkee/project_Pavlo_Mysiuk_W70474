using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Pavlo_Mysiuk_W70474
{
    using System;

    namespace SchoolManagement
    {
        public class Attendance
        {
            public DateTime Date { get; set; }
            public bool IsPresent { get; set; }

            public Attendance(DateTime date, bool isPresent)
            {
                Date = date;
                IsPresent = isPresent;
            }
        }
    }

}
