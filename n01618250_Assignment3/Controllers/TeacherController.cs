using System;
using System.Collections.Generic;
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
    }
}