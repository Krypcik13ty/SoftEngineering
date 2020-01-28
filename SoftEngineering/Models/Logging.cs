﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoftEngineering.Models
{
    public class Logging
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }

        public string passcheck { get; set; }

        public string Newpass { get; set; }
        public string Newpasscheck { get; set; }
        
    }
}