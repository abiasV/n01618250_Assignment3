using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace n01618250_Assignment3.Models
{
    public class Teacher
    {
        //teacher id
        public int TeacherId { get; set; }

        //teacher First Name
        public string TeacherFName { get; set; }

        //teacher Last Name
        public string TeacherLName { get; set; }

        //teacher employee number
        public string EmployeeNumber { get; set; }

        //teacher hire date
        public string HireDate { get; set; }

        //teacher salary
        public string Salary { get; set; }
    }
}