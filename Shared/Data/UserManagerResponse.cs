using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { set; get; }
      //  public IEnumerable<string> Errors { set; get; }
        public DateTime? ExpireDate { set; get; }
        public string Id { set; get; }
    }
}
