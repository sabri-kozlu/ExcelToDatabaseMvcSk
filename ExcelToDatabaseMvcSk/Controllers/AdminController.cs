using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelToDatabaseMvcSk.Models;
using Microsoft.SharePoint.Client.UserProfiles;
using UserProfile = ExcelToDatabaseMvcSk.Models.UserProfile;

namespace ExcelToDatabaseMvcSk.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserProfile objUser)
        {
            if (ModelState.IsValid)
            {
                using (ExcelImportDBEntities db = new ExcelImportDBEntities())
                {
                    var obj = db.UserProfile.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserId.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {

                
                return RedirectToAction("Index","Home");
            }

            else
            {
                return RedirectToAction("Login");
             
            }

          
        }
    }
}