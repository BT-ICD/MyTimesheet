﻿using Core.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TeamMember : BaseEntity
    {
        public int TeamMemberId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Notes { get; set; }

        [Required]
        public string AlternateContact { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public DateTime DOJ { get; set; }

        [Required]
        public int DesignationId { get; set; }

        [ForeignKey("DesignationId")]
        public Designation designation { get; set; }

        public override string ToString()
        {
            return JsonConvert.ToString(this);
        }


    }
}
