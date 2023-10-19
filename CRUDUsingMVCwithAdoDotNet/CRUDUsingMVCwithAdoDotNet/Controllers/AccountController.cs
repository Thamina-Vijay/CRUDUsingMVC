using CRUDUsingMVC.Models;
using CRUDUsingMVC.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDUsingMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
 
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel LoginView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AccountRepository AccountRepo = new AccountRepository();
                    if (AccountRepo.LoginDetails(LoginView))
                    {
                        ViewBag.Message = "Login details added successfully";
                    }

                }
                
                return RedirectToAction("Index", "Student");
                
            }
            catch
            {
                return View();
            }
            
        }
        //GET METHOD FOR PASSWORD
        public ActionResult ForgotPassword()
        {
            AccountRepository AccountRepo = new AccountRepository();
            List<RegisterViewModel> ListUsernames = AccountRepo.PopulateUserNames();
            ViewBag.UserNames= new SelectList(ListUsernames, "Id", "UserName");
            return View();
        }


        [HttpPost]
        public ActionResult ForgotPassword(string userId,string newPwd,string cfmPwd)
        {
            ForgotPwdModel PWDView = new ForgotPwdModel();
            PWDView.UserId= userId;
            PWDView.NewPassword = newPwd;
            PWDView.ConfirmPassword = cfmPwd;
            try
            {
                if (ModelState.IsValid)
                {
                    AccountRepository AccountRepo = new AccountRepository();
                    if (AccountRepo.UpdateForgotPassword(PWDView))
                    {
                        ViewBag.Message = "ForgotPassword details added successfully";
                    }

                }
                return Json("chamara", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("chamara", JsonRequestBehavior.AllowGet);
            }

        }




        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel RegisterView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AccountRepository AccountRepo = new AccountRepository();

                    if (AccountRepo.Register(RegisterView))
                    {
                        ViewBag.Message = "Register details added successfully";
                    }
                }

                return RedirectToAction("Login", "Account");

                
            }
            catch
            {
                return View();
            }

           

        }
        


    }


}