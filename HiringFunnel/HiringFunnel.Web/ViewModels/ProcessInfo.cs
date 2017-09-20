using System;
using System.ComponentModel.DataAnnotations;
using HiringFunnel.Data;

namespace HiringFunnel.Web.ViewModels
{
    public class ProcessInfo
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Morate uneti naziv.")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "{0} ne sme biti duži od {1} karaktera.")]
        [Display(Name = "Naziv procesa")]
        public string Name { get; set; }
        
        [Display(Name = "Potrebno iskustvo")]
        public string Technologies { get; set; }
        
        [Display(Name = "Senioritet procesa")]
        public SeniorityLevel Seniority { get; set; }
        
        [Display(Name = "Datum početka")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "Datum kraja")]
        public DateTime? EndDate { get; set; }

    }
}