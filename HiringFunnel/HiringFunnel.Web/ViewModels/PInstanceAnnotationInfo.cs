using System;
using System.ComponentModel.DataAnnotations;
using HiringFunnel.Data;

namespace HiringFunnel.Web.ViewModels
{
    public class PInstanceAnnotationInfo
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Morate uneti tekst komentara.")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "{0} ne sme biti duža od {1} karaktera.")]
        [Display(Name = "Poruka")]
        public string Message { get; set; }
        
        public DateTime Time { get; set; }

        public Phase Type { get; set; }

        [Display(Name = "Sakrij komentar")]
        public bool Hidden { get; set; }

        [Display(Name = "Autor: ")]
        public UserInfo Author { get; set; }

    }
}