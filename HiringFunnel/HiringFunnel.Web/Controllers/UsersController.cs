using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using HiringFunnel.Data;
using HiringFunnel.Data.DAL;
using HiringFunnel.Data.Models;
using HiringFunnel.Web.ViewModels;

namespace HiringFunnel.Web.Controllers
{

    [LoggedIn]
    public class UsersController : Controller
    {

        [HttpGet]
        [HRLevel]
        public ActionResult UserMgmt()
        {
            return View();
        }

        [HttpGet]
        [HRLevel]
        public ActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        [HRLevel]
        [ValidateAntiForgeryToken]
        public ActionResult NewUser(FormCollection newData)
        {
            SHA512Managed sha512 = new SHA512Managed();
            byte[] hashB = sha512.ComputeHash(Encoding.UTF8.GetBytes(newData["Password"]));
            string hashStr = BitConverter.ToString(hashB).Replace("-", string.Empty).ToLower();
            
            using (HFContext hfdb = new HFContext())
            {
                ICollection<Technology> userTechs = new Collection<Technology>();

                if (newData["Technologies"] != null)
                {
                    int[] userTechIds = newData["Technologies"].Split(',').Select(tIds => int.Parse(tIds)).ToArray();

                    foreach (int tId in userTechIds)
                    {
                        Technology uTech = hfdb.Technologies.Find(tId);
                        userTechs.Add(uTech);
                    }
                }

                User newUser = new User
                {
                    FirstName = newData["FirstName"],
                    LastName = newData["LastName"],
                    Email = newData["Email"],
                    PasswordHash = hashStr,
                    Seniority = (SeniorityLevel)int.Parse(newData["Seniority"]),
                    Role = (UserLevel)int.Parse(newData["Role"]),
                    Technologies = userTechs
                };

                hfdb.Users.Add(newUser);
                hfdb.SaveChanges();
            }
            
            return RedirectToAction("UserMgmt");
        }

        [HttpGet]
        public ActionResult UserDetails(int Id)
        {
            User detailUser = null;
            UserInfo detailUserView = null;

            using (HFContext hfdb = new HFContext())
            {
                detailUser = hfdb.Users.Find(Id);
                detailUserView = ModelConverter.ConvertUser(detailUser, hfdb);
            }

            return View(detailUserView);
        }

        [HttpPost]
        [AdminLevel]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(FormCollection editData)
        {
            int editUserId = int.Parse(editData["editUserId"]);

            using (HFContext hfdb = new HFContext())
            {
                User editUser = hfdb.Users.Find(editUserId);

                if (editData["Technologies"] != null)
                {
                    int[] userTechIds = editData["Technologies"].Split(',').Select(tIds => int.Parse(tIds)).ToArray();

                    editUser.Technologies.Clear();
                    foreach (int tId in userTechIds)
                    {
                        Technology uTech = hfdb.Technologies.Find(tId);
                        editUser.Technologies.Add(uTech);
                    }
                }
                
                editUser.FirstName = editData["FirstName"];
                editUser.LastName = editData["LastName"];
                editUser.Email = editData["Email"];
                editUser.Seniority = (SeniorityLevel)int.Parse(editData["Seniority"]);
                editUser.Role = (UserLevel)int.Parse(editData["Role"]);

                hfdb.SaveChanges(); 
            }
            
            return RedirectToAction("UserMgmt");
        }

        [HttpGet]
        [AdminLevel]
        public ActionResult DeleteUser(int Id)
        {
            using (HFContext hfdb = new HFContext())
            {
                User deleteUser = hfdb.Users.Find(Id);

                deleteUser.Deleted = true;
                hfdb.SaveChanges();
            }
            
            return RedirectToAction("UserMgmt");
        }

    }
}

