using Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ClientContacts : BaseEntity
    {
        public int ContactId {  get; set; }
        public int ClientId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Designation name should be 50 characters only.")]
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int DesignationId { get; set; }
     
    }
}
