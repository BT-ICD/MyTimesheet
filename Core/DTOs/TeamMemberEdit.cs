using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class TeamMemberEdit
    {
        public int TeamMemberId { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Notes { get; set; }

        public string AlternateContact { get; set; }

        public DateTime DOB { get; set; }

        public DateTime DOJ { get; set; }

        public int DesignationId { get; set; }

    }
}
