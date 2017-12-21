﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Take02.Models
{
    public class ComponentType
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
