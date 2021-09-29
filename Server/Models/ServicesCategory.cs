using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Models
{
    public class ServicesCategory 
    {
        [Key]
        public int Id { set; get; }

        [Required]
        [StringLength(100)]
        public string ServiceName { set; get; } 
    }
}
