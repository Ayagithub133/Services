using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class ProfileUser
    {
       
        public string UserName { set; get; }
       
        public string UserImage { set; get; }
        public DateTime CreateAt { set; get; }
        public int ServiceCount { set; get; }
        public int OffersCount { set; get; }
        public int CompleteServiceCount { set; get; }
        public bool isActive { set; get; }
    }
}
