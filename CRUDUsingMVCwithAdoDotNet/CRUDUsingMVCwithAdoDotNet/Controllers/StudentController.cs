using CRUDUsingMVC.Models;
using CRUDUsingMVC.Repositories;
using CRUDUsingMVC.Repository;
using CRUDUsingMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDUsingMVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            StdRepository stdRepo = new StdRepository();
            //ModelState.Clear();
            return View(stdRepo.GetAllStudent());
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            StdRepository stdRepo = new StdRepository();
            //ModelState.Clear();
            return View(stdRepo.GetAllStudent().Find(std => std.Id == id));
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }
        



        // POST: Student/Create
        [HttpPost]
        public ActionResult Student(StudentModel std)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StdRepository stdRepo = new StdRepository();

                    if (stdRepo.Create(std))
                    {
                        ViewBag.Message = "Student details added successfully";
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            StdRepository stdRepo = new StdRepository();
            //ModelState.Clear();
            return View(stdRepo.GetAllStudent().Find(std => std.Id == id));
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, StudentModel obj)
        {
            try
            {
                StdRepository stdRepo = new StdRepository();
                stdRepo.UpdateStudent(obj);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            StdRepository stdRepo = new StdRepository();
            //ModelState.Clear();
            return View(stdRepo.GetAllStudent().Find(std => std.Id == id));
        }
            // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, StudentModel obj)
        {
            try
            {
                StdRepository stdRepo = new StdRepository();
                if (stdRepo.DeleteStudent(id))
                {
                    ViewBag.AlertMsg = "Student details deleted successfully";

                }
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
    }
}
