﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ClientContactAddDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int ClientId { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
    }
}
