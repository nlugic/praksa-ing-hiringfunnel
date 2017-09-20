using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HiringFunnel.Web.ViewModels
{
    public class ToDoItemInfo
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Morate uneti tekst stavke.")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "{0} ne sme biti duža od {1} karaktera.")]
        [Display(Name = "Poruka")]
        public string Message { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Završeno")]
        public bool Completed { get; set; }

        [Display(Name = "Vreme: ")]
        public DateTime Time { get; set; }
        
    }
}