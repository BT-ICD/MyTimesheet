using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; set; }= DateTime.Now;
        public string CreatedBy { get; set; } = "Admin";
        public string CreatedFrom { get; set; } = "::1";
        public bool IsDaleted { get; set; }= false;

        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public string? DeletedFrom { get; set; }

        public DateTime? ModifiedOn { get; set; } 
        public string? ModifiedBy { get; set; }
        public string? ModifiedFrom { get; set; }

    }
}
