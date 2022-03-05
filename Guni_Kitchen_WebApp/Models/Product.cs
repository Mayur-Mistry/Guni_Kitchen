using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guni_Kitchen_WebApp.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [Display(Name = "Product ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar")]
        [Display(Name = "Product Name")]
        public string Product_Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Product Price")]

        public int Product_Price { get; set; }

        [Required]
        [StringLength(200)]
        [Column(TypeName = "varchar")]
        [Display(Name = "Product Image")]
        public string Product_image { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar")]
        [Display(Name = "Size")]
        public string Product_Size{ get; set; }


        [Required]
        [StringLength(500,MinimumLength = 5)]
        [Column(TypeName = "varchar")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Product_Description { get; set; }


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
