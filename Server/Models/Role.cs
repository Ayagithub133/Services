using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Models
{
    public class Role
    {
        [Key]
        public string Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string NormalizedName { set; get; }
        [Required]
        public string ConcurrencyStamp { set; get; }
    }
}
