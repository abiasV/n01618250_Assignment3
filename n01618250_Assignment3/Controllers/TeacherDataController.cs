using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//use class Teacher
using n01618250_Assignment3.Models;
// use MySQL.Data
using MySql.Data.MySqlClient;
using System.Web.Http.Cors;
using System.Globalization;
using System.Security.Cryptography;

namespace n01618250_Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();
        /// <summary>
        /// returns a list of teachers in the database 
        /// </summary>
        /// <returns>
        /// A list of teacher objects
        /// </returns>
        /// <param name="SearchKey"></param>
        /// <example>show all the record of teacher table in school database
        /// Get /api/TeacherData/ListTeachers => [{"TeacherId":"3", "TeacherFName":"Linda", "TeacherLName":"Chan", "EmployeeNumber": "T382", "HireDate": "2015-08-22", "Salary":"60.22"}]
        /// [{"TeacherId":"8", "TeacherFName":"Dana", "TeacherLName":"Ford", "EmployeeNumber": "T401", "HireDate": "2014-06-26", "Salary":"71.15"}]
        /// </example>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        [EnableCors(origins:"*", methods:"*", headers:"*")]
        public List<Teacher> ListTeachers(string SearchKey=null)
        {
            //create a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Create a command
            MySqlCommand cmd = Conn.CreateCommand();

            //command text SQL QUERY
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname,' ', teacherlname)) like lower(@key) or hiredate like @key or salary like @key ";
            cmd.Parameters.AddWithValue("@key" , "%" + SearchKey + "%");
            cmd.Prepare();

            //Get a Result Set for our response
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Teachers = new List<Teacher>();

            while (ResultSet.Read())
            {
                //get the teacher id
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                //get the teacher first name from database
                string TeacherFName = ResultSet["teacherfname"].ToString();
                //get the teacher last name from DB
                string TeacherLName = ResultSet["teacherlname"].ToString();
                //get the teacher employee number
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                //get the time that the teacher has been hired
                string HireDate = ResultSet["hiredate"].ToString();
                //string formattedDate = HireDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                //get the salary that the teacher receives
                string Salary = ResultSet["salary"].ToString();

                //create a teacher object
                Teacher NewTeacher = new Teacher();
                //set the information for the object
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFName = TeacherFName;
                NewTeacher.TeacherLName = TeacherLName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate.ToString();
                NewTeacher.Salary = Salary;

                // Add it to the Teachers list
                Teachers.Add(NewTeacher);
            }

            //close the connection
            Conn.Close();

            //return the Teachers
            return Teachers;

        }
        /// <summary>
        /// Find a Teacher by the input teacher id
        /// </summary>
        /// <param name="TeacherId">The teacherid primary in the database</param>
        /// <returns>A teacher object</returns>
        /// <example>show only one record of the specified teacherid
        /// Get api/TeacherData/FindTeacher/3 => [{"TeacherId":"3", "TeacherFName":"Linda", "TeacherLName":"Chan", "EmployeeNumber": "T382", "HireDate": "2015-08-22", "Salary":"60.22"}]
        /// </example>

        ///<example> GET /api/TeacherData/FindTeacher/{TeacherId} </example>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{TeacherId}")]
        public Teacher FindTeacher(int TeacherId)
        {
            //create a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Create a command
            MySqlCommand Command = Conn.CreateCommand();

            //command text SQL QUERY
            Command.CommandText = "Select * from teachers where teacherid = " + TeacherId;

            //Get a Result Set for our response
            MySqlDataReader ResultSet = Command.ExecuteReader();

            Teacher SelectedTeachers = new Teacher();

            while (ResultSet.Read())
            {
                //get the time that the teacher has been hired
                //DateTime HireDate = (DateTime)(ResultSet["hiredate"]);
                //string formattedDate = HireDate.ToString("dd-MMM-yyyy");

                //set the information for the object
                SelectedTeachers.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeachers.TeacherFName = ResultSet["teacherfname"].ToString();
                SelectedTeachers.TeacherLName = ResultSet["teacherlname"].ToString();
                SelectedTeachers.EmployeeNumber = ResultSet["employeenumber"].ToString();
                SelectedTeachers.HireDate = ResultSet["hiredate"].ToString();
                SelectedTeachers.Salary = ResultSet["salary"].ToString();

            }

            //close the connection
            Conn.Close();

            //return the Teachers
            return SelectedTeachers;

        }

        
        /// <example>POST: /api/TeacherData/DeleteTeacher/{1}</example>
        /// This method deletes a teacher from the database based on the provided teacher ID.
        /// <summary>
        /// Deletes a teacher from the database based on the provided teacher ID.
        /// </summary>
        /// <param name="id">The ID of the teacher to be deleted from the database.</param>
        /// <example>
        /// POST /api/TeacherData/DeleteTeacher/5
        /// </example>
        /// <returns>HTTP status code 200 OK upon successful deletion of the teacher</returns>

        [HttpPost]
        public void DeleteTeacher(int id)
        {
            // create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command(query) for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete FROM teachers WHERE teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Adds a new teacher to the database.
        /// </summary>
        /// <param name="NewTeacher">The Teacher object containing details of the new teacher to be added</param>
        /// <returns>HTTP status code 200 OK if the teacher is successfully added</returns>
        /// If the data is valid, the method inserts the teacher's details, including first name, last name, employee number, and salary into the teachers table.
        /// <example>
        /// POST /api/TeacherData/AddTeacher
        /// FORM DATA / POST DATA:
        /// {
        ///     "TeacherFName": "Abbas",
        ///     "TeacherLName": "Vaziri",
        ///     "EmployeeNumber": "T123",
        ///     "Salary": "22000",
        ///     "HireDate": "2023-12-04"
        /// }
        /// </example>
        /// <example>
        /// curl -d @teacher.json -H "Content-Type: application/json" http://localhost:14272/api/TeacherData/addTeacher 
     
        /// curl -d "{\"TeacherFName\": \"Abbas\", \"TeacherLName\": \"Vaziri\", \"EmployeeNumber\": \"T123\", \"Salary\": \"22000\", \"HireDate\": \"2023-12-04\"}" -H "Content-Type: application/json" http://localhost:14272/api/TeacherData/addTeacher
        /// curl -d @teacher.json
        /// </example>

        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            //assume that the information is received correctly
            //contact the database and execute a query
            //insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values ('ali', 'koon', 'T201', '2023-12-04', '66.65');

            // server validation, the validation that happens on the server
            //if (NewTeacher.TeacherFName == null) return;

            //create a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Create a command
            MySqlCommand Command = Conn.CreateCommand();
            string query = "insert into teachers(teacherfname, teacherlname, employeenumber, hiredate, salary) values (@teacherfname, @teacherlname, @employeenumber, @hiredate, @salary)";
            //command text SQL QUERY
            Command.CommandText = query;
            //put new teacher info into this query
            Command.Parameters.AddWithValue("@teacherfname", NewTeacher.TeacherFName);
            Command.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLName);
            Command.Parameters.AddWithValue("@employeenumber", NewTeacher.EmployeeNumber);
            Command.Parameters.AddWithValue("@hiredate", NewTeacher.HireDate);
            Command.Parameters.AddWithValue("@salary", NewTeacher.Salary);
            Command.Prepare();

            Command.ExecuteNonQuery();
            Conn.Close();
        }
    }
}
