using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusiCodeWebApp.Models.ViewModels
{
    public class PaymentViewModel
    {
        [Required(ErrorMessage = "Bu alan zorunlu")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage ="Bu alan zorunlu")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Bu alan zorunlu")]
        public string ReqMonth { get; set; }

        [Required(ErrorMessage = "Bu alan zorunlu")]
        public string ReqYear { get; set; }

        [Required(ErrorMessage = "Bu alan zorunlu")]
        public string Cvv { get; set; }
    }
}