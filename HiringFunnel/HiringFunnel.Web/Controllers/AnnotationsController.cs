using System;
using System.Web.Mvc;
using HiringFunnel.Data;
using HiringFunnel.Data.DAL;
using HiringFunnel.Data.Models;

namespace HiringFunnel.Web.Controllers
{

    [LoggedIn]
    public class AnnotationsController : Controller
    {
        
        [HttpGet]
        public PartialViewResult PostContactComment()
        {
            return PartialView();
        }

        [HttpPost]
        [HRLevel]
        [ValidateAntiForgeryToken]
        public ActionResult PostContactComment(FormCollection commentData)
        {
            int contactId = int.Parse(commentData["commentContactId"]);
            int authorId = int.Parse(commentData["commentAuthorId"]);
            Contact commentReceiver = null;
            User commentAuthor = null;

            using (HFContext hfdb = new HFContext())
            {
                commentReceiver = hfdb.Contacts.Find(contactId);
                commentAuthor = hfdb.Users.Find(authorId);

                commentReceiver.Comments.Add(new Annotation
                {
                    Message = commentData["Message"],
                    Time = DateTime.Now,
                    Type = Phase.Contact_Comment,
                    Hidden = bool.Parse(commentData["Hidden"] ?? "false"),
                    Author = commentAuthor
                });

                hfdb.SaveChanges();
            }

            return RedirectToRoute("Default", new { controller = "Contacts", action = "ContactDetails",  Id = commentReceiver.Id });
        }

        [HttpPost]
        [HRLevel]
        [ValidateAntiForgeryToken]
        public ActionResult EditContactComment(FormCollection editData)
        {
            int contactId = 0;
            int editCommentId = int.Parse(editData["editCommentId"]);

            using (HFContext hfdb = new HFContext())
            {
                Annotation editComment = hfdb.Annotations.Find(editCommentId);
                contactId = (int)editComment.ContactId;

                editComment.Message = editData["editCommentText"];

                hfdb.SaveChanges();
            }

            return RedirectToAction("ContactDetails", "Contacts", new { Id = contactId });
        }

        [HttpGet]
        [HRLevel]
        public ActionResult DeleteContactComment(int Id)
        {
            int contactId = 0;

            using (HFContext hfdb = new HFContext())
            {
                Annotation deleteAnnotation = hfdb.Annotations.Find(Id);
                contactId = (int)deleteAnnotation.ContactId;

                hfdb.Annotations.Remove(deleteAnnotation);

                hfdb.SaveChanges();
            }

            return RedirectToAction("ContactDetails", "Contacts", new { Id = contactId });
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult PostPhaseComment()
        {
            using (HFContext hfdb = new HFContext())
            {
                hfdb.Annotations.Add(new Annotation
                {
                    Message = Request.Form["commentMessage"],
                    Time = DateTime.Now,
                    Type = (Phase)int.Parse(Request.Form["commentPhase"]),
                    Hidden = bool.Parse(Request.Form["commentHidden"] ?? "false"),
                    PInsId = int.Parse(Request.Form["processInstanceId"]),
                    AuthorId = int.Parse(Request.Form["commentAuthorId"])
                });

                hfdb.SaveChanges();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult EditPhaseComment()
        {
            int editCommentId = int.Parse(Request.Form["editCommentId"]);

            using (HFContext hfdb = new HFContext())
            {
                Annotation editComment = hfdb.Annotations.Find(editCommentId);
                editComment.Message = Request.Form["editCommentText"];

                hfdb.SaveChanges();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult DeletePhaseComment()
        {
            int deleteCommentId = int.Parse(Request.Form["deleteCommentId"]);

            using (HFContext hfdb = new HFContext())
            {
                Annotation deleteAnnotation = hfdb.Annotations.Find(deleteCommentId);
                hfdb.Annotations.Remove(deleteAnnotation);

                hfdb.SaveChanges();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        [HRLevel]
        [AjaxOnly]
        public JsonResult PostToDoItem()
        {
            int processInstanceId = int.Parse(Request.Form["processInstanceId"]);
            string toDoMessage = Request.Form["toDoMessage"];
            int addedId = 0;

            using (HFContext hfdb = new HFContext())
            {
                ToDoItem newToDo = new ToDoItem
                {
                    Message = toDoMessage,
                    PInsId = processInstanceId,
                    Time = DateTime.Now
                };
                hfdb.ToDoItems.Add(newToDo);

                hfdb.SaveChanges();

                addedId = newToDo.Id;
            }

            return Json(new { toDoId = addedId });
        }

        [HttpPost]
        [HRLevel]
        [AjaxOnly]
        public JsonResult MarkToDoItem()
        {
            int toDoItemId = int.Parse(Request.Form["toDoItemId"]);

            using (HFContext hfdb = new HFContext())
            {
                ToDoItem markToDo = hfdb.ToDoItems.Find(toDoItemId);
                markToDo.Completed = true;

                hfdb.SaveChanges();
            }

            return Json(new { success = true });
        }

    }
}
