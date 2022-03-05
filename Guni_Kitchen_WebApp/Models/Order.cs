using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guni_Kitchen_WebApp.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Display(Name="Order Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Order_Id { get; set; }


        [Required]
        [Display(Name = "Quantity")]
        public int Order_Quantity { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Total")]
        public int Order_Total { get; set; }


        [Required]
        [Display(Name = "Order Modes")]
        public O_Modes Order_Modes { get; set; }
        /*
                [Required]
                [StringLength(200)]
                [Column(TypeName = "varchar")]
                [Display(Name = "Image")]
                public string Cart_T { get; set; }*/

        [Required]
        [DefaultValue(false)]
        [Display(Name = "Order Status")]
        public bool Order_Status { get; set; }

        /*User_Id,Product_ID*/

        #region :: Navigational Properties [ MyIdentityUser Model ]

        [ForeignKey(nameof(Order.Users))]
        public Guid Id { get; set; }

        public MyIdentityUser Users { get; set; }

        #endregion ::

        #region :: Navigational Properties [ Product Model ]

        [ForeignKey(nameof(Order.Products))]
        public int Product_Id { get; set; }

        public Product Products { get; set; }

        #endregion ::
    }
}
public enum O_Modes {
    [Display(Name = "Cash on Delivery")]
    COD,
    [Display(Name = "Pickup at store")]
    PAS
}