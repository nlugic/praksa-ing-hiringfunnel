using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HiringFunnel.Data;

namespace HiringFunnel.Web.ViewModels
{
    public class UserInfo
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

        [Required(ErrorMessage = "Morate uneti e-mail adresu.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Morate uneti validnu e-mail adresu.")]
        [EmailAddress(ErrorMessage = "Morate uneti validnu e-mail adresu.")]
        [Display(Name = "E-mail adresa")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Morate uneti lozinku.")]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "{0} mora biti {2}-{1} karaktera duga.", MinimumLength = 8)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lozinka i njena potvrda se ne podudaraju.")]
        [Display(Name = "Potvrdi lozinku")]
        public string ConfirmPassword { get; set; }
        
        [Display(Name = "Senioritet")]
        public SeniorityLevel Seniority { get; set; }

        [Display(Name = "Uloga")]
        public UserLevel Role { get; set; }

        [Display(Name = "Iskustvo")]
        public ICollection<TechnologyInfo> Technologies { get; set; }

    }
}