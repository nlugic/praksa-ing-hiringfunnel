using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using HiringFunnel.Data;
using HiringFunnel.Data.DAL;
using HiringFunnel.Data.Models;
using HiringFunnel.Web.ViewModels;

namespace HiringFunnel.Web.Controllers
{

    [LoggedIn]
    public class ContactsController : Controller
    {

        [HttpGet]
        public ActionResult ContactMgmt()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ContactDetails(int Id)
        {
            Contact detailContact = null;
            ContactInfo detailContactView = null;

            using (HFContext hfdb = new HFContext())
            {
                detailContact = hfdb.Contacts.Find(Id);
                detailContactView = ModelConverter.ConvertContact(detailContact);
            }
            
            return View(detailContactView);
        }

        [HttpGet]
        public FileResult OpenContactCV(int Id)
        {
            using (HFContext hfdb = new HFContext())
            {
                Contact cvContact = hfdb.Contacts.Find(Id);

                return File(cvContact.CVContent, cvContact.CVContentType);
            }
        }

        [HttpGet]
        [HRLevel]
        public ActionResult NewContact()
        {
            return View();
        }

        [HttpPost]
        [HRLevel]
        [ValidateAntiForgeryToken]
        public ActionResult NewContact(FormCollection newData)
        {
            using (HFContext hfdb = new HFContext())
            {
                ICollection<Technology> contactTechs = new Collection<Technology>();

                if (newData["Technologies"] != null)
                {
                    int[] contactTechIds = newData["Technologies"].Split(',').Select(tIds => int.Parse(tIds)).ToArray();

                    foreach (int tId in contactTechIds)
                    {
                        Technology cTech = hfdb.Technologies.Find(tId);
                        contactTechs.Add(cTech);
                    }
                }

                Contact newContact = new Contact
                {
                    FirstName = newData["FirstName"],
                    LastName = newData["LastName"],
                    DateOfBirth = DateTime.Parse(newData["DateOfBirth"]),
                    Location = newData["Location"],
                    Email = newData["Email"],
                    Phone = newData["Phone"],
                    LinkedIn = newData["LinkedIn"],
                    CurrentWorkplace = newData["CurrentWorkplace"],
                    Seniority = (SeniorityLevel)int.Parse(newData["Seniority"]),
                    Technologies = contactTechs
                };

                if (Request.Files["CV"] != null)
                    using (BinaryReader br = new BinaryReader(Request.Files["CV"].InputStream))
                    {
                        newContact.CVContent = br.ReadBytes(Request.Files["CV"].ContentLength);
                        newContact.CVFileName = Request.Files["CV"].FileName;
                        newContact.CVContentType = Request.Files["CV"].ContentType;
                    }

                hfdb.Contacts.Add(newContact);
                hfdb.SaveChanges();
            }
            
            return RedirectToAction("ContactMgmt");
        }

        [HttpPost]
        [HRLevel]
        [ValidateAntiForgeryToken]
        public ActionResult EditContact(FormCollection editData)
        {
            int editId = int.Parse(editData["editContactId"]);

            using (HFContext hfdb = new HFContext())
            {
                Contact editContact = hfdb.Contacts.Find(editId);

                if (editData["Technologies"] != null)
                {
                    int[] contactTechIds = editData["Technologies"].Split(',').Select(tIds => int.Parse(tIds)).ToArray();

                    editContact.Technologies.Clear();

                    foreach (int tId in contactTechIds)
                    {
                        Technology cTech = hfdb.Technologies.Find(tId);
                        editContact.Technologies.Add(cTech);
                    }
                }
                
                editContact.FirstName = editData["FirstName"];
                editContact.LastName = editData["LastName"];
                editContact.DateOfBirth = DateTime.Parse(editData["DateOfBirth"]);
                editContact.Location = editData["Location"];
                editContact.Email = editData["Email"];
                editContact.Phone = editData["Phone"];
                editContact.LinkedIn = editData["LinkedIn"];
                editContact.CurrentWorkplace = editData["CurrentWorkplace"];
                editContact.Seniority = (SeniorityLevel)int.Parse(editData["Seniority"]);

                if (Request.Files["CV"] != null)
                    using (BinaryReader br = new BinaryReader(Request.Files["CV"].InputStream))
                    {
                        editContact.CVContent = br.ReadBytes(Request.Files["CV"].ContentLength);
                        editContact.CVFileName = Request.Files["CV"].FileName;
                        editContact.CVContentType = Request.Files["CV"].ContentType;
                    }

                hfdb.SaveChanges();
            }
            
            return RedirectToAction("ContactMgmt");
        }

        [HttpGet]
        [HRLevel]
        public ActionResult DeleteContact(int Id)
        {
            using (HFContext hfdb = new HFContext())
            {
                Contact deleteContact = hfdb.Contacts.Find(Id);
                deleteContact.Deleted = true;

                hfdb.SaveChanges();
            }
            
            return RedirectToAction("ContactMgmt");
        }

    }
}
