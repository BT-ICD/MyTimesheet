using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ProjectEditDTO
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public DateTime InitiatedOn { get; set; }
    
        public string ClientName { get; set; }
    }
}
