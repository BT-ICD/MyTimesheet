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
    public class Designation: BaseEntity
    {
        public int DesignationId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Designation name should be 50 characters only.")]
        public string DesignationName { get; set; }

        public override string ToString()
        {
            return JsonConvert.ToString(this);
        }
    }
}
