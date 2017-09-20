using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HiringFunnel.Data;

namespace HiringFunnel.Web.ViewModels
{
    public class ProcessInstanceEditInfo
    {
        
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}")]
        [Display(Name = "Kontakt dodat")]
        public DateTime CreationDate { get; set; }
        
        public Phase? CurrentPhase { get; set; }

        [Display(Name = "Kontaktiran")]
        public bool Contacted { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum kontakta: ")]
        public DateTime? ContactDate { get; set; }

        [Display(Name = "Intervju zakazan")]
        public bool InterviewScheduled { get; set; }
        [Display(Name = "Kandidat obavešten")]
        public bool InterviewNoticed { get; set; }
        [Display(Name = "Intervju održan")]
        public bool InterviewHeld { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum intervjua: ")]
        public DateTime? InterviewDate { get; set; }

        [Display(Name = "Test zakazan")]
        public bool TestScheduled { get; set; }
        [Display(Name = "Kandidat obavešten")]
        public bool TestNoticed { get; set; }
        [Display(Name = "Test održan")]
        public bool TestHeld { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum testa: ")]
        public DateTime? TestDate { get; set; }

        [Display(Name = "Odbrana testa zakazana")]
        public bool TestDefenseScheduled { get; set; }
        [Display(Name = "Kandidat obavešten")]
        public bool TestDefenseNoticed { get; set; }
        [Display(Name = "Odbrana testa održana")]
        public bool TestDefenseHeld { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum odbrane testa: ")]
        public DateTime? TestDefenseDate { get; set; }

        [Display(Name = "Ponuda")]
        public bool OfferSent { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum ponude: ")]
        public DateTime? OfferDate { get; set; }

        [Display(Name = "Kandidat primljen")]
        public bool Accepted { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum prijema: ")]
        public DateTime? AcceptanceDate { get; set; }

        [Display(Name = "Kandidat odbijen")]
        public bool Rejected { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum odbijanja: ")]
        public DateTime? RejectionDate { get; set; }

        [Display(Name = "Kandidat odustao")]
        public bool Quit { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum odustajanja: ")]
        public DateTime? QuitDate { get; set; }
        
        [Display(Name = "Informacije o kandidatu")]
        public ContactInfo ContactInProcess { get; set; }
        [Display(Name = "Informacije o procesu")]
        public ProcessInfo InstanceOf { get; set; }
       
        public ICollection<PInstanceAnnotationInfo> Annotations { get; set; }
        public ICollection<InterviewerInfo> Interviewers { get; set; }
        public ICollection<ToDoItemInfo> ToDoItems { get; set; }

    }
}