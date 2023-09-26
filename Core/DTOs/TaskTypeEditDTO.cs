using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class TaskTypeEditDTO
    {
        public int Id { get; set; }
        public string TypeShortName { get; set; }
        public string TypeDescription { get; set; }
    }
}
