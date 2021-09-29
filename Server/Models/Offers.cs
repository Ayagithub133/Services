using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Models
{
    public class Offers : BaseEntity
    {
        [Required]
        [StringLength(600)]
        public string TextOffer { set; get; }
        
        [Required]
        public DateTime AddTime { set; get; }
        
        [Required]
        public double Budget { set; get; }

        [ForeignKey("User")]
        public string UserId { set; get; }
        public ApplicationUser User { set; get; }


        [ForeignKey("_Hall")]
        public string HallID { set; get; }
        public Hall _Hall { set; get; }


        [ForeignKey("Service")]
        public int ServiceID { set; get; }
        public ServicesCategory Service { set; get; }

    }
}
