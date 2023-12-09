using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace n01618250_Assignment3.Models
{
    public class Course
    {
        public int ClassId { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; } // Add this property to hold the class name
    }
}