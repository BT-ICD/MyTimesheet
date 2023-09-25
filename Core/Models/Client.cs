using Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Client:BaseEntity
    {
        public int ClientId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Client name should be 50 characters only.")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }
}
