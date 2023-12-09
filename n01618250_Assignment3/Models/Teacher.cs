using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace n01618250_Assignment3.Models
{
    public class Teacher
    {
        //teacher id
        public int TeacherId { get; set; }

        //teacher First Name
        // Properties with validation attributes
        [Required(ErrorMessage = "First name is required")]
        public string TeacherFName { get; set; }

        //teacher Last Name
        [Required(ErrorMessage = "Last name is required")]
        public string TeacherLName { get; set; }

        //teacher employee number
        [Required(ErrorMessage = "Employee number is required")]
        public string EmployeeNumber { get; set; }

        //teacher hire date
        [Required(ErrorMessage = "Hire Date is required")]
        public string HireDate { get; set; }

        //teacher salary
        [Required(ErrorMessage = "Salary is required")]
        public string Salary { get; set; }

        public List<Course> TaughtCourses { get; set; } = new List<Course>();
    }
}