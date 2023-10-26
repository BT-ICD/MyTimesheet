using Core.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ClientContact : BaseEntity
    {
        public int ContactId {  get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int DesignationId { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "COntact name should be 50 characters only.")]
        public string Name { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }
        

        public override string ToString()
        {
            return JsonConvert.ToString(this);
        }

    }
}
