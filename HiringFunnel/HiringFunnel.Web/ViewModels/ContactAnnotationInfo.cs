using System;
using System.ComponentModel.DataAnnotations;

namespace HiringFunnel.Web.ViewModels
{
    public class ContactAnnotationInfo
    {

        public int Id { get; set; }
        
        [Required(ErrorMessage = "Morate uneti tekst komentara.")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "{0} ne sme biti duži od {1} karaktera.")]
        [Display(Name = "Komentar")]
        public string Message { get; set; }
        
        public DateTime Time { get; set; }
        
        [Display(Name = "Sakrij komentar")]
        public bool Hidden { get; set; }

        [Display(Name = "Autor")]
        public UserInfo Author { get; set; }

    }
}