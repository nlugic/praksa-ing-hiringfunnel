using System;
using System.Linq;
using System.Web.Mvc;
using HiringFunnel.Data;
using HiringFunnel.Data.DAL;
using HiringFunnel.Data.Models;
using HiringFunnel.Web.ViewModels;

namespace HiringFunnel.Web.Controllers
{

    [LoggedIn]
    public class ProcessesController : Controller
    {
        
        [HttpGet]
        public ActionResult ProcessMgmt()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProcessDetails(int Id)
        {
            Process detailProcess = null;
            ProcessInfo detailProcessView = null;

            using (HFContext hfdb = new HFContext())
            {
                detailProcess = hfdb.Processes.Find(Id);
                detailProcessView = ModelConverter.ConvertProcess(detailProcess);
            }

            if (detailProcessView.EndDate != null)
                ViewBag.processEdit = "disabled";

            return View(detailProcessView);
        }
        
        [HttpPost]
        [AjaxOnly]
        public JsonResult GetProcessesForSelect()
        {
            IQueryable<Process> contactProcesses = null;
            int contactId = int.Parse(Request.Form["contactId"]);

            using (HFContext hfdb = new HFContext())
            {
                contactProcesses = from proc in hfdb.Processes
                                   where proc.EndDate == null
                                       && proc.Instances.FirstOrDefault(pIns => pIns.ContactId == contactId) == null
                                   select proc;
                
                var data = contactProcesses.Select(proc => new
                {
                    Id = proc.Id,
                    Name = proc.Name
                }).ToList();

                return Json(data);
            }
        }

        [HttpGet]
        [HRLevel]
        public PartialViewResult NewProcess()
        {
            return PartialView();
        }
        
        [HttpPost]
        [HRLevel]
        public ActionResult NewProcess(FormCollection newData)
        {
            int addedId = 0;
            
            using (HFContext hfdb = new HFContext())
            {
                string processTechs = string.Empty;

                if (newData["Technologies"] != null)
                {
                    int[] processTechIds = newData["Technologies"].Split(',').Select(tIds => int.Parse(tIds)).ToArray();

                    for (int i = 0; i < processTechIds.Length; ++i)
                    {
                        Technology pTech = hfdb.Technologies.Find(processTechIds[i]);
                        processTechs += pTech.Name + ((i != processTechIds.Length - 1) ? ", " : "");
                    }
                }

                Process newProcess = new Process
                {
                    Name = newData["Name"],
                    Technologies = processTechs,
                    Seniority = (SeniorityLevel)int.Parse(newData["Seniority"]),
                    StartDate = DateTime.Now
                };

                hfdb.Processes.Add(newProcess);
                hfdb.SaveChanges();

                addedId = newProcess.Id;
            }

            return RedirectToAction("ProcessDetails", new { Id = addedId });
        }
        
        [HttpPost]
        [HRLevel]
        [ValidateAntiForgeryToken]
        public ActionResult EditProcess(FormCollection editData)
        {
            int editId = int.Parse(editData["editProcessId"]);

            using (HFContext hfdb = new HFContext())
            {
                Process editProcess = hfdb.Processes.Find(editId);

                int[] processTechIds = null;

                if (editData["Technologies"] != null)
                    processTechIds = editData["Technologies"].Split(',').Select(tIds => int.Parse(tIds)).ToArray();

                editProcess.Technologies = string.Empty;

                for (int i = 0; i < processTechIds.Length; ++i)
                {
                    Technology pTech = hfdb.Technologies.Find(processTechIds[i]);
                    editProcess.Technologies += pTech.Name + ((i != processTechIds.Length - 1) ? ", " : "");
                }

                editProcess.Name = editData["Name"];
                editProcess.Seniority = (SeniorityLevel)int.Parse(editData["Seniority"]);

                hfdb.SaveChanges();
            }
            
            return RedirectToAction("ProcessMgmt");
        }

        [HttpPost]
        [HRLevel]
        [AjaxOnly]
        public JsonResult EndProcess()
        {
            int processId = int.Parse(Request.Form["processId"]);

            using (HFContext hfdb = new HFContext())
            {
                Process endProcess = hfdb.Processes.Find(processId);
                endProcess.EndDate = DateTime.Now;

                hfdb.SaveChanges();
            }

            return Json(new { success = true });
        }

    }
}
