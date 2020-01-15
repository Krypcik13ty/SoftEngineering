using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoftEngineering.Models
{
    public class MailSender
    {
        [Required]
        public string mailadress { get; set; }
        public string mailsubject { get; set; }
        public string mailtext { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

    }
}