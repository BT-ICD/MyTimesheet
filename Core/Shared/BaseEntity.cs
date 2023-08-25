using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedFrom { get; set; }
        public bool IsDaleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public string? DeletedFrom { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedFrom { get; set; }

    }
}
