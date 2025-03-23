using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusiCodeWebApp.Models
{
    public class Product:Entity
    {
        public Product()
        {
            IsDeleted = false;
            IsRecent = false;
            IsActive = true;
        }

        public int Category_ID { get; set; }

        [ForeignKey("Category_ID")]
        public virtual Category Category { get; set; }

        public int Brand_ID { get; set; }

        [ForeignKey("Brand_ID")]
        public virtual Brand Brand { get; set; }

        [Required(ErrorMessage ="Bu alan boş bırakılamaz")]
        [StringLength(maximumLength:250, ErrorMessage ="Bu alan en fazla 250 karakter olabilir")]
        public string Name { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        //[DisplayFormat(DataFormatString ="{0:n00}")]
        public decimal Price { get; set; }

        public string Image { get; set; }

        public short Stock { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-mm-yyyy}", ApplyFormatInEditMode =true)]
        public DateTime CreationTime { get; set; }

        public bool IsActive { get; set; }

        public bool IsRecent { get; set; }

        public virtual ICollection<Favorite> Favorities { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}