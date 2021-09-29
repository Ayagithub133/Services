using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class MailRequestDto
    {
        [Required]
        public string ToEmail { set; get; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public List<IFormFile> Attatchments { get; set; }
    }
}
