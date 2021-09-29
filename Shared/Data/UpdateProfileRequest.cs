using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Data
{
    public class UpdateProfileRequest
    {
        [Required (ErrorMessage ="ادخل الاسم الاول")]
        public string FirstName { set; get; }

        [Required(ErrorMessage = "ادخل الاسم الثانى")]
        public string SecondName { set; get; }

        [Required(ErrorMessage = "ادخل البريد الالكترونى")]
        [EmailAddress]
        public string Email { set; get; }

        [Required(ErrorMessage = "ادخل رقم الهاتف")]
        [Phone]
        public string PhoneNumber { set; get; }

        [Required(ErrorMessage = "ادخل كلمة المرور لتعديل الحساب")]
        public string Password { set; get; }

        [Required(ErrorMessage = "كم بتأكيد كلمة المرور")]
        [Compare(nameof(Password),ErrorMessage ="كلمة المرور وتأكيد كلمة المرور غير متطابقان")]
        public string ConfirmPassword { set; get; }

        
        public string Image { set; get; }
    }
}
