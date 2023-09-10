using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
	public class SigninInput
	{
		[Display(Name = "Email adresi:")]
		public string Email { get; set; }

		[Display(Name = "Sifre:")]
		public string Password { get; set; }

		[Display(Name = "Beni Hatırla")]
		public bool IsRemember { get; set; }
	}
}