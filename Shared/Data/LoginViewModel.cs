using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class LoginViewModel
    {
        [Phone]
        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "لاتدخل رقم اقل من تسع ارقام")]
        public string PhoneNumber { set; get; }

        [Required(ErrorMessage = "كلمه المرور مطلوبة")]
        [MinLength(7, ErrorMessage = "لا يسمح بأقل من 7 ارقام أو رموز أو حروف")]
        public string Password { set; get; }

        public bool isActive { set; get; }
    }
}
