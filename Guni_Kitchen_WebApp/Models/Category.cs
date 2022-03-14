using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guni_Kitchen_WebApp.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [Display(Name = "Category ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Category_Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        [Display(Name = "Category Name")]
        public string Category_Name { get; set; }


        [Display(Name ="Created at")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        #region Navigational Properties => Product Model

        public ICollection<Product> Products { get; set; }

        #endregion
    }
}
