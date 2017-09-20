using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using HiringFunnel.Data;
using HiringFunnel.Data.DAL;
using HiringFunnel.Data.Models;

namespace HiringFunnel.Web.Controllers
{
    
    [HRLevel]
    public class MetricsController : Controller
    {

        [HttpGet]
        public ActionResult ViewMetrics()
        {
            return View();
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetGlobalMetricsData()
        {
            double avgProcTime = 0.0, maxProcTime = 0.0, minProcTime = 0.0;
            Process maxProc = null, minProc = null;

            double avgStatTime = 0.0, maxStatTime = 0.0, minStatTime = 0.0;
            ProcessInstance maxPIns = null, minPIns = null;

            using (HFContext hfdb = new HFContext())
            {
                List<Process> processes = hfdb.Processes.ToList();
                avgProcTime = processes.Average(proc => (proc.EndDate ?? DateTime.Now).Subtract(proc.StartDate).TotalDays);

                maxProcTime = 0.0;
                minProcTime = double.MaxValue;
                maxProc = processes[0];
                minProc = maxProc;

                foreach (Process proc in processes)
                {
                    double time = (proc.EndDate ?? DateTime.Now).Subtract(proc.StartDate).TotalDays;
                    if (time > maxProcTime)
                    {
                        maxProcTime = time;
                        maxProc = proc;
                    }

                    if (time < minProcTime)
                    {
                        minProcTime = time;
                        minProc = proc;
                    }
                }

                List<ProcessInstance> processInstances = hfdb.ProcessInstances.ToList();
                avgStatTime = processInstances.Average(pIns => AverageTimeWithinInstance(pIns));

                maxStatTime = 0.0;
                minStatTime = double.MaxValue;
                maxPIns = processInstances[0];
                minPIns = maxPIns;

                foreach (ProcessInstance pIns in processInstances)
                {
                    double time = MaximumTimeWithinInstance(pIns);
                    if (time > maxStatTime)
                    {
                        maxStatTime = time;
                        maxPIns = pIns;
                    }

                    time = MinimumTimeWithinInstance(pIns);
                    if (time < minStatTime)
                    {
                        minStatTime = time;
                        minPIns = pIns;
                    }
                }
            }

            var data = new
            {
                avgProcessTime = avgProcTime.ToString("F"),
                maxProcessId = maxProc.Id,
                maxProcessName = maxProc.Name,
                maxProcessTime = maxProcTime.ToString("F"),
                minProcessId = minProc.Id,
                minProcessName = minProc.Name,
                minProcessTime = minProcTime.ToString("F"),

                avgStatusTime = avgStatTime.ToString("F"),
                maxStatusId = maxPIns.Id,
                maxStatusTime = maxStatTime.ToString("F"),
                minStatusId = minPIns.Id,
                minStatusTime = minStatTime.ToString("F")
            };

            return Json(data);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetMetricsData()
        {
            SeniorityLevel seniority = (SeniorityLevel)int.Parse(Request.Form["seniority"]);
            string technology = Request.Form["technology"];

            double avgProcTime = 0.0, maxProcTime = 0.0, minProcTime = 0.0;
            Process maxProc = null, minProc = null;

            double avgStatTime = 0.0, maxStatTime = 0.0, minStatTime = 0.0;
            ProcessInstance maxPIns = null, minPIns = null;

            using (HFContext hfdb = new HFContext())
            {
                List<Process> processes = (from proc in hfdb.Processes
                                           where proc.Seniority == seniority
                                               && proc.Technologies.Contains(technology)
                                           select proc).ToList();

                avgProcTime = processes.Average(proc => (proc.EndDate ?? DateTime.Now).Subtract(proc.StartDate).TotalDays);

                maxProcTime = 0.0;
                minProcTime = double.MaxValue;
                maxProc = processes[0];
                minProc = maxProc;

                foreach (Process proc in processes)
                {
                    double time = (proc.EndDate ?? DateTime.Now).Subtract(proc.StartDate).TotalDays;
                    if (time > maxProcTime)
                    {
                        maxProcTime = time;
                        maxProc = proc;
                    }

                    if (time < minProcTime)
                    {
                        minProcTime = time;
                        minProc = proc;
                    }
                }

                List<ProcessInstance> processInstances = (from pIns in hfdb.ProcessInstances
                                                          where pIns.InstanceOf.Seniority == seniority
                                                              && pIns.InstanceOf.Technologies.Contains(technology)
                                                          select pIns).ToList();

                avgStatTime = processInstances.Average(pIns => AverageTimeWithinInstance(pIns));

                maxStatTime = 0.0;
                minStatTime = double.MaxValue;
                maxPIns = processInstances[0];
                minPIns = maxPIns;

                foreach (ProcessInstance pIns in processInstances)
                {
                    double time = MaximumTimeWithinInstance(pIns);
                    if (time > maxStatTime)
                    {
                        maxStatTime = time;
                        maxPIns = pIns;
                    }

                    time = MinimumTimeWithinInstance(pIns);
                    if (time < minStatTime)
                    {
                        minStatTime = time;
                        minPIns = pIns;
                    }
                }
            }

            var data = new
            {
                avgProcessTime = avgProcTime.ToString("F"),
                maxProcessId = maxProc.Id,
                maxProcessName = maxProc.Name,
                maxProcessTime = maxProcTime.ToString("F"),
                minProcessId = minProc.Id,
                minProcessName = minProc.Name,
                minProcessTime = minProcTime.ToString("F"),

                avgStatusTime = avgStatTime.ToString("F"),
                maxStatusId = maxPIns.Id,
                maxStatusTime = maxStatTime.ToString("F"),
                minStatusId = minPIns.Id,
                minStatusTime = minStatTime.ToString("F")
            };

            return Json(data);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetAverageStatusTime()
        {
            Phase status = Phase.Contact_Phase;
            bool statusExists = false;

            if (Request.Form["status"] != string.Empty)
            {
                status = (Phase)int.Parse(Request.Form["status"]);
                statusExists = true;
            }

            double avgStatTime = 0.0;

            if (statusExists)
                using (HFContext hfdb = new HFContext())
                {
                    ICollection<ProcessInstance> processInstances = (from pIns in hfdb.ProcessInstances
                                                                     where PhaseFinished(pIns, status)
                                                                     select pIns).ToList();

                    avgStatTime = processInstances.Average(pIns => CalculateStatusTime(pIns, status));
                }

            var data = new
            {
                avgStatusTime = avgStatTime
            };

            return Json(data);
        }

        [HttpPost]
        [AjaxOnly]
        public JsonResult GetProcessMonthlyMetrics()
        {
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

            int acceptsNumber = 0, processesNumber = 0;

            using (HFContext hfdb = new HFContext())
            {
                ICollection<Process> processes = (from proc in hfdb.Processes
                                                  where ((start <= proc.StartDate && end >= proc.StartDate)
                                                      || (start >= proc.StartDate && start <= proc.EndDate))
                                                      && ((seniorityExists && proc.Seniority == seniority) || proc.Technologies.Contains(technology))
                                                  select proc).ToList();

                processesNumber = processes.Count;

                foreach (Process proc in processes)
                    foreach (ProcessInstance pIns in proc.Instances)
                        if (pIns.CurrentPhase == Phase.Acceptance_Phase)
                            ++acceptsNumber;
            }

            var data = new
            {
                acceptsNumber = acceptsNumber,
                processesNumber = processesNumber
            };

            return Json(data);
        }

        [NonAction]
        public void GenerateTimesArray(ProcessInstance pInstance, out ICollection<double> timeArray)
        {
            List<DateTime> dates = new List<DateTime>();
            ICollection<double> times = new Collection<double>();
            
            PropertyInfo[] props = pInstance.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
                if ((prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?)) 
                        && (prop.GetValue(pInstance) as DateTime?).HasValue)
                    dates.Add((DateTime)prop.GetValue(pInstance));

            // pretpostavljam da je poslednji indeks uvek jedna od krajnjih faza
            for (int i = 0; i < dates.Count - 1; ++i)
                times.Add((dates[i + 1].Subtract(dates[i])).TotalDays);

            timeArray = times;
        }

        [NonAction]
        public double AverageTimeWithinInstance(ProcessInstance pInstance)
        {
            ICollection<double> times = null;
            GenerateTimesArray(pInstance, out times);

            return ((times.Count == 0) ? 0.0 : times.Average());
        }

        [NonAction]
        public double MaximumTimeWithinInstance(ProcessInstance pInstance)
        {
            ICollection<double> times = null;
            GenerateTimesArray(pInstance, out times);

            return ((times.Count == 0) ? 0.0 : times.Max());
        }

        [NonAction]
        public double MinimumTimeWithinInstance(ProcessInstance pInstance)
        {
            ICollection<double> times = null;
            GenerateTimesArray(pInstance, out times);

            return ((times.Count == 0) ? 0.0 : times.Min());
        }

        [NonAction]
        public bool PhaseFinished(ProcessInstance pInstance, Phase phase)
        {
            return pInstance.CurrentPhase > phase;
        }

        [NonAction]
        public double CalculateStatusTime(ProcessInstance pInstance, Phase phase)
        {
            double time = 0.0;

            switch (phase)
            {
                case Phase.Contact_Phase: time = (pInstance.InterviewDate.Value.Subtract(pInstance.ContactDate.Value)).TotalDays;
                    break;
                case Phase.Interview_Phase: time = (pInstance.TestDate.Value.Subtract(pInstance.InterviewDate.Value)).TotalDays;
                    break;
                case Phase.Test_Phase: time = (pInstance.TestDefenseDate.Value.Subtract(pInstance.TestDate.Value)).TotalDays;
                    break;
                case Phase.TestDefense_Phase: time = (pInstance.OfferDate.Value.Subtract(pInstance.TestDefenseDate.Value)).TotalDays;
                    break;
                case Phase.Offer_Phase:
                    if (pInstance.AcceptanceDate.HasValue)
                        time = (pInstance.AcceptanceDate.Value.Subtract(pInstance.OfferDate.Value)).TotalDays;
                    else if (pInstance.RejectionDate.HasValue)
                        time = (pInstance.RejectionDate.Value.Subtract(pInstance.OfferDate.Value)).TotalDays;
                    else
                        time = (pInstance.QuitDate.Value.Subtract(pInstance.OfferDate.Value)).TotalDays;
                    break;
            }

            return time;
        }

    }
}