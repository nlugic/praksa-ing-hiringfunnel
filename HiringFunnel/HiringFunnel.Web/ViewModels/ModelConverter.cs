using System.Collections.Generic;
using System.Linq;
using HiringFunnel.Data.DAL;
using HiringFunnel.Data.Models;

namespace HiringFunnel.Web.ViewModels
{
    public static class ModelConverter
    {

        // ako je hfdb null ne uzima tehnologije (za slucajeve gde mi ne trebaju)
        public static UserInfo ConvertUser(User usr, HFContext hfdb = null)
        {
            ICollection<TechnologyInfo> uTechInfos = null;
            
            if (hfdb != null)
            {
                hfdb.Users.Attach(usr);

                uTechInfos =
                    (from uTech in usr.Technologies
                     select new TechnologyInfo
                     {
                         Name = uTech.Name
                     }).ToList();
            }

            return new UserInfo
            {
                Id = usr.Id,
                FirstName = usr.FirstName,
                LastName = usr.LastName,
                Email = usr.Email,
                Seniority = usr.Seniority,
                Role = usr.Role,
                Technologies = uTechInfos
            };
        }

        // ako je hfdb null ne uzima tehnologije&komentare (za slucajeve gde mi ne trebaju)
        public static ContactInfo ConvertContact(Contact con, HFContext hfdb = null)
        {
            ICollection<TechnologyInfo> cTechInfos = null;
            ICollection<ContactAnnotationInfo> cAnnotationInfos = null;

            if (hfdb != null)
            {
                hfdb.Contacts.Attach(con);

                cTechInfos =
                    (from cTech in con.Technologies
                     select new TechnologyInfo
                     {
                         Name = cTech.Name
                     }).ToList();

                cAnnotationInfos =
                    (from cAnnotation in con.Comments
                     select new ContactAnnotationInfo
                     {
                         Id = cAnnotation.Id,
                         Message = cAnnotation.Message,
                         Time = cAnnotation.Time,
                         Hidden = cAnnotation.Hidden,
                         Author = ConvertUser(cAnnotation.Author)
                     }).ToList();
            }
            
            return new ContactInfo
            {
                Id = con.Id,
                FirstName = con.FirstName,
                LastName = con.LastName,
                DateOfBirth = con.DateOfBirth,
                Location = con.Location,
                Email = con.Email,
                Phone = con.Phone,
                LinkedIn = con.LinkedIn,
                CurrentWorkplace = con.CurrentWorkplace,
                CVContent = con.CVContent,
                Seniority = con.Seniority,
                Technologies = cTechInfos,
                Comments = cAnnotationInfos
            };
        }
        
        public static ProcessInfo ConvertProcess(Process proc)
        {
            return new ProcessInfo
            {
                Id = proc.Id,
                Name = proc.Name,
                Technologies = proc.Technologies,
                Seniority = proc.Seniority,
                StartDate = proc.StartDate,
                EndDate = proc.EndDate,
            };
        }

        public static ProcessInstanceInfo ConvertProcessInstance(ProcessInstance procIns, HFContext hfdb)
        {
            Contact cInProc = null;
            Process proc = null;

            hfdb.ProcessInstances.Attach(procIns);

            cInProc = procIns.ContactInProcess;
            proc = procIns.InstanceOf;

            return new ProcessInstanceInfo
            {
                Id = procIns.Id,
                ProcessName = proc.Name,
                ContactName = cInProc.FirstName + " " + cInProc.LastName,
                ProcessSeniority = proc.Seniority.ToString(),
                ProcessTechnologies = proc.Technologies
            };
        }

        public static ProcessInstanceEditInfo ConvertProcessInstanceForEdit(ProcessInstance procIns, HFContext hfdb)
        {
            Contact cInProc = null;
            Process proc = null;

            hfdb.ProcessInstances.Attach(procIns);

            cInProc = procIns.ContactInProcess;
            proc = procIns.InstanceOf;

            ICollection<PInstanceAnnotationInfo> pInsAnnotations =
                (from pInsAnno in procIns.Annotations
                 select new PInstanceAnnotationInfo
                 {
                     Id = pInsAnno.Id,
                     Message = pInsAnno.Message,
                     Time = pInsAnno.Time,
                     Type = pInsAnno.Type,
                     Hidden = pInsAnno.Hidden,
                     Author = ConvertUser(pInsAnno.Author)
                 }).ToList();

            ICollection<InterviewerInfo> pInsInteviewers =
                (from pInsIntw in procIns.Interviewers
                 select new InterviewerInfo
                 {
                     Id = pInsIntw.Id,
                     Type = pInsIntw.Type,
                     Name = pInsIntw.InterviewerOnPhase.FirstName + " " + pInsIntw.InterviewerOnPhase.LastName,
                     Email = pInsIntw.InterviewerOnPhase.Email,
                     InterviewerId = pInsIntw.InterviewerId
                 }).ToList();

            ICollection<ToDoItemInfo> pInsToDoItems =
                (from pInsToDo in procIns.ToDoItems
                 select new ToDoItemInfo
                 {
                     Id = pInsToDo.Id,
                     Message = pInsToDo.Message,
                     Completed = pInsToDo.Completed,
                     Time = pInsToDo.Time
                 }).ToList();

            return new ProcessInstanceEditInfo
            {
                Id = procIns.Id,
                CreationDate = procIns.CreationDate,
                CurrentPhase = procIns.CurrentPhase,
                Contacted = procIns.Contacted,
                ContactDate = procIns.ContactDate,
                InterviewScheduled = procIns.InterviewScheduled,
                InterviewNoticed = procIns.InterviewNoticed,
                InterviewHeld = procIns.InterviewHeld,
                InterviewDate = procIns.InterviewDate,
                TestScheduled = procIns.TestScheduled,
                TestNoticed = procIns.TestNoticed,
                TestHeld = procIns.TestHeld,
                TestDate = procIns.TestDate,
                TestDefenseScheduled = procIns.TestDefenseScheduled,
                TestDefenseNoticed = procIns.TestDefenseNoticed,
                TestDefenseHeld = procIns.TestDefenseHeld,
                TestDefenseDate = procIns.TestDefenseDate,
                OfferSent = procIns.OfferSent,
                OfferDate = procIns.OfferDate,
                Accepted = procIns.Accepted,
                AcceptanceDate = procIns.AcceptanceDate,
                Rejected = procIns.Rejected,
                RejectionDate = procIns.RejectionDate,
                Quit = procIns.Quit,
                QuitDate = procIns.QuitDate,
                Annotations = pInsAnnotations,
                Interviewers = pInsInteviewers,
                ToDoItems = pInsToDoItems,
                ContactInProcess = ConvertContact(cInProc),
                InstanceOf = ConvertProcess(proc)
            };
        }
        
    }
}
