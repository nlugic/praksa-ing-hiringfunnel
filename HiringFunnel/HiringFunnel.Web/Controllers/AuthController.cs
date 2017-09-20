using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using HiringFunnel.Data;
using HiringFunnel.Data.DAL;
using HiringFunnel.Data.Models;

namespace HiringFunnel.Web.Controllers
{
    public class AuthController : Controller
    {

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.errorMessage = TempData["errorMessage"];

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection loginData)
        {
            string email = loginData["Email"];
            string pass = loginData["Password"];

            SHA512Managed sha512 = new SHA512Managed();
            byte[] hashB = sha512.ComputeHash(Encoding.UTF8.GetBytes(pass));
            string hashStr = BitConverter.ToString(hashB).Replace("-", string.Empty).ToLower();

            using (HFContext hfdb = new HFContext())
            {
                User login = (from usr in hfdb.Users
                              where (usr.Email == email && usr.PasswordHash == hashStr)
                              select usr).SingleOrDefault();

                if (login != null)
                {
                    Session["login"] = login;
                    if (TempData["returnUrl"] != null)
                        return Redirect(TempData["returnUrl"].ToString());
                    switch (login.Role)
                    {
                        case UserLevel.Admin: return RedirectToAction("UserMgmt", "Users");
                        case UserLevel.HR: return RedirectToAction("ContactMgmt", "Contacts");
                        case UserLevel.Interviewer: return RedirectToAction("ProcessMgmt", "Processes");
                    }
                }
                else
                    ViewBag.errorMessage = "Pogrešno korisničko ime i/ili lozinka.";
            }

            return View();
        }

        [HttpGet]
        [LoggedIn]
        public ActionResult Logout()
        {
            Session["login"] = null;

            return RedirectToAction("Login");
        }

        [HttpPost]
        [LoggedIn]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(FormCollection passwordData)
        {
            int changePassId = int.Parse(passwordData["changePassId"]);

            using (HFContext hfdb = new HFContext())
            {
                User changePassUser = hfdb.Users.Find(changePassId);

                if (passwordData["Password"] != string.Empty)
                {
                    SHA512Managed sha512 = new SHA512Managed();
                    byte[] hashB = sha512.ComputeHash(Encoding.UTF8.GetBytes(passwordData["Password"]));
                    string hashStr = BitConverter.ToString(hashB).Replace("-", string.Empty).ToLower();

                    changePassUser.PasswordHash = hashStr;

                    hfdb.SaveChanges();
                }
            }

            return RedirectToAction("UserMgmt", "Users");
        }

    }
}