using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Models
{
    public class Category
    {
        [Key]
        public int CategoryRowId { get; set; }
        [Required(ErrorMessage ="Category Id is Must")]
        public string CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name is Must")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Base Price is Must")]
        public int BasePrice { get; set; }
        public ICollection<Product> Products { get; set; }

    }

    public class Product
    {
        [Key]
        public int ProductRowId { get; set; }
        [Required(ErrorMessage = "Product Id Id is Must")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name Id is Must")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Manufacturer Id is Must")]
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = "Product Price Id is Must")]
        public int Price { get; set; }
        [ForeignKey("CategoryRowId")]
        public int CategoryRowId { get; set; }
        public Category Category { get; set; }

    }

    public class NonnegitiveAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int val = Convert.ToInt32(value);
            if(val < 0)
            {
                return false;
            }
            return true;
        }
    }
}
