using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01618250_Assignment3.Models;

namespace n01618250_Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: /Teacher or /Teacher/Index
        public ActionResult Index()
        {
            return View();
        }

        // Get: /Teacher/List
        // Browser opens a list teacher page
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        // Get /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher TeacherSelected = controller.FindTeacher(id);

            // You should definitely not use SelectedTeachers in TeacherDataController file
            return View(TeacherSelected);
        }

        // Get /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher TeacherSelected = controller.FindTeacher(id);

            // You should definitely not use SelectedTeachers in TeacherDataController file
            return View(TeacherSelected);
        }

        //Post : /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        // Get : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        // POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string TeacherEmployeeNo, string TeacherSalary) 
        {
            //Identify that this method is running
            //Identify the inputs provided from the form
            Debug.WriteLine("Create method");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherEmployeeNo);
            Debug.WriteLine(TeacherSalary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFName = TeacherFname;
            NewTeacher.TeacherLName = TeacherLname;
            NewTeacher.EmployeeNumber = TeacherEmployeeNo;
            NewTeacher.Salary = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
    }
}