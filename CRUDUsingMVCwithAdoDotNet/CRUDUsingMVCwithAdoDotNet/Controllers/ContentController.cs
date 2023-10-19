using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CRUDUsingMVC.Models;
using CRUDUsingMVC.Repositories;
using CRUDUsingMVC.ViewModel;

namespace CRUDUsingMVC.Controllers
{
  [RoutePrefix("Content")]
    [ValidateInput(false)]
    public class ContentController : Controller
    {
        //private DBContext db = new DBContext();
        /// <summary>
        /// Retrive content from database 
        /// </summary>  
        /// <returns></returns>
        [Route("Index")]
        [HttpGet]
        public ActionResult Index()
        {

            ContentRepository cs = new ContentRepository();

            var contentModel = cs.GetAllImage();

            return View(contentModel);
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

            ContentRepository cs = new ContentRepository();
            
            var q  = cs.DisplayImage(Id);

            
                
                //from temp in db.Contents where temp.ID == Id select temp.Image;
            byte[] cover = q;
            return cover;
        }

        [HttpGet]
        public ActionResult Create()
        {
            ContentViewModel model = new ContentViewModel();
            return View();
        }
        /// <summary>
        /// Save content and images
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public ActionResult Create(ContentViewModel model)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            ContentRepository service = new ContentRepository();
            int i = service.UploadImageInDataBase(file, model);
            //if (i == 1)
            //{
            //    return RedirectToAction("Index");
            //}
            return View(model);
        }
	}
}