using HiringFunnel.Data;

namespace HiringFunnel.Web.ViewModels
{
    public class InterviewerInfo
    {

        // ne koristi se kao viewmodel tako da nema potrebe za anotacijama

        public int Id { get; set; }

        public Phase Type { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        
        public int InterviewerId { get; set; }

    }
}