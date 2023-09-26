using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TaskType : BaseEntity
    {
        public int Id { get; set; }
        public string TypeShortName { get; set; }
        public string TypeDescription { get; set; }

    }
}
