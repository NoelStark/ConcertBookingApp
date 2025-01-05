using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [NotMapped]
        public string CreditCardType { get; set; } = string.Empty;
        [NotMapped]
        public string CreditCardNumber { get; set; } = string.Empty;
    }
}
