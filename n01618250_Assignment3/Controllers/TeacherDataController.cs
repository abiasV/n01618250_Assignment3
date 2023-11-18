﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//use class Teacher
using n01618250_Assignment3.Models;
// use MySQL.Data
using MySql.Data.MySqlClient;
using System.Globalization;

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
        /// <example>show all the record of teacher table in school database
        /// Get /api/TeacherData/ListTeachers => [{"TeacherId":"3", "TeacherFName":"Linda", "TeacherLName":"Chan", "EmployeeNumber": "T382", "HireDate": "2015-08-22", "Salary":"60.22"}]
        /// [{"TeacherId":"8", "TeacherFName":"Dana", "TeacherLName":"Ford", "EmployeeNumber": "T401", "HireDate": "2014-06-26", "Salary":"71.15"}]
        /// </example>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]

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
                DateTime HireDate = (DateTime)(ResultSet["hiredate"]);
                string formattedDate = HireDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                //get the salary that the teacher receives
                string Salary = ResultSet["salary"].ToString();

                //create a teacher object
                Teacher NewTeacher = new Teacher();
                //set the information for the object
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFName = TeacherFName;
                NewTeacher.TeacherLName = TeacherLName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = formattedDate;
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
                DateTime HireDate = (DateTime)(ResultSet["hiredate"]);
                string formattedDate = HireDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                //set the information for the object
                SelectedTeachers.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeachers.TeacherFName = ResultSet["teacherfname"].ToString();
                SelectedTeachers.TeacherLName = ResultSet["teacherlname"].ToString();
                SelectedTeachers.EmployeeNumber = ResultSet["employeenumber"].ToString();
                SelectedTeachers.HireDate = formattedDate;
                SelectedTeachers.Salary = ResultSet["salary"].ToString();

            }

            //close the connection
            Conn.Close();

            //return the Teachers
            return SelectedTeachers;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example>POST: /api/TeacherData/DeleteTeacher/{1}</example>
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
    }
}
