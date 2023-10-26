using Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TaskType : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string TypeShortName { get; set; }

        [Required]
        public string TypeDescription { get; set; }

    }
}
