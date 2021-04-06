using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewYearCard.Models
{
    public class Card
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} alanı zorunludur")]
        [MaxLength(50, ErrorMessage = "{0} en fazla {1} karakter içerebilir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} alanı zorunludur")]
        [MaxLength(30, ErrorMessage = "{0} en fazla {1} karakter içerebilir.")]
        public string SenderName { get; set; }

        [Display(Name = "Alıcı")]
        [Required(ErrorMessage = "{0} alanı zorunludur")]
        [MaxLength(50, ErrorMessage = "{0} en fazla {1} karakter içerebilir.")]
        public string ReciverName { get; set; }

        [Display(Name = "Mesaj")]
        [Required(ErrorMessage = "{0} alanı zorunludur")]
        [MaxLength(400, ErrorMessage = "{0} en fazla {1} karakter içerebilir.")]
        public string Message { get; set; }

        public string PhotoPath { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}