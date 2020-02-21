using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SoftEngineering.Models
{
    public class SubjectAdder
    {
        [Required]
        public string NewSubject { get; set; }
        public string TimeSubject { get; set; }
    }
}