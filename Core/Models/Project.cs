using Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Project : BaseEntity
    {
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public DateTime InitiatedOn { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

    }
}
