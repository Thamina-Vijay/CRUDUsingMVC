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
    [RoutePrefix("UserInfo")]
    [ValidateInput(false)]
    public class UserInfoController : Controller
    {
        [Route("Index")]
        [HttpGet]
        public ActionResult Index()
        {

            UserInfoRepository cs = new UserInfoRepository();

            var UserInfoModel = cs.GetAllImage();

            return View(UserInfoModel);
        }

        /// <summary>
        /// Retrive Image from database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public byte[] GetImageFromDataBase(int Id)
        {

            UserInfoRepository cs = new UserInfoRepository();

            var q = cs.DisplayImage(Id);



            //from temp in db.Contents where temp.ID == Id select temp.Image;
            byte[] cover = q;
            return cover;
        }

        [HttpGet]
        public ActionResult Create()
        {
            UserInfoModel model = new UserInfoModel();
            return View();
        }
        /// <summary>
        /// Save content and images
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public ActionResult Create(UserInfoModel model)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            UserInfoRepository service = new UserInfoRepository();
            int i = service.UploadImageInDataBase(file, model);
            //if (i == 1)
            //{
            //    return RedirectToAction("Index");
            //}
            return View(model);
        }
    }
    // GET: UserInfo


}
