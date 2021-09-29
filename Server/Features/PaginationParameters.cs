using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Features
{
    public class PaginationParameters
    {
        public int Page { set; get; } = 1;
        public int QuantityPerPage { set; get; } = 10;
        public int ServiceType { set; get; } = 1;
        public string ServiceTitle { set; get; }
        public DateTime? BookDate { set; get; }
        public string Address { set; get; }
    }
}
