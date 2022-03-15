using Guni_Kitchen_WebApp.Models;
using Guni_Kitchen_WebApp.Models.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guni_Kitchen_WebApp.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        [Display(Name = "Product ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_Id { get; set; }

        [Display(Name = "Name of the Product")]
        [Required]
        [StringLength(80, ErrorMessage = "{0} cannot have more than {1} characters.")]
        [MinLength(3, ErrorMessage = "{0} should have a minimum of {1} characters.")]
        public string Product_Name { get; set; }

        [Display(Name = "Price per unit")]
        [Required]
        [Range(0.0, 500.00, ErrorMessage = "{0} has to be between Rs. {1} and Rs. {2}")]
        public decimal Product_Price { get; set; }


        [Required]
        [Display(Name = "Unit of Measure")]
        public string UnitOfMeasure { get; set; }


        /*[Required]
        [StringLength(200)]
        [Column(TypeName = "varchar")]
        [Display(Name = "Product Image")]
        public string Product_image { get; set; }
*/
        [Display(Name = "Size")]
        [Column(TypeName = "varchar(20)")]
        public ProductSize Size { get; set; }

        [Display(Name = "Description")]

        public string Product_Description { get; set; }
        [Display(Name ="Product Image")]
        public IFormFile Photo{ get; set; }

        [StringLength(150)]
        public string ProductImageFileUrl { get; set; }

        [StringLength(60)]
        public string ProductImageType { get; set; }



        #region :: Navigational Properties [ Category Model ]

        [ForeignKey(nameof(Product.Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        #endregion ::

        #region :: Navigational Properties => Order Model

        public ICollection<Order> Orders { get; set; }

        #endregion::

    }
}
