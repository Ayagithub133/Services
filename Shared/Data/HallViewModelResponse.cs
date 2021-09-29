using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class HallViewModelResponse
    {
        public string UserName { set; get; }
        public string UserImage { set; get; }
        public int DurationPublish { set; get; }
        public string UserId { set; get; }
        public DateTime TimeAdd { set; get; }
        public string HallDescription { set; get; }
        public string ServiceTitle { set; get; }
        public string Id { set; get; }
    }
}
