using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class WelcomRequestDto
    {
        [Required]
        public string Email { set; get; }

        [Required]
        public string UserName { get; set; }
    }
}
