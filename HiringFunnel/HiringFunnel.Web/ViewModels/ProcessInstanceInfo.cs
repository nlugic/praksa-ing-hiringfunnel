using System.Collections.Generic;

namespace HiringFunnel.Web.ViewModels
{
    public class ProcessInstanceInfo
    {

        // ne koristi se kao viewmodel tako da nema potrebe za anotacijama

        public int Id { get; set; }
        
        public string ProcessName { get; set; }

        public string ContactName { get; set; }

        public string ProcessSeniority { get; set; }

        public string ProcessTechnologies { get; set; }

        public string ContactSeniority { get; set; }

        public IEnumerable<string> ContactTechnologies { get; set; }

    }
}