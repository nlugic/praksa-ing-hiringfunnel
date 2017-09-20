using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HiringFunnel.Data;
using HiringFunnel.Data.DAL;
using HiringFunnel.Data.Models;
using HiringFunnel.Web.ViewModels;

namespace HiringFunnel.Web.Controllers
{

    [LoggedIn]
    public class ProcessInstancesController : Controller
    {

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetContactInstances(int processId, int userId)
        {
            ICollection<ProcessInstanceInfo> contacts = null;

            using (HFContext hfdb = new HFContext())
            {
                contacts = (from pIns in hfdb.ProcessInstances
                            where (pIns.ProcessId == processId || (processId == 0 && userId == 0 && pIns.InstanceOf.EndDate == null)
                                   || (processId == 0 && pIns.InstanceOf.EndDate == null && userId != 0
                                   && pIns.Interviewers.FirstOrDefault(intw => intw.InterviewerId == userId) != null))
                                   && !pIns.ContactInProcess.Deleted && pIns.CurrentPhase == Phase.Contact_Phase
                            select new ProcessInstanceInfo
                            {
                                Id = pIns.Id,
                                ProcessName = pIns.InstanceOf.Name,
                                ContactName = pIns.ContactInProcess.FirstName + " " + pIns.ContactInProcess.LastName,
                                ProcessSeniority = pIns.InstanceOf.Seniority.ToString(),
                                ProcessTechnologies = pIns.InstanceOf.Technologies,
                                ContactSeniority = pIns.ContactInProcess.Seniority.ToString(),
                                ContactTechnologies = from cTech in pIns.ContactInProcess.Technologies
                                                      select " " + cTech.Name
                            }).ToList();
            }

            return Json(contacts);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetInterviewInstances(int processId, int userId)
        {
            ICollection<ProcessInstanceInfo> interviews = null;

            using (HFContext hfdb = new HFContext())
            {
                interviews = (from pIns in hfdb.ProcessInstances
                              where (pIns.ProcessId == processId || (processId == 0 && userId == 0 && pIns.InstanceOf.EndDate == null)
                                     || (processId == 0 && pIns.InstanceOf.EndDate == null && userId != 0
                                     && pIns.Interviewers.FirstOrDefault(intw => intw.InterviewerId == userId) != null))
                                     && !pIns.ContactInProcess.Deleted && pIns.CurrentPhase == Phase.Interview_Phase
                              select new ProcessInstanceInfo
                              {
                                  Id = pIns.Id,
                                  ProcessName = pIns.InstanceOf.Name,
                                  ContactName = pIns.ContactInProcess.FirstName + " " + pIns.ContactInProcess.LastName,
                                  ProcessSeniority = pIns.InstanceOf.Seniority.ToString(),
                                  ProcessTechnologies = pIns.InstanceOf.Technologies,
                                  ContactSeniority = pIns.ContactInProcess.Seniority.ToString(),
                                  ContactTechnologies = from cTech in pIns.ContactInProcess.Technologies
                                                        select " " + cTech.Name
                              }).ToList();
            }

            return Json(interviews);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetTestInstances(int processId, int userId)
        {
            ICollection<ProcessInstanceInfo> tests = null;

            using (HFContext hfdb = new HFContext())
            {
                tests = (from pIns in hfdb.ProcessInstances
                         where (pIns.ProcessId == processId || (processId == 0 && userId == 0 && pIns.InstanceOf.EndDate == null)
                                || (processId == 0 && pIns.InstanceOf.EndDate == null && userId != 0
                                && pIns.Interviewers.FirstOrDefault(intw => intw.InterviewerId == userId) != null))
                                && !pIns.ContactInProcess.Deleted && pIns.CurrentPhase == Phase.Test_Phase
                         select new ProcessInstanceInfo
                         {
                             Id = pIns.Id,
                             ProcessName = pIns.InstanceOf.Name,
                             ContactName = pIns.ContactInProcess.FirstName + " " + pIns.ContactInProcess.LastName,
                             ProcessSeniority = pIns.InstanceOf.Seniority.ToString(),
                             ProcessTechnologies = pIns.InstanceOf.Technologies,
                             ContactSeniority = pIns.ContactInProcess.Seniority.ToString(),
                             ContactTechnologies = from cTech in pIns.ContactInProcess.Technologies
                                                   select " " + cTech.Name
                         }).ToList();
            }

            return Json(tests);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetTestDefenseInstances(int processId, int userId)
        {
            ICollection<ProcessInstanceInfo> testDefenses = null;

            using (HFContext hfdb = new HFContext())
            {
                testDefenses = (from pIns in hfdb.ProcessInstances
                                where (pIns.ProcessId == processId || (processId == 0 && userId == 0 && pIns.InstanceOf.EndDate == null)
                                       || (processId == 0 && pIns.InstanceOf.EndDate == null && userId != 0
                                       && pIns.Interviewers.FirstOrDefault(intw => intw.InterviewerId == userId) != null))
                                       && !pIns.ContactInProcess.Deleted && pIns.CurrentPhase == Phase.TestDefense_Phase
                                select new ProcessInstanceInfo
                                {
                                    Id = pIns.Id,
                                    ProcessName = pIns.InstanceOf.Name,
                                    ContactName = pIns.ContactInProcess.FirstName + " " + pIns.ContactInProcess.LastName,
                                    ProcessSeniority = pIns.InstanceOf.Seniority.ToString(),
                                    ProcessTechnologies = pIns.InstanceOf.Technologies,
                                    ContactSeniority = pIns.ContactInProcess.Seniority.ToString(),
                                    ContactTechnologies = from cTech in pIns.ContactInProcess.Technologies
                                                          select " " + cTech.Name
                                }).ToList();
            }

            return Json(testDefenses);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetOfferInstances(int processId, int userId)
        {
            ICollection<ProcessInstanceInfo> offers = null;

            using (HFContext hfdb = new HFContext())
            {
                offers = (from pIns in hfdb.ProcessInstances
                          where (pIns.ProcessId == processId || (processId == 0 && userId == 0 && pIns.InstanceOf.EndDate == null)
                                 || (processId == 0 && pIns.InstanceOf.EndDate == null && userId != 0
                                 && pIns.Interviewers.FirstOrDefault(intw => intw.InterviewerId == userId) != null))
                                 && !pIns.ContactInProcess.Deleted && pIns.CurrentPhase == Phase.Offer_Phase
                          select new ProcessInstanceInfo
                          {
                              Id = pIns.Id,
                              ProcessName = pIns.InstanceOf.Name,
                              ContactName = pIns.ContactInProcess.FirstName + " " + pIns.ContactInProcess.LastName,
                              ProcessSeniority = pIns.InstanceOf.Seniority.ToString(),
                              ProcessTechnologies = pIns.InstanceOf.Technologies,
                              ContactSeniority = pIns.ContactInProcess.Seniority.ToString(),
                              ContactTechnologies = from cTech in pIns.ContactInProcess.Technologies
                                                    select " " + cTech.Name
                          }).ToList();
            }

            return Json(offers);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetAcceptInstances(int processId, int userId)
        {
            ICollection<ProcessInstanceInfo> accepts = null;

            using (HFContext hfdb = new HFContext())
            {
                accepts = (from pIns in hfdb.ProcessInstances
                           where (pIns.ProcessId == processId || (processId == 0 && userId == 0 && pIns.InstanceOf.EndDate == null)
                                  || (processId == 0 && pIns.InstanceOf.EndDate == null && userId != 0
                                  && pIns.Interviewers.FirstOrDefault(intw => intw.InterviewerId == userId) != null))
                                  && !pIns.ContactInProcess.Deleted && pIns.CurrentPhase == Phase.Acceptance_Phase
                           select new ProcessInstanceInfo
                           {
                               Id = pIns.Id,
                               ProcessName = pIns.InstanceOf.Name,
                               ContactName = pIns.ContactInProcess.FirstName + " " + pIns.ContactInProcess.LastName,
                               ProcessSeniority = pIns.InstanceOf.Seniority.ToString(),
                               ProcessTechnologies = pIns.InstanceOf.Technologies,
                               ContactSeniority = pIns.ContactInProcess.Seniority.ToString(),
                               ContactTechnologies = from cTech in pIns.ContactInProcess.Technologies
                                                     select " " + cTech.Name
                           }).ToList();
            }

            return Json(accepts);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetRejectInstances(int processId, int userId)
        {
            ICollection<ProcessInstanceInfo> rejects = null;

            using (HFContext hfdb = new HFContext())
            {
                rejects = (from pIns in hfdb.ProcessInstances
                           where (pIns.ProcessId == processId || (processId == 0 && userId == 0 && pIns.InstanceOf.EndDate == null)
                                  || (processId == 0 && pIns.InstanceOf.EndDate == null && userId != 0
                                  && pIns.Interviewers.FirstOrDefault(intw => intw.InterviewerId == userId) != null))
                                  && !pIns.ContactInProcess.Deleted && pIns.CurrentPhase == Phase.Rejection_Phase
                           select new ProcessInstanceInfo
                           {
                               Id = pIns.Id,
                               ProcessName = pIns.InstanceOf.Name,
                               ContactName = pIns.ContactInProcess.FirstName + " " + pIns.ContactInProcess.LastName,
                               ProcessSeniority = pIns.InstanceOf.Seniority.ToString(),
                               ProcessTechnologies = pIns.InstanceOf.Technologies,
                               ContactSeniority = pIns.ContactInProcess.Seniority.ToString(),
                               ContactTechnologies = from cTech in pIns.ContactInProcess.Technologies
                                                     select " " + cTech.Name
                           }).ToList();
            }

            return Json(rejects);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetQuitInstances(int processId, int userId)
        {
            ICollection<ProcessInstanceInfo> quits = null;

            using (HFContext hfdb = new HFContext())
            {
                quits = (from pIns in hfdb.ProcessInstances
                         where (pIns.ProcessId == processId || (processId == 0 && userId == 0 && pIns.InstanceOf.EndDate == null)
                                || (processId == 0 && pIns.InstanceOf.EndDate == null && userId != 0
                                && pIns.Interviewers.FirstOrDefault(intw => intw.InterviewerId == userId) != null))
                                && !pIns.ContactInProcess.Deleted && pIns.CurrentPhase == Phase.Quit_Phase
                         select new ProcessInstanceInfo
                         {
                             Id = pIns.Id,
                             ProcessName = pIns.InstanceOf.Name,
                             ContactName = pIns.ContactInProcess.FirstName + " " + pIns.ContactInProcess.LastName,
                             ProcessSeniority = pIns.InstanceOf.Seniority.ToString(),
                             ProcessTechnologies = pIns.InstanceOf.Technologies,
                             ContactSeniority = pIns.ContactInProcess.Seniority.ToString(),
                             ContactTechnologies = from cTech in pIns.ContactInProcess.Technologies
                                                   select " " + cTech.Name
                         }).ToList();
            }

            return Json(quits);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetInstancesData() // TESTIRATI OVE USLOVE VISESTRUKO!
        {
            int processId = int.Parse(Request.Form["processId"]);
            int userId = int.Parse(Request.Form["userId"] ?? "0");

            JsonResult contactInstances = GetContactInstances(processId, userId);
            JsonResult interviewInstances = GetInterviewInstances(processId, userId);
            JsonResult testInstances = GetTestInstances(processId, userId);
            JsonResult testDefenseInstances = GetTestDefenseInstances(processId, userId);
            JsonResult offerInstances = GetOfferInstances(processId, userId);
            JsonResult acceptInstances = GetAcceptInstances(processId, userId);
            JsonResult rejectInstances = GetRejectInstances(processId, userId);
            JsonResult quitInstances = GetQuitInstances(processId, userId);

            var data = new
            {
                Contacts = contactInstances.Data,
                Interviews = interviewInstances.Data,
                Tests = testInstances.Data,
                TestDefenses = testDefenseInstances.Data,
                Offers = offerInstances.Data,
                Accepts = acceptInstances.Data,
                Rejects = rejectInstances.Data,
                Quits = quitInstances.Data
            };

            return Json(data);
        }
        
        [HttpPost]
        [AjaxOnly]
        public JsonResult GetProcessInstanceId()
        {
            int contactId = int.Parse(Request.Form["contactId"]);
            int processId = int.Parse(Request.Form["processId"]);

            int pInstanceId = 0;

            using (HFContext hfdb = new HFContext())
            {
                pInstanceId = (from pIns in hfdb.ProcessInstances
                               where pIns.ContactId == contactId && pIns.ProcessId == processId
                               select pIns.Id).SingleOrDefault();
            }

            return Json(new { pInsId = pInstanceId });
        }

        [HttpPost]
        [HRLevel]
        [AjaxOnly]
        public JsonResult AddCandidateToContacts()
        {
            int candidateId = int.Parse(Request.Form["candidateId"]);
            int processId = int.Parse(Request.Form["processId"]);

            using (HFContext hfdb = new HFContext())
            {
                ProcessInstance newProcessInstance = new ProcessInstance
                {
                    CreationDate = DateTime.Now,
                    ContactId = candidateId,
                    ProcessId = processId
                };

                hfdb.ProcessInstances.Add(newProcessInstance);
                hfdb.SaveChanges();
            }

            return Json(new { reload = true });
        }

        [HttpPost]
        [HRLevel]
        [AjaxOnly]
        public JsonResult ContactCandidate()
        {
            int contactId = int.Parse(Request.Form["candidateId"]);
            int processId = int.Parse(Request.Form["processId"]);

            using (HFContext hfdb = new HFContext())
            {
                ProcessInstance contactProcessInstance =
                    (from pIns in hfdb.ProcessInstances
                     where pIns.ContactId == contactId && pIns.ProcessId == processId
                     select pIns).SingleOrDefault();

                contactProcessInstance.CurrentPhase = Phase.Contact_Phase;

                contactProcessInstance.Contacted = true;
                contactProcessInstance.ContactDate = DateTime.Now;

                hfdb.SaveChanges();
            }

            return Json(new { reload = true });
        }

        [HttpGet]
        public ActionResult ProcessInstanceDetails(int Id)
        {
            ProcessInstance detailPInstance = null;
            ProcessInstanceEditInfo detailViewPInstance = null;

            using (HFContext hfdb = new HFContext())
            {
                detailPInstance = hfdb.ProcessInstances.Find(Id);

                if ((Session["login"] as User).Role == UserLevel.Interviewer)
                    ViewBag.disableEdit = "disabled";

                if (detailPInstance.InstanceOf.EndDate != null)
                {
                    ViewBag.disableEdit = "disabled";
                    ViewBag.disableIntwEdit = "disabled";
                }
                detailViewPInstance = ModelConverter.ConvertProcessInstanceForEdit(detailPInstance, hfdb);
            }
            
            return View(detailViewPInstance);
        }

        [HttpPost]
        [HRLevel]
        [ValidateAntiForgeryToken]
        public ActionResult EditProcessInstance(FormCollection editData)
        {
            int editProcessInstanceId = int.Parse(editData["editProcessInstanceId"]);
            int processId = 0;

            using (HFContext hfdb = new HFContext())
            {
                ProcessInstance editProcessInstance = hfdb.ProcessInstances.Find(editProcessInstanceId);
                processId = editProcessInstance.ProcessId;

                // DODELJIVANJE POTENCIJALNIH KOMENTARA

                #region Comments

                int commentAuthorId = int.Parse(editData["currentUserId"]);
                
                if (editData["contactComment"] != string.Empty)
                {
                    editProcessInstance.Annotations.Add(new Annotation
                    {
                        Message = editData["contactComment"],
                        Time = DateTime.Now,
                        Type = Phase.Contact_Phase,
                        Hidden = bool.Parse(editData["hideContactComment"] ?? "false"),
                        AuthorId = commentAuthorId
                    });
                }

                if (editData["interviewComment"] != string.Empty)
                {
                    editProcessInstance.Annotations.Add(new Annotation
                    {
                        Message = editData["interviewComment"],
                        Time = DateTime.Now,
                        Type = Phase.Interview_Phase,
                        AuthorId = commentAuthorId
                    });
                }

                if (editData["testComment"] != string.Empty)
                {
                    editProcessInstance.Annotations.Add(new Annotation
                    {
                        Message = editData["testComment"],
                        Time = DateTime.Now,
                        Type = Phase.Test_Phase,
                        AuthorId = commentAuthorId
                    });
                }

                if (editData["testDefenseComment"] != string.Empty)
                {
                    editProcessInstance.Annotations.Add(new Annotation
                    {
                        Message = editData["testDefenseComment"],
                        Time = DateTime.Now,
                        Type = Phase.TestDefense_Phase,
                        AuthorId = commentAuthorId
                    });
                }

                if (editData["offerComment"] != string.Empty)
                {
                    editProcessInstance.Annotations.Add(new Annotation
                    {
                        Message = editData["offerComment"],
                        Time = DateTime.Now,
                        Type = Phase.Offer_Phase,
                        Hidden = bool.Parse(editData["hideOfferComment"] ?? "false"),
                        AuthorId = commentAuthorId
                    });
                }

                if (editData["acceptComment"] != string.Empty)
                {
                    editProcessInstance.Annotations.Add(new Annotation
                    {
                        Message = editData["acceptComment"],
                        Time = DateTime.Now,
                        Type = Phase.Acceptance_Phase,
                        Hidden = bool.Parse(editData["hideAcceptComment"] ?? "false"),
                        AuthorId = commentAuthorId
                    });
                }

                if (editData["rejectComment"] != string.Empty)
                {
                    editProcessInstance.Annotations.Add(new Annotation
                    {
                        Message = editData["rejectComment"],
                        Time = DateTime.Now,
                        Type = Phase.Rejection_Phase,
                        Hidden = bool.Parse(editData["hideRejectComment"] ?? "false"),
                        AuthorId = commentAuthorId
                    });
                }

                if (editData["quitComment"] != string.Empty)
                {
                    editProcessInstance.Annotations.Add(new Annotation
                    {
                        Message = editData["quitComment"],
                        Time = DateTime.Now,
                        Type = Phase.Quit_Phase,
                        Hidden = bool.Parse(editData["hideQuitComment"] ?? "false"),
                        AuthorId = commentAuthorId
                    });
                }

                #endregion

                // DODELJIVANJE INTERVIEWERA

                #region Interviewers

                hfdb.Interviewers.RemoveRange(editProcessInstance.Interviewers);

                editProcessInstance.Interviewers.Clear();

                if (editData["InterviewersIntw"] != null)
                {
                    int[] intwIds = editData["InterviewersIntw"].Split(',')
                                    .Select(intwId => int.Parse(intwId)).ToArray();

                    foreach (int intwId in intwIds)
                        editProcessInstance.Interviewers.Add(new Interviewer
                        {
                            Type = Phase.Interview_Phase,
                            InterviewerId = intwId,
                            PInsId = editProcessInstance.Id
                        });
                }

                if (editData["InterviewersTest"] != null)
                {
                    int[] testIds = editData["InterviewersTest"].Split(',')
                                                    .Select(testId => int.Parse(testId)).ToArray();

                    foreach (int testId in testIds)
                        editProcessInstance.Interviewers.Add(new Interviewer
                        {
                            Type = Phase.Test_Phase,
                            InterviewerId = testId,
                            PInsId = editProcessInstance.Id
                        });
                }

                if (editData["InterviewersTestDef"] != null)
                {
                    int[] testDefIds = editData["InterviewersTestDef"].Split(',')
                                   .Select(testDefId => int.Parse(testDefId)).ToArray();

                    foreach (int testDefId in testDefIds)
                        editProcessInstance.Interviewers.Add(new Interviewer
                        {
                            Type = Phase.TestDefense_Phase,
                            InterviewerId = testDefId,
                            PInsId = editProcessInstance.Id
                        });
                }

                #endregion

                // DODELJIVANJE BOOL & DATETIME PROMENLJIVIH KOJE CINE STATUS

                #region Status data

                bool contacted = bool.Parse(editData["Contacted"] ?? "false");

                if (!editProcessInstance.Contacted && contacted)
                    editProcessInstance.ContactDate = DateTime.Now;
                editProcessInstance.Contacted = contacted;

                editProcessInstance.InterviewScheduled = bool.Parse(editData["InterviewScheduled"] ?? "false");
                editProcessInstance.InterviewNoticed = bool.Parse(editData["InterviewNoticed"] ?? "false");
                editProcessInstance.InterviewHeld = bool.Parse(editData["InterviewHeld"] ?? "false");
                if (editData["InterviewDate"] != string.Empty)
                    editProcessInstance.InterviewDate = DateTime.Parse(editData["InterviewDate"]);
                
                editProcessInstance.TestScheduled = bool.Parse(editData["TestScheduled"] ?? "false");
                editProcessInstance.TestNoticed = bool.Parse(editData["TestNoticed"] ?? "false");
                editProcessInstance.TestHeld = bool.Parse(editData["TestHeld"] ?? "false");
                if (editData["TestDate"] != string.Empty)
                    editProcessInstance.TestDate = DateTime.Parse(editData["TestDate"]);
                
                editProcessInstance.TestDefenseScheduled = bool.Parse(editData["TestDefenseScheduled"] ?? "false");
                editProcessInstance.TestDefenseNoticed = bool.Parse(editData["TestDefenseNoticed"] ?? "false");
                editProcessInstance.TestDefenseHeld = bool.Parse(editData["TestDefenseHeld"] ?? "false");
                if (editData["TestDefenseDate"] != string.Empty)
                    editProcessInstance.TestDefenseDate = DateTime.Parse(editData["TestDefenseDate"]);

                bool offerSent = bool.Parse(editData["OfferSent"] ?? "false");

                if (!editProcessInstance.OfferSent && offerSent)
                    editProcessInstance.OfferDate = DateTime.Now;
                editProcessInstance.OfferSent = offerSent;

                bool accepted = bool.Parse(editData["Accepted"] ?? "false");
                bool rejected = bool.Parse(editData["Rejected"] ?? "false");
                bool quit = bool.Parse(editData["Quit"] ?? "false");

                if (!editProcessInstance.Accepted && accepted)
                    editProcessInstance.AcceptanceDate = DateTime.Now;
                editProcessInstance.Accepted = accepted;

                if (!editProcessInstance.Rejected && rejected)
                    editProcessInstance.RejectionDate = DateTime.Now;
                editProcessInstance.Rejected = rejected;

                if (!editProcessInstance.Quit && quit)
                    editProcessInstance.QuitDate = DateTime.Now;
                editProcessInstance.Quit = quit;

                #endregion

                // NA OSNOVU USLOVA PODESAVA SE CURRENT PHASE

                #region Phase conditions

                // najlepsi kod ikada
                if (editProcessInstance.Quit && editProcessInstance.Annotations.FirstOrDefault(anno =>
                            anno.Type == Phase.Quit_Phase) != null)
                    editProcessInstance.CurrentPhase = Phase.Quit_Phase;
                else if (editProcessInstance.Rejected && editProcessInstance.Annotations.FirstOrDefault(anno =>
                            anno.Type == Phase.Rejection_Phase) != null)
                    editProcessInstance.CurrentPhase = Phase.Rejection_Phase;
                else if (editProcessInstance.OfferSent && editProcessInstance.Accepted
                         && editProcessInstance.Annotations.FirstOrDefault(anno =>
                             anno.Hidden && anno.Type == Phase.Offer_Phase) != null)
                    editProcessInstance.CurrentPhase = Phase.Acceptance_Phase;
                else if (editProcessInstance.TestDefenseHeld
                         && editProcessInstance.Annotations.FirstOrDefault(anno =>
                             anno.Type == Phase.TestDefense_Phase) != null)
                    editProcessInstance.CurrentPhase = Phase.Offer_Phase;
                else if (editProcessInstance.TestHeld && editProcessInstance.Annotations.FirstOrDefault(anno =>
                             anno.Type == Phase.Test_Phase) != null && editProcessInstance.TestDefenseScheduled
                         && editProcessInstance.TestDefenseNoticed && editProcessInstance.TestDefenseDate != null
                         && editProcessInstance.Interviewers.FirstOrDefault(intw =>
                             intw.Type == Phase.TestDefense_Phase) != null)
                    editProcessInstance.CurrentPhase = Phase.TestDefense_Phase;
                else if (editProcessInstance.InterviewHeld && editProcessInstance.Annotations.FirstOrDefault(anno =>
                             anno.Type == Phase.Interview_Phase) != null
                         && editProcessInstance.TestScheduled && editProcessInstance.TestNoticed
                         && editProcessInstance.TestDate != null && editProcessInstance.Interviewers.FirstOrDefault(intw =>
                             intw.Type == Phase.Test_Phase) != null)
                    editProcessInstance.CurrentPhase = Phase.Test_Phase;
                else if (editProcessInstance.InterviewScheduled && editProcessInstance.InterviewNoticed
                         && editProcessInstance.InterviewDate != null
                         && editProcessInstance.Interviewers.FirstOrDefault(intw =>
                             intw.Type == Phase.Interview_Phase) != null)
                    editProcessInstance.CurrentPhase = Phase.Interview_Phase;
                else if (editProcessInstance.Contacted)
                    editProcessInstance.CurrentPhase = Phase.Contact_Phase;

                #endregion

                hfdb.SaveChanges();
            }

            return RedirectToAction("ProcessDetails", "Processes", new { Id = processId });
        }

        [HttpPost]
        [HRLevel]
        [AjaxOnly]
        public JsonResult SetPhase()
        {
            int processInstanceId = int.Parse(Request.Form["processInstanceId"]);
            Phase newPhase = (Phase)Enum.Parse(typeof(Phase), Request.Form["newPhase"]);
            int userId = int.Parse(Request.Form["userId"]);

            using (HFContext hfdb = new HFContext())
            {
                ProcessInstance editProcessInstance = hfdb.ProcessInstances.Find(processInstanceId);
                DateTime now = DateTime.Now;
                Phase oldPhase = editProcessInstance.CurrentPhase.Value; // sigurno ima value cim moz se vuce

                if (oldPhase < newPhase) // idemo unapred, setujemo novu fazu i sve unazad do stare
                    switch (newPhase)
                    {
                        case Phase.Quit_Phase:
                            editProcessInstance.Quit = true;
                            editProcessInstance.QuitDate = now;
                            editProcessInstance.Rejected = false;
                            editProcessInstance.RejectionDate = null;
                            editProcessInstance.Accepted = false;
                            editProcessInstance.AcceptanceDate = null;
                            break;
                        case Phase.Rejection_Phase:
                            editProcessInstance.Quit = false;
                            editProcessInstance.QuitDate = null;
                            editProcessInstance.Rejected = true;
                            editProcessInstance.RejectionDate = now;
                            editProcessInstance.Accepted = false;
                            editProcessInstance.AcceptanceDate = null;
                            break;
                        case Phase.Acceptance_Phase:
                            editProcessInstance.Quit = false;
                            editProcessInstance.QuitDate = null;
                            editProcessInstance.Rejected = false;
                            editProcessInstance.RejectionDate = null;
                            editProcessInstance.Accepted = true;
                            editProcessInstance.AcceptanceDate = now;
                            editProcessInstance.Annotations.Add(new Annotation
                            {
                                Message = "* Automatski podešen uslov za status kandidata. *",
                                Time = now,
                                Type = Phase.Offer_Phase,
                                Hidden = true,
                                AuthorId = userId
                            });
                            if (oldPhase < Phase.Offer_Phase)
                                goto case Phase.Offer_Phase;
                            break;
                        case Phase.Offer_Phase:
                            editProcessInstance.TestDefenseHeld = true;
                            editProcessInstance.OfferSent = true;
                            editProcessInstance.OfferDate = now;
                            editProcessInstance.Annotations.Add(new Annotation
                            {
                                Message = "* Automatski podešen uslov za status kandidata. *",
                                Time = now,
                                Type = Phase.TestDefense_Phase,
                                AuthorId = userId
                            });
                            if (oldPhase < Phase.TestDefense_Phase)
                                goto case Phase.TestDefense_Phase;
                            break;
                        case Phase.TestDefense_Phase:
                            editProcessInstance.TestHeld = true;
                            editProcessInstance.TestDefenseScheduled = true;
                            editProcessInstance.TestDefenseNoticed = true;
                            editProcessInstance.TestDefenseDate = now;
                            editProcessInstance.Annotations.Add(new Annotation
                            {
                                Message = "* Automatski podešen uslov za status kandidata. *",
                                Time = now,
                                Type = Phase.Test_Phase,
                                AuthorId = userId
                            });
                            editProcessInstance.Interviewers.Add(new Interviewer
                            {
                                Type = Phase.TestDefense_Phase,
                                PInsId = processInstanceId,
                                InterviewerId = userId
                            });
                            if (oldPhase < Phase.Test_Phase)
                                goto case Phase.Test_Phase;
                            break;
                        case Phase.Test_Phase:
                            editProcessInstance.InterviewHeld = true;
                            editProcessInstance.TestScheduled = true;
                            editProcessInstance.TestNoticed = true;
                            editProcessInstance.TestDate = now;
                            editProcessInstance.Annotations.Add(new Annotation
                            {
                                Message = "* Automatski podešen uslov za status kandidata. *",
                                Time = now,
                                Type = Phase.Interview_Phase,
                                AuthorId = userId
                            });
                            editProcessInstance.Interviewers.Add(new Interviewer
                            {
                                Type = Phase.Test_Phase,
                                PInsId = processInstanceId,
                                InterviewerId = userId
                            });
                            if (oldPhase < Phase.Interview_Phase)
                                goto case Phase.Interview_Phase;
                            break;
                        case Phase.Interview_Phase:
                            editProcessInstance.InterviewScheduled = true;
                            editProcessInstance.InterviewNoticed = true;
                            editProcessInstance.InterviewDate = now;
                            editProcessInstance.Interviewers.Add(new Interviewer
                            {
                                Type = Phase.Interview_Phase,
                                PInsId = processInstanceId,
                                InterviewerId = userId
                            });
                            break;
                    }
                else if (oldPhase > newPhase) // idemo unazad, skidamo vrednosti na svih od stare do nove faze, ne racunajuci novu
                {
                    switch (oldPhase)
                    {
                        case Phase.Quit_Phase:
                            editProcessInstance.Quit = false;
                            editProcessInstance.QuitDate = null;
                            editProcessInstance.Annotations = editProcessInstance.Annotations.Where(anno =>
                                anno.AuthorId != userId && anno.Type != Phase.Quit_Phase && !anno.Message.StartsWith("*")).ToList();
                            if (newPhase < Phase.Offer_Phase)
                                goto case Phase.Offer_Phase;
                            break;
                        case Phase.Rejection_Phase:
                            editProcessInstance.Rejected = false;
                            editProcessInstance.RejectionDate = null;
                            editProcessInstance.Annotations = editProcessInstance.Annotations.Where(anno =>
                                anno.AuthorId != userId && anno.Type != Phase.Rejection_Phase && !anno.Message.StartsWith("*")).ToList();
                            if (newPhase < Phase.Offer_Phase)
                                goto case Phase.Offer_Phase;
                            break;
                        case Phase.Acceptance_Phase:
                            editProcessInstance.Accepted = false;
                            editProcessInstance.AcceptanceDate = null;
                            editProcessInstance.Annotations = editProcessInstance.Annotations.Where(anno =>
                                anno.AuthorId != userId && anno.Type != Phase.Acceptance_Phase && !anno.Message.StartsWith("*")).ToList();
                            if (newPhase < Phase.Offer_Phase)
                                goto case Phase.Offer_Phase;
                            break;
                        case Phase.Offer_Phase:
                            editProcessInstance.TestDefenseHeld = false;
                            editProcessInstance.OfferSent = false;
                            editProcessInstance.OfferDate = null;
                            editProcessInstance.Annotations = editProcessInstance.Annotations.Where(anno =>
                                anno.AuthorId != userId && anno.Type != Phase.TestDefense_Phase && !anno.Message.StartsWith("*")).ToList();
                            editProcessInstance.Annotations = editProcessInstance.Annotations.Where(anno =>
                                anno.AuthorId != userId && anno.Type != Phase.Offer_Phase && !anno.Message.StartsWith("*")).ToList();
                            if (newPhase < Phase.TestDefense_Phase)
                                goto case Phase.TestDefense_Phase;
                            break;
                        case Phase.TestDefense_Phase:
                            editProcessInstance.TestHeld = false;
                            editProcessInstance.TestDefenseScheduled = false;
                            editProcessInstance.TestDefenseNoticed = false;
                            editProcessInstance.TestDefenseDate = null;
                            editProcessInstance.Annotations = editProcessInstance.Annotations.Where(anno =>
                                anno.AuthorId != userId && anno.Type != Phase.Test_Phase && !anno.Message.StartsWith("*")).ToList();
                            editProcessInstance.Interviewers = editProcessInstance.Interviewers.Where(intw =>
                                intw.InterviewerId != userId && intw.Type != Phase.TestDefense_Phase).ToList();
                            if (newPhase < Phase.Test_Phase)
                                goto case Phase.Test_Phase;
                            break;
                        case Phase.Test_Phase:
                            editProcessInstance.InterviewHeld = false;
                            editProcessInstance.TestScheduled = false;
                            editProcessInstance.TestNoticed = false;
                            editProcessInstance.TestDate = null;
                            editProcessInstance.Annotations = editProcessInstance.Annotations.Where(anno =>
                                anno.AuthorId != userId && anno.Type != Phase.Interview_Phase && !anno.Message.StartsWith("*")).ToList();
                            editProcessInstance.Interviewers = editProcessInstance.Interviewers.Where(intw =>
                                intw.InterviewerId != userId && intw.Type != Phase.Test_Phase).ToList();
                            if (newPhase < Phase.Interview_Phase)
                                goto case Phase.Interview_Phase;
                            break;
                        case Phase.Interview_Phase:
                            editProcessInstance.InterviewScheduled = true;
                            editProcessInstance.InterviewNoticed = true;
                            editProcessInstance.InterviewDate = now;
                            editProcessInstance.Interviewers.Add(new Interviewer
                            {
                                Type = Phase.Interview_Phase,
                                PInsId = processInstanceId,
                                InterviewerId = userId
                            });
                            break;
                    }

                    if (newPhase == Phase.Rejection_Phase)
                    {
                        editProcessInstance.Rejected = true;
                        editProcessInstance.RejectionDate = DateTime.Now;
                    }
                    else if (newPhase == Phase.Acceptance_Phase)
                    {
                        editProcessInstance.Accepted = true;
                        editProcessInstance.AcceptanceDate = DateTime.Now;
                    }
                }

                if (oldPhase != newPhase) // ako nekako uspe da dropuje u istu kolonu nista se ne desava
                {
                    editProcessInstance.Annotations.Add(new Annotation
                    {
                        Message = "* Automatski podešen status kandidata *",
                        Time = now,
                        Type = newPhase,
                        AuthorId = userId
                    });

                    editProcessInstance.ToDoItems.Add(new ToDoItem
                    {
                        Message = "* Promeniti podrazumevane podatke o učešću *",
                        Time = DateTime.Now
                    });

                    editProcessInstance.CurrentPhase = newPhase;
                }

                hfdb.SaveChanges();
            }

            return Json(new { success = true });
        }

    }
}
