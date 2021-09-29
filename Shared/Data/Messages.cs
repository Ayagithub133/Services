using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class Messages
    {
        
        public string FromUserId { set; get; }
      
        public string ToUserId { set; get; }
           
        [Required(ErrorMessage ="")]

        public string Message { set; get; }
        public DateTime CreatedDate { set; get; }

       

        public virtual AppUser FromUser { set; get; }

        
        public virtual AppUser ToUser { set; get; }
    }
}
