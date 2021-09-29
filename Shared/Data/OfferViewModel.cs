using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class OfferViewModel
    {
        [Required(ErrorMessage ="ضع عرض مناسب لا تترك رسالة فارغة")]
        [StringLength(600)]
        public string TextOffer { set; get; }

        [Required(ErrorMessage = "ضع سعرا مناسب لا تتركه فارغ")]
        public double Budget { set; get; }

        public string UserName { set; get; }
        public string UserImage { set; get; }
        public DateTime AddTime { set; get; }
        public int ServiceID { set; get; }
        public string HallID { set; get; }
        public string UserId { set; get; }

    }
}
