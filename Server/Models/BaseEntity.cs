using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Models
{
    public class BaseEntity
    {
        
        [Key]
        public string Id { set; get; }
    }
   
}
