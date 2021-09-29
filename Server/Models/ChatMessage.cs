using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Models
{
    public class ChatMessage : BaseEntity
    {

      [ForeignKey("FromUser ")]
      public string FromUserId { set; get; }
      public virtual ApplicationUser FromUser { set; get; }

      [ForeignKey("ToUser")]
      public string ToUserId { set; get; }
      public virtual ApplicationUser ToUser { set; get; }

      [Required]
      
      public string Message { set; get; }

      
      public DateTime CreateDate { set; get; }
    }
}
