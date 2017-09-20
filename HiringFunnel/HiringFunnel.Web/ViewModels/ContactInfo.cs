using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HiringFunnel.Data;

namespace HiringFunnel.Web.ViewModels
{
    public class ContactInfo
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Morate uneti ime.")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "{0} ne sme biti duže od {1} karaktera.")]
        [Display(Name = "Ime")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Morate uneti prezime.")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "{0} ne sme biti duže od {1} karaktera.")]
        [Display(Name = "Prezime")]
        public string LastName { get; set; }
        
        //[DataType(DataType.Date, ErrorMessage = "Morate uneti datum u pravilnom formatu.")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum rođenja")]
        public DateTime DateOfBirth { get; set; }
        
        [DataType(DataType.MultilineText)]
        [StringLength(100, ErrorMessage = "{0} ne sme biti duža od {1} karaktera.")]
        [Display(Name = "Lokacija")]
        public string Location { get; set; }
        
        [DataType(DataType.EmailAddress, ErrorMessage = "Morate uneti validnu e-mail adresu.")]
        [EmailAddress(ErrorMessage = "Morate uneti validnu e-mail adresu.")]
        [Display(Name = "E-mail adresa")]
        public string Email { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Broj telefona")]
        public string Phone { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "LinkedIn nalog")]
        public string LinkedIn { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "{0} ne sme biti duže od {1} karaktera.")]
        [DefaultValue("Nezaposlen")]
        [Display(Name = "Radno mesto")]
        public string CurrentWorkplace { get; set; }
        
        [DataType(DataType.Upload)]
        [Display(Name = "CV")]
        public byte[] CVContent { get; set; }

        [Display(Name = "Senioritet")]
        public SeniorityLevel Seniority { get; set; }

        [Display(Name = "Iskustvo")]
        public ICollection<TechnologyInfo> Technologies { get; set; }

        [Display(Name = "Komentari")]
        public ICollection<ContactAnnotationInfo> Comments { get; set; }
        
        public bool Deleted { get; set; }

    }
}