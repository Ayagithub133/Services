using Services.Shared.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class HallDetailResponse
    {
        public string UserName { set; get; }
        public string UserImage { set; get; }
        public string ServiceTitle { set; get; }
        public string UserId { set; get; }
        public double Budget { set; get; }
        public string UserContact { set; get; }
        public string UserCity { set; get; }
        public int GuestNumber { set; get; }
        public Menue Menue { set; get; }
        public string HallDescription { set; get; }
        public DateTime BookDate { set; get; }
        public string Id { set; get; }
        public IEnumerable<OfferViewModel> OffersPosted { set; get; }
    }
}

