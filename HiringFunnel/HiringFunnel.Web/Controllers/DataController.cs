using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using DataTables.Mvc;
using HiringFunnel.Data;
using HiringFunnel.Data.DAL;
using HiringFunnel.Data.Models;

namespace HiringFunnel.Web.Controllers
{

    [LoggedIn]
    public class DataController : Controller
    {

        [HttpPost]
        [AjaxOnly]
        public JsonResult AddNewTechnology()
        {
            string newTechName = Request.Form["newTechName"];
            int addedId = 0;

            using (HFContext hfdb = new HFContext())
            {
                Technology newTech = new Technology { Name = newTechName };

                hfdb.Technologies.Add(newTech);
                hfdb.SaveChanges();

                addedId = newTech.Id;
            }

            return Json(new { newTechId = addedId });
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetUserData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IQueryable<User> query = null;
            int totalCount = 0;
            int filteredCount = 0;

            using (HFContext hfdb = new HFContext())
            {
                query = from user in hfdb.Users
                        where user.Deleted == false
                        select user;

                totalCount = query.Count();

                if (requestModel.Search.Value != string.Empty)
                {
                    string value = requestModel.Search.Value.Trim();
                    query = query.Where(user => user.FirstName.Contains(value) ||
                                                user.LastName.Contains(value) ||
                                                user.Email.Contains(value) ||
                                                user.Technologies.FirstOrDefault(tech => tech.Name.Contains(value)) != null
                                       );
                }

                filteredCount = query.Count();

                IOrderedEnumerable<Column> sortedColumns = requestModel.Columns.GetSortedColumns();
                string orderByString = string.Empty;

                foreach (Column column in sortedColumns)
                {
                    orderByString += orderByString != string.Empty ? "," : "";
                    orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
                }

                query = query.OrderBy(orderByString == string.Empty ? "LastName asc" : orderByString);
                query = query.Skip(requestModel.Start).Take(requestModel.Length);

                var data = query.Select(user =>
                    new {
                        Id = user.Id,
                        FirstName = user.FirstName + " " + user.LastName,
                        Email = user.Email,
                        Seniority = user.Seniority.ToString(),
                        Role = user.Role.ToString(),
                        Technologies = from tech in user.Technologies
                                       select " " + tech.Name
                    }).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount));
            }
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetContactData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IQueryable<Contact> query = null;
            int totalCount = 0;
            int filteredCount = 0;

            using (HFContext hfdb = new HFContext())
            {
                query = from con in hfdb.Contacts
                        where con.Deleted == false
                        select con;

                totalCount = query.Count();

                if (requestModel.Search.Value != string.Empty)
                {
                    string value = requestModel.Search.Value.Trim();
                    query = query.Where(con => con.FirstName.Contains(value) ||
                                               con.LastName.Contains(value) ||
                                               con.Email.Contains(value) ||
                                               con.Technologies.FirstOrDefault(tech => tech.Name.Contains(value)) != null
                                       );
                }

                filteredCount = query.Count();

                IOrderedEnumerable<Column> sortedColumns = requestModel.Columns.GetSortedColumns();
                string orderByString = string.Empty;

                foreach (Column column in sortedColumns)
                {
                    orderByString += orderByString != string.Empty ? "," : "";
                    orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
                }

                query = query.OrderBy(orderByString == string.Empty ? "LastName asc" : orderByString);
                query = query.Skip(requestModel.Start).Take(requestModel.Length);

                var data = query.Select(con =>
                    new {
                        Id = con.Id,
                        FirstName = con.FirstName + " " + con.LastName,
                        Location = con.Location,
                        Email = con.Email,
                        Phone = con.Phone,
                        Seniority = con.Seniority.ToString(),
                        Technologies = from tech in con.Technologies
                                       select " " + tech.Name
                    }).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount));
            }
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetContactCommentData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IQueryable<Annotation> query = null;
            int totalCount = 0;
            int filteredCount = 0;
            int contactId = int.Parse(Request.Form["contactId"]);

            using (HFContext hfdb = new HFContext())
            {
                query = from anno in hfdb.Annotations
                        where anno.ContactId == contactId
                        select anno;

                totalCount = query.Count();

                if (requestModel.Search.Value != string.Empty)
                {
                    string value = requestModel.Search.Value.Trim();
                    query = query.Where(anno => anno.Message.Contains(value) ||
                                                anno.Author.FirstName.Contains(value) ||
                                                anno.Author.LastName.Contains(value)
                    );
                }

                filteredCount = query.Count();

                query = query.OrderBy("Time desc");
                query = query.Skip(requestModel.Start).Take(requestModel.Length);

                var data = query.Select(anno =>
                    new {
                        Id = anno.Id,
                        Message = anno.Message,
                        Hidden = anno.Hidden,
                        AuthorId = anno.Author.Id,
                        AuthorText = anno.Author.FirstName + " " + anno.Author.LastName,
                        Time = anno.Time.Day + ". " + anno.Time.Month + ". " + anno.Time.Year + ". " +
                                anno.Time.Hour + ":" + anno.Time.Minute + ":" + anno.Time.Second
                    }).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount));
            }
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetProcessData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IQueryable<Process> query = null;
            int totalCount = 0;
            int filteredCount = 0;

            using (HFContext hfdb = new HFContext())
            {
                query = from proc in hfdb.Processes
                        select proc;

                totalCount = query.Count();

                if (requestModel.Search.Value != string.Empty)
                {
                    string value = requestModel.Search.Value.Trim();
                    query = query.Where(p => p.Name.Contains(value) ||
                                             p.Technologies.Contains(value)
                                       );
                }

                filteredCount = query.Count();

                IOrderedEnumerable<Column> sortedColumns = requestModel.Columns.GetSortedColumns();
                string orderByString = string.Empty;

                foreach (Column column in sortedColumns)
                {
                    orderByString += orderByString != string.Empty ? "," : "";
                    orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
                }

                query = query.OrderBy(orderByString == string.Empty ? "StartDate asc" : orderByString);
                query = query.Skip(requestModel.Start).Take(requestModel.Length);

                var data = query.Select(process =>
                    new {
                        Id = process.Id,
                        Name = process.Name,
                        Technologies = process.Technologies,
                        Seniority = process.Seniority.ToString(),
                        StartDate = process.StartDate.Day + "." + process.StartDate.Month + "." + process.StartDate.Year + ".",
                        EndDate = process.EndDate.Value.Day + "." + process.EndDate.Value.Month + "." + process.EndDate.Value.Year + "."
                    }).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount));
            }
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetContactProcessData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IQueryable<Process> query = null;
            int totalCount = 0;
            int filteredCount = 0;
            int contactId = int.Parse(Request.Form["contactId"]);

            using (HFContext hfdb = new HFContext())
            {
                query = from proc in hfdb.Processes
                        where proc.Instances.FirstOrDefault(pIns => pIns.ContactId == contactId) != null
                        select proc;

                totalCount = query.Count();

                if (requestModel.Search.Value != string.Empty)
                {
                    string value = requestModel.Search.Value.Trim();
                    query = query.Where(p => p.Name.Contains(value) ||
                                             p.Technologies.Contains(value)
                                       );
                }

                filteredCount = query.Count();

                IOrderedEnumerable<Column> sortedColumns = requestModel.Columns.GetSortedColumns();
                string orderByString = string.Empty;

                foreach (Column column in sortedColumns)
                {
                    orderByString += orderByString != string.Empty ? "," : "";
                    orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
                }

                query = query.OrderBy(orderByString == string.Empty ? "StartDate asc" : orderByString);
                query = query.Skip(requestModel.Start).Take(requestModel.Length);

                var data = query.Select(process =>
                    new {
                        Id = process.Id,
                        Name = process.Name,
                        Technologies = process.Technologies,
                        Seniority = process.Seniority.ToString(),
                        Year = ((process.EndDate.Value != null) ? process.EndDate.Value.Year : process.StartDate.Year) + " ",
                        EndDate = process.EndDate.Value.Day + "." + process.EndDate.Value.Month + "." + process.EndDate.Value.Year + "."
                    }).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount));
            }
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetProcessCandidates([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IQueryable<Contact> query = null;
            int totalCount = 0;
            int filteredCount = 0;
            int processId = int.Parse(Request.Form["processId"]);

            using (HFContext hfdb = new HFContext())
            {
                query = from con in hfdb.Contacts
                        where (!con.Deleted && con.Instances.FirstOrDefault(pIns =>
                                pIns.ProcessId == processId) == null)
                        select con;

                totalCount = query.Count();

                if (requestModel.Search.Value != string.Empty)
                {
                    string value = requestModel.Search.Value.Trim();
                    query = query.Where(con => con.FirstName.Contains(value) ||
                                               con.LastName.Contains(value) ||
                                               con.Email.Contains(value) ||
                                               con.Technologies.FirstOrDefault(tech => tech.Name.Contains(value)) != null
                                       );
                }

                filteredCount = query.Count();

                IOrderedEnumerable<Column> sortedColumns = requestModel.Columns.GetSortedColumns();
                string orderByString = string.Empty;

                foreach (Column column in sortedColumns)
                {
                    orderByString += orderByString != string.Empty ? "," : "";
                    orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
                }

                query = query.OrderBy(orderByString == string.Empty ? "FirstName asc" : orderByString);
                query = query.Skip(requestModel.Start).Take(requestModel.Length);

                var data = query.Select(con =>
                    new {
                        Id = con.Id,
                        FirstName = con.FirstName + " " + con.LastName,
                        Location = con.Location,
                        Email = con.Email,
                        Phone = con.Phone,
                        Seniority = con.Seniority.ToString(),
                        Technologies = from tech in con.Technologies
                                       select " " + tech.Name
                    }).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount));
            }
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetContactsForProcess([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IQueryable<Contact> query = null;
            int totalCount = 0;
            int filteredCount = 0;
            int processId = int.Parse(Request.Form["processId"]);

            using (HFContext hfdb = new HFContext())
            {
                query = from con in hfdb.Contacts
                        where (!con.Deleted && con.Instances.FirstOrDefault(pIns => 
                                    pIns.ProcessId == processId && pIns.CurrentPhase == null) != null)
                        select con;

                totalCount = query.Count();

                if (requestModel.Search.Value != string.Empty)
                {
                    string value = requestModel.Search.Value.Trim();
                    query = query.Where(con => con.FirstName.Contains(value) ||
                                               con.LastName.Contains(value) ||
                                               con.Email.Contains(value) ||
                                               con.Technologies.FirstOrDefault(tech => tech.Name.Contains(value)) != null
                                       );
                }

                filteredCount = query.Count();

                IOrderedEnumerable<Column> sortedColumns = requestModel.Columns.GetSortedColumns();
                string orderByString = string.Empty;

                foreach (Column column in sortedColumns)
                {
                    orderByString += orderByString != string.Empty ? "," : "";
                    orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
                }

                query = query.OrderBy(orderByString == string.Empty ? "FirstName asc" : orderByString);
                query = query.Skip(requestModel.Start).Take(requestModel.Length);

                var data = query.Select(con =>
                    new {
                        Id = con.Id,
                        FirstName = con.FirstName + " " + con.LastName,
                        Location = con.Location,
                        Email = con.Email,
                        Phone = con.Phone,
                        Seniority = con.Seniority.ToString(),
                        Technologies = from tech in con.Technologies
                                       select " " + tech.Name
                    }).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount));
            }
        }
        
        [HttpPost]
        [AjaxOnly]
        public JsonResult GetPhaseCommentData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IQueryable<Annotation> query = null;
            int totalCount = 0;
            int filteredCount = 0;

            int processInstanceId = int.Parse(Request.Form["processInstanceId"]);
            Phase commentPhase = (Phase)int.Parse(Request.Form["commentPhase"]);

            using (HFContext hfdb = new HFContext())
            {
                query = from anno in hfdb.Annotations
                        where anno.PInsId == processInstanceId && anno.Type == commentPhase
                        select anno;

                totalCount = query.Count();

                if (requestModel.Search.Value != string.Empty)
                {
                    string value = requestModel.Search.Value.Trim();
                    query = query.Where(anno => anno.Message.Contains(value) ||
                                                anno.Author.FirstName.Contains(value) ||
                                                anno.Author.LastName.Contains(value)
                    );
                }

                filteredCount = query.Count();

                query = query.OrderBy("Time desc");
                query = query.Skip(requestModel.Start).Take(requestModel.Length);

                var data = query.Select(anno =>
                    new {
                        Id = anno.Id,
                        Message = anno.Message,
                        Hidden = anno.Hidden,
                        AuthorId = anno.AuthorId,
                        AuthorText = anno.Author.FirstName + " " + anno.Author.LastName,
                        Time = anno.Time.Day + ". " + anno.Time.Month + ". " + anno.Time.Year + ". " +
                                anno.Time.Hour + ":" + anno.Time.Minute + ":" + anno.Time.Second
                    }).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount));
            }
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetProcessMetricsData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IQueryable<Process> query = null;
            int totalCount = 0;
            int filteredCount = 0;

            SeniorityLevel seniority = SeniorityLevel.Entry;
            bool seniorityExists = false;

            if (Request.Form["seniority"] != string.Empty)
            {
                seniority = (SeniorityLevel)int.Parse(Request.Form["seniority"]);
                seniorityExists = true;
            }

            string technology = Request.Form["technology"];
            int month = int.Parse(Request.Form["month"]);
            int year = int.Parse(Request.Form["year"]);

            DateTime start = new DateTime(year, month, 1);
            DateTime end = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            
            using (HFContext hfdb = new HFContext())
            {
                query = from proc in hfdb.Processes
                        where ((start < proc.StartDate && end > proc.StartDate)
                            || (start > proc.StartDate && start < proc.EndDate))
                            && ((seniorityExists && proc.Seniority == seniority) || proc.Technologies.Contains(technology))
                        select proc;

                totalCount = query.Count();

                if (requestModel.Search.Value != string.Empty)
                {
                    string value = requestModel.Search.Value.Trim();
                    query = query.Where(p => p.Name.Contains(value) ||
                                             p.Technologies.Contains(value)
                                       );
                }

                filteredCount = query.Count();

                IOrderedEnumerable<Column> sortedColumns = requestModel.Columns.GetSortedColumns();
                string orderByString = string.Empty;

                foreach (Column column in sortedColumns)
                {
                    orderByString += orderByString != string.Empty ? "," : "";
                    orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
                }

                query = query.OrderBy(orderByString == string.Empty ? "StartDate asc" : orderByString);
                query = query.Skip(requestModel.Start).Take(requestModel.Length);

                ICollection<Process> processData = query.ToList();

                var data = processData.Select(process =>
                    new {
                        Id = process.Id,
                        Name = process.Name,
                        Technologies = process.Technologies,
                        Seniority = process.Seniority.ToString(),
                        Duration = (process.EndDate ?? DateTime.Now).Subtract(process.StartDate).TotalDays.ToString("F")
                    }).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount));
            }
        }

    }
}
