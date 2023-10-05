using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
	public class SigninInput
	{
		[Required]
		[Display(Name = "Email adresi:")]
		public string Email { get; set; }

        [Required]
        [Display(Name = "Sifre:")]
		public string Password { get; set; }

        [Required]
        [Display(Name = "Beni Hatırla")]
		public bool IsRemember { get; set; }
	}
}