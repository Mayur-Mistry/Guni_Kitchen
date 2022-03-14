using System.ComponentModel.DataAnnotations;

namespace Guni_Kitchen_WebApp.Models.Enums
{
    public class MyIdentityGenders
    {
        public enum Gender
        {
            [Display(Name = "Male")]
            Male,
            [Display(Name = "Female")]
            Female,
            [Display(Name = "Other")]
            Other
        }
    }
}
