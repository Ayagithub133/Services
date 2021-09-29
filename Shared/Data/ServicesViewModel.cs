using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class ServicesViewModel
    {
        [Required]
        public int Id { set; get; }

        [Required(ErrorMessage ="اختر نوع الخدمة")]
        [StringLength(100)]
        public string ServiceName { set; get; }
    }
}
