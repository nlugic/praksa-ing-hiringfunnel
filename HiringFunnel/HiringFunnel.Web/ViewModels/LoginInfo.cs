using System.ComponentModel.DataAnnotations;

namespace HiringFunnel.Web.ViewModels
{
    public class LoginInfo
    {

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

    }
}