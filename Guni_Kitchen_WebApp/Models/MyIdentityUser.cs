using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Guni_Kitchen_WebApp.Models.Enums.MyIdentityGenders;

namespace Guni_Kitchen_WebApp.Models
{

    public class MyIdentityUser
                    : IdentityUser<Guid>
    {
        /*private string phoneNumber;*/

        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [MinLength(2, ErrorMessage = "{0} should have at least {1} characters.")]
        [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters.")]
        public string DisplayName { get; set; }

       
        [Display(Name = "Date of Birth")]
        [Required]
        [PersonalData]
        [Column(TypeName = "smalldatetime")]
        [AgeValidation]
        public DateTime DateOfBirth { get; set; }


        [Display(Name = "Gender")]
        [Required]
        [PersonalData]
        public Gender Genders { get; set; }

        [Display(Name = "Is Admin User?")]
        [Required]
        public bool IsAdminUser { get; set; }

        #region :: Navigational Properties => Order Model

        public ICollection<Order> Orders { get; set; }

        #endregion::
    }
}


public class AgeValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        int givenAge = int.Parse(value.ToString());
        if (givenAge < 18)
        {
            return new ValidationResult("The age must be greater than 18");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}